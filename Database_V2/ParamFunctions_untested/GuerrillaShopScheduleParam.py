def GuerrillaShopScheduleParam(json):
    this={}#GuerrillaShopScheduleParamjson)
    if 'id' in json:
        this['id'] = json['id']
    if 'begin_at' in json:
        this['begin_at'] = json['begin_at']
    if 'end_at' in json:
        this['end_at'] = json['end_at']
    if 'accum_ap' in json:
        this['accum_ap'] = json['accum_ap']
    if 'open_time' in json:
        this['open_time'] = json['open_time']
    if 'cool_time' in json:
        this['cool_time'] = json['cool_time']
    #if(json.advent!=null)
        #GuerrillaShopScheduleAdventshopScheduleAdventArray=newGuerrillaShopScheduleAdvent[json.advent.Length]
        #for(intindex=0index<json.advent.Length++index)
            #shopScheduleAdventArray=newGuerrillaShopScheduleAdvent()
            #shopScheduleAdventArray.id=json.advent.id
            #shopScheduleAdventArray.coef=json.advent.coef
    #returntrue
return this
