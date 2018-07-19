import os
import json

def loadParams():
    dir = os.path.dirname(os.path.realpath(__file__))+'\\resources\\GameFiles\\'

    files={}

    for f in os.listdir(dir):
        if '.json' in f and 'Param' in f:
            with open(dir+f, "rt", encoding='utf8') as f:
                files[f[-5]]=(json.loads(f.read()))
    return files

global MASTER
MASTER=loadParams()