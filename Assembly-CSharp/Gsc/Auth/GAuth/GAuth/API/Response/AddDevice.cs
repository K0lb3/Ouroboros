// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Response.AddDevice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.DOM;
using Gsc.Network;

namespace Gsc.Auth.GAuth.GAuth.API.Response
{
  public class AddDevice : GAuthResponse<AddDevice>
  {
    public AddDevice(WebInternalResponse response)
    {
      using (IDocument document = this.Parse(response))
        this.IsSucceeded = document.Root["is_succeeded"].ToBool();
    }

    public bool IsSucceeded { get; private set; }
  }
}
