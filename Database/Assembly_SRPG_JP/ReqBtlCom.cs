// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlCom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqBtlCom : WebAPI
  {
    public ReqBtlCom(Network.ResponseCallback response, bool refresh = false, bool tower_progress = false)
    {
      this.name = "btl/com";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      if (refresh)
        stringBuilder.Append("\"event\":1,");
      if (tower_progress)
        stringBuilder.Append("\"is_tower\":1,");
      string body = stringBuilder.ToString();
      if (!string.IsNullOrEmpty(body))
        body = body.Remove(body.Length - 1);
      this.body = WebAPI.GetRequestString(body);
      this.callback = response;
    }
  }
}
