// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSetLanguage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
