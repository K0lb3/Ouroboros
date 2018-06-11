import os
import jellyfish
import discord
import asyncio
from datetime import datetime
from datetime import timedelta
from discord.ext import commands
from discord_bot.TAC_API import *
from discord_bot.model import Unit


# Constants
ELEMENT_COLOR = {
    'Fire': 0xFF0000,
    'Wind': 0x007F00,
    'Water': 0x2828FF,
    'Thunder': 0xFFCC00,
    'Light': 0xFFFFFF,
    'Dark': 0x140014,
}
DEFAULT_ELEMENT_COLOR = 0x7F7F7F

GEAR_COLOR = {
    1: 16711680,
    2: 2631935,
    3: 32512,
}
DEFAULT_GEAR_COLOR = 8355711

PRESENCES=[
        'WIP Job: o?job',
        'Collabs: o?collabs',
        'Unit: o?unit',
        'Lore: o?lore',
        'Art:  o?art',
        'Gear: o?gear',
        'Farm: o?farm',
        'Ranking: o?arena',
        'Tierlist: o?tierlist',
        'Info: o?info',
        'Help: o?help'
        ]

#functions
def loadFiles(files):
    dir = os.path.join(os.path.dirname(os.path.realpath(__file__)), 'res')
    ret = []
    for file in files:
        path = os.path.join(dir, file)
        if not os.path.exists(path):
            print(path, " was not found.")
            continue
        print(file)

        try:
            with open(path, "rt", encoding='utf8') as f:
                ret.append(json.loads(f.read()))
        except ValueError:
            with open(path, "rt", encoding='utf-8-sig') as f:
                ret.append(json.loads(f.read()))

    return ret

def find_best(source, text):
    """
    Given a dictionary and a text, find the best matched item from the dictionary using the name
    :param source: The dictionary to search from (i.e. units, gears, jobs, etc)
    :type source: dict
    :param text: String to find the match
    :type text: str
    :return: The best matched item from the dictionary
    :rtype: dict
    """
    # XXX: Purposely shadowing the text parameter
    text = text.title()

    # Calculate the match score for each key in the source dictionary using the input text.
    # Then, create a list of (key, the best score) tuples.
    similarities = [
        (key, max(jellyfish.jaro_winkler(text, i.title()) for i in value.get('inputs', [])))
        for key, value in source.items()
    ]
    # Find the key with the highest score (This is the best matched key)
    key, score = max(similarities, key=lambda s: s[1])

    # XXX: If needed, implement a minimum threshold here

    # Return the actual best-matched value
    best_match = source[key]
    print("{name} is the best match for input '{input}' with score of {score}".format(
        name=best_match.get('name'), input=text, score=score
    ))
    return best_match


def fix_fields(fields):
    remove=[]
    uses=['name','value']
    for f in range(0,len(fields)):
        field = fields[f]
        for i in uses:
            if len(field[i])==0:
                remove.append(f)
    fin = [i for j, i in enumerate(fields) if j not in remove]
    return(fin)

def timeDif_hms(time):
    FMT = '%H:%M:%S'
    tdelta = datetime.strptime(time, FMT) - datetime.now()
    if tdelta.days < 0:
        tdelta = timedelta(days=0,
            seconds=tdelta.seconds)

    #days = tdelta.days
    #hours, remainder = divmod(tdelta.seconds, 3600)
    #minutes, seconds = divmod(remainder, 60)
    # If you want to take into account fractions of a second
    #seconds += tdelta.microseconds / 1e6
    
    return str(tdelta)

#global vars
prefix='o?'
bot = commands.Bot(command_prefix=prefix)
[units,drops,gears,jobs]=loadFiles(['units.json','drops.json','gear.json','jobs.json'])




async def status_task(presences):
    while True:
        for p in presences:
            game = discord.Game(p)
            await bot.change_presence(status=discord.Status.online, activity=game)
            await asyncio.sleep(10)

    
@bot.event
async def on_ready():
    print('Logged in as')
    print(bot.user.name)
    print(bot.user.id)
    print(bot.guilds)
    print('------')
    bot.loop.create_task(status_task(PRESENCES))

