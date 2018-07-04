from MainFunctions import *
#[master_gl, master_jp,quest_gl, quest_jp, quest_drop] = loadFiles(
#    ['MasterParam.json', 'MasterParamJP.json', 'QuestParam.json','QuestParamJP.json', 'QuestDropParam.json')

def match(original, new):
    for entry in new:
        if entry in original:
            if type(new[entry])==dict:
                original[entry]=match(original[entry],new[entry])
            elif type(new[entry])==dict:
                for i in new[entry]:
                    if i not in original[entry]:
                        original[entry].append(i)
            elif 1:
                pass
        else:
            original[entry]=new[entry]
    return original
            

def FuseMasterParams():
    [master_gl, master_jp]=loadFiles(['MasterParam.json', 'MasterParamJP.json'])
    master=match(master_gl,master_jp)
    SaveAsJson(cpath()+'\\fusedMaster.json',master)

FuseMasterParams()


