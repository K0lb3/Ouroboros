def WeatherParam(json):
    this={}#WeatherParamjson)
    if 'iname' in json:
        this['mIname'] = json['iname']
    if 'name' in json:
        this['mName'] = json['name']
    if 'expr' in json:
        this['mExpr'] = json['expr']
    if 'icon' in json:
        this['mIcon'] = json['icon']
    if 'effect' in json:
        this['mEffect'] = json['effect']
    if('buff_ids' in json and len(json['buff_ids'])!=0):
        this['mBuffIdLists']=json['buff_ids']
    if('cond_ids' in json and len(json['cond_ids'])!=0):
        this['mCondIdLists']=json['cond_ids']
    return this
