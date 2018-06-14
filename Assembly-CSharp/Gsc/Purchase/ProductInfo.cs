// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.ProductInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace Gsc.Purchase
{
  public class ProductInfo
  {
    public readonly string ID;
    public readonly string LocalizedTitle;
    public readonly string LocalizedDescription;
    public readonly string LocalizedPrice;
    public readonly string CurrencyCode;
    public readonly float Price;

    public ProductInfo(string id, string title, string description, string price, string currencyCode, float priceValue)
    {
      this.ID = id;
      this.LocalizedTitle = title;
      this.LocalizedDescription = description;
      this.LocalizedPrice = price;
      this.CurrencyCode = currencyCode;
      this.Price = priceValue;
    }

    public bool enabled
    {
      get
      {
        return PurchaseFlow.IsEnable(this);
      }
    }
  }
}
