// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.TokenClient
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames
{
  internal interface TokenClient
  {
    string GetEmail();

    string GetAccessToken();

    string GetAuthorizationCode(string serverClientId);

    string GetIdToken(string serverClientId);
  }
}
