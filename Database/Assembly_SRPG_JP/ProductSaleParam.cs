// Decompiled with JetBrains decompiler
// Type: SRPG.ProductSaleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Globalization;

namespace SRPG
{
  public class ProductSaleParam
  {
    public string ProductId;
    public string Platform;
    public string Name;
    public string Description;
    public int AdditionalFreeCoin;
    public ProductSaleParam.Constrict Condition;

    public bool Deserialize(JSON_ProductSaleParam json)
    {
      if (json == null)
        return false;
      this.ProductId = json.fields.product_id;
      this.Platform = json.fields.platform;
      this.Name = json.fields.name;
      this.Description = json.fields.description;
      this.AdditionalFreeCoin = json.fields.additional_free_coin;
      this.Condition.type = (ProductSaleParam.Constrict.Type) json.fields.condition_type;
      this.Condition.value = json.fields.condition_value;
      return true;
    }

    public struct Constrict
    {
      public ProductSaleParam.Constrict.Type type;
      public string value;

      public int valueInt
      {
        get
        {
          return int.Parse(this.value, NumberStyles.Float);
        }
      }

      public enum Type
      {
        None,
        TimesAMonth,
      }
    }
  }
}
