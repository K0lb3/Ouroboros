// Decompiled with JetBrains decompiler
// Type: ConnectAndJoinRandom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Photon;
using UnityEngine;

public class ConnectAndJoinRandom : MonoBehaviour
{
  public bool AutoConnect = true;
  public byte Version = 1;
  private bool ConnectInUpdate = true;

  public virtual void Start()
  {
    PhotonNetwork.autoJoinLobby = false;
  }

  public virtual void Update()
  {
    if (!this.ConnectInUpdate || !this.AutoConnect || PhotonNetwork.connected)
      return;
    Debug.Log((object) "Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");
    this.ConnectInUpdate = false;
    PhotonNetwork.ConnectUsingSettings(((int) this.Version).ToString() + "." + (object) SceneManagerHelper.ActiveSceneBuildIndex);
  }

  public virtual void OnConnectedToMaster()
  {
    Debug.Log((object) "OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
    PhotonNetwork.JoinRandomRoom();
  }

  public virtual void OnJoinedLobby()
  {
    Debug.Log((object) "OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
    PhotonNetwork.JoinRandomRoom();
  }

  public virtual void OnPhotonRandomJoinFailed()
  {
    Debug.Log((object) "OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
    PhotonNetwork.CreateRoom((string) null, new RoomOptions()
    {
      MaxPlayers = (byte) 4
    }, (TypedLobby) null);
  }

  public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
  {
    Debug.LogError((object) ("Cause: " + (object) cause));
  }

  public void OnJoinedRoom()
  {
    Debug.Log((object) "OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");
  }
}
