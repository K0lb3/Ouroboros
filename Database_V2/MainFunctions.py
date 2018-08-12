import os
import json
import math
import jellyfish

PATH=os.path.dirname(os.path.realpath(__file__))
EXPORTPATH=PATH+'\\export\\'

def similarity(ori, inp):
    return (jellyfish.jaro_winkler(inp, ori))

def loadFiles(files):
    ret = []
    dir = os.path.dirname(os.path.realpath(__file__))+'\\resources\\GameFiles\\'

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

def saveAsJSON(path, name, var,indent=4):
    os.makedirs(path, exist_ok=True)
    with open(os.path.join(path, name), "wb") as f:
        f.write(json.dumps(var, indent=indent, ensure_ascii=False,).encode('utf8'))
    return 1

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

def match(original, new):
    for entry in new:
        if entry in original:
            if type(new[entry])==dict:
                original[entry]=match(original[entry],new[entry])
            elif type(new[entry])==list:
                for i in new[entry]:
                    if i not in original[entry]:
                        original[entry].append(i)
            elif 1:
                pass
        else:
            original[entry]=new[entry]
    return original
            
def DifParam(gl,jp):
    dif = {}
    #no difference
    if gl==jp:
        return dif

    if type(jp)==dict:
        for key,item in jp.items():
            if key in gl:
                if gl[key]!=jp[key]:
                        dif[key]=DifParam(gl[key],jp[key])
            else:
                dif[key]=item
                
    elif type(jp)==list:
        for ji in jp:
            dif=[]
            found=False
            for gi in gl:
                if gi==ji:
                    found=True
                    break
            if not found:
                dif.append(ji)
    else:
        dif = jp

    return dif