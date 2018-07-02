def ConceptCard(json):
    this={}#ConceptCardjson)
    if 'iid' in json:
        this['mUniqueID'] = json['iid']
    if 'exp' in json:
        this['mExp'] = json['exp']
    if 'trust' in json:
        this['mTrust'] = json['trust']
    if 'fav' in json:
        this['mFavorite'] = json['fav']!=0
    if 'trust_bonus' in json:
        this['mTrustBonus'] = json['trust_bonus']!=0
    if 'plus' in json:
        this['mAwakeCount'] = json['plus']
    if 'iname' in json:
        this['mConceptCardParam'] = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam
    #this.mLv=(OInt)this.CalcCardLevel()
    #this.UpdateEquipEffect()
    #this.RefreshFilterType()
    #returntrue
return this
