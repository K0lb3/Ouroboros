// Decompiled with JetBrains decompiler
// Type: SRPG.Network
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/Manager/Network")]
  public class Network : MonoSingleton<Network>
  {
    public static readonly float WEBAPI_TIMEOUT_SEC = 60f;
    public static readonly float DOWNLOAD_TIMEOUT_SEC = 10f;
    public static Network.EConnectMode Mode = Network.EConnectMode.Offline;
    public static readonly string OfficialUrl = "https://www.facebook.com/thealchemistcode/";
    public static readonly string DefaultHost = "https://app.alcww.gumi.sg/";
    public static readonly string DefaultDLHost = "https://app.alcww.gumi.sg/";
    public static readonly string DefaultSiteHost = "https://alchemistcode.com";
    public static readonly string DefaultNewsHost = "https://alchemistcode.com";
    private static AndroidJavaClass mSystemClock = new AndroidJavaClass("android.os.SystemClock");
    private string mHost = Network.DefaultHost;
    private string mDLHost = Network.DefaultDLHost;
    private string mSiteHost = Network.DefaultSiteHost;
    private string mNewsHost = Network.DefaultNewsHost;
    private string mSessionID = string.Empty;
    private string mVersion = string.Empty;
    private string mAssetVersion = string.Empty;
    private int mTicket = 1;
    private List<WebAPI> mRequests = new List<WebAPI>(4);
    private bool mIndicator = true;
    private string mDefaultHostConfigured = Network.DefaultHost;
    public static Network.RequestResults RequestResult;
    private static long ServerTime;
    private static long LastRealTime;
    private bool mBusy;
    private bool mRetry;
    private bool mError;
    private string mErrMsg;
    private Network.EErrCode mErrCode;
    private bool mImmediateMode;
    private WebAPI mCurrentRequest;

    public static bool IsImmediateMode
    {
      get
      {
        return MonoSingleton<Network>.Instance.mImmediateMode;
      }
    }

    public static string Host
    {
      get
      {
        return MonoSingleton<Network>.Instance.mHost;
      }
    }

    public static void SetHost(string host)
    {
      MonoSingleton<Network>.Instance.mHost = host;
    }

    public static void SetDefaultHostConfigured(string host)
    {
      MonoSingleton<Network>.Instance.mDefaultHostConfigured = host;
    }

    public static void ResetHost()
    {
      Network instance = MonoSingleton<Network>.Instance;
      instance.mHost = instance.mDefaultHostConfigured;
      instance.mDLHost = Network.DefaultDLHost;
      instance.mSiteHost = Network.DefaultSiteHost;
      instance.mNewsHost = Network.DefaultNewsHost;
    }

    public static string DLHost
    {
      get
      {
        return MonoSingleton<Network>.Instance.mDLHost;
      }
    }

    public static void SetDLHost(string host)
    {
      MonoSingleton<Network>.Instance.mDLHost = host;
    }

    public static string SiteHost
    {
      get
      {
        return MonoSingleton<Network>.Instance.mSiteHost;
      }
    }

    public static void SetSiteHost(string host)
    {
      MonoSingleton<Network>.Instance.mSiteHost = host;
    }

    public static string NewsHost
    {
      get
      {
        return MonoSingleton<Network>.Instance.mNewsHost;
      }
    }

    public static void SetNewsHost(string host)
    {
      MonoSingleton<Network>.Instance.mNewsHost = host;
    }

    public static string Version
    {
      get
      {
        return MonoSingleton<Network>.Instance.mVersion;
      }
      set
      {
        MonoSingleton<Network>.Instance.mVersion = value;
      }
    }

    public static string AssetVersion
    {
      get
      {
        return MonoSingleton<Network>.Instance.mAssetVersion;
      }
      set
      {
        MonoSingleton<Network>.Instance.mAssetVersion = value;
      }
    }

    public static string SessionID
    {
      get
      {
        return MonoSingleton<Network>.Instance.mSessionID;
      }
      set
      {
        MonoSingleton<Network>.Instance.mSessionID = value;
      }
    }

    public static int TicketID
    {
      get
      {
        return MonoSingleton<Network>.Instance.mTicket;
      }
      private set
      {
        MonoSingleton<Network>.Instance.mTicket = value;
      }
    }

    public static bool IsBusy
    {
      get
      {
        return MonoSingleton<Network>.Instance.mBusy;
      }
      private set
      {
        MonoSingleton<Network>.Instance.mBusy = value;
      }
    }

    public static bool IsRetry
    {
      get
      {
        return MonoSingleton<Network>.Instance.mRetry;
      }
      set
      {
        MonoSingleton<Network>.Instance.mRetry = value;
      }
    }

    public static bool IsError
    {
      get
      {
        return MonoSingleton<Network>.Instance.mError;
      }
      private set
      {
        MonoSingleton<Network>.Instance.mError = value;
      }
    }

    public static string ErrMsg
    {
      get
      {
        return MonoSingleton<Network>.Instance.mErrMsg;
      }
      set
      {
        MonoSingleton<Network>.Instance.mErrMsg = value;
      }
    }

    public static Network.EErrCode ErrCode
    {
      get
      {
        return MonoSingleton<Network>.Instance.mErrCode;
      }
      set
      {
        MonoSingleton<Network>.Instance.mErrCode = value;
      }
    }

    public static bool IsConnecting
    {
      get
      {
        if (!Network.IsBusy)
          return MonoSingleton<Network>.Instance.mRequests.Count > 0;
        return true;
      }
    }

    public static bool IsIndicator
    {
      get
      {
        return MonoSingleton<Network>.Instance.mIndicator;
      }
      set
      {
        MonoSingleton<Network>.Instance.mIndicator = value;
      }
    }

    protected override void Initialize()
    {
      Network.Reset();
      Object.DontDestroyOnLoad((Object) this);
    }

    protected override void Release()
    {
    }

    public static void Reset()
    {
      MonoSingleton<Network>.Instance.mTicket = 1;
      MonoSingleton<Network>.Instance.mRequests.Clear();
    }

    public static void RequestAPI(WebAPI api, bool highPriority = false)
    {
      DebugUtility.Log("Request WebAPI: " + Network.Host + api.name);
      if (highPriority)
        MonoSingleton<Network>.Instance.mRequests.Insert(0, api);
      else
        MonoSingleton<Network>.Instance.mRequests.Add(api);
      if (MonoSingleton<Network>.Instance.mRequests.Count != 1)
        return;
      CriticalSection.Enter(CriticalSections.Network);
    }

    public static Network.RequestResults RequestAPIImmediate(WebAPI api, bool autoRetry)
    {
      MonoSingleton<Network>.Instance.mImmediateMode = true;
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(Network.Host + api.name);
      httpWebRequest.ContentType = "application/json; charset=utf-8";
      httpWebRequest.Timeout = (int) ((double) Network.WEBAPI_TIMEOUT_SEC * 1000.0);
      httpWebRequest.Headers.Set("X-GUMI-TRANSACTION", api.GumiTransactionId);
      if (MonoSingleton<DebugManager>.Instance.IsWebViewEnable())
      {
        httpWebRequest.UserAgent = gu3.Device.System.GetUserAgent();
      }
      else
      {
        httpWebRequest.UserAgent = "Mozilla/5.0 (Linux; U; Android 4.3; ja-jp; Nexus 7 Build/JSS15Q) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Safari/534.30";
        DebugUtility.Log("Skip User-Agent");
      }
      if (!string.IsNullOrEmpty(Network.SessionID))
        httpWebRequest.Headers.Add("Authorization", "gumi " + Network.SessionID);
      if (!string.IsNullOrEmpty(Network.Version))
        httpWebRequest.Headers.Add("x-app-ver", Network.Version);
      if (!string.IsNullOrEmpty(Network.AssetVersion))
        httpWebRequest.Headers.Add("x-asset-ver", Network.AssetVersion);
      HttpWebResponse httpWebResponse = (HttpWebResponse) null;
      string str = (string) null;
      try
      {
        if (string.IsNullOrEmpty(api.body))
        {
          httpWebRequest.Method = "GET";
        }
        else
        {
          httpWebRequest.Method = "POST";
          byte[] bytes = Encoding.UTF8.GetBytes(api.body);
          httpWebRequest.ContentLength = (long) bytes.Length;
          BinaryWriter binaryWriter = new BinaryWriter(httpWebRequest.GetRequestStream());
          binaryWriter.Write(bytes);
          binaryWriter.Close();
        }
        httpWebResponse = (HttpWebResponse) httpWebRequest.GetResponse();
        Network.ErrCode = Network.EErrCode.Unknown;
        str = new StreamReader(httpWebResponse.GetResponseStream(), true).ReadToEnd();
        Network.ErrCode = (Network.EErrCode) Network.FindStat(str);
        Network.ErrMsg = Network.FindMessage(str);
      }
      catch (Exception ex)
      {
        Network.ErrCode = Network.EErrCode.Failed;
        Network.ErrMsg = LocalizedText.Get("embed.NETWORKERR");
        Debug.LogException(ex);
      }
      finally
      {
        Network.IsError = Network.ErrCode != Network.EErrCode.Success;
        if (httpWebResponse != null)
          httpWebResponse.Close();
      }
      Network.RequestResult = Network.RequestResults.Success;
      MonoSingleton<Network>.Instance.mCurrentRequest = (WebAPI) null;
      api.callback(new WWWResult(str));
      if (MonoSingleton<Network>.Instance.mImmediateMode)
      {
        MonoSingleton<Network>.Instance.mImmediateMode = false;
        if (autoRetry)
        {
          --Network.TicketID;
          Network.ResetError();
          Network.RequestAPI(api, true);
        }
      }
      return Network.RequestResult;
    }

    public static void RemoveAPI()
    {
      if (MonoSingleton<Network>.Instance.mImmediateMode)
        MonoSingleton<Network>.Instance.mImmediateMode = false;
      else if (MonoSingleton<Network>.Instance.mRequests.Count <= 0)
      {
        DebugUtility.LogWarning("Instance.mRequests.Count <= 0");
      }
      else
      {
        MonoSingleton<Network>.Instance.mRequests.Remove(MonoSingleton<Network>.Instance.mCurrentRequest);
        if (MonoSingleton<Network>.Instance.mRequests.Count != 0)
          return;
        CriticalSection.Leave(CriticalSections.Network);
      }
    }

    public static void ResetError()
    {
      MonoSingleton<Network>.Instance.mError = false;
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

    private static string FindLocalizedMessage(string response)
    {
      Regex regex1 = new Regex("\"stat_msg\":\"(?<stat_msg>.+?)\"[,}]", RegexOptions.None);
      string configLanguage = GameUtility.Config_Language;
      Regex regex2;
      if (configLanguage != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (Network.\u003C\u003Ef__switch\u0024map11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Network.\u003C\u003Ef__switch\u0024map11 = new Dictionary<string, int>(3)
          {
            {
              "french",
              0
            },
            {
              "german",
              1
            },
            {
              "spanish",
              2
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (Network.\u003C\u003Ef__switch\u0024map11.TryGetValue(configLanguage, out num))
        {
          switch (num)
          {
            case 0:
              regex2 = new Regex("\"fr\":\"(?<stat_msg>.+?)\"[,}]", RegexOptions.None);
              goto label_9;
            case 1:
              regex2 = new Regex("\"de\":\"(?<stat_msg>.+?)\"[,}]", RegexOptions.None);
              goto label_9;
            case 2:
              regex2 = new Regex("\"es\":\"(?<stat_msg>.+?)\"[,}]", RegexOptions.None);
              goto label_9;
          }
        }
      }
      regex2 = new Regex("\"en\":\"(?<stat_msg>.+?)\"[,}]", RegexOptions.None);
label_9:
      if (!regex2.Match(response).Success)
        return string.Empty;
      return regex2.Match(response).Result("${stat_msg}");
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
      if (Network.Mode == Network.EConnectMode.Offline)
        return TimeManager.Now();
      long systemUptime = Network.GetSystemUptime();
      return Network.ServerTime + (systemUptime - Network.LastRealTime);
    }

    public static long LastConnectionTime
    {
      get
      {
        return Network.ServerTime;
      }
    }

    private void Update()
    {
      if (Network.IsBusy || Network.IsError || this.mRequests.Count <= 0)
        return;
      WebAPI mRequest = this.mRequests[0];
      if (mRequest == null)
        return;
      this.StartCoroutine(Network.Connecting(mRequest));
    }

    [DebuggerHidden]
    private static IEnumerator Connecting(WebAPI api)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Network.\u003CConnecting\u003Ec__IteratorF6() { api = api, \u003C\u0024\u003Eapi = api };
    }

    private static long GetSystemUptime()
    {
      if (GameUtility.IsDebugBuild)
        return (long) Time.get_realtimeSinceStartup();
      return ((AndroidJavaObject) Network.mSystemClock).CallStatic<long>("elapsedRealtime", new object[0]) / 1000L;
    }

    public enum EErrCode
    {
      TimeOut = -2,
      Failed = -1,
      Success = 0,
      Unknown = 1,
      Version = 2,
      AssetVersion = 3,
      NoSID = 100, // 0x00000064
      Maintenance = 200, // 0x000000C8
      ChatMaintenance = 201, // 0x000000C9
      MultiMaintenance = 202, // 0x000000CA
      VsMaintenance = 203, // 0x000000CB
      IllegalParam = 300, // 0x0000012C
      API = 400, // 0x00000190
      NoFile = 1000, // 0x000003E8
      NoVersion = 1100, // 0x0000044C
      SessionFailure = 1200, // 0x000004B0
      CreateStopped = 1300, // 0x00000514
      IllegalName = 1400, // 0x00000578
      DuplicateName = 1401, // 0x00000579
      SameName = 1402, // 0x0000057A
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
      NoGacha = 4000, // 0x00000FA0
      GachaCostShort = 4001, // 0x00000FA1
      GachaItemMax = 4002, // 0x00000FA2
      GachaNotFree = 4003, // 0x00000FA3
      GachaPaidLimitOver = 4004, // 0x00000FA4
      GachaPlyLvOver = 4005, // 0x00000FA5
      GachaPlyNewOver = 4006, // 0x00000FA6
      GachaLimitSoldOut = 4007, // 0x00000FA7
      GachaLimitCntOver = 4008, // 0x00000FA8
      TrophyRewarded = 4100, // 0x00001004
      TrophyOutOfDate = 4101, // 0x00001005
      TrophyRollBack = 4102, // 0x00001006
      ShopRefreshCostShort = 4200, // 0x00001068
      ShopRefreshLvSort = 4201, // 0x00001069
      ShopSoldOut = 4300, // 0x000010CC
      ShopBuyCostShort = 4301, // 0x000010CD
      ShopBuyLvShort = 4302, // 0x000010CE
      ShopBuyNotFound = 4303, // 0x000010CF
      ShopBuyItemNotFound = 4304, // 0x000010D0
      GoldBuyCostShort = 4400, // 0x00001130
      GoldBuyLimit = 4401, // 0x00001131
      EventShopOutOfPeriod = 4403, // 0x00001133
      LimitedShopOutOfPeriod = 4403, // 0x00001133
      EventShopOutOfBuyLimit = 4405, // 0x00001135
      LimitedShopOutOfBuyLimit = 4405, // 0x00001135
      ProductIllegalDate = 4500, // 0x00001194
      ProductPurchaseMax = 4600, // 0x000011F8
      ProductCantPurchase = 4601, // 0x000011F9
      HikkoshiNoToken = 4700, // 0x0000125C
      RoomFailedMakeRoom = 4800, // 0x000012C0
      RoomIllegalComment = 4801, // 0x000012C1
      RoomNoRoom = 4900, // 0x00001324
      NoDevice = 5000, // 0x00001388
      Authorize = 5001, // 0x00001389
      GauthNoSid = 5002, // 0x0000138A
      ReturnForceTitle = 5003, // 0x0000138B
      MissingRelatedID = 5006, // 0x0000138E
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
      StringTooShort = 6002, // 0x00001772
      CountLimitForPlayer = 8005, // 0x00001F45
      ChargeError = 8100, // 0x00001FA4
      ChargeAge000 = 8101, // 0x00001FA5
      ChargeVipRemains = 8102, // 0x00001FA6
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

    public delegate void DownloadCallback(WWW result);
  }
}
