def RecipeParam(json):
    this={}#RecipeParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'cost' in json:
        this['cost'] = json['cost']
    
    mat='mat{}'
    num='num{}'

    this['items']=[
        {
            'iname':    json[mat.format(i)],
            'num':      json[num.format(i)]
        }
        for i in range(1,6)
        if mat.format(i) in json
    ]
    return this
