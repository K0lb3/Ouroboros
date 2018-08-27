from ToEmbed._main import DIRS,LinkDB,ConvertFields,StrBuff,StrCondition,Embed

def Item(iname, page):
    #get gear dir
    item=DIRS['Item'][iname]

    #create basic embed
    embed= Embed(
        title=page, #page name
        #url=LinkDB('item',iname)  #page link
        )
    embed.set_author(name=item['name'])#, url=LinkDB('gear',iname))
    embed.set_thumbnail(url=LinkDB('/items/icons/',item['icon'],'.png',False))

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
    pass

def drop(item, type):
    pass