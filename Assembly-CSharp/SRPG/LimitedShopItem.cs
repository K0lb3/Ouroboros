// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class LimitedShopItem : ShopItem
  {
    public int max_num;
    public int bougthnum;
    private JSON_LimitedShopItemListSet.Cost cost;
    public Json_ShopItemDesc[] children;
    public bool is_reset;

    public int remaining_num
    {
      get
      {
        return this.max_num - this.bougthnum;
      }
    }

    public bool IsSet
    {
      get
      {
        if (this.children != null)
          return this.children.Length > 0;
        return false;
      }
    }

    public bool isSetSaleValue
    {
      get
      {
        bool flag = this.saleValue > 0;
        if (!flag)
          DebugUtility.LogError("saleValueの値が入っていません");
        return flag;
      }
    }

    public bool Deserialize(JSON_LimitedShopItemListSet json)
    {
      if (json == null || json.item == null || (string.IsNullOrEmpty(json.item.iname) || json.cost == null) || string.IsNullOrEmpty(json.cost.type))
        return false;
      this.id = json.id;
      this.iname = json.item.iname;
      this.num = json.item.num;
      this.max_num = json.item.maxnum;
      this.bougthnum = json.item.boughtnum;
      this.saleValue = json.cost.value;
      this.saleType = ShopData.String2SaleType(json.cost.type);
      this.is_reset = json.isreset == 1;
      this.is_soldout = json.sold > 0;
      if (json.children != null)
        this.children = json.children;
      return true;
    }

    public void SetShopItem(ShopItem shop_item)
    {
      this.id = shop_item.id;
      this.iname = shop_item.iname;
      this.is_soldout = shop_item.is_soldout;
      this.num = shop_item.num;
      this.saleType = shop_item.saleType;
      this.saleValue = shop_item.saleValue;
    }
  }
}
