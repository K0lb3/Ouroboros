// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.SavedGame.SavedGameMetadataUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;
using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
  public struct SavedGameMetadataUpdate
  {
    private readonly bool mDescriptionUpdated;
    private readonly string mNewDescription;
    private readonly bool mCoverImageUpdated;
    private readonly byte[] mNewPngCoverImage;
    private readonly TimeSpan? mNewPlayedTime;

    private SavedGameMetadataUpdate(SavedGameMetadataUpdate.Builder builder)
    {
      this.mDescriptionUpdated = builder.mDescriptionUpdated;
      this.mNewDescription = builder.mNewDescription;
      this.mCoverImageUpdated = builder.mCoverImageUpdated;
      this.mNewPngCoverImage = builder.mNewPngCoverImage;
      this.mNewPlayedTime = builder.mNewPlayedTime;
    }

    public bool IsDescriptionUpdated
    {
      get
      {
        return this.mDescriptionUpdated;
      }
    }

    public string UpdatedDescription
    {
      get
      {
        return this.mNewDescription;
      }
    }

    public bool IsCoverImageUpdated
    {
      get
      {
        return this.mCoverImageUpdated;
      }
    }

    public byte[] UpdatedPngCoverImage
    {
      get
      {
        return this.mNewPngCoverImage;
      }
    }

    public bool IsPlayedTimeUpdated
    {
      get
      {
        return this.mNewPlayedTime.HasValue;
      }
    }

    public TimeSpan? UpdatedPlayedTime
    {
      get
      {
        return this.mNewPlayedTime;
      }
    }

    public struct Builder
    {
      internal bool mDescriptionUpdated;
      internal string mNewDescription;
      internal bool mCoverImageUpdated;
      internal byte[] mNewPngCoverImage;
      internal TimeSpan? mNewPlayedTime;

      public SavedGameMetadataUpdate.Builder WithUpdatedDescription(string description)
      {
        this.mNewDescription = Misc.CheckNotNull<string>(description);
        this.mDescriptionUpdated = true;
        return this;
      }

      public SavedGameMetadataUpdate.Builder WithUpdatedPngCoverImage(byte[] newPngCoverImage)
      {
        this.mCoverImageUpdated = true;
        this.mNewPngCoverImage = newPngCoverImage;
        return this;
      }

      public SavedGameMetadataUpdate.Builder WithUpdatedPlayedTime(TimeSpan newPlayedTime)
      {
        if (newPlayedTime.TotalMilliseconds > 1.84467440737096E+19)
          throw new InvalidOperationException("Timespans longer than ulong.MaxValue milliseconds are not allowed");
        this.mNewPlayedTime = new TimeSpan?(newPlayedTime);
        return this;
      }

      public SavedGameMetadataUpdate Build()
      {
        return new SavedGameMetadataUpdate(this);
      }
    }
  }
}
