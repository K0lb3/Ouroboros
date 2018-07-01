def WeatherParam(json):
    this={}#WeatherParamjson)
    #return
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
    #this.mBuffIdLists.Clear()
        #foreach(stringbuffIdinjson.buff_ids)
        #this.mBuffIdLists.Add(buffId)
    #this.mCondIdLists.Clear()
    #return
    #foreach(stringcondIdinjson.cond_ids)
    #this.mCondIdLists.Add(condId)
return this
