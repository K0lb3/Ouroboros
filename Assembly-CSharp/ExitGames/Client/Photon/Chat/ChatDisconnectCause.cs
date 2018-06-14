// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ChatDisconnectCause
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace ExitGames.Client.Photon.Chat
{
  public enum ChatDisconnectCause
  {
    None,
    DisconnectByServerUserLimit,
    ExceptionOnConnect,
    DisconnectByServer,
    TimeoutDisconnect,
    Exception,
    InvalidAuthentication,
    MaxCcuReached,
    InvalidRegion,
    OperationNotAllowedInCurrentState,
    CustomAuthenticationFailed,
  }
}
