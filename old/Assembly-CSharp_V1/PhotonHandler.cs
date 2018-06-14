// Decompiled with JetBrains decompiler
// Type: PhotonHandler
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

internal class PhotonHandler : MonoBehaviour
{
  internal static CloudRegionCode BestRegionCodeCurrently = CloudRegionCode.none;
  private const string PlayerPrefsKey = "PUNCloudBestRegion";
  public static PhotonHandler SP;
  public int updateInterval;
  public int updateIntervalOnSerialize;
  private int nextSendTickCount;
  private int nextSendTickCountOnSerialize;
  private static bool sendThreadShouldRun;
  private static Stopwatch timerToStopConnectionInBackground;
  protected internal static bool AppQuits;
  protected internal static System.Type PingImplementation;

  public PhotonHandler()
  {
    base.\u002Ector();
  }

  protected void Awake()
  {
    if (Object.op_Inequality((Object) PhotonHandler.SP, (Object) null) && Object.op_Inequality((Object) PhotonHandler.SP, (Object) this) && Object.op_Inequality((Object) ((Component) PhotonHandler.SP).get_gameObject(), (Object) null))
      Object.DestroyImmediate((Object) ((Component) PhotonHandler.SP).get_gameObject());
    PhotonHandler.SP = this;
    Object.DontDestroyOnLoad((Object) ((Component) this).get_gameObject());
    this.updateInterval = 1000 / PhotonNetwork.sendRate;
    this.updateIntervalOnSerialize = 1000 / PhotonNetwork.sendRateOnSerialize;
    PhotonHandler.StartFallbackSendAckThread();
  }

  protected void OnLevelWasLoaded(int level)
  {
    PhotonNetwork.networkingPeer.NewSceneLoaded();
    PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced((object) SceneManagerHelper.ActiveSceneName);
  }

  protected void OnApplicationQuit()
  {
    PhotonHandler.AppQuits = true;
    PhotonHandler.StopFallbackSendAckThread();
    PhotonNetwork.Disconnect();
  }

  protected void OnApplicationPause(bool pause)
  {
    if ((double) PhotonNetwork.BackgroundTimeout <= 0.100000001490116)
      return;
    if (PhotonHandler.timerToStopConnectionInBackground == null)
      PhotonHandler.timerToStopConnectionInBackground = new Stopwatch();
    PhotonHandler.timerToStopConnectionInBackground.Reset();
    if (pause)
      PhotonHandler.timerToStopConnectionInBackground.Start();
    else
      PhotonHandler.timerToStopConnectionInBackground.Stop();
  }

  protected void OnDestroy()
  {
    PhotonHandler.StopFallbackSendAckThread();
  }

  protected void Update()
  {
    if (PhotonNetwork.networkingPeer == null)
    {
      Debug.LogError((object) "NetworkPeer broke!");
    }
    else
    {
      if (PhotonNetwork.connectionStateDetailed == ClientState.PeerCreated || PhotonNetwork.connectionStateDetailed == ClientState.Disconnected || (PhotonNetwork.offlineMode || !PhotonNetwork.isMessageQueueRunning))
        return;
      bool flag1 = true;
      while (PhotonNetwork.isMessageQueueRunning && flag1)
        flag1 = PhotonNetwork.networkingPeer.DispatchIncomingCommands();
      int num1 = (int) ((double) Time.get_realtimeSinceStartup() * 1000.0);
      if (PhotonNetwork.isMessageQueueRunning && num1 > this.nextSendTickCountOnSerialize)
      {
        PhotonNetwork.networkingPeer.RunViewUpdate();
        this.nextSendTickCountOnSerialize = num1 + this.updateIntervalOnSerialize;
        this.nextSendTickCount = 0;
      }
      int num2 = (int) ((double) Time.get_realtimeSinceStartup() * 1000.0);
      if (num2 <= this.nextSendTickCount)
        return;
      bool flag2 = true;
      while (PhotonNetwork.isMessageQueueRunning && flag2)
        flag2 = PhotonNetwork.networkingPeer.SendOutgoingCommands();
      this.nextSendTickCount = num2 + this.updateInterval;
    }
  }

  protected void OnJoinedRoom()
  {
    PhotonNetwork.networkingPeer.LoadLevelIfSynced();
  }

  protected void OnCreatedRoom()
  {
    PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced((object) SceneManagerHelper.ActiveSceneName);
  }

  public static void StartFallbackSendAckThread()
  {
    if (PhotonHandler.sendThreadShouldRun)
      return;
    PhotonHandler.sendThreadShouldRun = true;
    SupportClass.CallInBackground(new Func<bool>(PhotonHandler.FallbackSendAckThread));
  }

  public static void StopFallbackSendAckThread()
  {
    PhotonHandler.sendThreadShouldRun = false;
  }

  public static bool FallbackSendAckThread()
  {
    if (PhotonHandler.sendThreadShouldRun && PhotonNetwork.networkingPeer != null)
    {
      if (PhotonHandler.timerToStopConnectionInBackground != null && (double) PhotonNetwork.BackgroundTimeout > 0.100000001490116 && (double) PhotonHandler.timerToStopConnectionInBackground.ElapsedMilliseconds > (double) PhotonNetwork.BackgroundTimeout * 1000.0)
      {
        if (PhotonNetwork.connected)
          PhotonNetwork.Disconnect();
        PhotonHandler.timerToStopConnectionInBackground.Stop();
        PhotonHandler.timerToStopConnectionInBackground.Reset();
        return PhotonHandler.sendThreadShouldRun;
      }
      if (PhotonNetwork.networkingPeer.get_ConnectionTime() - PhotonNetwork.networkingPeer.get_LastSendOutgoingTime() > 200)
        PhotonNetwork.networkingPeer.SendAcksOnly();
    }
    return PhotonHandler.sendThreadShouldRun;
  }

  internal static CloudRegionCode BestRegionCodeInPreferences
  {
    get
    {
      string codeAsString = PlayerPrefs.GetString("PUNCloudBestRegion", string.Empty);
      if (!string.IsNullOrEmpty(codeAsString))
        return Region.Parse(codeAsString);
      return CloudRegionCode.none;
    }
    set
    {
      if (value == CloudRegionCode.none)
        PlayerPrefs.DeleteKey("PUNCloudBestRegion");
      else
        PlayerPrefs.SetString("PUNCloudBestRegion", value.ToString());
    }
  }

  protected internal static void PingAvailableRegionsAndConnectToBest()
  {
    PhotonHandler.SP.StartCoroutine(PhotonHandler.SP.PingAvailableRegionsCoroutine(true));
  }

  [DebuggerHidden]
  internal IEnumerator PingAvailableRegionsCoroutine(bool connectToBest)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new PhotonHandler.\u003CPingAvailableRegionsCoroutine\u003Ec__Iterator8() { connectToBest = connectToBest, \u003C\u0024\u003EconnectToBest = connectToBest };
  }
}
