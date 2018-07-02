def TowerParam(json):
    this={}#TowerParamjson)
    #if(json==null)
    #thrownewInvalidJSONException()
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'expr' in json:
        this['expr'] = json['expr']
    if 'banr' in json:
        this['banr'] = json['banr']
    if 'item' in json:
        this['prefabPath'] = json['item']
    if 'bg' in json:
        this['bg'] = json['bg']
    if 'floor_bg_open' in json:
        this['floor_bg_open'] = json['floor_bg_open']
    if 'floor_bg_close' in json:
        this['floor_bg_close'] = json['floor_bg_close']
    if 'can_unit_recover' in json:
        this['can_unit_recover'] = json['can_unit_recover']==1
    if 'unit_recover_minute' in json:
        this['unit_recover_minute'] = json['unit_recover_minute']
    if 'unit_recover_coin' in json:
        this['unit_recover_coin'] = json['unit_recover_coin']
    if 'eventURL' in json:
        this['eventURL'] = json['eventURL']
    if 'is_down' in json:
        this['is_down'] = json['is_down']>0
    if 'is_view_ranking' in json:
        this['is_view_ranking'] = json['is_view_ranking']>0
    if 'unlock_level' in json:
        this['unlock_level'] = json['unlock_level']
    if 'unlock_quest' in json:
        this['unlock_quest'] = json['unlock_quest']
    if 'url' in json:
        this['URL'] = json['url']
    if 'floor_reset_coin' in json:
        this['floor_reset_coin'] = json['floor_reset_coin']
    if 'score_iname' in json:
        this['score_iname'] = json['score_iname']
return this
