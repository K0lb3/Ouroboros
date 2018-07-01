// Decompiled with JetBrains decompiler
// Type: SRPG.ItemParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  public class ItemParam
  {
    public const string CoinID = "$COIN";
    private const string ITEM_EXPR_PREFIX = "_EXPR";
    private const string ITEM_FLAVOR_PREFIX = "_FLAVOR";
    public int no;
    public string iname;
    public string name;
    public EItemType type;
    public EItemTabType tabtype;
    public int rare;
    public int cap;
    public int invcap;
    public int equipLv;
    public int coin;
    public int tour_coin;
    public int arena_coin;
    public int multi_coin;
    public int piece_point;
    public int buy;
    public int sell;
    public int enhace_cost;
    public int enhace_point;
    public int value;
    public string icon;
    public string skill;
    public string recipe;
    public string[] quests;
    public bool is_valuables;
    public byte cmn_type;

    public string Expr
    {
      get
      {
        return this.GetText("external_item", this.iname + "_EXPR");
      }
    }

    public string Flavor
    {
      get
      {
        return this.GetText("external_item", this.iname + "_FLAVOR");
      }
    }

    public RecipeParam Recipe
    {
      get
      {
        if (!string.IsNullOrEmpty(this.recipe))
          return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRecipeParam(this.recipe);
        return (RecipeParam) null;
      }
    }

    public bool IsCommon
    {
      get
      {
        return this.cmn_type > (byte) 0;
      }
    }

    public bool Deserialize(JSON_ItemParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.name = json.name;
      this.type = (EItemType) json.type;
      this.tabtype = (EItemTabType) json.tabtype;
      this.rare = json.rare;
      this.cap = json.cap;
      this.invcap = json.invcap;
      this.equipLv = Math.Max(json.eqlv, 1);
      this.coin = json.coin;
      this.tour_coin = json.tc;
      this.arena_coin = json.ac;
      this.multi_coin = json.mc;
      this.piece_point = json.pp;
      this.buy = json.buy;
      this.sell = json.sell;
      this.enhace_cost = json.encost;
      this.enhace_point = json.enpt;
      this.value = json.val;
      this.icon = json.icon;
      this.skill = json.skill;
      this.recipe = json.recipe;
      this.quests = (string[]) null;
      this.is_valuables = json.is_valuables > 0;
      this.cmn_type = json.cmn_type;
      if (json.quests != null)
      {
        this.quests = new string[json.quests.Length];
        for (int index = 0; index < json.quests.Length; ++index)
          this.quests[index] = json.quests[index];
      }
      return true;
    }

    public int GetPiercePoint()
    {
      if (this.type != EItemType.UnitPiece)
        return 0;
      return (int) MonoSingleton<GameManager>.GetInstanceDirect().GetRarityParam(this.rare).PieceToPoint;
    }

    public int GetEnhanceRankCap()
    {
      if (this.type != EItemType.Equip)
        return 1;
      return (int) MonoSingleton<GameManager>.GetInstanceDirect().GetRarityParam(this.rare).EquipEnhanceParam.rankcap;
    }

    public bool CheckEquipEnhanceMaterial()
    {
      EItemType type = this.type;
      switch (type)
      {
        case EItemType.ItemPiece:
        case EItemType.Equip:
        case EItemType.Material:
        case EItemType.ExpUpEquip:
        case EItemType.ItemPiecePiece:
          return true;
        default:
          if (type != EItemType.ArtifactPiece)
            return false;
          goto case EItemType.ItemPiece;
      }
    }

    public bool CheckCanShowInList()
    {
      switch (this.type)
      {
        case EItemType.Other:
        case EItemType.UnitSkin:
        case EItemType.EventCoin:
        case EItemType.Award:
          return false;
        default:
          return true;
      }
    }

    public override string ToString()
    {
      return string.Format("{0} [ItemParam]", (object) this.iname);
    }

    public int GetBuyNum(ESaleType type)
    {
      switch (type)
      {
        case ESaleType.Gold:
          return this.buy;
        case ESaleType.Coin:
          return this.coin;
        case ESaleType.TourCoin:
          return this.tour_coin;
        case ESaleType.ArenaCoin:
          return this.arena_coin;
        case ESaleType.PiecePoint:
          return this.piece_point;
        case ESaleType.MultiCoin:
          return this.multi_coin;
        case ESaleType.EventCoin:
          return 0;
        case ESaleType.Coin_P:
          return this.coin;
        default:
          return 0;
      }
    }

    public string GetText(string table, string key)
    {
      string str = LocalizedText.Get(table + "." + key);
      if (str.Equals(key))
        return string.Empty;
      return str;
    }
  }
}
