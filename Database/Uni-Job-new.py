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

#I think this is how scaling works
def tac_scale(num,scale):
    res = num + num*(scale/100)
    return math.floor(res) if num > 0 else math.ceil(res)

def main():
    main = loadFiles(['MasterParam.json'])[0]
    mainc = convertMaster(main)
    unit=mainc['UN_V2_GL_SIEG']
    unitLevel=85

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

    unit['stats']={}
    for raw,con in stats.items():
        unit['stats'][con] = unit[raw] + math.floor((unit['m'+raw]-unit[raw]) / 99 * (unitLevel - 1))
    
 # add jobs

    jobs=[]
    if 'jobsets' in unit:
        for i in unit['jobsets']:
            job = mainc[mainc[i]['job']]
            mod = job['ranks'][-1]

            jobs.append({
                'stats': job_stats(job,mainc),
                'jm': buff(mainc[mainc[job['master']]['t_buff']],1,2,True),#use bmin
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
                    }
                })

    #unit 
    total_jm={}
    for j in jobs:
        for b,val in j['jm'].items():
            if b not in total_jm:
                total_jm[b]=0
            total_jm[b]+=val
    print(total_jm)

    for i in range(0,3):
        j_stats={}
        for key,stat in unit['stats'].items():
            try:
                j_stats[key]=jobs[i]['stats'][key]+tac_scale( stat, jobs[i]['modifiers'][key] )
            except:
                pass

        for key,val in total_jm.items():
            j_stats[key]=tac_scale(j_stats[key],100*val)
        print(j_stats)
main()