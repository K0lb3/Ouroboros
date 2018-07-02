from MainFunctions import *
from ParamFunctions import *

def createAIO():
    [master_gl]=loadFiles(['MasterParam.json'])
    #[master_gl, master_jp,quest_gl, quest_jp, lore, wyte] = loadFiles(
    #    ['MasterParam.json', 'MasterParamJP.json', 'QuestParam.json','QuestParamJP.json', 'unit.json', 'wytesong.json'])
    #loc = Translation()

    #some tries first
    export={}
    for unit in master_gl['Unit']:
        export[unit['iname']]=UnitParam(unit)

    saveAsJSON('Test.json',export)

createAIO()