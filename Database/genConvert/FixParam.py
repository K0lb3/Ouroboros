def FixParam(json):
    this={}#FixParamjson)
    #returnfalse
    if 'mulcri' in json:
        this['CriticalRate_Cri_Multiply'] = json['mulcri']
    if 'divcri' in json:
        this['CriticalRate_Cri_Division'] = json['divcri']
    if 'mulluk' in json:
        this['CriticalRate_Luk_Multiply'] = json['mulluk']
    if 'divluk' in json:
        this['CriticalRate_Luk_Division'] = json['divluk']
    if 'mincri' in json:
        this['MinCriticalDamageRate'] = json['mincri']
    if 'maxcri' in json:
        this['MaxCriticalDamageRate'] = json['maxcri']
    if 'hatk' in json:
        this['HighGridAtkRate'] = json['hatk']
    if 'hdef' in json:
        this['HighGridDefRate'] = json['hdef']
    if 'hcri' in json:
        this['HighGridCriRate'] = json['hcri']
    if 'datk' in json:
        this['DownGridAtkRate'] = json['datk']
    if 'ddef' in json:
        this['DownGridDefRate'] = json['ddef']
    if 'dcri' in json:
        this['DownGridCriRate'] = json['dcri']
    if 'paralyse' in json:
        this['ParalysedRate'] = json['paralyse']
    if 'poi_rate' in json:
        this['PoisonDamageRate'] = json['poi_rate']
    if 'bli_hit' in json:
        this['BlindnessHitRate'] = json['bli_hit']
    if 'bli_avo' in json:
        this['BlindnessAvoidRate'] = json['bli_avo']
    if 'ber_atk' in json:
        this['BerserkAtkRate'] = json['ber_atk']
    if 'ber_def' in json:
        this['BerserkDefRate'] = json['ber_def']
    if 'tk_rate' in json:
        this['TokkouDamageRate'] = json['tk_rate']
    if 'abilupcoin' in json:
        this['AbilityRankUpCountCoin'] = json['abilupcoin']
    if 'abilupmax' in json:
        this['AbilityRankUpCountMax'] = json['abilupmax']
    if 'abiluprec' in json:
        this['AbilityRankUpCountRecoveryVal'] = json['abiluprec']
    if 'abilupsec' in json:
        this['AbilityRankUpCountRecoverySec'] = ((long)json['abilupsec'])
    if 'stmncoin' in json:
        this['StaminaRecoveryCoin'] = json['stmncoin']
    if 'stmnrec' in json:
        this['StaminaRecoveryVal'] = json['stmnrec']
    if 'stmnsec' in json:
        this['StaminaRecoverySec'] = ((long)json['stmnsec'])
    if 'stmncap' in json:
        this['StaminaStockCap'] = json['stmncap']
    if 'stmnadd' in json:
        this['StaminaAdd'] = json['stmnadd']
    if 'stmnadd2' in json:
        this['StaminaAdd2'] = json['stmnadd2']
        if 'stmncost' in json:
            this['StaminaAddCost'] = newOInt[json['stmncost'].Length]
        if 'stmncost' in json:
            this['StaminaAddCost[index]'] = json['stmncost'][index]
    if 'cavemax' in json:
        this['CaveStaminaMax'] = json['cavemax']
    if 'caverec' in json:
        this['CaveStaminaRecoveryVal'] = json['caverec']
    if 'cavesec' in json:
        this['CaveStaminaRecoverySec'] = ((long)json['cavesec'])
    if 'cavecap' in json:
        this['CaveStaminaStockCap'] = json['cavecap']
    if 'caveadd' in json:
        this['CaveStaminaAdd'] = json['caveadd']
        if 'cavecost' in json:
            this['CaveStaminaAddCost'] = newOInt[json['cavecost'].Length]
        if 'cavecost' in json:
            this['CaveStaminaAddCost[index]'] = json['cavecost'][index]
    if 'arenamax' in json:
        this['ChallengeArenaMax'] = json['arenamax']
    if 'arenasec' in json:
        this['ChallengeArenaCoolDownSec'] = ((long)json['arenasec'])
    if 'arenamedal' in json:
        this['ArenaMedalMultipler'] = json['arenamedal']
    if 'arenacoin' in json:
        this['ArenaCoinRewardMultipler'] = json['arenacoin']
    if 'arenaccost' in json:
        this['ArenaResetCooldownCost'] = json['arenaccost']
        if 'arenatcost' in json:
            this['ArenaResetTicketCost'] = newOInt[json['arenatcost'].Length]
        if 'arenatcost' in json:
            this['ArenaResetTicketCost[index]'] = json['arenatcost'][index]
    if 'tourmax' in json:
        this['ChallengeTourMax'] = json['tourmax']
    if 'multimax' in json:
        this['ChallengeMultiMax'] = json['multimax']
    if 'awakerate' in json:
        this['AwakeRate'] = json['awakerate']
    if 'na_gems' in json:
        this['GemsGainNormalAttack'] = json['na_gems']
    if 'sa_gems' in json:
        this['GemsGainSideAttack'] = json['sa_gems']
    if 'ba_gems' in json:
        this['GemsGainBackAttack'] = json['ba_gems']
    if 'wa_gems' in json:
        this['GemsGainWeakAttack'] = json['wa_gems']
    if 'ca_gems' in json:
        this['GemsGainCriticalAttack'] = json['ca_gems']
    if 'ki_gems' in json:
        this['GemsGainKillBonus'] = json['ki_gems']
    if 'di_gems_floor' in json:
        this['GemsGainDiffFloorCount'] = json['di_gems_floor']
    if 'di_gems_max' in json:
        this['GemsGainDiffFloorMax'] = json['di_gems_max']
    if 'elem_up' in json:
        this['ElementResistUpRate'] = json['elem_up']
    if 'elem_down' in json:
        this['ElementResistDownRate'] = json['elem_down']
    if 'gems_gain' in json:
        this['GemsGainValue'] = json['gems_gain']
    if 'gems_buff' in json:
        this['GemsBuffValue'] = json['gems_buff']
    if 'gems_buff_turn' in json:
        this['GemsBuffTurn'] = json['gems_buff_turn']
    if 'continue_cost' in json:
        this['ContinueCoinCost'] = json['continue_cost']
    if 'continue_cost_multi' in json:
        this['ContinueCoinCostMulti'] = json['continue_cost_multi']
    if 'continue_cost_multitower' in json:
        this['ContinueCoinCostMultiTower'] = json['continue_cost_multitower']
    if 'avoid_rate' in json:
        this['AvoidBaseRate'] = json['avoid_rate']
    if 'avoid_scale' in json:
        this['AvoidParamScale'] = json['avoid_scale']
    if 'avoid_rate_max' in json:
        this['MaxAvoidRate'] = json['avoid_rate_max']
        if 'shop_update_time' in json:
            this['ShopUpdateTime'] = newOInt[json['shop_update_time'].Length]
        if 'shop_update_time' in json:
            this['ShopUpdateTime[index]'] = json['shop_update_time'][index]
        if 'products' in json:
            this['Products'] = newOString[json['products'].Length]
        if 'products' in json:
            this['Products[index]'] = json['products'][index]
    if 'vip_product' in json:
        this['VipCardProduct'] = json['vip_product']
    if 'vip_date' in json:
        this['VipCardDate'] = json['vip_date']
    if 'ggmax' in json:
        this['FreeGachaGoldMax'] = json['ggmax']
    if 'ggsec' in json:
        this['FreeGachaGoldCoolDownSec'] = ((long)json['ggsec'])
    if 'cgsec' in json:
        this['FreeGachaCoinCoolDownSec'] = ((long)json['cgsec'])
    if 'buygoldcost' in json:
        this['BuyGoldCost'] = json['buygoldcost']
    if 'buygold' in json:
        this['BuyGoldAmount'] = json['buygold']
    if 'sp_cost' in json:
        this['SupportCost'] = json['sp_cost']
    if 'elitemax' in json:
        this['ChallengeEliteMax'] = json['elitemax']
    if 'elite_reset_max' in json:
        this['EliteResetMax'] = json['elite_reset_max']
        if 'elite_reset_cost' in json:
            this['EliteResetCosts'] = newOInt[json['elite_reset_cost'].Length]
        if 'elite_reset_cost' in json:
            this['EliteResetCosts[index]'] = json['elite_reset_cost'][index]
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.Poison))
    #this.DefaultCondTurns.Add(EUnitCondition.Poison,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.Paralysed))
    #this.DefaultCondTurns.Add(EUnitCondition.Paralysed,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.Stun))
    #this.DefaultCondTurns.Add(EUnitCondition.Stun,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.Sleep))
    #this.DefaultCondTurns.Add(EUnitCondition.Sleep,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.Charm))
    #this.DefaultCondTurns.Add(EUnitCondition.Charm,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.Stone))
    #this.DefaultCondTurns.Add(EUnitCondition.Stone,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.Blindness))
    #this.DefaultCondTurns.Add(EUnitCondition.Blindness,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableSkill))
    #this.DefaultCondTurns.Add(EUnitCondition.DisableSkill,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableMove))
    #this.DefaultCondTurns.Add(EUnitCondition.DisableMove,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableAttack))
    #this.DefaultCondTurns.Add(EUnitCondition.DisableAttack,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.Zombie))
    #this.DefaultCondTurns.Add(EUnitCondition.Zombie,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.DeathSentence))
    #this.DefaultCondTurns.Add(EUnitCondition.DeathSentence,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.Berserk))
    #this.DefaultCondTurns.Add(EUnitCondition.Berserk,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableKnockback))
    #this.DefaultCondTurns.Add(EUnitCondition.DisableKnockback,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableBuff))
    #this.DefaultCondTurns.Add(EUnitCondition.DisableBuff,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableDebuff))
    #this.DefaultCondTurns.Add(EUnitCondition.DisableDebuff,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.Stop))
    #this.DefaultCondTurns.Add(EUnitCondition.Stop,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.Fast))
    #this.DefaultCondTurns.Add(EUnitCondition.Fast,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.Slow))
    #this.DefaultCondTurns.Add(EUnitCondition.Slow,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.AutoHeal))
    #this.DefaultCondTurns.Add(EUnitCondition.AutoHeal,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.Donsoku))
    #this.DefaultCondTurns.Add(EUnitCondition.Donsoku,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.Rage))
    #this.DefaultCondTurns.Add(EUnitCondition.Rage,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.GoodSleep))
    #this.DefaultCondTurns.Add(EUnitCondition.GoodSleep,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.AutoJewel))
    #this.DefaultCondTurns.Add(EUnitCondition.AutoJewel,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableHeal))
    #this.DefaultCondTurns.Add(EUnitCondition.DisableHeal,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableSingleAttack))
    #this.DefaultCondTurns.Add(EUnitCondition.DisableSingleAttack,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableAreaAttack))
    #this.DefaultCondTurns.Add(EUnitCondition.DisableAreaAttack,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableDecCT))
    #this.DefaultCondTurns.Add(EUnitCondition.DisableDecCT,(OInt)0)
    #if(!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableIncCT))
    #this.DefaultCondTurns.Add(EUnitCondition.DisableIncCT,(OInt)0)
    this['']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_poi' in json:
        this['DefaultCondTurns[EUnitCondition']['Poison]'] = json['ct_poi']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_par' in json:
        this['DefaultCondTurns[EUnitCondition']['Paralysed]'] = json['ct_par']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_stu' in json:
        this['DefaultCondTurns[EUnitCondition']['Stun]'] = json['ct_stu']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_sle' in json:
        this['DefaultCondTurns[EUnitCondition']['Sleep]'] = json['ct_sle']
    this['DefaultCondTurns[EUnitCondition']
    if 'st_cha' in json:
        this['DefaultCondTurns[EUnitCondition']['Charm]'] = json['st_cha']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_sto' in json:
        this['DefaultCondTurns[EUnitCondition']['Stone]'] = json['ct_sto']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_bli' in json:
        this['DefaultCondTurns[EUnitCondition']['Blindness]'] = json['ct_bli']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_dsk' in json:
        this['DefaultCondTurns[EUnitCondition']['DisableSkill]'] = json['ct_dsk']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_dmo' in json:
        this['DefaultCondTurns[EUnitCondition']['DisableMove]'] = json['ct_dmo']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_dat' in json:
        this['DefaultCondTurns[EUnitCondition']['DisableAttack]'] = json['ct_dat']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_zom' in json:
        this['DefaultCondTurns[EUnitCondition']['Zombie]'] = json['ct_zom']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_dea' in json:
        this['DefaultCondTurns[EUnitCondition']['DeathSentence]'] = json['ct_dea']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_ber' in json:
        this['DefaultCondTurns[EUnitCondition']['Berserk]'] = json['ct_ber']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_dkn' in json:
        this['DefaultCondTurns[EUnitCondition']['DisableKnockback]'] = json['ct_dkn']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_dbu' in json:
        this['DefaultCondTurns[EUnitCondition']['DisableBuff]'] = json['ct_dbu']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_ddb' in json:
        this['DefaultCondTurns[EUnitCondition']['DisableDebuff]'] = json['ct_ddb']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_stop' in json:
        this['DefaultCondTurns[EUnitCondition']['Stop]'] = json['ct_stop']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_fast' in json:
        this['DefaultCondTurns[EUnitCondition']['Fast]'] = json['ct_fast']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_slow' in json:
        this['DefaultCondTurns[EUnitCondition']['Slow]'] = json['ct_slow']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_ahe' in json:
        this['DefaultCondTurns[EUnitCondition']['AutoHeal]'] = json['ct_ahe']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_don' in json:
        this['DefaultCondTurns[EUnitCondition']['Donsoku]'] = json['ct_don']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_rag' in json:
        this['DefaultCondTurns[EUnitCondition']['Rage]'] = json['ct_rag']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_gsl' in json:
        this['DefaultCondTurns[EUnitCondition']['GoodSleep]'] = json['ct_gsl']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_aje' in json:
        this['DefaultCondTurns[EUnitCondition']['AutoJewel]'] = json['ct_aje']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_dhe' in json:
        this['DefaultCondTurns[EUnitCondition']['DisableHeal]'] = json['ct_dhe']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_dsa' in json:
        this['DefaultCondTurns[EUnitCondition']['DisableSingleAttack]'] = json['ct_dsa']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_daa' in json:
        this['DefaultCondTurns[EUnitCondition']['DisableAreaAttack]'] = json['ct_daa']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_ddc' in json:
        this['DefaultCondTurns[EUnitCondition']['DisableDecCT]'] = json['ct_ddc']
    this['DefaultCondTurns[EUnitCondition']
    if 'ct_dic' in json:
        this['DefaultCondTurns[EUnitCondition']['DisableIncCT]'] = json['ct_dic']
    if 'yuragi' in json:
        this['RandomEffectMax'] = json['yuragi']
    if 'ct_max' in json:
        this['ChargeTimeMax'] = json['ct_max']
    if 'ct_wait' in json:
        this['ChargeTimeDecWait'] = json['ct_wait']
    if 'ct_mov' in json:
        this['ChargeTimeDecMove'] = json['ct_mov']
    if 'ct_act' in json:
        this['ChargeTimeDecAction'] = json['ct_act']
    if 'hit_side' in json:
        this['AddHitRateSide'] = json['hit_side']
    if 'hit_back' in json:
        this['AddHitRateBack'] = json['hit_back']
    if 'ahhp_rate' in json:
        this['HpAutoHealRate'] = json['ahhp_rate']
    if 'ahmp_rate' in json:
        this['MpAutoHealRate'] = json['ahmp_rate']
    if 'gshp_rate' in json:
        this['GoodSleepHpHealRate'] = json['gshp_rate']
    if 'gsmp_rate' in json:
        this['GoodSleepMpHealRate'] = json['gsmp_rate']
    if 'dy_rate' in json:
        this['HpDyingRate'] = json['dy_rate']
    if 'zsup_rate' in json:
        this['ZeneiSupportSkillRate'] = json['zsup_rate']
    if 'beginner_days' in json:
        this['BeginnerDays'] = json['beginner_days']
    if 'afcap' in json:
        this['ArtifactBoxCap'] = json['afcap']
    if 'cmn_pi_fire' in json:
        this['CommonPieceFire'] = json['cmn_pi_fire']
    if 'cmn_pi_water' in json:
        this['CommonPieceWater'] = json['cmn_pi_water']
    if 'cmn_pi_thunder' in json:
        this['CommonPieceThunder'] = json['cmn_pi_thunder']
    if 'cmn_pi_wind' in json:
        this['CommonPieceWind'] = json['cmn_pi_wind']
    if 'cmn_pi_shine' in json:
        this['CommonPieceShine'] = json['cmn_pi_shine']
    if 'cmn_pi_dark' in json:
        this['CommonPieceDark'] = json['cmn_pi_dark']
    if 'cmn_pi_all' in json:
        this['CommonPieceAll'] = json['cmn_pi_all']
    if 'ptnum_nml' in json:
        this['PartyNumNormal'] = json['ptnum_nml']
    if 'ptnum_evnt' in json:
        this['PartyNumEvent'] = json['ptnum_evnt']
    if 'ptnum_mlt' in json:
        this['PartyNumMulti'] = json['ptnum_mlt']
    if 'ptnum_aatk' in json:
        this['PartyNumArenaAttack'] = json['ptnum_aatk']
    if 'ptnum_adef' in json:
        this['PartyNumArenaDefense'] = json['ptnum_adef']
    if 'ptnum_chq' in json:
        this['PartyNumChQuest'] = json['ptnum_chq']
    if 'ptnum_tow' in json:
        this['PartyNumTower'] = json['ptnum_tow']
    if 'ptnum_vs' in json:
        this['PartyNumVersus'] = json['ptnum_vs']
    if 'ptnum_mt' in json:
        this['PartyNumMultiTower'] = json['ptnum_mt']
    if 'notsus' in json:
        this['IsDisableSuspend'] = (json['notsus']!=0)
    if 'sus_int' in json:
        this['SuspendSaveInterval'] = json['sus_int']
    if 'jobms' in json:
        this['IsJobMaster'] = json['jobms']!=0
    if 'death_count' in json:
        this['DefaultDeathCount'] = json['death_count']
    if 'fast_val' in json:
        this['DefaultClockUpValue'] = json['fast_val']
    if 'slow_val' in json:
        this['DefaultClockDownValue'] = json['slow_val']
        if 'equip_artifact_slot_unlock' in json:
            this['EquipArtifactSlotUnlock'] = newOInt[json['equip_artifact_slot_unlock'].Length]
        if 'equip_artifact_slot_unlock' in json:
            this['EquipArtifactSlotUnlock[index]'] = json['equip_artifact_slot_unlock'][index]
    if 'kb_gh' in json:
        this['KnockBackHeight'] = json['kb_gh']
    if 'th_gh' in json:
        this['ThrowHeight'] = json['th_gh']
        if 'art_rare_pi' in json:
            this['ArtifactRarePiece'] = newOString[json['art_rare_pi'].Length]
        if 'art_rare_pi' in json:
            this['ArtifactRarePiece[index]'] = json['art_rare_pi'][index]
    if 'art_cmn_pi' in json:
        this['ArtifactCommonPiece'] = json['art_cmn_pi']
    if 'soul_rare' in json:
        this['SoulCommonPiece'] = this.ConvertOStringArray
    if 'equ_rare_pi' in json:
        this['EquipCommonPiece'] = this.ConvertOStringArray
    if 'equ_rare_pi_use' in json:
        this['EquipCommonPieceNum'] = this.ConvertOIntArray
    if 'equ_rare_cost' in json:
        this['EquipCommonPieceCost'] = this.ConvertOIntArray
    if 'equip_cmn' in json:
        this['EquipCmn'] = this.ConvertOStringArray
    if 'aud_max' in json:
        this['AudienceMax'] = json['aud_max']
    if 'ab_rankup_max' in json:
        this['AbilityRankUpPointMax'] = json['ab_rankup_max']
    if 'ab_rankup_addmax' in json:
        this['AbilityRankUpPointAddMax'] = json['ab_rankup_addmax']
    if 'ab_coin_convert' in json:
        this['AbilityRankupPointCoinRate'] = json['ab_coin_convert']
    if 'firstfriend_max' in json:
        this['FirstFriendMax'] = json['firstfriend_max']
    if 'firstfriend_coin' in json:
        this['FirstFriendCoin'] = json['firstfriend_coin']
    if 'cmb_rate' in json:
        this['CombinationRate'] = json['cmb_rate']
    if 'weak_up' in json:
        this['WeakUpRate'] = json['weak_up']
    if 'resist_dw' in json:
        this['ResistDownRate'] = json['resist_dw']
    #returntrue
return this
