// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.GameServices
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class GameServices
  {
    [DllImport("gpg")]
    internal static extern void GameServices_Flush(HandleRef self, GameServices.FlushCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool GameServices_IsAuthorized(HandleRef self);

    [DllImport("gpg")]
    internal static extern void GameServices_Dispose(HandleRef self);

    [DllImport("gpg")]
    internal static extern void GameServices_SignOut(HandleRef self);

    [DllImport("gpg")]
    internal static extern void GameServices_StartAuthorizationUI(HandleRef self);

    internal delegate void FlushCallback(CommonErrorStatus.FlushStatus arg0, IntPtr arg1);
  }
}
