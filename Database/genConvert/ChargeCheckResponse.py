def ChargeCheckResponse(json):
    this={}#ChargeCheckResponsejson)
    #if(json==null)
    #returnfalse
    if 'age' in json:
        this['Age'] = json['age']
    if 'accept_ids' in json:
        this['AcceptIds'] = json['accept_ids']
    if 'reject_ids' in json:
        this['RejectIds'] = json['reject_ids']
    #if(this.RejectIds==null)
    #this.RejectIds=newstring[0]
    #returntrue
return this
