import discord
from discord.ext import commands
from os import environ
import asyncio
import sys, traceback

"""This is a multi file example showcasing many features of the command extension and the use of cogs.
These are examples only and are not intended to be used as a fully functioning bot. Rather they should give you a basic
understanding and platform for creating your own bot.

These examples make use of Python 3.6.2 and the rewrite version on the lib.

For examples on cogs for the async version:
https://gist.github.com/leovoel/46cd89ed6a8f41fd09c5

Rewrite Documentation:
http://discordpy.readthedocs.io/en/rewrite/api.html

Rewrite Commands Documentation:
http://discordpy.readthedocs.io/en/rewrite/ext/commands/api.html

Familiarising yourself with the documentation will greatly help you in creating your bot and using cogs.
"""


def get_prefix(bot, message):
	"""A callable Prefix for our bot. This could be edited to allow per server prefixes."""

	# Notice how you can use spaces in prefixes. Try to keep them simple though.
	prefixes = ['o.', 'o?','!','.','o!','?']

	# Check to see if we are outside of a guild. e.g DM's etc.
	#if not message.guild:
		# Only allow ? to be used in DMs
	#	return '.'

	# If we are in a guild, we allow for the user to mention us or use any of the prefixes in our list.
	return commands.when_mentioned_or(*prefixes)(bot, message)


# Below cogs represents our folder our cogs are in. Following is the file name. So 'meme.py' in cogs, would be cogs.meme
# Think of it like a dot path import
#initial_extensions = ['cogs.The_Alchemist_Code',
#					  'cogs.TAC_Discord'
#					  ]

TAC_BOT = commands.Bot(command_prefix='o.', description='The Alchemist Code bot, written by W0lf in discord.py')
TAC_DISCORD = commands.Bot(command_prefix='!', description='The Alchemist Code discord bot, written by W0lf in discord.py')
# Here we load our extensions(cogs) listed above in [initial_extensions].
if __name__ == '__main__':
	TAC_BOT.load_extension('cogs.The_Alchemist_Code')
	TAC_BOT.load_extension('cogs.Spy')
	TAC_DISCORD.load_extension('cogs.TAC_Discord')
	TAC_DISCORD.load_extension('cogs.TAC_FID')
	TAC_DISCORD.load_extension('cogs.Spy')
	# for extension in initial_extensions:
	# 	try:
	# 		bot.load_extension(extension)
	# 	except Exception as e:
	# 		print(f'Failed to load extension {extension}.', file=sys.stderr)
	# 		traceback.print_exc()

#Create the loop
loop = asyncio.get_event_loop()

#Create each bot task
loop.create_task(TAC_DISCORD.start(environ.get('DISCORD_BOT_TOKEN_ROLES'), bot=True, reconnect=True, case_sensitive=False))
loop.create_task(TAC_BOT.start(environ.get('DISCORD_BOT_TOKEN'), bot=True, reconnect=True, case_sensitive=False))

#Run loop and catch exception to stop it
try:
	loop.run_forever()
finally:
	loop.stop()
