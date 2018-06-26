import string
import os
import json
import re
import math
import requests
import re
import jellyfish

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

ParamTypes = {
    "0": "None",
    "1": "HP",
    "2": "HP",
    "3": "Max Jewels",
    "4": "Initial Jewels",
    "5": "PATK",
    "6": "PDEF",
    "7": "MATK",
    "8": "MDEF",
    "9": "Healing",
    "10": "DEX",
    "11": "AGI",
    "12": "CRIT",
    "13": "LUCK",
    "14": "MOVE",
    "15": "JUMP",
    "16": "RANGE",
    "17": "Scope",
    "18": "Height RANGE",
    "19": "Fire",
    "20": "Water",
    "21": "Wind",
    "22": "Thunder",
    "23": "Light",
    "24": "Dark",
    "25": "Poison",
    "26": "Paralyze",
    "27": "Stun",
    "28": "Sleep",
    "29": "Charm",
    "30": "Petrify",
    "31": "Blind",
    "32": "Silence",
    "33": "Bind",
    "34": "Daze",
    "35": "Infect",
    "36": "Death Sentence",
    "37": "Berserk",
    "38": "Knockback",
    "39": "Buff",
    "40": "Debuff",
    "41": "Stop",
    "42": "Quicken",
    "43": "Delay",
    "44": "Auto Heal",
    "45": "Slow",
    "46": "Rage",
    "47": "Sleep",
    "48": "All Statuses",
    "49": "Fire Res",
    "50": "Water Res",
    "51": "Wind Res",
    "52": "Thunder Res",
    "53": "Light Res",
    "54": "Dark Res",
    "55": "Poison Res",
    "56": "Paralyze Res",
    "57": "Stun Res",
    "58": "Sleep Res",
    "59": "Charm Res",
    "60": "Petrify Res",
    "61": "Blind Res",
    "62": "Silence Res",
    "63": "Bind Res",
    "64": "Daze Res",
    "65": "Infect Res",
    "66": "Death Sentence Res",
    "67": "Berserk Res",
    "68": "Knockback Res",
    "69": "Buff Res",
    "70": "Debuff Res",
    "71": "Stop Res",
    "72": "Quicken Res",
    "73": "Delay Res",
    "74": "Auto Heal Res",
    "75": "Slow Res",
    "76": "Rage Res",
    "77": "Sleep Res",
    "78": "All Status Res",
    "79": "Hit Rate",
    "80": "Evasion Rate",
    "81": "CRIT Rate",
    "82": "Jewels Obtained",
    "83": "Jewels Spent",
    "84": "Action Count Operation",
    "85": "Slash ATK",
    "86": "Pierce ATK",
    "87": "Strike ATK",
    "88": "Missile ATK",
    "89": "Magic ATK",
    "90": "Counter ATK",
    "91": "JUMP ATK",
    "92": "Guts",
    "93": "Auto Jewel Charge",
    "94": "Charge Time Ratio",
    "95": "Cast Time Ratio",
    "96": "Buff Duration",
    "97": "Debuff Duration",
    "98": "COMBO Scope",
    "99": "HP Cost Rate",
    "100": "Skill Use Count",
    "101": "Poison Dmg Rate",
    "102": "Poison Duration",
    "103": "Jewel Auto Charge",
    "104": "Jewel Auto Charge Res",
    "105": "Disable Heal",
    "106": "Disable Heal Res",
    "107": "Slash Res",
    "108": "Pierce Res",
    "109": "Strike Res",
    "110": "Missile Res",
    "111": "Magic Res",
    "112": "Counter Res",
    "113": "JUMP Res",
    "114": "Slash Evasion Rate",
    "115": "Pierce Evasion Rate",
    "116": "Strike Evasion Rate",
    "117": "Missile Evasion Rate",
    "118": "Magic Evasion Rate",
    "119": "Counter Evasion Rate",
    "120": "JUMP Evasion Rate",
    "122": "Reduced Jewel Cost",
    "123": "Assist Single-Attack",
    "124": "Assist Area-Attack",
    "125": "Resist Single-Attack",
    "126": "Resist Area-Attack",
    "127": "Assist Decrease CT",
    "128": "Assist Increase CT",
    "129": "Resist Decrease CT",
    "130": "Resist Increase CT",
    "131": "Fire DMG",
    "132": "Water DMG",
    "133": "Wind DMG",
    "134": "Thunder DMG",
    "135": "Light DMG",
    "136": "Dark DMG",
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
    copy = ['']
    for i in trans:
        if i in loc:
            loc[i].update(trans[i])
        else:
            loc[i] = trans[i]
            loc[i]['NAME'] = trans[i]['name']

        if len(loc[i]['long des']) == 0:
            loc[i]['long des'] = loc[i]['short des']

    return loc

def convertMaster(master):
    ma = {}
    for main in master:
        for sub in master[main]:
            try:
                ma[sub['iname']] = sub
            except:
                continue
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

def buff(buff, lv, mlv, array=False):
    global ParamTypes

    text = ""

    mods = []
    restriction={}
    rate= buff['rate'] if 'rate' in buff else 100

    if 'elem' in buff:
        text += element(buff['elem']).title()+': '
        restriction['element']=element(buff['elem'])
    if 'birth' in buff:
        text += (birth[buff['birth']]).title()+': '
        restriction['birth']=element(buff['birth'])

    allElem = {'mod': "", 'index': []}
    allAtk = {'mod': "", 'index': []}

    used = ['type', 'vmax', 'vini']
    for i in range(1, 9):
        for u in used:
            if (u+'0'+str(i)) in buff:
                buff[u+str(i)] = buff[u+'0'+str(i)]

        if ('type'+str(i) in buff) and ('vmax'+str(i) in buff):
            btyp = buff['type'+str(i)]
            bmin = buff['vini'+str(i)]
            bmax = buff['vmax'+str(i)]
            bmod = '' if (
                'calc'+str(i) not in buff or buff['calc'+str(i)] == 0) else '%'

            try:
                number = math.floor(bmin + ((bmax - bmin) * (lv-1) / (mlv-1)))
                if number==0:
                    continue
                # add + if pos
                number = '+'+str(number) if number >= 0 else str(number)
                mods.append({
                    'value': number, 
                    'mod': bmod,
                    'stat': ParamTypes[str(btyp)]
                    })

                if 84 < btyp and btyp < 92:
                    allAtk['index'].append(i-1)
                    allAtk['mod'] = [number, bmod]
                if 48 < btyp and btyp < 55:
                    allElem['index'].append(i-1)
                    allElem['mod'] = [number, bmod]
            except:
                print(buff)
                print(btyp)
                print(bmin)
                print(bmax)
                print(bmod)

        else:
            break

    # reduce clutter | all elements & all dmg shortening
    # +25 Slash ATK & +25 Pierce ATK & +25 Strike ATK & +25 Missile ATK & +25 MATK & +25 JUMP ATK

    if not array:
        if len(allAtk['index']) == 6:
            mods = [i for j, i in enumerate(mods) if j not in allAtk['index']]
            mods.append({
                'value':allAtk['mod'][0],
                'mod': allAtk['mod'][1],
                'stat': 'Damage'
            })
        if len(allElem['index']) == 6:
            mods = [i for j, i in enumerate(mods) if j not in allElem['index']]
            mods.append({
                'value':allElem['mod'][0],
                'mod': allElem['mod'][1], 
                'stat':'Elemental Res'
            })

        for m in mods:
            text += '{value}{mod} {stat} & '.format(value=m['value'],mod=m['mod'],stat=m['stat'])
        text=text[:-3]
        if rate!=100:
            text+= ' [{chance}%]'.format(chance=rate)
    else:
        mod.append(restriction)
        mod.append({'chance':rate})

    return mods if array else text

def condition(cond, lv,  mlv):
    condType = {
        0: 'Poison',
        1: 'Paralysed',
        2: 'Stun',
        3: 'Sleep',
        4: 'Charm',
        5: 'Stone',
        6: 'Blindness',
        7: 'Disable Skill',
        8: 'Disable Move',
        9: 'Disable Attack',
        10: 'Zombie',
        11: 'Death Sentence',
        12: 'Berserk',
        13: 'Disable Knockback',
        14: 'Disable Buff',
        15: 'Disable Debuff',
        16: 'Stop',
        17: 'Quicken',
        18: 'Slow',
        19: 'Auto Heal',
        20: 'Donsoku',
        21: 'Rage',
        22: 'Good Sleep',
        23: 'Auto Jewel',
        24: 'Disable Heal',
    }
    if 'rini' in cond and cond['rini'] < 999:
        rate = math.floor(
            cond['rini'] + ((cond['rmax']-cond['rini']) * (lv-1) / (mlv-1)))
        return(condType[cond['conds'][0]] + ' ['+str(rate)+'%]')
    else:
        return(condType[cond['conds'][0]])

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
    WeaponFormulaTypes = {
        0: "None",
        1: "Atk",
        2: "Mag",
        3: "AtkSpd",
        4: "MagSpd",
        5: "AtkDex",
        6: "MagDex",
        7: "AtkLuk",
        8: "MagLuk",
        9: "AtkMag",
        10: "SpAtk",
        11: "SpMag",
        12: "AtkSpdDex",
        13: "MagSpdDex",
        14: "AtkDexLuk",
        15: "MagDexLuk",
        16: "Luk",
        17: "Dex",
        18: "Spd",
        19: "Cri",
        20: "Def",
        21: "Mnd",
        22: "AtkRndLuk",
        23: "MagRndLuk",
        24: "AtkEAt",
        25: "MagEMg",
        26: "AtkDefEDf",
        27: "MagMndEMd",
        28: "LukELk",
        29: "MHp",
    }

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
        "AtkRndLuk": modifier(weaponParam['atk']/10) + " (3*PATK + (1 + 0~[LV/10]) * LUCK)",
        "MagRndLuk": modifier(weaponParam['atk']/10) + " (3*MATK + (1 + 0~[LV/10]) * LUCK)",
        "AtkSpdDex": modifier(weaponParam['atk']/20) + " (PATK + AGI/2 + AGI*LV/100 + DEX/4)",
        "MagSpdDex": modifier(weaponParam['atk']/20) + " (MATK + AGI/2 + AGI*LV/100 + DEX/4)",
        "AtkDexLuk": modifier(weaponParam['atk']/20) + " (PATK + DEX/2 + LUCK/2)",
        "MagDexLuk": modifier(weaponParam['atk']/20) + " (MATK + DEX/2 + LUCK/2)",
        "AtkEAt":    modifier(weaponParam['atk']/10) + " (2*PATK - Target's ATK)",
        "MagEMg":    modifier(weaponParam['atk']/10) + " (2*MATK - Target's MATK)",
        "LukELk":    modifier(weaponParam['atk']/10) + " (2*LUCK - Target's LUCK)",
        "AtkDefEDf": modifier(weaponParam['atk']/10) + " (PATK + PDEF - Target's PDEF)",
        "MagMndEMd": modifier(weaponParam['atk']/10) + " (MATK + MDEF - Target's MDEF)",
        #default = PATK;
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
        name = loc['UN_V2_'+iname]['NAME']
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
                'official': loc[unit['iname']]['NAME'] if unit['iname'] in loc else name,
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
