// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ProductSaleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class JSON_ProductSaleParam
  {
    public int pk;
    public JSON_ProductSaleParam.Fields fields;

    public class Fields
    {
      public int id;
      public string product_id;
      public string platform;
      public string name;
      public string description;
      public int additional_free_coin;
      public int condition_type;
      public string condition_value;
    }
  }
}
