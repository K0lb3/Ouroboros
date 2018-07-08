def QuestCampaignParentParam(json):
    this={}#QuestCampaignParentParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'children' in json:
        this['children'] = json['children']
    
    #this.beginAt=DateTime.MinValue
    #this.endAt=DateTime.MaxValue
    #if(!string.IsNullOrEmpty(json.begin_at))
    #DateTime.TryParse(json.begin_at,outthis.beginAt)
    #if(!string.IsNullOrEmpty(json.end_at))
    #DateTime.TryParse(json.end_at,outthis.endAt)
    #returntrue
return this
