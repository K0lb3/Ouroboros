// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTobiraOpen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqTobiraOpen : WebAPI
  {
    public ReqTobiraOpen(long unit_iid, TobiraParam.Category category, Network.ResponseCallback response)
    {
      this.name = "unit/door/open";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"unit_iid\":");
      stringBuilder.Append(unit_iid);
      stringBuilder.Append(",");
      stringBuilder.Append("\"category\":");
      stringBuilder.Append((int) category);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
