// Decompiled with JetBrains decompiler
// Type: SRPG.ShopData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class ShopData
  {
    public List<ShopItem> items = new List<ShopItem>();
    public int UpdateCount;
    public bool btn_update;

    public static ESaleType String2SaleType(string type)
    {
      string key = type;
      if (key != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (ShopData.\u003C\u003Ef__switch\u0024map13 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ShopData.\u003C\u003Ef__switch\u0024map13 = new Dictionary<string, int>(8)
          {
            {
              "gold",
              0
            },
            {
              "coin",
              1
            },
            {
              "coin_p",
              2
            },
            {
              "tc",
              3
            },
            {
              "ac",
              4
            },
            {
              "ec",
              5
            },
            {
              "pp",
              6
            },
            {
              "mc",
              7
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (ShopData.\u003C\u003Ef__switch\u0024map13.TryGetValue(key, out num))
        {
          switch (num)
          {
            case 0:
              return ESaleType.Gold;
            case 1:
              return ESaleType.Coin;
            case 2:
              return ESaleType.Coin_P;
            case 3:
              return ESaleType.TourCoin;
            case 4:
              return ESaleType.ArenaCoin;
            case 5:
              return ESaleType.EventCoin;
            case 6:
              return ESaleType.PiecePoint;
            case 7:
              return ESaleType.MultiCoin;
          }
        }
      }
      return ESaleType.Coin;
    }

    public bool Deserialize(Json_ShopResponse response)
    {
      if (response.shopitems == null || !this.Deserialize(response.shopitems))
        return false;
      this.UpdateCount = response.relcnt;
      this.btn_update = true;
      if (!string.IsNullOrEmpty(response.msg) && response.msg.StartsWith("{"))
        this.btn_update = JSONParser.parseJSONObject<Json_ShopMsgResponse>(response.msg).update.Equals("on");
      return true;
    }

    public bool Deserialize(Json_ShopUpdateResponse response)
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
      if (response.shopitems == null || !this.Deserialize(response.shopitems))
        return false;
      this.UpdateCount = response.relcnt;
      return true;
    }

    public bool Deserialize(Json_ShopBuyResponse response)
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
      Json_ShopItem[] shopitems = response.shopitems;
      for (int index = 0; index < shopitems.Length; ++index)
      {
        ShopItem shopItem = this.items[index];
        if (shopItem == null)
        {
          shopItem = new ShopItem();
          this.items.Add(shopItem);
        }
        if (!shopItem.Deserialize(shopitems[index]))
          return false;
      }
      return true;
    }

    public bool Deserialize(Json_ShopItem[] shopitems)
    {
      if (shopitems == null)
        return true;
      this.items.Clear();
      for (int index = 0; index < shopitems.Length; ++index)
      {
        ShopItem shopItem = new ShopItem();
        if (!shopItem.Deserialize(shopitems[index]))
          return false;
        this.items.Add(shopItem);
      }
      return true;
    }
  }
}
