// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.AppStateClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;

namespace GooglePlayGames.Native
{
  internal interface AppStateClient
  {
    void LoadState(int slot, OnStateLoadedListener listener);

    void UpdateState(int slot, byte[] data, OnStateLoadedListener listener);
  }
}
