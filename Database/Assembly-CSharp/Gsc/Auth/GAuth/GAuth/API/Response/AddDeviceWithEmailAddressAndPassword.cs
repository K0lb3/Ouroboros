// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Response.AddDeviceWithEmailAddressAndPassword
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.DOM;
using Gsc.Network;

namespace Gsc.Auth.GAuth.GAuth.API.Response
{
  public class AddDeviceWithEmailAddressAndPassword : GAuthResponse<AddDeviceWithEmailAddressAndPassword>
  {
    public AddDeviceWithEmailAddressAndPassword(WebInternalResponse response)
    {
      using (IDocument document = this.Parse(response))
      {
        this.DeviceId = document.Root["device_id"].ToString();
        this.SecretKey = document.Root["secret_key"].ToString();
      }
    }

    public string DeviceId { get; private set; }

    public string SecretKey { get; private set; }
  }
}
