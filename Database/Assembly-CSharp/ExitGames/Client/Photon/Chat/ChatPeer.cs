// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ChatPeer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExitGames.Client.Photon.Chat
{
  public class ChatPeer : PhotonPeer
  {
    public const string NameServerHost = "ns.exitgames.com";
    public const string NameServerHttp = "http://ns.exitgamescloud.com:80/photon/n";
    private static readonly Dictionary<ConnectionProtocol, int> ProtocolToNameServerPort;

    public ChatPeer(IPhotonPeerListener listener, ConnectionProtocol protocol)
    {
      base.\u002Ector(listener, protocol);
      this.ConfigUnitySockets();
    }

    static ChatPeer()
    {
      Dictionary<ConnectionProtocol, int> dictionary = new Dictionary<ConnectionProtocol, int>();
      dictionary.Add((ConnectionProtocol) 0, 5058);
      dictionary.Add((ConnectionProtocol) 1, 4533);
      dictionary.Add((ConnectionProtocol) 4, 9093);
      dictionary.Add((ConnectionProtocol) 5, 19093);
      ChatPeer.ProtocolToNameServerPort = dictionary;
    }

    public string NameServerAddress
    {
      get
      {
        return this.GetNameServerAddress();
      }
    }

    internal virtual bool IsProtocolSecure
    {
      get
      {
        return this.get_UsedProtocol() == 5;
      }
    }

    [Conditional("UNITY")]
    private void ConfigUnitySockets()
    {
      Type type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp", false);
      if ((object) type == null)
        type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp-firstpass", false);
      if ((object) type == null)
        return;
      ((Dictionary<ConnectionProtocol, Type>) this.SocketImplementationConfig)[(ConnectionProtocol) 4] = type;
      ((Dictionary<ConnectionProtocol, Type>) this.SocketImplementationConfig)[(ConnectionProtocol) 5] = type;
    }

    private string GetNameServerAddress()
    {
      int num = 0;
      ChatPeer.ProtocolToNameServerPort.TryGetValue(this.get_TransportProtocol(), out num);
      switch ((int) this.get_TransportProtocol())
      {
        case 0:
        case 1:
          return string.Format("{0}:{1}", (object) "ns.exitgames.com", (object) num);
        case 4:
          return string.Format("ws://{0}:{1}", (object) "ns.exitgames.com", (object) num);
        case 5:
          return string.Format("wss://{0}:{1}", (object) "ns.exitgames.com", (object) num);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public bool Connect()
    {
      if (this.DebugOut >= 3)
        this.get_Listener().DebugReturn((DebugLevel) 3, "Connecting to nameserver " + this.NameServerAddress);
      return this.Connect(this.NameServerAddress, "NameServer");
    }

    public bool AuthenticateOnNameServer(string appId, string appVersion, string region, AuthenticationValues authValues)
    {
      if (this.DebugOut >= 3)
        this.get_Listener().DebugReturn((DebugLevel) 3, "OpAuthenticate()");
      Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
      dictionary[(byte) 220] = (object) appVersion;
      dictionary[(byte) 224] = (object) appId;
      dictionary[(byte) 210] = (object) region;
      if (authValues != null)
      {
        if (!string.IsNullOrEmpty(authValues.UserId))
          dictionary[(byte) 225] = (object) authValues.UserId;
        if (authValues != null && authValues.AuthType != CustomAuthenticationType.None)
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
      return this.OpCustom((byte) 230, dictionary, true, (byte) 0, this.get_IsEncryptionAvailable());
    }
  }
}
