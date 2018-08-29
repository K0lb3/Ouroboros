import os
import discord
import asyncio
import json
from discord.ext import commands
from functions import FindBest
import ToEmbed
from settings import BOT_TOKEN,PRESENCES, PAGES

# Constants
prefix='o.'
bot = commands.Bot(command_prefix=prefix)

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
    #bot.loop.create_task(status_task(PRESENCES))

#WIP
@bot.command() 
async def quest(ctx, *, name):
    quest = FindBest(ToEmbed.DIRS['Quests'], name,['name'],True)
    (embed,image)=ToEmbed.Quest(quest,'main')

    if image:
        await ctx.send(embed=embed,file=discord.File(image,filename='{}.png'.format(quest)))
    else:
        await ctx.send(embed=embed)


@bot.command()
async def item(ctx, *, name):
    item = FindBest(ToEmbed.DIRS['Item'], name,['name'], True)
    msg = await ctx.send(embed=ToEmbed.Item(item,'main'))
    await add_reactions(msg,PAGES['item'])

@bot.command()
async def farm(ctx, *, name):
    item = FindBest(ToEmbed.DIRS['Item'], name,['name'], True)
    msg = await ctx.send(embed=ToEmbed.Item(item,'Story'))
    await add_reactions(msg,PAGES['item'])

#done

@bot.command()
async def unit(ctx, *, name):
    unit = FindBest(ToEmbed.DIRS['Unit'], name,['name'], True)
    msg = await ctx.send(embed=ToEmbed.Unit(unit,'main'))
    await add_reactions(msg,PAGES['unit'])

@bot.command()
async def kaigan(ctx, *, name):
    unit = FindBest(ToEmbed.DIRS['Unit'], name,['name'], True)
    msg = await ctx.send(embed=ToEmbed.Unit(unit,'kaigan'))
    await add_reactions(msg,PAGES['unit'])

@bot.command()
async def nensou(ctx,*,name):
    card = FindBest(ToEmbed.DIRS['Conceptcard'], name,['name','unit','iname'], True)
    msg = await ctx.send(embed=ToEmbed.Conceptcard(card,'main'))
    await add_reactions(msg,PAGES['conceptcard'])

@bot.command()
async def job(ctx, *, name):
    job = FindBest(ToEmbed.DIRS['Job'], name,['name'],True)
    msg = await ctx.send(embed= ToEmbed.Job(job, 'main'))
    await add_reactions(msg,PAGES['job'])

@bot.command()
async def gear(ctx, *, name):
    gear = FindBest(ToEmbed.DIRS['Artifact'], name,['name'],True)
    msg = await ctx.send(embed= ToEmbed.Gear(gear, 'main'))
    await add_reactions(msg,PAGES['gear'])

@bot.command()
async def lore(ctx,*,name):
    unit = FindBest(ToEmbed.DIRS['Unit'], name,['name'], True)
    msg = await ctx.send(embed=ToEmbed.Unit(unit,'lore'))
    await add_reactions(msg,PAGES['unit'])

@bot.command()
async def art(ctx,*,name):
    unit = FindBest(ToEmbed.DIRS['Unit'], name,['name'], True)
    for embed in ToEmbed.Unit(unit,'art'):
        await ctx.send(embed=embed)



@bot.event
async def on_raw_reaction_add(payload):
    #message_id #int – The message ID that got or lost a reaction.
    #user_id #int – The user ID who added or removed the reaction.
    #channel_id #int – The channel ID where the reaction got added or removed.
    #guild_id #Optional[int] – The guild ID where the reaction got added or removed, if applicable.
    #emoji #PartialEmoji – The custom or unicode emoji being used.
    msg = await bot.get_channel(payload.channel_id).get_message(payload.message_id)
    if payload.user_id != bot.user.id and msg.author == bot.user:
        
        emoji = payload.emoji.name
        ftext=msg.embeds[0].footer.text
        etitle = msg.embeds[0].author.name

        if ftext == 'Emoji-Converter':
            embed2 = discord.Embed(title="Reaction", description='```'+emoji+'```', color=0x00FF00)
            embed2.set_footer(text='Emoji-Converter')
            await msg.edit(embed=embed2)

        elif ftext == 'Unit':
                unit = FindBest(ToEmbed.DIRS['Unit'], etitle)
                await msg.edit(embed=ToEmbed.Unit(unit,PAGES['unit'][emoji]))
        elif ftext == 'Job':
                job = FindBest(ToEmbed.DIRS['Job'], etitle)
                await msg.edit(embed=ToEmbed.Job(job,PAGES['job'][emoji]))
        elif ftext == 'Item':
                item = FindBest(ToEmbed.DIRS['Item'], etitle)
                await msg.edit(embed=ToEmbed.Item(item,PAGES['item'][emoji]))
    
@bot.command()
async def emoji(ctx):
    embed = discord.Embed(title="Emoji to Unicode", description='~emoji~', color=8355711)
    embed.set_footer(text='Emoji-Converter')
    await ctx.send(embed=embed)

bot.run(BOT_TOKEN)
