// Decompiled with JetBrains decompiler
// Type: SRPG.BattleLogServer
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Text;

namespace SRPG
{
  public class BattleLogServer
  {
    private static readonly int MAX_REGISTER_BATTLE_LOG = 128;
    private static readonly int BATTLE_LOG_REPORT_SIZE = 2048;
    private StringBuilder mReport = new StringBuilder(BattleLogServer.BATTLE_LOG_REPORT_SIZE);
    private BattleLog[] mLogs;
    private int mLogNum;
    private int mLogTop;

    public BattleLogServer()
    {
      this.mLogs = new BattleLog[BattleLogServer.MAX_REGISTER_BATTLE_LOG];
      this.Reset();
    }

    public BattleLog this[int offset]
    {
      get
      {
        return this.mLogs[(this.mLogTop + offset) % this.mLogs.Length];
      }
    }

    public int Num
    {
      get
      {
        return this.mLogNum;
      }
    }

    public int Top
    {
      get
      {
        return this.mLogTop;
      }
    }

    public BattleLog Peek
    {
      get
      {
        return this.mLogs[this.mLogTop];
      }
    }

    public StringBuilder Report
    {
      get
      {
        return this.mReport;
      }
    }

    public void Release()
    {
      if (this.mLogs != null)
      {
        for (int index = 0; index < this.mLogs.Length; ++index)
          this.mLogs[index] = (BattleLog) null;
        this.mLogs = (BattleLog[]) null;
      }
      this.mReport = (StringBuilder) null;
    }

    public void Reset()
    {
      for (int index = 0; index < this.mLogs.Length; ++index)
        this.mLogs[index] = (BattleLog) null;
      this.mLogNum = 0;
      this.mLogTop = 0;
    }

    public LogType Log<LogType>() where LogType : BattleLog, new()
    {
      if (this.mLogNum > this.mLogs.Length)
      {
        DebugUtility.LogError("failed many log.");
        return (LogType) null;
      }
      int index = (this.mLogTop + this.mLogNum) % this.mLogs.Length;
      LogType instance = Activator.CreateInstance<LogType>();
      this.mLogs[index] = (BattleLog) instance;
      ++this.mLogNum;
      return instance;
    }

    public void RemoveLog()
    {
      if (this.mLogs[this.mLogTop] != null)
        this.mLogs[this.mLogTop].Serialize(this.mReport);
      this.mLogs[this.mLogTop] = (BattleLog) null;
      this.mLogTop = (this.mLogTop + 1) % this.mLogs.Length;
      --this.mLogNum;
    }
  }
}
