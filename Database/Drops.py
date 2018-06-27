from MainFunctions import *


def drops():
    [qdrop,locM, locQ, qmaster,master] = loadFiles(
        ['QuestDropParam.json','LocalizedMasterParam.json', 'LocalizedQuestParam.json','QuestParam.json','MasterParam.json'])
    qmaster=convertMaster(qmaster)
    master=convertMaster(master)
    qdrop_c=convertMaster(qdrop)

    export = {}
    #fix quests first
    for item in qdrop['simpleQuestDrops']:
        for quest in item["questlist"]:
            try:
                if item['iname'] not in qdrop_c[quest]['droplist']:
                    qdrop_c[quest]['droplist'].append(item['iname'])
            except KeyError:
                try:
                    qdrop_c[quest]['droplist']=[item['iname']]
                except:
                    pass


    def generate_droplist(qlist):
        story=[]
        hard=[]
        event=[]

        def loc_drops(drops):
            ret=[]
            for drop in drops:
                ret.append(locM[drop]['name'])
            return(ret)

        for quest in qlist:
            q=qmaster[quest]
            if 'pt' not in q:
                continue

            try:
                if (quest in locQ) and ('Ch ' in locQ[quest]['title']):
                    story.append({
                        'name': '[{cost} AP] {title}'.format(cost=str(q['pt']), title=locQ[quest]['title']),
                        'drops': loc_drops(qdrop_c[quest]['droplist'])
                        })
                else:
                    event.append({
                        'name': '[{cost} AP] {MP} {title}'.format(cost=str(q['pt']), MP='MP' if 'multi' in q else "", title=locQ[quest]['name'] if quest in locQ else quest),
                        'drops': loc_drops(qdrop_c[quest]['droplist'])
                        })
            except:
                print(quest)

        return({'story':story,'hard':hard,'event':event})

    #listing them in earnest now
    for item in qdrop['simpleQuestDrops']:
        iname=item['iname']

        drops=generate_droplist(item['questlist'])

        export[iname]={
            'iname':iname,
            'name': locM[iname]['name'],
            'inputs': [locM[iname]['name']],
            'link': 'http://www.alchemistcodedb.com/item/{iname}'.format(iname=iname.replace('_', '-').lower()),
            'icon': 'http://cdn.alchemistcodedb.com/images/items/icons/{icon}.png'.format(icon=master[iname]['icon']),
            'story':drops['story'],
            #'hard': drops['hard'],
            'event':drops['event']
            }
    
    return(export)


# save to out
path = cPath()+'\\out\\'
saveAsJSON(path+'drops.json', drops())
# GSSUpload(drop,"drops")
