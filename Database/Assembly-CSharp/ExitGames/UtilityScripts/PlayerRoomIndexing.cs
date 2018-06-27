// Decompiled with JetBrains decompiler
// Type: ExitGames.UtilityScripts.PlayerRoomIndexing
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using Photon;
using System.Collections.Generic;
using UnityEngine;

namespace ExitGames.UtilityScripts
{
  public class PlayerRoomIndexing : PunBehaviour
  {
    public const string RoomPlayerIndexedProp = "PlayerIndexes";
    public static PlayerRoomIndexing instance;
    public PlayerRoomIndexing.RoomIndexingChanged OnRoomIndexingChanged;
    private int[] _playerIds;
    private object _indexes;
    private Dictionary<int, int> _indexesLUT;
    private List<bool> _indexesPool;
    private PhotonPlayer _p;

    public int[] PlayerIds
    {
      get
      {
        return this._playerIds;
      }
    }

    public void Awake()
    {
      if (Object.op_Inequality((Object) PlayerRoomIndexing.instance, (Object) null))
        Debug.LogError((object) "Existing instance of PlayerRoomIndexing found. Only One instance is required at the most. Please correct and have only one at any time.");
      PlayerRoomIndexing.instance = this;
    }

    public override void OnJoinedRoom()
    {
      if (PhotonNetwork.isMasterClient)
        this.AssignIndex(PhotonNetwork.player);
      else
        this.RefreshData();
    }

    public override void OnLeftRoom()
    {
      this.RefreshData();
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
      if (!PhotonNetwork.isMasterClient)
        return;
      this.AssignIndex(newPlayer);
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
      if (!PhotonNetwork.isMasterClient)
        return;
      this.UnAssignIndex(otherPlayer);
    }

    public override void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
    {
      if (!((Dictionary<object, object>) propertiesThatChanged).ContainsKey((object) "PlayerIndexes"))
        return;
      this.RefreshData();
    }

    public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
      if (!PhotonNetwork.isMasterClient)
        return;
      this.SanitizeIndexing();
    }

    public int GetRoomIndex(PhotonPlayer player)
    {
      if (this._indexesLUT != null && this._indexesLUT.ContainsKey(player.ID))
        return this._indexesLUT[player.ID];
      return -1;
    }

    private void SanitizeIndexing()
    {
      if (!PhotonNetwork.isMasterClient || PhotonNetwork.room == null)
        return;
      Dictionary<int, int> dictionary = new Dictionary<int, int>();
      if (((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).TryGetValue((object) "PlayerIndexes", out this._indexes))
        dictionary = this._indexes as Dictionary<int, int>;
      if (dictionary.Count == PhotonNetwork.room.PlayerCount)
        return;
      foreach (PhotonPlayer player in PhotonNetwork.playerList)
      {
        if (!dictionary.ContainsKey(player.ID))
          this.AssignIndex(player);
      }
    }

    private void RefreshData()
    {
      if (PhotonNetwork.room != null)
      {
        this._playerIds = new int[PhotonNetwork.room.MaxPlayers];
        if (((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).TryGetValue((object) "PlayerIndexes", out this._indexes))
        {
          this._indexesLUT = this._indexes as Dictionary<int, int>;
          using (Dictionary<int, int>.Enumerator enumerator = this._indexesLUT.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              KeyValuePair<int, int> current = enumerator.Current;
              this._p = PhotonPlayer.Find(current.Key);
              this._playerIds[current.Value] = this._p.ID;
            }
          }
        }
      }
      else
        this._playerIds = new int[0];
      if (this.OnRoomIndexingChanged == null)
        return;
      this.OnRoomIndexingChanged();
    }

    private void AssignIndex(PhotonPlayer player)
    {
      this._indexesLUT = !((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).TryGetValue((object) "PlayerIndexes", out this._indexes) ? new Dictionary<int, int>() : this._indexes as Dictionary<int, int>;
      List<bool> boolList = new List<bool>((IEnumerable<bool>) new bool[PhotonNetwork.room.MaxPlayers]);
      using (Dictionary<int, int>.Enumerator enumerator = this._indexesLUT.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<int, int> current = enumerator.Current;
          boolList[current.Value] = true;
        }
      }
      this._indexesLUT[player.ID] = Mathf.Max(0, boolList.IndexOf(false));
      Room room = PhotonNetwork.room;
      Hashtable hashtable = new Hashtable();
      ((Dictionary<object, object>) hashtable).Add((object) "PlayerIndexes", (object) this._indexesLUT);
      Hashtable propertiesToSet = hashtable;
      // ISSUE: variable of the null type
      __Null local = null;
      int num = 0;
      room.SetCustomProperties(propertiesToSet, (Hashtable) local, num != 0);
      this.RefreshData();
    }

    private void UnAssignIndex(PhotonPlayer player)
    {
      if (((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).TryGetValue((object) "PlayerIndexes", out this._indexes))
      {
        this._indexesLUT = this._indexes as Dictionary<int, int>;
        this._indexesLUT.Remove(player.ID);
        Room room = PhotonNetwork.room;
        Hashtable hashtable = new Hashtable();
        ((Dictionary<object, object>) hashtable).Add((object) "PlayerIndexes", (object) this._indexesLUT);
        Hashtable propertiesToSet = hashtable;
        // ISSUE: variable of the null type
        __Null local = null;
        int num = 0;
        room.SetCustomProperties(propertiesToSet, (Hashtable) local, num != 0);
      }
      this.RefreshData();
    }

    public delegate void RoomIndexingChanged();
  }
}
