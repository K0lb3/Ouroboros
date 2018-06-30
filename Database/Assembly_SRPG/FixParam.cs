namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class FixParam
    {
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
        public OInt ContinueCoinCostMultiTower;
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
        public Dictionary<EUnitCondition, OInt> DefaultCondTurns;
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
        public int PartyNumMultiTower;
        public int PartyNumOrdeal;
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
        public OString[] EquipCommonPiece;
        public OInt[] EquipCommonPieceNum;
        public OString[] SoulCommonPiece;
        public OInt[] EquipCommonPieceCost;
        public OString[] EquipCmn;
        public OInt AudienceMax;
        public OInt AbilityRankUpPointMax;
        public OInt AbilityRankUpPointAddMax;
        public OInt AbilityRankupPointCoinRate;
        public OInt FirstFriendMax;
        public OInt FirstFriendCoin;
        public OInt CombinationRate;
        public OInt WeakUpRate;
        public OInt ResistDownRate;
        public OInt OrdealCT;
        public OInt EsaAssist;
        public OInt EsaResist;
        public int CardSellMul;
        public int CardExpMul;
        public OInt CardMax;
        public OInt CardTrustMax;
        public OInt CardTrustPileUp;
        public OInt CardAwakeUnlockLevelCap;
        public OInt TobiraLvCap;
        public OInt TobiraUnitLvCapBonus;
        public OString[] TobiraUnlockElem;
        public OString[] TobiraUnlockBirth;
        public OInt IniValRec;
        public OInt GuerrillaVal;
        public OInt DraftSelectSeconds;
        public OInt DraftOrganizeSeconds;
        public OInt DraftPlaceSeconds;

        public FixParam()
        {
            this.DefaultCondTurns = new Dictionary<EUnitCondition, OInt>(Unit.MAX_UNIT_CONDITION);
            base..ctor();
            return;
        }

        public unsafe OInt[] ConvertOIntArray(int[] strs)
        {
            OInt[] numArray;
            int num;
            numArray = null;
            if (strs == null)
            {
                goto Label_0039;
            }
            numArray = new OInt[(int) strs.Length];
            num = 0;
            goto Label_0030;
        Label_0018:
            *(&(numArray[num])) = strs[num];
            num += 1;
        Label_0030:
            if (num < ((int) numArray.Length))
            {
                goto Label_0018;
            }
        Label_0039:
            return numArray;
        }

        public unsafe OString[] ConvertOStringArray(string[] strs)
        {
            OString[] strArray;
            int num;
            strArray = null;
            if (strs == null)
            {
                goto Label_0039;
            }
            strArray = new OString[(int) strs.Length];
            num = 0;
            goto Label_0030;
        Label_0018:
            *(&(strArray[num])) = strs[num];
            num += 1;
        Label_0030:
            if (num < ((int) strArray.Length))
            {
                goto Label_0018;
            }
        Label_0039:
            return strArray;
        }

        public unsafe bool Deserialize(JSON_FixParam json)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.ShopUpdateTime = null;
            this.CriticalRate_Cri_Multiply = json.mulcri;
            this.CriticalRate_Cri_Division = json.divcri;
            this.CriticalRate_Luk_Multiply = json.mulluk;
            this.CriticalRate_Luk_Division = json.divluk;
            this.MinCriticalDamageRate = json.mincri;
            this.MaxCriticalDamageRate = json.maxcri;
            this.HighGridAtkRate = json.hatk;
            this.HighGridDefRate = json.hdef;
            this.HighGridCriRate = json.hcri;
            this.DownGridAtkRate = json.datk;
            this.DownGridDefRate = json.ddef;
            this.DownGridCriRate = json.dcri;
            this.ParalysedRate = json.paralyse;
            this.PoisonDamageRate = json.poi_rate;
            this.BlindnessHitRate = json.bli_hit;
            this.BlindnessAvoidRate = json.bli_avo;
            this.BerserkAtkRate = json.ber_atk;
            this.BerserkDefRate = json.ber_def;
            this.TokkouDamageRate = json.tk_rate;
            this.AbilityRankUpCountCoin = json.abilupcoin;
            this.AbilityRankUpCountMax = json.abilupmax;
            this.AbilityRankUpCountRecoveryVal = json.abiluprec;
            this.AbilityRankUpCountRecoverySec = (long) json.abilupsec;
            this.StaminaRecoveryCoin = json.stmncoin;
            this.StaminaRecoveryVal = json.stmnrec;
            this.StaminaRecoverySec = (long) json.stmnsec;
            this.StaminaStockCap = json.stmncap;
            this.StaminaAdd = json.stmnadd;
            this.StaminaAdd2 = json.stmnadd2;
            this.StaminaAddCost = null;
            if (json.stmncost == null)
            {
                goto Label_025A;
            }
            this.StaminaAddCost = new OInt[(int) json.stmncost.Length];
            num = 0;
            goto Label_024C;
        Label_022A:
            *(&(this.StaminaAddCost[num])) = json.stmncost[num];
            num += 1;
        Label_024C:
            if (num < ((int) json.stmncost.Length))
            {
                goto Label_022A;
            }
        Label_025A:
            this.CaveStaminaMax = json.cavemax;
            this.CaveStaminaRecoveryVal = json.caverec;
            this.CaveStaminaRecoverySec = (long) json.cavesec;
            this.CaveStaminaStockCap = json.cavecap;
            this.CaveStaminaAdd = json.caveadd;
            this.CaveStaminaAddCost = null;
            if (json.cavecost == null)
            {
                goto Label_030C;
            }
            this.CaveStaminaAddCost = new OInt[(int) json.cavecost.Length];
            num2 = 0;
            goto Label_02FE;
        Label_02DC:
            *(&(this.CaveStaminaAddCost[num2])) = json.cavecost[num2];
            num2 += 1;
        Label_02FE:
            if (num2 < ((int) json.cavecost.Length))
            {
                goto Label_02DC;
            }
        Label_030C:
            this.ChallengeArenaMax = json.arenamax;
            this.ChallengeArenaCoolDownSec = (long) json.arenasec;
            this.ArenaMedalMultipler = json.arenamedal;
            this.ArenaCoinRewardMultipler = json.arenacoin;
            this.ArenaResetCooldownCost = json.arenaccost;
            this.ArenaResetTicketCost = null;
            if (json.arenatcost == null)
            {
                goto Label_03BE;
            }
            this.ArenaResetTicketCost = new OInt[(int) json.arenatcost.Length];
            num3 = 0;
            goto Label_03B0;
        Label_038E:
            *(&(this.ArenaResetTicketCost[num3])) = json.arenatcost[num3];
            num3 += 1;
        Label_03B0:
            if (num3 < ((int) json.arenatcost.Length))
            {
                goto Label_038E;
            }
        Label_03BE:
            this.ChallengeTourMax = json.tourmax;
            this.ChallengeMultiMax = json.multimax;
            this.AwakeRate = json.awakerate;
            this.GemsGainNormalAttack = json.na_gems;
            this.GemsGainSideAttack = json.sa_gems;
            this.GemsGainBackAttack = json.ba_gems;
            this.GemsGainWeakAttack = json.wa_gems;
            this.GemsGainCriticalAttack = json.ca_gems;
            this.GemsGainKillBonus = json.ki_gems;
            this.GemsGainDiffFloorCount = json.di_gems_floor;
            this.GemsGainDiffFloorMax = json.di_gems_max;
            this.ElementResistUpRate = json.elem_up;
            this.ElementResistDownRate = json.elem_down;
            this.GemsGainValue = json.gems_gain;
            this.GemsBuffValue = json.gems_buff;
            this.GemsBuffTurn = json.gems_buff_turn;
            this.ContinueCoinCost = json.continue_cost;
            this.ContinueCoinCostMulti = json.continue_cost_multi;
            this.ContinueCoinCostMultiTower = json.continue_cost_multitower;
            this.AvoidBaseRate = json.avoid_rate;
            this.AvoidParamScale = json.avoid_scale;
            this.MaxAvoidRate = json.avoid_rate_max;
            if (json.shop_update_time == null)
            {
                goto Label_0597;
            }
            if (((int) json.shop_update_time.Length) <= 0)
            {
                goto Label_0597;
            }
            this.ShopUpdateTime = new OInt[(int) json.shop_update_time.Length];
            num4 = 0;
            goto Label_0589;
        Label_0567:
            *(&(this.ShopUpdateTime[num4])) = json.shop_update_time[num4];
            num4 += 1;
        Label_0589:
            if (num4 < ((int) this.ShopUpdateTime.Length))
            {
                goto Label_0567;
            }
        Label_0597:
            if (json.products == null)
            {
                goto Label_0600;
            }
            if (((int) json.products.Length) <= 0)
            {
                goto Label_0600;
            }
            this.Products = new OString[(int) json.products.Length];
            num5 = 0;
            goto Label_05F1;
        Label_05CB:
            *(&(this.Products[num5])) = json.products[num5];
            num5 += 1;
        Label_05F1:
            if (num5 < ((int) this.Products.Length))
            {
                goto Label_05CB;
            }
        Label_0600:
            this.VipCardProduct = json.vip_product;
            this.VipCardDate = json.vip_date;
            this.FreeGachaGoldMax = json.ggmax;
            this.FreeGachaGoldCoolDownSec = (long) json.ggsec;
            this.FreeGachaCoinCoolDownSec = (long) json.cgsec;
            this.BuyGoldCost = json.buygoldcost;
            this.BuyGoldAmount = json.buygold;
            this.SupportCost = json.sp_cost;
            this.ChallengeEliteMax = json.elitemax;
            this.EliteResetMax = json.elite_reset_max;
            if (json.elite_reset_cost == null)
            {
                goto Label_0715;
            }
            if (((int) json.elite_reset_cost.Length) <= 0)
            {
                goto Label_0715;
            }
            this.EliteResetCosts = new OInt[(int) json.elite_reset_cost.Length];
            num6 = 0;
            goto Label_0706;
        Label_06E0:
            *(&(this.EliteResetCosts[num6])) = json.elite_reset_cost[num6];
            num6 += 1;
        Label_0706:
            if (num6 < ((int) this.EliteResetCosts.Length))
            {
                goto Label_06E0;
            }
        Label_0715:
            if (this.DefaultCondTurns.ContainsKey(1L) != null)
            {
                goto Label_073A;
            }
            this.DefaultCondTurns.Add(1L, 0);
        Label_073A:
            if (this.DefaultCondTurns.ContainsKey(2L) != null)
            {
                goto Label_075F;
            }
            this.DefaultCondTurns.Add(2L, 0);
        Label_075F:
            if (this.DefaultCondTurns.ContainsKey(4L) != null)
            {
                goto Label_0784;
            }
            this.DefaultCondTurns.Add(4L, 0);
        Label_0784:
            if (this.DefaultCondTurns.ContainsKey(8L) != null)
            {
                goto Label_07A9;
            }
            this.DefaultCondTurns.Add(8L, 0);
        Label_07A9:
            if (this.DefaultCondTurns.ContainsKey(0x10L) != null)
            {
                goto Label_07D0;
            }
            this.DefaultCondTurns.Add(0x10L, 0);
        Label_07D0:
            if (this.DefaultCondTurns.ContainsKey(0x20L) != null)
            {
                goto Label_07F7;
            }
            this.DefaultCondTurns.Add(0x20L, 0);
        Label_07F7:
            if (this.DefaultCondTurns.ContainsKey(0x40L) != null)
            {
                goto Label_081E;
            }
            this.DefaultCondTurns.Add(0x40L, 0);
        Label_081E:
            if (this.DefaultCondTurns.ContainsKey(0x80L) != null)
            {
                goto Label_084B;
            }
            this.DefaultCondTurns.Add(0x80L, 0);
        Label_084B:
            if (this.DefaultCondTurns.ContainsKey(0x100L) != null)
            {
                goto Label_0878;
            }
            this.DefaultCondTurns.Add(0x100L, 0);
        Label_0878:
            if (this.DefaultCondTurns.ContainsKey(0x200L) != null)
            {
                goto Label_08A5;
            }
            this.DefaultCondTurns.Add(0x200L, 0);
        Label_08A5:
            if (this.DefaultCondTurns.ContainsKey(0x400L) != null)
            {
                goto Label_08D2;
            }
            this.DefaultCondTurns.Add(0x400L, 0);
        Label_08D2:
            if (this.DefaultCondTurns.ContainsKey(0x800L) != null)
            {
                goto Label_08FF;
            }
            this.DefaultCondTurns.Add(0x800L, 0);
        Label_08FF:
            if (this.DefaultCondTurns.ContainsKey(0x1000L) != null)
            {
                goto Label_092C;
            }
            this.DefaultCondTurns.Add(0x1000L, 0);
        Label_092C:
            if (this.DefaultCondTurns.ContainsKey(0x2000L) != null)
            {
                goto Label_0959;
            }
            this.DefaultCondTurns.Add(0x2000L, 0);
        Label_0959:
            if (this.DefaultCondTurns.ContainsKey(0x4000L) != null)
            {
                goto Label_0986;
            }
            this.DefaultCondTurns.Add(0x4000L, 0);
        Label_0986:
            if (this.DefaultCondTurns.ContainsKey(0x8000L) != null)
            {
                goto Label_09B3;
            }
            this.DefaultCondTurns.Add(0x8000L, 0);
        Label_09B3:
            if (this.DefaultCondTurns.ContainsKey(0x10000L) != null)
            {
                goto Label_09E0;
            }
            this.DefaultCondTurns.Add(0x10000L, 0);
        Label_09E0:
            if (this.DefaultCondTurns.ContainsKey(0x20000L) != null)
            {
                goto Label_0A0D;
            }
            this.DefaultCondTurns.Add(0x20000L, 0);
        Label_0A0D:
            if (this.DefaultCondTurns.ContainsKey(0x40000L) != null)
            {
                goto Label_0A3A;
            }
            this.DefaultCondTurns.Add(0x40000L, 0);
        Label_0A3A:
            if (this.DefaultCondTurns.ContainsKey(0x80000L) != null)
            {
                goto Label_0A67;
            }
            this.DefaultCondTurns.Add(0x80000L, 0);
        Label_0A67:
            if (this.DefaultCondTurns.ContainsKey(0x100000L) != null)
            {
                goto Label_0A94;
            }
            this.DefaultCondTurns.Add(0x100000L, 0);
        Label_0A94:
            if (this.DefaultCondTurns.ContainsKey(0x200000L) != null)
            {
                goto Label_0AC1;
            }
            this.DefaultCondTurns.Add(0x200000L, 0);
        Label_0AC1:
            if (this.DefaultCondTurns.ContainsKey(0x400000L) != null)
            {
                goto Label_0AEE;
            }
            this.DefaultCondTurns.Add(0x400000L, 0);
        Label_0AEE:
            if (this.DefaultCondTurns.ContainsKey(0x800000L) != null)
            {
                goto Label_0B1B;
            }
            this.DefaultCondTurns.Add(0x800000L, 0);
        Label_0B1B:
            if (this.DefaultCondTurns.ContainsKey(0x1000000L) != null)
            {
                goto Label_0B48;
            }
            this.DefaultCondTurns.Add(0x1000000L, 0);
        Label_0B48:
            if (this.DefaultCondTurns.ContainsKey(0x2000000L) != null)
            {
                goto Label_0B75;
            }
            this.DefaultCondTurns.Add(0x2000000L, 0);
        Label_0B75:
            if (this.DefaultCondTurns.ContainsKey(0x4000000L) != null)
            {
                goto Label_0BA2;
            }
            this.DefaultCondTurns.Add(0x4000000L, 0);
        Label_0BA2:
            if (this.DefaultCondTurns.ContainsKey(0x8000000L) != null)
            {
                goto Label_0BCF;
            }
            this.DefaultCondTurns.Add(0x8000000L, 0);
        Label_0BCF:
            if (this.DefaultCondTurns.ContainsKey(0x10000000L) != null)
            {
                goto Label_0BFC;
            }
            this.DefaultCondTurns.Add(0x10000000L, 0);
        Label_0BFC:
            if (this.DefaultCondTurns.ContainsKey(0x20000000L) != null)
            {
                goto Label_0C29;
            }
            this.DefaultCondTurns.Add(0x20000000L, 0);
        Label_0C29:
            if (this.DefaultCondTurns.ContainsKey(0x40000000L) != null)
            {
                goto Label_0C56;
            }
            this.DefaultCondTurns.Add(0x40000000L, 0);
        Label_0C56:
            if (this.DefaultCondTurns.ContainsKey(0x80000000L) != null)
            {
                goto Label_0C83;
            }
            this.DefaultCondTurns.Add(0x80000000L, 0);
        Label_0C83:
            if (this.DefaultCondTurns.ContainsKey(0x100000000L) != null)
            {
                goto Label_0CB6;
            }
            this.DefaultCondTurns.Add(0x100000000L, 0);
        Label_0CB6:
            if (this.DefaultCondTurns.ContainsKey(0x200000000L) != null)
            {
                goto Label_0CE9;
            }
            this.DefaultCondTurns.Add(0x200000000L, 0);
        Label_0CE9:
            if (this.DefaultCondTurns.ContainsKey(0x400000000L) != null)
            {
                goto Label_0D1C;
            }
            this.DefaultCondTurns.Add(0x400000000L, 0);
        Label_0D1C:
            if (this.DefaultCondTurns.ContainsKey(0x800000000L) != null)
            {
                goto Label_0D4F;
            }
            this.DefaultCondTurns.Add(0x800000000L, 0);
        Label_0D4F:
            if (this.DefaultCondTurns.ContainsKey(0x1000000000L) != null)
            {
                goto Label_0D82;
            }
            this.DefaultCondTurns.Add(0x1000000000L, 0);
        Label_0D82:
            this.DefaultCondTurns[1L] = json.ct_poi;
            this.DefaultCondTurns[2L] = json.ct_par;
            this.DefaultCondTurns[4L] = json.ct_stu;
            this.DefaultCondTurns[8L] = json.ct_sle;
            this.DefaultCondTurns[0x10L] = json.st_cha;
            this.DefaultCondTurns[0x20L] = json.ct_sto;
            this.DefaultCondTurns[0x40L] = json.ct_bli;
            this.DefaultCondTurns[0x80L] = json.ct_dsk;
            this.DefaultCondTurns[0x100L] = json.ct_dmo;
            this.DefaultCondTurns[0x200L] = json.ct_dat;
            this.DefaultCondTurns[0x400L] = json.ct_zom;
            this.DefaultCondTurns[0x800L] = json.ct_dea;
            this.DefaultCondTurns[0x1000L] = json.ct_ber;
            this.DefaultCondTurns[0x2000L] = json.ct_dkn;
            this.DefaultCondTurns[0x4000L] = json.ct_dbu;
            this.DefaultCondTurns[0x8000L] = json.ct_ddb;
            this.DefaultCondTurns[0x10000L] = json.ct_stop;
            this.DefaultCondTurns[0x20000L] = json.ct_fast;
            this.DefaultCondTurns[0x40000L] = json.ct_slow;
            this.DefaultCondTurns[0x80000L] = json.ct_ahe;
            this.DefaultCondTurns[0x100000L] = json.ct_don;
            this.DefaultCondTurns[0x200000L] = json.ct_rag;
            this.DefaultCondTurns[0x400000L] = json.ct_gsl;
            this.DefaultCondTurns[0x800000L] = json.ct_aje;
            this.DefaultCondTurns[0x1000000L] = json.ct_dhe;
            this.DefaultCondTurns[0x2000000L] = json.ct_dsa;
            this.DefaultCondTurns[0x4000000L] = json.ct_daa;
            this.DefaultCondTurns[0x8000000L] = json.ct_ddc;
            this.DefaultCondTurns[0x10000000L] = json.ct_dic;
            this.DefaultCondTurns[0x20000000L] = json.ct_esa;
            this.DefaultCondTurns[0x40000000L] = json.ct_esa;
            this.DefaultCondTurns[0x80000000L] = json.ct_esa;
            this.DefaultCondTurns[0x100000000L] = json.ct_esa;
            this.DefaultCondTurns[0x200000000L] = json.ct_esa;
            this.DefaultCondTurns[0x400000000L] = json.ct_esa;
            this.DefaultCondTurns[0x800000000L] = json.ct_mdh;
            this.DefaultCondTurns[0x1000000000L] = json.ct_mdm;
            this.RandomEffectMax = json.yuragi;
            this.ChargeTimeMax = json.ct_max;
            this.ChargeTimeDecWait = json.ct_wait;
            this.ChargeTimeDecMove = json.ct_mov;
            this.ChargeTimeDecAction = json.ct_act;
            this.AddHitRateSide = json.hit_side;
            this.AddHitRateBack = json.hit_back;
            this.HpAutoHealRate = json.ahhp_rate;
            this.MpAutoHealRate = json.ahmp_rate;
            this.GoodSleepHpHealRate = json.gshp_rate;
            this.GoodSleepMpHealRate = json.gsmp_rate;
            this.HpDyingRate = json.dy_rate;
            this.ZeneiSupportSkillRate = json.zsup_rate;
            this.BeginnerDays = json.beginner_days;
            this.ArtifactBoxCap = json.afcap;
            this.CommonPieceFire = json.cmn_pi_fire;
            this.CommonPieceWater = json.cmn_pi_water;
            this.CommonPieceThunder = json.cmn_pi_thunder;
            this.CommonPieceWind = json.cmn_pi_wind;
            this.CommonPieceShine = json.cmn_pi_shine;
            this.CommonPieceDark = json.cmn_pi_dark;
            this.CommonPieceAll = json.cmn_pi_all;
            this.PartyNumNormal = json.ptnum_nml;
            this.PartyNumEvent = json.ptnum_evnt;
            this.PartyNumMulti = json.ptnum_mlt;
            this.PartyNumArenaAttack = json.ptnum_aatk;
            this.PartyNumArenaDefense = json.ptnum_adef;
            this.PartyNumChQuest = json.ptnum_chq;
            this.PartyNumTower = json.ptnum_tow;
            this.PartyNumVersus = json.ptnum_vs;
            this.PartyNumMultiTower = json.ptnum_mt;
            this.PartyNumOrdeal = json.ptnum_ordeal;
            this.IsDisableSuspend = (json.notsus == 0) == 0;
            this.SuspendSaveInterval = json.sus_int;
            this.IsJobMaster = (json.jobms == 0) == 0;
            this.DefaultDeathCount = json.death_count;
            this.DefaultClockUpValue = json.fast_val;
            this.DefaultClockDownValue = json.slow_val;
            if (json.equip_artifact_slot_unlock == null)
            {
                goto Label_1448;
            }
            if (((int) json.equip_artifact_slot_unlock.Length) <= 0)
            {
                goto Label_1448;
            }
            this.EquipArtifactSlotUnlock = new OInt[(int) json.equip_artifact_slot_unlock.Length];
            num7 = 0;
            goto Label_1439;
        Label_1413:
            *(&(this.EquipArtifactSlotUnlock[num7])) = json.equip_artifact_slot_unlock[num7];
            num7 += 1;
        Label_1439:
            if (num7 < ((int) json.equip_artifact_slot_unlock.Length))
            {
                goto Label_1413;
            }
        Label_1448:
            this.KnockBackHeight = json.kb_gh;
            this.ThrowHeight = json.th_gh;
            if (json.art_rare_pi == null)
            {
                goto Label_14C5;
            }
            this.ArtifactRarePiece = new OString[(int) json.art_rare_pi.Length];
            num8 = 0;
            goto Label_14B6;
        Label_1490:
            *(&(this.ArtifactRarePiece[num8])) = json.art_rare_pi[num8];
            num8 += 1;
        Label_14B6:
            if (num8 < ((int) this.ArtifactRarePiece.Length))
            {
                goto Label_1490;
            }
        Label_14C5:
            this.ArtifactCommonPiece = json.art_cmn_pi;
            this.SoulCommonPiece = this.ConvertOStringArray(json.soul_rare);
            this.EquipCommonPiece = this.ConvertOStringArray(json.equ_rare_pi);
            this.EquipCommonPieceNum = this.ConvertOIntArray(json.equ_rare_pi_use);
            this.EquipCommonPieceCost = this.ConvertOIntArray(json.equ_rare_cost);
            this.EquipCmn = this.ConvertOStringArray(json.equip_cmn);
            this.AudienceMax = json.aud_max;
            this.AbilityRankUpPointMax = json.ab_rankup_max;
            this.AbilityRankUpPointAddMax = json.ab_rankup_addmax;
            this.AbilityRankupPointCoinRate = json.ab_coin_convert;
            this.FirstFriendMax = json.firstfriend_max;
            this.FirstFriendCoin = json.firstfriend_coin;
            this.CombinationRate = json.cmb_rate;
            this.WeakUpRate = json.weak_up;
            this.ResistDownRate = json.resist_dw;
            this.OrdealCT = json.ordeal_ct;
            this.EsaAssist = json.esa_assist;
            this.EsaResist = json.esa_resist;
            this.CardSellMul = json.card_sell_mul;
            this.CardExpMul = json.card_exp_mul;
            this.CardMax = json.card_max;
            this.CardTrustMax = json.card_trust_max;
            this.CardTrustPileUp = json.card_trust_en_bonus;
            this.CardAwakeUnlockLevelCap = json.card_awake_unlock_lvcap;
            this.TobiraLvCap = json.tobira_lv_cap;
            this.TobiraUnitLvCapBonus = json.tobira_unit_lv_cap;
            this.TobiraUnlockElem = new OString[(int) json.tobira_unlock_elem.Length];
            num9 = 0;
            goto Label_16BB;
        Label_1695:
            *(&(this.TobiraUnlockElem[num9])) = json.tobira_unlock_elem[num9];
            num9 += 1;
        Label_16BB:
            if (num9 < ((int) this.TobiraUnlockElem.Length))
            {
                goto Label_1695;
            }
            this.TobiraUnlockBirth = new OString[(int) json.tobira_unlock_birth.Length];
            num10 = 0;
            goto Label_170B;
        Label_16E5:
            *(&(this.TobiraUnlockBirth[num10])) = json.tobira_unlock_birth[num10];
            num10 += 1;
        Label_170B:
            if (num10 < ((int) this.TobiraUnlockBirth.Length))
            {
                goto Label_16E5;
            }
            this.IniValRec = json.ini_rec;
            this.GuerrillaVal = json.guerrilla_val;
            this.DraftSelectSeconds = json.draft_select_sec;
            this.DraftOrganizeSeconds = json.draft_organize_sec;
            this.DraftPlaceSeconds = json.draft_place_sec;
            return 1;
        }
    }
}

