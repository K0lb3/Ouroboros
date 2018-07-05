from ParamFunctions._variables import ENUM
from ParamFunctions.TobiraLearnAbilityParam import TobiraLearnAbilityParam
def TobiraParam(json):
    this={}#TobiraParamjson)
    if 'unit_iname' in json:
        this['mUnitIname'] = json['unit_iname']
    if 'enable' in json:
        this['mEnable'] = json['enable']==1
    if 'category' in json:
        this['mCategory'] = ENUM['Category'][json['category']]
    if 'recipe_id' in json:
        this['mRecipeId'] = json['recipe_id']
    if 'skill_iname' in json:
        this['mSkillIname'] = json['skill_iname']
    
    this['mLearnAbilities']=[]
    if('learn_abils' in json and len(json['learn_abils'])!=0):
        this['mLearnAbilities']=[
            TobiraLearnAbilityParam(abil)
            for abil in json['learn_abils']
            ]

    if 'overwrite_ls_iname' in json:
        this['mOverwriteLeaderSkillIname'] = json['overwrite_ls_iname']
        this['mOverwriteLeaderSkillLevel'] = 6#MasterParam.FixParam.TobiraLvCap

    if 'priority' in json:
        this['mPriority'] = json['priority']

    return this
