// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.NativeRealtimeMultiplayerClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GooglePlayGames.Native
{
  public class NativeRealtimeMultiplayerClient : IRealTimeMultiplayerClient
  {
    private readonly object mSessionLock = new object();
    private readonly NativeClient mNativeClient;
    private readonly RealtimeManager mRealtimeManager;
    private volatile NativeRealtimeMultiplayerClient.RoomSession mCurrentSession;

    internal NativeRealtimeMultiplayerClient(NativeClient nativeClient, RealtimeManager manager)
    {
      this.mNativeClient = Misc.CheckNotNull<NativeClient>(nativeClient);
      this.mRealtimeManager = Misc.CheckNotNull<RealtimeManager>(manager);
      this.mCurrentSession = this.GetTerminatedSession();
      PlayGamesHelperObject.AddPauseCallback(new Action<bool>(this.HandleAppPausing));
    }

    private NativeRealtimeMultiplayerClient.RoomSession GetTerminatedSession()
    {
      NativeRealtimeMultiplayerClient.RoomSession session = new NativeRealtimeMultiplayerClient.RoomSession(this.mRealtimeManager, (RealTimeMultiplayerListener) new NativeRealtimeMultiplayerClient.NoopListener());
      session.EnterState((NativeRealtimeMultiplayerClient.State) new NativeRealtimeMultiplayerClient.ShutdownState(session));
      return session;
    }

    public void CreateQuickGame(uint minOpponents, uint maxOpponents, uint variant, RealTimeMultiplayerListener listener)
    {
      this.CreateQuickGame(minOpponents, maxOpponents, variant, 0UL, listener);
    }

    public void CreateQuickGame(uint minOpponents, uint maxOpponents, uint variant, ulong exclusiveBitMask, RealTimeMultiplayerListener listener)
    {
      lock (this.mSessionLock)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        NativeRealtimeMultiplayerClient.\u003CCreateQuickGame\u003Ec__AnonStorey19D gameCAnonStorey19D = new NativeRealtimeMultiplayerClient.\u003CCreateQuickGame\u003Ec__AnonStorey19D();
        // ISSUE: reference to a compiler-generated field
        gameCAnonStorey19D.\u003C\u003Ef__this = this;
        // ISSUE: reference to a compiler-generated field
        gameCAnonStorey19D.newSession = new NativeRealtimeMultiplayerClient.RoomSession(this.mRealtimeManager, listener);
        if (this.mCurrentSession.IsActive())
        {
          Logger.e("Received attempt to create a new room without cleaning up the old one.");
          // ISSUE: reference to a compiler-generated field
          gameCAnonStorey19D.newSession.LeaveRoom();
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          this.mCurrentSession = gameCAnonStorey19D.newSession;
          Logger.d("QuickGame: Setting MinPlayersToStart = " + (object) minOpponents);
          this.mCurrentSession.MinPlayersToStart = minOpponents;
          using (RealtimeRoomConfigBuilder roomConfigBuilder = RealtimeRoomConfigBuilder.Create())
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            NativeRealtimeMultiplayerClient.\u003CCreateQuickGame\u003Ec__AnonStorey19B gameCAnonStorey19B = new NativeRealtimeMultiplayerClient.\u003CCreateQuickGame\u003Ec__AnonStorey19B();
            // ISSUE: reference to a compiler-generated field
            gameCAnonStorey19B.\u003C\u003Ef__this = this;
            // ISSUE: reference to a compiler-generated field
            gameCAnonStorey19B.config = roomConfigBuilder.SetMinimumAutomatchingPlayers(minOpponents).SetMaximumAutomatchingPlayers(maxOpponents).SetVariant(variant).SetExclusiveBitMask(exclusiveBitMask).Build();
            // ISSUE: reference to a compiler-generated field
            using (gameCAnonStorey19B.config)
            {
              // ISSUE: object of a compiler-generated type is created
              // ISSUE: variable of a compiler-generated type
              NativeRealtimeMultiplayerClient.\u003CCreateQuickGame\u003Ec__AnonStorey19C gameCAnonStorey19C = new NativeRealtimeMultiplayerClient.\u003CCreateQuickGame\u003Ec__AnonStorey19C();
              // ISSUE: reference to a compiler-generated field
              gameCAnonStorey19C.\u003C\u003Ef__ref\u0024413 = gameCAnonStorey19D;
              // ISSUE: reference to a compiler-generated field
              gameCAnonStorey19C.\u003C\u003Ef__ref\u0024411 = gameCAnonStorey19B;
              // ISSUE: reference to a compiler-generated field
              gameCAnonStorey19C.\u003C\u003Ef__this = this;
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              gameCAnonStorey19C.helper = NativeRealtimeMultiplayerClient.HelperForSession(gameCAnonStorey19D.newSession);
              try
              {
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated method
                gameCAnonStorey19D.newSession.StartRoomCreation(this.mNativeClient.GetUserId(), new Action(gameCAnonStorey19C.\u003C\u003Em__88));
              }
              finally
              {
                // ISSUE: reference to a compiler-generated field
                if (gameCAnonStorey19C.helper != null)
                {
                  // ISSUE: reference to a compiler-generated field
                  gameCAnonStorey19C.helper.Dispose();
                }
              }
            }
          }
        }
      }
    }

    private static GooglePlayGames.Native.PInvoke.RealTimeEventListenerHelper HelperForSession(NativeRealtimeMultiplayerClient.RoomSession session)
    {
      return GooglePlayGames.Native.PInvoke.RealTimeEventListenerHelper.Create().SetOnDataReceivedCallback((Action<NativeRealTimeRoom, GooglePlayGames.Native.PInvoke.MultiplayerParticipant, byte[], bool>) ((room, participant, data, isReliable) => session.OnDataReceived(room, participant, data, isReliable))).SetOnParticipantStatusChangedCallback((Action<NativeRealTimeRoom, GooglePlayGames.Native.PInvoke.MultiplayerParticipant>) ((room, participant) => session.OnParticipantStatusChanged(room, participant))).SetOnRoomConnectedSetChangedCallback((Action<NativeRealTimeRoom>) (room => session.OnConnectedSetChanged(room))).SetOnRoomStatusChangedCallback((Action<NativeRealTimeRoom>) (room => session.OnRoomStatusChanged(room)));
    }

    private void HandleAppPausing(bool paused)
    {
      if (!paused)
        return;
      Logger.d("Application is pausing, which disconnects the RTMP  client.  Leaving room.");
      this.LeaveRoom();
    }

    public void CreateWithInvitationScreen(uint minOpponents, uint maxOppponents, uint variant, RealTimeMultiplayerListener listener)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      NativeRealtimeMultiplayerClient.\u003CCreateWithInvitationScreen\u003Ec__AnonStorey1A0 screenCAnonStorey1A0 = new NativeRealtimeMultiplayerClient.\u003CCreateWithInvitationScreen\u003Ec__AnonStorey1A0();
      // ISSUE: reference to a compiler-generated field
      screenCAnonStorey1A0.variant = variant;
      // ISSUE: reference to a compiler-generated field
      screenCAnonStorey1A0.\u003C\u003Ef__this = this;
      lock (this.mSessionLock)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        NativeRealtimeMultiplayerClient.\u003CCreateWithInvitationScreen\u003Ec__AnonStorey19F screenCAnonStorey19F = new NativeRealtimeMultiplayerClient.\u003CCreateWithInvitationScreen\u003Ec__AnonStorey19F();
        // ISSUE: reference to a compiler-generated field
        screenCAnonStorey19F.\u003C\u003Ef__ref\u0024416 = screenCAnonStorey1A0;
        // ISSUE: reference to a compiler-generated field
        screenCAnonStorey19F.\u003C\u003Ef__this = this;
        // ISSUE: reference to a compiler-generated field
        screenCAnonStorey19F.newRoom = new NativeRealtimeMultiplayerClient.RoomSession(this.mRealtimeManager, listener);
        if (this.mCurrentSession.IsActive())
        {
          Logger.e("Received attempt to create a new room without cleaning up the old one.");
          // ISSUE: reference to a compiler-generated field
          screenCAnonStorey19F.newRoom.LeaveRoom();
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          this.mCurrentSession = screenCAnonStorey19F.newRoom;
          this.mCurrentSession.ShowingUI = true;
          // ISSUE: reference to a compiler-generated method
          this.mRealtimeManager.ShowPlayerSelectUI(minOpponents, maxOppponents, true, new Action<PlayerSelectUIResponse>(screenCAnonStorey19F.\u003C\u003Em__8D));
        }
      }
    }

    public void ShowWaitingRoomUI()
    {
      lock (this.mSessionLock)
        this.mCurrentSession.ShowWaitingRoomUI();
    }

    public void GetAllInvitations(Action<Invitation[]> callback)
    {
      this.mRealtimeManager.FetchInvitations((Action<RealtimeManager.FetchInvitationsResponse>) (response =>
      {
        if (!response.RequestSucceeded())
        {
          Logger.e("Couldn't load invitations.");
          callback(new Invitation[0]);
        }
        else
        {
          List<Invitation> invitationList = new List<Invitation>();
          foreach (GooglePlayGames.Native.PInvoke.MultiplayerInvitation invitation in response.Invitations())
          {
            using (invitation)
              invitationList.Add(invitation.AsInvitation());
          }
          callback(invitationList.ToArray());
        }
      }));
    }

    public void AcceptFromInbox(RealTimeMultiplayerListener listener)
    {
      lock (this.mSessionLock)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        NativeRealtimeMultiplayerClient.\u003CAcceptFromInbox\u003Ec__AnonStorey1A4 inboxCAnonStorey1A4 = new NativeRealtimeMultiplayerClient.\u003CAcceptFromInbox\u003Ec__AnonStorey1A4();
        // ISSUE: reference to a compiler-generated field
        inboxCAnonStorey1A4.\u003C\u003Ef__this = this;
        // ISSUE: reference to a compiler-generated field
        inboxCAnonStorey1A4.newRoom = new NativeRealtimeMultiplayerClient.RoomSession(this.mRealtimeManager, listener);
        if (this.mCurrentSession.IsActive())
        {
          Logger.e("Received attempt to accept invitation without cleaning up active session.");
          // ISSUE: reference to a compiler-generated field
          inboxCAnonStorey1A4.newRoom.LeaveRoom();
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          this.mCurrentSession = inboxCAnonStorey1A4.newRoom;
          this.mCurrentSession.ShowingUI = true;
          // ISSUE: reference to a compiler-generated method
          this.mRealtimeManager.ShowRoomInboxUI(new Action<RealtimeManager.RoomInboxUIResponse>(inboxCAnonStorey1A4.\u003C\u003Em__8F));
        }
      }
    }

    public void AcceptInvitation(string invitationId, RealTimeMultiplayerListener listener)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      NativeRealtimeMultiplayerClient.\u003CAcceptInvitation\u003Ec__AnonStorey1A8 invitationCAnonStorey1A8 = new NativeRealtimeMultiplayerClient.\u003CAcceptInvitation\u003Ec__AnonStorey1A8();
      // ISSUE: reference to a compiler-generated field
      invitationCAnonStorey1A8.invitationId = invitationId;
      // ISSUE: reference to a compiler-generated field
      invitationCAnonStorey1A8.\u003C\u003Ef__this = this;
      lock (this.mSessionLock)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        NativeRealtimeMultiplayerClient.\u003CAcceptInvitation\u003Ec__AnonStorey1A7 invitationCAnonStorey1A7 = new NativeRealtimeMultiplayerClient.\u003CAcceptInvitation\u003Ec__AnonStorey1A7();
        // ISSUE: reference to a compiler-generated field
        invitationCAnonStorey1A7.\u003C\u003Ef__ref\u0024424 = invitationCAnonStorey1A8;
        // ISSUE: reference to a compiler-generated field
        invitationCAnonStorey1A7.\u003C\u003Ef__this = this;
        // ISSUE: reference to a compiler-generated field
        invitationCAnonStorey1A7.newRoom = new NativeRealtimeMultiplayerClient.RoomSession(this.mRealtimeManager, listener);
        if (this.mCurrentSession.IsActive())
        {
          Logger.e("Received attempt to accept invitation without cleaning up active session.");
          // ISSUE: reference to a compiler-generated field
          invitationCAnonStorey1A7.newRoom.LeaveRoom();
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          this.mCurrentSession = invitationCAnonStorey1A7.newRoom;
          // ISSUE: reference to a compiler-generated method
          this.mRealtimeManager.FetchInvitations(new Action<RealtimeManager.FetchInvitationsResponse>(invitationCAnonStorey1A7.\u003C\u003Em__90));
        }
      }
    }

    public Invitation GetInvitation()
    {
      return this.mCurrentSession.GetInvitation();
    }

    public void LeaveRoom()
    {
      this.mCurrentSession.LeaveRoom();
    }

    public void SendMessageToAll(bool reliable, byte[] data)
    {
      this.mCurrentSession.SendMessageToAll(reliable, data);
    }

    public void SendMessageToAll(bool reliable, byte[] data, int offset, int length)
    {
      this.mCurrentSession.SendMessageToAll(reliable, data, offset, length);
    }

    public void SendMessage(bool reliable, string participantId, byte[] data)
    {
      this.mCurrentSession.SendMessage(reliable, participantId, data);
    }

    public void SendMessage(bool reliable, string participantId, byte[] data, int offset, int length)
    {
      this.mCurrentSession.SendMessage(reliable, participantId, data, offset, length);
    }

    public List<Participant> GetConnectedParticipants()
    {
      return this.mCurrentSession.GetConnectedParticipants();
    }

    public Participant GetSelf()
    {
      return this.mCurrentSession.GetSelf();
    }

    public Participant GetParticipant(string participantId)
    {
      return this.mCurrentSession.GetParticipant(participantId);
    }

    public bool IsRoomConnected()
    {
      return this.mCurrentSession.IsRoomConnected();
    }

    public void DeclineInvitation(string invitationId)
    {
      this.mRealtimeManager.FetchInvitations((Action<RealtimeManager.FetchInvitationsResponse>) (response =>
      {
        if (!response.RequestSucceeded())
        {
          Logger.e("Couldn't load invitations.");
        }
        else
        {
          foreach (GooglePlayGames.Native.PInvoke.MultiplayerInvitation invitation in response.Invitations())
          {
            using (invitation)
            {
              if (invitation.Id().Equals(invitationId))
                this.mRealtimeManager.DeclineInvitation(invitation);
            }
          }
        }
      }));
    }

    private static T WithDefault<T>(T presented, T defaultValue) where T : class
    {
      if ((object) presented != null)
        return presented;
      return defaultValue;
    }

    private class NoopListener : RealTimeMultiplayerListener
    {
      public void OnRoomSetupProgress(float percent)
      {
      }

      public void OnRoomConnected(bool success)
      {
      }

      public void OnLeftRoom()
      {
      }

      public void OnParticipantLeft(Participant participant)
      {
      }

      public void OnPeersConnected(string[] participantIds)
      {
      }

      public void OnPeersDisconnected(string[] participantIds)
      {
      }

      public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
      {
      }
    }

    private class RoomSession
    {
      private readonly object mLifecycleLock = new object();
      private readonly NativeRealtimeMultiplayerClient.OnGameThreadForwardingListener mListener;
      private readonly RealtimeManager mManager;
      private volatile string mCurrentPlayerId;
      private volatile NativeRealtimeMultiplayerClient.State mState;
      private volatile bool mStillPreRoomCreation;
      private Invitation mInvitation;
      private volatile bool mShowingUI;
      private uint mMinPlayersToStart;

      internal RoomSession(RealtimeManager manager, RealTimeMultiplayerListener listener)
      {
        this.mManager = Misc.CheckNotNull<RealtimeManager>(manager);
        this.mListener = new NativeRealtimeMultiplayerClient.OnGameThreadForwardingListener(listener);
        this.EnterState((NativeRealtimeMultiplayerClient.State) new NativeRealtimeMultiplayerClient.BeforeRoomCreateStartedState(this));
        this.mStillPreRoomCreation = true;
      }

      internal bool ShowingUI
      {
        get
        {
          return this.mShowingUI;
        }
        set
        {
          this.mShowingUI = value;
        }
      }

      internal uint MinPlayersToStart
      {
        get
        {
          return this.mMinPlayersToStart;
        }
        set
        {
          this.mMinPlayersToStart = value;
        }
      }

      internal RealtimeManager Manager()
      {
        return this.mManager;
      }

      internal bool IsActive()
      {
        return this.mState.IsActive();
      }

      internal string SelfPlayerId()
      {
        return this.mCurrentPlayerId;
      }

      public void SetInvitation(Invitation invitation)
      {
        this.mInvitation = invitation;
      }

      public Invitation GetInvitation()
      {
        return this.mInvitation;
      }

      internal NativeRealtimeMultiplayerClient.OnGameThreadForwardingListener OnGameThreadListener()
      {
        return this.mListener;
      }

      internal void EnterState(NativeRealtimeMultiplayerClient.State handler)
      {
        lock (this.mLifecycleLock)
        {
          this.mState = Misc.CheckNotNull<NativeRealtimeMultiplayerClient.State>(handler);
          Logger.d("Entering state: " + handler.GetType().Name);
          this.mState.OnStateEntered();
        }
      }

      internal void LeaveRoom()
      {
        if (!this.ShowingUI)
        {
          lock (this.mLifecycleLock)
            this.mState.LeaveRoom();
        }
        else
          Logger.d("Not leaving room since showing UI");
      }

      internal void ShowWaitingRoomUI()
      {
        this.mState.ShowWaitingRoomUI(this.MinPlayersToStart);
      }

      internal void StartRoomCreation(string currentPlayerId, Action createRoom)
      {
        lock (this.mLifecycleLock)
        {
          if (!this.mStillPreRoomCreation)
            Logger.e("Room creation started more than once, this shouldn't happen!");
          else if (!this.mState.IsActive())
          {
            Logger.w("Received an attempt to create a room after the session was already torn down!");
          }
          else
          {
            this.mCurrentPlayerId = Misc.CheckNotNull<string>(currentPlayerId);
            this.mStillPreRoomCreation = false;
            this.EnterState((NativeRealtimeMultiplayerClient.State) new NativeRealtimeMultiplayerClient.RoomCreationPendingState(this));
            createRoom();
          }
        }
      }

      internal void OnRoomStatusChanged(NativeRealTimeRoom room)
      {
        lock (this.mLifecycleLock)
          this.mState.OnRoomStatusChanged(room);
      }

      internal void OnConnectedSetChanged(NativeRealTimeRoom room)
      {
        lock (this.mLifecycleLock)
          this.mState.OnConnectedSetChanged(room);
      }

      internal void OnParticipantStatusChanged(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant)
      {
        lock (this.mLifecycleLock)
          this.mState.OnParticipantStatusChanged(room, participant);
      }

      internal void HandleRoomResponse(RealtimeManager.RealTimeRoomResponse response)
      {
        lock (this.mLifecycleLock)
          this.mState.HandleRoomResponse(response);
      }

      internal void OnDataReceived(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant sender, byte[] data, bool isReliable)
      {
        this.mState.OnDataReceived(room, sender, data, isReliable);
      }

      internal void SendMessageToAll(bool reliable, byte[] data)
      {
        this.SendMessageToAll(reliable, data, 0, data.Length);
      }

      internal void SendMessageToAll(bool reliable, byte[] data, int offset, int length)
      {
        this.mState.SendToAll(data, offset, length, reliable);
      }

      internal void SendMessage(bool reliable, string participantId, byte[] data)
      {
        this.SendMessage(reliable, participantId, data, 0, data.Length);
      }

      internal void SendMessage(bool reliable, string participantId, byte[] data, int offset, int length)
      {
        this.mState.SendToSpecificRecipient(participantId, data, offset, length, reliable);
      }

      internal List<Participant> GetConnectedParticipants()
      {
        return this.mState.GetConnectedParticipants();
      }

      internal virtual Participant GetSelf()
      {
        return this.mState.GetSelf();
      }

      internal virtual Participant GetParticipant(string participantId)
      {
        return this.mState.GetParticipant(participantId);
      }

      internal virtual bool IsRoomConnected()
      {
        return this.mState.IsRoomConnected();
      }
    }

    private class OnGameThreadForwardingListener
    {
      private readonly RealTimeMultiplayerListener mListener;

      internal OnGameThreadForwardingListener(RealTimeMultiplayerListener listener)
      {
        this.mListener = Misc.CheckNotNull<RealTimeMultiplayerListener>(listener);
      }

      public void RoomSetupProgress(float percent)
      {
        PlayGamesHelperObject.RunOnGameThread((Action) (() => this.mListener.OnRoomSetupProgress(percent)));
      }

      public void RoomConnected(bool success)
      {
        PlayGamesHelperObject.RunOnGameThread((Action) (() => this.mListener.OnRoomConnected(success)));
      }

      public void LeftRoom()
      {
        PlayGamesHelperObject.RunOnGameThread((Action) (() => this.mListener.OnLeftRoom()));
      }

      public void PeersConnected(string[] participantIds)
      {
        PlayGamesHelperObject.RunOnGameThread((Action) (() => this.mListener.OnPeersConnected(participantIds)));
      }

      public void PeersDisconnected(string[] participantIds)
      {
        PlayGamesHelperObject.RunOnGameThread((Action) (() => this.mListener.OnPeersDisconnected(participantIds)));
      }

      public void RealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
      {
        PlayGamesHelperObject.RunOnGameThread((Action) (() => this.mListener.OnRealTimeMessageReceived(isReliable, senderId, data)));
      }

      public void ParticipantLeft(Participant participant)
      {
        PlayGamesHelperObject.RunOnGameThread((Action) (() => this.mListener.OnParticipantLeft(participant)));
      }
    }

    internal abstract class State
    {
      internal virtual void HandleRoomResponse(RealtimeManager.RealTimeRoomResponse response)
      {
        Logger.d(this.GetType().Name + ".HandleRoomResponse: Defaulting to no-op.");
      }

      internal virtual bool IsActive()
      {
        Logger.d(this.GetType().Name + ".IsNonPreemptable: Is preemptable by default.");
        return true;
      }

      internal virtual void LeaveRoom()
      {
        Logger.d(this.GetType().Name + ".LeaveRoom: Defaulting to no-op.");
      }

      internal virtual void ShowWaitingRoomUI(uint minimumParticipantsBeforeStarting)
      {
        Logger.d(this.GetType().Name + ".ShowWaitingRoomUI: Defaulting to no-op.");
      }

      internal virtual void OnStateEntered()
      {
        Logger.d(this.GetType().Name + ".OnStateEntered: Defaulting to no-op.");
      }

      internal virtual void OnRoomStatusChanged(NativeRealTimeRoom room)
      {
        Logger.d(this.GetType().Name + ".OnRoomStatusChanged: Defaulting to no-op.");
      }

      internal virtual void OnConnectedSetChanged(NativeRealTimeRoom room)
      {
        Logger.d(this.GetType().Name + ".OnConnectedSetChanged: Defaulting to no-op.");
      }

      internal virtual void OnParticipantStatusChanged(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant)
      {
        Logger.d(this.GetType().Name + ".OnParticipantStatusChanged: Defaulting to no-op.");
      }

      internal virtual void OnDataReceived(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant sender, byte[] data, bool isReliable)
      {
        Logger.d(this.GetType().Name + ".OnDataReceived: Defaulting to no-op.");
      }

      internal virtual void SendToSpecificRecipient(string recipientId, byte[] data, int offset, int length, bool isReliable)
      {
        Logger.d(this.GetType().Name + ".SendToSpecificRecipient: Defaulting to no-op.");
      }

      internal virtual void SendToAll(byte[] data, int offset, int length, bool isReliable)
      {
        Logger.d(this.GetType().Name + ".SendToApp: Defaulting to no-op.");
      }

      internal virtual List<Participant> GetConnectedParticipants()
      {
        Logger.d(this.GetType().Name + ".GetConnectedParticipants: Returning empty connected participants");
        return new List<Participant>();
      }

      internal virtual Participant GetSelf()
      {
        Logger.d(this.GetType().Name + ".GetSelf: Returning null self.");
        return (Participant) null;
      }

      internal virtual Participant GetParticipant(string participantId)
      {
        Logger.d(this.GetType().Name + ".GetSelf: Returning null participant.");
        return (Participant) null;
      }

      internal virtual bool IsRoomConnected()
      {
        Logger.d(this.GetType().Name + ".IsRoomConnected: Returning room not connected.");
        return false;
      }
    }

    private abstract class MessagingEnabledState : NativeRealtimeMultiplayerClient.State
    {
      protected readonly NativeRealtimeMultiplayerClient.RoomSession mSession;
      protected NativeRealTimeRoom mRoom;
      protected Dictionary<string, GooglePlayGames.Native.PInvoke.MultiplayerParticipant> mNativeParticipants;
      protected Dictionary<string, Participant> mParticipants;

      internal MessagingEnabledState(NativeRealtimeMultiplayerClient.RoomSession session, NativeRealTimeRoom room)
      {
        this.mSession = Misc.CheckNotNull<NativeRealtimeMultiplayerClient.RoomSession>(session);
        this.UpdateCurrentRoom(room);
      }

      internal void UpdateCurrentRoom(NativeRealTimeRoom room)
      {
        if (this.mRoom != null)
          this.mRoom.Dispose();
        this.mRoom = Misc.CheckNotNull<NativeRealTimeRoom>(room);
        this.mNativeParticipants = this.mRoom.Participants().ToDictionary<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, string>((Func<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, string>) (p => p.Id()));
        this.mParticipants = this.mNativeParticipants.Values.Select<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, Participant>((Func<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, Participant>) (p => p.AsParticipant())).ToDictionary<Participant, string>((Func<Participant, string>) (p => p.ParticipantId));
      }

      internal override sealed void OnRoomStatusChanged(NativeRealTimeRoom room)
      {
        this.HandleRoomStatusChanged(room);
        this.UpdateCurrentRoom(room);
      }

      internal virtual void HandleRoomStatusChanged(NativeRealTimeRoom room)
      {
      }

      internal override sealed void OnConnectedSetChanged(NativeRealTimeRoom room)
      {
        this.HandleConnectedSetChanged(room);
        this.UpdateCurrentRoom(room);
      }

      internal virtual void HandleConnectedSetChanged(NativeRealTimeRoom room)
      {
      }

      internal override sealed void OnParticipantStatusChanged(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant)
      {
        this.HandleParticipantStatusChanged(room, participant);
        this.UpdateCurrentRoom(room);
      }

      internal virtual void HandleParticipantStatusChanged(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant)
      {
      }

      internal override sealed List<Participant> GetConnectedParticipants()
      {
        List<Participant> list = this.mParticipants.Values.Where<Participant>((Func<Participant, bool>) (p => p.IsConnectedToRoom)).ToList<Participant>();
        list.Sort();
        return list;
      }

      internal override void SendToSpecificRecipient(string recipientId, byte[] data, int offset, int length, bool isReliable)
      {
        if (!this.mNativeParticipants.ContainsKey(recipientId))
          Logger.e("Attempted to send message to unknown participant " + recipientId);
        else if (isReliable)
          this.mSession.Manager().SendReliableMessage(this.mRoom, this.mNativeParticipants[recipientId], Misc.GetSubsetBytes(data, offset, length), (Action<CommonErrorStatus.MultiplayerStatus>) null);
        else
          this.mSession.Manager().SendUnreliableMessageToSpecificParticipants(this.mRoom, new List<GooglePlayGames.Native.PInvoke.MultiplayerParticipant>()
          {
            this.mNativeParticipants[recipientId]
          }, Misc.GetSubsetBytes(data, offset, length));
      }

      internal override void SendToAll(byte[] data, int offset, int length, bool isReliable)
      {
        byte[] subsetBytes = Misc.GetSubsetBytes(data, offset, length);
        if (isReliable)
        {
          using (Dictionary<string, GooglePlayGames.Native.PInvoke.MultiplayerParticipant>.KeyCollection.Enumerator enumerator = this.mNativeParticipants.Keys.GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.SendToSpecificRecipient(enumerator.Current, subsetBytes, 0, subsetBytes.Length, true);
          }
        }
        else
          this.mSession.Manager().SendUnreliableMessageToAll(this.mRoom, subsetBytes);
      }

      internal override void OnDataReceived(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant sender, byte[] data, bool isReliable)
      {
        this.mSession.OnGameThreadListener().RealTimeMessageReceived(isReliable, sender.Id(), data);
      }
    }

    private class BeforeRoomCreateStartedState : NativeRealtimeMultiplayerClient.State
    {
      private readonly NativeRealtimeMultiplayerClient.RoomSession mContainingSession;

      internal BeforeRoomCreateStartedState(NativeRealtimeMultiplayerClient.RoomSession session)
      {
        this.mContainingSession = Misc.CheckNotNull<NativeRealtimeMultiplayerClient.RoomSession>(session);
      }

      internal override void LeaveRoom()
      {
        Logger.d("Session was torn down before room was created.");
        this.mContainingSession.OnGameThreadListener().RoomConnected(false);
        this.mContainingSession.EnterState((NativeRealtimeMultiplayerClient.State) new NativeRealtimeMultiplayerClient.ShutdownState(this.mContainingSession));
      }
    }

    private class RoomCreationPendingState : NativeRealtimeMultiplayerClient.State
    {
      private readonly NativeRealtimeMultiplayerClient.RoomSession mContainingSession;

      internal RoomCreationPendingState(NativeRealtimeMultiplayerClient.RoomSession session)
      {
        this.mContainingSession = Misc.CheckNotNull<NativeRealtimeMultiplayerClient.RoomSession>(session);
      }

      internal override void HandleRoomResponse(RealtimeManager.RealTimeRoomResponse response)
      {
        if (!response.RequestSucceeded())
        {
          this.mContainingSession.EnterState((NativeRealtimeMultiplayerClient.State) new NativeRealtimeMultiplayerClient.ShutdownState(this.mContainingSession));
          this.mContainingSession.OnGameThreadListener().RoomConnected(false);
        }
        else
          this.mContainingSession.EnterState((NativeRealtimeMultiplayerClient.State) new NativeRealtimeMultiplayerClient.ConnectingState(response.Room(), this.mContainingSession));
      }

      internal override bool IsActive()
      {
        return true;
      }

      internal override void LeaveRoom()
      {
        Logger.d("Received request to leave room during room creation, aborting creation.");
        this.mContainingSession.EnterState((NativeRealtimeMultiplayerClient.State) new NativeRealtimeMultiplayerClient.AbortingRoomCreationState(this.mContainingSession));
      }
    }

    private class ConnectingState : NativeRealtimeMultiplayerClient.MessagingEnabledState
    {
      private static readonly HashSet<Types.ParticipantStatus> FailedStatuses = new HashSet<Types.ParticipantStatus>() { Types.ParticipantStatus.DECLINED, Types.ParticipantStatus.LEFT };
      private HashSet<string> mConnectedParticipants = new HashSet<string>();
      private float mPercentComplete = 20f;
      private const float InitialPercentComplete = 20f;
      private float mPercentPerParticipant;

      internal ConnectingState(NativeRealTimeRoom room, NativeRealtimeMultiplayerClient.RoomSession session)
        : base(session, room)
      {
        this.mPercentPerParticipant = 80f / (float) session.MinPlayersToStart;
      }

      internal override void OnStateEntered()
      {
        this.mSession.OnGameThreadListener().RoomSetupProgress(this.mPercentComplete);
      }

      internal override void HandleConnectedSetChanged(NativeRealTimeRoom room)
      {
        HashSet<string> first = new HashSet<string>();
        if ((room.Status() == Types.RealTimeRoomStatus.AUTO_MATCHING || room.Status() == Types.RealTimeRoomStatus.CONNECTING) && this.mSession.MinPlayersToStart <= room.ParticipantCount())
        {
          this.mSession.MinPlayersToStart += room.ParticipantCount();
          this.mPercentPerParticipant = 80f / (float) this.mSession.MinPlayersToStart;
        }
        foreach (GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant in room.Participants())
        {
          using (participant)
          {
            if (participant.IsConnectedToRoom())
              first.Add(participant.Id());
          }
        }
        if (this.mConnectedParticipants.Equals((object) first))
        {
          Logger.w("Received connected set callback with unchanged connected set!");
        }
        else
        {
          IEnumerable<string> source1 = this.mConnectedParticipants.Except<string>((IEnumerable<string>) first);
          if (room.Status() == Types.RealTimeRoomStatus.DELETED)
          {
            Logger.e("Participants disconnected during room setup, failing. Participants were: " + string.Join(",", source1.ToArray<string>()));
            this.mSession.OnGameThreadListener().RoomConnected(false);
            this.mSession.EnterState((NativeRealtimeMultiplayerClient.State) new NativeRealtimeMultiplayerClient.ShutdownState(this.mSession));
          }
          else
          {
            IEnumerable<string> source2 = first.Except<string>((IEnumerable<string>) this.mConnectedParticipants);
            Logger.d("New participants connected: " + string.Join(",", source2.ToArray<string>()));
            if (room.Status() == Types.RealTimeRoomStatus.ACTIVE)
            {
              Logger.d("Fully connected! Transitioning to active state.");
              this.mSession.EnterState((NativeRealtimeMultiplayerClient.State) new NativeRealtimeMultiplayerClient.ActiveState(room, this.mSession));
              this.mSession.OnGameThreadListener().RoomConnected(true);
            }
            else
            {
              this.mPercentComplete += this.mPercentPerParticipant * (float) source2.Count<string>();
              this.mConnectedParticipants = first;
              this.mSession.OnGameThreadListener().RoomSetupProgress(this.mPercentComplete);
            }
          }
        }
      }

      internal override void HandleParticipantStatusChanged(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant)
      {
        if (!NativeRealtimeMultiplayerClient.ConnectingState.FailedStatuses.Contains(participant.Status()))
          return;
        this.mSession.OnGameThreadListener().ParticipantLeft(participant.AsParticipant());
        if (room.Status() == Types.RealTimeRoomStatus.CONNECTING || room.Status() == Types.RealTimeRoomStatus.AUTO_MATCHING)
          return;
        this.LeaveRoom();
      }

      internal override void LeaveRoom()
      {
        this.mSession.EnterState((NativeRealtimeMultiplayerClient.State) new NativeRealtimeMultiplayerClient.LeavingRoom(this.mSession, this.mRoom, (Action) (() => this.mSession.OnGameThreadListener().RoomConnected(false))));
      }

      internal override void ShowWaitingRoomUI(uint minimumParticipantsBeforeStarting)
      {
        this.mSession.ShowingUI = true;
        this.mSession.Manager().ShowWaitingRoomUI(this.mRoom, minimumParticipantsBeforeStarting, (Action<RealtimeManager.WaitingRoomUIResponse>) (response =>
        {
          this.mSession.ShowingUI = false;
          Logger.d("ShowWaitingRoomUI Response: " + (object) response.ResponseStatus());
          if (response.ResponseStatus() == CommonErrorStatus.UIStatus.VALID)
          {
            Logger.d("Connecting state ShowWaitingRoomUI: room pcount:" + (object) response.Room().ParticipantCount() + " status: " + (object) response.Room().Status());
            if (response.Room().Status() != Types.RealTimeRoomStatus.ACTIVE)
              return;
            this.mSession.EnterState((NativeRealtimeMultiplayerClient.State) new NativeRealtimeMultiplayerClient.ActiveState(response.Room(), this.mSession));
          }
          else if (response.ResponseStatus() == CommonErrorStatus.UIStatus.ERROR_LEFT_ROOM)
            this.LeaveRoom();
          else
            this.mSession.OnGameThreadListener().RoomSetupProgress(this.mPercentComplete);
        }));
      }
    }

    private class ActiveState : NativeRealtimeMultiplayerClient.MessagingEnabledState
    {
      internal ActiveState(NativeRealTimeRoom room, NativeRealtimeMultiplayerClient.RoomSession session)
        : base(session, room)
      {
      }

      internal override void OnStateEntered()
      {
        if (this.GetSelf() != null)
          return;
        Logger.e("Room reached active state with unknown participant for the player");
        this.LeaveRoom();
      }

      internal override bool IsRoomConnected()
      {
        return true;
      }

      internal override Participant GetParticipant(string participantId)
      {
        if (this.mParticipants.ContainsKey(participantId))
          return this.mParticipants[participantId];
        Logger.e("Attempted to retrieve unknown participant " + participantId);
        return (Participant) null;
      }

      internal override Participant GetSelf()
      {
        using (Dictionary<string, Participant>.ValueCollection.Enumerator enumerator = this.mParticipants.Values.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            Participant current = enumerator.Current;
            if (current.Player != null && current.Player.id.Equals(this.mSession.SelfPlayerId()))
              return current;
          }
        }
        return (Participant) null;
      }

      internal override void HandleConnectedSetChanged(NativeRealTimeRoom room)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        NativeRealtimeMultiplayerClient.ActiveState.\u003CHandleConnectedSetChanged\u003Ec__AnonStorey1B2 changedCAnonStorey1B2 = new NativeRealtimeMultiplayerClient.ActiveState.\u003CHandleConnectedSetChanged\u003Ec__AnonStorey1B2();
        List<string> source1 = new List<string>();
        List<string> source2 = new List<string>();
        Dictionary<string, GooglePlayGames.Native.PInvoke.MultiplayerParticipant> dictionary = room.Participants().ToDictionary<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, string>((Func<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, string>) (p => p.Id()));
        using (Dictionary<string, GooglePlayGames.Native.PInvoke.MultiplayerParticipant>.KeyCollection.Enumerator enumerator = this.mNativeParticipants.Keys.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            string current = enumerator.Current;
            GooglePlayGames.Native.PInvoke.MultiplayerParticipant multiplayerParticipant = dictionary[current];
            GooglePlayGames.Native.PInvoke.MultiplayerParticipant nativeParticipant = this.mNativeParticipants[current];
            if (!multiplayerParticipant.IsConnectedToRoom())
              source2.Add(current);
            if (!nativeParticipant.IsConnectedToRoom() && multiplayerParticipant.IsConnectedToRoom())
              source1.Add(current);
          }
        }
        using (Dictionary<string, GooglePlayGames.Native.PInvoke.MultiplayerParticipant>.ValueCollection.Enumerator enumerator = this.mNativeParticipants.Values.GetEnumerator())
        {
          while (enumerator.MoveNext())
            enumerator.Current.Dispose();
        }
        this.mNativeParticipants = dictionary;
        this.mParticipants = this.mNativeParticipants.Values.Select<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, Participant>((Func<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, Participant>) (p => p.AsParticipant())).ToDictionary<Participant, string>((Func<Participant, string>) (p => p.ParticipantId));
        Logger.d("Updated participant statuses: " + string.Join(",", this.mParticipants.Values.Select<Participant, string>((Func<Participant, string>) (p => p.ToString())).ToArray<string>()));
        if (source2.Contains(this.GetSelf().ParticipantId))
          Logger.w("Player was disconnected from the multiplayer session.");
        // ISSUE: reference to a compiler-generated field
        changedCAnonStorey1B2.selfId = this.GetSelf().ParticipantId;
        // ISSUE: reference to a compiler-generated method
        List<string> list1 = source1.Where<string>(new Func<string, bool>(changedCAnonStorey1B2.\u003C\u003Em__A7)).ToList<string>();
        // ISSUE: reference to a compiler-generated method
        List<string> list2 = source2.Where<string>(new Func<string, bool>(changedCAnonStorey1B2.\u003C\u003Em__A8)).ToList<string>();
        if (list1.Count > 0)
        {
          list1.Sort();
          // ISSUE: reference to a compiler-generated method
          this.mSession.OnGameThreadListener().PeersConnected(list1.Where<string>(new Func<string, bool>(changedCAnonStorey1B2.\u003C\u003Em__A9)).ToArray<string>());
        }
        if (list2.Count <= 0)
          return;
        list2.Sort();
        // ISSUE: reference to a compiler-generated method
        this.mSession.OnGameThreadListener().PeersDisconnected(list2.Where<string>(new Func<string, bool>(changedCAnonStorey1B2.\u003C\u003Em__AA)).ToArray<string>());
      }

      internal override void LeaveRoom()
      {
        this.mSession.EnterState((NativeRealtimeMultiplayerClient.State) new NativeRealtimeMultiplayerClient.LeavingRoom(this.mSession, this.mRoom, (Action) (() => this.mSession.OnGameThreadListener().LeftRoom())));
      }
    }

    private class ShutdownState : NativeRealtimeMultiplayerClient.State
    {
      private readonly NativeRealtimeMultiplayerClient.RoomSession mSession;

      internal ShutdownState(NativeRealtimeMultiplayerClient.RoomSession session)
      {
        this.mSession = Misc.CheckNotNull<NativeRealtimeMultiplayerClient.RoomSession>(session);
      }

      internal override bool IsActive()
      {
        return false;
      }

      internal override void LeaveRoom()
      {
        this.mSession.OnGameThreadListener().LeftRoom();
      }
    }

    private class LeavingRoom : NativeRealtimeMultiplayerClient.State
    {
      private readonly NativeRealtimeMultiplayerClient.RoomSession mSession;
      private readonly NativeRealTimeRoom mRoomToLeave;
      private readonly Action mLeavingCompleteCallback;

      internal LeavingRoom(NativeRealtimeMultiplayerClient.RoomSession session, NativeRealTimeRoom room, Action leavingCompleteCallback)
      {
        this.mSession = Misc.CheckNotNull<NativeRealtimeMultiplayerClient.RoomSession>(session);
        this.mRoomToLeave = Misc.CheckNotNull<NativeRealTimeRoom>(room);
        this.mLeavingCompleteCallback = Misc.CheckNotNull<Action>(leavingCompleteCallback);
      }

      internal override bool IsActive()
      {
        return false;
      }

      internal override void OnStateEntered()
      {
        this.mSession.Manager().LeaveRoom(this.mRoomToLeave, (Action<CommonErrorStatus.ResponseStatus>) (status =>
        {
          this.mLeavingCompleteCallback();
          this.mSession.EnterState((NativeRealtimeMultiplayerClient.State) new NativeRealtimeMultiplayerClient.ShutdownState(this.mSession));
        }));
      }
    }

    private class AbortingRoomCreationState : NativeRealtimeMultiplayerClient.State
    {
      private readonly NativeRealtimeMultiplayerClient.RoomSession mSession;

      internal AbortingRoomCreationState(NativeRealtimeMultiplayerClient.RoomSession session)
      {
        this.mSession = Misc.CheckNotNull<NativeRealtimeMultiplayerClient.RoomSession>(session);
      }

      internal override bool IsActive()
      {
        return false;
      }

      internal override void HandleRoomResponse(RealtimeManager.RealTimeRoomResponse response)
      {
        if (!response.RequestSucceeded())
        {
          this.mSession.EnterState((NativeRealtimeMultiplayerClient.State) new NativeRealtimeMultiplayerClient.ShutdownState(this.mSession));
          this.mSession.OnGameThreadListener().RoomConnected(false);
        }
        else
          this.mSession.EnterState((NativeRealtimeMultiplayerClient.State) new NativeRealtimeMultiplayerClient.LeavingRoom(this.mSession, response.Room(), (Action) (() => this.mSession.OnGameThreadListener().RoomConnected(false))));
      }
    }
  }
}
