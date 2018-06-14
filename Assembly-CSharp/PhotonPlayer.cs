// Decompiled with JetBrains decompiler
// Type: PhotonPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonPlayer : IComparable<PhotonPlayer>, IComparable<int>, IEquatable<PhotonPlayer>, IEquatable<int>
{
  private int actorID = -1;
  private string nameField = string.Empty;
  public readonly bool IsLocal;
  public object TagObject;

  public PhotonPlayer(bool isLocal, int actorID, string name)
  {
    this.CustomProperties = new Hashtable();
    this.IsLocal = isLocal;
    this.actorID = actorID;
    this.nameField = name;
  }

  protected internal PhotonPlayer(bool isLocal, int actorID, Hashtable properties)
  {
    this.CustomProperties = new Hashtable();
    this.IsLocal = isLocal;
    this.actorID = actorID;
    this.InternalCacheProperties(properties);
  }

  public int ID
  {
    get
    {
      return this.actorID;
    }
  }

  public string NickName
  {
    get
    {
      return this.nameField;
    }
    set
    {
      if (!this.IsLocal)
      {
        Debug.LogError((object) "Error: Cannot change the name of a remote player!");
      }
      else
      {
        if (string.IsNullOrEmpty(value) || value.Equals(this.nameField))
          return;
        this.nameField = value;
        PhotonNetwork.playerName = value;
      }
    }
  }

  public string UserId { get; internal set; }

  public bool IsMasterClient
  {
    get
    {
      return PhotonNetwork.networkingPeer.mMasterClientId == this.ID;
    }
  }

  public bool IsInactive { get; set; }

  public Hashtable CustomProperties { get; internal set; }

  public Hashtable AllProperties
  {
    get
    {
      Hashtable hashtable = new Hashtable();
      ((IDictionary) hashtable).Merge((IDictionary) this.CustomProperties);
      hashtable.set_Item((object) byte.MaxValue, (object) this.NickName);
      return hashtable;
    }
  }

  public override bool Equals(object p)
  {
    PhotonPlayer photonPlayer = p as PhotonPlayer;
    if (photonPlayer != null)
      return this.GetHashCode() == photonPlayer.GetHashCode();
    return false;
  }

  public override int GetHashCode()
  {
    return this.ID;
  }

  internal void InternalChangeLocalID(int newID)
  {
    if (!this.IsLocal)
      Debug.LogError((object) "ERROR You should never change PhotonPlayer IDs!");
    else
      this.actorID = newID;
  }

  internal void InternalCacheProperties(Hashtable properties)
  {
    if (properties == null || ((Dictionary<object, object>) properties).Count == 0 || ((object) this.CustomProperties).Equals((object) properties))
      return;
    if (((Dictionary<object, object>) properties).ContainsKey((object) byte.MaxValue))
      this.nameField = (string) properties.get_Item((object) byte.MaxValue);
    if (((Dictionary<object, object>) properties).ContainsKey((object) (byte) 253))
      this.UserId = (string) properties.get_Item((object) (byte) 253);
    if (((Dictionary<object, object>) properties).ContainsKey((object) (byte) 254))
      this.IsInactive = (bool) properties.get_Item((object) (byte) 254);
    ((IDictionary) this.CustomProperties).MergeStringKeys((IDictionary) properties);
    ((IDictionary) this.CustomProperties).StripKeysWithNullValues();
  }

  public void SetCustomProperties(Hashtable propertiesToSet, Hashtable expectedValues = null, bool webForward = false)
  {
    if (propertiesToSet == null)
      return;
    Hashtable stringKeys1 = ((IDictionary) propertiesToSet).StripToStringKeys();
    Hashtable stringKeys2 = ((IDictionary) expectedValues).StripToStringKeys();
    bool flag1 = stringKeys2 == null || ((Dictionary<object, object>) stringKeys2).Count == 0;
    bool flag2 = this.actorID > 0 && !PhotonNetwork.offlineMode;
    if (flag1)
    {
      ((IDictionary) this.CustomProperties).Merge((IDictionary) stringKeys1);
      ((IDictionary) this.CustomProperties).StripKeysWithNullValues();
    }
    if (flag2)
      PhotonNetwork.networkingPeer.OpSetPropertiesOfActor(this.actorID, stringKeys1, stringKeys2, webForward);
    if (flag2 && !flag1)
      return;
    this.InternalCacheProperties(stringKeys1);
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, (object) this, (object) stringKeys1);
  }

  public static PhotonPlayer Find(int ID)
  {
    if (PhotonNetwork.networkingPeer != null)
      return PhotonNetwork.networkingPeer.GetPlayerWithId(ID);
    return (PhotonPlayer) null;
  }

  public PhotonPlayer Get(int id)
  {
    return PhotonPlayer.Find(id);
  }

  public PhotonPlayer GetNext()
  {
    return this.GetNextFor(this.ID);
  }

  public PhotonPlayer GetNextFor(PhotonPlayer currentPlayer)
  {
    if (currentPlayer == null)
      return (PhotonPlayer) null;
    return this.GetNextFor(currentPlayer.ID);
  }

  public PhotonPlayer GetNextFor(int currentPlayerId)
  {
    if (PhotonNetwork.networkingPeer == null || PhotonNetwork.networkingPeer.mActors == null || PhotonNetwork.networkingPeer.mActors.Count < 2)
      return (PhotonPlayer) null;
    Dictionary<int, PhotonPlayer> mActors = PhotonNetwork.networkingPeer.mActors;
    int index1 = int.MaxValue;
    int index2 = currentPlayerId;
    using (Dictionary<int, PhotonPlayer>.KeyCollection.Enumerator enumerator = mActors.Keys.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        int current = enumerator.Current;
        if (current < index2)
          index2 = current;
        else if (current > currentPlayerId && current < index1)
          index1 = current;
      }
    }
    if (index1 != int.MaxValue)
      return mActors[index1];
    return mActors[index2];
  }

  public int CompareTo(PhotonPlayer other)
  {
    if (other == null)
      return 0;
    return this.GetHashCode().CompareTo(other.GetHashCode());
  }

  public int CompareTo(int other)
  {
    return this.GetHashCode().CompareTo(other);
  }

  public bool Equals(PhotonPlayer other)
  {
    if (other == null)
      return false;
    return this.GetHashCode().Equals(other.GetHashCode());
  }

  public bool Equals(int other)
  {
    return this.GetHashCode().Equals(other);
  }

  public override string ToString()
  {
    if (string.IsNullOrEmpty(this.NickName))
      return string.Format("#{0:00}{1}{2}", (object) this.ID, !this.IsInactive ? (object) " " : (object) " (inactive)", !this.IsMasterClient ? (object) string.Empty : (object) "(master)");
    return string.Format("'{0}'{1}{2}", (object) this.NickName, !this.IsInactive ? (object) " " : (object) " (inactive)", !this.IsMasterClient ? (object) string.Empty : (object) "(master)");
  }

  public string ToStringFull()
  {
    return string.Format("#{0:00} '{1}'{2} {3}", new object[4]
    {
      (object) this.ID,
      (object) this.NickName,
      !this.IsInactive ? (object) string.Empty : (object) " (inactive)",
      (object) ((IDictionary) this.CustomProperties).ToStringFull()
    });
  }

  [Obsolete("Please use NickName (updated case for naming).")]
  public string name
  {
    get
    {
      return this.NickName;
    }
    set
    {
      this.NickName = value;
    }
  }

  [Obsolete("Please use UserId (updated case for naming).")]
  public string userId
  {
    get
    {
      return this.UserId;
    }
    internal set
    {
      this.UserId = value;
    }
  }

  [Obsolete("Please use IsLocal (updated case for naming).")]
  public bool isLocal
  {
    get
    {
      return this.IsLocal;
    }
  }

  [Obsolete("Please use IsMasterClient (updated case for naming).")]
  public bool isMasterClient
  {
    get
    {
      return this.IsMasterClient;
    }
  }

  [Obsolete("Please use IsInactive (updated case for naming).")]
  public bool isInactive
  {
    get
    {
      return this.IsInactive;
    }
    set
    {
      this.IsInactive = value;
    }
  }

  [Obsolete("Please use CustomProperties (updated case for naming).")]
  public Hashtable customProperties
  {
    get
    {
      return this.CustomProperties;
    }
    internal set
    {
      this.CustomProperties = value;
    }
  }

  [Obsolete("Please use AllProperties (updated case for naming).")]
  public Hashtable allProperties
  {
    get
    {
      return this.AllProperties;
    }
  }
}
