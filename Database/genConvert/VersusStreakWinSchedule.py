def VersusStreakWinSchedule(json):
    this={}#VersusStreakWinSchedulejson)
    #if(json==null)
    #returnfalse
    if 'id' in json:
        this['id'] = json['id']
    #try
        if 'judge' in json:
            this['judge'] = ENUM['STREAK_JUDGE'][json['judge']]
        #if(!string.IsNullOrEmpty(json.begin_at))
        if 'begin_at' in json:
            this['begin_at'] = DateTime.Parse
        #if(!string.IsNullOrEmpty(json.end_at))
        if 'end_at' in json:
            this['end_at'] = DateTime.Parse
    #catch(Exceptionex)
        #DebugUtility.LogError(ex.Message)
        #returnfalse
    #returntrue
return this
