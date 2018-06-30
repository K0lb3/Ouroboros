namespace SRPG
{
    using GR;
    using Gsc;
    using Gsc.App;
    using Gsc.App.NetworkHelper;
    using Gsc.Network;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using UnityEngine;
    using UnityEngine.Experimental.Networking;

    [AddComponentMenu("Scripts/SRPG/Manager/Network")]
    public class Network : MonoSingleton<Network>
    {
        public static RequestResults RequestResult;
        public static readonly float WEBAPI_TIMEOUT_SEC;
        public static EConnectMode Mode;
        public static readonly string OfficialUrl;
        public static readonly string DefaultHost;
        public static readonly string DefaultDLHost;
        public static readonly string DefaultSiteHost;
        public static readonly string DefaultNewsHost;
        private static long ServerTime;
        private static long LastRealTime;
        private string mSessionID;
        private string mVersion;
        private string mAssets;
        private string mAssetsEx;
        private int mTicket;
        private bool mBusy;
        private bool mRetry;
        private bool mError;
        private string mErrMsg;
        private EErrCode mErrCode;
        private bool mImmediateMode;
        private WebAPI mCurrentRequest;
        private List<WebAPI> mRequests;
        private bool mIndicator;
        private UnityWebRequest mWebReq;
        private bool mAbort;
        private bool mNoVersion;
        private bool mForceBusy;
        private string mDefaultHostConfigured;

        static Network()
        {
            WEBAPI_TIMEOUT_SEC = 60f;
            Mode = 1;
            OfficialUrl = "https://al.fg-games.co.jp/";
            DefaultHost = "https://alchemist.gu3.jp/";
            DefaultDLHost = "https://alchemist.gu3.jp/";
            DefaultSiteHost = "https://st-al.fg-games.co.jp/";
            DefaultNewsHost = "https://st-al.fg-games.co.jp/";
            return;
        }

        public Network()
        {
            this.mSessionID = string.Empty;
            this.mVersion = string.Empty;
            this.mAssets = string.Empty;
            this.mAssetsEx = string.Empty;
            this.mTicket = 1;
            this.mRequests = new List<WebAPI>(4);
            this.mIndicator = 1;
            this.mDefaultHostConfigured = DefaultHost;
            base..ctor();
            return;
        }

        public static void Abort()
        {
            if (uniWebRequest == null)
            {
                goto Label_001A;
            }
            uniWebRequest.Abort();
            Aborted = 1;
        Label_001A:
            return;
        }

        [DebuggerHidden]
        private static IEnumerator Connecting(WebAPI api)
        {
            <Connecting>c__Iterator183 iterator;
            iterator = new <Connecting>c__Iterator183();
            iterator.api = api;
            iterator.<$>api = api;
            return iterator;
        }

        private void ConnectingGsc(WebAPI api)
        {
            TicketID += 1;
            IsError = 0;
            ErrCode = 0;
            MonoSingleton<Network>.Instance.mCurrentRequest = api;
            GsccBridge.Send(api, 0);
            return;
        }

        public static void ConnectingResponse(WebResponse response, ResponseCallback callback)
        {
            ErrCode = response.ErrorCode;
            ErrMsg = response.ErrorMessage;
            IsError = (ErrCode == 0) == 0;
            if (FlowNode_Network.HasCommonError(response.Result) == null)
            {
                goto Label_0037;
            }
            return;
        Label_0037:
            if (callback == null)
            {
                goto Label_0069;
            }
            if (response.ServerTime == null)
            {
                goto Label_0053;
            }
            ServerTime = response.ServerTime;
        Label_0053:
            LastRealTime = GetSystemUptime();
            callback(response.Result);
        Label_0069:
            return;
        }

        private static string FindMessage(string response)
        {
            Regex regex;
            Match match;
            regex = new Regex("\"stat_msg\":\"(?<stat_msg>.+?)\"[,}]", 0);
            if (regex.Match(response).Success != null)
            {
                goto Label_0025;
            }
            return string.Empty;
        Label_0025:
            return regex.Match(response).Result("${stat_msg}");
        }

        private static int FindStat(string response)
        {
            Regex regex;
            Match match;
            regex = new Regex("\"stat\":(?<stat>\\d+)", 0);
            if (regex.Match(response).Success != null)
            {
                goto Label_0021;
            }
            return 0;
        Label_0021:
            return Convert.ToInt32(regex.Match(response).Result("${stat}"));
        }

        private static long FindTime(string response)
        {
            Regex regex;
            Match match;
            regex = new Regex("\"time\":(?<time>\\d+)", 0);
            if (regex.Match(response).Success != null)
            {
                goto Label_0022;
            }
            return 0L;
        Label_0022:
            return Convert.ToInt64(regex.Match(response).Result("${time}"));
        }

        public static string GetDefaultHostConfigured()
        {
            return MonoSingleton<Network>.Instance.mDefaultHostConfigured;
        }

        public static long GetServerTime()
        {
            long num;
            if (Mode != 1)
            {
                goto Label_0011;
            }
            return TimeManager.Now();
        Label_0011:
            num = GetSystemUptime();
            return (ServerTime + (num - LastRealTime));
        }

        private static long GetSystemUptime()
        {
            return (long) Time.get_realtimeSinceStartup();
        }

        protected override void Initialize()
        {
            Reset();
            Object.DontDestroyOnLoad(this);
            return;
        }

        protected override void Release()
        {
        }

        public static void RemoveAPI()
        {
            GsccBridge.Reset();
            if (MonoSingleton<Network>.Instance.mImmediateMode == null)
            {
                goto Label_0020;
            }
            MonoSingleton<Network>.Instance.mImmediateMode = 0;
            return;
        Label_0020:
            if (MonoSingleton<Network>.Instance.mRequests.Count > 0)
            {
                goto Label_0040;
            }
            DebugUtility.LogWarning("Instance.mRequestsGsc.Count <= 0");
            return;
        Label_0040:
            MonoSingleton<Network>.Instance.mRequests.Remove(MonoSingleton<Network>.Instance.mCurrentRequest);
            if (MonoSingleton<Network>.Instance.mRequests.Count != null)
            {
                goto Label_0074;
            }
            CriticalSection.Leave(2);
        Label_0074:
            return;
        }

        public static void RequestAPI(WebAPI api, bool highPriority)
        {
            DebugUtility.Log("Request WebAPI: " + api.name);
            if (highPriority == null)
            {
                goto Label_0031;
            }
            MonoSingleton<Network>.Instance.mRequests.Insert(0, api);
            goto Label_0041;
        Label_0031:
            MonoSingleton<Network>.Instance.mRequests.Add(api);
        Label_0041:
            if (MonoSingleton<Network>.Instance.mRequests.Count != 1)
            {
                goto Label_005C;
            }
            CriticalSection.Enter(2);
        Label_005C:
            return;
        }

        public static void RequestAPIImmediate(WebAPI api, bool autoRetry)
        {
            MonoSingleton<Network>.Instance.mImmediateMode = 1;
            GsccBridge.SendImmediate(api);
            if (MonoSingleton<Network>.Instance.mImmediateMode == null)
            {
                goto Label_0049;
            }
            MonoSingleton<Network>.Instance.mImmediateMode = 0;
            if (autoRetry == null)
            {
                goto Label_0049;
            }
            TicketID -= 1;
            ResetError();
            RequestAPI(api, 1);
        Label_0049:
            return;
        }

        public static void Reset()
        {
            MonoSingleton<Network>.Instance.mTicket = 1;
            MonoSingleton<Network>.Instance.mRequests.Clear();
            GsccBridge.Reset();
            return;
        }

        public static void ResetError()
        {
            MonoSingleton<Network>.Instance.mError = 0;
            return;
        }

        public static void SetDefaultHostConfigured(string host)
        {
            MonoSingleton<Network>.Instance.mDefaultHostConfigured = host;
            return;
        }

        public static void SetRetry()
        {
            GsccBridge.Retry();
            return;
        }

        public static void SetServerInvalidDeviceError()
        {
            ErrCode = 0x1389;
            ErrMsg = LocalizedText.Get("sys.AUTHORIZEERR");
            IsError = 1;
            return;
        }

        public static void SetServerMetaDataAsError()
        {
            ErrCode = -1;
            ErrMsg = LocalizedText.Get("embed.NETWORKERR");
            IsError = 1;
            return;
        }

        public static void SetServerMetaDataAsError(EErrCode code, string msg)
        {
            ErrCode = code;
            ErrMsg = msg;
            IsError = 1;
            return;
        }

        public static void SetServerSessionExpired()
        {
            ErrCode = 0x2af8;
            ErrMsg = LocalizedText.Get("embed.DMM_EXPIRED");
            IsError = 1;
            return;
        }

        public static void SetServerTime(long time)
        {
            if (time == null)
            {
                goto Label_000C;
            }
            ServerTime = time;
        Label_000C:
            LastRealTime = GetSystemUptime();
            return;
        }

        private void Update()
        {
            WebAPI bapi;
            if (IsBusy == null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            if (IsError == null)
            {
                goto Label_0016;
            }
            return;
        Label_0016:
            if (IsForceBusy == null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            if (this.mRequests.Count <= 0)
            {
                goto Label_006A;
            }
            bapi = this.mRequests[0];
            if (bapi == null)
            {
                goto Label_006A;
            }
            if (bapi.reqtype != null)
            {
                goto Label_005C;
            }
            this.ConnectingGsc(bapi);
            goto Label_0069;
        Label_005C:
            base.StartCoroutine(Connecting(bapi));
        Label_0069:
            return;
        Label_006A:
            return;
        }

        public static bool IsImmediateMode
        {
            get
            {
                return MonoSingleton<Network>.Instance.mImmediateMode;
            }
        }

        public static Environment GetEnvironment
        {
            get
            {
                Configuration configuration;
                return &SDK.Configuration.GetEnv<Environment>();
            }
        }

        public static string Host
        {
            get
            {
                Environment environment;
                return &GetEnvironment.ServerUrl;
            }
        }

        public static string DLHost
        {
            get
            {
                Environment environment;
                return &GetEnvironment.DLHost;
            }
        }

        public static string SiteHost
        {
            get
            {
                Environment environment;
                return &GetEnvironment.SiteHost;
            }
        }

        public static string NewsHost
        {
            get
            {
                Environment environment;
                return &GetEnvironment.NewsHost;
            }
        }

        public static string Digest
        {
            get
            {
                Environment environment;
                return &GetEnvironment.Digest;
            }
        }

        public static string Pub
        {
            get
            {
                Environment environment;
                return &GetEnvironment.Pub;
            }
        }

        public static string PubU
        {
            get
            {
                Environment environment;
                return &GetEnvironment.PubU;
            }
        }

        public static string AssetVersion
        {
            get
            {
                return MonoSingleton<Network>.Instance.mAssets;
            }
            set
            {
                MonoSingleton<Network>.Instance.mAssets = value;
                return;
            }
        }

        public static string AssetVersionEx
        {
            get
            {
                return MonoSingleton<Network>.Instance.mAssetsEx;
            }
            set
            {
                MonoSingleton<Network>.Instance.mAssetsEx = value;
                return;
            }
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
                return;
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
                return;
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
                return;
            }
        }

        public static bool IsBusy
        {
            get
            {
                return ((MonoSingleton<Network>.Instance.mBusy != null) ? 1 : ((WebQueue.defaultQueue == null) ? 0 : WebQueue.defaultQueue.isRunning));
            }
            private set
            {
                MonoSingleton<Network>.Instance.mBusy = value;
                return;
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
                return;
            }
        }

        public static bool IsError
        {
            get
            {
                return ((MonoSingleton<Network>.Instance.mError != null) ? 1 : GsccBridge.HasUnhandledTasks);
            }
            private set
            {
                MonoSingleton<Network>.Instance.mError = value;
                return;
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
                return;
            }
        }

        public static EErrCode ErrCode
        {
            get
            {
                return MonoSingleton<Network>.Instance.mErrCode;
            }
            set
            {
                MonoSingleton<Network>.Instance.mErrCode = value;
                return;
            }
        }

        public static bool IsConnecting
        {
            get
            {
                return ((IsBusy != null) ? 1 : (MonoSingleton<Network>.Instance.mRequests.Count > 0));
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
                return;
            }
        }

        public static UnityWebRequest uniWebRequest
        {
            get
            {
                return MonoSingleton<Network>.Instance.mWebReq;
            }
            set
            {
                MonoSingleton<Network>.Instance.mWebReq = value;
                return;
            }
        }

        public static bool IsStreamConnecting
        {
            get
            {
                return ((uniWebRequest == null) == 0);
            }
        }

        public static bool Aborted
        {
            get
            {
                return MonoSingleton<Network>.Instance.mAbort;
            }
            set
            {
                MonoSingleton<Network>.Instance.mAbort = value;
                return;
            }
        }

        public static bool IsNoVersion
        {
            get
            {
                return MonoSingleton<Network>.Instance.mNoVersion;
            }
            set
            {
                MonoSingleton<Network>.Instance.mNoVersion = value;
                return;
            }
        }

        public static bool IsForceBusy
        {
            get
            {
                return MonoSingleton<Network>.Instance.mForceBusy;
            }
            set
            {
                MonoSingleton<Network>.Instance.mForceBusy = value;
                return;
            }
        }

        public static long LastConnectionTime
        {
            get
            {
                return ServerTime;
            }
        }

        [CompilerGenerated]
        private sealed class <Connecting>c__Iterator183 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal WebAPI api;
            internal string <apiname>__0;
            internal string <body>__1;
            internal Network.ResponseCallback <complete>__2;
            internal byte[] <req>__3;
            internal Dictionary<string, string> <header>__4;
            internal string <url>__5;
            internal UnityWebRequest <webReq>__6;
            internal Dictionary<string, string>.KeyCollection.Enumerator <$s_1262>__7;
            internal string <key>__8;
            internal int $PC;
            internal object $current;
            internal WebAPI <$>api;

            public <Connecting>c__Iterator183()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                uint num;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_003D;

                    case 1:
                        goto Label_0021;
                }
                goto Label_003D;
            Label_0021:
                try
                {
                    goto Label_003D;
                }
                finally
                {
                Label_0026:
                    if (this.<webReq>__6 == null)
                    {
                        goto Label_003C;
                    }
                    this.<webReq>__6.Dispose();
                Label_003C:;
                }
            Label_003D:
                return;
            }

            public unsafe bool MoveNext()
            {
                object[] objArray1;
                uint num;
                bool flag;
                bool flag2;
                num = this.$PC;
                this.$PC = -1;
                flag = 0;
                switch (num)
                {
                    case 0:
                        goto Label_0023;

                    case 1:
                        goto Label_0132;
                }
                goto Label_03F1;
            Label_0023:
                this.<apiname>__0 = this.api.name;
                this.<body>__1 = this.api.body;
                this.<complete>__2 = this.api.callback;
                Network.TicketID += 1;
                Network.IsBusy = 1;
                Network.IsError = 0;
                Network.ErrCode = 0;
                this.<req>__3 = null;
                if (string.IsNullOrEmpty(this.<body>__1) != null)
                {
                    goto Label_00A1;
                }
                this.<req>__3 = Encoding.UTF8.GetBytes(this.<body>__1);
            Label_00A1:
                this.<header>__4 = new Dictionary<string, string>();
                GsccBridge.SetBaseCustomHeaders(new Action<string, string>(this.<header>__4.Add), this.api.GumiTransactionId);
                Network.IsIndicator = 0;
                this.<url>__5 = Network.Host + this.<apiname>__0;
                if (Network.Host.EndsWith("/") != null)
                {
                    goto Label_0119;
                }
                this.<url>__5 = Network.Host + "/" + this.<apiname>__0;
            Label_0119:
                this.<webReq>__6 = new UnityWebRequest(this.<url>__5, "POST");
                num = -3;
            Label_0132:
                try
                {
                    switch ((num - 1))
                    {
                        case 0:
                            goto Label_021D;
                    }
                    Network.uniWebRequest = this.<webReq>__6;
                    this.<webReq>__6.set_uploadHandler(new UploadHandlerRaw(this.<req>__3));
                    this.<$s_1262>__7 = this.<header>__4.Keys.GetEnumerator();
                Label_0175:
                    try
                    {
                        goto Label_01C2;
                    Label_017A:
                        this.<key>__8 = &this.<$s_1262>__7.Current;
                        if ((this.<key>__8 != "User-Agent") == null)
                        {
                            goto Label_01C2;
                        }
                        this.<webReq>__6.SetRequestHeader(this.<key>__8, this.<header>__4[this.<key>__8]);
                    Label_01C2:
                        if (&this.<$s_1262>__7.MoveNext() != null)
                        {
                            goto Label_017A;
                        }
                        goto Label_01E8;
                    }
                    finally
                    {
                    Label_01D7:
                        ((Dictionary<string, string>.KeyCollection.Enumerator) this.<$s_1262>__7).Dispose();
                    }
                Label_01E8:
                    this.<webReq>__6.set_downloadHandler(this.api.dlHandler);
                    this.$current = this.<webReq>__6.Send();
                    this.$PC = 1;
                    flag = 1;
                    goto Label_03F3;
                Label_021D:
                    objArray1 = new object[] { "error:", this.<webReq>__6.get_error(), "/isDone:", (bool) this.<webReq>__6.get_isDone(), "/isError:", (bool) this.<webReq>__6.get_isError() };
                    DebugUtility.Log(string.Concat(objArray1));
                    if (this.<webReq>__6.get_isDone() == null)
                    {
                        goto Label_0301;
                    }
                    Network.ErrCode = 0;
                    if (Network.Aborted != null)
                    {
                        goto Label_0301;
                    }
                    Network.ErrMsg = this.<webReq>__6.get_error();
                    if (string.IsNullOrEmpty(Network.ErrMsg) != null)
                    {
                        goto Label_02D2;
                    }
                    Network.ErrCode = -1;
                    Network.ErrMsg = LocalizedText.Get("embed.NETWORKERR");
                    goto Label_0301;
                Label_02D2:
                    if (this.<webReq>__6.get_downloadHandler() == null)
                    {
                        goto Label_0301;
                    }
                    Network.ErrCode = Network.FindStat(((DownloadLogger) this.<webReq>__6.get_downloadHandler()).Response);
                Label_0301:
                    if (Network.ErrCode == null)
                    {
                        goto Label_0311;
                    }
                    Network.IsError = 1;
                Label_0311:
                    if (this.<complete>__2 == null)
                    {
                        goto Label_038F;
                    }
                    if (this.<webReq>__6.get_downloadHandler() == null)
                    {
                        goto Label_0370;
                    }
                    Network.ServerTime = Network.FindTime(((DownloadLogger) this.<webReq>__6.get_downloadHandler()).Response);
                    this.<complete>__2(new WWWResult(((DownloadLogger) this.<webReq>__6.get_downloadHandler()).Response));
                Label_0370:
                    Network.LastRealTime = Network.GetSystemUptime();
                    MonoSingleton<Network>.Instance.mCurrentRequest = this.api;
                    goto Label_03A4;
                Label_038F:
                    MonoSingleton<Network>.Instance.mCurrentRequest = this.api;
                    Network.RemoveAPI();
                Label_03A4:
                    goto Label_03C4;
                }
                finally
                {
                Label_03A9:
                    if (flag == null)
                    {
                        goto Label_03AD;
                    }
                Label_03AD:
                    if (this.<webReq>__6 == null)
                    {
                        goto Label_03C3;
                    }
                    this.<webReq>__6.Dispose();
                Label_03C3:;
                }
            Label_03C4:
                Network.Aborted = 0;
                Network.IsIndicator = 1;
                this.<header>__4 = null;
                this.<req>__3 = null;
                Network.IsBusy = 0;
                Network.uniWebRequest = null;
                this.$PC = -1;
            Label_03F1:
                return 0;
            Label_03F3:
                return 1;
                return flag2;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        public enum EConnectMode
        {
            Online,
            Offline
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
            NoSID = 100,
            Maintenance = 200,
            ChatMaintenance = 0xc9,
            MultiMaintenance = 0xca,
            VsMaintenance = 0xcb,
            BattleRecordMaintenance = 0xcc,
            MultiVersionMaintenance = 0xcd,
            MultiTowerMaintenance = 0xce,
            RankingQuestMaintenance = 0xcf,
            IllegalParam = 300,
            API = 400,
            NoFile = 0x3e8,
            NoVersion = 0x44c,
            SessionFailure = 0x4b0,
            CreateStopped = 0x514,
            IllegalName = 0x578,
            NoMail = 0x5dc,
            MailReadable = 0x5dd,
            ReqFriendRequestMax = 0x640,
            ReqFriendIsFull = 0x641,
            ReqNoFriend = 0x642,
            ReqFriendRegistered = 0x643,
            ReqFriendRequesting = 0x644,
            RmNoFriend = 0x6a4,
            RmFriendIsMe = 0x6a5,
            FindNoFriend = 0x1770,
            FindIsMine = 0x1771,
            ApprNoFriend = 0x170c,
            ApprNoRequest = 0x170d,
            ApprRequestMax = 0x170e,
            ApprFriendIsFull = 0x170f,
            NoUnitParty = 0x708,
            IllegalParty = 0x709,
            ExpMaterialShort = 0x76c,
            RareMaterialShort = 0x7d0,
            RarePlayerLvShort = 0x7d1,
            PlusMaterialShot = 0x834,
            PlusPlayerLvShort = 0x835,
            AbilityMaterialShort = 0x898,
            AbilityNotFound = 0x899,
            NoJobSetJob = 0x8fc,
            CantSelectJob = 0x8fd,
            NoUnitSetJob = 0x8fe,
            NoAbilitySetAbility = 0x960,
            NoJobSetAbility = 0x961,
            UnsetAbility = 0x962,
            NoJobSetEquip = 0x9c4,
            NoEquipItem = 0x9c5,
            Equipped = 0x9c6,
            NoJobEnforceEquip = 0xa28,
            NoEquipEnforce = 0xa29,
            ForceMax = 0xa2a,
            MaterialShort = 0xa2b,
            EnforcePlayerLvShort = 0xa2c,
            NoJobLvUpEquip = 0xa8c,
            EquipNotComp = 0xa8d,
            PlusShort = 0xa8e,
            UnitAddExist = 0x1644,
            UnitAddCostShort = 0x1645,
            UnitAddCantUnlock = 0x1646,
            ArtifactBoxLimit = 0x2328,
            ArtifactPieceShort = 0x2329,
            ArtifactMatShort = 0x232a,
            ArtifactFavorite = 0x232b,
            SkinNoSkin = 0x2332,
            SkinNoJob = 0x2333,
            NoItemSell = 0xaf0,
            ConvertAnotherItem = 0xaf1,
            StaminaCoinShort = 0xb54,
            AddStaminaLimit = 0xb55,
            AbilityCoinShort = 0xbb8,
            AbilityVipLvShort = 0xbb9,
            AbilityPlayerLvShort = 0xbba,
            GouseiNoTarget = 0xc80,
            GouseiMaterialShort = 0xc81,
            GouseiCostShort = 0xc82,
            UnSelectable = 0xce4,
            OutOfDateQuest = 0xce5,
            QuestNotEnd = 0xce6,
            ChallengeLimit = 0xce7,
            RecordLimitUpload = 0xced,
            QuestResume = 0xd48,
            QuestEnd = 0xdac,
            ContinueCostShort = 0xe10,
            CantContinue = 0xe11,
            ColoCantSelect = 0xed8,
            ColoIsBusy = 0xed9,
            ColoCostShort = 0xeda,
            ColoIntervalShort = 0xedb,
            ColoBattleNotEnd = 0xedc,
            ColoPlayerLvShort = 0xedd,
            ColoVipShort = 0xede,
            ColoRankLower = 0xedf,
            ColoNoBattle = 0xf3c,
            ColoRankModify = 0xf3d,
            ColoMyRankModify = 0xf3e,
            RaidTicketShort = 0x15e0,
            ColoResetCostShort = 0x157c,
            NoGacha = 0xfa0,
            GachaCostShort = 0xfa1,
            GachaItemMax = 0xfa2,
            GachaNotFree = 0xfa3,
            GachaPaidLimitOver = 0xfa4,
            GachaPlyLvOver = 0xfa5,
            GachaPlyNewOver = 0xfa6,
            GachaLimitSoldOut = 0xfa7,
            GachaLimitCntOver = 0xfa8,
            GachaOutofPeriod = 0xfaa,
            TrophyRewarded = 0x1004,
            TrophyOutOfDate = 0x1005,
            TrophyRollBack = 0x1006,
            BingoOutofDateReceive = 0x1010,
            ShopRefreshCostShort = 0x1068,
            ShopRefreshLvSort = 0x1069,
            ShopSoldOut = 0x10cc,
            ShopBuyCostShort = 0x10cd,
            ShopBuyLvShort = 0x10ce,
            ShopBuyNotFound = 0x10cf,
            ShopBuyItemNotFound = 0x10d0,
            ShopRefreshItemList = 0x10d1,
            ShopBuyOutofItemPeriod = 0x10d2,
            GoldBuyCostShort = 0x1130,
            GoldBuyLimit = 0x1131,
            ShopBuyOutofPeriod = 0x1133,
            ProductIllegalDate = 0x1194,
            ProductPurchaseMax = 0x11f8,
            ProductCantPurchase = 0x11f9,
            HikkoshiNoToken = 0x125c,
            NoBtlInfo = 0xe74,
            MultiPlayerLvShort = 0xe75,
            MultiBtlNotEnd = 0xe76,
            MultiVersionMismatch = 0xe78,
            RoomFailedMakeRoom = 0x12c0,
            RoomIllegalComment = 0x12c1,
            RoomNoRoom = 0x1324,
            NoWatching = 0x1325,
            RoomPlayerLvShort = 0x16a8,
            NoDevice = 0x1388,
            Authorize = 0x1389,
            GauthNoSid = 0x138a,
            ReturnForceTitle = 0x138b,
            MigrateIllCode = 0x13ec,
            MigrateSameDev = 0x13ed,
            MigrateLockCode = 0x13ee,
            CountLimitForPlayer = 0x1f45,
            ChargeError = 0x1fa4,
            ChargeAge000 = 0x1fa5,
            ChargeVipRemains = 0x1fa6,
            FirstChargeInvalid = 0x1fa7,
            FirstChargeNoLog = 0x1fa8,
            FirstChargeReceipt = 0x1fa9,
            FirstChargePast = 0x1faa,
            LimitedShopOutOfPeriod = 0x1133,
            LimitedShopOutOfBuyLimit = 0x1135,
            EventShopOutOfPeriod = 0x1133,
            EventShopOutOfBuyLimit = 0x1135,
            NoChannelAction = 0x2134,
            NoUserAction = 0x2135,
            SendChatInterval = 0x2136,
            CanNotAddBlackList = 0x2137,
            NotLocation = 0x191,
            NotGpsQuest = 0xcec,
            NotGpsMail = 0x2198,
            ReceivedGpsMail = 0x2199,
            AcheiveMigrateIllcode = 0x2260,
            AcheiveMigrateNoCoop = 0x2261,
            AcheiveMigrateLock = 0x2262,
            AcheiveMigrateAuthorize = 0x2263,
            TowerLocked = 0x2009,
            ConditionsErr = 0x200a,
            NotRecovery_permit = 0x200b,
            NotExist_tower = 0x2013,
            NotExist_reward = 0x2014,
            NotExist_floor = 0x2015,
            NoMatch_party = 0x201d,
            NoMatch_mid = 0x201e,
            IncorrectCoin = 0x2027,
            IncorrectBtlparam = 0x2028,
            AlreadyClear = 0x2031,
            AlreadyBtlend = 0x2032,
            FaildRegistration = 0x2033,
            FaildReset = 0x2034,
            VS_NotSelfBattle = 0x2710,
            VS_NotPlayer = 0x2711,
            VS_NotQuestInfo = 0x2712,
            VS_NotLINERoomInfo = 0x2713,
            VS_FailRoomID = 0x2714,
            VS_BattleEnd = 0x2715,
            VS_NotQuestData = 0x2716,
            VS_NotPhotonAppID = 0x2717,
            VS_Version = 0x2718,
            VS_IllComment = 0x2719,
            VS_LvShort = 0x271a,
            VS_BattleNotEnd = 0x271b,
            VS_NoRoom = 0x271c,
            VS_ComBattleEnd = 0x271d,
            VS_FaildSeasonGift = 0x271e,
            VS_TowerNotPlay = 0x271f,
            VS_NotContinuousEnemy = 0x2720,
            VS_RowerNotMatching = 0x2721,
            VS_EnableTimeOutOfPriod = 0x2722,
            DmmSessionExpired = 0x2af8,
            QR_OutOfPeriod = 0x1f48,
            QR_InvalidQRSerial = 0x1f49,
            QR_CanNotReward = 0x1f4a,
            QR_LockSerialCampaign = 0x1f4b,
            QR_AlreadyRewardSkin = 0x1f4c,
            QuestBookmark_RequestMax = 0x2774,
            QuestBookmark_AlreadyLimited = 0x2775,
            MT_NotClearFloor = 0x2ee1,
            MT_AlreadyFinish = 0x2ee2,
            MT_NoRoom = 0x2ee3,
            RankingQuest_NotNewScore = 0x32c9,
            RankingQuest_AlreadyEntry = 0x32ca,
            RankingQuest_OutOfPeriod = 0x32cb,
            Gallery_MigrationInProgress = 0x36b1,
            Gift_ConceptCardBoxLimit = 0x233c,
            RepelledBlockList = 0x1326
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
            IllegalParam
        }

        public delegate void ResponseCallback(WWWResult result);
    }
}

