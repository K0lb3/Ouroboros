namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class VersusAudienceManager
    {
        public static readonly float CONNECTTIME_MAX;
        public readonly int RETRY_MAX;
        private Dictionary<int, List<SceneBattle.MultiPlayRecvData>> mTurnLog;
        private AudienceStartParam mStartedParam;
        private JSON_MyPhotonRoomParam mRoomParam;
        private DownloadLogger mDownloadLogger;
        private int mReadCnt;
        private int mSkipReadMax;
        private int mRetryStartQuestCnt;
        private bool mSkipLog;
        private float mNoneConnectedTime;
        private string mNonAnalyzeLog;
        private CONNECT_STATE mState;

        static VersusAudienceManager()
        {
            CONNECTTIME_MAX = 30f;
            return;
        }

        public VersusAudienceManager()
        {
            this.RETRY_MAX = 30;
            this.mTurnLog = new Dictionary<int, List<SceneBattle.MultiPlayRecvData>>();
            this.mNonAnalyzeLog = string.Empty;
            base..ctor();
            return;
        }

        public void Add(string data)
        {
            this.Analyze(data);
            this.mState = 2;
            this.mNoneConnectedTime = 0f;
            return;
        }

        public void AddStartQuest()
        {
            this.mRetryStartQuestCnt += 1;
            return;
        }

        public unsafe void Analyze(string log)
        {
            string str;
            string str2;
            AudienceLog log2;
            byte[] buffer;
            SceneBattle.MultiPlayRecvData data;
            SceneBattle.MultiPlayRecvBinData data2;
            string str3;
            str = string.Empty;
            str2 = string.Empty;
            if (string.IsNullOrEmpty(this.mNonAnalyzeLog) != null)
            {
                goto Label_0035;
            }
            log = this.mNonAnalyzeLog + log;
            this.mNonAnalyzeLog = string.Empty;
        Label_0035:
            if (log.IndexOf("creatorName") == -1)
            {
                goto Label_004D;
            }
            str = log;
            goto Label_0147;
        Label_004D:
            if (log.IndexOf("players") == -1)
            {
                goto Label_0065;
            }
            str2 = log;
            goto Label_0147;
        Label_0065:
            if (log.IndexOf("bm") == -1)
            {
                goto Label_00F4;
            }
        Label_0076:
            try
            {
                buffer = MyEncrypt.Decrypt(JsonUtility.FromJson<AudienceLog>(log).bm);
                if (GameUtility.Binary2Object<SceneBattle.MultiPlayRecvData>(&data, buffer) == null)
                {
                    goto Label_00DD;
                }
                if (this.mTurnLog.ContainsKey(data.b) != null)
                {
                    goto Label_00C4;
                }
                this.mTurnLog[data.b] = new List<SceneBattle.MultiPlayRecvData>();
            Label_00C4:
                this.mTurnLog[data.b].Add(data);
            Label_00DD:
                goto Label_00EF;
            }
            catch
            {
            Label_00E2:
                this.mNonAnalyzeLog = log;
                goto Label_00EF;
            }
        Label_00EF:
            goto Label_0147;
        Label_00F4:
            if (log.IndexOf("bin") == -1)
            {
                goto Label_0140;
            }
        Label_0105:
            try
            {
                data2 = JsonUtility.FromJson<SceneBattle.MultiPlayRecvBinData>(log);
                GameUtility.Binary2Object<string>(&str3, data2.bin);
                this.Analyze(str3);
                goto Label_01A5;
            }
            catch
            {
            Label_012E:
                this.mNonAnalyzeLog = log;
                goto Label_013B;
            }
        Label_013B:
            goto Label_0147;
        Label_0140:
            this.mNonAnalyzeLog = log;
        Label_0147:
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0176;
            }
        Label_0152:
            try
            {
                this.mRoomParam = JSON_MyPhotonRoomParam.Parse(str);
                goto Label_0176;
            }
            catch
            {
            Label_0163:
                Debug.LogWarning(str);
                this.mNonAnalyzeLog = str;
                goto Label_0176;
            }
        Label_0176:
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_01A5;
            }
        Label_0181:
            try
            {
                this.mStartedParam = JSONParser.parseJSONObject<AudienceStartParam>(str2);
                goto Label_01A5;
            }
            catch
            {
            Label_0192:
                Debug.LogWarning(str2);
                this.mNonAnalyzeLog = str2;
                goto Label_01A5;
            }
        Label_01A5:
            return;
        }

        public void Disconnect()
        {
            this.mState = 0;
            return;
        }

        public void FinishLoad()
        {
            this.mSkipReadMax = this.LogLength;
            return;
        }

        public unsafe SceneBattle.MultiPlayRecvData GetData()
        {
            List<SceneBattle.MultiPlayRecvData> list;
            int num;
            Dictionary<int, List<SceneBattle.MultiPlayRecvData>>.KeyCollection.Enumerator enumerator;
            int num2;
            if (this.mReadCnt >= this.LogLength)
            {
                goto Label_007C;
            }
            list = new List<SceneBattle.MultiPlayRecvData>();
            enumerator = this.mTurnLog.Keys.GetEnumerator();
        Label_0028:
            try
            {
                goto Label_0047;
            Label_002D:
                num = &enumerator.Current;
                list.AddRange(this.mTurnLog[num]);
            Label_0047:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_002D;
                }
                goto Label_0064;
            }
            finally
            {
            Label_0058:
                ((Dictionary<int, List<SceneBattle.MultiPlayRecvData>>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_0064:
            return list[this.mReadCnt++];
        Label_007C:
            return null;
        }

        public JSON_MyPhotonRoomParam GetRoomParam()
        {
            return this.mRoomParam;
        }

        public AudienceStartParam GetStartedParam()
        {
            return this.mStartedParam;
        }

        public void Reset()
        {
            if (this.mTurnLog != null)
            {
                goto Label_0016;
            }
            this.mTurnLog = new Dictionary<int, List<SceneBattle.MultiPlayRecvData>>();
        Label_0016:
            this.mTurnLog.Clear();
            this.mStartedParam = null;
            this.mRoomParam = null;
            this.mNonAnalyzeLog = null;
            this.mReadCnt = 0;
            this.mRetryStartQuestCnt = 0;
            this.mDownloadLogger = null;
            this.mDownloadLogger = new DownloadLogger();
            this.mDownloadLogger.Manager = this;
            this.mState = 1;
            return;
        }

        public void ResetTime()
        {
            this.mNoneConnectedTime = 0f;
            this.mState = 1;
            return;
        }

        public void Restore()
        {
            if (this.mReadCnt <= 0)
            {
                goto Label_001A;
            }
            this.mReadCnt -= 1;
        Label_001A:
            return;
        }

        public void Update()
        {
            if (this.mState != 2)
            {
                goto Label_0035;
            }
            this.mNoneConnectedTime += Time.get_deltaTime();
            if (this.mNoneConnectedTime < CONNECTTIME_MAX)
            {
                goto Label_0035;
            }
            this.mState = 0;
        Label_0035:
            return;
        }

        public bool IsConnected
        {
            get
            {
                return (this.mState == 2);
            }
        }

        public bool IsDisconnected
        {
            get
            {
                return (this.mState == 0);
            }
        }

        public DownloadLogger Logger
        {
            get
            {
                return this.mDownloadLogger;
            }
        }

        public bool IsRetryError
        {
            get
            {
                return ((this.mRetryStartQuestCnt < this.RETRY_MAX) == 0);
            }
        }

        private int LogLength
        {
            get
            {
                int num;
                int num2;
                Dictionary<int, List<SceneBattle.MultiPlayRecvData>>.KeyCollection.Enumerator enumerator;
                num = 0;
                enumerator = this.mTurnLog.Keys.GetEnumerator();
            Label_0013:
                try
                {
                    goto Label_0034;
                Label_0018:
                    num2 = &enumerator.Current;
                    num += this.mTurnLog[num2].Count;
                Label_0034:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_0018;
                    }
                    goto Label_0051;
                }
                finally
                {
                Label_0045:
                    ((Dictionary<int, List<SceneBattle.MultiPlayRecvData>>.KeyCollection.Enumerator) enumerator).Dispose();
                }
            Label_0051:
                return num;
            }
        }

        public bool SkipMode
        {
            set
            {
                this.mSkipLog = value;
                return;
            }
        }

        public bool IsSkipEnd
        {
            get
            {
                if (this.mSkipLog == null)
                {
                    goto Label_001A;
                }
                return (this.mSkipReadMax == this.mReadCnt);
            Label_001A:
                return 1;
            }
        }

        public bool IsEnd
        {
            get
            {
            Label_001B:
                return (((Network.IsStreamConnecting == null) && (this.mReadCnt == this.LogLength)) ? 1 : Network.IsError);
            }
        }

        private enum CONNECT_STATE
        {
            NONE,
            REQ,
            CONNECTED
        }
    }
}

