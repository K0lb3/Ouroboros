// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeMessageListenerHelper
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class NativeMessageListenerHelper : BaseReferenceHolder
  {
    internal NativeMessageListenerHelper()
      : base(MessageListenerHelper.MessageListenerHelper_Construct())
    {
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      MessageListenerHelper.MessageListenerHelper_Dispose(selfPointer);
    }

    internal void SetOnMessageReceivedCallback(NativeMessageListenerHelper.OnMessageReceived callback)
    {
      MessageListenerHelper.MessageListenerHelper_SetOnMessageReceivedCallback(this.SelfPtr(), new MessageListenerHelper.OnMessageReceivedCallback(NativeMessageListenerHelper.InternalOnMessageReceivedCallback), Callbacks.ToIntPtr((Delegate) callback));
    }

    [MonoPInvokeCallback(typeof (MessageListenerHelper.OnMessageReceivedCallback))]
    private static void InternalOnMessageReceivedCallback(long id, string name, IntPtr data, UIntPtr dataLength, bool isReliable, IntPtr userData)
    {
      NativeMessageListenerHelper.OnMessageReceived permanentCallback = Callbacks.IntPtrToPermanentCallback<NativeMessageListenerHelper.OnMessageReceived>(userData);
      if (permanentCallback == null)
        return;
      try
      {
        permanentCallback(id, name, Callbacks.IntPtrAndSizeToByteArray(data, dataLength), isReliable);
      }
      catch (Exception ex)
      {
        Logger.e("Error encountered executing NativeMessageListenerHelper#InternalOnMessageReceivedCallback. Smothering to avoid passing exception into Native: " + (object) ex);
      }
    }

    internal void SetOnDisconnectedCallback(Action<long, string> callback)
    {
      MessageListenerHelper.MessageListenerHelper_SetOnDisconnectedCallback(this.SelfPtr(), new MessageListenerHelper.OnDisconnectedCallback(NativeMessageListenerHelper.InternalOnDisconnectedCallback), Callbacks.ToIntPtr((Delegate) callback));
    }

    [MonoPInvokeCallback(typeof (MessageListenerHelper.OnDisconnectedCallback))]
    private static void InternalOnDisconnectedCallback(long id, string lostEndpointId, IntPtr userData)
    {
      Action<long, string> permanentCallback = Callbacks.IntPtrToPermanentCallback<Action<long, string>>(userData);
      if (permanentCallback == null)
        return;
      try
      {
        permanentCallback(id, lostEndpointId);
      }
      catch (Exception ex)
      {
        Logger.e("Error encountered executing NativeMessageListenerHelper#InternalOnDisconnectedCallback. Smothering to avoid passing exception into Native: " + (object) ex);
      }
    }

    internal delegate void OnMessageReceived(long localClientId, string remoteEndpointId, byte[] data, bool isReliable);
  }
}
