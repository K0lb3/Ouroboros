TRANSLATION = {}



def Translation():
    [loc, trans] = loadFiles(
        ['LocalizedMasterParam.json', 'TranslationsS.json'])
    
    for i in trans:
        if i in loc:
            trans[i].update(loc[i])
            TRANSLATION[i]=trans[i]
        else:
            TRANSLATION[i] = trans[i]

        if len(TRANSLATION[i]['long des']) == 0:
            TRANSLATION[i]['long des'] = loc[i]['short des']


    collab = ""
    collab_short = ''
    for c in collabs:
        if c+'_' == iname[:len(c)+1]:
            collab = collabs[c][1]
            collab_short = collabs[c][0]
            iname = iname[len(c)+1:]
            break

    # unknown name
    try:
        name = loc['UN_V2_'+iname]['name']
    except:
        name = iname.replace('_', ' ')
        prefix = {
            'BLK': 'Black Killer',
            'L': 'Little',
        }

        for p in prefix:
            if p+' ' == name[:len(p)+1]:
                name = prefix[p] + ' ' + name[len(p)+1:]

        name = name.title()
        u_m_names['UN_V2_'+iname] = {'generic': name, 'NAME': ""}

    return [name, collab, collab_short]

def wytesong(wyte, units, loc):
    wunit = {}
    for unit in wyte['Units']:
        wunit[unit['Name JPN']] = unit

    def get_collab(acquire):
        # acquire
        collabs = {
            'Sacred Stone':         ['SSM', 'Sacred Stone Memories'],
            'BF Collab':            ['BF', 'Brave Frontier'],
            'POTK':                 ['POTK', 'Phantom Of The Kill'],
            'Shinobina':            ['SN', 'Shinobi Nightmare'],
            'EO Collab':            ['EO', 'Etrian Odyssey'],
            'Fate Collab':          ['Fate', 'Fate/Stay Night [UBW]'],
            'EO Mystery Dungeon':   ['EMD', 'Etrian Mystery Dungeon'],
            'Fullmetal Alchemist':  ['FA', 'Fullmetal Alchemist'],
            'Radiant Historia':     ['RH', 'Radiant Historia'],
            'FFXV':                 ['FF', 'Final Fantasy 15'],
            'Disgaea':              ['DIS', 'Disgea'],
            'April Fools':          ['', 'April Fools'],
            'Crystal of Reunion':  ['CR', 'Crystal Re:Union']
        }
        for c in collabs:
            if c in acquire:
                return collabs[c]
        return ["", ""]

    found = {}
    none = {}
    for unit in units:
            [name, collab, collab_short] = name_collab(unit['iname'], loc)
            c = {
                'iname': unit['iname'],
                'official': loc[unit['iname']]['name'] if unit['iname'] in loc else name,
                'generated': name,
                'inofficial': "",
                'collab': collab,
                'collab_short': collab_short,
                'japanese': unit['name']
            }
            try:
                c['inofficial'] = ReBr.sub(
                    '', wunit[unit['name']]['Name']).rstrip(' ')
                c['inofficial2'] = wunit[unit['name']]['Name']
                if c['collab'] == "":
                    [c['collab_short'], c['collab']] = get_collab(
                        wunit[unit['name']]['Acquire'])
                found[unit['iname']] = c
                del wunit[unit['name']]
            except:
                none[unit['iname']] = c
        else:
            break

    delete = []

    for unit in none:
        try:
            for left in wunit:
                if (similarity(none[unit]['official'], ReBr.sub('', wunit[left]['Name']).title()) >= 0.8 or
                    similarity(none[unit]['official'], wunit[left]['Romanji'].title()) >= 0.8 or
                        similarity(none[unit]['japanese'], left) >= 0.8):

                    found[none[unit]['iname']] = none[unit]
                    found[none[unit]['iname']].update({
                        'inofficial': ReBr.sub('', wunit[left]['Name']).rstrip(' '),
                        'inofficial2': wunit[left]['Name'],
                        'japanese2': wunit[left]['Name JPN']
                    })
                    if none[unit]['collab'] == "":
                        [found[none[unit]['iname']]['collab_short'], found[none[unit]
                                                                           ['iname']]['collab']] = get_collab(wunit[left]['Acquire'])

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
    found.update(none)
    return(found)
