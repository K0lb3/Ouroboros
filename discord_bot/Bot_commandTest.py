import asyncio
import sys
import discord
from discord.ext import commands


#functions


#global vars
prefix='oß'
bot = commands.Bot(command_prefix=prefix)

@bot.event
async def on_ready():
    print('Logged in as')
    print(bot.user.name)
    print(bot.user.id)
    print(bot.guilds)
    print('------')
    

@bot.command()
async def collab(ctx):
    embed = discord.Embed(title="Collab Codes", description="for unit commands \n \n"+
        'Fate : Fate/Stay Night [UBW] \n'+
        'FA   : Fullmetal Alchemist \n'+
        'RH   : Radiant Historia \n'+
        'POTK : Phantom Of The Kill \n'+
        'SN   : Shinobi Nightmare \n'+
        'FF   : Final Fantasy 15 \n'+
        'DIS  : Disgea \n'+
        'EO   : Etrian Odyssey \n'+
        'EMD  : Etrian Mystery Dungeon',
        color=8355711)
    await ctx.send(embed=embed)

    def check(reaction, user):
        return user == ctx.author and str(reaction.emoji) == '\N{THUMBS UP SIGN}'

    try:
        reaction, user = await bot.wait_for('reaction_add', timeout=60.0, check=check)

        await ctx.send('\N{THUMBS UP SIGN}')
    except asyncio.TimeoutError:
        await ctx.send('\N{THUMBS DOWN SIGN}')

bot.run('NDU0MDQ5MTYyMTQ1NjkzNjk2.Dfnxwg.nEcjc4TqBb00V41IGRyPeYa0lJ8')
