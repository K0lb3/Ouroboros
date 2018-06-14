// Decompiled with JetBrains decompiler
// Type: SRPG.ReqHealAp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqHealAp : WebAPI
  {
    public ReqHealAp(long iid, int num, Network.ResponseCallback response)
    {
      this.name = "item/addstm";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid\" : ");
      stringBuilder.Append(iid);
      stringBuilder.Append(",");
      stringBuilder.Append("\"num\" : ");
      stringBuilder.Append(num);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
