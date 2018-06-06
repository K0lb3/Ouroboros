// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.ScorePage
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class ScorePage
  {
    [DllImport("gpg")]
    internal static extern void ScorePage_Dispose(HandleRef self);

    [DllImport("gpg")]
    internal static extern Types.LeaderboardTimeSpan ScorePage_TimeSpan(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr ScorePage_LeaderboardId(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    internal static extern Types.LeaderboardCollection ScorePage_Collection(HandleRef self);

    [DllImport("gpg")]
    internal static extern Types.LeaderboardStart ScorePage_Start(HandleRef self);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool ScorePage_Valid(HandleRef self);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool ScorePage_HasPreviousScorePage(HandleRef self);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool ScorePage_HasNextScorePage(HandleRef self);

    [DllImport("gpg")]
    internal static extern IntPtr ScorePage_PreviousScorePageToken(HandleRef self);

    [DllImport("gpg")]
    internal static extern IntPtr ScorePage_NextScorePageToken(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr ScorePage_Entries_Length(HandleRef self);

    [DllImport("gpg")]
    internal static extern IntPtr ScorePage_Entries_GetElement(HandleRef self, UIntPtr index);

    [DllImport("gpg")]
    internal static extern void ScorePage_Entry_Dispose(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr ScorePage_Entry_PlayerId(HandleRef self, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    internal static extern ulong ScorePage_Entry_LastModified(HandleRef self);

    [DllImport("gpg")]
    internal static extern IntPtr ScorePage_Entry_Score(HandleRef self);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool ScorePage_Entry_Valid(HandleRef self);

    [DllImport("gpg")]
    internal static extern ulong ScorePage_Entry_LastModifiedTime(HandleRef self);

    [DllImport("gpg")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool ScorePage_ScorePageToken_Valid(HandleRef self);

    [DllImport("gpg")]
    internal static extern void ScorePage_ScorePageToken_Dispose(HandleRef self);
  }
}
