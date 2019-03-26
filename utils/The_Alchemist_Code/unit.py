from ._main import DIRS,LinkDB,ConvertFields,StrBuff,StrCondition,Embed,Rarity,TACScale,GitLabLink
import re
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
            region='jp/' if 'birthid' in unit or ('dif' in unit and 'birthid' in unit['dif']) else '',
            unit= unit['iname'].replace('UN_V2_', "").replace('_', "-").lower()
            ),
        color=ELEMENT_COLOR[unit['element']]
    )
    embed.set_author(name=unit['name'], url=embed.url)
    #embed.set_thumbnail(url='http://cdn.alchemistcodedb.com/images/units/profiles/' + unit["image"] + ".png")
    embed.set_thumbnail(url=GitLabLink('/Portraits/%s.png'%unit['image']))
    embed.set_footer(text='Unit')

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
            fields,name=job(unit,page)
            embed.ConvertFields(fields)
            embed.title=name
            break          

        if page=='art':
            embed=art(unit)
            break
        
        if page=='tierlist':
            embed.ConvertFields(tierlist(unit))
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
    f.append({'name': 'Leader Skill - '+DIRS['Skill'][unit['leader_skills'][-1]]['name'], 'value': DIRS['Skill'][unit['leader_skills'][-1]]['expr2'], 'inline':False})
    if 'kaigan' in unit and 'mOverwriteLeaderSkillIname' in unit['kaigan']['Sloth']:
        f[-1]['value']='**B:** %s\n**K:** %s'%(
            f[-1]['value'], 
            DIRS['Skill'][unit['kaigan']['Sloth']['mOverwriteLeaderSkillIname']]['expr2']
            )
    #jobs
    for i,job in enumerate(unit['jobs']):
        f.append({'name': 'Job {}'.format(str(i+1)), 'value': DIRS['Job'][job]['name'], 'inline': not i==2})

    if 'jobchanges' in unit:
        for i,job in enumerate(unit['jobchanges']):
            if job:
                f.append({'name': 'Job Change {}'.format(str(i+1)), 'value': DIRS['Job'][job]['name'], 'inline': not i==2})

    #tierlist
    # if 'tierlist' in unit:
    #     f.append({'name': 'Tierlist Rating', 'value': 'LB:\t0\t5\t10\t15\t20\t25\n\t\t%s%s'%(
    #         '\t'.join([rat for LB,rat in unit['tierlist']['rating'].items()]),
    #         '\nJob+:\t \t \t \t15\t20\t25\n\t\t\t \t \t \t%s'%'\t'.join([rat for LB,rat in unit['tierlist']['jobchange']['rating'].items()]) if 'jobchange' in unit['tierlist'] else ''
    #     ), 'inline':False})
    #nensou
    if 'conceptcards' in unit:
        f.append({'name': 'Nensou', 'value': '\n'.join([
            DIRS['Conceptcard'][ucard]['name']
            for ucard in unit['conceptcards']
        ]), 'inline':True})
    #piece/HQ
    try:
        shard=DIRS['Item'][unit['piece']]
        if 'questlist' in shard:
            f.append({'name': 'Hard Quests - GL', 'value': '\n'.join([
                DIRS['Quests'][quest]['title']+' ~ '+DIRS['Quests'][quest]['name']
                for quest in shard['questlist']
            ]), 'inline':False})
        if 'quests' in shard:
            f.append({'name': 'Hard Quests - JP', 'value': '\n'.join([
                DIRS['Quests'][quest]['title']+' ~ '+DIRS['Quests'][quest]['name']
                for quest in shard['quests']
            ]), 'inline':False})
    except:
        pass
    #add tierlist on top

    return f

def lore(unit):
    lore=unit['lore']
    using={
        'Birthday':'birth',
        'Country':'country',
        'Zodiac':'zodiac',
        'Blood Type':'blood',
        'Height':'height',
        'Weight':'weight',
        'Hobby':'hobby',
        'Favorite':'favorite',
        'Illustrator':'illust',
        'Cast Voice':'cv',
        'Profile':'profile'
    }
    fields = [
        {'name': item, 'value': lore[key], 'inline':True}
        for item,key in using.items()
        if key in lore
    ]
    return fields

