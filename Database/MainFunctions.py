import string
import os
import json
import re
import math
import requests
import re
import jellyfish
from convertRawData import *

re_job = re.compile(r'^([^_]*)_([^_]*)_(.*)$')
ReBr = re.compile(r'(\s+)?(\n)?(\[|\『|\().*')
u_m_names = {}
birth = {
    "その他": "Foreign World",
    "エンヴィリア": "Envylia",
    "スロウスシュタイン": "Slothstein",
    "ルストブルグ": "Lustburg",
    "サガ地方": "Saga Region",
    "ラーストリス": "Wratharis",
    "ワダツミ": "Wadatsumi",
    "砂漠地帯": "Desert Zone",
    "ノーザンブライド": "Northern Pride",
    "グリードダイク": "Greed Dike",
    "グラトニー＝フォス": "Gluttony Foss"
}

GEAR_TAG = {
    "剣": "Sword",
    "杖": "Rod",
    "ローブ": "Robe",
    "鎧": "Armor",
    "具足": "Legguards",
    "籠手": "Gauntlet",
    "盾": "Shield",
    "アクセサリー": "Earring",
    "大剣": "Greatsword/Scythe",
    "短剣": "Dagger",
    "銃": "Gun",
    "ランス": "Spear",
    "ウェディングソード": "Wedding Sword",
    "弓": "Bow",
    "グローブ": "Gloves",
    "サーベル": "Saber",
    "ライフル": "Sniper Rifle",
    "槍": "Spear",
    "カード": "Cards",
    "忍者刀": "Kunai",
    "チャクラム": "Chakram",
    "弦楽器": "Harp",
    "斧": "Axe",
    "双剣": "Twin Swords",
    "薬瓶": "Drug Vial",
    "獣": "Claw",
    "大麻": "Praying Staff",
    "鎚": "Hammer",
    "刀": "Katana",
    "ロケットランチャー": "Rocket Launcher",
    "薙刀": "Halberd",
    "風水盤": "Feng Shui",
    "タクト": "Tact",
    "手袋": "Gloves",
    "グレネードランチャー": "Grenade Launcher",
    "長銃": "Rifle",  # JB_DGO
    "台本": "Script",  # JB_ACT_F
    "無手": "Paw",  # JB_DIS_CAT
    "大砲": "Cannon",  # JB_ART
    "銃槍": "Gunlance",  # JB_GUNL
    "装備なし": "N/A",  # JB_LUROB01
    "杖剣": "Swordstick",  # JB_HENC_WATER
}

Grow_Curves={}

def similarity(ori, inp):
    return (jellyfish.jaro_winkler(inp, ori))

def cPath():
    return os.path.dirname(os.path.realpath(__file__))

def loadFiles(files):
    ret = []
    dir = os.path.dirname(os.path.realpath(__file__))+'\\res'

    for file in files:
        path = dir+'\\' + file
        if not os.path.exists(path):
            print(path, " was not found.")
            continue

        print(file)
        try:
            with open(path, "rt", encoding='utf8') as f:
                ret.append(json.loads(f.read()))
        except ValueError:
            with open(path, "rt", encoding='utf-8-sig') as f:
                ret.append(json.loads(f.read()))

    return ret

def loadOut(files):
    ret = []
    dir = os.path.dirname(os.path.realpath(__file__))+'\\out'

    for file in files:
        path = dir+'\\' + file
        if not os.path.exists(path):
            print(path, " was not found.")
            continue

        print(file)
        try:
            with open(path, "rt", encoding='utf8') as f:
                ret.append(json.loads(f.read()))
        except ValueError:
            with open(path, "rt", encoding='utf-8-sig') as f:
                ret.append(json.loads(f.read()))

    return ret

def saveAsJSON(name, var):
    os.makedirs(name[:name.rindex("\\")], exist_ok=True)
    with open(name, "wb") as f:
        f.write(json.dumps(var, indent=4, ensure_ascii=False).encode('utf8'))

    # try:
    #    with codecs.open(name, 'w', encoding='utf-8') as f:
    #        json.dump(var, f, indent=4, ensure_ascii=False)
    # except ValueError:
    #    print('Codecs Error, Retry')
    #    saveAsJSON(name,var)
    return 1

