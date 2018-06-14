// Decompiled with JetBrains decompiler
// Type: InRoomTime
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class InRoomTime : MonoBehaviour
{
  private const string StartTimeKey = "#rt";
  private int roomStartTimestamp;

  public InRoomTime()
  {
    base.\u002Ector();
  }

  public double RoomTime
  {
    get
    {
      return (double) (uint) this.RoomTimestamp / 1000.0;
    }
  }

  public int RoomTimestamp
  {
    get
    {
      if (PhotonNetwork.inRoom)
        return PhotonNetwork.ServerTimestamp - this.roomStartTimestamp;
      return 0;
    }
  }

  public bool IsRoomTimeSet
  {
    get
    {
      if (PhotonNetwork.inRoom)
        return ((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).ContainsKey((object) "#rt");
      return false;
    }
  }

  [DebuggerHidden]
  internal IEnumerator SetRoomStartTimestamp()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new InRoomTime.\u003CSetRoomStartTimestamp\u003Ec__IteratorA() { \u003C\u003Ef__this = this };
  }

  public void OnJoinedRoom()
  {
    this.StartCoroutine("SetRoomStartTimestamp");
  }

  public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
  {
    this.StartCoroutine("SetRoomStartTimestamp");
  }

  public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
  {
    if (!((Dictionary<object, object>) propertiesThatChanged).ContainsKey((object) "#rt"))
      return;
    this.roomStartTimestamp = (int) propertiesThatChanged.get_Item((object) "#rt");
  }
}
