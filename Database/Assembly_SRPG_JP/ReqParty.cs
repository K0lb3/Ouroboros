// Decompiled with JetBrains decompiler
// Type: SRPG.ReqParty
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;

namespace SRPG
{
  public class ReqParty : WebAPI
  {
    public ReqParty(Network.ResponseCallback response, bool needUpdateMultiRoom = false, bool ignoreEmpty = true, bool needUpdateMultiRoomMT = false)
    {
      List<PartyData> partys = MonoSingleton<GameManager>.Instance.Player.Partys;
      this.name = "party2";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"parties\":[");
      int num = 0;
      for (int index1 = 0; index1 < partys.Count; ++index1)
      {
        if ((!ignoreEmpty || partys[index1].Num != 0) && index1 != 9)
        {
          if (num > 0)
            stringBuilder.Append(',');
          stringBuilder.Append("{\"units\":[");
          for (int index2 = 0; index2 < partys[index1].MAX_UNIT; ++index2)
          {
            if (index2 > 0)
              stringBuilder.Append(',');
            stringBuilder.Append(partys[index1].GetUnitUniqueID(index2));
          }
          stringBuilder.Append(']');
          string stringFromPartyType = PartyData.GetStringFromPartyType((PlayerPartyTypes) index1);
          stringBuilder.Append(",\"ptype\":\"");
          stringBuilder.Append(stringFromPartyType);
          stringBuilder.Append('"');
          stringBuilder.Append('}');
          ++num;
        }
      }
      stringBuilder.Append(']');
      if (needUpdateMultiRoom)
      {
        stringBuilder.Append(",\"roomowner\":1");
        DebugUtility.Log("UpdateMulti!");
      }
      else if (needUpdateMultiRoomMT)
      {
        stringBuilder.Append(",\"roomowner_mt\":1");
        DebugUtility.Log("UpdateMultiTower!");
      }
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
