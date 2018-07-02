def ConceptCardMaterial(json):
    this={}#ConceptCardMaterialjson)
    if 'id' in json:
        this['mUniqueID'] = json['id']
    if 'iname' in json:
        this['mIName'] = json['iname']
    if 'num' in json:
        this['mNum'] = json['num']
    if 'iname' in json:
        this['mParam'] = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam
    #returntrue
return this
