def TobiraCategoriesParam(json):
    this={}#TobiraCategoriesParamjson)
    #if(json==null)
    #return
    if 'category' in json:
        this['mCategory'] = json['category']
    if 'name' in json:
        this['mName'] = json['name']
return this
