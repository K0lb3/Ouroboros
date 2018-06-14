// Decompiled with JetBrains decompiler
// Type: SRPG.GachaHistoryData
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class GachaHistoryData
  {
    public GachaDropData.Type type;
    public UnitParam unit;
    public ItemParam item;
    public ArtifactParam artifact;
    public int num;
    public bool isConvert;
    public bool isNew;

    public void Init()
    {
      this.type = GachaDropData.Type.None;
      this.unit = (UnitParam) null;
      this.item = (ItemParam) null;
      this.artifact = (ArtifactParam) null;
      this.num = 0;
      this.isNew = false;
    }

    public bool Deserialize(Json_GachaHistoryItem json)
    {
      this.Init();
      if (json == null)
        return false;
      string itype = json.itype;
      if (itype != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (GachaHistoryData.\u003C\u003Ef__switch\u0024mapF == null)
        {
          // ISSUE: reference to a compiler-generated field
          GachaHistoryData.\u003C\u003Ef__switch\u0024mapF = new Dictionary<string, int>(3)
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
        if (GachaHistoryData.\u003C\u003Ef__switch\u0024mapF.TryGetValue(itype, out num))
        {
          switch (num)
          {
            case 0:
              this.type = GachaDropData.Type.Item;
              this.item = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetItemParam(json.iname);
              break;
            case 1:
              this.type = GachaDropData.Type.Unit;
              this.unit = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitParam(json.iname);
              break;
            case 2:
              this.type = GachaDropData.Type.Artifact;
              this.artifact = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(json.iname);
              break;
          }
        }
      }
      this.num = json.num;
      this.isConvert = json.convert_piece == 1;
      this.isNew = json.is_new == 1;
      return true;
    }
  }
}
