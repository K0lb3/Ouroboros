// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.EventManager
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class EventManager
  {
    private readonly GameServices mServices;

    internal EventManager(GameServices services)
    {
      this.mServices = Misc.CheckNotNull<GameServices>(services);
    }

    internal void FetchAll(Types.DataSource source, Action<EventManager.FetchAllResponse> callback)
    {
      GooglePlayGames.Native.Cwrapper.EventManager.EventManager_FetchAll(this.mServices.AsHandle(), source, new GooglePlayGames.Native.Cwrapper.EventManager.FetchAllCallback(EventManager.InternalFetchAllCallback), Callbacks.ToIntPtr<EventManager.FetchAllResponse>(callback, new Func<IntPtr, EventManager.FetchAllResponse>(EventManager.FetchAllResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.EventManager.FetchAllCallback))]
    internal static void InternalFetchAllCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("EventManager#FetchAllCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void Fetch(Types.DataSource source, string eventId, Action<EventManager.FetchResponse> callback)
    {
      GooglePlayGames.Native.Cwrapper.EventManager.EventManager_Fetch(this.mServices.AsHandle(), source, eventId, new GooglePlayGames.Native.Cwrapper.EventManager.FetchCallback(EventManager.InternalFetchCallback), Callbacks.ToIntPtr<EventManager.FetchResponse>(callback, new Func<IntPtr, EventManager.FetchResponse>(EventManager.FetchResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.EventManager.FetchCallback))]
    internal static void InternalFetchCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("EventManager#FetchCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void Increment(string eventId, uint steps)
    {
      GooglePlayGames.Native.Cwrapper.EventManager.EventManager_Increment(this.mServices.AsHandle(), eventId, steps);
    }

    internal class FetchResponse : BaseReferenceHolder
    {
      internal FetchResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.ResponseStatus ResponseStatus()
      {
        return GooglePlayGames.Native.Cwrapper.EventManager.EventManager_FetchResponse_GetStatus(this.SelfPtr());
      }

      internal bool RequestSucceeded()
      {
        return this.ResponseStatus() > ~CommonErrorStatus.ResponseStatus.ERROR_LICENSE_CHECK_FAILED;
      }

      internal NativeEvent Data()
      {
        if (!this.RequestSucceeded())
          return (NativeEvent) null;
        return new NativeEvent(GooglePlayGames.Native.Cwrapper.EventManager.EventManager_FetchResponse_GetData(this.SelfPtr()));
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.EventManager.EventManager_FetchResponse_Dispose(selfPointer);
      }

      internal static EventManager.FetchResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (EventManager.FetchResponse) null;
        return new EventManager.FetchResponse(pointer);
      }
    }

    internal class FetchAllResponse : BaseReferenceHolder
    {
      internal FetchAllResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.ResponseStatus ResponseStatus()
      {
        return GooglePlayGames.Native.Cwrapper.EventManager.EventManager_FetchAllResponse_GetStatus(this.SelfPtr());
      }

      internal List<NativeEvent> Data()
      {
        return ((IEnumerable<IntPtr>) PInvokeUtilities.OutParamsToArray<IntPtr>((PInvokeUtilities.OutMethod<IntPtr>) ((out_arg, out_size) => GooglePlayGames.Native.Cwrapper.EventManager.EventManager_FetchAllResponse_GetData(this.SelfPtr(), out_arg, out_size)))).Select<IntPtr, NativeEvent>((Func<IntPtr, NativeEvent>) (ptr => new NativeEvent(ptr))).ToList<NativeEvent>();
      }

      internal bool RequestSucceeded()
      {
        return this.ResponseStatus() > ~CommonErrorStatus.ResponseStatus.ERROR_LICENSE_CHECK_FAILED;
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.EventManager.EventManager_FetchAllResponse_Dispose(selfPointer);
      }

      internal static EventManager.FetchAllResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (EventManager.FetchAllResponse) null;
        return new EventManager.FetchAllResponse(pointer);
      }
    }
  }
}
