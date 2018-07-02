def ConceptCardConditionsParam(json):
    this={}#ConceptCardConditionsParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'un_group' in json:
        this['unit_group'] = json['un_group']
    if 'units_cnds_type' in json:
        this['units_conditions_type'] = ENUM['EUseConditionsType'][json['units_cnds_type']]
    if 'job_group' in json:
        this['job_group'] = json['job_group']
    if 'jobs_cnds_type' in json:
        this['jobs_conditions_type'] = ENUM['EUseConditionsType'][json['jobs_cnds_type']]
    if 'sex' in json:
        this['sex'] = ENUM['ESex'][json['sex']]
    #if(json.birth_id!=null)
        #for(intindex=0index<this.birth_id.Length++index)
        if 'birth_id' in json:
            this['birth_id'] = json['birth_id']
    #this.conditions_elements=newDictionary<EElement,int>()
    #this.conditions_elements.Add(EElement.Fire,json.el_fire)
    #this.conditions_elements.Add(EElement.Water,json.el_watr)
    #this.conditions_elements.Add(EElement.Wind,json.el_wind)
    #this.conditions_elements.Add(EElement.Thunder,json.el_thdr)
    #this.conditions_elements.Add(EElement.Shine,json.el_lit)
    #this.conditions_elements.Add(EElement.Dark,json.el_drk)
    if 'el_fire' in json:
        this['element_sum'] = json['el_fire']+json.el_watr+json.el_wind+json.el_thdr+json.el_lit+json.el_drk
    #returntrue
return this
