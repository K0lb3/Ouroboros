// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusMake
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusMake : WebAPI
  {
    public ReqVersusMake(VERSUS_TYPE type, string comment, string iname, bool isLine = false, Network.ResponseCallback response = null)
    {
      this.name = "vs/" + type.ToString().ToLower() + "match/make";
      this.body = string.Empty;
      ReqVersusMake reqVersusMake1 = this;
      reqVersusMake1.body = reqVersusMake1.body + "\"comment\":\"" + JsonEscape.Escape(comment) + "\",";
      ReqVersusMake reqVersusMake2 = this;
      reqVersusMake2.body = reqVersusMake2.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\",";
      ReqVersusMake reqVersusMake3 = this;
      reqVersusMake3.body = reqVersusMake3.body + "\"Line\":" + (object) (!isLine ? 0 : 1);
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public string token;
      public string owner_name;
      public int roomid;
    }
  }
}
