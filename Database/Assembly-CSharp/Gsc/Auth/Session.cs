// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.Session
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Device;

namespace Gsc.Auth
{
  public static class Session
  {
    public static ISession DefaultSession { get; private set; }

    public static void Init(string envName, IAccountManager accountManager)
    {
      Session.DefaultSession = (ISession) new Gsc.Auth.GAuth.GAuth.Session(envName, accountManager);
    }
  }
}
