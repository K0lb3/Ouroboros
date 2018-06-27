// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.NativeTurnBasedMultiplayerClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace GooglePlayGames.Native
{
  public class NativeTurnBasedMultiplayerClient : ITurnBasedMultiplayerClient
  {
    private readonly TurnBasedManager mTurnBasedManager;
    private readonly NativeClient mNativeClient;
    private volatile Action<GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch, bool> mMatchDelegate;

    internal NativeTurnBasedMultiplayerClient(NativeClient nativeClient, TurnBasedManager manager)
    {
      this.mTurnBasedManager = manager;
      this.mNativeClient = nativeClient;
    }

    public void CreateQuickMatch(uint minOpponents, uint maxOpponents, uint variant, Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
    {
      this.CreateQuickMatch(minOpponents, maxOpponents, variant, 0UL, callback);
    }

    public void CreateQuickMatch(uint minOpponents, uint maxOpponents, uint variant, ulong exclusiveBitmask, Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
    {
      callback = Callbacks.AsOnGameThreadCallback<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>(callback);
      using (GooglePlayGames.Native.PInvoke.TurnBasedMatchConfigBuilder matchConfigBuilder = GooglePlayGames.Native.PInvoke.TurnBasedMatchConfigBuilder.Create())
      {
        matchConfigBuilder.SetVariant(variant).SetMinimumAutomatchingPlayers(minOpponents).SetMaximumAutomatchingPlayers(maxOpponents).SetExclusiveBitMask(exclusiveBitmask);
        using (GooglePlayGames.Native.PInvoke.TurnBasedMatchConfig config = matchConfigBuilder.Build())
          this.mTurnBasedManager.CreateMatch(config, this.BridgeMatchToUserCallback((Action<GooglePlayGames.BasicApi.UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>) ((status, match) => callback(status == GooglePlayGames.BasicApi.UIStatus.Valid, match))));
      }
    }

    public void CreateWithInvitationScreen(uint minOpponents, uint maxOpponents, uint variant, Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
    {
      this.CreateWithInvitationScreen(minOpponents, maxOpponents, variant, (Action<GooglePlayGames.BasicApi.UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>) ((status, match) => callback(status == GooglePlayGames.BasicApi.UIStatus.Valid, match)));
    }

    public void CreateWithInvitationScreen(uint minOpponents, uint maxOpponents, uint variant, Action<GooglePlayGames.BasicApi.UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
    {
      callback = Callbacks.AsOnGameThreadCallback<GooglePlayGames.BasicApi.UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>(callback);
      this.mTurnBasedManager.ShowPlayerSelectUI(minOpponents, maxOpponents, true, (Action<PlayerSelectUIResponse>) (result =>
      {
        if (result.Status() != CommonErrorStatus.UIStatus.VALID)
          callback((GooglePlayGames.BasicApi.UIStatus) result.Status(), (GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch) null);
        using (GooglePlayGames.Native.PInvoke.TurnBasedMatchConfigBuilder matchConfigBuilder = GooglePlayGames.Native.PInvoke.TurnBasedMatchConfigBuilder.Create())
        {
          matchConfigBuilder.PopulateFromUIResponse(result).SetVariant(variant);
          using (GooglePlayGames.Native.PInvoke.TurnBasedMatchConfig config = matchConfigBuilder.Build())
            this.mTurnBasedManager.CreateMatch(config, this.BridgeMatchToUserCallback(callback));
        }
      }));
    }

    public void GetAllInvitations(Action<Invitation[]> callback)
    {
      this.mTurnBasedManager.GetAllTurnbasedMatches((Action<TurnBasedManager.TurnBasedMatchesResponse>) (allMatches =>
      {
        Invitation[] invitationArray = new Invitation[allMatches.InvitationCount()];
        int num = 0;
        foreach (GooglePlayGames.Native.PInvoke.MultiplayerInvitation invitation in allMatches.Invitations())
          invitationArray[num++] = invitation.AsInvitation();
        callback(invitationArray);
      }));
    }

    public void GetAllMatches(Action<GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch[]> callback)
    {
      this.mTurnBasedManager.GetAllTurnbasedMatches((Action<TurnBasedManager.TurnBasedMatchesResponse>) (allMatches =>
      {
        GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch[] turnBasedMatchArray = new GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch[allMatches.MyTurnMatchesCount() + allMatches.TheirTurnMatchesCount() + allMatches.CompletedMatchesCount()];
        int num = 0;
        foreach (NativeTurnBasedMatch myTurnMatch in allMatches.MyTurnMatches())
          turnBasedMatchArray[num++] = myTurnMatch.AsTurnBasedMatch(this.mNativeClient.GetUserId());
        foreach (NativeTurnBasedMatch theirTurnMatch in allMatches.TheirTurnMatches())
          turnBasedMatchArray[num++] = theirTurnMatch.AsTurnBasedMatch(this.mNativeClient.GetUserId());
        foreach (NativeTurnBasedMatch completedMatch in allMatches.CompletedMatches())
          turnBasedMatchArray[num++] = completedMatch.AsTurnBasedMatch(this.mNativeClient.GetUserId());
        callback(turnBasedMatchArray);
      }));
    }

    private Action<TurnBasedManager.TurnBasedMatchResponse> BridgeMatchToUserCallback(Action<GooglePlayGames.BasicApi.UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> userCallback)
    {
      return (Action<TurnBasedManager.TurnBasedMatchResponse>) (callbackResult =>
      {
        using (NativeTurnBasedMatch nativeTurnBasedMatch = callbackResult.Match())
        {
          if (nativeTurnBasedMatch == null)
          {
            GooglePlayGames.BasicApi.UIStatus uiStatus = GooglePlayGames.BasicApi.UIStatus.InternalError;
            switch (callbackResult.ResponseStatus() + 5)
            {
              case ~(CommonErrorStatus.MultiplayerStatus.ERROR_INTERNAL | CommonErrorStatus.MultiplayerStatus.VALID):
                uiStatus = GooglePlayGames.BasicApi.UIStatus.Timeout;
                break;
              case CommonErrorStatus.MultiplayerStatus.VALID:
                uiStatus = GooglePlayGames.BasicApi.UIStatus.VersionUpdateRequired;
                break;
              case CommonErrorStatus.MultiplayerStatus.VALID_BUT_STALE:
                uiStatus = GooglePlayGames.BasicApi.UIStatus.NotAuthorized;
                break;
              case CommonErrorStatus.MultiplayerStatus.VALID | CommonErrorStatus.MultiplayerStatus.VALID_BUT_STALE:
                uiStatus = GooglePlayGames.BasicApi.UIStatus.InternalError;
                break;
              case ~CommonErrorStatus.MultiplayerStatus.ERROR_MATCH_ALREADY_REMATCHED:
                uiStatus = GooglePlayGames.BasicApi.UIStatus.Valid;
                break;
              case ~CommonErrorStatus.MultiplayerStatus.ERROR_INACTIVE_MATCH:
                uiStatus = GooglePlayGames.BasicApi.UIStatus.Valid;
                break;
            }
            userCallback(uiStatus, (GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch) null);
          }
          else
          {
            GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch turnBasedMatch = nativeTurnBasedMatch.AsTurnBasedMatch(this.mNativeClient.GetUserId());
            Logger.d("Passing converted match to user callback:" + (object) turnBasedMatch);
            userCallback(GooglePlayGames.BasicApi.UIStatus.Valid, turnBasedMatch);
          }
        }
      });
    }

    public void AcceptFromInbox(Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
    {
      callback = Callbacks.AsOnGameThreadCallback<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>(callback);
      this.mTurnBasedManager.ShowInboxUI((Action<TurnBasedManager.MatchInboxUIResponse>) (callbackResult =>
      {
        using (NativeTurnBasedMatch nativeTurnBasedMatch = callbackResult.Match())
        {
          if (nativeTurnBasedMatch == null)
          {
            callback(false, (GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch) null);
          }
          else
          {
            GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch turnBasedMatch = nativeTurnBasedMatch.AsTurnBasedMatch(this.mNativeClient.GetUserId());
            Logger.d("Passing converted match to user callback:" + (object) turnBasedMatch);
            callback(true, turnBasedMatch);
          }
        }
      }));
    }

    public void AcceptInvitation(string invitationId, Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
    {
      callback = Callbacks.AsOnGameThreadCallback<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>(callback);
      this.FindInvitationWithId(invitationId, (Action<GooglePlayGames.Native.PInvoke.MultiplayerInvitation>) (invitation =>
      {
        if (invitation == null)
        {
          Logger.e("Could not find invitation with id " + invitationId);
          callback(false, (GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch) null);
        }
        else
          this.mTurnBasedManager.AcceptInvitation(invitation, this.BridgeMatchToUserCallback((Action<GooglePlayGames.BasicApi.UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>) ((status, match) => callback(status == GooglePlayGames.BasicApi.UIStatus.Valid, match))));
      }));
    }

    private void FindInvitationWithId(string invitationId, Action<GooglePlayGames.Native.PInvoke.MultiplayerInvitation> callback)
    {
      this.mTurnBasedManager.GetAllTurnbasedMatches((Action<TurnBasedManager.TurnBasedMatchesResponse>) (allMatches =>
      {
        if (allMatches.Status() <= ~(CommonErrorStatus.MultiplayerStatus.ERROR_INTERNAL | CommonErrorStatus.MultiplayerStatus.VALID))
        {
          callback((GooglePlayGames.Native.PInvoke.MultiplayerInvitation) null);
        }
        else
        {
          foreach (GooglePlayGames.Native.PInvoke.MultiplayerInvitation invitation in allMatches.Invitations())
          {
            using (invitation)
            {
              if (invitation.Id().Equals(invitationId))
              {
                callback(invitation);
                return;
              }
            }
          }
          callback((GooglePlayGames.Native.PInvoke.MultiplayerInvitation) null);
        }
      }));
    }

    public void RegisterMatchDelegate(MatchDelegate del)
    {
      if (del == null)
        this.mMatchDelegate = (Action<GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch, bool>) null;
      else
        this.mMatchDelegate = Callbacks.AsOnGameThreadCallback<GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch, bool>((Action<GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch, bool>) ((match, autoLaunch) => del(match, autoLaunch)));
    }

    internal void HandleMatchEvent(Types.MultiplayerEvent eventType, string matchId, NativeTurnBasedMatch match)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      NativeTurnBasedMultiplayerClient.\u003CHandleMatchEvent\u003Ec__AnonStorey1C8 eventCAnonStorey1C8 = new NativeTurnBasedMultiplayerClient.\u003CHandleMatchEvent\u003Ec__AnonStorey1C8();
      // ISSUE: reference to a compiler-generated field
      eventCAnonStorey1C8.match = match;
      // ISSUE: reference to a compiler-generated field
      eventCAnonStorey1C8.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      eventCAnonStorey1C8.currentDelegate = this.mMatchDelegate;
      // ISSUE: reference to a compiler-generated field
      if (eventCAnonStorey1C8.currentDelegate == null)
        return;
      if (eventType == Types.MultiplayerEvent.REMOVED)
      {
        Logger.d("Ignoring REMOVE event for match " + matchId);
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        eventCAnonStorey1C8.shouldAutolaunch = eventType == Types.MultiplayerEvent.UPDATED_FROM_APP_LAUNCH;
        // ISSUE: reference to a compiler-generated field
        eventCAnonStorey1C8.match.ReferToMe();
        // ISSUE: reference to a compiler-generated method
        Callbacks.AsCoroutine(this.WaitForLogin(new Action(eventCAnonStorey1C8.\u003C\u003Em__C7)));
      }
    }

    [DebuggerHidden]
    private IEnumerator WaitForLogin(Action method)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new NativeTurnBasedMultiplayerClient.\u003CWaitForLogin\u003Ec__Iterator25() { method = method, \u003C\u0024\u003Emethod = method, \u003C\u003Ef__this = this };
    }

    public void TakeTurn(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, byte[] data, string pendingParticipantId, Action<bool> callback)
    {
      Logger.describe(data);
      callback = Callbacks.AsOnGameThreadCallback<bool>(callback);
      this.FindEqualVersionMatchWithParticipant(match, pendingParticipantId, callback, (Action<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, NativeTurnBasedMatch>) ((pendingParticipant, foundMatch) => this.mTurnBasedManager.TakeTurn(foundMatch, data, pendingParticipant, (Action<TurnBasedManager.TurnBasedMatchResponse>) (result =>
      {
        if (result.RequestSucceeded())
        {
          callback(true);
        }
        else
        {
          Logger.d("Taking turn failed: " + (object) result.ResponseStatus());
          callback(false);
        }
      }))));
    }

    private void FindEqualVersionMatch(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, Action<bool> onFailure, Action<NativeTurnBasedMatch> onVersionMatch)
    {
      this.mTurnBasedManager.GetMatch(match.MatchId, (Action<TurnBasedManager.TurnBasedMatchResponse>) (response =>
      {
        using (NativeTurnBasedMatch nativeTurnBasedMatch = response.Match())
        {
          if (nativeTurnBasedMatch == null)
          {
            Logger.e(string.Format("Could not find match {0}", (object) match.MatchId));
            onFailure(false);
          }
          else if ((int) nativeTurnBasedMatch.Version() != (int) match.Version)
          {
            Logger.e(string.Format("Attempted to update a stale version of the match. Expected version was {0} but current version is {1}.", (object) match.Version, (object) nativeTurnBasedMatch.Version()));
            onFailure(false);
          }
          else
            onVersionMatch(nativeTurnBasedMatch);
        }
      }));
    }

    private void FindEqualVersionMatchWithParticipant(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, string participantId, Action<bool> onFailure, Action<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, NativeTurnBasedMatch> onFoundParticipantAndMatch)
    {
      this.FindEqualVersionMatch(match, onFailure, (Action<NativeTurnBasedMatch>) (foundMatch =>
      {
        if (participantId == null)
        {
          using (GooglePlayGames.Native.PInvoke.MultiplayerParticipant multiplayerParticipant = GooglePlayGames.Native.PInvoke.MultiplayerParticipant.AutomatchingSentinel())
            onFoundParticipantAndMatch(multiplayerParticipant, foundMatch);
        }
        else
        {
          using (GooglePlayGames.Native.PInvoke.MultiplayerParticipant multiplayerParticipant = foundMatch.ParticipantWithId(participantId))
          {
            if (multiplayerParticipant == null)
            {
              Logger.e(string.Format("Located match {0} but desired participant with ID {1} could not be found", (object) match.MatchId, (object) participantId));
              onFailure(false);
            }
            else
              onFoundParticipantAndMatch(multiplayerParticipant, foundMatch);
          }
        }
      }));
    }

    public int GetMaxMatchDataSize()
    {
      throw new NotImplementedException();
    }

    public void Finish(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, byte[] data, MatchOutcome outcome, Action<bool> callback)
    {
      callback = Callbacks.AsOnGameThreadCallback<bool>(callback);
      this.FindEqualVersionMatch(match, callback, (Action<NativeTurnBasedMatch>) (foundMatch =>
      {
        GooglePlayGames.Native.PInvoke.ParticipantResults results = foundMatch.Results();
        using (List<string>.Enumerator enumerator = outcome.ParticipantIds.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            string current = enumerator.Current;
            Types.MatchResult matchResult1 = NativeTurnBasedMultiplayerClient.ResultToMatchResult(outcome.GetResultFor(current));
            uint placementFor = outcome.GetPlacementFor(current);
            if (results.HasResultsForParticipant(current))
            {
              Types.MatchResult matchResult2 = results.ResultsForParticipant(current);
              uint num = results.PlacingForParticipant(current);
              if (matchResult1 != matchResult2 || (int) placementFor != (int) num)
              {
                Logger.e(string.Format("Attempted to override existing results for participant {0}: Placing {1}, Result {2}", (object) current, (object) num, (object) matchResult2));
                callback(false);
                return;
              }
            }
            else
            {
              GooglePlayGames.Native.PInvoke.ParticipantResults participantResults = results;
              results = participantResults.WithResult(current, placementFor, matchResult1);
              participantResults.Dispose();
            }
          }
        }
        this.mTurnBasedManager.FinishMatchDuringMyTurn(foundMatch, data, results, (Action<TurnBasedManager.TurnBasedMatchResponse>) (response => callback(response.RequestSucceeded())));
      }));
    }

    private static Types.MatchResult ResultToMatchResult(MatchOutcome.ParticipantResult result)
    {
      switch (result)
      {
        case MatchOutcome.ParticipantResult.None:
          return Types.MatchResult.NONE;
        case MatchOutcome.ParticipantResult.Win:
          return Types.MatchResult.WIN;
        case MatchOutcome.ParticipantResult.Loss:
          return Types.MatchResult.LOSS;
        case MatchOutcome.ParticipantResult.Tie:
          return Types.MatchResult.TIE;
        default:
          Logger.e("Received unknown ParticipantResult " + (object) result);
          return Types.MatchResult.NONE;
      }
    }

    public void AcknowledgeFinished(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, Action<bool> callback)
    {
      callback = Callbacks.AsOnGameThreadCallback<bool>(callback);
      this.FindEqualVersionMatch(match, callback, (Action<NativeTurnBasedMatch>) (foundMatch => this.mTurnBasedManager.ConfirmPendingCompletion(foundMatch, (Action<TurnBasedManager.TurnBasedMatchResponse>) (response => callback(response.RequestSucceeded())))));
    }

    public void Leave(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, Action<bool> callback)
    {
      callback = Callbacks.AsOnGameThreadCallback<bool>(callback);
      this.FindEqualVersionMatch(match, callback, (Action<NativeTurnBasedMatch>) (foundMatch => this.mTurnBasedManager.LeaveMatchDuringTheirTurn(foundMatch, (Action<CommonErrorStatus.MultiplayerStatus>) (status => callback(status > ~(CommonErrorStatus.MultiplayerStatus.ERROR_INTERNAL | CommonErrorStatus.MultiplayerStatus.VALID))))));
    }

    public void LeaveDuringTurn(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, string pendingParticipantId, Action<bool> callback)
    {
      callback = Callbacks.AsOnGameThreadCallback<bool>(callback);
      this.FindEqualVersionMatchWithParticipant(match, pendingParticipantId, callback, (Action<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, NativeTurnBasedMatch>) ((pendingParticipant, foundMatch) => this.mTurnBasedManager.LeaveDuringMyTurn(foundMatch, pendingParticipant, (Action<CommonErrorStatus.MultiplayerStatus>) (status => callback(status > ~(CommonErrorStatus.MultiplayerStatus.ERROR_INTERNAL | CommonErrorStatus.MultiplayerStatus.VALID))))));
    }

    public void Cancel(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, Action<bool> callback)
    {
      callback = Callbacks.AsOnGameThreadCallback<bool>(callback);
      this.FindEqualVersionMatch(match, callback, (Action<NativeTurnBasedMatch>) (foundMatch => this.mTurnBasedManager.CancelMatch(foundMatch, (Action<CommonErrorStatus.MultiplayerStatus>) (status => callback(status > ~(CommonErrorStatus.MultiplayerStatus.ERROR_INTERNAL | CommonErrorStatus.MultiplayerStatus.VALID))))));
    }

    public void Rematch(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
    {
      callback = Callbacks.AsOnGameThreadCallback<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>(callback);
      this.FindEqualVersionMatch(match, (Action<bool>) (failed => callback(false, (GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch) null)), (Action<NativeTurnBasedMatch>) (foundMatch => this.mTurnBasedManager.Rematch(foundMatch, this.BridgeMatchToUserCallback((Action<GooglePlayGames.BasicApi.UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>) ((status, m) => callback(status == GooglePlayGames.BasicApi.UIStatus.Valid, m))))));
    }

    public void DeclineInvitation(string invitationId)
    {
      this.FindInvitationWithId(invitationId, (Action<GooglePlayGames.Native.PInvoke.MultiplayerInvitation>) (invitation =>
      {
        if (invitation == null)
          return;
        this.mTurnBasedManager.DeclineInvitation(invitation);
      }));
    }
  }
}
