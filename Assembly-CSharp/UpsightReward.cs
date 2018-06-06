// Decompiled with JetBrains decompiler
// Type: UpsightReward
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UpsightMiniJSON;

public class UpsightReward
{
  public string productIdentifier { get; private set; }

  public int quantity { get; private set; }

  public string signatureData { get; private set; }

  public string billboardScope { get; private set; }

  public static UpsightReward rewardFromJson(string json)
  {
    UpsightReward upsightReward = new UpsightReward();
    upsightReward.populateFromJson(json);
    return upsightReward;
  }

  protected void populateFromJson(string json)
  {
    Dictionary<string, object> jsonObject = Json.ToJsonObject(json);
    if (jsonObject == null)
      return;
    if (jsonObject.ContainsKey("productIdentifier"))
      this.productIdentifier = jsonObject["productIdentifier"].ToString();
    if (jsonObject.ContainsKey("quantity"))
      this.quantity = int.Parse(jsonObject["quantity"].ToString());
    if (jsonObject.ContainsKey("signatureData"))
      this.signatureData = Json.Serialize(jsonObject["signatureData"]);
    if (!jsonObject.ContainsKey("billboardScope"))
      return;
    this.billboardScope = jsonObject["billboardScope"].ToString();
  }

  public override string ToString()
  {
    return string.Format("[UpsightReward] productIdentifier: {0}, quantity: {1}, signatureData: {2}, billboardScope: {3}", new object[4]
    {
      (object) this.productIdentifier,
      (object) this.quantity,
      (object) this.signatureData,
      (object) this.billboardScope
    });
  }
}
