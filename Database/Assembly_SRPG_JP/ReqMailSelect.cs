// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMailSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqMailSelect : WebAPI
  {
    public ReqMailSelect(string ticketid, ReqMailSelect.type type, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "mail/select";
      stringBuilder.Append("\"iname\" : \"");
      stringBuilder.Append(ticketid);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"type\" : \"");
      switch (type)
      {
        case ReqMailSelect.type.item:
          stringBuilder.Append("item");
          break;
        case ReqMailSelect.type.unit:
          stringBuilder.Append("unit");
          break;
        case ReqMailSelect.type.artifact:
          stringBuilder.Append("artifact");
          break;
        case ReqMailSelect.type.conceptcard:
          stringBuilder.Append("conceptcard");
          break;
      }
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public enum type : byte
    {
      item,
      unit,
      artifact,
      conceptcard,
    }
  }
}
