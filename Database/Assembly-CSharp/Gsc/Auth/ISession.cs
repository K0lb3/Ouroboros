// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.ISession
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Network;
using System;

namespace Gsc.Auth
{
  public interface ISession
  {
    string SecretKey { get; }

    string DeviceID { get; }

    string AccessToken { get; }

    string UserAgent { get; }

    void DeleteAuthKeys();

    bool CanRefreshToken(Type requestType);

    IRefreshTokenTask GetRefreshTokenTask();

    IWebTask RegisterEmailAddressAndPassword(string email, string password, bool disableValicationEmail, Action<RegisterEmailAddressAndPasswordResult> callback);

    IWebTask AddDeviceWithEmailAddressAndPassword(string email, string password, Action<AddDeviceWithEmailAddressAndPasswordResult> callback);
  }
}
