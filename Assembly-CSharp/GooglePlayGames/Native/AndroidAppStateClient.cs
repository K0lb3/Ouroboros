// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.AndroidAppStateClient
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GooglePlayGames.Native
{
  internal class AndroidAppStateClient : AppStateClient
  {
    private static AndroidJavaClass AppStateManager = new AndroidJavaClass("com.google.android.gms.appstate.AppStateManager");
    private const int STATUS_OK = 0;
    private const int STATUS_STALE_DATA = 3;
    private const int STATUS_NO_DATA = 4;
    private const int STATUS_KEY_NOT_FOUND = 2002;
    private const int STATUS_CONFLICT = 2000;
    private const string ResultCallbackClassname = "com.google.android.gms.common.api.ResultCallback";
    private readonly GooglePlayGames.Native.PInvoke.GameServices mServices;

    internal AndroidAppStateClient(GooglePlayGames.Native.PInvoke.GameServices services)
    {
      this.mServices = Misc.CheckNotNull<GooglePlayGames.Native.PInvoke.GameServices>(services);
    }

    private static AndroidJavaObject GetApiClient(GooglePlayGames.Native.PInvoke.GameServices services)
    {
      return JavaUtils.JavaObjectFromPointer(InternalHooks.InternalHooks_GetApiClient(services.AsHandle()));
    }

    public void LoadState(int slot, OnStateLoadedListener listener)
    {
      Logger.d("LoadState, slot=" + (object) slot);
      using (AndroidJavaObject apiClient = AndroidAppStateClient.GetApiClient(this.mServices))
        AndroidAppStateClient.CallAppState(apiClient, "load", (AndroidJavaProxy) new AndroidAppStateClient.OnStateResultProxy(this.mServices, listener), (object) slot);
    }

    public void UpdateState(int slot, byte[] data, OnStateLoadedListener listener)
    {
      Logger.d("UpdateState, slot=" + (object) slot);
      using (AndroidJavaObject apiClient = AndroidAppStateClient.GetApiClient(this.mServices))
        ((AndroidJavaObject) AndroidAppStateClient.AppStateManager).CallStatic("update", new object[3]
        {
          (object) apiClient,
          (object) slot,
          (object) data
        });
      if (listener == null)
        return;
      PlayGamesHelperObject.RunOnGameThread((Action) (() => listener.OnStateSaved(true, slot)));
    }

    private static object[] PrependApiClient(AndroidJavaObject apiClient, params object[] args)
    {
      List<object> objectList = new List<object>();
      objectList.Add((object) apiClient);
      objectList.AddRange((IEnumerable<object>) args);
      return objectList.ToArray();
    }

    private static void CallAppState(AndroidJavaObject apiClient, string methodName, params object[] args)
    {
      ((AndroidJavaObject) AndroidAppStateClient.AppStateManager).CallStatic(methodName, AndroidAppStateClient.PrependApiClient(apiClient, args));
    }

    private static void CallAppState(AndroidJavaObject apiClient, string methodName, AndroidJavaProxy callbackProxy, params object[] args)
    {
      AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) AndroidAppStateClient.AppStateManager).CallStatic<AndroidJavaObject>(methodName, AndroidAppStateClient.PrependApiClient(apiClient, args));
      using (androidJavaObject)
        androidJavaObject.Call("setResultCallback", new object[1]
        {
          (object) callbackProxy
        });
    }

    private static int GetStatusCode(AndroidJavaObject result)
    {
      if (result == null)
        return -1;
      return (int) ((AndroidJavaObject) result.Call<AndroidJavaObject>("getStatus", new object[0])).Call<int>("getStatusCode", new object[0]);
    }

    internal static byte[] ToByteArray(AndroidJavaObject javaByteArray)
    {
      if (javaByteArray == null)
        return (byte[]) null;
      return (byte[]) AndroidJNIHelper.ConvertFromJNIArray<byte[]>(javaByteArray.GetRawObject());
    }

    private class OnStateResultProxy : AndroidJavaProxy
    {
      private readonly GooglePlayGames.Native.PInvoke.GameServices mServices;
      private readonly OnStateLoadedListener mListener;

      internal OnStateResultProxy(GooglePlayGames.Native.PInvoke.GameServices services, OnStateLoadedListener listener)
      {
        this.\u002Ector("com.google.android.gms.common.api.ResultCallback");
        this.mServices = Misc.CheckNotNull<GooglePlayGames.Native.PInvoke.GameServices>(services);
        this.mListener = listener;
      }

      private void OnStateConflict(int stateKey, string resolvedVersion, byte[] localData, byte[] serverData)
      {
        Logger.d("OnStateResultProxy.onStateConflict called, stateKey=" + (object) stateKey + ", resolvedVersion=" + resolvedVersion);
        this.debugLogData(nameof (localData), localData);
        this.debugLogData(nameof (serverData), serverData);
        if (this.mListener != null)
        {
          Logger.d("OnStateResultProxy.onStateConflict invoking conflict callback.");
          PlayGamesHelperObject.RunOnGameThread((Action) (() => this.ResolveState(stateKey, resolvedVersion, this.mListener.OnStateConflict(stateKey, localData, serverData), this.mListener)));
        }
        else
          Logger.w("No conflict callback specified! Cannot resolve cloud save conflict.");
      }

      private void ResolveState(int slot, string resolvedVersion, byte[] resolvedData, OnStateLoadedListener listener)
      {
        Logger.d(string.Format("AndroidClient.ResolveState, slot={0}, ver={1}, data={2}", (object) slot, (object) resolvedVersion, (object) resolvedData));
        using (AndroidJavaObject apiClient = AndroidAppStateClient.GetApiClient(this.mServices))
          AndroidAppStateClient.CallAppState(apiClient, "resolve", (AndroidJavaProxy) new AndroidAppStateClient.OnStateResultProxy(this.mServices, listener), (object) slot, (object) resolvedVersion, (object) resolvedData);
      }

      private void OnStateLoaded(int statusCode, int stateKey, byte[] localData)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        AndroidAppStateClient.OnStateResultProxy.\u003COnStateLoaded\u003Ec__AnonStorey104 loadedCAnonStorey104 = new AndroidAppStateClient.OnStateResultProxy.\u003COnStateLoaded\u003Ec__AnonStorey104();
        // ISSUE: reference to a compiler-generated field
        loadedCAnonStorey104.stateKey = stateKey;
        // ISSUE: reference to a compiler-generated field
        loadedCAnonStorey104.localData = localData;
        // ISSUE: reference to a compiler-generated field
        loadedCAnonStorey104.\u003C\u003Ef__this = this;
        // ISSUE: reference to a compiler-generated field
        Logger.d("OnStateResultProxy.onStateLoaded called, status " + (object) statusCode + ", stateKey=" + (object) loadedCAnonStorey104.stateKey);
        // ISSUE: reference to a compiler-generated field
        this.debugLogData(nameof (localData), loadedCAnonStorey104.localData);
        // ISSUE: reference to a compiler-generated field
        loadedCAnonStorey104.success = false;
        int num = statusCode;
        switch (num)
        {
          case 0:
            Logger.d("Status is OK, so success.");
            // ISSUE: reference to a compiler-generated field
            loadedCAnonStorey104.success = true;
            break;
          case 3:
            Logger.d("Status is STALE DATA, so considering as success.");
            // ISSUE: reference to a compiler-generated field
            loadedCAnonStorey104.success = true;
            break;
          case 4:
            Logger.d("Status is NO DATA (no network?), so it's a failure.");
            // ISSUE: reference to a compiler-generated field
            loadedCAnonStorey104.success = false;
            // ISSUE: reference to a compiler-generated field
            loadedCAnonStorey104.localData = (byte[]) null;
            break;
          default:
            if (num == 2002)
            {
              Logger.d("Status is KEY NOT FOUND, which is a success, but with no data.");
              // ISSUE: reference to a compiler-generated field
              loadedCAnonStorey104.success = true;
              // ISSUE: reference to a compiler-generated field
              loadedCAnonStorey104.localData = (byte[]) null;
              break;
            }
            Logger.e("Cloud load failed with status code " + (object) statusCode);
            // ISSUE: reference to a compiler-generated field
            loadedCAnonStorey104.success = false;
            // ISSUE: reference to a compiler-generated field
            loadedCAnonStorey104.localData = (byte[]) null;
            break;
        }
        if (this.mListener != null)
        {
          Logger.d("OnStateResultProxy.onStateLoaded invoking load callback.");
          // ISSUE: reference to a compiler-generated method
          PlayGamesHelperObject.RunOnGameThread(new Action(loadedCAnonStorey104.\u003C\u003Em__10));
        }
        else
          Logger.w("No load callback specified!");
      }

      private void debugLogData(string tag, byte[] data)
      {
        Logger.d("   " + tag + ": " + Logger.describe(data));
      }

      public void onResult(AndroidJavaObject result)
      {
        Logger.d("OnStateResultProxy.onResult, result=" + (object) result);
        int statusCode = AndroidAppStateClient.GetStatusCode(result);
        Logger.d("OnStateResultProxy: status code is " + (object) statusCode);
        if (result == null)
        {
          Logger.e("OnStateResultProxy: result is null.");
        }
        else
        {
          Logger.d("OnstateResultProxy: retrieving result objects...");
          AndroidJavaObject target1 = result.NullSafeCall("getLoadedResult");
          AndroidJavaObject target2 = result.NullSafeCall("getConflictResult");
          Logger.d("Got result objects.");
          Logger.d("loadedResult = " + (object) target1);
          Logger.d("conflictResult = " + (object) target2);
          if (target2 != null)
          {
            Logger.d("OnStateResultProxy: processing conflict.");
            int stateKey = (int) target2.Call<int>("getStateKey", new object[0]);
            string resolvedVersion = (string) target2.Call<string>("getResolvedVersion", new object[0]);
            byte[] byteArray1 = AndroidAppStateClient.ToByteArray(target2.NullSafeCall("getLocalData"));
            byte[] byteArray2 = AndroidAppStateClient.ToByteArray(target2.NullSafeCall("getServerData"));
            Logger.d("OnStateResultProxy: conflict args parsed, calling.");
            this.OnStateConflict(stateKey, resolvedVersion, byteArray1, byteArray2);
          }
          else if (target1 != null)
          {
            Logger.d("OnStateResultProxy: processing normal load.");
            int stateKey = (int) target1.Call<int>("getStateKey", new object[0]);
            byte[] byteArray = AndroidAppStateClient.ToByteArray(target1.NullSafeCall("getLocalData"));
            Logger.d("OnStateResultProxy: loaded args parsed, calling.");
            this.OnStateLoaded(statusCode, stateKey, byteArray);
          }
          else
            Logger.e("OnStateResultProxy: both loadedResult and conflictResult are null!");
        }
      }
    }
  }
}
