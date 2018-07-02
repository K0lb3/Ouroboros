def TowerFloorParam(json):
    this={}#TowerFloorParamjson)
    #if(json==null)
    #thrownewInvalidJSONException()
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'title' in json:
        this['title'] = json['title']
    if 'name' in json:
        this['name'] = json['name']
    if 'expr' in json:
        this['expr'] = json['expr']
    if 'cond' in json:
        this['cond'] = json['cond']
    if 'tower_id' in json:
        this['tower_id'] = json['tower_id']
    if 'cond_quest' in json:
        this['cond_quest'] = json['cond_quest']
    if 'hp_recover_rate' in json:
        this['hp_recover_rate'] = json['hp_recover_rate']
    if 'pt' in json:
        this['pt'] = json['pt']
    if 'lv' in json:
        this['lv'] = json['lv']
    if 'joblv' in json:
        this['joblv'] = json['joblv']
    if 'can_help' in json:
        this['can_help'] = json['can_help']==1
    if 'rdy_cnd' in json:
        this['rdy_cnd'] = json['rdy_cnd']
    if 'reward_id' in json:
        this['reward_id'] = json['reward_id']
    if 'floor' in json:
        this['floor'] = json['floor']
    if 'is_unit_chg' in json:
        this['is_unit_chg'] = json['is_unit_chg']
    if 'me_id' in json:
        this['me_id'] = json['me_id']
    if 'is_wth_no_chg' in json:
        this['is_wth_no_chg'] = json['is_wth_no_chg']
    if 'wth_set_id' in json:
        this['wth_set_id'] = json['wth_set_id']
    if 'naut' in json:
        this['naut'] = json['naut']
    #this.map.Clear()
    if 'mission' in json:
        this['mission'] = json['mission']
    #if(json.map!=null)
        #for(intindex=0index<json.map.Length++index)
            #MapParammapParam=newMapParam()
            #mapParam.Deserialize(json.map)
            #this.map.Add(mapParam)
    #this.BaseQuest=MonoSingleton<GameManager>.Instance.FindBaseQuest(QuestTypes.Tower,this.tower_id)
return this
