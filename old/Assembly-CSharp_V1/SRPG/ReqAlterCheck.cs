// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAlterCheck
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqAlterCheck : WebAPI
  {
    public ReqAlterCheck(string hash, Network.ResponseCallback response)
    {
      this.name = "altercheck";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append(nameof (hash));
      stringBuilder.Append(hash);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
