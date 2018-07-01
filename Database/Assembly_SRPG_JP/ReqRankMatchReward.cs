// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqRankMatchReward : WebAPI
  {
    public ReqRankMatchReward(Network.ResponseCallback response)
    {
      this.name = "vs/rankmatch/reward";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class RwardResponse
    {
      public string ranking;
      public string type;
    }

    [Serializable]
    public class Response
    {
      public int schedule_id;
      public int score;
      public int rank;
      public int type;
      public ReqRankMatchReward.RwardResponse reward;
    }
  }
}
