from ParamFunctions._variables import ENUM

def MapSetting(json):
    return NPCSetting(json)

def EventTrigger(json):
    this={}
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
    if 'tag' in json:
        this['mTag'] = json['tag']
    return this

def NPCSetting(json):
    this={}
    if 'name' in json:
        this['uniqname'] = json['name']
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'side' in json:
        this['side'] = 'Ally' if json['side']==0 else 'Enemy'
    if 'lv' in json:
        this['lv'] = max(json['lv'], 1)
    if 'rare' in json:
        this['rare'] = json['rare']
    if 'awake' in json:
        this['awake'] = json['awake']
    if 'elem' in json:
        this['elem'] = json['elem']
    if 'exp' in json:
        this['exp'] = json['exp']
    if 'gems' in json:
        this['gems'] = json['gems']
    if 'gold' in json:
        this['gold'] = json['gold']
    if 'ai' in json:
        this['ai'] = json['ai']

    this['pos']={}
    if 'x' in json:
        this['pos']['x'] = json['x']
    if 'y' in json:
        this['pos']['y'] = json['y']

    if 'dir' in json:
        this['dir'] = json['dir']
    if 'search' in json:
        this['search'] = json['search']
    if 'ctrl' in json:
        this['control'] = (json['ctrl'] != 0)
    if 'wait_e' in json:
        this['waitEntryClock'] = json['wait_e']
    if 'wait_m' in json:
        this['waitMoveTurn'] = json['wait_m']
    if 'wait_exit' in json:
        this['waitExitTurn'] = json['wait_exit']
    if 'ct_calc' in json:
        this['startCtCalc'] = ENUM['eMapUnitCtCalcType'][json['ct_calc']]
    if 'ct_val' in json:
        this['startCtVal'] = json['ct_val']
    if 'fvoff' in json:
        this['DisableFirceVoice'] = json['fvoff'] != 0
    if 'ai_type' in json:
        this['ai_type'] = ENUM['AIActionType'][json['ai_type']]

    this['ai_pos']={}
    if 'ai_x' in json:
        this['ai_pos']['x'] = json['ai_x']
    if 'ai_y' in json:
        this['ai_pos']['y'] = json['ai_y']

    if 'ai_len' in json:
        this['ai_len'] = json['ai_len']
    if 'parent' in json:
        this['parent'] = json['parent']
    if 'fskl' in json:
        this['fskl'] = json['fskl']
    if 'notice_damage' in json:
        this['notice_damage'] = json['notice_damage']
    if  'notice_members' in json:
        this['notice_members'] = json['notice_members']
    if 'trg' in json:
        this['trigger'] = EventTrigger(json['trg'])
    if 'entries' in json:
        this['entries'] = json['entries']
    if 'entries_and' in json:
        this['entries_and'] = json['entries_and']
    if 'abils' in json:
        this['abilities']=json['abils']
        for abil in json['abils']:
            if 'cond' in abil:
                abil['cond']['type']=ENUM['SkillLockTypes'][abil['cond']['type']]

    if 'acttbl' in json:
        this['acttbl']=json['acttbl']
        for action in this['acttbl']['actions']:
            if 'type' in action:
                action['type']=ENUM['AIActionType'][action['type']]
            if 'cond' in action:
                action['cond']['type']=ENUM['SkillLockTypes'][action['cond']['type']]
            if 'noExecAct' in action:
                action['noExecAct'] = ENUM['eAIActionNoExecAct'][action['noExecAct']]

    if 'patrol' in json:
        this['patrol'] = json['patrol']
    if 'break_obj' in json:
        this['break_obj']=json['break_obj']
        
    return this