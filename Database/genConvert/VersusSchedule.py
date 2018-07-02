def VersusSchedule(json):
    this={}#VersusSchedulejson)
    #if(json==null)
    #return
    if 'tower_iname' in json:
        this['tower_iname'] = json['tower_iname']
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'begin_at' in json:
        this['begin_at'] = json['begin_at']
    if 'end_at' in json:
        this['end_at'] = json['end_at']
    if 'gift_begin_at' in json:
        this['gift_begin_at'] = json['gift_begin_at']
    if 'gift_end_at' in json:
        this['gift_end_at'] = json['gift_end_at']
    #try
        #this.BeginDate=DateTime.Parse(this.begin_at)
        #this.EndDate=DateTime.Parse(this.end_at)
        #this.GiftBeginDate=DateTime.Parse(this.gift_begin_at)
        #this.GiftEndDate=DateTime.Parse(this.gift_end_at)
    #catch(Exceptionex)
        #DebugUtility.Log(ex.ToString())
return this
