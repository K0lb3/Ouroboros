from MainFunctions import *

ESkillType=["Attack", "Skill", "Passive", "Item", "Reaction"]
ESkillTiming=["Used", "Passive", "Wait", "Dead", "DamageCalculate", "DamageControl", "Reaction", "FirstReaction", "Auto"]

def Skills(ability,master,loc):
    if type(ability)!="dict":
        ability=master[ability]
    print (ability)
    s=1
    skills={}
    while True:
        if True:
        #try:
            #ini
            skl = master[ability['skl'+str(s)]]
            iname=skl['iname']

            skill={
                'iname':iname,
                'name':"",
                'expr':"",
            }


            def formulas(var,value):
                formula={
                'formula':{
                    'ori':'weapon',
                    'value': dmg_formula(master[skl['weapon']])
                },
                'buff':{
                    'ori':
                }
                }
            uses={
                'modifier': 'eff_val_max',
                }
            uses2={
                "eff_ini":"eff_val_ini",
                "eff_max":"eff_val_max",
                "eff_type":"eff_type",
                "element":"elem",
                "weapon" : 'weapon' ,
                "tokkou" : 'tktag' ,
                "tk_rate" : 'tkrate' ,
                "type" : 'type' ,
                "timing" : 'timing' ,
                "condition" : 'cond' ,
                "target" : 'target' ,
                "line_type" : 'line' ,
                "lvcap" : 'cap' ,
                "cost" : 'cost' ,
                "count" : 'count' ,
                "rate" : 'rate' ,
                "select_range" : 'sran' ,
                "range_min" : 'rangemin' ,
                "range_max" : 'range' ,
                "select_scope" : 'ssco' ,
                "scope" : 'scope' ,
                "effect_height" : 'eff_h' ,
                "back_defrate" : 'bdb' ,
                "side_defrate" : 'sdb' ,
                "ignore_defense_rate" : 'idr' ,
                "job" : 'job' ,
                "SceneName" : 'scn' ,
                "ComboNum" : 'combo_num' ,
                #"ComboDamageRate" : (100 - math.floor(skl.combo_rate)),
                #"IsCritical" : 1 if skl.is_cri != 0 else 0,
                "JewelDamageType" : 'jdtype' ,
                "JewelDamageValue" : 'jdv' ,
                #"IsJewelAbsorb" :  1 if (skl.jdabs != 0) else 0,
                "DuplicateCount" : 'dupli' ,
                "CollaboMainId" : 'cs_main_id' ,
                "CollaboHeight" : 'cs_height' ,
                "KnockBackRate" : 'kb_rate' ,
                "KnockBackVal" : 'kb_val' ,
                "DamageDispType" : 'dmg_dt' ,
                }

            for u in uses:
                try:
                    skill[uses[u]]=skl[u]
                except:
                    pass

            if iname in loc:
                skill['name']=loc[iname]['NAME']
                skill['expr']=loc[iname]['EXPR']
            else:
                skill['name']=""
                skill['expr']=""


            skills[skill['iname']]=skill
            s+=1
            break
        #except NameError:
        #    break
    
    print(skills)
    #return(skills)














#code

Skills('AB_BASI_SOL_UPPER', convertMaster(loadFiles(['MasterParam.json'])[0]),loadFiles(['LocalizedMasterParam.json'])[0])
