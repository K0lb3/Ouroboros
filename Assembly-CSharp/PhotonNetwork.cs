// Decompiled with JetBrains decompiler
// Type: PhotonNetwork
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PhotonNetwork
{
  public static readonly int MAX_VIEW_IDS = 1000;
  public static ServerSettings PhotonServerSettings = (ServerSettings) Resources.Load(nameof (PhotonServerSettings), typeof (ServerSettings));
  public static bool InstantiateInRoomOnly = true;
  public static PhotonLogLevel logLevel = PhotonLogLevel.ErrorsOnly;
  public static float precisionForVectorSynchronization = 9.9E-05f;
  public static float precisionForQuaternionSynchronization = 1f;
  public static float precisionForFloatSynchronization = 0.01f;
  public static bool UsePrefabCache = true;
  public static Dictionary<string, GameObject> PrefabCache = new Dictionary<string, GameObject>();
  public static System.Type SendMonoMessageTargetType = typeof (MonoBehaviour);
  public static bool StartRpcsAsCoroutine = true;
  private static bool isOfflineMode = false;
  private static Room offlineModeRoom = (Room) null;
  private static bool _mAutomaticallySyncScene = false;
  private static bool m_autoCleanUpPlayerObjects = true;
  private static int sendInterval = 50;
  private static int sendIntervalOnSerialize = 100;
  private static bool m_isMessageQueueRunning = true;
  private static bool UsePreciseTimer = false;
  public static float BackgroundTimeout = 60f;
  internal static int lastUsedViewSubId = 0;
  internal static int lastUsedViewSubIdStatic = 0;
  internal static List<int> manuallyAllocatedViewIds = new List<int>();
  public const string versionPUN = "1.80";
  internal const string serverSettingsAssetFile = "PhotonServerSettings";
  internal const string serverSettingsAssetPath = "Assets/Photon Unity Networking/Resources/PhotonServerSettings.asset";
  internal static readonly PhotonHandler photonMono;
  internal static NetworkingPeer networkingPeer;
  public static bool UseRpcMonoBehaviourCache;
  public static HashSet<GameObject> SendMonoMessageTargets;
  [Obsolete("Used for compatibility with Unity networking only.")]
  public static int maxConnections;
  private static Stopwatch startupStopwatch;
  public static PhotonNetwork.EventCallback OnEventCall;

  static PhotonNetwork()
  {
    if (Object.op_Inequality((Object) PhotonNetwork.PhotonServerSettings, (Object) null))
      Application.set_runInBackground(PhotonNetwork.PhotonServerSettings.RunInBackground);
    GameObject gameObject = new GameObject();
    PhotonNetwork.photonMono = (PhotonHandler) gameObject.AddComponent<PhotonHandler>();
    ((Object) gameObject).set_name("PhotonMono");
    ((Object) gameObject).set_hideFlags((HideFlags) 1);
    ConnectionProtocol protocol = PhotonNetwork.PhotonServerSettings.Protocol;
    PhotonNetwork.networkingPeer = new NetworkingPeer(string.Empty, protocol);
    PhotonNetwork.networkingPeer.set_QuickResendAttempts((byte) 2);
    PhotonNetwork.networkingPeer.SentCountAllowance = (__Null) 7;
    if (PhotonNetwork.UsePreciseTimer)
    {
      Debug.Log((object) "Using Stopwatch as precision timer for PUN.");
      PhotonNetwork.startupStopwatch = new Stopwatch();
      PhotonNetwork.startupStopwatch.Start();
      NetworkingPeer networkingPeer = PhotonNetwork.networkingPeer;
      // ISSUE: reference to a compiler-generated field
      if (PhotonNetwork.\u003C\u003Ef__am\u0024cache20 == null)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        PhotonNetwork.\u003C\u003Ef__am\u0024cache20 = new SupportClass.IntegerMillisecondsDelegate((object) null, __methodptr(\u003CPhotonNetwork\u003Em__E5));
      }
      // ISSUE: reference to a compiler-generated field
      SupportClass.IntegerMillisecondsDelegate fAmCache20 = PhotonNetwork.\u003C\u003Ef__am\u0024cache20;
      networkingPeer.set_LocalMsTimestampDelegate(fAmCache20);
    }
    CustomTypes.Register();
  }

  public static string gameVersion { get; set; }

  public static string ServerAddress
  {
    get
    {
      if (PhotonNetwork.networkingPeer != null)
        return PhotonNetwork.networkingPeer.get_ServerAddress();
      return "<not connected>";
    }
  }

  public static bool connected
  {
    get
    {
      if (PhotonNetwork.offlineMode)
        return true;
      if (PhotonNetwork.networkingPeer == null || (PhotonNetwork.networkingPeer.IsInitialConnect || PhotonNetwork.networkingPeer.State == ClientState.PeerCreated || (PhotonNetwork.networkingPeer.State == ClientState.Disconnected || PhotonNetwork.networkingPeer.State == ClientState.Disconnecting)))
        return false;
      return PhotonNetwork.networkingPeer.State != ClientState.ConnectingToNameServer;
    }
  }

  public static bool connecting
  {
    get
    {
      if (PhotonNetwork.networkingPeer.IsInitialConnect)
        return !PhotonNetwork.offlineMode;
      return false;
    }
  }

  public static bool connectedAndReady
  {
    get
    {
      if (!PhotonNetwork.connected)
        return false;
      if (PhotonNetwork.offlineMode)
        return true;
      ClientState connectionStateDetailed = PhotonNetwork.connectionStateDetailed;
      switch (connectionStateDetailed)
      {
        case ClientState.ConnectingToMasterserver:
        case ClientState.Disconnecting:
        case ClientState.Disconnected:
        case ClientState.ConnectingToNameServer:
        case ClientState.Authenticating:
label_7:
          return false;
        default:
          switch (connectionStateDetailed - 6)
          {
            case ClientState.Uninitialized:
            case ClientState.Queued:
              goto label_7;
            default:
              if (connectionStateDetailed != ClientState.PeerCreated)
                return true;
              goto label_7;
          }
      }
    }
  }

  public static ConnectionState connectionState
  {
    get
    {
      if (PhotonNetwork.offlineMode)
        return ConnectionState.Connected;
      if (PhotonNetwork.networkingPeer == null)
        return ConnectionState.Disconnected;
      PeerStateValue peerState = PhotonNetwork.networkingPeer.get_PeerState();
      switch ((int) peerState)
      {
        case 0:
          return ConnectionState.Disconnected;
        case 1:
          return ConnectionState.Connecting;
        case 3:
          return ConnectionState.Connected;
        case 4:
          return ConnectionState.Disconnecting;
        default:
          return peerState == 10 ? ConnectionState.InitializingApplication : ConnectionState.Disconnected;
      }
    }
  }

  public static ClientState connectionStateDetailed
  {
    get
    {
      if (PhotonNetwork.offlineMode)
        return PhotonNetwork.offlineModeRoom != null ? ClientState.Joined : ClientState.ConnectedToMaster;
      if (PhotonNetwork.networkingPeer == null)
        return ClientState.Disconnected;
      return PhotonNetwork.networkingPeer.State;
    }
  }

  public static ServerConnection Server
  {
    get
    {
      if (PhotonNetwork.networkingPeer != null)
        return PhotonNetwork.networkingPeer.Server;
      return ServerConnection.NameServer;
    }
  }

  public static AuthenticationValues AuthValues
  {
    get
    {
      if (PhotonNetwork.networkingPeer != null)
        return PhotonNetwork.networkingPeer.AuthValues;
      return (AuthenticationValues) null;
    }
    set
    {
      if (PhotonNetwork.networkingPeer == null)
        return;
      PhotonNetwork.networkingPeer.AuthValues = value;
    }
  }

  public static Room room
  {
    get
    {
      if (PhotonNetwork.isOfflineMode)
        return PhotonNetwork.offlineModeRoom;
      return PhotonNetwork.networkingPeer.CurrentRoom;
    }
  }

  public static PhotonPlayer player
  {
    get
    {
      if (PhotonNetwork.networkingPeer == null)
        return (PhotonPlayer) null;
      return PhotonNetwork.networkingPeer.LocalPlayer;
    }
  }

  public static PhotonPlayer masterClient
  {
    get
    {
      if (PhotonNetwork.offlineMode)
        return PhotonNetwork.player;
      if (PhotonNetwork.networkingPeer == null)
        return (PhotonPlayer) null;
      return PhotonNetwork.networkingPeer.GetPlayerWithId(PhotonNetwork.networkingPeer.mMasterClientId);
    }
  }

  public static string playerName
  {
    get
    {
      return PhotonNetwork.networkingPeer.PlayerName;
    }
    set
    {
      PhotonNetwork.networkingPeer.PlayerName = value;
    }
  }

  public static PhotonPlayer[] playerList
  {
    get
    {
      if (PhotonNetwork.networkingPeer == null)
        return new PhotonPlayer[0];
      return PhotonNetwork.networkingPeer.mPlayerListCopy;
    }
  }

  public static PhotonPlayer[] otherPlayers
  {
    get
    {
      if (PhotonNetwork.networkingPeer == null)
        return new PhotonPlayer[0];
      return PhotonNetwork.networkingPeer.mOtherPlayerListCopy;
    }
  }

  public static List<FriendInfo> Friends { get; internal set; }

  public static int FriendsListAge
  {
    get
    {
      if (PhotonNetwork.networkingPeer != null)
        return PhotonNetwork.networkingPeer.FriendListAge;
      return 0;
    }
  }

  public static IPunPrefabPool PrefabPool
  {
    get
    {
      return PhotonNetwork.networkingPeer.ObjectPool;
    }
    set
    {
      PhotonNetwork.networkingPeer.ObjectPool = value;
    }
  }

  public static bool offlineMode
  {
    get
    {
      return PhotonNetwork.isOfflineMode;
    }
    set
    {
      if (value == PhotonNetwork.isOfflineMode)
        return;
      if (value && PhotonNetwork.connected)
      {
        Debug.LogError((object) "Can't start OFFLINE mode while connected!");
      }
      else
      {
        if (PhotonNetwork.networkingPeer.get_PeerState() != null)
          PhotonNetwork.networkingPeer.Disconnect();
        PhotonNetwork.isOfflineMode = value;
        if (PhotonNetwork.isOfflineMode)
        {
          PhotonNetwork.networkingPeer.ChangeLocalID(-1);
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectedToMaster);
        }
        else
        {
          PhotonNetwork.offlineModeRoom = (Room) null;
          PhotonNetwork.networkingPeer.ChangeLocalID(-1);
        }
      }
    }
  }

  public static bool automaticallySyncScene
  {
    get
    {
      return PhotonNetwork._mAutomaticallySyncScene;
    }
    set
    {
      PhotonNetwork._mAutomaticallySyncScene = value;
      if (!PhotonNetwork._mAutomaticallySyncScene || PhotonNetwork.room == null)
        return;
      PhotonNetwork.networkingPeer.LoadLevelIfSynced();
    }
  }

  public static bool autoCleanUpPlayerObjects
  {
    get
    {
      return PhotonNetwork.m_autoCleanUpPlayerObjects;
    }
    set
    {
      if (PhotonNetwork.room != null)
        Debug.LogError((object) "Setting autoCleanUpPlayerObjects while in a room is not supported.");
      else
        PhotonNetwork.m_autoCleanUpPlayerObjects = value;
    }
  }

  public static bool autoJoinLobby
  {
    get
    {
      return PhotonNetwork.PhotonServerSettings.JoinLobby;
    }
    set
    {
      PhotonNetwork.PhotonServerSettings.JoinLobby = value;
    }
  }

  public static bool EnableLobbyStatistics
  {
    get
    {
      return PhotonNetwork.PhotonServerSettings.EnableLobbyStatistics;
    }
    set
    {
      PhotonNetwork.PhotonServerSettings.EnableLobbyStatistics = value;
    }
  }

  public static List<TypedLobbyInfo> LobbyStatistics
  {
    get
    {
      return PhotonNetwork.networkingPeer.LobbyStatistics;
    }
    private set
    {
      PhotonNetwork.networkingPeer.LobbyStatistics = value;
    }
  }

  public static bool insideLobby
  {
    get
    {
      return PhotonNetwork.networkingPeer.insideLobby;
    }
  }

  public static TypedLobby lobby
  {
    get
    {
      return PhotonNetwork.networkingPeer.lobby;
    }
    set
    {
      PhotonNetwork.networkingPeer.lobby = value;
    }
  }

  public static int sendRate
  {
    get
    {
      return 1000 / PhotonNetwork.sendInterval;
    }
    set
    {
      PhotonNetwork.sendInterval = 1000 / value;
      if (Object.op_Inequality((Object) PhotonNetwork.photonMono, (Object) null))
        PhotonNetwork.photonMono.updateInterval = PhotonNetwork.sendInterval;
      if (value >= PhotonNetwork.sendRateOnSerialize)
        return;
      PhotonNetwork.sendRateOnSerialize = value;
    }
  }

  public static int sendRateOnSerialize
  {
    get
    {
      return 1000 / PhotonNetwork.sendIntervalOnSerialize;
    }
    set
    {
      if (value > PhotonNetwork.sendRate)
      {
        Debug.LogError((object) "Error: Can not set the OnSerialize rate higher than the overall SendRate.");
        value = PhotonNetwork.sendRate;
      }
      PhotonNetwork.sendIntervalOnSerialize = 1000 / value;
      if (!Object.op_Inequality((Object) PhotonNetwork.photonMono, (Object) null))
        return;
      PhotonNetwork.photonMono.updateIntervalOnSerialize = PhotonNetwork.sendIntervalOnSerialize;
    }
  }

  public static bool isMessageQueueRunning
  {
    get
    {
      return PhotonNetwork.m_isMessageQueueRunning;
    }
    set
    {
      if (value)
        PhotonHandler.StartFallbackSendAckThread();
      PhotonNetwork.networkingPeer.set_IsSendingOnlyAcks(!value);
      PhotonNetwork.m_isMessageQueueRunning = value;
    }
  }

  public static int unreliableCommandsLimit
  {
    get
    {
      return PhotonNetwork.networkingPeer.get_LimitOfUnreliableCommands();
    }
    set
    {
      PhotonNetwork.networkingPeer.set_LimitOfUnreliableCommands(value);
    }
  }

  public static double time
  {
    get
    {
      return (double) (uint) PhotonNetwork.ServerTimestamp / 1000.0;
    }
  }

  public static int ServerTimestamp
  {
    get
    {
      if (!PhotonNetwork.offlineMode)
        return PhotonNetwork.networkingPeer.get_ServerTimeInMilliSeconds();
      if (PhotonNetwork.UsePreciseTimer && PhotonNetwork.startupStopwatch != null && PhotonNetwork.startupStopwatch.IsRunning)
        return (int) PhotonNetwork.startupStopwatch.ElapsedMilliseconds;
      return Environment.TickCount;
    }
  }

  public static bool isMasterClient
  {
    get
    {
      if (PhotonNetwork.offlineMode)
        return true;
      return PhotonNetwork.networkingPeer.mMasterClientId == PhotonNetwork.player.ID;
    }
  }

  public static bool inRoom
  {
    get
    {
      return PhotonNetwork.connectionStateDetailed == ClientState.Joined;
    }
  }

  public static bool isNonMasterClientInRoom
  {
    get
    {
      if (!PhotonNetwork.isMasterClient)
        return PhotonNetwork.room != null;
      return false;
    }
  }

  public static int countOfPlayersOnMaster
  {
    get
    {
      return PhotonNetwork.networkingPeer.PlayersOnMasterCount;
    }
  }

  public static int countOfPlayersInRooms
  {
    get
    {
      return PhotonNetwork.networkingPeer.PlayersInRoomsCount;
    }
  }

  public static int countOfPlayers
  {
    get
    {
      return PhotonNetwork.networkingPeer.PlayersInRoomsCount + PhotonNetwork.networkingPeer.PlayersOnMasterCount;
    }
  }

  public static int countOfRooms
  {
    get
    {
      return PhotonNetwork.networkingPeer.RoomsCount;
    }
  }

  public static bool NetworkStatisticsEnabled
  {
    get
    {
      return PhotonNetwork.networkingPeer.get_TrafficStatsEnabled();
    }
    set
    {
      PhotonNetwork.networkingPeer.set_TrafficStatsEnabled(value);
    }
  }

  public static int ResentReliableCommands
  {
    get
    {
      return PhotonNetwork.networkingPeer.get_ResentReliableCommands();
    }
  }

  public static bool CrcCheckEnabled
  {
    get
    {
      return PhotonNetwork.networkingPeer.get_CrcEnabled();
    }
    set
    {
      if (!PhotonNetwork.connected && !PhotonNetwork.connecting)
        PhotonNetwork.networkingPeer.set_CrcEnabled(value);
      else
        Debug.Log((object) ("Can't change CrcCheckEnabled while being connected. CrcCheckEnabled stays " + (object) PhotonNetwork.networkingPeer.get_CrcEnabled()));
    }
  }

  public static int PacketLossByCrcCheck
  {
    get
    {
      return PhotonNetwork.networkingPeer.get_PacketLossByCrc();
    }
  }

  public static int MaxResendsBeforeDisconnect
  {
    get
    {
      return (int) PhotonNetwork.networkingPeer.SentCountAllowance;
    }
    set
    {
      if (value < 3)
        value = 3;
      if (value > 10)
        value = 10;
      PhotonNetwork.networkingPeer.SentCountAllowance = (__Null) value;
    }
  }

  public static int QuickResends
  {
    get
    {
      return (int) PhotonNetwork.networkingPeer.get_QuickResendAttempts();
    }
    set
    {
      if (value < 0)
        value = 0;
      if (value > 3)
        value = 3;
      PhotonNetwork.networkingPeer.set_QuickResendAttempts((byte) value);
    }
  }

  public static void SwitchToProtocol(ConnectionProtocol cp)
  {
    PhotonNetwork.networkingPeer.set_TransportProtocol(cp);
  }

  public static bool ConnectUsingSettings(string gameVersion)
  {
    if (PhotonNetwork.networkingPeer.get_PeerState() != null)
    {
      Debug.LogWarning((object) ("ConnectUsingSettings() failed. Can only connect while in state 'Disconnected'. Current state: " + (object) PhotonNetwork.networkingPeer.get_PeerState()));
      return false;
    }
    if (Object.op_Equality((Object) PhotonNetwork.PhotonServerSettings, (Object) null))
    {
      Debug.LogError((object) "Can't connect: Loading settings failed. ServerSettings asset must be in any 'Resources' folder as: PhotonServerSettings");
      return false;
    }
    if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.NotSet)
    {
      Debug.LogError((object) "You did not select a Hosting Type in your PhotonServerSettings. Please set it up or don't use ConnectUsingSettings().");
      return false;
    }
    PhotonNetwork.logLevel = PhotonNetwork.PhotonServerSettings.PunLogging;
    PhotonNetwork.networkingPeer.DebugOut = (__Null) PhotonNetwork.PhotonServerSettings.NetworkLogging;
    PhotonNetwork.SwitchToProtocol(PhotonNetwork.PhotonServerSettings.Protocol);
    PhotonNetwork.networkingPeer.SetApp(PhotonNetwork.PhotonServerSettings.AppID, gameVersion);
    if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.OfflineMode)
    {
      PhotonNetwork.offlineMode = true;
      return true;
    }
    if (PhotonNetwork.offlineMode)
      Debug.LogWarning((object) "ConnectUsingSettings() disabled the offline mode. No longer offline.");
    PhotonNetwork.offlineMode = false;
    PhotonNetwork.isMessageQueueRunning = true;
    PhotonNetwork.networkingPeer.IsInitialConnect = true;
    if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.SelfHosted)
    {
      PhotonNetwork.networkingPeer.IsUsingNameServer = false;
      PhotonNetwork.networkingPeer.MasterServerAddress = PhotonNetwork.PhotonServerSettings.ServerPort != 0 ? PhotonNetwork.PhotonServerSettings.ServerAddress + ":" + (object) PhotonNetwork.PhotonServerSettings.ServerPort : PhotonNetwork.PhotonServerSettings.ServerAddress;
      return PhotonNetwork.networkingPeer.Connect(PhotonNetwork.networkingPeer.MasterServerAddress, ServerConnection.MasterServer);
    }
    if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.BestRegion)
      return PhotonNetwork.ConnectToBestCloudServer(gameVersion);
    return PhotonNetwork.networkingPeer.ConnectToRegionMaster(PhotonNetwork.PhotonServerSettings.PreferredRegion);
  }

  public static bool ConnectToMaster(string masterServerAddress, int port, string appID, string gameVersion)
  {
    if (PhotonNetwork.networkingPeer.get_PeerState() != null)
    {
      Debug.LogWarning((object) ("ConnectToMaster() failed. Can only connect while in state 'Disconnected'. Current state: " + (object) PhotonNetwork.networkingPeer.get_PeerState()));
      return false;
    }
    if (PhotonNetwork.offlineMode)
    {
      PhotonNetwork.offlineMode = false;
      Debug.LogWarning((object) "ConnectToMaster() disabled the offline mode. No longer offline.");
    }
    if (!PhotonNetwork.isMessageQueueRunning)
    {
      PhotonNetwork.isMessageQueueRunning = true;
      Debug.LogWarning((object) "ConnectToMaster() enabled isMessageQueueRunning. Needs to be able to dispatch incoming messages.");
    }
    PhotonNetwork.networkingPeer.SetApp(appID, gameVersion);
    PhotonNetwork.networkingPeer.IsUsingNameServer = false;
    PhotonNetwork.networkingPeer.IsInitialConnect = true;
    PhotonNetwork.networkingPeer.MasterServerAddress = port != 0 ? masterServerAddress + ":" + (object) port : masterServerAddress;
    return PhotonNetwork.networkingPeer.Connect(PhotonNetwork.networkingPeer.MasterServerAddress, ServerConnection.MasterServer);
  }

  public static bool Reconnect()
  {
    if (string.IsNullOrEmpty(PhotonNetwork.networkingPeer.MasterServerAddress))
    {
      Debug.LogWarning((object) ("Reconnect() failed. It seems the client wasn't connected before?! Current state: " + (object) PhotonNetwork.networkingPeer.get_PeerState()));
      return false;
    }
    if (PhotonNetwork.networkingPeer.get_PeerState() != null)
    {
      Debug.LogWarning((object) ("Reconnect() failed. Can only connect while in state 'Disconnected'. Current state: " + (object) PhotonNetwork.networkingPeer.get_PeerState()));
      return false;
    }
    if (PhotonNetwork.offlineMode)
    {
      PhotonNetwork.offlineMode = false;
      Debug.LogWarning((object) "Reconnect() disabled the offline mode. No longer offline.");
    }
    if (!PhotonNetwork.isMessageQueueRunning)
    {
      PhotonNetwork.isMessageQueueRunning = true;
      Debug.LogWarning((object) "Reconnect() enabled isMessageQueueRunning. Needs to be able to dispatch incoming messages.");
    }
    PhotonNetwork.networkingPeer.IsUsingNameServer = false;
    PhotonNetwork.networkingPeer.IsInitialConnect = false;
    return PhotonNetwork.networkingPeer.ReconnectToMaster();
  }

  public static bool ReconnectAndRejoin()
  {
    if (PhotonNetwork.networkingPeer.get_PeerState() != null)
    {
      Debug.LogWarning((object) ("ReconnectAndRejoin() failed. Can only connect while in state 'Disconnected'. Current state: " + (object) PhotonNetwork.networkingPeer.get_PeerState()));
      return false;
    }
    if (PhotonNetwork.offlineMode)
    {
      PhotonNetwork.offlineMode = false;
      Debug.LogWarning((object) "ReconnectAndRejoin() disabled the offline mode. No longer offline.");
    }
    if (string.IsNullOrEmpty(PhotonNetwork.networkingPeer.GameServerAddress))
    {
      Debug.LogWarning((object) "ReconnectAndRejoin() failed. It seems the client wasn't connected to a game server before (no address).");
      return false;
    }
    if (PhotonNetwork.networkingPeer.enterRoomParamsCache == null)
    {
      Debug.LogWarning((object) "ReconnectAndRejoin() failed. It seems the client doesn't have any previous room to re-join.");
      return false;
    }
    if (!PhotonNetwork.isMessageQueueRunning)
    {
      PhotonNetwork.isMessageQueueRunning = true;
      Debug.LogWarning((object) "ReconnectAndRejoin() enabled isMessageQueueRunning. Needs to be able to dispatch incoming messages.");
    }
    PhotonNetwork.networkingPeer.IsUsingNameServer = false;
    PhotonNetwork.networkingPeer.IsInitialConnect = false;
    return PhotonNetwork.networkingPeer.ReconnectAndRejoin();
  }

  public static bool ConnectToBestCloudServer(string gameVersion)
  {
    if (PhotonNetwork.networkingPeer.get_PeerState() != null)
    {
      Debug.LogWarning((object) ("ConnectToBestCloudServer() failed. Can only connect while in state 'Disconnected'. Current state: " + (object) PhotonNetwork.networkingPeer.get_PeerState()));
      return false;
    }
    if (Object.op_Equality((Object) PhotonNetwork.PhotonServerSettings, (Object) null))
    {
      Debug.LogError((object) "Can't connect: Loading settings failed. ServerSettings asset must be in any 'Resources' folder as: PhotonServerSettings");
      return false;
    }
    if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.OfflineMode)
      return PhotonNetwork.ConnectUsingSettings(gameVersion);
    PhotonNetwork.networkingPeer.IsInitialConnect = true;
    PhotonNetwork.networkingPeer.SetApp(PhotonNetwork.PhotonServerSettings.AppID, gameVersion);
    CloudRegionCode codeInPreferences = PhotonHandler.BestRegionCodeInPreferences;
    if (codeInPreferences == CloudRegionCode.none)
      return PhotonNetwork.networkingPeer.ConnectToNameServer();
    Debug.Log((object) ("Best region found in PlayerPrefs. Connecting to: " + (object) codeInPreferences));
    return PhotonNetwork.networkingPeer.ConnectToRegionMaster(codeInPreferences);
  }

  public static bool ConnectToRegion(CloudRegionCode region, string gameVersion)
  {
    if (PhotonNetwork.networkingPeer.get_PeerState() != null)
    {
      Debug.LogWarning((object) ("ConnectToRegion() failed. Can only connect while in state 'Disconnected'. Current state: " + (object) PhotonNetwork.networkingPeer.get_PeerState()));
      return false;
    }
    if (Object.op_Equality((Object) PhotonNetwork.PhotonServerSettings, (Object) null))
    {
      Debug.LogError((object) "Can't connect: ServerSettings asset must be in any 'Resources' folder as: PhotonServerSettings");
      return false;
    }
    if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.OfflineMode)
      return PhotonNetwork.ConnectUsingSettings(gameVersion);
    PhotonNetwork.networkingPeer.IsInitialConnect = true;
    PhotonNetwork.networkingPeer.SetApp(PhotonNetwork.PhotonServerSettings.AppID, gameVersion);
    if (region == CloudRegionCode.none)
      return false;
    Debug.Log((object) ("ConnectToRegion: " + (object) region));
    return PhotonNetwork.networkingPeer.ConnectToRegionMaster(region);
  }

  public static void OverrideBestCloudServer(CloudRegionCode region)
  {
    PhotonHandler.BestRegionCodeInPreferences = region;
  }

  public static void RefreshCloudServerRating()
  {
    throw new NotImplementedException("not available at the moment");
  }

  public static void NetworkStatisticsReset()
  {
    PhotonNetwork.networkingPeer.TrafficStatsReset();
  }

  public static string NetworkStatisticsToString()
  {
    if (PhotonNetwork.networkingPeer == null || PhotonNetwork.offlineMode)
      return "Offline or in OfflineMode. No VitalStats available.";
    return PhotonNetwork.networkingPeer.VitalStatsToString(false);
  }

  [Obsolete("Used for compatibility with Unity networking only. Encryption is automatically initialized while connecting.")]
  public static void InitializeSecurity()
  {
  }

  private static bool VerifyCanUseNetwork()
  {
    if (PhotonNetwork.connected)
      return true;
    Debug.LogError((object) "Cannot send messages when not connected. Either connect to Photon OR use offline mode!");
    return false;
  }

  public static void Disconnect()
  {
    if (PhotonNetwork.offlineMode)
    {
      PhotonNetwork.offlineMode = false;
      PhotonNetwork.offlineModeRoom = (Room) null;
      PhotonNetwork.networkingPeer.State = ClientState.Disconnecting;
      PhotonNetwork.networkingPeer.OnStatusChanged((StatusCode) 1025);
    }
    else
    {
      if (PhotonNetwork.networkingPeer == null)
        return;
      PhotonNetwork.networkingPeer.Disconnect();
    }
  }

  public static bool FindFriends(string[] friendsToFind)
  {
    if (PhotonNetwork.networkingPeer == null || PhotonNetwork.isOfflineMode)
      return false;
    return PhotonNetwork.networkingPeer.OpFindFriends(friendsToFind);
  }

  public static bool CreateRoom(string roomName)
  {
    return PhotonNetwork.CreateRoom(roomName, (RoomOptions) null, (TypedLobby) null, (string[]) null);
  }

  public static bool CreateRoom(string roomName, RoomOptions roomOptions, TypedLobby typedLobby)
  {
    return PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby, (string[]) null);
  }

  public static bool CreateRoom(string roomName, RoomOptions roomOptions, TypedLobby typedLobby, string[] expectedUsers)
  {
    if (PhotonNetwork.offlineMode)
    {
      if (PhotonNetwork.offlineModeRoom != null)
      {
        Debug.LogError((object) "CreateRoom failed. In offline mode you still have to leave a room to enter another.");
        return false;
      }
      PhotonNetwork.EnterOfflineRoom(roomName, roomOptions, true);
      return true;
    }
    if (PhotonNetwork.networkingPeer.Server != ServerConnection.MasterServer || !PhotonNetwork.connectedAndReady)
    {
      Debug.LogError((object) "CreateRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
      return false;
    }
    typedLobby = typedLobby ?? (!PhotonNetwork.networkingPeer.insideLobby ? (TypedLobby) null : PhotonNetwork.networkingPeer.lobby);
    return PhotonNetwork.networkingPeer.OpCreateGame(new EnterRoomParams() { RoomName = roomName, RoomOptions = roomOptions, Lobby = typedLobby, ExpectedUsers = expectedUsers });
  }

  public static bool JoinRoom(string roomName)
  {
    return PhotonNetwork.JoinRoom(roomName, (string[]) null);
  }

  public static bool JoinRoom(string roomName, string[] expectedUsers)
  {
    if (PhotonNetwork.offlineMode)
    {
      if (PhotonNetwork.offlineModeRoom != null)
      {
        Debug.LogError((object) "JoinRoom failed. In offline mode you still have to leave a room to enter another.");
        return false;
      }
      PhotonNetwork.EnterOfflineRoom(roomName, (RoomOptions) null, true);
      return true;
    }
    if (PhotonNetwork.networkingPeer.Server != ServerConnection.MasterServer || !PhotonNetwork.connectedAndReady)
    {
      Debug.LogError((object) "JoinRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
      return false;
    }
    if (string.IsNullOrEmpty(roomName))
    {
      Debug.LogError((object) "JoinRoom failed. A roomname is required. If you don't know one, how will you join?");
      return false;
    }
    return PhotonNetwork.networkingPeer.OpJoinRoom(new EnterRoomParams() { RoomName = roomName, ExpectedUsers = expectedUsers });
  }

  public static bool JoinOrCreateRoom(string roomName, RoomOptions roomOptions, TypedLobby typedLobby)
  {
    return PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby, (string[]) null);
  }

  public static bool JoinOrCreateRoom(string roomName, RoomOptions roomOptions, TypedLobby typedLobby, string[] expectedUsers)
  {
    if (PhotonNetwork.offlineMode)
    {
      if (PhotonNetwork.offlineModeRoom != null)
      {
        Debug.LogError((object) "JoinOrCreateRoom failed. In offline mode you still have to leave a room to enter another.");
        return false;
      }
      PhotonNetwork.EnterOfflineRoom(roomName, roomOptions, true);
      return true;
    }
    if (PhotonNetwork.networkingPeer.Server != ServerConnection.MasterServer || !PhotonNetwork.connectedAndReady)
    {
      Debug.LogError((object) "JoinOrCreateRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
      return false;
    }
    if (string.IsNullOrEmpty(roomName))
    {
      Debug.LogError((object) "JoinOrCreateRoom failed. A roomname is required. If you don't know one, how will you join?");
      return false;
    }
    typedLobby = typedLobby ?? (!PhotonNetwork.networkingPeer.insideLobby ? (TypedLobby) null : PhotonNetwork.networkingPeer.lobby);
    return PhotonNetwork.networkingPeer.OpJoinRoom(new EnterRoomParams() { RoomName = roomName, RoomOptions = roomOptions, Lobby = typedLobby, CreateIfNotExists = true, PlayerProperties = PhotonNetwork.player.CustomProperties, ExpectedUsers = expectedUsers });
  }

  public static bool JoinRandomRoom()
  {
    return PhotonNetwork.JoinRandomRoom((Hashtable) null, (byte) 0, MatchmakingMode.FillRoom, (TypedLobby) null, (string) null, (string[]) null);
  }

  public static bool JoinRandomRoom(Hashtable expectedCustomRoomProperties, byte expectedMaxPlayers)
  {
    return PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, expectedMaxPlayers, MatchmakingMode.FillRoom, (TypedLobby) null, (string) null, (string[]) null);
  }

  public static bool JoinRandomRoom(Hashtable expectedCustomRoomProperties, byte expectedMaxPlayers, MatchmakingMode matchingType, TypedLobby typedLobby, string sqlLobbyFilter, string[] expectedUsers = null)
  {
    if (PhotonNetwork.offlineMode)
    {
      if (PhotonNetwork.offlineModeRoom != null)
      {
        Debug.LogError((object) "JoinRandomRoom failed. In offline mode you still have to leave a room to enter another.");
        return false;
      }
      PhotonNetwork.EnterOfflineRoom("offline room", (RoomOptions) null, true);
      return true;
    }
    if (PhotonNetwork.networkingPeer.Server != ServerConnection.MasterServer || !PhotonNetwork.connectedAndReady)
    {
      Debug.LogError((object) "JoinRandomRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
      return false;
    }
    typedLobby = typedLobby ?? (!PhotonNetwork.networkingPeer.insideLobby ? (TypedLobby) null : PhotonNetwork.networkingPeer.lobby);
    return PhotonNetwork.networkingPeer.OpJoinRandomRoom(new OpJoinRandomRoomParams() { ExpectedCustomRoomProperties = expectedCustomRoomProperties, ExpectedMaxPlayers = expectedMaxPlayers, MatchingType = matchingType, TypedLobby = typedLobby, SqlLobbyFilter = sqlLobbyFilter, ExpectedUsers = expectedUsers });
  }

  public static bool ReJoinRoom(string roomName)
  {
    if (PhotonNetwork.offlineMode)
    {
      Debug.LogError((object) "ReJoinRoom failed due to offline mode.");
      return false;
    }
    if (PhotonNetwork.networkingPeer.Server != ServerConnection.MasterServer || !PhotonNetwork.connectedAndReady)
    {
      Debug.LogError((object) "ReJoinRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
      return false;
    }
    if (string.IsNullOrEmpty(roomName))
    {
      Debug.LogError((object) "ReJoinRoom failed. A roomname is required. If you don't know one, how will you join?");
      return false;
    }
    return PhotonNetwork.networkingPeer.OpJoinRoom(new EnterRoomParams() { RoomName = roomName, RejoinOnly = true, PlayerProperties = PhotonNetwork.player.CustomProperties });
  }

  private static void EnterOfflineRoom(string roomName, RoomOptions roomOptions, bool createdRoom)
  {
    PhotonNetwork.offlineModeRoom = new Room(roomName, roomOptions);
    PhotonNetwork.networkingPeer.ChangeLocalID(1);
    PhotonNetwork.offlineModeRoom.MasterClientId = 1;
    if (createdRoom)
      NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom);
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedRoom);
  }

  public static bool JoinLobby()
  {
    return PhotonNetwork.JoinLobby((TypedLobby) null);
  }

  public static bool JoinLobby(TypedLobby typedLobby)
  {
    if (!PhotonNetwork.connected || PhotonNetwork.Server != ServerConnection.MasterServer)
      return false;
    if (typedLobby == null)
      typedLobby = TypedLobby.Default;
    bool flag = PhotonNetwork.networkingPeer.OpJoinLobby(typedLobby);
    if (flag)
      PhotonNetwork.networkingPeer.lobby = typedLobby;
    return flag;
  }

  public static bool LeaveLobby()
  {
    if (PhotonNetwork.connected && PhotonNetwork.Server == ServerConnection.MasterServer)
      return PhotonNetwork.networkingPeer.OpLeaveLobby();
    return false;
  }

  public static bool LeaveRoom()
  {
    if (PhotonNetwork.offlineMode)
    {
      PhotonNetwork.offlineModeRoom = (Room) null;
      NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnLeftRoom);
      return true;
    }
    if (PhotonNetwork.room == null)
      Debug.LogWarning((object) ("PhotonNetwork.room is null. You don't have to call LeaveRoom() when you're not in one. State: " + (object) PhotonNetwork.connectionStateDetailed));
    return PhotonNetwork.networkingPeer.OpLeave();
  }

  public static RoomInfo[] GetRoomList()
  {
    if (PhotonNetwork.offlineMode || PhotonNetwork.networkingPeer == null)
      return new RoomInfo[0];
    return PhotonNetwork.networkingPeer.mGameListCopy;
  }

  public static void SetPlayerCustomProperties(Hashtable customProperties)
  {
    if (customProperties == null)
    {
      customProperties = new Hashtable();
      using (Dictionary<object, object>.KeyCollection.Enumerator enumerator = ((Dictionary<object, object>) PhotonNetwork.player.CustomProperties).Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          object current = enumerator.Current;
          customProperties.set_Item((object) (string) current, (object) null);
        }
      }
    }
    if (PhotonNetwork.room != null && PhotonNetwork.room.IsLocalClientInside)
      PhotonNetwork.player.SetCustomProperties(customProperties, (Hashtable) null, false);
    else
      PhotonNetwork.player.InternalCacheProperties(customProperties);
  }

  public static void RemovePlayerCustomProperties(string[] customPropertiesToDelete)
  {
    if (customPropertiesToDelete == null || customPropertiesToDelete.Length == 0 || PhotonNetwork.player.CustomProperties == null)
    {
      PhotonNetwork.player.CustomProperties = new Hashtable();
    }
    else
    {
      for (int index = 0; index < customPropertiesToDelete.Length; ++index)
      {
        string str = customPropertiesToDelete[index];
        if (((Dictionary<object, object>) PhotonNetwork.player.CustomProperties).ContainsKey((object) str))
          ((Dictionary<object, object>) PhotonNetwork.player.CustomProperties).Remove((object) str);
      }
    }
  }

  public static bool RaiseEvent(byte eventCode, object eventContent, bool sendReliable, RaiseEventOptions options)
  {
    if (PhotonNetwork.inRoom && (int) eventCode < 200)
      return PhotonNetwork.networkingPeer.OpRaiseEvent(eventCode, eventContent, sendReliable, options);
    Debug.LogWarning((object) "RaiseEvent() failed. Your event is not being sent! Check if your are in a Room and the eventCode must be less than 200 (0..199).");
    return false;
  }

  public static int AllocateViewID()
  {
    int num = PhotonNetwork.AllocateViewID(PhotonNetwork.player.ID);
    PhotonNetwork.manuallyAllocatedViewIds.Add(num);
    return num;
  }

  public static int AllocateSceneViewID()
  {
    if (!PhotonNetwork.isMasterClient)
    {
      Debug.LogError((object) "Only the Master Client can AllocateSceneViewID(). Check PhotonNetwork.isMasterClient!");
      return -1;
    }
    int num = PhotonNetwork.AllocateViewID(0);
    PhotonNetwork.manuallyAllocatedViewIds.Add(num);
    return num;
  }

  private static int AllocateViewID(int ownerId)
  {
    if (ownerId == 0)
    {
      int num1 = PhotonNetwork.lastUsedViewSubIdStatic;
      int num2 = ownerId * PhotonNetwork.MAX_VIEW_IDS;
      for (int index = 1; index < PhotonNetwork.MAX_VIEW_IDS; ++index)
      {
        num1 = (num1 + 1) % PhotonNetwork.MAX_VIEW_IDS;
        if (num1 != 0)
        {
          int key = num1 + num2;
          if (!PhotonNetwork.networkingPeer.photonViewList.ContainsKey(key))
          {
            PhotonNetwork.lastUsedViewSubIdStatic = num1;
            return key;
          }
        }
      }
      throw new Exception(string.Format("AllocateViewID() failed. Room (user {0}) is out of 'scene' viewIDs. It seems all available are in use.", (object) ownerId));
    }
    int num3 = PhotonNetwork.lastUsedViewSubId;
    int num4 = ownerId * PhotonNetwork.MAX_VIEW_IDS;
    for (int index = 1; index < PhotonNetwork.MAX_VIEW_IDS; ++index)
    {
      num3 = (num3 + 1) % PhotonNetwork.MAX_VIEW_IDS;
      if (num3 != 0)
      {
        int key = num3 + num4;
        if (!PhotonNetwork.networkingPeer.photonViewList.ContainsKey(key) && !PhotonNetwork.manuallyAllocatedViewIds.Contains(key))
        {
          PhotonNetwork.lastUsedViewSubId = num3;
          return key;
        }
      }
    }
    throw new Exception(string.Format("AllocateViewID() failed. User {0} is out of subIds, as all viewIDs are used.", (object) ownerId));
  }

  private static int[] AllocateSceneViewIDs(int countOfNewViews)
  {
    int[] numArray = new int[countOfNewViews];
    for (int index = 0; index < countOfNewViews; ++index)
      numArray[index] = PhotonNetwork.AllocateViewID(0);
    return numArray;
  }

  public static void UnAllocateViewID(int viewID)
  {
    PhotonNetwork.manuallyAllocatedViewIds.Remove(viewID);
    if (!PhotonNetwork.networkingPeer.photonViewList.ContainsKey(viewID))
      return;
    Debug.LogWarning((object) string.Format("UnAllocateViewID() should be called after the PhotonView was destroyed (GameObject.Destroy()). ViewID: {0} still found in: {1}", (object) viewID, (object) PhotonNetwork.networkingPeer.photonViewList[viewID]));
  }

  public static GameObject Instantiate(string prefabName, Vector3 position, Quaternion rotation, int group)
  {
    return PhotonNetwork.Instantiate(prefabName, position, rotation, group, (object[]) null);
  }

  public static GameObject Instantiate(string prefabName, Vector3 position, Quaternion rotation, int group, object[] data)
  {
    if (!PhotonNetwork.connected || PhotonNetwork.InstantiateInRoomOnly && !PhotonNetwork.inRoom)
    {
      Debug.LogError((object) ("Failed to Instantiate prefab: " + prefabName + ". Client should be in a room. Current connectionStateDetailed: " + (object) PhotonNetwork.connectionStateDetailed));
      return (GameObject) null;
    }
    GameObject gameObject;
    if (!PhotonNetwork.UsePrefabCache || !PhotonNetwork.PrefabCache.TryGetValue(prefabName, out gameObject))
    {
      gameObject = (GameObject) Resources.Load(prefabName, typeof (GameObject));
      if (PhotonNetwork.UsePrefabCache)
        PhotonNetwork.PrefabCache.Add(prefabName, gameObject);
    }
    if (Object.op_Equality((Object) gameObject, (Object) null))
    {
      Debug.LogError((object) ("Failed to Instantiate prefab: " + prefabName + ". Verify the Prefab is in a Resources folder (and not in a subfolder)"));
      return (GameObject) null;
    }
    if (Object.op_Equality((Object) gameObject.GetComponent<PhotonView>(), (Object) null))
    {
      Debug.LogError((object) ("Failed to Instantiate prefab:" + prefabName + ". Prefab must have a PhotonView component."));
      return (GameObject) null;
    }
    int[] viewIDs = new int[((Component[]) gameObject.GetPhotonViewsInChildren()).Length];
    for (int index = 0; index < viewIDs.Length; ++index)
      viewIDs[index] = PhotonNetwork.AllocateViewID(PhotonNetwork.player.ID);
    Hashtable evData = PhotonNetwork.networkingPeer.SendInstantiate(prefabName, position, rotation, group, viewIDs, data, false);
    return PhotonNetwork.networkingPeer.DoInstantiate(evData, PhotonNetwork.networkingPeer.LocalPlayer, gameObject);
  }

  public static GameObject InstantiateSceneObject(string prefabName, Vector3 position, Quaternion rotation, int group, object[] data)
  {
    if (!PhotonNetwork.connected || PhotonNetwork.InstantiateInRoomOnly && !PhotonNetwork.inRoom)
    {
      Debug.LogError((object) ("Failed to InstantiateSceneObject prefab: " + prefabName + ". Client should be in a room. Current connectionStateDetailed: " + (object) PhotonNetwork.connectionStateDetailed));
      return (GameObject) null;
    }
    if (!PhotonNetwork.isMasterClient)
    {
      Debug.LogError((object) ("Failed to InstantiateSceneObject prefab: " + prefabName + ". Client is not the MasterClient in this room."));
      return (GameObject) null;
    }
    GameObject gameObject;
    if (!PhotonNetwork.UsePrefabCache || !PhotonNetwork.PrefabCache.TryGetValue(prefabName, out gameObject))
    {
      gameObject = (GameObject) Resources.Load(prefabName, typeof (GameObject));
      if (PhotonNetwork.UsePrefabCache)
        PhotonNetwork.PrefabCache.Add(prefabName, gameObject);
    }
    if (Object.op_Equality((Object) gameObject, (Object) null))
    {
      Debug.LogError((object) ("Failed to InstantiateSceneObject prefab: " + prefabName + ". Verify the Prefab is in a Resources folder (and not in a subfolder)"));
      return (GameObject) null;
    }
    if (Object.op_Equality((Object) gameObject.GetComponent<PhotonView>(), (Object) null))
    {
      Debug.LogError((object) ("Failed to InstantiateSceneObject prefab:" + prefabName + ". Prefab must have a PhotonView component."));
      return (GameObject) null;
    }
    int[] viewIDs = PhotonNetwork.AllocateSceneViewIDs(((Component[]) gameObject.GetPhotonViewsInChildren()).Length);
    if (viewIDs == null)
    {
      Debug.LogError((object) ("Failed to InstantiateSceneObject prefab: " + prefabName + ". No ViewIDs are free to use. Max is: " + (object) PhotonNetwork.MAX_VIEW_IDS));
      return (GameObject) null;
    }
    Hashtable evData = PhotonNetwork.networkingPeer.SendInstantiate(prefabName, position, rotation, group, viewIDs, data, true);
    return PhotonNetwork.networkingPeer.DoInstantiate(evData, PhotonNetwork.networkingPeer.LocalPlayer, gameObject);
  }

  public static int GetPing()
  {
    return PhotonNetwork.networkingPeer.get_RoundTripTime();
  }

  public static void FetchServerTimestamp()
  {
    if (PhotonNetwork.networkingPeer == null)
      return;
    PhotonNetwork.networkingPeer.FetchServerTimestamp();
  }

  public static void SendOutgoingCommands()
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    do
      ;
    while (PhotonNetwork.networkingPeer.SendOutgoingCommands());
  }

  public static bool CloseConnection(PhotonPlayer kickPlayer)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return false;
    if (!PhotonNetwork.player.IsMasterClient)
    {
      Debug.LogError((object) "CloseConnection: Only the masterclient can kick another player.");
      return false;
    }
    if (kickPlayer == null)
    {
      Debug.LogError((object) "CloseConnection: No such player connected!");
      return false;
    }
    RaiseEventOptions raiseEventOptions = new RaiseEventOptions() { TargetActors = new int[1]{ kickPlayer.ID } };
    return PhotonNetwork.networkingPeer.OpRaiseEvent((byte) 203, (object) null, true, raiseEventOptions);
  }

  public static bool SetMasterClient(PhotonPlayer masterClientPlayer)
  {
    if (!PhotonNetwork.inRoom || !PhotonNetwork.VerifyCanUseNetwork() || PhotonNetwork.offlineMode)
    {
      if (PhotonNetwork.logLevel == PhotonLogLevel.Informational)
        Debug.Log((object) "Can not SetMasterClient(). Not in room or in offlineMode.");
      return false;
    }
    if (PhotonNetwork.room.serverSideMasterClient)
    {
      Hashtable hashtable1 = new Hashtable();
      ((Dictionary<object, object>) hashtable1).Add((object) (byte) 248, (object) masterClientPlayer.ID);
      Hashtable gameProperties = hashtable1;
      Hashtable hashtable2 = new Hashtable();
      ((Dictionary<object, object>) hashtable2).Add((object) (byte) 248, (object) PhotonNetwork.networkingPeer.mMasterClientId);
      Hashtable expectedProperties = hashtable2;
      return PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(gameProperties, expectedProperties, false);
    }
    if (!PhotonNetwork.isMasterClient)
      return false;
    return PhotonNetwork.networkingPeer.SetMasterClient(masterClientPlayer.ID, true);
  }

  public static void Destroy(PhotonView targetView)
  {
    if (Object.op_Inequality((Object) targetView, (Object) null))
      PhotonNetwork.networkingPeer.RemoveInstantiatedGO(((Component) targetView).get_gameObject(), !PhotonNetwork.inRoom);
    else
      Debug.LogError((object) "Destroy(targetPhotonView) failed, cause targetPhotonView is null.");
  }

  public static void Destroy(GameObject targetGo)
  {
    PhotonNetwork.networkingPeer.RemoveInstantiatedGO(targetGo, !PhotonNetwork.inRoom);
  }

  public static void DestroyPlayerObjects(PhotonPlayer targetPlayer)
  {
    if (PhotonNetwork.player == null)
      Debug.LogError((object) "DestroyPlayerObjects() failed, cause parameter 'targetPlayer' was null.");
    PhotonNetwork.DestroyPlayerObjects(targetPlayer.ID);
  }

  public static void DestroyPlayerObjects(int targetPlayerId)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    if (PhotonNetwork.player.IsMasterClient || targetPlayerId == PhotonNetwork.player.ID)
      PhotonNetwork.networkingPeer.DestroyPlayerObjects(targetPlayerId, false);
    else
      Debug.LogError((object) ("DestroyPlayerObjects() failed, cause players can only destroy their own GameObjects. A Master Client can destroy anyone's. This is master: " + (object) PhotonNetwork.isMasterClient));
  }

  public static void DestroyAll()
  {
    if (PhotonNetwork.isMasterClient)
      PhotonNetwork.networkingPeer.DestroyAll(false);
    else
      Debug.LogError((object) "Couldn't call DestroyAll() as only the master client is allowed to call this.");
  }

  public static void RemoveRPCs(PhotonPlayer targetPlayer)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    if (!targetPlayer.IsLocal && !PhotonNetwork.isMasterClient)
      Debug.LogError((object) "Error; Only the MasterClient can call RemoveRPCs for other players.");
    else
      PhotonNetwork.networkingPeer.OpCleanRpcBuffer(targetPlayer.ID);
  }

  public static void RemoveRPCs(PhotonView targetPhotonView)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    PhotonNetwork.networkingPeer.CleanRpcBufferIfMine(targetPhotonView);
  }

  public static void RemoveRPCsInGroup(int targetGroup)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    PhotonNetwork.networkingPeer.RemoveRPCsInGroup(targetGroup);
  }

  internal static void RPC(PhotonView view, string methodName, PhotonTargets target, bool encrypt, params object[] parameters)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    if (PhotonNetwork.room == null)
      Debug.LogWarning((object) ("RPCs can only be sent in rooms. Call of \"" + methodName + "\" gets executed locally only, if at all."));
    else if (PhotonNetwork.networkingPeer != null)
    {
      if (PhotonNetwork.room.serverSideMasterClient)
        PhotonNetwork.networkingPeer.RPC(view, methodName, target, (PhotonPlayer) null, encrypt, parameters);
      else if (PhotonNetwork.networkingPeer.hasSwitchedMC && target == PhotonTargets.MasterClient)
        PhotonNetwork.networkingPeer.RPC(view, methodName, PhotonTargets.Others, PhotonNetwork.masterClient, encrypt, parameters);
      else
        PhotonNetwork.networkingPeer.RPC(view, methodName, target, (PhotonPlayer) null, encrypt, parameters);
    }
    else
      Debug.LogWarning((object) ("Could not execute RPC " + methodName + ". Possible scene loading in progress?"));
  }

  internal static void RPC(PhotonView view, string methodName, PhotonPlayer targetPlayer, bool encrpyt, params object[] parameters)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    if (PhotonNetwork.room == null)
    {
      Debug.LogWarning((object) ("RPCs can only be sent in rooms. Call of \"" + methodName + "\" gets executed locally only, if at all."));
    }
    else
    {
      if (PhotonNetwork.player == null)
        Debug.LogError((object) ("RPC can't be sent to target PhotonPlayer being null! Did not send \"" + methodName + "\" call."));
      if (PhotonNetwork.networkingPeer != null)
        PhotonNetwork.networkingPeer.RPC(view, methodName, PhotonTargets.Others, targetPlayer, encrpyt, parameters);
      else
        Debug.LogWarning((object) ("Could not execute RPC " + methodName + ". Possible scene loading in progress?"));
    }
  }

  public static void CacheSendMonoMessageTargets(System.Type type)
  {
    if ((object) type == null)
      type = PhotonNetwork.SendMonoMessageTargetType;
    PhotonNetwork.SendMonoMessageTargets = PhotonNetwork.FindGameObjectsWithComponent(type);
  }

  public static HashSet<GameObject> FindGameObjectsWithComponent(System.Type type)
  {
    HashSet<GameObject> gameObjectSet = new HashSet<GameObject>();
    Component[] objectsOfType = (Component[]) Object.FindObjectsOfType(type);
    for (int index = 0; index < objectsOfType.Length; ++index)
    {
      if (Object.op_Inequality((Object) objectsOfType[index], (Object) null))
        gameObjectSet.Add(objectsOfType[index].get_gameObject());
    }
    return gameObjectSet;
  }

  public static void SetReceivingEnabled(int group, bool enabled)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    PhotonNetwork.networkingPeer.SetReceivingEnabled(group, enabled);
  }

  public static void SetReceivingEnabled(int[] enableGroups, int[] disableGroups)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    PhotonNetwork.networkingPeer.SetReceivingEnabled(enableGroups, disableGroups);
  }

  public static void SetSendingEnabled(int group, bool enabled)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    PhotonNetwork.networkingPeer.SetSendingEnabled(group, enabled);
  }

  public static void SetSendingEnabled(int[] enableGroups, int[] disableGroups)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    PhotonNetwork.networkingPeer.SetSendingEnabled(enableGroups, disableGroups);
  }

  public static void SetLevelPrefix(short prefix)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    PhotonNetwork.networkingPeer.SetLevelPrefix(prefix);
  }

  public static void LoadLevel(int levelNumber)
  {
    PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced((object) levelNumber);
    PhotonNetwork.isMessageQueueRunning = false;
    PhotonNetwork.networkingPeer.loadingLevelAndPausedNetwork = true;
    SceneManager.LoadScene(levelNumber);
  }

  public static void LoadLevel(string levelName)
  {
    PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced((object) levelName);
    PhotonNetwork.isMessageQueueRunning = false;
    PhotonNetwork.networkingPeer.loadingLevelAndPausedNetwork = true;
    SceneManager.LoadScene(levelName);
  }

  public static bool WebRpc(string name, object parameters)
  {
    return PhotonNetwork.networkingPeer.WebRpc(name, parameters);
  }

  public delegate void EventCallback(byte eventCode, object content, int senderId);
}
