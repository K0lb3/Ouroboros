// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ChatDisconnectCause
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