def Translation():
    [loc, trans] = loadFiles(
        ['LocalizedMasterParam.json', 'TranslationsS.json'])
    
    for i in trans:
        if i in loc:
            trans[i].update(loc[i])
            loc[i]=trans[i]
        else:
            loc[i] = trans[i]

        if len(loc[i]['long des']) == 0:
            loc[i]['long des'] = loc[i]['short des']

    return loc

def convertMaster(master):
    ma = {}
    for main in master:
        for sub in master[main]:
            if type(sub) == dict and 'iname' in sub:
                ma[sub['iname']] = sub

    return ma

def convertSubMaster(main):
    ma = {}
    for sub in main:
        try:
            ma[sub['iname']] = sub
        except:
            continue
    return ma

def element(type):
    if type == 1:
        return 'fire'
    if type in [2, 10]:
        return 'water'
    if type in [3, 100]:
        return 'wind'
    if type in [4, 1000]:
        return 'thunder'
    if type in [5, 10000]:
        return 'light'
    if type in [6, 100000]:
        return 'dark'
    if type in [0, 111111]:
        return 'all'
    return 'error'

def TACScale(ini,max,lv,mlv):
    return math.floor(ini + ((max-ini) / (mlv-1) * (lv-1)))


def buff(buff, lv, mlv, array=False):
    buff=convertRawBuff(buff,SYS,ENUM)

    if array:
        return([{
            'type': SYS[mod['type']],
            'value': TACScale(mod['value_ini'],mod['value_max'],lv,mlv),
            'calc': mod['calc']
            }
            for mod in buff['buffs']            
            ])

    text=""

    rest={
        'job' : 'Job',
        'birth': 'Country',
        'sex': 'Gender',
        'elem': 'Element'
        }

    restrictions=[
        '{rest}: {value}'.format(rest=rest[key],value=buff[key])
        for key,value in rest.items()
        if key in buff
        ]

    def withSign(number):
        if number<0:
            return str(number)
        return '+'+str(number)

    mods=[
        '{value}{calc} {stat}'.format(
            stat= SYS[mod['type']] if mod['type'] in SYS else mod['type'],
            value=withSign(TACScale(mod['value_ini'],mod['value_max'],lv,mlv)),
            calc='%' if 'Scale' == mod['calc'] else '*Fixed*' if mod['calc']== 'Fixed' else ''
            )
        for mod in buff['buffs']
        ]

    runt=[]
    if 'rate' in buff and buff['rate']!=100:
        runt.append('Chance: {chance}%'.format(chance=buff['rate']))
    if 'turn' in buff and buff['turn']!=0:
        runt.append('Turns: {turns}'.format(turns=buff['turn']-1))

    if len(restrictions)!=0:
        text+='Restriction(s): {rest}\n'.format(rest=', '.join(restrictions))
    if len(mods)!=0:
        text+=', '.join(mods)
    if len(runt)!=0:
        text+=' [{runt}]'.format(runt=', '.join(runt))
    return text

def condition(cond, lv,  mlv):      
    cond=convertRawCondition(cond,SYS,ENUM)

    vals=[]
    if 'turn_ini' in cond:
        turn = TACScale(cond['turn_ini'],cond['turn_max'],lv,mlv)
        vals.append('Turns: {turn}'.format(turn=turn-1))

    if 'rate_ini' in cond:
        rate = TACScale(cond['rate_ini'],cond['rate_max'],lv,mlv)
        vals.append('Chance: {rate}%'.format(rate=rate))

    if 'value_ini' in cond:
        value = TACScale(cond['value_ini'],cond['value_max'],lv,mlv)
        vals.append('Value: {val}'.format(val=value))
    
    if len(vals)!=0:
        vals='[{values}]'.format(values=', '.join(vals))
        if len(cond['conds'])<6:
            return('{cond} {vals}'.format(vals=vals,cond=', '.join(cond['conds'])))
        else:
            return('{vals} {cond}'.format(vals=vals,cond=', '.join(cond['conds'])))
    else:
        return(', '.join(cond['conds']))

