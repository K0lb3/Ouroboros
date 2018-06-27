// Decompiled with JetBrains decompiler
// Type: Gsc.Network.WebTaskResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace Gsc.Network
{
  [Flags]
  public enum WebTaskResult : uint
  {
    None = 0,
    kLocalResult = 65280, // 0x0000FF00
    Success = 256, // 0x00000100
    MustErrorHandle = 512, // 0x00000200
    Broken = 1024, // 0x00000400
    kGrobalResult = 16711680, // 0x00FF0000
    ServerError = 65536, // 0x00010000
    UpdateResource = 131072, // 0x00020000
    Interrupt = 262144, // 0x00040000
    kUnableToContinue = 251658240, // 0x0F000000
    Maintenance = 16777216, // 0x01000000
    UpdateApplication = 33554432, // 0x02000000
    ExpiredSessionError = 67108864, // 0x04000000
    InvalidDeviceError = 134217728, // 0x08000000
    kCreticalError = 4026531840, // 0xF0000000
    UnknownError = 268435456, // 0x10000000
    ParseError = 536870912, // 0x20000000
    InvalidContentType = 1073741824, // 0x40000000
    kInternalResult = 240, // 0x000000F0
    InternalExpiredTokenError = 16, // 0x00000010
    InternalCheckMaintenance = 32, // 0x00000020
    kAll = 4294967040, // 0xFFFFFF00
  }
}
