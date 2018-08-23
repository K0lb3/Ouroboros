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
                    'hp'	:	curve['hp'],
                    'mp'	:	curve['mp'],
                    'atk'	:	curve['atk'],
                    'def'	:	curve['def'],
                    'mag'	:	curve['mag'],
                    'mnd'	:	curve['mnd'],
                    'dex'	:	curve['dex'],
                    'spd'	:	curve['spd'],
                    'cri'	:	curve['cri'],
                    'luk'	:	curve['lu'],
                },
                'element_assist':{
                    'fire'	:	curve['afi'],
                    'water'	:	curve['awa'],
                    'wind'	:	curve['awi'],
                    'thunder'	:	curve['ath'],
                    'shine'	:	curve['ash'],
                    'dark'	:	curve['ad'],
                },
                'element_resist':{
                    'fire'	:	curve['rfi'],
                    'water'	:	curve['rwa'],
                    'wind'	:	curve['rwi'],
                    'thunder'	:	curve['rth'],
                    'shine'	:	curve['rsh'],
                    'dark'	:	curve['rd'],
                    },
                'enchant_assist':{
                    'poison'	:	curve['apo'],
                    'paralyse'	:	curve['apa'],
                    'stun'	:	curve['ast'],
                    'sleep'	:	curve['asl'],
                    'charm'	:	curve['ach'],
                    'stone'	:	curve['asn'],
                    'blind'	:	curve['abl'],
                    'notskl'	:	curve['ans'],
                    'notmov'	:	curve['anm'],
                    'notatk'	:	curve['ana'],
                    'zombie'	:	curve['azo'],
                    'death'	:	curve['ade'],
                    'knockback'	:	curve['akn'],
                    'berserk'	:	curve['abe'],
                    'resist_buff'	:	curve['abf'],
                    'resist_debuff'	:	curve['adf'],
                    'stop'	:	curve['acs'],
                    'fast'	:	curve['acu'],
                    'slow'	:	curve['acd'],
                    'donsoku'	:	curve['ado'],
                    'rage'	:	curve['ara'],
                    'dec_ct'	:	curve['adc'],
                    'inc_ct'	:	curve['ai'],
                    },
                'enchant_resist':{
                    'poison'	:	curve['rpo'],
                    'paralyse'	:	curve['rpa'],
                    'stun'	:	curve['rst'],
                    'sleep'	:	curve['rsl'],
                    'charm'	:	curve['rch'],
                    'stone'	:	curve['rsn'],
                    'blind'	:	curve['rbl'],
                    'notskl'	:	curve['rns'],
                    'notmov'	:	curve['rnm'],
                    'notatk'	:	curve['rna'],
                    'zombie'	:	curve['rzo'],
                    'death'	:	curve['rde'],
                    'knockback'	:	curve['rkn'],
                    'berserk'	:	curve['rbe'],
                    'resist_buff'	:	curve['rbf'],
                    'resist_debuff'	:	curve['rdf'],
                    'stop'	:	curve['rcs'],
                    'fast'	:	curve['rcu'],
                    'slow'	:	curve['rcd'],
                    'donsoku'	:	curve['rdo'],
                    'rage'	:	curve['rra'],
                    'dec_ct'	:	curve['rdc'],
                    'inc_ct'	:	curve['ri'],
                }
            }
        }
        for curve in json['curve']
        ]
    return this
