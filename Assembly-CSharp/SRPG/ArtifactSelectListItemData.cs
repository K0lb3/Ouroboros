// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactSelectListItemData
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ArtifactSelectListItemData
  {
    public string iname;
    public int id;
    public int num;
    public ArtifactParam param;

    public void Deserialize(Json_ArtifactSelectItem json)
    {
      this.iname = json.iname;
      this.id = (int) json.id;
      this.num = (int) json.num;
    }
  }
}
