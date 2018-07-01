def CollaboSkillParam(json):
    this={}#CollaboSkillParamjson)
    #return
    if 'iname' in json:
        this['mIname'] = json['iname']
    if 'uname' in json:
        this['mUnitIname'] = json['uname']
    if 'abid' in json:
        this['mAbilityIname'] = json['abid']
    #this.mLearnSkillLists.Clear()
    #return
    #foreach(stringlqinjson.lqs)
    #this.mLearnSkillLists.Add(newCollaboSkillParam.LearnSkill(lq))
return this
