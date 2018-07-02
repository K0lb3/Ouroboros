def VersusRankClassParam(json):
    this={}#VersusRankClassParamjson)
    #if(json==null)
    #returnfalse
    if 'schedule_id' in json:
        this['mScheduleId'] = json['schedule_id']
    if 'type' in json:
        this['mClass'] = ENUM['RankMatchClass'][json['type']]
    if 'up_pt' in json:
        this['mUpPoint'] = json['up_pt']
    if 'down_pt' in json:
        this['mDownPoint'] = json['down_pt']
    if 'down_losing_streak' in json:
        this['mDownLosingStreak'] = json['down_losing_streak']
    if 'reward_id' in json:
        this['mRewardId'] = json['reward_id']
    if 'win_pt_max' in json:
        this['mWinPointMax'] = json['win_pt_max']
    if 'win_pt_min' in json:
        this['mWinPointMin'] = json['win_pt_min']
    if 'lose_pt_max' in json:
        this['mLosePointMax'] = json['lose_pt_max']
    if 'lose_pt_min' in json:
        this['mLosePointMin'] = json['lose_pt_min']
    #returntrue
return this
