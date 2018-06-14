// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.RealTimeRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class RealTimeRoom
  {
    [DllImport("gpg")]
    internal static extern Types.RealTimeRoomStatus RealTimeRoom_Status(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr RealTimeRoom_Description(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    internal static extern uint RealTimeRoom_Variant(HandleRef self);

    [DllImport("gpg")]
    internal static extern ulong RealTimeRoom_CreationTime(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr RealTimeRoom_Participants_Length(HandleRef self);

    [DllImport("gpg")]
    internal static extern IntPtr RealTimeRoom_Participants_GetElement(HandleRef self, UIntPtr index);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool RealTimeRoom_Valid(HandleRef self);

    [DllImport("gpg")]
    internal static extern uint RealTimeRoom_RemainingAutomatchingSlots(HandleRef self);

    [DllImport("gpg")]
    internal static extern ulong RealTimeRoom_AutomatchWaitEstimate(HandleRef self);

    [DllImport("gpg")]
    internal static extern IntPtr RealTimeRoom_CreatingParticipant(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr RealTimeRoom_Id(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    internal static extern void RealTimeRoom_Dispose(HandleRef self);
  }
}
