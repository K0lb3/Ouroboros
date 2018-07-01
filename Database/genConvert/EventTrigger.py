def EventTrigger(json):
    this={}#EventTriggerjson)
    #returnfalse
    if 'trg' in json:
        this['mTrigger'] = ENUM['EEventTrigger'][json['trg']]
    if 'type' in json:
        this['mEventType'] = ENUM['EEventType'][json['type']]
    if 'detail' in json:
        this['mGimmickType'] = ENUM['EEventGimmick'][json['detail']]
    if 'sval' in json:
        this['mStrValue'] = json['sval']
    if 'ival' in json:
        this['mIntValue'] = json['ival']
    if 'cnt' in json:
        this['mCount'] = json['cnt']
    #returntrue
return this
