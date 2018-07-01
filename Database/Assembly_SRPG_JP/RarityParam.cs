// Decompiled with JetBrains decompiler
// Type: SRPG.RarityParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class RarityParam
  {
    public static readonly int MAX_RARITY = 6;
    public StatusParam GrowStatus = new StatusParam();
    public OInt UnitLvCap;
    public OInt UnitJobLvCap;
    public OInt UnitAwakeLvCap;
    public OInt UnitUnlockPieceNum;
    public OInt UnitChangePieceNum;
    public OInt UnitSelectChangePieceNum;
    public OInt UnitRarityUpCost;
    public OInt PieceToPoint;
    public string DropSE;
    public RarityEquipEnhanceParam EquipEnhanceParam;
    public OInt ArtifactLvCap;
    public OInt ArtifactCostRate;
    public OInt ArtifactCreatePieceNum;
    public OInt ArtifactGouseiPieceNum;
    public OInt ArtifactChangePieceNum;
    public OInt ArtifactCreateCost;
    public OInt ArtifactRarityUpCost;
    public OInt ArtifactChangeCost;
    public OInt ConceptCardLvCap;
    public OInt ConceptCardAwakeCountMax;

    public bool Deserialize(JSON_RarityParam json)
    {
      if (json == null)
        return false;
      this.UnitLvCap = (OInt) json.unitcap;
      this.UnitJobLvCap = (OInt) json.jobcap;
      this.UnitAwakeLvCap = (OInt) json.awakecap;
      this.UnitUnlockPieceNum = (OInt) json.piece;
      this.UnitChangePieceNum = (OInt) json.ch_piece;
      this.UnitSelectChangePieceNum = (OInt) json.ch_piece_select;
      this.UnitRarityUpCost = (OInt) json.rareup_cost;
      this.PieceToPoint = (OInt) json.gain_pp;
      if (this.EquipEnhanceParam == null)
        this.EquipEnhanceParam = new RarityEquipEnhanceParam();
      int length = json.eq_enhcap + 1;
      this.EquipEnhanceParam.rankcap = (OInt) length;
      this.EquipEnhanceParam.cost_scale = (OInt) json.eq_costscale;
      this.EquipEnhanceParam.ranks = (RarityEquipEnhanceParam.RankParam[]) null;
      if (length > 0)
      {
        if (json.eq_points == null || json.eq_num1 == null || (json.eq_num2 == null || json.eq_num3 == null))
          return false;
        this.EquipEnhanceParam.ranks = new RarityEquipEnhanceParam.RankParam[length];
        for (int index = 0; index < length; ++index)
        {
          this.EquipEnhanceParam.ranks[index] = new RarityEquipEnhanceParam.RankParam();
          this.EquipEnhanceParam.ranks[index].need_point = (OInt) json.eq_points[index];
        }
        string[] strArray = new string[3]
        {
          json.eq_item1,
          json.eq_item2,
          json.eq_item3
        };
        int[][] numArray = new int[3][]
        {
          json.eq_num1,
          json.eq_num2,
          json.eq_num3
        };
        for (int index1 = 0; index1 < strArray.Length; ++index1)
        {
          for (int index2 = 0; index2 < length; ++index2)
          {
            this.EquipEnhanceParam.ranks[index2].return_item[index1] = new ReturnItem();
            this.EquipEnhanceParam.ranks[index2].return_item[index1].iname = strArray[index1];
            this.EquipEnhanceParam.ranks[index2].return_item[index1].num = (OInt) numArray[index1][index2];
          }
        }
      }
      this.ArtifactLvCap = (OInt) json.af_lvcap;
      this.ArtifactCostRate = (OInt) json.af_upcost;
      this.ArtifactCreatePieceNum = (OInt) json.af_unlock;
      this.ArtifactGouseiPieceNum = (OInt) json.af_gousei;
      this.ArtifactChangePieceNum = (OInt) json.af_change;
      this.ArtifactCreateCost = (OInt) json.af_unlock_cost;
      this.ArtifactRarityUpCost = (OInt) json.af_gousei_cost;
      this.ArtifactChangeCost = (OInt) json.af_change_cost;
      this.GrowStatus.hp = (OInt) json.hp;
      this.GrowStatus.mp = (OShort) json.mp;
      this.GrowStatus.atk = (OShort) json.atk;
      this.GrowStatus.def = (OShort) json.def;
      this.GrowStatus.mag = (OShort) json.mag;
      this.GrowStatus.mnd = (OShort) json.mnd;
      this.GrowStatus.dex = (OShort) json.dex;
      this.GrowStatus.spd = (OShort) json.spd;
      this.GrowStatus.cri = (OShort) json.cri;
      this.GrowStatus.luk = (OShort) json.luk;
      this.DropSE = json.drop;
      this.ConceptCardLvCap = (OInt) json.card_lvcap;
      this.ConceptCardAwakeCountMax = (OInt) json.card_awake_count;
      return true;
    }
  }
}
