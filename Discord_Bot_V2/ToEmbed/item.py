from ToEmbed._main import DIRS,LinkDB,ConvertFields,StrBuff,StrCondition,Embed

def Item(iname, page):
    #get gear dir
    item=DIRS['Item'][iname]

    #create basic embed
    embed= Embed(
        title=page, #page name
        url=LinkDB('item',iname)  #page link
        )
    embed.set_author(name=item['name'], url=embed.url)#, url=LinkDB('gear',iname))
    embed.set_thumbnail(url='http://cdn.alchemistcodedb.com/images/items/icons/{}.png'.format(item['icon']))
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
        {'name': 'Type',    'value': item['tabtype'] if 'tabtype' in item else item['dif']['tabtype'],  'inline':True},
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
        "piece_point"	:	"Soul Shards",
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
        return([{'name':'This item isn\'t dropped in any mission of this type', 'value': 'Try the other type or you have to craft it ~~or buy it', 'inline':False}])
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


    #sort fields
    return sorted(fields, key=lambda k: k['effiency'], reverse=True) 
    