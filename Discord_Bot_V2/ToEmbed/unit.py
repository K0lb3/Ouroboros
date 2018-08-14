from ToEmbed._main import DIRS,LinkDB,ConvertFields,StrBuff,StrCondition,Embed

#To do:
#title url
ELEMENT_COLOR = {
        'Fire': 0xFF0000,
        'Wind': 0x007F00,
        'Water': 0x2828FF,
        'Thunder': 0xFFCC00,
        'Light': 0xFFFFFF,
        'Dark': 0x140014,
    }

def Unit(iname, page):
    #get job dir
    unit=DIRS['Unit'][iname]

    #create basic embed
    embed= Embed(
        title=unit['name'], #page name
        url='http://www.alchemistcodedb.com/{region}unit/{unit}'.format(
            region=''if 'dif' not in unit else 'jp/',
            unit= unit['iname'].replace('UN_V2_', "").replace('_', "-").lower()
            ),
        color=ELEMENT_COLOR[unit['element']]
    )
    embed.set_author(name=page, url=embed.url)
    embed.set_thumbnail(url='http://cdn.alchemistcodedb.com/images/units/profiles/' + unit["image"] + ".png")

    while page:
        if page=='main':
            embed.ConvertFields(main(unit))
            embed.title=page
            if 'tierlist' in unit:
                embed.title += ", overall rank: [{tier}]".format(tier=unit['tierlist']['total'])

            break

        if page=='lore':
            embed.ConvertFields(lore(unit))
            embed.title=page,
            embed.url=embed.url+'#profile'
            break

        if page=='kaigan':
            embed.ConvertFields(kaigan(unit))
            embed.title=page
            break  

        if page=='nensou':
            embed.ConvertFields(nensou(unit))
            embed.title=page
            break  

        if 'job' in page:
            embed.ConvertFields(job(unit,page))
            embed.title=page
            break          

        if page=='art':
            embed=art(unit)
            break

    
    return embed


def main(unit):
    fields = []
    #gender
    #rarity
    #country
    #master ability
    #kaigan
    #nensou
    #leader skill
    #jobs
    #piece/HQ
    #add tierlist on top
    return fields

def lore(unit):
    lore=unit['lore']
    using={
        'Birthday':'birth',
        'Zodiac':'zodiac',
        'Country':'country',
        'Blood Type':'blood',
        'Height':'height',
        'Weight':'weight',
        'Hobby':'hobby',
        'Favorite':'favorite',
        'Illustrator':'illust',
        'CV':'CV',
        'Profile':'profile'
    }
    fields = [
        {'name': item, 'value': lore[key], 'inline':True}
        for item,key in using.items()
        if key in lore
    ]
    return fields

def job(unit,job):
    fields=[]
        # _IGNORE_STATS = ['move', 'jump']

        # #modifiers
        # modifiers = [
        #     "{key}: {value}%".format(key=key, value=str(value))
        #     for key, value in job.modifiers.items()
        #     if value != 0
        #     ]

        # #stats
        # #unit stats * job-modifier
        # rstats={}
        # for key, value in self.stats.items():
        #     if key in job.modifiers:
        #         rstats[key] = int(value*(100+job.modifiers[key])/100)
        #     else:
        #         rstats[key] = value

        # rstats['Initial Jewels'] = int(rstats['Max Jewels']*job.modifiers['Initial Jewels']/100)

        # #dd job stats
        # for key, value in job.stats.items():
        #     if key in rstats:
        #         rstats[key] += value
        #     else:
        #         rstats[key] = value
                

        # #transform to output
        # stats = [
        #     "{key}: {value}".format(key=key, value=value)
        #     for key, value in rstats.items()
        # ]
        
        # jm_values=[
        #     '{stat}: {value}{mod}'.format(stat=stat['type'], value=int(stat['value']), mod='%'if stat['calc']=='Scale' else '')
        #     for stat in getattr(job,"job master buff")
        # ]

        # fields = [
        #     {'name': 'description', 'value': getattr(job, 'long description'),  'inline': False},
        #     {'name': 'formula',     'value': getattr(job, 'formula'),           'inline': False},
        #     {'name': 'weapon',      'value': getattr(job, 'weapon')},
        #     {'name': 'origin',      'value': getattr(job, 'origin')},
        #     {'name': 'move',        'value': job.stats['Move']},
        #     {'name': 'jump',        'value': job.stats['Jump']},
        #     {'name': 'JM bonus',    'value': ' , '.join(jm_values), 'inline':False},
        #     {'name': 'modifiers',   'value': '\n'.join(modifiers)},
        #     {'name': 'max stats (without JM bonus)', 'value': '\n'.join(stats)},
        # ]
    return fields

def kaigan(unit):
    pass

def nensou(unit):
    pass

def art(unit):
    art_link = 'http://cdn.alchemistcodedb.com/images/units/artworks/'
    embeds=[]

    skins=[{
        'name': 'Default',
        'full': art_link+unit['image'] + '.png',
        'closeup': art_link+unit['image']+'-closeup.png'
    }]
    if 'skins' in unit:
        for skin in unit['skins']:
            if 'UNIQUE' not in skin:
                skin=DIRS['Artifact'][skin]
                skins.append({
                    'name':     skin['name'],
                    'full':     art_link+unit['image']+'_'+skin['asset']+'.png',
                    'closeup':  art_link+unit['image']+'_'+skin['asset']+'-closeup.png',
                })

    for art in skins:
        embed= Embed(
            title=art['name'], #page name
            url=art['closeup'],
            color=ELEMENT_COLOR[unit['element']]
        )
        embed.set_author(name=unit['name'], url='http://www.alchemistcodedb.com/{region}unit/{unit}'.format(
                region=''if 'dif' not in unit else 'jp/',
                unit= unit['iname'].replace('UN_V2_', "").replace('_', "-").lower()
                ))
        embed.set_thumbnail(url=art['closeup'])
        embed.set_image(url=art['full'])


        embeds.append(embed)
    return embeds