def calc_stat_grow(item, master, lv):
    global Grow_Curves
    if len(Grow_Curves)==0:
        for j in master['Grow']:
            Grow_Curves[j['type']]=j['curve']

    stats={
        "hp":   "HP",
        'mp':   "Max Jewels",
        "atk":  "PATK",
        "def":  "PDEF",
        "mag":  "MATK",
        "mnd":  "MDEF",
        "dex":  "DEX",
        "spd":  "AGI",
        "cri":  "CRIT",
        "luk":  "LUCK",
        "rpo": "Posion Res",
        "rpa": "Paralyse",
        "rst": "Stun Res",
        "rsl": "Sleep Res",
        "rch": "Charm Res",
        "rsn": "Petrify Res",
        "rbl": "Blind Res",
        "rns": "Silence Res",
        "rnm": "Bind Res",
        "rna": "Daze Res",
        "rzo": "Infect Res",
        "rde": "Death entence Res",
        "rkn": "Knockback Res",
        "rdf": "Debuff Res",
        "rbe": "Beserk Res",
        "rcs": "Stop Res",
        "rcu": "Quick Res",
        "rcd": "Delay Res",
        "rdo": "Slow Res",
        "rra": "Rage Res",
        "rsa": "Single Target Res",
        "raa": "AOE Res",
        "rdc": "Decrease CT Res",
        "ric": "Increase CT Res",
        }
    #select curve
    for curve in Grow_Curves[item['grow']]:
        if lv <= curve['lv']:
            break
    LV_MOD=(lv-1)/(curve['lv']-1)
    ret=({
        stat:int(item['m'+key] + (LV_MOD * curve[key]/100 * (item['m'+key]-item['m'+key])/(item['m'+key])))
        for key,stat in stats.items()
        if key in item
        })
    return(ret)

def rarity(base, max):
    text = ""
    for i in range(0, base+1):
        text += "★"
    for i in range(base, max):
        text += "☆"

    return text

def GSSUpload(obj, sheet, id='1wze5mJCLeTZtBJiCrlOL4UMqUqc9mdBO_3siL2JtI3w'):
    url = 'https://script.google.com/macros/s/AKfycbzMqkHxhLsiMWqtFDmMHzqgT4a1R8yhBAxHN6YRkeN1lotYmsfg/exec'
    payload = json.dumps({
        "id": id,
        "sheet": sheet,
        "obj": obj
    })

    response = requests.post(url, data=payload)

    # print(response.text) #TEXT/HTMLs
    res = json.loads(response.text)
    if res['message'] == 'success':
        print('upload successfull')
    else:
        print(res)
        print(response.status_code, response.reason)  # HTTPs

