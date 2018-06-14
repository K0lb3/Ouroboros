// Decompiled with JetBrains decompiler
// Type: PickupItemSyncer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Photon;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class PickupItemSyncer : MonoBehaviour
{
  private const float TimeDeltaToIgnore = 0.2f;
  public bool IsWaitingForPickupInit;

  public void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
  {
    if (!PhotonNetwork.isMasterClient)
      return;
    this.SendPickedUpItems(newPlayer);
  }

  public void OnJoinedRoom()
  {
    Debug.Log((object) ("Joined Room. isMasterClient: " + (object) PhotonNetwork.isMasterClient + " id: " + (object) PhotonNetwork.player.ID));
    this.IsWaitingForPickupInit = !PhotonNetwork.isMasterClient;
    if (PhotonNetwork.playerList.Length < 2)
      return;
    this.Invoke("AskForPickupItemSpawnTimes", 2f);
  }

  public void AskForPickupItemSpawnTimes()
  {
    if (!this.IsWaitingForPickupInit)
      return;
    if (PhotonNetwork.playerList.Length < 2)
    {
      Debug.Log((object) "Cant ask anyone else for PickupItem spawn times.");
      this.IsWaitingForPickupInit = false;
    }
    else
    {
      PhotonPlayer next = PhotonNetwork.masterClient.GetNext();
      if (next == null || next.Equals(PhotonNetwork.player))
        next = PhotonNetwork.player.GetNext();
      if (next != null && !next.Equals(PhotonNetwork.player))
      {
        this.photonView.RPC("RequestForPickupItems", next);
      }
      else
      {
        Debug.Log((object) "No player left to ask");
        this.IsWaitingForPickupInit = false;
      }
    }
  }

  [Obsolete("Use RequestForPickupItems(PhotonMessageInfo msgInfo) with corrected typing instead.")]
  [PunRPC]
  public void RequestForPickupTimes(PhotonMessageInfo msgInfo)
  {
    this.RequestForPickupItems(msgInfo);
  }

  [PunRPC]
  public void RequestForPickupItems(PhotonMessageInfo msgInfo)
  {
    if (msgInfo.sender == null)
      Debug.LogError((object) "Unknown player asked for PickupItems");
    else
      this.SendPickedUpItems(msgInfo.sender);
  }

  private void SendPickedUpItems(PhotonPlayer targetPlayer)
  {
    if (targetPlayer == null)
    {
      Debug.LogWarning((object) "Cant send PickupItem spawn times to unknown targetPlayer.");
    }
    else
    {
      double time = PhotonNetwork.time;
      double num1 = time + 0.200000002980232;
      PickupItem[] array = new PickupItem[PickupItem.DisabledPickupItems.Count];
      PickupItem.DisabledPickupItems.CopyTo(array);
      List<float> floatList = new List<float>(array.Length * 2);
      for (int index = 0; index < array.Length; ++index)
      {
        PickupItem pickupItem = array[index];
        if ((double) pickupItem.SecondsBeforeRespawn <= 0.0)
        {
          floatList.Add((float) pickupItem.ViewID);
          floatList.Add(0.0f);
        }
        else
        {
          double num2 = pickupItem.TimeOfRespawn - PhotonNetwork.time;
          if (pickupItem.TimeOfRespawn > num1)
          {
            Debug.Log((object) (pickupItem.ViewID.ToString() + " respawn: " + (object) pickupItem.TimeOfRespawn + " timeUntilRespawn: " + (object) num2 + " (now: " + (object) PhotonNetwork.time + ")"));
            floatList.Add((float) pickupItem.ViewID);
            floatList.Add((float) num2);
          }
        }
      }
      Debug.Log((object) ("Sent count: " + (object) floatList.Count + " now: " + (object) time));
      this.photonView.RPC("PickupItemInit", targetPlayer, (object) PhotonNetwork.time, (object) floatList.ToArray());
    }
  }

  [PunRPC]
  public void PickupItemInit(double timeBase, float[] inactivePickupsAndTimes)
  {
    this.IsWaitingForPickupInit = false;
    for (int index1 = 0; index1 < inactivePickupsAndTimes.Length / 2; ++index1)
    {
      int index2 = index1 * 2;
      int inactivePickupsAndTime1 = (int) inactivePickupsAndTimes[index2];
      float inactivePickupsAndTime2 = inactivePickupsAndTimes[index2 + 1];
      PhotonView photonView = PhotonView.Find(inactivePickupsAndTime1);
      PickupItem component = (PickupItem) ((Component) photonView).GetComponent<PickupItem>();
      if ((double) inactivePickupsAndTime2 <= 0.0)
      {
        component.PickedUp(0.0f);
      }
      else
      {
        double num1 = (double) inactivePickupsAndTime2 + timeBase;
        Debug.Log((object) (photonView.viewID.ToString() + " respawn: " + (object) num1 + " timeUntilRespawnBasedOnTimeBase:" + (object) inactivePickupsAndTime2 + " SecondsBeforeRespawn: " + (object) component.SecondsBeforeRespawn));
        double num2 = num1 - PhotonNetwork.time;
        if ((double) inactivePickupsAndTime2 <= 0.0)
          num2 = 0.0;
        component.PickedUp((float) num2);
      }
    }
  }
}
