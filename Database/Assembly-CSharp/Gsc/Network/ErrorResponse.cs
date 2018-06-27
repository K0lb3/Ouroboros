// Decompiled with JetBrains decompiler
// Type: Gsc.Network.ErrorResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.DOM.Json;

namespace Gsc.Network
{
  public class ErrorResponse : Response<ErrorResponse>, IResponse, IErrorResponse
  {
    public ErrorResponse(WebInternalResponse response)
    {
      if (response.Payload.Length <= 0)
        return;
      Document document = Document.Parse(response.Payload);
      this.data = (IDocument) document;
      this.ErrorCode = document.Root.GetValueByPointer("/error_code", (string) null);
    }

    public IDocument data { get; private set; }

    public string ErrorCode { get; private set; }
  }
}
