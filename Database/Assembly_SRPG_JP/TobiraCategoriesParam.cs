// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraCategoriesParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class TobiraCategoriesParam
  {
    private TobiraParam.Category mCategory;
    private string mName;

    public TobiraParam.Category TobiraCategory
    {
      get
      {
        return this.mCategory;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public void Deserialize(JSON_TobiraCategoriesParam json)
    {
      if (json == null)
        return;
      this.mCategory = (TobiraParam.Category) json.category;
      this.mName = json.name;
    }
  }
}
