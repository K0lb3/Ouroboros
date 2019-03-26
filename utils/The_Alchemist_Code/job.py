from ._main import DIRS,LinkDB,ConvertFields,StrBuff,StrCondition,Embed,StrSkill,GitLabLink

#To do:
#title url

def Job(iname, page):
    #get job dir
    job=DIRS['Job'][iname]

    #icon = 'http://cdn.alchemistcodedb.com/images/jobs/icons/'
    if 'ac2d' in job and len(job['ac2d']) > len(job['model']):
        icon = job['ac2d']
    else:
        icon = job['model']

    #create basic embed
    embed= Embed(
        title=page, #page name
        url=LinkDB('job',iname)  #page link
        #'http://www.alchemistcodedb.com/unit/'+unit_in[6:].replace('_', '').lower()+'#'+job_na.replace(' ', '-').replace('+', '-plus').lower()
        )
    embed.set_author(name=job['name'], url=LinkDB('job',iname))
    embed.set_thumbnail(url=GitLabLink('/JobIcon/%s.png'%icon))
    embed.set_footer(text='Job|%s'%iname)

    while page:
        if page=='main':
            embed.ConvertFields(main(job))
            embed.title='main'
            break
        if page=='main ability':
            embed.ConvertFields(ability(job['abilities']['main'],job))
            embed.title=page
        if page=='sub ability':
            embed.ConvertFields(ability(job['abilities']['sub'],job))
            embed.title=page
        if page=='reaction ability':
            embed.ConvertFields(ability(job['abilities']['reaction'],job))
            embed.title=page
        if page=='passive ability':
            embed.ConvertFields(ability(job['abilities']['passive'],job))
            embed.title=page
        if page=='units':
            embed.description=(units(job))
            embed.title=page       
        break
    
    return embed

def units(job):
    return (
            '__Following units have this job__:\n%s'%', '.join(sorted([
                DIRS['Unit'][unit]['name']
                for unit in job['units']['unit']
        ]))) if job['units']['unit'] else (
            'No unit has this job,\n__Following enemies have this job__:\n, '.join(sorted([
                DIRS['Unit'][unit]['name']
                for unit in job['units']['NPC']
        ])))
    

def ability(ability,job):
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
            'value': "%s\n%s%s"%(skill['expr'],StrSkill(skill),('\n%s'%skill['formula'] if 'formula' in skill and skill['formula']!= job['formula'] else '')), 
            'inline':False
            }
        for skill in skills
        ]


def main(job):
    mrank=job['ranks'][-1] if 'avoid' in job['ranks'][-1] else job
    return [
        {'name': 'Weapon',          'value':job['weapon'] if 'weapon' in job else '',                              'inline':True},
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
        