// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_LimitedShopItemListSet
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class JSON_LimitedShopItemListSet
  {
    public int id;
    public int sold;
    public JSON_LimitedShopItemListSet.Item item;
    public JSON_LimitedShopItemListSet.Cost cost;
    public Json_ShopItemDesc[] children;
    public int isreset;

    public class Item : Json_ShopItemDesc
    {
      public int maxnum;
      public int boughtnum;
    }

    public class Cost : Json_ShopItemCost
    {
      public int value;
    }
  }
}
