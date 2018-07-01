// Decompiled with JetBrains decompiler
// Type: SRPG.Network
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using Gsc;
using Gsc.App.NetworkHelper;
using Gsc.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Experimental.Networking;

namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/Manager/Network")]
  public class Network : MonoSingleton<SRPG.Network>
  {
    public static readonly float WEBAPI_TIMEOUT_SEC = 60f;
    public static SRPG.Network.EConnectMode Mode = SRPG.Network.EConnectMode.Offline;
    public static readonly string OfficialUrl = "https://al.fg-games.co.jp/";
    public static readonly string DefaultHost = "https://alchemist.gu3.jp/";
    public static readonly string DefaultDLHost = "https://alchemist.gu3.jp/";
    public static readonly string DefaultSiteHost = "https://st-al.fg-games.co.jp/";
    public static readonly string DefaultNewsHost = "https://st-al.fg-games.co.jp/";
    private string mSessionID = string.Empty;
    private string mVersion = string.Empty;
    private string mAssets = string.Empty;
    private string mAssetsEx = string.Empty;
    private int mTicket = 1;
    private List<WebAPI> mRequests = new List<WebAPI>(4);
    private bool mIndicator = true;
    private string mDefaultHostConfigured = SRPG.Network.DefaultHost;
    public static SRPG.Network.RequestResults RequestResult;
    private static long ServerTime;
    private static long LastRealTime;
    private bool mBusy;
    private bool mRetry;
    private bool mError;
    private string mErrMsg;
    private SRPG.Network.EErrCode mErrCode;
    private bool mImmediateMode;
    private WebAPI mCurrentRequest;
    private UnityWebRequest mWebReq;
    private bool mAbort;
    private bool mNoVersion;
    private bool mForceBusy;

    public static bool IsImmediateMode
    {
      get
      {
        return MonoSingleton<SRPG.Network>.Instance.mImmediateMode;
      }
    }

    public static void SetDefaultHostConfigured(string host)
    {
      MonoSingleton<SRPG.Network>.Instance.mDefaultHostConfigured = host;
    }

    public static string GetDefaultHostConfigured()
    {
      return MonoSingleton<SRPG.Network>.Instance.mDefaultHostConfigured;
    }

    public static Gsc.App.Environment GetEnvironment
    {
      get
      {
        return SDK.Configuration.GetEnv<Gsc.App.Environment>();
      }
    }

    public static string Host
    {
      get
      {
        return SRPG.Network.GetEnvironment.ServerUrl;
      }
    }

    public static string DLHost
    {
      get
      {
        return SRPG.Network.GetEnvironment.DLHost;
      }
    }

    public static string SiteHost
    {
      get
      {
        return SRPG.Network.GetEnvironment.SiteHost;
      }
    }

    public static string NewsHost
    {
      get
      {
        return SRPG.Network.GetEnvironment.NewsHost;
      }
    }

    public static string Digest
    {
      get
      {
        return SRPG.Network.GetEnvironment.Digest;
      }
    }

    public static string Pub
    {
      get
      {
        return SRPG.Network.GetEnvironment.Pub;
      }
    }

    public static string PubU
    {
      get
      {
        return SRPG.Network.GetEnvironment.PubU;
      }
    }

    public static string AssetVersion
    {
      get
      {
        return MonoSingleton<SRPG.Network>.Instance.mAssets;
      }
      set
      {
        MonoSingleton<SRPG.Network>.Instance.mAssets = value;
      }
    }

    public static string AssetVersionEx
    {
      get
      {
        return MonoSingleton<SRPG.Network>.Instance.mAssetsEx;
      }
      set
      {
        MonoSingleton<SRPG.Network>.Instance.mAssetsEx = value;
      }
    }

    public static string Version
    {
      get
      {
        return MonoSingleton<SRPG.Network>.Instance.mVersion;
      }
      set
      {
        MonoSingleton<SRPG.Network>.Instance.mVersion = value;
      }
    }

    public static string SessionID
    {
      get
      {
        return MonoSingleton<SRPG.Network>.Instance.mSessionID;
      }
      set
      {
        MonoSingleton<SRPG.Network>.Instance.mSessionID = value;
      }
    }

    public static int TicketID
    {
      get
      {
        return MonoSingleton<SRPG.Network>.Instance.mTicket;
      }
      private set
      {
        MonoSingleton<SRPG.Network>.Instance.mTicket = value;
      }
    }

    public static bool IsBusy
    {
      get
      {
        if (MonoSingleton<SRPG.Network>.Instance.mBusy)
          return true;
        if (WebQueue.defaultQueue != null)
          return WebQueue.defaultQueue.isRunning;
        return false;
      }
      private set
      {
        MonoSingleton<SRPG.Network>.Instance.mBusy = value;
      }
    }

    public static bool IsRetry
    {
      get
      {
        return MonoSingleton<SRPG.Network>.Instance.mRetry;
      }
      set
      {
        MonoSingleton<SRPG.Network>.Instance.mRetry = value;
      }
    }

    public static bool IsError
    {
      get
      {
        if (!MonoSingleton<SRPG.Network>.Instance.mError)
          return GsccBridge.HasUnhandledTasks;
        return true;
      }
      private set
      {
        MonoSingleton<SRPG.Network>.Instance.mError = value;
      }
    }

    public static string ErrMsg
    {
      get
      {
        return MonoSingleton<SRPG.Network>.Instance.mErrMsg;
      }
      set
      {
        MonoSingleton<SRPG.Network>.Instance.mErrMsg = value;
      }
    }

    public static SRPG.Network.EErrCode ErrCode
    {
      get
      {
        return MonoSingleton<SRPG.Network>.Instance.mErrCode;
      }
      set
      {
        MonoSingleton<SRPG.Network>.Instance.mErrCode = value;
      }
    }

    public static bool IsConnecting
    {
      get
      {
        if (!SRPG.Network.IsBusy)
          return MonoSingleton<SRPG.Network>.Instance.mRequests.Count > 0;
        return true;
      }
    }

    public static bool IsIndicator
    {
      get
      {
        return MonoSingleton<SRPG.Network>.Instance.mIndicator;
      }
      set
      {
        MonoSingleton<SRPG.Network>.Instance.mIndicator = value;
      }
    }

    public static UnityWebRequest uniWebRequest
    {
      get
      {
        return MonoSingleton<SRPG.Network>.Instance.mWebReq;
      }
      set
      {
        MonoSingleton<SRPG.Network>.Instance.mWebReq = value;
      }
    }

    public static bool IsStreamConnecting
    {
      get
      {
        return SRPG.Network.uniWebRequest != null;
      }
    }

    public static bool Aborted
    {
      get
      {
        return MonoSingleton<SRPG.Network>.Instance.mAbort;
      }
      set
      {
        MonoSingleton<SRPG.Network>.Instance.mAbort = value;
      }
    }

    public static bool IsNoVersion
    {
      get
      {
        return MonoSingleton<SRPG.Network>.Instance.mNoVersion;
      }
      set
      {
        MonoSingleton<SRPG.Network>.Instance.mNoVersion = value;
      }
    }

    public static bool IsForceBusy
    {
      get
      {
        return MonoSingleton<SRPG.Network>.Instance.mForceBusy;
      }
      set
      {
        MonoSingleton<SRPG.Network>.Instance.mForceBusy = value;
      }
    }

    protected override void Initialize()
    {
      SRPG.Network.Reset();
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
    }

    protected override void Release()
    {
    }

    public static void Reset()
    {
      MonoSingleton<SRPG.Network>.Instance.mTicket = 1;
      MonoSingleton<SRPG.Network>.Instance.mRequests.Clear();
      GsccBridge.Reset();
    }

    public static void RequestAPI(WebAPI api, bool highPriority = false)
    {
      DebugUtility.Log("Request WebAPI: " + api.name);
      if (highPriority)
        MonoSingleton<SRPG.Network>.Instance.mRequests.Insert(0, api);
      else
        MonoSingleton<SRPG.Network>.Instance.mRequests.Add(api);
      if (MonoSingleton<SRPG.Network>.Instance.mRequests.Count != 1)
        return;
      CriticalSection.Enter(CriticalSections.Network);
    }

    public static void RequestAPIImmediate(WebAPI api, bool autoRetry)
    {
      MonoSingleton<SRPG.Network>.Instance.mImmediateMode = true;
      GsccBridge.SendImmediate(api);
      if (!MonoSingleton<SRPG.Network>.Instance.mImmediateMode)
        return;
      MonoSingleton<SRPG.Network>.Instance.mImmediateMode = false;
      if (!autoRetry)
        return;
      --SRPG.Network.TicketID;
      SRPG.Network.ResetError();
      SRPG.Network.RequestAPI(api, true);
    }

    public static void RemoveAPI()
    {
      GsccBridge.Reset();
      if (MonoSingleton<SRPG.Network>.Instance.mImmediateMode)
        MonoSingleton<SRPG.Network>.Instance.mImmediateMode = false;
      else if (MonoSingleton<SRPG.Network>.Instance.mRequests.Count <= 0)
      {
        DebugUtility.LogWarning("Instance.mRequestsGsc.Count <= 0");
      }
      else
      {
        MonoSingleton<SRPG.Network>.Instance.mRequests.Remove(MonoSingleton<SRPG.Network>.Instance.mCurrentRequest);
        if (MonoSingleton<SRPG.Network>.Instance.mRequests.Count != 0)
          return;
        CriticalSection.Leave(CriticalSections.Network);
      }
    }

    public static void ResetError()
    {
      MonoSingleton<SRPG.Network>.Instance.mError = false;
    }

    public static void SetRetry()
    {
      GsccBridge.Retry();
    }

    private static int FindStat(string response)
    {
      Regex regex = new Regex("\"stat\":(?<stat>\\d+)", RegexOptions.None);
      if (!regex.Match(response).Success)
        return 0;
      return Convert.ToInt32(regex.Match(response).Result("${stat}"));
    }

    private static string FindMessage(string response)
    {
      Regex regex = new Regex("\"stat_msg\":\"(?<stat_msg>.+?)\"[,}]", RegexOptions.None);
      if (!regex.Match(response).Success)
        return string.Empty;
      return regex.Match(response).Result("${stat_msg}");
    }

    private static long FindTime(string response)
    {
      Regex regex = new Regex("\"time\":(?<time>\\d+)", RegexOptions.None);
      if (!regex.Match(response).Success)
        return 0;
      return Convert.ToInt64(regex.Match(response).Result("${time}"));
    }

    public static long GetServerTime()
    {
      if (SRPG.Network.Mode == SRPG.Network.EConnectMode.Offline)
        return TimeManager.Now();
      long systemUptime = SRPG.Network.GetSystemUptime();
      return SRPG.Network.ServerTime + (systemUptime - SRPG.Network.LastRealTime);
    }

    public static long LastConnectionTime
    {
      get
      {
        return SRPG.Network.ServerTime;
      }
    }

    private void Update()
    {
      if (SRPG.Network.IsBusy || SRPG.Network.IsError || (SRPG.Network.IsForceBusy || this.mRequests.Count <= 0))
        return;
      WebAPI mRequest = this.mRequests[0];
      if (mRequest == null)
        return;
      if (mRequest.reqtype == WebAPI.ReqeustType.REQ_GSC)
        this.ConnectingGsc(mRequest);
      else
        this.StartCoroutine(SRPG.Network.Connecting(mRequest));
    }

    private void ConnectingGsc(WebAPI api)
    {
      ++SRPG.Network.TicketID;
      SRPG.Network.IsError = false;
      SRPG.Network.ErrCode = SRPG.Network.EErrCode.Success;
      MonoSingleton<SRPG.Network>.Instance.mCurrentRequest = api;
      GsccBridge.Send(api, false);
    }

    public static void ConnectingResponse(WebResponse response, SRPG.Network.ResponseCallback callback)
    {
      SRPG.Network.ErrCode = response.ErrorCode;
      SRPG.Network.ErrMsg = response.ErrorMessage;
      SRPG.Network.IsError = SRPG.Network.ErrCode != SRPG.Network.EErrCode.Success;
      if (FlowNode_Network.HasCommonError(response.Result) || callback == null)
        return;
      if (response.ServerTime != 0L)
        SRPG.Network.ServerTime = response.ServerTime;
      SRPG.Network.LastRealTime = SRPG.Network.GetSystemUptime();
      callback(response.Result);
    }

    public static void SetServerTime(long time)
    {
      if (time != 0L)
        SRPG.Network.ServerTime = time;
      SRPG.Network.LastRealTime = SRPG.Network.GetSystemUptime();
    }

    [DebuggerHidden]
    private static IEnumerator Connecting(WebAPI api)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SRPG.Network.\u003CConnecting\u003Ec__Iterator183()
      {
        api = api,
        \u003C\u0024\u003Eapi = api
      };
    }

    public static void SetServerSessionExpired()
    {
      SRPG.Network.ErrCode = SRPG.Network.EErrCode.DmmSessionExpired;
      SRPG.Network.ErrMsg = LocalizedText.Get("embed.DMM_EXPIRED");
      SRPG.Network.IsError = true;
    }

    public static void SetServerMetaDataAsError()
    {
      SRPG.Network.ErrCode = SRPG.Network.EErrCode.Failed;
      SRPG.Network.ErrMsg = LocalizedText.Get("embed.NETWORKERR");
      SRPG.Network.IsError = true;
    }

    public static void SetServerInvalidDeviceError()
    {
      SRPG.Network.ErrCode = SRPG.Network.EErrCode.Authorize;
      SRPG.Network.ErrMsg = LocalizedText.Get("sys.AUTHORIZEERR");
      SRPG.Network.IsError = true;
    }

    public static void SetServerMetaDataAsError(SRPG.Network.EErrCode code, string msg)
    {
      SRPG.Network.ErrCode = code;
      SRPG.Network.ErrMsg = msg;
      SRPG.Network.IsError = true;
    }

    private static long GetSystemUptime()
    {
      return (long) Time.get_realtimeSinceStartup();
    }

    public static void Abort()
    {
      if (SRPG.Network.uniWebRequest == null)
        return;
      SRPG.Network.uniWebRequest.Abort();
      SRPG.Network.Aborted = true;
    }

    public enum EErrCode
    {
      TimeOut = -2,
      Failed = -1,
      Success = 0,
      Unknown = 1,
      Version = 2,
      AssetVersion = 3,
      NoVersionDbg = 4,
      NoSID = 100, // 0x00000064
      Maintenance = 200, // 0x000000C8
      ChatMaintenance = 201, // 0x000000C9
      MultiMaintenance = 202, // 0x000000CA
      VsMaintenance = 203, // 0x000000CB
      BattleRecordMaintenance = 204, // 0x000000CC
      MultiVersionMaintenance = 205, // 0x000000CD
      MultiTowerMaintenance = 206, // 0x000000CE
      RankingQuestMaintenance = 207, // 0x000000CF
      IllegalParam = 300, // 0x0000012C
      API = 400, // 0x00000190
      NotLocation = 401, // 0x00000191
      NoFile = 1000, // 0x000003E8
      NoVersion = 1100, // 0x0000044C
      SessionFailure = 1200, // 0x000004B0
      CreateStopped = 1300, // 0x00000514
      IllegalName = 1400, // 0x00000578
      NoMail = 1500, // 0x000005DC
      MailReadable = 1501, // 0x000005DD
      ReqFriendRequestMax = 1600, // 0x00000640
      ReqFriendIsFull = 1601, // 0x00000641
      ReqNoFriend = 1602, // 0x00000642
      ReqFriendRegistered = 1603, // 0x00000643
      ReqFriendRequesting = 1604, // 0x00000644
      RmNoFriend = 1700, // 0x000006A4
      RmFriendIsMe = 1701, // 0x000006A5
      NoUnitParty = 1800, // 0x00000708
      IllegalParty = 1801, // 0x00000709
      ExpMaterialShort = 1900, // 0x0000076C
      RareMaterialShort = 2000, // 0x000007D0
      RarePlayerLvShort = 2001, // 0x000007D1
      PlusMaterialShot = 2100, // 0x00000834
      PlusPlayerLvShort = 2101, // 0x00000835
      AbilityMaterialShort = 2200, // 0x00000898
      AbilityNotFound = 2201, // 0x00000899
      NoJobSetJob = 2300, // 0x000008FC
      CantSelectJob = 2301, // 0x000008FD
      NoUnitSetJob = 2302, // 0x000008FE
      NoAbilitySetAbility = 2400, // 0x00000960
      NoJobSetAbility = 2401, // 0x00000961
      UnsetAbility = 2402, // 0x00000962
      NoJobSetEquip = 2500, // 0x000009C4
      NoEquipItem = 2501, // 0x000009C5
      Equipped = 2502, // 0x000009C6
      NoJobEnforceEquip = 2600, // 0x00000A28
      NoEquipEnforce = 2601, // 0x00000A29
      ForceMax = 2602, // 0x00000A2A
      MaterialShort = 2603, // 0x00000A2B
      EnforcePlayerLvShort = 2604, // 0x00000A2C
      NoJobLvUpEquip = 2700, // 0x00000A8C
      EquipNotComp = 2701, // 0x00000A8D
      PlusShort = 2702, // 0x00000A8E
      NoItemSell = 2800, // 0x00000AF0
      ConvertAnotherItem = 2801, // 0x00000AF1
      StaminaCoinShort = 2900, // 0x00000B54
      AddStaminaLimit = 2901, // 0x00000B55
      AbilityCoinShort = 3000, // 0x00000BB8
      AbilityVipLvShort = 3001, // 0x00000BB9
      AbilityPlayerLvShort = 3002, // 0x00000BBA
      GouseiNoTarget = 3200, // 0x00000C80
      GouseiMaterialShort = 3201, // 0x00000C81
      GouseiCostShort = 3202, // 0x00000C82
      UnSelectable = 3300, // 0x00000CE4
      OutOfDateQuest = 3301, // 0x00000CE5
      QuestNotEnd = 3302, // 0x00000CE6
      ChallengeLimit = 3303, // 0x00000CE7
      NotGpsQuest = 3308, // 0x00000CEC
      RecordLimitUpload = 3309, // 0x00000CED
      QuestResume = 3400, // 0x00000D48
      QuestEnd = 3500, // 0x00000DAC
      ContinueCostShort = 3600, // 0x00000E10
      CantContinue = 3601, // 0x00000E11
      NoBtlInfo = 3700, // 0x00000E74
      MultiPlayerLvShort = 3701, // 0x00000E75
      MultiBtlNotEnd = 3702, // 0x00000E76
      MultiVersionMismatch = 3704, // 0x00000E78
      ColoCantSelect = 3800, // 0x00000ED8
      ColoIsBusy = 3801, // 0x00000ED9
      ColoCostShort = 3802, // 0x00000EDA
      ColoIntervalShort = 3803, // 0x00000EDB
      ColoBattleNotEnd = 3804, // 0x00000EDC
      ColoPlayerLvShort = 3805, // 0x00000EDD
      ColoVipShort = 3806, // 0x00000EDE
      ColoRankLower = 3807, // 0x00000EDF
      ColoNoBattle = 3900, // 0x00000F3C
      ColoRankModify = 3901, // 0x00000F3D
      ColoMyRankModify = 3902, // 0x00000F3E
      NoGacha = 4000, // 0x00000FA0
      GachaCostShort = 4001, // 0x00000FA1
      GachaItemMax = 4002, // 0x00000FA2
      GachaNotFree = 4003, // 0x00000FA3
      GachaPaidLimitOver = 4004, // 0x00000FA4
      GachaPlyLvOver = 4005, // 0x00000FA5
      GachaPlyNewOver = 4006, // 0x00000FA6
      GachaLimitSoldOut = 4007, // 0x00000FA7
      GachaLimitCntOver = 4008, // 0x00000FA8
      GachaOutofPeriod = 4010, // 0x00000FAA
      TrophyRewarded = 4100, // 0x00001004
      TrophyOutOfDate = 4101, // 0x00001005
      TrophyRollBack = 4102, // 0x00001006
      BingoOutofDateReceive = 4112, // 0x00001010
      ShopRefreshCostShort = 4200, // 0x00001068
      ShopRefreshLvSort = 4201, // 0x00001069
      ShopSoldOut = 4300, // 0x000010CC
      ShopBuyCostShort = 4301, // 0x000010CD
      ShopBuyLvShort = 4302, // 0x000010CE
      ShopBuyNotFound = 4303, // 0x000010CF
      ShopBuyItemNotFound = 4304, // 0x000010D0
      ShopRefreshItemList = 4305, // 0x000010D1
      ShopBuyOutofItemPeriod = 4306, // 0x000010D2
      GoldBuyCostShort = 4400, // 0x00001130
      GoldBuyLimit = 4401, // 0x00001131
      EventShopOutOfPeriod = 4403, // 0x00001133
      LimitedShopOutOfPeriod = 4403, // 0x00001133
      ShopBuyOutofPeriod = 4403, // 0x00001133
      EventShopOutOfBuyLimit = 4405, // 0x00001135
      LimitedShopOutOfBuyLimit = 4405, // 0x00001135
      ProductIllegalDate = 4500, // 0x00001194
      ProductPurchaseMax = 4600, // 0x000011F8
      ProductCantPurchase = 4601, // 0x000011F9
      HikkoshiNoToken = 4700, // 0x0000125C
      RoomFailedMakeRoom = 4800, // 0x000012C0
      RoomIllegalComment = 4801, // 0x000012C1
      RoomNoRoom = 4900, // 0x00001324
      NoWatching = 4901, // 0x00001325
      RepelledBlockList = 4902, // 0x00001326
      NoDevice = 5000, // 0x00001388
      Authorize = 5001, // 0x00001389
      GauthNoSid = 5002, // 0x0000138A
      ReturnForceTitle = 5003, // 0x0000138B
      MigrateIllCode = 5100, // 0x000013EC
      MigrateSameDev = 5101, // 0x000013ED
      MigrateLockCode = 5102, // 0x000013EE
      ColoResetCostShort = 5500, // 0x0000157C
      RaidTicketShort = 5600, // 0x000015E0
      UnitAddExist = 5700, // 0x00001644
      UnitAddCostShort = 5701, // 0x00001645
      UnitAddCantUnlock = 5702, // 0x00001646
      RoomPlayerLvShort = 5800, // 0x000016A8
      ApprNoFriend = 5900, // 0x0000170C
      ApprNoRequest = 5901, // 0x0000170D
      ApprRequestMax = 5902, // 0x0000170E
      ApprFriendIsFull = 5903, // 0x0000170F
      FindNoFriend = 6000, // 0x00001770
      FindIsMine = 6001, // 0x00001771
      CountLimitForPlayer = 8005, // 0x00001F45
      QR_OutOfPeriod = 8008, // 0x00001F48
      QR_InvalidQRSerial = 8009, // 0x00001F49
      QR_CanNotReward = 8010, // 0x00001F4A
      QR_LockSerialCampaign = 8011, // 0x00001F4B
      QR_AlreadyRewardSkin = 8012, // 0x00001F4C
      ChargeError = 8100, // 0x00001FA4
      ChargeAge000 = 8101, // 0x00001FA5
      ChargeVipRemains = 8102, // 0x00001FA6
      FirstChargeInvalid = 8103, // 0x00001FA7
      FirstChargeNoLog = 8104, // 0x00001FA8
      FirstChargeReceipt = 8105, // 0x00001FA9
      FirstChargePast = 8106, // 0x00001FAA
      TowerLocked = 8201, // 0x00002009
      ConditionsErr = 8202, // 0x0000200A
      NotRecovery_permit = 8203, // 0x0000200B
      NotExist_tower = 8211, // 0x00002013
      NotExist_reward = 8212, // 0x00002014
      NotExist_floor = 8213, // 0x00002015
      NoMatch_party = 8221, // 0x0000201D
      NoMatch_mid = 8222, // 0x0000201E
      IncorrectCoin = 8231, // 0x00002027
      IncorrectBtlparam = 8232, // 0x00002028
      AlreadyClear = 8241, // 0x00002031
      AlreadyBtlend = 8242, // 0x00002032
      FaildRegistration = 8243, // 0x00002033
      FaildReset = 8244, // 0x00002034
      NoChannelAction = 8500, // 0x00002134
      NoUserAction = 8501, // 0x00002135
      SendChatInterval = 8502, // 0x00002136
      CanNotAddBlackList = 8503, // 0x00002137
      NotGpsMail = 8600, // 0x00002198
      ReceivedGpsMail = 8601, // 0x00002199
      AcheiveMigrateIllcode = 8800, // 0x00002260
      AcheiveMigrateNoCoop = 8801, // 0x00002261
      AcheiveMigrateLock = 8802, // 0x00002262
      AcheiveMigrateAuthorize = 8803, // 0x00002263
      ArtifactBoxLimit = 9000, // 0x00002328
      ArtifactPieceShort = 9001, // 0x00002329
      ArtifactMatShort = 9002, // 0x0000232A
      ArtifactFavorite = 9003, // 0x0000232B
      SkinNoSkin = 9010, // 0x00002332
      SkinNoJob = 9011, // 0x00002333
      Gift_ConceptCardBoxLimit = 9020, // 0x0000233C
      VS_NotSelfBattle = 10000, // 0x00002710
      VS_NotPlayer = 10001, // 0x00002711
      VS_NotQuestInfo = 10002, // 0x00002712
      VS_NotLINERoomInfo = 10003, // 0x00002713
      VS_FailRoomID = 10004, // 0x00002714
      VS_BattleEnd = 10005, // 0x00002715
      VS_NotQuestData = 10006, // 0x00002716
      VS_NotPhotonAppID = 10007, // 0x00002717
      VS_Version = 10008, // 0x00002718
      VS_IllComment = 10009, // 0x00002719
      VS_LvShort = 10010, // 0x0000271A
      VS_BattleNotEnd = 10011, // 0x0000271B
      VS_NoRoom = 10012, // 0x0000271C
      VS_ComBattleEnd = 10013, // 0x0000271D
      VS_FaildSeasonGift = 10014, // 0x0000271E
      VS_TowerNotPlay = 10015, // 0x0000271F
      VS_NotContinuousEnemy = 10016, // 0x00002720
      VS_RowerNotMatching = 10017, // 0x00002721
      VS_EnableTimeOutOfPriod = 10018, // 0x00002722
      QuestBookmark_RequestMax = 10100, // 0x00002774
      QuestBookmark_AlreadyLimited = 10101, // 0x00002775
      DmmSessionExpired = 11000, // 0x00002AF8
      MT_NotClearFloor = 12001, // 0x00002EE1
      MT_AlreadyFinish = 12002, // 0x00002EE2
      MT_NoRoom = 12003, // 0x00002EE3
      RankingQuest_NotNewScore = 13001, // 0x000032C9
      RankingQuest_AlreadyEntry = 13002, // 0x000032CA
      RankingQuest_OutOfPeriod = 13003, // 0x000032CB
      Gallery_MigrationInProgress = 14001, // 0x000036B1
    }

    public enum RequestResults
    {
      Success,
      Failure,
      Retry,
      Back,
      Timeout,
      Maintenance,
      VersionMismatch,
      InvalidSession,
      IllegalParam,
    }

    public enum EConnectMode
    {
      Online,
      Offline,
    }

    public delegate void ResponseCallback(WWWResult result);
  }
}
