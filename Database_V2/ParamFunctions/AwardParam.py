def AwardParam(json):
    this={}#AwardParamjson)
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
    if 'start_at' in json:
        this['start_at']=json['start_at']
    if 'grade' in json:
        this['grade'] = json['grade']
    if 'tab' in json:
        this['tab'] = json['tab']
    return this
