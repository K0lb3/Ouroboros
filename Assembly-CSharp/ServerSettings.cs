// Decompiled with JetBrains decompiler
// Type: ServerSettings
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ServerSettings : ScriptableObject
{
  public string AppID;
  public string VoiceAppID;
  public string ChatAppID;
  public ServerSettings.HostingOption HostType;
  public CloudRegionCode PreferredRegion;
  public CloudRegionFlag EnabledRegions;
  public ConnectionProtocol Protocol;
  public string ServerAddress;
  public int ServerPort;
  public int VoiceServerPort;
  public bool JoinLobby;
  public bool EnableLobbyStatistics;
  public PhotonLogLevel PunLogging;
  public DebugLevel NetworkLogging;
  public bool RunInBackground;
  public List<string> RpcList;
  [HideInInspector]
  public bool DisableAutoOpenWizard;

  public ServerSettings()
  {
    base.\u002Ector();
  }

  public void UseCloudBestRegion(string cloudAppid)
  {
    this.HostType = ServerSettings.HostingOption.BestRegion;
    this.AppID = cloudAppid;
  }

  public void UseCloud(string cloudAppid)
  {
    this.HostType = ServerSettings.HostingOption.PhotonCloud;
    this.AppID = cloudAppid;
  }

  public void UseCloud(string cloudAppid, CloudRegionCode code)
  {
    this.HostType = ServerSettings.HostingOption.PhotonCloud;
    this.AppID = cloudAppid;
    this.PreferredRegion = code;
  }

  public void UseMyServer(string serverAddress, int serverPort, string application)
  {
    this.HostType = ServerSettings.HostingOption.SelfHosted;
    this.AppID = application == null ? "master" : application;
    this.ServerAddress = serverAddress;
    this.ServerPort = serverPort;
  }

  public static bool IsAppId(string val)
  {
    try
    {
      Guid guid = new Guid(val);
    }
    catch
    {
      return false;
    }
    return true;
  }

  public virtual string ToString()
  {
    return "ServerSettings: " + (object) this.HostType + " " + this.ServerAddress;
  }

  public enum HostingOption
  {
    NotSet,
    PhotonCloud,
    SelfHosted,
    OfflineMode,
    BestRegion,
  }
}
