// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.CustomAuthenticationType
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace ExitGames.Client.Photon.Chat
{
  public enum CustomAuthenticationType : byte
  {
    Custom = 0,
    Steam = 1,
    Facebook = 2,
    Oculus = 3,
    PlayStation = 4,
    Xbox = 5,
    None = 255, // 0xFF
  }
}
