def AbilityParam(json):
    this={}#AbilityParamjson)
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
        #++length
        #if(length>0)
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
                if 'units' in json:
                    this['condition_units'] = json['units']
                if 'jobs' in json:
                    this['condition_jobs'] = json['jobs']
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
            #returntrue
        #
        #publicintGetRankCap()
            #return(int)this.lvcap
        #
        #publicboolCheckEnableUseAbility(UnitDataself,intjob_index)
            #//ISSUE:objectofacompiler-generatedtypeiscreated
            #//ISSUE:variableofacompiler-generatedtype
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedmethod
            #returnfalse
                #//ISSUE:objectofacompiler-generatedtypeiscreated
                #//ISSUE:variableofacompiler-generatedtype
                #//ISSUE:referencetoacompiler-generatedfield
                #//ISSUE:referencetoacompiler-generatedfield
                #//ISSUE:referencetoacompiler-generatedfield
                #returnfalse
                #//ISSUE:referencetoacompiler-generatedmethod
                    #//ISSUE:objectofacompiler-generatedtypeiscreated
                    #//ISSUE:variableofacompiler-generatedtype
                    #//ISSUE:referencetoacompiler-generatedfield
                    #if(string.IsNullOrEmpty(abilityCAnonStorey209.job.Param.origin))
                    #returnfalse
                    #//ISSUE:referencetoacompiler-generatedfield
                    #//ISSUE:referencetoacompiler-generatedfield
                    #//ISSUE:referencetoacompiler-generatedfield
                    #//ISSUE:referencetoacompiler-generatedmethod
                    #returnfalse
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedfield
return this
