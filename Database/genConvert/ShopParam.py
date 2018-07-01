def ShopParam(json):
    this={}#ShopParamjson)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'upd_type' in json:
        this['UpdateCostType'] = ENUM['ESaleType'][json['upd_type']]
        if 'upd_costs' in json:
            this['UpdateCosts'] = newint[json['upd_costs'].Length]
        if 'upd_costs' in json:
            this['UpdateCosts[index]'] = json['upd_costs'][index]
    #returntrue
return this