#gear
@bot.command()
async def gear(ctx, *, name):
    gear = find_best(gears, name)
    #start embed - title
    embed = discord.Embed(
        title=gear['name']+' '+gear['rarity'], 
        description="", 
        url=gear['link'], 
        color=GEAR_COLOR.get(gear['type'], DEFAULT_GEAR_COLOR)
        )
    #icon
    embed.set_thumbnail(url=gear['icon'])
    #gear data
    #stats
    stats=""
    for s in gear['stats']:
        stats+= s+'★: ' + gear['stats'][s] + '\n'
    embed.add_field(name='Max Stats:',      value=stats,     inline=False)
    #on attack
    atk_buff=""
    for s in gear['atk_buff']:
        atk_buff+= s+'★: ' + gear['atk_buff'][s] + '\n'
    if len(atk_buff)>1:
        embed.add_field(name='Max Attack (De)Buff:',      value=atk_buff,     inline=False)

    #abilities
    if len(gear['ability']):
        for a in gear['ability']:
            link = 'http://www.alchemistcodedb.com/skill/'+a['iname'].lower().replace('_','-')
            if len(a['restriction'])>1:
                embed.add_field(name='Ability: '+a['NAME'],      value='restriction: '+a['restriction']+'\n'+a['EXPR']+'\n [link](<'+link+'>)',     inline=False)
            else:
                embed.add_field(name='Ability: '+a['NAME'],      value=a['EXPR']+'\n [link](<'+link+'>)',     inline=False)

    embed.add_field(name="flavor",value=gear['flavor'],inline=False)

    await ctx.send(embed=embed) 

#drops
@bot.command()
async def farm(ctx, *, name):
    item = find_best(drops, name)
    #start embed - title
    embed = discord.Embed(title=item['name'], description="", url=item['link'])
    #icon
    embed.set_thumbnail(url=item['icon'])
    #unit data
    array = item['story'] if len(item['story'])>0 else item['event']
    for m in array:
        embed.add_field(name=m["name"],      value="\n".join(m['drops']),     inline=False)

    await ctx.send(embed=embed) 

#jobs
@bot.command()
async def job(ctx, *, name):
    job = find_best(jobs, name)
    #start embed - title
    embed = discord.Embed(
        title=job['name'], 
        description="", 
        url=job['link']
        )
    embed.set_footer(text='ᴶ - japan only', icon_url='')
    #icon
    if ':' in job['name'] or len(job['short description'])>5:
        embed.set_thumbnail(url=job['token'])
    else:
        embed.set_thumbnail(url=job['icon'])
    #unit data
    modifiers=""
    for i in job['modifiers']:
        if job['modifiers'][i] != 0:
            modifiers+= i +': '+str(job['modifiers'][i])+'%, '
    stats=""
    for i in job['stats']:
        stats+= i +': '+str(job['stats'][i])+' \n'

    fields=[
        {'name':'formula'               ,'value': job['formula']            ,'inline':False},
        {'name':'origin'                ,'value': job['origin']             ,'inline':False},
        {'name':'short description'     ,'value': job['short description']  ,'inline':False},
        {'name':'long description'      ,'value': job['long description']   ,'inline':False},
        {'name':'modifiers'             ,'value': modifiers                 ,'inline':False},
        {'name':'stats'                 ,'value': stats                     ,'inline':True},
        {'name':'units'                 ,'value': '\n'.join(job['units'])   ,'inline':True},
        {'name':'job:e'                 ,'value': '\n'.join(job['jobe'])    ,'inline':True},
    ]
    fields=fix_fields(fields)
    for i in fields:
        embed.add_field(name=i["name"],      value=i['value'],     inline=i['inline'])

    await ctx.send(embed=embed) 

# unit commands
@bot.command() # info
async def unit(ctx, *, name):
    unit_dict = find_best(units, name)
    unit = Unit(source=unit_dict)

    await ctx.send(embed=unit.to_unit_embed())

@bot.command() # lore
async def lore(ctx, *, name):
    unit_dict = find_best(units, name)
    unit = Unit(source=unit_dict)

    await ctx.send(embed=unit.to_lore_embed())

@bot.command() #  artwork
async def art(ctx, *, name):
    unit_dict = find_best(units, name)
    unit = Unit(source=unit_dict)

    for embed in unit.to_art_embeds():
        await ctx.send(embed=embed)

