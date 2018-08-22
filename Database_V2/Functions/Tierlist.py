import json
import jellyfish
import os.path

def Tierlist(units):
    path = os.path.dirname(os.path.realpath(__file__)).replace('\\Functions','')+'\\resources\\tierlist_gl.json'
    print('tierlist.json')
    with open(path, "rt", encoding='utf8') as f:
        tierlist=json.loads(f.read())['Tier List']
    # convert tierlist
    rating = ["", "D", "D/C", "C", "C/B", "B",
            "B/A", "A", "A/S", "S", "S/SS", "SS"]

    tl = {}
    for row in range(1, len(tierlist)):
        for i in range(0, 6):
            j = i*6+1
            tl[tierlist[row][j]] = {
                'job 1': tierlist[row][j+1],
                'job 2': tierlist[row][j+2],
                'job 3': tierlist[row][j+3],
                'jc': tierlist[row][j+4],
                'total': ""
            }
    # add total
    invalid=[]
    for i in tl:
        best = 0
        for j in tl[i]:
            try:
                index = rating.index(tl[i][j])
                if index > best:
                    best = index
            except:
                invalid.append(i)
        tl[i]['total'] = rating[best]

    ##########################################################
    # add tierlist to units
    unitsN={
        unit['name']:unit
        for key,unit in units.items()
        }
    used=[]
    for i in tl:
        if i in invalid:
            continue

        if i in unitsN:
            unitsN[i]['tierlist'] = tl[i]
            used.append(i)
            continue
 
        else:
            similarities = [
                (key, jellyfish.jaro_winkler(i, key))
                for key in unitsN
                if key not in used
            ]
            key, score = max(similarities, key=lambda s: s[1])
            if score > 0.85:
                unitsN[key]['tierlist'] = tl[i]
                print(i + ' to ' + key + ' sim: ' + str(score))
                used.append(key)
            else:
                print('Not Found:',i,str(score),key)

    return units