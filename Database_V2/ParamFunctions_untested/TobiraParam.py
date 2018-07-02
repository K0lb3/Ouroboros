def TobiraParam(json):
    this={}#TobiraParamjson)
    #if(json==null)
    #return
    if 'unit_iname' in json:
        this['mUnitIname'] = json['unit_iname']
    if 'enable' in json:
        this['mEnable'] = json['enable']==1
    if 'category' in json:
        this['mCategory'] = json['category']
    if 'recipe_id' in json:
        this['mRecipeId'] = json['recipe_id']
    if 'skill_iname' in json:
        this['mSkillIname'] = json['skill_iname']
    #this.mLearnAbilities.Clear()
    #if(json.learn_abils!=null)
        #for(intindex=0index<json.learn_abils.Length++index)
            #TobiraLearnAbilityParamlearnAbilityParam=newTobiraLearnAbilityParam()
            #learnAbilityParam.Deserialize(json.learn_abils)
            #this.mLearnAbilities.Add(learnAbilityParam)
    if 'overwrite_ls_iname' in json:
        this['mOverwriteLeaderSkillIname'] = json['overwrite_ls_iname']
    #if(!string.IsNullOrEmpty(this.mOverwriteLeaderSkillIname))
        #GameManagerinstanceDirect=MonoSingleton<GameManager>.GetInstanceDirect()
        #if(Object.op_Inequality((Object)instanceDirect,(Object)null)&&instanceDirect.MasterParam!=null)
        #this.mOverwriteLeaderSkillLevel=(int)instanceDirect.MasterParam.FixParam.TobiraLvCap
    if 'priority' in json:
        this['mPriority'] = json['priority']
return this
