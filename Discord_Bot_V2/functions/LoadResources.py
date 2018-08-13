import os
import json

def LoadResources():
    mypath = os.path.dirname(os.path.realpath(__file__)).replace('functions','resources')+'\\'
    files = os.listdir(mypath)

    ret={}

    for f in files:
        print(f)
        with open(mypath+f, "rt", encoding='utf8') as file:
            ret[f[:-5].title()]=json.loads(file.read())
            
    return ret