// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class EventShopData
  {
    public List<EventShopItem> items = new List<EventShopItem>();
    private ShopData mShopData = new ShopData();
    public int UpdateCount;

    public bool Deserialize(Json_EventShopResponse response)
    {
      if (response.shopitems == null || !this.Deserialize(response.shopitems))
        return false;
      this.UpdateCount = response.relcnt;
      GlobalVars.ConceptCardNum.Set(response.concept_count);
      return true;
    }

    public bool Deserialize(Json_EventShopUpdateResponse response)
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
      if (response.items == null)
        return false;
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(response.items);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      if (response.shopitems == null || !this.Deserialize(response.shopitems))
        return false;
      this.UpdateCount = response.relcnt;
      GlobalVars.ConceptCardNum.Set(response.concept_count);
      return true;
    }

    public bool Deserialize(Json_EventShopBuyResponse response)
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
      if (response.items == null)
        return false;
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(response.items);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      if (response.shopitems == null)
        return false;
      JSON_EventShopItemListSet[] shopitems = response.shopitems;
      for (int index = 0; index < shopitems.Length; ++index)
      {
        EventShopItem eventShopItem = this.items[index];
        if (eventShopItem == null)
        {
          eventShopItem = new EventShopItem();
          this.items.Add(eventShopItem);
        }
        if (!eventShopItem.Deserialize(shopitems[index]))
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
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(response.units);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      GlobalVars.ConceptCardNum.Set(response.concept_count);
      return true;
    }

    public bool Deserialize(JSON_EventShopItemListSet[] shopitems)
    {
      if (shopitems == null)
        return true;
      this.items.Clear();
      for (int index = 0; index < shopitems.Length; ++index)
      {
        EventShopItem eventShopItem = new EventShopItem();
        if (!eventShopItem.Deserialize(shopitems[index]))
          return false;
        this.items.Add(eventShopItem);
      }
      return true;
    }

    public ShopData GetShopData()
    {
      this.mShopData.items = new List<ShopItem>();
      for (int index = 0; index < this.items.Count; ++index)
        this.mShopData.items.Add((ShopItem) this.items[index]);
      this.mShopData.UpdateCount = this.UpdateCount;
      return this.mShopData;
    }

    public void SetShopData(ShopData shopData)
    {
      this.mShopData = shopData;
    }
  }
}
