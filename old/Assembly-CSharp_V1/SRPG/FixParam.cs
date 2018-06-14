// Decompiled with JetBrains decompiler
// Type: SRPG.FixParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class FixParam
  {
    public Dictionary<EUnitCondition, OInt> DefaultCondTurns = new Dictionary<EUnitCondition, OInt>((int) Unit.MAX_UNIT_CONDITION);
    public OInt CriticalRate_Cri_Multiply;
    public OInt CriticalRate_Cri_Division;
    public OInt CriticalRate_Luk_Multiply;
    public OInt CriticalRate_Luk_Division;
    public OInt MinCriticalDamageRate;
    public OInt MaxCriticalDamageRate;
    public OInt HighGridAtkRate;
    public OInt HighGridDefRate;
    public OInt HighGridCriRate;
    public OInt DownGridAtkRate;
    public OInt DownGridDefRate;
    public OInt DownGridCriRate;
    public OInt ParalysedRate;
    public OInt PoisonDamageRate;
    public OInt BlindnessHitRate;
    public OInt BlindnessAvoidRate;
    public OInt BerserkAtkRate;
    public OInt BerserkDefRate;
    public OInt TokkouDamageRate;
    public OInt AbilityRankUpCountCoin;
    public OInt AbilityRankUpCountMax;
    public OInt AbilityRankUpCountRecoveryVal;
    public OLong AbilityRankUpCountRecoverySec;
    public OInt StaminaRecoveryCoin;
    public OInt StaminaRecoveryVal;
    public OLong StaminaRecoverySec;
    public OInt StaminaStockCap;
    public OInt StaminaAdd;
    public OInt StaminaAdd2;
    public OInt[] StaminaAddCost;
    public OInt CaveStaminaMax;
    public OInt CaveStaminaRecoveryVal;
    public OLong CaveStaminaRecoverySec;
    public OInt CaveStaminaStockCap;
    public OInt CaveStaminaAdd;
    public OInt[] CaveStaminaAddCost;
    public OInt ChallengeArenaMax;
    public OLong ChallengeArenaCoolDownSec;
    public OInt ArenaMedalMultipler;
    public OInt ArenaCoinRewardMultipler;
    public OInt ArenaResetCooldownCost;
    public OInt[] ArenaResetTicketCost;
    public OInt ChallengeTourMax;
    public OInt ChallengeMultiMax;
    public OInt AwakeRate;
    public OInt GemsGainNormalAttack;
    public OInt GemsGainSideAttack;
    public OInt GemsGainBackAttack;
    public OInt GemsGainWeakAttack;
    public OInt GemsGainCriticalAttack;
    public OInt GemsGainKillBonus;
    public OInt GemsGainDiffFloorCount;
    public OInt GemsGainDiffFloorMax;
    public OInt ElementResistUpRate;
    public OInt ElementResistDownRate;
    public OInt GemsGainValue;
    public OInt GemsBuffValue;
    public OInt GemsBuffTurn;
    public OInt[] ShopUpdateTime;
    public OInt ContinueCoinCost;
    public OInt ContinueCoinCostMulti;
    public OInt AvoidBaseRate;
    public OInt AvoidParamScale;
    public OInt MaxAvoidRate;
    public OString[] Products;
    public OString VipCardProduct;
    public OInt VipCardDate;
    public OInt FreeGachaGoldMax;
    public OLong FreeGachaGoldCoolDownSec;
    public OLong FreeGachaCoinCoolDownSec;
    public OInt BuyGoldCost;
    public OInt BuyGoldAmount;
    public OInt SupportCost;
    public OInt ChallengeEliteMax;
    public OInt[] EliteResetCosts;
    public OInt EliteResetMax;
    public OInt RandomEffectMax;
    public OInt ChargeTimeMax;
    public OInt ChargeTimeDecWait;
    public OInt ChargeTimeDecMove;
    public OInt ChargeTimeDecAction;
    public OInt AddHitRateSide;
    public OInt AddHitRateBack;
    public OInt HpAutoHealRate;
    public OInt MpAutoHealRate;
    public OInt GoodSleepHpHealRate;
    public OInt GoodSleepMpHealRate;
    public OInt HpDyingRate;
    public OInt ZeneiSupportSkillRate;
    public OInt BeginnerDays;
    public OInt ArtifactBoxCap;
    public OString CommonPieceFire;
    public OString CommonPieceWater;
    public OString CommonPieceThunder;
    public OString CommonPieceWind;
    public OString CommonPieceShine;
    public OString CommonPieceDark;
    public OString CommonPieceAll;
    public int PartyNumNormal;
    public int PartyNumEvent;
    public int PartyNumMulti;
    public int PartyNumArenaAttack;
    public int PartyNumArenaDefense;
    public int PartyNumChQuest;
    public int PartyNumTower;
    public int PartyNumVersus;
    public OBool IsDisableSuspend;
    public OInt SuspendSaveInterval;
    public bool IsJobMaster;
    public OInt DefaultDeathCount;
    public OInt DefaultClockUpValue;
    public OInt DefaultClockDownValue;
    public OInt[] EquipArtifactSlotUnlock;
    public OInt KnockBackHeight;
    public OInt ThrowHeight;
    public OString[] ArtifactRarePiece;
    public OString ArtifactCommonPiece;

    public bool Deserialize(JSON_FixParam json)
    {
      if (json == null)
        return false;
      this.ShopUpdateTime = (OInt[]) null;
      this.CriticalRate_Cri_Multiply = (OInt) json.mulcri;
      this.CriticalRate_Cri_Division = (OInt) json.divcri;
      this.CriticalRate_Luk_Multiply = (OInt) json.mulluk;
      this.CriticalRate_Luk_Division = (OInt) json.divluk;
      this.MinCriticalDamageRate = (OInt) json.mincri;
      this.MaxCriticalDamageRate = (OInt) json.maxcri;
      this.HighGridAtkRate = (OInt) json.hatk;
      this.HighGridDefRate = (OInt) json.hdef;
      this.HighGridCriRate = (OInt) json.hcri;
      this.DownGridAtkRate = (OInt) json.datk;
      this.DownGridDefRate = (OInt) json.ddef;
      this.DownGridCriRate = (OInt) json.dcri;
      this.ParalysedRate = (OInt) json.paralyse;
      this.PoisonDamageRate = (OInt) json.poi_rate;
      this.BlindnessHitRate = (OInt) json.bli_hit;
      this.BlindnessAvoidRate = (OInt) json.bli_avo;
      this.BerserkAtkRate = (OInt) json.ber_atk;
      this.BerserkDefRate = (OInt) json.ber_def;
      this.TokkouDamageRate = (OInt) json.tk_rate;
      this.AbilityRankUpCountCoin = (OInt) json.abilupcoin;
      this.AbilityRankUpCountMax = (OInt) json.abilupmax;
      this.AbilityRankUpCountRecoveryVal = (OInt) json.abiluprec;
      this.AbilityRankUpCountRecoverySec = (OLong) ((long) json.abilupsec);
      this.StaminaRecoveryCoin = (OInt) json.stmncoin;
      this.StaminaRecoveryVal = (OInt) json.stmnrec;
      this.StaminaRecoverySec = (OLong) ((long) json.stmnsec);
      this.StaminaStockCap = (OInt) json.stmncap;
      this.StaminaAdd = (OInt) json.stmnadd;
      this.StaminaAdd2 = (OInt) json.stmnadd2;
      this.StaminaAddCost = (OInt[]) null;
      if (json.stmncost != null)
      {
        this.StaminaAddCost = new OInt[json.stmncost.Length];
        for (int index = 0; index < json.stmncost.Length; ++index)
          this.StaminaAddCost[index] = (OInt) json.stmncost[index];
      }
      this.CaveStaminaMax = (OInt) json.cavemax;
      this.CaveStaminaRecoveryVal = (OInt) json.caverec;
      this.CaveStaminaRecoverySec = (OLong) ((long) json.cavesec);
      this.CaveStaminaStockCap = (OInt) json.cavecap;
      this.CaveStaminaAdd = (OInt) json.caveadd;
      this.CaveStaminaAddCost = (OInt[]) null;
      if (json.cavecost != null)
      {
        this.CaveStaminaAddCost = new OInt[json.cavecost.Length];
        for (int index = 0; index < json.cavecost.Length; ++index)
          this.CaveStaminaAddCost[index] = (OInt) json.cavecost[index];
      }
      this.ChallengeArenaMax = (OInt) json.arenamax;
      this.ChallengeArenaCoolDownSec = (OLong) ((long) json.arenasec);
      this.ArenaMedalMultipler = (OInt) json.arenamedal;
      this.ArenaCoinRewardMultipler = (OInt) json.arenacoin;
      this.ArenaResetCooldownCost = (OInt) json.arenaccost;
      this.ArenaResetTicketCost = (OInt[]) null;
      if (json.arenatcost != null)
      {
        this.ArenaResetTicketCost = new OInt[json.arenatcost.Length];
        for (int index = 0; index < json.arenatcost.Length; ++index)
          this.ArenaResetTicketCost[index] = (OInt) json.arenatcost[index];
      }
      this.ChallengeTourMax = (OInt) json.tourmax;
      this.ChallengeMultiMax = (OInt) json.multimax;
      this.AwakeRate = (OInt) json.awakerate;
      this.GemsGainNormalAttack = (OInt) json.na_gems;
      this.GemsGainSideAttack = (OInt) json.sa_gems;
      this.GemsGainBackAttack = (OInt) json.ba_gems;
      this.GemsGainWeakAttack = (OInt) json.wa_gems;
      this.GemsGainCriticalAttack = (OInt) json.ca_gems;
      this.GemsGainKillBonus = (OInt) json.ki_gems;
      this.GemsGainDiffFloorCount = (OInt) json.di_gems_floor;
      this.GemsGainDiffFloorMax = (OInt) json.di_gems_max;
      this.ElementResistUpRate = (OInt) json.elem_up;
      this.ElementResistDownRate = (OInt) json.elem_down;
      this.GemsGainValue = (OInt) json.gems_gain;
      this.GemsBuffValue = (OInt) json.gems_buff;
      this.GemsBuffTurn = (OInt) json.gems_buff_turn;
      this.ContinueCoinCost = (OInt) json.continue_cost;
      this.ContinueCoinCostMulti = (OInt) json.continue_cost_multi;
      this.AvoidBaseRate = (OInt) json.avoid_rate;
      this.AvoidParamScale = (OInt) json.avoid_scale;
      this.MaxAvoidRate = (OInt) json.avoid_rate_max;
      if (json.shop_update_time != null && json.shop_update_time.Length > 0)
      {
        this.ShopUpdateTime = new OInt[json.shop_update_time.Length];
        for (int index = 0; index < this.ShopUpdateTime.Length; ++index)
          this.ShopUpdateTime[index] = (OInt) json.shop_update_time[index];
      }
      if (json.products != null && json.products.Length > 0)
      {
        this.Products = new OString[json.products.Length];
        for (int index = 0; index < this.Products.Length; ++index)
          this.Products[index] = (OString) json.products[index];
      }
      this.VipCardProduct = (OString) json.vip_product;
      this.VipCardDate = (OInt) json.vip_date;
      this.FreeGachaGoldMax = (OInt) json.ggmax;
      this.FreeGachaGoldCoolDownSec = (OLong) ((long) json.ggsec);
      this.FreeGachaCoinCoolDownSec = (OLong) ((long) json.cgsec);
      this.BuyGoldCost = (OInt) json.buygoldcost;
      this.BuyGoldAmount = (OInt) json.buygold;
      this.SupportCost = (OInt) json.sp_cost;
      this.ChallengeEliteMax = (OInt) json.elitemax;
      this.EliteResetMax = (OInt) json.elite_reset_max;
      if (json.elite_reset_cost != null && json.elite_reset_cost.Length > 0)
      {
        this.EliteResetCosts = new OInt[json.elite_reset_cost.Length];
        for (int index = 0; index < this.EliteResetCosts.Length; ++index)
          this.EliteResetCosts[index] = (OInt) json.elite_reset_cost[index];
      }
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Poison))
        this.DefaultCondTurns.Add(EUnitCondition.Poison, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Paralysed))
        this.DefaultCondTurns.Add(EUnitCondition.Paralysed, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Stun))
        this.DefaultCondTurns.Add(EUnitCondition.Stun, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Sleep))
        this.DefaultCondTurns.Add(EUnitCondition.Sleep, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Charm))
        this.DefaultCondTurns.Add(EUnitCondition.Charm, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Stone))
        this.DefaultCondTurns.Add(EUnitCondition.Stone, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Blindness))
        this.DefaultCondTurns.Add(EUnitCondition.Blindness, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableSkill))
        this.DefaultCondTurns.Add(EUnitCondition.DisableSkill, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableMove))
        this.DefaultCondTurns.Add(EUnitCondition.DisableMove, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableAttack))
        this.DefaultCondTurns.Add(EUnitCondition.DisableAttack, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Zombie))
        this.DefaultCondTurns.Add(EUnitCondition.Zombie, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DeathSentence))
        this.DefaultCondTurns.Add(EUnitCondition.DeathSentence, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Berserk))
        this.DefaultCondTurns.Add(EUnitCondition.Berserk, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableKnockback))
        this.DefaultCondTurns.Add(EUnitCondition.DisableKnockback, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableBuff))
        this.DefaultCondTurns.Add(EUnitCondition.DisableBuff, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableDebuff))
        this.DefaultCondTurns.Add(EUnitCondition.DisableDebuff, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Stop))
        this.DefaultCondTurns.Add(EUnitCondition.Stop, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Fast))
        this.DefaultCondTurns.Add(EUnitCondition.Fast, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Slow))
        this.DefaultCondTurns.Add(EUnitCondition.Slow, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.AutoHeal))
        this.DefaultCondTurns.Add(EUnitCondition.AutoHeal, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Donsoku))
        this.DefaultCondTurns.Add(EUnitCondition.Donsoku, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Rage))
        this.DefaultCondTurns.Add(EUnitCondition.Rage, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.GoodSleep))
        this.DefaultCondTurns.Add(EUnitCondition.GoodSleep, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.AutoJewel))
        this.DefaultCondTurns.Add(EUnitCondition.AutoJewel, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableHeal))
        this.DefaultCondTurns.Add(EUnitCondition.DisableHeal, (OInt) 0);
      this.DefaultCondTurns[EUnitCondition.Poison] = (OInt) json.ct_poi;
      this.DefaultCondTurns[EUnitCondition.Paralysed] = (OInt) json.ct_par;
      this.DefaultCondTurns[EUnitCondition.Stun] = (OInt) json.ct_stu;
      this.DefaultCondTurns[EUnitCondition.Sleep] = (OInt) json.ct_sle;
      this.DefaultCondTurns[EUnitCondition.Charm] = (OInt) json.st_cha;
      this.DefaultCondTurns[EUnitCondition.Stone] = (OInt) json.ct_sto;
      this.DefaultCondTurns[EUnitCondition.Blindness] = (OInt) json.ct_bli;
      this.DefaultCondTurns[EUnitCondition.DisableSkill] = (OInt) json.ct_dsk;
      this.DefaultCondTurns[EUnitCondition.DisableMove] = (OInt) json.ct_dmo;
      this.DefaultCondTurns[EUnitCondition.DisableAttack] = (OInt) json.ct_dat;
      this.DefaultCondTurns[EUnitCondition.Zombie] = (OInt) json.ct_zom;
      this.DefaultCondTurns[EUnitCondition.DeathSentence] = (OInt) json.ct_dea;
      this.DefaultCondTurns[EUnitCondition.Berserk] = (OInt) json.ct_ber;
      this.DefaultCondTurns[EUnitCondition.DisableKnockback] = (OInt) json.ct_dkn;
      this.DefaultCondTurns[EUnitCondition.DisableBuff] = (OInt) json.ct_dbu;
      this.DefaultCondTurns[EUnitCondition.DisableDebuff] = (OInt) json.ct_ddb;
      this.DefaultCondTurns[EUnitCondition.Stop] = (OInt) json.ct_stop;
      this.DefaultCondTurns[EUnitCondition.Fast] = (OInt) json.ct_fast;
      this.DefaultCondTurns[EUnitCondition.Slow] = (OInt) json.ct_slow;
      this.DefaultCondTurns[EUnitCondition.AutoHeal] = (OInt) json.ct_ahe;
      this.DefaultCondTurns[EUnitCondition.Donsoku] = (OInt) json.ct_don;
      this.DefaultCondTurns[EUnitCondition.Rage] = (OInt) json.ct_rag;
      this.DefaultCondTurns[EUnitCondition.GoodSleep] = (OInt) json.ct_gsl;
      this.DefaultCondTurns[EUnitCondition.AutoJewel] = (OInt) json.ct_aje;
      this.DefaultCondTurns[EUnitCondition.DisableHeal] = (OInt) json.ct_dhe;
      this.RandomEffectMax = (OInt) json.yuragi;
      this.ChargeTimeMax = (OInt) json.ct_max;
      this.ChargeTimeDecWait = (OInt) json.ct_wait;
      this.ChargeTimeDecMove = (OInt) json.ct_mov;
      this.ChargeTimeDecAction = (OInt) json.ct_act;
      this.AddHitRateSide = (OInt) json.hit_side;
      this.AddHitRateBack = (OInt) json.hit_back;
      this.HpAutoHealRate = (OInt) json.ahhp_rate;
      this.MpAutoHealRate = (OInt) json.ahmp_rate;
      this.GoodSleepHpHealRate = (OInt) json.gshp_rate;
      this.GoodSleepMpHealRate = (OInt) json.gsmp_rate;
      this.HpDyingRate = (OInt) json.dy_rate;
      this.ZeneiSupportSkillRate = (OInt) json.zsup_rate;
      this.BeginnerDays = (OInt) json.beginner_days;
      this.ArtifactBoxCap = (OInt) json.afcap;
      this.CommonPieceFire = (OString) json.cmn_pi_fire;
      this.CommonPieceWater = (OString) json.cmn_pi_water;
      this.CommonPieceThunder = (OString) json.cmn_pi_thunder;
      this.CommonPieceWind = (OString) json.cmn_pi_wind;
      this.CommonPieceShine = (OString) json.cmn_pi_shine;
      this.CommonPieceDark = (OString) json.cmn_pi_dark;
      this.CommonPieceAll = (OString) json.cmn_pi_all;
      this.PartyNumNormal = json.ptnum_nml;
      this.PartyNumEvent = json.ptnum_evnt;
      this.PartyNumMulti = json.ptnum_mlt;
      this.PartyNumArenaAttack = json.ptnum_aatk;
      this.PartyNumArenaDefense = json.ptnum_adef;
      this.PartyNumChQuest = json.ptnum_chq;
      this.PartyNumTower = json.ptnum_tow;
      this.PartyNumVersus = json.ptnum_vs;
      this.IsDisableSuspend = (OBool) (json.notsus != 0);
      this.SuspendSaveInterval = (OInt) json.sus_int;
      this.IsJobMaster = json.jobms != 0;
      this.DefaultDeathCount = (OInt) json.death_count;
      this.DefaultClockUpValue = (OInt) json.fast_val;
      this.DefaultClockDownValue = (OInt) json.slow_val;
      if (json.equip_artifact_slot_unlock != null && json.equip_artifact_slot_unlock.Length > 0)
      {
        this.EquipArtifactSlotUnlock = new OInt[json.equip_artifact_slot_unlock.Length];
        for (int index = 0; index < json.equip_artifact_slot_unlock.Length; ++index)
          this.EquipArtifactSlotUnlock[index] = (OInt) json.equip_artifact_slot_unlock[index];
      }
      this.KnockBackHeight = (OInt) json.kb_gh;
      this.ThrowHeight = (OInt) json.th_gh;
      if (json.art_rare_pi != null)
      {
        this.ArtifactRarePiece = new OString[json.art_rare_pi.Length];
        for (int index = 0; index < this.ArtifactRarePiece.Length; ++index)
          this.ArtifactRarePiece[index] = (OString) json.art_rare_pi[index];
      }
      this.ArtifactCommonPiece = (OString) json.art_cmn_pi;
      return true;
    }
  }
}
