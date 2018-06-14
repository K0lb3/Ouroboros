// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeScoreSummary
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class NativeScoreSummary : BaseReferenceHolder
  {
    internal NativeScoreSummary(IntPtr selfPtr)
      : base(selfPtr)
    {
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      ScoreSummary.ScoreSummary_Dispose(selfPointer);
    }

    internal ulong ApproximateResults()
    {
      return ScoreSummary.ScoreSummary_ApproximateNumberOfScores(this.SelfPtr());
    }

    internal NativeScore LocalUserScore()
    {
      return NativeScore.FromPointer(ScoreSummary.ScoreSummary_CurrentPlayerScore(this.SelfPtr()));
    }

    internal static NativeScoreSummary FromPointer(IntPtr pointer)
    {
      if (pointer.Equals((object) IntPtr.Zero))
        return (NativeScoreSummary) null;
      return new NativeScoreSummary(pointer);
    }
  }
}
