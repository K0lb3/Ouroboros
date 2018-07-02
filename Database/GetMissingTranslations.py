from MainFunctions import *

[master_gl,master_jp]=loadFiles(['MasterParam.json','MasterParamJP.json'])

check={}
for main in master_jp:
    if type(master_jp[main])== list:
        try:
            check[main]={}
            for item in master_jp[main]:
                check[main][item['iname']]=item['name']

            if main in master_gl:
                for item in master_gl[main]:
                    if item['iname'] not in check[main]:
                        check[main][item['iname']]=item['name']
        except:
            pass



loc = Translation()

missing={}
for main in check:
    missing[main]={}
    for iname,kanji in check[main].items():
        if iname not in loc:
            missing[main][iname]={
                'iname':iname,
                'name': '',
                'kanji': kanji
            }

for main in missing:
    if len(missing[main])!=0:
        saveAsJSON(cPath()+'\\TO_Translate\\{main}.json'.format(main=main),missing[main])