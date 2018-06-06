// Decompiled with JetBrains decompiler
// Type: SRPG.RaidResultWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "スキップ", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(2, "プレイヤー経験値の獲得演出開始", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(1000, "終了", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1, "ドロップアイテムの獲得演出開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(200, "プレイヤー経験値の獲得演出終了", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(300, "ファントム経験値の獲得演出終了", FlowNode.PinTypes.Output, 300)]
  [FlowNode.Pin(3, "ファントム経験値の獲得演出開始", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(100, "ドロップアイテムの獲得演出終了", FlowNode.PinTypes.Output, 100)]
  public class RaidResultWindow : SRPG_ListBase, IFlowInterface
  {
    [Description("レベルアップ時に使用するトリガー。ゲームオブジェクトにアタッチされたAnimatorへ送られます。")]
    public string LevelUpTrigger = "levelup";
    [Description("一秒あたりの経験値の増加量")]
    public float ExpGainRate = 100f;
    [Description("経験値増加アニメーションの最長時間。経験値がExpGainRateの速度で増加する時、これで設定した時間を超える時に加算速度を上げる。")]
    public float ExpGainTimeMax = 2f;
    public float ResultScrollInterval = 1f;
    [Range(0.1f, 10f)]
    public float SkipTimeScale = 2f;
    private List<GameObject> mResults = new List<GameObject>();
    private List<GameObject> mUnitListItems = new List<GameObject>();
    public ScrollRect ResultLayout;
    public Transform ResultParent;
    public GameObject ResultTemplate;
    public Button BtnUp;
    public Button BtnDown;
    public Button BtnOutSide;
    public Button BtnGainExpOutSide;
    public GameObject GainExpWindow;
    public GameObject PlayerResult;
    public Slider PlayerGauge;
    public Text TxtPlayerLvVal;
    public Text TxtPlayerExpVal;
    public Text TxtGainGoldVal;
    public GameObject UnitList;
    public GameObject UnitListItem;
    public Button SkipButton;
    private RaidResult mRaidResult;
    private RaidResultElement mCurrentElement;
    public int[] AcquiredUnitExp;

    protected override ScrollRect GetScrollRect()
    {
      return this.ResultLayout;
    }

    protected override RectTransform GetRectTransform()
    {
      return this.ResultParent as RectTransform;
    }

    protected override void Start()
    {
      base.Start();
      if (Object.op_Inequality((Object) this.ResultTemplate, (Object) null))
        this.ResultTemplate.SetActive(false);
      if (Object.op_Implicit((Object) this.UnitListItem))
        this.UnitListItem.SetActive(false);
      if (Object.op_Inequality((Object) this.BtnUp, (Object) null))
        ((Selectable) this.BtnUp).set_interactable(false);
      if (Object.op_Inequality((Object) this.BtnDown, (Object) null))
        ((Selectable) this.BtnUp).set_interactable(false);
      if (Object.op_Inequality((Object) this.BtnOutSide, (Object) null))
        ((Selectable) this.BtnOutSide).set_interactable(false);
      if (Object.op_Inequality((Object) this.BtnGainExpOutSide, (Object) null))
        ((Selectable) this.BtnGainExpOutSide).set_interactable(false);
      if (Object.op_Inequality((Object) this.ResultLayout, (Object) null))
        ((Behaviour) this.ResultLayout).set_enabled(false);
      if (Object.op_Inequality((Object) this.GainExpWindow, (Object) null))
        this.GainExpWindow.SetActive(false);
      this.mRaidResult = GlobalVars.RaidResult;
      if (this.mRaidResult != null)
      {
        this.ApplyQuestCampaignParams(this.mRaidResult.campaignIds);
        if (Object.op_Inequality((Object) this.ResultTemplate, (Object) null))
        {
          for (int index = 0; index < this.mRaidResult.results.Count; ++index)
          {
            GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ResultTemplate);
            gameObject.get_transform().SetParent(this.ResultParent, false);
            DataSource.Bind<RaidQuestResult>(gameObject, this.mRaidResult.results[index]);
            ListItemEvents component = (ListItemEvents) gameObject.GetComponent<ListItemEvents>();
            if (Object.op_Inequality((Object) component, (Object) null))
              this.AddItem(component);
            this.mResults.Add(gameObject);
          }
        }
        if (Object.op_Inequality((Object) this.UnitListItem, (Object) null))
        {
          Transform transform = !Object.op_Inequality((Object) this.UnitList, (Object) null) ? this.UnitListItem.get_transform().get_parent() : this.UnitList.get_transform();
          for (int index = 0; index < this.mRaidResult.members.Count; ++index)
          {
            GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.UnitListItem);
            gameObject.get_transform().SetParent(transform, false);
            this.mUnitListItems.Add(gameObject);
            DataSource.Bind<UnitData>(gameObject, this.mRaidResult.members[index]);
            gameObject.SetActive(true);
          }
        }
        if (Object.op_Inequality((Object) this.TxtGainGoldVal, (Object) null))
          this.TxtGainGoldVal.set_text(this.mRaidResult.gold.ToString());
      }
      GlobalVars.RaidResult = (RaidResult) null;
      GlobalVars.RaidNum = 0;
      if (!Object.op_Inequality((Object) this.SkipButton, (Object) null))
        return;
      ((Component) this.SkipButton).get_gameObject().SetActive(false);
    }

    private void ApplyQuestCampaignParams(string[] campaignIds)
    {
      this.AcquiredUnitExp = new int[this.mRaidResult.members.Count];
      if (campaignIds != null)
      {
        QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.GetInstanceDirect().FindQuestCampaigns(campaignIds);
        List<UnitData> members = this.mRaidResult.members;
        float[] numArray = new float[members.Count];
        float num1 = 1f;
        for (int index = 0; index < numArray.Length; ++index)
          numArray[index] = 1f;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        RaidResultWindow.\u003CApplyQuestCampaignParams\u003Ec__AnonStorey26C paramsCAnonStorey26C = new RaidResultWindow.\u003CApplyQuestCampaignParams\u003Ec__AnonStorey26C();
        foreach (QuestCampaignData questCampaignData in questCampaigns)
        {
          // ISSUE: reference to a compiler-generated field
          paramsCAnonStorey26C.data = questCampaignData;
          // ISSUE: reference to a compiler-generated field
          if (paramsCAnonStorey26C.data.type == QuestCampaignValueTypes.ExpUnit)
          {
            // ISSUE: reference to a compiler-generated field
            if (string.IsNullOrEmpty(paramsCAnonStorey26C.data.unit))
            {
              // ISSUE: reference to a compiler-generated field
              num1 = paramsCAnonStorey26C.data.GetRate();
            }
            else
            {
              // ISSUE: reference to a compiler-generated method
              int index = members.FindIndex(new Predicate<UnitData>(paramsCAnonStorey26C.\u003C\u003Em__2D3));
              if (index >= 0)
              {
                // ISSUE: reference to a compiler-generated field
                numArray[index] = paramsCAnonStorey26C.data.GetRate();
              }
            }
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (paramsCAnonStorey26C.data.type == QuestCampaignValueTypes.ExpPlayer)
            {
              // ISSUE: reference to a compiler-generated field
              this.mRaidResult.pexp = Mathf.RoundToInt((float) this.mRaidResult.pexp * paramsCAnonStorey26C.data.GetRate());
            }
          }
        }
        int uexp = this.mRaidResult.uexp;
        for (int index = 0; index < numArray.Length; ++index)
        {
          float num2 = 1f;
          if ((double) num1 != 1.0 && (double) numArray[index] != 1.0)
            num2 = num1 + numArray[index];
          else if ((double) num1 != 1.0)
            num2 = num1;
          else if ((double) numArray[index] != 1.0)
            num2 = numArray[index];
          this.AcquiredUnitExp[index] = Mathf.RoundToInt((float) uexp * num2);
        }
      }
      else
      {
        for (int index = 0; index < this.AcquiredUnitExp.Length; ++index)
          this.AcquiredUnitExp[index] = this.mRaidResult.uexp;
      }
    }

    public void Activated(int pinID)
    {
      if (pinID == 1)
      {
        if (Object.op_Inequality((Object) this.SkipButton, (Object) null))
          ((Component) this.SkipButton).get_gameObject().SetActive(true);
        this.StartCoroutine(this.QuestResultAnimation());
      }
      if (pinID == 2)
        this.StartCoroutine(this.GainPlayerExpAnimation());
      if (pinID == 3)
        this.StartCoroutine(this.GainUnitExpAnimation());
      if (pinID != 10 || Object.op_Equality((Object) this.SkipButton, (Object) null) || Object.op_Equality((Object) this.mCurrentElement, (Object) null))
        return;
      this.mCurrentElement.TimeScale = this.SkipTimeScale;
    }

    [DebuggerHidden]
    private IEnumerator QuestResultAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RaidResultWindow.\u003CQuestResultAnimation\u003Ec__IteratorD0() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator GainPlayerExpAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RaidResultWindow.\u003CGainPlayerExpAnimation\u003Ec__IteratorD1() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator GainUnitExpAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RaidResultWindow.\u003CGainUnitExpAnimation\u003Ec__IteratorD2() { \u003C\u003Ef__this = this };
    }
  }
}
