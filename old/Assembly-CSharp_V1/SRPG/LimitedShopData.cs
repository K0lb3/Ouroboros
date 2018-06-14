// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopData
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class LimitedShopData
  {
    public List<LimitedShopItem> items = new List<LimitedShopItem>();
    public int UpdateCount;

    public bool Deserialize(Json_LimitedShopResponse response)
    {
      if (response.shopitems == null || !this.Deserialize(response.shopitems))
        return false;
      this.UpdateCount = response.relcnt;
      return true;
    }

    public bool Deserialize(Json_LimitedShopBuyResponse response)
    {
      if (response.currencies == null)
        return false;
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(response.currencies);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      if (response.items != null)
      {
        try
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(response.items);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return false;
        }
      }
      if (response.shopitems == null)
        return false;
      JSON_LimitedShopItemListSet[] shopitems = response.shopitems;
      for (int index = 0; index < shopitems.Length; ++index)
      {
        LimitedShopItem limitedShopItem = this.items[index];
        if (limitedShopItem == null)
        {
          limitedShopItem = new LimitedShopItem();
          this.items.Add(limitedShopItem);
        }
        if (!limitedShopItem.Deserialize(shopitems[index]))
          return false;
      }
      if (response.mail_info == null)
        return false;
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(response.mail_info);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      return true;
    }

    public bool Deserialize(JSON_LimitedShopItemListSet[] shopitems)
    {
      if (shopitems == null)
        return true;
      this.items.Clear();
      for (int index = 0; index < shopitems.Length; ++index)
      {
        LimitedShopItem limitedShopItem = new LimitedShopItem();
        if (!limitedShopItem.Deserialize(shopitems[index]))
          return false;
        this.items.Add(limitedShopItem);
      }
      return true;
    }

    public ShopData GetShopData()
    {
      ShopData shopData = new ShopData();
      shopData.items = new List<ShopItem>();
      for (int index = 0; index < shopData.items.Count; ++index)
        shopData.items[index] = (ShopItem) this.items[index];
      shopData.UpdateCount = this.UpdateCount;
      return shopData;
    }

    public void SetShopData(ShopData shopData)
    {
      shopData.items = new List<ShopItem>();
      for (int index = 0; index < shopData.items.Count; ++index)
        this.items[index].SetShopItem(shopData.items[index]);
      shopData.UpdateCount = this.UpdateCount;
    }
  }
}
