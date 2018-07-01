// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTowerFloorMission
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Text;

namespace SRPG
{
  public class ReqTowerFloorMission : WebAPI
  {
    public ReqTowerFloorMission(string tower_iname, string floor_iname, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "tower/floor/mission";
      stringBuilder.Append(WebAPI.KeyValueToString(nameof (tower_iname), tower_iname));
      stringBuilder.Append(",");
      stringBuilder.Append(WebAPI.KeyValueToString(nameof (floor_iname), floor_iname));
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    [Serializable]
    public class Json_Response
    {
      public int[] missions;
      public int[] missions_val;
    }
  }
}
