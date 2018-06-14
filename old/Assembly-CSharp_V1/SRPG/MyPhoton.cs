// Decompiled with JetBrains decompiler
// Type: SRPG.MyPhoton
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class MyPhoton : PunMonoSingleton<MyPhoton>
  {
    public static readonly int MAX_PLAYER_NUM = 10;
    public static readonly int TIMEOUT_SECOND = 30;
    public static readonly int SEND_RATE = 30;
    private float mDelaySec = -1f;
    private List<MyPhoton.MyEvent> mEvents = new List<MyPhoton.MyEvent>();
    private List<JSON_MyPhotonPlayerParam> mPlayersStarted = new List<JSON_MyPhotonPlayerParam>();
    private const string STARTED_ROOM = "start";
    private MyPhoton.MyState mState;
    private bool mIsRoomListUpdated;
    private bool mIsUpdateRoomProperty;
    private MyPhoton.MyError mError;
    private NetworkReachability mNetworkReach;
    private int mSendRoomMessageID;

    public MyPhoton.MyState CurrentState
    {
      get
      {
        return this.mState;
      }
    }

    public bool IsRoomListUpdated
    {
      get
      {
        return this.mIsRoomListUpdated;
      }
      set
      {
        this.mIsRoomListUpdated = value;
      }
    }

    public bool IsUpdateRoomProperty
    {
      get
      {
        return this.mIsUpdateRoomProperty;
      }
      set
      {
        this.mIsUpdateRoomProperty = value;
      }
    }

    public MyPhoton.MyError LastError
    {
      get
      {
        return this.mError;
      }
    }

    public void ResetLastError()
    {
      this.mError = MyPhoton.MyError.NOP;
    }

    private void Log(string str)
    {
      if (!GameUtility.IsDebugBuild)
        return;
      DebugUtility.Log(str);
    }

    private void LogWarning(string str)
    {
      if (!GameUtility.IsDebugBuild)
        return;
      DebugUtility.LogWarning(str);
    }

    private void LogError(string str)
    {
      if (!GameUtility.IsDebugBuild)
        return;
      DebugUtility.LogError(str);
    }

    public float TimeOutSec { get; set; }

    public bool SendRoomMessageFlush { get; set; }

    public bool DisconnectIfSendRoomMessageFailed { get; set; }

    public bool SortRoomMessage { get; set; }

    public PeerStateValue ConnectState
    {
      get
      {
        return PhotonNetwork.networkingPeer.get_PeerState();
      }
    }

    public bool IsDisconnected()
    {
      return this.ConnectState == null;
    }

    protected override void Initialize()
    {
      Object.DontDestroyOnLoad((Object) this);
      if (GameUtility.IsDebugBuild)
        ((PhotonLagSimulationGui) ((Component) this).get_gameObject().AddComponent<PhotonLagSimulationGui>()).Visible = false;
      PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;
      PhotonNetwork.OnEventCall += new PhotonNetwork.EventCallback(this.OnEventHandler);
      PhotonNetwork.CrcCheckEnabled = true;
      PhotonNetwork.QuickResends = 3;
      PhotonNetwork.MaxResendsBeforeDisconnect = 7;
      PhotonNetwork.sendRate = MyPhoton.SEND_RATE;
      this.UseEncrypt = true;
      this.TimeOutSec = (float) MyPhoton.TIMEOUT_SECOND;
    }

    protected override void Release()
    {
    }

    public string GetTrafficState()
    {
      return "lastrecv:" + (object) (SupportClass.GetTickCount() - PhotonNetwork.networkingPeer.get_TimestampOfLastSocketReceive()) + " og:" + PhotonNetwork.networkingPeer.get_TrafficStatsOutgoing().ToString() + " ic:" + PhotonNetwork.networkingPeer.get_TrafficStatsIncoming().ToString();
    }

    private void Update()
    {
      if (this.mState == MyPhoton.MyState.NOP)
        return;
      NetworkReachability internetReachability = Application.get_internetReachability();
      if (this.mState != MyPhoton.MyState.NOP && internetReachability != this.mNetworkReach && this.mNetworkReach != null)
        this.LogWarning("internet reach change to " + (object) internetReachability + "\n" + this.GetTrafficState());
      this.mNetworkReach = internetReachability;
      if (this.mState != MyPhoton.MyState.ROOM)
      {
        this.mDelaySec = -1f;
        this.mSendRoomMessageID = 0;
      }
      else if ((double) (SupportClass.GetTickCount() - PhotonNetwork.networkingPeer.get_TimestampOfLastSocketReceive()) < (double) this.TimeOutSec * 1000.0)
      {
        this.mDelaySec = -1f;
      }
      else
      {
        if ((double) this.mDelaySec < 0.0)
        {
          this.mDelaySec = 0.0f;
          this.LogWarning(PhotonNetwork.NetworkStatisticsToString() + "\n" + this.GetTrafficState());
        }
        this.mDelaySec += Time.get_deltaTime();
        if ((double) this.mDelaySec < (double) this.TimeOutSec)
          return;
        this.LogWarning("maybe connection lost.");
        this.LogWarning(PhotonNetwork.NetworkStatisticsToString() + "\n" + this.GetTrafficState());
        this.Disconnect();
        this.mError = MyPhoton.MyError.TIMEOUT2;
      }
    }

    public string CurrentAppID { get; private set; }

    public bool StartConnect(string appID, bool autoJoin = false, string ver = "1.0")
    {
      if (this.mState != MyPhoton.MyState.NOP)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      this.CurrentAppID = appID;
      PhotonNetwork.autoJoinLobby = autoJoin;
      PhotonNetwork.PhotonServerSettings.AppID = appID;
      PhotonNetwork.PhotonServerSettings.Protocol = (ConnectionProtocol) 1;
      bool flag = PhotonNetwork.ConnectUsingSettings(ver);
      if (flag)
      {
        this.mState = MyPhoton.MyState.CONNECTING;
        PhotonNetwork.NetworkStatisticsEnabled = true;
      }
      else
      {
        this.mState = MyPhoton.MyState.NOP;
        this.mError = MyPhoton.MyError.UNKNOWN;
      }
      this.Log("StartConnect:" + (object) flag);
      return flag;
    }

    public void Disconnect()
    {
      this.Log("call Disconnect().");
      this.mEvents.Clear();
      if (this.CurrentState == MyPhoton.MyState.NOP)
        return;
      PhotonNetwork.Disconnect();
    }

    public override void OnWebRpcResponse(OperationResponse response)
    {
      this.Log("WebRpc:" + response.ToStringFull());
      if (response.ReturnCode == null)
        return;
      WebRpcResponse webRpcResponse = new WebRpcResponse(response);
      if (webRpcResponse.ReturnCode != 0)
        this.Log("WebRPC '" + webRpcResponse.Name + "' に失敗しました. Error: " + (object) webRpcResponse.ReturnCode + " Message: " + webRpcResponse.DebugMessage);
      using (Dictionary<string, object>.Enumerator enumerator = webRpcResponse.Parameters.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<string, object> current = enumerator.Current;
          this.Log("Key:" + current.Key + "/ Value:" + current.Value);
        }
      }
    }

    public override void OnConnectedToPhoton()
    {
      this.Log("Connected to Photon Server");
    }

    public override void OnDisconnectedFromPhoton()
    {
      this.Log("DisconnectedFromPhoton. LostPacket:" + (object) PhotonNetwork.PacketLossByCrcCheck + " MaxResendsBeforeDisconnect:" + (object) PhotonNetwork.MaxResendsBeforeDisconnect + " ResentReliableCommands" + (object) PhotonNetwork.ResentReliableCommands);
      this.mState = MyPhoton.MyState.NOP;
    }

    public override void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
      this.Log("FailedToConnectToPhoton." + cause.ToString());
      if (cause == DisconnectCause.DisconnectByClientTimeout || cause == DisconnectCause.DisconnectByServerTimeout)
        this.mError = MyPhoton.MyError.TIMEOUT;
      if (cause == DisconnectCause.DisconnectByServerUserLimit)
        this.mError = MyPhoton.MyError.FULL_CLIENTS;
      this.mState = MyPhoton.MyState.NOP;
    }

    public override void OnConnectionFail(DisconnectCause cause)
    {
      this.Log("ConnectionFail." + cause.ToString());
      if (cause == DisconnectCause.DisconnectByClientTimeout || cause == DisconnectCause.DisconnectByServerTimeout)
        this.mError = MyPhoton.MyError.TIMEOUT;
      if (cause == DisconnectCause.DisconnectByServerUserLimit)
        this.mError = MyPhoton.MyError.FULL_CLIENTS;
      this.mState = MyPhoton.MyState.NOP;
    }

    public override void OnConnectedToMaster()
    {
      this.Log("Joined Default Lobby.");
      this.mState = MyPhoton.MyState.LOBBY;
      this.mEvents.Clear();
      this.IsRoomListUpdated = false;
    }

    public override void OnJoinedLobby()
    {
      this.Log("Joined Lobby.");
      this.mState = MyPhoton.MyState.LOBBY;
      this.mEvents.Clear();
      this.IsRoomListUpdated = false;
    }

    public override void OnReceivedRoomListUpdate()
    {
      this.Log("Room List Updated.");
      this.mIsRoomListUpdated = true;
    }

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
      this.Log("Create Room failed.");
      if (codeAndMsg == null || codeAndMsg.Length < 2 || !(codeAndMsg[0] is IConvertible))
      {
        this.mError = MyPhoton.MyError.UNKNOWN;
        this.Log("codeAndMsg is null");
      }
      else
      {
        this.mError = ((IConvertible) codeAndMsg[0]).ToInt32((IFormatProvider) null) != 32766 ? MyPhoton.MyError.UNKNOWN : MyPhoton.MyError.ROOM_NAME_DUPLICATED;
        string str = (string) codeAndMsg[1];
        if (str != null)
          this.Log("err:" + str);
      }
      this.mState = MyPhoton.MyState.LOBBY;
    }

    public override void OnJoinedRoom()
    {
      this.Log("Joined Room.");
      this.mEvents.Clear();
      this.mState = MyPhoton.MyState.ROOM;
      this.mSendRoomMessageID = 0;
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
      this.Log("Join Room failed.");
      if (codeAndMsg == null || codeAndMsg.Length < 2 || !(codeAndMsg[0] is IConvertible))
      {
        this.mError = MyPhoton.MyError.UNKNOWN;
      }
      else
      {
        switch (((IConvertible) codeAndMsg[0]).ToInt32((IFormatProvider) null))
        {
          case 32758:
            this.mError = MyPhoton.MyError.ROOM_IS_NOT_EXIST;
            break;
          case 32764:
            this.mError = MyPhoton.MyError.ROOM_IS_NOT_OPEN;
            break;
          case 32765:
            this.mError = MyPhoton.MyError.ROOM_IS_FULL;
            break;
          default:
            this.mError = MyPhoton.MyError.UNKNOWN;
            break;
        }
        string str = (string) codeAndMsg[1];
        if (str != null)
          this.Log("err:" + str);
      }
      this.mState = MyPhoton.MyState.LOBBY;
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
      this.Log("Join Room failed.");
      if (codeAndMsg == null || codeAndMsg.Length < 2 || !(codeAndMsg[0] is IConvertible))
      {
        this.mError = MyPhoton.MyError.UNKNOWN;
      }
      else
      {
        this.mError = ((IConvertible) codeAndMsg[0]).ToInt32((IFormatProvider) null) != 32760 ? MyPhoton.MyError.UNKNOWN : MyPhoton.MyError.ROOM_IS_NOT_EXIST;
        string str = (string) codeAndMsg[1];
        if (str != null)
          this.Log("err:" + str);
      }
      this.mState = MyPhoton.MyState.LOBBY;
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
      this.Log("Join other player to your room. playerID:" + (object) newPlayer.ID);
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
      this.Log("Leave other player from your room. playerID:" + (object) otherPlayer.ID);
    }

    public override void OnLeftRoom()
    {
      this.Log("Left Room.");
      this.mState = MyPhoton.MyState.LOBBY;
      this.mEvents.Clear();
    }

    public override void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
    {
      this.Log("Update Room Property");
      this.mIsUpdateRoomProperty = true;
    }

    private int GetCryptKey()
    {
      MyPhoton.MyRoom currentRoom = this.GetCurrentRoom();
      if (currentRoom == null || string.IsNullOrEmpty(currentRoom.name))
        return 123;
      int num = 0;
      foreach (char ch in currentRoom.name)
        num += (int) ch;
      return num;
    }

    public List<MyPhoton.MyEvent> GetEvents()
    {
      return this.mEvents;
    }

    private void OnEventHandler(byte eventCode, object content, int senderID)
    {
      Hashtable hashtable = (Hashtable) content;
      int num = (int) hashtable.get_Item((object) "s");
      string str;
      if (num == 0)
      {
        str = (string) hashtable.get_Item((object) "m");
      }
      else
      {
        byte[] data = (byte[]) hashtable.get_Item((object) "m");
        str = MyEncrypt.Decrypt(num + this.GetCryptKey(), data, true);
      }
      MyPhoton.MyEvent myEvent = new MyPhoton.MyEvent();
      myEvent.code = (MyPhoton.SEND_TYPE) eventCode;
      myEvent.playerID = senderID;
      myEvent.json = str;
      if (((Dictionary<object, object>) hashtable).ContainsKey((object) "sq"))
        myEvent.sendID = (int) hashtable.get_Item((object) "sq");
      this.mEvents.Add(myEvent);
      if (this.SortRoomMessage)
        this.mEvents.Sort((Comparison<MyPhoton.MyEvent>) ((a, b) => a.sendID - b.sendID));
      this.Log("OnEventHandler: " + (object) senderID + (string) hashtable.get_Item((object) "msg"));
    }

    public List<MyPhoton.MyRoom> GetRoomList()
    {
      List<MyPhoton.MyRoom> myRoomList = new List<MyPhoton.MyRoom>();
      foreach (RoomInfo room in PhotonNetwork.GetRoomList())
      {
        MyPhoton.MyRoom myRoom = new MyPhoton.MyRoom();
        myRoom.name = room.Name;
        myRoom.playerCount = room.PlayerCount;
        myRoom.maxPlayers = (int) room.MaxPlayers;
        myRoom.open = room.IsOpen;
        Hashtable customProperties = room.CustomProperties;
        if (customProperties != null && ((Dictionary<object, object>) customProperties).Count > 0)
          myRoom.json = (string) customProperties.get_Item((object) "json");
        myRoomList.Add(myRoom);
      }
      return myRoomList;
    }

    public MyPhoton.MyPlayer GetMyPlayer()
    {
      Hashtable customProperties = PhotonNetwork.player.CustomProperties;
      MyPhoton.MyPlayer myPlayer = new MyPhoton.MyPlayer();
      myPlayer.photonPlayerID = PhotonNetwork.player.ID;
      if (customProperties != null && ((Dictionary<object, object>) customProperties).Count > 0)
      {
        myPlayer.json = (string) customProperties.get_Item((object) "json");
        if (((Dictionary<object, object>) customProperties).ContainsKey((object) "resumeID"))
          myPlayer.resumeID = (int) customProperties.get_Item((object) "resumeID");
      }
      return myPlayer;
    }

    public void SetMyPlayerParam(string json)
    {
      Hashtable propertiesToSet = new Hashtable();
      ((Dictionary<object, object>) propertiesToSet).Add((object) nameof (json), (object) json);
      PhotonNetwork.player.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
    }

    public void SetResumeMyPlayer(int playerID = 0)
    {
      if (this.GetMyPlayer() == null)
        return;
      string empty = string.Empty;
      Hashtable customProperties = PhotonNetwork.player.CustomProperties;
      if (customProperties != null && ((Dictionary<object, object>) customProperties).ContainsKey((object) "json"))
        empty = (string) customProperties.get_Item((object) "json");
      Hashtable propertiesToSet = new Hashtable();
      ((Dictionary<object, object>) propertiesToSet).Add((object) "json", (object) empty);
      ((Dictionary<object, object>) propertiesToSet).Add((object) "resume", (object) true);
      ((Dictionary<object, object>) propertiesToSet).Add((object) "resumeID", (object) playerID);
      PhotonNetwork.player.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
    }

    public void AddMyPlayerParam(string key, object param)
    {
      if (this.GetMyPlayer() == null)
        return;
      Hashtable customProperties = PhotonNetwork.player.CustomProperties;
      if (!((Dictionary<object, object>) customProperties).ContainsKey((object) key))
        ((Dictionary<object, object>) customProperties).Add((object) key, param);
      else
        customProperties.set_Item((object) key, param);
      PhotonNetwork.player.SetCustomProperties(customProperties, (Hashtable) null, false);
    }

    public bool IsResume()
    {
      if (this.GetMyPlayer() != null)
      {
        Hashtable customProperties = PhotonNetwork.player.CustomProperties;
        if (customProperties != null && ((Dictionary<object, object>) customProperties).ContainsKey((object) "resume"))
          return (bool) customProperties.get_Item((object) "resume");
      }
      return false;
    }

    public bool CreateRoom(int maxPlayerNum, string roomName, string roomJson, string playerJson, string VersusKey = null, int floor = -1, int plv = -1)
    {
      if (this.mState != MyPhoton.MyState.LOBBY)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      PhotonNetwork.player.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
      PhotonNetwork.SetPlayerCustomProperties((Hashtable) null);
      this.SetMyPlayerParam(playerJson);
      RoomOptions roomOptions1 = new RoomOptions();
      roomOptions1.MaxPlayers = (byte) maxPlayerNum;
      roomOptions1.IsVisible = true;
      roomOptions1.IsOpen = true;
      RoomOptions roomOptions2 = roomOptions1;
      Hashtable hashtable1 = new Hashtable();
      ((Dictionary<object, object>) hashtable1).Add((object) "json", (object) roomJson);
      ((Dictionary<object, object>) hashtable1).Add((object) "name", (object) roomName);
      ((Dictionary<object, object>) hashtable1).Add((object) "start", (object) false);
      Hashtable hashtable2 = hashtable1;
      roomOptions2.CustomRoomProperties = hashtable2;
      roomOptions1.CustomRoomPropertiesForLobby = new string[3]
      {
        "json",
        "name",
        "start"
      };
      if (!string.IsNullOrEmpty(VersusKey))
      {
        int length = roomOptions1.CustomRoomPropertiesForLobby.Length;
        int num1 = roomOptions1.CustomRoomPropertiesForLobby.Length + 2;
        int num2;
        int num3 = plv == -1 ? num1 : (num2 = num1 + 1);
        int newSize = floor == -1 ? num3 : (num2 = num3 + 1);
        Array.Resize<string>(ref roomOptions1.CustomRoomPropertiesForLobby, newSize);
        ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) "MatchType", (object) VersusKey);
        string[] propertiesForLobby1 = roomOptions1.CustomRoomPropertiesForLobby;
        int index1 = length;
        int num4 = index1 + 1;
        string str1 = "MatchType";
        propertiesForLobby1[index1] = str1;
        ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) "lobby", (object) "vs");
        string[] propertiesForLobby2 = roomOptions1.CustomRoomPropertiesForLobby;
        int index2 = num4;
        int num5 = index2 + 1;
        string str2 = "lobby";
        propertiesForLobby2[index2] = str2;
        if (plv != -1)
        {
          ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) nameof (plv), (object) plv);
          roomOptions1.CustomRoomPropertiesForLobby[num5++] = nameof (plv);
        }
        if (floor != -1)
        {
          ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) nameof (floor), (object) floor);
          string[] propertiesForLobby3 = roomOptions1.CustomRoomPropertiesForLobby;
          int index3 = num5;
          int num6 = index3 + 1;
          string str3 = nameof (floor);
          propertiesForLobby3[index3] = str3;
        }
      }
      else
      {
        ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) "lobby", (object) "coop");
        Array.Resize<string>(ref roomOptions1.CustomRoomPropertiesForLobby, roomOptions1.CustomRoomPropertiesForLobby.Length + 1);
        roomOptions1.CustomRoomPropertiesForLobby[roomOptions1.CustomRoomPropertiesForLobby.Length - 1] = "lobby";
      }
      bool room = PhotonNetwork.CreateRoom(roomName, roomOptions1, (TypedLobby) null);
      if (room)
        this.mState = MyPhoton.MyState.JOINING;
      else
        this.mError = MyPhoton.MyError.UNKNOWN;
      return room;
    }

    public bool JoinRoom(string roomName, string playerJson, bool isResume = false)
    {
      if (this.mState != MyPhoton.MyState.LOBBY)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      PhotonNetwork.player.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
      PhotonNetwork.SetPlayerCustomProperties((Hashtable) null);
      this.SetMyPlayerParam(playerJson);
      bool flag;
      if (isResume)
      {
        flag = PhotonNetwork.JoinRoom(roomName);
      }
      else
      {
        Hashtable expectedCustomRoomProperties = new Hashtable();
        ((Dictionary<object, object>) expectedCustomRoomProperties).Add((object) "name", (object) roomName);
        ((Dictionary<object, object>) expectedCustomRoomProperties).Add((object) "start", (object) false);
        flag = PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, (byte) 0);
      }
      if (flag)
        this.mState = MyPhoton.MyState.JOINING;
      else
        this.mError = MyPhoton.MyError.UNKNOWN;
      return flag;
    }

    public bool JoinRandomRoom(byte maxplayer, string playerJson, string VersusHash, string roomName = null)
    {
      PhotonNetwork.player.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
      PhotonNetwork.SetPlayerCustomProperties((Hashtable) null);
      this.SetMyPlayerParam(playerJson);
      Hashtable hashtable = new Hashtable();
      ((Dictionary<object, object>) hashtable).Add((object) "MatchType", (object) VersusHash);
      Hashtable expectedCustomRoomProperties = hashtable;
      if (!string.IsNullOrEmpty(roomName))
        ((Dictionary<object, object>) expectedCustomRoomProperties).Add((object) "name", (object) roomName);
      bool flag = PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, maxplayer);
      if (flag)
        this.mState = MyPhoton.MyState.JOINING;
      else
        this.mError = MyPhoton.MyError.UNKNOWN;
      return flag;
    }

    public bool JoinRoomCheckParam(string VersusHash, string playerJson, int lvRange, int floorRange, int lv, int floor)
    {
      if (this.mState != MyPhoton.MyState.LOBBY)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      string roomName = string.Empty;
      bool flag1 = false;
      PhotonNetwork.player.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
      PhotonNetwork.SetPlayerCustomProperties((Hashtable) null);
      this.SetMyPlayerParam(playerJson);
      RoomInfo[] roomList = PhotonNetwork.GetRoomList();
      List<RoomInfo> roomInfoList = new List<RoomInfo>();
      foreach (RoomInfo roomInfo in roomList)
      {
        Hashtable customProperties = roomInfo.CustomProperties;
        if (((Dictionary<object, object>) customProperties).ContainsKey((object) "MatchType") && VersusHash == (string) customProperties.get_Item((object) "MatchType"))
          roomInfoList.Add(roomInfo);
      }
      if (lvRange != -1)
      {
        int num1 = lv - lvRange;
        int num2 = lv + lvRange;
        using (List<RoomInfo>.Enumerator enumerator = roomInfoList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            RoomInfo current = enumerator.Current;
            Hashtable customProperties = current.CustomProperties;
            if (((Dictionary<object, object>) customProperties).ContainsKey((object) "plv") && ((Dictionary<object, object>) customProperties).ContainsKey((object) nameof (floor)))
            {
              int num3 = (int) customProperties.get_Item((object) nameof (floor));
              int num4 = (int) customProperties.get_Item((object) "plv");
              if (num1 <= num4 && num4 <= num2 && num3 == floor)
              {
                roomName = current.Name;
                flag1 = true;
                break;
              }
            }
          }
        }
      }
      if (!flag1)
      {
        using (List<RoomInfo>.Enumerator enumerator = roomInfoList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            RoomInfo current = enumerator.Current;
            Hashtable customProperties = current.CustomProperties;
            if (((Dictionary<object, object>) customProperties).ContainsKey((object) nameof (floor)) && floor == (int) customProperties.get_Item((object) nameof (floor)))
            {
              roomName = current.Name;
              flag1 = true;
              break;
            }
          }
        }
      }
      if (floorRange != -1)
      {
        int num1 = floor - floorRange;
        int num2 = floor + floorRange;
        using (List<RoomInfo>.Enumerator enumerator = roomInfoList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            RoomInfo current = enumerator.Current;
            Hashtable customProperties = current.CustomProperties;
            if (((Dictionary<object, object>) customProperties).ContainsKey((object) nameof (floor)))
            {
              int num3 = (int) customProperties.get_Item((object) nameof (floor));
              if (num1 <= num3 && num3 <= num2)
              {
                roomName = current.Name;
                flag1 = true;
                break;
              }
            }
          }
        }
      }
      bool flag2 = false;
      if (flag1)
      {
        flag2 = PhotonNetwork.JoinRoom(roomName);
        if (flag2)
          this.mState = MyPhoton.MyState.JOINING;
        else
          this.mError = MyPhoton.MyError.UNKNOWN;
      }
      return flag2;
    }

    public bool LeaveRoom()
    {
      if (this.mState != MyPhoton.MyState.ROOM)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      bool flag = PhotonNetwork.LeaveRoom();
      if (flag)
        this.mState = MyPhoton.MyState.LEAVING;
      else
        this.mError = MyPhoton.MyError.UNKNOWN;
      return flag;
    }

    public MyPhoton.MyRoom GetCurrentRoom()
    {
      Room room = PhotonNetwork.room;
      MyPhoton.MyRoom myRoom = new MyPhoton.MyRoom();
      if (room != null)
      {
        myRoom.name = room.Name;
        myRoom.playerCount = room.PlayerCount;
        myRoom.maxPlayers = room.MaxPlayers;
        myRoom.open = room.IsOpen;
        myRoom.start = false;
        Hashtable customProperties = room.CustomProperties;
        if (customProperties != null)
        {
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) "json"))
            myRoom.json = (string) customProperties.get_Item((object) "json");
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) "start"))
            myRoom.start = (bool) customProperties.get_Item((object) "start");
        }
      }
      return myRoom;
    }

    public bool SetRoomParam(string json)
    {
      if (this.mState != MyPhoton.MyState.ROOM)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      Hashtable hashtable = new Hashtable();
      ((Dictionary<object, object>) hashtable).Add((object) nameof (json), (object) json);
      Hashtable propertiesToSet = hashtable;
      if (PhotonNetwork.room == null)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      PhotonNetwork.room.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
      return true;
    }

    public bool AddRoomParam(string key, string param)
    {
      if (this.mState != MyPhoton.MyState.ROOM)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      Room room = PhotonNetwork.room;
      if (room == null)
        return false;
      Hashtable customProperties = room.CustomProperties;
      Hashtable propertiesToSet = new Hashtable();
      if (customProperties != null)
      {
        ((Dictionary<object, object>) propertiesToSet).Add((object) "json", customProperties.get_Item((object) "json"));
        if (((Dictionary<object, object>) customProperties).ContainsKey((object) key))
          return false;
      }
      ((Dictionary<object, object>) propertiesToSet).Add((object) key, (object) param);
      room.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
      room.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
      return true;
    }

    public string GetRoomParam(string key)
    {
      if (this.mState != MyPhoton.MyState.ROOM)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return (string) null;
      }
      Room room = PhotonNetwork.room;
      if (room == null)
        return (string) null;
      Hashtable customProperties = room.CustomProperties;
      if (customProperties != null)
      {
        object obj = (object) null;
        if (((Dictionary<object, object>) customProperties).TryGetValue((object) key, out obj))
          return (string) obj;
      }
      return (string) null;
    }

    public bool CloseRoom()
    {
      if (this.mState != MyPhoton.MyState.ROOM)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      PhotonNetwork.room.IsVisible = false;
      string empty = string.Empty;
      if (((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).Count > 0)
      {
        Hashtable customProperties = PhotonNetwork.room.CustomProperties;
        if (customProperties != null && ((Dictionary<object, object>) customProperties).Count > 0)
          empty = (string) customProperties.get_Item((object) "json");
      }
      Hashtable propertiesToSet = new Hashtable();
      ((Dictionary<object, object>) propertiesToSet).Add((object) "json", (object) empty);
      ((Dictionary<object, object>) propertiesToSet).Add((object) "start", (object) true);
      PhotonNetwork.room.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
      return true;
    }

    public bool OpenRoom()
    {
      if (this.mState != MyPhoton.MyState.ROOM)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      PhotonNetwork.room.IsVisible = true;
      string empty = string.Empty;
      if (((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).Count > 0)
      {
        Hashtable customProperties = PhotonNetwork.room.CustomProperties;
        if (customProperties != null && ((Dictionary<object, object>) customProperties).Count > 0)
          empty = (string) customProperties.get_Item((object) "json");
      }
      Hashtable hashtable = new Hashtable();
      ((Dictionary<object, object>) hashtable).Add((object) "json", (object) empty);
      ((Dictionary<object, object>) hashtable).Add((object) "start", (object) false);
      Hashtable propertiesToSet = hashtable;
      PhotonNetwork.room.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
      PhotonNetwork.room.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
      return true;
    }

    public bool IsOldestPlayer(int playerID)
    {
      if (this.mState != MyPhoton.MyState.ROOM)
        return false;
      bool flag = false;
      using (List<MyPhoton.MyPlayer>.Enumerator enumerator = this.GetRoomPlayerList().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          MyPhoton.MyPlayer current = enumerator.Current;
          if (current.playerID < playerID)
            return false;
          if (current.playerID == playerID)
            flag = true;
        }
      }
      return flag;
    }

    public bool IsOldestPlayer()
    {
      if (this.mState != MyPhoton.MyState.ROOM)
        return false;
      return this.IsOldestPlayer(this.GetMyPlayer().playerID);
    }

    public int GetOldestPlayer()
    {
      if (this.mState != MyPhoton.MyState.ROOM)
        return 0;
      int num = 0;
      using (List<MyPhoton.MyPlayer>.Enumerator enumerator = this.GetRoomPlayerList().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          MyPhoton.MyPlayer current = enumerator.Current;
          if ((current.playerID < num || num == 0) && current.start)
            num = current.playerID;
        }
      }
      return num;
    }

    public bool IsHost()
    {
      if (this.mState != MyPhoton.MyState.ROOM)
        return false;
      MyPhoton.MyPlayer myPlayer = this.GetMyPlayer();
      List<MyPhoton.MyPlayer> roomPlayerList = this.GetRoomPlayerList();
      int photonPlayerId = myPlayer.photonPlayerID;
      using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          if (enumerator.Current.photonPlayerID < photonPlayerId)
            return false;
        }
      }
      return true;
    }

    public bool IsHost(int playerID)
    {
      if (this.mState != MyPhoton.MyState.ROOM)
        return false;
      using (List<MyPhoton.MyPlayer>.Enumerator enumerator = this.GetRoomPlayerList().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          if (enumerator.Current.playerID < playerID)
            return false;
        }
      }
      return true;
    }

    public bool UseEncrypt { get; set; }

    public bool SendRoomMessage(bool reliable, string msg, MyPhoton.SEND_TYPE eventcode = MyPhoton.SEND_TYPE.Normal)
    {
      if (this.mState != MyPhoton.MyState.ROOM)
        return false;
      int num = 0;
      Hashtable hashtable1;
      if (num == 0)
      {
        Hashtable hashtable2 = new Hashtable();
        ((Dictionary<object, object>) hashtable2).Add((object) "s", (object) num);
        ((Dictionary<object, object>) hashtable2).Add((object) "m", (object) msg);
        hashtable1 = hashtable2;
      }
      else
      {
        byte[] numArray = MyEncrypt.Encrypt(num + this.GetCryptKey(), msg, true);
        Hashtable hashtable2 = new Hashtable();
        ((Dictionary<object, object>) hashtable2).Add((object) "s", (object) num);
        ((Dictionary<object, object>) hashtable2).Add((object) "m", (object) numArray);
        hashtable1 = hashtable2;
      }
      if (this.SortRoomMessage)
      {
        ((Dictionary<object, object>) hashtable1).Add((object) "sq", (object) this.mSendRoomMessageID);
        ++this.mSendRoomMessageID;
      }
      bool flag = PhotonNetwork.RaiseEvent((byte) eventcode, (object) hashtable1, reliable, (RaiseEventOptions) null);
      if (!this.DisconnectIfSendRoomMessageFailed || flag)
        return flag;
      this.Disconnect();
      this.mError = MyPhoton.MyError.RAISE_EVENT_FAILED;
      DebugUtility.LogError("SendRoomMessage failed!");
      return false;
    }

    public void SendFlush()
    {
      PhotonNetwork.SendOutgoingCommands();
    }

    public List<MyPhoton.MyPlayer> GetRoomPlayerList()
    {
      List<MyPhoton.MyPlayer> myPlayerList = new List<MyPhoton.MyPlayer>();
      foreach (PhotonPlayer player in PhotonNetwork.playerList)
      {
        MyPhoton.MyPlayer myPlayer = new MyPhoton.MyPlayer();
        myPlayer.playerID = player.ID;
        Hashtable customProperties = player.CustomProperties;
        if (customProperties != null && ((Dictionary<object, object>) customProperties).Count > 0)
        {
          myPlayer.json = (string) customProperties.get_Item((object) "json");
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) "resumeID"))
            myPlayer.resumeID = (int) customProperties.get_Item((object) "resumeID");
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) "BattleStart"))
            myPlayer.start = (bool) customProperties.get_Item((object) "BattleStart");
        }
        myPlayerList.Add(myPlayer);
      }
      return myPlayerList;
    }

    public List<JSON_MyPhotonPlayerParam> GetMyPlayersStarted()
    {
      return this.mPlayersStarted;
    }

    public int MyPlayerIndex { get; set; }

    public bool IsMultiPlay { get; set; }

    public bool IsMultiVersus { get; set; }

    public void Reset()
    {
      if (this.mState != MyPhoton.MyState.NOP)
        this.Disconnect();
      this.MyPlayerIndex = 0;
      this.IsMultiPlay = false;
      this.IsMultiVersus = false;
      this.mPlayersStarted.Clear();
    }

    public void EnableKeepAlive(bool flag)
    {
      if (PhotonNetwork.isMessageQueueRunning != flag)
        this.Log("[PUN]KeepAlive changed to:" + (object) flag);
      PhotonNetwork.isMessageQueueRunning = flag;
    }

    public bool IsConnected()
    {
      return PhotonNetwork.connected;
    }

    public enum MyState
    {
      NOP,
      CONNECTING,
      LOBBY,
      JOINING,
      ROOM,
      LEAVING,
      NUM,
    }

    public enum MyError
    {
      NOP,
      UNKNOWN,
      ILLEGAL_STATE,
      TIMEOUT,
      TIMEOUT2,
      FULL_CLIENTS,
      ROOM_NAME_DUPLICATED,
      ROOM_IS_FULL,
      ROOM_IS_NOT_EXIST,
      ROOM_IS_NOT_OPEN,
      RAISE_EVENT_FAILED,
      NUM,
    }

    public enum SEND_TYPE : byte
    {
      Normal,
      Resume,
    }

    public class MyEvent
    {
      public MyPhoton.SEND_TYPE code;
      public int playerID;
      public string json;
      public int sendID;
    }

    public class MyRoom
    {
      public string name = string.Empty;
      public int maxPlayers = 1;
      public bool open = true;
      public string json = string.Empty;
      public int playerCount;
      public bool start;
    }

    public class MyPlayer
    {
      public int resumeID = -1;
      public string json = string.Empty;
      public int photonPlayerID;
      public bool start;

      public int playerID
      {
        get
        {
          if (this.resumeID != -1)
            return this.resumeID;
          return this.photonPlayerID;
        }
        set
        {
          this.photonPlayerID = value;
        }
      }
    }
  }
}
