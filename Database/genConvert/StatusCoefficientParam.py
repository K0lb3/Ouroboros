def StatusCoefficientParam(json):
    this={}#StatusCoefficientParamjson)
    #if(json==null)
    #return
    if 'hp' in json:
        this['mHP'] = json['hp']
    if 'atk' in json:
        this['mAttack'] = json['atk']
    if 'def' in json:
        this['mDefense'] = json['def']
    if 'matk' in json:
        this['mMagAttack'] = json['matk']
    if 'mdef' in json:
        this['mMagDefense'] = json['mdef']
    if 'dex' in json:
        this['mDex'] = json['dex']
    if 'spd' in json:
        this['mSpeed'] = json['spd']
    if 'cri' in json:
        this['mCritical'] = json['cri']
    if 'luck' in json:
        this['mLuck'] = json['luck']
    if 'cmb' in json:
        this['mCombo'] = json['cmb']
    if 'move' in json:
        this['mMove'] = json['move']
    if 'jmp' in json:
        this['mJump'] = json['jmp']
return this
