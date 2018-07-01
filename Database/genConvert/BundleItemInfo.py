def BundleItemInfo(json):
    this={}#BundleItemInfojson)
    #returnfalse
    if 'iname' in json:
        this['Name'] = json['iname']
    if 'num' in json:
        this['Quantity'] = json['num']
    #returntrue
return this
