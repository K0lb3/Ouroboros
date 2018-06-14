// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.Builder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class Builder
  {
    [DllImport("gpg")]
    internal static extern void GameServices_Builder_SetOnAuthActionStarted(HandleRef self, Builder.OnAuthActionStartedCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void GameServices_Builder_AddOauthScope(HandleRef self, string scope);

    [DllImport("gpg")]
    internal static extern void GameServices_Builder_SetLogging(HandleRef self, Builder.OnLogCallback callback, IntPtr callback_arg, Types.LogLevel min_level);

    [DllImport("gpg")]
    internal static extern IntPtr GameServices_Builder_Construct();

    [DllImport("gpg")]
    internal static extern void GameServices_Builder_EnableSnapshots(HandleRef self);

    [DllImport("gpg")]
    internal static extern void GameServices_Builder_SetOnLog(HandleRef self, Builder.OnLogCallback callback, IntPtr callback_arg, Types.LogLevel min_level);

    [DllImport("gpg")]
    internal static extern void GameServices_Builder_SetDefaultOnLog(HandleRef self, Types.LogLevel min_level);

    [DllImport("gpg")]
    internal static extern void GameServices_Builder_SetOnAuthActionFinished(HandleRef self, Builder.OnAuthActionFinishedCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void GameServices_Builder_SetOnTurnBasedMatchEvent(HandleRef self, Builder.OnTurnBasedMatchEventCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void GameServices_Builder_SetOnQuestCompleted(HandleRef self, Builder.OnQuestCompletedCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void GameServices_Builder_SetOnMultiplayerInvitationEvent(HandleRef self, Builder.OnMultiplayerInvitationEventCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern IntPtr GameServices_Builder_Create(HandleRef self, IntPtr platform);

    [DllImport("gpg")]
    internal static extern void GameServices_Builder_Dispose(HandleRef self);

    internal delegate void OnLogCallback(Types.LogLevel arg0, string arg1, IntPtr arg2);

    internal delegate void OnAuthActionStartedCallback(Types.AuthOperation arg0, IntPtr arg1);

    internal delegate void OnAuthActionFinishedCallback(Types.AuthOperation arg0, CommonErrorStatus.AuthStatus arg1, IntPtr arg2);

    internal delegate void OnMultiplayerInvitationEventCallback(Types.MultiplayerEvent arg0, string arg1, IntPtr arg2, IntPtr arg3);

    internal delegate void OnTurnBasedMatchEventCallback(Types.MultiplayerEvent arg0, string arg1, IntPtr arg2, IntPtr arg3);

    internal delegate void OnQuestCompletedCallback(IntPtr arg0, IntPtr arg1);
  }
}
