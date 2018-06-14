// Decompiled with JetBrains decompiler
// Type: RoomOptions
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System;

public class RoomOptions
{
  private bool isVisibleField = true;
  private bool isOpenField = true;
  private bool cleanupCacheOnLeaveField = PhotonNetwork.autoCleanUpPlayerObjects;
  public string[] CustomRoomPropertiesForLobby = new string[0];
  public byte MaxPlayers;
  public int PlayerTtl;
  public int EmptyRoomTtl;
  public Hashtable CustomRoomProperties;
  public string[] Plugins;
  private bool suppressRoomEventsField;
  private bool publishUserIdField;

  public bool IsVisible
  {
    get
    {
      return this.isVisibleField;
    }
    set
    {
      this.isVisibleField = value;
    }
  }

  public bool IsOpen
  {
    get
    {
      return this.isOpenField;
    }
    set
    {
      this.isOpenField = value;
    }
  }

  public bool CleanupCacheOnLeave
  {
    get
    {
      return this.cleanupCacheOnLeaveField;
    }
    set
    {
      this.cleanupCacheOnLeaveField = value;
    }
  }

  public bool SuppressRoomEvents
  {
    get
    {
      return this.suppressRoomEventsField;
    }
  }

  public bool PublishUserId
  {
    get
    {
      return this.publishUserIdField;
    }
    set
    {
      this.publishUserIdField = value;
    }
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public bool isVisible
  {
    get
    {
      return this.isVisibleField;
    }
    set
    {
      this.isVisibleField = value;
    }
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public bool isOpen
  {
    get
    {
      return this.isOpenField;
    }
    set
    {
      this.isOpenField = value;
    }
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public byte maxPlayers
  {
    get
    {
      return this.MaxPlayers;
    }
    set
    {
      this.MaxPlayers = value;
    }
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public bool cleanupCacheOnLeave
  {
    get
    {
      return this.cleanupCacheOnLeaveField;
    }
    set
    {
      this.cleanupCacheOnLeaveField = value;
    }
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public Hashtable customRoomProperties
  {
    get
    {
      return this.CustomRoomProperties;
    }
    set
    {
      this.CustomRoomProperties = value;
    }
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public string[] customRoomPropertiesForLobby
  {
    get
    {
      return this.CustomRoomPropertiesForLobby;
    }
    set
    {
      this.CustomRoomPropertiesForLobby = value;
    }
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public string[] plugins
  {
    get
    {
      return this.Plugins;
    }
    set
    {
      this.Plugins = value;
    }
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public bool suppressRoomEvents
  {
    get
    {
      return this.suppressRoomEventsField;
    }
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public bool publishUserId
  {
    get
    {
      return this.publishUserIdField;
    }
    set
    {
      this.publishUserIdField = value;
    }
  }
}
