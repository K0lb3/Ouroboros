from MainFunctions import *

[units] = loadOut(['units.json'])
bday={
    'Jan':[],
    'Feb':[],
    'Mar':[],
    'Apr':[],
    'May':[],
    'Jun':[],
    'Jul':[],
    'Aug':[],
    'Sep':[],
    'Okt':[],
    'Nov':[],
    'Dec':[],
}
for mon in bday:
    for i in range(1,32):
        bday[mon].append([])

for key, unit in units.items():
    try:
        [month, day] = unit['BIRTH'].split(' ')
        bday[month][int(day)].append(unit['name'])
    except:
        pass

text=""
for month in bday:
    text+=month+'\n'
    i=-1
    for day in bday[month]:
        i+=1
        if len(day)>0:
            text+='\t'+str(i)+': '+', '.join(day)+'\n'
    text+='\n'

with open('bday.txt', "wt",encoding='utf8') as f:
    f.write(text)
