def QuestClearUnlockUnitDataParam(json):
    this={}#QuestClearUnlockUnitDataParamjson)
    #return
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'uid' in json:
        this['uid'] = json['uid']
    if 'add' in json:
        this['add'] = json['add']>0
    if 'type' in json:
        this['type'] = json['type']
    if 'new_id' in json:
        this['new_id'] = json['new_id']
    if 'old_id' in json:
        this['old_id'] = json['old_id']
    if 'parent_id' in json:
        this['parent_id'] = json['parent_id']
    if 'ulv' in json:
        this['ulv'] = json['ulv']
    if 'aid' in json:
        this['aid'] = json['aid']
    if 'alv' in json:
        this['alv'] = json['alv']
    if 'qcnd' in json:
        this['qcnd'] = json['qcnd']>0
    #return
    if 'qids' in json:
        this['qids'] = newstring[json['qids'].Length]
    #json.qids.CopyTo((Array)this.qids,0)
return this
