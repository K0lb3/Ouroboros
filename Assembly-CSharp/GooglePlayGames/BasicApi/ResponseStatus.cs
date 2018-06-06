// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.ResponseStatus
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
