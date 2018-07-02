def TrophyCategoryParam(json):
    this={}#TrophyCategoryParamjson)
    #if(json==null)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    #this.hash_code=this.iname.GetHashCode()
    if 'is_not_pull' in json:
        this['is_not_pull'] = json['is_not_pull']==1
    if 'day_reset' in json:
        this['days'] = json['day_reset']
    if 'bgnr' in json:
        this['beginner'] = json['bgnr']
    #this.begin_at.Set(json.begin_at,DateTime.MinValue)
    #this.end_at.Set(json.end_at,DateTime.MaxValue)
    if 'category' in json:
        this['category'] = ENUM['TrophyCategorys'][json['category']]
    if 'linked_quest' in json:
        this['linked_quest'] = json['linked_quest']
    #returntrue
return this
