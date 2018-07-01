// Decompiled with JetBrains decompiler
// Type: SRPG.RaidResultWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(11, "アイテムスキップHoldOff", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(1, "ドロップアイテムの獲得演出開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "プレイヤー経験値の獲得演出開始", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "ファントム経験値の獲得演出開始", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(1000, "終了", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(300, "ファントム経験値の獲得演出終了", FlowNode.PinTypes.Output, 300)]
  [FlowNode.Pin(200, "プレイヤー経験値の獲得演出終了", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(100, "ドロップアイテムの獲得演出終了", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(20, "経験値演出スキップ", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(10, "アイテムスキップHoldOn", FlowNode.PinTypes.Input, 10)]
  public class RaidResultWindow : SRPG_ListBase, IFlowInterface
  {
    [Description("レベルアップ時に使用するトリガー。ゲームオブジェクトにアタッチされたAnimatorへ送られます。")]
    public string LevelUpTrigger = "levelup";
    [Description("一秒あたりの経験値の増加量")]
    public float ExpGainRate = 100f;
    [Description("経験値増加アニメーションの最長時間。経験値がExpGainRateの速度で増加する時、これで設定した時間を超える時に加算速度を上げる。")]
    public float ExpGainTimeMax = 2f;
    public float ResultScrollInterval = 1f;
    [Description("経験値増加アニメーションスキップの倍速設定")]
    public float ExpSkipSpeedMul = 10f;
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
    [Description("入手アイテムのリストになる親ゲームオブジェクト")]
    public GameObject TreasureList;
    [Description("入手アイテムのゲームオブジェクト")]
    public GameObject TreasureListItem;
    [Description("入手真理念装のゲームオブジェクト")]
    public GameObject TreasureListConceptCard;
    [Description("入新規取得アイテムのバッジ")]
    public GameObject NewItemBadge;
    public GameObject GainExpWindow;
    public GameObject PlayerResult;
    public Slider PlayerGauge;
    public Text TxtPlayerLvVal;
    public Text TxtPlayerExpVal;
    public Text TxtGainGoldVal;
    public GameObject UnitList;
    public GameObject UnitListItem;
    public Button SkipButton;
    public Button ExpSkipButton;
    private RaidResult mRaidResult;
    private RaidResultElement mCurrentElement;
    private bool mItemSkipElement;
    private bool mExpSkipElement;
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ResultTemplate, (UnityEngine.Object) null))
        this.ResultTemplate.SetActive(false);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.UnitListItem))
        this.UnitListItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnUp, (UnityEngine.Object) null))
        ((Selectable) this.BtnUp).set_interactable(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnDown, (UnityEngine.Object) null))
        ((Selectable) this.BtnUp).set_interactable(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnOutSide, (UnityEngine.Object) null))
        ((Selectable) this.BtnOutSide).set_interactable(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnGainExpOutSide, (UnityEngine.Object) null))
        ((Selectable) this.BtnGainExpOutSide).set_interactable(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ResultLayout, (UnityEngine.Object) null))
        ((Behaviour) this.ResultLayout).set_enabled(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GainExpWindow, (UnityEngine.Object) null))
        this.GainExpWindow.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureListItem, (UnityEngine.Object) null))
        this.TreasureListItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureListConceptCard, (UnityEngine.Object) null))
        this.TreasureListConceptCard.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NewItemBadge, (UnityEngine.Object) null))
        this.NewItemBadge.SetActive(false);
      this.mRaidResult = GlobalVars.RaidResult;
      if (this.mRaidResult != null)
      {
        this.ApplyQuestCampaignParams(this.mRaidResult.campaignIds);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ResultTemplate, (UnityEngine.Object) null))
        {
          for (int index = 0; index < this.mRaidResult.results.Count; ++index)
          {
            GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ResultTemplate);
            gameObject.get_transform().SetParent(this.ResultParent, false);
            DataSource.Bind<RaidQuestResult>(gameObject, this.mRaidResult.results[index]);
            ListItemEvents component = (ListItemEvents) gameObject.GetComponent<ListItemEvents>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              this.AddItem(component);
            this.mResults.Add(gameObject);
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitListItem, (UnityEngine.Object) null))
        {
          Transform transform = !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitList, (UnityEngine.Object) null) ? this.UnitListItem.get_transform().get_parent() : this.UnitList.get_transform();
          for (int index = 0; index < this.mRaidResult.members.Count; ++index)
          {
            GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.UnitListItem);
            gameObject.get_transform().SetParent(transform, false);
            this.mUnitListItems.Add(gameObject);
            DataSource.Bind<UnitData>(gameObject, this.mRaidResult.members[index]);
            gameObject.SetActive(true);
          }
        }
        this.CreateDropItemObjects(this.MergeDropItems(this.mRaidResult));
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtGainGoldVal, (UnityEngine.Object) null))
          this.TxtGainGoldVal.set_text(this.mRaidResult.gold.ToString());
      }
      GlobalVars.RaidResult = (RaidResult) null;
      GlobalVars.RaidNum = 0;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SkipButton, (UnityEngine.Object) null))
        ((Component) this.SkipButton).get_gameObject().SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExpSkipButton, (UnityEngine.Object) null))
        return;
      ((Component) this.ExpSkipButton).get_gameObject().SetActive(false);
    }

    private QuestResult.DropItemData[] MergeDropItems(RaidResult raidResult)
    {
      if (raidResult == null)
        return new QuestResult.DropItemData[0];
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      List<QuestResult.DropItemData> dropItemDataList = new List<QuestResult.DropItemData>();
      using (List<RaidQuestResult>.Enumerator enumerator = raidResult.results.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          RaidQuestResult current = enumerator.Current;
          if (current != null)
          {
            foreach (QuestResult.DropItemData drop in current.drops)
            {
              if (drop != null)
              {
                bool flag = false;
                for (int index = 0; index < dropItemDataList.Count; ++index)
                {
                  if (dropItemDataList[index].IsItem && drop.IsItem && dropItemDataList[index].itemParam == drop.itemParam)
                  {
                    dropItemDataList[index].Gain(drop.Num);
                    flag = true;
                    break;
                  }
                  if (dropItemDataList[index].IsConceptCard && drop.IsConceptCard && dropItemDataList[index].conceptCardParam == drop.conceptCardParam)
                  {
                    dropItemDataList[index].Gain(drop.Num);
                    flag = true;
                    break;
                  }
                }
                if (!flag)
                {
                  QuestResult.DropItemData dropItemData = new QuestResult.DropItemData();
                  if (drop.IsItem)
                  {
                    dropItemData.SetupDropItemData(EBattleRewardType.Item, 0L, drop.itemParam.iname, drop.Num);
                    if (drop.itemParam.type != EItemType.Unit)
                    {
                      ItemData itemDataByItemParam = player.FindItemDataByItemParam(drop.itemParam);
                      dropItemData.IsNew = !player.ItemEntryExists(drop.itemParam.iname) || (itemDataByItemParam == null || itemDataByItemParam.IsNew);
                    }
                    else
                    {
                      string iid = drop.itemParam.iname;
                      if (player.Units.Find((Predicate<UnitData>) (p => p.UnitParam.iname == iid)) == null)
                        dropItemData.IsNew = true;
                    }
                  }
                  else if (drop.IsConceptCard)
                    dropItemData.SetupDropItemData(EBattleRewardType.ConceptCard, 0L, drop.conceptCardParam.iname, drop.Num);
                  dropItemDataList.Add(dropItemData);
                }
              }
            }
          }
        }
      }
      return dropItemDataList.ToArray();
    }

    private void CreateDropItemObjects(QuestResult.DropItemData[] items)
    {
      Transform transform1 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureList, (UnityEngine.Object) null) ? this.TreasureListItem.get_transform().get_parent() : this.TreasureList.get_transform();
      foreach (QuestResult.DropItemData data in items)
      {
        GameObject root = (GameObject) null;
        if (data.IsConceptCard)
        {
          root = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.TreasureListConceptCard);
          root.get_transform().SetParent(transform1, false);
          DataSource.Bind<QuestResult.DropItemData>(root, data);
          root.SetActive(true);
          GameParameter.UpdateAll(root);
        }
        else if (data.IsItem)
        {
          root = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.TreasureListItem);
          root.get_transform().SetParent(transform1, false);
          DataSource.Bind<ItemData>(root, (ItemData) data);
          root.SetActive(true);
          GameParameter.UpdateAll(root);
        }
        else
          DebugUtility.LogError(string.Format("[コードの追加が必要] DropItemData.mBattleRewardType(={0})は不明な列挙です", (object) data.BattleRewardType));
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NewItemBadge, (UnityEngine.Object) null) && data.IsNew)
        {
          RectTransform transform2 = ((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.NewItemBadge)).get_transform() as RectTransform;
          ((Component) transform2).get_gameObject().SetActive(true);
          transform2.set_anchoredPosition(Vector2.get_zero());
          ((Transform) transform2).SetParent(root.get_transform(), false);
        }
      }
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
        foreach (QuestCampaignData questCampaignData in questCampaigns)
        {
          QuestCampaignData data = questCampaignData;
          if (data.type == QuestCampaignValueTypes.ExpUnit)
          {
            if (string.IsNullOrEmpty(data.unit))
            {
              num1 = data.GetRate();
            }
            else
            {
              int index = members.FindIndex((Predicate<UnitData>) (value => value.UnitParam.iname == data.unit));
              if (index >= 0)
                numArray[index] = data.GetRate();
            }
          }
          else if (data.type == QuestCampaignValueTypes.ExpPlayer)
            this.mRaidResult.pexp = Mathf.RoundToInt((float) this.mRaidResult.pexp * data.GetRate());
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
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SkipButton, (UnityEngine.Object) null))
          ((Component) this.SkipButton).get_gameObject().SetActive(true);
        this.StartCoroutine(this.QuestResultAnimation());
      }
      if (pinID == 2)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExpSkipButton, (UnityEngine.Object) null))
          ((Component) this.ExpSkipButton).get_gameObject().SetActive(true);
        this.StartCoroutine(this.GainPlayerExpAnimation());
      }
      if (pinID == 3)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExpSkipButton, (UnityEngine.Object) null))
          ((Component) this.ExpSkipButton).get_gameObject().SetActive(true);
        this.StartCoroutine(this.GainUnitExpAnimation());
      }
      if (pinID == 10)
        this.mItemSkipElement = true;
      if (pinID == 11)
        this.mItemSkipElement = false;
      if (pinID != 20)
        return;
      this.mExpSkipElement = true;
    }

    [DebuggerHidden]
    private IEnumerator QuestResultAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RaidResultWindow.\u003CQuestResultAnimation\u003Ec__Iterator137()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator GainPlayerExpAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RaidResultWindow.\u003CGainPlayerExpAnimation\u003Ec__Iterator138()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator GainUnitExpAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RaidResultWindow.\u003CGainUnitExpAnimation\u003Ec__Iterator139()
      {
        \u003C\u003Ef__this = this
      };
    }
  }
}
