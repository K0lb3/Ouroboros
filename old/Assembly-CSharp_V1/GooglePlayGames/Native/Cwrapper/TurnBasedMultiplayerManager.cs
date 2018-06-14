// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.TurnBasedMultiplayerManager
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class TurnBasedMultiplayerManager
  {
    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_ShowPlayerSelectUI(HandleRef self, uint minimum_players, uint maximum_players, [MarshalAs(UnmanagedType.I1)] bool allow_automatch, TurnBasedMultiplayerManager.PlayerSelectUICallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_CancelMatch(HandleRef self, IntPtr match, TurnBasedMultiplayerManager.MultiplayerStatusCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_DismissMatch(HandleRef self, IntPtr match);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_ShowMatchInboxUI(HandleRef self, TurnBasedMultiplayerManager.MatchInboxUICallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_SynchronizeData(HandleRef self);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_Rematch(HandleRef self, IntPtr match, TurnBasedMultiplayerManager.TurnBasedMatchCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_DismissInvitation(HandleRef self, IntPtr invitation);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_FetchMatch(HandleRef self, string match_id, TurnBasedMultiplayerManager.TurnBasedMatchCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_DeclineInvitation(HandleRef self, IntPtr invitation);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_FinishMatchDuringMyTurn(HandleRef self, IntPtr match, byte[] match_data, UIntPtr match_data_size, IntPtr results, TurnBasedMultiplayerManager.TurnBasedMatchCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_FetchMatches(HandleRef self, TurnBasedMultiplayerManager.TurnBasedMatchesCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_CreateTurnBasedMatch(HandleRef self, IntPtr config, TurnBasedMultiplayerManager.TurnBasedMatchCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_AcceptInvitation(HandleRef self, IntPtr invitation, TurnBasedMultiplayerManager.TurnBasedMatchCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_TakeMyTurn(HandleRef self, IntPtr match, byte[] match_data, UIntPtr match_data_size, IntPtr results, IntPtr next_participant, TurnBasedMultiplayerManager.TurnBasedMatchCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_ConfirmPendingCompletion(HandleRef self, IntPtr match, TurnBasedMultiplayerManager.TurnBasedMatchCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_LeaveMatchDuringMyTurn(HandleRef self, IntPtr match, IntPtr next_participant, TurnBasedMultiplayerManager.MultiplayerStatusCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_LeaveMatchDuringTheirTurn(HandleRef self, IntPtr match, TurnBasedMultiplayerManager.MultiplayerStatusCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_TurnBasedMatchResponse_Dispose(HandleRef self);

    [DllImport("gpg")]
    internal static extern CommonErrorStatus.MultiplayerStatus TurnBasedMultiplayerManager_TurnBasedMatchResponse_GetStatus(HandleRef self);

    [DllImport("gpg")]
    internal static extern IntPtr TurnBasedMultiplayerManager_TurnBasedMatchResponse_GetMatch(HandleRef self);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_TurnBasedMatchesResponse_Dispose(HandleRef self);

    [DllImport("gpg")]
    internal static extern CommonErrorStatus.MultiplayerStatus TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetStatus(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetInvitations_Length(HandleRef self);

    [DllImport("gpg")]
    internal static extern IntPtr TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetInvitations_GetElement(HandleRef self, UIntPtr index);

    [DllImport("gpg")]
    internal static extern UIntPtr TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetMyTurnMatches_Length(HandleRef self);

    [DllImport("gpg")]
    internal static extern IntPtr TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetMyTurnMatches_GetElement(HandleRef self, UIntPtr index);

    [DllImport("gpg")]
    internal static extern UIntPtr TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetTheirTurnMatches_Length(HandleRef self);

    [DllImport("gpg")]
    internal static extern IntPtr TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetTheirTurnMatches_GetElement(HandleRef self, UIntPtr index);

    [DllImport("gpg")]
    internal static extern UIntPtr TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetCompletedMatches_Length(HandleRef self);

    [DllImport("gpg")]
    internal static extern IntPtr TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetCompletedMatches_GetElement(HandleRef self, UIntPtr index);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_MatchInboxUIResponse_Dispose(HandleRef self);

    [DllImport("gpg")]
    internal static extern CommonErrorStatus.UIStatus TurnBasedMultiplayerManager_MatchInboxUIResponse_GetStatus(HandleRef self);

    [DllImport("gpg")]
    internal static extern IntPtr TurnBasedMultiplayerManager_MatchInboxUIResponse_GetMatch(HandleRef self);

    [DllImport("gpg")]
    internal static extern void TurnBasedMultiplayerManager_PlayerSelectUIResponse_Dispose(HandleRef self);

    [DllImport("gpg")]
    internal static extern CommonErrorStatus.UIStatus TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetStatus(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetPlayerIds_Length(HandleRef self);

    [DllImport("gpg")]
    internal static extern UIntPtr TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetPlayerIds_GetElement(HandleRef self, UIntPtr index, StringBuilder out_arg, UIntPtr out_size);

    [DllImport("gpg")]
    internal static extern uint TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetMinimumAutomatchingPlayers(HandleRef self);

    [DllImport("gpg")]
    internal static extern uint TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetMaximumAutomatchingPlayers(HandleRef self);

    internal delegate void TurnBasedMatchCallback(IntPtr arg0, IntPtr arg1);

    internal delegate void MultiplayerStatusCallback(CommonErrorStatus.MultiplayerStatus arg0, IntPtr arg1);

    internal delegate void TurnBasedMatchesCallback(IntPtr arg0, IntPtr arg1);

    internal delegate void MatchInboxUICallback(IntPtr arg0, IntPtr arg1);

    internal delegate void PlayerSelectUICallback(IntPtr arg0, IntPtr arg1);
  }
}
