from ToEmbed._main import DIRS,LinkDB,ConvertFields,StrBuff,StrCondition,Embed

def Gear(iname, page):
    #get gear dir
    gear=DIRS['Artifact'][iname]

    #create basic embed
    embed= Embed(
        title='', #page name
        url=LinkDB('gear',iname)  #page link
        )
    embed.set_author(name=gear['name'], url=LinkDB('gear',iname))
    embed.set_thumbnail(url=LinkDB('/items/icons/',gear['icon'],'.png',False))

    while page:
        if page=='main':
            embed.ConvertFields(main(gear))
            embed.title='main'
            break
        break
    
    return embed

def main(gear):
    fields = []

    #Weapon Type ~ Tag
    fields.append({'name': 'Type', 'value': gear['tag'], 'inline': False})

    #Stats
    stats = [
        "{grade}★: {stats}".format(
            grade=grade+1, 
            stats=StrBuff(DIRS['Skill'][gear['equip_effects'][grade]]['target_buff_iname'],grade*5+9,29)
            )
        for grade in range(gear['rareini'],gear['raremax']+1)
        ]
    fields.append({'name': 'Max Stats', 'value': '\n'.join(stats), 'inline': False})

    #On Attack De/Buff
    target={
        'Self':     'Self',
        'SelfSide': 'Ally',
        'EnemySide':'Enemy',
        'NotSelf':  'Target'
        }

    on_attack=[]
    for grade in range(gear['rareini'],gear['raremax']+1):
        skill=DIRS['Skill'][gear['attack_effects'][grade]]
        effects=[]
        if 'self_buff_iname' in skill:
            effects.append('Self: '+StrBuff(skill['self_buff_iname'],grade*5+9,29,False))
        if 'target_buff_iname' in skill:
            effects.append('Target: '+StrBuff(skill['target_buff_iname'],grade*5+9,29,False))
        if 'self_cond_iname' in skill:
            effects.append('Self: '+StrCondition(skill['self_cond_iname'],grade*5+9,29))
        if 'target_cond_iname'in skill:
            effects.append('Target: '+StrCondition(skill['target_cond_iname'],grade*5+9,29))

        on_attack.append("{grade}★: {effect}".format(
            grade=grade+1,
            effect='\n\t'.join(effects)
            ))

    fields.append({'name': 'Max Attack (De)Buff', 'value': '\n'.join(on_attack), 'inline': False})

    #weapon abilities
    abilities=[]
    for abil in gear['abil_inames']:
        ability=DIRS['Ability'][abil]
        conditions=''
        if 'condition_units'    in ability:
            conditions+=', '.join([
                DIRS['Unit'][unit]['name']
                for unit in ability['condition_units']
            ])
        if 'condition_jobs'     in ability:
            conditions+='Job: '+DIRS['Job'][ability['condition_jobs'][0]]['name']
        elif 'condition_sex'    in ability:
            conditions+=ability['condition_sex']

        abilities.append(conditions+': '+ability['name'])
    fields.append({'name': 'Ability', 'value': "\n\n".join(abilities), 'inline': False})

    #lore
    fields.append({'name': 'Flavor Text', 'value': gear['flavor'], 'inline': False})
    return fields