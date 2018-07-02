def QuestCampaignChildParam(json):
    this={}#QuestCampaignChildParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'scope' in json:
        this['scope'] = ENUM['QuestCampaignScopes'][json['scope']]
    if 'quest_type' in json:
        this['questType'] = ENUM['QuestTypes'][json['quest_type']]
    if 'quest_mode' in json:
        this['questMode'] = ENUM['QuestDifficulties'][json['quest_mode']]
    if 'quest_id' in json:
        this['questId'] = json['quest_id']
    if 'unit' in json:
        this['unit'] = json['unit']
    if 'drop_rate' in json:
        this['dropRate'] = json['drop_rate']
    if 'drop_num' in json:
        this['dropNum'] = json['drop_num']
    if 'exp_player' in json:
        this['expPlayer'] = json['exp_player']
    if 'exp_unit' in json:
        this['expUnit'] = json['exp_unit']
    if 'ap_rate' in json:
        this['apRate'] = json['ap_rate']
    #this.parents=newQuestCampaignParentParam[0]
    #this.campaignTrust=(QuestCampaignTrust)null
    #returntrue
return this
