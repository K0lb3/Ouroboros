import discord
from discord.ext import commands
from os import environ
import asyncio
import sys, traceback

TAC_DISCORD = commands.Bot(command_prefix='!', description='The Alchemist Code discord bot, written by W0lf in discord.py')
# Here we load our extensions(cogs) listed above in [initial_extensions].
if __name__ == '__main__':
	TAC_DISCORD.load_extension('cogs.TAC_Discord')
	TAC_DISCORD.load_extension('cogs.TAC_FID')
	TAC_DISCORD.load_extension('cogs.Spy')
	TAC_DISCORD.run(environ.get('DISCORD_BOT_TOKEN_ROLES'), bot=True, reconnect=True, case_sensitive=False)