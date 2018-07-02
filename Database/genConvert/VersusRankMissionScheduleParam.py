def VersusRankMissionScheduleParam(json):
    this={}#VersusRankMissionScheduleParamjson)
    #if(json==null)
    #returnfalse
    if 'schedule_id' in json:
        this['mScheduleId'] = json['schedule_id']
    if 'iname' in json:
        this['mIName'] = json['iname']
    #returntrue
return this
