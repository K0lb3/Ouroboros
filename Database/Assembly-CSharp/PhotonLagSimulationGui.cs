// Decompiled with JetBrains decompiler
// Type: PhotonLagSimulationGui
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using UnityEngine;

public class PhotonLagSimulationGui : MonoBehaviour
{
  public Rect WindowRect;
  public int WindowId;
  public bool Visible;

  public PhotonLagSimulationGui()
  {
    base.\u002Ector();
  }

  public PhotonPeer Peer { get; set; }

  public void Start()
  {
    this.Peer = (PhotonPeer) PhotonNetwork.networkingPeer;
  }

  public void OnGUI()
  {
    if (!this.Visible)
      return;
    if (this.Peer == null)
    {
      // ISSUE: method pointer
      this.WindowRect = GUILayout.Window(this.WindowId, this.WindowRect, new GUI.WindowFunction((object) this, __methodptr(NetSimHasNoPeerWindow)), "Netw. Sim.", new GUILayoutOption[0]);
    }
    else
    {
      // ISSUE: method pointer
      this.WindowRect = GUILayout.Window(this.WindowId, this.WindowRect, new GUI.WindowFunction((object) this, __methodptr(NetSimWindow)), "Netw. Sim.", new GUILayoutOption[0]);
    }
  }

  private void NetSimHasNoPeerWindow(int windowId)
  {
    GUILayout.Label("No peer to communicate with. ", new GUILayoutOption[0]);
  }

  private void NetSimWindow(int windowId)
  {
    GUILayout.Label(string.Format("Rtt:{0,4} +/-{1,3}", (object) this.Peer.get_RoundTripTime(), (object) this.Peer.get_RoundTripTimeVariance()), new GUILayoutOption[0]);
    bool simulationEnabled = this.Peer.get_IsSimulationEnabled();
    bool flag = GUILayout.Toggle(simulationEnabled, "Simulate", new GUILayoutOption[0]);
    if (flag != simulationEnabled)
      this.Peer.set_IsSimulationEnabled(flag);
    float incomingLag = (float) this.Peer.get_NetworkSimulationSettings().get_IncomingLag();
    GUILayout.Label("Lag " + (object) incomingLag, new GUILayoutOption[0]);
    float num1 = GUILayout.HorizontalSlider(incomingLag, 0.0f, 500f, new GUILayoutOption[0]);
    this.Peer.get_NetworkSimulationSettings().set_IncomingLag((int) num1);
    this.Peer.get_NetworkSimulationSettings().set_OutgoingLag((int) num1);
    float incomingJitter = (float) this.Peer.get_NetworkSimulationSettings().get_IncomingJitter();
    GUILayout.Label("Jit " + (object) incomingJitter, new GUILayoutOption[0]);
    float num2 = GUILayout.HorizontalSlider(incomingJitter, 0.0f, 100f, new GUILayoutOption[0]);
    this.Peer.get_NetworkSimulationSettings().set_IncomingJitter((int) num2);
    this.Peer.get_NetworkSimulationSettings().set_OutgoingJitter((int) num2);
    float incomingLossPercentage = (float) this.Peer.get_NetworkSimulationSettings().get_IncomingLossPercentage();
    GUILayout.Label("Loss " + (object) incomingLossPercentage, new GUILayoutOption[0]);
    float num3 = GUILayout.HorizontalSlider(incomingLossPercentage, 0.0f, 10f, new GUILayoutOption[0]);
    this.Peer.get_NetworkSimulationSettings().set_IncomingLossPercentage((int) num3);
    this.Peer.get_NetworkSimulationSettings().set_OutgoingLossPercentage((int) num3);
    if (GUI.get_changed())
    {
      // ISSUE: explicit reference operation
      ((Rect) @this.WindowRect).set_height(100f);
    }
    GUI.DragWindow();
  }
}
