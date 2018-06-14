// Decompiled with JetBrains decompiler
// Type: DisconnectCause
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

public enum DisconnectCause
{
  SecurityExceptionOnConnect = 1022, // 0x000003FE
  ExceptionOnConnect = 1023, // 0x000003FF
  Exception = 1026, // 0x00000402
  InternalReceiveException = 1039, // 0x0000040F
  DisconnectByClientTimeout = 1040, // 0x00000410
  DisconnectByServerTimeout = 1041, // 0x00000411
  DisconnectByServerUserLimit = 1042, // 0x00000412
  DisconnectByServerLogic = 1043, // 0x00000413
  AuthenticationTicketExpired = 32753, // 0x00007FF1
  InvalidRegion = 32756, // 0x00007FF4
  MaxCcuReached = 32757, // 0x00007FF5
  InvalidAuthentication = 32767, // 0x00007FFF
}
