// Decompiled with JetBrains decompiler
// Type: AnalyticsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;
using System.Text;
using TapjoyUnity;
using TapjoyUnity.Internal;
using UnityEngine;

public static class AnalyticsManager
{
  private static readonly List<AnalyticsManager.TrackingTriggerEventData> TutorialEventTriggers = new List<AnalyticsManager.TrackingTriggerEventData>()
  {
    new AnalyticsManager.TrackingTriggerEventData("splash", "splash", "1"),
    new AnalyticsManager.TrackingTriggerEventData("Tutorial_Download", "download_main", "2"),
    new AnalyticsManager.TrackingTriggerEventData("Tutorial_Download_Dialog", "download_bg", "3"),
    new AnalyticsManager.TrackingTriggerEventData("BackgroundDownloaderEnabled", "download_bg_yes", "3.1"),
    new AnalyticsManager.TrackingTriggerEventData("BackgroundDownloaderDisabled", "download_bg_no", "3.2"),
    new AnalyticsManager.TrackingTriggerEventData("Tutorial_Download_Start", "download_bg_start", "4"),
    new AnalyticsManager.TrackingTriggerEventData("Tutorial_Movie_Intro", "movie_intro", "5"),
    new AnalyticsManager.TrackingTriggerEventData("tut001a.001", "scene_thecode", "6"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_BattleGrid_Show.ToString(), "guide_battle_move", "7"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_0.003", "guide_battle_movebehind", "7.1"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_0.004", "guide_battle_basicshield", "7.2"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_0.005", "guide_battle_swordcrush", "7.3"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_0.006", "guide_battle_activateskill", "7.4"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_0.007", "guide_battle_endmove", "7.5"),
    new AnalyticsManager.TrackingTriggerEventData("guide_battle_freeplay", "guide_battle_freeplay", "7.6"),
    new AnalyticsManager.TrackingTriggerEventData("tut001b.001", "dialogue_start", "7.7"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_SkipDialog_AfterLogiVSDias.ToString(), "skipdialog_after_logi_vs_dias", "7.7.1"),
    new AnalyticsManager.TrackingTriggerEventData("tut001b.007a", "dialogue_end", "7.8"),
    new AnalyticsManager.TrackingTriggerEventData("Tutorial_Movie_AnimeIntro", "movie_anime", "8"),
    new AnalyticsManager.TrackingTriggerEventData("tut002a.001", "scene_resistance", "9"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_SkipDialog_BeforeLogiDiasVSVlad.ToString(), "skipdialog_before_logidias_vs_vlad", "9.1"),
    new AnalyticsManager.TrackingTriggerEventData("tut003a.001", "scene_trepidation", "10"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_SkipDialog_AfterLogiDiasVSVlad.ToString(), "skipdialog_after_logidias_vs_vlad", "10.1"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_SkipDialog_AfterLogiDiasVSNevillePt1.ToString(), "skipdialog_after_logidias_vs_neville_pt1", "10.2"),
    new AnalyticsManager.TrackingTriggerEventData("0_6b_2d.001", "scene_ouroboros", "11"),
    new AnalyticsManager.TrackingTriggerEventData("0_6b_2d.017", "guide_summon1", "12"),
    new AnalyticsManager.TrackingTriggerEventData("tut004a.001", "scene_started", "13"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_SkipDialog_BeforeLogiDiasVSNevillePt2.ToString(), "skipdialog_before_logidias_vs_neville_pt2", "13.1"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_SkipDialog_AfterLogiDiasVSNevillePt2.ToString(), "skipdialog_after_logidias_vs_neville_pt2", "13.2"),
    new AnalyticsManager.TrackingTriggerEventData("Tutorial_Movie_World", "movie_world", "14"),
    new AnalyticsManager.TrackingTriggerEventData("0_8_2d.001", "scene_throne", "15"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_HomeScreen_BasicGuideDialog_Show.ToString(), "confirm_skip1", "16"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_HomeScreen_BasicGuideDialog_Continue.ToString(), "confirm_skip1_yes", "16.1"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_HomeScreen_BasicGuideDialog_Cancel.ToString(), "confirm_skip1_no", "16.2"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.001", "guide_missions", "17"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.007", "guide_summon2", "18"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.008", "guide_summon2_use", "19"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.009", "guide_summon2_get", "20"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.011", "guide_challenges", "21"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_HomeScreen_UnitsGuideDialog_Show.ToString(), "confirm_skip2", "22"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_HomeScreen_UnitsGuideDialog_Continue.ToString(), "confirm_skip2_yes", "22.1"),
    new AnalyticsManager.TrackingTriggerEventData(AnalyticsManager.TrackingType.Tutorial_HomeScreen_UnitsGuideDialog_Cancel.ToString(), "confirm_skip2_no", "22.2"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.015", "guide_units", "23"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.032", "guide_quests", "24"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.035", "guide_party", "25"),
    new AnalyticsManager.TrackingTriggerEventData("sg_tut_1.038", "end", "26")
  };
  private static readonly List<AnalyticsManager.TrackingTriggerEventData> MissionEventTriggers = new List<AnalyticsManager.TrackingTriggerEventData>()
  {
    new AnalyticsManager.TrackingTriggerEventData("QE_ST_NO_010001", "first_quest_clear", "1")
  };
  private static readonly string[] GachaCostType = new string[8]
  {
    "None",
    "Gems",
    "Paid Gems",
    "Zeni",
    "Ticket",
    "Free Gems",
    "Zeni",
    "All"
  };
  private static readonly string[] GachaSummonType = new string[6]
  {
    "None",
    "Rare",
    "Equipment",
    "Ticket",
    "Normal",
    "Special"
  };
  private static readonly string[] NonPremiumCurrencyString = new string[4]
  {
    "zeni",
    "ticket",
    "ap",
    "item"
  };
  private static readonly Encoding encoding = Encoding.GetEncoding("utf-8");
  private static readonly string mBundleID = "sg.gumi.alchemistww";
  private static bool isDebugMode = false;
  private static List<string> _namesOfPlacementsToPreload = new List<string>()
  {
    "title",
    "homescreen"
  };
  private static List<TJPlacement> _preloadedPlacements = new List<TJPlacement>();
  private static Dictionary<TJPlacement, Action> _placementToShowAndCallbackDictionary = new Dictionary<TJPlacement, Action>();
  private static Dictionary<string, object> summonData;
  private static AppsFlyerTrackerCallbacks _appsflyerGameObject;
  private static TapjoyComponent _tapjoyGameObject;
  private static Action _tapjoyActionsSavedBeforeConnectSuccess;
  public static Action<string> PlacementWantedFlowChangeHandler;

  private static bool HasPlayerCompletedGameplayPortionOfTutorial
  {
    get
    {
      return !MonoSingleton<GameManager>.Instance.IsTutorial();
    }
  }

  public static void TrackTutorialEventGeneric(AnalyticsManager.TrackingType inTrackingType)
  {
    switch (inTrackingType)
    {
      case AnalyticsManager.TrackingType.StaminaReward_Video:
        AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.AP, (long) GlobalVars.LastReward.Get().Stamina, "Video Ads", (string) null);
        break;
      case AnalyticsManager.TrackingType.StaminaReward_Milestone:
        AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.AP, (long) GlobalVars.LastReward.Get().Stamina, "Milestone Rewards", (string) null);
        break;
      case AnalyticsManager.TrackingType.Tutorial_HomeScreen_BasicGuideDialog_Continue:
      case AnalyticsManager.TrackingType.Tutorial_HomeScreen_BasicGuideDialog_Cancel:
      case AnalyticsManager.TrackingType.Tutorial_Download:
      case AnalyticsManager.TrackingType.Tutorial_Download_Dialog:
      case AnalyticsManager.TrackingType.Tutorial_Download_Start:
      case AnalyticsManager.TrackingType.Tutorial_Movie_Intro:
      case AnalyticsManager.TrackingType.Tutorial_Movie_AnimeIntro:
      case AnalyticsManager.TrackingType.Tutorial_HomeScreen_UnitsGuideDialog_Continue:
      case AnalyticsManager.TrackingType.Tutorial_HomeScreen_UnitsGuideDialog_Cancel:
      case AnalyticsManager.TrackingType.Tutorial_Movie_World:
      case AnalyticsManager.TrackingType.Tutorial_SkipDialog_AfterLogiVSDias:
      case AnalyticsManager.TrackingType.Tutorial_SkipDialog_BeforeLogiDiasVSVlad:
      case AnalyticsManager.TrackingType.Tutorial_SkipDialog_AfterLogiDiasVSVlad:
      case AnalyticsManager.TrackingType.Tutorial_SkipDialog_AfterLogiDiasVSNevillePt1:
      case AnalyticsManager.TrackingType.Tutorial_SkipDialog_BeforeLogiDiasVSNevillePt2:
      case AnalyticsManager.TrackingType.Tutorial_SkipDialog_AfterLogiDiasVSNevillePt2:
      case AnalyticsManager.TrackingType.Tutorial_HomeScreen_BasicGuideDialog_Show:
      case AnalyticsManager.TrackingType.Tutorial_HomeScreen_UnitsGuideDialog_Show:
      case AnalyticsManager.TrackingType.Tutorial_BattleGrid_Show:
        AnalyticsManager.TrackTutorialAnalyticsEvent(inTrackingType.ToString());
        break;
      case AnalyticsManager.TrackingType.Player_New:
        AnalyticsManager.RecordNewPlayerLogin();
        break;
      case AnalyticsManager.TrackingType.Player_Guest:
        AnalyticsManager.RecordGuestLogin();
        break;
      case AnalyticsManager.TrackingType.Player_FB:
        AnalyticsManager.RecordFacebookLogin();
        break;
      case AnalyticsManager.TrackingType.Tutorial_BGDLC:
        if (BackgroundDownloader.Instance.IsEnabled)
        {
          AnalyticsManager.TrackTutorialAnalyticsEvent("BackgroundDownloaderEnabled");
          break;
        }
        AnalyticsManager.TrackTutorialAnalyticsEvent("BackgroundDownloaderDisabled");
        break;
      default:
        throw new ArgumentOutOfRangeException("Unknown Tutorial Analytics Event");
    }
  }

  private static string GetGachaSummonCostTypeString(GachaButton.GachaCostType inGachaCostType)
  {
    int num = (int) inGachaCostType;
    if (num > -1 && num < AnalyticsManager.GachaCostType.Length)
      return AnalyticsManager.GachaCostType[(int) inGachaCostType];
    return (string) null;
  }

  private static long GetGachaSummonCostTypeLong(string inGachaSummonCostName)
  {
    int index = 0;
    for (int length = AnalyticsManager.GachaCostType.Length; index < length; ++index)
    {
      if (inGachaSummonCostName.Equals(AnalyticsManager.GachaCostType[index]))
        return (long) index;
    }
    throw new ArgumentOutOfRangeException("Unrecognised inGachaSummonCostName", inGachaSummonCostName);
  }

  private static string GetGachaSummonTypeString(GachaWindow.GachaTabCategory inGachaType)
  {
    int num = (int) inGachaType;
    if (num > -1 && num < AnalyticsManager.GachaSummonType.Length)
      return AnalyticsManager.GachaSummonType[(int) inGachaType];
    return (string) null;
  }

  private static string GetNonPremiumCurrencyStringType(AnalyticsManager.NonPremiumCurrencyType inCurrencyType)
  {
    int num = (int) inCurrencyType;
    if (num > -1 && num < AnalyticsManager.NonPremiumCurrencyString.Length)
      return AnalyticsManager.NonPremiumCurrencyString[(int) inCurrencyType];
    return (string) null;
  }

  public static void Setup()
  {
    DebugUtility.Log("AnalyticsManager : Setup");
    AnalyticsManager.SetupAppsFlyer();
    AnalyticsManager.SetupTapJoy();
  }

  public static void SetPlayerIsPaying(bool inIsPayingPlayer)
  {
    AnalyticsManager.TapjoyRerouteActionBasedOnConnectStatus((Action) (() =>
    {
      if (!inIsPayingPlayer)
        return;
      Tapjoy.AddUserTag("Paying");
    }));
  }

  private static void SetupAppsFlyer()
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) AnalyticsManager._appsflyerGameObject, (UnityEngine.Object) null))
    {
      AnalyticsManager._appsflyerGameObject = (AppsFlyerTrackerCallbacks) new GameObject("AppsFlyerTrackerCallbacks").AddComponent<AppsFlyerTrackerCallbacks>();
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) AnalyticsManager._appsflyerGameObject);
    }
    AppsFlyer.setAppsFlyerKey("WMa4kPf8ZdvNhcpvdpwAvE");
    AppsFlyer.setIsDebug(AnalyticsManager.isDebugMode);
    AppsFlyer.setCollectIMEI(false);
    AppsFlyer.setAppID(AnalyticsManager.mBundleID);
  }

  private static void SetupTapJoy()
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) AnalyticsManager._tapjoyGameObject, (UnityEngine.Object) null))
    {
      AnalyticsManager._tapjoyGameObject = (TapjoyComponent) UnityEngine.Object.FindObjectOfType<TapjoyComponent>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) AnalyticsManager._tapjoyGameObject, (UnityEngine.Object) null))
        throw new Exception("Scene does not contain a precreated Tapjoy tracker object");
    }
    DebugUtility.LogWarning("Entered Set up");
    Tapjoy.SetDebugEnabled(AnalyticsManager.isDebugMode);
    Tapjoy.SetGcmSender("813126952066");
    Tapjoy.Connect("8KX9gHMyQoe1uZ44k2UZFAECmkrdHkw3JeTIaSi9Gh54pEK7mTONyurmoPRt");
    // ISSUE: method pointer
    Tapjoy.add_OnConnectSuccess(new Tapjoy.OnConnectSuccessHandler((object) null, __methodptr(HandleConnectSuccess)));
    // ISSUE: method pointer
    Tapjoy.add_OnConnectFailure(new Tapjoy.OnConnectFailureHandler((object) null, __methodptr(HandleConnectFailure)));
  }

  private static void HandleConnectSuccess()
  {
    Debug.Log((object) "Tapjoy Connect Success");
    // ISSUE: method pointer
    TJPlacement.add_OnRequestSuccess(new TJPlacement.OnRequestSuccessHandler((object) null, __methodptr(HandlePlacementRequestSuccess)));
    // ISSUE: method pointer
    TJPlacement.add_OnContentShow(new TJPlacement.OnContentShowHandler((object) null, __methodptr(HandlePlacementContentShow)));
    // ISSUE: method pointer
    TJPlacement.add_OnPurchaseRequest(new TJPlacement.OnPurchaseRequestHandler((object) null, __methodptr(HandlePurchaseRequest)));
    if (AnalyticsManager._namesOfPlacementsToPreload != null)
    {
      using (List<string>.Enumerator enumerator = AnalyticsManager._namesOfPlacementsToPreload.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          TJPlacement placement = TJPlacement.CreatePlacement(enumerator.Current);
          placement.RequestContent();
          AnalyticsManager._preloadedPlacements.Add(placement);
        }
      }
      AnalyticsManager._namesOfPlacementsToPreload.Clear();
    }
    if (AnalyticsManager._tapjoyActionsSavedBeforeConnectSuccess != null)
    {
      AnalyticsManager._tapjoyActionsSavedBeforeConnectSuccess();
      AnalyticsManager._tapjoyActionsSavedBeforeConnectSuccess = (Action) null;
    }
    if (AnalyticsManager.isDebugMode)
      Tapjoy.SetUserCohortVariable(0, "Development");
    else
      Tapjoy.SetUserCohortVariable(0, "Release");
    Tapjoy.SetAppDataVersion(MyApplicationPlugin.get_version());
    Tapjoy.SetPushNotificationDisabled(false);
  }

  public static void HandlePurchaseRequest(TJPlacement inPlacement, TJActionRequest inRequest, string inProductId)
  {
    Debug.Log((object) "We have a purchase callback");
    if (AnalyticsManager.PlacementWantedFlowChangeHandler != null)
    {
      Debug.Log((object) ("We are sending back our placement flow: " + inProductId));
      AnalyticsManager.PlacementWantedFlowChangeHandler(inProductId);
    }
    if (inRequest == null)
      return;
    inRequest.Completed();
  }

  private static void HandleConnectFailure()
  {
    Debug.Log((object) "Tapjoy Connect Failed");
    // ISSUE: method pointer
    Tapjoy.remove_OnConnectSuccess(new Tapjoy.OnConnectSuccessHandler((object) null, __methodptr(HandleConnectSuccess)));
    // ISSUE: method pointer
    Tapjoy.remove_OnConnectFailure(new Tapjoy.OnConnectFailureHandler((object) null, __methodptr(HandleConnectFailure)));
    AnalyticsManager.SetupTapJoy();
  }

  private static void HandlePlacementContentShow(TJPlacement inPlacement)
  {
    if (AnalyticsManager._placementToShowAndCallbackDictionary == null || !AnalyticsManager._placementToShowAndCallbackDictionary.ContainsKey(inPlacement))
      return;
    Action toShowAndCallback = AnalyticsManager._placementToShowAndCallbackDictionary[inPlacement];
    if (toShowAndCallback != null)
      toShowAndCallback();
    AnalyticsManager._placementToShowAndCallbackDictionary.Remove(inPlacement);
  }

  private static void HandlePlacementRequestSuccess(TJPlacement inPlacement)
  {
    if (AnalyticsManager._placementToShowAndCallbackDictionary == null || !AnalyticsManager._placementToShowAndCallbackDictionary.ContainsKey(inPlacement))
      return;
    inPlacement.ShowContent();
  }

  public static void TrackAppLaunch()
  {
    AppsFlyer.init("WMa4kPf8ZdvNhcpvdpwAvE", "AppsFlyerTrackerCallbacks");
    AppsFlyer.createValidateInAppListener("AppsFlyerTrackerCallbacks", "onInAppBillingSuccess", "onInAppBillingFailure");
    AppsFlyer.loadConversionData(nameof (AnalyticsManager));
    AppsFlyer.enableUninstallTracking("8KX9gHMyQoe1uZ44k2UZFAECmkrdHkw3JeTIaSi9Gh54pEK7mTONyurmoPRt");
  }

  public static void SetSummonTracking(GachaButton.GachaCostType inCostType, GachaWindow.GachaTabCategory inTabCategory, string inGateID, bool inIsFree, int inSummonCost, int inNumberOfThingsSummoned, int inCurrentSummonStepIndex)
  {
    string str;
    if (inIsFree)
    {
      switch (inCostType)
      {
        case GachaButton.GachaCostType.COIN:
          str = "Rare (Free)";
          break;
        case GachaButton.GachaCostType.GOLD:
          str = "Normal (Free)";
          break;
        default:
          str = string.Empty;
          break;
      }
    }
    else
    {
      switch (inCostType)
      {
        case GachaButton.GachaCostType.COIN:
          str = "Rare (Paid)";
          break;
        case GachaButton.GachaCostType.COIN_P:
          str = "Rare (Discount)";
          break;
        case GachaButton.GachaCostType.GOLD:
          str = "Normal (Zeni)";
          break;
        default:
          str = string.Empty;
          break;
      }
    }
    AnalyticsManager.summonData = new Dictionary<string, object>()
    {
      {
        "main_type",
        (object) AnalyticsManager.GetGachaSummonTypeString(inTabCategory)
      },
      {
        "sub_type",
        (object) str
      },
      {
        "currency_used",
        (object) AnalyticsManager.GetGachaSummonCostTypeString(inCostType)
      },
      {
        "currency_amount",
        (object) inSummonCost
      },
      {
        "gate_id",
        (object) inGateID
      },
      {
        "step_id",
        (object) inCurrentSummonStepIndex
      },
      {
        "step_reward_id",
        (object) string.Empty
      },
      {
        "step_reward_amount",
        (object) inNumberOfThingsSummoned
      }
    };
  }

  public static void TrackSummonComplete()
  {
    AnalyticsManager.TrackAppsFlyerEvent("summon", AnalyticsManager.summonData.ConvertValuesToString<string, object>());
    AnalyticsManager.TrackTapjoyEvent("summon", AnalyticsManager.summonData["main_type"].ToString(), AnalyticsManager.summonData["gate_id"].ToString(), AnalyticsManager.summonData["sub_type"].ToString(), "currency_amount", long.Parse(AnalyticsManager.summonData["currency_amount"].ToString()), "currency_used", AnalyticsManager.GetGachaSummonCostTypeLong(AnalyticsManager.summonData["currency_used"].ToString()), "step_id", long.Parse(AnalyticsManager.summonData["step_id"].ToString()));
    AnalyticsManager.summonData.Clear();
  }

  public static void TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType inCurrencyType, long inAmount, string inObtainSource, string inUniqueCurrencyID = null)
  {
    if (inAmount <= 0L)
      return;
    AnalyticsManager.TrackNonPremiumCurrencyTransaction(inCurrencyType, inAmount, inObtainSource, inUniqueCurrencyID, false);
  }

  public static void TrackFreePremiumCurrencyObtain(long inAmount, string inObtainSource)
  {
    if (inAmount <= 0L)
      return;
    AnalyticsManager.TrackTapjoyEvent("economy", "currency.obtain.gem", inObtainSource, inAmount);
  }

  public static void TrackPaidPremiumCurrencyObtain(long inAmount, string inObtainSource)
  {
    if (inAmount <= 0L)
      return;
    AnalyticsManager.TrackTapjoyEvent("economy", "currency.obtain.gem", inObtainSource, inAmount);
  }

  public static void TrackNonPremiumCurrencyUse(AnalyticsManager.NonPremiumCurrencyType inCurrencyType, long inAmount, string inSinkSource, string inUniqueCurrencyID = null)
  {
    if (inAmount <= 0L)
      return;
    AnalyticsManager.TrackNonPremiumCurrencyTransaction(inCurrencyType, inAmount, inSinkSource, inUniqueCurrencyID, true);
  }

  public static void TrackFreePremiumCurrencyUse(long inAmount, string inSinkSource)
  {
    if (inAmount <= 0L)
      return;
    AnalyticsManager.TrackTapjoyEvent("economy", "currency.use.gem", inSinkSource, inAmount);
  }

  public static void TrackPaidPremiumCurrencyUse(long inAmount, string inSinkSource)
  {
    if (inAmount <= 0L)
      return;
    AnalyticsManager.TrackTapjoyEvent("economy", "currency.use.gem", inSinkSource, inAmount);
  }

  public static void TrackOriginalCurrencyUse(ESaleType inSaleType, int inAmount, string inSinkSource)
  {
    switch (inSaleType)
    {
      case ESaleType.Gold:
        AnalyticsManager.TrackNonPremiumCurrencyUse(AnalyticsManager.NonPremiumCurrencyType.Zeni, (long) inAmount, inSinkSource, (string) null);
        break;
      case ESaleType.Coin:
        AnalyticsManager.TrackFreePremiumCurrencyUse((long) inAmount, inSinkSource);
        break;
      case ESaleType.Coin_P:
        AnalyticsManager.TrackPaidPremiumCurrencyUse((long) inAmount, inSinkSource);
        break;
    }
  }

  private static void TrackNonPremiumCurrencyTransaction(AnalyticsManager.NonPremiumCurrencyType inCurrencyType, long inAmount, string inObtainSource, string inUniqueCurrencyID, bool isUseCurrency)
  {
    string currencyStringType = AnalyticsManager.GetNonPremiumCurrencyStringType(inCurrencyType);
    string inEventName = !isUseCurrency ? "currency.obtain." + currencyStringType : "currency.use." + currencyStringType;
    if (!string.IsNullOrEmpty(inUniqueCurrencyID) && (inCurrencyType == AnalyticsManager.NonPremiumCurrencyType.Item || inCurrencyType == AnalyticsManager.NonPremiumCurrencyType.SummonTicket))
    {
      string inEventCategory2 = inCurrencyType != AnalyticsManager.NonPremiumCurrencyType.Item ? "ticket_id" : "item_id";
      AnalyticsManager.TrackTapjoyEvent("economy", inEventName, inObtainSource, inEventCategory2, inUniqueCurrencyID, inAmount);
    }
    else
      AnalyticsManager.TrackTapjoyEvent("economy", inEventName, inObtainSource, inAmount);
  }

  public static void TrackPurchase(string inProductID, string inCurrencyCode, double inPrice)
  {
    if (AnalyticsManager.isDebugMode)
      return;
    AnalyticsManager.AttemptToTrackFirstTimePurchase();
    AppsFlyer.setCurrencyCode("USD");
    AnalyticsManager.TrackAppsFlyerEvent("purchase", new Dictionary<string, string>()
    {
      {
        "product_id",
        inProductID
      },
      {
        "af_currency",
        inCurrencyCode
      },
      {
        "af_revenue",
        inPrice.ToString()
      }
    });
    AnalyticsManager.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.TrackPurchaseInGooglePlayStore(inProductID, (string) null, (string) null, (string) null)));
    AnalyticsManager.TrackTapjoyEvent("IAP Purchase", inProductID, inPrice.ToString(), 0L);
  }

  private static void AttemptToTrackFirstTimePurchase()
  {
    if (PlayerPrefs.HasKey("FIRST_PURCHASE"))
      return;
    AnalyticsManager.TrackTapjoyEvent("user.paying.first", 0L);
    AnalyticsManager.TrackTapjoyEvent("revenue.paying.first", 0L);
  }

  public static void TrackPlayerLevelUp(int currentLevel)
  {
    AnalyticsManager.TrackAppsFlyerEvent("level_achieved", new Dictionary<string, string>()
    {
      {
        "level_achieved",
        currentLevel.ToString()
      }
    });
    AnalyticsManager.TrackTapjoyEvent("level_achieved", (long) currentLevel);
    AnalyticsManager.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.SetUserLevel(currentLevel)));
    AnalyticsManager.RecordUserAttribute("Account_Level", currentLevel.ToString());
  }

  public static void TrackTutorialAnalyticsEvent(string inTag)
  {
    AnalyticsManager.TrackingTriggerEventData triggerEventData1 = AnalyticsManager.TutorialEventTriggers.Find((Predicate<AnalyticsManager.TrackingTriggerEventData>) (triggerEventData => triggerEventData.ID.Equals(inTag)));
    if (triggerEventData1.Equals((object) new AnalyticsManager.TrackingTriggerEventData()))
      return;
    string inEventName = "funnel.tutorial." + triggerEventData1.ReportingName;
    if (AnalyticsManager.HasPlayerCompletedGameplayPortionOfTutorial || PlayerPrefs.HasKey(inEventName))
      return;
    PlayerPrefs.SetString(inEventName, string.Empty);
    Dictionary<string, object> inDictionary = new Dictionary<string, object>()
    {
      {
        "step_number",
        (object) triggerEventData1.StepNumber
      }
    };
    AnalyticsManager.TrackAppsFlyerEvent(inEventName, inDictionary.ConvertValuesToString<string, object>());
    AnalyticsManager.TrackTapjoyEvent("funnel.tutorial", inEventName, triggerEventData1.StepNumber, 0L);
  }

  public static void TrackMissionAnalyticsEvent(string inMissionID)
  {
    AnalyticsManager.TrackingTriggerEventData triggerEventData1 = AnalyticsManager.MissionEventTriggers.Find((Predicate<AnalyticsManager.TrackingTriggerEventData>) (triggerEventData => triggerEventData.ID.Equals(inMissionID)));
    if (triggerEventData1.Equals((object) new AnalyticsManager.TrackingTriggerEventData()))
      return;
    string reportingName = triggerEventData1.ReportingName;
    if (PlayerPrefs.HasKey(reportingName))
      return;
    PlayerPrefs.SetString(reportingName, string.Empty);
    AnalyticsManager.TrackAppsFlyerEvent(reportingName, new Dictionary<string, string>()
    {
      {
        "mission",
        inMissionID
      }
    });
    AnalyticsManager.TrackTapjoyEvent("mission", reportingName, inMissionID, 0L);
  }

  private static void TrackTapjoyEvent(string inEventName, long inValue = 0)
  {
    AnalyticsManager.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.TrackEvent(inEventName, inValue)));
  }

  private static void TrackTapjoyEvent(string category, string inEventName, long inValue = 0)
  {
    AnalyticsManager.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.TrackEvent(category, inEventName, inValue)));
  }

  private static void TrackTapjoyEvent(string category, string inEventName, string inEventCategory, long inValue = 0)
  {
    AnalyticsManager.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.TrackEvent(category, inEventName, inEventCategory, (string) null, inValue)));
  }

  private static void TrackTapjoyEvent(string category, string inEventName, string inEventCategory1, string inEventCategory2, string inValueName1, long inValue1)
  {
    AnalyticsManager.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.TrackEvent(category, inEventName, inEventCategory1, inEventCategory2, inValueName1, inValue1, (string) null, 0L, (string) null, 0L)));
  }

  private static void TrackTapjoyEvent(string category, string inEventName, string inEventCategory1, string inEventCategory2, string inValueName1, long inValue1, string inValueName2, long inValue2, string inValueName3, long inValue3)
  {
    AnalyticsManager.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.TrackEvent(category, inEventName, inEventCategory1, inEventCategory2, inValueName1, inValue1, inValueName2, inValue2, inValueName3, inValue3)));
  }

  private static void TrackAppsFlyerEvent(string inEventName, Dictionary<string, string> inEventContent)
  {
    AppsFlyer.trackRichEvent(inEventName, inEventContent);
  }

  private static void RecordUserAttribute(string inAttributeKey, string inAttributeValue)
  {
    AnalyticsManager.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.AddUserTag(inAttributeKey + "," + inAttributeValue)));
  }

  private static void RecordNewPlayerLogin()
  {
    AnalyticsManager.TrackTapjoyEvent("login", "user.new", GlobalVars.CustomerID, 0L);
    AnalyticsManager.RecordUserAttribute("Start_Version", MyApplicationPlugin.get_version());
  }

  private static void RecordGuestLogin()
  {
    if (PlayerPrefs.GetInt("AccountLinked", 0) != 0)
      return;
    AnalyticsManager.TrackTapjoyEvent("login", "user.account.guest", GlobalVars.CustomerID, 0L);
    AnalyticsManager.RecordUserAttribute("Account_Type", "guest");
  }

  private static void RecordFacebookLogin()
  {
    if (PlayerPrefs.GetInt("AccountLinked", 0) != 1)
      return;
    AnalyticsManager.TrackTapjoyEvent("login", "user.account.facebook", GlobalVars.CustomerID, 0L);
    AnalyticsManager.RecordUserAttribute("Account_Type", "facebook");
  }

  public static void AttemptToShowPlacement(string inPlacementName, Action inCallbackForPlacementBeingShown)
  {
    AnalyticsManager.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => AnalyticsManager.ShowPlacement(inPlacementName, inCallbackForPlacementBeingShown, AnalyticsManager._placementToShowAndCallbackDictionary)));
  }

  private static void ShowPlacement(string inPlacementName, Action inCallbackForPlacementBeingShown, Dictionary<TJPlacement, Action> inPlacementToShowAndCallbackDictionary)
  {
    TJPlacement key = AnalyticsManager._preloadedPlacements.Find((Predicate<TJPlacement>) (placement => placement.GetName().Equals(inPlacementName)));
    if (key != null)
    {
      AnalyticsManager._preloadedPlacements.Remove(key);
      if (key.IsContentAvailable() && key.IsContentReady())
        key.ShowContent();
      else
        key.RequestContent();
      inPlacementToShowAndCallbackDictionary.Add(key, inCallbackForPlacementBeingShown);
    }
    else
    {
      TJPlacement placement = TJPlacement.CreatePlacement(inPlacementName);
      placement.RequestContent();
      inPlacementToShowAndCallbackDictionary.Add(placement, inCallbackForPlacementBeingShown);
    }
  }

  private static void TapjoyRerouteActionBasedOnConnectStatus(Action inTapjoyAction)
  {
    if (Tapjoy.get_IsConnected())
      inTapjoyAction();
    else
      AnalyticsManager._tapjoyActionsSavedBeforeConnectSuccess += inTapjoyAction;
  }

  public static void TrackUserID(string userID)
  {
    AnalyticsManager.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.SetUserID(userID)));
    AppsFlyer.setCustomerUserID(userID);
  }

  private static bool Validate(string data)
  {
    return !string.IsNullOrEmpty(data) && AnalyticsManager.encoding.GetByteCount(data) == data.Length && data.Length <= 64;
  }

  private struct TrackingTriggerEventData
  {
    public readonly string ID;
    public readonly string ReportingName;
    public readonly string StepNumber;

    public TrackingTriggerEventData(string inID, string inReportingName, string inStepNumber)
    {
      this.ID = inID;
      this.ReportingName = inReportingName;
      this.StepNumber = inStepNumber;
    }
  }

  public enum TrackingType
  {
    StaminaReward_Video,
    StaminaReward_Milestone,
    Tutorial_HomeScreen_BasicGuideDialog_Continue,
    Tutorial_HomeScreen_BasicGuideDialog_Cancel,
    Tutorial_Download,
    Tutorial_Download_Dialog,
    Tutorial_Download_Start,
    Tutorial_Movie_Intro,
    Tutorial_Movie_AnimeIntro,
    Tutorial_HomeScreen_UnitsGuideDialog_Continue,
    Tutorial_HomeScreen_UnitsGuideDialog_Cancel,
    Player_New,
    Player_Guest,
    Player_FB,
    Tutorial_Movie_World,
    Tutorial_BGDLC,
    Tutorial_SkipDialog_AfterLogiVSDias,
    Tutorial_SkipDialog_BeforeLogiDiasVSVlad,
    Tutorial_SkipDialog_AfterLogiDiasVSVlad,
    Tutorial_SkipDialog_AfterLogiDiasVSNevillePt1,
    Tutorial_SkipDialog_BeforeLogiDiasVSNevillePt2,
    Tutorial_SkipDialog_AfterLogiDiasVSNevillePt2,
    Tutorial_HomeScreen_BasicGuideDialog_Show,
    Tutorial_HomeScreen_UnitsGuideDialog_Show,
    Tutorial_BattleGrid_Show,
  }

  public enum CurrencySubType
  {
    PAID,
    FREE,
  }

  public enum NonPremiumCurrencyType
  {
    Zeni,
    SummonTicket,
    AP,
    Item,
  }
}
