// Decompiled with JetBrains decompiler
// Type: SRPG.VersusAudienceManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class VersusAudienceManager
  {
    public static readonly float CONNECTTIME_MAX = 30f;
    public readonly int RETRY_MAX = 30;
    private Dictionary<int, List<SceneBattle.MultiPlayRecvData>> mTurnLog = new Dictionary<int, List<SceneBattle.MultiPlayRecvData>>();
    private string mNonAnalyzeLog = string.Empty;
    private AudienceStartParam mStartedParam;
    private JSON_MyPhotonRoomParam mRoomParam;
    private DownloadLogger mDownloadLogger;
    private int mReadCnt;
    private int mSkipReadMax;
    private int mRetryStartQuestCnt;
    private bool mSkipLog;
    private float mNoneConnectedTime;
    private VersusAudienceManager.CONNECT_STATE mState;

    public bool IsConnected
    {
      get
      {
        return this.mState == VersusAudienceManager.CONNECT_STATE.CONNECTED;
      }
    }

    public bool IsDisconnected
    {
      get
      {
        return this.mState == VersusAudienceManager.CONNECT_STATE.NONE;
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
        return this.mRetryStartQuestCnt >= this.RETRY_MAX;
      }
    }

    private int LogLength
    {
      get
      {
        int num = 0;
        using (Dictionary<int, List<SceneBattle.MultiPlayRecvData>>.KeyCollection.Enumerator enumerator = this.mTurnLog.Keys.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            int current = enumerator.Current;
            num += this.mTurnLog[current].Count;
          }
        }
        return num;
      }
    }

    public void Analyze(string log)
    {
      string json = string.Empty;
      string src = string.Empty;
      if (!string.IsNullOrEmpty(this.mNonAnalyzeLog))
      {
        log = this.mNonAnalyzeLog + log;
        this.mNonAnalyzeLog = string.Empty;
      }
      if (log.IndexOf("creatorName") != -1)
        json = log;
      else if (log.IndexOf("players") != -1)
        src = log;
      else if (log.IndexOf("bm") != -1)
      {
        try
        {
          byte[] data = MyEncrypt.Decrypt(((AudienceLog) JsonUtility.FromJson<AudienceLog>(log)).bm);
          SceneBattle.MultiPlayRecvData buffer;
          if (GameUtility.Binary2Object<SceneBattle.MultiPlayRecvData>(out buffer, data))
          {
            if (!this.mTurnLog.ContainsKey(buffer.b))
              this.mTurnLog[buffer.b] = new List<SceneBattle.MultiPlayRecvData>();
            this.mTurnLog[buffer.b].Add(buffer);
          }
        }
        catch
        {
          this.mNonAnalyzeLog = log;
        }
      }
      else
        this.mNonAnalyzeLog = log;
      if (!string.IsNullOrEmpty(json))
      {
        try
        {
          this.mRoomParam = JSON_MyPhotonRoomParam.Parse(json);
        }
        catch
        {
          Debug.LogWarning((object) json);
          this.mNonAnalyzeLog = json;
        }
      }
      if (string.IsNullOrEmpty(src))
        return;
      try
      {
        this.mStartedParam = JSONParser.parseJSONObject<AudienceStartParam>(src);
      }
      catch
      {
        Debug.LogWarning((object) src);
        this.mNonAnalyzeLog = src;
      }
    }

    public void AddStartQuest()
    {
      ++this.mRetryStartQuestCnt;
    }

    public AudienceStartParam GetStartedParam()
    {
      return this.mStartedParam;
    }

    public JSON_MyPhotonRoomParam GetRoomParam()
    {
      return this.mRoomParam;
    }

    public SceneBattle.MultiPlayRecvData GetData()
    {
      if (this.mReadCnt >= this.LogLength)
        return (SceneBattle.MultiPlayRecvData) null;
      List<SceneBattle.MultiPlayRecvData> multiPlayRecvDataList = new List<SceneBattle.MultiPlayRecvData>();
      using (Dictionary<int, List<SceneBattle.MultiPlayRecvData>>.KeyCollection.Enumerator enumerator = this.mTurnLog.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          int current = enumerator.Current;
          multiPlayRecvDataList.AddRange((IEnumerable<SceneBattle.MultiPlayRecvData>) this.mTurnLog[current]);
        }
      }
      return multiPlayRecvDataList[this.mReadCnt++];
    }

    public void Restore()
    {
      if (this.mReadCnt <= 0)
        return;
      --this.mReadCnt;
    }

    public void Reset()
    {
      if (this.mTurnLog == null)
        this.mTurnLog = new Dictionary<int, List<SceneBattle.MultiPlayRecvData>>();
      this.mTurnLog.Clear();
      this.mStartedParam = (AudienceStartParam) null;
      this.mRoomParam = (JSON_MyPhotonRoomParam) null;
      this.mNonAnalyzeLog = (string) null;
      this.mReadCnt = 0;
      this.mRetryStartQuestCnt = 0;
      this.mDownloadLogger = (DownloadLogger) null;
      this.mDownloadLogger = new DownloadLogger();
      this.mDownloadLogger.Manager = this;
      this.mState = VersusAudienceManager.CONNECT_STATE.REQ;
    }

    public void Add(string data)
    {
      this.Analyze(data);
      this.mState = VersusAudienceManager.CONNECT_STATE.CONNECTED;
      this.mNoneConnectedTime = 0.0f;
    }

    public void Disconnect()
    {
      this.mState = VersusAudienceManager.CONNECT_STATE.NONE;
    }

    public void FinishLoad()
    {
      this.mSkipReadMax = this.LogLength;
    }

    public void Update()
    {
      if (this.mState != VersusAudienceManager.CONNECT_STATE.CONNECTED)
        return;
      this.mNoneConnectedTime += Time.get_deltaTime();
      if ((double) this.mNoneConnectedTime < (double) VersusAudienceManager.CONNECTTIME_MAX)
        return;
      this.mState = VersusAudienceManager.CONNECT_STATE.NONE;
    }

    public void ResetTime()
    {
      this.mNoneConnectedTime = 0.0f;
      this.mState = VersusAudienceManager.CONNECT_STATE.REQ;
    }

    public bool SkipMode
    {
      set
      {
        this.mSkipLog = value;
      }
    }

    public bool IsSkipEnd
    {
      get
      {
        if (this.mSkipLog)
          return this.mSkipReadMax == this.mReadCnt;
        return true;
      }
    }

    public bool IsEnd
    {
      get
      {
        if (Network.IsStreamConnecting || this.mReadCnt != this.LogLength)
          return Network.IsError;
        return true;
      }
    }

    private enum CONNECT_STATE
    {
      NONE,
      REQ,
      CONNECTED,
    }
  }
}
