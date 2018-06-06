// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.ScorePageToken
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi
{
  public class ScorePageToken
  {
    private string mId;
    private object mInternalObject;
    private LeaderboardCollection mCollection;
    private LeaderboardTimeSpan mTimespan;

    internal ScorePageToken(object internalObject, string id, LeaderboardCollection collection, LeaderboardTimeSpan timespan)
    {
      this.mInternalObject = internalObject;
      this.mId = id;
      this.mCollection = collection;
      this.mTimespan = timespan;
    }

    public LeaderboardCollection Collection
    {
      get
      {
        return this.mCollection;
      }
    }

    public LeaderboardTimeSpan TimeSpan
    {
      get
      {
        return this.mTimespan;
      }
    }

    public string LeaderboardId
    {
      get
      {
        return this.mId;
      }
    }

    internal object InternalObject
    {
      get
      {
        return this.mInternalObject;
      }
    }
  }
}
