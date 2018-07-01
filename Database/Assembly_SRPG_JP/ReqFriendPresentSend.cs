// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendPresentSend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqFriendPresentSend : WebAPI
  {
    public ReqFriendPresentSend(string url, Network.ResponseCallback response, string text, string trophyprog = null, string bingoprog = null)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append(text);
      if (!string.IsNullOrEmpty(trophyprog))
        stringBuilder.Append(trophyprog);
      if (!string.IsNullOrEmpty(bingoprog))
      {
        if (!string.IsNullOrEmpty(trophyprog))
          stringBuilder.Append(",");
        stringBuilder.Append(bingoprog);
      }
      this.name = url;
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
