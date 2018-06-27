import os
import json

mypath = os.path.dirname(os.path.realpath(__file__))+'\\'
files = os.listdir(mypath)

with open(mypath+'LocalizedMasterParam.json', "rt", encoding='utf8') as f:
    loc = json.loads(f.read())

tcount=0
total=[]
onefile={'pull':[],'item':{}}
for f in files:
    if 'Zeni Gacha' not in f:
        continue

    print(f)

    with open(mypath+f, "rt", encoding='utf8') as f:
        log = json.loads(f.read())

    ##adding to onefile
    onefile['pull'].append(log['pull'])

    tcount+= len(log['pull'])

    total={}
    for item,count in log['item'].items():
        if item not in total:
            total[item]=0
        total[item]+=count

onefile['item']=total

export=[]
for item,count in total.items():
    export.append((loc[item]['NAME'],count/tcount*100))

def getKey(item):
    return item[1]
export=sorted(export, key=getKey,reverse=True)

text=""
for item,count in export:
    text+= '{count}%\t{item}\n'.format(count=count,item=item)

print(text) 

with open('Zeni Gacha - total.json', "wb") as f:
    f.write(json.dumps(onefile, indent=4, ensure_ascii=False).encode('utf8'))








