// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ChatClient
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace ExitGames.Client.Photon.Chat
{
  public class ChatClient : IPhotonPeerListener
  {
    private string chatRegion = "EU";
    private int msDeltaForServiceCalls = 50;
    private const int FriendRequestListMax = 1024;
    private const string ChatAppName = "chat";
    public int MessageLimit;
    public readonly Dictionary<string, ChatChannel> PublicChannels;
    public readonly Dictionary<string, ChatChannel> PrivateChannels;
    private readonly HashSet<string> PublicChannelsUnsubscribing;
    private readonly IChatClientListener listener;
    public ChatPeer chatPeer;
    private bool didAuthenticate;
    private int msTimestampOfLastServiceCall;

    public ChatClient(IChatClientListener listener, ConnectionProtocol protocol = 0)
    {
      this.listener = listener;
      this.State = ChatState.Uninitialized;
      this.chatPeer = new ChatPeer((IPhotonPeerListener) this, protocol);
      this.PublicChannels = new Dictionary<string, ChatChannel>();
      this.PrivateChannels = new Dictionary<string, ChatChannel>();
      this.PublicChannelsUnsubscribing = new HashSet<string>();
    }

    void IPhotonPeerListener.DebugReturn(DebugLevel level, string message)
    {
      this.listener.DebugReturn(level, message);
    }

    void IPhotonPeerListener.OnEvent(EventData eventData)
    {
      switch ((byte) eventData.Code)
      {
        case 0:
          this.HandleChatMessagesEvent(eventData);
          break;
        case 2:
          this.HandlePrivateMessageEvent(eventData);
          break;
        case 4:
          this.HandleStatusUpdate(eventData);
          break;
        case 5:
          this.HandleSubscribeEvent(eventData);
          break;
        case 6:
          this.HandleUnsubscribeEvent(eventData);
          break;
      }
    }

    void IPhotonPeerListener.OnOperationResponse(OperationResponse operationResponse)
    {
      switch ((byte) operationResponse.OperationCode)
      {
        case 230:
          this.HandleAuthResponse(operationResponse);
          break;
        default:
          if (operationResponse.ReturnCode == null || this.DebugOut < 1)
            break;
          if (operationResponse.ReturnCode == -2)
          {
            this.listener.DebugReturn((DebugLevel) 1, string.Format("Chat Operation {0} unknown on server. Check your AppId and make sure it's for a Chat application.", (object) (byte) operationResponse.OperationCode));
            break;
          }
          this.listener.DebugReturn((DebugLevel) 1, string.Format("Chat Operation {0} failed (Code: {1}). Debug Message: {2}", (object) (byte) operationResponse.OperationCode, (object) (short) operationResponse.ReturnCode, (object) operationResponse.DebugMessage));
          break;
      }
    }

    void IPhotonPeerListener.OnStatusChanged(StatusCode statusCode)
    {
      StatusCode statusCode1 = statusCode;
      if (statusCode1 != 1024)
      {
        if (statusCode1 != 1025)
        {
          if (statusCode1 != 1048)
          {
            if (statusCode1 != 1049)
              return;
            this.State = ChatState.Disconnecting;
            this.chatPeer.Disconnect();
          }
          else
          {
            if (this.didAuthenticate)
              return;
            this.didAuthenticate = this.chatPeer.AuthenticateOnNameServer(this.AppId, this.AppVersion, this.chatRegion, this.AuthValues);
            if (this.didAuthenticate || this.DebugOut < 1)
              return;
            ((IPhotonPeerListener) this).DebugReturn((DebugLevel) 1, "Error calling OpAuthenticate! Did not work. Check log output, AuthValues and if you're connected. State: " + (object) this.State);
          }
        }
        else if (this.State == ChatState.Authenticated)
        {
          this.ConnectToFrontEnd();
        }
        else
        {
          this.State = ChatState.Disconnected;
          this.listener.OnChatStateChange(ChatState.Disconnected);
          this.listener.OnDisconnected();
        }
      }
      else
      {
        if (!this.chatPeer.IsProtocolSecure)
          this.chatPeer.EstablishEncryption();
        else if (!this.didAuthenticate)
        {
          this.didAuthenticate = this.chatPeer.AuthenticateOnNameServer(this.AppId, this.AppVersion, this.chatRegion, this.AuthValues);
          if (!this.didAuthenticate && this.DebugOut >= 1)
            ((IPhotonPeerListener) this).DebugReturn((DebugLevel) 1, "Error calling OpAuthenticate! Did not work. Check log output, AuthValues and if you're connected. State: " + (object) this.State);
        }
        if (this.State == ChatState.ConnectingToNameServer)
        {
          this.State = ChatState.ConnectedToNameServer;
          this.listener.OnChatStateChange(this.State);
        }
        else
        {
          if (this.State != ChatState.ConnectingToFrontEnd)
            return;
          this.AuthenticateOnFrontEnd();
        }
      }
    }

    public string NameServerAddress { get; private set; }

    public string FrontendAddress { get; private set; }

    public string ChatRegion
    {
      get
      {
        return this.chatRegion;
      }
      set
      {
        this.chatRegion = value;
      }
    }

    public ChatState State { get; private set; }

    public ChatDisconnectCause DisconnectedCause { get; private set; }

    public bool CanChat
    {
      get
      {
        if (this.State == ChatState.ConnectedToFrontEnd)
          return this.HasPeer;
        return false;
      }
    }

    public bool CanChatInChannel(string channelName)
    {
      if (this.CanChat && this.PublicChannels.ContainsKey(channelName))
        return !this.PublicChannelsUnsubscribing.Contains(channelName);
      return false;
    }

    private bool HasPeer
    {
      get
      {
        return this.chatPeer != null;
      }
    }

    public string AppVersion { get; private set; }

    public string AppId { get; private set; }

    public AuthenticationValues AuthValues { get; set; }

    public string UserId
    {
      get
      {
        if (this.AuthValues != null)
          return this.AuthValues.UserId;
        return (string) null;
      }
      private set
      {
        if (this.AuthValues == null)
          this.AuthValues = new AuthenticationValues();
        this.AuthValues.UserId = value;
      }
    }

    public bool Connect(string appId, string appVersion, AuthenticationValues authValues)
    {
      this.chatPeer.TimePingInterval = (__Null) 3000;
      this.DisconnectedCause = ChatDisconnectCause.None;
      if (authValues != null)
      {
        this.AuthValues = authValues;
        if (this.AuthValues.UserId == null || this.AuthValues.UserId == string.Empty)
        {
          if (this.DebugOut >= 1)
            this.listener.DebugReturn((DebugLevel) 1, "Connect failed: no UserId specified in authentication values");
          return false;
        }
        this.AppId = appId;
        this.AppVersion = appVersion;
        this.didAuthenticate = false;
        this.msDeltaForServiceCalls = 100;
        this.chatPeer.set_QuickResendAttempts((byte) 2);
        this.chatPeer.SentCountAllowance = (__Null) 7;
        this.PublicChannels.Clear();
        this.PrivateChannels.Clear();
        this.PublicChannelsUnsubscribing.Clear();
        this.NameServerAddress = this.chatPeer.NameServerAddress;
        bool flag = this.chatPeer.Connect();
        if (flag)
          this.State = ChatState.ConnectingToNameServer;
        return flag;
      }
      if (this.DebugOut >= 1)
        this.listener.DebugReturn((DebugLevel) 1, "Connect failed: no authentication values specified");
      return false;
    }

    public void Service()
    {
      if (!this.HasPeer || Environment.TickCount - this.msTimestampOfLastServiceCall <= this.msDeltaForServiceCalls && this.msTimestampOfLastServiceCall != 0)
        return;
      this.msTimestampOfLastServiceCall = Environment.TickCount;
      this.chatPeer.Service();
    }

    public void Disconnect()
    {
      if (!this.HasPeer || this.chatPeer.get_PeerState() == null)
        return;
      this.chatPeer.Disconnect();
    }

    public void StopThread()
    {
      if (!this.HasPeer)
        return;
      this.chatPeer.StopThread();
    }

    public bool Subscribe(string[] channels)
    {
      return this.Subscribe(channels, 0);
    }

    public bool Subscribe(string[] channels, int messagesFromHistory)
    {
      if (!this.CanChat)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "Subscribe called while not connected to front end server.");
        return false;
      }
      if (channels != null && channels.Length != 0)
        return this.SendChannelOperation(channels, (byte) 0, messagesFromHistory);
      if (this.DebugOut >= 2)
        this.listener.DebugReturn((DebugLevel) 2, "Subscribe can't be called for empty or null channels-list.");
      return false;
    }

    public bool Unsubscribe(string[] channels)
    {
      if (!this.CanChat)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "Unsubscribe called while not connected to front end server.");
        return false;
      }
      if (channels == null || channels.Length == 0)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "Unsubscribe can't be called for empty or null channels-list.");
        return false;
      }
      foreach (string channel in channels)
        this.PublicChannelsUnsubscribing.Add(channel);
      return this.SendChannelOperation(channels, (byte) 1, 0);
    }

    public bool PublishMessage(string channelName, object message)
    {
      return this.publishMessage(channelName, message, true);
    }

    internal bool PublishMessageUnreliable(string channelName, object message)
    {
      return this.publishMessage(channelName, message, false);
    }

    private bool publishMessage(string channelName, object message, bool reliable)
    {
      if (!this.CanChat)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "PublishMessage called while not connected to front end server.");
        return false;
      }
      if (string.IsNullOrEmpty(channelName) || message == null)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "PublishMessage parameters must be non-null and not empty.");
        return false;
      }
      return this.chatPeer.OpCustom((byte) 2, new Dictionary<byte, object>() { { (byte) 1, (object) channelName }, { (byte) 3, message } }, reliable);
    }

    public bool SendPrivateMessage(string target, object message)
    {
      return this.SendPrivateMessage(target, message, false);
    }

    public bool SendPrivateMessage(string target, object message, bool encrypt)
    {
      return this.sendPrivateMessage(target, message, encrypt, true);
    }

    internal bool SendPrivateMessageUnreliable(string target, object message, bool encrypt)
    {
      return this.sendPrivateMessage(target, message, encrypt, false);
    }

    private bool sendPrivateMessage(string target, object message, bool encrypt, bool reliable)
    {
      if (!this.CanChat)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "SendPrivateMessage called while not connected to front end server.");
        return false;
      }
      if (string.IsNullOrEmpty(target) || message == null)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "SendPrivateMessage parameters must be non-null and not empty.");
        return false;
      }
      return this.chatPeer.OpCustom((byte) 3, new Dictionary<byte, object>() { { (byte) 225, (object) target }, { (byte) 3, message } }, reliable, (byte) 0, encrypt);
    }

    private bool SetOnlineStatus(int status, object message, bool skipMessage)
    {
      if (!this.CanChat)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "SetOnlineStatus called while not connected to front end server.");
        return false;
      }
      Dictionary<byte, object> dictionary = new Dictionary<byte, object>() { { (byte) 10, (object) status } };
      if (skipMessage)
        dictionary[(byte) 12] = (object) true;
      else
        dictionary[(byte) 3] = message;
      return this.chatPeer.OpCustom((byte) 5, dictionary, true);
    }

    public bool SetOnlineStatus(int status)
    {
      return this.SetOnlineStatus(status, (object) null, true);
    }

    public bool SetOnlineStatus(int status, object message)
    {
      return this.SetOnlineStatus(status, message, false);
    }

    public bool AddFriends(string[] friends)
    {
      if (!this.CanChat)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "AddFriends called while not connected to front end server.");
        return false;
      }
      if (friends == null || friends.Length == 0)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "AddFriends can't be called for empty or null list.");
        return false;
      }
      if (friends.Length > 1024)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "AddFriends max list size exceeded: " + (object) friends.Length + " > " + (object) 1024);
        return false;
      }
      return this.chatPeer.OpCustom((byte) 6, new Dictionary<byte, object>() { { (byte) 11, (object) friends } }, true);
    }

    public bool RemoveFriends(string[] friends)
    {
      if (!this.CanChat)
      {
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "RemoveFriends called while not connected to front end server.");
        return false;
      }
      if (friends == null || friends.Length == 0)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "RemoveFriends can't be called for empty or null list.");
        return false;
      }
      if (friends.Length > 1024)
      {
        if (this.DebugOut >= 2)
          this.listener.DebugReturn((DebugLevel) 2, "RemoveFriends max list size exceeded: " + (object) friends.Length + " > " + (object) 1024);
        return false;
      }
      return this.chatPeer.OpCustom((byte) 7, new Dictionary<byte, object>() { { (byte) 11, (object) friends } }, true);
    }

    public string GetPrivateChannelNameByUser(string userName)
    {
      return string.Format("{0}:{1}", (object) this.UserId, (object) userName);
    }

    public bool TryGetChannel(string channelName, bool isPrivate, out ChatChannel channel)
    {
      if (!isPrivate)
        return this.PublicChannels.TryGetValue(channelName, out channel);
      return this.PrivateChannels.TryGetValue(channelName, out channel);
    }

    public bool TryGetChannel(string channelName, out ChatChannel channel)
    {
      if (this.PublicChannels.TryGetValue(channelName, out channel))
        return true;
      return this.PrivateChannels.TryGetValue(channelName, out channel);
    }

    public void SendAcksOnly()
    {
      if (this.chatPeer == null)
        return;
      this.chatPeer.SendAcksOnly();
    }

    public DebugLevel DebugOut
    {
      set
      {
        this.chatPeer.DebugOut = (__Null) value;
      }
      get
      {
        return (DebugLevel) this.chatPeer.DebugOut;
      }
    }

    private bool SendChannelOperation(string[] channels, byte operation, int historyLength)
    {
      Dictionary<byte, object> dictionary = new Dictionary<byte, object>() { { (byte) 0, (object) channels } };
      if (historyLength != 0)
        dictionary.Add((byte) 14, (object) historyLength);
      return this.chatPeer.OpCustom(operation, dictionary, true);
    }

    private void HandlePrivateMessageEvent(EventData eventData)
    {
      object parameter1 = ((Dictionary<byte, object>) eventData.Parameters)[(byte) 3];
      string parameter2 = (string) ((Dictionary<byte, object>) eventData.Parameters)[(byte) 5];
      string str = this.UserId == null || !this.UserId.Equals(parameter2) ? this.GetPrivateChannelNameByUser(parameter2) : this.GetPrivateChannelNameByUser((string) ((Dictionary<byte, object>) eventData.Parameters)[(byte) 225]);
      ChatChannel chatChannel;
      if (!this.PrivateChannels.TryGetValue(str, out chatChannel))
      {
        chatChannel = new ChatChannel(str);
        chatChannel.IsPrivate = true;
        chatChannel.MessageLimit = this.MessageLimit;
        this.PrivateChannels.Add(chatChannel.Name, chatChannel);
      }
      chatChannel.Add(parameter2, parameter1);
      this.listener.OnPrivateMessage(parameter2, parameter1, str);
    }

    private void HandleChatMessagesEvent(EventData eventData)
    {
      object[] parameter1 = (object[]) ((Dictionary<byte, object>) eventData.Parameters)[(byte) 2];
      string[] parameter2 = (string[]) ((Dictionary<byte, object>) eventData.Parameters)[(byte) 4];
      string parameter3 = (string) ((Dictionary<byte, object>) eventData.Parameters)[(byte) 1];
      ChatChannel chatChannel;
      if (!this.PublicChannels.TryGetValue(parameter3, out chatChannel))
      {
        if (this.DebugOut < 2)
          return;
        this.listener.DebugReturn((DebugLevel) 2, "Channel " + parameter3 + " for incoming message event not found.");
      }
      else
      {
        chatChannel.Add(parameter2, parameter1);
        this.listener.OnGetMessages(parameter3, parameter2, parameter1);
      }
    }

    private void HandleSubscribeEvent(EventData eventData)
    {
      string[] parameter1 = (string[]) ((Dictionary<byte, object>) eventData.Parameters)[(byte) 0];
      bool[] parameter2 = (bool[]) ((Dictionary<byte, object>) eventData.Parameters)[(byte) 15];
      for (int index = 0; index < parameter1.Length; ++index)
      {
        if (parameter2[index])
        {
          string str = parameter1[index];
          if (!this.PublicChannels.ContainsKey(str))
          {
            ChatChannel chatChannel = new ChatChannel(str);
            chatChannel.MessageLimit = this.MessageLimit;
            this.PublicChannels.Add(chatChannel.Name, chatChannel);
          }
        }
      }
      this.listener.OnSubscribed(parameter1, parameter2);
    }

    private void HandleUnsubscribeEvent(EventData eventData)
    {
      string[] channels = (string[]) eventData.get_Item((byte) 0);
      for (int index = 0; index < channels.Length; ++index)
      {
        string key = channels[index];
        this.PublicChannels.Remove(key);
        this.PublicChannelsUnsubscribing.Remove(key);
      }
      this.listener.OnUnsubscribed(channels);
    }

    private void HandleAuthResponse(OperationResponse operationResponse)
    {
      if (this.DebugOut >= 3)
        this.listener.DebugReturn((DebugLevel) 3, operationResponse.ToStringFull() + " on: " + this.chatPeer.NameServerAddress);
      if (operationResponse.ReturnCode == null)
      {
        if (this.State == ChatState.ConnectedToNameServer)
        {
          this.State = ChatState.Authenticated;
          this.listener.OnChatStateChange(this.State);
          if (((Dictionary<byte, object>) operationResponse.Parameters).ContainsKey((byte) 221))
          {
            if (this.AuthValues == null)
              this.AuthValues = new AuthenticationValues();
            this.AuthValues.Token = operationResponse.get_Item((byte) 221) as string;
            this.FrontendAddress = (string) operationResponse.get_Item((byte) 230);
            this.chatPeer.Disconnect();
          }
          else
          {
            if (this.DebugOut < 1)
              return;
            this.listener.DebugReturn((DebugLevel) 1, "No secret in authentication response.");
          }
        }
        else
        {
          if (this.State != ChatState.ConnectingToFrontEnd)
            return;
          this.msDeltaForServiceCalls *= 4;
          this.State = ChatState.ConnectedToFrontEnd;
          this.listener.OnChatStateChange(this.State);
          this.listener.OnConnected();
        }
      }
      else
      {
        switch ((short) operationResponse.ReturnCode)
        {
          case -3:
            this.DisconnectedCause = ChatDisconnectCause.OperationNotAllowedInCurrentState;
            break;
          case 32755:
            this.DisconnectedCause = ChatDisconnectCause.CustomAuthenticationFailed;
            break;
          case 32756:
            this.DisconnectedCause = ChatDisconnectCause.InvalidRegion;
            break;
          case 32757:
            this.DisconnectedCause = ChatDisconnectCause.MaxCcuReached;
            break;
          case short.MaxValue:
            this.DisconnectedCause = ChatDisconnectCause.InvalidAuthentication;
            break;
        }
        if (this.DebugOut >= 1)
          this.listener.DebugReturn((DebugLevel) 1, "Authentication request error: " + (object) (short) operationResponse.ReturnCode + ". Disconnecting.");
        this.State = ChatState.Disconnecting;
        this.chatPeer.Disconnect();
      }
    }

    private void HandleStatusUpdate(EventData eventData)
    {
      string parameter1 = (string) ((Dictionary<byte, object>) eventData.Parameters)[(byte) 5];
      int parameter2 = (int) ((Dictionary<byte, object>) eventData.Parameters)[(byte) 10];
      object message = (object) null;
      bool gotMessage = ((Dictionary<byte, object>) eventData.Parameters).ContainsKey((byte) 3);
      if (gotMessage)
        message = ((Dictionary<byte, object>) eventData.Parameters)[(byte) 3];
      this.listener.OnStatusUpdate(parameter1, parameter2, gotMessage, message);
    }

    private void ConnectToFrontEnd()
    {
      this.State = ChatState.ConnectingToFrontEnd;
      if (this.DebugOut >= 3)
        this.listener.DebugReturn((DebugLevel) 3, "Connecting to frontend " + this.FrontendAddress);
      this.chatPeer.Connect(this.FrontendAddress, "chat");
    }

    private bool AuthenticateOnFrontEnd()
    {
      if (this.AuthValues != null)
      {
        if (this.AuthValues.Token == null || this.AuthValues.Token == string.Empty)
        {
          if (this.DebugOut >= 1)
            this.listener.DebugReturn((DebugLevel) 1, "Can't authenticate on front end server. Secret is not set");
          return false;
        }
        return this.chatPeer.OpCustom((byte) 230, new Dictionary<byte, object>() { { (byte) 221, (object) this.AuthValues.Token } }, true);
      }
      if (this.DebugOut >= 1)
        this.listener.DebugReturn((DebugLevel) 1, "Can't authenticate on front end server. Authentication Values are not set");
      return false;
    }
  }
}
