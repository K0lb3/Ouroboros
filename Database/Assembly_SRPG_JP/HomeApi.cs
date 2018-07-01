// Decompiled with JetBrains decompiler
// Type: SRPG.HomeApi
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class HomeApi : WebAPI
  {
    public HomeApi(bool isMultiPush, Network.ResponseCallback response)
    {
      this.name = "home";
      if (isMultiPush)
      {
        StringBuilder stringBuilder = WebAPI.GetStringBuilder();
        stringBuilder.Append("\"is_multi_push\":1");
        this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      }
      else
        this.body = WebAPI.GetRequestString((string) null);
      this.callback = response;
    }
  }
}
