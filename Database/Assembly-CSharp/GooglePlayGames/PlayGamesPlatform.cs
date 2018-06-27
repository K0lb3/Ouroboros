// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.PlayGamesPlatform
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Events;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.BasicApi.Nearby;
using GooglePlayGames.BasicApi.Quests;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
  public class PlayGamesPlatform : ISocialPlatform
  {
    private Dictionary<string, string> mIdMap = new Dictionary<string, string>();
    private static volatile PlayGamesPlatform sInstance;
    private readonly PlayGamesClientConfiguration mConfiguration;
    private PlayGamesLocalUser mLocalUser;
    private IPlayGamesClient mClient;
    private static volatile bool sNearbyInitializePending;
    private static volatile INearbyConnectionClient sNearbyConnectionClient;
    private string mDefaultLbUi;

    private PlayGamesPlatform(PlayGamesClientConfiguration configuration)
    {
      this.mLocalUser = new PlayGamesLocalUser(this);
      this.mConfiguration = configuration;
    }

    internal PlayGamesPlatform(IPlayGamesClient client)
    {
      this.mClient = Misc.CheckNotNull<IPlayGamesClient>(client);
      this.mLocalUser = new PlayGamesLocalUser(this);
      this.mConfiguration = PlayGamesClientConfiguration.DefaultConfiguration;
    }

    public static void InitializeInstance(PlayGamesClientConfiguration configuration)
    {
      if (PlayGamesPlatform.sInstance != null)
        Logger.w("PlayGamesPlatform already initialized. Ignoring this call.");
      else
        PlayGamesPlatform.sInstance = new PlayGamesPlatform(configuration);
    }

    public static PlayGamesPlatform Instance
    {
      get
      {
        if (PlayGamesPlatform.sInstance == null)
        {
          Logger.d("Instance was not initialized, using default configuration.");
          PlayGamesPlatform.InitializeInstance(PlayGamesClientConfiguration.DefaultConfiguration);
        }
        return PlayGamesPlatform.sInstance;
      }
    }

    public static void InitializeNearby(Action<INearbyConnectionClient> callback)
    {
      Debug.Log((object) "Calling InitializeNearby!");
      if (PlayGamesPlatform.sNearbyConnectionClient == null)
        NearbyConnectionClientFactory.Create((Action<INearbyConnectionClient>) (client =>
        {
          Debug.Log((object) "Nearby Client Created!!");
          PlayGamesPlatform.sNearbyConnectionClient = client;
          if (callback != null)
            callback(client);
          else
            Debug.Log((object) "Initialize Nearby callback is null");
        }));
      else if (callback != null)
      {
        Debug.Log((object) "Nearby Already initialized: calling callback directly");
        callback(PlayGamesPlatform.sNearbyConnectionClient);
      }
      else
        Debug.Log((object) "Nearby Already initialized");
    }

    public static INearbyConnectionClient Nearby
    {
      get
      {
        if (PlayGamesPlatform.sNearbyConnectionClient == null && !PlayGamesPlatform.sNearbyInitializePending)
        {
          PlayGamesPlatform.sNearbyInitializePending = true;
          PlayGamesPlatform.InitializeNearby((Action<INearbyConnectionClient>) null);
        }
        return PlayGamesPlatform.sNearbyConnectionClient;
      }
    }

    public static bool DebugLogEnabled
    {
      get
      {
        return Logger.DebugLogEnabled;
      }
      set
      {
        Logger.DebugLogEnabled = value;
      }
    }

    public IRealTimeMultiplayerClient RealTime
    {
      get
      {
        return this.mClient.GetRtmpClient();
      }
    }

    public ITurnBasedMultiplayerClient TurnBased
    {
      get
      {
        return this.mClient.GetTbmpClient();
      }
    }

    public ISavedGameClient SavedGame
    {
      get
      {
        return this.mClient.GetSavedGameClient();
      }
    }

    public IEventsClient Events
    {
      get
      {
        return this.mClient.GetEventsClient();
      }
    }

    public IQuestsClient Quests
    {
      get
      {
        return this.mClient.GetQuestsClient();
      }
    }

    public IntPtr GetApiClient()
    {
      return this.mClient.GetApiClient();
    }

    public static PlayGamesPlatform Activate()
    {
      Logger.d("Activating PlayGamesPlatform.");
      Social.set_Active((ISocialPlatform) PlayGamesPlatform.Instance);
      Logger.d("PlayGamesPlatform activated: " + (object) Social.get_Active());
      return PlayGamesPlatform.Instance;
    }

    public void AddIdMapping(string fromId, string toId)
    {
      this.mIdMap[fromId] = toId;
    }

    public void Authenticate(Action<bool> callback)
    {
      this.Authenticate(callback, false);
    }

    public void Authenticate(Action<bool> callback, bool silent)
    {
      if (this.mClient == null)
      {
        Logger.d("Creating platform-specific Play Games client.");
        this.mClient = PlayGamesClientFactory.GetPlatformPlayGamesClient(this.mConfiguration);
      }
      this.mClient.Authenticate(callback, silent);
    }

    public void Authenticate(ILocalUser unused, Action<bool> callback)
    {
      this.Authenticate(callback, false);
    }

    public bool IsAuthenticated()
    {
      if (this.mClient != null)
        return this.mClient.IsAuthenticated();
      return false;
    }

    public void SignOut()
    {
      if (this.mClient == null)
        return;
      this.mClient.SignOut();
    }

    public void LoadUsers(string[] userIds, Action<IUserProfile[]> callback)
    {
      if (!this.IsAuthenticated())
      {
        Logger.e("GetUserId() can only be called after authentication.");
        callback(new IUserProfile[0]);
      }
      this.mClient.LoadUsers(userIds, callback);
    }

    public string GetUserId()
    {
      if (this.IsAuthenticated())
        return this.mClient.GetUserId();
      Logger.e("GetUserId() can only be called after authentication.");
      return "0";
    }

    public string GetIdToken()
    {
      if (this.mClient != null)
        return this.mClient.GetIdToken();
      return (string) null;
    }

    public string GetAccessToken()
    {
      if (this.mClient != null)
        return this.mClient.GetAccessToken();
      return (string) null;
    }

    public string GetUserEmail()
    {
      if (this.mClient != null)
        return this.mClient.GetUserEmail();
      return (string) null;
    }

    public void GetPlayerStats(Action<CommonStatusCodes, PlayGamesLocalUser.PlayerStats> callback)
    {
      if (this.mClient != null && this.mClient.IsAuthenticated())
      {
        this.mClient.GetPlayerStats(callback);
      }
      else
      {
        Logger.e("GetPlayerStats can only be called after authentication.");
        callback(CommonStatusCodes.SignInRequired, (PlayGamesLocalUser.PlayerStats) null);
      }
    }

    public Achievement GetAchievement(string achievementId)
    {
      if (this.IsAuthenticated())
        return this.mClient.GetAchievement(achievementId);
      Logger.e("GetAchievement can only be called after authentication.");
      return (Achievement) null;
    }

    public string GetUserDisplayName()
    {
      if (this.IsAuthenticated())
        return this.mClient.GetUserDisplayName();
      Logger.e("GetUserDisplayName can only be called after authentication.");
      return string.Empty;
    }

    public string GetUserImageUrl()
    {
      if (this.IsAuthenticated())
        return this.mClient.GetUserImageUrl();
      Logger.e("GetUserImageUrl can only be called after authentication.");
      return (string) null;
    }

    public void ReportProgress(string achievementID, double progress, Action<bool> callback)
    {
      if (!this.IsAuthenticated())
      {
        Logger.e("ReportProgress can only be called after authentication.");
        if (callback == null)
          return;
        callback(false);
      }
      else
      {
        Logger.d("ReportProgress, " + achievementID + ", " + (object) progress);
        achievementID = this.MapId(achievementID);
        if (progress < 1E-06)
        {
          Logger.d("Progress 0.00 interpreted as request to reveal.");
          this.mClient.RevealAchievement(achievementID, callback);
        }
        else
        {
          int num1 = 0;
          int num2 = 0;
          Achievement achievement = this.mClient.GetAchievement(achievementID);
          bool flag;
          if (achievement == null)
          {
            Logger.w("Unable to locate achievement " + achievementID);
            Logger.w("As a quick fix, assuming it's standard.");
            flag = false;
          }
          else
          {
            flag = achievement.IsIncremental;
            num1 = achievement.CurrentSteps;
            num2 = achievement.TotalSteps;
            Logger.d("Achievement is " + (!flag ? "STANDARD" : "INCREMENTAL"));
            if (flag)
              Logger.d("Current steps: " + (object) num1 + "/" + (object) num2);
          }
          if (flag)
          {
            Logger.d("Progress " + (object) progress + " interpreted as incremental target (approximate).");
            if (progress >= 0.0 && progress <= 1.0)
              Logger.w("Progress " + (object) progress + " is less than or equal to 1. You might be trying to use values in the range of [0,1], while values are expected to be within the range [0,100]. If you are using the latter, you can safely ignore this message.");
            int num3 = (int) (progress / 100.0 * (double) num2);
            int steps = num3 - num1;
            Logger.d("Target steps: " + (object) num3 + ", cur steps:" + (object) num1);
            Logger.d("Steps to increment: " + (object) steps);
            if (steps <= 0)
              return;
            this.mClient.IncrementAchievement(achievementID, steps, callback);
          }
          else if (progress >= 100.0)
          {
            Logger.d("Progress " + (object) progress + " interpreted as UNLOCK.");
            this.mClient.UnlockAchievement(achievementID, callback);
          }
          else
            Logger.d("Progress " + (object) progress + " not enough to unlock non-incremental achievement.");
        }
      }
    }

    public void IncrementAchievement(string achievementID, int steps, Action<bool> callback)
    {
      if (!this.IsAuthenticated())
      {
        Logger.e("IncrementAchievement can only be called after authentication.");
        if (callback == null)
          return;
        callback(false);
      }
      else
      {
        Logger.d("IncrementAchievement: " + achievementID + ", steps " + (object) steps);
        achievementID = this.MapId(achievementID);
        this.mClient.IncrementAchievement(achievementID, steps, callback);
      }
    }

    public void SetStepsAtLeast(string achievementID, int steps, Action<bool> callback)
    {
      if (!this.IsAuthenticated())
      {
        Logger.e("SetStepsAtLeast can only be called after authentication.");
        if (callback == null)
          return;
        callback(false);
      }
      else
      {
        Logger.d("SetStepsAtLeast: " + achievementID + ", steps " + (object) steps);
        achievementID = this.MapId(achievementID);
        this.mClient.SetStepsAtLeast(achievementID, steps, callback);
      }
    }

    public void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback)
    {
      if (!this.IsAuthenticated())
      {
        Logger.e("LoadAchievementDescriptions can only be called after authentication.");
        callback((IAchievementDescription[]) null);
      }
      this.mClient.LoadAchievements((Action<Achievement[]>) (ach =>
      {
        IAchievementDescription[] iachievementDescriptionArray = new IAchievementDescription[ach.Length];
        for (int index = 0; index < iachievementDescriptionArray.Length; ++index)
          iachievementDescriptionArray[index] = (IAchievementDescription) new PlayGamesAchievement(ach[index]);
        callback(iachievementDescriptionArray);
      }));
    }

    public void LoadAchievements(Action<IAchievement[]> callback)
    {
      if (!this.IsAuthenticated())
      {
        Logger.e("LoadAchievements can only be called after authentication.");
        callback((IAchievement[]) null);
      }
      this.mClient.LoadAchievements((Action<Achievement[]>) (ach =>
      {
        IAchievement[] iachievementArray = new IAchievement[ach.Length];
        for (int index = 0; index < iachievementArray.Length; ++index)
          iachievementArray[index] = (IAchievement) new PlayGamesAchievement(ach[index]);
        callback(iachievementArray);
      }));
    }

    public IAchievement CreateAchievement()
    {
      return (IAchievement) new PlayGamesAchievement();
    }

    public void ReportScore(long score, string board, Action<bool> callback)
    {
      if (!this.IsAuthenticated())
      {
        Logger.e("ReportScore can only be called after authentication.");
        if (callback == null)
          return;
        callback(false);
      }
      else
      {
        Logger.d("ReportScore: score=" + (object) score + ", board=" + board);
        this.mClient.SubmitScore(this.MapId(board), score, callback);
      }
    }

    public void ReportScore(long score, string board, string metadata, Action<bool> callback)
    {
      if (!this.IsAuthenticated())
      {
        Logger.e("ReportScore can only be called after authentication.");
        if (callback == null)
          return;
        callback(false);
      }
      else
      {
        Logger.d("ReportScore: score=" + (object) score + ", board=" + board + " metadata=" + metadata);
        this.mClient.SubmitScore(this.MapId(board), score, metadata, callback);
      }
    }

    public void LoadScores(string leaderboardId, Action<IScore[]> callback)
    {
      this.LoadScores(leaderboardId, LeaderboardStart.PlayerCentered, this.mClient.LeaderboardMaxResults(), LeaderboardCollection.Public, LeaderboardTimeSpan.AllTime, (Action<LeaderboardScoreData>) (scoreData => callback(scoreData.Scores)));
    }

    public void LoadScores(string leaderboardId, LeaderboardStart start, int rowCount, LeaderboardCollection collection, LeaderboardTimeSpan timeSpan, Action<LeaderboardScoreData> callback)
    {
      if (!this.IsAuthenticated())
      {
        Logger.e("LoadScores can only be called after authentication.");
        callback(new LeaderboardScoreData(leaderboardId, ResponseStatus.NotAuthorized));
      }
      else
        this.mClient.LoadScores(leaderboardId, start, rowCount, collection, timeSpan, callback);
    }

    public void LoadMoreScores(ScorePageToken token, int rowCount, Action<LeaderboardScoreData> callback)
    {
      if (!this.IsAuthenticated())
      {
        Logger.e("LoadMoreScores can only be called after authentication.");
        callback(new LeaderboardScoreData(token.LeaderboardId, ResponseStatus.NotAuthorized));
      }
      else
        this.mClient.LoadMoreScores(token, rowCount, callback);
    }

    public ILeaderboard CreateLeaderboard()
    {
      return (ILeaderboard) new PlayGamesLeaderboard(this.mDefaultLbUi);
    }

    public void ShowAchievementsUI()
    {
      this.ShowAchievementsUI((Action<UIStatus>) null);
    }

    public void ShowAchievementsUI(Action<UIStatus> callback)
    {
      if (!this.IsAuthenticated())
      {
        Logger.e("ShowAchievementsUI can only be called after authentication.");
      }
      else
      {
        Logger.d("ShowAchievementsUI callback is " + (object) callback);
        this.mClient.ShowAchievementsUI(callback);
      }
    }

    public void ShowLeaderboardUI()
    {
      Logger.d("ShowLeaderboardUI with default ID");
      this.ShowLeaderboardUI(this.MapId(this.mDefaultLbUi), (Action<UIStatus>) null);
    }

    public void ShowLeaderboardUI(string lbId)
    {
      if (lbId != null)
        lbId = this.MapId(lbId);
      this.mClient.ShowLeaderboardUI(lbId, LeaderboardTimeSpan.AllTime, (Action<UIStatus>) null);
    }

    public void ShowLeaderboardUI(string lbId, Action<UIStatus> callback)
    {
      this.ShowLeaderboardUI(lbId, LeaderboardTimeSpan.AllTime, callback);
    }

    public void ShowLeaderboardUI(string lbId, LeaderboardTimeSpan span, Action<UIStatus> callback)
    {
      if (!this.IsAuthenticated())
      {
        Logger.e("ShowLeaderboardUI can only be called after authentication.");
        callback(UIStatus.NotAuthorized);
      }
      else
      {
        Logger.d("ShowLeaderboardUI, lbId=" + lbId + " callback is " + (object) callback);
        this.mClient.ShowLeaderboardUI(lbId, span, callback);
      }
    }

    public void SetDefaultLeaderboardForUI(string lbid)
    {
      Logger.d("SetDefaultLeaderboardForUI: " + lbid);
      if (lbid != null)
        lbid = this.MapId(lbid);
      this.mDefaultLbUi = lbid;
    }

    public void LoadFriends(ILocalUser user, Action<bool> callback)
    {
      if (!this.IsAuthenticated())
      {
        Logger.e("LoadScores can only be called after authentication.");
        if (callback != null)
          callback(false);
      }
      this.mClient.LoadFriends(callback);
    }

    public void LoadScores(ILeaderboard board, Action<bool> callback)
    {
      if (!this.IsAuthenticated())
      {
        Logger.e("LoadScores can only be called after authentication.");
        if (callback != null)
          callback(false);
      }
      LeaderboardTimeSpan timeSpan;
      switch ((int) board.get_timeScope())
      {
        case 0:
          timeSpan = LeaderboardTimeSpan.Daily;
          break;
        case 1:
          timeSpan = LeaderboardTimeSpan.Weekly;
          break;
        case 2:
          timeSpan = LeaderboardTimeSpan.AllTime;
          break;
        default:
          timeSpan = LeaderboardTimeSpan.AllTime;
          break;
      }
      ((PlayGamesLeaderboard) board).loading = true;
      Logger.d("LoadScores, board=" + (object) board + " callback is " + (object) callback);
      this.mClient.LoadScores(board.get_id(), LeaderboardStart.PlayerCentered, board.get_range().count <= 0 ? this.mClient.LeaderboardMaxResults() : (int) board.get_range().count, board.get_userScope() != 1 ? LeaderboardCollection.Public : LeaderboardCollection.Social, timeSpan, (Action<LeaderboardScoreData>) (scoreData => this.HandleLoadingScores((PlayGamesLeaderboard) board, scoreData, callback)));
    }

    internal void HandleLoadingScores(PlayGamesLeaderboard board, LeaderboardScoreData scoreData, Action<bool> callback)
    {
      bool flag = board.SetFromData(scoreData);
      if (flag && !board.HasAllScores() && scoreData.NextPageToken != null)
      {
        int rowCount = board.range.count - board.ScoreCount;
        this.mClient.LoadMoreScores(scoreData.NextPageToken, rowCount, (Action<LeaderboardScoreData>) (nextScoreData => this.HandleLoadingScores(board, nextScoreData, callback)));
      }
      else
        callback(flag);
    }

    public bool GetLoading(ILeaderboard board)
    {
      if (board != null)
        return board.get_loading();
      return false;
    }

    public ILocalUser localUser
    {
      get
      {
        return (ILocalUser) this.mLocalUser;
      }
    }

    public void RegisterInvitationDelegate(InvitationReceivedDelegate deleg)
    {
      this.mClient.RegisterInvitationDelegate(deleg);
    }

    private string MapId(string id)
    {
      if (id == null)
        return (string) null;
      if (!this.mIdMap.ContainsKey(id))
        return id;
      string mId = this.mIdMap[id];
      Logger.d("Mapping alias " + id + " to ID " + mId);
      return mId;
    }

    internal IUserProfile[] GetFriends()
    {
      if (this.IsAuthenticated())
        return this.mClient.GetFriends();
      Logger.d("Cannot get friends when not authenticated!");
      return new IUserProfile[0];
    }

    public string GetToken()
    {
      return this.mClient.GetToken();
    }
  }
}
