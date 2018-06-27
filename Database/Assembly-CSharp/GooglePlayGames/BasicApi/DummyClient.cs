// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.DummyClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.Events;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.BasicApi.Quests;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.OurUtils;
using System;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames.BasicApi
{
  public class DummyClient : IPlayGamesClient
  {
    public void Authenticate(Action<bool> callback, bool silent)
    {
      DummyClient.LogUsage();
      if (callback == null)
        return;
      callback(false);
    }

    public bool IsAuthenticated()
    {
      DummyClient.LogUsage();
      return false;
    }

    public void SignOut()
    {
      DummyClient.LogUsage();
    }

    public string GetAccessToken()
    {
      DummyClient.LogUsage();
      return "DummyAccessToken";
    }

    public string GetIdToken()
    {
      DummyClient.LogUsage();
      return "DummyIdToken";
    }

    public string GetUserId()
    {
      DummyClient.LogUsage();
      return "DummyID";
    }

    public string GetToken()
    {
      return "DummyToken";
    }

    public string GetUserEmail()
    {
      return string.Empty;
    }

    public void GetPlayerStats(Action<CommonStatusCodes, PlayGamesLocalUser.PlayerStats> callback)
    {
      DummyClient.LogUsage();
      callback(CommonStatusCodes.ApiNotConnected, new PlayGamesLocalUser.PlayerStats());
    }

    public string GetUserDisplayName()
    {
      DummyClient.LogUsage();
      return "Player";
    }

    public string GetUserImageUrl()
    {
      DummyClient.LogUsage();
      return (string) null;
    }

    public void LoadUsers(string[] userIds, Action<IUserProfile[]> callback)
    {
      DummyClient.LogUsage();
      if (callback == null)
        return;
      callback((IUserProfile[]) null);
    }

    public void LoadAchievements(Action<Achievement[]> callback)
    {
      DummyClient.LogUsage();
      if (callback == null)
        return;
      callback((Achievement[]) null);
    }

    public Achievement GetAchievement(string achId)
    {
      DummyClient.LogUsage();
      return (Achievement) null;
    }

    public void UnlockAchievement(string achId, Action<bool> callback)
    {
      DummyClient.LogUsage();
      if (callback == null)
        return;
      callback(false);
    }

    public void RevealAchievement(string achId, Action<bool> callback)
    {
      DummyClient.LogUsage();
      if (callback == null)
        return;
      callback(false);
    }

    public void IncrementAchievement(string achId, int steps, Action<bool> callback)
    {
      DummyClient.LogUsage();
      if (callback == null)
        return;
      callback(false);
    }

    public void SetStepsAtLeast(string achId, int steps, Action<bool> callback)
    {
      DummyClient.LogUsage();
      if (callback == null)
        return;
      callback(false);
    }

    public void ShowAchievementsUI(Action<UIStatus> callback)
    {
      DummyClient.LogUsage();
      if (callback == null)
        return;
      callback(UIStatus.VersionUpdateRequired);
    }

    public void ShowLeaderboardUI(string lbId, LeaderboardTimeSpan span, Action<UIStatus> callback)
    {
      DummyClient.LogUsage();
      if (callback == null)
        return;
      callback(UIStatus.VersionUpdateRequired);
    }

    public int LeaderboardMaxResults()
    {
      return 25;
    }

    public void LoadScores(string leaderboardId, LeaderboardStart start, int rowCount, LeaderboardCollection collection, LeaderboardTimeSpan timeSpan, Action<LeaderboardScoreData> callback)
    {
      DummyClient.LogUsage();
      if (callback == null)
        return;
      callback(new LeaderboardScoreData(leaderboardId, ResponseStatus.LicenseCheckFailed));
    }

    public void LoadMoreScores(ScorePageToken token, int rowCount, Action<LeaderboardScoreData> callback)
    {
      DummyClient.LogUsage();
      if (callback == null)
        return;
      callback(new LeaderboardScoreData(token.LeaderboardId, ResponseStatus.LicenseCheckFailed));
    }

    public void SubmitScore(string lbId, long score, Action<bool> callback)
    {
      DummyClient.LogUsage();
      if (callback == null)
        return;
      callback(false);
    }

    public void SubmitScore(string lbId, long score, string metadata, Action<bool> callback)
    {
      DummyClient.LogUsage();
      if (callback == null)
        return;
      callback(false);
    }

    public IRealTimeMultiplayerClient GetRtmpClient()
    {
      DummyClient.LogUsage();
      return (IRealTimeMultiplayerClient) null;
    }

    public ITurnBasedMultiplayerClient GetTbmpClient()
    {
      DummyClient.LogUsage();
      return (ITurnBasedMultiplayerClient) null;
    }

    public ISavedGameClient GetSavedGameClient()
    {
      DummyClient.LogUsage();
      return (ISavedGameClient) null;
    }

    public IEventsClient GetEventsClient()
    {
      DummyClient.LogUsage();
      return (IEventsClient) null;
    }

    public IQuestsClient GetQuestsClient()
    {
      DummyClient.LogUsage();
      return (IQuestsClient) null;
    }

    public void RegisterInvitationDelegate(InvitationReceivedDelegate deleg)
    {
      DummyClient.LogUsage();
    }

    public Invitation GetInvitationFromNotification()
    {
      DummyClient.LogUsage();
      return (Invitation) null;
    }

    public bool HasInvitationFromNotification()
    {
      DummyClient.LogUsage();
      return false;
    }

    public void LoadFriends(Action<bool> callback)
    {
      DummyClient.LogUsage();
      callback(false);
    }

    public IUserProfile[] GetFriends()
    {
      DummyClient.LogUsage();
      return new IUserProfile[0];
    }

    public IntPtr GetApiClient()
    {
      DummyClient.LogUsage();
      return IntPtr.Zero;
    }

    private static void LogUsage()
    {
      Logger.d("Received method call on DummyClient - using stub implementation.");
    }
  }
}
