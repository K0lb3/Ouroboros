def TobiraCondsParam(json):
    this={}#TobiraCondsParamjson)
    if 'unit_iname' in json:
        this['mUnitIname'] = json['unit_iname']
    if 'category' in json:
        this['mCategory'] = json['category']
    this['mConditions']=[]
    if('conds' in json):
        this['mConditions']=[{
            'mCondIname' : cond['conds_iname'],
            'mCondType'  : cond['conds_type']
            }
            for cond in json['conds']
        ]
    return this
