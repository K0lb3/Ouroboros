// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchMission
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqRankMatchMission : WebAPI
  {
    public ReqRankMatchMission(Network.ResponseCallback response)
    {
      this.name = "vs/rankmatch/mission";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class MissionProgress
    {
      public string iname;
      public int prog;
      public string rewarded_at;
    }

    [Serializable]
    public class Response
    {
      public ReqRankMatchMission.MissionProgress[] missionprogs;
    }
  }
}
