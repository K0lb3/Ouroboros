// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_LimitedShopItemListSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class JSON_LimitedShopItemListSet
  {
    public int id;
    public int sold;
    public Json_ShopItemDesc item;
    public Json_ShopItemCost cost;
    public Json_ShopItemDesc[] children;
    public int isreset;
    public long start;
    public long end;
  }
}
