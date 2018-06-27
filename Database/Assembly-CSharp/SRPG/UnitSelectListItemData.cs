// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSelectListItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
