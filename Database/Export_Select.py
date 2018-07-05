from MainFunctions import *

[master_gl,master_jp]=loadFiles(['MasterParam.json','MasterParamJP.json'])

export={}
for group in master_jp['UnitGroup']:
    print(', '.join(group['units']))
    export[group['iname']]={
        'iname':group['iname'],
        'name': '',
        'kanji': group['name'],
        'units': ', '.join(group['units']),
    }


saveAsJSON(cPath()+'\\TO_Translate\\{main}.json'.format(main='UnitGroup.json'),export)