// Decompiled with JetBrains decompiler
// Type: SRPG.ProductParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ProductParam
  {
    public string mProductId;
    public string mPlatform;
    public string mName;
    public string mDescription;
    public int mAdditionalPaidCoin;
    public int mAdditionalFreeCoin;
    public ProductParam.ProductSaleInfo mSale;

    public string ProductId
    {
      get
      {
        return this.mProductId;
      }
    }

    public string Platform
    {
      get
      {
        return this.mPlatform;
      }
    }

    public string Name
    {
      get
      {
        if (this.mSale != null && !string.IsNullOrEmpty(this.mSale.Name))
          return this.mSale.Name;
        return this.mName;
      }
    }

    public string Description
    {
      get
      {
        if (this.mSale != null && !string.IsNullOrEmpty(this.mSale.Description))
          return this.mSale.Description;
        return this.mDescription;
      }
    }

    public int AdditionalPaidCoin
    {
      get
      {
        return this.mAdditionalPaidCoin;
      }
    }

    public int AdditionalFreeCoin
    {
      get
      {
        if (this.mSale != null && this.mSale.AdditionalFreeCoin > 0)
          return this.mSale.AdditionalFreeCoin;
        return this.mAdditionalFreeCoin;
      }
    }

    public bool Deserialize(JSON_ProductParam json)
    {
      if (json == null)
        return false;
      this.mProductId = json.product_id;
      this.mPlatform = json.platform;
      this.mName = json.name;
      this.mDescription = json.description;
      this.mAdditionalPaidCoin = json.additional_paid_coin;
      this.mAdditionalFreeCoin = json.additional_free_coin;
      if (json.sale != null)
      {
        this.mSale = new ProductParam.ProductSaleInfo();
        this.mSale.Name = !string.IsNullOrEmpty(json.sale.name) ? json.sale.name : string.Empty;
        this.mSale.Description = !string.IsNullOrEmpty(json.sale.description) ? json.sale.description : string.Empty;
        this.mSale.AdditionalFreeCoin = json.sale.additional_free_coin > 0 ? json.sale.additional_free_coin : 0;
      }
      return true;
    }

    public class ProductSaleInfo
    {
      public string Name;
      public string Description;
      public int AdditionalFreeCoin;
    }
  }
}
