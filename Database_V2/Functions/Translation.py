import json
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

TRANSLATION=FuseTranslations()
print('Loaded: Translations')