def TobiraRecipeParam(json):
    this={}#TobiraRecipeParamjson)
    if 'recipe_iname' in json:
        this['mRecipeIname'] = json['recipe_iname']
    if 'tobira_lv' in json:
        this['mLevel'] = json['tobira_lv']
    if 'cost' in json:
        this['mCost'] = json['cost']
    if 'unit_piece_num' in json:
        this['mUnitPieceNum'] = json['unit_piece_num']
    if 'piece_elem_num' in json:
        this['mElementNum'] = json['piece_elem_num']
    if 'unlock_elem_num' in json:
        this['mUnlockElementNum'] = json['unlock_elem_num']
    if 'unlock_birth_num' in json:
        this['mUnlockBirthNum'] = json['unlock_birth_num']

    if('mats'in json and len(json['mats'])!=0):
        this['mMaterials']=[{
                'mIname': recipe['iname'],
                'mNum':   recipe['num']
            }
            for recipe in json['mats']
            ]
    return this