def dmg_formula(weaponParam):
    WeaponFormulaTypes = ENUM['WeaponFormulaTypes']

    def modifier(num):
        return str(int(num*100))+"%"

    Formulas = {
        "Atk":    modifier(weaponParam['atk']/10) + " PATK",
        "Def":    modifier(weaponParam['atk']/10) + " PDEF",
        "Mag":    modifier(weaponParam['atk']/10) + " MATK",
        "Mnd":    modifier(weaponParam['atk']/10) + " MDEF",
        "Luk":    modifier(weaponParam['atk']/10) + " LUCK",
        "Dex":    modifier(weaponParam['atk']/10) + " DEX",
        "Spd":    modifier(weaponParam['atk']/10) + " AGI",
        "Cri":    modifier(weaponParam['atk']/10) + " CRIT",
        "Mhp":    modifier(weaponParam['atk']/10) + " MAX HP",
        "AtkSpd": modifier(weaponParam['atk']/15) + " (PATK + AGI)",
        "MagSpd": modifier(weaponParam['atk']/15) + " (MATK + AGI)",
        "AtkDex": modifier(weaponParam['atk']/20) + " (PATK + DEX)",
        "MagDex": modifier(weaponParam['atk']/20) + " (MATK + DEX)",
        "AtkLuk": modifier(weaponParam['atk']/20) + " (PATK + LUCK)",
        "MagLuk": modifier(weaponParam['atk']/20) + " (MATK + LUCK)",
        "AtkMag": modifier(weaponParam['atk']/20) + " (PATK + MATK)",
        "SpAtk":  modifier(weaponParam['atk']/10) + " PATK * random(150% to 250%)",
        "SpMag":  modifier(weaponParam['atk']/10) + " MATK * (20% + MATK/(MATK + Target's MDEF))",
        "AtkRndLuk": modifier(weaponParam['atk']/60) + " (3*PATK + (1 + 0~[LV/10]) * LUCK)",
        "MagRndLuk": modifier(weaponParam['atk']/60) + " (3*MATK + (1 + 0~[LV/10]) * LUCK)",
        "AtkSpdDex": modifier(weaponParam['atk']/20) + " (PATK + AGI/2 + AGI*LV/100 + DEX/4)",
        "MagSpdDex": modifier(weaponParam['atk']/20) + " (MATK + AGI/2 + AGI*LV/100 + DEX/4)",
        "AtkDexLuk": modifier(weaponParam['atk']/20) + " (PATK + DEX/2 + LUCK/2)",
        "MagDexLuk": modifier(weaponParam['atk']/20) + " (MATK + DEX/2 + LUCK/2)",
        "AtkEAt":    modifier(weaponParam['atk']/10) + " (2*PATK - Target's ATK)",
        "MagEMg":    modifier(weaponParam['atk']/10) + " (2*MATK - Target's MATK)",
        "LukELk":    modifier(weaponParam['atk']/10) + " (2*LUCK - Target's LUCK)",
        "AtkDefEDf": modifier(weaponParam['atk']/10) + " (PATK + PDEF - Target's PDEF)",
        "MagMndEMd": modifier(weaponParam['atk']/10) + " (MATK + MDEF - Target's MDEF)",
        #default = PATK
    }

    return Formulas[WeaponFormulaTypes[weaponParam['formula']]]

def dmg_calc(weaponParam, attacker, statusParam1, statusParam2):
    #placeholder for now
    #formula in BattleCore.css
    return dmg

def name_collab(iname, loc):
    global u_m_names

    iname = iname[6:]

    # collab
    collabs = {
        #'GL':   ['Global', 'Global Exclusive'],
        'FATE': ['Fate', 'Fate/Stay Night [UBW]'],
        'FA': ['FA', 'Fullmetal Alchemist'],
        'RH': ['RH', 'Radiant Historia'],
        'POK': ['POTK', 'Phantom Of The Kill'],
        'S': ['SN', 'Shinobi Nightmare'],
        'FF15': ['FF', 'Final Fantasy 15'],
        'DIS': ['DIS', 'Disgea'],
        'SEK': ['EO', 'Etrian Odyssey'],
        'SQ': ['EMD', 'Etrian Mystery Dungeon'],
        'APR': ['', 'April Fool'],
        'CRY': ['CR', 'Crystal Re:Union']
    }

    collab = ""
    collab_short = ''
    for c in collabs:
        if c+'_' == iname[:len(c)+1]:
            collab = collabs[c][1]
            collab_short = collabs[c][0]
            iname = iname[len(c)+1:]
            break

    # unknown name
    try:
        name = loc['UN_V2_'+iname]['name']
    except:
        name = iname.replace('_', ' ')
        prefix = {
            'BLK': 'Black Killer',
            'L': 'Little',
        }

        for p in prefix:
            if p+' ' == name[:len(p)+1]:
                name = prefix[p] + ' ' + name[len(p)+1:]

        name = name.title()
        u_m_names['UN_V2_'+iname] = {'generic': name, 'NAME': ""}

    return [name, collab, collab_short]

