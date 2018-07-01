def RankingQuestRewardParam(json):
    this={}#RankingQuestRewardParamjson)
    if 'id' in json:
        this['id'] = json['id']
    #try
        if 'type' in json:
            this['type'] = ENUM['RankingQuestRewardType'][json['type']]
    #catch
        #DebugUtility.LogError("定義されていない列挙値が指定されようとしました")
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'num' in json:
        this['num'] = json['num']
    #returntrue
return this
