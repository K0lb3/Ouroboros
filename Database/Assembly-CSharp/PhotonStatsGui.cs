// Decompiled with JetBrains decompiler
// Type: PhotonStatsGui
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using UnityEngine;

public class PhotonStatsGui : MonoBehaviour
{
  public bool statsWindowOn;
  public bool statsOn;
  public bool healthStatsVisible;
  public bool trafficStatsOn;
  public bool buttonsOn;
  public Rect statsRect;
  public int WindowId;

  public PhotonStatsGui()
  {
    base.\u002Ector();
  }

  public void Start()
  {
    // ISSUE: explicit reference operation
    if ((double) ((Rect) @this.statsRect).get_x() > 0.0)
      return;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    ((Rect) @this.statsRect).set_x((float) Screen.get_width() - ((Rect) @this.statsRect).get_width());
  }

  public void Update()
  {
    if (!Input.GetKeyDown((KeyCode) 9) || !Input.GetKey((KeyCode) 304))
      return;
    this.statsWindowOn = !this.statsWindowOn;
    this.statsOn = true;
  }

  public void OnGUI()
  {
    if (PhotonNetwork.networkingPeer.get_TrafficStatsEnabled() != this.statsOn)
      PhotonNetwork.networkingPeer.set_TrafficStatsEnabled(this.statsOn);
    if (!this.statsWindowOn)
      return;
    // ISSUE: method pointer
    this.statsRect = GUILayout.Window(this.WindowId, this.statsRect, new GUI.WindowFunction((object) this, __methodptr(TrafficStatsWindow)), "Messages (shift+tab)", new GUILayoutOption[0]);
  }

  public void TrafficStatsWindow(int windowID)
  {
    bool flag = false;
    TrafficStatsGameLevel trafficStatsGameLevel = PhotonNetwork.networkingPeer.get_TrafficStatsGameLevel();
    long num = PhotonNetwork.networkingPeer.get_TrafficStatsElapsedMs() / 1000L;
    if (num == 0L)
      num = 1L;
    GUILayout.BeginHorizontal(new GUILayoutOption[0]);
    this.buttonsOn = GUILayout.Toggle(this.buttonsOn, "buttons", new GUILayoutOption[0]);
    this.healthStatsVisible = GUILayout.Toggle(this.healthStatsVisible, "health", new GUILayoutOption[0]);
    this.trafficStatsOn = GUILayout.Toggle(this.trafficStatsOn, "traffic", new GUILayoutOption[0]);
    GUILayout.EndHorizontal();
    string str1 = string.Format("Out {0,4} | In {1,4} | Sum {2,4}", (object) trafficStatsGameLevel.get_TotalOutgoingMessageCount(), (object) trafficStatsGameLevel.get_TotalIncomingMessageCount(), (object) trafficStatsGameLevel.get_TotalMessageCount());
    string str2 = string.Format("{0}sec average:", (object) num);
    string str3 = string.Format("Out {0,4} | In {1,4} | Sum {2,4}", (object) ((long) trafficStatsGameLevel.get_TotalOutgoingMessageCount() / num), (object) ((long) trafficStatsGameLevel.get_TotalIncomingMessageCount() / num), (object) ((long) trafficStatsGameLevel.get_TotalMessageCount() / num));
    GUILayout.Label(str1, new GUILayoutOption[0]);
    GUILayout.Label(str2, new GUILayoutOption[0]);
    GUILayout.Label(str3, new GUILayoutOption[0]);
    if (this.buttonsOn)
    {
      GUILayout.BeginHorizontal(new GUILayoutOption[0]);
      this.statsOn = GUILayout.Toggle(this.statsOn, "stats on", new GUILayoutOption[0]);
      if (GUILayout.Button("Reset", new GUILayoutOption[0]))
      {
        PhotonNetwork.networkingPeer.TrafficStatsReset();
        PhotonNetwork.networkingPeer.set_TrafficStatsEnabled(true);
      }
      flag = GUILayout.Button("To Log", new GUILayoutOption[0]);
      GUILayout.EndHorizontal();
    }
    string str4 = string.Empty;
    string str5 = string.Empty;
    if (this.trafficStatsOn)
    {
      GUILayout.Box("Traffic Stats", new GUILayoutOption[0]);
      str4 = "Incoming: \n" + PhotonNetwork.networkingPeer.get_TrafficStatsIncoming().ToString();
      str5 = "Outgoing: \n" + PhotonNetwork.networkingPeer.get_TrafficStatsOutgoing().ToString();
      GUILayout.Label(str4, new GUILayoutOption[0]);
      GUILayout.Label(str5, new GUILayoutOption[0]);
    }
    string str6 = string.Empty;
    if (this.healthStatsVisible)
    {
      GUILayout.Box("Health Stats", new GUILayoutOption[0]);
      str6 = string.Format("ping: {6}[+/-{7}]ms resent:{8} \n\nmax ms between\nsend: {0,4} \ndispatch: {1,4} \n\nlongest dispatch for: \nev({3}):{2,3}ms \nop({5}):{4,3}ms", (object) trafficStatsGameLevel.get_LongestDeltaBetweenSending(), (object) trafficStatsGameLevel.get_LongestDeltaBetweenDispatching(), (object) trafficStatsGameLevel.get_LongestEventCallback(), (object) trafficStatsGameLevel.get_LongestEventCallbackCode(), (object) trafficStatsGameLevel.get_LongestOpResponseCallback(), (object) trafficStatsGameLevel.get_LongestOpResponseCallbackOpCode(), (object) PhotonNetwork.networkingPeer.get_RoundTripTime(), (object) PhotonNetwork.networkingPeer.get_RoundTripTimeVariance(), (object) PhotonNetwork.networkingPeer.get_ResentReliableCommands());
      GUILayout.Label(str6, new GUILayoutOption[0]);
    }
    if (flag)
      Debug.Log((object) string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}", (object) str1, (object) str2, (object) str3, (object) str4, (object) str5, (object) str6));
    if (GUI.get_changed())
    {
      // ISSUE: explicit reference operation
      ((Rect) @this.statsRect).set_height(100f);
    }
    GUI.DragWindow();
  }
}
