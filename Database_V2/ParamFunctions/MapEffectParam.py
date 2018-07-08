from ParamFunctions._variables import TRANSLATION
def MapEffectParam(json):
    this={}#MapEffectParamjson)
    if 'iname' in json:
        this['mIname'] = json['iname']
    if 'name' in json:
        this['mName'] = TRANSLATION[json['iname']]['name'] if json['iname'] in TRANSLATION else json['name']
    if 'expr' in json:
        this['mExpr'] = json['expr']
    if 'skills' in json:
        this['mValidSkillLists'] = json['skills']
    return this
