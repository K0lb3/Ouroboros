// Decompiled with JetBrains decompiler
// Type: Com.Google.Android.Gms.Games.Games_BaseGamesApiMethodImpl`1
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Com.Google.Android.Gms.Common.Api;
using Google.Developers;
using System;

namespace Com.Google.Android.Gms.Games
{
  public class Games_BaseGamesApiMethodImpl<R> : JavaObjWrapper where R : Result
  {
    private const string CLASS_NAME = "com/google/android/gms/games/Games$BaseGamesApiMethodImpl";

    public Games_BaseGamesApiMethodImpl(IntPtr ptr)
      : base(ptr)
    {
    }

    public Games_BaseGamesApiMethodImpl(GoogleApiClient arg_GoogleApiClient_1)
    {
      this.CreateInstance("com/google/android/gms/games/Games$BaseGamesApiMethodImpl", (object) arg_GoogleApiClient_1);
    }
  }
}
