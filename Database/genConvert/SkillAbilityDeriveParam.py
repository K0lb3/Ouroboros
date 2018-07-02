def SkillAbilityDeriveParam(json):
    this={}#SkillAbilityDeriveParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    #ESkillAbilityDeriveCondsarray1=((IEnumerable<ESkillAbilityDeriveConds>)newESkillAbilityDeriveConds[3]
        #(ESkillAbilityDeriveConds)json.trig_type_1,
        #(ESkillAbilityDeriveConds)json.trig_type_2,
        #(ESkillAbilityDeriveConds)json.trig_type_3
        #}).Where<ESkillAbilityDeriveConds>((Func<ESkillAbilityDeriveConds,bool>)(trig_type=>trig_type!=ESkillAbilityDeriveConds.Unknown)).ToArray<ESkillAbilityDeriveConds>()
        #stringarray2=((IEnumerable<string>)newstring[3]
            #json.trig_iname_1,
            #json.trig_iname_2,
            #json.trig_iname_3
            #}).Where<string>((Func<string,bool>)(trig_iname=>!string.IsNullOrEmpty(trig_iname))).ToArray<string>()
            #this.deriveTriggers=newSkillAbilityDeriveTriggerParam[array2.Length]
            #for(intindex=0index<array2.Length++index)
            #this.deriveTriggers=newSkillAbilityDeriveTriggerParam(array1,array2)
            #if(json.base_abils!=null)
                #for(intindex=0index<this.base_abils.Length++index)
                if 'base_abils' in json:
                    this['base_abils'] = json['base_abils']
            #if(json.derive_abils!=null)
                #for(intindex=0index<this.derive_abils.Length++index)
                if 'derive_abils' in json:
                    this['derive_abils'] = json['derive_abils']
            #if(json.base_skills!=null)
                #for(intindex=0index<this.base_skills.Length++index)
                if 'base_skills' in json:
                    this['base_skills'] = json['base_skills']
            #if(json.base_skills==null)
            #return
            #for(intindex=0index<this.derive_skills.Length++index)
            if 'derive_skills' in json:
                this['derive_skills'] = json['derive_skills']
        #
        #publicvoidDeserialize(JSON_SkillAbilityDeriveParamjson,MasterParammasterParam)
            #this.Deserialize(json)
            #this.FindSkillAbilityDeriveParams(masterParam)
        #
        #privatevoidFindSkillAbilityDeriveParams(MasterParammasterParam)
            #if(masterParam==null)
            #return
            #this.m_SkillDeriveParams=this.GetSkillDeriveParams(masterParam)
            #this.m_AbilityDeriveParams=this.GetAbilityDeriveParams(masterParam)
        #
        #publicboolCheckContainsTriggerIname(ESkillAbilityDeriveCondsconditionsType,stringtriggerIname)
            #if(string.IsNullOrEmpty(triggerIname))
            #returnfalse
            #inthashCode=triggerIname.GetHashCode()
            #for(intindex=0index<this.deriveTriggers.Length++index)
                #if(this.deriveTriggers.m_TriggerType==conditionsType&&!string.IsNullOrEmpty(this.deriveTriggers.m_TriggerIname)&&this.deriveTriggers.m_TriggerIname.GetHashCode()==hashCode)
                #returntrue
            #returnfalse
        #
        #publicboolCheckContainsTriggerInames(SkillAbilityDeriveTriggerParamsearchKeyTriggerParam)
            #if(searchKeyTriggerParam==null||searchKeyTriggerParam.Length!=this.deriveTriggers.Length)
            #returnfalse
            #boolflagArray=newbool[searchKeyTriggerParam.Length]
            #for(intindex1=0index1<this.deriveTriggers.Length++index1)
                #if(this.deriveTriggers[index1].m_TriggerType==searchKeyTriggerParam[index1].m_TriggerType&&!string.IsNullOrEmpty(this.deriveTriggers[index1].m_TriggerIname))
                    #for(intindex2=0index2<searchKeyTriggerParam.Length++index2)
                        #if(!flagArray[index2]&&!string.IsNullOrEmpty(searchKeyTriggerParam[index2].m_TriggerIname)&&this.deriveTriggers[index1].m_TriggerIname.GetHashCode()==searchKeyTriggerParam[index2].m_TriggerIname.GetHashCode())
                            #flagArray[index2]=true
                            #break
            #return((IEnumerable<bool>)flagArray).Count<bool>((Func<bool,bool>)(value=>value))>=searchKeyTriggerParam.Length
        #
        #privateList<SkillDeriveParam>GetSkillDeriveParams(MasterParammasterParam)
            #List<SkillDeriveParam>skillDeriveParamList=newList<SkillDeriveParam>()
            #if(this.base_skills==null||this.derive_skills==null)
            #returnskillDeriveParamList
            #for(intindex=0index<this.base_skills.Length++index)
                #stringbaseSkill=this.base_skills
                #stringderiveSkill=this.derive_skills
                #if((!string.IsNullOrEmpty(baseSkill)||!string.IsNullOrEmpty(deriveSkill))&&(!string.IsNullOrEmpty(baseSkill)&&!string.IsNullOrEmpty(deriveSkill)))
                    #SkillDeriveParamskillDeriveParam=newSkillDeriveParam()
                    #skillDeriveParam.m_BaseParam=masterParam.GetSkillParam(baseSkill)
                    #skillDeriveParam.m_DeriveParam=masterParam.GetSkillParam(deriveSkill)
                    #skillDeriveParam.m_SkillAbilityDeriveParam=this
                    #skillDeriveParamList.Add(skillDeriveParam)
            #returnskillDeriveParamList
        #
        #privateList<AbilityDeriveParam>GetAbilityDeriveParams(MasterParammasterParam)
            #List<AbilityDeriveParam>abilityDeriveParamList=newList<AbilityDeriveParam>()
            #if(this.base_abils==null||this.derive_abils==null)
            #returnabilityDeriveParamList
            #for(intindex=0index<this.base_abils.Length++index)
                #stringbaseAbil=this.base_abils
                #stringderiveAbil=this.derive_abils
                #if((!string.IsNullOrEmpty(baseAbil)||!string.IsNullOrEmpty(deriveAbil))&&(!string.IsNullOrEmpty(baseAbil)&&!string.IsNullOrEmpty(deriveAbil)))
                    #AbilityDeriveParamabilityDeriveParam=newAbilityDeriveParam()
                    #abilityDeriveParam.m_BaseParam=masterParam.GetAbilityParam(baseAbil)
                    #abilityDeriveParam.m_DeriveParam=masterParam.GetAbilityParam(deriveAbil)
                    #abilityDeriveParam.m_SkillAbilityDeriveParam=this
                    #abilityDeriveParamList.Add(abilityDeriveParam)
            #returnabilityDeriveParamList
return this
