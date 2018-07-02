def TobiraCondsUnitParam(json):
    this={}#TobiraCondsUnitParamjson)
    #if(json==null)
    #return
    if 'id' in json:
        this['mId'] = json['id']
    if 'unit_iname' in json:
        this['mUnitIname'] = json['unit_iname']
    if 'lv' in json:
        this['mLevel'] = json['lv']
    if 'awake_lv' in json:
        this['mAwakeLevel'] = json['awake_lv']
    if 'job_iname' in json:
        this['mJobIname'] = json['job_iname']
    if 'job_lv' in json:
        this['mJobLevel'] = json['job_lv']
    if 'category' in json:
        this['mCategory'] = json['category']
    if 'tobira_lv' in json:
        this['mTobiraLv'] = json['tobira_lv']
    #this.UpdateConditionsFlag()
return this
