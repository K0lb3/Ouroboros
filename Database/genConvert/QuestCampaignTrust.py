def QuestCampaignTrust(json):
    this={}#QuestCampaignTrustjson)
    if 'children_iname' in json:
        this['iname'] = json['children_iname']
    if 'concept_card' in json:
        this['concept_card'] = json['concept_card']
    if 'card_trust_lottery_rate' in json:
        this['card_trust_lottery_rate'] = json['card_trust_lottery_rate']
    if 'card_trust_qe_bonus' in json:
        this['card_trust_qe_bonus'] = json['card_trust_qe_bonus']
    #returntrue
return this