def FanTranslatedNames(wyte, master, loc):
    wunit = {}
    for unit in wyte['Units']:
        wunit[unit['Name JPN']] = unit

    def get_collab(acquire):
        # acquire
        collabs = {
            'Sacred Stone':         ['SSM', 'Sacred Stone Memories'],
            'BF Collab':            ['BF', 'Brave Frontier'],
            'POTK':                 ['POTK', 'Phantom Of The Kill'],
            'Shinobina':            ['SN', 'Shinobi Nightmare'],
            'EO Collab':            ['EO', 'Etrian Odyssey'],
            'Fate Collab':          ['Fate', 'Fate/Stay Night [UBW]'],
            'EO Mystery Dungeon':   ['EMD', 'Etrian Mystery Dungeon'],
            'Fullmetal Alchemist':  ['FA', 'Fullmetal Alchemist'],
            'Radiant Historia':     ['RH', 'Radiant Historia'],
            'FFXV':                 ['FF', 'Final Fantasy 15'],
            'Disgaea':              ['DIS', 'Disgea'],
            'April Fools':          ['', 'April Fools'],
            'Crystal of Reunion':  ['CR', 'Crystal Re:Union']
        }
        for c in collabs:
            if c in acquire:
                return collabs[c]
        return ["", ""]

    found = {}
    none = {}
    for unit in master['Unit']:
        if unit['ai'] == "AI_PLAYER":
            [name, collab, collab_short] = name_collab(unit['iname'], loc)
            c = {
                'iname': unit['iname'],
                'official': loc[unit['iname']]['name'] if unit['iname'] in loc else name,
                'generated': name,
                'inofficial': "",
                'collab': collab,
                'collab_short': collab_short,
                'japanese': unit['name']
            }
            try:
                c['inofficial'] = ReBr.sub(
                    '', wunit[unit['name']]['Name']).rstrip(' ')
                c['inofficial2'] = wunit[unit['name']]['Name']
                if c['collab'] == "":
                    [c['collab_short'], c['collab']] = get_collab(
                        wunit[unit['name']]['Acquire'])
                found[unit['iname']] = c
                del wunit[unit['name']]
            except:
                none[unit['iname']] = c
        else:
            break

    delete = []

    for unit in none:
        try:
            for left in wunit:
                if (similarity(none[unit]['official'], ReBr.sub('', wunit[left]['Name']).title()) >= 0.8 or
                    similarity(none[unit]['official'], wunit[left]['Romanji'].title()) >= 0.8 or
                        similarity(none[unit]['japanese'], left) >= 0.8):

                    found[none[unit]['iname']] = none[unit]
                    found[none[unit]['iname']].update({
                        'inofficial': ReBr.sub('', wunit[left]['Name']).rstrip(' '),
                        'inofficial2': wunit[left]['Name'],
                        'japanese2': wunit[left]['Name JPN']
                    })
                    if none[unit]['collab'] == "":
                        [found[none[unit]['iname']]['collab_short'], found[none[unit]
                                                                           ['iname']]['collab']] = get_collab(wunit[left]['Acquire'])

                    del wunit[left]
                    delete.append(unit)
                    break

        except RuntimeError:
            pass

    for i in delete:
        try:
            del none[i]
        except:
            print(i)
    found.update(none)
    return(found)

