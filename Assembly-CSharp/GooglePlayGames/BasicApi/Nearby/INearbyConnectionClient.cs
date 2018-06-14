// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.INearbyConnectionClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace GooglePlayGames.BasicApi.Nearby
{
  public interface INearbyConnectionClient
  {
    int MaxUnreliableMessagePayloadLength();

    int MaxReliableMessagePayloadLength();

    void SendReliable(List<string> recipientEndpointIds, byte[] payload);

    void SendUnreliable(List<string> recipientEndpointIds, byte[] payload);

    void StartAdvertising(string name, List<string> appIdentifiers, TimeSpan? advertisingDuration, Action<AdvertisingResult> resultCallback, Action<ConnectionRequest> connectionRequestCallback);

    void StopAdvertising();

    void SendConnectionRequest(string name, string remoteEndpointId, byte[] payload, Action<ConnectionResponse> responseCallback, IMessageListener listener);

    void AcceptConnectionRequest(string remoteEndpointId, byte[] payload, IMessageListener listener);

    void StartDiscovery(string serviceId, TimeSpan? advertisingTimeout, IDiscoveryListener listener);

    void StopDiscovery(string serviceId);

    void RejectConnectionRequest(string requestingEndpointId);

    void DisconnectFromEndpoint(string remoteEndpointId);

    void StopAllConnections();

    string LocalEndpointId();

    string LocalDeviceId();

    string GetAppBundleId();

    string GetServiceId();
  }
}
