import json
import jellyfish
import re
import os

def loadTranslations():
    translations_path = os.path.dirname(os.path.realpath(__file__)).replace('\\Functions','\\resources\\Translations\\')
    files = os.listdir(translations_path)

    ret={}
    for f in files:
        with open(translations_path+f, "rt", encoding='utf8') as f:
            ret.update(json.loads(f.read()))
    return ret

def loadLoc():
    path = os.path.dirname(os.path.realpath(__file__)).replace('\\Functions','\\resources\\GameFiles\\')
    with open(path+'LocalizedMasterParam.json', "rt", encoding='utf8') as f:
        ret= (json.loads(f.read()))
    with open(path+'sys.json', "rt", encoding='utf8') as f:
        ret.update(json.loads(f.read()))
    return ret

def FuseTranslations():
    loc=loadLoc()
    Translations=loadTranslations()

    for i in Translations:
        if i in loc:
            Translations[i].update(loc[i])
            loc[i]=Translations[i]
        else:
            loc[i] = Translations[i]
    return loc

def wytesong(UNITS):
    loc = TRANSLATION
    def similarity(ori, inp):
        return (jellyfish.levenshtein_distance(inp, ori))

    ReBr = re.compile(r'(\s+)?(\n)?(\[|\『|\().*')
    path = os.path.dirname(os.path.realpath(__file__)).replace('\\Functions','')+'\\resources\\wytesong.json'
    print('wytesong.json')
    with open(path, "rt", encoding='utf8') as f:
        wunit= {
            unit['Name JPN'] : {
                'Name':ReBr.sub('', unit['Name']).rstrip(' ').title(),
                'Original': unit['Name'],
                'Romanji': unit['Romanji']
            }
            for unit in json.loads(f.read())['Units']
        }

    none = {}
    used = []
    # directly found via kanji
    for iname,unit in UNITS.items():
        if unit['kanji'] in wunit:
            if iname not in loc:
                unit['name'] = wunit[unit['kanji']]['Name']
            del wunit[unit['kanji']]
            used.append(unit['name'])
        else:
            if iname not in loc:
                none[iname] = unit
            else:
                used.append(unit['name'])
    
    # searching via laevenstein
    
    for iname,unit in none.items():
        found=False
        backup=None
        for kanji,left in wunit.items():
            if  (similarity(unit['kanji'], kanji) <= 2 or unit['kanji'] in kanji):
                if left['Name'] not in used:
                    unit['name'] = left['Name']
                    found=True
                    del wunit[kanji]
                    break
                else:
                    backup=left['Original']

        if not found:
            if backup:
                unit['name']=backup
            else:
                print('Not found:',iname)


TRANSLATION=FuseTranslations()
print('Loaded: Translations')