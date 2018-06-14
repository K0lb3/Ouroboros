// Decompiled with JetBrains decompiler
// Type: LoadBalancingPeer
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class LoadBalancingPeer : PhotonPeer
{
  private readonly Dictionary<byte, object> opParameters;

  public LoadBalancingPeer(ConnectionProtocol protocolType)
  {
    base.\u002Ector(protocolType);
  }

  public LoadBalancingPeer(IPhotonPeerListener listener, ConnectionProtocol protocolType)
  {
    base.\u002Ector(listener, protocolType);
  }

  internal bool IsProtocolSecure
  {
    get
    {
      return this.get_UsedProtocol() == 5;
    }
  }

  public virtual bool OpGetRegions(string appId)
  {
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    dictionary[(byte) 224] = (object) appId;
    return this.OpCustom((byte) 220, dictionary, true, (byte) 0, true);
  }

  public virtual bool OpJoinLobby(TypedLobby lobby = null)
  {
    if (this.DebugOut >= 3)
      this.get_Listener().DebugReturn((DebugLevel) 3, "OpJoinLobby()");
    Dictionary<byte, object> dictionary = (Dictionary<byte, object>) null;
    if (lobby != null && !lobby.IsDefault)
    {
      dictionary = new Dictionary<byte, object>();
      dictionary[(byte) 213] = (object) lobby.Name;
      dictionary[(byte) 212] = (object) lobby.Type;
    }
    return this.OpCustom((byte) 229, dictionary, true);
  }

  public virtual bool OpLeaveLobby()
  {
    if (this.DebugOut >= 3)
      this.get_Listener().DebugReturn((DebugLevel) 3, "OpLeaveLobby()");
    return this.OpCustom((byte) 228, (Dictionary<byte, object>) null, true);
  }

  private void RoomOptionsToOpParameters(Dictionary<byte, object> op, RoomOptions roomOptions)
  {
    if (roomOptions == null)
      roomOptions = new RoomOptions();
    Hashtable hashtable = new Hashtable();
    hashtable.set_Item((object) (byte) 253, (object) roomOptions.IsOpen);
    hashtable.set_Item((object) (byte) 254, (object) roomOptions.IsVisible);
    hashtable.set_Item((object) (byte) 250, roomOptions.CustomRoomPropertiesForLobby != null ? (object) roomOptions.CustomRoomPropertiesForLobby : (object) new string[0]);
    ((IDictionary) hashtable).MergeStringKeys((IDictionary) roomOptions.CustomRoomProperties);
    if ((int) roomOptions.MaxPlayers > 0)
      hashtable.set_Item((object) byte.MaxValue, (object) roomOptions.MaxPlayers);
    op[(byte) 248] = (object) hashtable;
    op[(byte) 241] = (object) roomOptions.CleanupCacheOnLeave;
    if (roomOptions.CleanupCacheOnLeave)
      hashtable.set_Item((object) (byte) 249, (object) true);
    if (roomOptions.PlayerTtl > 0 || roomOptions.PlayerTtl == -1)
    {
      op[(byte) 232] = (object) true;
      op[(byte) 235] = (object) roomOptions.PlayerTtl;
    }
    if (roomOptions.EmptyRoomTtl > 0)
      op[(byte) 236] = (object) roomOptions.EmptyRoomTtl;
    if (roomOptions.SuppressRoomEvents)
      op[(byte) 237] = (object) true;
    if (roomOptions.Plugins != null)
      op[(byte) 204] = (object) roomOptions.Plugins;
    if (!roomOptions.PublishUserId)
      return;
    op[(byte) 239] = (object) true;
  }

  public virtual bool OpCreateRoom(EnterRoomParams opParams)
  {
    if (this.DebugOut >= 3)
      this.get_Listener().DebugReturn((DebugLevel) 3, "OpCreateRoom()");
    Dictionary<byte, object> op = new Dictionary<byte, object>();
    if (!string.IsNullOrEmpty(opParams.RoomName))
      op[byte.MaxValue] = (object) opParams.RoomName;
    if (opParams.Lobby != null && !string.IsNullOrEmpty(opParams.Lobby.Name))
    {
      op[(byte) 213] = (object) opParams.Lobby.Name;
      op[(byte) 212] = (object) opParams.Lobby.Type;
    }
    if (opParams.ExpectedUsers != null && opParams.ExpectedUsers.Length > 0)
      op[(byte) 238] = (object) opParams.ExpectedUsers;
    if (opParams.OnGameServer)
    {
      if (opParams.PlayerProperties != null && ((Dictionary<object, object>) opParams.PlayerProperties).Count > 0)
      {
        op[(byte) 249] = (object) opParams.PlayerProperties;
        op[(byte) 250] = (object) true;
      }
      this.RoomOptionsToOpParameters(op, opParams.RoomOptions);
    }
    return this.OpCustom((byte) 227, op, true);
  }

  public virtual bool OpJoinRoom(EnterRoomParams opParams)
  {
    if (this.DebugOut >= 3)
      this.get_Listener().DebugReturn((DebugLevel) 3, "OpJoinRoom()");
    Dictionary<byte, object> op = new Dictionary<byte, object>();
    if (!string.IsNullOrEmpty(opParams.RoomName))
      op[byte.MaxValue] = (object) opParams.RoomName;
    if (opParams.CreateIfNotExists)
    {
      op[(byte) 215] = (object) (byte) 1;
      if (opParams.Lobby != null)
      {
        op[(byte) 213] = (object) opParams.Lobby.Name;
        op[(byte) 212] = (object) opParams.Lobby.Type;
      }
    }
    if (opParams.RejoinOnly)
      op[(byte) 215] = (object) (byte) 3;
    if (opParams.ExpectedUsers != null && opParams.ExpectedUsers.Length > 0)
      op[(byte) 238] = (object) opParams.ExpectedUsers;
    if (opParams.OnGameServer)
    {
      if (opParams.PlayerProperties != null && ((Dictionary<object, object>) opParams.PlayerProperties).Count > 0)
      {
        op[(byte) 249] = (object) opParams.PlayerProperties;
        op[(byte) 250] = (object) true;
      }
      if (opParams.CreateIfNotExists)
        this.RoomOptionsToOpParameters(op, opParams.RoomOptions);
    }
    return this.OpCustom((byte) 226, op, true);
  }

  public virtual bool OpJoinRandomRoom(OpJoinRandomRoomParams opJoinRandomRoomParams)
  {
    if (this.DebugOut >= 3)
      this.get_Listener().DebugReturn((DebugLevel) 3, "OpJoinRandomRoom()");
    Hashtable hashtable = new Hashtable();
    ((IDictionary) hashtable).MergeStringKeys((IDictionary) opJoinRandomRoomParams.ExpectedCustomRoomProperties);
    if ((int) opJoinRandomRoomParams.ExpectedMaxPlayers > 0)
      hashtable.set_Item((object) byte.MaxValue, (object) opJoinRandomRoomParams.ExpectedMaxPlayers);
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    if (((Dictionary<object, object>) hashtable).Count > 0)
      dictionary[(byte) 248] = (object) hashtable;
    if (opJoinRandomRoomParams.MatchingType != MatchmakingMode.FillRoom)
      dictionary[(byte) 223] = (object) opJoinRandomRoomParams.MatchingType;
    if (opJoinRandomRoomParams.TypedLobby != null && !string.IsNullOrEmpty(opJoinRandomRoomParams.TypedLobby.Name))
    {
      dictionary[(byte) 213] = (object) opJoinRandomRoomParams.TypedLobby.Name;
      dictionary[(byte) 212] = (object) opJoinRandomRoomParams.TypedLobby.Type;
    }
    if (!string.IsNullOrEmpty(opJoinRandomRoomParams.SqlLobbyFilter))
      dictionary[(byte) 245] = (object) opJoinRandomRoomParams.SqlLobbyFilter;
    if (opJoinRandomRoomParams.ExpectedUsers != null && opJoinRandomRoomParams.ExpectedUsers.Length > 0)
      dictionary[(byte) 238] = (object) opJoinRandomRoomParams.ExpectedUsers;
    return this.OpCustom((byte) 225, dictionary, true);
  }

  public virtual bool OpLeaveRoom(bool becomeInactive)
  {
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    if (becomeInactive)
      dictionary[(byte) 233] = (object) becomeInactive;
    return this.OpCustom((byte) 254, dictionary, true);
  }

  public virtual bool OpFindFriends(string[] friendsToFind)
  {
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    if (friendsToFind != null && friendsToFind.Length > 0)
      dictionary[(byte) 1] = (object) friendsToFind;
    return this.OpCustom((byte) 222, dictionary, true);
  }

  public bool OpSetCustomPropertiesOfActor(int actorNr, Hashtable actorProperties)
  {
    return this.OpSetPropertiesOfActor(actorNr, ((IDictionary) actorProperties).StripToStringKeys(), (Hashtable) null, false);
  }

  protected internal bool OpSetPropertiesOfActor(int actorNr, Hashtable actorProperties, Hashtable expectedProperties = null, bool webForward = false)
  {
    if (this.DebugOut >= 3)
      this.get_Listener().DebugReturn((DebugLevel) 3, "OpSetPropertiesOfActor()");
    if (actorNr <= 0 || actorProperties == null)
    {
      if (this.DebugOut >= 3)
        this.get_Listener().DebugReturn((DebugLevel) 3, "OpSetPropertiesOfActor not sent. ActorNr must be > 0 and actorProperties != null.");
      return false;
    }
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    dictionary.Add((byte) 251, (object) actorProperties);
    dictionary.Add((byte) 254, (object) actorNr);
    dictionary.Add((byte) 250, (object) true);
    if (expectedProperties != null && ((Dictionary<object, object>) expectedProperties).Count != 0)
      dictionary.Add((byte) 231, (object) expectedProperties);
    if (webForward)
      dictionary[(byte) 234] = (object) true;
    return this.OpCustom((byte) 252, dictionary, true, (byte) 0, false);
  }

  protected void OpSetPropertyOfRoom(byte propCode, object value)
  {
    Hashtable gameProperties = new Hashtable();
    gameProperties.set_Item((object) propCode, value);
    this.OpSetPropertiesOfRoom(gameProperties, (Hashtable) null, false);
  }

  public bool OpSetCustomPropertiesOfRoom(Hashtable gameProperties, bool broadcast, byte channelId)
  {
    return this.OpSetPropertiesOfRoom(((IDictionary) gameProperties).StripToStringKeys(), (Hashtable) null, false);
  }

  protected internal bool OpSetPropertiesOfRoom(Hashtable gameProperties, Hashtable expectedProperties = null, bool webForward = false)
  {
    if (this.DebugOut >= 3)
      this.get_Listener().DebugReturn((DebugLevel) 3, "OpSetPropertiesOfRoom()");
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    dictionary.Add((byte) 251, (object) gameProperties);
    dictionary.Add((byte) 250, (object) true);
    if (expectedProperties != null && ((Dictionary<object, object>) expectedProperties).Count != 0)
      dictionary.Add((byte) 231, (object) expectedProperties);
    if (webForward)
      dictionary[(byte) 234] = (object) true;
    return this.OpCustom((byte) 252, dictionary, true, (byte) 0, false);
  }

  public virtual bool OpAuthenticate(string appId, string appVersion, AuthenticationValues authValues, string regionCode, bool getLobbyStatistics)
  {
    if (this.DebugOut >= 3)
      this.get_Listener().DebugReturn((DebugLevel) 3, "OpAuthenticate()");
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    if (getLobbyStatistics)
      dictionary[(byte) 211] = (object) true;
    if (authValues != null && authValues.Token != null)
    {
      dictionary[(byte) 221] = (object) authValues.Token;
      return this.OpCustom((byte) 230, dictionary, true, (byte) 0, false);
    }
    dictionary[(byte) 220] = (object) appVersion;
    dictionary[(byte) 224] = (object) appId;
    if (!string.IsNullOrEmpty(regionCode))
      dictionary[(byte) 210] = (object) regionCode;
    if (authValues != null)
    {
      if (!string.IsNullOrEmpty(authValues.UserId))
        dictionary[(byte) 225] = (object) authValues.UserId;
      if (authValues.AuthType != CustomAuthenticationType.None)
      {
        if (!this.IsProtocolSecure && !this.get_IsEncryptionAvailable())
        {
          this.get_Listener().DebugReturn((DebugLevel) 1, "OpAuthenticate() failed. When you want Custom Authentication encryption is mandatory.");
          return false;
        }
        dictionary[(byte) 217] = (object) authValues.AuthType;
        if (!string.IsNullOrEmpty(authValues.Token))
        {
          dictionary[(byte) 221] = (object) authValues.Token;
        }
        else
        {
          if (!string.IsNullOrEmpty(authValues.AuthGetParameters))
            dictionary[(byte) 216] = (object) authValues.AuthGetParameters;
          if (authValues.AuthPostData != null)
            dictionary[(byte) 214] = authValues.AuthPostData;
        }
      }
    }
    bool flag = this.OpCustom((byte) 230, dictionary, true, (byte) 0, this.get_IsEncryptionAvailable());
    if (!flag)
      this.get_Listener().DebugReturn((DebugLevel) 1, "Error calling OpAuthenticate! Did not work. Check log output, AuthValues and if you're connected.");
    return flag;
  }

  public virtual bool OpAuthenticateOnce(string appId, string appVersion, AuthenticationValues authValues, string regionCode, EncryptionMode encryptionMode, ConnectionProtocol expectedProtocol)
  {
    if (this.DebugOut >= 3)
      this.get_Listener().DebugReturn((DebugLevel) 3, "OpAuthenticate()");
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    if (authValues != null && authValues.Token != null)
    {
      dictionary[(byte) 221] = (object) authValues.Token;
      return this.OpCustom((byte) 231, dictionary, true, (byte) 0, false);
    }
    if (encryptionMode == EncryptionMode.DatagramEncryption && expectedProtocol != null)
    {
      Debug.LogWarning((object) ("Expected protocol set to UDP, due to encryption mode DatagramEncryption. Changing protocol in PhotonServerSettings from: " + (object) PhotonNetwork.PhotonServerSettings.Protocol));
      PhotonNetwork.PhotonServerSettings.Protocol = (ConnectionProtocol) 0;
      expectedProtocol = (ConnectionProtocol) 0;
    }
    dictionary[(byte) 195] = (object) (byte) expectedProtocol;
    dictionary[(byte) 193] = (object) (byte) encryptionMode;
    dictionary[(byte) 220] = (object) appVersion;
    dictionary[(byte) 224] = (object) appId;
    if (!string.IsNullOrEmpty(regionCode))
      dictionary[(byte) 210] = (object) regionCode;
    if (authValues != null)
    {
      if (!string.IsNullOrEmpty(authValues.UserId))
        dictionary[(byte) 225] = (object) authValues.UserId;
      if (authValues.AuthType != CustomAuthenticationType.None)
      {
        dictionary[(byte) 217] = (object) authValues.AuthType;
        if (!string.IsNullOrEmpty(authValues.Token))
        {
          dictionary[(byte) 221] = (object) authValues.Token;
        }
        else
        {
          if (!string.IsNullOrEmpty(authValues.AuthGetParameters))
            dictionary[(byte) 216] = (object) authValues.AuthGetParameters;
          if (authValues.AuthPostData != null)
            dictionary[(byte) 214] = authValues.AuthPostData;
        }
      }
    }
    return this.OpCustom((byte) 231, dictionary, true, (byte) 0, this.get_IsEncryptionAvailable());
  }

  public virtual bool OpChangeGroups(byte[] groupsToRemove, byte[] groupsToAdd)
  {
    if (this.DebugOut >= 5)
      this.get_Listener().DebugReturn((DebugLevel) 5, "OpChangeGroups()");
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    if (groupsToRemove != null)
      dictionary[(byte) 239] = (object) groupsToRemove;
    if (groupsToAdd != null)
      dictionary[(byte) 238] = (object) groupsToAdd;
    return this.OpCustom((byte) 248, dictionary, true, (byte) 0);
  }

  public virtual bool OpRaiseEvent(byte eventCode, object customEventContent, bool sendReliable, RaiseEventOptions raiseEventOptions)
  {
    this.opParameters.Clear();
    this.opParameters[(byte) 244] = (object) eventCode;
    if (customEventContent != null)
      this.opParameters[(byte) 245] = customEventContent;
    if (raiseEventOptions == null)
    {
      raiseEventOptions = RaiseEventOptions.Default;
    }
    else
    {
      if (raiseEventOptions.CachingOption != EventCaching.DoNotCache)
        this.opParameters[(byte) 247] = (object) raiseEventOptions.CachingOption;
      if (raiseEventOptions.Receivers != ReceiverGroup.Others)
        this.opParameters[(byte) 246] = (object) raiseEventOptions.Receivers;
      if ((int) raiseEventOptions.InterestGroup != 0)
        this.opParameters[(byte) 240] = (object) raiseEventOptions.InterestGroup;
      if (raiseEventOptions.TargetActors != null)
        this.opParameters[(byte) 252] = (object) raiseEventOptions.TargetActors;
      if (raiseEventOptions.ForwardToWebhook)
        this.opParameters[(byte) 234] = (object) true;
    }
    return this.OpCustom((byte) 253, this.opParameters, sendReliable, raiseEventOptions.SequenceChannel, raiseEventOptions.Encrypt);
  }

  public virtual bool OpSettings(bool receiveLobbyStats)
  {
    if (this.DebugOut >= 5)
      this.get_Listener().DebugReturn((DebugLevel) 5, "OpSettings()");
    this.opParameters.Clear();
    if (receiveLobbyStats)
      this.opParameters[(byte) 0] = (object) receiveLobbyStats;
    if (this.opParameters.Count == 0)
      return true;
    return this.OpCustom((byte) 218, this.opParameters, true);
  }
}
