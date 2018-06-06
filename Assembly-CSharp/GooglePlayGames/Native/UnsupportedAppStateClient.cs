// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.UnsupportedAppStateClient
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;
using GooglePlayGames.OurUtils;
using System;

namespace GooglePlayGames.Native
{
  internal class UnsupportedAppStateClient : AppStateClient
  {
    private readonly string mMessage;

    internal UnsupportedAppStateClient(string message)
    {
      this.mMessage = Misc.CheckNotNull<string>(message);
    }

    public void LoadState(int slot, OnStateLoadedListener listener)
    {
      throw new NotImplementedException(this.mMessage);
    }

    public void UpdateState(int slot, byte[] data, OnStateLoadedListener listener)
    {
      throw new NotImplementedException(this.mMessage);
    }
  }
}
