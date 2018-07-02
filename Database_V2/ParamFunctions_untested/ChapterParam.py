def ChapterParam(json):
    this={}#ChapterParamjson)
    #if(json==null)
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
    #this.keys=newList<KeyItem>()
    #if(!string.IsNullOrEmpty(json.keyitem1)&&json.keynum1>0)
    #this.keys.Add(newKeyItem()
        #iname=json.keyitem1,
        #num=json.keynum1
        #})
        #if(this.keys.Count>0)
        if 'keytime' in json:
            this['keytime'] = json['keytime']
        #this.quests.Clear()
    #
    #publicboolIsAvailable(DateTimet)
        #if(this.end<=0L)
        #return!this.hidden
        #DateTimedateTime1=TimeManager.FromUnixTime(this.start)
        #DateTimedateTime2=TimeManager.FromUnixTime(this.end)
        #if(dateTime1<=t)
        #returnt<dateTime2
        #returnfalse
    #
    #publicboolIsKeyQuest()
        #returnthis.keys.Count>0
    #
    #publicKeyQuestTypesGetKeyQuestType()
        #if(!this.IsKeyQuest())
        #returnKeyQuestTypes.None
        #returnthis.keytime!=0L?KeyQuestTypes.Timer:KeyQuestTypes.Count
    #
    #publicboolIsGpsQuest()
        #if(this.quests!=null)
            #for(intindex=0index<this.quests.Count++index)
                #if(this.quests.type==QuestTypes.Gps)
                #returntrue
        #for(intindex=0index<this.children.Count++index)
            #if(this.children.IsGpsQuest())
            #returntrue
        #returnfalse
    #
    #publicboolIsTowerQuest()
        #if(this.quests!=null)
            #for(intindex=0index<this.quests.Count++index)
                #if(this.quests.type==QuestTypes.Tower||this.quests.type==QuestTypes.MultiTower)
                #returntrue
        #for(intindex=0index<this.children.Count++index)
            #if(this.children.IsTowerQuest())
            #returntrue
        #returnfalse
    #
    #publicboolIsBeginnerQuest()
        #if(this.quests!=null)
            #for(intindex=0index<this.quests.Count++index)
                #if(this.quests.type==QuestTypes.Beginner)
                #returntrue
        #for(intindex=0index<this.children.Count++index)
            #if(this.children.IsBeginnerQuest())
            #returntrue
        #returnfalse
    #
    #publicboolIsMultiGpsQuest()
        #if(this.quests!=null)
            #for(intindex=0index<this.quests.Count++index)
                #if(this.quests.type==QuestTypes.MultiGps)
                #returntrue
        #for(intindex=0index<this.children.Count++index)
            #if(this.children.IsMultiGpsQuest())
            #returntrue
        #returnfalse
    #
    #publicboolIsOrdealQuest()
        #if(this.quests!=null)
            #for(intindex=0index<this.quests.Count++index)
                #if(this.quests.type==QuestTypes.Ordeal)
                #returntrue
        #for(intindex=0index<this.children.Count++index)
            #if(this.children.IsOrdealQuest())
            #returntrue
        #returnfalse
    #
    #publicSubQuestTypesGetSubQuestType()
        #if(this.quests!=null&&this.quests.Count>0)
        #returnthis.quests[0].subtype
        #if(this.children!=null&&this.children.Count>0)
        #returnthis.children[0].GetSubQuestType()
        #returnSubQuestTypes.Normal
    #
    #publicboolHasGpsQuest()
        #if(this.quests!=null)
            #for(intindex=0index<this.quests.Count++index)
                #if(this.quests.gps_enable)
                #returntrue
        #for(intindex=0index<this.children.Count++index)
            #if(this.children.HasGpsQuest())
            #returntrue
        #returnfalse
    #
    #publicboolIsDateUnlock(longunixtime)
        #for(intindex=0index<this.quests.Count++index)
            #if(this.quests.IsDateUnlock(unixtime))
            #returntrue
        #returnfalse
    #
    #publicboolIsKeyUnlock(longunixtime)
        #if(!this.IsKeyQuest()||!this.IsDateUnlock(unixtime))
        #returnfalse
        #KeyQuestTypeskeyQuestType=this.GetKeyQuestType()
        #if(this.key_end<=0L)
        #returnfalse
        #switch(keyQuestType)
            #caseKeyQuestTypes.Timer:
            #returnunixtime<this.key_end
            #caseKeyQuestTypes.Count:
            #for(intindex=0index<this.quests.Count++index)
                #if(this.quests.CheckEnableChallange())
                #returntrue
            #returnfalse
            #default:
            #returnfalse
    #
    #publicboolCheckHasKeyItem()
        #for(intindex=0index<this.keys.Count++index)
            #if(this.keys.IsHasItem())
            #returntrue
        #returnfalse
return this
