// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.App.ProductList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Auth;
using System.Collections.Generic;

namespace Gsc.Purchase.API.App
{
  public class ProductList : GenericRequest<ProductList, Gsc.Purchase.API.Response.ProductList>
  {
    private const string ___path = "/api{0}/{1}/products";

    public override string GetPath()
    {
      return string.Format("/api{0}/{1}/products", (object) SDK.Configuration.Env.PurchaseApiPrefix, (object) Device.Platform);
    }

    public override string GetMethod()
    {
      return "GET";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      return (Dictionary<string, object>) null;
    }
  }
}
