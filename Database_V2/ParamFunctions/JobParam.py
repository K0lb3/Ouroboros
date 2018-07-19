from ParamFunctions._variables import ENUM,TRANSLATION
from ParamFunctions.JobRankParam import JobRankParam
def JobParam(json):
    this={}#JobParamjson,MasterParammaster_param)
    if 'iname' in json:
        this['iname'] = json['iname']

    if 'name' in json:
        this['kanji'] = json['name']
    if json['iname'] in TRANSLATION:
        json.update(TRANSLATION[json['iname']])
    
    if 'name' in json:
        this['name'] = json['name'] 
    if 'expr' in json:
        this['expr'] = json['expr']

    if 'mdl' in json:
        this['model'] = json['mdl']
    if 'ac2d' in json:
        this['ac2d'] = json['ac2d']
    if 'mdlp' in json:
        this['modelp'] = json['mdlp']
    if 'pet' in json:
        this['pet'] = json['pet']
    if 'buki' in json:
        this['buki'] = json['buki']
    if 'origin' in json:
        this['origin'] = json['origin']
    if 'type' in json:
        this['type'] = ENUM['JobTypes'][json['type']]
    if 'role' in json:
        this['role'] = ENUM['RoleTypes'][json['role']]
    if 'wepmdl' in json:
        this['wepmdl'] = json['wepmdl']
    if 'jmov' in json:
        this['mov'] = json['jmov']
    if 'jjmp' in json:
        this['jmp'] = json['jjmp']
    this['atkskill']=[None]*7
    if 'atkskl' in json:
        this['atkskill'][0] = json['atkskl']
    if 'atkfi' in json:
        this['atkskill'][1] = json['atkfi']
    if 'atkwa' in json:
        this['atkskill'][2] = json['atkwa']
    if 'atkwi' in json:
        this['atkskill'][3] = json['atkwi']
    if 'atkth' in json:
        this['atkskill'][4] = json['atkth']
    if 'atksh' in json:
        this['atkskill'][5] = json['atksh']
    if 'atkda' in json:
        this['atkskill'][6] = json['atkda']
    if 'fixabl' in json:
        this['fixed_ability'] = json['fixabl']
    if 'artifact' in json:
        this['artifact'] = json['artifact']
    if 'ai' in json:
        this['ai'] = json['ai']
    if 'master' in json:
        this['master'] = json['master']
    if 'me_abl' in json:
        this['MapEffectAbility'] = json['me_abl']
    if 'is_me_rr' in json:
        this['IsMapEffectRevReso'] = json['is_me_rr']!=0
    if 'desc_ch' in json:
        this['DescCharacteristic'] = json['desc_ch']
    if 'desc_ot' in json:
        this['DescOther'] = json['desc_ot']

    this['status']={}
    if 'hp' in json:
        this['status']['hp'] = json['hp']
    if 'mp' in json:
        this['status']['mp'] = json['mp']
    if 'atk' in json:
        this['status']['atk'] = json['atk']
    if 'def' in json:
        this['status']['def'] = json['def']
    if 'mag' in json:
        this['status']['mag'] = json['mag']
    if 'mnd' in json:
        this['status']['mnd'] = json['mnd']
    if 'dex' in json:
        this['status']['dex'] = json['dex']
    if 'spd' in json:
        this['status']['spd'] = json['spd']
    if 'cri' in json:
        this['status']['cri'] = json['cri']
    if 'luk' in json:
        this['status']['luk'] = json['luk']
    if 'avoid' in json:
        this['avoid'] = json['avoid']
    if 'inimp' in json:
        this['inimp'] = json['inimp']

    if 'ranks' in json and len(json['ranks'])>0:
        this['ranks']=[
            JobRankParam(rank)
            for rank in json['ranks']
        ]
    
    #stat calc would be here, ignored for now
    #if(master_param!=null)
    #this.CreateBuffList(master_param)
    if 'unit_image' in json:
        this['unit_image'] = json['unit_image']
    #returntrue
    return this
