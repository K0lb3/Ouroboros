def ChapterParam(json):
    this={}#ChapterParamjson)
    #thrownewInvalidJSONException()
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'expr' in json:
        this['expr'] = json['expr']
    if 'world' in json:
        this['world'] = json['world']
    if 'start' in json:
        this['start'] = json['start']
    if 'end' in json:
        this['end'] = json['end']
    if 'hide' in json:
        this['hidden'] = json['hide']!=0
    if 'chap' in json:
        this['section'] = json['chap']
    if 'banr' in json:
        this['banner'] = json['banr']
    if 'item' in json:
        this['prefabPath'] = json['item']
    if 'hurl' in json:
        this['helpURL'] = json['hurl']
    #if(!string.IsNullOrEmpty(json.keyitem1)&&json.keynum1>0)
    #this.keys.Add(newKeyItem()
        #})
        #if(this.keys.Count>0)
        if 'keytime' in json:
            this['keytime'] = json['keytime']
        #this.quests.Clear()
    #
    #publicboolIsAvailable(DateTimet)
        #return!this.hidden
        #returnt<dateTime2
        #returnfalse
    #
    #publicboolIsKeyQuest()
        #returnthis.keys.Count>0
    #
    #publicKeyQuestTypesGetKeyQuestType()
        #if(!this.IsKeyQuest())
        #returnKeyQuestTypes.None
    #
    #publicboolIsGpsQuest()
                #returntrue
            #if(this.children[index].IsGpsQuest())
            #returntrue
        #returnfalse
    #
    #publicboolIsTowerQuest()
                #returntrue
            #if(this.children[index].IsTowerQuest())
            #returntrue
        #returnfalse
    #
    #publicboolIsBeginnerQuest()
                #returntrue
            #if(this.children[index].IsBeginnerQuest())
            #returntrue
        #returnfalse
    #
    #publicSubQuestTypesGetSubQuestType()
        #returnthis.quests[0].subtype
        #returnthis.children[0].GetSubQuestType()
        #returnSubQuestTypes.Normal
    #
    #publicboolHasGpsQuest()
                #if(this.quests[index].gps_enable)
                #returntrue
            #if(this.children[index].HasGpsQuest())
            #returntrue
        #returnfalse
    #
    #publicboolIsDateUnlock(longunixtime)
            #if(this.quests[index].IsDateUnlock(unixtime))
            #returntrue
        #returnfalse
    #
    #publicboolIsKeyUnlock(longunixtime)
        #if(!this.IsKeyQuest()||!this.IsDateUnlock(unixtime))
        #returnfalse
        #returnfalse
        #switch(keyQuestType)
            #caseKeyQuestTypes.Timer:
            #returnunixtime<this.key_end
            #caseKeyQuestTypes.Count:
                #if(this.quests[index].CheckEnableChallange())
                #returntrue
            #returnfalse
            #default:
            #returnfalse
    #
    #publicboolCheckHasKeyItem()
            #if(this.keys[index].IsHasItem())
            #returntrue
        #returnfalse
return this
