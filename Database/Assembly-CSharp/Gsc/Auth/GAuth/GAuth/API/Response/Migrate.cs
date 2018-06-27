// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Response.Migrate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.DOM;
using Gsc.Network;

namespace Gsc.Auth.GAuth.GAuth.API.Response
{
  public class Migrate : GAuthResponse<Migrate>
  {
    public Migrate(WebInternalResponse response)
    {
      using (IDocument document = this.Parse(response))
        this.OldDeviceId = document.Root["old_device_id"].ToString();
    }

    public string OldDeviceId { get; private set; }
  }
}
