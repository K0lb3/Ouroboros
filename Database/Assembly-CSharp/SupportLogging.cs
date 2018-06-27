// Decompiled with JetBrains decompiler
// Type: SupportLogging
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

public class SupportLogging : MonoBehaviour
{
  public bool LogTrafficStats;

  public SupportLogging()
  {
    base.\u002Ector();
  }

  public void Start()
  {
    if (!this.LogTrafficStats)
      return;
    this.InvokeRepeating("LogStats", 10f, 10f);
  }

  protected void OnApplicationPause(bool pause)
  {
    Debug.Log((object) ("SupportLogger OnApplicationPause: " + (object) pause + " connected: " + (object) PhotonNetwork.connected));
  }

  public void OnApplicationQuit()
  {
    this.CancelInvoke();
  }

  public void LogStats()
  {
    if (!this.LogTrafficStats)
      return;
    Debug.Log((object) ("SupportLogger " + PhotonNetwork.NetworkStatisticsToString()));
  }

  private void LogBasics()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("SupportLogger Info: PUN {0}: ", (object) "1.81");
    stringBuilder.AppendFormat("AppID: {0}*** GameVersion: {1} ", (object) PhotonNetwork.networkingPeer.AppId.Substring(0, 8), (object) PhotonNetwork.networkingPeer.AppVersion);
    stringBuilder.AppendFormat("Server: {0}. Region: {1} ", (object) PhotonNetwork.ServerAddress, (object) PhotonNetwork.networkingPeer.CloudRegion);
    stringBuilder.AppendFormat("HostType: {0} ", (object) PhotonNetwork.PhotonServerSettings.HostType);
    Debug.Log((object) stringBuilder.ToString());
  }

  public void OnConnectedToPhoton()
  {
    Debug.Log((object) "SupportLogger OnConnectedToPhoton().");
    this.LogBasics();
    if (!this.LogTrafficStats)
      return;
    PhotonNetwork.NetworkStatisticsEnabled = true;
  }

  public void OnFailedToConnectToPhoton(DisconnectCause cause)
  {
    Debug.Log((object) ("SupportLogger OnFailedToConnectToPhoton(" + (object) cause + ")."));
    this.LogBasics();
  }

  public void OnJoinedLobby()
  {
    Debug.Log((object) ("SupportLogger OnJoinedLobby(" + (object) PhotonNetwork.lobby + ")."));
  }

  public void OnJoinedRoom()
  {
    Debug.Log((object) ("SupportLogger OnJoinedRoom(" + (object) PhotonNetwork.room + "). " + (object) PhotonNetwork.lobby + " GameServer:" + PhotonNetwork.ServerAddress));
  }

  public void OnCreatedRoom()
  {
    Debug.Log((object) ("SupportLogger OnCreatedRoom(" + (object) PhotonNetwork.room + "). " + (object) PhotonNetwork.lobby + " GameServer:" + PhotonNetwork.ServerAddress));
  }

  public void OnLeftRoom()
  {
    Debug.Log((object) "SupportLogger OnLeftRoom().");
  }

  public void OnDisconnectedFromPhoton()
  {
    Debug.Log((object) "SupportLogger OnDisconnectedFromPhoton().");
  }
}
