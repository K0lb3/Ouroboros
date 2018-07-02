def VersusMatchCondParam(json):
    this={}#VersusMatchCondParamjson)
    #if(json==null)
    #return
    if 'floor' in json:
        this['Floor'] = json['floor']
    if 'lvrang' in json:
        this['LvRange'] = json['lvrang']
    if 'floorrange' in json:
        this['FloorRange'] = json['floorrange']
return this