def job(unit,job):
    job=int(job[3])
    if job < 4:
        job=DIRS['Job'][unit['jobs'][job-1]]
    else:
        job=DIRS['Job'][unit['jobchanges'][job-4]]
        if not job:
            return([])
    

    mrank=job['ranks'][-1] if 'avoid' in job['ranks'][-1] else job

    return ([
        {'name': 'Weapon',          'value':job['weapon'],                              'inline':True},
        {'name': 'AI',            'value':'%s\n%s'%(job['type'],job['ai']),                                'inline':True},
        {'name': 'DMG-Formula',     'value':job['formula'],                             'inline':False},
        {'name': 'Move',            'value':job['move'],                                'inline':True},
        {'name': 'Jump',            'value':job['jump'],                                'inline':True},
        {'name': 'Avoid',           'value':'{}%'.format(mrank['avoid'] if 'avoid' in mrank else 0),    'inline':True},
        {'name': 'Initial Jewels',  'value':'{}%'.format(100+mrank['inimp']),'inline':True},
        {'name': 'Modifiers',        
            'value':', '.join([
                '{}% {}'.format(value,stat)
                for stat,value in mrank['status'].items()
                if value!=0
                ]),
            'inline':False},
        {'name': 'JM Bonus Stats',        
            'value': StrBuff(DIRS['Skill'][job['master']]['target_buff_iname']),
            'inline':False},
        {'name': 'Stats without JM bonus',        
            'value': '\n'.join([
                '{}: {}'.format(
                    stat,
                    int(value+
                        (
                            (TACScale(unit['ini_status']['param'][stat],unit['max_status']['param'][stat],85,100)*
                            (1+
                            (mrank['status'][stat]/100))) if stat in unit['max_status']['param'] else 0
                        )
                    )
                    )
                for stat,value in job['stats'].items()
                if 'Res' not in stat
            ]),    
            'inline':True},
        {'name': 'Resistances',        
            'value': '\n'.join([
                '{}: {}'.format(stat,value)
                for stat,value in job['stats'].items()
                if 'Res' in stat
            ]),    
            'inline':True},
        ],job['name'])

def kaigan(unit):
    if 'kaigan' in unit:
        f=[]
        for typ,kaigan in unit['kaigan'].items():
            if kaigan=='Unlock':
                continue
            text=''
            if 'mSkillIname' in kaigan:
                skill = DIRS['Skill'][kaigan['mSkillIname']]
                # if 'target_buff_iname' in skill:
                #     text+='\n**Buff:**\n' + StrBuff(skill['target_buff_iname'],2,2,True)
                # if 'target_cond_iname' in skill:
                #     text+='\n**Condition:**\n' + StrCondition(skill['target_cond_iname'],2,2)
                text+=skill['expr2']

            if 'mOverwriteLeaderSkillIname' in kaigan:
                text+='\n**Leader Skill Upgrade (LV: %s ):**\n%s'%(kaigan['mOverwriteLeaderSkillLevel']-1,DIRS['Skill'][kaigan['mOverwriteLeaderSkillIname']]['expr2'])#LeaderSkill(kaigan['mOverwriteLeaderSkillIname'])

            if 'mLearnAbilities' in kaigan and len(kaigan['mLearnAbilities']):
                for abil in kaigan['mLearnAbilities']:
                    text+='\n**{name} (LV: {lv}):**\n{skill}'.format(
                        name= '%s Upgrade'%DIRS['Ability'][abil['mAbilityOverwrite']]['name'] if 'mAbilityOverwrite' in abil else DIRS['Ability'][abil['mAbilityIname']]['name'],
                        lv = str(abil['mLevel']-1),
                        skill = '\n'.join([
                            DIRS['Skill'][skl['iname']]['expr2']
                            for skl in DIRS['Ability'][abil['mAbilityIname']]['skills']
                        ])
                    )

            f.append({'name':typ, 'value':text[1:], 'inline':False})
        return f
    else:
        return 0
            

def nensou(unit):
    ugroup=[]
    jgroup=[]
    egroup=[]
    bgroup=[]
    for card in DIRS['Conceptcard']:

        for effect in card['effects']:
            if 'cnds_iname' not in effect:
                continue

            conds=DIRS['Conceptcardconditions'][effect['cnds_iname']]
            if 'unit_group' in conds and unit['iname'] in DIRS['Unitgroup'][conds['unit_group']]['units']:
                ugroup.append(card['name'])
            elif 'job_group' in conds and 'job' in DIRS['Jobgroup'][conds['job_group']]['jobs']:
                jgroup.append(card['name'])
            elif 'birth' in conds and unit['country'] in conds['birth']:
                bgroup.append(card['name'])
            elif 'conditions_elements' in conds and unit['element'] in conds['conditions_elements']:
                egroup.append(card['name'])

