from MainFunctions import *
import ParamFunctions

def createAIO():
    master=FuseMasterParams()
    #loc = Translation()

    #some tries first
    export={}
    for main in master:
        method= getattr(ParamFunctions, main+'Param',False)
        if not method:
            method= getattr(ParamFunctions, main+'EffectParam',False)
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
            else:
                 print('Failed: '+main)           
        else:
            print('Not Found: '+main)


    saveAsJSON('AIO.json',export)

createAIO()