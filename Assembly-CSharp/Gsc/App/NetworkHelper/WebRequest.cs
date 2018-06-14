// Decompiled with JetBrains decompiler
// Type: Gsc.App.NetworkHelper.WebRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace Gsc.App.NetworkHelper
{
  public class WebRequest : ApiRequest<WebRequest, WebResponse>
  {
    private readonly string method;
    private readonly string path;
    private readonly byte[] payload;

    public WebRequest(string method, string path, byte[] payload)
    {
      this.method = method;
      this.path = path;
      this.payload = payload;
    }

    public override string GetMethod()
    {
      return this.method;
    }

    public override byte[] GetPayload()
    {
      return this.payload;
    }

    public override string GetPath()
    {
      return "/" + this.path;
    }
  }
}
