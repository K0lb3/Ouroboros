// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Response`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.DOM.Json;
using Gsc.Network.Data;
using System.Collections.Generic;

namespace Gsc.Network
{
  public abstract class Response<T> : IResponse, IResponse<T> where T : IResponse<T>
  {
    protected virtual Dictionary<string, object> GetResult(byte[] payload)
    {
      if (payload == null || payload.Length <= 0)
        return (Dictionary<string, object>) null;
      Document document = Document.Parse(payload);
      document.SetRoot((Value) this.GetResponseRoot((IDocument) document));
      return (Dictionary<string, object>) Gsc.DOM.MiniJSON.Json.Deserialize((IValue) document.Root);
    }

    protected virtual IValue GetResponseRoot(IDocument document)
    {
      Gsc.DOM.IObject root = document.Root.GetObject();
      IValue obj;
      if (!root.TryGetValue("response", out obj))
        return document.Root;
      EntityRepository.Update(root);
      return obj;
    }
  }
}
