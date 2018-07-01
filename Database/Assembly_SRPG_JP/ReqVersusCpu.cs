// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusCpu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqVersusCpu : WebAPI
  {
    public ReqVersusCpu(string iname, int deck_id, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "vs/com/req";
      stringBuilder.Append("\"iname\":\"");
      stringBuilder.Append(JsonEscape.Escape(iname));
      stringBuilder.Append("\",");
      stringBuilder.Append("\"deck_id\":");
      stringBuilder.Append(deck_id);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
