// Decompiled with JetBrains decompiler
// Type: SRPG.Json_ShopBuyConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class Json_ShopBuyConceptCard
  {
    public string iname;
    public int num;
    public string get_unit;

    public bool IsGetConceptCardUnit
    {
      get
      {
        return !string.IsNullOrEmpty(this.get_unit);
      }
    }
  }
}
