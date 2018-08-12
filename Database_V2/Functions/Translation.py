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
            unit['Name JPN'] : unit
            for unit in json.loads(f.read())['Units']
        }

    none = {}
    used = []
    for iname,unit in UNITS.items():
        if unit['kanji'] in wunit:
            if iname not in loc:
                unit['name'] = ReBr.sub(
                    '', wunit[unit['kanji']]['Name']).rstrip(' ')
            del wunit[unit['kanji']]
            used.append(unit['name'])
        else:
            if iname not in loc:
                none[iname] = unit
            else:
                used.append(unit['name'])

    for iname,unit in none.items():
        try:
            found=False
            for kanji,left in wunit.items():
                generated=iname[6:].replace('_',' ').title()
                wName=ReBr.sub('', left['Name']).rstrip(' ').title()
                if wName not in used and (similarity(generated, wName) >= 0.8 or
                        similarity(generated, left['Romanji'].title()) >= 0.8 or
                        similarity(unit['kanji'], kanji) >= 0.8 or
                        unit['kanji'] in kanji):

                    unit['name'] =wName
                    found=True
                    del wunit[kanji]
                    break
            if not found:
                print('Not found:',iname)
        except RuntimeError:
            pass



TRANSLATION=FuseTranslations()
print('Loaded: Translations')