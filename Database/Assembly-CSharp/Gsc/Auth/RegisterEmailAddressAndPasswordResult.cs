// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.RegisterEmailAddressAndPasswordResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace Gsc.Auth
{
  public struct RegisterEmailAddressAndPasswordResult
  {
    public RegisterEmailAddressAndPasswordResult(RegisterEmailAddressAndPasswordResultCode resultCode)
    {
      this.ResultCode = resultCode;
    }

    public RegisterEmailAddressAndPasswordResultCode ResultCode { get; private set; }

    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public static bool operator true(RegisterEmailAddressAndPasswordResult self)
    {
      return self.ResultCode == RegisterEmailAddressAndPasswordResultCode.Success;
    }

    public static bool operator false(RegisterEmailAddressAndPasswordResult self)
    {
      return self.ResultCode != RegisterEmailAddressAndPasswordResultCode.Success;
    }

    public static bool operator ==(RegisterEmailAddressAndPasswordResult self, RegisterEmailAddressAndPasswordResultCode resultCode)
    {
      return self.ResultCode == resultCode;
    }

    public static bool operator !=(RegisterEmailAddressAndPasswordResult self, RegisterEmailAddressAndPasswordResultCode resultCode)
    {
      return self.ResultCode != resultCode;
    }
  }
}