def Skill_GenericDescription(skl,masterc,loc):
    skill=convertRawSkill(skl,loc,ENUM)
    try:
        effect_type=skill['effect_type']
    except:
        #print(skill)
        return skill
    
    desc=""

    def s(key,pre='',post=''):
        if key in skill:
            return pre+str(skill[key])+post
        else:
            return ''

    def span():
        text=[]
        if 'range_max' in skill:
            r ='Range: '
            if 'select_range' in skill:
                r+='{area} ({range})'.format(area=str(skill['select_range']),range=skill['range_max'])
            else:
                r+=str(skill['range_max'])
            text.append(r)
        if 'effect_height' in skill:
            text.append('Height: {height}'.format(height=str(skill['effect_height'])))
        if 'scope' in skill:
            r ='Area: '
            if 'select_scope' in skill:
                r+='{area} ({range})'.format(area=str(skill['select_scope']),range=math.ceil(skill['scope']*2.1))
            else:
                r+=str(skill['scope'])
            text.append(r)
        return('[{text}]'.format(text=', '.join(text)))

    def de_buffs():
        text=[]
        #Buff
        if 'self_buff_iname' in skill:
            text.append('Self: '+buff(masterc[skill['self_buff_iname']],2,2))
        if 'target_buff_iname' in skill:
            text.append('Target: '+buff(masterc[skill['target_buff_iname']],2,2))
        #Condition
        if 'self_cond_iname' in skill:
            text.append('Self: '+condition(masterc[skill['self_cond_iname']],2,2))
        if 'target_cond_iname' in skill:
            text.append('Target: '+condition(masterc[skill['target_cond_iname']],2,2))
        return(text)


    #if effect_type == "Equipment":
        #desc=''.formart()()
    if effect_type == "Attack": #"Inflicts Thunder Mag Dmg (High) on units within target area [Range: 4, Area: Diamond (5), Height Range: 2]",
        #skill['type'] == 'Attack'
        try:
            desc='{mod}% {element} {type} Dmg {span}\n{buffs}'.format(
                mod=str(skill["effect_value.max"]+100), element=s('element_type'), type=s("attack_detail"), 
                span=span(), buffs='\n'.join(de_buffs())
            )
        except KeyError:
            pass #reaction
    if effect_type == "Defend":
        desc='Blocks {value} {dmg}{chance}'.format(
            value=s('effect_value.max','','%'),
            dmg=s('reaction_damage_type').replace('Total','').replace('Damage',' Dmg').replace('Phy','Phys'),
            chance=s('effect_rate.max',' [','% Chance]')
        )
    if effect_type == "Heal":
        try: #heal
            desc='Heals {mod}% Scaling {span}'.format(
                mod=str(skill["effect_value.max"]+100), 
                span=span()
            )
        except KeyError: #prof heal ~ panacea
            desc='{buff} {span}'.format(
                buff= de_buffs(), 
                span=span()
            )
    #if effect_type == "Buff":
        #desc=''.formart()()
    #if effect_type == "Debuff":
        #desc=''.formart()()
    #if effect_type == "Revive":
        #desc=''.formart()()
    #if effect_type == "Shield":
        #desc=''.formart()()
    #if effect_type == "ReflectDamage":
        #desc=''.formart()()
    #if effect_type == "DamageControl":
        #desc=''.formart()()
    #if effect_type == "FailCondition":
        #desc=''.formart()()
    #if effect_type == "CureCondition":
        #desc=''.formart()()
    #if effect_type == "DisableCondition":
        #desc=''.formart()()
    #if effect_type == "GemsGift":
        #desc=''.formart()()
    #if effect_type == "GemsIncDec":
        #desc=''.formart()()
    #if effect_type == "Guard":
        #desc=''.formart()()
    #if effect_type == "Teleport":
        #desc=''.formart()()
    #if effect_type == "Changing":
        #desc=''.formart()()
    #if effect_type == "RateHeal":
        #desc=''.formart()()
    #if effect_type == "RateDamage":
        #desc=''.formart()()
    #if effect_type == "PerfectAvoid":
        #desc=''.formart()()
    #if effect_type == "Throw":
        #desc=''.formart()()
    #if effect_type == "EffReplace":
        #desc=''.formart()()
    #if effect_type == "SetTrick":
        #desc=''.formart()()
    #if effect_type == "TransformUnit":
        #desc=''.formart()()
    #if effect_type == "SetBreakObj":
        #desc=''.formart()()
    #if effect_type == "ChangeWeather":
        #desc=''.formart()()
    #if effect_type == "RateDamageCurrent":
        #desc=''.formart()()

    #add to expr
    if desc!= "":
        if 'expr' in skill:
            skill['expr']+='\n'+desc
        else:
            skill['expr']=desc

    return(skill)


global ENUM,SYS
[ENUM,SYS]=loadFiles(['ENums.json','sys.json'])

#hotfixes
for param in ENUM:
    if type(ENUM[param])==dict:
        ENUM[param]={int(k):v for k,v in ENUM[param].items()}