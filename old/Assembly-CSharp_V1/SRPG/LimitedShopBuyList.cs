// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopBuyList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class LimitedShopBuyList : MonoBehaviour
  {
    private LimitedShopItem mLimitedShopItem;
    public GameObject amount;
    public GameObject day_reset;
    public GameObject limit;
    public GameObject icon_set;

    public LimitedShopBuyList()
    {
      base.\u002Ector();
    }

    public LimitedShopItem limitedShopItem
    {
      set
      {
        this.mLimitedShopItem = value;
        this.day_reset.SetActive(this.mLimitedShopItem.is_reset);
        this.limit.SetActive(!this.mLimitedShopItem.is_reset);
        this.icon_set.SetActive(this.mLimitedShopItem.saleType == ESaleType.Coin_P);
      }
      get
      {
        return this.mLimitedShopItem;
      }
    }
  }
}
