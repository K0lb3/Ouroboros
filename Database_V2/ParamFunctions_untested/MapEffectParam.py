def MapEffectParam(json):
    this={}#MapEffectParamjson)
    #if(json==null)
    #return
    #this.mIndex=++MapEffectParam.CurrentIndex
    if 'iname' in json:
        this['mIname'] = json['iname']
    if 'name' in json:
        this['mName'] = json['name']
    if 'expr' in json:
        this['mExpr'] = json['expr']
    #this.mValidSkillLists.Clear()
    #if(json.skills==null)
    #return
    #foreach(stringskillinjson.skills)
    #this.mValidSkillLists.Add(skill)
return this
