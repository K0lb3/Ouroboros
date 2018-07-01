// Decompiled with JetBrains decompiler
// Type: SRPG.ProductParamResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class ProductParamResponse
  {
    public List<ProductParam> products = new List<ProductParam>();

    public bool Deserialize(JSON_ProductParamResponse json)
    {
      if (json == null || json.products == null)
        return true;
      this.products.Clear();
      for (int index = 0; index < json.products.Length; ++index)
      {
        ProductParam productParam = new ProductParam();
        if (!productParam.Deserialize(json.products[index]))
          return false;
        this.products.Add(productParam);
      }
      return true;
    }
  }
}
