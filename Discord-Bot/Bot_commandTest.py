import asyncio
import sys
import discord
from discord.ext import commands
from discord.utils import get


#functions


#global vars
prefix='oß'
bot = commands.Bot(command_prefix=prefix)
emojis = []

@bot.event
async def on_ready():
    print('Logged in as')
    print(bot.user.name)
    print(bot.user.id)
    print(bot.guilds)
    print('------')
    
@bot.event
async def on_reaction_add(reaction, user):
    ctx=reaction.message
    if ctx.author == bot.user:
        embed2 = discord.Embed(title="Collab", description='Test2', color=0x00FF00)
        await ctx.edit(embed=embed2)

@bot.command()
async def test2(ctx):
    await ctx.send('on reaction add')

@bot.command()
async def test(ctx):
    embed = discord.Embed(title="Collab Codes", description='Test1', color=8355711)
    embed2 = discord.Embed(title="Collab Codes", description='Test2', color=0x00FF00)

    msg = await ctx.send(embed=embed)
    await msg.add_reaction('⬅️')
    await msg.add_reaction('➡️')

    def check(reaction, user):
        return user == ctx.author and str(reaction.emoji) == '➡️'

    try:
        reaction, user = await bot.wait_for('reaction_add', timeout=10.0, check=check)

        await msg.edit(embed=embed2)

    except asyncio.TimeoutError:
        await ctx.send('TimeoutError')

bot.run('NDU0MDQ5MTYyMTQ1NjkzNjk2.Dfnxwg.nEcjc4TqBb00V41IGRyPeYa0lJ8')
