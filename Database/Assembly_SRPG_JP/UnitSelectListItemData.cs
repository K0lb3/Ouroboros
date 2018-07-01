// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSelectListItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class UnitSelectListItemData
  {
    public string iname;
    public UnitParam param;

    public void Deserialize(Json_UnitSelectItem json)
    {
      this.iname = json.iname;
    }
  }
}
