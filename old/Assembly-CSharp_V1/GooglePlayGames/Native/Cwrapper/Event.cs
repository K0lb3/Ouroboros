// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.Event
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class Event
  {
    [DllImport("gpg")]
    internal static extern ulong Event_Count(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr Event_Description(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    internal static extern UIntPtr Event_ImageUrl(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    internal static extern Types.EventVisibility Event_Visibility(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr Event_Id(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool Event_Valid(HandleRef self);

    [DllImport("gpg")]
    internal static extern void Event_Dispose(HandleRef self);

    [DllImport("gpg")]
    internal static extern IntPtr Event_Copy(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr Event_Name(HandleRef self, StringBuilder out_arg, UIntPtr out_size);
  }
}
