from ToEmbed._main import DIRS,LinkDB,ConvertFields,StrBuff,StrCondition,Embed, Rarity

def Conceptcard(iname, page):
    #get card dir
    card=DIRS['Conceptcard'][iname]

    #create basic embed
    embed= Embed(
        title='', #page name
        #url=LinkDB('card',iname)  #page link
        )
    embed.set_author(name=card['name'])#, url=LinkDB('card',iname))
    embed.set_thumbnail(url='http://cdn.alchemistcodedb.com/images/cards/icons/{}.png'.format(iname))
    embed.set_image(url='http://cdn.alchemistcodedb.com/images/cards/artworks/{}.png'.format(iname))

    while page:
        if page=='main':
            embed.ConvertFields(main(card))
            embed.title='main'
            break
        break
    
    return embed

def main(card):
    try:
        unit=DIRS['Unit']['UN_V2_'+card['iname'].rsplit('_',2)[1]]['name']
    except:
        unit='None'

    fields = [
        {'name': 'Unit',            'value': unit, 'inline':True},
        {'name': 'Rarity',          'value': Rarity(card['rare'],4), 'inline':True},
        {'name': 'Enhance Cost',    'value': str(card['en_cost']), 'inline':True},
        {'name': 'Enhance EXP',     'value': str(card['en_exp']), 'inline':True},
        ]
    #Weapon Type ~ Tag
    if 'trust_reward' in card:
        fields.append({'name': 'Trust Reward', 'value': card['trust_reward'], 'inline': False})

    #effects
    for i,effect in enumerate(card['effects']):
        value=[]
        #cnds_iname
        if 'cnds_iname' in effect:
            value.append(
                '__**Condition(s):**__\n'+CardConditions(effect['cnds_iname'])
            )
        #abil_iname
        if 'abil_iname' in effect:
            value.append('__**Vision Ability:**__'+
            DIRS['Ability'][effect['abil_iname']]['name']
            )
        #skin
        if 'skin' in effect:
            value.append('__**Skin:**__'+DIRS['Artifact'][effect['skin']]['name'])
        #statusup_skill
        if 'statusup_skill' in effect:
            value.append('__**Unit Stats**__\n:'+
            StrBuff(DIRS['Skill'][effect['statusup_skill']]['target_buff_iname'],2,2)
            )
        #card_skill
        if 'card_skill' in effect:
            value.append('__**Stats**__\n:'+
            StrBuff(DIRS['Skill'][effect['card_skill']]['target_buff_iname'],2,2)
            )
        #add_card_skill_buff_awake
        if 'add_card_skill_buff_awake' in effect:
            value.append('__**Awakenend Stats**__\n:'+
            StrBuff(effect['add_card_skill_buff_awake'],2,2)
            )
        #add_card_skill_buff_lvmax
        if 'add_card_skill_buff_lvmax' in effect:
            value.append('__**Max LV Stats**__\n:'+
            StrBuff(effect['add_card_skill_buff_lvmax'],2,2)
            )
        fields.append({'name':  'Effect '+str(i+1),      'value': '\n'.join(value), 'inline':False})
    #lore
    fields.append({'name': 'Description',   'value': card['expr'], 'inline': False})
    return fields

def CardConditions(iname):
    conds=DIRS['Conceptcardconditions'][iname]
    ret=[]
    #unit group
    if 'unit_group' in conds:
        ret.append('Units: '+', '.join([
            DIRS['Unit'][uiname]['name']
            for uiname in DIRS['Unitgroup'][conds['unit_group']]['units']
        ]))
    #job group
    if 'job_group' in conds:
        ret.append('Jobs: '+', '.join([
            DIRS['Job'][jiname]['name']
            for jiname in DIRS['Jobgroup'][conds['job_group']]['jobs']
        ]))
    #element
    if 'conditions_elements' in conds:
        ret.append('Element: '+', '.join(conds['conditions_elements']))
    #birth
    if 'birth' in conds:
        ret.append('Birth: '+', '.join(conds['birth']))

    return '\n'.join(ret)