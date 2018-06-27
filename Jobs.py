from MainFunctions import *


def job_stats(job, main):
    global ParamTypes

    if type(job) == 'string':
        job = main[job]
    stats = {
        'Move': job['jmov'],
        'Jump': job['jjmp']
    }
    unc = {}

    # from rank
    used = ['type', 'vini', 'vmax']
    for j in range(0, len(job['ranks'])):
        r = job['ranks'][j]
        for i in range(1, 7):
            item = main[r['eqid'+str(i)]]
            if 'skill' not in item:
                continue
            buff = main[main[item['skill']]['t_buff']]
            for t in range(1, 9):
                for u in used:
                    if (u+'0'+str(t)) in buff:
                        buff[u+str(t)] = buff[u+'0'+str(t)]
                try:
                    btyp = buff['type'+str(t)]
                    if j == len(job['ranks'])-1:
                        bmax = buff['vmax'+str(t)]
                    else:
                        bmax = buff['vini'+str(t)]
                    try:
                        unc[btyp] += bmax
                    except:
                        unc[btyp] = bmax
                except KeyError:
                    break

    for typ in unc:
        stats[ParamTypes[str(typ)]] = unc[typ]

    return(stats)


def job_create(job, glc, jpc, loc):
    # decide which main to use
    if job in glc:
        mainc = glc
        region = 'GL'
    else:
        mainc = jpc
        region = 'JP'
    j = mainc[job]
    # small modifier fix
    mod = j
    if 'hp' not in mod:
        mod = mod['ranks'][-1]

    def locFunc(iname):
        try:
            return loc[job]['name']
        except:
            print(job)
            jb = job.split('_')
            base = jb[0]+'_'+jb[1]
            if base in loc:
                return loc[base]['name']+': '+' '.join(jb[2:])
            else:
                return job[3:].replace('_', ' ').title()
    
    def ability(inp,name=""):
        if type(inp)==str:
            iname=inp
            abil=mainc[iname]
            ability={
                'iname':iname,
                'name': loc[iname]['name'] if iname in loc else abil['name'],
                'expr': loc[iname]['expr'] if iname in loc else abil['expr'],
                'skills':[]
                }
        elif type(inp)==list:
            abil={}
            i=1
            for abils in inp:
                abil['skl'+str(i)]=mainc[abils]['skl1']
                i+=1
            ability={
                'iname':'',
                'name': name,
                'expr': '',
                'skills':[]
                }            
        

        i=1
        while 'skl'+str(i) in abil:
            skli=abil['skl'+str(i)]
            skl=mainc[skli]
            try:
                copy=loc[skli]
                ability['skills'].append({
                    'name': copy['name'],
                    'expr': copy['expr'],
                    #'lv':   abil['lv'+str(i)],
                    #'effect': skl["eff_val_max"],
                    })
            except:
                ability['skills'].append({
                    'name': skl['name'],
                    'expr': skl['expr'],
                    #'lv':   abil['lv'+str(i)],
                    #'effect': skl["eff_val_max"],
                    })
            i+=1
        return(ability)

    # get abilities:
    reactives=[]
    passives=[]
    for r in j['ranks']:
        i=1
        while 'learn'+str(i) in r:
            abil=r['learn'+str(i)]
            slot=mainc[abil]['slot']
            if slot==0:   #Action, ~ from EAbilitySlot
                sub=abil
            elif slot==2: #Reaction,
                reactives.append(abil)
            elif slot==1: #Support,
                passives.append(abil)
            i+=1

    return {
        'iname': job,
        'name': locFunc(job),
        'japan': "",
        'kanji': j['name'],
        'short description': loc[job]['short des'] if job in loc and 'short des' in loc[job] else "",
        'long description':  loc[job]['long des'] if job in loc and 'long des' in loc[job] else "",
        'icon': 'http://cdn.alchemistcodedb.com/images/jobs/icons/'+j['mdl']+'.png',
        'token': 'http://cdn.alchemistcodedb.com/images/items/icons/'+mainc[j['ranks'][0]['eqid1']]['icon']+'.png',
        'origin': loc[j['origin']]['name'] if 'origin' in j else "",
        'units': [],
        'jobe': [],
        'main': ability(j['fixabl']),
        'sub': ability(sub),
        'reactives': ability(reactives,"Reactives"),
        'passives': ability(passives,"Passives"),
        'weapon': GEAR_TAG[jpc[j['artifact']]['tag']],
        'modifiers': {
            'HP':   mod["hp"],
            'Max Jewels': mod["mp"],
            'Initial Jewels': 100+mod["inimp"],
            'PATK': mod["atk"],
            'PDEF': mod["def"],
            'MATK': mod["mag"],
            'MDEF': mod["mnd"],
            'DEX':  mod["dex"],
            'AGI':  mod["spd"],
            'LUCK': mod["luk"],
            'CRIT': mod["cri"],
            'AVOID': mod["avoid"],
        },
        'job master buff': buff(mainc[mainc[j['master']]['t_buff']],2,2,True),
        'stats': job_stats(j, mainc),
        'formula': dmg_formula(mainc[mainc[j['atkskl']]['weapon']]),
        'link': "",
        'region': region,
    }


