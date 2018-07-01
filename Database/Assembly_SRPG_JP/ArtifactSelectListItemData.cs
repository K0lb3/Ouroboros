// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactSelectListItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
