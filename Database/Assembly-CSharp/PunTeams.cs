// Decompiled with JetBrains decompiler
// Type: PunTeams
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class PunTeams : MonoBehaviour
{
  public const string TeamPlayerProp = "team";
  public static Dictionary<PunTeams.Team, List<PhotonPlayer>> PlayersPerTeam;

  public PunTeams()
  {
    base.\u002Ector();
  }

  public void Start()
  {
    PunTeams.PlayersPerTeam = new Dictionary<PunTeams.Team, List<PhotonPlayer>>();
    foreach (object obj in Enum.GetValues(typeof (PunTeams.Team)))
      PunTeams.PlayersPerTeam[(PunTeams.Team) obj] = new List<PhotonPlayer>();
  }

  public void OnDisable()
  {
    PunTeams.PlayersPerTeam = new Dictionary<PunTeams.Team, List<PhotonPlayer>>();
  }

  public void OnJoinedRoom()
  {
    this.UpdateTeams();
  }

  public void OnLeftRoom()
  {
    this.Start();
  }

  public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
  {
    this.UpdateTeams();
  }

  public void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
  {
    this.UpdateTeams();
  }

  public void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
  {
    this.UpdateTeams();
  }

  public void UpdateTeams()
  {
    foreach (object obj in Enum.GetValues(typeof (PunTeams.Team)))
      PunTeams.PlayersPerTeam[(PunTeams.Team) obj].Clear();
    for (int index = 0; index < PhotonNetwork.playerList.Length; ++index)
    {
      PhotonPlayer player = PhotonNetwork.playerList[index];
      PunTeams.Team team = player.GetTeam();
      PunTeams.PlayersPerTeam[team].Add(player);
    }
  }

  public enum Team : byte
  {
    none,
    red,
    blue,
  }
}
