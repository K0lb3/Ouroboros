def ReqTowerResuponse.Json_RankStatus(json):
    this={}#ReqTowerResuponse.Json_RankStatusjson)
    #if(json==null)
    #return
    if 'turn_num' in json:
        this['turn_num'] = json['turn_num']
    if 'died_num' in json:
        this['died_num'] = json['died_num']
    if 'retire_num' in json:
        this['retire_num'] = json['retire_num']
    if 'recovery_num' in json:
        this['recover_num'] = json['recovery_num']
    if 'spd_rank' in json:
        this['speedRank'] = json['spd_rank']
    if 'tec_rank' in json:
        this['techRank'] = json['tec_rank']
    if 'spd_score' in json:
        this['spd_score'] = json['spd_score']
    if 'tec_score' in json:
        this['tec_score'] = json['tec_score']
    if 'ret_score' in json:
        this['ret_score'] = json['ret_score']
    if 'rcv_score' in json:
        this['rcv_score'] = json['rcv_score']
    if 'challenge_num' in json:
        this['challenge_num'] = json['challenge_num']
    if 'lose_num' in json:
        this['lose_num'] = json['lose_num']
    if 'reset_num' in json:
        this['reset_num'] = json['reset_num']
    if 'challenge_score' in json:
        this['challenge_score'] = json['challenge_score']
    if 'lose_score' in json:
        this['lose_score'] = json['lose_score']
    if 'reset_score' in json:
        this['reset_score'] = json['reset_score']
return this
