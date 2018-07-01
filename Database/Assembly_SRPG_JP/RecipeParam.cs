// Decompiled with JetBrains decompiler
// Type: SRPG.RecipeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class RecipeParam
  {
    public string iname;
    public int cost;
    public RecipeItem[] items;

    public bool Deserialize(JSON_RecipeParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.cost = json.cost;
      int length = 0;
      string[] strArray = new string[5]
      {
        json.mat1,
        json.mat2,
        json.mat3,
        json.mat4,
        json.mat5
      };
      for (int index = 0; index < strArray.Length && !string.IsNullOrEmpty(strArray[index]); ++index)
        ++length;
      if (length > 0)
      {
        int[] numArray = new int[5]
        {
          json.num1,
          json.num2,
          json.num3,
          json.num4,
          json.num5
        };
        this.items = new RecipeItem[length];
        for (int index = 0; index < length; ++index)
        {
          this.items[index] = new RecipeItem();
          this.items[index].iname = strArray[index];
          this.items[index].num = numArray[index];
        }
      }
      return true;
    }
  }
}
