// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.PlayGamesUserProfile
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
  public class PlayGamesUserProfile : IUserProfile
  {
    private string mDisplayName;
    private string mPlayerId;
    private string mAvatarUrl;
    private volatile bool mImageLoading;
    private Texture2D mImage;

    internal PlayGamesUserProfile(string displayName, string playerId, string avatarUrl)
    {
      this.mDisplayName = displayName;
      this.mPlayerId = playerId;
      this.mAvatarUrl = avatarUrl;
      this.mImageLoading = false;
    }

    protected void ResetIdentity(string displayName, string playerId, string avatarUrl)
    {
      this.mDisplayName = displayName;
      this.mPlayerId = playerId;
      this.mAvatarUrl = avatarUrl;
      this.mImageLoading = false;
    }

    public string userName
    {
      get
      {
        return this.mDisplayName;
      }
    }

    public string id
    {
      get
      {
        return this.mPlayerId;
      }
    }

    public bool isFriend
    {
      get
      {
        return true;
      }
    }

    public UserState state
    {
      get
      {
        return (UserState) 0;
      }
    }

    public Texture2D image
    {
      get
      {
        if (!this.mImageLoading && Object.op_Equality((Object) this.mImage, (Object) null) && !string.IsNullOrEmpty(this.AvatarURL))
        {
          Debug.Log((object) ("Starting to load image: " + this.AvatarURL));
          this.mImageLoading = true;
          PlayGamesHelperObject.RunCoroutine(this.LoadImage());
        }
        return this.mImage;
      }
    }

    public string AvatarURL
    {
      get
      {
        return this.mAvatarUrl;
      }
    }

    [DebuggerHidden]
    internal IEnumerator LoadImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PlayGamesUserProfile.\u003CLoadImage\u003Ec__Iterator5() { \u003C\u003Ef__this = this };
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (!typeof (object).IsSubclassOf(typeof (PlayGamesUserProfile)))
        return false;
      return this.mPlayerId.Equals(((PlayGamesUserProfile) obj).mPlayerId);
    }

    public override int GetHashCode()
    {
      return typeof (PlayGamesUserProfile).GetHashCode() ^ this.mPlayerId.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format("[Player: '{0}' (id {1})]", (object) this.mDisplayName, (object) this.mPlayerId);
    }
  }
}
