// Decompiled with JetBrains decompiler
// Type: SRPG.ProductSaleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Globalization;

namespace SRPG
{
  public class ProductSaleParam
  {
    private string localizedNameID;
    private string localizedDescriptionID;
    public string ProductId;
    public string Platform;
    public string Name;
    public string Description;
    public int AdditionalFreeCoin;
    public ProductSaleParam.Constrict Condition;

    protected void localizeFields(string language)
    {
      this.init();
      this.Name = LocalizedText.SGGet(language, GameUtility.LocalisedProductSaleFileName, this.localizedNameID);
    }

    protected void init()
    {
      this.localizedNameID = this.GetType().GenerateLocalizedID(this.ProductId, "NAME");
      this.localizedDescriptionID = this.GetType().GenerateLocalizedID(this.ProductId, "DESC");
    }

    public void Deserialize(string language, JSON_ProductSaleParam json)
    {
      this.Deserialize(json);
      this.localizeFields(language);
    }

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
