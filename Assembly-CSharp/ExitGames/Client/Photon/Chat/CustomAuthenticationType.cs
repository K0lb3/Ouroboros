// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.CustomAuthenticationType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
