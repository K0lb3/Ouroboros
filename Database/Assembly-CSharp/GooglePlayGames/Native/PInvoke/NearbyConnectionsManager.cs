// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NearbyConnectionsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using AOT;
using GooglePlayGames.Native.Cwrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace GooglePlayGames.Native.PInvoke
{
  internal class NearbyConnectionsManager : BaseReferenceHolder
  {
    private static readonly string sServiceId = NearbyConnectionsManager.ReadServiceId();

    internal NearbyConnectionsManager(IntPtr selfPointer)
      : base(selfPointer)
    {
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      NearbyConnections.NearbyConnections_Dispose(selfPointer);
    }

    internal void SendUnreliable(string remoteEndpointId, byte[] payload)
    {
      NearbyConnections.NearbyConnections_SendUnreliableMessage(this.SelfPtr(), remoteEndpointId, payload, new UIntPtr((ulong) payload.Length));
    }

    internal void SendReliable(string remoteEndpointId, byte[] payload)
    {
      NearbyConnections.NearbyConnections_SendReliableMessage(this.SelfPtr(), remoteEndpointId, payload, new UIntPtr((ulong) payload.Length));
    }

    internal void StartAdvertising(string name, List<NativeAppIdentifier> appIds, long advertisingDuration, Action<long, NativeStartAdvertisingResult> advertisingCallback, Action<long, NativeConnectionRequest> connectionRequestCallback)
    {
      NearbyConnections.NearbyConnections_StartAdvertising(this.SelfPtr(), name, appIds.Select<NativeAppIdentifier, IntPtr>((Func<NativeAppIdentifier, IntPtr>) (id => id.AsPointer())).ToArray<IntPtr>(), new UIntPtr((ulong) appIds.Count), advertisingDuration, new NearbyConnectionTypes.StartAdvertisingCallback(NearbyConnectionsManager.InternalStartAdvertisingCallback), Callbacks.ToIntPtr<long, NativeStartAdvertisingResult>(advertisingCallback, new Func<IntPtr, NativeStartAdvertisingResult>(NativeStartAdvertisingResult.FromPointer)), new NearbyConnectionTypes.ConnectionRequestCallback(NearbyConnectionsManager.InternalConnectionRequestCallback), Callbacks.ToIntPtr<long, NativeConnectionRequest>(connectionRequestCallback, new Func<IntPtr, NativeConnectionRequest>(NativeConnectionRequest.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (NearbyConnectionTypes.StartAdvertisingCallback))]
    private static void InternalStartAdvertisingCallback(long id, IntPtr result, IntPtr userData)
    {
      Callbacks.PerformInternalCallback<long>("NearbyConnectionsManager#InternalStartAdvertisingCallback", Callbacks.Type.Permanent, id, result, userData);
    }

    [MonoPInvokeCallback(typeof (NearbyConnectionTypes.ConnectionRequestCallback))]
    private static void InternalConnectionRequestCallback(long id, IntPtr result, IntPtr userData)
    {
      Callbacks.PerformInternalCallback<long>("NearbyConnectionsManager#InternalConnectionRequestCallback", Callbacks.Type.Permanent, id, result, userData);
    }

    internal void StopAdvertising()
    {
      NearbyConnections.NearbyConnections_StopAdvertising(this.SelfPtr());
    }

    internal void SendConnectionRequest(string name, string remoteEndpointId, byte[] payload, Action<long, NativeConnectionResponse> callback, NativeMessageListenerHelper listener)
    {
      NearbyConnections.NearbyConnections_SendConnectionRequest(this.SelfPtr(), name, remoteEndpointId, payload, new UIntPtr((ulong) payload.Length), new NearbyConnectionTypes.ConnectionResponseCallback(NearbyConnectionsManager.InternalConnectResponseCallback), Callbacks.ToIntPtr<long, NativeConnectionResponse>(callback, new Func<IntPtr, NativeConnectionResponse>(NativeConnectionResponse.FromPointer)), listener.AsPointer());
    }

    [MonoPInvokeCallback(typeof (NearbyConnectionTypes.ConnectionResponseCallback))]
    private static void InternalConnectResponseCallback(long localClientId, IntPtr response, IntPtr userData)
    {
      Callbacks.PerformInternalCallback<long>("NearbyConnectionManager#InternalConnectResponseCallback", Callbacks.Type.Temporary, localClientId, response, userData);
    }

    internal void AcceptConnectionRequest(string remoteEndpointId, byte[] payload, NativeMessageListenerHelper listener)
    {
      NearbyConnections.NearbyConnections_AcceptConnectionRequest(this.SelfPtr(), remoteEndpointId, payload, new UIntPtr((ulong) payload.Length), listener.AsPointer());
    }

    internal void DisconnectFromEndpoint(string remoteEndpointId)
    {
      NearbyConnections.NearbyConnections_Disconnect(this.SelfPtr(), remoteEndpointId);
    }

    internal void StopAllConnections()
    {
      NearbyConnections.NearbyConnections_Stop(this.SelfPtr());
    }

    internal void StartDiscovery(string serviceId, long duration, NativeEndpointDiscoveryListenerHelper listener)
    {
      NearbyConnections.NearbyConnections_StartDiscovery(this.SelfPtr(), serviceId, duration, listener.AsPointer());
    }

    internal void StopDiscovery(string serviceId)
    {
      NearbyConnections.NearbyConnections_StopDiscovery(this.SelfPtr(), serviceId);
    }

    internal void RejectConnectionRequest(string remoteEndpointId)
    {
      NearbyConnections.NearbyConnections_RejectConnectionRequest(this.SelfPtr(), remoteEndpointId);
    }

    internal void Shutdown()
    {
      NearbyConnections.NearbyConnections_Stop(this.SelfPtr());
    }

    internal string LocalEndpointId()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_arg, out_size) => NearbyConnections.NearbyConnections_GetLocalEndpointId(this.SelfPtr(), out_arg, out_size)));
    }

    internal string LocalDeviceId()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_arg, out_size) => NearbyConnections.NearbyConnections_GetLocalDeviceId(this.SelfPtr(), out_arg, out_size)));
    }

    public string AppBundleId
    {
      get
      {
        using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
          return (string) ((AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity")).Call<string>("getPackageName", new object[0]);
      }
    }

    internal static string ReadServiceId()
    {
      Debug.Log((object) "Initializing ServiceId property!!!!");
      using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
      {
        using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity"))
        {
          string str1 = (string) androidJavaObject.Call<string>("getPackageName", new object[0]);
          string str2 = (string) ((AndroidJavaObject) ((AndroidJavaObject) ((AndroidJavaObject) androidJavaObject.Call<AndroidJavaObject>("getPackageManager", new object[0])).Call<AndroidJavaObject>("getApplicationInfo", new object[2]{ (object) str1, (object) 128 })).Get<AndroidJavaObject>("metaData")).Call<string>("getString", new object[1]{ (object) "com.google.android.gms.nearby.connection.SERVICE_ID" });
          Debug.Log((object) ("SystemId from Manifest: " + str2));
          return str2;
        }
      }
    }

    public static string ServiceId
    {
      get
      {
        return NearbyConnectionsManager.sServiceId;
      }
    }
  }
}
