import os
import discord
import asyncio
import json
import urllib.request
from discord.ext import commands
import aiohttp
import datetime
import re

class Spy_Cog:
	def __init__(self, bot):
		self.bot = bot

	async def on_ready(self):
		print(f'\n\nLogged in as: {self.bot.user.name} - {self.bot.user.id}\nVersion: {discord.__version__}\n')
		print(f'Successfully logged in and booted spy!')

	@commands.command(name='servers', hidden=True)
	@commands.is_owner()
	async def servers(self,ctx):
		for guild in self.bot.guilds:
			embed = discord.Embed(title=str(guild),description=guild.id)
			self_member = guild.get_member(self.bot.user.id)
			try:
				embed.add_field(name='Permissions', value='\n'.join([perm for perm,val in self_member.guild_permissions if val==True]))
			except:	#global blocked, local rights, loop through the channels required -> permission_in(channel)
				pass
			try:
				embed.add_field(name='Invites', value='\n'.join([invite.url for invite in await guild.invites()]))
			except discord.errors.Forbidden as e:
				pass
			await ctx.send(embed=embed)

	@commands.command(name='sharedUsers', hidden=True)
	@commands.is_owner()
	async def sharedUsers(self,ctx,*,input):
		g1,g2 = input.split(' ',1)[:2]
		print(g1)
		print(g2)
		type(self.bot.get_guild(g1))

		g1m = [member.id for member in self.bot.get_guild(int(g1)).members]
		spies = ['<@{id}> - {id}'.format(id=member.id) for member in self.bot.get_guild(int(g2)).members if member.id in g1m]

		await ctx.send('\n'.join(spies))

	@commands.command(name='emojiusage')
	async def emojiusage(self, ctx):
		inp=ctx.message.content.split('emojiusage')[-1].lstrip(' ').rstrip(' ')
		#print(inp)
		sguild=True

		def between(string, pre, post):
			b=string.find(pre)+len(pre)
			e=string[b:].find(post)+b
			if b!=-1 and e!=-1:
				return string[b:e]
			else:
				''

		if '<#' in inp:
			schannel=between(inp, '<#', '>')
			sguild=False
		if '<d' in inp:
			days=between(inp, '<d', '>')
		else:
			days=7
		
		channels=[channel for channel in ctx.guild.channels] if sguild else [schannel]
		after=datetime.datetime.now() - datetime.timedelta(days=int(days))
		emojis={ cemoji.name:0 for cemoji in ctx.guild.emojis}

		cmsg = await ctx.send('This might take a while.\nSettings: Days:%s, Channels: %s'%(days, 'all' if sguild else '<#%s>'%schannel))

		for ci,channel in enumerate(channels):
			# check if text channel and if access to the channel
			if type(channel) != discord.channel.TextChannel or self.bot.user not in channel.members:
				continue
			#local function to fetch messages
			global f_messages
			f_messages=[]
			async def Fetch_Messages(**keys):
				global f_messages
				while True:
					try:
						f_messages = await channel.history(**keys).flatten()
						return f_messages
					except  aiohttp.client_exceptions.ClientOSError:
						pass

			#Message Loop Init
			mc=0
			lusers={}
			before =datetime.datetime.utcnow()
			print(ctx.guild,channel,channel.id,'\n\tNow:','{:%y.%m.%d_%H-%M-%S}'.format(datetime.datetime.utcnow()),'\n\tCLast:',after.date(),'\n\tNLast:','{:%y.%m.%d_%H-%M-%S}'.format(before.date()))
			# function returns messages and changes the variable f_messages
			while len(await Fetch_Messages(limit=1000,after=after,before=before, reverse=False)):
				#print(f_messages)
				#local users for incremental counter increase on db
				for message in f_messages:
					mc+=1
					for cemoji in re.findall(r':(.+?):',message.content):
						cemoji=str(cemoji)
						if cemoji in emojis:
						# if len(cemoji)<33 and cemoji[0]!=' ':
						# 	if cemoji not in emojis:
						# 		emojis[cemoji]=0
							emojis[cemoji]+=1
				before=f_messages[-1]

			embed=discord.Embed(title='Channel %s/%s (%s)'%(ci,len(channels),str(channel)))
			embed.description='```%s```'%'\n'.join([
				'%s.  :%s:  %s'%(i+1,cemoji,cusage)
				for i,(cemoji,cusage) in enumerate(sorted(emojis.items(), key=lambda key:key[1], reverse=True))
			])
			await cmsg.edit(embed=embed)


		# for cemoji in ctx.guild.emojis:
		# 	if not cemoji.name in emojis:
		# 		emojis[cemoji.name]=0

		embed=discord.Embed(title='Emoji Usage in %s during the last %s days'%((str(ctx.guild) if sguild else '<#%s>'%schannel),days))
		embed.description='```%s```'%'\n'.join([
			'%s.  :%s:  %s'%(i+1,cemoji,cusage)
			for i,(cemoji,cusage) in enumerate(sorted(emojis.items(), key=lambda key:key[1], reverse=True))
		])
		if len(embed.description)==0:
			embed.description='No emoji found'
		await cmsg.edit(embed=embed)

# The setup fucntion below is neccesarry. Remember we give self.bot.add_cog() the name of the class in this case MembersCog.
# When we load the cog, we use the name of the file.
def setup(bot):
	bot.add_cog(Spy_Cog(bot))