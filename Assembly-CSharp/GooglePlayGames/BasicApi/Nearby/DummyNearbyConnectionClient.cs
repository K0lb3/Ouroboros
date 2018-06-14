// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.DummyNearbyConnectionClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace GooglePlayGames.BasicApi.Nearby
{
  public class DummyNearbyConnectionClient : INearbyConnectionClient
  {
    public int MaxUnreliableMessagePayloadLength()
    {
      return 1168;
    }

    public int MaxReliableMessagePayloadLength()
    {
      return 4096;
    }

    public void SendReliable(List<string> recipientEndpointIds, byte[] payload)
    {
      Debug.LogError((object) "SendReliable called from dummy implementation");
    }

    public void SendUnreliable(List<string> recipientEndpointIds, byte[] payload)
    {
      Debug.LogError((object) "SendUnreliable called from dummy implementation");
    }

    public void StartAdvertising(string name, List<string> appIdentifiers, TimeSpan? advertisingDuration, Action<AdvertisingResult> resultCallback, Action<ConnectionRequest> connectionRequestCallback)
    {
      AdvertisingResult advertisingResult = new AdvertisingResult(ResponseStatus.LicenseCheckFailed, string.Empty);
      resultCallback(advertisingResult);
    }

    public void StopAdvertising()
    {
      Debug.LogError((object) "StopAvertising in dummy implementation called");
    }

    public void SendConnectionRequest(string name, string remoteEndpointId, byte[] payload, Action<ConnectionResponse> responseCallback, IMessageListener listener)
    {
      Debug.LogError((object) "SendConnectionRequest called from dummy implementation");
      if (responseCallback == null)
        return;
      ConnectionResponse connectionResponse = ConnectionResponse.Rejected(0L, string.Empty);
      responseCallback(connectionResponse);
    }

    public void AcceptConnectionRequest(string remoteEndpointId, byte[] payload, IMessageListener listener)
    {
      Debug.LogError((object) "AcceptConnectionRequest in dummy implementation called");
    }

    public void StartDiscovery(string serviceId, TimeSpan? advertisingTimeout, IDiscoveryListener listener)
    {
      Debug.LogError((object) "StartDiscovery in dummy implementation called");
    }

    public void StopDiscovery(string serviceId)
    {
      Debug.LogError((object) "StopDiscovery in dummy implementation called");
    }

    public void RejectConnectionRequest(string requestingEndpointId)
    {
      Debug.LogError((object) "RejectConnectionRequest in dummy implementation called");
    }

    public void DisconnectFromEndpoint(string remoteEndpointId)
    {
      Debug.LogError((object) "DisconnectFromEndpoint in dummy implementation called");
    }

    public void StopAllConnections()
    {
      Debug.LogError((object) "StopAllConnections in dummy implementation called");
    }

    public string LocalEndpointId()
    {
      return string.Empty;
    }

    public string LocalDeviceId()
    {
      return "DummyDevice";
    }

    public string GetAppBundleId()
    {
      return "dummy.bundle.id";
    }

    public string GetServiceId()
    {
      return "dummy.service.id";
    }
  }
}
