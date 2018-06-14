// Decompiled with JetBrains decompiler
// Type: SRPG.ItemParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  public class ItemParam
  {
    public const string CoinID = "$COIN";
    private string localizedNameID;
    private string localizedExprID;
    private string localizedFlavorID;
    public int no;
    public string iname;
    public string name;
    public string expr;
    public string flavor;
    public EItemType type;
    public OInt rare;
    public OInt cap;
    public OInt invcap;
    public OInt equipLv;
    public OInt coin;
    public OInt tour_coin;
    public OInt arena_coin;
    public OInt multi_coin;
    public OInt piece_point;
    public OInt buy;
    public OInt sell;
    public OInt enhace_cost;
    public OInt enhace_point;
    public OInt value;
    public OString icon;
    public OString skill;
    public string recipe;
    public string[] quests;

    protected void localizeFields(string language)
    {
      this.init();
      this.name = LocalizedText.SGGet(language, GameUtility.LocalizedMasterParamFileName, this.localizedNameID);
      this.expr = LocalizedText.SGGet(language, GameUtility.LocalizedMasterParamFileName, this.localizedExprID);
      this.flavor = LocalizedText.SGGet(language, GameUtility.LocalizedMasterParamFileName, this.localizedFlavorID);
    }

    protected void init()
    {
      this.localizedNameID = this.GetType().GenerateLocalizedID(this.iname, "NAME");
      this.localizedExprID = this.GetType().GenerateLocalizedID(this.iname, "EXPR");
      this.localizedFlavorID = this.GetType().GenerateLocalizedID(this.iname, "FLAVOR");
    }

    public void Deserialize(string language, JSON_ItemParam json)
    {
      this.Deserialize(json);
      this.localizeFields(language);
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

    public bool Deserialize(JSON_ItemParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.name = json.name;
      this.expr = json.expr;
      this.flavor = json.flavor;
      this.type = (EItemType) json.type;
      this.rare = (OInt) json.rare;
      this.cap = (OInt) json.cap;
      this.invcap = (OInt) json.invcap;
      this.equipLv = (OInt) Math.Max(json.eqlv, 1);
      this.coin = (OInt) json.coin;
      this.tour_coin = (OInt) json.tc;
      this.arena_coin = (OInt) json.ac;
      this.multi_coin = (OInt) json.mc;
      this.piece_point = (OInt) json.pp;
      this.buy = (OInt) json.buy;
      this.sell = (OInt) json.sell;
      this.enhace_cost = (OInt) json.encost;
      this.enhace_point = (OInt) json.enpt;
      this.value = (OInt) json.val;
      this.icon = (OString) json.icon;
      this.skill = (OString) json.skill;
      this.recipe = json.recipe;
      this.quests = (string[]) null;
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
      return (int) MonoSingleton<GameManager>.GetInstanceDirect().GetRarityParam((int) this.rare).PieceToPoint;
    }

    public int GetEnhanceRankCap()
    {
      if (this.type != EItemType.Equip)
        return 1;
      return (int) MonoSingleton<GameManager>.GetInstanceDirect().GetRarityParam((int) this.rare).EquipEnhanceParam.rankcap;
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
  }
}
