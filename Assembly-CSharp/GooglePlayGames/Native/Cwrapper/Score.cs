// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.Score
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class Score
  {
    [DllImport("gpg")]
    internal static extern ulong Score_Value(HandleRef self);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool Score_Valid(HandleRef self);

    [DllImport("gpg")]
    internal static extern ulong Score_Rank(HandleRef self);

    [DllImport("gpg")]
    internal static extern void Score_Dispose(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr Score_Metadata(HandleRef self, StringBuilder out_arg, UIntPtr out_size);
  }
}
