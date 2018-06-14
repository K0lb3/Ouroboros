// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopBuyList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class EventShopBuyList : MonoBehaviour
  {
    private EventShopItem mEventShopItem;
    public GameObject day_reset;
    public GameObject limit;

    public EventShopBuyList()
    {
      base.\u002Ector();
    }

    public EventShopItem eventShopItem
    {
      set
      {
        this.mEventShopItem = value;
        this.day_reset.SetActive(this.mEventShopItem.is_reset);
        this.limit.SetActive(!this.mEventShopItem.is_reset);
      }
      get
      {
        return this.mEventShopItem;
      }
    }
  }
}
