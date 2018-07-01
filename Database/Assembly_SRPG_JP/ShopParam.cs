// Decompiled with JetBrains decompiler
// Type: SRPG.ShopParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ShopParam
  {
    public string iname;
    public ESaleType UpdateCostType;
    public int[] UpdateCosts;

    public bool Deserialize(JSON_ShopParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.UpdateCostType = (ESaleType) json.upd_type;
      this.UpdateCosts = (int[]) null;
      if (json.upd_costs != null && json.upd_costs.Length > 0)
      {
        this.UpdateCosts = new int[json.upd_costs.Length];
        for (int index = 0; index < json.upd_costs.Length; ++index)
          this.UpdateCosts[index] = json.upd_costs[index];
      }
      return true;
    }
  }
}
