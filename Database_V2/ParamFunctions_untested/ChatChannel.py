def ChatChannel(json):
    this={}#ChatChanneljson)
    #if(json==null||json.channels==null)
    #return
    if 'channels' in json:
        this['channels'] = newChatChannelParam[json['channels'].Length]
    #ChatChannelMasterParamchatChannelMaster=MonoSingleton<GameManager>.Instance.GetChatChannelMaster()
    #for(intindex=0index<json.channels.Length++index)
        if 'channels' in json:
            this['channels'] = json['channels']
        #if(chatChannelMaster.Length>=this.channels.id)
            #this.channels.category_id=(int)chatChannelMaster[this.channels.id-1].category_id
            #this.channels.name=chatChannelMaster[this.channels.id-1].name
return this
