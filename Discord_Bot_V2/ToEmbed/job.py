from ToEmbed._main import DIRS,LinkDB,ConvertFields,StrBuff,StrCondition,Embed

#To do:
#title url

def Job(iname, page):
    #get job dir
    job=DIRS['Job'][iname]

    #create basic embed
    embed= Embed(
        title='', #page name
        url=LinkDB('job',iname)  #page link
        #'http://www.alchemistcodedb.com/unit/'+unit_in[6:].replace('_', '').lower()+'#'+job_na.replace(' ', '-').replace('+', '-plus').lower()
        )
    embed.set_author(name=job['name'], url=LinkDB('job',iname))
    embed.set_thumbnail(url=LinkDB('/items/icons/',job['icon'],'.png',False))

    while page:
        if page=='main':
            embed.ConvertFields(main(job))
            embed.title='main'
            break
        if page=='main ability':
            embed.ConvertFields(ability(job['']))
            embed.title=page
        if page=='sub ability':
            embed.ConvertFields(ability(job['']))
            embed.title=page
        if page=='reactives':
            embed.ConvertFields(ability(job['']))
            embed.title=page
        if page=='passives':
            embed.ConvertFields(ability(job['']))
            embed.title=page

        break
    
    return embed

def ability(ability):
    fields = []
    return fields


def main(job):
    fields = []
    return fields


