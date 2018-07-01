def TrickParam(json):
    this={}#TrickParamjson)
    #return
    if 'iname' in json:
        this['mIname'] = json['iname']
    if 'name' in json:
        this['mName'] = json['name']
    if 'expr' in json:
        this['mExpr'] = json['expr']
    if 'dmg_type' in json:
        this['mDamageType'] = ENUM['eTrickDamageType'][json['dmg_type']]
    if 'dmg_val' in json:
        this['mDamageVal'] = json['dmg_val']
    if 'calc' in json:
        this['mCalcType'] = ENUM['SkillParamCalcTypes'][json['calc']]
    if 'elem' in json:
        this['mElem'] = ENUM['EElement'][json['elem']]
    if 'atk_det' in json:
        this['mAttackDetail'] = ENUM['AttackDetailTypes'][json['atk_det']]
    if 'buff' in json:
        this['mBuffId'] = json['buff']
    if 'cond' in json:
        this['mCondId'] = json['cond']
    if 'kb_rate' in json:
        this['mKnockBackRate'] = json['kb_rate']
    if 'kb_val' in json:
        this['mKnockBackVal'] = json['kb_val']
    if 'target' in json:
        this['mTarget'] = ENUM['ESkillTarget'][json['target']]
    if 'visual' in json:
        this['mVisualType'] = ENUM['eTrickVisualType'][json['visual']]
    if 'count' in json:
        this['mActionCount'] = json['count']
    if 'clock' in json:
        this['mValidClock'] = json['clock']
    if 'is_no_ow' in json:
        this['mIsNoOverWrite'] = (json['is_no_ow']!=0)
    if 'marker' in json:
        this['mMarkerName'] = json['marker']
    if 'effect' in json:
        this['mEffectName'] = json['effect']
    if 'eff_target' in json:
        this['mEffTarget'] = ENUM['ESkillTarget'][json['eff_target']]
    if 'eff_shape' in json:
        this['mEffShape'] = ENUM['ESelectType'][json['eff_shape']]
    if 'eff_scope' in json:
        this['mEffScope'] = json['eff_scope']
    if 'eff_height' in json:
        this['mEffHeight'] = json['eff_height']
return this