def jobs():
    # load files from work dir\res
    [gl, jp, wyte] = loadFiles(
        ['MasterParam.json', 'MasterParamJP.json', 'wytesong.json'])
    loc = Translation()
    # compendium data
    Fan = FanTranslatedNames(wyte, jp, loc)
    wunit = {}
    for unit in wyte['Units']:
        wunit[unit['Name']] = unit

    # convert master parameters
    glc = convertMaster(gl)
    jpc = convertMaster(jp)
    jpS = json.dumps(jp)

    mainc=convertMaster(gl)
    mainc.update(jpc)    

    #create unit list
    unit_list={}
    for unit in gl['Unit']:
        if 'UN_V2_' not in unit['iname']:
            print('End at: ', unit['iname'])
            break
        if unit['ai'] != "AI_PLAYER":
            print('End at: ', unit['iname'])
            break
        unit_list[unit['iname']]=unit

    for unit in jp['Unit']:
        if unit['ai'] != "AI_PLAYER":
            print('End at: ', unit['iname'])
            break
        if unit['iname'] in unit_list:
            unit_list[unit['iname']].update(unit)
        else:
            unit.update({'region':'JP'})
            unit_list[unit['iname']]=unit

    # code
    def create_link(unit_in, job_na):
        return 'http://www.alchemistcodedb.com/unit/'+unit_in[6:].replace('_', '').lower()+'#'+job_na.replace(' ', '-').replace('+', '-plus').lower()

    jobs = {}

    for uiname,unit in unit_list.items():
        # add jobs
        j = 1
        l = uiname in glc and 'vce' in glc[uiname]
        mainc[uiname]['l'] = l

        if 'jobsets' in unit:
            for i in unit['jobsets']:
                job = mainc[i]['job']
                if job not in jobs:
                    # create job from data
                    jobs[job] = job_create(job, glc, mainc, loc)

                    # add Fan translation
                    try:
                        jobs[job]['japan'] = wunit[Fan[uiname]
                                                   ['inofficial2']]['Job'+str(j)]
                    except KeyError:
                        pass
                # add unit to list
                n_c = name_collab(uiname, loc)
                jobs[job]['units'].append((n_c[0]+' '+n_c[2]).rstrip(' '))

                # add link
                if len(jobs[job]['link']) < 30:
                    if l and jobs[job]['region'] == 'GL':
                        jobs[job]['link'] = create_link(
                            uiname, jobs[job]['name'])
                    else:
                        jobs[job]['link'] = uiname

                j += 1

    # add j+ and j/e
    for js in jp['JobSet']:
        if 'cjob' in js:
            j = 0
            for i in mainc[js['target_unit']]['jobsets']:
                j += 1
                if i == js['cjob']:
                    break

            job = js['job']
            if job not in jobs:
                # create job from data
                jobs[job] = job_create(job, glc, mainc, loc)

                # fix icon'
                icon = 'http://cdn.alchemistcodedb.com/images/jobs/icons/'
                if 'ac2d' in mainc[job] and len(mainc[job]['ac2d']) > len(mainc[job]['mdl']):
                    icon += mainc[job]['ac2d']+'.png'
                else:
                    icon += mainc[job]['mdl']+'.png'
                jobs[job]['icon'] = icon

                # add link
                if len(jobs[job]['link']) < 30:
                    if mainc[js['target_unit']]['l'] and jobs[job]['region'] == 'GL':
                        jobs[job]['link'] = create_link(
                            js['target_unit'], jobs[job]['name'])
                    else:
                        jobs[job]['link'] = js['target_unit']

                # add fan translation
                try:
                    jobs[job]['japan'] = wunit[Fan[js['target_unit']]
                                               ['inofficial2']]['JC'+str(j)]
                except KeyError:
                    pass

            else:
                if jobs[job]['japan'] == "N/A":
                    jobs[job]['japan'] = wunit[Fan[js['target_unit']]
                                               ['inofficial2']]['JC'+str(j)]

            # add units to list
            n_c = name_collab(js['target_unit'], loc)
            jobs[job]['units'].append((n_c[0]+' '+n_c[2]).rstrip(' '))

            # add je to basic job
            if jobs[job]['name'] not in jobs[js['ljob1']]['jobe']:
                jobs[js['ljob1']]['jobe'].append(jobs[job]['name'])

    # add missing links
    for j in jobs:
        if len(jobs[j]['link']) < 30:
            jobs[j]['link'] = 'http://www.alchemistcodedb.com/jp/unit/' + \
                jobs[j]['link'][6:].replace(
                    '_', '-').lower()+'#'+jobs[j]['kanji'].replace(' ', '-').lower()

    # add inputs
    export = {}
    for j in jobs:
        job = jobs[j]
        jobs[j]['inputs'] = []
        # job +
        if '+' in job['name']:
            # unit name
            jobs[j]['inputs'].append(job['units'][0])
            if job['units'][0] in export:
                jobs[export[job['units'][0]]]['inputs'].remove(job['units'][0])
            export[job['units'][0]] = j
            #unit + job
            jobs[j]['inputs'].append(job['units'][0] + ' ' + job['name'])
            # job name
            if job['name'] not in export:
                jobs[j]['inputs'].append(job['name'])
                export[job['name']] = j
            else:
                try:
                    jobs[export[job['name']]]['inputs'].remove(job['name'])
                except:
                    pass
            continue

        # unique job (more or less, Zeke HC, MC first jobs)
        if len(job['units']) == 1:
            # unit name
            if job['units'][0] not in export:
                jobs[j]['inputs'].append(job['units'][0])
                export[job['units'][0]] = j
            #unit + job
            jobs[j]['inputs'].append(job['units'][0] + ' ' + job['name'])
            # job name
            if job['name'] not in export:
                jobs[j]['inputs'].append(job['name'])
                export[job['name']] = j
            continue

        # common job
        jobs[j]['inputs'].append(job['name'])
        if job['name'] in export:
            jobs[export[job['name']]]['inputs'].remove(job['name'])
        export[job['name']] = j

    jexport = {}
    for j in jobs:
        # fan translation
        job = jobs[j]
        japan = jobs[j]['japan']
        if len(japan) < 2:
            continue
        if '\n' in japan:
            japan = jobs[j]['japan'][:jobs[j]['japan'].index('\n')]
        if japan not in export:
            # job +
            if '+' in japan:
                #unit + job
                jobs[j]['inputs'].append(job['units'][0] + ' ' + japan)
                # job name
                if japan not in jexport:
                    jobs[j]['inputs'].append(japan)
                    jexport[japan] = j
                else:
                    jobs[jexport[japan]]['inputs'].remove(japan)
                continue

            # unique job (more or less, Zeke HC, MC first jobs)
            if len(job['units']) == 1:
                # unit name
                if job['units'][0] not in jexport:
                    jobs[j]['inputs'].append(job['units'][0])
                    jexport[job['units'][0]] = j
                #unit + job
                jobs[j]['inputs'].append(job['units'][0] + ' ' + japan)
                # job name
                if japan not in jexport:
                    jobs[j]['inputs'].append(japan)
                    jexport[japan] = j
                continue

            # common job
            jobs[j]['inputs'].append(japan)
            if japan in jexport:
                jobs[jexport[japan]]['inputs'].remove(japan)
            jexport[japan] = j

    # add jp tag
    for j in jobs:
        if jobs[j]['region'] == 'JP':
            jobs[j]['name'] += 'ᴶ'
        del jobs[j]['region']
    # save to out
    path = cPath()+'\\out\\'
    saveAsJSON(path+'jobs.json', jobs)

    # GSSUpload(jobs,'Job1','1sAS44oeV_WBqZSireDT--73zmEVtCriG_RpUBu5jq5s')


# code ~~~~~~~~~~~~~~~~~~~~~~~~~~++++++++++++++++
jobs()
