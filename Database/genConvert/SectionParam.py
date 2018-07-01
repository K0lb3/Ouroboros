def SectionParam(json):
    this={}#SectionParamjson)
    #thrownewInvalidJSONException()
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'expr' in json:
        this['expr'] = json['expr']
    if 'start' in json:
        this['start'] = json['start']
    if 'end' in json:
        this['end'] = json['end']
    if 'hide' in json:
        this['hidden'] = json['hide']!=0
    if 'home' in json:
        this['home'] = json['home']
    if 'unit' in json:
        this['unit'] = json['unit']
    if 'item' in json:
        this['prefabPath'] = json['item']
    if 'shop' in json:
        this['shop'] = json['shop']
    if 'inn' in json:
        this['inn'] = json['inn']
    if 'bar' in json:
        this['bar'] = json['bar']
    if 'bgm' in json:
        this['bgm'] = json['bgm']
return this
