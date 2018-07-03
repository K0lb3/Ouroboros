from MainFunctions import *
from ParamFunctions import *

def createAIO():
    [master_gl,master_jp]=loadFiles(['MasterParam.json','MasterParamJP.json'])
    #[master_gl, master_jp,quest_gl, quest_jp, lore, wyte] = loadFiles(
    #    ['MasterParam.json', 'MasterParamJP.json', 'QuestParam.json','QuestParamJP.json', 'unit.json', 'wytesong.json'])
    #loc = Translation()

    #some tries first
    export={}
    #export['Fix']=FixParam(master_gl[main])
    #export['Buff']={
    #    buff['iname']:BuffEffectParam(buff)
    #    for buff in master_gl['Buff']
    #}
    #export['Job']={
    #    entry['iname']:JobParam(entry)
    #    for entry in master_gl['Job']
    #}
    #export['Item']={
    #    entry['iname']:ItemParam(entry)
    #    for entry in master_gl['Item']
    #}
    export={
        entry['iname']:SkillParam(entry)
        for entry in master_gl['Skill']
    }
    saveAsJSON('Skill'+'.json',export)

createAIO()