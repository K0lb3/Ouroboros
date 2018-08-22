from ParamFunctions._variables import RAWBIRTH,RAWELEMENT,ENUM

CondBirth=list(RAWBIRTH.items())

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
        #this['birth_id'] = json['birth_id']
        this['birth']= [
            CondBirth[ID][1]
            for ID in json['birth_id']
        ]
    
    this['conditions_elements']=[]
    if 'el_fire' in json:
        this['conditions_elements'].append('Fire')
    if 'el_watr' in json:
        this['conditions_elements'].append('Water')
    if 'el_wind' in json:
        this['conditions_elements'].append('Wind')
    if 'el_thdr' in json:
        this['conditions_elements'].append('Thunder')
    if 'el_lit' in json:
        this['conditions_elements'].append('Light')
    if 'el_drk' in json:
        this['conditions_elements'].append('Dark')
    if len(this['conditions_elements']) == 0:
        del this['conditions_elements']
    #this['element_sum'] = len(this['conditions_elements'])

    #returntrue
    return this
