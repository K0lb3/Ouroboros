// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGetConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqGetConceptCard : WebAPI
  {
    public ReqGetConceptCard(long last_card_iid, Network.ResponseCallback response)
    {
      this.name = "unit/concept";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"last_iid\":");
      stringBuilder.Append(last_card_iid);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
