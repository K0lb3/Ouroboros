import sys
import io
import string
import os
import json
import re
import jellyfish

ReBr=re.compile(r'(\s+)?(\n)?(\[|\『|\().*')

def loadFiles(files):
    ret=[]
    dir=os.path.dirname(os.path.realpath(__file__))+'\\res'
    
    for file in files:
        path = dir+'\\' + file
        if not os.path.exists(path):
            print(path, " was not found.")
            continue
        
        print (file)
        try:
            with open(path, "rt",encoding='utf8') as f:
                ret.append(json.loads(f.read()))  
        except ValueError:
            with open(path, "rt",encoding='utf-8-sig') as f:
                ret.append(json.loads(f.read())) 
            
    
    return ret
    
def saveAsJSON(name, var):
    with open(name, "wb") as f:
      f.write(json.dumps(var, indent=4, ensure_ascii=False).encode('utf8'))
    
    #try:
    #    with codecs.open(name, 'w', encoding='utf-8') as f:
    #        json.dump(var, f, indent=4, ensure_ascii=False)
    #except ValueError:
    #    print('Codecs Error, Retry')
    #    saveAsJSON(name,var)
    return 1    
    
def convertMaster(master):
    ma={}
    for main in master:
        for sub in master[main]:
            try:
                ma[sub['iname']]=sub
            except:
                continue
    return ma

def similarity(ori,inp):
    return (jellyfish.jaro_winkler(inp,ori))

def name_collab(iname,loc):
    iname = iname[6:]
    
    #collab 
    collabs={
        'FATE': ['Fate' ,'Fate/Stay Night [UBW]'],
        'FA'  : ['FA'   ,'Fullmetal Alchemist'],
        'RH'  : ['RH'   ,'Radiant Historia'],
        'POK' : ['POTK' ,'Phantom Of The Kill'],
        'S'   : ['SN'   ,'Shinobi Nightmare'],
        'FF15': ['FF'   ,'Final Fantasy 15'],
        'DIS' : ['DIS'  ,'Disgea'],
        'SEK' : ['EO'   ,'Etrian Odyssey'],
        'SQ'  : ['EMD'  ,'Etrian Mystery Dungeon'],
        'APR' : [''     ,'April Fool'],
        'CRY' : ['CR'   ,'Crystal Re:Union']
        }
        
    collab=""
    collab_short=''    
    for c in collabs:
        if c+'_' == iname[:len(c)+1]:
            collab = collabs[c][1]
            collab_short = collabs[c][0]
            iname= iname[len(c)+1:]
            break
            
    #unknown name
    try:
        name = loc['UN_V2_'+iname]['NAME']
    except:
        name = iname.replace('_',' ')
        prefix={
            'BLK' : 'Black Killer',
            'L'   : 'Little',
            }
        
        for p in prefix:
            if  p+' ' == name[:len(p)+1]:
                name = prefix[p] + ' ' + name[len(p)+1:]
        
        name=name.title()
        
    return [name,collab,collab_short]


def FanTranslatedNames():
    [wyte,master,loc]=loadFiles(['wytesong.json','MasterParamJP.json','LocalizedMasterParam.json'])
    wunit={}
    for unit in wyte['Units']:
        wunit[unit['Name JPN']]=unit


    found={}
    none={}
    for unit in master['Unit']:
        if unit['ai'] == "AI_PLAYER":
            [name,collab,collab_short]=name_collab(unit['iname'],loc)
            try:
                found[unit['iname']]={
                    'iname':unit['iname'], 
                    'official': loc[unit['iname']]['NAME'] if unit['iname'] in loc else name,
                    'generated': name,
                    'inofficial': ReBr.sub('',wunit[unit['name']]['Name']).rstrip(' '),
                    'collab': collab,
                    'collab_short':collab_short,
                    'japanese': unit['name']
                    }
                del wunit[unit['name']]
            except:
                none[unit['iname']]={
                    'iname':unit['iname'], 
                    'official': loc[unit['iname']]['NAME'] if unit['iname'] in loc else name,
                    'generated': name,
                    'inofficial': "",
                    'collab': collab,
                    'collab_short':collab_short,
                    'japanese': unit['name']
                    }
        else:
            break

    delete=[]

    for unit in none:
        try:
            for left in wunit:
                if  (similarity(none[unit]['official'],ReBr.sub('',wunit[left]['Name']).title())>=0.8 or
                    similarity(none[unit]['official'],wunit[left]['Romanji'].title())>=0.8 or
                    similarity(none[unit]['japanese'],left)>=0.8):

                    found[none[unit]['iname']]=none[unit]
                    found[none[unit]['iname']].update({
                        'inofficial': ReBr.sub('',wunit[left]['Name']).rstrip(' '),
                        'japanese2': wunit[left]['Name JPN']
                    })
                    del wunit[left]
                    delete.append(unit)
                    break
                
        except RuntimeError:
            pass

    for i in delete:
        try:
            del none[i]
        except: 
            print(i)




    saveAsJSON('unit_found.json',found)
    saveAsJSON('unit_notfound.json',none)
    saveAsJSON('unit_notfoundJP.json',wunit)
            
    found.update(none)
    db={}
    names=['official','generated','inofficial']
    for unit1 in found:
        unit=found[unit1]
        saved=0
        for name in names:
            if unit[name]!="":
                if unit[name] not in db:
                    db[unit[name]]=unit['iname']
                    saved=1

                if unit['collab'] != "":
                    db[unit['collab_short'] + ' ' + unit[name]]=unit['iname']
                    db[unit[name] + ' ' + unit['collab_short']]=unit['iname']
                    saved=1
        
        if not saved:
            print (unit)

    path=os.path.dirname(os.path.realpath(__file__))+'\\out\\'
    saveAsJSON(path+'unit_namelist.json',db)    







#code
FanTranslatedNames()
