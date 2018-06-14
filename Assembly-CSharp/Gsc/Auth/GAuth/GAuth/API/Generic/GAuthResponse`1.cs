// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Generic.GAuthResponse`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.DOM.Json;
using Gsc.Network;

namespace Gsc.Auth.GAuth.GAuth.API.Generic
{
  public abstract class GAuthResponse<TResponse> : Response<TResponse> where TResponse : IResponse<TResponse>
  {
    public IDocument Parse(WebInternalResponse response)
    {
      Document document = Document.Parse(response.Payload);
      Value root;
      if (document.Root.GetObject().TryGetValue("body", out root))
        document.SetRoot(root);
      return (IDocument) document;
    }
  }
}
