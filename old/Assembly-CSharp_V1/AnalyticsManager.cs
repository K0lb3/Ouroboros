// Decompiled with JetBrains decompiler
// Type: AnalyticsManager
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using gu3.gacct;
using gu3.Payment;
using SRPG;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class AnalyticsManager
{
  private static string LastRecordedSessionID = string.Empty;
  private static object lockObj = new object();
  private static readonly AnalyticsManager.TriggerEventData[] TutorialDialogTrigger = new AnalyticsManager.TriggerEventData[6]{ new AnalyticsManager.TriggerEventData("tut001b.001", "funnel.tutorial.dialogue_start", "7.7"), new AnalyticsManager.TriggerEventData("tut001b.007a", "funnel.tutorial.dialogue_end", "7.8"), new AnalyticsManager.TriggerEventData("tut002a.001", "funnel.tutorial.scene_resistance", "9"), new AnalyticsManager.TriggerEventData("tut003a.001", "funnel.tutorial.scene_trepidation", "10"), new AnalyticsManager.TriggerEventData("tut004a.001", "funnel.tutorial.scene_started", "13"), new AnalyticsManager.TriggerEventData("tut001a.001", "funnel.tutorial.scene_thecode", "6") };
  private static readonly AnalyticsManager.TriggerEventData[] TutorialDialog2DTrigger = new AnalyticsManager.TriggerEventData[3]{ new AnalyticsManager.TriggerEventData("0_6b_2d.001", "funnel.tutorial.scene_ouroboros", "11"), new AnalyticsManager.TriggerEventData("0_6b_2d.017", "funnel.tutorial.guide_summon1", "12"), new AnalyticsManager.TriggerEventData("0_8_2d.001", "funnel.tutorial.scene_throne", "15") };
  private static readonly AnalyticsManager.TriggerEventData[] SGTutorialTrigger = new AnalyticsManager.TriggerEventData[14]{ new AnalyticsManager.TriggerEventData("sg_tut_0.003", "funnel.tutorial.guide_battle_movebehind", " 7.1"), new AnalyticsManager.TriggerEventData("sg_tut_0.004", "funnel.tutorial.guide_battle_basicshield", " 7.2"), new AnalyticsManager.TriggerEventData("sg_tut_0.005", "funnel.tutorial.guide_battle_swordcrush", " 7.3"), new AnalyticsManager.TriggerEventData("sg_tut_0.006", "funnel.tutorial.guide_battle_activateskill", " 7.4"), new AnalyticsManager.TriggerEventData("sg_tut_0.007", "funnel.tutorial.guide_battle_endmove", " 7.5"), new AnalyticsManager.TriggerEventData("sg_tut_1.001", "funnel.tutorial.guide_missions", "17"), new AnalyticsManager.TriggerEventData("sg_tut_1.007", "funnel.tutorial.guide_summon2", "18"), new AnalyticsManager.TriggerEventData("sg_tut_1.008", "funnel.tutorial.guide_summon2_use", "19"), new AnalyticsManager.TriggerEventData("sg_tut_1.009", "funnel.tutorial.guide_summon2_get", "20"), new AnalyticsManager.TriggerEventData("sg_tut_1.011", "funnel.tutorial.guide_challenges", "21"), new AnalyticsManager.TriggerEventData("sg_tut_1.015", "funnel.tutorial.guide_units", "23"), new AnalyticsManager.TriggerEventData("sg_tut_1.032", "funnel.tutorial.guide_quests", "24"), new AnalyticsManager.TriggerEventData("sg_tut_1.035", "funnel.tutorial.guide_party", "25"), new AnalyticsManager.TriggerEventData("sg_tut_1.038", "funnel.tutorial.end", "26") };
  private static readonly string[] GachaCostType = new string[8]{ "None", "Gems", "Paid Gems", "Zeni", "Ticket", "Free Gems", "Zeni", "All" };
  private static readonly string[] GachaSummonType = new string[6]{ "None", "Rare", "Equipment", "Ticket", "Normal", "Special" };
  private static readonly string[] CurrencyTypeString = new string[6]{ "gem", "zeni", "ticket", "ap", "item", "none" };
  private static readonly string LifetimePaidGemsUsed = "Lifetime_Paid_Gems_Used";
  private static readonly string LifetimePaidGemsObtained = "Lifetime_Paid_Gems_Obtained";
  private static readonly string LifetimeFreeGemsUsed = "Lifetime_Free_Gems_Used";
  private static readonly string LifetimeFreeGemsObtained = "Lifetime_Free_Gems_Obtained";
  private static readonly string LifetimeZeniUsed = "Lifetime_Zeni_Used";
  private static readonly string LifetimeZeniObtained = "Lifetime_Zeni_Obtained";
  private static readonly Encoding encoding = Encoding.GetEncoding("utf-8");
  private static readonly string mBundleID = "sg.gumi.alchemistww";
  private const string FIRST_PURCHASE_TAG = "FIRST_PURCHASE";
  private static Dictionary<string, object> summonData;

  private static bool HasPlayerCompletedGameplayPortionOfTutorial
  {
    get
    {
      return (MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) != 0L;
    }
  }

  private static string GetGachaSummonCostTypeString(GachaButton.GachaCostType inGachaCostType)
  {
    int num = (int) inGachaCostType;
    if (num > -1 && num < AnalyticsManager.GachaCostType.Length)
      return AnalyticsManager.GachaCostType[(int) inGachaCostType];
    return (string) null;
  }

  private static string GetGachaSummonTypeString(GachaWindow.GachaTabCategory inGachaType)
  {
    int num = (int) inGachaType;
    if (num > -1 && num < AnalyticsManager.GachaSummonType.Length)
      return AnalyticsManager.GachaSummonType[(int) inGachaType];
    return (string) null;
  }

  private static string GetCurrencyStringType(AnalyticsManager.CurrencyType inCurrencyType)
  {
    int num = (int) inCurrencyType;
    if (num > -1 && num < AnalyticsManager.CurrencyTypeString.Length)
      return AnalyticsManager.CurrencyTypeString[(int) inCurrencyType];
    return (string) null;
  }

  public static void Setup()
  {
    AppsFlyer.setAppsFlyerKey("WMa4kPf8ZdvNhcpvdpwAvE");
    AppsFlyer.setIsDebug(true);
    AnalyticsManager.InitialiseAndroid();
    DebugUtility.Log("AnalyticsManager : Setup");
    GameObject gameObject = new GameObject("AppsFlyerTrackerCallbacks");
    gameObject.AddComponent<AppsFlyerTrackerCallbacks>();
    Object.DontDestroyOnLoad((Object) gameObject);
    AnalyticsManager.SetCurrentVersion();
    AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.splash", new Dictionary<string, object>()
    {
      {
        "step_number",
        (object) "1"
      }
    });
  }

  private static void InitialiseIOS()
  {
    Upsight.init();
  }

  private static void InitialiseAndroid()
  {
    AppsFlyer.setAppID(AnalyticsManager.mBundleID);
    AppsFlyer.createValidateInAppListener("AppsFlyerTrackerCallbacks", "onInAppBillingSuccess", "onInAppBillingFailure");
    Upsight.init();
  }

  public static void TrackAppLaunch()
  {
    AppsFlyer.init("WMa4kPf8ZdvNhcpvdpwAvE", "AppsFlyerTrackerCallbacks");
    Upsight.registerForPushNotifications();
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
    AnalyticsManager.RecordCustomEvent("summon", AnalyticsManager.summonData);
    AnalyticsManager.summonData.Clear();
  }

  public static void TrackCurrencyObtain(AnalyticsManager.CurrencyType inCurrencyType, AnalyticsManager.CurrencySubType inCurrencySubType, long inAmount, string inObtainSource, Dictionary<string, object> inExtraEventsData = null)
  {
    if (inAmount <= 0L)
      return;
    Dictionary<string, object> eventContent = new Dictionary<string, object>();
    switch (inCurrencyType)
    {
      case AnalyticsManager.CurrencyType.Gem:
        eventContent["type"] = (object) inCurrencyType;
        switch (inCurrencySubType)
        {
          case AnalyticsManager.CurrencySubType.PAID:
            eventContent["subtype"] = (object) "paid";
            AnalyticsManager.IncrementUserAttribute(AnalyticsManager.LifetimePaidGemsObtained, inAmount);
            break;
          case AnalyticsManager.CurrencySubType.FREE:
            eventContent["subtype"] = (object) "free";
            AnalyticsManager.IncrementUserAttribute(AnalyticsManager.LifetimeFreeGemsObtained, inAmount);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      case AnalyticsManager.CurrencyType.Zeni:
        AnalyticsManager.IncrementUserAttribute(AnalyticsManager.LifetimeZeniObtained, inAmount);
        break;
    }
    eventContent["amount"] = (object) inAmount;
    eventContent["source"] = (object) inObtainSource;
    if (inExtraEventsData != null)
    {
      using (Dictionary<string, object>.Enumerator enumerator = inExtraEventsData.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<string, object> current = enumerator.Current;
          eventContent[current.Key] = current.Value;
        }
      }
    }
    AnalyticsManager.RecordCustomEvent("currency.obtain." + AnalyticsManager.GetCurrencyStringType(inCurrencyType), eventContent);
  }

  public static void TrackCurrencyUse(AnalyticsManager.CurrencyType inCurrencyType, AnalyticsManager.CurrencySubType inCurrencySubType, long inAmount, string inSinkSource, Dictionary<string, object> inExtraEventsData = null)
  {
    if (inAmount <= 0L)
      return;
    Dictionary<string, object> eventContent = new Dictionary<string, object>();
    switch (inCurrencyType)
    {
      case AnalyticsManager.CurrencyType.Gem:
        eventContent["type"] = (object) AnalyticsManager.GetCurrencyStringType(inCurrencyType);
        switch (inCurrencySubType)
        {
          case AnalyticsManager.CurrencySubType.PAID:
            AnalyticsManager.IncrementUserAttribute(AnalyticsManager.LifetimePaidGemsUsed, inAmount);
            eventContent["subtype"] = (object) "paid";
            break;
          case AnalyticsManager.CurrencySubType.FREE:
            AnalyticsManager.IncrementUserAttribute(AnalyticsManager.LifetimeFreeGemsUsed, inAmount);
            eventContent["subtype"] = (object) "free";
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      case AnalyticsManager.CurrencyType.Zeni:
        AnalyticsManager.IncrementUserAttribute(AnalyticsManager.LifetimeZeniUsed, inAmount);
        break;
    }
    eventContent["amount"] = (object) inAmount;
    eventContent["sink"] = (object) inSinkSource;
    if (inExtraEventsData != null)
    {
      using (Dictionary<string, object>.Enumerator enumerator = inExtraEventsData.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<string, object> current = enumerator.Current;
          eventContent[current.Key] = current.Value;
        }
      }
    }
    AnalyticsManager.RecordCustomEvent("currency.use." + AnalyticsManager.GetCurrencyStringType(inCurrencyType), eventContent);
  }

  public static bool TrackPurchase(Product product, VerifyResponse response, GlobalVars.PurchaseType purchaseType, string receipt, Purchase purchase)
  {
    lock (AnalyticsManager.lockObj)
    {
      string id = (string) product.id;
      string currency = (string) product.currency;
      double price = (double) product.price;
      if (!AnalyticsManager.Validate(id) || !AnalyticsManager.Validate(currency) || price <= 0.0)
        return false;
      string eventName;
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      if ((^(GACCTError&) @((Allocator<MetaVerify, Verify>) response).get_Meta().error).isError == null)
      {
        if (!PlayerPrefs.HasKey("FIRST_PURCHASE"))
        {
          PlayerPrefs.SetInt("FIRST_PURCHASE", 1);
          AnalyticsManager.RecordCustomEvent("user.paying.first", (Dictionary<string, object>) null);
          AnalyticsManager.RecordCustomEvent("revenue.paying.first", (Dictionary<string, object>) null);
        }
        AnalyticsManager.TrackCurrencyObtain(AnalyticsManager.CurrencyType.Gem, AnalyticsManager.CurrencySubType.PAID, (long) ((Allocator<MetaVerify, Verify>) response).get_Meta().AdditionalPaidCoin, "IAP Purchase", (Dictionary<string, object>) null);
        AnalyticsManager.TrackCurrencyObtain(AnalyticsManager.CurrencyType.Gem, AnalyticsManager.CurrencySubType.FREE, (long) ((Allocator<MetaVerify, Verify>) response).get_Meta().AdditionalFreeCoin, "IAP Purchase", (Dictionary<string, object>) null);
        AppsFlyer.setCurrencyCode("USD");
        AppsFlyer.trackRichEvent(nameof (purchase), new Dictionary<string, string>()
        {
          {
            "product_id",
            id
          },
          {
            "af_currency",
            (string) product.currency
          },
          {
            "af_revenue",
            price.ToString()
          }
        });
        string inAppPurchaseData = "{\"orderID\":" + (object) purchase.orderId + "\",\"packageName\":\" " + AnalyticsManager.mBundleID + "\",\"productId\":\"" + id + "\",\"purchaseTime\":" + (object) Network.GetServerTime() + ",\"purchaseState\":0,\"developerPayload\":\"paymentPayload\",\"purchaseToken\":\"" + (object) purchase.data + "\"}";
        Upsight.recordGooglePlayPurchase(1, currency, price, price, id, 0, inAppPurchaseData, receipt, (Dictionary<string, object>) null);
        eventName = "purchase.success.";
      }
      else
        eventName = "purchase.fail.";
      AnalyticsManager.RecordCustomEvent(eventName, new Dictionary<string, object>()
      {
        {
          "price",
          (object) price
        },
        {
          "purchase_type",
          (object) purchaseType
        },
        {
          "id",
          (object) id
        },
        {
          "gem_type",
          (object) AnalyticsManager.CurrencySubType.PAID
        }
      });
      return true;
    }
  }

  private static void TrackSpend(ESaleType inSaleType, string inSpendSource, int inSpendAmount)
  {
    if (!AnalyticsManager.Validate(inSpendSource))
      return;
    switch (inSaleType)
    {
      case ESaleType.Gold:
        AnalyticsManager.TrackCurrencyUse(AnalyticsManager.CurrencyType.Zeni, AnalyticsManager.CurrencySubType.FREE, (long) inSpendAmount, inSpendSource, (Dictionary<string, object>) null);
        break;
      case ESaleType.Coin:
        AnalyticsManager.TrackCurrencyUse(AnalyticsManager.CurrencyType.Gem, AnalyticsManager.CurrencySubType.FREE, (long) inSpendAmount, inSpendSource, (Dictionary<string, object>) null);
        break;
      case ESaleType.Coin_P:
        AnalyticsManager.TrackCurrencyUse(AnalyticsManager.CurrencyType.Gem, AnalyticsManager.CurrencySubType.PAID, (long) inSpendAmount, inSpendSource, (Dictionary<string, object>) null);
        break;
    }
  }

  private static bool Validate(string data)
  {
    return !string.IsNullOrEmpty(data) && AnalyticsManager.encoding.GetByteCount(data) == data.Length && data.Length <= 64;
  }

  public static void TrackPlayerLevelUp(int currentLevel)
  {
    AnalyticsManager.RecordCustomEvent("levelup", new Dictionary<string, object>()
    {
      {
        "level",
        (object) currentLevel
      }
    });
    AnalyticsManager.RecordUserAttribute("Account_Level", currentLevel);
    AppsFlyer.trackRichEvent("level_achieved", new Dictionary<string, string>()
    {
      {
        "level_achieved",
        currentLevel.ToString()
      }
    });
  }

  public static void TrackSpendCoin(string inSpendSource, int inSpendAmount)
  {
    AnalyticsManager.TrackSpend(ESaleType.Coin, inSpendSource, inSpendAmount);
  }

  public static void TrackSpendShop(ESaleType sale_type, EShopType shop_type, int inSpendAmount)
  {
    AnalyticsManager.TrackSpend(sale_type, "ShopBuy." + (object) shop_type, inSpendAmount);
  }

  public static void TrackSpendShopUpdate(ESaleType sale_type, EShopType shop_type, int inSpendAmount)
  {
    AnalyticsManager.TrackSpend(sale_type, "ShopUpdate." + (object) shop_type, inSpendAmount);
  }

  public static void TrackTutorialAnalyticsEvent(string inTag, AnalyticsManager.TutorialTrackingEventType inTrackingEventType)
  {
    if (AnalyticsManager.HasPlayerCompletedGameplayPortionOfTutorial)
      return;
    AnalyticsManager.TriggerEventData[] triggerEventDataArray;
    switch (inTrackingEventType)
    {
      case AnalyticsManager.TutorialTrackingEventType.EVENT_DIALOG:
        triggerEventDataArray = AnalyticsManager.TutorialDialogTrigger;
        break;
      case AnalyticsManager.TutorialTrackingEventType.EVENT_DIALOG_2D:
        triggerEventDataArray = AnalyticsManager.TutorialDialog2DTrigger;
        break;
      case AnalyticsManager.TutorialTrackingEventType.SG_TUTORIAL:
        triggerEventDataArray = AnalyticsManager.SGTutorialTrigger;
        break;
      default:
        triggerEventDataArray = (AnalyticsManager.TriggerEventData[]) null;
        break;
    }
    for (int index = 0; index < triggerEventDataArray.Length; ++index)
    {
      if (triggerEventDataArray[index].Tag.Equals(inTag))
      {
        AnalyticsManager.TrackTutorialCustomEvent(triggerEventDataArray[index].EventName, new Dictionary<string, object>()
        {
          {
            "step_number",
            (object) triggerEventDataArray[index].StepNumber
          }
        });
        break;
      }
    }
  }

  public static void TrackTutorialCustomEvent(string eventName, Dictionary<string, object> inExtraData)
  {
    if (AnalyticsManager.HasPlayerCompletedGameplayPortionOfTutorial || PlayerPrefs.HasKey(eventName))
      return;
    PlayerPrefs.SetString(eventName, string.Empty);
    AnalyticsManager.RecordCustomEvent(eventName, inExtraData);
    AppsFlyer.trackRichEvent(eventName, inExtraData.ConvertValuesToString<string, object>());
  }

  private static void RecordCustomEvent(string eventName, Dictionary<string, object> eventContent = null)
  {
    Upsight.recordCustomEvent(eventName, eventContent);
  }

  private static void RecordUserAttribute(string inAttributeKey, string inAttributeValue, string inCheckAgainstValue = null)
  {
    if (!string.IsNullOrEmpty(inCheckAgainstValue))
    {
      string userAttributeString = Upsight.getUserAttributeString(inAttributeKey);
      if (userAttributeString != null && userAttributeString.Equals(inCheckAgainstValue))
        return;
      Upsight.setUserAttributeString(inAttributeKey, inAttributeValue);
    }
    else
      Upsight.setUserAttributeString(inAttributeKey, inAttributeValue);
  }

  private static void RecordUserAttribute(string inAttributeKey, int inAttributeValue)
  {
    Upsight.setUserAttributeInt(inAttributeKey, inAttributeValue);
  }

  private static void IncrementUserAttribute(string inAttributeKey, long inAttributeIncrementValue)
  {
    string userAttributeString = Upsight.getUserAttributeString(inAttributeKey);
    long num1 = 0;
    if (!string.IsNullOrEmpty(userAttributeString))
      num1 = Convert.ToInt64(userAttributeString);
    long num2 = num1 + inAttributeIncrementValue;
    Upsight.setUserAttributeString(inAttributeKey, num2.ToString());
  }

  public static void RecordNewPlayerLogin()
  {
    AnalyticsManager.RecordCustomEvent("user.new", new Dictionary<string, object>()
    {
      {
        "user_id",
        (object) GlobalVars.CustomerID
      }
    });
    AnalyticsManager.RecordUserAttribute("Start_Version", MyApplicationPlugin.get_version(), "0");
  }

  public static void RecordGuestLogin()
  {
    AnalyticsManager.RecordCustomEvent("user.account.guest", new Dictionary<string, object>()
    {
      {
        "user_id",
        (object) GlobalVars.CustomerID
      }
    });
    AnalyticsManager.RecordUserAttribute("Account_Type", "guest", (string) null);
  }

  public static void RecordFacebookLogin()
  {
    AnalyticsManager.RecordCustomEvent("user.account.facebook", new Dictionary<string, object>()
    {
      {
        "user_id",
        (object) GlobalVars.CustomerID
      }
    });
  }

  public static bool RecordMileStone(string mileStoneName)
  {
    if (AnalyticsManager.LastRecordedSessionID.Equals(Network.SessionID))
      return true;
    AnalyticsManager.LastRecordedSessionID = Network.SessionID;
    Upsight.recordMilestoneEvent(mileStoneName, (Dictionary<string, object>) null);
    if (!Upsight.isContentReadyForBillboardWithScope(mileStoneName))
      return false;
    Upsight.prepareBillboard(mileStoneName);
    return true;
  }

  public static bool AttemptToShowBillBoard(string mileStoneName)
  {
    if (!Upsight.isContentReadyForBillboardWithScope(mileStoneName))
      return false;
    Upsight.prepareBillboard(mileStoneName);
    return true;
  }

  public static void LinkWithAndSetUserID(string userID)
  {
    AnalyticsManager.RecordUserAttribute("User_ID", userID, "0");
    AnalyticsManager.RecordCustomEvent("link_userid_sid", new Dictionary<string, object>()
    {
      {
        "sid",
        (object) Upsight.getSid()
      },
      {
        "userid",
        (object) userID
      }
    });
    AppsFlyer.setCustomerUserID(userID);
  }

  private static void SetCurrentVersion()
  {
    AnalyticsManager.RecordUserAttribute("Current_Version", MyApplicationPlugin.get_version(), (string) null);
    AnalyticsManager.RecordCustomEvent("currentVersion_update", new Dictionary<string, object>()
    {
      {
        "sid",
        (object) Upsight.getSid()
      },
      {
        "currentVersion",
        (object) MyApplicationPlugin.get_version()
      }
    });
  }

  public static void SetFirstMissionClear(string missionID)
  {
    if (!missionID.Equals("QE_ST_NO_010001") || PlayerPrefs.HasKey(missionID))
      return;
    PlayerPrefs.SetString(missionID, string.Empty);
    AppsFlyer.trackRichEvent("first_quest_clear", new Dictionary<string, string>()
    {
      {
        "mission",
        missionID
      }
    });
  }

  private struct TriggerEventData
  {
    public readonly string Tag;
    public readonly string EventName;
    public readonly string StepNumber;

    public TriggerEventData(string inTag, string inEventName, string inStepNumber)
    {
      this.Tag = inTag;
      this.EventName = inEventName;
      this.StepNumber = inStepNumber;
    }
  }

  public enum TutorialTrackingEventType
  {
    EVENT_DIALOG,
    EVENT_DIALOG_2D,
    SG_TUTORIAL,
  }

  public enum CurrencySubType
  {
    PAID,
    FREE,
  }

  public enum CurrencyType
  {
    Gem,
    Zeni,
    SummonTicket,
    AP,
    Item,
    None,
  }
}
