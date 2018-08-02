from MainFunctions import *
import ParamFunctions

PATH_convert=os.path.join(os.path.dirname(os.path.realpath(__file__)),'_converted1')

def createAIO():
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

    for main in export:
        saveAsJSON(PATH_convert, main+'.json',export[main])
    saveAsJSON(PATH_convert, 'AIO.json',export)

createAIO()










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
