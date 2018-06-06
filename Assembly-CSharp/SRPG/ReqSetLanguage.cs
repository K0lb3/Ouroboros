// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSetLanguage
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqSetLanguage : WebAPI
  {
    public ReqSetLanguage(string language, Network.ResponseCallback response)
    {
      string isO639 = GameUtility.ConvertLanguageToISO639(language);
      this.name = "setlanguage";
      this.body = WebAPI.GetRequestString("\"lang\":\"" + isO639 + "\"");
      this.callback = response;
    }
  }
}
