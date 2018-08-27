from ParamFunctions._variables import TRANSLATION
def GrowParam(json):
    this={}#GrowParamjson)
    if 'type' in json:
        this['type'] = json['type']
    if(json['curve']):
        this['curve']=[{
            'lv'	:	curve['lv'],
            'scale'	:	curve['val'],
            'status':{
                'param':{
                    'HP'	:	curve['hp'],
                    'Max Jewels'	:	curve['mp'],
                    'PATK'	:	curve['atk'],
                    'PDEF'	:	curve['def'],
                    'MATK'	:	curve['mag'],
                    'MDEF'	:	curve['mnd'],
                    'DEX'	:	curve['dex'],
                    'AGI'	:	curve['spd'],
                    'CRIT'	:	curve['cri'],
                    'LUCK'	:	curve['luk'],
                },
                'element_assist':{
                    TRANSLATION['Assist_'+trans]:curve['a'+key]
                    for trans,key in ELEMENT.items()
                    if 'a'+key in curve
                },
                'element_resist':{
                    TRANSLATION['Resist_'+trans]:curve['r'+key]
                    for trans,key in ELEMENT.items()
                    if 'r'+key in curve
                    },
                'enchant_assist':{
                    TRANSLATION['Assist_'+trans]:curve['a'+key]
                    for trans,key in STATUS.items()
                    if 'a'+key in curve
                    },
                'enchant_resist':{
                    TRANSLATION['Resist_'+trans]:curve['r'+key]
                    for trans,key in STATUS.items()
                    if 'r'+key in curve
                }
            }
        }
        for curve in json['curve']
        ]
    return this


ELEMENT={
    'Fire'	:	'fi',
    'Water'	:	'wa',
    'Wind'	:	'wi',
    'Thunder':	'th',
    'Shine'	:	'sh',
    'Dark'	:	'da',
}

STATUS={
    'Poison'	:	'po',
    'Paralysed'	:	'pa',
    'Stun'	:	'st',
    'Sleep'	:	'sl',
    'Charm'	:	'ch',
    'Stone'	:	'sn',
    'Blind'	:	'bl',
    'DisableSkill'	:	'ns',
    'DisableMove'	:	'nm',
    'DisableAttack'	:	'na',
    'Zombie'	:	'zo',
    'DeathSentence'	:	'de',
    'Knockback'	:	'kn',
    'Berserk'	:	'be',
    'ResistBuff'	:	'bf',
    'ResistDebuff'	:	'df',
    'Stop'	:	'cs',
    'Fast'	:	'cu',
    'Slow'	:	'cd',
    'Donsoku'	:	'do',
    'Rage'	:	'ra',
    'DecCT'	:	'dc',
    'IncCT'	:	'ic',
}
