// Decompiled with JetBrains decompiler
// Type: NetworkingPeer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

internal class NetworkingPeer : LoadBalancingPeer, IPhotonPeerListener
{
  protected internal List<TypedLobbyInfo> LobbyStatistics = new List<TypedLobbyInfo>();
  public Dictionary<string, RoomInfo> mGameList = new Dictionary<string, RoomInfo>();
  public RoomInfo[] mGameListCopy = new RoomInfo[0];
  private string playername = string.Empty;
  public Dictionary<int, PhotonPlayer> mActors = new Dictionary<int, PhotonPlayer>();
  public PhotonPlayer[] mOtherPlayerListCopy = new PhotonPlayer[0];
  public PhotonPlayer[] mPlayerListCopy = new PhotonPlayer[0];
  private HashSet<int> allowedReceivingGroups = new HashSet<int>();
  private HashSet<int> blockSendingGroups = new HashSet<int>();
  protected internal Dictionary<int, PhotonView> photonViewList = new Dictionary<int, PhotonView>();
  private readonly PhotonStream readStream = new PhotonStream(false, (object[]) null);
  private readonly PhotonStream pStream = new PhotonStream(true, (object[]) null);
  private readonly Dictionary<int, Hashtable> dataPerGroupReliable = new Dictionary<int, Hashtable>();
  private readonly Dictionary<int, Hashtable> dataPerGroupUnreliable = new Dictionary<int, Hashtable>();
  private Dictionary<System.Type, List<MethodInfo>> monoRPCMethodsCache = new Dictionary<System.Type, List<MethodInfo>>();
  private Dictionary<int, object[]> tempInstantiationData = new Dictionary<int, object[]>();
  public const string NameServerHost = "ns.exitgames.com";
  public const string NameServerHttp = "http://ns.exitgamescloud.com:80/photon/n";
  protected internal const string CurrentSceneProperty = "curScn";
  public const int SyncViewId = 0;
  public const int SyncCompressed = 1;
  public const int SyncNullValues = 2;
  public const int SyncFirstValue = 3;
  protected internal string AppId;
  private string tokenCache;
  public AuthModeOption AuthMode;
  public EncryptionMode EncryptionMode;
  private static readonly Dictionary<ConnectionProtocol, int> ProtocolToNameServerPort;
  public bool IsInitialConnect;
  public bool insideLobby;
  private bool mPlayernameHasToBeUpdated;
  private Room currentRoom;
  private JoinType lastJoinType;
  protected internal EnterRoomParams enterRoomParamsCache;
  private bool didAuthenticate;
  private string[] friendListRequested;
  private int friendListTimestamp;
  private bool isFetchingFriendList;
  public bool hasSwitchedMC;
  protected internal short currentLevelPrefix;
  protected internal bool loadingLevelAndPausedNetwork;
  public static bool UsePrefabCache;
  internal IPunPrefabPool ObjectPool;
  public static Dictionary<string, GameObject> PrefabCache;
  private readonly Dictionary<string, int> rpcShortcuts;
  private static readonly string OnPhotonInstantiateString;
  public static int ObjectsInOneUpdate;

  public NetworkingPeer(string playername, ConnectionProtocol connectionProtocol)
    : base(connectionProtocol)
  {
    this.set_Listener((IPhotonPeerListener) this);
    this.set_LimitOfUnreliableCommands(40);
    this.lobby = TypedLobby.Default;
    this.PlayerName = playername;
    this.LocalPlayer = new PhotonPlayer(true, -1, this.playername);
    this.AddNewPlayer(this.LocalPlayer.ID, this.LocalPlayer);
    this.rpcShortcuts = new Dictionary<string, int>(PhotonNetwork.PhotonServerSettings.RpcList.Count);
    for (int index = 0; index < PhotonNetwork.PhotonServerSettings.RpcList.Count; ++index)
      this.rpcShortcuts[PhotonNetwork.PhotonServerSettings.RpcList[index]] = index;
    this.State = ClientState.PeerCreated;
  }

  static NetworkingPeer()
  {
    Dictionary<ConnectionProtocol, int> dictionary = new Dictionary<ConnectionProtocol, int>();
    dictionary.Add((ConnectionProtocol) 0, 5058);
    dictionary.Add((ConnectionProtocol) 1, 4533);
    dictionary.Add((ConnectionProtocol) 4, 9093);
    dictionary.Add((ConnectionProtocol) 5, 19093);
    NetworkingPeer.ProtocolToNameServerPort = dictionary;
    NetworkingPeer.UsePrefabCache = true;
    NetworkingPeer.PrefabCache = new Dictionary<string, GameObject>();
    NetworkingPeer.OnPhotonInstantiateString = PhotonNetworkingMessage.OnPhotonInstantiate.ToString();
    NetworkingPeer.ObjectsInOneUpdate = 10;
  }

  protected internal string AppVersion
  {
    get
    {
      return string.Format("{0}_{1}", (object) PhotonNetwork.gameVersion, (object) "1.81");
    }
  }

  public AuthenticationValues AuthValues { get; set; }

  private string TokenForInit
  {
    get
    {
      if (this.AuthMode == AuthModeOption.Auth)
        return (string) null;
      if (this.AuthValues != null)
        return this.AuthValues.Token;
      return (string) null;
    }
  }

  public bool IsUsingNameServer { get; protected internal set; }

  public string NameServerAddress
  {
    get
    {
      return this.GetNameServerAddress();
    }
  }

  public string MasterServerAddress { get; protected internal set; }

  public string GameServerAddress { get; protected internal set; }

  protected internal ServerConnection Server { get; private set; }

  public ClientState State { get; internal set; }

  public TypedLobby lobby { get; set; }

  private bool requestLobbyStatistics
  {
    get
    {
      if (PhotonNetwork.EnableLobbyStatistics)
        return this.Server == ServerConnection.MasterServer;
      return false;
    }
  }

  public string PlayerName
  {
    get
    {
      return this.playername;
    }
    set
    {
      if (string.IsNullOrEmpty(value) || value.Equals(this.playername))
        return;
      if (this.LocalPlayer != null)
        this.LocalPlayer.NickName = value;
      this.playername = value;
      if (this.CurrentRoom == null)
        return;
      this.SendPlayerName();
    }
  }

  public Room CurrentRoom
  {
    get
    {
      if (this.currentRoom != null && this.currentRoom.IsLocalClientInside)
        return this.currentRoom;
      return (Room) null;
    }
    private set
    {
      this.currentRoom = value;
    }
  }

  public PhotonPlayer LocalPlayer { get; internal set; }

  public int PlayersOnMasterCount { get; internal set; }

  public int PlayersInRoomsCount { get; internal set; }

  public int RoomsCount { get; internal set; }

  protected internal int FriendListAge
  {
    get
    {
      if (this.isFetchingFriendList || this.friendListTimestamp == 0)
        return 0;
      return Environment.TickCount - this.friendListTimestamp;
    }
  }

  public bool IsAuthorizeSecretAvailable
  {
    get
    {
      if (this.AuthValues != null)
        return !string.IsNullOrEmpty(this.AuthValues.Token);
      return false;
    }
  }

  public List<Region> AvailableRegions { get; protected internal set; }

  public CloudRegionCode CloudRegion { get; protected internal set; }

  public int mMasterClientId
  {
    get
    {
      if (PhotonNetwork.offlineMode)
        return this.LocalPlayer.ID;
      if (this.CurrentRoom == null)
        return 0;
      return this.CurrentRoom.MasterClientId;
    }
    private set
    {
      if (this.CurrentRoom == null)
        return;
      this.CurrentRoom.MasterClientId = value;
    }
  }

  private string GetNameServerAddress()
  {
    ConnectionProtocol transportProtocol = this.get_TransportProtocol();
    int num = 0;
    NetworkingPeer.ProtocolToNameServerPort.TryGetValue(transportProtocol, out num);
    string str = string.Empty;
    if (transportProtocol == 4)
      str = "ws://";
    else if (transportProtocol == 5)
      str = "wss://";
    return string.Format("{0}{1}:{2}", (object) str, (object) "ns.exitgames.com", (object) num);
  }

  public virtual bool Connect(string serverAddress, string applicationName)
  {
    Debug.LogError((object) "Avoid using this directly. Thanks.");
    return false;
  }

  public bool ReconnectToMaster()
  {
    if (this.AuthValues == null)
    {
      Debug.LogWarning((object) "ReconnectToMaster() with AuthValues == null is not correct!");
      this.AuthValues = new AuthenticationValues();
    }
    this.AuthValues.Token = this.tokenCache;
    return this.Connect(this.MasterServerAddress, ServerConnection.MasterServer);
  }

  public bool ReconnectAndRejoin()
  {
    if (this.AuthValues == null)
    {
      Debug.LogWarning((object) "ReconnectAndRejoin() with AuthValues == null is not correct!");
      this.AuthValues = new AuthenticationValues();
    }
    this.AuthValues.Token = this.tokenCache;
    if (string.IsNullOrEmpty(this.GameServerAddress) || this.enterRoomParamsCache == null)
      return false;
    this.lastJoinType = JoinType.JoinRoom;
    this.enterRoomParamsCache.RejoinOnly = true;
    return this.Connect(this.GameServerAddress, ServerConnection.GameServer);
  }

