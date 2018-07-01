def MultiTowerFloorParam(json):
    this={}#MultiTowerFloorParamjson)
    #thrownewInvalidJSONException()
    if 'id' in json:
        this['id'] = json['id']
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
    if 'cond_floor' in json:
        this['cond_floor'] = json['cond_floor']
    if 'pt' in json:
        this['pt'] = json['pt']
    if 'lv' in json:
        this['lv'] = json['lv']
    if 'joblv' in json:
        this['joblv'] = json['joblv']
    if 'reward_id' in json:
        this['reward_id'] = json['reward_id']
    if 'floor' in json:
        this['floor'] = json['floor']
    if 'unitnum' in json:
        this['unitnum'] = json['unitnum']
    if 'notcon' in json:
        this['notcon'] = json['notcon']
    if 'me_id' in json:
        this['me_id'] = json['me_id']
    if 'is_wth_no_chg' in json:
        this['is_wth_no_chg'] = json['is_wth_no_chg']
    if 'wth_set_id' in json:
        this['wth_set_id'] = json['wth_set_id']
    #this.map.Clear()
            #mapParam.Deserialize(json.map[index])
            #this.map.Add(mapParam)
    #instance.AddMTQuest(questParam.iname,questParam)
return this
