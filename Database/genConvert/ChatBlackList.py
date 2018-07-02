def ChatBlackList(json):
    this={}#ChatBlackListjson)
    #if(json==null)
    #return
    #this.lists=(ChatBlackListParam)null
    #if(json.blacklist!=null)
        #for(intindex=0index<json.blacklist.Length++index)
        if 'blacklist' in json:
            this['lists'] = json['blacklist']
    #else
    #this.lists=newChatBlackListParam[0]
    if 'total' in json:
        this['total'] = json['total']
return this
