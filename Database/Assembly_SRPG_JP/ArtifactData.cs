// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class ArtifactData
  {
    private static BaseStatus WorkScaleStatus = new BaseStatus();
    private OLong mUniqueID = (OLong) 0L;
    private OInt mRarity = (OInt) 0;
    private OInt mLv = (OInt) 1;
    private ArtifactParam mArtifactParam;
    private RarityParam mRarityParam;
    private OInt mExp;
    private bool mFavorite;
    private SkillData mEquipSkill;
    private SkillData mBattleEffectSkill;
    private List<AbilityData> mLearningAbilities;

    public int Exp
    {
      get
      {
        return (int) this.mExp;
      }
    }

    public OLong UniqueID
    {
      get
      {
        return this.mUniqueID;
      }
    }

    public ArtifactParam ArtifactParam
    {
      get
      {
        return this.mArtifactParam;
      }
    }

    public RarityParam RarityParam
    {
      get
      {
        return this.mRarityParam;
      }
    }

    public OInt Rarity
    {
      get
      {
        return this.mRarity;
      }
    }

    public OInt RarityCap
    {
      get
      {
        return (OInt) this.mArtifactParam.raremax;
      }
    }

    public OInt Lv
    {
      get
      {
        return this.mLv;
      }
    }

    public OInt LvCap
    {
      get
      {
        return (OInt) this.GetLevelCap();
      }
    }

    public ItemParam Kakera
    {
      get
      {
        return MonoSingleton<GameManager>.Instance.GetItemParam(this.mArtifactParam.kakera);
      }
    }

    public ItemData KakeraData
    {
      get
      {
        return MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(this.Kakera);
      }
    }

    public ItemParam RarityKakera
    {
      get
      {
        FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
        if (fixParam.ArtifactRarePiece == null)
          return (ItemParam) null;
        if (fixParam.ArtifactRarePiece.Length <= (int) this.Rarity)
          return (ItemParam) null;
        return MonoSingleton<GameManager>.Instance.GetItemParam((string) fixParam.ArtifactRarePiece[(int) this.Rarity]);
      }
    }

    public ItemData RarityKakeraData
    {
      get
      {
        return MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(this.RarityKakera);
      }
    }

    public ItemParam CommonKakera
    {
      get
      {
        return MonoSingleton<GameManager>.Instance.GetItemParam((string) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ArtifactCommonPiece);
      }
    }

    public ItemData CommonKakeraData
    {
      get
      {
        return MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(this.CommonKakera);
      }
    }

    public bool IsFavorite
    {
      get
      {
        return this.mFavorite;
      }
      set
      {
        this.mFavorite = value;
      }
    }

    public SkillData EquipSkill
    {
      get
      {
        return this.mEquipSkill;
      }
    }

    public SkillData BattleEffectSkill
    {
      get
      {
        return this.mBattleEffectSkill;
      }
    }

    public List<AbilityData> LearningAbilities
    {
      get
      {
        return this.mLearningAbilities;
      }
    }

    public void Deserialize(Json_Artifact json)
    {
      if (json == null || string.IsNullOrEmpty(json.iname))
      {
        this.Reset();
      }
      else
      {
        GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
        this.mArtifactParam = instanceDirect.MasterParam.GetArtifactParam(json.iname);
        this.mUniqueID = (OLong) json.iid;
        this.mRarity = (OInt) Math.Min(Math.Max(json.rare, this.mArtifactParam.rareini), this.mArtifactParam.raremax);
        this.mRarityParam = instanceDirect.GetRarityParam((int) this.mRarity);
        this.mExp = (OInt) json.exp;
        this.mLv = (OInt) this.GetLevelFromExp((int) this.mExp);
        this.mFavorite = json.fav != 0;
        this.UpdateEquipEffect();
        this.UpdateLearningAbilities(false);
      }
    }

    public void Reset()
    {
      this.mArtifactParam = (ArtifactParam) null;
      this.mRarityParam = (RarityParam) null;
      this.mRarity = (OInt) 0;
      this.mExp = (OInt) 0;
      this.mLv = (OInt) 1;
      this.mUniqueID = (OLong) 0L;
      this.mFavorite = false;
    }

    public ArtifactData Copy()
    {
      ArtifactData artifactData = new ArtifactData();
      artifactData.Deserialize(new Json_Artifact()
      {
        iname = this.mArtifactParam.iname,
        iid = (long) this.mUniqueID,
        rare = (int) this.mRarity,
        exp = (int) this.mExp,
        fav = !this.mFavorite ? 0 : 1
      });
      return artifactData;
    }

    public bool IsValid()
    {
      return this.mArtifactParam != null;
    }

    private void UpdateLearningAbilities(bool bRarityUp = false)
    {
      if (!this.IsValid() || this.mArtifactParam == null || this.mArtifactParam.abil_inames == null)
        return;
      if (this.mLearningAbilities == null)
        this.mLearningAbilities = new List<AbilityData>(this.mArtifactParam.abil_inames.Length);
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      for (int index = 0; index < this.mArtifactParam.abil_inames.Length; ++index)
      {
        if ((int) this.mLv >= this.mArtifactParam.abil_levels[index] && (int) this.mRarity >= this.mArtifactParam.abil_rareties[index])
        {
          AbilityParam param = instanceDirect.GetAbilityParam(this.mArtifactParam.abil_inames[index]);
          if (param != null)
          {
            AbilityData abilityData1 = this.mLearningAbilities.Find((Predicate<AbilityData>) (p => p.Param == param));
            if (abilityData1 == null)
            {
              AbilityData abilityData2 = new AbilityData();
              abilityData2.Setup((UnitData) null, 0L, param.iname, (int) this.mRarity, 0);
              abilityData2.IsNoneCategory = true;
              abilityData2.IsHideList = this.mArtifactParam.abil_shows[index] == 0;
              this.mLearningAbilities.Add(abilityData2);
            }
            else if (bRarityUp)
              abilityData1.Setup((UnitData) null, 0L, param.iname, (int) this.mRarity, 0);
          }
        }
      }
    }

    public List<AbilityData> GetEnableAbitilies(UnitData unit, int job_index)
    {
      if (this.mLearningAbilities == null || this.mLearningAbilities.Count == 0)
        return (List<AbilityData>) null;
      List<AbilityData> abilityDataList = new List<AbilityData>(this.mLearningAbilities.Count);
      for (int index = 0; index < this.mLearningAbilities.Count; ++index)
      {
        if (this.mLearningAbilities[index].CheckEnableUseAbility(unit, job_index))
          abilityDataList.Add(this.mLearningAbilities[index]);
      }
      return abilityDataList;
    }

    public bool CheckEnableEquip(UnitData unit, int jobIndex = -1)
    {
      if (this.IsValid())
        return this.mArtifactParam.CheckEnableEquip(unit, jobIndex);
      return false;
    }

    private void UpdateEquipEffect()
    {
      RarityParam rarityParam = MonoSingleton<GameManager>.GetInstanceDirect().GetRarityParam(this.mArtifactParam.raremax);
      int rankcap = 1;
      if (rarityParam != null)
        rankcap = (int) rarityParam.ArtifactLvCap;
      if (this.mArtifactParam.equip_effects != null && (int) this.mRarity >= 0 && (int) this.mRarity < this.mArtifactParam.equip_effects.Length)
      {
        if (!string.IsNullOrEmpty(this.mArtifactParam.equip_effects[(int) this.mRarity]))
        {
          if (this.mEquipSkill == null)
            this.mEquipSkill = new SkillData();
          this.mEquipSkill.Setup(this.mArtifactParam.equip_effects[(int) this.mRarity], (int) this.mLv, rankcap, (MasterParam) null);
        }
      }
      else
        this.mEquipSkill = (SkillData) null;
      if (this.mArtifactParam.attack_effects != null && (int) this.mRarity >= 0 && (int) this.mRarity < this.mArtifactParam.attack_effects.Length)
      {
        if (string.IsNullOrEmpty(this.mArtifactParam.attack_effects[(int) this.mRarity]))
          return;
        if (this.mBattleEffectSkill == null)
          this.mBattleEffectSkill = new SkillData();
        this.mBattleEffectSkill.Setup(this.mArtifactParam.attack_effects[(int) this.mRarity], (int) this.mLv, rankcap, (MasterParam) null);
      }
      else
        this.mBattleEffectSkill = (SkillData) null;
    }

    public int GetLevelCap()
    {
      if (this.mRarityParam == null)
        return 1;
      return (int) this.mRarityParam.ArtifactLvCap;
    }

    public int GetLevelFromExp(int exp)
    {
      return Math.Min(ArtifactData.StaticCalcLevelFromExp(exp), this.GetLevelCap());
    }

    public static int StaticCalcLevelFromExp(int exp)
    {
      int num1 = 0;
      int num2 = 0;
      OInt[] artifactExpTable = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactExpTable();
      int num3 = artifactExpTable.Length + 1;
      for (int index = 0; index < num3; ++index)
      {
        num2 += (int) artifactExpTable[index + 1];
        if (num2 <= exp)
          ++num1;
        else
          break;
      }
      return num1 + 1;
    }

    public int GetTotalExpFromLevel(int lv)
    {
      return ArtifactData.StaticCalcExpFromLevel(Math.Min(lv, this.GetLevelCap()));
    }

    public static int StaticCalcExpFromLevel(int lv)
    {
      int num = 0;
      OInt[] artifactExpTable = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactExpTable();
      for (int index = 0; index < lv; ++index)
        num += (int) artifactExpTable[index];
      return num;
    }

    public int GetNextExpFromLevel(int lv)
    {
      int index = Math.Max(Math.Min(lv, this.GetLevelCap()) - 1, 0);
      OInt[] artifactExpTable = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactExpTable();
      if (artifactExpTable == null)
        return 0;
      return (int) artifactExpTable[index];
    }

    public int GetShowExp()
    {
      return (int) this.mExp - this.GetTotalExpFromLevel((int) this.mLv);
    }

    public int GetNextExp()
    {
      return this.GetTotalExpFromLevel((int) this.mLv + 1) - (int) this.mExp;
    }

    public void GainExp(ItemData item, int num)
    {
      if (item == null || item.Num < num)
        return;
      ItemParam itemParam = item.Param;
      if (itemParam == null || itemParam.type != EItemType.ExpUpArtifact)
        return;
      this.GainExp(itemParam.value * num);
      item.Used(num);
    }

    public void GainExp(int exp)
    {
      int totalExpFromLevel = this.GetTotalExpFromLevel(this.GetLevelCap());
      int mLv = (int) this.mLv;
      this.mExp = (OInt) Math.Min((int) this.mExp + exp, totalExpFromLevel);
      this.mLv = (OInt) this.GetLevelFromExp((int) this.mExp);
      if ((int) this.mLv == mLv)
        return;
      this.LevelUp();
    }

    public int GetGainExpCap()
    {
      return this.GetTotalExpFromLevel(this.GetLevelCap());
    }

    public void LevelUp()
    {
      this.UpdateEquipEffect();
      this.UpdateLearningAbilities(false);
    }

    public int GetKakeraCreateNum()
    {
      if (this.mRarityParam == null)
        return 0;
      return (int) this.mRarityParam.ArtifactCreatePieceNum;
    }

    public int GetKakeraNeedNum()
    {
      if (this.mRarityParam == null)
        return 0;
      return (int) this.mRarityParam.ArtifactGouseiPieceNum;
    }

    public int GetKakeraChangeNum()
    {
      if (this.mRarityParam == null)
        return 0;
      return (int) this.mRarityParam.ArtifactChangePieceNum;
    }

    public bool CheckEnableCreate()
    {
      ItemData kakeraData = this.KakeraData;
      return kakeraData != null && kakeraData.Num > 0 && (this.GetKakeraCreateNum() <= kakeraData.Num && MonoSingleton<GameManager>.GetInstanceDirect().Player.Gold >= (int) this.mRarityParam.ArtifactCreateCost);
    }

    public int GetKakeraNumForRarityUp()
    {
      int result = 0;
      Action<ItemData> action = (Action<ItemData>) (itemData =>
      {
        if (itemData == null)
          return;
        result += itemData.Num;
      });
      action(this.KakeraData);
      action(this.RarityKakeraData);
      action(this.CommonKakeraData);
      return result;
    }

    public List<ItemData> GetKakeraDataListForRarityUp()
    {
      List<ItemData> result = new List<ItemData>();
      Action<ItemData> action = (Action<ItemData>) (itemData =>
      {
        if (itemData == null)
          return;
        result.Add(itemData);
      });
      action(this.KakeraData);
      action(this.RarityKakeraData);
      action(this.CommonKakeraData);
      return result;
    }

    public ArtifactData.RarityUpResults CheckEnableRarityUp()
    {
      ArtifactData.RarityUpResults rarityUpResults = ArtifactData.RarityUpResults.Success;
      if ((int) this.mRarity >= (int) this.RarityCap)
        rarityUpResults |= ArtifactData.RarityUpResults.RarityMaxed;
      if ((int) this.mLv < (int) this.LvCap)
        rarityUpResults |= ArtifactData.RarityUpResults.NoLv;
      if (this.GetKakeraNumForRarityUp() < this.GetKakeraNeedNum())
        rarityUpResults |= ArtifactData.RarityUpResults.NoKakera;
      if (MonoSingleton<GameManager>.GetInstanceDirect().Player.Gold < (int) this.mRarityParam.ArtifactRarityUpCost)
        rarityUpResults |= ArtifactData.RarityUpResults.NoGold;
      return rarityUpResults;
    }

    public bool ConsumeKakera(int num)
    {
      int kakeraNumForRarityUp = this.GetKakeraNumForRarityUp();
      if (num < 1 || kakeraNumForRarityUp < num)
        return false;
      ItemData[] itemDataArray = new ItemData[3]
      {
        this.KakeraData,
        this.RarityKakeraData,
        this.CommonKakeraData
      };
      int val2 = num;
      for (int index = 0; index < itemDataArray.Length; ++index)
      {
        if (itemDataArray[index] != null)
        {
          if (val2 >= 1)
          {
            int num1 = Math.Min(itemDataArray[index].Num, val2);
            val2 -= num1;
          }
          else
            break;
        }
      }
      return val2 < 1;
    }

    public void RarityUp()
    {
      if ((int) this.mLv < this.GetLevelCap())
        return;
      int kakeraNumForRarityUp = this.GetKakeraNumForRarityUp();
      int kakeraNeedNum = this.GetKakeraNeedNum();
      if (kakeraNumForRarityUp < kakeraNeedNum)
        return;
      if (!this.ConsumeKakera(kakeraNeedNum))
      {
        DebugUtility.LogWarning("カケラが不足している場合");
      }
      else
      {
        GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
        int artifactRarityUpCost = (int) this.mRarityParam.ArtifactRarityUpCost;
        if (instanceDirect.Player.Gold < artifactRarityUpCost)
          return;
        instanceDirect.Player.GainGold(-artifactRarityUpCost);
        this.mRarity = (OInt) Math.Min(Math.Max((int) (++this.mRarity), this.mArtifactParam.rareini), this.mArtifactParam.raremax);
        this.mRarityParam = instanceDirect.GetRarityParam((int) this.mRarity);
        this.UpdateEquipEffect();
        this.UpdateLearningAbilities(true);
      }
    }

    public int GetSellPrice()
    {
      int sell = this.mArtifactParam.sell;
      int num = 100;
      if (this.mRarityParam != null)
        num = (int) this.mRarityParam.ArtifactCostRate;
      return sell * num / 100;
    }

    public void GetHomePassiveBuffStatus(ref BaseStatus fixed_status, ref BaseStatus scale_status, UnitData user = null, int job_index = 0, bool bCheckCondition = true)
    {
      fixed_status.Clear();
      scale_status.Clear();
      ArtifactData.WorkScaleStatus.Clear();
      SkillData.GetHomePassiveBuffStatus(this.EquipSkill, EElement.None, ref fixed_status, ref ArtifactData.WorkScaleStatus);
      scale_status.Add(ArtifactData.WorkScaleStatus);
      if (this.mLearningAbilities == null)
        return;
      for (int index1 = 0; index1 < this.mLearningAbilities.Count; ++index1)
      {
        AbilityData mLearningAbility = this.mLearningAbilities[index1];
        if ((user == null || mLearningAbility.CheckEnableUseAbility(user, job_index) || bCheckCondition) && mLearningAbility.Skills != null)
        {
          for (int index2 = 0; index2 < mLearningAbility.Skills.Count; ++index2)
          {
            SkillData skill = mLearningAbility.Skills[index2];
            ArtifactData.WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(skill, EElement.None, ref fixed_status, ref ArtifactData.WorkScaleStatus);
            scale_status.Add(ArtifactData.WorkScaleStatus);
          }
        }
      }
    }

    public bool CheckEquiped()
    {
      UnitData unit = (UnitData) null;
      JobData job = (JobData) null;
      return MonoSingleton<GameManager>.Instance.Player.FindOwner(this, out unit, out job);
    }

    [Flags]
    public enum RarityUpResults
    {
      Success = 0,
      NoLv = 1,
      NoGold = 2,
      NoKakera = 4,
      RarityMaxed = 8,
    }
  }
}
