import discord
from discord.ext import commands
from os import environ
import asyncio
import sys, traceback

TAC_BOT = commands.Bot(command_prefix='o.', description='The Alchemist Code bot, written by W0lf in discord.py')
# Here we load our extensions(cogs) listed above in [initial_extensions].
if __name__ == '__main__':
	TAC_BOT.load_extension('cogs.The_Alchemist_Code')
	TAC_BOT.load_extension('cogs.Spy')
	TAC_BOT.run(environ.get('DISCORD_BOT_TOKEN'), bot=True, reconnect=True, case_sensitive=False)