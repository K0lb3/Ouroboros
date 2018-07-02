from ParamFunctions._variables import RAWBIRTH,RAWELEMENT,ENUM

def UnitParam(json):
    this={}#UnitParamjson)
    #if(json==null)
    #returnfalse
    if 'no' in json:
        this['no'] = json['no']
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'ai' in json:
        this['ai'] = json['ai']
    if 'mdl' in json:
        this['model'] = json['mdl']
    if 'grow' in json:
        this['grow'] = json['grow']
    if 'piece' in json:
        this['piece'] = json['piece']
    if 'birth' in json:
        this['birth'] = RAWBIRTH[json['birth']]
    if 'birth_id' in json:
        this['birthID'] = json['birth_id']
    if 'skill' in json:
        this['skill'] = json['skill']
    if 'ability' in json:
        this['ability'] = json['ability']
    if 'ma_quest' in json:
        this['ma_quest'] = json['ma_quest']
    if 'sw' in json:
        this['sw'] = max(json['sw'],1)
    if 'sh' in json:
        this['sh'] = max(json['sh'],1)
    if 'sex' in json:
        this['sex'] = ENUM['ESex'][json['sex']]
    if 'rare' in json:
        this['rare'] = json['rare']
    if 'raremax' in json:
        this['raremax'] = json['raremax']
    if 'type' in json:
        this['type'] = ENUM['EUnitType'][json['type']]
    if 'elem' in json:
        this['element'] = RAWELEMENT[json['elem']]
    if 'hero' in json:
        this['hero'] = json['hero']
    if 'search' in json:
        this['search'] = json['search']
    if 'stop' in json:
        this['stopped'] = (json['stop']!=0)
    if 'notsmn' in json:
        this['summon'] = json['notsmn']==0
    if 'available_at' in json:
        this['available_at'] = DateTime.Parse
    if 'height' in json:
        this['height'] = json['height']
    if 'weight' in json:
        this['weight'] = json['weight']
    if 'jt' in json:
        this['jobtype'] = ENUM['JobTypes'][json['jt']]
    if 'role' in json:
        this['role'] = ENUM['RoleTypes'][json['role']]
    if 'mov' in json:
        this['mov'] = json['mov']
    if 'jmp' in json:
        this['jmp'] = json['jmp']
    if 'inimp' in json:
        this['inimp'] = json['inimp']
    if 'ma_rarity' in json:
        this['ma_rarity'] = json['ma_rarity']
    if 'ma_lv' in json:
        this['ma_lv'] = json['ma_lv']
    if 'skins' in json:
        this['skins'] = json['skins']
    if 'djob' in json:
        this['djob'] = json['djob']
    if 'dbuki' in json:
        this['dbuki'] = json['dbuki']
    if 'dskl' in json:
        this['default_skill'] = json['dskl']
    if 'dabi' in json:
        this['default_abilities'] = json['dabi']
    if 'jobsets' in json:
        this['jobsets'] = json['jobsets']
    if 'tag' in json:
        this['tags'] = json['tag'].split(',')
    #stats
    this['ini_status']={}
    this['ini_status']['param']={}
    if 'hp' in json:
        this['ini_status']['param']['hp'] = json['hp']
    if 'mp' in json:
        this['ini_status']['param']['mp'] = json['mp']
    if 'atk' in json:
        this['ini_status']['param']['atk'] = json['atk']
    if 'def' in json:
        this['ini_status']['param']['def'] = json['def']
    if 'mag' in json:
        this['ini_status']['param']['mag'] = json['mag']
    if 'mnd' in json:
        this['ini_status']['param']['mnd'] = json['mnd']
    if 'dex' in json:
        this['ini_status']['param']['dex'] = json['dex']
    if 'spd' in json:
        this['ini_status']['param']['spd'] = json['spd']
    if 'cri' in json:
        this['ini_status']['param']['cri'] = json['cri']
    if 'luk' in json:
        this['ini_status']['param']['luk'] = json['luk']
    #Ini resists
    this['ini_status']['enchant_resist']={}
    if 'rpo' in json:
        this['ini_status']['enchant_resist']['poison'] = json['rpo']
    if 'rpa' in json:
        this['ini_status']['enchant_resist']['paralyse'] = json['rpa']
    if 'rst' in json:
        this['ini_status']['enchant_resist']['stun'] = json['rst']
    if 'rsl' in json:
        this['ini_status']['enchant_resist']['sleep'] = json['rsl']
    if 'rch' in json:
        this['ini_status']['enchant_resist']['charm'] = json['rch']
    if 'rsn' in json:
        this['ini_status']['enchant_resist']['stone'] = json['rsn']
    if 'rbl' in json:
        this['ini_status']['enchant_resist']['blind'] = json['rbl']
    if 'rns' in json:
        this['ini_status']['enchant_resist']['notskl'] = json['rns']
    if 'rnm' in json:
        this['ini_status']['enchant_resist']['notmov'] = json['rnm']
    if 'rna' in json:
        this['ini_status']['enchant_resist']['notatk'] = json['rna']
    if 'rzo' in json:
        this['ini_status']['enchant_resist']['zombie'] = json['rzo']
    if 'rde' in json:
        this['ini_status']['enchant_resist']['death'] = json['rde']
    if 'rkn' in json:
        this['ini_status']['enchant_resist']['knockback'] = json['rkn']
    if 'rdf' in json:
        this['ini_status']['enchant_resist']['resist_debuff'] = json['rdf']
    if 'rbe' in json:
        this['ini_status']['enchant_resist']['berserk'] = json['rbe']
    if 'rcs' in json:
        this['ini_status']['enchant_resist']['stop'] = json['rcs']
    if 'rcu' in json:
        this['ini_status']['enchant_resist']['fast'] = json['rcu']
    if 'rcd' in json:
        this['ini_status']['enchant_resist']['slow'] = json['rcd']
    if 'rdo' in json:
        this['ini_status']['enchant_resist']['donsoku'] = json['rdo']
    if 'rra' in json:
        this['ini_status']['enchant_resist']['rage'] = json['rra']
    if 'rsa' in json:
        this['ini_status']['enchant_resist']['single_attack'] = json['rsa']
    if 'raa' in json:
        this['ini_status']['enchant_resist']['area_attack'] = json['raa']
    if 'rdc' in json:
        this['ini_status']['enchant_resist']['dec_ct'] = json['rdc']
    if 'ric' in json:
        this['ini_status']['enchant_resist']['inc_ct'] = json['ric']

    this['max_status']={}
    this['max_status']['param']={}
    this['max_status']['enchant_resist']={}
    if 'mhp' in json:
        this['max_status']['param']['hp'] = json['mhp']
    if 'mmp' in json:
        this['max_status']['param']['mp'] = json['mmp']
    if 'matk' in json:
        this['max_status']['param']['atk'] = json['matk']
    if 'mdef' in json:
        this['max_status']['param']['def'] = json['mdef']
    if 'mmag' in json:
        this['max_status']['param']['mag'] = json['mmag']
    if 'mmnd' in json:
        this['max_status']['param']['mnd'] = json['mmnd']
    if 'mdex' in json:
        this['max_status']['param']['dex'] = json['mdex']
    if 'mspd' in json:
        this['max_status']['param']['spd'] = json['mspd']
    if 'mcri' in json:
        this['max_status']['param']['cri'] = json['mcri']
    if 'mluk' in json:
        this['max_status']['param']['luk'] = json['mluk']
    if 'mrpo' in json:
        this['max_status']['enchant_resist']['poison'] = json['mrpo']
    if 'mrpa' in json:
        this['max_status']['enchant_resist']['paralyse'] = json['mrpa']
    if 'mrst' in json:
        this['max_status']['enchant_resist']['stun'] = json['mrst']
    if 'mrsl' in json:
        this['max_status']['enchant_resist']['sleep'] = json['mrsl']
    if 'mrch' in json:
        this['max_status']['enchant_resist']['charm'] = json['mrch']
    if 'mrsn' in json:
        this['max_status']['enchant_resist']['stone'] = json['mrsn']
    if 'mrbl' in json:
        this['max_status']['enchant_resist']['blind'] = json['mrbl']
    if 'mrns' in json:
        this['max_status']['enchant_resist']['notskl'] = json['mrns']
    if 'mrnm' in json:
        this['max_status']['enchant_resist']['notmov'] = json['mrnm']
    if 'mrna' in json:
        this['max_status']['enchant_resist']['notatk'] = json['mrna']
    if 'mrzo' in json:
        this['max_status']['enchant_resist']['zombie'] = json['mrzo']
    if 'mrde' in json:
        this['max_status']['enchant_resist']['death'] = json['mrde']
    if 'mrkn' in json:
        this['max_status']['enchant_resist']['knockback'] = json['mrkn']
    if 'mrdf' in json:
        this['max_status']['enchant_resist']['resist_debuff'] = json['mrdf']
    if 'mrbe' in json:
        this['max_status']['enchant_resist']['berserk'] = json['mrbe']
    if 'mrcs' in json:
        this['max_status']['enchant_resist']['stop'] = json['mrcs']
    if 'mrcu' in json:
        this['max_status']['enchant_resist']['fast'] = json['mrcu']
    if 'mrcd' in json:
        this['max_status']['enchant_resist']['slow'] = json['mrcd']
    if 'mrdo' in json:
        this['max_status']['enchant_resist']['donsoku'] = json['mrdo']
    if 'mrra' in json:
        this['max_status']['enchant_resist']['rage'] = json['mrra']
    if 'mrsa' in json:
        this['max_status']['enchant_resist']['single_attack'] = json['mrsa']
    if 'mraa' in json:
        this['max_status']['enchant_resist']['area_attack'] = json['mraa']
    if 'mrdc' in json:
        this['max_status']['enchant_resist']['dec_ct'] = json['mrdc']
    if 'mric' in json:
        this['max_status']['enchant_resist']['inc_ct'] = json['mric']

    this['leader_skills']=[None] * 6
    if 'ls1' in json:
        this['leader_skills'][0] = json['ls1']
    if 'ls2' in json:
        this['leader_skills'][1] = json['ls2']
    if 'ls3' in json:
        this['leader_skills'][2] = json['ls3']
    if 'ls4' in json:
        this['leader_skills'][3] = json['ls4']
    if 'ls5' in json:
        this['leader_skills'][4] = json['ls5']
    if 'ls6' in json:
        this['leader_skills'][5] = json['ls6']

    this['recipes']=[None] * 6
    if 'recipe1' in json:
        this['recipes'][0] = json['recipe1']
    if 'recipe2' in json:
        this['recipes'][1] = json['recipe2']
    if 'recipe3' in json:
        this['recipes'][2] = json['recipe3']
    if 'recipe4' in json:
        this['recipes'][3] = json['recipe4']
    if 'recipe5' in json:
        this['recipes'][4] = json['recipe5']
    if 'recipe6' in json:
        this['recipes'][5] = json['recipe6']
    if 'img' in json:
        this['image'] = json['img']
    if 'vce' in json:
        this['voice'] = json['vce']

    if 'unlck_t' in json:
        this['unlock_time'] = json['unlck_t']
    if 'jidx' in json:
        this['job_option_index'] = json['jidx']
    if 'jimgs' in json:
        this['job_images'] = json['jimgs']
    if 'jvcs' in json:
        this['job_voices'] = json['jvcs']
    if 'no_trw' in json:
        this['is_throw'] = (json['no_trw']==0)
    if 'no_kb' in json:
        this['is_knock_back'] = (json['no_kb']==0)
    #returntrue
    return this
