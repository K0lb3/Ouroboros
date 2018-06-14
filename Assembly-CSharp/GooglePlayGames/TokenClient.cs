// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.TokenClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
