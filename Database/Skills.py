from MainFunctions import *
from SkillParam import convert_raw_skill

def Get_Skills(ability,master,loc):
    if type(ability)!="dict":
        ability=master[ability]
    print (ability)
    s=1
    skills={}
    while 'skl'+str(s) in ability:
        siname=ability['skl'+str(s)]

        skill=convert_raw_skill(master[siname],loc)
        skills[siname]=skill
        s+=1
    
    print(skills)
    #return(skills)

#code
Get_Skills('AB_BASI_SOL_UPPER', convertMaster(loadFiles(['MasterParam.json'])[0]),loadFiles(['LocalizedMasterParam.json'])[0])
