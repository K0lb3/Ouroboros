from ToEmbed._main import DIRS,LinkDB,ConvertFields,StrBuff,StrCondition,Embed

#To do:
#title url

def Job(iname, page):
    #get job dir
    job=DIRS['Job'][iname]

    icon = 'http://cdn.alchemistcodedb.com/images/jobs/icons/'
    if 'ac2d' in job and len(job['ac2d']) > len(job['model']):
        icon += job['ac2d']+'.png'
    else:
        icon += job['model']+'.png'

    #create basic embed
    embed= Embed(
        title=page, #page name
        url=LinkDB('job',iname)  #page link
        #'http://www.alchemistcodedb.com/unit/'+unit_in[6:].replace('_', '').lower()+'#'+job_na.replace(' ', '-').replace('+', '-plus').lower()
        )
    embed.set_author(name=job['name'], url=LinkDB('job',iname))
    embed.set_thumbnail(url=icon)

    while page:
        if page=='main':
            embed.ConvertFields(main(job))
            embed.title='main'
            break
        if page=='main ability':
            embed.ConvertFields(ability(job['abilities']['main']))
            embed.title=page
        if page=='sub ability':
            embed.ConvertFields(ability(job['abilities']['sub']))
            embed.title=page
        if page=='reactive ability':
            embed.ConvertFields(ability(job['abilities']['reactive']))
            embed.title=page
        if page=='passive ability':
            embed.ConvertFields(ability(job['abilities']['passive']))
            embed.title=page

        break
    
    return embed

def ability(ability):
    if type(ability)==list:
        skills=[
            DIRS['Skill'][skill['iname']]
            for abil in ability
            for skill in DIRS['Ability'][abil]['skills']
        ]
    else:
        skills=[
            DIRS['Skill'][skill['iname']]
            for skill in DIRS['Ability'][ability]['skills']
        ]
    
    return [
            {
            'name':skill['name'], 
            'value': skill['expr'], 
            'inline':False
            }
        for skill in skills
        ]


def main(job):
    return [
        {'name': 'Weapon',          'value':job['weapon'],                              'inline':True},
        {'name': 'Role',            'value':job['type'],                                'inline':True},
        {'name': 'DMG-Formula',     'value':job['formula'],                             'inline':False},
        {'name': 'Move',            'value':job['move'],                                'inline':True},
        {'name': 'Jump',            'value':job['jump'],                                'inline':True},
        {'name': 'Avoid',           'value':'{}%'.format(job['ranks'][-1]['avoid']),    'inline':True},
        {'name': 'Initial Jewels',  'value':'{}%'.format(100+job['ranks'][-1]['inimp']),'inline':True},
        {'name': 'Modifiers',        
            'value':', '.join([
                '{}% {}'.format(value,stat)
                for stat,value in job['ranks'][-1]['status'].items()
                if value!=0
                ]),
            'inline':False},
        {'name': 'Stats',        
            'value': '\n'.join([
                '{}: {}'.format(stat,value)
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
        ]