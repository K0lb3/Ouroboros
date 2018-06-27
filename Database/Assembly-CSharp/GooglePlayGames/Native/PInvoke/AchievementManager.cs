// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.AchievementManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class AchievementManager
  {
    private readonly GameServices mServices;

    internal AchievementManager(GameServices services)
    {
      this.mServices = Misc.CheckNotNull<GameServices>(services);
    }

    internal void ShowAllUI(Action<CommonErrorStatus.UIStatus> callback)
    {
      Misc.CheckNotNull<Action<CommonErrorStatus.UIStatus>>(callback);
      GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_ShowAllUI(this.mServices.AsHandle(), new GooglePlayGames.Native.Cwrapper.AchievementManager.ShowAllUICallback(Callbacks.InternalShowUICallback), Callbacks.ToIntPtr((Delegate) callback));
    }

    internal void FetchAll(Action<AchievementManager.FetchAllResponse> callback)
    {
      Misc.CheckNotNull<Action<AchievementManager.FetchAllResponse>>(callback);
      GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchAll(this.mServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, new GooglePlayGames.Native.Cwrapper.AchievementManager.FetchAllCallback(AchievementManager.InternalFetchAllCallback), Callbacks.ToIntPtr<AchievementManager.FetchAllResponse>(callback, new Func<IntPtr, AchievementManager.FetchAllResponse>(AchievementManager.FetchAllResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.AchievementManager.FetchAllCallback))]
    private static void InternalFetchAllCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("AchievementManager#InternalFetchAllCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void Fetch(string achId, Action<AchievementManager.FetchResponse> callback)
    {
      Misc.CheckNotNull<string>(achId);
      Misc.CheckNotNull<Action<AchievementManager.FetchResponse>>(callback);
      GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_Fetch(this.mServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, achId, new GooglePlayGames.Native.Cwrapper.AchievementManager.FetchCallback(AchievementManager.InternalFetchCallback), Callbacks.ToIntPtr<AchievementManager.FetchResponse>(callback, new Func<IntPtr, AchievementManager.FetchResponse>(AchievementManager.FetchResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.AchievementManager.FetchCallback))]
    private static void InternalFetchCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("AchievementManager#InternalFetchCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void Increment(string achievementId, uint numSteps)
    {
      Misc.CheckNotNull<string>(achievementId);
      GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_Increment(this.mServices.AsHandle(), achievementId, numSteps);
    }

    internal void SetStepsAtLeast(string achivementId, uint numSteps)
    {
      Misc.CheckNotNull<string>(achivementId);
      GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_SetStepsAtLeast(this.mServices.AsHandle(), achivementId, numSteps);
    }

    internal void Reveal(string achievementId)
    {
      Misc.CheckNotNull<string>(achievementId);
      GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_Reveal(this.mServices.AsHandle(), achievementId);
    }

    internal void Unlock(string achievementId)
    {
      Misc.CheckNotNull<string>(achievementId);
      GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_Unlock(this.mServices.AsHandle(), achievementId);
    }

    internal class FetchResponse : BaseReferenceHolder
    {
      internal FetchResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.ResponseStatus Status()
      {
        return GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchResponse_GetStatus(this.SelfPtr());
      }

      internal NativeAchievement Achievement()
      {
        return new NativeAchievement(GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchResponse_GetData(this.SelfPtr()));
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchResponse_Dispose(selfPointer);
      }

      internal static AchievementManager.FetchResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (AchievementManager.FetchResponse) null;
        return new AchievementManager.FetchResponse(pointer);
      }
    }

    internal class FetchAllResponse : BaseReferenceHolder, IEnumerable, IEnumerable<NativeAchievement>
    {
      internal FetchAllResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) this.GetEnumerator();
      }

      internal CommonErrorStatus.ResponseStatus Status()
      {
        return GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchAllResponse_GetStatus(this.SelfPtr());
      }

      private UIntPtr Length()
      {
        return GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchAllResponse_GetData_Length(this.SelfPtr());
      }

      private NativeAchievement GetElement(UIntPtr index)
      {
        if (index.ToUInt64() >= this.Length().ToUInt64())
          throw new ArgumentOutOfRangeException();
        return new NativeAchievement(GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchAllResponse_GetData_GetElement(this.SelfPtr(), index));
      }

      public IEnumerator<NativeAchievement> GetEnumerator()
      {
        return PInvokeUtilities.ToEnumerator<NativeAchievement>(GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchAllResponse_GetData_Length(this.SelfPtr()), (Func<UIntPtr, NativeAchievement>) (index => this.GetElement(index)));
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchAllResponse_Dispose(selfPointer);
      }

      internal static AchievementManager.FetchAllResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (AchievementManager.FetchAllResponse) null;
        return new AchievementManager.FetchAllResponse(pointer);
      }
    }
  }
}
