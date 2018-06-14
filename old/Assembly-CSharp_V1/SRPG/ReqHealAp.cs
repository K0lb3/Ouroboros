// Decompiled with JetBrains decompiler
// Type: SRPG.ReqHealAp
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
