// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.IClientImpl
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.PInvoke;
using System;

namespace GooglePlayGames
{
  internal interface IClientImpl
  {
    PlatformConfiguration CreatePlatformConfiguration();

    TokenClient CreateTokenClient();

    void GetPlayerStats(IntPtr apiClient, Action<CommonStatusCodes, PlayGamesLocalUser.PlayerStats> callback);
  }
}
