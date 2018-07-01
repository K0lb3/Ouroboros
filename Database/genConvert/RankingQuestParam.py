def RankingQuestParam(json):
    this={}#RankingQuestParamjson)
    if 'schedule_id' in json:
        this['schedule_id'] = json['schedule_id']
    #if(Enum.GetNames(typeof(RankingQuestType)).Length>json.type)
    if 'type' in json:
        this['type'] = ENUM['RankingQuestType'][json['type']]
    #else
    #DebugUtility.LogError("定義されていない列挙値が指定されようとしました")
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'reward_id' in json:
        this['reward_id'] = json['reward_id']
    #returntrue
return this
