// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.SavedGame.ISavedGameMetadata
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
  public interface ISavedGameMetadata
  {
    bool IsOpen { get; }

    string Filename { get; }

    string Description { get; }

    string CoverImageURL { get; }

    TimeSpan TotalTimePlayed { get; }

    DateTime LastModifiedTimestamp { get; }
  }
}
