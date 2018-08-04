from MainFunctions import *
import ParamFunctions

PATH_convert=os.path.join(os.path.dirname(os.path.realpath(__file__)),'_converted1')
PATH_convert2=os.path.join(os.path.dirname(os.path.realpath(__file__)),'_converted2')

def convertRaws(save=True):
    masters=loadFiles(
    ['MasterParam.json', 'MasterParamJP.json', 'QuestParam.json','QuestParamJP.json', 'QuestDropParam.json'])
    #loc = Translation()
    export={}
    #some tries first
    i=0
    for master in masters:
        for main in master:
            #get right method for convertion
            mName=main[0].upper()+main[1:]
            method= getattr(ParamFunctions, mName+'Param',  getattr(ParamFunctions, mName+'EffectParam',False))
            if not method:
               continue
               #print('Not Found: '+main)

            #convert the main
            print(main)
            converted={}
            if 'iname' in master[main][0]:#most Params
                converted={
                    entry['iname']:method(entry)
                    for entry in master[main]
                    }
            else: #like Tobira
                converted=[
                    method(entry)
                    for entry in master[main]
                    ]
                if len(converted)==1:
                    converted=converted[0]

            #add to new main
            if main in export:
                if type(converted)==dict:
                    if type(list(converted.items())[0][1])==dict:
                        for item in converted:
                            if item in export[main]:
                                export[main][item]['dif']=DifParam(export[main][item],converted[item])
                            else:
                                export[main][item]=converted[item]
                    else:
                        export[main]['dif']=DifParam(export[main],converted)
                elif type(converted)==list:
                    pass
                print('Fused '+main)
            else:
                export[main]=converted

    if save:
        for main in export:
            saveAsJSON(PATH_convert, main+'.json',export[main])
        #saveAsJSON(PATH_convert, 'AIO.json',export)
    return export

def Fixes(master):
    UNIT=master['Unit']
    SKILL=master['Skill']
    BUFF=master['Buff']
    ITEM=master['Item']
    JOB=master['Job']

    ##units##########################################################
    print('Fix - Unit')
    # jobset to job and add skins and seperate unit and NPC
    (eUnit, eEnemy)=({},{})
    for key,unit in UNIT.items():
        #unit or enemy
        if 'lore' in unit or ('piece' in unit and 'ai' in unit and unit['ai']=='AI_PLAYER' and 'EN' != key[6]+key[7] and 'role' not in unit):
            eUnit[key]=unit
        else:
            eEnemy[key]=unit

        #add skins from dif
        if 'dif' in unit and 'skins' in unit['dif']:
            unit['skins']+=unit['dif']['skins']

        #change js to job
        if 'jobsets' in unit:
            unit['jobs']=[
                master['JobSet'][js]['job']
                for js in unit['jobsets']
                ]
            del unit['jobsets']

    #add job+
    for key,js in master['JobSet'].items():
        if 'jobchange' in js:
            unit = UNIT[js['target_unit']]
            if 'jobchanges' not in unit:
                unit['jobchanges']=[None]*len(unit['jobs'])

            for index,job in enumerate(unit['jobs']):
                if job == js['lock_jobs']['iname']:
                    unit['jobchanges'][index]=js['job']

    #add kaigan
    for kaigan in master['Tobira']:
        unit = UNIT[kaigan['mUnitIname']]
        if 'kaigan' not in unit:
            unit['kaigan']={}
        unit['kaigan'][kaigan['mCategory']]=kaigan

    #add nensou
    for key,card in master['ConceptCard'].items():
        if 'effects' not in card:
            continue

        for effect in card['effects']:
            if 'cnds_iname' not in effect:
                continue

            conds = master['ConceptCardConditions'][effect['cnds_iname']]
            if 'unit_group' not in conds:
                continue
            units = master['UnitGroup'][conds['unit_group']]['units']

            if len(units)==1:
                #skin
                if 'skin' in effect:
                    UNIT[units[0]]['skins'].append(effect['skin'])
                #normal stuff
                UNIT[units[0]]['conceptcard']=key

    #save
    saveAsJSON(PATH_convert2, 'Unit.json',eUnit)
    saveAsJSON(PATH_convert2, 'Enemy.json',eEnemy)
    ##job############################################################
    print('Fix - Job')
    #abilities and stats
    for key,job in master['Job'].items():
        if 'fixed_ability' not in job:
            continue
        #abilities
        abilities={
            'main': job['fixed_ability'],
            'sub' : '',
            'reaction': [],
            'passive': []
        }
        for abil in job['abilities']:
            slot = master['Ability'][abil]['slot']
            if slot=='Action':
                abilities['sub']=abil
            elif slot == 'Reaction':
                abilities['reaction']+=[abil]
            elif slot == 'Support':
                abilities['passive']+=[abil]

        job['abilities']=abilities
        del job['fixed_ability']

        #stats
        stats={}
        for i,rank in enumerate(job['ranks']):
            if i == 0:
                continue
            for item in rank['equips']:
                for buff in BUFF[SKILL[ITEM[item]['skill']]['target_buff_iname']]['buffs']:
                    if buff['type'] not in stats:
                        stats[buff['type']]=0
                    if i<len(job['ranks'])-1:
                        stats[buff['type']]+=buff['value_ini']
                    else:
                        stats[buff['type']]+=buff['value_max']
        job['stats']=stats

    #save
    saveAsJSON(PATH_convert2, 'Job.json',JOB)


    


Con1=convertRaws()
Fixes(Con1)









    # for master in masters:
    #     i+=1
    #     for main in master:
    #         mName=main[0].upper()+main[1:]
    #         method= getattr(ParamFunctions, mName+'Param',False)
    #         if not method:
    #             method= getattr(ParamFunctions, mName+'EffectParam',False)
    #         if method:
    #             if type(master[main])==dict: #like FIxParam
    #                 export[main]=method(master[main])
    #             elif type(master[main])==list:
    #                 if 'iname' in master[main][0]:#most Params
    #                     export[main]={
    #                         entry['iname']:method(entry)
    #                         for entry in master[main]
    #                         }
    #                 else: #like Tobira
    #                     export[main]=[
    #                         method(entry)
    #                         for entry in master[main]
    #                         ]
    #                 print('Success: '+main)
    #                 #save as json
    #                 saveAsJSON(PATH_convert, main+('.json'if i%2 else 'JP.json'),export[main]) 
    #             else:
    #                 print('Failed: '+main)           
    #         else:
    #             pass
    #             #print('Not Found: '+main)
    # return masters
