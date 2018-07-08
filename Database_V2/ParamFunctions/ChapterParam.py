def ChapterParam(json):
    this={}#ChapterParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'expr' in json:
        this['expr'] = json['expr']
    if 'world' in json:
        this['world'] = json['world']
    if 'start' in json:
        this['start'] = json['start']
    if 'end' in json:
        this['end'] = json['end']
    if 'hide' in json:
        this['hidden'] = json['hide']!=0
    if 'chap' in json:
        this['section'] = json['chap']
    if 'banr' in json:
        this['banner'] = json['banr']
    if 'item' in json:
        this['prefabPath'] = json['item']
    if 'hurl' in json:
        this['helpURL'] = json['hurl']
    this['keys']=[]
    for i in range(1,3):
        if 'keyitem'+str(i) in json and 'keynum'+str(i) in json:
            this['keys'].append({
                'iname': json['keynum'+str(i)],
                'num': json['keynum'+str(i)]
            })
        else:
            break
    if 'keytime' in json:
        this['keytime'] = json['keytime']
    else:
        if len(this['keys'])==0:
            del this['keys']
    return this
