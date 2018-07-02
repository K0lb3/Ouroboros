def TobiraCondsParam(json):
    this={}#TobiraCondsParamjson)
    #if(json==null)
    #return
    if 'unit_iname' in json:
        this['mUnitIname'] = json['unit_iname']
    if 'category' in json:
        this['mCategory'] = json['category']
    #this.mConditions.Clear()
    #if(json.conds==null)
    #return
    #for(intindex=0index<json.conds.Length++index)
        #TobiraConditionParamtobiraConditionParam=newTobiraConditionParam()
        #tobiraConditionParam.Deserialize(json.conds)
        #this.mConditions.Add(tobiraConditionParam)
return this
