from MainFunctions import *

def loc_drops(drops,loc):
    ret = []
    for d in drops:
        ret.append(loc[d]['NAME'])
    return(ret)

def drops():
    [quests,master,loc,locQ]=loadFiles(['QuestParam.json','MasterParam.json','LocalizedMasterParam.json','LocalizedQuestParam.json'])
    quest = quests['quests']
    master = convertMaster(master)
    export={}

    drop = {}
    for  q in quest:
        if 'drops' in q and len(q['drops']):
            for d in q['drops']:
                if d not in drop:
                    drop[d]={
                        'iname' :    d, 
                        'name'  :    loc[d]['NAME'],
                        'link'  :    'http://www.alchemistcodedb.com/item/'+d.replace('_', '-').lower(),
                        'icon'  :    'http://cdn.alchemistcodedb.com/images/items/icons/'+master[d]['icon']+'.png', 
                        'story' :    [], 
                        'event' :    []
                    }

                if (q['iname'] in locQ) and ('Ch ' in locQ[q['iname']]['TITLE']):
                    drop[d]['story'].append({
                        'name': '['+str(q['pt'])+' AP] '+locQ[q['iname']]['TITLE'],
                        'drops':loc_drops(q['drops'],loc)
                        })   
                else:
                    drop[d]['event'].append({
                        'name': '['+str(q['pt'])+' AP] '+('MP ' if 'multi' in q else "") + (locQ[q['iname']]['NAME'] if (q['iname'] in locQ) else q['iname']), 
                        'drops':loc_drops(q['drops'],loc)
                        })
    #print(json.dumps(drop, indent=4)) 
    for i in drop:
        drop[i]['inputs']=[drop[i]['name']]
    return(drop)

#save to out
path=cPath()+'\\out\\'
drop=drops()
saveAsJSON(path+'drops.json',drop)
#GSSUpload(drop,"drops")