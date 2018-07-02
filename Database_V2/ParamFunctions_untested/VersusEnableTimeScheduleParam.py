def VersusEnableTimeScheduleParam(json):
    this={}#VersusEnableTimeScheduleParamjson)
    if 'begin_time' in json:
        this['mBegin'] = json['begin_time']
    if 'open_time' in json:
        this['mOpen'] = json['open_time']
    if 'quest_iname' in json:
        this['mQuestIname'] = json['quest_iname']
    #try
        #if(json.add_date!=null)
            #this.mAddDateList=newList<DateTime>()
            #for(intindex=0index<json.add_date.Length++index)
                #if(!string.IsNullOrEmpty(json.add_date))
                #this.mAddDateList.Add(DateTime.Parse(json.add_date))
    #catch(Exceptionex)
        #DebugUtility.LogError(ex.Message)
        #returnfalse
    #returntrue
return this
