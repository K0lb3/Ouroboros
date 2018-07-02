def VersusRankRankingRewardParam(json):
    this={}#VersusRankRankingRewardParamjson)
    #if(json==null)
    #returnfalse
    if 'schedule_id' in json:
        this['mScheduleId'] = json['schedule_id']
    if 'rank_begin' in json:
        this['mRankBegin'] = json['rank_begin']
    if 'rank_end' in json:
        this['mRankEnd'] = json['rank_end']
    if 'reward_id' in json:
        this['mRewardId'] = json['reward_id']
    #returntrue
return this
