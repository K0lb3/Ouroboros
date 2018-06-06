// Decompiled with JetBrains decompiler
// Type: InRoomRoundTimer
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System.Collections.Generic;
using UnityEngine;

public class InRoomRoundTimer : MonoBehaviour
{
  private const string StartTimeKey = "st";
  public int SecondsPerTurn;
  public double StartTime;
  public Rect TextPos;
  private bool startRoundWhenTimeIsSynced;

  public InRoomRoundTimer()
  {
    base.\u002Ector();
  }

  private void StartRoundNow()
  {
    if (PhotonNetwork.time < 9.99999974737875E-05)
    {
      this.startRoundWhenTimeIsSynced = true;
    }
    else
    {
      this.startRoundWhenTimeIsSynced = false;
      Hashtable propertiesToSet = new Hashtable();
      propertiesToSet.set_Item((object) "st", (object) PhotonNetwork.time);
      PhotonNetwork.room.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
    }
  }

  public void OnJoinedRoom()
  {
    if (PhotonNetwork.isMasterClient)
      this.StartRoundNow();
    else
      Debug.Log((object) ("StartTime already set: " + (object) ((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).ContainsKey((object) "st")));
  }

  public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
  {
    if (!((Dictionary<object, object>) propertiesThatChanged).ContainsKey((object) "st"))
      return;
    this.StartTime = (double) propertiesThatChanged.get_Item((object) "st");
  }

  public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
  {
    if (((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).ContainsKey((object) "st"))
      return;
    Debug.Log((object) "The new master starts a new round, cause we didn't start yet.");
    this.StartRoundNow();
  }

  private void Update()
  {
    if (!this.startRoundWhenTimeIsSynced)
      return;
    this.StartRoundNow();
  }

  public void OnGUI()
  {
    double num1 = PhotonNetwork.time - this.StartTime;
    double num2 = (double) this.SecondsPerTurn - num1 % (double) this.SecondsPerTurn;
    int num3 = (int) (num1 / (double) this.SecondsPerTurn);
    GUILayout.BeginArea(this.TextPos);
    GUILayout.Label(string.Format("elapsed: {0:0.000}", (object) num1), new GUILayoutOption[0]);
    GUILayout.Label(string.Format("remaining: {0:0.000}", (object) num2), new GUILayoutOption[0]);
    GUILayout.Label(string.Format("turn: {0:0}", (object) num3), new GUILayoutOption[0]);
    if (GUILayout.Button("new round", new GUILayoutOption[0]))
      this.StartRoundNow();
    GUILayout.EndArea();
  }
}
