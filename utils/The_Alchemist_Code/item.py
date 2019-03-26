from ._main import DIRS,LinkDB,ConvertFields,StrBuff,StrCondition,Embed,GitLabLink

def Item(iname, page):
    #get gear dir
    item=DIRS['Item'][iname]

    #create basic embed
    embed= Embed(
        title=page, #page name
        url=LinkDB('item',iname)  #page link
        )
    embed.set_author(name=item['name'], url=embed.url)#, url=LinkDB('gear',iname))
    #embed.set_thumbnail(url='http://cdn.alchemistcodedb.com/images/items/icons/{}.png'.format(item['icon']))
    embed.set_thumbnail(url=GitLabLink('/ItemIcon/%s.png'%item['icon']))
    embed.set_footer(text='Item')

    while page:
        if page=='main':
            embed.ConvertFields(main(item))
            embed.title='main'
            break
        else:
            embed.ConvertFields(drop(item,page))
            embed.title=page
            break
        break
    
    return embed

def main(item):
    fields=[
        {'name': 'Type',    'value': item['tabtype'] if 'tabtype' in item else item['dif']['tabtype'] if 'dif' in item else '/',  'inline':True},
        {'name': 'Recipe',    'value': '' if 'recipe' not in item else 
            'Zeni: {cost}\nItems:\n{items}'.format(
                cost=DIRS['Recipe'][item['recipe']]['cost'],
                items='\n'.join([
                    '{}x - {}'.format(l_item['num'],DIRS['Item'][l_item['iname']]['name'])
                    for l_item in DIRS['Recipe'][item['recipe']]['items']
                ])
            ),
            'inline':True},
        {'name': 'Description',    'value': item['expr'],  'inline':False},
        {'name': 'Lore',    'value': item['flavor'],  'inline':False},
    ]
    Currency={
        "coin"	:	"Gems",
        "tour_coin"	:	"Tour Coins",
        "arena_coin"	:	"Arena Coins",
        "multi_coin"	:	"MP Coins",
        "piece_point"	:	"Soul Coins",
        "buy"	:	"Zeni",
        "sell"	:	"Sell",
        "enhace_cost"	:	"Enhance Cost",
        "enhace_point"	:	"Enhance Points",
        "value"	:	"EXP",
    }
    fields+=[
        {'name': name,    'value': item[key],  'inline':True}
        for key,name in Currency.items()
        if key in item and item[key]!= 9999999
    ]
    return fields

def drop(item, typ):
    if 'questlist' not in item:
        if 'recipe' in item:
            recipe = DIRS['Recipe'][item['recipe']]
            if len(recipe['items'])==0:
                return ([{'name': 'Notice','value': 'This item can\'t be farmed\nIt can probably only be acquired from shops or milestones.', 'inline': False}])
            elif len(recipe['items'])==1:
                return (drop(DIRS['Item'][recipe['items'][0]['iname']],typ))
            else:
                return ([
                    {'name': 'Notice','value': 'This item can\'t be farmed in %s mode,\n please look at it\'s crafting materials'%typ.title(), 'inline': False},
                    {
                        'name': 'Recipe',    'value': '' if 'recipe' not in item else 
                            'Zeni: {cost}\nItems:\n{items}'.format(
                                cost=DIRS['Recipe'][item['recipe']]['cost'],
                                items='\n'.join([
                                    '{}x - {}'.format(l_item['num'],DIRS['Item'][l_item['iname']]['name'])
                                    for l_item in DIRS['Recipe'][item['recipe']]['items']
                                ])
                            ),
                        'inline':True
                    },
                ])
    else:
        fields=[]
        for quest in item['questlist']:
            quest=DIRS['Quests'][quest]
            if typ!=quest['type']:
                continue

            Set=quest['map'][0]['Set']
            effiency=(len(Set['enemy'])+len(Set['treasure']))/(quest['point']*len(quest['dropList']))
            fields.append({
                'name': '{title}{MP}{name} [AP: {AP}, Effiency: {effiency}]'.format(
                    effiency=(int(effiency*100)/100),
                    name=quest['name'],
                    title=(quest['title']+' ~ ') if 'title' in quest else '',
                    MP='**MP** ' if 'multi' in quest else '',
                    AP=quest['point']
                ),
                'value': '\n'.join([
                    DIRS['Item'][l_item]['name']
                    for l_item in quest['dropList']
                ]),
                'inline': False,
                'effiency': effiency
            })
        
        if len(fields) == 0:
            return ([{'name': 'Notice','value': 'This item can\'t be farmed in this mode,\nplease try the other one.', 'inline': False}])
        else:
            length=0
            fields=sorted(fields, key=lambda k: k['effiency'], reverse=True) 
            
            i=0
            try:
                while length<5800:
                    length+=len(fields[i]['name'])+len(fields[i]['value'])
                    i+=1
            except:
                pass
            if length>5900:
                fields.insert(0,{'name': 'Notice','value': 'There are too many quests to display,\n therefore only the higher effiency ones are shown.', 'inline': False})
                fields=fields[:i]
            return fields
        