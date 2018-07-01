// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusDraft
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqVersusDraft : WebAPI
  {
    public ReqVersusDraft(string token, Network.ResponseCallback response)
    {
      this.name = "vs/draft";
      this.body = WebAPI.GetRequestString<ReqVersusDraft.RequestParam>(new ReqVersusDraft.RequestParam()
      {
        token = token
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public string token;
    }

    public class ResponseUnit
    {
      public long id;
      public int secret;
    }

    public class Response
    {
      public int turn_own;
      public ReqVersusDraft.ResponseUnit[] draft_units;
    }
  }
}
