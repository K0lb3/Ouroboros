from MainFunctions import *

def gear():
    [gl, jp,lore]=loadFiles(['MasterParam.json','MasterParamJP.json','unit.json'])
    loc = Translation()
    gears = gl['Artifact']
    cmaster = convertMaster(gl)
    cjp= convertMaster(jp)
    export={}

    gear={}
    for g in gears:
        if not 'icon' in g or not 'equip5' in g:
            continue

        iname = g['iname']
        c={'iname':iname,'type': ['None','Weapon','Armor','Accessory'][g['type']]} #current
        if iname in loc:
            for i in loc[iname]:
                c[i.lower()]=loc[iname][i]
        else:
            try:
                c.update({
                    'name'      :   g['name'],
                    'expression':   g['expr'],
                    'flavor'    :   g['flavor']
                })
            except KeyError:
                c.update({'name':"/",'expression':"/",'flavor':'/'})

        c.update({
            'tag':      GEAR_TAG[g['tag']],
            'rarity':   rarity(g['rini'],g['rmax']),
            'link':     'http://www.alchemistcodedb.com/gear/'+iname.replace('_','-').lower(),
            'icon':     'http://cdn.alchemistcodedb.com/images/items/icons/'+g['icon']+'.png',
            'inputs':   []
            })

        c['ability']=[]
        if 'abils' in g:
            for a in g['abils']:
                ability=cmaster[a]
                restriction=""
                if 'units' in ability:
                    aunits=[]
                    for u in ability['units']:
                        uname=name_collab(u,loc)[0]
                        aunits.append(uname)
                        if uname not in export:
                            c['inputs'].append(uname)
                            export[uname]=c['iname']
                        else:
                            c['inputs'].append(uname+'2')

                    restriction+=", ".join(aunits)

                if 'sex' in ability:
                    restriction+= 'female' if (ability['sex']-1) else 'male'

                locA=loc[ability['skl1']] if ability['skl1'] in loc else {'NAME':cjp[ability['skl1']]['name'],'EXPR':cjp[ability['skl1']]['name']}
                locA.update({'restriction':restriction,'iname':ability['skl1']})

                c['ability'].append(locA)

        c['stats']={}
        for r in range(g['rini']+1,g['rmax']+2):
            c['stats'][str(r)]=buff(cmaster[cmaster[g['equip'+str(r)]]['t_buff']],(r+1)*5,30)

        c['atk_buff']={}
        for r in range(g['rini']+1,g['rmax']+2):
            if ('attack'+str(r)) in g:
                skill=cmaster[g['attack'+str(r)]]
                c['atk_buff'][str(r)]=""
                if 's_buff' in skill:
                    c['atk_buff'][str(r)]+='Self: '+buff(cmaster[skill['s_buff']],(r+1)*5,30)
                elif 't_buff' in skill:
                    c['atk_buff'][str(r)]+='Target: '+buff(cmaster[skill['t_buff']],(r+1)*5,30)
                elif 's_cond' in skill:
                    c['atk_buff'][str(r)]+='Self: '+condition(cmaster[skill['s_cond']],(r+1)*5,30)
                elif 't_cond' in skill:
                    c['atk_buff'][str(r)]+='Target: '+condition(cmaster[skill['t_cond']],(r+1)*5,30)
                else:
                    print (skill)

       
       
       
        if c['name'] not in export:
            c['inputs'].append(c['name'])
            export[c['name']]=c['iname']
        else:
            c['inputs'].append(c['name']+'2')

        
       
        gear[c['iname']]=c

    return(gear)

#code
path=cPath()+'\\out\\'
gears=gear()
saveAsJSON(path+'gear.json',gears)  
#GSSUpload(gears,"gear") 