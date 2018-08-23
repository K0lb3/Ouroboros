from ParamFunctions._variables import ENUM
def TipsParam(json):
    this={}#TipsParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'type' in json:
        this['type'] = ENUM['ETipsType'][json['type']]
    if 'order' in json:
        this['order'] = json['order']
    if 'title' in json:
        this['title'] = json['title']
    if 'text' in json:
        this['text'] = json['text']
    if 'images' in json:
        this['images'] = json['images']
    if 'hide' in json:
        this['hide'] = json['hide']!=0
    if 'cond_text' in json:
        this['cond_text'] = json['cond_text']
    return this
