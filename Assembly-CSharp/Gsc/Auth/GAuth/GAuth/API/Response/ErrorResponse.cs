// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.DOM.Json;
using Gsc.Network;

namespace Gsc.Auth.GAuth.GAuth.API.Response
{
  public class ErrorResponse : Gsc.Network.Response<ErrorResponse>, IResponse, IErrorResponse
  {
    public ErrorResponse(WebInternalResponse response)
    {
      this.data = Document.Parse(response.Payload);
      this.ErrorCode = this.data.Root.GetValueByPointer("/code", (string) null) ?? this.data.Root.GetValueByPointer("/error_code", (string) null);
    }

    IDocument IErrorResponse.data
    {
      get
      {
        return (IDocument) this.data;
      }
    }

    public string ErrorCode { get; private set; }

    public Document data { get; private set; }
  }
}
