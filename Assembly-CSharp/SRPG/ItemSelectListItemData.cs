// Decompiled with JetBrains decompiler
// Type: SRPG.ItemSelectListItemData
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
