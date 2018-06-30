from MainFunctions import *
path=cPath()
[master]=loadFiles(['MasterParam.json'])
masterc = convertMaster(master)
loc=Translation()
skills=[]
new=[]
for skl in master['Skill']:
    skill=Skill_GenericDescription(skl,masterc,loc)
    skills.append(skill)

    if 'expr' in skill:
        if '\n' in skill['expr']:
            new.append(skill['expr'].split('\n'))

    

saveAsJSON(path+'\\Skills_gen.json',new)
saveAsJSON(path+'\\Skills.json',skills)