// Decompiled with JetBrains decompiler
// Type: SRPG.ItemSelectListItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ItemSelectListItemData
  {
    public string iiname;
    public short id;
    public short num;
    public ItemParam param;

    public void Deserialize(Json_ItemSelectItem json)
    {
      this.iiname = json.iname;
      this.id = json.id;
      this.num = json.num;
    }
  }
}
