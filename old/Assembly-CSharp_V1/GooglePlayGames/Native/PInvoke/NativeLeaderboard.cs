// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeLeaderboard
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class NativeLeaderboard : BaseReferenceHolder
  {
    internal NativeLeaderboard(IntPtr selfPtr)
      : base(selfPtr)
    {
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      Leaderboard.Leaderboard_Dispose(selfPointer);
    }

    internal string Title()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => Leaderboard.Leaderboard_Name(this.SelfPtr(), out_string, out_size)));
    }

    internal static NativeLeaderboard FromPointer(IntPtr pointer)
    {
      if (pointer.Equals((object) IntPtr.Zero))
        return (NativeLeaderboard) null;
      return new NativeLeaderboard(pointer);
    }
  }
}
