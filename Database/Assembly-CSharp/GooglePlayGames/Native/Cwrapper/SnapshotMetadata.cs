// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.SnapshotMetadata
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class SnapshotMetadata
  {
    [DllImport("gpg")]
    internal static extern void SnapshotMetadata_Dispose(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr SnapshotMetadata_CoverImageURL(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    internal static extern UIntPtr SnapshotMetadata_Description(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool SnapshotMetadata_IsOpen(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr SnapshotMetadata_FileName(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool SnapshotMetadata_Valid(HandleRef self);

    [DllImport("gpg")]
    internal static extern long SnapshotMetadata_PlayedTime(HandleRef self);

    [DllImport("gpg")]
    internal static extern long SnapshotMetadata_LastModifiedTime(HandleRef self);
  }
}
