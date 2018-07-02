import os
import json
import math as Math
import jellyfish

PATH=os.path.dirname(os.path.realpath(__file__))
EXPORTPATH=PATH+'\\export\\'

def similarity(ori, inp):
    return (jellyfish.jaro_winkler(inp, ori))

def loadFiles(files):
    ret = []
    dir = os.path.dirname(os.path.realpath(__file__))+'\\resources'

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
    os.makedirs(EXPORTPATH, exist_ok=True)
    with open(EXPORTPATH+name, "wb") as f:
        f.write(json.dumps(var, indent=4, ensure_ascii=False).encode('utf8'))
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

def TACScale(ini,max,lv,mlv):
    return math.floor(ini + ((max-ini) / (mlv-1) * (lv-1)))

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
