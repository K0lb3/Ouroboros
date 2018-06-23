import os
import jellyfish
import discord
import asyncio
import json
from datetime import datetime,timedelta
from discord.ext import commands
from model import *

# Constants
prefix='o?'
bot = commands.Bot(command_prefix=prefix)
PRESENCES=[
        'WIP Job: o?job',
        'Collabs: o?collabs',
        'Unit: o?unit',
        'Lore: o?lore',
        'Art:  o?art',
        'Gear: o?gear',
        'Farm: o?farm',
        'Ranking: o?arena',
        'Arena Enemy: o?rank x'
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

def find_best(source, text, print_=False):
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
    if text in source:
        best_match=source[text]
        if print_:
            print("{name} ~ direct match for {input}".format(
                name=best_match.get('name'), input=text
                ))
    
    else:
        # Calculate the match score for each key in the source dictionary using the input text.
        # Then, create a list of (key, the best score) tuples.
        similarities = [
            (key, jellyfish.jaro_winkler(text, key.title()))
            for key in source.keys()
            ]  

        #similarities = [
        #    (key, max(jellyfish.jaro_winkler(text, i.title()) for i in value.get('inputs', [])))
        #    for key, value in source.items()
        #    ]

        # Find the key with the highest score (This is the best matched key)
        key, score = max(similarities, key=lambda s: s[1])

        # XXX: If needed, implement a minimum threshold here

        # Return the actual best-matched value
        best_match = source[key]
        if print_:
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

def timeDif_hms(time,time2=datetime.now()):
    FMT = '%H:%M:%S'
    tdelta = datetime.strptime(time, FMT) - time2
    if tdelta.days < 0:
        tdelta = timedelta(days=0,
            seconds=tdelta.seconds)

    #days = tdelta.days
    #hours, remainder = divmod(tdelta.seconds, 3600)
    #minutes, seconds = divmod(remainder, 60)
    # If you want to take into account fractions of a second
    #seconds += tdelta.microseconds / 1e6

    return str(tdelta)

def convertDBS(DBS):
    newDBS=[]
    for DB in DBS:
        newDB={}
        for i in DB:
            for inp in DB[i]['inputs']:
                newDB[inp]=DB[i]
        newDBS.append(newDB)
    return newDBS

async def statistic(ctx, command, input=False, result=False):
    Channel = bot.get_channel(457645688583618560)
    embed=discord.Embed(title='Command: '+command,color=0x00FF00)
    embed.add_field(name="Guild",   value=ctx.guild)
    embed.add_field(name="User",    value=ctx.author)
    if input:
        embed.add_field(name="Input",   value=input)
    if result:
        embed.add_field(name="Result",  value=result)
    await Channel.send(embed=embed)

async def status_task(presences):
    while True:
        for p in presences:
            game = discord.Game(p)
            await bot.change_presence(status=discord.Status.online, activity=game)
            await asyncio.sleep(10)

async def add_reactions(msg,reactions):
    for r in reactions:
        await msg.add_reaction(r)

@bot.event
async def on_ready():
    print('Logged in as')
    print(bot.user.name)
    print(bot.user.id)
    print(bot.guilds)
    print('------')
    bot.loop.create_task(status_task(PRESENCES))

@bot.event
async def on_reaction_add(reaction, user):
    msg=reaction.message
    emoji=reaction.emoji
    if (type(emoji)==str) and user != bot.user and msg.author == bot.user:
        membed=msg.embeds[0]
        if FOOTER_URL['UNIT'] == membed.footer.icon_url:
            unit_dict = find_best(units, membed.title.replace('ᴶ',''))
            unit_obj = Unit(source=unit_dict)

            if emoji=='🗃': #main page
                    await msg.edit(embed=unit_obj.to_unit_embed())
            elif emoji=='📰': #lore page
                    await msg.edit(embed=unit_obj.to_lore_embed())
            else:
                job_e={
                    '1⃣':'job 1',     
                    '2⃣':'job 2',
                    '3⃣':'job 3',
                    '4⃣':'jc 1',
                    '5⃣':'jc 2',
                    '6⃣':'jc 3'
                    }
                if emoji in job_e:
                    job_dict = find_best(jobs, getattr(unit_obj,job_e[emoji]))
                    if unit_obj.name not in job_dict['units']:
                        job_dict = find_best(jobs, '{unit} {job}'.format(unit=unit_obj.name, job=getattr(unit_obj,job_e[emoji])))

                    job_obj = Job(source=job_dict)
                    await msg.edit(embed=unit_obj.to_unit_job_embed(job=job_obj))

        elif FOOTER_URL['JOB'] == membed.footer.icon_url:
            job_dict = find_best(jobs, membed.title.replace('ᴶ',''))
            job_obj = Job(source=job_dict)

            if emoji=='🗃': #main page
                    await msg.edit(embed=job_obj.to_job_embed())
            else:
                page={
                    '🇲':'main',     
                    '🇸':'sub',
                    '🇷':'reactives',
                    '🇵':'passives',
                    }
                if emoji in page:
                    await msg.edit(embed=job_obj.to_skill_embed(page[emoji]))


#gear
@bot.command()
async def gear(ctx, *, name):
    gear_dict = find_best(gears, name,True)
    gear_obj = Gear(source=gear_dict)

    await ctx.send(embed=gear_obj.to_gear_embed())
    await statistic(ctx, "Gear", name, gear_dict['name'])


#drops
@bot.command()
async def farm(ctx, *, name):
    item = find_best(drops, name,True)
    #start embed - title
    embed = discord.Embed(title=item['name'], description="", url=item['link'])
    #icon
    embed.set_thumbnail(url=item['icon'])
    #unit data
    array = item['story'] if len(item['story'])>0 else item['event']
    for m in array:
        embed.add_field(name=m["name"],      value="\n".join(m['drops']),     inline=False)

    await ctx.send(embed=embed)
    await statistic(ctx, "Farm", name, item['name']) 

#jobs
@bot.command()
async def job(ctx, *, name):
    job_dict = find_best(jobs, name,True)
    job_obj = Job(source=job_dict)

    msg = await ctx.send(embed=job_obj.to_job_embed())
    await statistic(ctx, "Job", name, job_dict['name'])
    reactions=['🗃','🇲','🇸','🇷','🇵'] 
    #await add_reactions(msg, reactions)

# unit commands
@bot.command() # info
async def unit(ctx, *, name):
    unit_dict = find_best(units, name, True)
    unit_obj = Unit(source=unit_dict)

    msg = await ctx.send(embed=unit_obj.to_unit_embed())

    pos_reactions={
        'job 1':'1⃣',     
        'job 2':'2⃣',
        'job 3':'3⃣',
        'jc 1':'4⃣',
        'jc 2':'5⃣',
        'jc 3':'6⃣'
        }        
    reactions=['🗃','📰'] 
    for key,value in pos_reactions.items():
        if getattr(unit_obj, key, "")!="":
            reactions.append(value)

    #await add_reactions(msg, reactions)
    await statistic(ctx, "Unit", name, unit_dict['name'])


@bot.command() # lore
async def lore(ctx, *, name):
    unit_dict = find_best(units, name,True)
    unit_obj = Unit(source=unit_dict)

    await ctx.send(embed=unit_obj.to_lore_embed())
    await statistic(ctx, "Lore", name, unit_dict['name'])

@bot.command() #  artwork
async def art(ctx, *, name):
    unit_dict = find_best(units, name,True)
    unit_obj = Unit(source=unit_dict)

    for embed in unit_obj.to_art_embeds():
        await ctx.send(embed=embed)
    await statistic(ctx, "Art", name, unit_dict['name'])

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
    try:
        for i in range(0,25):
            field[0].append(str(i+1)+": " + arena[i]['name'])
            field[1].append(str(i+26)+": " + arena[i+25]['name'])

        embed.add_field(name="Rank  1-25",    value='\n'.join(field[0]),    inline=True)
        embed.add_field(name="Rank 26-50",    value='\n'.join(field[1]),    inline=True)
        await ctx.send(embed=embed)
        await statistic(ctx, "Arena")
    
    except KeyError:
        embed.description=json.dumps(arena,indent=4)[:2047]
        content="<@281201917802315776>"
        await ctx.send(content=content,embed=embed)

@bot.command() # arena enemy
async def rank(ctx,*,input):
    try:
        number = int(input)
    except:
        ctx.send('Invalid Input')
        ctx.send(len(input))
        return

    #request info
    try:
        enemy = req_arena_ranking()[number-1]
    except IndexError:
        ctx.send('Only the top 50 can be accessed')
        return

    time = datetime.fromtimestamp(enemy['btl_at']) - timedelta(hours=8)
    #create embed header
    embed = discord.Embed(
        title=input+': '+enemy['name'], 
        description="last battle at: "+time.strftime('%H:%M:%S ~ %d.%m'), #epoch time in sec 1528371048
        color=0xeee657,
        )
    #diggest info
    units=[]
    for unit in enemy['units']:
        c={
            'name'  : loc[unit['iname']]['NAME'],
            'LB'    : unit['plus'],
            'LV'    : unit['lv'],
            'jobs'  : [],
            }
        seljob = unit['select']['job'] #number, compare to jobs
        for job in unit['jobs']:
            #selected job?
            if job['iid']==seljob:
                seljob=len(c['jobs'])
            #basic job info
            cjob={
                'name'  :   loc[job['iname']]['NAME'],
                'LV'    :   str(job['rank']), #if equip wthout empty jm,
                'gears'  :   [],
                'abilities':    []
            }
            #rank
            if cjob['LV']=='11':
                e=0
                for equip in job['equips']:
                    if equip['iid']!=0:
                        e+=1
                if e==6:
                    cjob['LV']='JM'
            #gear
            for gear in job['artis']:
                cjob['gears'].append({
                    'name'  : loc[gear['iname']]['NAME'],
                    'rarity': str(gear['rare']+1),
                })
            #abilities
            for abil in job['abils']:
                cjob['abilities'].append({
                    'name'  : loc[abil['iname']]['NAME'],
                    'lv'    : str(abil['exp']+1),
                })
            c['jobs'].append(cjob)
        c['job']=seljob
        units.append(c)

    #add fields
        #"value": "Lv: 75\tLb: 25\nJobs:\n\tJob1: JM\n\tJob2: JM\n\tJob 3: JM\nGear:\n\tGear 1\n\tGear 2\n\tGear 3",
    for unit in units:
        value = 'LV: {LV}/{maxLV}'.format(LV=str(unit['LV']), maxLV=str(unit['LB']+60))
        #job info
        value+='\n__Jobs:__'
        for i in range(0,len(unit['jobs'])):
            j= unit['jobs'][i]
            cvalue='\n\t'+str(j['name']+': '+j['LV'])
            if i == unit['job']:
                cvalue='**'+cvalue+'**'
            value+=cvalue

        #gear
        value+='\n__Gear:__'
        for g in unit['jobs'][unit['job']]['gears']:
            value+='\n\t'+g['name']+' '+str(g['rarity']+'*')
        #create field
        embed.add_field(name=unit['name'], value=value, inline=True)

    await ctx.send(embed=embed)
    await statistic(ctx, "Rank",input)


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
    await statistic(ctx, "Tierlist")

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


[loc]=loadFiles(['LocalizedMasterParam.json'])
[units,drops,gears,jobs]=convertDBS(loadFiles(['units.json','drops.json','gear.json','jobs.json']))
BOT_TOKEN = os.environ.get('DISCORD_BOT_TOKEN')
bot.run(BOT_TOKEN)
