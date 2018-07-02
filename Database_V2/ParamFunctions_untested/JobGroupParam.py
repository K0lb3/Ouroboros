def JobGroupParam(json):
    this={}#JobGroupParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'jobs' in json:
        this['jobs'] = json['jobs']
    if 'name' in json:
        this['name'] = json['name']
    #returntrue
return this
