// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.ResponseStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi
{
  public enum ResponseStatus
  {
    Timeout = -5,
    VersionUpdateRequired = -4,
    NotAuthorized = -3,
    InternalError = -2,
    LicenseCheckFailed = -1,
    Success = 1,
    SuccessWithStale = 2,
  }
}