def tierlist(unit):
		# "tierlist": {
		# 	"job": "Holy Cavalier [Isaiah]",
		# 	"rating": {
		# 		"LB0": "C",
		# 		"LB5": "A",
		# 		"LB10": "A",
		# 		"LB15": "A",
		# 		"LB20": "S",
		# 		"LB25": "S"
		# 	},
		# 	"jobchange": {
		# 		"name": "Holy Cavalier [Isaiah]",
		# 		"rating": {
		# 			"LB15": "A",
		# 			"LB20": "S",
		# 			"LB25": "S"
		# 		}
		# 	},
		# 	"total": "S"
    if 'tierlist' in unit:
        fields=[
            {'name':'suggested job','value':unit['tierlist']['job'],'inline':False},
            *[
                {'name':'LB %s'%i,'value':unit['tierlist']['rating']['LB%s'%i], 'inline':True}
                for i in range(0,26,5)
            ]
            ]
        if 'jobchange' in unit['tierlist']:
            fields+=[
            {'name':'job change','value':unit['tierlist']['jobchange']['name'],'inline':False},
            *[
                {'name':'LB %d'%i,'value':unit['tierlist']['jobchange']['rating']['LB%d'%i], 'inline':True}
                for i in range(15,26,5)
            ]
            ]
        return fields
    else:
        return [{'name':'this unit has no tierlist ranking','value':'*in Game\'s tierlist\n~~or I haven\'t had the time to update the bot data~~'}]





def art(unit):
    #art_link = 'http://cdn.alchemistcodedb.com/images/units/artworks/'
    embeds=[]

    skins=[{
        'name': 'Default',
        'full': GitLabLink('/UnitImages/%s.png'%unit['image']),#art_link+unit['image'] + '.png',
        'closeup': GitLabLink('/Portraits/%s.png'%unit['image'])#art_link+unit['image']+'-closeup.png'
    }]
    if 'skins' in unit:
        for skin in unit['skins']:
            if 'UNIQUE' not in skin:
                skin=DIRS['Artifact'][skin]
                skins.append({
                    'name':     skin['name'],
                    'full':     GitLabLink('/UnitImages/%s_%s.png'%(unit['image'],skin['asset'])),#art_link+unit['image']+'_'+skin['asset']+'.png',
                    'closeup':  GitLabLink('/Portraits/%s_%s.png'%(unit['image'],skin['asset']))#art_link+unit['image']+'_'+skin['asset']+'-closeup.png',
                })

    if 'dif' in unit and 'ability' in unit['dif']:
        unit['ability']=unit['dif']['ability']
    if 'ability' in unit and 'wytesong' in DIRS['Skill'][DIRS['Ability'][unit['ability']]['skills'][0]['iname']]:
        MA=DIRS['Skill'][DIRS['Ability'][unit['ability']]['skills'][0]['iname']]
        skins.append({
            'name': 'Master Ability ~ %s'%MA['name'],
            'full': MA['wytesong']['img'],
            'closeup': MA['wytesong']['img']
        })


    if 'conceptcards' in unit:
        for ucard in unit['conceptcards']:
            skins.append({
                'name': 'Nensou ~ %s'%DIRS['Conceptcard'][ucard]['name'],
                'full': GitLabLink('/ConceptCard/%s.png'%ucard),#http://cdn.alchemistcodedb.com/images/cards/artworks/{}.png'.format(ucard),
                'closeup': GitLabLink('/ConceptCardIcon/%s.png'%ucard)#'http://cdn.alchemistcodedb.com/images/cards/icons/{}.png'.format(ucard)
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

    for opath in DIRS['Path']['ConceptArt']:
        if unit['iname'] in opath:
            embed= Embed(
                title='original concept art (from twitter)',
                color=ELEMENT_COLOR[unit['element']]
            )
            embed.set_image(url="https://gitlab.com/the-alchemist-codes/Database/raw/master/resources%s"%opath)
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
