def AwardParam(json):
    this={}#AwardParamjson)
    #returnfalse
    if 'id' in json:
        this['id'] = json['id']
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'expr' in json:
        this['expr'] = json['expr']
    if 'icon' in json:
        this['icon'] = json['icon']
    if 'bg' in json:
        this['bg'] = json['bg']
    if 'txt_img' in json:
        this['txt_img'] = json['txt_img']
    #if(!string.IsNullOrEmpty(json.start_at))
    #DateTime.TryParse(json.start_at,outthis.start_at)
    if 'grade' in json:
        this['grade'] = json['grade']
    if 'iname' in json:
        this['hash'] = json['iname'].GetHashCode
    if 'tab' in json:
        this['tab'] = json['tab']
    #returntrue
return this
