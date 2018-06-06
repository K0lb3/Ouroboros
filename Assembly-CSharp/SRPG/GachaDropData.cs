// Decompiled with JetBrains decompiler
// Type: SRPG.GachaDropData
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class GachaDropData
  {
    public GachaDropData.Type type;
    public UnitParam unit;
    public ItemParam item;
    public ArtifactParam artifact;
    public int num;
    public UnitParam unitOrigin;
    public bool isNew;
    public int[] excites;

    public int Rare
    {
      get
      {
        int num = 1;
        if (this.type == GachaDropData.Type.Unit)
          num = (int) this.unit.rare;
        else if (this.type == GachaDropData.Type.Item)
          num = (int) this.item.rare;
        else if (this.type == GachaDropData.Type.Artifact)
          num = this.artifact.rareini;
        return num;
      }
    }

    public void Init()
    {
      this.type = GachaDropData.Type.None;
      this.unit = (UnitParam) null;
      this.item = (ItemParam) null;
      this.artifact = (ArtifactParam) null;
      this.num = 0;
      this.unitOrigin = (UnitParam) null;
      this.isNew = false;
      this.excites = new int[3];
    }

    public bool Deserialize(Json_DropInfo json)
    {
      this.Init();
      if (json == null)
        return false;
      string type = json.type;
      if (type != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (GachaDropData.\u003C\u003Ef__switch\u0024map5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          GachaDropData.\u003C\u003Ef__switch\u0024map5 = new Dictionary<string, int>(3)
          {
            {
              "item",
              0
            },
            {
              "unit",
              1
            },
            {
              "artifact",
              2
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (GachaDropData.\u003C\u003Ef__switch\u0024map5.TryGetValue(type, out num))
        {
          switch (num)
          {
            case 0:
              this.type = GachaDropData.Type.Item;
              this.item = MonoSingleton<GameManager>.Instance.GetItemParam(json.iname);
              break;
            case 1:
              this.unit = MonoSingleton<GameManager>.Instance.GetUnitParam(json.iname);
              this.type = GachaDropData.Type.Unit;
              break;
            case 2:
              this.artifact = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(json.iname);
              this.type = GachaDropData.Type.Artifact;
              break;
          }
        }
      }
      this.num = json.num;
      if (0 < json.iname_origin.Length)
        this.unitOrigin = MonoSingleton<GameManager>.Instance.GetUnitParam(json.iname_origin);
      this.isNew = 1 == json.is_new;
      return true;
    }

    public override string ToString()
    {
      string str = "type: " + (object) this.type + "\n";
      switch (this.type)
      {
        case GachaDropData.Type.Item:
          str = str + "name: " + this.item.name + " rare: " + (object) this.item.rare;
          break;
        case GachaDropData.Type.Unit:
          str = str + "name: " + this.unit.name + " rare: " + (object) this.unit.rare;
          break;
        case GachaDropData.Type.Artifact:
          str = str + "name: " + this.artifact.name + " rare: " + (object) this.artifact.rareini;
          break;
      }
      if (this.unitOrigin != null)
        str = str + " origin: " + this.unitOrigin.name;
      return str + " isNew: " + (object) this.isNew;
    }

    public enum Type
    {
      None,
      Item,
      Unit,
      Artifact,
      End,
    }
  }
}
