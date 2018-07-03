from MainFunctions import *
from ParamFunctions import *

def createAIO():
    [master_gl,master_jp]=loadFiles(['MasterParam.json','MasterParamJP.json'])
    #[master_gl, master_jp,quest_gl, quest_jp, lore, wyte] = loadFiles(
    #    ['MasterParam.json', 'MasterParamJP.json', 'QuestParam.json','QuestParamJP.json', 'unit.json', 'wytesong.json'])
    #loc = Translation()

    #some tries first
    main='Artifact'
    export={}
    for unit in master_gl[main]:
        export[unit['iname']]=ArtifactParam(unit)

    saveAsJSON(main+'.json',export)

createAIO()