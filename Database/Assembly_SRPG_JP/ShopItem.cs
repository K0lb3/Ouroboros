// Decompiled with JetBrains decompiler
// Type: SRPG.ShopItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class ShopItem
  {
    public int id;
    public string iname;
    public int num;
    public ESaleType saleType;
    public bool is_soldout;
    public int saleValue;
    public int max_num;
    public int bougthnum;
    public Json_ShopItemDesc[] children;
    public bool is_reset;
    public long start;
    public long end;
    public int discount;
    protected EShopItemType shopItemType;

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

    public bool isSetSaleValue
    {
      get
      {
        return this.saleValue > 0;
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

    public bool IsItem
    {
      get
      {
        return this.shopItemType == EShopItemType.Item;
      }
    }

    public bool IsArtifact
    {
      get
      {
        return this.shopItemType == EShopItemType.Artifact;
      }
    }

    public bool IsConceptCard
    {
      get
      {
        return this.shopItemType == EShopItemType.ConceptCard;
      }
    }

    public EShopItemType ShopItemType
    {
      get
      {
        return this.shopItemType;
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
      this.start = json.start;
      this.end = json.end;
      this.discount = json.discount;
      if (json.children != null)
        this.children = json.children;
      if (json.children != null)
      {
        this.shopItemType = EShopItemType.Set;
      }
      else
      {
        this.shopItemType = ShopData.String2ShopItemType(json.item.itype);
        if (this.shopItemType == EShopItemType.Unknown)
          this.shopItemType = ShopData.Iname2ShopItemType(json.item.iname);
      }
      if (this.IsConceptCard)
        MonoSingleton<GameManager>.Instance.Player.SetConceptCardNum(this.iname, json.item.has_count);
      return true;
    }
  }
}
