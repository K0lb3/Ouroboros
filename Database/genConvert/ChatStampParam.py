def ChatStampParam(json):
    this={}#ChatStampParamjson)
    #if(json==null||json.fields==null)
    #returnfalse
    if 'fields' in json:
        this['id'] = json['fields'].id
    if 'fields' in json:
        this['img_id'] = json['fields'].img_id
    if 'fields' in json:
        this['iname'] = json['fields'].iname
    if 'fields' in json:
        this['IsPrivate'] = json['fields'].is_private==1
    #returntrue
return this
