from MainFunctions import *


def drops():
    [quests, master, jp, locM, locQ] = loadFiles(
        ['QuestParam.json', 'MasterParam.json', 'MasterParamJP.json', 'LocalizedMasterParam.json', 'LocalizedQuestParam.json'])
    quest = quests['quests']
    master = convertMaster(master)
    jp = convertMaster(jp)
    export = {}

    def loc(iname):
        if iname in locM:
            return(locM[iname]['name'])
        else:
            return(iname[6:].replace('_', ' ').title())

    def loc_drops(drops, loc):
        ret = []
        for d in drops:
            ret.append(loc(d))
        return(ret)

    def icon(iname):
        if iname in master:
            return master[d]['icon']
        elif d in jp:
            return jp[d]['icon']

        return iname

    drop = {}
    for q in quest:
        if 'drops' in q and len(q['drops']):
            for d in q['drops']:
                if d not in drop:
                    drop[d] = {
                        'iname':    d,
                        'name':    loc(d),
                        'link':    'http://www.alchemistcodedb.com/item/{iname}'.format(iname=d.replace('_', '-').lower()),
                        'icon':    'http://cdn.alchemistcodedb.com/images/items/icons/{icon}.png'.format(icon=icon(d)),
                        'story':    [],
                        'event':    []
                    }

                if (q['iname'] in locQ) and ('Ch ' in locQ[q['iname']]['title']):
                    drop[d]['story'].append({
                        'name': '[{cost} AP] {title}'.format(cost=str(q['pt']), title=locQ[q['iname']]['title']),
                        'drops': loc_drops(q['drops'], loc)
                    })
                else:
                    drop[d]['event'].append({
                        'name': '[{cost} AP] {MP} {title}'.format(cost=str(q['pt']), MP='MP' if 'multi' in q else "", title=locQ[q['iname']]['name'] if q['iname'] in locQ else q['iname']),
                        'drops': loc_drops(q['drops'], loc)
                    })
    #print(json.dumps(drop, indent=4))
    for i in drop:
        drop[i]['inputs'] = [drop[i]['name']]
    return(drop)


# save to out
path = cPath()+'\\out\\'
drop = drops()
saveAsJSON(path+'drops.json', drop)
# GSSUpload(drop,"drops")
