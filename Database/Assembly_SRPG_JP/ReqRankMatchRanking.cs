// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqRankMatchRanking : WebAPI
  {
    public ReqRankMatchRanking(Network.ResponseCallback response)
    {
      this.name = "vs/rankmatch/ranking";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class ResponceRanking
    {
      public int type;
      public int score;
      public int rank;
      public Json_Friend enemy;
    }

    [Serializable]
    public class Response
    {
      public ReqRankMatchRanking.ResponceRanking[] rankings;
    }
  }
}
