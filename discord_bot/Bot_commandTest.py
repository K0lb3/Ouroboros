import os.path
import asyncio
import urllib.request
import discord
from discord.ext import commands


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

#log stuff
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
        embed2 = discord.Embed(title="Reaction", description='```'+emoji+'```', color=0x00FF00)
        await msg.edit(embed=embed2)
    
#@bot.event
#async def on_reaction_add(reaction, user):
#    ctx=reaction.message
#    if user != bot.user and ctx.author == bot.user:
#        emoji=reaction.emoji#
#
#        embed2 = discord.Embed(title="Reaction", description='```'+emoji+'```', color=0x00FF00)
#        await ctx.edit(embed=embed2)
@bot.command()
async def emoji(ctx):
    embed = discord.Embed(title="Emoji to Unicode", description='~emoji~', color=8355711)
    await ctx.send(embed=embed)

BOT_TOKEN = os.environ.get('DISCORD_BOT_TOKEN')
bot.run(BOT_TOKEN)
