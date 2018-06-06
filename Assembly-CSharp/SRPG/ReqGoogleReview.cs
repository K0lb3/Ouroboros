// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGoogleReview
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqGoogleReview : WebAPI
  {
    public ReqGoogleReview(Network.ResponseCallback response)
    {
      this.name = "serial/register/greview";
      this.body = WebAPI.GetRequestString(WebAPI.GetStringBuilder().ToString());
      this.callback = response;
    }
  }
}
