from ParamFunctions._variables import RAWBIRTH,RAWELEMENT,ENUM
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
    if 'birth_id' in json:
        this['birth_id'] = json['birth_id']
    
    this['conditions_elements']={}
    if 'el_fire' in json:
        this['conditions_elements']['Fire'] = json['el_fire']
    if 'el_watr' in json:
        this['conditions_elements']['Water']= json['el_watr']
    if 'el_wind' in json:
        this['conditions_elements']['Wind']= json['el_wind']
    if 'el_thdr' in json:
        this['conditions_elements']['Thunder']= json['el_thdr']
    if 'el_lit' in json:
        this['conditions_elements']['Light']= json['el_lit']
    if 'el_drk' in json:
        this['conditions_elements']['Dark']= json['el_drk']

    this['element_sum'] = len(this['conditions_elements'])

    #returntrue
    return this
