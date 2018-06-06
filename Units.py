from MainFunctions import *
    
def master_ability(unit,cMaster,loc):
    skill = cMaster[cMaster[unit['ability']]['skl1']]

    text= (loc[skill['iname']]['NAME']+"\n") if skill['iname'] in loc else ""
    
    if 't_buff' in skill:
        text+=buff(cMaster[skill['t_buff']],2,2).replace('%','')

    if 't_cond' in skill:
        if 't_buff' in skill:
            text+= ' & '
        text+=condition(cMaster[skill['t_cond']],2,2).replace('[100%]','')
    
    return text

    
def main():
    global ParamTypes
    global birth
    
    jobe={} #missing job evolves
    mjob={} #missing jobs

    
    #load files from work dir\res
    [gl, jp, quest_jp, loc, translation,lore, wyte]=loadFiles(['MasterParam.json','MasterParamJP.json','QuestParamJP.json','LocalizedMasterParam.json','Translations.json','unit.json','wytesong.json'])
    #load drop list
    [drops]=loadOut(['drops.json'])
    pieces={}
    for drop in drops:
        drop = drops[drop]
        if drop['iname'][:6]=='IT_PI_' and len(drop['story'])>0:
            pieces[drop['iname']]=[]
            for mission in drop['story']:
                pieces[drop['iname']].append('GL: '+mission['name'])

            
    #patch jp unto drop list
    for quest in quest_jp['quests']:
        if 'pieces' in quest and quest['iname'][:9]=="QE_ST_HA_":  
            for piece in quest['pieces']:
                if piece not in pieces:
                    pieces[piece]=[]
                #"title": "【ハード】１章３話1-10",
                name='['+str(quest['pt'])+' AP] '+ quest['title'].replace('【ハード】','[Hard] Ch ').replace('章', ': Ep ').replace('話',' [') + ']'
                name=name.replace('１','1').replace('２','2').replace('３','3').replace('４','4').replace('５','5')
                #[24 AP] [Hard] Ch 1: Ep 3 [1-10]"
                pieces[piece].append('JP: '+name)

    #patching loc
    FAN = FanTranslatedNames(wyte, jp, loc)
    translation=Translation_1()
    for i in translation:
        if i not in loc:
            loc[i]=translation[i]
    
    #convert master parameters
    glc=convertMaster(gl)
    jpc=convertMaster(jp)
    jpS=json.dumps(jp)    

    units={}
    
    for unit in jp['Unit']:
        if unit['ai'] != "AI_PLAYER":
            print('End at: ',unit['iname'])
            break         
        
        iname=unit['iname']
        [u_name,u_collab,u_collab_short] = name_collab(unit['iname'],loc)
        
        units[iname]={
            'iname'     : iname,
            'name'      : u_name,
            'inofficial': FAN[iname]['inofficial'] if iname in FAN else "",
            'gender'    : "♀" if (unit['sex']-1) else "♂" if (unit['sex']) else "/",
            'element'   : element(unit['elem']).title() if 'elem' in unit else "/",
            'country'   : birth[unit['birth']] if 'birth' in unit else "/",
            'collab'    : u_collab,
            'collab short': u_collab_short,
            'rarity'    : rarity(unit['rare'],unit['raremax']),
            'icon'      : 'http://cdn.alchemistcodedb.com/images/units/profiles/' + unit["img"] + ".png",
            'farm'      : pieces[unit['piece']] if ('piece' in unit and unit['piece'] in pieces) else [],
            'leader skill': buff(jpc[jpc[unit['ls6']]['t_buff']],2,2) if ('ls6' in unit) and ('t_buff' in jpc[unit['ls6']]) else "/",
            'master ability': master_ability(unit,jpc,loc) if 'ability' in unit else "",        
            #'shard quests': drops[unit['piece']],
            'link': 'http://www.alchemistcodedb.com/'+ (''if iname in glc else 'jp/') + 'unit/' + iname.replace('UN_V2_', "").replace('_', "-").lower(),
            'job 1' : "",
            'job 2' : "",
            'job 3' : "",
            'jc 1' : "",
            'jc 2' : "",
            'jc 3' : "",
            'inputs': []
            }
          
        #add lore
        if iname in lore:
            units[iname].update(lore[iname])
         
        #add artworks
        art_link='http://cdn.alchemistcodedb.com/images/units/artworks/'
        units[iname]['artworks']=[({'name':"default",'full':art_link+unit['img']+'.png','closeup':art_link+unit['img']+'-closeup.png'})]
        if 'skins' in unit:
            for s in unit['skins']:
                if jpc[s]['asset'] != 'unique':
                    units[iname]['artworks'].append({
                        'name':     loc[s]['NAME'] if s in loc else s[(7+len(unit['img'])):].replace('-',' ').title(),
                        'full':     art_link+unit['img']+ '_' + jpc[s]['asset']+'.png',
                        'closeup':  art_link+unit['img']+ '_' + jpc[s]['asset']+'-closeup.png',
                        })
            
        if (jpS.find('AF_SK_' + unit['img'].upper() + '_BABEL')+1):
            units[iname]['artworks'].append({
                'name':     'Kaigan',
                'full':     art_link+unit['img']+ '_' + 'babel'+'.png',
                'closeup':  art_link+unit['img']+ '_' + 'babel'+'-closeup.png',
                })
                
        #add jobs
        j=1
        if 'jobsets' in unit:
            for i in unit['jobsets']:
                job = jpc[i]['job']
                
                if not job in loc:
                    mjob[job]={'unit':iname,'NAME':""}
                    
                units[iname]['job '+str(j)]= job if not job in loc else loc[job]['NAME']
                
                j+=1
                    
    #add j+ and j/e    
    for js in jp['JobSet']:
        if 'cjob' in js:
            j=0
            for i in jpc[js['target_unit']]['jobsets']:
                j+=1
                if i == js['cjob']:
                    try:
                        units[js['target_unit']]['jc '+str(j)]=loc[js['job']]['NAME']
                        if 'description' in loc[js['job']] and len(loc[js['job']]['description'])>1:
                            units[js['target_unit']]['jc '+str(j)]+='\n['+loc[js['job']]['description']+']'
                    
                    except: # job:e not found, trying to create a name
                        je=(re_job.search(js['job']))
                        
                        jem= je.group(2) if (je.group(1)+'_'+je.group(2)) not in loc else loc[je.group(1)+'_'+je.group(2)]['NAME']
                        units[js['target_unit']]['jc '+str(j)] = (jem + ': ' + je.group(3)).replace('_',' ').title()
                        
                        if units[js['target_unit']]['jc '+str(j)] not in jobe:
                            jobe[js['job']]={'generic':(units[js['target_unit']]['jc '+str(j)]),'NAME':""}
                            print (units[js['target_unit']]['jc '+str(j)])
                    
                    break

    #add collabs
    #Gilg, Yomi, Selena,Vargas, eo1

    #add weapon abilities
        
    export={}
    for u in units:
        unit=units[u]
        if unit['name'] not in export:
            units[u]['inputs'].append(unit['name'])
            export[unit['name']]=u
        if len(unit['inofficial'])>1 and unit['inofficial'] not in export:
            units[u]['inputs'].append(unit['inofficial'])
            export[unit['inofficial']]=u

        if len(unit['collab short'])>1:
            units[u]['inputs'].append(unit['name'] + ' ' + unit['collab short'])
            units[u]['inputs'].append(unit['collab short'] + ' ' + unit['name'])

    #save to out
    path=cPath()+'\\out\\'
    saveAsJSON(path+'units.json',units)
      
    GSSUpload(units,'units')

 
# code ~~~~~~~~~~~~~~~~~~~~~~~~~~++++++++++++++++
main()