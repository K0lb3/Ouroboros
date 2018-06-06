// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.EndpointDiscoveryListenerHelper
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class EndpointDiscoveryListenerHelper
  {
    [DllImport("gpg")]
    internal static extern IntPtr EndpointDiscoveryListenerHelper_Construct();

    [DllImport("gpg")]
    internal static extern void EndpointDiscoveryListenerHelper_SetOnEndpointLostCallback(HandleRef self, EndpointDiscoveryListenerHelper.OnEndpointLostCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void EndpointDiscoveryListenerHelper_SetOnEndpointFoundCallback(HandleRef self, EndpointDiscoveryListenerHelper.OnEndpointFoundCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void EndpointDiscoveryListenerHelper_Dispose(HandleRef self);

    internal delegate void OnEndpointFoundCallback(long arg0, IntPtr arg1, IntPtr arg2);

    internal delegate void OnEndpointLostCallback(long arg0, string arg1, IntPtr arg2);
  }
}
