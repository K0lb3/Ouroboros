def ShopParam(json):
    this={}#ShopParamjson)
    #if(json==null)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'upd_type' in json:
        this['UpdateCostType'] = ENUM['ESaleType'][json['upd_type']]
    #this.UpdateCosts=(int)null
    #if(json.upd_costs!=null&&json.upd_costs.Length>0)
        #for(intindex=0index<json.upd_costs.Length++index)
        if 'upd_costs' in json:
            this['UpdateCosts'] = json['upd_costs']
    #returntrue
return this
