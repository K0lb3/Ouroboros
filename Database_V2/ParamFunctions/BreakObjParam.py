from ParamFunctions._variables import ENUM
def BreakObjParam(json):
    this={}#BreakObjParamjson)
    if 'iname' in json:
        this['mIname'] = json['iname']
    if 'name' in json:
        this['mName'] = json['name']
    if 'expr' in json:
        this['mExpr'] = json['expr']
    if 'unit_id' in json:
        this['mUnitId'] = json['unit_id']
    if 'clash_type' in json:
        this['mClashType'] = ENUM['eMapBreakClashType'][json['clash_type']]
    if 'ai_type' in json:
        this['mAiType'] = ENUM['eMapBreakAIType'][json['ai_type']]
    if 'side_type' in json:
        this['mSideType'] = ENUM['eMapBreakSideType'][json['side_type']]
    if 'ray_type' in json:
        this['mRayType'] = ENUM['eMapBreakRayType'][json['ray_type']]
    
    this['mIsUI'] = True if 'is_ui' in json and json[is_ui] != 0 else False

    if 'json.rest_hps' in json:
        this['mRestHps']=json['rest_hps'].split(',')
    if 'clock' in json:
        this['mAliveClock'] = json['clock']
    if 'appear_dir' in json:
        this['mAppearDir'] = ENUM['EUnitDirection'][json['appear_dir']]
    return this
