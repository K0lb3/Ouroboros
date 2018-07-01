// Decompiled with JetBrains decompiler
// Type: SRPG.GachaHistoryData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class GachaHistoryData
  {
    public GachaDropData.Type type;
    public UnitParam unit;
    public ItemParam item;
    public ArtifactParam artifact;
    public ConceptCardParam conceptcard;
    public int num;
    public bool isConvert;
    public bool isNew;
    public int rarity;

    public void Init()
    {
      this.type = GachaDropData.Type.None;
      this.unit = (UnitParam) null;
      this.item = (ItemParam) null;
      this.artifact = (ArtifactParam) null;
      this.conceptcard = (ConceptCardParam) null;
      this.num = 0;
      this.isNew = false;
      this.rarity = 0;
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
        if (GachaHistoryData.\u003C\u003Ef__switch\u0024map13 == null)
        {
          // ISSUE: reference to a compiler-generated field
          GachaHistoryData.\u003C\u003Ef__switch\u0024map13 = new Dictionary<string, int>(4)
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
            },
            {
              "concept_card",
              3
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (GachaHistoryData.\u003C\u003Ef__switch\u0024map13.TryGetValue(itype, out num))
        {
          switch (num)
          {
            case 0:
              this.type = GachaDropData.Type.Item;
              this.item = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetItemParam(json.iname);
              if (this.item == null)
              {
                DebugUtility.LogError("iname:" + json.iname + " => Not ItemParam!");
                return false;
              }
              this.rarity = this.item.rare;
              break;
            case 1:
              this.type = GachaDropData.Type.Unit;
              this.unit = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitParam(json.iname);
              if (this.unit == null)
              {
                DebugUtility.LogError("iname:" + json.iname + " => Not UnitParam!");
                return false;
              }
              this.rarity = (int) this.unit.rare;
              break;
            case 2:
              this.type = GachaDropData.Type.Artifact;
              this.artifact = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(json.iname);
              if (this.artifact == null)
              {
                DebugUtility.LogError("iname:" + json.iname + " => Not ArtifactParam!");
                return false;
              }
              if (json.rare != -1 && json.rare > this.artifact.raremax)
                DebugUtility.LogError("武具:" + this.artifact.name + "の最大レアリティより大きい値が設定されています.");
              else if (json.rare != -1 && json.rare < this.artifact.rareini)
                DebugUtility.LogError("武具:" + this.artifact.name + "の初期レアリティより小さい値が設定されています.");
              this.rarity = json.rare <= -1 ? this.artifact.rareini : Mathf.Min(Mathf.Max(this.artifact.rareini, json.rare), this.artifact.raremax);
              break;
            case 3:
              this.type = GachaDropData.Type.ConceptCard;
              this.conceptcard = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(json.iname);
              if (this.conceptcard == null)
              {
                DebugUtility.LogError("iname:" + json.iname + " => Not ConceptCardParam!");
                return false;
              }
              this.rarity = this.conceptcard.rare;
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
