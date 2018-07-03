def FixParam(json):
    if type(json)==list:
        json=json[0]
    this={}#FixParamjson)
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
        this['AbilityRankUpCountRecoverySec'] = json['abilupsec']
    if 'stmncoin' in json:
        this['StaminaRecoveryCoin'] = json['stmncoin']
    if 'stmnrec' in json:
        this['StaminaRecoveryVal'] = json['stmnrec']
    if 'stmnsec' in json:
        this['StaminaRecoverySec'] = json['stmnsec']
    if 'stmncap' in json:
        this['StaminaStockCap'] = json['stmncap']
    if 'stmnadd' in json:
        this['StaminaAdd'] = json['stmnadd']
    if 'stmnadd2' in json:
        this['StaminaAdd2'] = json['stmnadd2']
    if 'stmncost' in json:
        this['StaminaAddCost'] = json['stmncost']
    if 'cavemax' in json:
        this['CaveStaminaMax'] = json['cavemax']
    if 'caverec' in json:
        this['CaveStaminaRecoveryVal'] = json['caverec']
    if 'cavesec' in json:
        this['CaveStaminaRecoverySec'] = json['cavesec']
    if 'cavecap' in json:
        this['CaveStaminaStockCap'] = json['cavecap']
    if 'caveadd' in json:
        this['CaveStaminaAdd'] = json['caveadd']
    if 'cavecost' in json:
        this['CaveStaminaAddCost'] = json['cavecost']
    if 'arenamax' in json:
        this['ChallengeArenaMax'] = json['arenamax']
    if 'arenasec' in json:
        this['ChallengeArenaCoolDownSec'] = json['arenasec']
    if 'arenamedal' in json:
        this['ArenaMedalMultipler'] = json['arenamedal']
    if 'arenacoin' in json:
        this['ArenaCoinRewardMultipler'] = json['arenacoin']
    if 'arenaccost' in json:
        this['ArenaResetCooldownCost'] = json['arenaccost']
    if 'arenatcost' in json:
        this['ArenaResetTicketCost'] = json['arenatcost']
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
        this['ShopUpdateTime'] = json['shop_update_time']
    if 'products' in json:
        this['Products'] = json['products']
    if 'vip_product' in json:
        this['VipCardProduct'] = json['vip_product']
    if 'vip_date' in json:
        this['VipCardDate'] = json['vip_date']
    if 'ggmax' in json:
        this['FreeGachaGoldMax'] = json['ggmax']
    if 'ggsec' in json:
        this['FreeGachaGoldCoolDownSec'] = json['ggsec']
    if 'cgsec' in json:
        this['FreeGachaCoinCoolDownSec'] = json['cgsec']
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
        this['EliteResetCosts'] = json['elite_reset_cost']
    
    this['DefaultCondTurns']={}
    this['DefaultCondTurns']['Poison'] = 0
    this['DefaultCondTurns']['Paralysed'] = 0
    this['DefaultCondTurns']['Stun'] = 0
    this['DefaultCondTurns']['Sleep'] = 0
    this['DefaultCondTurns']['Charm'] = 0
    this['DefaultCondTurns']['Stone'] = 0
    this['DefaultCondTurns']['Blindness'] = 0
    this['DefaultCondTurns']['DisableSkill'] = 0
    this['DefaultCondTurns']['DisableMove'] = 0
    this['DefaultCondTurns']['DisableAttack'] = 0
    this['DefaultCondTurns']['Zombie'] = 0
    this['DefaultCondTurns']['DeathSentence'] = 0
    this['DefaultCondTurns']['Berserk'] = 0
    this['DefaultCondTurns']['DisableKnockback'] = 0
    this['DefaultCondTurns']['DisableBuff'] = 0
    this['DefaultCondTurns']['DisableDebuff'] = 0
    this['DefaultCondTurns']['Stop'] = 0
    this['DefaultCondTurns']['Fast'] = 0
    this['DefaultCondTurns']['Slow'] = 0
    this['DefaultCondTurns']['AutoHeal'] = 0
    this['DefaultCondTurns']['Donsoku'] = 0
    this['DefaultCondTurns']['Rage'] = 0
    this['DefaultCondTurns']['GoodSleep'] = 0
    this['DefaultCondTurns']['AutoJewel'] = 0
    this['DefaultCondTurns']['DisableHeal'] = 0
    this['DefaultCondTurns']['DisableSingleAttack'] = 0
    this['DefaultCondTurns']['DisableAreaAttack'] = 0
    this['DefaultCondTurns']['DisableDecCT'] = 0
    this['DefaultCondTurns']['DisableIncCT'] = 0
    this['DefaultCondTurns']['DisableEsaFire'] = 0
    this['DefaultCondTurns']['DisableEsaWater'] = 0
    this['DefaultCondTurns']['DisableEsaWind'] = 0
    this['DefaultCondTurns']['DisableEsaThunder'] = 0
    this['DefaultCondTurns']['DisableEsaShine'] = 0
    this['DefaultCondTurns']['DisableEsaDark'] = 0
    this['DefaultCondTurns']['DisableMaxDamageHp'] = 0
    this['DefaultCondTurns']['DisableMaxDamageMp'] = 0

    if 'ct_poi' in json:
        this['DefaultCondTurns']['Poison'] = json['ct_poi']
    if 'ct_par' in json:
        this['DefaultCondTurns']['Paralysed'] = json['ct_par']
    if 'ct_stu' in json:
        this['DefaultCondTurns']['Stun'] = json['ct_stu']
    if 'ct_sle' in json:
        this['DefaultCondTurns']['Sleep'] = json['ct_sle']
    if 'st_cha' in json:
        this['DefaultCondTurns']['Charm'] = json['st_cha']
    if 'ct_sto' in json:
        this['DefaultCondTurns']['Stone'] = json['ct_sto']
    if 'ct_bli' in json:
        this['DefaultCondTurns']['Blindness'] = json['ct_bli']
    if 'ct_dsk' in json:
        this['DefaultCondTurns']['DisableSkill'] = json['ct_dsk']
    if 'ct_dmo' in json:
        this['DefaultCondTurns']['DisableMove'] = json['ct_dmo']
    if 'ct_dat' in json:
        this['DefaultCondTurns']['DisableAttack'] = json['ct_dat']
    if 'ct_zom' in json:
        this['DefaultCondTurns']['Zombie'] = json['ct_zom']
    if 'ct_dea' in json:
        this['DefaultCondTurns']['DeathSentence'] = json['ct_dea']
    if 'ct_ber' in json:
        this['DefaultCondTurns']['Berserk'] = json['ct_ber']
    if 'ct_dkn' in json:
        this['DefaultCondTurns']['DisableKnockback'] = json['ct_dkn']
    if 'ct_dbu' in json:
        this['DefaultCondTurns']['DisableBuff'] = json['ct_dbu']
    if 'ct_ddb' in json:
        this['DefaultCondTurns']['DisableDebuff'] = json['ct_ddb']
    if 'ct_stop' in json:
        this['DefaultCondTurns']['Stop'] = json['ct_stop']
    if 'ct_fast' in json:
        this['DefaultCondTurns']['Fast'] = json['ct_fast']
    if 'ct_slow' in json:
        this['DefaultCondTurns']['Slow'] = json['ct_slow']
    if 'ct_ahe' in json:
        this['DefaultCondTurns']['AutoHeal'] = json['ct_ahe']
    if 'ct_don' in json:
        this['DefaultCondTurns']['Donsoku'] = json['ct_don']
    if 'ct_rag' in json:
        this['DefaultCondTurns']['Rage'] = json['ct_rag']
    if 'ct_gsl' in json:
        this['DefaultCondTurns']['GoodSleep'] = json['ct_gsl']
    if 'ct_aje' in json:
        this['DefaultCondTurns']['AutoJewel'] = json['ct_aje']
    if 'ct_dhe' in json:
        this['DefaultCondTurns']['DisableHeal'] = json['ct_dhe']
    if 'ct_dsa' in json:
        this['DefaultCondTurns']['DisableSingleAttack'] = json['ct_dsa']
    if 'ct_daa' in json:
        this['DefaultCondTurns']['DisableAreaAttack'] = json['ct_daa']
    if 'ct_ddc' in json:
        this['DefaultCondTurns']['DisableDecCT'] = json['ct_ddc']
    if 'ct_dic' in json:
        this['DefaultCondTurns']['DisableIncCT'] = json['ct_dic']
    if 'ct_esa' in json:
        this['DefaultCondTurns']['DisableEsaFire'] = json['ct_esa']
    if 'ct_esa' in json:
        this['DefaultCondTurns']['DisableEsaWater'] = json['ct_esa']
    if 'ct_esa' in json:
        this['DefaultCondTurns']['DisableEsaWind'] = json['ct_esa']
    if 'ct_esa' in json:
        this['DefaultCondTurns']['DisableEsaThunder'] = json['ct_esa']
    if 'ct_esa' in json:
        this['DefaultCondTurns']['DisableEsaShine'] = json['ct_esa']
    if 'ct_esa' in json:
        this['DefaultCondTurns']['DisableEsaDark'] = json['ct_esa']
    if 'ct_mdh' in json:
        this['DefaultCondTurns']['DisableMaxDamageHp'] = json['ct_mdh']
    if 'ct_mdm' in json:
        this['DefaultCondTurns']['DisableMaxDamageMp'] = json['ct_mdm']
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
    if 'ptnum_ordeal' in json:
        this['PartyNumOrdeal'] = json['ptnum_ordeal']
    if 'notsus' in json:
        this['IsDisableSuspend'] = json['notsus']!=0
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
        this['EquipArtifactSlotUnlock'] = json['equip_artifact_slot_unlock']
    if 'kb_gh' in json:
        this['KnockBackHeight'] = json['kb_gh']
    if 'th_gh' in json:
        this['ThrowHeight'] = json['th_gh']
    if 'art_rare_pi' in json:
        this['ArtifactRarePiece'] = json['art_rare_pi']
    if 'art_cmn_pi' in json:
        this['ArtifactCommonPiece'] = json['art_cmn_pi']
    if 'soul_rare' in json:
        this['SoulCommonPiece'] = json['soul_rare']
    if 'equ_rare_pi' in json:
        this['EquipCommonPiece'] = json['equ_rare_pi']
    if 'equ_rare_pi_use' in json:
        this['EquipCommonPieceNum'] = json['equ_rare_pi_use']
    if 'equ_rare_cost' in json:
        this['EquipCommonPieceCost'] = json['equ_rare_cost']
    if 'equip_cmn' in json:
        this['EquipCmn'] = json['equip_cmn']
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
    if 'ordeal_ct' in json:
        this['OrdealCT'] = json['ordeal_ct']
    if 'esa_assist' in json:
        this['EsaAssist'] = json['esa_assist']
    if 'esa_resist' in json:
        this['EsaResist'] = json['esa_resist']
    if 'card_sell_mul' in json:
        this['CardSellMul'] = json['card_sell_mul']
    if 'card_exp_mul' in json:
        this['CardExpMul'] = json['card_exp_mul']
    if 'card_max' in json:
        this['CardMax'] = json['card_max']
    if 'card_trust_max' in json:
        this['CardTrustMax'] = json['card_trust_max']
    if 'card_trust_en_bonus' in json:
        this['CardTrustPileUp'] = json['card_trust_en_bonus']
    if 'card_awake_unlock_lvcap' in json:
        this['CardAwakeUnlockLevelCap'] = json['card_awake_unlock_lvcap']
    if 'tobira_lv_cap' in json:
        this['TobiraLvCap'] = json['tobira_lv_cap']
    if 'tobira_unit_lv_cap' in json:
        this['TobiraUnitLvCapBonus'] = json['tobira_unit_lv_cap']
    if 'tobira_unlock_elem' in json:
        this['TobiraUnlockElem'] = json['tobira_unlock_elem']
    if 'tobira_unlock_birth' in json:
        this['TobiraUnlockBirth'] = json['tobira_unlock_birth']
    if 'ini_rec' in json:
        this['IniValRec'] = json['ini_rec']
    if 'guerrilla_val' in json:
        this['GuerrillaVal'] = json['guerrilla_val']
    if 'draft_select_sec' in json:
        this['DraftSelectSeconds'] = json['draft_select_sec']
    if 'draft_organize_sec' in json:
        this['DraftOrganizeSeconds'] = json['draft_organize_sec']
    if 'draft_place_sec' in json:
        this['DraftPlaceSeconds'] = json['draft_place_sec']
    #returntrue
    return this
