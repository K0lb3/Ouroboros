// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.CommonStatusCodes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi
{
  public enum CommonStatusCodes
  {
    SuccessCached = -1,
    Success = 0,
    ServiceMissing = 1,
    ServiceVersionUpdateRequired = 2,
    ServiceDisabled = 3,
    SignInRequired = 4,
    InvalidAccount = 5,
    ResolutionRequired = 6,
    NetworkError = 7,
    InternalError = 8,
    ServiceInvalid = 9,
    DeveloperError = 10, // 0x0000000A
    LicenseCheckFailed = 11, // 0x0000000B
    Error = 13, // 0x0000000D
    Interrupted = 14, // 0x0000000E
    Timeout = 15, // 0x0000000F
    Canceled = 16, // 0x00000010
    ApiNotConnected = 17, // 0x00000011
    AuthApiInvalidCredentials = 3000, // 0x00000BB8
    AuthApiAccessForbidden = 3001, // 0x00000BB9
    AuthApiClientError = 3002, // 0x00000BBA
    AuthApiServerError = 3003, // 0x00000BBB
    AuthTokenError = 3004, // 0x00000BBC
    AuthUrlResolution = 3005, // 0x00000BBD
  }
}
