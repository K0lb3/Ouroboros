// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.TurnBasedManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class TurnBasedManager
  {
    private readonly GameServices mGameServices;

    internal TurnBasedManager(GameServices services)
    {
      this.mGameServices = services;
    }

    internal void GetMatch(string matchId, Action<TurnBasedManager.TurnBasedMatchResponse> callback)
    {
      TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_FetchMatch(this.mGameServices.AsHandle(), matchId, new TurnBasedMultiplayerManager.TurnBasedMatchCallback(TurnBasedManager.InternalTurnBasedMatchCallback), TurnBasedManager.ToCallbackPointer(callback));
    }

    [MonoPInvokeCallback(typeof (TurnBasedMultiplayerManager.TurnBasedMatchCallback))]
    internal static void InternalTurnBasedMatchCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("TurnBasedManager#InternalTurnBasedMatchCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void CreateMatch(TurnBasedMatchConfig config, Action<TurnBasedManager.TurnBasedMatchResponse> callback)
    {
      TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_CreateTurnBasedMatch(this.mGameServices.AsHandle(), config.AsPointer(), new TurnBasedMultiplayerManager.TurnBasedMatchCallback(TurnBasedManager.InternalTurnBasedMatchCallback), TurnBasedManager.ToCallbackPointer(callback));
    }

    internal void ShowPlayerSelectUI(uint minimumPlayers, uint maxiumPlayers, bool allowAutomatching, Action<PlayerSelectUIResponse> callback)
    {
      TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_ShowPlayerSelectUI(this.mGameServices.AsHandle(), minimumPlayers, maxiumPlayers, allowAutomatching, new TurnBasedMultiplayerManager.PlayerSelectUICallback(TurnBasedManager.InternalPlayerSelectUIcallback), Callbacks.ToIntPtr<PlayerSelectUIResponse>(callback, new Func<IntPtr, PlayerSelectUIResponse>(PlayerSelectUIResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (TurnBasedMultiplayerManager.PlayerSelectUICallback))]
    internal static void InternalPlayerSelectUIcallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("TurnBasedManager#PlayerSelectUICallback", Callbacks.Type.Temporary, response, data);
    }

    internal void GetAllTurnbasedMatches(Action<TurnBasedManager.TurnBasedMatchesResponse> callback)
    {
      TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_FetchMatches(this.mGameServices.AsHandle(), new TurnBasedMultiplayerManager.TurnBasedMatchesCallback(TurnBasedManager.InternalTurnBasedMatchesCallback), Callbacks.ToIntPtr<TurnBasedManager.TurnBasedMatchesResponse>(callback, new Func<IntPtr, TurnBasedManager.TurnBasedMatchesResponse>(TurnBasedManager.TurnBasedMatchesResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (TurnBasedMultiplayerManager.TurnBasedMatchesCallback))]
    internal static void InternalTurnBasedMatchesCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("TurnBasedManager#TurnBasedMatchesCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void AcceptInvitation(MultiplayerInvitation invitation, Action<TurnBasedManager.TurnBasedMatchResponse> callback)
    {
      Logger.d("Accepting invitation: " + (object) invitation.AsPointer().ToInt64());
      TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_AcceptInvitation(this.mGameServices.AsHandle(), invitation.AsPointer(), new TurnBasedMultiplayerManager.TurnBasedMatchCallback(TurnBasedManager.InternalTurnBasedMatchCallback), TurnBasedManager.ToCallbackPointer(callback));
    }

    internal void DeclineInvitation(MultiplayerInvitation invitation)
    {
      TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_DeclineInvitation(this.mGameServices.AsHandle(), invitation.AsPointer());
    }

    internal void TakeTurn(NativeTurnBasedMatch match, byte[] data, MultiplayerParticipant nextParticipant, Action<TurnBasedManager.TurnBasedMatchResponse> callback)
    {
      TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TakeMyTurn(this.mGameServices.AsHandle(), match.AsPointer(), data, new UIntPtr((uint) data.Length), match.Results().AsPointer(), nextParticipant.AsPointer(), new TurnBasedMultiplayerManager.TurnBasedMatchCallback(TurnBasedManager.InternalTurnBasedMatchCallback), TurnBasedManager.ToCallbackPointer(callback));
    }

    [MonoPInvokeCallback(typeof (TurnBasedMultiplayerManager.MatchInboxUICallback))]
    internal static void InternalMatchInboxUICallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("TurnBasedManager#MatchInboxUICallback", Callbacks.Type.Temporary, response, data);
    }

    internal void ShowInboxUI(Action<TurnBasedManager.MatchInboxUIResponse> callback)
    {
      TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_ShowMatchInboxUI(this.mGameServices.AsHandle(), new TurnBasedMultiplayerManager.MatchInboxUICallback(TurnBasedManager.InternalMatchInboxUICallback), Callbacks.ToIntPtr<TurnBasedManager.MatchInboxUIResponse>(callback, new Func<IntPtr, TurnBasedManager.MatchInboxUIResponse>(TurnBasedManager.MatchInboxUIResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (TurnBasedMultiplayerManager.MultiplayerStatusCallback))]
    internal static void InternalMultiplayerStatusCallback(CommonErrorStatus.MultiplayerStatus status, IntPtr data)
    {
      Logger.d("InternalMultiplayerStatusCallback: " + (object) status);
      Action<CommonErrorStatus.MultiplayerStatus> tempCallback = Callbacks.IntPtrToTempCallback<Action<CommonErrorStatus.MultiplayerStatus>>(data);
      try
      {
        tempCallback(status);
      }
      catch (Exception ex)
      {
        Logger.e("Error encountered executing InternalMultiplayerStatusCallback. Smothering to avoid passing exception into Native: " + (object) ex);
      }
    }

    internal void LeaveDuringMyTurn(NativeTurnBasedMatch match, MultiplayerParticipant nextParticipant, Action<CommonErrorStatus.MultiplayerStatus> callback)
    {
      TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_LeaveMatchDuringMyTurn(this.mGameServices.AsHandle(), match.AsPointer(), nextParticipant.AsPointer(), new TurnBasedMultiplayerManager.MultiplayerStatusCallback(TurnBasedManager.InternalMultiplayerStatusCallback), Callbacks.ToIntPtr((Delegate) callback));
    }

    internal void FinishMatchDuringMyTurn(NativeTurnBasedMatch match, byte[] data, ParticipantResults results, Action<TurnBasedManager.TurnBasedMatchResponse> callback)
    {
      TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_FinishMatchDuringMyTurn(this.mGameServices.AsHandle(), match.AsPointer(), data, new UIntPtr((uint) data.Length), results.AsPointer(), new TurnBasedMultiplayerManager.TurnBasedMatchCallback(TurnBasedManager.InternalTurnBasedMatchCallback), TurnBasedManager.ToCallbackPointer(callback));
    }

    internal void ConfirmPendingCompletion(NativeTurnBasedMatch match, Action<TurnBasedManager.TurnBasedMatchResponse> callback)
    {
      TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_ConfirmPendingCompletion(this.mGameServices.AsHandle(), match.AsPointer(), new TurnBasedMultiplayerManager.TurnBasedMatchCallback(TurnBasedManager.InternalTurnBasedMatchCallback), TurnBasedManager.ToCallbackPointer(callback));
    }

    internal void LeaveMatchDuringTheirTurn(NativeTurnBasedMatch match, Action<CommonErrorStatus.MultiplayerStatus> callback)
    {
      TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_LeaveMatchDuringTheirTurn(this.mGameServices.AsHandle(), match.AsPointer(), new TurnBasedMultiplayerManager.MultiplayerStatusCallback(TurnBasedManager.InternalMultiplayerStatusCallback), Callbacks.ToIntPtr((Delegate) callback));
    }

    internal void CancelMatch(NativeTurnBasedMatch match, Action<CommonErrorStatus.MultiplayerStatus> callback)
    {
      TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_CancelMatch(this.mGameServices.AsHandle(), match.AsPointer(), new TurnBasedMultiplayerManager.MultiplayerStatusCallback(TurnBasedManager.InternalMultiplayerStatusCallback), Callbacks.ToIntPtr((Delegate) callback));
    }

    internal void Rematch(NativeTurnBasedMatch match, Action<TurnBasedManager.TurnBasedMatchResponse> callback)
    {
      TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_Rematch(this.mGameServices.AsHandle(), match.AsPointer(), new TurnBasedMultiplayerManager.TurnBasedMatchCallback(TurnBasedManager.InternalTurnBasedMatchCallback), TurnBasedManager.ToCallbackPointer(callback));
    }

    private static IntPtr ToCallbackPointer(Action<TurnBasedManager.TurnBasedMatchResponse> callback)
    {
      return Callbacks.ToIntPtr<TurnBasedManager.TurnBasedMatchResponse>(callback, new Func<IntPtr, TurnBasedManager.TurnBasedMatchResponse>(TurnBasedManager.TurnBasedMatchResponse.FromPointer));
    }

    internal class MatchInboxUIResponse : BaseReferenceHolder
    {
      internal MatchInboxUIResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.UIStatus UiStatus()
      {
        return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_MatchInboxUIResponse_GetStatus(this.SelfPtr());
      }

      internal NativeTurnBasedMatch Match()
      {
        if (this.UiStatus() != CommonErrorStatus.UIStatus.VALID)
          return (NativeTurnBasedMatch) null;
        return new NativeTurnBasedMatch(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_MatchInboxUIResponse_GetMatch(this.SelfPtr()));
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_MatchInboxUIResponse_Dispose(selfPointer);
      }

      internal static TurnBasedManager.MatchInboxUIResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (TurnBasedManager.MatchInboxUIResponse) null;
        return new TurnBasedManager.MatchInboxUIResponse(pointer);
      }
    }

    internal class TurnBasedMatchResponse : BaseReferenceHolder
    {
      internal TurnBasedMatchResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.MultiplayerStatus ResponseStatus()
      {
        return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchResponse_GetStatus(this.SelfPtr());
      }

      internal bool RequestSucceeded()
      {
        return this.ResponseStatus() > ~(CommonErrorStatus.MultiplayerStatus.ERROR_INTERNAL | CommonErrorStatus.MultiplayerStatus.VALID);
      }

      internal NativeTurnBasedMatch Match()
      {
        if (!this.RequestSucceeded())
          return (NativeTurnBasedMatch) null;
        return new NativeTurnBasedMatch(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchResponse_GetMatch(this.SelfPtr()));
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchResponse_Dispose(selfPointer);
      }

      internal static TurnBasedManager.TurnBasedMatchResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (TurnBasedManager.TurnBasedMatchResponse) null;
        return new TurnBasedManager.TurnBasedMatchResponse(pointer);
      }
    }

    internal class TurnBasedMatchesResponse : BaseReferenceHolder
    {
      internal TurnBasedMatchesResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_Dispose(this.SelfPtr());
      }

      internal CommonErrorStatus.MultiplayerStatus Status()
      {
        return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetStatus(this.SelfPtr());
      }

      internal IEnumerable<MultiplayerInvitation> Invitations()
      {
        return PInvokeUtilities.ToEnumerable<MultiplayerInvitation>(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetInvitations_Length(this.SelfPtr()), (Func<UIntPtr, MultiplayerInvitation>) (index => new MultiplayerInvitation(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetInvitations_GetElement(this.SelfPtr(), index))));
      }

      internal int InvitationCount()
      {
        return (int) TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetInvitations_Length(this.SelfPtr()).ToUInt32();
      }

      internal IEnumerable<NativeTurnBasedMatch> MyTurnMatches()
      {
        return PInvokeUtilities.ToEnumerable<NativeTurnBasedMatch>(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetMyTurnMatches_Length(this.SelfPtr()), (Func<UIntPtr, NativeTurnBasedMatch>) (index => new NativeTurnBasedMatch(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetMyTurnMatches_GetElement(this.SelfPtr(), index))));
      }

      internal int MyTurnMatchesCount()
      {
        return (int) TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetMyTurnMatches_Length(this.SelfPtr()).ToUInt32();
      }

      internal IEnumerable<NativeTurnBasedMatch> TheirTurnMatches()
      {
        return PInvokeUtilities.ToEnumerable<NativeTurnBasedMatch>(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetTheirTurnMatches_Length(this.SelfPtr()), (Func<UIntPtr, NativeTurnBasedMatch>) (index => new NativeTurnBasedMatch(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetTheirTurnMatches_GetElement(this.SelfPtr(), index))));
      }

      internal int TheirTurnMatchesCount()
      {
        return (int) TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetTheirTurnMatches_Length(this.SelfPtr()).ToUInt32();
      }

      internal IEnumerable<NativeTurnBasedMatch> CompletedMatches()
      {
        return PInvokeUtilities.ToEnumerable<NativeTurnBasedMatch>(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetCompletedMatches_Length(this.SelfPtr()), (Func<UIntPtr, NativeTurnBasedMatch>) (index => new NativeTurnBasedMatch(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetCompletedMatches_GetElement(this.SelfPtr(), index))));
      }

      internal int CompletedMatchesCount()
      {
        return (int) TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_TurnBasedMatchesResponse_GetCompletedMatches_Length(this.SelfPtr()).ToUInt32();
      }

      internal static TurnBasedManager.TurnBasedMatchesResponse FromPointer(IntPtr pointer)
      {
        if (PInvokeUtilities.IsNull(pointer))
          return (TurnBasedManager.TurnBasedMatchesResponse) null;
        return new TurnBasedManager.TurnBasedMatchesResponse(pointer);
      }
    }

    internal delegate void TurnBasedMatchCallback(TurnBasedManager.TurnBasedMatchResponse response);
  }
}
