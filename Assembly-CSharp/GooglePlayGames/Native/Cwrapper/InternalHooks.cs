// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.InternalHooks
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class InternalHooks
  {
    [DllImport("gpg")]
    internal static extern void InternalHooks_ConfigureForUnityPlugin(HandleRef builder);

    [DllImport("gpg")]
    internal static extern IntPtr InternalHooks_GetApiClient(HandleRef services);

    [DllImport("gpg")]
    internal static extern void InternalHooks_EnableAppState(HandleRef config);
  }
}
