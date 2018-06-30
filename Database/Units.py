from MainFunctions import *
import jellyfish

def Units():
    def master_ability(unit, cMaster, loc):
        skill = cMaster[cMaster[unit['ability']]['skl1']]

        text = (loc[skill['iname']]['name']+"\n") if skill['iname'] in loc else ""

        if 't_buff' in skill:
            text += buff(cMaster[skill['t_buff']], 2, 2)

        if 't_cond' in skill:
            if 't_buff' in skill:
                text += ' & '
            text += condition(cMaster[skill['t_cond']], 2, 2).replace('[Chance: 100%]', '')

        return text

    def add_tierlist(units):
        [tierlist] = loadFiles(['tierlist_gl.json'])
        # convert tierlist
        tierlist = tierlist['Tier List']

        rating = ["", "D", "D/C", "C", "C/B", "B",
                "B/A", "A", "A/S", "S", "S/SS", "SS"]

        tl = {}
        for row in range(1, len(tierlist)):
            for i in range(0, 6):
                j = i*6+1
                tl[tierlist[row][j]] = {
                    'job 1': tierlist[row][j+1],
                    'job 2': tierlist[row][j+2],
                    'job 3': tierlist[row][j+3],
                    'jc': tierlist[row][j+4],
                    'total': ""
                }
        # add total
        invalid=[]
        for i in tl:
            best = 0
            for j in tl[i]:
                try:
                    index = rating.index(tl[i][j])
                    if index > best:
                        best = index
                except:
                    invalid.append(i)
            tl[i]['total'] = rating[best]


        # add tierlist to units
        for i in tl:
            if i in invalid:
                continue

            found = 0
            for u in units:
                if units[u]['name'] == i:
                    units[u]['tierlist'] = tl[i]
                    found = 1
                    break
            if found == 0:
                max = 0
                best = ""
                for iname,obj in units.items():
                    if 'tierlist' in obj:
                        continue
                    for inputs in obj['inputs']:
                        sim = jellyfish.jaro_winkler(i, inputs.replace('Fate','[F/sn]'))
                        if sim > max:
                            max = sim
                            best = iname
                if max > 0.90:
                    units[best]['tierlist'] = tl[i]
                    print(i + ' to ' + best + ' sim: ' + str(max))
                else:
                    print('Not Found:',i,str(max),best)

        return units

    jobe = {}  # missing job evolves
    mjob = {}  # missing jobs

    # load files from work dir\res
    [gl, jp, quest_jp, lore, wyte] = loadFiles(
        ['MasterParam.json', 'MasterParamJP.json', 'QuestParamJP.json', 'unit.json', 'wytesong.json'])
    loc = Translation()
    # load drop list
    [drops] = loadOut(['drops.json'])
    pieces = {}
    for drop in drops:
        drop = drops[drop]
        if drop['iname'][:6] == 'IT_PI_' and len(drop['story']) > 0:
            pieces[drop['iname']] = []
            for mission in drop['story']:
                pieces[drop['iname']].append('GL: '+mission['name'])

    # patch jp unto drop list
    for quest in quest_jp['quests']:
        if 'pieces' in quest and quest['iname'][:9] == "QE_ST_HA_":
            for piece in quest['pieces']:
                if piece not in pieces:
                    pieces[piece] = []
                # "title": "【ハード】１章３話1-10",
                name = '['+str(quest['pt'])+' AP] ' + quest['title'].replace('【ハード】',
                                                                             '[Hard] Ch ').replace('章', ': Ep ').replace('話', ' [') + ']'
                name = name.replace('１', '1').replace('２', '2').replace(
                    '３', '3').replace('４', '4').replace('５', '5')
                # [24 AP] [Hard] Ch 1: Ep 3 [1-10]"
                pieces[piece].append('JP: '+name)

    # patching loc
    FAN = FanTranslatedNames(wyte, jp, loc)

    # convert master parameters
    glc = convertMaster(gl)
    jpc = convertMaster(jp)
    jpS = json.dumps(jp)

    mainc_=convertMaster(gl)
    mainc_.update(jpc)

    units = {}

    #create unit list
    unit_list={}
    for unit in jp['Unit']:
        if unit['ai'] != "AI_PLAYER":
            print('End at: ', unit['iname'])
            break
        unit_list[unit['iname']]=unit

    for unit in gl['Unit']:
        if 'UN_V2_' not in unit['iname']:
            print('End at: ', unit['iname'])
            break
        if unit['ai'] != "AI_PLAYER":
            print('End at: ', unit['iname'])
            break
        if unit['iname'] not in unit_list:
            unit_list[unit['iname']]=unit
        else:
            unit_list[unit['iname']].update(unit)




    for iname,unit in unit_list.items():
        [main, mainc]= [gl, glc] if iname in glc else [jp, jpc]
        units[iname] = {
            'iname': iname,
            'name': FAN[iname]['official'] if iname in FAN else unit['name'],
            'inofficial': FAN[iname]['inofficial'] if iname in FAN else "",
            'gender': "♀" if (unit['sex']-1) else "♂" if (unit['sex']) else "/",
            'element': element(unit['elem']).title() if 'elem' in unit else "/",
            'country': birth[unit['birth']] if 'birth' in unit else "/",
            'collab':  FAN[iname]['collab'] if iname in FAN else "",
            'collab short': FAN[iname]['collab_short'] if iname in FAN else "",
            'rarity': rarity(unit['rare'], unit['raremax']),
            'icon': 'http://cdn.alchemistcodedb.com/images/units/profiles/' + unit["img"] + ".png",
            'farm': pieces[unit['piece']] if ('piece' in unit and unit['piece'] in pieces) else [],
            'leader skill': buff(mainc_[mainc_[unit['ls6']]['t_buff']], 1, 2).replace('Restriction(s): ','').replace('Element: ','').replace('Country: ','').replace('\n',': ') if ('ls6' in unit) and ('t_buff' in mainc_[unit['ls6']]) else "/",
            'master ability': master_ability(unit, mainc_, loc) if 'ability' in unit else "",
            # 'shard quests': drops[unit['piece']],
            'link': 'http://www.alchemistcodedb.com/' + (''if iname in glc else 'jp/') + 'unit/' + iname.replace('UN_V2_', "").replace('_', "-").lower(),
            'job 1': "",
            'job 2': "",
            'job 3': "",
            'jc 1': "",
            'jc 2': "",
            'jc 3': "",
            'inputs': [],
            "stats":  {}
        }

        # add stats
        stats = {
            "hp":   "HP",
            'mp':   "Max Jewels",
            "atk":  "PATK",
            "def":  "PDEF",
            "mag":  "MATK",
            "mnd":  "MDEF",
            "dex":  "DEX",
            "spd":  "AGI",
            "cri":  "CRIT",
            "luk":  "LUCK",
        }
        unitLevel=84
        for raw,con in stats.items():
            units[iname]['stats'][con] = TACScale(unit[raw],unit['m'+raw],unitLevel,100)


        # add lore
        if iname in lore:
            units[iname].update(lore[iname])

        # add artworks
        art_link = 'http://cdn.alchemistcodedb.com/images/units/artworks/'
        units[iname]['artworks'] = [
            ({'name': "default", 'full': art_link+unit['img']+'.png', 'closeup':art_link+unit['img']+'-closeup.png'})]
        if 'skins' in unit:
            for s in unit['skins']:
                if mainc_[s]['asset'] != 'unique':
                    units[iname]['artworks'].append({
                        'name':     loc[s]['name'] if s in loc else s[(7+len(unit['img'])):].replace('-', ' ').title(),
                        'full':     art_link+unit['img'] + '_' + mainc_[s]['asset']+'.png',
                        'closeup':  art_link+unit['img'] + '_' + mainc_[s]['asset']+'-closeup.png',
                    })

        if (jpS.find('AF_SK_' + unit['img'].upper() + '_BABEL')+1):
            units[iname]['artworks'].append({
                'name':     'Kaigan',
                'full':     art_link+unit['img'] + '_' + 'babel'+'.png',
                'closeup':  art_link+unit['img'] + '_' + 'babel'+'-closeup.png',
            })

        # add jobs
        j = 1
        if 'jobsets' in unit:
            for i in unit['jobsets']:
                job = mainc_[i]['job']

                if job not in loc:
                    mjob[job] = {'unit': iname, 'name': ""}

                units[iname]['job ' +
                             str(j)] = job if not job in loc else loc[job]['name']
                if job not in glc:
                    units[iname]['job '+str(j)] += 'ᴶ'

                j += 1

    # add collabs
    #Gilg, Yomi, Selena,Vargas, eo1

    # add weapon abilities

    # add possible inputs
    export = {}
    for u in units:
        unit = units[u]
        if unit['name'] not in export:
            units[u]['inputs'].append(unit['name'])
            export[unit['name']] = u
        if len(unit['inofficial']) > 1 and unit['inofficial'] not in export:
            units[u]['inputs'].append(unit['inofficial'])
            export[unit['inofficial']] = u

        if len(unit['collab short']) > 1:
            units[u]['inputs'].append(
                unit['name'] + ' ' + unit['collab short'])
            units[u]['inputs'].append(
                unit['collab short'] + ' ' + unit['name'])

    # add tierlist
    units = add_tierlist(units)

    # add j+ and j/e
    for js in jp['JobSet']:
        if 'cjob' in js:
            j = 0
            for i in mainc_[js['target_unit']]['jobsets']:
                j += 1
                if i == js['cjob']:
                    try:
                        jname = loc[js['job']]['name']
                        if js['iname'] in glc:
                            if 'tierlist' in units[js['target_unit']] and units[js['target_unit']]['tierlist']['jc'] != "":
                                units[js['target_unit']]['tierlist']['jc ' +
                                                                     str(j)] = units[js['target_unit']]['tierlist']['jc']
                                del units[js['target_unit']]['tierlist']['jc']
                        else:
                            jname += 'ᴶ'

                        if 'short des' in loc[js['job']] and len(loc[js['job']]['short des']) > 1:
                            jname += '\n['+loc[js['job']]['short des']+']'

                    except:  # job:e not found, trying to create a name
                        je = (re_job.search(js['job']))

                        jem = je.group(2) if (je.group(
                            1)+'_'+je.group(2)) not in loc else loc[je.group(1)+'_'+je.group(2)]['name']
                        jname = (jem + ': ' + je.group(3)
                                 ).replace('_', ' ').title()

                        if jname not in jobe:
                            jobe[js['job']] = {'generic': (jname), 'name': ""}
                            print(jname)

                    units[js['target_unit']]['jc '+str(j)] = jname
                    break


    # add jp tag
    for u in units:
        if u not in glc:
            units[u]['name'] += 'ᴶ'

    # save to out
    path = cPath()+'\\out\\'
    saveAsJSON(path+'units.json', units)

    # GSSUpload(units,'units')


# code ~~~~~~~~~~~~~~~~~~~~~~~~~~++++++++++++++++
Units()
