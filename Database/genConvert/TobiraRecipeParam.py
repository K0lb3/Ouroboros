def TobiraRecipeParam(json):
    this={}#TobiraRecipeParamjson)
    #if(json==null)
    #return
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
    #this.mMaterials.Clear()
    #if(json.mats==null)
    #return
    #for(intindex=0index<json.mats.Length++index)
        #TobiraRecipeMaterialParamrecipeMaterialParam=newTobiraRecipeMaterialParam()
        #recipeMaterialParam.Deserialize(json.mats)
        #this.mMaterials.Add(recipeMaterialParam)
return this
