import os
import json

def loadParams():
    dir = os.path.dirname(os.path.realpath(__file__))[:-10]+'\\resources\\GameFiles\\'
    fdir=os.listdir(dir)
    print(fdir)
    files={}

    for f in fdir:
        if '.json' in f and 'Param' in f:
            with open(dir+f, "rt", encoding='utf8') as file:
                files[f[-5]]=(json.loads(file.read()))
    return files

global MASTER
MASTER=loadParams()