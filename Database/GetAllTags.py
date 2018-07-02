from MainFunctions import *

[master_gl,master_jp]=loadFiles(['MasterParam.json','MasterParamJP.json'])
gl = convertMaster(master_gl)
jp = convertMaster(master_jp)

tags={}
for main in master_jp:
    tags[main]={}
    try:
        for item in master_jp[main]:
            if 'tag' in item:
                ltags= item['tag'].split(',')
                for tag in ltags:
                    if tag not in tags[main]:
                        tags[main][tag]=[item['iname']]
                    elif len(tags[main][tag])<20:
                        tags[main][tag].append(item['iname'])
    except:
        pass

saveAsJSON(cPath()+'\\tags.json',tags)