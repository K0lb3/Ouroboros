// Decompiled with JetBrains decompiler
// Type: SRPG.HomeApi
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
