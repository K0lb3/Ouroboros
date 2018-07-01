// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactSelectListData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class ArtifactSelectListData
  {
    public List<ArtifactSelectListItemData> items;

    public void Deserialize(Json_ArtifactSelectResponse json)
    {
      if (json == null || json.select == null)
        return;
      this.items = new List<ArtifactSelectListItemData>();
      for (int index = 0; index < json.select.Length; ++index)
      {
        this.items.Add(new ArtifactSelectListItemData());
        this.items[index].Deserialize(json.select[index]);
        this.items[index].param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.items[index].iname);
      }
    }
  }
}
