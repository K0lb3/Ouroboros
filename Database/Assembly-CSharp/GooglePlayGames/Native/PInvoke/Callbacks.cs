// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.Callbacks
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal static class Callbacks
  {
    internal static readonly Action<CommonErrorStatus.UIStatus> NoopUICallback = (Action<CommonErrorStatus.UIStatus>) (status => Logger.d("Received UI callback: " + (object) status));

    internal static IntPtr ToIntPtr<T>(Action<T> callback, Func<IntPtr, T> conversionFunction) where T : BaseReferenceHolder
    {
      return Callbacks.ToIntPtr((Delegate) (result =>
      {
        using (T obj = conversionFunction(result))
        {
          if (callback == null)
            return;
          callback(obj);
        }
      }));
    }

    internal static IntPtr ToIntPtr<T, P>(Action<T, P> callback, Func<IntPtr, P> conversionFunction) where P : BaseReferenceHolder
    {
      return Callbacks.ToIntPtr((Delegate) ((param1, param2) =>
      {
        using (P p = conversionFunction(param2))
        {
          if (callback == null)
            return;
          callback(param1, p);
        }
      }));
    }

    internal static IntPtr ToIntPtr(Delegate callback)
    {
      if ((object) callback == null)
        return IntPtr.Zero;
      return GCHandle.ToIntPtr(GCHandle.Alloc((object) callback));
    }

    internal static T IntPtrToTempCallback<T>(IntPtr handle) where T : class
    {
      return Callbacks.IntPtrToCallback<T>(handle, true);
    }

    private static T IntPtrToCallback<T>(IntPtr handle, bool unpinHandle) where T : class
    {
      if (PInvokeUtilities.IsNull(handle))
        return (T) null;
      GCHandle gcHandle = GCHandle.FromIntPtr(handle);
      try
      {
        return (T) gcHandle.Target;
      }
      catch (InvalidCastException ex)
      {
        Logger.e("GC Handle pointed to unexpected type: " + gcHandle.Target.ToString() + ". Expected " + (object) typeof (T));
        throw ex;
      }
      finally
      {
        if (unpinHandle)
          gcHandle.Free();
      }
    }

    internal static T IntPtrToPermanentCallback<T>(IntPtr handle) where T : class
    {
      return Callbacks.IntPtrToCallback<T>(handle, false);
    }

    [MonoPInvokeCallback(typeof (Callbacks.ShowUICallbackInternal))]
    internal static void InternalShowUICallback(CommonErrorStatus.UIStatus status, IntPtr data)
    {
      Logger.d("Showing UI Internal callback: " + (object) status);
      Action<CommonErrorStatus.UIStatus> tempCallback = Callbacks.IntPtrToTempCallback<Action<CommonErrorStatus.UIStatus>>(data);
      try
      {
        tempCallback(status);
      }
      catch (Exception ex)
      {
        Logger.e("Error encountered executing InternalShowAllUICallback. Smothering to avoid passing exception into Native: " + (object) ex);
      }
    }

    internal static void PerformInternalCallback(string callbackName, Callbacks.Type callbackType, IntPtr response, IntPtr userData)
    {
      Logger.d("Entering internal callback for " + callbackName);
      Action<IntPtr> action = callbackType != Callbacks.Type.Permanent ? Callbacks.IntPtrToTempCallback<Action<IntPtr>>(userData) : Callbacks.IntPtrToPermanentCallback<Action<IntPtr>>(userData);
      if (action == null)
        return;
      try
      {
        action(response);
      }
      catch (Exception ex)
      {
        Logger.e("Error encountered executing " + callbackName + ". Smothering to avoid passing exception into Native: " + (object) ex);
      }
    }

    internal static void PerformInternalCallback<T>(string callbackName, Callbacks.Type callbackType, T param1, IntPtr param2, IntPtr userData)
    {
      Logger.d("Entering internal callback for " + callbackName);
      Action<T, IntPtr> action;
      try
      {
        action = callbackType != Callbacks.Type.Permanent ? Callbacks.IntPtrToTempCallback<Action<T, IntPtr>>(userData) : Callbacks.IntPtrToPermanentCallback<Action<T, IntPtr>>(userData);
      }
      catch (Exception ex)
      {
        Logger.e("Error encountered converting " + callbackName + ". Smothering to avoid passing exception into Native: " + (object) ex);
        return;
      }
      Logger.d("Internal Callback converted to action");
      if (action == null)
        return;
      try
      {
        action(param1, param2);
      }
      catch (Exception ex)
      {
        Logger.e("Error encountered executing " + callbackName + ". Smothering to avoid passing exception into Native: " + (object) ex);
      }
    }

    internal static Action<T> AsOnGameThreadCallback<T>(Action<T> toInvokeOnGameThread)
    {
      return (Action<T>) (result =>
      {
        if (toInvokeOnGameThread == null)
          return;
        PlayGamesHelperObject.RunOnGameThread((Action) (() => toInvokeOnGameThread(result)));
      });
    }

    internal static Action<T1, T2> AsOnGameThreadCallback<T1, T2>(Action<T1, T2> toInvokeOnGameThread)
    {
      return (Action<T1, T2>) ((result1, result2) =>
      {
        if (toInvokeOnGameThread == null)
          return;
        PlayGamesHelperObject.RunOnGameThread((Action) (() => toInvokeOnGameThread(result1, result2)));
      });
    }

    internal static void AsCoroutine(IEnumerator routine)
    {
      PlayGamesHelperObject.RunCoroutine(routine);
    }

    internal static byte[] IntPtrAndSizeToByteArray(IntPtr data, UIntPtr dataLength)
    {
      if ((long) dataLength.ToUInt64() == 0L)
        return (byte[]) null;
      byte[] destination = new byte[(IntPtr) dataLength.ToUInt32()];
      Marshal.Copy(data, destination, 0, (int) dataLength.ToUInt32());
      return destination;
    }

    internal enum Type
    {
      Permanent,
      Temporary,
    }

    internal delegate void ShowUICallbackInternal(CommonErrorStatus.UIStatus status, IntPtr data);
  }
}
