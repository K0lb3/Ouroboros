// Decompiled with JetBrains decompiler
// Type: SRPG.Json_LimitedShopBuyResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class Json_LimitedShopBuyResponse
  {
    public Json_Currencies currencies;
    public JSON_LimitedShopItemListSet[] shopitems;
    public Json_Item[] items;
    public Json_MailInfo mail_info;
    public Json_ShopBuyConceptCard[] cards;
    public Json_Unit[] units;
    public int concept_count;
  }
}
