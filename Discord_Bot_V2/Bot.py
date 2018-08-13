import os
import discord
import asyncio
import json
from discord.ext import commands
from functions import *
import ToEmbed
from settings import *

# Constants
prefix='o?'
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

@bot.command() 
async def quest(ctx, *, name):
    quest = FindBest(ToEmbed.DIRS['Quests'], name,True)
    (embed,image)=ToEmbed.quest(quest,'')
    await ctx.send(embed=embed,file=discord.File(image,filename='{}.png'.format(quest)))

bot.run(BOT_TOKEN)
