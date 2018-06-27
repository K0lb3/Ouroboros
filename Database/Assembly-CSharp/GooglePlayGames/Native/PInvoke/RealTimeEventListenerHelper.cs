// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.RealTimeEventListenerHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using AOT;
using GooglePlayGames.OurUtils;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class RealTimeEventListenerHelper : BaseReferenceHolder
  {
    internal RealTimeEventListenerHelper(IntPtr selfPointer)
      : base(selfPointer)
    {
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.RealTimeEventListenerHelper_Dispose(selfPointer);
    }

    internal RealTimeEventListenerHelper SetOnRoomStatusChangedCallback(Action<NativeRealTimeRoom> callback)
    {
      GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.RealTimeEventListenerHelper_SetOnRoomStatusChangedCallback(this.SelfPtr(), new GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.OnRoomStatusChangedCallback(RealTimeEventListenerHelper.InternalOnRoomStatusChangedCallback), RealTimeEventListenerHelper.ToCallbackPointer(callback));
      return this;
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.OnRoomStatusChangedCallback))]
    internal static void InternalOnRoomStatusChangedCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("RealTimeEventListenerHelper#InternalOnRoomStatusChangedCallback", Callbacks.Type.Permanent, response, data);
    }

    internal RealTimeEventListenerHelper SetOnRoomConnectedSetChangedCallback(Action<NativeRealTimeRoom> callback)
    {
      GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.RealTimeEventListenerHelper_SetOnRoomConnectedSetChangedCallback(this.SelfPtr(), new GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.OnRoomConnectedSetChangedCallback(RealTimeEventListenerHelper.InternalOnRoomConnectedSetChangedCallback), RealTimeEventListenerHelper.ToCallbackPointer(callback));
      return this;
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.OnRoomConnectedSetChangedCallback))]
    internal static void InternalOnRoomConnectedSetChangedCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("RealTimeEventListenerHelper#InternalOnRoomConnectedSetChangedCallback", Callbacks.Type.Permanent, response, data);
    }

    internal RealTimeEventListenerHelper SetOnP2PConnectedCallback(Action<NativeRealTimeRoom, MultiplayerParticipant> callback)
    {
      GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.RealTimeEventListenerHelper_SetOnP2PConnectedCallback(this.SelfPtr(), new GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.OnP2PConnectedCallback(RealTimeEventListenerHelper.InternalOnP2PConnectedCallback), Callbacks.ToIntPtr((Delegate) callback));
      return this;
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.OnP2PConnectedCallback))]
    internal static void InternalOnP2PConnectedCallback(IntPtr room, IntPtr participant, IntPtr data)
    {
      RealTimeEventListenerHelper.PerformRoomAndParticipantCallback(nameof (InternalOnP2PConnectedCallback), room, participant, data);
    }

    internal RealTimeEventListenerHelper SetOnP2PDisconnectedCallback(Action<NativeRealTimeRoom, MultiplayerParticipant> callback)
    {
      GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.RealTimeEventListenerHelper_SetOnP2PDisconnectedCallback(this.SelfPtr(), new GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.OnP2PDisconnectedCallback(RealTimeEventListenerHelper.InternalOnP2PDisconnectedCallback), Callbacks.ToIntPtr((Delegate) callback));
      return this;
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.OnP2PDisconnectedCallback))]
    internal static void InternalOnP2PDisconnectedCallback(IntPtr room, IntPtr participant, IntPtr data)
    {
      RealTimeEventListenerHelper.PerformRoomAndParticipantCallback(nameof (InternalOnP2PDisconnectedCallback), room, participant, data);
    }

    internal RealTimeEventListenerHelper SetOnParticipantStatusChangedCallback(Action<NativeRealTimeRoom, MultiplayerParticipant> callback)
    {
      GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.RealTimeEventListenerHelper_SetOnParticipantStatusChangedCallback(this.SelfPtr(), new GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.OnParticipantStatusChangedCallback(RealTimeEventListenerHelper.InternalOnParticipantStatusChangedCallback), Callbacks.ToIntPtr((Delegate) callback));
      return this;
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.OnParticipantStatusChangedCallback))]
    internal static void InternalOnParticipantStatusChangedCallback(IntPtr room, IntPtr participant, IntPtr data)
    {
      RealTimeEventListenerHelper.PerformRoomAndParticipantCallback(nameof (InternalOnParticipantStatusChangedCallback), room, participant, data);
    }

    internal static void PerformRoomAndParticipantCallback(string callbackName, IntPtr room, IntPtr participant, IntPtr data)
    {
      Logger.d("Entering " + callbackName);
      try
      {
        NativeRealTimeRoom nativeRealTimeRoom = NativeRealTimeRoom.FromPointer(room);
        using (MultiplayerParticipant multiplayerParticipant = MultiplayerParticipant.FromPointer(participant))
        {
          Action<NativeRealTimeRoom, MultiplayerParticipant> permanentCallback = Callbacks.IntPtrToPermanentCallback<Action<NativeRealTimeRoom, MultiplayerParticipant>>(data);
          if (permanentCallback == null)
            return;
          permanentCallback(nativeRealTimeRoom, multiplayerParticipant);
        }
      }
      catch (Exception ex)
      {
        Logger.e("Error encountered executing " + callbackName + ". Smothering to avoid passing exception into Native: " + (object) ex);
      }
    }

    internal RealTimeEventListenerHelper SetOnDataReceivedCallback(Action<NativeRealTimeRoom, MultiplayerParticipant, byte[], bool> callback)
    {
      IntPtr intPtr = Callbacks.ToIntPtr((Delegate) callback);
      Logger.d("OnData Callback has addr: " + (object) intPtr.ToInt64());
      GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.RealTimeEventListenerHelper_SetOnDataReceivedCallback(this.SelfPtr(), new GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.OnDataReceivedCallback(RealTimeEventListenerHelper.InternalOnDataReceived), intPtr);
      return this;
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.OnDataReceivedCallback))]
    internal static void InternalOnDataReceived(IntPtr room, IntPtr participant, IntPtr data, UIntPtr dataLength, bool isReliable, IntPtr userData)
    {
      Logger.d("Entering InternalOnDataReceived: " + (object) userData.ToInt64());
      Action<NativeRealTimeRoom, MultiplayerParticipant, byte[], bool> permanentCallback = Callbacks.IntPtrToPermanentCallback<Action<NativeRealTimeRoom, MultiplayerParticipant, byte[], bool>>(userData);
      using (NativeRealTimeRoom nativeRealTimeRoom = NativeRealTimeRoom.FromPointer(room))
      {
        using (MultiplayerParticipant multiplayerParticipant = MultiplayerParticipant.FromPointer(participant))
        {
          if (permanentCallback == null)
            return;
          byte[] destination = (byte[]) null;
          if ((long) dataLength.ToUInt64() != 0L)
          {
            destination = new byte[(IntPtr) dataLength.ToUInt32()];
            Marshal.Copy(data, destination, 0, (int) dataLength.ToUInt32());
          }
          try
          {
            permanentCallback(nativeRealTimeRoom, multiplayerParticipant, destination, isReliable);
          }
          catch (Exception ex)
          {
            Logger.e("Error encountered executing InternalOnDataReceived. Smothering to avoid passing exception into Native: " + (object) ex);
          }
        }
      }
    }

    private static IntPtr ToCallbackPointer(Action<NativeRealTimeRoom> callback)
    {
      return Callbacks.ToIntPtr((Delegate) (result =>
      {
        NativeRealTimeRoom nativeRealTimeRoom = NativeRealTimeRoom.FromPointer(result);
        if (callback != null)
        {
          callback(nativeRealTimeRoom);
        }
        else
        {
          if (nativeRealTimeRoom == null)
            return;
          nativeRealTimeRoom.Dispose();
        }
      }));
    }

    internal static RealTimeEventListenerHelper Create()
    {
      return new RealTimeEventListenerHelper(GooglePlayGames.Native.Cwrapper.RealTimeEventListenerHelper.RealTimeEventListenerHelper_Construct());
    }
  }
}
