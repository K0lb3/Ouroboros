def VersusRule(json):
    this={}#VersusRulejson)
    #if(json==null)
    #returnfalse
    if 'id' in json:
        this['id'] = json['id']
    if 'vsmode' in json:
        this['mode'] = ENUM['VS_MODE'][json['vsmode']]
    if 'getcoin' in json:
        this['coin'] = json['getcoin']
    if 'rate' in json:
        this['coinrate'] = json['rate']<=0?1:json['rate']
    #try
        #if(!string.IsNullOrEmpty(json.begin_at))
        if 'begin_at' in json:
            this['begin_at'] = DateTime.Parse
        #if(!string.IsNullOrEmpty(json.end_at))
        if 'end_at' in json:
            this['end_at'] = DateTime.Parse
    #catch(Exceptionex)
        #DebugUtility.LogError(ex.Message)
        #returnfalse
    #returntrue
return this
