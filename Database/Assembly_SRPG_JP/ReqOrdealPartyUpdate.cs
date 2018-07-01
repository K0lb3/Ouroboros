// Decompiled with JetBrains decompiler
// Type: SRPG.ReqOrdealPartyUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;

namespace SRPG
{
  public class ReqOrdealPartyUpdate : WebAPI
  {
    public ReqOrdealPartyUpdate(Network.ResponseCallback response, List<PartyEditData> parties)
    {
      PartyData party = MonoSingleton<GameManager>.Instance.Player.Partys[9];
      this.name = "party2/ordeal/update";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"parties\":[");
      int num = 0;
      stringBuilder.Append("{\"units\":[");
      using (List<PartyEditData>.Enumerator enumerator = parties.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          PartyEditData current = enumerator.Current;
          if (num > 0)
            stringBuilder.Append(',');
          stringBuilder.Append('[');
          for (int index = 0; index < party.MAX_UNIT && index < current.Units.Length && current.Units[index] != null; ++index)
          {
            if (index > 0)
              stringBuilder.Append(',');
            stringBuilder.Append(current.Units[index].UniqueID);
          }
          stringBuilder.Append(']');
          ++num;
        }
      }
      stringBuilder.Append(']');
      string stringFromPartyType = PartyData.GetStringFromPartyType(PlayerPartyTypes.Ordeal);
      stringBuilder.Append(",\"ptype\":\"");
      stringBuilder.Append(stringFromPartyType);
      stringBuilder.Append('"');
      stringBuilder.Append('}');
      stringBuilder.Append(']');
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
