// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusSeason
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusSeason : WebAPI
  {
    public ReqVersusSeason(Network.ResponseCallback response)
    {
      this.name = "vs/towermatch/season";
      this.body = string.Empty;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public int unreadmail;
    }
  }
}
