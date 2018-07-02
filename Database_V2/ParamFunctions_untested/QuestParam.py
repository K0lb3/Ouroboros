def QuestParam(json):
    this={}#QuestParamjson)
    #if(json==null)
    #thrownewInvalidJSONException()
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'expr' in json:
        this['expr'] = json['expr']
    if 'cond' in json:
        this['cond'] = json['cond']
    if 'mission' in json:
        this['mission'] = json['mission']
    if 'pexp' in json:
        this['pexp'] = json['pexp']
    if 'uexp' in json:
        this['uexp'] = json['uexp']
    if 'gold' in json:
        this['gold'] = json['gold']
    if 'mcoin' in json:
        this['mcoin'] = json['mcoin']
    if 'pt' in json:
        this['point'] = CheckCast.to_short
    if 'multi' in json:
        this['multi'] = CheckCast.to_short(json['multi'])
    if 'multi_dead' in json:
        this['multiDead'] = CheckCast.to_short(json['multi_dead'])
    if 'pnum' in json:
        this['playerNum'] = CheckCast.to_short(json['pnum'])
    if 'unum' in json:
        this['unitNum'] = CheckCast.to_short(json['unum']<=QuestParam.MULTI_MAX_PLAYER_UNIT?json['unum']:QuestParam.MULTI_MAX_PLAYER_UNIT)
    if 'aplv' in json:
        this['aplv'] = CheckCast.to_short
    if 'limit' in json:
        this['challengeLimit'] = CheckCast.to_short
    if 'dayreset' in json:
        this['dayReset'] = json['dayreset']
    #if((int)this.multi!=0)
        #if(json.pnum*json.unum>QuestParam.MULTI_MAX_TOTAL_UNIT)
        #DebugUtility.LogError("iname:"+json.iname+"/Currenttotalunitis"+(object)(json.pnum*json.unum)+".Pleasesetthetotalnumberofunitsto"+(object)QuestParam.MULTI_MAX_TOTAL_UNIT)
        #if(json.unum>QuestParam.MULTI_MAX_PLAYER_UNIT)
        #DebugUtility.LogError("iname:"+json.iname+"/Current1playerunitis"+(object)json.unum+".Pleasesetthe1playernumberofunitsto"+(object)QuestParam.MULTI_MAX_PLAYER_UNIT)
    if 'key_limit' in json:
        this['key_limit'] = json['key_limit']
    if 'ctw' in json:
        this['clock_win'] = CheckCast.to_short(json['ctw'])
    if 'ctl' in json:
        this['clock_lose'] = CheckCast.to_short(json['ctl'])
    if 'lv' in json:
        this['lv'] = CheckCast.to_short)
    if 'win' in json:
        this['win'] = CheckCast.to_short(json['win'])
    if 'lose' in json:
        this['lose'] = CheckCast.to_short(json['lose'])
    if 'type' in json:
        this['type'] = ENUM['QuestTypes'][json['type']]
    if 'subtype' in json:
        this['subtype'] = ENUM['SubQuestTypes'][json['subtype']]
    #this.cond_quests=(string)null
    #this.units.Clear()
    if 'area' in json:
        this['ChapterID'] = json['area']
    if 'world' in json:
        this['world'] = json['world']
    if 'text' in json:
        this['storyTextID'] = json['text']
    if 'hide' in json:
        this['hidden'] = json['hide']!=0
    if 'replay_limit' in json:
        this['replayLimit'] = json['replay_limit']!=0
    if 'ticket' in json:
        this['ticket'] = json['ticket']
    if 'title' in json:
        this['title'] = json['title']
    if 'nav' in json:
        this['navigation'] = json['nav']
    if 'ajob' in json:
        this['AllowedJobs'] = json['ajob']
    #this.AllowedTags=(QuestParam.Tags)0
    #if(!string.IsNullOrEmpty(json.atag))
        #stringstrArray=json.atag.Split(',')
        #for(intindex=0index<strArray.Length++index)
            #if(!string.IsNullOrEmpty(strArray))
            #this.AllowedTags|=(QuestParam.Tags)Enum.Parse(typeof(QuestParam.Tags),strArray)
    if 'phyb' in json:
        this['PhysBonus'] = json['phyb']+100
    if 'magb' in json:
        this['MagBonus'] = json['magb']+100
    if 'bgnr' in json:
        this['IsBeginner'] = 0!=json['bgnr']
    if 'i_lyt' in json:
        this['ItemLayout'] = json['i_lyt']
    if 'not_search' in json:
        this['notSearch'] = json['not_search']!=0
    #ObjectiveParamobjective=MonoSingleton<GameManager>.GetInstanceDirect().FindObjective(json.mission)
    #if(objective!=null)
        #this.bonusObjective=newQuestBonusObjective[objective.objective.Length]
        #for(intindex=0index<objective.objective.Length++index)
            #this.bonusObjective=newQuestBonusObjective()
            #this.bonusObjective.Type=(EMissionType)objective.objective.type
            #this.bonusObjective.TypeParam=objective.objective.val
            #this.bonusObjective.item=objective.objective.item
            #this.bonusObjective.itemNum=objective.objective.num
            #this.bonusObjective.itemType=(RewardType)objective.objective.item_type
            #this.bonusObjective.IsTakeoverProgress=objective.objective.IsTakeoverProgress
    #ObjectiveParamtowerObjective=MonoSingleton<GameManager>.GetInstanceDirect().FindTowerObjective(json.tower_mission)
    #if(towerObjective!=null)
        #this.bonusObjective=newQuestBonusObjective[towerObjective.objective.Length]
        #for(intindex=0index<towerObjective.objective.Length++index)
            #this.bonusObjective=newQuestBonusObjective()
            #this.bonusObjective.Type=(EMissionType)towerObjective.objective.type
            #this.bonusObjective.TypeParam=towerObjective.objective.val
            #this.bonusObjective.item=towerObjective.objective.item
            #this.bonusObjective.itemNum=towerObjective.objective.num
            #this.bonusObjective.itemType=(RewardType)towerObjective.objective.item_type
            #this.bonusObjective.IsTakeoverProgress=towerObjective.objective.IsTakeoverProgress
        #this.mission_values=newint[towerObjective.objective.Length]
    #MagnificationParammagnification=MonoSingleton<GameManager>.GetInstanceDirect().FindMagnification(json.atk_mag)
    #if(magnification!=null&&magnification.atkMagnifications!=null)
    #this.AtkTypeMags=magnification.atkMagnifications
    #QuestCondParamquestCond1=MonoSingleton<GameManager>.GetInstanceDirect().FindQuestCond(json.rdy_cnd)
    #if(questCond1!=null)
    #this.EntryCondition=questCond1
    #QuestCondParamquestCond2=MonoSingleton<GameManager>.GetInstanceDirect().FindQuestCond(json.rdy_cnd_ch)
    #if(questCond2!=null)
    #this.EntryConditionCh=questCond2
    if 'mode' in json:
        this['difficulty'] = ENUM['QuestDifficulties'][json['mode']]
    #if(json.units!=null)
        #this.units.Setup(json.units.Length)
        #for(intindex=0index<json.units.Length++index)
        #this.units.Set(index,json.units)
    #if(json.cond_quests!=null)
        #for(intindex=0index<json.cond_quests.Length++index)
        if 'cond_quests' in json:
            this['cond_quests'] = json['cond_quests']
    #this.map.Clear()
    #if(json.map!=null)
        #for(intindex=0index<json.map.Length++index)
            #MapParammapParam=newMapParam()
            #mapParam.Deserialize(json.map)
            #this.map.Add(mapParam)
    if 'evst' in json:
        this['event_start'] = json['evst']
    if 'evw' in json:
        this['event_clear'] = json['evw']
    if 'retr' in json:
        this['AllowRetreat'] = json['retr']==0
    if 'naut' in json:
        this['AllowAutoPlay'] = json['naut']==0||json['naut']==2
    if 'naut' in json:
        this['FirstAutoPlayProhibit'] = json['naut']==2
    if 'swin' in json:
        this['Silent'] = json['swin']!=0
    if 'notabl' in json:
        this['DisableAbilities'] = json['notabl']!=0
    if 'notitm' in json:
        this['DisableItems'] = json['notitm']!=0
    if 'notcon' in json:
        this['DisableContinue'] = json['notcon']!=0
    if 'fix_editor' in json:
        this['UseFixEditor'] = json['fix_editor']!=0
    if 'is_no_start_voice' in json:
        this['IsNoStartVoice'] = json['is_no_start_voice']!=0
    if 'sprt' in json:
        this['UseSupportUnit'] = json['sprt']==0
    if 'is_unit_chg' in json:
        this['IsUnitChange'] = json['is_unit_chg']!=0
    if 'thumnail' in json:
        this['VersusThumnail'] = json['thumnail']
    if 'mskill' in json:
        this['MapBuff'] = json['mskill']
    if 'vsmovecnt' in json:
        this['VersusMoveCount'] = json['vsmovecnt']
    if 'dmg_up_pl' in json:
        this['DamageUpprPl'] = json['dmg_up_pl']
    if 'dmg_up_en' in json:
        this['DamageUpprEn'] = json['dmg_up_en']
    if 'dmg_rt_pl' in json:
        this['DamageRatePl'] = json['dmg_rt_pl']
    if 'dmg_rt_en' in json:
        this['DamageRateEn'] = json['dmg_rt_en']
    if 'extra' in json:
        this['IsExtra'] = json['extra']==1
    if 'review' in json:
        this['ShowReviewPopup'] = json['review']==1
    if 'is_multileader' in json:
        this['IsMultiLeaderSkill'] = json['is_multileader']!=0
    if 'me_id' in json:
        this['MapEffectId'] = json['me_id']
    if 'is_wth_no_chg' in json:
        this['IsWeatherNoChange'] = json['is_wth_no_chg']!=0
    if 'wth_set_id' in json:
        this['WeatherSetId'] = json['wth_set_id']
    #if(json.fclr_items!=null)
        #for(intindex=0index<json.fclr_items.Length++index)
        if 'fclr_items' in json:
            this['FirstClearItems'] = json['fclr_items']
    if 'party_id' in json:
        this['questParty'] = MonoSingleton<GameManager>.GetInstanceDirect.FindQuestParty(json['party_id'])
return this