  public bool Connect(string serverAddress, ServerConnection type)
  {
    if (PhotonHandler.AppQuits)
    {
      Debug.LogWarning((object) "Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
      return false;
    }
    if (this.State == ClientState.Disconnecting)
    {
      Debug.LogError((object) ("Connect() failed. Can't connect while disconnecting (still). Current state: " + (object) PhotonNetwork.connectionStateDetailed));
      return false;
    }
    this.SetupProtocol(type);
    bool flag = this.Connect(serverAddress, string.Empty, (object) this.TokenForInit);
    if (flag)
    {
      switch (type)
      {
        case ServerConnection.MasterServer:
          this.State = ClientState.ConnectingToMasterserver;
          break;
        case ServerConnection.GameServer:
          this.State = ClientState.ConnectingToGameserver;
          break;
        case ServerConnection.NameServer:
          this.State = ClientState.ConnectingToNameServer;
          break;
      }
    }
    return flag;
  }

  public bool ConnectToNameServer()
  {
    if (PhotonHandler.AppQuits)
    {
      Debug.LogWarning((object) "Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
      return false;
    }
    this.IsUsingNameServer = true;
    this.CloudRegion = CloudRegionCode.none;
    if (this.State == ClientState.ConnectedToNameServer)
      return true;
    this.SetupProtocol(ServerConnection.NameServer);
    if (!this.Connect(this.NameServerAddress, "ns", (object) this.TokenForInit))
      return false;
    this.State = ClientState.ConnectingToNameServer;
    return true;
  }

  public bool ConnectToRegionMaster(CloudRegionCode region)
  {
    if (PhotonHandler.AppQuits)
    {
      Debug.LogWarning((object) "Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
      return false;
    }
    this.IsUsingNameServer = true;
    this.CloudRegion = region;
    if (this.State == ClientState.ConnectedToNameServer)
      return this.CallAuthenticate();
    this.SetupProtocol(ServerConnection.NameServer);
    if (!this.Connect(this.NameServerAddress, "ns", (object) this.TokenForInit))
      return false;
    this.State = ClientState.ConnectingToNameServer;
    return true;
  }

  protected internal void SetupProtocol(ServerConnection serverType)
  {
    ConnectionProtocol connectionProtocol = this.get_TransportProtocol();
    if (this.AuthMode == AuthModeOption.AuthOnceWss)
    {
      if (serverType != ServerConnection.NameServer)
      {
        if (PhotonNetwork.logLevel >= PhotonLogLevel.ErrorsOnly)
          Debug.LogWarning((object) ("Using PhotonServerSettings.Protocol when leaving the NameServer (AuthMode is AuthOnceWss): " + (object) PhotonNetwork.PhotonServerSettings.Protocol));
        connectionProtocol = PhotonNetwork.PhotonServerSettings.Protocol;
      }
      else
      {
        if (PhotonNetwork.logLevel >= PhotonLogLevel.ErrorsOnly)
          Debug.LogWarning((object) "Using WebSocket to connect NameServer (AuthMode is AuthOnceWss).");
        connectionProtocol = (ConnectionProtocol) 5;
      }
    }
    System.Type type = System.Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp", false);
    if ((object) type == null)
      type = System.Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp-firstpass", false);
    if ((object) type != null)
    {
      ((Dictionary<ConnectionProtocol, System.Type>) this.SocketImplementationConfig)[(ConnectionProtocol) 4] = type;
      ((Dictionary<ConnectionProtocol, System.Type>) this.SocketImplementationConfig)[(ConnectionProtocol) 5] = type;
    }
    if ((object) PhotonHandler.PingImplementation == null)
      PhotonHandler.PingImplementation = typeof (PingMono);
    if (this.get_TransportProtocol() == connectionProtocol)
      return;
    if (PhotonNetwork.logLevel >= PhotonLogLevel.ErrorsOnly)
      Debug.LogWarning((object) ("Protocol switch from: " + (object) this.get_TransportProtocol() + " to: " + (object) connectionProtocol + "."));
    this.set_TransportProtocol(connectionProtocol);
  }

  public virtual void Disconnect()
  {
    if (this.get_PeerState() == null)
    {
      if (PhotonHandler.AppQuits)
        return;
      Debug.LogWarning((object) string.Format("Can't execute Disconnect() while not connected. Nothing changed. State: {0}", (object) this.State));
    }
    else
    {
      this.State = ClientState.Disconnecting;
      base.Disconnect();
    }
  }

  private bool CallAuthenticate()
  {
    AuthenticationValues authenticationValues = this.AuthValues;
    if (authenticationValues == null)
      authenticationValues = new AuthenticationValues()
      {
        UserId = this.PlayerName
      };
    AuthenticationValues authValues = authenticationValues;
    if (this.AuthMode == AuthModeOption.Auth)
      return this.OpAuthenticate(this.AppId, this.AppVersion, authValues, this.CloudRegion.ToString(), this.requestLobbyStatistics);
    return this.OpAuthenticateOnce(this.AppId, this.AppVersion, authValues, this.CloudRegion.ToString(), this.EncryptionMode, PhotonNetwork.PhotonServerSettings.Protocol);
  }

  private void DisconnectToReconnect()
  {
    switch (this.Server)
    {
      case ServerConnection.MasterServer:
        this.State = ClientState.DisconnectingFromMasterserver;
        base.Disconnect();
        break;
      case ServerConnection.GameServer:
        this.State = ClientState.DisconnectingFromGameserver;
        base.Disconnect();
        break;
      case ServerConnection.NameServer:
        this.State = ClientState.DisconnectingFromNameServer;
        base.Disconnect();
        break;
    }
  }

  public bool GetRegions()
  {
    if (this.Server != ServerConnection.NameServer)
      return false;
    bool regions = this.OpGetRegions(this.AppId);
    if (regions)
      this.AvailableRegions = (List<Region>) null;
    return regions;
  }

  public override bool OpFindFriends(string[] friendsToFind)
  {
    if (this.isFetchingFriendList)
      return false;
    this.friendListRequested = friendsToFind;
    this.isFetchingFriendList = true;
    return base.OpFindFriends(friendsToFind);
  }

  public bool OpCreateGame(EnterRoomParams enterRoomParams)
  {
    bool flag = this.Server == ServerConnection.GameServer;
    enterRoomParams.OnGameServer = flag;
    enterRoomParams.PlayerProperties = this.GetLocalActorProperties();
    if (!flag)
      this.enterRoomParamsCache = enterRoomParams;
    this.lastJoinType = JoinType.CreateRoom;
    return this.OpCreateRoom(enterRoomParams);
  }

  public override bool OpJoinRoom(EnterRoomParams opParams)
  {
    bool flag = this.Server == ServerConnection.GameServer;
    opParams.OnGameServer = flag;
    if (!flag)
      this.enterRoomParamsCache = opParams;
    this.lastJoinType = !opParams.CreateIfNotExists ? JoinType.JoinRoom : JoinType.JoinOrCreateRoom;
    return base.OpJoinRoom(opParams);
  }

  public override bool OpJoinRandomRoom(OpJoinRandomRoomParams opJoinRandomRoomParams)
  {
    this.enterRoomParamsCache = new EnterRoomParams();
    this.enterRoomParamsCache.Lobby = opJoinRandomRoomParams.TypedLobby;
    this.enterRoomParamsCache.ExpectedUsers = opJoinRandomRoomParams.ExpectedUsers;
    this.lastJoinType = JoinType.JoinRandomRoom;
    return base.OpJoinRandomRoom(opJoinRandomRoomParams);
  }

  public virtual bool OpLeave()
  {
    if (this.State == ClientState.Joined)
      return this.OpCustom((byte) 254, (Dictionary<byte, object>) null, true, (byte) 0);
    Debug.LogWarning((object) ("Not sending leave operation. State is not 'Joined': " + (object) this.State));
    return false;
  }

  public override bool OpRaiseEvent(byte eventCode, object customEventContent, bool sendReliable, RaiseEventOptions raiseEventOptions)
  {
    if (PhotonNetwork.offlineMode)
      return false;
    return base.OpRaiseEvent(eventCode, customEventContent, sendReliable, raiseEventOptions);
  }

  private void ReadoutProperties(Hashtable gameProperties, Hashtable pActorProperties, int targetActorNr)
  {
    if (pActorProperties != null && ((Dictionary<object, object>) pActorProperties).Count > 0)
    {
      if (targetActorNr > 0)
      {
        PhotonPlayer playerWithId = this.GetPlayerWithId(targetActorNr);
        if (playerWithId != null)
        {
          Hashtable properties = this.ReadoutPropertiesForActorNr(pActorProperties, targetActorNr);
          playerWithId.InternalCacheProperties(properties);
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, (object) playerWithId, (object) properties);
        }
      }
      else
      {
        using (Dictionary<object, object>.KeyCollection.Enumerator enumerator = ((Dictionary<object, object>) pActorProperties).Keys.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            object current = enumerator.Current;
            int num = (int) current;
            Hashtable properties = (Hashtable) pActorProperties.get_Item(current);
            string name = (string) properties.get_Item((object) byte.MaxValue);
            PhotonPlayer player = this.GetPlayerWithId(num);
            if (player == null)
            {
              player = new PhotonPlayer(false, num, name);
              this.AddNewPlayer(num, player);
            }
            player.InternalCacheProperties(properties);
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, (object) player, (object) properties);
          }
        }
      }
    }
    if (this.CurrentRoom == null || gameProperties == null)
      return;
    this.CurrentRoom.InternalCacheProperties(gameProperties);
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCustomRoomPropertiesChanged, (object) gameProperties);
    if (!PhotonNetwork.automaticallySyncScene)
      return;
    this.LoadLevelIfSynced();
  }

  private Hashtable ReadoutPropertiesForActorNr(Hashtable actorProperties, int actorNr)
  {
    if (((Dictionary<object, object>) actorProperties).ContainsKey((object) actorNr))
      return (Hashtable) actorProperties.get_Item((object) actorNr);
    return actorProperties;
  }

  public void ChangeLocalID(int newID)
  {
    if (this.LocalPlayer == null)
      Debug.LogWarning((object) string.Format("LocalPlayer is null or not in mActors! LocalPlayer: {0} mActors==null: {1} newID: {2}", (object) this.LocalPlayer, (object) (this.mActors == null), (object) newID));
    if (this.mActors.ContainsKey(this.LocalPlayer.ID))
      this.mActors.Remove(this.LocalPlayer.ID);
    this.LocalPlayer.InternalChangeLocalID(newID);
    this.mActors[this.LocalPlayer.ID] = this.LocalPlayer;
    this.RebuildPlayerListCopies();
  }

  private void LeftLobbyCleanup()
  {
    this.mGameList = new Dictionary<string, RoomInfo>();
    this.mGameListCopy = new RoomInfo[0];
    if (!this.insideLobby)
      return;
    this.insideLobby = false;
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnLeftLobby);
  }

  private void LeftRoomCleanup()
  {
    bool flag1 = this.CurrentRoom != null;
    bool flag2 = this.CurrentRoom == null ? PhotonNetwork.autoCleanUpPlayerObjects : this.CurrentRoom.AutoCleanUp;
    this.hasSwitchedMC = false;
    this.CurrentRoom = (Room) null;
    this.mActors = new Dictionary<int, PhotonPlayer>();
    this.mPlayerListCopy = new PhotonPlayer[0];
    this.mOtherPlayerListCopy = new PhotonPlayer[0];
    this.allowedReceivingGroups = new HashSet<int>();
    this.blockSendingGroups = new HashSet<int>();
    this.mGameList = new Dictionary<string, RoomInfo>();
    this.mGameListCopy = new RoomInfo[0];
    this.isFetchingFriendList = false;
    this.ChangeLocalID(-1);
    if (flag2)
    {
      this.LocalCleanupAnythingInstantiated(true);
      PhotonNetwork.manuallyAllocatedViewIds = new List<int>();
    }
    if (!flag1)
      return;
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnLeftRoom);
  }

  protected internal void LocalCleanupAnythingInstantiated(bool destroyInstantiatedGameObjects)
  {
    if (this.tempInstantiationData.Count > 0)
      Debug.LogWarning((object) "It seems some instantiation is not completed, as instantiation data is used. You should make sure instantiations are paused when calling this method. Cleaning now, despite this.");
    if (destroyInstantiatedGameObjects)
    {
      HashSet<GameObject> gameObjectSet = new HashSet<GameObject>();
      using (Dictionary<int, PhotonView>.ValueCollection.Enumerator enumerator = this.photonViewList.Values.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          PhotonView current = enumerator.Current;
          if (current.isRuntimeInstantiated)
            gameObjectSet.Add(((Component) current).get_gameObject());
        }
      }
      using (HashSet<GameObject>.Enumerator enumerator = gameObjectSet.GetEnumerator())
      {
        while (enumerator.MoveNext())
          this.RemoveInstantiatedGO(enumerator.Current, true);
      }
    }
    this.tempInstantiationData.Clear();
    PhotonNetwork.lastUsedViewSubId = 0;
    PhotonNetwork.lastUsedViewSubIdStatic = 0;
  }

  private void GameEnteredOnGameServer(OperationResponse operationResponse)
  {
    if (operationResponse.ReturnCode != null)
    {
      switch ((byte) operationResponse.OperationCode)
      {
        case 225:
          if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
          {
            Debug.Log((object) ("Join failed on GameServer. Changing back to MasterServer. Msg: " + (string) operationResponse.DebugMessage));
            if (operationResponse.ReturnCode == 32758)
              Debug.Log((object) "Most likely the game became empty during the switch to GameServer.");
          }
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonRandomJoinFailed, (object) (short) operationResponse.ReturnCode, (object) operationResponse.DebugMessage);
          break;
        case 226:
          if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
          {
            Debug.Log((object) ("Join failed on GameServer. Changing back to MasterServer. Msg: " + (string) operationResponse.DebugMessage));
            if (operationResponse.ReturnCode == 32758)
              Debug.Log((object) "Most likely the game became empty during the switch to GameServer.");
          }
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonJoinRoomFailed, (object) (short) operationResponse.ReturnCode, (object) operationResponse.DebugMessage);
          break;
        case 227:
          if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
            Debug.Log((object) ("Create failed on GameServer. Changing back to MasterServer. Msg: " + (string) operationResponse.DebugMessage));
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCreateRoomFailed, (object) (short) operationResponse.ReturnCode, (object) operationResponse.DebugMessage);
          break;
      }
      this.DisconnectToReconnect();
    }
    else
    {
      Room room = new Room(this.enterRoomParamsCache.RoomName, (RoomOptions) null);
      room.IsLocalClientInside = true;
      this.CurrentRoom = room;
      this.State = ClientState.Joined;
      if (((Dictionary<byte, object>) operationResponse.Parameters).ContainsKey((byte) 252))
        this.UpdatedActorList((int[]) ((Dictionary<byte, object>) operationResponse.Parameters)[(byte) 252]);
      this.ChangeLocalID((int) operationResponse.get_Item((byte) 254));
      Hashtable pActorProperties = (Hashtable) operationResponse.get_Item((byte) 249);
      this.ReadoutProperties((Hashtable) operationResponse.get_Item((byte) 248), pActorProperties, 0);
      if (!this.CurrentRoom.serverSideMasterClient)
        this.CheckMasterClient(-1);
      if (this.mPlayernameHasToBeUpdated)
        this.SendPlayerName();
      switch ((byte) operationResponse.OperationCode)
      {
        case 227:
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom);
          break;
      }
    }
  }

  private void AddNewPlayer(int ID, PhotonPlayer player)
  {
    if (!this.mActors.ContainsKey(ID))
    {
      this.mActors[ID] = player;
      this.RebuildPlayerListCopies();
    }
    else
      Debug.LogError((object) ("Adding player twice: " + (object) ID));
  }

  private void RemovePlayer(int ID, PhotonPlayer player)
  {
    this.mActors.Remove(ID);
    if (player.IsLocal)
      return;
    this.RebuildPlayerListCopies();
  }

  private void RebuildPlayerListCopies()
  {
    this.mPlayerListCopy = new PhotonPlayer[this.mActors.Count];
    this.mActors.Values.CopyTo(this.mPlayerListCopy, 0);
    List<PhotonPlayer> photonPlayerList = new List<PhotonPlayer>();
    for (int index = 0; index < this.mPlayerListCopy.Length; ++index)
    {
      PhotonPlayer photonPlayer = this.mPlayerListCopy[index];
      if (!photonPlayer.IsLocal)
        photonPlayerList.Add(photonPlayer);
    }
    this.mOtherPlayerListCopy = photonPlayerList.ToArray();
  }

  private void ResetPhotonViewsOnSerialize()
  {
    using (Dictionary<int, PhotonView>.ValueCollection.Enumerator enumerator = this.photonViewList.Values.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.lastOnSerializeDataSent = (object[]) null;
    }
  }

  private void HandleEventLeave(int actorID, EventData evLeave)
  {
    if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
      Debug.Log((object) ("HandleEventLeave for player ID: " + (object) actorID + " evLeave: " + evLeave.ToStringFull()));
    PhotonPlayer playerWithId = this.GetPlayerWithId(actorID);
    if (playerWithId == null)
    {
      Debug.LogError((object) string.Format("Received event Leave for unknown player ID: {0}", (object) actorID));
    }
    else
    {
      bool isInactive = playerWithId.IsInactive;
      if (((Dictionary<byte, object>) evLeave.Parameters).ContainsKey((byte) 233))
      {
        playerWithId.IsInactive = (bool) ((Dictionary<byte, object>) evLeave.Parameters)[(byte) 233];
        if (playerWithId.IsInactive != isInactive)
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerActivityChanged, (object) playerWithId);
        if (playerWithId.IsInactive && isInactive)
        {
          Debug.LogWarning((object) ("HandleEventLeave for player ID: " + (object) actorID + " isInactive: " + (object) playerWithId.IsInactive + ". Stopping handling if inactive."));
          return;
        }
      }
      if (((Dictionary<byte, object>) evLeave.Parameters).ContainsKey((byte) 203))
      {
        if ((int) evLeave.get_Item((byte) 203) != 0)
        {
          this.mMasterClientId = (int) evLeave.get_Item((byte) 203);
          this.UpdateMasterClient();
        }
      }
      else if (!this.CurrentRoom.serverSideMasterClient)
        this.CheckMasterClient(actorID);
      if (playerWithId.IsInactive && !isInactive)
        return;
      if (this.CurrentRoom != null && this.CurrentRoom.AutoCleanUp)
        this.DestroyPlayerObjects(actorID, true);
      this.RemovePlayer(actorID, playerWithId);
      NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerDisconnected, (object) playerWithId);
    }
  }

  private void CheckMasterClient(int leavingPlayerId)
  {
    bool flag1 = this.mMasterClientId == leavingPlayerId;
    bool flag2 = leavingPlayerId > 0;
    if (flag2 && !flag1)
      return;
    int number;
    if (this.mActors.Count <= 1)
    {
      number = this.LocalPlayer.ID;
    }
    else
    {
      number = int.MaxValue;
      using (Dictionary<int, PhotonPlayer>.KeyCollection.Enumerator enumerator = this.mActors.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          int current = enumerator.Current;
          if (current < number && current != leavingPlayerId)
            number = current;
        }
      }
    }
    this.mMasterClientId = number;
    if (!flag2)
      return;
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnMasterClientSwitched, (object) this.GetPlayerWithId(number));
  }

  protected internal void UpdateMasterClient()
  {
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnMasterClientSwitched, (object) PhotonNetwork.masterClient);
  }

  private static int ReturnLowestPlayerId(PhotonPlayer[] players, int playerIdToIgnore)
  {
    if (players == null || players.Length == 0)
      return -1;
    int num = int.MaxValue;
    for (int index = 0; index < players.Length; ++index)
    {
      PhotonPlayer player = players[index];
      if (player.ID != playerIdToIgnore && player.ID < num)
        num = player.ID;
    }
    return num;
  }

  protected internal bool SetMasterClient(int playerId, bool sync)
  {
    if (this.mMasterClientId == playerId || !this.mActors.ContainsKey(playerId))
      return false;
    if (sync)
    {
      int num1 = 208;
      Hashtable hashtable1 = new Hashtable();
      ((Dictionary<object, object>) hashtable1).Add((object) (byte) 1, (object) playerId);
      Hashtable hashtable2 = hashtable1;
      int num2 = 1;
      // ISSUE: variable of the null type
      __Null local = null;
      if (!this.OpRaiseEvent((byte) num1, (object) hashtable2, num2 != 0, (RaiseEventOptions) local))
        return false;
    }
    this.hasSwitchedMC = true;
    this.CurrentRoom.MasterClientId = playerId;
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnMasterClientSwitched, (object) this.GetPlayerWithId(playerId));
    return true;
  }

  public bool SetMasterClient(int nextMasterId)
  {
    Hashtable hashtable1 = new Hashtable();
    ((Dictionary<object, object>) hashtable1).Add((object) (byte) 248, (object) nextMasterId);
    Hashtable gameProperties = hashtable1;
    Hashtable hashtable2 = new Hashtable();
    ((Dictionary<object, object>) hashtable2).Add((object) (byte) 248, (object) this.mMasterClientId);
    Hashtable expectedProperties = hashtable2;
    return this.OpSetPropertiesOfRoom(gameProperties, expectedProperties, false);
  }

  protected internal PhotonPlayer GetPlayerWithId(int number)
  {
    if (this.mActors == null)
      return (PhotonPlayer) null;
    PhotonPlayer photonPlayer = (PhotonPlayer) null;
    this.mActors.TryGetValue(number, out photonPlayer);
    return photonPlayer;
  }

  private void SendPlayerName()
  {
    if (this.State == ClientState.Joining)
    {
      this.mPlayernameHasToBeUpdated = true;
    }
    else
    {
      if (this.LocalPlayer == null)
        return;
      this.LocalPlayer.NickName = this.PlayerName;
      Hashtable actorProperties = new Hashtable();
      actorProperties.set_Item((object) byte.MaxValue, (object) this.PlayerName);
      if (this.LocalPlayer.ID <= 0)
        return;
      this.OpSetPropertiesOfActor(this.LocalPlayer.ID, actorProperties, (Hashtable) null, false);
      this.mPlayernameHasToBeUpdated = false;
    }
  }

  private Hashtable GetLocalActorProperties()
  {
    if (PhotonNetwork.player != null)
      return PhotonNetwork.player.AllProperties;
    Hashtable hashtable = new Hashtable();
    hashtable.set_Item((object) byte.MaxValue, (object) this.PlayerName);
    return hashtable;
  }

  public void DebugReturn(DebugLevel level, string message)
  {
    if (level == 1)
      Debug.LogError((object) message);
    else if (level == 2)
      Debug.LogWarning((object) message);
    else if (level == 3 && PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
    {
      Debug.Log((object) message);
    }
    else
    {
      if (level != 5 || PhotonNetwork.logLevel != PhotonLogLevel.Full)
        return;
      Debug.Log((object) message);
    }
  }

  public void OnOperationResponse(OperationResponse operationResponse)
  {
    if (PhotonNetwork.networkingPeer.State == ClientState.Disconnecting)
    {
      if (PhotonNetwork.logLevel < PhotonLogLevel.Informational)
        return;
      Debug.Log((object) ("OperationResponse ignored while disconnecting. Code: " + (object) (byte) operationResponse.OperationCode));
    }
    else
    {
      if (operationResponse.ReturnCode == null)
      {
        if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
          Debug.Log((object) operationResponse.ToString());
      }
      else if (operationResponse.ReturnCode == -3)
        Debug.LogError((object) ("Operation " + (object) (byte) operationResponse.OperationCode + " could not be executed (yet). Wait for state JoinedLobby or ConnectedToMaster and their callbacks before calling operations. WebRPCs need a server-side configuration. Enum OperationCode helps identify the operation."));
      else if (operationResponse.ReturnCode == 32752)
        Debug.LogError((object) ("Operation " + (object) (byte) operationResponse.OperationCode + " failed in a server-side plugin. Check the configuration in the Dashboard. Message from server-plugin: " + (object) operationResponse.DebugMessage));
      else if (operationResponse.ReturnCode == 32760)
        Debug.LogWarning((object) ("Operation failed: " + operationResponse.ToStringFull()));
      else
        Debug.LogError((object) ("Operation failed: " + operationResponse.ToStringFull() + " Server: " + (object) this.Server));
      if (((Dictionary<byte, object>) operationResponse.Parameters).ContainsKey((byte) 221))
      {
        if (this.AuthValues == null)
          this.AuthValues = new AuthenticationValues();
        this.AuthValues.Token = operationResponse.get_Item((byte) 221) as string;
        this.tokenCache = this.AuthValues.Token;
      }
      byte operationCode = (byte) operationResponse.OperationCode;
      switch (operationCode)
      {
        case 217:
          if (operationResponse.ReturnCode != null)
          {
            this.DebugReturn((DebugLevel) 1, "GetGameList failed: " + operationResponse.ToStringFull());
            break;
          }
          this.mGameList = new Dictionary<string, RoomInfo>();
          Hashtable hashtable = (Hashtable) operationResponse.get_Item((byte) 222);
          using (Dictionary<object, object>.KeyCollection.Enumerator enumerator = ((Dictionary<object, object>) hashtable).Keys.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              object current = enumerator.Current;
              string roomName = (string) current;
              this.mGameList[roomName] = new RoomInfo(roomName, (Hashtable) hashtable.get_Item(current));
            }
          }
          this.mGameListCopy = new RoomInfo[this.mGameList.Count];
          this.mGameList.Values.CopyTo(this.mGameListCopy, 0);
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate);
          break;
        case 219:
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnWebRpcResponse, (object) operationResponse);
          break;
        case 220:
          if (operationResponse.ReturnCode == (int) short.MaxValue)
          {
            Debug.LogError((object) string.Format("The appId this client sent is unknown on the server (Cloud). Check settings. If using the Cloud, check account."));
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, (object) DisconnectCause.InvalidAuthentication);
            this.State = ClientState.Disconnecting;
            this.Disconnect();
            break;
          }
          if (operationResponse.ReturnCode != null)
          {
            Debug.LogError((object) ("GetRegions failed. Can't provide regions list. Error: " + (object) (short) operationResponse.ReturnCode + ": " + (object) operationResponse.DebugMessage));
            break;
          }
          string[] strArray1 = operationResponse.get_Item((byte) 210) as string[];
          string[] strArray2 = operationResponse.get_Item((byte) 230) as string[];
          if (strArray1 == null || strArray2 == null || strArray1.Length != strArray2.Length)
          {
            Debug.LogError((object) ("The region arrays from Name Server are not ok. Must be non-null and same length. " + (object) (strArray1 == null) + " " + (object) (strArray2 == null) + "\n" + operationResponse.ToStringFull()));
            break;
          }
          this.AvailableRegions = new List<Region>(strArray1.Length);
          for (int index = 0; index < strArray1.Length; ++index)
          {
            string str = strArray1[index];
            if (!string.IsNullOrEmpty(str))
            {
              string lower = str.ToLower();
              CloudRegionCode cloudRegionCode = Region.Parse(lower);
              bool flag1 = true;
              if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.BestRegion && PhotonNetwork.PhotonServerSettings.EnabledRegions != (CloudRegionFlag) 0)
              {
                CloudRegionFlag flag2 = Region.ParseFlag(lower);
                flag1 = (PhotonNetwork.PhotonServerSettings.EnabledRegions & flag2) != (CloudRegionFlag) 0;
                if (!flag1 && PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                  Debug.Log((object) ("Skipping region because it's not in PhotonServerSettings.EnabledRegions: " + (object) cloudRegionCode));
              }
              if (flag1)
                this.AvailableRegions.Add(new Region()
                {
                  Code = cloudRegionCode,
                  HostAndPort = strArray2[index]
                });
            }
          }
          if (PhotonNetwork.PhotonServerSettings.HostType != ServerSettings.HostingOption.BestRegion)
            break;
          PhotonHandler.PingAvailableRegionsAndConnectToBest();
          break;
        case 222:
          bool[] flagArray = operationResponse.get_Item((byte) 1) as bool[];
          string[] strArray3 = operationResponse.get_Item((byte) 2) as string[];
          if (flagArray != null && strArray3 != null && (this.friendListRequested != null && flagArray.Length == this.friendListRequested.Length))
          {
            List<FriendInfo> friendInfoList = new List<FriendInfo>(this.friendListRequested.Length);
            for (int index = 0; index < this.friendListRequested.Length; ++index)
              friendInfoList.Insert(index, new FriendInfo()
              {
                Name = this.friendListRequested[index],
                Room = strArray3[index],
                IsOnline = flagArray[index]
              });
            PhotonNetwork.Friends = friendInfoList;
          }
          else
            Debug.LogError((object) "FindFriends failed to apply the result, as a required value wasn't provided or the friend list length differed from result.");
          this.friendListRequested = (string[]) null;
          this.isFetchingFriendList = false;
          this.friendListTimestamp = Environment.TickCount;
          if (this.friendListTimestamp == 0)
            this.friendListTimestamp = 1;
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnUpdatedFriendList);
          break;
        case 225:
          if (operationResponse.ReturnCode != null)
          {
            if (operationResponse.ReturnCode == 32760)
            {
              if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
                Debug.Log((object) "JoinRandom failed: No open game. Calling: OnPhotonRandomJoinFailed() and staying on master server.");
            }
            else if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
              Debug.LogWarning((object) string.Format("JoinRandom failed: {0}.", (object) operationResponse.ToStringFull()));
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonRandomJoinFailed, (object) (short) operationResponse.ReturnCode, (object) operationResponse.DebugMessage);
            break;
          }
          this.enterRoomParamsCache.RoomName = (string) operationResponse.get_Item(byte.MaxValue);
          this.GameServerAddress = (string) operationResponse.get_Item((byte) 230);
          this.DisconnectToReconnect();
          break;
        case 226:
          if (this.Server != ServerConnection.GameServer)
          {
            if (operationResponse.ReturnCode != null)
            {
              if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                Debug.Log((object) string.Format("JoinRoom failed (room maybe closed by now). Client stays on masterserver: {0}. State: {1}", (object) operationResponse.ToStringFull(), (object) this.State));
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonJoinRoomFailed, (object) (short) operationResponse.ReturnCode, (object) operationResponse.DebugMessage);
              break;
            }
            this.GameServerAddress = (string) operationResponse.get_Item((byte) 230);
            this.DisconnectToReconnect();
            break;
          }
          this.GameEnteredOnGameServer(operationResponse);
          break;
        case 227:
          if (this.Server == ServerConnection.GameServer)
          {
            this.GameEnteredOnGameServer(operationResponse);
            break;
          }
          if (operationResponse.ReturnCode != null)
          {
            if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
              Debug.LogWarning((object) string.Format("CreateRoom failed, client stays on masterserver: {0}.", (object) operationResponse.ToStringFull()));
            this.State = !this.insideLobby ? ClientState.ConnectedToMaster : ClientState.JoinedLobby;
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCreateRoomFailed, (object) (short) operationResponse.ReturnCode, (object) operationResponse.DebugMessage);
            break;
          }
          string str1 = (string) operationResponse.get_Item(byte.MaxValue);
          if (!string.IsNullOrEmpty(str1))
            this.enterRoomParamsCache.RoomName = str1;
          this.GameServerAddress = (string) operationResponse.get_Item((byte) 230);
          this.DisconnectToReconnect();
          break;
        case 228:
          this.State = ClientState.Authenticated;
          this.LeftLobbyCleanup();
          break;
        case 229:
          this.State = ClientState.JoinedLobby;
          this.insideLobby = true;
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedLobby);
          break;
        case 230:
        case 231:
          if (operationResponse.ReturnCode != null)
          {
            if (operationResponse.ReturnCode == -2)
              Debug.LogError((object) string.Format("If you host Photon yourself, make sure to start the 'Instance LoadBalancing' " + this.get_ServerAddress()));
            else if (operationResponse.ReturnCode == (int) short.MaxValue)
            {
              Debug.LogError((object) string.Format("The appId this client sent is unknown on the server (Cloud). Check settings. If using the Cloud, check account."));
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, (object) DisconnectCause.InvalidAuthentication);
            }
            else if (operationResponse.ReturnCode == 32755)
            {
              Debug.LogError((object) string.Format("Custom Authentication failed (either due to user-input or configuration or AuthParameter string format). Calling: OnCustomAuthenticationFailed()"));
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCustomAuthenticationFailed, (object) operationResponse.DebugMessage);
            }
            else
              Debug.LogError((object) string.Format("Authentication failed: '{0}' Code: {1}", (object) operationResponse.DebugMessage, (object) (short) operationResponse.ReturnCode));
            this.State = ClientState.Disconnecting;
            this.Disconnect();
            if (operationResponse.ReturnCode == 32757)
            {
              if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                Debug.LogWarning((object) string.Format("Currently, the limit of users is reached for this title. Try again later. Disconnecting"));
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonMaxCccuReached);
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, (object) DisconnectCause.MaxCcuReached);
              break;
            }
            if (operationResponse.ReturnCode == 32756)
            {
              if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                Debug.LogError((object) string.Format("The used master server address is not available with the subscription currently used. Got to Photon Cloud Dashboard or change URL. Disconnecting."));
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, (object) DisconnectCause.InvalidRegion);
              break;
            }
            if (operationResponse.ReturnCode != 32753)
              break;
            if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
              Debug.LogError((object) string.Format("The authentication ticket expired. You need to connect (and authenticate) again. Disconnecting."));
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, (object) DisconnectCause.AuthenticationTicketExpired);
            break;
          }
          if (this.Server == ServerConnection.NameServer || this.Server == ServerConnection.MasterServer)
          {
            if (((Dictionary<byte, object>) operationResponse.Parameters).ContainsKey((byte) 225))
            {
              string parameter = (string) ((Dictionary<byte, object>) operationResponse.Parameters)[(byte) 225];
              if (!string.IsNullOrEmpty(parameter))
              {
                if (this.AuthValues == null)
                  this.AuthValues = new AuthenticationValues();
                this.AuthValues.UserId = parameter;
                PhotonNetwork.player.UserId = parameter;
                if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                  this.DebugReturn((DebugLevel) 3, string.Format("Received your UserID from server. Updating local value to: {0}", (object) parameter));
              }
            }
            if (((Dictionary<byte, object>) operationResponse.Parameters).ContainsKey((byte) 202))
            {
              this.playername = (string) ((Dictionary<byte, object>) operationResponse.Parameters)[(byte) 202];
              if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                this.DebugReturn((DebugLevel) 3, string.Format("Received your NickName from server. Updating local value to: {0}", (object) this.playername));
            }
            if (((Dictionary<byte, object>) operationResponse.Parameters).ContainsKey((byte) 192))
              this.SetupEncryption((Dictionary<byte, object>) ((Dictionary<byte, object>) operationResponse.Parameters)[(byte) 192]);
          }
          if (this.Server == ServerConnection.NameServer)
          {
            this.MasterServerAddress = operationResponse.get_Item((byte) 230) as string;
            this.DisconnectToReconnect();
          }
          else if (this.Server == ServerConnection.MasterServer)
          {
            if (this.AuthMode != AuthModeOption.Auth)
              this.OpSettings(this.requestLobbyStatistics);
            if (PhotonNetwork.autoJoinLobby)
            {
              this.State = ClientState.Authenticated;
              this.OpJoinLobby(this.lobby);
            }
            else
            {
              this.State = ClientState.ConnectedToMaster;
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectedToMaster);
            }
          }
          else if (this.Server == ServerConnection.GameServer)
          {
            this.State = ClientState.Joining;
            this.enterRoomParamsCache.PlayerProperties = this.GetLocalActorProperties();
            this.enterRoomParamsCache.OnGameServer = true;
            if (this.lastJoinType == JoinType.JoinRoom || this.lastJoinType == JoinType.JoinRandomRoom || this.lastJoinType == JoinType.JoinOrCreateRoom)
              this.OpJoinRoom(this.enterRoomParamsCache);
            else if (this.lastJoinType == JoinType.CreateRoom)
              this.OpCreateGame(this.enterRoomParamsCache);
          }
          if (!((Dictionary<byte, object>) operationResponse.Parameters).ContainsKey((byte) 245))
            break;
          Dictionary<string, object> parameter1 = (Dictionary<string, object>) ((Dictionary<byte, object>) operationResponse.Parameters)[(byte) 245];
          if (parameter1 == null)
            break;
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCustomAuthenticationResponse, (object) parameter1);
          break;
        default:
          switch (operationCode)
          {
            case 251:
              Hashtable pActorProperties = (Hashtable) operationResponse.get_Item((byte) 249);
              this.ReadoutProperties((Hashtable) operationResponse.get_Item((byte) 248), pActorProperties, 0);
              return;
            case 252:
              return;
            case 253:
              return;
            case 254:
              this.DisconnectToReconnect();
              return;
            default:
              Debug.LogWarning((object) string.Format("OperationResponse unhandled: {0}", (object) operationResponse.ToString()));
              return;
          }
      }
    }
  }

  public void OnStatusChanged(StatusCode statusCode)
  {
    if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
      Debug.Log((object) string.Format("OnStatusChanged: {0} current State: {1}", (object) ((Enum) (object) statusCode).ToString(), (object) this.State));
    switch (statusCode - 1022)
    {
      case 0:
      case 1:
        this.State = ClientState.PeerCreated;
        if (this.AuthValues != null)
          this.AuthValues.Token = (string) null;
        NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, (object) (DisconnectCause) statusCode);
        break;
      case 2:
        if (this.State == ClientState.ConnectingToNameServer)
        {
          if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
            Debug.Log((object) "Connected to NameServer.");
          this.Server = ServerConnection.NameServer;
          if (this.AuthValues != null)
            this.AuthValues.Token = (string) null;
        }
        if (this.State == ClientState.ConnectingToGameserver)
        {
          if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
            Debug.Log((object) "Connected to gameserver.");
          this.Server = ServerConnection.GameServer;
          this.State = ClientState.ConnectedToGameserver;
        }
        if (this.State == ClientState.ConnectingToMasterserver)
        {
          if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
            Debug.Log((object) "Connected to masterserver.");
          this.Server = ServerConnection.MasterServer;
          this.State = ClientState.Authenticating;
          if (this.IsInitialConnect)
          {
            this.IsInitialConnect = false;
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectedToPhoton);
          }
        }
        if (this.get_TransportProtocol() != 5)
        {
          if (this.Server != ServerConnection.NameServer && this.AuthMode != AuthModeOption.Auth)
            break;
          this.EstablishEncryption();
          break;
        }
        if (this.DebugOut == 3)
        {
          Debug.Log((object) "Skipping EstablishEncryption. Protocol is secure.");
          goto case 26;
        }
        else
          goto case 26;
      case 3:
        this.didAuthenticate = false;
        this.isFetchingFriendList = false;
        if (this.Server == ServerConnection.GameServer)
          this.LeftRoomCleanup();
        if (this.Server == ServerConnection.MasterServer)
          this.LeftLobbyCleanup();
        if (this.State == ClientState.DisconnectingFromMasterserver)
        {
          if (!this.Connect(this.GameServerAddress, ServerConnection.GameServer))
            break;
          this.State = ClientState.ConnectingToGameserver;
          break;
        }
        if (this.State == ClientState.DisconnectingFromGameserver || this.State == ClientState.DisconnectingFromNameServer)
        {
          this.SetupProtocol(ServerConnection.MasterServer);
          if (!this.Connect(this.MasterServerAddress, ServerConnection.MasterServer))
            break;
          this.State = ClientState.ConnectingToMasterserver;
          break;
        }
        if (this.AuthValues != null)
          this.AuthValues.Token = (string) null;
        this.State = ClientState.PeerCreated;
        NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnDisconnectedFromPhoton);
        break;
      case 4:
        if (this.IsInitialConnect)
        {
          Debug.LogError((object) ("Exception while connecting to: " + this.get_ServerAddress() + ". Check if the server is available."));
          if (this.get_ServerAddress() == null || this.get_ServerAddress().StartsWith("127.0.0.1"))
          {
            Debug.LogWarning((object) "The server address is 127.0.0.1 (localhost): Make sure the server is running on this machine. Android and iOS emulators have their own localhost.");
            if (this.get_ServerAddress() == this.GameServerAddress)
              Debug.LogWarning((object) "This might be a misconfiguration in the game server config. You need to edit it to a (public) address.");
          }
          this.State = ClientState.PeerCreated;
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, (object) (DisconnectCause) statusCode);
        }
        else
        {
          this.State = ClientState.PeerCreated;
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, (object) (DisconnectCause) statusCode);
        }
        this.Disconnect();
        break;
      case 5:
        break;
      case 7:
        break;
      case 8:
        break;
      case 9:
        break;
      case 11:
      case 13:
        Debug.Log((object) (statusCode.ToString() + ". This client buffers many incoming messages. This is OK temporarily. With lots of these warnings, check if you send too much or execute messages too slow. " + (!PhotonNetwork.isMessageQueueRunning ? (object) "Your isMessageQueueRunning is false. This can cause the issue temporarily." : (object) string.Empty)));
        break;
      case 15:
        break;
      case 17:
      case 19:
      case 20:
      case 21:
        if (this.IsInitialConnect)
        {
          Debug.LogWarning((object) (statusCode.ToString() + " while connecting to: " + this.get_ServerAddress() + ". Check if the server is available."));
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, (object) (DisconnectCause) statusCode);
        }
        else
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, (object) (DisconnectCause) statusCode);
        if (this.AuthValues != null)
          this.AuthValues.Token = (string) null;
        this.Disconnect();
        break;
      case 18:
        if (this.IsInitialConnect)
        {
          Debug.LogWarning((object) (statusCode.ToString() + " while connecting to: " + this.get_ServerAddress() + ". Check if the server is available."));
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, (object) (DisconnectCause) statusCode);
        }
        else
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, (object) (DisconnectCause) statusCode);
        if (this.AuthValues != null)
          this.AuthValues.Token = (string) null;
        this.Disconnect();
        break;
      case 26:
        if (this.Server == ServerConnection.NameServer)
        {
          this.State = ClientState.ConnectedToNameServer;
          if (!this.didAuthenticate && this.CloudRegion == CloudRegionCode.none)
            this.OpGetRegions(this.AppId);
        }
        if (this.Server != ServerConnection.NameServer && (this.AuthMode == AuthModeOption.AuthOnce || this.AuthMode == AuthModeOption.AuthOnceWss) || (this.didAuthenticate || this.IsUsingNameServer && this.CloudRegion == CloudRegionCode.none))
          break;
        this.didAuthenticate = this.CallAuthenticate();
        if (!this.didAuthenticate)
          break;
        this.State = ClientState.Authenticating;
        break;
      case 27:
        Debug.LogError((object) ("Encryption wasn't established: " + (object) statusCode + ". Going to authenticate anyways."));
        AuthenticationValues authValues = this.AuthValues;
        if (authValues == null)
          authValues = new AuthenticationValues()
          {
            UserId = this.PlayerName
          };
        this.OpAuthenticate(this.AppId, this.AppVersion, authValues, this.CloudRegion.ToString(), this.requestLobbyStatistics);
        break;
      default:
        Debug.LogError((object) ("Received unknown status code: " + (object) statusCode));
        break;
    }
  }

  public void OnEvent(EventData photonEvent)
  {
    if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
      Debug.Log((object) string.Format("OnEvent: {0}", (object) photonEvent.ToString()));
    int index1 = -1;
    PhotonPlayer photonPlayer = (PhotonPlayer) null;
    if (((Dictionary<byte, object>) photonEvent.Parameters).ContainsKey((byte) 254))
    {
      index1 = (int) photonEvent.get_Item((byte) 254);
      photonPlayer = this.GetPlayerWithId(index1);
    }
    byte code = (byte) photonEvent.Code;
    switch (code)
    {
      case 200:
        this.ExecuteRpc(photonEvent.get_Item((byte) 245) as Hashtable, photonPlayer);
        break;
      case 201:
      case 206:
        Hashtable hashtable1 = (Hashtable) photonEvent.get_Item((byte) 245);
        int networkTime = (int) hashtable1.get_Item((object) (byte) 0);
        short correctPrefix = -1;
        byte num1 = 10;
        int num2 = 1;
        if (((Dictionary<object, object>) hashtable1).ContainsKey((object) (byte) 1))
        {
          correctPrefix = (short) hashtable1.get_Item((object) (byte) 1);
          num2 = 2;
        }
        for (byte index2 = num1; (int) index2 - (int) num1 < ((Dictionary<object, object>) hashtable1).Count - num2; ++index2)
          this.OnSerializeRead(hashtable1.get_Item((object) index2) as object[], photonPlayer, networkTime, correctPrefix);
        break;
      case 202:
        this.DoInstantiate((Hashtable) photonEvent.get_Item((byte) 245), photonPlayer, (GameObject) null);
        break;
      case 203:
        if (photonPlayer == null || !photonPlayer.IsMasterClient)
        {
          Debug.LogError((object) ("Error: Someone else(" + (object) photonPlayer + ") then the masterserver requests a disconnect!"));
          break;
        }
        PhotonNetwork.LeaveRoom();
        break;
      case 204:
        int key = (int) ((Hashtable) photonEvent.get_Item((byte) 245)).get_Item((object) (byte) 0);
        PhotonView photonView1 = (PhotonView) null;
        if (this.photonViewList.TryGetValue(key, out photonView1))
        {
          this.RemoveInstantiatedGO(((Component) photonView1).get_gameObject(), true);
          break;
        }
        if (this.DebugOut < 1)
          break;
        Debug.LogError((object) ("Ev Destroy Failed. Could not find PhotonView with instantiationId " + (object) key + ". Sent by actorNr: " + (object) index1));
        break;
      case 207:
        int playerId = (int) ((Hashtable) photonEvent.get_Item((byte) 245)).get_Item((object) (byte) 0);
        if (playerId >= 0)
        {
          this.DestroyPlayerObjects(playerId, true);
          break;
        }
        if (this.DebugOut >= 3)
          Debug.Log((object) ("Ev DestroyAll! By PlayerId: " + (object) index1));
        this.DestroyAll(true);
        break;
      case 208:
        this.SetMasterClient((int) ((Hashtable) photonEvent.get_Item((byte) 245)).get_Item((object) (byte) 1), false);
        break;
      case 209:
        int[] parameter1 = (int[]) ((Dictionary<byte, object>) photonEvent.Parameters)[(byte) 245];
        int viewID1 = parameter1[0];
        int num3 = parameter1[1];
        PhotonView photonView2 = PhotonView.Find(viewID1);
        if (Object.op_Equality((Object) photonView2, (Object) null))
        {
          Debug.LogWarning((object) ("Can't find PhotonView of incoming OwnershipRequest. ViewId not found: " + (object) viewID1));
          break;
        }
        if (PhotonNetwork.logLevel == PhotonLogLevel.Informational)
          Debug.Log((object) ("Ev OwnershipRequest " + (object) photonView2.ownershipTransfer + ". ActorNr: " + (object) index1 + " takes from: " + (object) num3 + ". local RequestedView.ownerId: " + (object) photonView2.ownerId + " isOwnerActive: " + (object) photonView2.isOwnerActive + ". MasterClient: " + (object) this.mMasterClientId + ". This client's player: " + PhotonNetwork.player.ToStringFull()));
        switch (photonView2.ownershipTransfer)
        {
          case OwnershipOption.Fixed:
            Debug.LogWarning((object) "Ownership mode == fixed. Ignoring request.");
            return;
          case OwnershipOption.Takeover:
            if (num3 != photonView2.ownerId && (num3 != 0 || photonView2.ownerId != this.mMasterClientId) && photonView2.ownerId != 0)
              return;
            photonView2.OwnerShipWasTransfered = true;
            PhotonPlayer playerWithId = this.GetPlayerWithId(photonView2.ownerId);
            photonView2.ownerId = index1;
            if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
              Debug.LogWarning((object) (photonView2.ToString() + " ownership transfered to: " + (object) index1));
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnOwnershipTransfered, (object) photonView2, (object) photonPlayer, (object) playerWithId);
            return;
          case OwnershipOption.Request:
            if (num3 != PhotonNetwork.player.ID && !PhotonNetwork.player.IsMasterClient || photonView2.ownerId != PhotonNetwork.player.ID && (!PhotonNetwork.player.IsMasterClient || photonView2.isOwnerActive))
              return;
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnOwnershipRequest, (object) photonView2, (object) photonPlayer);
            return;
          default:
            return;
        }
      case 210:
        int[] parameter2 = (int[]) ((Dictionary<byte, object>) photonEvent.Parameters)[(byte) 245];
        if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
          Debug.Log((object) ("Ev OwnershipTransfer. ViewID " + (object) parameter2[0] + " to: " + (object) parameter2[1] + " Time: " + (object) (Environment.TickCount % 1000)));
        int viewID2 = parameter2[0];
        int ID = parameter2[1];
        PhotonView photonView3 = PhotonView.Find(viewID2);
        if (!Object.op_Inequality((Object) photonView3, (Object) null))
          break;
        int ownerId = photonView3.ownerId;
        photonView3.OwnerShipWasTransfered = true;
        photonView3.ownerId = ID;
        NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnOwnershipTransfered, (object) photonView3, (object) PhotonPlayer.Find(ID), (object) PhotonPlayer.Find(ownerId));
        break;
      case 223:
        if (this.AuthValues == null)
          this.AuthValues = new AuthenticationValues();
        this.AuthValues.Token = photonEvent.get_Item((byte) 221) as string;
        this.tokenCache = this.AuthValues.Token;
        break;
      case 224:
        string[] strArray = photonEvent.get_Item((byte) 213) as string[];
        byte[] numArray1 = photonEvent.get_Item((byte) 212) as byte[];
        int[] numArray2 = photonEvent.get_Item((byte) 229) as int[];
        int[] numArray3 = photonEvent.get_Item((byte) 228) as int[];
        this.LobbyStatistics.Clear();
        for (int index2 = 0; index2 < strArray.Length; ++index2)
        {
          TypedLobbyInfo typedLobbyInfo = new TypedLobbyInfo();
          typedLobbyInfo.Name = strArray[index2];
          typedLobbyInfo.Type = (LobbyType) numArray1[index2];
          typedLobbyInfo.PlayerCount = numArray2[index2];
          typedLobbyInfo.RoomCount = numArray3[index2];
          this.LobbyStatistics.Add(typedLobbyInfo);
        }
        NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnLobbyStatisticsUpdate);
        break;
      case 226:
        this.PlayersInRoomsCount = (int) photonEvent.get_Item((byte) 229);
        this.PlayersOnMasterCount = (int) photonEvent.get_Item((byte) 227);
        this.RoomsCount = (int) photonEvent.get_Item((byte) 228);
        break;
      case 228:
        break;
      case 229:
        Hashtable hashtable2 = (Hashtable) photonEvent.get_Item((byte) 222);
        using (Dictionary<object, object>.KeyCollection.Enumerator enumerator = ((Dictionary<object, object>) hashtable2).Keys.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            object current = enumerator.Current;
            string index2 = (string) current;
            RoomInfo roomInfo = new RoomInfo(index2, (Hashtable) hashtable2.get_Item(current));
            if (roomInfo.removedFromList)
              this.mGameList.Remove(index2);
            else
              this.mGameList[index2] = roomInfo;
          }
        }
        this.mGameListCopy = new RoomInfo[this.mGameList.Count];
        this.mGameList.Values.CopyTo(this.mGameListCopy, 0);
        NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate);
        break;
      case 230:
        this.mGameList = new Dictionary<string, RoomInfo>();
        Hashtable hashtable3 = (Hashtable) photonEvent.get_Item((byte) 222);
        using (Dictionary<object, object>.KeyCollection.Enumerator enumerator = ((Dictionary<object, object>) hashtable3).Keys.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            object current = enumerator.Current;
            string roomName = (string) current;
            this.mGameList[roomName] = new RoomInfo(roomName, (Hashtable) hashtable3.get_Item(current));
          }
        }
        this.mGameListCopy = new RoomInfo[this.mGameList.Count];
        this.mGameList.Values.CopyTo(this.mGameListCopy, 0);
        NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate);
        break;
      default:
        switch (code)
        {
          case 251:
            if (PhotonNetwork.OnEventCall != null)
            {
              object content = photonEvent.get_Item((byte) 245);
              PhotonNetwork.OnEventCall((byte) photonEvent.Code, content, index1);
              return;
            }
            Debug.LogWarning((object) "Warning: Unhandled Event ErrorInfo (251). Set PhotonNetwork.OnEventCall to the method PUN should call for this event.");
            return;
          case 253:
            int targetActorNr = (int) photonEvent.get_Item((byte) 253);
            Hashtable gameProperties = (Hashtable) null;
            Hashtable pActorProperties = (Hashtable) null;
            if (targetActorNr == 0)
              gameProperties = (Hashtable) photonEvent.get_Item((byte) 251);
            else
              pActorProperties = (Hashtable) photonEvent.get_Item((byte) 251);
            this.ReadoutProperties(gameProperties, pActorProperties, targetActorNr);
            return;
          case 254:
            this.HandleEventLeave(index1, photonEvent);
            return;
          case byte.MaxValue:
            bool flag = false;
            Hashtable properties = (Hashtable) photonEvent.get_Item((byte) 249);
            if (photonPlayer == null)
            {
              bool isLocal = this.LocalPlayer.ID == index1;
              this.AddNewPlayer(index1, new PhotonPlayer(isLocal, index1, properties));
              this.ResetPhotonViewsOnSerialize();
            }
            else
            {
              flag = photonPlayer.IsInactive;
              photonPlayer.InternalCacheProperties(properties);
              photonPlayer.IsInactive = false;
            }
            if (index1 == this.LocalPlayer.ID)
            {
              this.UpdatedActorList((int[]) photonEvent.get_Item((byte) 252));
              if (this.lastJoinType == JoinType.JoinOrCreateRoom && this.LocalPlayer.ID == 1)
                NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom);
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedRoom);
              return;
            }
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerConnected, (object) this.mActors[index1]);
            if (!flag)
              return;
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerActivityChanged, (object) this.mActors[index1]);
            return;
          default:
            if (photonEvent.Code >= 200)
              return;
            if (PhotonNetwork.OnEventCall != null)
            {
              object content = photonEvent.get_Item((byte) 245);
              PhotonNetwork.OnEventCall((byte) photonEvent.Code, content, index1);
              return;
            }
            Debug.LogWarning((object) ("Warning: Unhandled event " + (object) photonEvent + ". Set PhotonNetwork.OnEventCall."));
            return;
        }
    }
  }

  public void OnMessage(object messages)
  {
  }

  private void SetupEncryption(Dictionary<byte, object> encryptionData)
  {
    if (this.AuthMode == AuthModeOption.Auth && this.DebugOut == 1)
    {
      Debug.LogWarning((object) ("SetupEncryption() called but ignored. Not XB1 compiled. EncryptionData: " + encryptionData.ToStringFull()));
    }
    else
    {
      if (this.DebugOut == 3)
        Debug.Log((object) ("SetupEncryption() got called. " + encryptionData.ToStringFull()));
      switch ((EncryptionMode) (byte) encryptionData[(byte) 0])
      {
        case EncryptionMode.PayloadEncryption:
          this.InitPayloadEncryption((byte[]) encryptionData[(byte) 1]);
          break;
        case EncryptionMode.DatagramEncryption:
          this.InitDatagramEncryption((byte[]) encryptionData[(byte) 1], (byte[]) encryptionData[(byte) 2]);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }

  protected internal void UpdatedActorList(int[] actorsInRoom)
  {
    for (int index = 0; index < actorsInRoom.Length; ++index)
    {
      int num = actorsInRoom[index];
      if (this.LocalPlayer.ID != num && !this.mActors.ContainsKey(num))
        this.AddNewPlayer(num, new PhotonPlayer(false, num, string.Empty));
    }
  }

  private void SendVacantViewIds()
  {
    Debug.Log((object) "SendVacantViewIds()");
    List<int> intList = new List<int>();
    using (Dictionary<int, PhotonView>.ValueCollection.Enumerator enumerator = this.photonViewList.Values.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        PhotonView current = enumerator.Current;
        if (!current.isOwnerActive)
          intList.Add(current.viewID);
      }
    }
    Debug.Log((object) ("Sending vacant view IDs. Length: " + (object) intList.Count));
    this.OpRaiseEvent((byte) 211, (object) intList.ToArray(), true, (RaiseEventOptions) null);
  }

  public static void SendMonoMessage(PhotonNetworkingMessage methodString, params object[] parameters)
  {
    HashSet<GameObject> gameObjectSet = PhotonNetwork.SendMonoMessageTargets == null ? PhotonNetwork.FindGameObjectsWithComponent(PhotonNetwork.SendMonoMessageTargetType) : PhotonNetwork.SendMonoMessageTargets;
    string str = methodString.ToString();
    object obj = parameters == null || parameters.Length != 1 ? (object) parameters : parameters[0];
    using (HashSet<GameObject>.Enumerator enumerator = gameObjectSet.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        GameObject current = enumerator.Current;
        if (Object.op_Inequality((Object) current, (Object) null))
          current.SendMessage(str, obj, (SendMessageOptions) 1);
      }
    }
  }

  protected internal void ExecuteRpc(Hashtable rpcData, PhotonPlayer sender)
  {
    if (rpcData == null || !((Dictionary<object, object>) rpcData).ContainsKey((object) (byte) 0))
    {
      Debug.LogError((object) ("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString((IDictionary) rpcData)));
    }
    else
    {
      int viewID = (int) rpcData.get_Item((object) (byte) 0);
      int num1 = 0;
      if (((Dictionary<object, object>) rpcData).ContainsKey((object) (byte) 1))
        num1 = (int) (short) rpcData.get_Item((object) (byte) 1);
      string rpc;
      if (((Dictionary<object, object>) rpcData).ContainsKey((object) (byte) 5))
      {
        int index = (int) (byte) rpcData.get_Item((object) (byte) 5);
        if (index > PhotonNetwork.PhotonServerSettings.RpcList.Count - 1)
        {
          Debug.LogError((object) ("Could not find RPC with index: " + (object) index + ". Going to ignore! Check PhotonServerSettings.RpcList"));
          return;
        }
        rpc = PhotonNetwork.PhotonServerSettings.RpcList[index];
      }
      else
        rpc = (string) rpcData.get_Item((object) (byte) 3);
      object[] parameters1 = (object[]) null;
      if (((Dictionary<object, object>) rpcData).ContainsKey((object) (byte) 4))
        parameters1 = (object[]) rpcData.get_Item((object) (byte) 4);
      if (parameters1 == null)
        parameters1 = new object[0];
      PhotonView photonView = this.GetPhotonView(viewID);
      if (Object.op_Equality((Object) photonView, (Object) null))
      {
        int num2 = viewID / PhotonNetwork.MAX_VIEW_IDS;
        bool flag1 = num2 == this.LocalPlayer.ID;
        bool flag2 = num2 == sender.ID;
        if (flag1)
          Debug.LogWarning((object) ("Received RPC \"" + rpc + "\" for viewID " + (object) viewID + " but this PhotonView does not exist! View was/is ours." + (!flag2 ? (object) " Remote called." : (object) " Owner called.") + " By: " + (object) sender.ID));
        else
          Debug.LogWarning((object) ("Received RPC \"" + rpc + "\" for viewID " + (object) viewID + " but this PhotonView does not exist! Was remote PV." + (!flag2 ? (object) " Remote called." : (object) " Owner called.") + " By: " + (object) sender.ID + " Maybe GO was destroyed but RPC not cleaned up."));
      }
      else if (photonView.prefix != num1)
        Debug.LogError((object) ("Received RPC \"" + rpc + "\" on viewID " + (object) viewID + " with a prefix of " + (object) num1 + ", our prefix is " + (object) photonView.prefix + ". The RPC has been ignored."));
      else if (string.IsNullOrEmpty(rpc))
      {
        Debug.LogError((object) ("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString((IDictionary) rpcData)));
      }
      else
      {
        if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
          Debug.Log((object) ("Received RPC: " + rpc));
        if (photonView.group != 0 && !this.allowedReceivingGroups.Contains(photonView.group))
          return;
        System.Type[] callParameterTypes = new System.Type[0];
        if (parameters1.Length > 0)
        {
          callParameterTypes = new System.Type[parameters1.Length];
          int index1 = 0;
          for (int index2 = 0; index2 < parameters1.Length; ++index2)
          {
            object obj = parameters1[index2];
            callParameterTypes[index1] = obj != null ? obj.GetType() : (System.Type) null;
            ++index1;
          }
        }
        int num2 = 0;
        int num3 = 0;
        if (!PhotonNetwork.UseRpcMonoBehaviourCache || photonView.RpcMonoBehaviours == null || photonView.RpcMonoBehaviours.Length == 0)
          photonView.RefreshRpcMonoBehaviourCache();
        for (int index1 = 0; index1 < photonView.RpcMonoBehaviours.Length; ++index1)
        {
          MonoBehaviour rpcMonoBehaviour = photonView.RpcMonoBehaviours[index1];
          if (Object.op_Equality((Object) rpcMonoBehaviour, (Object) null))
          {
            Debug.LogError((object) "ERROR You have missing MonoBehaviours on your gameobjects!");
          }
          else
          {
            System.Type type = ((object) rpcMonoBehaviour).GetType();
            List<MethodInfo> methodInfoList = (List<MethodInfo>) null;
            if (!this.monoRPCMethodsCache.TryGetValue(type, out methodInfoList))
            {
              List<MethodInfo> methods = SupportClass.GetMethods(type, typeof (PunRPC));
              this.monoRPCMethodsCache[type] = methods;
              methodInfoList = methods;
            }
            if (methodInfoList != null)
            {
              for (int index2 = 0; index2 < methodInfoList.Count; ++index2)
              {
                MethodInfo mo = methodInfoList[index2];
                if (mo.Name.Equals(rpc))
                {
                  ++num3;
                  ParameterInfo[] cachedParemeters = mo.GetCachedParemeters();
                  if (cachedParemeters.Length == callParameterTypes.Length)
                  {
                    if (this.CheckTypeMatch(cachedParemeters, callParameterTypes))
                    {
                      ++num2;
                      object obj = mo.Invoke((object) rpcMonoBehaviour, parameters1);
                      if (PhotonNetwork.StartRpcsAsCoroutine && (object) mo.ReturnType == (object) typeof (IEnumerator))
                        rpcMonoBehaviour.StartCoroutine((IEnumerator) obj);
                    }
                  }
                  else if (cachedParemeters.Length - 1 == callParameterTypes.Length)
                  {
                    if (this.CheckTypeMatch(cachedParemeters, callParameterTypes) && (object) cachedParemeters[cachedParemeters.Length - 1].ParameterType == (object) typeof (PhotonMessageInfo))
                    {
                      ++num2;
                      int timestamp = (int) rpcData.get_Item((object) (byte) 2);
                      object[] parameters2 = new object[parameters1.Length + 1];
                      parameters1.CopyTo((Array) parameters2, 0);
                      parameters2[parameters2.Length - 1] = (object) new PhotonMessageInfo(sender, timestamp, photonView);
                      object obj = mo.Invoke((object) rpcMonoBehaviour, parameters2);
                      if (PhotonNetwork.StartRpcsAsCoroutine && (object) mo.ReturnType == (object) typeof (IEnumerator))
                        rpcMonoBehaviour.StartCoroutine((IEnumerator) obj);
                    }
                  }
                  else if (cachedParemeters.Length == 1 && cachedParemeters[0].ParameterType.IsArray)
                  {
                    ++num2;
                    object obj = mo.Invoke((object) rpcMonoBehaviour, new object[1]
                    {
                      (object) parameters1
                    });
                    if (PhotonNetwork.StartRpcsAsCoroutine && (object) mo.ReturnType == (object) typeof (IEnumerator))
                      rpcMonoBehaviour.StartCoroutine((IEnumerator) obj);
                  }
                }
              }
            }
          }
        }
        if (num2 == 1)
          return;
        string str = string.Empty;
        for (int index = 0; index < callParameterTypes.Length; ++index)
        {
          System.Type type = callParameterTypes[index];
          if (str != string.Empty)
            str += ", ";
          str = (object) type != null ? str + type.Name : str + "null";
        }
        if (num2 == 0)
        {
          if (num3 == 0)
            Debug.LogError((object) ("PhotonView with ID " + (object) viewID + " has no method \"" + rpc + "\" marked with the [PunRPC](C#) or @PunRPC(JS) property! Args: " + str));
          else
            Debug.LogError((object) ("PhotonView with ID " + (object) viewID + " has no method \"" + rpc + "\" that takes " + (object) callParameterTypes.Length + " argument(s): " + str));
        }
        else
          Debug.LogError((object) ("PhotonView with ID " + (object) viewID + " has " + (object) num2 + " methods \"" + rpc + "\" that takes " + (object) callParameterTypes.Length + " argument(s): " + str + ". Should be just one?"));
      }
    }
  }

  private bool CheckTypeMatch(ParameterInfo[] methodParameters, System.Type[] callParameterTypes)
  {
    if (methodParameters.Length < callParameterTypes.Length)
      return false;
    for (int index = 0; index < callParameterTypes.Length; ++index)
    {
      System.Type parameterType = methodParameters[index].ParameterType;
      if ((object) callParameterTypes[index] != null && !parameterType.IsAssignableFrom(callParameterTypes[index]) && (!parameterType.IsEnum || !Enum.GetUnderlyingType(parameterType).IsAssignableFrom(callParameterTypes[index])))
        return false;
    }
    return true;
  }

  internal Hashtable SendInstantiate(string prefabName, Vector3 position, Quaternion rotation, int group, int[] viewIDs, object[] data, bool isGlobalObject)
  {
    int viewId = viewIDs[0];
    Hashtable hashtable = new Hashtable();
    hashtable.set_Item((object) (byte) 0, (object) prefabName);
    if (Vector3.op_Inequality(position, Vector3.get_zero()))
      hashtable.set_Item((object) (byte) 1, (object) position);
    if (Quaternion.op_Inequality(rotation, Quaternion.get_identity()))
      hashtable.set_Item((object) (byte) 2, (object) rotation);
    if (group != 0)
      hashtable.set_Item((object) (byte) 3, (object) group);
    if (viewIDs.Length > 1)
      hashtable.set_Item((object) (byte) 4, (object) viewIDs);
    if (data != null)
      hashtable.set_Item((object) (byte) 5, (object) data);
    if ((int) this.currentLevelPrefix > 0)
      hashtable.set_Item((object) (byte) 8, (object) this.currentLevelPrefix);
    hashtable.set_Item((object) (byte) 6, (object) PhotonNetwork.ServerTimestamp);
    hashtable.set_Item((object) (byte) 7, (object) viewId);
    this.OpRaiseEvent((byte) 202, (object) hashtable, true, new RaiseEventOptions()
    {
      CachingOption = !isGlobalObject ? EventCaching.AddToRoomCache : EventCaching.AddToRoomCacheGlobal
    });
    return hashtable;
  }

  internal GameObject DoInstantiate(Hashtable evData, PhotonPlayer photonPlayer, GameObject resourceGameObject)
  {
    string str = (string) evData.get_Item((object) (byte) 0);
    int timestamp = (int) evData.get_Item((object) (byte) 6);
    int instantiationId = (int) evData.get_Item((object) (byte) 7);
    Vector3 position = !((Dictionary<object, object>) evData).ContainsKey((object) (byte) 1) ? Vector3.get_zero() : (Vector3) evData.get_Item((object) (byte) 1);
    Quaternion identity = Quaternion.get_identity();
    if (((Dictionary<object, object>) evData).ContainsKey((object) (byte) 2))
      identity = (Quaternion) evData.get_Item((object) (byte) 2);
    int num1 = 0;
    if (((Dictionary<object, object>) evData).ContainsKey((object) (byte) 3))
      num1 = (int) evData.get_Item((object) (byte) 3);
    short num2 = 0;
    if (((Dictionary<object, object>) evData).ContainsKey((object) (byte) 8))
      num2 = (short) evData.get_Item((object) (byte) 8);
    int[] numArray;
    if (((Dictionary<object, object>) evData).ContainsKey((object) (byte) 4))
      numArray = (int[]) evData.get_Item((object) (byte) 4);
    else
      numArray = new int[1]{ instantiationId };
    object[] instantiationData = !((Dictionary<object, object>) evData).ContainsKey((object) (byte) 5) ? (object[]) null : (object[]) evData.get_Item((object) (byte) 5);
    if (num1 != 0 && !this.allowedReceivingGroups.Contains(num1))
      return (GameObject) null;
    if (this.ObjectPool != null)
    {
      GameObject go = this.ObjectPool.Instantiate(str, position, identity);
      PhotonView[] photonViewsInChildren = go.GetPhotonViewsInChildren();
      if (photonViewsInChildren.Length != numArray.Length)
        throw new Exception("Error in Instantiation! The resource's PhotonView count is not the same as in incoming data.");
      for (int index = 0; index < photonViewsInChildren.Length; ++index)
      {
        photonViewsInChildren[index].didAwake = false;
        photonViewsInChildren[index].viewID = 0;
        photonViewsInChildren[index].prefix = (int) num2;
        photonViewsInChildren[index].instantiationId = instantiationId;
        photonViewsInChildren[index].isRuntimeInstantiated = true;
        photonViewsInChildren[index].instantiationDataField = instantiationData;
        photonViewsInChildren[index].didAwake = true;
        photonViewsInChildren[index].viewID = numArray[index];
      }
      go.SendMessage(NetworkingPeer.OnPhotonInstantiateString, (object) new PhotonMessageInfo(photonPlayer, timestamp, (PhotonView) null), (SendMessageOptions) 1);
      return go;
    }
    if (Object.op_Equality((Object) resourceGameObject, (Object) null))
    {
      if (!NetworkingPeer.UsePrefabCache || !NetworkingPeer.PrefabCache.TryGetValue(str, out resourceGameObject))
      {
        resourceGameObject = (GameObject) Resources.Load(str, typeof (GameObject));
        if (NetworkingPeer.UsePrefabCache)
          NetworkingPeer.PrefabCache.Add(str, resourceGameObject);
      }
      if (Object.op_Equality((Object) resourceGameObject, (Object) null))
      {
        Debug.LogError((object) ("PhotonNetwork error: Could not Instantiate the prefab [" + str + "]. Please verify you have this gameobject in a Resources folder."));
        return (GameObject) null;
      }
    }
    PhotonView[] photonViewsInChildren1 = resourceGameObject.GetPhotonViewsInChildren();
    if (photonViewsInChildren1.Length != numArray.Length)
      throw new Exception("Error in Instantiation! The resource's PhotonView count is not the same as in incoming data.");
    for (int index = 0; index < numArray.Length; ++index)
    {
      photonViewsInChildren1[index].viewID = numArray[index];
      photonViewsInChildren1[index].prefix = (int) num2;
      photonViewsInChildren1[index].instantiationId = instantiationId;
      photonViewsInChildren1[index].isRuntimeInstantiated = true;
    }
    this.StoreInstantiationData(instantiationId, instantiationData);
    GameObject gameObject = (GameObject) Object.Instantiate((Object) resourceGameObject, position, identity);
    for (int index = 0; index < numArray.Length; ++index)
    {
      photonViewsInChildren1[index].viewID = 0;
      photonViewsInChildren1[index].prefix = -1;
      photonViewsInChildren1[index].prefixBackup = -1;
      photonViewsInChildren1[index].instantiationId = -1;
      photonViewsInChildren1[index].isRuntimeInstantiated = false;
    }
    this.RemoveInstantiationData(instantiationId);
    gameObject.SendMessage(NetworkingPeer.OnPhotonInstantiateString, (object) new PhotonMessageInfo(photonPlayer, timestamp, (PhotonView) null), (SendMessageOptions) 1);
    return gameObject;
  }

  private void StoreInstantiationData(int instantiationId, object[] instantiationData)
  {
    this.tempInstantiationData[instantiationId] = instantiationData;
  }

  public object[] FetchInstantiationData(int instantiationId)
  {
    object[] objArray = (object[]) null;
    if (instantiationId == 0)
      return (object[]) null;
    this.tempInstantiationData.TryGetValue(instantiationId, out objArray);
    return objArray;
  }

  private void RemoveInstantiationData(int instantiationId)
  {
    this.tempInstantiationData.Remove(instantiationId);
  }

  public void DestroyPlayerObjects(int playerId, bool localOnly)
  {
    if (playerId <= 0)
    {
      Debug.LogError((object) ("Failed to Destroy objects of playerId: " + (object) playerId));
    }
    else
    {
      if (!localOnly)
      {
        this.OpRemoveFromServerInstantiationsOfPlayer(playerId);
        this.OpCleanRpcBuffer(playerId);
        this.SendDestroyOfPlayer(playerId);
      }
      HashSet<GameObject> gameObjectSet = new HashSet<GameObject>();
      using (Dictionary<int, PhotonView>.ValueCollection.Enumerator enumerator = this.photonViewList.Values.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          PhotonView current = enumerator.Current;
          if (Object.op_Inequality((Object) current, (Object) null) && current.CreatorActorNr == playerId)
            gameObjectSet.Add(((Component) current).get_gameObject());
        }
      }
      using (HashSet<GameObject>.Enumerator enumerator = gameObjectSet.GetEnumerator())
      {
        while (enumerator.MoveNext())
          this.RemoveInstantiatedGO(enumerator.Current, true);
      }
      using (Dictionary<int, PhotonView>.ValueCollection.Enumerator enumerator = this.photonViewList.Values.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          PhotonView current = enumerator.Current;
          if (current.ownerId == playerId)
            current.ownerId = current.CreatorActorNr;
        }
      }
    }
  }

  public void DestroyAll(bool localOnly)
  {
    if (!localOnly)
    {
      this.OpRemoveCompleteCache();
      this.SendDestroyOfAll();
    }
    this.LocalCleanupAnythingInstantiated(true);
  }

  protected internal void RemoveInstantiatedGO(GameObject go, bool localOnly)
  {
    if (Object.op_Equality((Object) go, (Object) null))
    {
      Debug.LogError((object) "Failed to 'network-remove' GameObject because it's null.");
    }
    else
    {
      PhotonView[] componentsInChildren = (PhotonView[]) go.GetComponentsInChildren<PhotonView>(true);
      if (componentsInChildren == null || componentsInChildren.Length <= 0)
      {
        Debug.LogError((object) ("Failed to 'network-remove' GameObject because has no PhotonView components: " + (object) go));
      }
      else
      {
        PhotonView photonView = componentsInChildren[0];
        int creatorActorNr = photonView.CreatorActorNr;
        int instantiationId = photonView.instantiationId;
        if (!localOnly)
        {
          if (!photonView.isMine)
          {
            Debug.LogError((object) ("Failed to 'network-remove' GameObject. Client is neither owner nor masterClient taking over for owner who left: " + (object) photonView));
            return;
          }
          if (instantiationId < 1)
          {
            Debug.LogError((object) ("Failed to 'network-remove' GameObject because it is missing a valid InstantiationId on view: " + (object) photonView + ". Not Destroying GameObject or PhotonViews!"));
            return;
          }
        }
        if (!localOnly)
          this.ServerCleanInstantiateAndDestroy(instantiationId, creatorActorNr, photonView.isRuntimeInstantiated);
        for (int index = componentsInChildren.Length - 1; index >= 0; --index)
        {
          PhotonView view = componentsInChildren[index];
          if (!Object.op_Equality((Object) view, (Object) null))
          {
            if (view.instantiationId >= 1)
              this.LocalCleanPhotonView(view);
            if (!localOnly)
              this.OpCleanRpcBuffer(view);
          }
        }
        if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
          Debug.Log((object) ("Network destroy Instantiated GO: " + ((Object) go).get_name()));
        if (this.ObjectPool != null)
        {
          foreach (PhotonView photonViewsInChild in go.GetPhotonViewsInChildren())
            photonViewsInChild.viewID = 0;
          this.ObjectPool.Destroy(go);
        }
        else
          Object.Destroy((Object) go);
      }
    }
  }

  private void ServerCleanInstantiateAndDestroy(int instantiateId, int creatorId, bool isRuntimeInstantiated)
  {
    Hashtable hashtable1 = new Hashtable();
    hashtable1.set_Item((object) (byte) 7, (object) instantiateId);
    RaiseEventOptions raiseEventOptions1 = new RaiseEventOptions()
    {
      CachingOption = EventCaching.RemoveFromRoomCache,
      TargetActors = new int[1]{ creatorId }
    };
    this.OpRaiseEvent((byte) 202, (object) hashtable1, true, raiseEventOptions1);
    Hashtable hashtable2 = new Hashtable();
    hashtable2.set_Item((object) (byte) 0, (object) instantiateId);
    RaiseEventOptions raiseEventOptions2 = (RaiseEventOptions) null;
    if (!isRuntimeInstantiated)
    {
      raiseEventOptions2 = new RaiseEventOptions();
      raiseEventOptions2.CachingOption = EventCaching.AddToRoomCacheGlobal;
      Debug.Log((object) ("Destroying GO as global. ID: " + (object) instantiateId));
    }
    this.OpRaiseEvent((byte) 204, (object) hashtable2, true, raiseEventOptions2);
  }

  private void SendDestroyOfPlayer(int actorNr)
  {
    Hashtable hashtable = new Hashtable();
    hashtable.set_Item((object) (byte) 0, (object) actorNr);
    this.OpRaiseEvent((byte) 207, (object) hashtable, true, (RaiseEventOptions) null);
  }

  private void SendDestroyOfAll()
  {
    Hashtable hashtable = new Hashtable();
    hashtable.set_Item((object) (byte) 0, (object) -1);
    this.OpRaiseEvent((byte) 207, (object) hashtable, true, (RaiseEventOptions) null);
  }

  private void OpRemoveFromServerInstantiationsOfPlayer(int actorNr)
  {
    this.OpRaiseEvent((byte) 202, (object) null, true, new RaiseEventOptions()
    {
      CachingOption = EventCaching.RemoveFromRoomCache,
      TargetActors = new int[1]{ actorNr }
    });
  }

  protected internal void RequestOwnership(int viewID, int fromOwner)
  {
    Debug.Log((object) ("RequestOwnership(): " + (object) viewID + " from: " + (object) fromOwner + " Time: " + (object) (Environment.TickCount % 1000)));
    this.OpRaiseEvent((byte) 209, (object) new int[2]
    {
      viewID,
      fromOwner
    }, 1 != 0, new RaiseEventOptions()
    {
      Receivers = ReceiverGroup.All
    });
  }

  protected internal void TransferOwnership(int viewID, int playerID)
  {
    Debug.Log((object) ("TransferOwnership() view " + (object) viewID + " to: " + (object) playerID + " Time: " + (object) (Environment.TickCount % 1000)));
    this.OpRaiseEvent((byte) 210, (object) new int[2]
    {
      viewID,
      playerID
    }, 1 != 0, new RaiseEventOptions()
    {
      Receivers = ReceiverGroup.All
    });
  }

  public bool LocalCleanPhotonView(PhotonView view)
  {
    view.removedFromLocalViewList = true;
    return this.photonViewList.Remove(view.viewID);
  }

  public PhotonView GetPhotonView(int viewID)
  {
    PhotonView photonView1 = (PhotonView) null;
    this.photonViewList.TryGetValue(viewID, out photonView1);
    if (Object.op_Equality((Object) photonView1, (Object) null))
    {
      foreach (PhotonView photonView2 in Object.FindObjectsOfType(typeof (PhotonView)) as PhotonView[])
      {
        if (photonView2.viewID == viewID)
        {
          if (photonView2.didAwake)
            Debug.LogWarning((object) ("Had to lookup view that wasn't in photonViewList: " + (object) photonView2));
          return photonView2;
        }
      }
    }
    return photonView1;
  }

  public void RegisterPhotonView(PhotonView netView)
  {
    if (!Application.get_isPlaying())
      this.photonViewList = new Dictionary<int, PhotonView>();
    else if (netView.viewID == 0)
    {
      Debug.Log((object) ("PhotonView register is ignored, because viewID is 0. No id assigned yet to: " + (object) netView));
    }
    else
    {
      PhotonView photonView = (PhotonView) null;
      if (this.photonViewList.TryGetValue(netView.viewID, out photonView))
      {
        if (!Object.op_Inequality((Object) netView, (Object) photonView))
          return;
        Debug.LogError((object) string.Format("PhotonView ID duplicate found: {0}. New: {1} old: {2}. Maybe one wasn't destroyed on scene load?! Check for 'DontDestroyOnLoad'. Destroying old entry, adding new.", (object) netView.viewID, (object) netView, (object) photonView));
        this.RemoveInstantiatedGO(((Component) photonView).get_gameObject(), true);
      }
      this.photonViewList.Add(netView.viewID, netView);
      if (PhotonNetwork.logLevel < PhotonLogLevel.Full)
        return;
      Debug.Log((object) ("Registered PhotonView: " + (object) netView.viewID));
    }
  }

  public void OpCleanRpcBuffer(int actorNumber)
  {
    this.OpRaiseEvent((byte) 200, (object) null, true, new RaiseEventOptions()
    {
      CachingOption = EventCaching.RemoveFromRoomCache,
      TargetActors = new int[1]{ actorNumber }
    });
  }

  public void OpRemoveCompleteCacheOfPlayer(int actorNumber)
  {
    this.OpRaiseEvent((byte) 0, (object) null, true, new RaiseEventOptions()
    {
      CachingOption = EventCaching.RemoveFromRoomCache,
      TargetActors = new int[1]{ actorNumber }
    });
  }

  public void OpRemoveCompleteCache()
  {
    this.OpRaiseEvent((byte) 0, (object) null, true, new RaiseEventOptions()
    {
      CachingOption = EventCaching.RemoveFromRoomCache,
      Receivers = ReceiverGroup.MasterClient
    });
  }

  private void RemoveCacheOfLeftPlayers()
  {
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    dictionary[(byte) 244] = (object) (byte) 0;
    dictionary[(byte) 247] = (object) (byte) 7;
    this.OpCustom((byte) 253, dictionary, true, (byte) 0);
  }

  public void CleanRpcBufferIfMine(PhotonView view)
  {
    if (view.ownerId != this.LocalPlayer.ID && !this.LocalPlayer.IsMasterClient)
      Debug.LogError((object) ("Cannot remove cached RPCs on a PhotonView thats not ours! " + (object) view.owner + " scene: " + (object) view.isSceneView));
    else
      this.OpCleanRpcBuffer(view);
  }

  public void OpCleanRpcBuffer(PhotonView view)
  {
    Hashtable hashtable = new Hashtable();
    hashtable.set_Item((object) (byte) 0, (object) view.viewID);
    RaiseEventOptions raiseEventOptions = new RaiseEventOptions()
    {
      CachingOption = EventCaching.RemoveFromRoomCache
    };
    this.OpRaiseEvent((byte) 200, (object) hashtable, true, raiseEventOptions);
  }

  public void RemoveRPCsInGroup(int group)
  {
    using (Dictionary<int, PhotonView>.ValueCollection.Enumerator enumerator = this.photonViewList.Values.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        PhotonView current = enumerator.Current;
        if (current.group == group)
          this.CleanRpcBufferIfMine(current);
      }
    }
  }

  public void SetLevelPrefix(short prefix)
  {
    this.currentLevelPrefix = prefix;
  }

  internal void RPC(PhotonView view, string methodName, PhotonTargets target, PhotonPlayer player, bool encrypt, params object[] parameters)
  {
    if (this.blockSendingGroups.Contains(view.group))
      return;
    if (view.viewID < 1)
      Debug.LogError((object) ("Illegal view ID:" + (object) view.viewID + " method: " + methodName + " GO:" + ((Object) ((Component) view).get_gameObject()).get_name()));
    if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
      Debug.Log((object) ("Sending RPC \"" + methodName + "\" to target: " + (object) target + " or player:" + (object) player + "."));
    Hashtable rpcData = new Hashtable();
    rpcData.set_Item((object) (byte) 0, (object) view.viewID);
    if (view.prefix > 0)
      rpcData.set_Item((object) (byte) 1, (object) (short) view.prefix);
    rpcData.set_Item((object) (byte) 2, (object) PhotonNetwork.ServerTimestamp);
    int num = 0;
    if (this.rpcShortcuts.TryGetValue(methodName, out num))
      rpcData.set_Item((object) (byte) 5, (object) (byte) num);
    else
      rpcData.set_Item((object) (byte) 3, (object) methodName);
    if (parameters != null && parameters.Length > 0)
      rpcData.set_Item((object) (byte) 4, (object) parameters);
    if (player != null)
    {
      if (this.LocalPlayer.ID == player.ID)
      {
        this.ExecuteRpc(rpcData, player);
      }
      else
      {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions()
        {
          TargetActors = new int[1]{ player.ID },
          Encrypt = encrypt
        };
        this.OpRaiseEvent((byte) 200, (object) rpcData, true, raiseEventOptions);
      }
    }
    else
    {
      switch (target)
      {
        case PhotonTargets.All:
          RaiseEventOptions raiseEventOptions1 = new RaiseEventOptions()
          {
            InterestGroup = (byte) view.group,
            Encrypt = encrypt
          };
          this.OpRaiseEvent((byte) 200, (object) rpcData, true, raiseEventOptions1);
          this.ExecuteRpc(rpcData, this.LocalPlayer);
          break;
        case PhotonTargets.Others:
          RaiseEventOptions raiseEventOptions2 = new RaiseEventOptions()
          {
            InterestGroup = (byte) view.group,
            Encrypt = encrypt
          };
          this.OpRaiseEvent((byte) 200, (object) rpcData, true, raiseEventOptions2);
          break;
        case PhotonTargets.MasterClient:
          if (this.mMasterClientId == this.LocalPlayer.ID)
          {
            this.ExecuteRpc(rpcData, this.LocalPlayer);
            break;
          }
          RaiseEventOptions raiseEventOptions3 = new RaiseEventOptions()
          {
            Receivers = ReceiverGroup.MasterClient,
            Encrypt = encrypt
          };
          this.OpRaiseEvent((byte) 200, (object) rpcData, true, raiseEventOptions3);
          break;
        case PhotonTargets.AllBuffered:
          RaiseEventOptions raiseEventOptions4 = new RaiseEventOptions()
          {
            CachingOption = EventCaching.AddToRoomCache,
            Encrypt = encrypt
          };
          this.OpRaiseEvent((byte) 200, (object) rpcData, true, raiseEventOptions4);
          this.ExecuteRpc(rpcData, this.LocalPlayer);
          break;
        case PhotonTargets.OthersBuffered:
          RaiseEventOptions raiseEventOptions5 = new RaiseEventOptions()
          {
            CachingOption = EventCaching.AddToRoomCache,
            Encrypt = encrypt
          };
          this.OpRaiseEvent((byte) 200, (object) rpcData, true, raiseEventOptions5);
          break;
        case PhotonTargets.AllViaServer:
          RaiseEventOptions raiseEventOptions6 = new RaiseEventOptions()
          {
            InterestGroup = (byte) view.group,
            Receivers = ReceiverGroup.All,
            Encrypt = encrypt
          };
          this.OpRaiseEvent((byte) 200, (object) rpcData, true, raiseEventOptions6);
          if (!PhotonNetwork.offlineMode)
            break;
          this.ExecuteRpc(rpcData, this.LocalPlayer);
          break;
        case PhotonTargets.AllBufferedViaServer:
          RaiseEventOptions raiseEventOptions7 = new RaiseEventOptions()
          {
            InterestGroup = (byte) view.group,
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache,
            Encrypt = encrypt
          };
          this.OpRaiseEvent((byte) 200, (object) rpcData, true, raiseEventOptions7);
          if (!PhotonNetwork.offlineMode)
            break;
          this.ExecuteRpc(rpcData, this.LocalPlayer);
          break;
        default:
          Debug.LogError((object) ("Unsupported target enum: " + (object) target));
          break;
      }
    }
  }

  public void SetReceivingEnabled(int group, bool enabled)
  {
    if (group <= 0)
      Debug.LogError((object) ("Error: PhotonNetwork.SetReceivingEnabled was called with an illegal group number: " + (object) group + ". The group number should be at least 1."));
    else if (enabled)
    {
      if (this.allowedReceivingGroups.Contains(group))
        return;
      this.allowedReceivingGroups.Add(group);
      this.OpChangeGroups((byte[]) null, new byte[1]
      {
        (byte) group
      });
    }
    else
    {
      if (!this.allowedReceivingGroups.Contains(group))
        return;
      this.allowedReceivingGroups.Remove(group);
      this.OpChangeGroups(new byte[1]{ (byte) group }, (byte[]) null);
    }
  }

  public void SetReceivingEnabled(int[] enableGroups, int[] disableGroups)
  {
    List<byte> byteList1 = new List<byte>();
    List<byte> byteList2 = new List<byte>();
    if (enableGroups != null)
    {
      for (int index = 0; index < enableGroups.Length; ++index)
      {
        int enableGroup = enableGroups[index];
        if (enableGroup <= 0)
          Debug.LogError((object) ("Error: PhotonNetwork.SetReceivingEnabled was called with an illegal group number: " + (object) enableGroup + ". The group number should be at least 1."));
        else if (!this.allowedReceivingGroups.Contains(enableGroup))
        {
          this.allowedReceivingGroups.Add(enableGroup);
          byteList1.Add((byte) enableGroup);
        }
      }
    }
    if (disableGroups != null)
    {
      for (int index = 0; index < disableGroups.Length; ++index)
      {
        int disableGroup = disableGroups[index];
        if (disableGroup <= 0)
          Debug.LogError((object) ("Error: PhotonNetwork.SetReceivingEnabled was called with an illegal group number: " + (object) disableGroup + ". The group number should be at least 1."));
        else if (byteList1.Contains((byte) disableGroup))
          Debug.LogError((object) ("Error: PhotonNetwork.SetReceivingEnabled disableGroups contains a group that is also in the enableGroups: " + (object) disableGroup + "."));
        else if (this.allowedReceivingGroups.Contains(disableGroup))
        {
          this.allowedReceivingGroups.Remove(disableGroup);
          byteList2.Add((byte) disableGroup);
        }
      }
    }
    this.OpChangeGroups(byteList2.Count <= 0 ? (byte[]) null : byteList2.ToArray(), byteList1.Count <= 0 ? (byte[]) null : byteList1.ToArray());
  }

  public void SetSendingEnabled(int group, bool enabled)
  {
    if (!enabled)
      this.blockSendingGroups.Add(group);
    else
      this.blockSendingGroups.Remove(group);
  }

  public void SetSendingEnabled(int[] enableGroups, int[] disableGroups)
  {
    if (enableGroups != null)
    {
      foreach (int enableGroup in enableGroups)
      {
        if (this.blockSendingGroups.Contains(enableGroup))
          this.blockSendingGroups.Remove(enableGroup);
      }
    }
    if (disableGroups == null)
      return;
    foreach (int disableGroup in disableGroups)
    {
      if (!this.blockSendingGroups.Contains(disableGroup))
        this.blockSendingGroups.Add(disableGroup);
    }
  }

  public void NewSceneLoaded()
  {
    if (this.loadingLevelAndPausedNetwork)
    {
      this.loadingLevelAndPausedNetwork = false;
      PhotonNetwork.isMessageQueueRunning = true;
    }
    List<int> intList = new List<int>();
    using (Dictionary<int, PhotonView>.Enumerator enumerator = this.photonViewList.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        KeyValuePair<int, PhotonView> current = enumerator.Current;
        if (Object.op_Equality((Object) current.Value, (Object) null))
          intList.Add(current.Key);
      }
    }
    for (int index = 0; index < intList.Count; ++index)
      this.photonViewList.Remove(intList[index]);
    if (intList.Count <= 0 || PhotonNetwork.logLevel < PhotonLogLevel.Informational)
      return;
    Debug.Log((object) ("New level loaded. Removed " + (object) intList.Count + " scene view IDs from last level."));
  }

  public void RunViewUpdate()
  {
    if (!PhotonNetwork.connected || PhotonNetwork.offlineMode || (this.mActors == null || this.mActors.Count <= 1))
      return;
    int num = 0;
    RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
    using (Dictionary<int, PhotonView>.ValueCollection.Enumerator enumerator = this.photonViewList.Values.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        PhotonView current = enumerator.Current;
        if (current.synchronization != ViewSynchronization.Off && current.isMine && (((Component) current).get_gameObject().get_activeInHierarchy() && !this.blockSendingGroups.Contains(current.group)))
        {
          object[] objArray = this.OnSerializeWrite(current);
          if (objArray != null)
          {
            if (current.synchronization == ViewSynchronization.ReliableDeltaCompressed || current.mixedModeIsReliable)
            {
              Hashtable hashtable = (Hashtable) null;
              if (!this.dataPerGroupReliable.TryGetValue(current.group, out hashtable))
              {
                hashtable = new Hashtable(NetworkingPeer.ObjectsInOneUpdate);
                this.dataPerGroupReliable[current.group] = hashtable;
              }
              ((Dictionary<object, object>) hashtable).Add((object) (byte) (((Dictionary<object, object>) hashtable).Count + 10), (object) objArray);
              ++num;
              if (((Dictionary<object, object>) hashtable).Count >= NetworkingPeer.ObjectsInOneUpdate)
              {
                num -= ((Dictionary<object, object>) hashtable).Count;
                raiseEventOptions.InterestGroup = (byte) current.group;
                hashtable.set_Item((object) (byte) 0, (object) PhotonNetwork.ServerTimestamp);
                if ((int) this.currentLevelPrefix >= 0)
                  hashtable.set_Item((object) (byte) 1, (object) this.currentLevelPrefix);
                this.OpRaiseEvent((byte) 206, (object) hashtable, true, raiseEventOptions);
                ((Dictionary<object, object>) hashtable).Clear();
              }
            }
            else
            {
              Hashtable hashtable = (Hashtable) null;
              if (!this.dataPerGroupUnreliable.TryGetValue(current.group, out hashtable))
              {
                hashtable = new Hashtable(NetworkingPeer.ObjectsInOneUpdate);
                this.dataPerGroupUnreliable[current.group] = hashtable;
              }
              ((Dictionary<object, object>) hashtable).Add((object) (byte) (((Dictionary<object, object>) hashtable).Count + 10), (object) objArray);
              ++num;
              if (((Dictionary<object, object>) hashtable).Count >= NetworkingPeer.ObjectsInOneUpdate)
              {
                num -= ((Dictionary<object, object>) hashtable).Count;
                raiseEventOptions.InterestGroup = (byte) current.group;
                hashtable.set_Item((object) (byte) 0, (object) PhotonNetwork.ServerTimestamp);
                if ((int) this.currentLevelPrefix >= 0)
                  hashtable.set_Item((object) (byte) 1, (object) this.currentLevelPrefix);
                this.OpRaiseEvent((byte) 201, (object) hashtable, false, raiseEventOptions);
                ((Dictionary<object, object>) hashtable).Clear();
              }
            }
          }
        }
      }
    }
    if (num == 0)
      return;
    using (Dictionary<int, Hashtable>.KeyCollection.Enumerator enumerator = this.dataPerGroupReliable.Keys.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        int current = enumerator.Current;
        raiseEventOptions.InterestGroup = (byte) current;
        Hashtable hashtable = this.dataPerGroupReliable[current];
        if (((Dictionary<object, object>) hashtable).Count != 0)
        {
          hashtable.set_Item((object) (byte) 0, (object) PhotonNetwork.ServerTimestamp);
          if ((int) this.currentLevelPrefix >= 0)
            hashtable.set_Item((object) (byte) 1, (object) this.currentLevelPrefix);
          this.OpRaiseEvent((byte) 206, (object) hashtable, true, raiseEventOptions);
          ((Dictionary<object, object>) hashtable).Clear();
        }
      }
    }
    using (Dictionary<int, Hashtable>.KeyCollection.Enumerator enumerator = this.dataPerGroupUnreliable.Keys.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        int current = enumerator.Current;
        raiseEventOptions.InterestGroup = (byte) current;
        Hashtable hashtable = this.dataPerGroupUnreliable[current];
        if (((Dictionary<object, object>) hashtable).Count != 0)
        {
          hashtable.set_Item((object) (byte) 0, (object) PhotonNetwork.ServerTimestamp);
          if ((int) this.currentLevelPrefix >= 0)
            hashtable.set_Item((object) (byte) 1, (object) this.currentLevelPrefix);
          this.OpRaiseEvent((byte) 201, (object) hashtable, false, raiseEventOptions);
          ((Dictionary<object, object>) hashtable).Clear();
        }
      }
    }
  }

  private object[] OnSerializeWrite(PhotonView view)
  {
    if (view.synchronization == ViewSynchronization.Off)
      return (object[]) null;
    PhotonMessageInfo info = new PhotonMessageInfo(this.LocalPlayer, PhotonNetwork.ServerTimestamp, view);
    this.pStream.ResetWriteStream();
    this.pStream.SendNext((object) view.viewID);
    this.pStream.SendNext((object) false);
    this.pStream.SendNext((object) null);
    view.SerializeView(this.pStream, info);
    if (this.pStream.Count <= 3)
      return (object[]) null;
    if (view.synchronization == ViewSynchronization.Unreliable)
      return this.pStream.ToArray();
    object[] array = this.pStream.ToArray();
    if (view.synchronization == ViewSynchronization.UnreliableOnChange)
    {
      if (this.AlmostEquals(array, view.lastOnSerializeDataSent))
      {
        if (view.mixedModeIsReliable)
          return (object[]) null;
        view.mixedModeIsReliable = true;
        view.lastOnSerializeDataSent = array;
      }
      else
      {
        view.mixedModeIsReliable = false;
        view.lastOnSerializeDataSent = array;
      }
      return array;
    }
    if (view.synchronization != ViewSynchronization.ReliableDeltaCompressed)
      return (object[]) null;
    object[] objArray = this.DeltaCompressionWrite(view.lastOnSerializeDataSent, array);
    view.lastOnSerializeDataSent = array;
    return objArray;
  }

  private void OnSerializeRead(object[] data, PhotonPlayer sender, int networkTime, short correctPrefix)
  {
    int viewID = (int) data[0];
    PhotonView photonView = this.GetPhotonView(viewID);
    if (Object.op_Equality((Object) photonView, (Object) null))
      Debug.LogWarning((object) ("Received OnSerialization for view ID " + (object) viewID + ". We have no such PhotonView! Ignored this if you're leaving a room. State: " + (object) this.State));
    else if (photonView.prefix > 0 && (int) correctPrefix != photonView.prefix)
    {
      Debug.LogError((object) ("Received OnSerialization for view ID " + (object) viewID + " with prefix " + (object) correctPrefix + ". Our prefix is " + (object) photonView.prefix));
    }
    else
    {
      if (photonView.group != 0 && !this.allowedReceivingGroups.Contains(photonView.group))
        return;
      if (photonView.synchronization == ViewSynchronization.ReliableDeltaCompressed)
      {
        object[] objArray = this.DeltaCompressionRead(photonView.lastOnSerializeDataReceived, data);
        if (objArray == null)
        {
          if (PhotonNetwork.logLevel < PhotonLogLevel.Informational)
            return;
          Debug.Log((object) ("Skipping packet for " + ((Object) photonView).get_name() + " [" + (object) photonView.viewID + "] as we haven't received a full packet for delta compression yet. This is OK if it happens for the first few frames after joining a game."));
          return;
        }
        photonView.lastOnSerializeDataReceived = objArray;
        data = objArray;
      }
      if (sender.ID != photonView.ownerId && (!photonView.OwnerShipWasTransfered || photonView.ownerId == 0) && photonView.currentMasterID == -1)
        photonView.ownerId = sender.ID;
      this.readStream.SetReadStream(data, (byte) 3);
      PhotonMessageInfo info = new PhotonMessageInfo(sender, networkTime, photonView);
      photonView.DeserializeView(this.readStream, info);
    }
  }

  private object[] DeltaCompressionWrite(object[] previousContent, object[] currentContent)
  {
    if (currentContent == null || previousContent == null || previousContent.Length != currentContent.Length)
      return currentContent;
    if (currentContent.Length <= 3)
      return (object[]) null;
    object[] objArray = previousContent;
    objArray[1] = (object) false;
    int num = 0;
    Queue<int> intQueue = (Queue<int>) null;
    for (int index = 3; index < currentContent.Length; ++index)
    {
      object one = currentContent[index];
      object two = previousContent[index];
      if (this.AlmostEquals(one, two))
      {
        ++num;
        objArray[index] = (object) null;
      }
      else
      {
        objArray[index] = one;
        if (one == null)
        {
          if (intQueue == null)
            intQueue = new Queue<int>(currentContent.Length);
          intQueue.Enqueue(index);
        }
      }
    }
    if (num > 0)
    {
      if (num == currentContent.Length - 3)
        return (object[]) null;
      objArray[1] = (object) true;
      if (intQueue != null)
        objArray[2] = (object) intQueue.ToArray();
    }
    objArray[0] = currentContent[0];
    return objArray;
  }

  private object[] DeltaCompressionRead(object[] lastOnSerializeDataReceived, object[] incomingData)
  {
    if (!(bool) incomingData[1])
      return incomingData;
    if (lastOnSerializeDataReceived == null)
      return (object[]) null;
    int[] target = incomingData[2] as int[];
    for (int nr = 3; nr < incomingData.Length; ++nr)
    {
      if ((target == null || !Extensions.Contains(target, nr)) && incomingData[nr] == null)
      {
        object obj = lastOnSerializeDataReceived[nr];
        incomingData[nr] = obj;
      }
    }
    return incomingData;
  }

  private bool AlmostEquals(object[] lastData, object[] currentContent)
  {
    if (lastData == null && currentContent == null)
      return true;
    if (lastData == null || currentContent == null || lastData.Length != currentContent.Length)
      return false;
    for (int index = 0; index < currentContent.Length; ++index)
    {
      if (!this.AlmostEquals(currentContent[index], lastData[index]))
        return false;
    }
    return true;
  }

  private bool AlmostEquals(object one, object two)
  {
    if (one == null || two == null)
    {
      if (one == null)
        return two == null;
      return false;
    }
    if (one.Equals(two))
      return true;
    if (one is Vector3)
    {
      if (((Vector3) one).AlmostEquals((Vector3) two, PhotonNetwork.precisionForVectorSynchronization))
        return true;
    }
    else if (one is Vector2)
    {
      if (((Vector2) one).AlmostEquals((Vector2) two, PhotonNetwork.precisionForVectorSynchronization))
        return true;
    }
    else if (one is Quaternion)
    {
      if (((Quaternion) one).AlmostEquals((Quaternion) two, PhotonNetwork.precisionForQuaternionSynchronization))
        return true;
    }
    else if (one is float && ((float) one).AlmostEquals((float) two, PhotonNetwork.precisionForFloatSynchronization))
      return true;
    return false;
  }

  protected internal static bool GetMethod(MonoBehaviour monob, string methodType, out MethodInfo mi)
  {
    mi = (MethodInfo) null;
    if (Object.op_Equality((Object) monob, (Object) null) || string.IsNullOrEmpty(methodType))
      return false;
    List<MethodInfo> methods = SupportClass.GetMethods(((object) monob).GetType(), (System.Type) null);
    for (int index = 0; index < methods.Count; ++index)
    {
      MethodInfo methodInfo = methods[index];
      if (methodInfo.Name.Equals(methodType))
      {
        mi = methodInfo;
        return true;
      }
    }
    return false;
  }

  protected internal void LoadLevelIfSynced()
  {
    if (!PhotonNetwork.automaticallySyncScene || PhotonNetwork.isMasterClient || (PhotonNetwork.room == null || !((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).ContainsKey((object) "curScn")))
      return;
    object obj = PhotonNetwork.room.CustomProperties.get_Item((object) "curScn");
    if (obj is int)
    {
      if (SceneManagerHelper.ActiveSceneBuildIndex == (int) obj)
        return;
      PhotonNetwork.LoadLevel((int) obj);
    }
    else
    {
      if (!(obj is string) || !(SceneManagerHelper.ActiveSceneName != (string) obj))
        return;
      PhotonNetwork.LoadLevel((string) obj);
    }
  }

  protected internal void SetLevelInPropsIfSynced(object levelId)
  {
    if (!PhotonNetwork.automaticallySyncScene || !PhotonNetwork.isMasterClient || PhotonNetwork.room == null)
      return;
    if (levelId == null)
    {
      Debug.LogError((object) "Parameter levelId can't be null!");
    }
    else
    {
      if (((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).ContainsKey((object) "curScn"))
      {
        object obj = PhotonNetwork.room.CustomProperties.get_Item((object) "curScn");
        if (obj is int && SceneManagerHelper.ActiveSceneBuildIndex == (int) obj || obj is string && SceneManagerHelper.ActiveSceneName != null && SceneManagerHelper.ActiveSceneName.Equals((string) obj))
          return;
      }
      Hashtable propertiesToSet = new Hashtable();
      if (levelId is int)
        propertiesToSet.set_Item((object) "curScn", (object) (int) levelId);
      else if (levelId is string)
        propertiesToSet.set_Item((object) "curScn", (object) (string) levelId);
      else
        Debug.LogError((object) "Parameter levelId must be int or string!");
      PhotonNetwork.room.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
      this.SendOutgoingCommands();
    }
  }

  public void SetApp(string appId, string gameVersion)
  {
    this.AppId = appId.Trim();
    if (string.IsNullOrEmpty(gameVersion))
      return;
    PhotonNetwork.gameVersion = gameVersion.Trim();
  }

  public bool WebRpc(string uriPath, object parameters)
  {
    return this.OpCustom((byte) 219, new Dictionary<byte, object>()
    {
      {
        (byte) 209,
        (object) uriPath
      },
      {
        (byte) 208,
        parameters
      }
    }, true);
  }
}
