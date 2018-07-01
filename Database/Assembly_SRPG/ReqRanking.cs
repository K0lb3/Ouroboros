// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqRanking : WebAPI
  {
    public ReqRanking(string[] inames, Network.ResponseCallback response)
    {
      this.name = "btl/usedunit/multiple";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"inames\":[");
      for (int index = 0; index < inames.Length; ++index)
      {
        stringBuilder.Append("\"");
        stringBuilder.Append(JsonEscape.Escape(inames[index]));
        if (index == inames.Length - 1)
          stringBuilder.Append("\"]");
        else
          stringBuilder.Append("\",");
      }
      this.body = stringBuilder.ToString();
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
