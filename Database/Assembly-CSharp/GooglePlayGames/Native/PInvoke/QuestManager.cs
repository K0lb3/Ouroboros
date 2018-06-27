// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.QuestManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class QuestManager
  {
    private readonly GameServices mServices;

    internal QuestManager(GameServices services)
    {
      this.mServices = Misc.CheckNotNull<GameServices>(services);
    }

    internal void Fetch(Types.DataSource source, string questId, Action<QuestManager.FetchResponse> callback)
    {
      GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_Fetch(this.mServices.AsHandle(), source, questId, new GooglePlayGames.Native.Cwrapper.QuestManager.FetchCallback(QuestManager.InternalFetchCallback), Callbacks.ToIntPtr<QuestManager.FetchResponse>(callback, new Func<IntPtr, QuestManager.FetchResponse>(QuestManager.FetchResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.QuestManager.FetchCallback))]
    internal static void InternalFetchCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("QuestManager#FetchCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void FetchList(Types.DataSource source, int fetchFlags, Action<QuestManager.FetchListResponse> callback)
    {
      GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_FetchList(this.mServices.AsHandle(), source, fetchFlags, new GooglePlayGames.Native.Cwrapper.QuestManager.FetchListCallback(QuestManager.InternalFetchListCallback), Callbacks.ToIntPtr<QuestManager.FetchListResponse>(callback, new Func<IntPtr, QuestManager.FetchListResponse>(QuestManager.FetchListResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.QuestManager.FetchListCallback))]
    internal static void InternalFetchListCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("QuestManager#FetchListCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void ShowAllQuestUI(Action<QuestManager.QuestUIResponse> callback)
    {
      GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_ShowAllUI(this.mServices.AsHandle(), new GooglePlayGames.Native.Cwrapper.QuestManager.QuestUICallback(QuestManager.InternalQuestUICallback), Callbacks.ToIntPtr<QuestManager.QuestUIResponse>(callback, new Func<IntPtr, QuestManager.QuestUIResponse>(QuestManager.QuestUIResponse.FromPointer)));
    }

    internal void ShowQuestUI(NativeQuest quest, Action<QuestManager.QuestUIResponse> callback)
    {
      GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_ShowUI(this.mServices.AsHandle(), quest.AsPointer(), new GooglePlayGames.Native.Cwrapper.QuestManager.QuestUICallback(QuestManager.InternalQuestUICallback), Callbacks.ToIntPtr<QuestManager.QuestUIResponse>(callback, new Func<IntPtr, QuestManager.QuestUIResponse>(QuestManager.QuestUIResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.QuestManager.QuestUICallback))]
    internal static void InternalQuestUICallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("QuestManager#QuestUICallback", Callbacks.Type.Temporary, response, data);
    }

    internal void Accept(NativeQuest quest, Action<QuestManager.AcceptResponse> callback)
    {
      GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_Accept(this.mServices.AsHandle(), quest.AsPointer(), new GooglePlayGames.Native.Cwrapper.QuestManager.AcceptCallback(QuestManager.InternalAcceptCallback), Callbacks.ToIntPtr<QuestManager.AcceptResponse>(callback, new Func<IntPtr, QuestManager.AcceptResponse>(QuestManager.AcceptResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.QuestManager.AcceptCallback))]
    internal static void InternalAcceptCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("QuestManager#AcceptCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void ClaimMilestone(NativeQuestMilestone milestone, Action<QuestManager.ClaimMilestoneResponse> callback)
    {
      GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_ClaimMilestone(this.mServices.AsHandle(), milestone.AsPointer(), new GooglePlayGames.Native.Cwrapper.QuestManager.ClaimMilestoneCallback(QuestManager.InternalClaimMilestoneCallback), Callbacks.ToIntPtr<QuestManager.ClaimMilestoneResponse>(callback, new Func<IntPtr, QuestManager.ClaimMilestoneResponse>(QuestManager.ClaimMilestoneResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.QuestManager.ClaimMilestoneCallback))]
    internal static void InternalClaimMilestoneCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("QuestManager#ClaimMilestoneCallback", Callbacks.Type.Temporary, response, data);
    }

    internal class FetchResponse : BaseReferenceHolder
    {
      internal FetchResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.ResponseStatus ResponseStatus()
      {
        return GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_FetchResponse_GetStatus(this.SelfPtr());
      }

      internal NativeQuest Data()
      {
        if (!this.RequestSucceeded())
          return (NativeQuest) null;
        return new NativeQuest(GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_FetchResponse_GetData(this.SelfPtr()));
      }

      internal bool RequestSucceeded()
      {
        return this.ResponseStatus() > ~CommonErrorStatus.ResponseStatus.ERROR_LICENSE_CHECK_FAILED;
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_FetchResponse_Dispose(selfPointer);
      }

      internal static QuestManager.FetchResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (QuestManager.FetchResponse) null;
        return new QuestManager.FetchResponse(pointer);
      }
    }

    internal class FetchListResponse : BaseReferenceHolder
    {
      internal FetchListResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.ResponseStatus ResponseStatus()
      {
        return GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_FetchListResponse_GetStatus(this.SelfPtr());
      }

      internal bool RequestSucceeded()
      {
        return this.ResponseStatus() > ~CommonErrorStatus.ResponseStatus.ERROR_LICENSE_CHECK_FAILED;
      }

      internal IEnumerable<NativeQuest> Data()
      {
        return PInvokeUtilities.ToEnumerable<NativeQuest>(GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_FetchListResponse_GetData_Length(this.SelfPtr()), (Func<UIntPtr, NativeQuest>) (index => new NativeQuest(GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_FetchListResponse_GetData_GetElement(this.SelfPtr(), index))));
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_FetchListResponse_Dispose(selfPointer);
      }

      internal static QuestManager.FetchListResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (QuestManager.FetchListResponse) null;
        return new QuestManager.FetchListResponse(pointer);
      }
    }

    internal class ClaimMilestoneResponse : BaseReferenceHolder
    {
      internal ClaimMilestoneResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.QuestClaimMilestoneStatus ResponseStatus()
      {
        return GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_ClaimMilestoneResponse_GetStatus(this.SelfPtr());
      }

      internal NativeQuest Quest()
      {
        if (!this.RequestSucceeded())
          return (NativeQuest) null;
        NativeQuest nativeQuest = new NativeQuest(GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_ClaimMilestoneResponse_GetQuest(this.SelfPtr()));
        if (nativeQuest.Valid())
          return nativeQuest;
        nativeQuest.Dispose();
        return (NativeQuest) null;
      }

      internal NativeQuestMilestone ClaimedMilestone()
      {
        if (!this.RequestSucceeded())
          return (NativeQuestMilestone) null;
        NativeQuestMilestone nativeQuestMilestone = new NativeQuestMilestone(GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_ClaimMilestoneResponse_GetClaimedMilestone(this.SelfPtr()));
        if (nativeQuestMilestone.Valid())
          return nativeQuestMilestone;
        nativeQuestMilestone.Dispose();
        return (NativeQuestMilestone) null;
      }

      internal bool RequestSucceeded()
      {
        return this.ResponseStatus() > ~(CommonErrorStatus.QuestClaimMilestoneStatus.ERROR_INTERNAL | CommonErrorStatus.QuestClaimMilestoneStatus.VALID);
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_ClaimMilestoneResponse_Dispose(selfPointer);
      }

      internal static QuestManager.ClaimMilestoneResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (QuestManager.ClaimMilestoneResponse) null;
        return new QuestManager.ClaimMilestoneResponse(pointer);
      }
    }

    internal class AcceptResponse : BaseReferenceHolder
    {
      internal AcceptResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.QuestAcceptStatus ResponseStatus()
      {
        return GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_AcceptResponse_GetStatus(this.SelfPtr());
      }

      internal NativeQuest AcceptedQuest()
      {
        if (!this.RequestSucceeded())
          return (NativeQuest) null;
        return new NativeQuest(GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_AcceptResponse_GetAcceptedQuest(this.SelfPtr()));
      }

      internal bool RequestSucceeded()
      {
        return this.ResponseStatus() > ~(CommonErrorStatus.QuestAcceptStatus.ERROR_INTERNAL | CommonErrorStatus.QuestAcceptStatus.VALID);
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_AcceptResponse_Dispose(selfPointer);
      }

      internal static QuestManager.AcceptResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (QuestManager.AcceptResponse) null;
        return new QuestManager.AcceptResponse(pointer);
      }
    }

    internal class QuestUIResponse : BaseReferenceHolder
    {
      internal QuestUIResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.UIStatus RequestStatus()
      {
        return GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_QuestUIResponse_GetStatus(this.SelfPtr());
      }

      internal bool RequestSucceeded()
      {
        return this.RequestStatus() > ~(CommonErrorStatus.UIStatus.ERROR_INTERNAL | CommonErrorStatus.UIStatus.VALID);
      }

      internal NativeQuest AcceptedQuest()
      {
        if (!this.RequestSucceeded())
          return (NativeQuest) null;
        NativeQuest nativeQuest = new NativeQuest(GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_QuestUIResponse_GetAcceptedQuest(this.SelfPtr()));
        if (nativeQuest.Valid())
          return nativeQuest;
        nativeQuest.Dispose();
        return (NativeQuest) null;
      }

      internal NativeQuestMilestone MilestoneToClaim()
      {
        if (!this.RequestSucceeded())
          return (NativeQuestMilestone) null;
        NativeQuestMilestone nativeQuestMilestone = new NativeQuestMilestone(GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_QuestUIResponse_GetMilestoneToClaim(this.SelfPtr()));
        if (nativeQuestMilestone.Valid())
          return nativeQuestMilestone;
        nativeQuestMilestone.Dispose();
        return (NativeQuestMilestone) null;
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.QuestManager.QuestManager_QuestUIResponse_Dispose(selfPointer);
      }

      internal static QuestManager.QuestUIResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (QuestManager.QuestUIResponse) null;
        return new QuestManager.QuestUIResponse(pointer);
      }
    }
  }
}
