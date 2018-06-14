// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.PlayerManager
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
  internal class PlayerManager
  {
    private readonly GameServices mGameServices;

    internal PlayerManager(GameServices services)
    {
      this.mGameServices = Misc.CheckNotNull<GameServices>(services);
    }

    internal void FetchSelf(Action<PlayerManager.FetchSelfResponse> callback)
    {
      GooglePlayGames.Native.Cwrapper.PlayerManager.PlayerManager_FetchSelf(this.mGameServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, new GooglePlayGames.Native.Cwrapper.PlayerManager.FetchSelfCallback(PlayerManager.InternalFetchSelfCallback), Callbacks.ToIntPtr<PlayerManager.FetchSelfResponse>(callback, new Func<IntPtr, PlayerManager.FetchSelfResponse>(PlayerManager.FetchSelfResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.PlayerManager.FetchSelfCallback))]
    private static void InternalFetchSelfCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("PlayerManager#InternalFetchSelfCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void FetchList(string[] userIds, Action<NativePlayer[]> callback)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PlayerManager.\u003CFetchList\u003Ec__AnonStorey1DB listCAnonStorey1Db = new PlayerManager.\u003CFetchList\u003Ec__AnonStorey1DB();
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey1Db.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey1Db.coll = new PlayerManager.FetchResponseCollector();
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey1Db.coll.pendingCount = userIds.Length;
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey1Db.coll.callback = callback;
      foreach (string userId in userIds)
      {
        // ISSUE: reference to a compiler-generated method
        GooglePlayGames.Native.Cwrapper.PlayerManager.PlayerManager_Fetch(this.mGameServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, userId, new GooglePlayGames.Native.Cwrapper.PlayerManager.FetchCallback(PlayerManager.InternalFetchCallback), Callbacks.ToIntPtr<PlayerManager.FetchResponse>(new Action<PlayerManager.FetchResponse>(listCAnonStorey1Db.\u003C\u003Em__11E), new Func<IntPtr, PlayerManager.FetchResponse>(PlayerManager.FetchResponse.FromPointer)));
      }
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.PlayerManager.FetchCallback))]
    private static void InternalFetchCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("PlayerManager#InternalFetchCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void HandleFetchResponse(PlayerManager.FetchResponseCollector collector, PlayerManager.FetchResponse resp)
    {
      if (resp.Status() == CommonErrorStatus.ResponseStatus.VALID || resp.Status() == CommonErrorStatus.ResponseStatus.VALID_BUT_STALE)
      {
        NativePlayer player = resp.GetPlayer();
        collector.results.Add(player);
      }
      --collector.pendingCount;
      if (collector.pendingCount != 0)
        return;
      collector.callback(collector.results.ToArray());
    }

    internal void FetchFriends(Action<GooglePlayGames.BasicApi.ResponseStatus, List<GooglePlayGames.BasicApi.Multiplayer.Player>> callback)
    {
      GooglePlayGames.Native.Cwrapper.PlayerManager.PlayerManager_FetchConnected(this.mGameServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, new GooglePlayGames.Native.Cwrapper.PlayerManager.FetchListCallback(PlayerManager.InternalFetchConnectedCallback), Callbacks.ToIntPtr<PlayerManager.FetchListResponse>((Action<PlayerManager.FetchListResponse>) (rsp => this.HandleFetchCollected(rsp, callback)), new Func<IntPtr, PlayerManager.FetchListResponse>(PlayerManager.FetchListResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.PlayerManager.FetchListCallback))]
    private static void InternalFetchConnectedCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("PlayerManager#InternalFetchConnectedCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void HandleFetchCollected(PlayerManager.FetchListResponse rsp, Action<GooglePlayGames.BasicApi.ResponseStatus, List<GooglePlayGames.BasicApi.Multiplayer.Player>> callback)
    {
      List<GooglePlayGames.BasicApi.Multiplayer.Player> playerList = new List<GooglePlayGames.BasicApi.Multiplayer.Player>();
      if (rsp.Status() == CommonErrorStatus.ResponseStatus.VALID || rsp.Status() == CommonErrorStatus.ResponseStatus.VALID_BUT_STALE)
      {
        Logger.d("Got " + (object) rsp.Length().ToUInt64() + " players");
        foreach (NativePlayer nativePlayer in rsp)
          playerList.Add(nativePlayer.AsPlayer());
      }
      callback((GooglePlayGames.BasicApi.ResponseStatus) rsp.Status(), playerList);
    }

    internal class FetchListResponse : BaseReferenceHolder, IEnumerable, IEnumerable<NativePlayer>
    {
      internal FetchListResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) this.GetEnumerator();
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.PlayerManager.PlayerManager_FetchListResponse_Dispose(this.SelfPtr());
      }

      internal CommonErrorStatus.ResponseStatus Status()
      {
        return GooglePlayGames.Native.Cwrapper.PlayerManager.PlayerManager_FetchListResponse_GetStatus(this.SelfPtr());
      }

      public IEnumerator<NativePlayer> GetEnumerator()
      {
        return PInvokeUtilities.ToEnumerator<NativePlayer>(this.Length(), (Func<UIntPtr, NativePlayer>) (index => this.GetElement(index)));
      }

      internal UIntPtr Length()
      {
        return GooglePlayGames.Native.Cwrapper.PlayerManager.PlayerManager_FetchListResponse_GetData_Length(this.SelfPtr());
      }

      internal NativePlayer GetElement(UIntPtr index)
      {
        if (index.ToUInt64() >= this.Length().ToUInt64())
          throw new ArgumentOutOfRangeException();
        return new NativePlayer(GooglePlayGames.Native.Cwrapper.PlayerManager.PlayerManager_FetchListResponse_GetData_GetElement(this.SelfPtr(), index));
      }

      internal static PlayerManager.FetchListResponse FromPointer(IntPtr selfPointer)
      {
        if (PInvokeUtilities.IsNull(selfPointer))
          return (PlayerManager.FetchListResponse) null;
        return new PlayerManager.FetchListResponse(selfPointer);
      }
    }

    internal class FetchResponseCollector
    {
      internal List<NativePlayer> results = new List<NativePlayer>();
      internal int pendingCount;
      internal Action<NativePlayer[]> callback;
    }

    internal class FetchResponse : BaseReferenceHolder
    {
      internal FetchResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.PlayerManager.PlayerManager_FetchResponse_Dispose(this.SelfPtr());
      }

      internal NativePlayer GetPlayer()
      {
        return new NativePlayer(GooglePlayGames.Native.Cwrapper.PlayerManager.PlayerManager_FetchResponse_GetData(this.SelfPtr()));
      }

      internal CommonErrorStatus.ResponseStatus Status()
      {
        return GooglePlayGames.Native.Cwrapper.PlayerManager.PlayerManager_FetchResponse_GetStatus(this.SelfPtr());
      }

      internal static PlayerManager.FetchResponse FromPointer(IntPtr selfPointer)
      {
        if (PInvokeUtilities.IsNull(selfPointer))
          return (PlayerManager.FetchResponse) null;
        return new PlayerManager.FetchResponse(selfPointer);
      }
    }

    internal class FetchSelfResponse : BaseReferenceHolder
    {
      internal FetchSelfResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.ResponseStatus Status()
      {
        return GooglePlayGames.Native.Cwrapper.PlayerManager.PlayerManager_FetchSelfResponse_GetStatus(this.SelfPtr());
      }

      internal NativePlayer Self()
      {
        return new NativePlayer(GooglePlayGames.Native.Cwrapper.PlayerManager.PlayerManager_FetchSelfResponse_GetData(this.SelfPtr()));
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.PlayerManager.PlayerManager_FetchSelfResponse_Dispose(this.SelfPtr());
      }

      internal static PlayerManager.FetchSelfResponse FromPointer(IntPtr selfPointer)
      {
        if (PInvokeUtilities.IsNull(selfPointer))
          return (PlayerManager.FetchSelfResponse) null;
        return new PlayerManager.FetchSelfResponse(selfPointer);
      }
    }
  }
}
