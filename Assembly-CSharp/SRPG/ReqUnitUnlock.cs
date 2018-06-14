// Decompiled with JetBrains decompiler
// Type: SRPG.ReqUnitUnlock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqUnitUnlock : WebAPI
  {
    public ReqUnitUnlock(string iname, Network.ResponseCallback response)
    {
      this.name = "unit/add";
      this.body = WebAPI.GetRequestString("\"iname\":\"" + iname + "\"");
      this.callback = response;
    }
  }
}
