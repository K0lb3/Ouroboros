def JobGroupParam(json):
    this={}#JobGroupParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'jobs' in json:
        this['jobs'] = json['jobs']
    return this
