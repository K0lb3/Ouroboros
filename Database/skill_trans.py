from MainFunctions import *
path=cPath()
[master]=loadFiles(['MasterParam.json'])
masterc = convertMaster(master)
loc=Translation()
skills=[]
for skl in master['Skill']:
    skills.append(Skill_GenericDescription(skl,masterc,loc))

saveAsJSON(path+'\\Skills.json',skills)