#arena
@bot.command()
async def arena(ctx):
    arena = req_arena_ranking()
    #start embed - title
    embed = discord.Embed(
        title="Top 50", 
        description="ranking in:  " + timeDif_hms("06:00:00") + "\n" + "reset in:    " + timeDif_hms("10:00:00"), 
        url="", 
        color=16711680
        )
    #icon
    embed.set_thumbnail(url="https://drive.google.com/uc?export=download&id=1W369kdS_4ISgJgOcHAgNxoidJRqWNNsl")
    #fields
    field=[[],[]]
    for i in range(0,25):
        field[0].append(str(i+1)+": " + arena[i]['name'])
        field[1].append(str(i+26)+": " + arena[i+25]['name'])

    embed.add_field(name="Rank  1-25",    value='\n'.join(field[0]),    inline=True)
    embed.add_field(name="Rank 26-50",    value='\n'.join(field[1]),    inline=True)

    await ctx.send(embed=embed)

@bot.command() #  artwork
async def tierlist(ctx):
    #start embed - title
    embed = discord.Embed(
        title="GL Tierlist", 
        description="", 
        url='https://docs.google.com/spreadsheets/d/1DWeFk0wiPaDKAYEcmf_9LnMFYy1nBy2lPTNAX52LkPU/edit#gid=1081890459',
        color=0xeee657,
        )
    #icon
    #embed.set_thumbnail(url="https://cdn.discordapp.com/attachments/453970242914353167/453978923844239362/Chibi_Lucian.png")
    #image
    embed.set_image(url="https://i.imgur.com/crlzqAL.jpg")
    embed.set_footer(text='Tierlist by Game, Visualisation by Ｅｉｋｅ/アイケ', icon_url='')
    await ctx.send(embed=embed) 
    
@bot.command()
async def collabs(ctx):
    embed = discord.Embed(title="Collab Codes", description="for unit commands \n \n"+
        'Fate : Fate/Stay Night [UBW] \n'+
        'FA   : Fullmetal Alchemist \n'+
        'RH   : Radiant Historia \n'+
        'POTK : Phantom Of The Kill \n'+
        'SN   : Shinobi Nightmare \n'+
        'FF   : Final Fantasy 15 \n'+
        'DIS  : Disgea \n'+
        'EO   : Etrian Odyssey \n'+
        'EMD  : Etrian Mystery Dungeon \n'+
        'CR   : Crystal Re:Union',
        color=8355711)

    await ctx.send(embed=embed)

@bot.command()
async def info(ctx):
    embed = discord.Embed(title="Ouroboros 2", description="TAC bot powered by data-mining :rigaltsmirk:", color=0xeee657)

    # Shows the number of servers the bot is member of.
    embed.add_field(name="Server count", value=f"{len(bot.guilds)}")
    # give users a link to invite thsi bot to their server
    embed.add_field(name="Invite", value="[Invite link](<https://discordapp.com/oauth2/authorize?client_id=437868948130758668&permissions=388160&scope=bot>)")
    # credits
    embed.add_field(name="Credits", value="missing job and j+/je naming: <@213962205895458818> \n"+
        "global data: <@178153963248549889> \n"+
        "japan  data: <@203298307601072130> \n"+
        "code & hosting: <@281201917802315776>")
    #embed.add_field(name="Thanks", value="")
    await ctx.send(embed=embed)

bot.remove_command('help')

@bot.command()
async def help(ctx):
    embed = discord.Embed(title="Ouroborus Bot", description="**Commands:**", color=0xeee657)

    embed.add_field(name="o?unit *name*", value="important informations about the unit", inline=False)
    embed.add_field(name="o?lore *name*", value="lore of the unit", inline=False)
    embed.add_field(name="o?art *name*", value="artworks of the unit", inline=False)
    embed.add_field(name="o?farm *name*", value="maps on which the item can be acquired", inline=False)
    embed.add_field(name="o?collabs *name*", value="collab shortcuts for unit names (after the unit name)", inline=False)
    embed.add_field(name="o?info", value="some informations about the bot", inline=False)

    await ctx.send(embed=embed)

BOT_TOKEN = os.environ.get('DISCORD_BOT_TOKEN')
bot.run(BOT_TOKEN)
