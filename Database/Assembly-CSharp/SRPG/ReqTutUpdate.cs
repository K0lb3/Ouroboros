// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTutUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqTutUpdate : WebAPI
  {
    public ReqTutUpdate(long flags, Network.ResponseCallback response)
    {
      this.name = "tut/update";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"tut\":");
      stringBuilder.Append(flags);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
