// Decompiled with JetBrains decompiler
// Type: SRPG.BeginnerTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(4, "Open Tab Practice", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Refresh", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "Open Tab Basic", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(5, "Open Tab Banner", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(10, "Select Tips", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "Select Practice", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "Select Banner", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(100, "Reset Status", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(10000, "Tips Detail", FlowNode.PinTypes.Output, 10000)]
  [FlowNode.Pin(10001, "Go To Quest", FlowNode.PinTypes.Output, 10001)]
  public class BeginnerTop : MonoBehaviour, IFlowInterface
  {
    private const int START = 1;
    private const int REFRESH = 2;
    private const int TAB_BASIC = 3;
    private const int TAB_PRACTICE = 4;
    private const int TAB_BANNER = 5;
    private const int ON_SELECT_TIPS = 10;
    private const int ON_SELECT_PRACTICE = 11;
    private const int ON_SELECT_BANNER = 12;
    private const int RESET_STATUS = 100;
    private const int OUTPUT_SHOW_TIPS_DETAIL = 10000;
    private const int OUTPUT_GOTO_QUEST = 10001;
    [SerializeField]
    private Toggle ToggleTips;
    [SerializeField]
    private Toggle TogglePractice;
    [SerializeField]
    private Toggle ToggleBanners;
    [SerializeField]
    [Space(8f)]
    private GameObject BadgeTips;
    [SerializeField]
    private GameObject BadgePractice;
    [SerializeField]
    [Space(8f)]
    private GameObject BasicPanel;
    [SerializeField]
    private GameObject PracticePanel;
    [SerializeField]
    private GameObject BannerPanel;
    [SerializeField]
    [Space(8f)]
    private Transform BasicHolder;
    [SerializeField]
    private Transform PracticeHolder;
    [SerializeField]
    private GameObject BasicTemplate;
    [SerializeField]
    private GameObject PracticeTemplate;
    private BeginnerTop.TabType mCurrentTabType;
    private List<GameObject> mTipsItems;
    private List<GameObject> mQuestItems;

    public BeginnerTop()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          PlayerPrefsUtility.SetInt(PlayerPrefsUtility.BEGINNER_TOP_HAS_VISITED, 1, false);
          Network.RequestAPI((WebAPI) new ReqGetTipsAlreadyRead(new Network.ResponseCallback(this.ResponseCallback)), false);
          break;
        case 2:
          this.Refresh();
          break;
        case 3:
          this.ChangeTab(BeginnerTop.TabType.Basic, false);
          break;
        case 4:
          this.ChangeTab(BeginnerTop.TabType.Practice, false);
          break;
        case 5:
          this.ChangeTab(BeginnerTop.TabType.Banners, false);
          break;
        case 10:
          this.OnSelectBasic();
          break;
        case 11:
          this.OnSelectPractice();
          break;
        case 12:
          this.OnSelectBanner();
          break;
        case 100:
          GlobalVars.RestoreBeginnerQuest = false;
          break;
      }
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BasicTemplate, (UnityEngine.Object) null))
        this.BasicTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PracticeTemplate, (UnityEngine.Object) null))
        this.PracticeTemplate.SetActive(false);
      GlobalVars.LastReadTips = (string) null;
    }

    private void OnDestroy()
    {
      this.DeleteAllObj();
      GlobalVars.LastReadTips = (string) null;
    }

    private void Refresh()
    {
      if (this.mCurrentTabType != BeginnerTop.TabType.Basic)
        return;
      bool flag1 = true;
      using (List<GameObject>.Enumerator enumerator = this.mTipsItems.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          TipsItem component = (TipsItem) enumerator.Current.GetComponent<TipsItem>();
          TipsParam dataOfClass = DataSource.FindDataOfClass<TipsParam>(((Component) component).get_gameObject(), (TipsParam) null);
          bool hide = dataOfClass.hide;
          bool flag2 = MonoSingleton<GameManager>.Instance.Tips.Contains(dataOfClass.iname);
          if (!hide && !flag2)
            flag1 = false;
          component.IsCompleted = flag2;
          component.IsHidden = hide && !flag2;
          component.Title = !hide || flag2 ? dataOfClass.title : dataOfClass.cond_text;
          component.UpdateContent();
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BadgeTips, (UnityEngine.Object) null))
        return;
      this.BadgeTips.SetActive(!flag1);
    }

    private void OnSelectBasic()
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      GlobalVars.LastReadTips = DataSource.FindDataOfClass<TipsParam>(currentValue.GetGameObject("item"), (TipsParam) null).iname;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10000);
    }

    private void CreateAllTabs()
    {
      bool basicTabContent = this.CreateBasicTabContent();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BadgeTips, (UnityEngine.Object) null))
        this.BadgeTips.SetActive(!basicTabContent);
      bool practiceTabContent = this.CreatePracticeTabContent();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BadgePractice, (UnityEngine.Object) null))
        return;
      this.BadgePractice.SetActive(!practiceTabContent);
    }

    private void ChangeTab(BeginnerTop.TabType tabType, bool forceRefresh = false)
    {
      if (!forceRefresh && tabType == this.mCurrentTabType)
        return;
      switch (tabType)
      {
        case BeginnerTop.TabType.Basic:
          this.ToggleTips.set_isOn(true);
          this.mCurrentTabType = BeginnerTop.TabType.Basic;
          this.RefreshBasicTabPage();
          break;
        case BeginnerTop.TabType.Practice:
          this.TogglePractice.set_isOn(true);
          this.mCurrentTabType = BeginnerTop.TabType.Practice;
          this.RefreshPracticeTabPage();
          break;
        case BeginnerTop.TabType.Banners:
          this.ToggleBanners.set_isOn(true);
          this.mCurrentTabType = BeginnerTop.TabType.Banners;
          this.RefreshBannerTabPage();
          break;
      }
    }

    private void DeleteAllObj()
    {
      using (List<GameObject>.Enumerator enumerator = this.mTipsItems.GetEnumerator())
      {
        while (enumerator.MoveNext())
          UnityEngine.Object.Destroy((UnityEngine.Object) enumerator.Current.get_gameObject());
      }
      this.mTipsItems.Clear();
      using (List<GameObject>.Enumerator enumerator = this.mQuestItems.GetEnumerator())
      {
        while (enumerator.MoveNext())
          UnityEngine.Object.Destroy((UnityEngine.Object) enumerator.Current.get_gameObject());
      }
      this.mQuestItems.Clear();
    }

    private bool CreateBasicTabContent()
    {
      bool flag1 = true;
      foreach (TipsParam data in (IEnumerable<TipsParam>) ((IEnumerable<TipsParam>) MonoSingleton<GameManager>.Instance.MasterParam.Tips).OrderBy<TipsParam, int>((Func<TipsParam, int>) (t => t.order)))
      {
        GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.BasicTemplate);
        TipsItem component = (TipsItem) gameObject.GetComponent<TipsItem>();
        bool hide = data.hide;
        bool flag2 = MonoSingleton<GameManager>.Instance.Tips.Contains(data.iname);
        if (!hide && !flag2)
          flag1 = false;
        component.IsCompleted = flag2;
        component.IsHidden = hide && !flag2;
        component.Title = !hide || flag2 ? data.title : data.cond_text;
        gameObject.get_transform().SetParent(this.BasicHolder, false);
        gameObject.SetActive(true);
        DataSource.Bind<TipsParam>(gameObject, data);
        this.mTipsItems.Add(gameObject);
        component.UpdateContent();
      }
      return flag1;
    }

    private bool CreatePracticeTabContent()
    {
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      List<QuestParam> questParamList = new List<QuestParam>();
      foreach (QuestParam questParam in availableQuests)
      {
        if (questParam.type == QuestTypes.Beginner && questParam.IsDateUnlock(-1L))
          questParamList.Add(questParam);
      }
      bool flag1 = true;
      using (List<QuestParam>.Enumerator enumerator = questParamList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          QuestParam current = enumerator.Current;
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.PracticeTemplate);
          TipsQuestItem component = (TipsQuestItem) gameObject.GetComponent<TipsQuestItem>();
          component.Title = current.title;
          component.Name = current.name;
          bool flag2 = current.state == QuestStates.Cleared;
          if (!flag2)
            flag1 = false;
          component.IsCompleted = flag2;
          gameObject.get_transform().SetParent(this.PracticeHolder, false);
          gameObject.SetActive(true);
          DataSource.Bind<QuestParam>(gameObject, current);
          this.mQuestItems.Add(gameObject);
          component.UpdateContent();
        }
      }
      return flag1;
    }

    private void RefreshBasicTabPage()
    {
      this.BasicPanel.SetActive(true);
      this.PracticePanel.SetActive(false);
      this.BannerPanel.SetActive(false);
    }

    private void RefreshPracticeTabPage()
    {
      this.BasicPanel.SetActive(false);
      this.PracticePanel.SetActive(true);
      this.BannerPanel.SetActive(false);
    }

    private void RefreshBannerTabPage()
    {
      this.BasicPanel.SetActive(false);
      this.PracticePanel.SetActive(false);
      this.BannerPanel.SetActive(true);
    }

    private void OnSelectPractice()
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      GlobalVars.SelectedQuestID = DataSource.FindDataOfClass<QuestParam>(currentValue.GetGameObject("item"), (QuestParam) null).iname;
      GlobalVars.RestoreBeginnerQuest = true;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10001);
    }

    private void OnSelectBanner()
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      string str = currentValue.GetString("url");
      if (string.IsNullOrEmpty(str))
        return;
      Application.OpenURL(str);
    }

    private void ResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_Tips> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_Tips>>(www.text);
        Network.RemoveAPI();
        if (jsonObject.body.tips != null)
          MonoSingleton<GameManager>.Instance.Tips = ((IEnumerable<string>) jsonObject.body.tips).ToList<string>();
        this.CreateAllTabs();
        if (GlobalVars.RestoreBeginnerQuest)
        {
          GlobalVars.RestoreBeginnerQuest = false;
          this.ChangeTab(BeginnerTop.TabType.Practice, true);
        }
        else
          this.ChangeTab(BeginnerTop.TabType.Basic, true);
      }
    }

    private enum TabType
    {
      Basic,
      Practice,
      Banners,
    }
  }
}
