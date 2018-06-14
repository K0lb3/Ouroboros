// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeScoreEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class NativeScoreEntry : BaseReferenceHolder
  {
    private const ulong MinusOne = 18446744073709551615;

    internal NativeScoreEntry(IntPtr selfPtr)
      : base(selfPtr)
    {
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      ScorePage.ScorePage_Entry_Dispose(selfPointer);
    }

    internal ulong GetLastModifiedTime()
    {
      return ScorePage.ScorePage_Entry_LastModifiedTime(this.SelfPtr());
    }

    internal string GetPlayerId()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => ScorePage.ScorePage_Entry_PlayerId(this.SelfPtr(), out_string, out_size)));
    }

    internal NativeScore GetScore()
    {
      return new NativeScore(ScorePage.ScorePage_Entry_Score(this.SelfPtr()));
    }

    internal PlayGamesScore AsScore(string leaderboardId)
    {
      DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
      ulong num = this.GetLastModifiedTime();
      if ((long) num == -1L)
        num = 0UL;
      return new PlayGamesScore(dateTime.AddMilliseconds((double) num), leaderboardId, this.GetScore().GetRank(), this.GetPlayerId(), this.GetScore().GetValue(), this.GetScore().GetMetadata());
    }
  }
}
