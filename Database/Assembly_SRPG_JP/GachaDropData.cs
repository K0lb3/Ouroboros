// Decompiled with JetBrains decompiler
// Type: SRPG.GachaDropData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
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
    private int rarity;
    public ConceptCardParam conceptcard;
    public UnitParam cardunit;
    public bool isGift;

    public int Rare
    {
      get
      {
        return this.rarity;
      }
      set
      {
        this.rarity = value;
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
      this.rarity = 0;
      this.excites = new int[3];
      this.isGift = false;
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
        if (GachaDropData.\u003C\u003Ef__switch\u0024map8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          GachaDropData.\u003C\u003Ef__switch\u0024map8 = new Dictionary<string, int>(4)
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
        if (GachaDropData.\u003C\u003Ef__switch\u0024map8.TryGetValue(type, out num))
        {
          switch (num)
          {
            case 0:
              this.type = GachaDropData.Type.Item;
              this.item = MonoSingleton<GameManager>.Instance.GetItemParam(json.iname);
              if (this.item == null)
              {
                DebugUtility.LogError("iname:" + json.iname + " => Not ItemParam!");
                return false;
              }
              this.rarity = this.item.rare;
              break;
            case 1:
              this.unit = MonoSingleton<GameManager>.Instance.GetUnitParam(json.iname);
              if (this.unit == null)
              {
                DebugUtility.LogError("iname:" + json.iname + " => Not UnitParam!");
                return false;
              }
              this.rarity = (int) this.unit.rare;
              this.type = GachaDropData.Type.Unit;
              break;
            case 2:
              this.artifact = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(json.iname);
              if (this.artifact == null)
              {
                DebugUtility.LogError("iname:" + json.iname + " => Not ArtifactParam!");
                return false;
              }
              if (json.rare != -1 && json.rare > this.artifact.raremax)
                DebugUtility.LogError("武具:" + this.artifact.name + "の最大レアリティより大きい値が設定されています.");
              else if (json.rare != -1 && json.rare < this.artifact.rareini)
                DebugUtility.LogError("武具:" + this.artifact.name + "の初期レアリティより小さい値が設定されています.");
              this.rarity = json.rare <= -1 ? this.artifact.rareini : Math.Min(Math.Max(this.artifact.rareini, json.rare), this.artifact.raremax);
              this.type = GachaDropData.Type.Artifact;
              break;
            case 3:
              this.conceptcard = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(json.iname);
              if (this.conceptcard == null)
              {
                DebugUtility.LogError("iname:" + json.iname + " => Not ConceptCardParam!");
                return false;
              }
              this.rarity = this.conceptcard.rare;
              this.type = GachaDropData.Type.ConceptCard;
              break;
          }
        }
      }
      this.num = json.num;
      if (0 < json.iname_origin.Length)
        this.unitOrigin = MonoSingleton<GameManager>.Instance.GetUnitParam(json.iname_origin);
      this.isNew = 1 == json.is_new;
      if (this.type == GachaDropData.Type.ConceptCard && !string.IsNullOrEmpty(json.get_unit))
      {
        this.cardunit = MonoSingleton<GameManager>.Instance.GetUnitParam(json.get_unit);
        if (this.cardunit == null)
        {
          DebugUtility.LogError("get_unit:" + json.get_unit + " => Not UnitParam!");
          return false;
        }
      }
      this.isGift = json.is_gift == 1;
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
      ConceptCard,
      End,
    }
  }
}
