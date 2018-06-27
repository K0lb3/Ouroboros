// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.PlayGamesLocalUser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;
using System;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
  public class PlayGamesLocalUser : PlayGamesUserProfile, IUserProfile, ILocalUser
  {
    internal PlayGamesPlatform mPlatform;
    private string emailAddress;
    private PlayGamesLocalUser.PlayerStats mStats;

    internal PlayGamesLocalUser(PlayGamesPlatform plaf)
      : base("localUser", string.Empty, string.Empty)
    {
      this.mPlatform = plaf;
      this.emailAddress = (string) null;
      this.mStats = (PlayGamesLocalUser.PlayerStats) null;
    }

    public void Authenticate(Action<bool> callback)
    {
      this.mPlatform.Authenticate(callback);
    }

    public void Authenticate(Action<bool> callback, bool silent)
    {
      this.mPlatform.Authenticate(callback, silent);
    }

    public void LoadFriends(Action<bool> callback)
    {
      this.mPlatform.LoadFriends((ILocalUser) this, callback);
    }

    public IUserProfile[] friends
    {
      get
      {
        return this.mPlatform.GetFriends();
      }
    }

    public bool authenticated
    {
      get
      {
        return this.mPlatform.IsAuthenticated();
      }
    }

    public bool underage
    {
      get
      {
        return true;
      }
    }

    public new string userName
    {
      get
      {
        string displayName = string.Empty;
        if (this.authenticated)
        {
          displayName = this.mPlatform.GetUserDisplayName();
          if (!base.userName.Equals(displayName))
            this.ResetIdentity(displayName, this.mPlatform.GetUserId(), this.mPlatform.GetUserImageUrl());
        }
        return displayName;
      }
    }

    public new string id
    {
      get
      {
        string playerId = string.Empty;
        if (this.authenticated)
        {
          playerId = this.mPlatform.GetUserId();
          if (!base.id.Equals(playerId))
            this.ResetIdentity(this.mPlatform.GetUserDisplayName(), playerId, this.mPlatform.GetUserImageUrl());
        }
        return playerId;
      }
    }

    public string idToken
    {
      get
      {
        if (this.authenticated)
          return this.mPlatform.GetIdToken();
        return string.Empty;
      }
    }

    public string accessToken
    {
      get
      {
        if (this.authenticated)
          return this.mPlatform.GetAccessToken();
        return string.Empty;
      }
    }

    public new bool isFriend
    {
      get
      {
        return true;
      }
    }

    public new UserState state
    {
      get
      {
        return (UserState) 0;
      }
    }

    public new string AvatarURL
    {
      get
      {
        string avatarUrl = string.Empty;
        if (this.authenticated)
        {
          avatarUrl = this.mPlatform.GetUserImageUrl();
          if (!base.id.Equals(avatarUrl))
            this.ResetIdentity(this.mPlatform.GetUserDisplayName(), this.mPlatform.GetUserId(), avatarUrl);
        }
        return avatarUrl;
      }
    }

    public string Email
    {
      get
      {
        if (this.authenticated && this.emailAddress == null)
        {
          this.emailAddress = this.mPlatform.GetUserEmail();
          this.emailAddress = this.emailAddress == null ? string.Empty : this.emailAddress;
        }
        if (this.authenticated)
          return this.emailAddress;
        return string.Empty;
      }
    }

    public void GetStats(Action<CommonStatusCodes, PlayGamesLocalUser.PlayerStats> callback)
    {
      if (this.mStats == null)
        this.mPlatform.GetPlayerStats((Action<CommonStatusCodes, PlayGamesLocalUser.PlayerStats>) ((rc, stats) =>
        {
          this.mStats = stats;
          callback(rc, stats);
        }));
      else
        callback(CommonStatusCodes.Success, this.mStats);
    }

    public class PlayerStats
    {
      private int numberOfPurchases;
      private float avgSessonLength;
      private int daysSinceLastPlayed;
      private int numOfSessions;
      private float sessPercentile;
      private float spendPercentile;

      public int NumberOfPurchases { get; set; }

      public float AvgSessonLength { get; set; }

      public int DaysSinceLastPlayed { get; set; }

      public int NumOfSessions { get; set; }

      public float SessPercentile { get; set; }

      public float SpendPercentile { get; set; }
    }
  }
}
