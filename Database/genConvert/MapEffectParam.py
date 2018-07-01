def MapEffectParam(json):
    this={}#MapEffectParamjson)
    #return
    if 'iname' in json:
        this['mIname'] = json['iname']
    if 'name' in json:
        this['mName'] = json['name']
    if 'expr' in json:
        this['mExpr'] = json['expr']
    #this.mValidSkillLists.Clear()
    #return
    #foreach(stringskillinjson.skills)
    #this.mValidSkillLists.Add(skill)
return this
