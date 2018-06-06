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
                    'self_buff':{
                        'ori': 's_buff',
                        'value': buff(master[skl['s_buff']],2,2)
                    },
                    'self_condition':{
                        'ori': 's_cond',
                        'value': condition(master[skl['s_cond']])
                    },
                    'target_buff':{
                        'ori': 't_buff',
                        'value': buff(master[skl['t_buff']],2,2)
                    },
                    'target_condition':{
                        'ori': 't_cond',
                        'value': condition(master[skl['t_cond']])
                    },
                }
            uses={
                'modifier': 'eff_val_max',
                "type" : 'type' ,
                "timing" : 'timing' ,
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
