// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.RealtimeManager
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class RealtimeManager
  {
    private readonly GameServices mGameServices;

    internal RealtimeManager(GameServices gameServices)
    {
      this.mGameServices = Misc.CheckNotNull<GameServices>(gameServices);
    }

    internal void CreateRoom(RealtimeRoomConfig config, RealTimeEventListenerHelper helper, Action<RealtimeManager.RealTimeRoomResponse> callback)
    {
      RealTimeMultiplayerManager.RealTimeMultiplayerManager_CreateRealTimeRoom(this.mGameServices.AsHandle(), config.AsPointer(), helper.AsPointer(), new RealTimeMultiplayerManager.RealTimeRoomCallback(RealtimeManager.InternalRealTimeRoomCallback), RealtimeManager.ToCallbackPointer(callback));
    }

    internal void ShowPlayerSelectUI(uint minimumPlayers, uint maxiumPlayers, bool allowAutomatching, Action<PlayerSelectUIResponse> callback)
    {
      RealTimeMultiplayerManager.RealTimeMultiplayerManager_ShowPlayerSelectUI(this.mGameServices.AsHandle(), minimumPlayers, maxiumPlayers, allowAutomatching, new RealTimeMultiplayerManager.PlayerSelectUICallback(RealtimeManager.InternalPlayerSelectUIcallback), Callbacks.ToIntPtr<PlayerSelectUIResponse>(callback, new Func<IntPtr, PlayerSelectUIResponse>(PlayerSelectUIResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (RealTimeMultiplayerManager.PlayerSelectUICallback))]
    internal static void InternalPlayerSelectUIcallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("RealtimeManager#PlayerSelectUICallback", Callbacks.Type.Temporary, response, data);
    }

    [MonoPInvokeCallback(typeof (RealTimeMultiplayerManager.RealTimeRoomCallback))]
    internal static void InternalRealTimeRoomCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("RealtimeManager#InternalRealTimeRoomCallback", Callbacks.Type.Temporary, response, data);
    }

    [MonoPInvokeCallback(typeof (RealTimeMultiplayerManager.RoomInboxUICallback))]
    internal static void InternalRoomInboxUICallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("RealtimeManager#InternalRoomInboxUICallback", Callbacks.Type.Temporary, response, data);
    }

    internal void ShowRoomInboxUI(Action<RealtimeManager.RoomInboxUIResponse> callback)
    {
      RealTimeMultiplayerManager.RealTimeMultiplayerManager_ShowRoomInboxUI(this.mGameServices.AsHandle(), new RealTimeMultiplayerManager.RoomInboxUICallback(RealtimeManager.InternalRoomInboxUICallback), Callbacks.ToIntPtr<RealtimeManager.RoomInboxUIResponse>(callback, new Func<IntPtr, RealtimeManager.RoomInboxUIResponse>(RealtimeManager.RoomInboxUIResponse.FromPointer)));
    }

    internal void ShowWaitingRoomUI(NativeRealTimeRoom room, uint minimumParticipantsBeforeStarting, Action<RealtimeManager.WaitingRoomUIResponse> callback)
    {
      Misc.CheckNotNull<NativeRealTimeRoom>(room);
      RealTimeMultiplayerManager.RealTimeMultiplayerManager_ShowWaitingRoomUI(this.mGameServices.AsHandle(), room.AsPointer(), minimumParticipantsBeforeStarting, new RealTimeMultiplayerManager.WaitingRoomUICallback(RealtimeManager.InternalWaitingRoomUICallback), Callbacks.ToIntPtr<RealtimeManager.WaitingRoomUIResponse>(callback, new Func<IntPtr, RealtimeManager.WaitingRoomUIResponse>(RealtimeManager.WaitingRoomUIResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (RealTimeMultiplayerManager.WaitingRoomUICallback))]
    internal static void InternalWaitingRoomUICallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("RealtimeManager#InternalWaitingRoomUICallback", Callbacks.Type.Temporary, response, data);
    }

    [MonoPInvokeCallback(typeof (RealTimeMultiplayerManager.FetchInvitationsCallback))]
    internal static void InternalFetchInvitationsCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("RealtimeManager#InternalFetchInvitationsCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void FetchInvitations(Action<RealtimeManager.FetchInvitationsResponse> callback)
    {
      RealTimeMultiplayerManager.RealTimeMultiplayerManager_FetchInvitations(this.mGameServices.AsHandle(), new RealTimeMultiplayerManager.FetchInvitationsCallback(RealtimeManager.InternalFetchInvitationsCallback), Callbacks.ToIntPtr<RealtimeManager.FetchInvitationsResponse>(callback, new Func<IntPtr, RealtimeManager.FetchInvitationsResponse>(RealtimeManager.FetchInvitationsResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (RealTimeMultiplayerManager.LeaveRoomCallback))]
    internal static void InternalLeaveRoomCallback(CommonErrorStatus.ResponseStatus response, IntPtr data)
    {
      Logger.d("Entering internal callback for InternalLeaveRoomCallback");
      Action<CommonErrorStatus.ResponseStatus> tempCallback = Callbacks.IntPtrToTempCallback<Action<CommonErrorStatus.ResponseStatus>>(data);
      if (tempCallback == null)
        return;
      try
      {
        tempCallback(response);
      }
      catch (Exception ex)
      {
        Logger.e("Error encountered executing InternalLeaveRoomCallback. Smothering to avoid passing exception into Native: " + (object) ex);
      }
    }

    internal void LeaveRoom(NativeRealTimeRoom room, Action<CommonErrorStatus.ResponseStatus> callback)
    {
      RealTimeMultiplayerManager.RealTimeMultiplayerManager_LeaveRoom(this.mGameServices.AsHandle(), room.AsPointer(), new RealTimeMultiplayerManager.LeaveRoomCallback(RealtimeManager.InternalLeaveRoomCallback), Callbacks.ToIntPtr((Delegate) callback));
    }

    internal void AcceptInvitation(MultiplayerInvitation invitation, RealTimeEventListenerHelper listener, Action<RealtimeManager.RealTimeRoomResponse> callback)
    {
      RealTimeMultiplayerManager.RealTimeMultiplayerManager_AcceptInvitation(this.mGameServices.AsHandle(), invitation.AsPointer(), listener.AsPointer(), new RealTimeMultiplayerManager.RealTimeRoomCallback(RealtimeManager.InternalRealTimeRoomCallback), RealtimeManager.ToCallbackPointer(callback));
    }

    internal void DeclineInvitation(MultiplayerInvitation invitation)
    {
      RealTimeMultiplayerManager.RealTimeMultiplayerManager_DeclineInvitation(this.mGameServices.AsHandle(), invitation.AsPointer());
    }

    internal void SendReliableMessage(NativeRealTimeRoom room, MultiplayerParticipant participant, byte[] data, Action<CommonErrorStatus.MultiplayerStatus> callback)
    {
      RealTimeMultiplayerManager.RealTimeMultiplayerManager_SendReliableMessage(this.mGameServices.AsHandle(), room.AsPointer(), participant.AsPointer(), data, PInvokeUtilities.ArrayToSizeT<byte>(data), new RealTimeMultiplayerManager.SendReliableMessageCallback(RealtimeManager.InternalSendReliableMessageCallback), Callbacks.ToIntPtr((Delegate) callback));
    }

    [MonoPInvokeCallback(typeof (RealTimeMultiplayerManager.SendReliableMessageCallback))]
    internal static void InternalSendReliableMessageCallback(CommonErrorStatus.MultiplayerStatus response, IntPtr data)
    {
      Logger.d("Entering internal callback for InternalSendReliableMessageCallback " + (object) response);
      Action<CommonErrorStatus.MultiplayerStatus> tempCallback = Callbacks.IntPtrToTempCallback<Action<CommonErrorStatus.MultiplayerStatus>>(data);
      if (tempCallback == null)
        return;
      try
      {
        tempCallback(response);
      }
      catch (Exception ex)
      {
        Logger.e("Error encountered executing InternalSendReliableMessageCallback. Smothering to avoid passing exception into Native: " + (object) ex);
      }
    }

    internal void SendUnreliableMessageToAll(NativeRealTimeRoom room, byte[] data)
    {
      RealTimeMultiplayerManager.RealTimeMultiplayerManager_SendUnreliableMessageToOthers(this.mGameServices.AsHandle(), room.AsPointer(), data, PInvokeUtilities.ArrayToSizeT<byte>(data));
    }

    internal void SendUnreliableMessageToSpecificParticipants(NativeRealTimeRoom room, List<MultiplayerParticipant> recipients, byte[] data)
    {
      RealTimeMultiplayerManager.RealTimeMultiplayerManager_SendUnreliableMessage(this.mGameServices.AsHandle(), room.AsPointer(), recipients.Select<MultiplayerParticipant, IntPtr>((Func<MultiplayerParticipant, IntPtr>) (r => r.AsPointer())).ToArray<IntPtr>(), new UIntPtr((ulong) recipients.LongCount<MultiplayerParticipant>()), data, PInvokeUtilities.ArrayToSizeT<byte>(data));
    }

    private static IntPtr ToCallbackPointer(Action<RealtimeManager.RealTimeRoomResponse> callback)
    {
      return Callbacks.ToIntPtr<RealtimeManager.RealTimeRoomResponse>(callback, new Func<IntPtr, RealtimeManager.RealTimeRoomResponse>(RealtimeManager.RealTimeRoomResponse.FromPointer));
    }

    internal class RealTimeRoomResponse : BaseReferenceHolder
    {
      internal RealTimeRoomResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.MultiplayerStatus ResponseStatus()
      {
        return RealTimeMultiplayerManager.RealTimeMultiplayerManager_RealTimeRoomResponse_GetStatus(this.SelfPtr());
      }

      internal bool RequestSucceeded()
      {
        return this.ResponseStatus() > ~(CommonErrorStatus.MultiplayerStatus.ERROR_INTERNAL | CommonErrorStatus.MultiplayerStatus.VALID);
      }

      internal NativeRealTimeRoom Room()
      {
        if (!this.RequestSucceeded())
          return (NativeRealTimeRoom) null;
        return new NativeRealTimeRoom(RealTimeMultiplayerManager.RealTimeMultiplayerManager_RealTimeRoomResponse_GetRoom(this.SelfPtr()));
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        RealTimeMultiplayerManager.RealTimeMultiplayerManager_RealTimeRoomResponse_Dispose(selfPointer);
      }

      internal static RealtimeManager.RealTimeRoomResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (RealtimeManager.RealTimeRoomResponse) null;
        return new RealtimeManager.RealTimeRoomResponse(pointer);
      }
    }

    internal class RoomInboxUIResponse : BaseReferenceHolder
    {
      internal RoomInboxUIResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.UIStatus ResponseStatus()
      {
        return RealTimeMultiplayerManager.RealTimeMultiplayerManager_RoomInboxUIResponse_GetStatus(this.SelfPtr());
      }

      internal MultiplayerInvitation Invitation()
      {
        if (this.ResponseStatus() != CommonErrorStatus.UIStatus.VALID)
          return (MultiplayerInvitation) null;
        return new MultiplayerInvitation(RealTimeMultiplayerManager.RealTimeMultiplayerManager_RoomInboxUIResponse_GetInvitation(this.SelfPtr()));
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        RealTimeMultiplayerManager.RealTimeMultiplayerManager_RoomInboxUIResponse_Dispose(selfPointer);
      }

      internal static RealtimeManager.RoomInboxUIResponse FromPointer(IntPtr pointer)
      {
        if (PInvokeUtilities.IsNull(pointer))
          return (RealtimeManager.RoomInboxUIResponse) null;
        return new RealtimeManager.RoomInboxUIResponse(pointer);
      }
    }

    internal class WaitingRoomUIResponse : BaseReferenceHolder
    {
      internal WaitingRoomUIResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.UIStatus ResponseStatus()
      {
        return RealTimeMultiplayerManager.RealTimeMultiplayerManager_WaitingRoomUIResponse_GetStatus(this.SelfPtr());
      }

      internal NativeRealTimeRoom Room()
      {
        if (this.ResponseStatus() != CommonErrorStatus.UIStatus.VALID)
          return (NativeRealTimeRoom) null;
        return new NativeRealTimeRoom(RealTimeMultiplayerManager.RealTimeMultiplayerManager_WaitingRoomUIResponse_GetRoom(this.SelfPtr()));
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        RealTimeMultiplayerManager.RealTimeMultiplayerManager_WaitingRoomUIResponse_Dispose(selfPointer);
      }

      internal static RealtimeManager.WaitingRoomUIResponse FromPointer(IntPtr pointer)
      {
        if (PInvokeUtilities.IsNull(pointer))
          return (RealtimeManager.WaitingRoomUIResponse) null;
        return new RealtimeManager.WaitingRoomUIResponse(pointer);
      }
    }

    internal class FetchInvitationsResponse : BaseReferenceHolder
    {
      internal FetchInvitationsResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal bool RequestSucceeded()
      {
        return this.ResponseStatus() > ~CommonErrorStatus.ResponseStatus.ERROR_LICENSE_CHECK_FAILED;
      }

      internal CommonErrorStatus.ResponseStatus ResponseStatus()
      {
        return RealTimeMultiplayerManager.RealTimeMultiplayerManager_FetchInvitationsResponse_GetStatus(this.SelfPtr());
      }

      internal IEnumerable<MultiplayerInvitation> Invitations()
      {
        return PInvokeUtilities.ToEnumerable<MultiplayerInvitation>(RealTimeMultiplayerManager.RealTimeMultiplayerManager_FetchInvitationsResponse_GetInvitations_Length(this.SelfPtr()), (Func<UIntPtr, MultiplayerInvitation>) (index => new MultiplayerInvitation(RealTimeMultiplayerManager.RealTimeMultiplayerManager_FetchInvitationsResponse_GetInvitations_GetElement(this.SelfPtr(), index))));
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        RealTimeMultiplayerManager.RealTimeMultiplayerManager_FetchInvitationsResponse_Dispose(selfPointer);
      }

      internal static RealtimeManager.FetchInvitationsResponse FromPointer(IntPtr pointer)
      {
        if (PInvokeUtilities.IsNull(pointer))
          return (RealtimeManager.FetchInvitationsResponse) null;
        return new RealtimeManager.FetchInvitationsResponse(pointer);
      }
    }
  }
}
