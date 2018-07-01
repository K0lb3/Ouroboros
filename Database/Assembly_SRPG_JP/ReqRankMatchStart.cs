// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchStart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqRankMatchStart : WebAPI
  {
    public ReqRankMatchStart(Network.ResponseCallback response)
    {
      this.name = "vs/rankmatch/start";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class EnableTimeSchedule
    {
      public long expired;
      public long next;
      public string iname;
    }

    [Serializable]
    public class StreakWin
    {
      public int num;
      public int best;
    }

    [Serializable]
    public class Response
    {
      public string app_id;
      public int schedule_id;
      public int rank;
      public int score;
      public int type;
      public int bp;
      public ReqRankMatchStart.EnableTimeSchedule enabletime;
      public string[] enemies;
      public ReqRankMatchStart.StreakWin streakwin;
      public int wincnt;
      public int losecnt;
    }
  }
}
