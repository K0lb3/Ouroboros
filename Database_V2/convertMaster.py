from MainFunctions import *
import ParamFunctions

def createAIO():
    masters=loadFiles(
    ['MasterParam.json', 'MasterParamJP.json', 'QuestParam.json','QuestParamJP.json', 'QuestDropParam.json'])
    #loc = Translation()
    export={}
    #some tries first
    i=0
    for master in masters:
        i+=1
        for main in master:
            mName=main[0].upper()+main[1:]
            method= getattr(ParamFunctions, mName+'Param',False)
            if not method:
                method= getattr(ParamFunctions, mName+'EffectParam',False)
            if method:
                if type(master[main])==dict: #like FIxParam
                    export[main]=method(master[main])
                elif type(master[main])==list:
                    if 'iname' in master[main][0]:#most Params
                        export[main]={
                            entry['iname']:method(entry)
                            for entry in master[main]
                            }
                    else: #like Tobira
                        export[main]=[
                            method(entry)
                            for entry in master[main]
                            ]
                    print('Success: '+main)
                    #save as json
                    saveAsJSON(main+('.json'if i%2 else 'JP.json'),export[main]) 
                else:
                    print('Failed: '+main)           
            else:
                pass
                #print('Not Found: '+main)

createAIO()