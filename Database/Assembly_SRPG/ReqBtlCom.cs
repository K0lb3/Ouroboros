// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlCom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
