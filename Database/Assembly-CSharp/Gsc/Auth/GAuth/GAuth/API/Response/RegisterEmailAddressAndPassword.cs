// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Response.RegisterEmailAddressAndPassword
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.DOM;
using Gsc.Network;

namespace Gsc.Auth.GAuth.GAuth.API.Response
{
  public class RegisterEmailAddressAndPassword : GAuthResponse<RegisterEmailAddressAndPassword>
  {
    public RegisterEmailAddressAndPassword(WebInternalResponse response)
    {
      using (IDocument document = this.Parse(response))
        this.IsSuccess = document.Root["is_succeeded"].ToBool();
    }

    public bool IsSuccess { get; private set; }
  }
}
