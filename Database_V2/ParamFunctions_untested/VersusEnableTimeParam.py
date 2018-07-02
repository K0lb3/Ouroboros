def VersusEnableTimeParam(json):
    this={}#VersusEnableTimeParamjson)
    #if(json==null)
    #returnfalse
    if 'id' in json:
        this['mScheduleId'] = json['id']
    if 'mode' in json:
        this['mVersusType'] = ENUM['VERSUS_TYPE'][json['mode']]
    #try
        #if(!string.IsNullOrEmpty(json.begin_at))
        if 'begin_at' in json:
            this['mBeginAt'] = DateTime.Parse
        #if(!string.IsNullOrEmpty(json.end_at))
        if 'end_at' in json:
            this['mEndAt'] = DateTime.Parse
    #catch(Exceptionex)
        #DebugUtility.LogError(ex.Message)
        #returnfalse
    #this.mSchedule=newList<VersusEnableTimeScheduleParam>()
    #for(intindex=0index<json.schedule.Length++index)
        #VersusEnableTimeScheduleParamtimeScheduleParam=newVersusEnableTimeScheduleParam()
        #if(timeScheduleParam.Deserialize(json.schedule))
        #this.mSchedule.Add(timeScheduleParam)
    if 'draft_type' in json:
        this['mDraftType'] = ENUM['VersusDraftType'][json['draft_type']]
    #returntrue
return this
