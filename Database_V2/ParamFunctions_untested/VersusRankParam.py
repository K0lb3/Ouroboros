def VersusRankParam(json):
    this={}#VersusRankParamjson)
    #if(json==null)
    #returnfalse
    if 'id' in json:
        this['mId'] = json['id']
    if 'btl_mode' in json:
        this['mVSMode'] = ENUM['VS_MODE'][json['btl_mode']]
    if 'name' in json:
        this['mName'] = json['name']
    if 'limit' in json:
        this['mLimit'] = json['limit']
    if 'win_pt_base' in json:
        this['mWinPointBase'] = json['win_pt_base']
    if 'lose_pt_base' in json:
        this['mLosePointBase'] = json['lose_pt_base']
    if 'hurl' in json:
        this['mHUrl'] = json['hurl']
    #this.mDisableDateList=newList<DateTime>()
    #try
        #if(!string.IsNullOrEmpty(json.begin_at))
        if 'begin_at' in json:
            this['mBeginAt'] = DateTime.Parse
        #if(!string.IsNullOrEmpty(json.end_at))
        if 'end_at' in json:
            this['mEndAt'] = DateTime.Parse
        #if(json.disabledate!=null)
            #for(intindex=0index<json.disabledate.Length++index)
                #if(!string.IsNullOrEmpty(json.disabledate))
                #this.mDisableDateList.Add(DateTime.Parse(json.disabledate))
    #catch(Exceptionex)
        #DebugUtility.LogError(ex.Message)
        #returnfalse
    #returntrue
return this
