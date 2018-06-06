// Decompiled with JetBrains decompiler
// Type: SRPG.ShopItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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

    public bool Deserialize(Json_ShopItem json)
    {
      if (json == null || json.item == null || (string.IsNullOrEmpty(json.item.iname) || json.cost == null) || string.IsNullOrEmpty(json.cost.type))
        return false;
      this.id = json.id;
      this.iname = json.item.iname;
      this.num = json.item.num;
      this.saleType = ShopData.String2SaleType(json.cost.type);
      this.is_soldout = json.sold > 0;
      return true;
    }
  }
}
