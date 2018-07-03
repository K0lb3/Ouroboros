from ParamFunctions._variables import RAWBIRTH,RAWELEMENT,ENUM,TRANSLATION
def AbilityParam(json):
    this={}#AbilityParamjson)

    if 'iname' in json:
        this['iname'] = json['iname']

    if 'name' in json:
        this['kanji'] = json['name']
    if json['iname'] in TRANSLATION:
        json.update(TRANSLATION[json['iname']])
    
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
        this['lvcap'] = max(json['cap'],1)
    if 'fix' in json:
        this['is_fixed'] = json['fix']!=0
    this['skills']=[]
    for i in range(1,11):
        skill='skl{}'.format(i)
        lv =  'lv{}'.format(i)
        if skill in json:
            this['skills'].append({
                'iname' : json[skill],
                'locklv': json[lv] if lv in json else None
            })
        
    if 'units' in json:
        this['condition_units'] = json['units']
    if 'units_cnds_type' in json:
        this['units_conditions_type'] = ENUM['EUseConditionsType'][json['units_cnds_type']]
    if 'jobs' in json:
        this['condition_jobs'] = json['jobs']
    if 'jobs_cnds_type' in json:
        this['jobs_conditions_type'] = ENUM['EUseConditionsType'][json['jobs_cnds_type']]
    if 'birth' in json:
        this['condition_birth'] = RAWBIRTH[json['birth']]
    if 'sex' in json:
        this['condition_sex'] = ENUM['ESex'][json['sex']]
    if 'elem' in json:
        this['condition_element'] = RAWELEMENT[json['elem']]
    if 'rmin' in json:
        this['condition_raremin'] = json['rmin']
    if 'rmax' in json:
        this['condition_raremax'] = json['rmax']
    if 'type_detail' in json:
        this['type_detail'] = ENUM['EAbilityTypeDetail'][json['type_detail']]

    return this
