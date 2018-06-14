// Decompiled with JetBrains decompiler
// Type: SRPG.ShopItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ShopItem
  {
    public int id;
    public string iname;
    public int num;
    public ESaleType saleType;
    public int saleValue;
    public bool is_soldout;
    public int max_num;
    public int bougthnum;
    private Json_ShopItemCost cost;
    public Json_ShopItemDesc[] children;
    public bool is_reset;

    public int remaining_num
    {
      get
      {
        return this.max_num - this.bougthnum;
      }
    }

    public bool IsNotLimited
    {
      get
      {
        return this.max_num == 0;
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

    public bool IsArtifact
    {
      get
      {
        return this.iname.StartsWith("AF_");
      }
    }

    public bool isSetSaleValue
    {
      get
      {
        return this.saleValue > 0;
      }
    }

    public bool Deserialize(Json_ShopItem json)
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
  }
}
