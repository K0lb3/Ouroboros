// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSetConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqSetConceptCard : WebAPI
  {
    private ReqSetConceptCard(long card_iid, long unit_iid, Network.ResponseCallback response)
    {
      this.name = "unit/concept/set";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"concept_iid\":");
      stringBuilder.Append(card_iid);
      stringBuilder.Append(",");
      stringBuilder.Append("\"unit_iid\":");
      stringBuilder.Append(unit_iid);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public static ReqSetConceptCard CreateSet(long card_iid, long unit_iid, Network.ResponseCallback response)
    {
      return new ReqSetConceptCard(card_iid, unit_iid, response);
    }

    public static ReqSetConceptCard CreateUnset(long card_iid, Network.ResponseCallback response)
    {
      return new ReqSetConceptCard(card_iid, 0L, response);
    }
  }
}
