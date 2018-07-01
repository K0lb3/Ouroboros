// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFavoriteConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqFavoriteConceptCard : WebAPI
  {
    public ReqFavoriteConceptCard(long card_iid, bool is_favorite, Network.ResponseCallback response)
    {
      this.name = "unit/concept/favorite";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"concept_iid\":");
      stringBuilder.Append(card_iid);
      stringBuilder.Append(",");
      stringBuilder.Append("\"is_favorite\":");
      stringBuilder.Append(!is_favorite ? 0 : 1);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
