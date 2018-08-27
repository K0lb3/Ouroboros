from ParamFunctions._variables import RAWBIRTH,RAWELEMENT,ENUM,TRANSLATION,UNITLORE,TAG_UNIT

def UnitParam(json):
    this={}#UnitParamjson)
    #if(json==null)
    #returnfalse
    if 'no' in json:
        this['no'] = json['no']
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = TRANSLATION[json['iname']]['name'] if json['iname'] in TRANSLATION else json['name']
        this['kanji'] = json['name']
    if 'height' in json:
        this['height'] = json['height']
    if 'weight' in json:
        this['weight'] = json['weight']
    if 'birth' in json:
        this['birth'] = RAWBIRTH[json['birth']]
    if 'birth_id' in json:
        this['birthID'] = json['birth_id']
    if json['iname'] in UNITLORE:
        this['lore']= UNITLORE[json['iname']]
    if 'ai' in json:
        this['ai'] = json['ai']
    if 'mdl' in json:
        this['model'] = json['mdl']
    if 'grow' in json:
        this['grow'] = json['grow']
    if 'piece' in json:
        this['piece'] = json['piece']
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
        this['available_at'] = json['available_at']
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
    this['skins'] = json['skins'] if 'skins' in json else []
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
        try:
            this['tags']=[
                TAG_UNIT[tag]
                for tag in json['tag'].split(',')
            ]
        except:
            this['tags']=[json['tag']]
        
    #stats
    this['ini_status']={
        'param':{
            trans:json[key]
            for key,trans in PARAMS.items()
            if key in json
        },
        'enchant_resist':{
            trans:json[key]
            for trans,key in RESISTS.items()
            if key in json
        }
    }
    this['max_status']={
        'param':{
            trans:json['m'+key]
            for key,trans in PARAMS.items()
            if 'm'+key in json
        },
        'enchant_resist':{
            trans:json['m'+key]
            for trans,key in RESISTS.items()
            if 'm'+key in json
        }
    }

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


PARAMS={
    'hp'       :   'HP',
    'mp'       :   'Max Jewels',
    'atk'      :   'PATK',
    'def'      :   'PDEF',
    'mag'      :   'MATK',
    'mnd'      :   'MDEF',
    'dex'      :   'DEX',
    'spd'      :   'AGI',
    'cri'      :   'CRIT',
    'luk'      :   'LUCK',
    }
RESISTS={    
    'Poison Res'	:	'rpo',
    'Paralyze Res'	:	'rpa',
    'Stun Res'	:	'rst',
    'Sleep Res'	:	'rsl',
    'Charm Res'	:	'rch',
    'Petrify Res'	:	'rsn',
    'Blind Res'	:	'rbl',
    'Silence Res'	:	'rns',
    'Bind Res'	:	'rnm',
    'Daze Res'	:	'rna',
    'Infect Res'	:	'rzo',
    'Death Res'	:	'rde',
    'Knockback Res'	:	'rkn',
    'Debuff Res'	:	'rdf',
    'Berserk Res'	:	'rbe',
    'Stop Res'	:	'rcs',
    'Quicken Res'	:	'rcu',
    'Delay Res'	:	'rcd',
    'Slow Res'	:	'rdo',
    'Rage Res'	:	'rra',
    'Single Target ATK Res'	:	'rsa',
    'Area ATK Res'	:	'raa',
    'CT-Down Res'	:	'rdc',
    'CT-Up Res'	:	'ric',
    }
