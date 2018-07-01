// Decompiled with JetBrains decompiler
// Type: SRPG.EventPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "Select", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(51, "ToEvent", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(52, "ToMulti", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(53, "ToShop", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(54, "ToGacha", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(55, "ToURL", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(59, "ToBeginner", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(58, "ToOrdeal", FlowNode.PinTypes.Output, 9)]
  [FlowNode.Pin(57, "ToPVP", FlowNode.PinTypes.Output, 8)]
  [FlowNode.Pin(56, "ToArena", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(50, "ToStory", FlowNode.PinTypes.Output, 1)]
  public class EventPopup : MonoBehaviour, IFlowInterface
  {
    public const int INPUT_BANNER_SELECT = 100;
    public const int OUTPUT_BANNER_TO_STORY = 50;
    public const int OUTPUT_BANNER_TO_EVENT = 51;
    public const int OUTPUT_BANNER_TO_MULTI = 52;
    public const int OUTPUT_BANNER_TO_SHOP = 53;
    public const int OUTPUT_BANNER_TO_GACHA = 54;
    public const int OUTPUT_BANNER_TO_URL = 55;
    public const int OUTPUT_BANNER_TO_ARENA = 56;
    public const int OUTPUT_BANNER_TO_PVP = 57;
    public const int OUTPUT_BANNER_TO_ORDEAL = 58;
    public const int OUTPUT_BANNER_TO_BEGINNER = 59;
    [SerializeField]
    private GameObject EventBannerTemplate;
    [SerializeField]
    private Transform ListRoot;
    private List<GameObject> m_EventBannerList;
    private List<BannerParam> m_BannerList;

    public EventPopup()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 100)
        return;
      this.OnSelect();
    }

    private void Awake()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EventBannerTemplate, (UnityEngine.Object) null))
        return;
      this.EventBannerTemplate.SetActive(false);
    }

    private void Start()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
        return;
      this.m_BannerList.Clear();
      this.m_BannerList.AddRange((IEnumerable<BannerParam>) EventPopup.MakeValidBannerParams(false));
      this.Setup(this.m_BannerList.ToArray());
    }

    private void Setup(BannerParam[] _params)
    {
      if (_params == null || _params.Length <= 0)
        DebugUtility.LogError("イベントバナーデータが存在しません");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.EventBannerTemplate, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("テンプレートオブジェクトが指定されていません");
      }
      else
      {
        this.m_EventBannerList.Clear();
        for (int index = 0; index < _params.Length; ++index)
        {
          BannerParam bannerParam = _params[index];
          int num = index;
          if (bannerParam != null)
          {
            GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.EventBannerTemplate);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
            {
              gameObject.get_transform().SetParent(this.ListRoot, false);
              EventPopupListItem component1 = (EventPopupListItem) gameObject.GetComponent<EventPopupListItem>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
                component1.SetupBannerParam(bannerParam);
              ButtonEvent component2 = (ButtonEvent) gameObject.GetComponent<ButtonEvent>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
              {
                ButtonEvent.Event @event = component2.GetEvent("EVENTPOPUP_BANNER_SELECT");
                if (@event != null)
                  @event.valueList.SetField("select", num);
              }
              gameObject.SetActive(true);
              this.m_EventBannerList.Add(gameObject);
            }
          }
        }
      }
    }

    private void OnSelect()
    {
      this.Select((FlowNode_ButtonEvent.currentValue as SerializeValueList).GetInt("select"));
    }

    private void Select(int index)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.Instance))
        return;
      BannerParam[] array = this.m_BannerList.ToArray();
      if (array == null)
        return;
      if (array.Length < index)
      {
        DebugUtility.LogError("選択されたイベントバナーが正しくありません");
      }
      else
      {
        BannerParam bannerParam = array[index];
        int pinID = -1;
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        switch (bannerParam.type)
        {
          case BannerType.storyQuest:
            if (EventPopup.SetupQuestVariables(bannerParam.sval, true))
            {
              GlobalVars.SelectedQuestID = (string) null;
              pinID = 50;
              break;
            }
            break;
          case BannerType.eventQuest:
            GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuest;
            EventPopup.SetupQuestVariables(bannerParam.sval, false);
            pinID = 51;
            break;
          case BannerType.multiQuest:
            if (player.CheckUnlock(UnlockTargets.MultiPlay))
            {
              pinID = 52;
              break;
            }
            LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiPlay);
            break;
          case BannerType.gacha:
            GlobalVars.SelectedGachaTableId = bannerParam.sval;
            pinID = 54;
            break;
          case BannerType.shop:
            pinID = 53;
            break;
          case BannerType.url:
            if (!string.IsNullOrEmpty(bannerParam.sval))
              Application.OpenURL(bannerParam.sval);
            pinID = 55;
            break;
          case BannerType.towerQuest:
            if (player.CheckUnlock(UnlockTargets.TowerQuest))
            {
              GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Tower;
              pinID = 51;
              break;
            }
            LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.TowerQuest);
            break;
          case BannerType.arena:
            if (player.CheckUnlock(UnlockTargets.Arena))
            {
              pinID = 56;
              break;
            }
            LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Arena);
            break;
          case BannerType.pvp:
            if (player.CheckUnlock(UnlockTargets.MultiVS))
            {
              pinID = 57;
              break;
            }
            LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiVS);
            break;
          case BannerType.ordealQuest:
            if (player.CheckUnlock(UnlockTargets.Ordeal))
            {
              pinID = 58;
              break;
            }
            LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Ordeal);
            break;
          case BannerType.beginner:
            pinID = 59;
            break;
        }
        if (pinID == -1)
          return;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
      }
    }

    public static bool SetupQuestVariables(string _questID, bool _is_story)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      for (int index = 0; index < availableQuests.Length; ++index)
      {
        if (availableQuests[index].iname == _questID)
        {
          GlobalVars.SelectedSection.Set(availableQuests[index].Chapter.section);
          GlobalVars.SelectedChapter.Set(availableQuests[index].ChapterID);
          return true;
        }
      }
      if (_is_story)
      {
        QuestParam lastStoryQuest = instance.Player.FindLastStoryQuest();
        if (lastStoryQuest != null && lastStoryQuest.IsDateUnlock(Network.GetServerTime()))
        {
          GlobalVars.SelectedSection.Set(lastStoryQuest.Chapter.section);
          GlobalVars.SelectedChapter.Set(lastStoryQuest.ChapterID);
          return true;
        }
      }
      return false;
    }

    public static BannerParam[] MakeValidBannerParams(bool _is_home_banner = true)
    {
      List<BannerParam> bannerParamList = new List<BannerParam>();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      BannerParam[] banners = instance.MasterParam.Banners;
      if (banners == null)
      {
        DebugUtility.LogError("バナーの設定がありません、有効なバナーを1つ以上設定してください");
        return (BannerParam[]) null;
      }
      QuestParam lastStoryQuest = instance.Player.FindLastStoryQuest();
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      DateTime now = TimeManager.FromUnixTime(serverTime);
      for (int index = 0; index < banners.Length; ++index)
      {
        BannerParam param = banners[index];
        bool flag = true;
        if (param != null && !string.IsNullOrEmpty(param.banner) && bannerParamList.FindIndex((Predicate<BannerParam>) (p => p.iname == param.iname)) == -1 && (!_is_home_banner || param.IsHomeBanner))
        {
          if (param.type == BannerType.shop)
          {
            if (instance.IsLimitedShopOpen)
            {
              if (instance.LimitedShopList != null && !string.IsNullOrEmpty(param.sval))
              {
                JSON_ShopListArray.Shops shops = Array.Find<JSON_ShopListArray.Shops>(instance.LimitedShopList, (Predicate<JSON_ShopListArray.Shops>) (p => p.gname == param.sval));
                if (shops != null)
                {
                  param.begin_at = TimeManager.FromUnixTime(shops.start).ToString();
                  param.end_at = TimeManager.FromUnixTime(shops.end).ToString();
                }
                else
                  continue;
              }
            }
            else
              continue;
          }
          else if (param.type == BannerType.storyQuest)
          {
            flag = false;
            if (lastStoryQuest != null)
            {
              QuestParam questParam;
              if (string.IsNullOrEmpty(param.sval))
              {
                questParam = lastStoryQuest;
              }
              else
              {
                questParam = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p.iname == param.sval));
                if (questParam == null || questParam.iname != lastStoryQuest.iname && questParam.state == QuestStates.New)
                  questParam = lastStoryQuest;
              }
              if (!questParam.IsDateUnlock(serverTime))
                continue;
            }
            else
              continue;
          }
          else if (param.type == BannerType.eventQuest || param.type == BannerType.multiQuest)
          {
            if (!string.IsNullOrEmpty(param.sval))
            {
              QuestParam questParam = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p.iname == param.sval));
              if (questParam == null || !questParam.IsDateUnlock(serverTime))
                continue;
            }
          }
          else if (param.type != BannerType.towerQuest && param.type != BannerType.gacha)
          {
            if (param.type == BannerType.url)
            {
              if (string.IsNullOrEmpty(param.sval))
                continue;
            }
            else if (param.type == BannerType.arena || param.type == BannerType.pvp || param.type != BannerType.ordealQuest)
              ;
          }
          if (!flag || param.IsAvailablePeriod(now))
            bannerParamList.Add(param);
        }
      }
      bannerParamList.Sort((Comparison<BannerParam>) ((a, b) => a.priority - b.priority));
      return bannerParamList.ToArray();
    }
  }
}
