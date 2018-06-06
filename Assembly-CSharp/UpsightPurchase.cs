// Decompiled with JetBrains decompiler
// Type: UpsightPurchase
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UpsightMiniJSON;

public class UpsightPurchase
{
  public string productIdentifier { get; private set; }

  public int quantity { get; private set; }

  public string billboardScope { get; private set; }

  public static UpsightPurchase purchaseFromJson(string json)
  {
    UpsightPurchase upsightPurchase = new UpsightPurchase();
    upsightPurchase.populateFromJson(json);
    return upsightPurchase;
  }

  protected void populateFromJson(string json)
  {
    Dictionary<string, object> jsonObject = Json.ToJsonObject(json);
    if (jsonObject == null)
      return;
    if (jsonObject.ContainsKey("productIdentifier"))
      this.productIdentifier = jsonObject["productIdentifier"].ToString();
    if (jsonObject.ContainsKey("quantity"))
      this.quantity = jsonObject.GetPrimitive<int>("quantity");
    if (!jsonObject.ContainsKey("billboardScope"))
      return;
    this.billboardScope = jsonObject["billboardScope"].ToString();
  }

  public override string ToString()
  {
    return string.Format("[UpsightPurchase] productIdentifier: {0}, quantity: {1}, billboardScope: {2}", (object) this.productIdentifier, (object) this.quantity, (object) this.billboardScope);
  }
}
