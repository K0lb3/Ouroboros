from MainFunctions import *   

def job_stats(job,main):
    global ParamTypes

    if type(job) == 'string':
        job=main[job]
    stats={
        'Move':job['jmov'],
        'Jump':job['jjmp']
    }
    unc={}

    #from rank
    used=['type','vmax']
    for r in job['ranks']:
        for i in range(1,7):
            item=main[r['eqid'+str(i)]]
            if 'skill' not in item:
                continue
            buff=main[main[item['skill']]['t_buff']]
            for t in range(1,9):
                for u in used:
                    if (u+'0'+str(t)) in buff:
                        buff[u+str(t)]=buff[u+'0'+str(t)]
                try:
                    btyp  = buff['type'+str(t)]
                    bmax  = buff['vmax'+str(t)]
                    try:                
                        unc[btyp]+=bmax
                    except:
                        unc[btyp]=bmax
                except KeyError:
                    break

    for typ in unc:
        stats[ParamTypes[str(typ)]]=unc[typ]

    return(stats)

def main():  
    #load files from work dir\res
    [gl, jp, wyte]=loadFiles(['MasterParam.json','MasterParamJP.json','wytesong.json'])
    loc=Translation()
    #compendium data
    Fan = FanTranslatedNames(wyte, jp, loc)
    wunit={}
    for unit in wyte['Units']:
        wunit[unit['Name']]=unit
    
    #convert master parameters
    glc=convertMaster(gl)
    jpc=convertMaster(jp)
    jpS=json.dumps(jp)    

    #real code
    jobs={}
    
    for unit in jp['Unit']:
        if unit['ai'] != "AI_PLAYER":
            print('End at: ',unit['iname'])
            break         

        #add jobs
        j=1
        if 'jobsets' in unit:
            for i in unit['jobsets']:
                job = jpc[i]['job']
                if job not in jobs:
                    jobs[job]={
                        'iname':job,
                        'name': loc[job]['NAME'],
                        'japan': "",
                        'kanji': jpc[job]['name'],
                        'short description': "" if 'short des' not in loc[job] else loc[job]['short des'],
                        'long description': "" if 'long des' not in loc[job] else loc[job]['long des'],
                        'icon': 'http://cdn.alchemistcodedb.com/images/jobs/icons/'+jpc[job]['mdl']+'.png',
                        'token': 'http://cdn.alchemistcodedb.com/images/items/icons/'+jpc[jpc[job]['ranks'][0]['eqid1']]['icon']+'.png',
                        'origin': loc[jpc[job]['origin']]['NAME'] if 'origin' in jpc[job] else "",
                        'units':[],
                        'jobe':[],
                        'main':"",
                        'sub':"",
                        'reactives':"",
                        'passives':"",
                        'modifiers': {
                            'HP': jpc[job]["hp"],
                            'Max Jewels': jpc[job]["mp"],
                            'Initial Jewels': 100+jpc[job]["inimp"],
                            'PATK': jpc[job]["atk"],
                            'PDEF': jpc[job]["def"],
                            'MATK': jpc[job]["mag"],
                            'MDEF':jpc[job]["mnd"],
                            'DEX':  jpc[job]["dex"],
                            'AGI': jpc[job]["spd"],
                            'LUCK': jpc[job]["luk"],
                            'CRIT': jpc[job]["cri"],
                	        'AVOID': jpc[job]["avoid"],            
                            #"type": 1, 
                        },
                        'stats':job_stats(jpc[job],jpc),
                        'formula': dmg_formula(jpc[jpc[jpc[job]['atkskl']]['weapon']]),
                        }
                    try:
                        jobs[job]['japan']=wunit[Fan[unit['iname']]['inofficial2']]['Job'+str(j)]
                    except KeyError:
                        pass
                jobs[job]['units'].append(name_collab(unit['iname'],loc)[0])
                j+=1
                    
    #add j+ and j/e    
    for js in jp['JobSet']:
        if 'cjob' in js:            
            j=0
            for i in jpc[js['target_unit']]['jobsets']:
                j+=1
                if i == js['cjob']:
                    break
                    
            job=js['job']
            if job not in jobs:
                    icon= 'http://cdn.alchemistcodedb.com/images/jobs/icons/'
                    if 'ac2d' in jpc[job] and len(jpc[job]['ac2d']) > len(jpc[job]['mdl']):
                            icon+= jpc[job]['ac2d']+'.png'
                    else:
                            icon+= jpc[job]['mdl']+'.png'

                    jobs[job]={
                        'iname':job,
                        'name': "" if job not in loc else loc[job]['NAME'],
                        'japan':  "",
                        'kanji': jpc[job]['name'],
                        'short description': "" if 'short des' not in loc[job] else loc[job]['short des'],
                        'long description': "" if 'long des' not in loc[job] else loc[job]['long des'],
                        'icon': icon,
                        'token': 'http://cdn.alchemistcodedb.com/images/items/icons/'+jpc[jpc[job]['ranks'][0]['eqid1']]['icon']+'.png',
                        'origin': loc[jpc[job]['origin']]['NAME'] if 'origin' in jpc[job] else "",
                        'units':[],
                        'jobe':[],
                        'main':"",
                        'sub':"",
                        'reactives':"",
                        'passives':"",
                        'stats':job_stats(jpc[job],jpc),
                        'modifiers': {
                            'HP': jpc[job]["hp"],
                            'Max Jewels': jpc[job]["mp"],
                            'Initial Jewels': 100+jpc[job]["inimp"],
                            'PATK': jpc[job]["atk"],
                            'PDEF': jpc[job]["def"],
                            'MATK': jpc[job]["mag"],
                            'MDEF':jpc[job]["mnd"],
                            'DEX':  jpc[job]["dex"],
                            'AGI': jpc[job]["spd"],
                            'LUCK': jpc[job]["luk"],
                            'CRIT': jpc[job]["cri"],
                	        'AVOID': jpc[job]["avoid"],            
                            #"type": 1, 
                        },
                        'formula': dmg_formula(jpc[jpc[jpc[job]['atkskl']]['weapon']]),
                        }
                    try:
                        jobs[job]['japan']=wunit[Fan[js['target_unit']]['inofficial2']]['JC'+str(j)]
                    except KeyError:
                        pass

            else:
                if jobs[job]['japan'] == "N/A":
                    jobs[job]['japan'] = wunit[Fan[js['target_unit']]['inofficial2']]['JC'+str(j)]

            jobs[job]['units'].append(name_collab(js['target_unit'],loc)[0])
            if jobs[job]['name'] not in jobs[js['ljob1']]['jobe']:
                jobs[js['ljob1']]['jobe'].append(jobs[job]['name'])

    export={}
    for j in jobs:
        job=jobs[j]
        jobs[j]['inputs']=[]
        #job +
        if '+' in job['name']:
            #unit name
            jobs[j]['inputs'].append(job['units'][0])
            if job['units'][0] in export:
                jobs[export[job['units'][0]]]['inputs'].remove(job['units'][0])
            export[job['units'][0]]=j
            #unit + job
            jobs[j]['inputs'].append(job['units'][0] + ' ' + job['name'])
            #job name
            if job['name'] not in export:
                jobs[j]['inputs'].append(job['name'])
                export[job['name']]=j
            else:
                jobs[export[job['name']]]['inputs'].remove(job['name'])
            continue
    
        #unique job (more or less, Zeke HC, MC first jobs)
        if len(job['units'])==1:
            #unit name
            if job['units'][0] not in export:
                jobs[j]['inputs'].append(job['units'][0])
                export[job['units'][0]]=j
            #unit + job
            jobs[j]['inputs'].append(job['units'][0] + ' ' + job['name'])
            #job name
            if job['name'] not in export:
                jobs[j]['inputs'].append(job['name'])
                export[job['name']]=j
            continue
        
        #common job
        jobs[j]['inputs'].append(job['name'])
        if job['name'] in export:
            jobs[export[job['name']]]['inputs'].remove(job['name'])
        export[job['name']]=j

    #save to out
    path=cPath()+'\\out\\'
    saveAsJSON(path+'jobs.json',jobs)
        
    #GSSUpload(jobs,'Job1','1sAS44oeV_WBqZSireDT--73zmEVtCriG_RpUBu5jq5s')

 
# code ~~~~~~~~~~~~~~~~~~~~~~~~~~++++++++++++++++
main()