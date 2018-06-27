// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.PlayGamesAchievement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;
using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
  internal class PlayGamesAchievement : IAchievementDescription, IAchievement
  {
    private string mId = string.Empty;
    private DateTime mLastModifiedTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
    private string mTitle = string.Empty;
    private string mRevealedImageUrl = string.Empty;
    private string mUnlockedImageUrl = string.Empty;
    private string mDescription = string.Empty;
    private readonly ReportProgress mProgressCallback;
    private double mPercentComplete;
    private bool mCompleted;
    private bool mHidden;
    private WWW mImageFetcher;
    private Texture2D mImage;
    private ulong mPoints;

    internal PlayGamesAchievement()
      : this(new ReportProgress(PlayGamesPlatform.Instance.ReportProgress))
    {
    }

    internal PlayGamesAchievement(ReportProgress progressCallback)
    {
      this.mProgressCallback = progressCallback;
    }

    internal PlayGamesAchievement(Achievement ach)
      : this()
    {
      this.mId = ach.Id;
      this.mPercentComplete = (double) ach.CurrentSteps / (double) ach.TotalSteps;
      this.mCompleted = ach.IsUnlocked;
      this.mHidden = !ach.IsRevealed;
      this.mLastModifiedTime = ach.LastModifiedTime;
      this.mTitle = ach.Name;
      this.mDescription = ach.Description;
      this.mPoints = ach.Points;
      this.mRevealedImageUrl = ach.RevealedImageUrl;
      this.mUnlockedImageUrl = ach.UnlockedImageUrl;
    }

    public void ReportProgress(Action<bool> callback)
    {
      this.mProgressCallback(this.mId, this.mPercentComplete, callback);
    }

    private Texture2D LoadImage()
    {
      if (this.hidden)
        return (Texture2D) null;
      string str = !this.completed ? this.mRevealedImageUrl : this.mUnlockedImageUrl;
      if (!string.IsNullOrEmpty(str))
      {
        if (this.mImageFetcher == null || this.mImageFetcher.get_url() != str)
        {
          this.mImageFetcher = new WWW(str);
          this.mImage = (Texture2D) null;
        }
        if (Object.op_Inequality((Object) this.mImage, (Object) null))
          return this.mImage;
        if (this.mImageFetcher.get_isDone())
        {
          this.mImage = this.mImageFetcher.get_texture();
          return this.mImage;
        }
      }
      return (Texture2D) null;
    }

    public string id
    {
      get
      {
        return this.mId;
      }
      set
      {
        this.mId = value;
      }
    }

    public double percentCompleted
    {
      get
      {
        return this.mPercentComplete;
      }
      set
      {
        this.mPercentComplete = value;
      }
    }

    public bool completed
    {
      get
      {
        return this.mCompleted;
      }
    }

    public bool hidden
    {
      get
      {
        return this.mHidden;
      }
    }

    public DateTime lastReportedDate
    {
      get
      {
        return this.mLastModifiedTime;
      }
    }

    public string title
    {
      get
      {
        return this.mTitle;
      }
    }

    public Texture2D image
    {
      get
      {
        return this.LoadImage();
      }
    }

    public string achievedDescription
    {
      get
      {
        return this.mDescription;
      }
    }

    public string unachievedDescription
    {
      get
      {
        return this.mDescription;
      }
    }

    public int points
    {
      get
      {
        return (int) this.mPoints;
      }
    }
  }
}
