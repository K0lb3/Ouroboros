// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeAchievement
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class NativeAchievement : BaseReferenceHolder
  {
    private const ulong MinusOne = 18446744073709551615;

    internal NativeAchievement(IntPtr selfPointer)
      : base(selfPointer)
    {
    }

    internal uint CurrentSteps()
    {
      return GooglePlayGames.Native.Cwrapper.Achievement.Achievement_CurrentSteps(this.SelfPtr());
    }

    internal string Description()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => GooglePlayGames.Native.Cwrapper.Achievement.Achievement_Description(this.SelfPtr(), out_string, out_size)));
    }

    internal string Id()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => GooglePlayGames.Native.Cwrapper.Achievement.Achievement_Id(this.SelfPtr(), out_string, out_size)));
    }

    internal string Name()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => GooglePlayGames.Native.Cwrapper.Achievement.Achievement_Name(this.SelfPtr(), out_string, out_size)));
    }

    internal Types.AchievementState State()
    {
      return GooglePlayGames.Native.Cwrapper.Achievement.Achievement_State(this.SelfPtr());
    }

    internal uint TotalSteps()
    {
      return GooglePlayGames.Native.Cwrapper.Achievement.Achievement_TotalSteps(this.SelfPtr());
    }

    internal Types.AchievementType Type()
    {
      return GooglePlayGames.Native.Cwrapper.Achievement.Achievement_Type(this.SelfPtr());
    }

    internal ulong LastModifiedTime()
    {
      if (GooglePlayGames.Native.Cwrapper.Achievement.Achievement_Valid(this.SelfPtr()))
        return GooglePlayGames.Native.Cwrapper.Achievement.Achievement_LastModifiedTime(this.SelfPtr());
      return 0;
    }

    internal ulong getXP()
    {
      return GooglePlayGames.Native.Cwrapper.Achievement.Achievement_XP(this.SelfPtr());
    }

    internal string getRevealedImageUrl()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => GooglePlayGames.Native.Cwrapper.Achievement.Achievement_RevealedIconUrl(this.SelfPtr(), out_string, out_size)));
    }

    internal string getUnlockedImageUrl()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => GooglePlayGames.Native.Cwrapper.Achievement.Achievement_UnlockedIconUrl(this.SelfPtr(), out_string, out_size)));
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      GooglePlayGames.Native.Cwrapper.Achievement.Achievement_Dispose(selfPointer);
    }

    internal GooglePlayGames.BasicApi.Achievement AsAchievement()
    {
      GooglePlayGames.BasicApi.Achievement achievement = new GooglePlayGames.BasicApi.Achievement();
      achievement.Id = this.Id();
      achievement.Name = this.Name();
      achievement.Description = this.Description();
      DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
      ulong num = this.LastModifiedTime();
      if ((long) num == -1L)
        num = 0UL;
      achievement.LastModifiedTime = dateTime.AddMilliseconds((double) num);
      achievement.Points = this.getXP();
      achievement.RevealedImageUrl = this.getRevealedImageUrl();
      achievement.UnlockedImageUrl = this.getUnlockedImageUrl();
      if (this.Type() == Types.AchievementType.INCREMENTAL)
      {
        achievement.IsIncremental = true;
        achievement.CurrentSteps = (int) this.CurrentSteps();
        achievement.TotalSteps = (int) this.TotalSteps();
      }
      achievement.IsRevealed = this.State() == Types.AchievementState.REVEALED;
      achievement.IsUnlocked = this.State() == Types.AchievementState.UNLOCKED;
      return achievement;
    }
  }
}
