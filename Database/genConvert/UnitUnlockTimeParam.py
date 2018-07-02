def UnitUnlockTimeParam(json):
    this={}#UnitUnlockTimeParamjson)
    #if(json==null)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    #if(!string.IsNullOrEmpty(json.begin_at))
        #try
            if 'begin_at' in json:
                this['begin_at'] = DateTime.Parse
        #catch
            #this.begin_at=DateTime.MaxValue
    #if(!string.IsNullOrEmpty(json.end_at))
        #try
            if 'end_at' in json:
                this['end_at'] = DateTime.Parse
        #catch
            #this.end_at=DateTime.MinValue
    #returntrue
return this
