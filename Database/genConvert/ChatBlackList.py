def ChatBlackList(json):
    this={}#ChatBlackListjson)
    #return
        if 'blacklist' in json:
            this['lists'] = newChatBlackListParam[json['blacklist'].Length]
        if 'blacklist' in json:
            this['lists[index]'] = json['blacklist'][index]
    #else
    if 'total' in json:
        this['total'] = json['total']
return this
