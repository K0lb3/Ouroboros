def AbilityParam(json):
    this={}#AbilityParamjson)
    #if(json==null)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'expr' in json:
        this['expr'] = json['expr']
    if 'icon' in json:
        this['icon'] = json['icon']
    if 'type' in json:
        this['type'] = ENUM['EAbilityType'][json['type']]
    if 'slot' in json:
        this['slot'] = ENUM['EAbilitySlot'][json['slot']]
    if 'cap' in json:
        this['lvcap'] = Math.Max(json['cap'],1)
    if 'fix' in json:
        this['is_fixed'] = json['fix']!=0
    #intlength=0
    #stringstrArray=newstring[10]
        #json.skl1,
        #json.skl2,
        #json.skl3,
        #json.skl4,
        #json.skl5,
        #json.skl6,
        #json.skl7,
        #json.skl8,
        #json.skl9,
        #json.skl10
        #}
        #for(intindex=0index<strArray.Length&&!string.IsNullOrEmpty(strArray)++index)
        #++length
        #if(length>0)
            #intnumArray=newint[10]
                #json.lv1,
                #json.lv2,
                #json.lv3,
                #json.lv4,
                #json.lv5,
                #json.lv6,
                #json.lv7,
                #json.lv8,
                #json.lv9,
                #json.lv10
                #}
                #this.skills=newLearningSkill[length]
                #for(intindex=0index<length++index)
                    #this.skills=newLearningSkill()
                    #this.skills.iname=strArray
                    #this.skills.locklv=numArray
            #this.condition_units=(string)null
            #if(json.units!=null&&json.units.Length>0)
                #for(intindex=0index<json.units.Length++index)
                if 'units' in json:
                    this['condition_units'] = json['units']
            if 'units_cnds_type' in json:
                this['units_conditions_type'] = ENUM['EUseConditionsType'][json['units_cnds_type']]
            #this.condition_jobs=(string)null
            #if(json.jobs!=null&&json.jobs.Length>0)
                #for(intindex=0index<json.jobs.Length++index)
                if 'jobs' in json:
                    this['condition_jobs'] = json['jobs']
            if 'jobs_cnds_type' in json:
                this['jobs_conditions_type'] = ENUM['EUseConditionsType'][json['jobs_cnds_type']]
            if 'birth' in json:
                this['condition_birth'] = json['birth']
            if 'sex' in json:
                this['condition_sex'] = ENUM['ESex'][json['sex']]
            if 'elem' in json:
                this['condition_element'] = ENUM['EElement'][json['elem']]
            if 'rmin' in json:
                this['condition_raremin'] = json['rmin']
            if 'rmax' in json:
                this['condition_raremax'] = json['rmax']
            if 'type_detail' in json:
                this['type_detail'] = ENUM['EAbilityTypeDetail'][json['type_detail']]
            #returntrue
        #
        #publicintGetRankCap()
            #return(int)this.lvcap
        #
        #publicboolCheckEnableUseAbility(UnitDataself,intjob_index)
            #if(this.condition_units!=null)
                #boolflag=Array.Find<string>(this.condition_units,(Predicate<string>)(p=>p==self.UnitParam.iname))!=null
                #if(!(this.units_conditions_type!=EUseConditionsType.Match?!flag:flag))
                #returnfalse
            #if(this.condition_jobs!=null)
                #JobDatajob=self.GetJobData(job_index)
                #if(job==null)
                #returnfalse
                #boolflag=Array.Find<string>(this.condition_jobs,(Predicate<string>)(p=>p==job.JobID))!=null
                #if(!flag&&!string.IsNullOrEmpty(job.Param.origin))
                    #JobParamoriginJobParam=MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(job.Param.origin)
                    #if(originJobParam!=null)
                    #flag=Array.Find<string>(this.condition_jobs,(Predicate<string>)(p=>p==originJobParam.iname))!=null
                #if(!(this.jobs_conditions_type!=EUseConditionsType.Match?!flag:flag))
                #returnfalse
            #return(string.IsNullOrEmpty(this.condition_birth)||!((string)self.UnitParam.birth!=this.condition_birth))&&(this.condition_sex==ESex.Unknown||self.UnitParam.sex==this.condition_sex)&&(this.condition_element==EElement.None||self.Element==this.condition_element)&&((int)this.condition_raremax==0||(int)this.condition_raremin<=self.Rarity&&(int)this.condition_raremax>=self.Rarity&&(int)this.condition_raremax>=(int)this.condition_raremin)
        #
        #publicList<UnitParam>FindConditionUnitParams(MasterParammasterParam=null)
            #List<UnitParam>unitParamList=newList<UnitParam>()
            #if(this.condition_units!=null)
                #for(intindex=0index<this.condition_units.Length++index)
                    #if(!string.IsNullOrEmpty(this.condition_units))
                        #UnitParamunitParam=(UnitParam)null
                        #stringconditionUnit=this.condition_units
                        #if(masterParam==null)
                            #GameManagerinstanceDirect=MonoSingleton<GameManager>.GetInstanceDirect()
                            #if(UnityEngine.Object.op_Inequality((UnityEngine.Object)instanceDirect,(UnityEngine.Object)null))
                            #unitParam=instanceDirect.GetUnitParam(conditionUnit)
                        #else
                        #unitParam=masterParam.GetUnitParam(conditionUnit)
                        #if(unitParam!=null)
                        #unitParamList.Add(unitParam)
            #returnunitParamList
        #
        #publicList<JobParam>FindConditionJobParams(MasterParammasterParam=null)
            #List<JobParam>jobParamList=newList<JobParam>()
            #if(this.condition_jobs!=null)
                #for(intindex=0index<this.condition_jobs.Length++index)
                    #if(!string.IsNullOrEmpty(this.condition_jobs))
                        #JobParamjobParam=(JobParam)null
                        #stringconditionJob=this.condition_jobs
                        #if(masterParam==null)
                            #GameManagerinstanceDirect=MonoSingleton<GameManager>.GetInstanceDirect()
                            #if(UnityEngine.Object.op_Inequality((UnityEngine.Object)instanceDirect,(UnityEngine.Object)null))
                            #jobParam=instanceDirect.GetJobParam(conditionJob)
                        #else
                        #jobParam=masterParam.GetJobParam(conditionJob)
                        #if(jobParam!=null)
                        #jobParamList.Add(jobParam)
            #returnjobParamList
        #
        #publicstaticstringTypeDetailToSpriteSheetKey(EAbilityTypeDetailtypeDetail)
            #stringstr=string.Empty
            #switch(typeDetail)
                #caseEAbilityTypeDetail.Job_Unique:
                #caseEAbilityTypeDetail.Job_Basic:
                #caseEAbilityTypeDetail.Job_Support:
                #caseEAbilityTypeDetail.Job_Reaction:
                #str="ABILITY_TITLE_NORMAL"
                #break
                #caseEAbilityTypeDetail.MasterAbility:
                #str="ABILITY_TITLE_MASTER"
                #break
                #caseEAbilityTypeDetail.WeaponAbility:
                #str="ABILITY_TITLE_WEAPON"
                #break
                #caseEAbilityTypeDetail.VisionAbility:
                #str="ABILITY_TITLE_VISION"
                #break
            #returnstr
return this
