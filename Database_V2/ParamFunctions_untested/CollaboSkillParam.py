def CollaboSkillParam(json):
    this={}#CollaboSkillParamjson)
    #if(json==null)
    #return
    if 'iname' in json:
        this['mIname'] = json['iname']
    if 'uname' in json:
        this['mUnitIname'] = json['uname']
    if 'abid' in json:
        this['mAbilityIname'] = json['abid']
    #this.mLearnSkillLists.Clear()
    #if(json.lqs==null)
    #return
    #foreach(stringlqinjson.lqs)
    #this.mLearnSkillLists.Add(newCollaboSkillParam.LearnSkill(lq))
return this
