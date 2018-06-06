// Decompiled with JetBrains decompiler
// Type: SRPG.ReqCheckVersion
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqCheckVersion : WebAPI
  {
    public ReqCheckVersion(string ver, string os, Network.ResponseCallback response)
    {
      this.name = "chkver";
      this.body = "{";
      ReqCheckVersion reqCheckVersion1 = this;
      reqCheckVersion1.body = reqCheckVersion1.body + "\"ticket\":" + (object) Network.TicketID + ",";
      this.body += "\"param\":{";
      ReqCheckVersion reqCheckVersion2 = this;
      reqCheckVersion2.body = reqCheckVersion2.body + "\"ver\":\"" + ver + "\",";
      ReqCheckVersion reqCheckVersion3 = this;
      reqCheckVersion3.body = reqCheckVersion3.body + "\"os\":\"" + os + "\"";
      this.body += "}";
      this.body += "}";
      this.callback = response;
    }
  }
}
