// Decompiled with JetBrains decompiler
// Type: SRPG.ProductParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
    public int mEnabled;
    public int mRemainingDays;

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

    public int IsEnabled
    {
      get
      {
        return this.mEnabled;
      }
    }

    public int RemainingDays
    {
      get
      {
        return this.mRemainingDays;
      }
    }

    public bool IsOnSale
    {
      get
      {
        return this.mSale != null;
      }
    }

    public bool Deserialize(JSON_ProductParam json)
    {
      if (json == null)
        return false;
      this.mProductId = json.product_id;
      this.mPlatform = json.platform;
      this.mName = LocalizedText.Get(json.name);
      this.mDescription = LocalizedText.Get(json.description);
      this.mAdditionalPaidCoin = json.additional_paid_coin;
      this.mAdditionalFreeCoin = json.additional_free_coin;
      if (json.sale != null)
      {
        this.mSale = new ProductParam.ProductSaleInfo();
        this.mSale.Name = !string.IsNullOrEmpty(json.sale.name) ? LocalizedText.Get(json.sale.name) : string.Empty;
        this.mSale.Description = !string.IsNullOrEmpty(json.sale.description) ? LocalizedText.Get(json.sale.description) : string.Empty;
        this.mSale.AdditionalFreeCoin = json.sale.additional_free_coin > 0 ? json.sale.additional_free_coin : 0;
      }
      this.mEnabled = json.enabled;
      this.mRemainingDays = json.remaining_days;
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
