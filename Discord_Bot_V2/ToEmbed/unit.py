from ToEmbed._main import DIRS,LinkDB,ConvertFields,StrBuff,StrCondition,Embed,Rarity

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
        title=page, #page name
        url='http://www.alchemistcodedb.com/{region}unit/{unit}'.format(
            region=''if 'dif' not in unit else 'jp/',
            unit= unit['iname'].replace('UN_V2_', "").replace('_', "-").lower()
            ),
        color=ELEMENT_COLOR[unit['element']]
    )
    embed.set_author(name=unit['name'], url=embed.url)
    embed.set_thumbnail(url='http://cdn.alchemistcodedb.com/images/units/profiles/' + unit["image"] + ".png")

    while page:
        if page=='main':
            embed.ConvertFields(main(unit))
            if 'tierlist' in unit:
                embed.title += ", overall rank: [{tier}]".format(tier=unit['tierlist']['total'])

            break

        if page=='lore':
            embed.ConvertFields(lore(unit))
            embed.url=embed.url+'#profile'
            break

        if page=='kaigan':
            embed.ConvertFields(kaigan(unit))
            break  

        if page=='nensou':
            embed.ConvertFields(nensou(unit))
            break  

        if 'job' in page:
            embed.ConvertFields(job(unit,page))
            break          

        if page=='art':
            embed=art(unit)
            break
    return embed


def main(unit):
    f = []
        
    #tags
    f.append({'name': 'Tags' , 'value': ', '.join(unit['tags']), 'inline':True})
    #rarity
    f.append({'name': 'Rarity', 'value': Rarity(unit['rare'],unit['raremax']), 'inline':True})
    #country
    f.append({'name': 'Country', 'value': unit['birth'], 'inline':True})
    #kaigan
    if 'kaigan' in unit:
        f.append({'name': 'Kaigan', 'value': '✅', 'inline':True})
    #master ability
    ability=None
    if 'ability' in unit:
        ability=unit['ability']
    elif 'dif' in unit and 'ability' in unit['dif']:
        ability=unit['dif']['ability']
    if ability:
        f.append({'name': 'Master Ability - {name}{region}'.format(
            name=DIRS['Skill'][DIRS['Ability'][ability]['skills'][0]['iname']]['name'],
            region='' if 'ability' in unit else 'ᴶ'
            ), 
            'value': MasterAbility(ability), 'inline':False})
    #leader skill
    f.append({'name': 'Leader Skill - '+DIRS['Skill'][unit['skill']]['name'], 'value': LeaderSkill(unit['skill']), 'inline':False})
    #jobs
    for i,job in enumerate(unit['jobs']):
        f.append({'name': 'Job {}'.format(str(i+1)), 'value': DIRS['Job'][job]['name'], 'inline': not i==2})
    if 'jobchanges' in unit:
        for i,job in enumerate(unit['jobchanges']):
            if job:
                f.append({'name': 'Job Change {}'.format(str(i+1)), 'value': DIRS['Job'][job]['name'], 'inline': not i==2})
    #nensou
    if 'conceptcard' in unit:
        f.append({'name': 'Nensou', 'value': unit['conceptcard'], 'inline':True})
    #piece/HQ
    try:
        f.append({'name': 'Hard Quests', 'value': DIRS['Quests'][DIRS['Item'][unit['piece']]['quest']]['name'], 'inline':True})
    except:
        pass
    #add tierlist on top

    return f

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
    if 'kaigan' in unit:
        f=[]
        for typ,kaigan in unit['kaigan'].items():
            if kaigan=='Unlock':
                continue
            text=''
            if 'mSkillIname' in kaigan:
                skill = DIRS['Skill'][kaigan['mSkillIname']]
                if 'target_buff_iname' in skill:
                    text+='\n**Buff:**\n' + StrBuff(skill['target_buff_iname'],2,2,True)
                if 'target_cond_iname' in skill:
                    text+='\n**Condition:**\n' + StrCondition(skill['target_cond_iname'],2,2)
            if 'mOverwriteLeaderSkillIname' in kaigan:
                text+='\n**Leader Skill:**\n' + LeaderSkill(kaigan['mOverwriteLeaderSkillIname'])
            if 'mLearnAbilities' in kaigan and len(kaigan['mLearnAbilities']):
                pass
            f.append({'name':typ, 'value':text[1:], 'inline':False})
        return f
    else:
        return 0
            

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

    if 'conceptcard' in unit:
        skins.append({
            'name': 'Concept Card',
            'full': 'http://cdn.alchemistcodedb.com/images/cards/artworks/{}.png'.format(unit['conceptcard']),
            'closeup': 'http://cdn.alchemistcodedb.com/images/cards/icons/{}.png'.format(unit['conceptcard'])
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


##other functions

def MasterAbility(ability, dif=False):
    ability = DIRS['Ability'][ability]
    skill = DIRS['Skill'][ability['skills'][0]['iname']]
    ret = []
    if 'target_buff_iname' in skill:
        ret.append(StrBuff(skill['target_buff_iname'], 2, 2, True))
    if 'target_cond_iname' in skill:
        ret.append(StrCondition(skill['target_cond_iname'], 2, 2))
    if 'ReplaceTargetIdLists' in skill:
        ret.append('{skill} Upgrade'.format(
            skill=DIRS['Skill'][skill['ReplaceTargetIdLists'][0]]['name']))
    return '\n'.join(ret) if ret else '*Active Skill*'

def LeaderSkill(skill):
    skill = DIRS['Skill'][skill]
    ret = []
    if 'target_buff_iname' in skill:
        buff = StrBuff(skill['target_buff_iname'], 2, 2, False)
        if ':' in buff:
            buff = buff[buff.rindex(': ')+2:].replace('\n', ': ')
        ret.append(buff)
    if 'target_cond_iname' in skill:
        ret.append(StrCondition(skill['target_cond_iname'], 2, 2))
    return '\n'.join(ret)
