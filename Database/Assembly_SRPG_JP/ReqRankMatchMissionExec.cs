// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchMissionExec
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqRankMatchMissionExec : WebAPI
  {
    public ReqRankMatchMissionExec(string iname, Network.ResponseCallback response)
    {
      this.name = "vs/rankmatch/mission/exec";
      this.body = WebAPI.GetRequestString<ReqRankMatchMissionExec.RequestParam>(new ReqRankMatchMissionExec.RequestParam()
      {
        iname = iname
      });
      this.callback = response;
    }

    [Serializable]
    private class RequestParam
    {
      public string iname;
    }
  }
}
