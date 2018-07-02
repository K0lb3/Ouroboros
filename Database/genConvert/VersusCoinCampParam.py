def VersusCoinCampParam(json):
    this={}#VersusCoinCampParamjson)
    #if(json==null)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
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
