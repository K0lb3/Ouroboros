import os
import discord
import asyncio
import json
from discord.ext import commands
from utils import FindBest
import datetime
import utils.The_Alchemist_Code as ToEmbed
PAGES=ToEmbed.PAGES

import sys
import pymongo
CLIENT=pymongo.MongoClient("mongodb+srv://{}:{}@discord-bsm0o.mongodb.net/test?retryWrites=true".format(*[os.environ.get(key) for key in ['MONGO_USER','MONGO_PW']]))
DISCORD_DB=CLIENT['DISCORD']	#database
TRACKER=DISCORD_DB['COMMANDS']


async def SearchDB(db,query):
	return [x for x in db.find(query)]

async def UpdateDB(key,val,skey,nval):
	res=[x for x in TRACKER.find({key:val})]
	if len(res)==1:
		TRACKER.update_one({key:val},{'$set':{skey:nval}})
	elif len(res)==0:
		TRACKER.insert_one({key:val,skey:nval})
	else:
		print('shouldn\'t happen\n%s-%s\n%s'%(key,val,res))

class The_Alchemist_Code_Cog:
	def __init__(self, bot):
		self.bot = bot
		self.bot.remove_command('help')

	async def add_reactions(self,msg,reactions):
		for r in reactions:
			await msg.add_reaction(r)

	async def on_ready(self):
		"""http://discordpy.readthedocs.io/en/rewrite/api.html#discord.on_ready"""
		print(f'\n\nLogged in as: {self.bot.user.name} - {self.bot.user.id}\nVersion: {discord.__version__}\n')
		# Changes our bots Playing Status. type=1(streaming) for a standard game you could remove type and url.
		#await bot.change_presence(game=discord.Game(name='Cogs Example', type=1, url='https://twitch.tv/kraken'))
		print(f'Successfully logged in and booted...!')
		await self.bot.change_presence(status=discord.Status.online, activity=discord.Game('o.help'))

	async def on_message(self,message):
		content=message.content
		preLen=len(self.bot.command_prefix)
		if content[:preLen]==self.bot.command_prefix:
			#Identify command and input
			command = content[preLen:]
			inp=''
			if ' ' in command:
				command,inp = command.split(' ',1)
			#Increase DB counter
			timestamp='{:%d.%m.%y | %H-%M-%S}'.format(datetime.datetime.utcnow())
			ret = await SearchDB(TRACKER,{'_id':command})
			if ret:
				ret=ret[0]
				msgs=ret['msgs']
				msgs.append([timestamp,inp])
			else:
				msgs=[[timestamp,inp]]
			await UpdateDB('_id',command,'msgs',msgs)

			#Create and send log
			embed=discord.Embed(title='%s - %s'%(command.upper(),len(msgs)))
			embed.add_field(name='Server',	value=message.guild)
			embed.add_field(name='Channel',	value=message.channel)
			embed.add_field(name='Command',	value=command)
			if inp:
				embed.add_field(name='Input',	value=inp)
			embed.add_field(name='Time',	value=timestamp, inline=False)

			log=self.bot.get_channel(457645688583618560)
			await	log.send(embed=embed)

	####	AI		######################################################################################
	@commands.command(name='ai',description='displays boundary conditions and settings of the ai')
	async def ai(self,ctx,*,name):
		ai = FindBest(ToEmbed.DIRS['Ai'], name,['iname'], True)
		msg = await ctx.send(embed=ToEmbed.AI(ai,'main'))
		#await self.add_reactions(msg,PAGES['item'])

	####	QUESTS  #######################################################################################
	# @commands.group(pass_context=True)
	# async def Quests(self,ctx):
	#	 if ctx.invoked_subcommand is None:
	#		 await self.bot.say('Invalid command passed...')
	@commands.command(name='quest',description='displays quest informations') 
	async def quest(self,ctx, *, name):
		quest = FindBest(ToEmbed.DIRS['Quests'], name,['name','iname'],True)
		(embed,image)=ToEmbed.Quest(quest,'main')

		if image:
			msg = await ctx.send(embed=embed,file=discord.File(image,filename='{}.png'.format(quest)))
		else:
			msg = await ctx.send(embed=embed)
		await self.add_reactions(msg,PAGES['quest'])


	@commands.command(name='map',description='displays map and enemies of the mission') 
	async def map(self,ctx, *, name):
		quest = FindBest(ToEmbed.DIRS['Quests'], name,['name','iname'],True)
		(embed,image)=ToEmbed.Quest(quest,'map')

		if image:
			msg = await ctx.send(embed=embed,file=discord.File(image,filename='{}.png'.format(quest)))
		else:
			msg = await ctx.send(embed=embed)
		await self.add_reactions(msg,PAGES['quest'])

	@commands.command(name='story',description='shows the conversations of the mission') 
	async def story(self,ctx, *, name):
		quest = FindBest(ToEmbed.DIRS['Quests'], name,['iname','name'],True)
		(embed,limit)=ToEmbed.Quest(quest,'story')
		if not limit:
			msg = await ctx.send(embed=embed)
			#await self.add_reactions(msg,PAGES['quest'])
		else:
			for e in embed:
				await ctx.send(embed=e)


	####	ITEMS   #########################################################################################
	# @commands.group(pass_context=True)
	# async def Items(self,ctx):
	#	 if ctx.invoked_subcommand is None:
	#		 await self.bot.say('Invalid command passed...')

	@commands.command(name='item',description='displays data of the item')
	async def item(self,ctx, *, name):
		item = FindBest(ToEmbed.DIRS['Item'], name,['name'], True)
		msg = await ctx.send(embed=ToEmbed.Item(item,'main'))
		await self.add_reactions(msg,PAGES['item'])

	@commands.command(name='farm',aliases=['grind','drop'],description='shows missions which drop the item')
	async def farm(self,ctx, *, name):
		item = FindBest(ToEmbed.DIRS['Item'], name,['name'], True)
		msg = await ctx.send(embed=ToEmbed.Item(item,'Story'))
		await self.add_reactions(msg,PAGES['item'])

	####	ConceptCards	################################################################################
	# @commands.group(pass_context=True)
	# async def Conceptcards(self,ctx):
	#	 if ctx.invoked_subcommand is None:
	#		 await self.bot.say('Invalid command passed...')

	@commands.command(name='nensou',aliases=['conceptcard','concept card','card'],description='displays stats of the concept card')
	async def nensou(self,ctx,*,name):
		card = FindBest(ToEmbed.DIRS['Conceptcard'], name,['keywords','iname'], True)
		msg = await ctx.send(embed=ToEmbed.Conceptcard(card,'main'))
		await self.add_reactions(msg,PAGES['conceptcard'])

	####	Units   ########################################################################################
	# @commands.group(pass_context=True)
	# async def Units(self,ctx):
	#	 if ctx.invoked_subcommand is None:
	#		 await self.bot.say('Invalid command passed...')

	@commands.command(name='unit',description='displays key data of the unit')
	async def unit(self,ctx, *, name):
		unit = FindBest(ToEmbed.DIRS['Unit'], name,['name','nameJP','keywords'], True)
		msg = await ctx.send(embed=ToEmbed.Unit(unit,'main'))
		await self.add_reactions(msg,PAGES['unit'])

	@commands.command(name='lore',description='shows the lore of the unit')
	async def lore(self,ctx,*,name):
		unit = FindBest(ToEmbed.DIRS['Unit'], name,['name','nameJP','keywords'], True)
		msg = await ctx.send(embed=ToEmbed.Unit(unit,'lore'))
		await self.add_reactions(msg,PAGES['unit'])

	@commands.command(name='kaigan',aliases=['enlightment'],description='displays the kaigan stats and skills of the unit')
	async def kaigan(self,ctx, *, name):
		unit = FindBest(ToEmbed.DIRS['Unit'], name,['name','nameJP','keywords'], True)
		msg = await ctx.send(embed=ToEmbed.Unit(unit,'kaigan'))
		await self.add_reactions(msg,PAGES['unit'])

	@commands.command(name='art',aliases=['artwork'],description='posts all images of the unit')
	async def art(self,ctx,*,name):
		unit = FindBest(ToEmbed.DIRS['Unit'], name,['name','nameJP','keywords'], True)
		for embed in ToEmbed.Unit(unit,'art'):
			await ctx.send(embed=embed)

	####	JOBS	#########################################################################################
	# @commands.group(pass_context=True)
	# async def Jobs(self,ctx):
	#	 if ctx.invoked_subcommand is None:
	#		 await self.bot.say('Invalid command passed...')
	
	@commands.command(name='job',description='displays details of the job')
	async def job(self,ctx, *, name):
		job = FindBest(ToEmbed.DIRS['Job'], name,['keywords'],True)
		msg = await ctx.send(embed= ToEmbed.Job(job, 'main'))
		await self.add_reactions(msg,PAGES['job'])

	####	GEAR	#########################################################################################
	# @commands.group(pass_context=True)
	# async def Gear(self,ctx):
	#	 if ctx.invoked_subcommand is None:
	#		 await self.bot.say('Invalid command passed...')
	
	@commands.command(name='gear',aliases=['arments','equip'],description='displays stats of the gear')
	async def gear(self,ctx, *, name):
		gear = FindBest(ToEmbed.DIRS['Artifact'], name,['keywords'],True)
		msg = await ctx.send(embed= ToEmbed.Gear(gear, 'main'))
		await self.add_reactions(msg,PAGES['gear'])

	####	ARENA   ##########################################################################################
	# @commands.group(pass_context=True)
	# async def Arena(self,ctx):
	#	 if ctx.invoked_subcommand is None:
	#		 await self.bot.say('Invalid command passed...')

	@commands.command(name='arena',aliases=['gr','ranking'],description='shows the current top 50 arena ranking')
	async def arena(self,ctx):
		await ctx.send(embed=ToEmbed.Arena())

	@commands.command(name='arena_map',aliases=['arenamap','amap','am'],description='displays the current arena map') 
	async def arena_map(self,ctx):
		(embed,image)=ToEmbed.Arena_Map()
		if image:
			await ctx.send(embed=embed,file=discord.File(image,filename='{}.png'.format(embed.footer.text)))

	@commands.command(name='rank',aliases=['enemy'],description='shows units and their gear of the selected rank (top 50)')
	async def rank(self,ctx,*,name):
		await ctx.send(embed=ToEmbed.Rank(name))

	####	ETC ##############################################################################################
	# @commands.group(pass_context=True)
	# async def ETC(self,ctx):
	#	 if ctx.invoked_subcommand is None:
	#		 await self.bot.say('Invalid command passed...')

	@commands.command(name='emoji',description='converts reaction to unicode (for copypaste)')
	async def emoji(self,ctx):
		embed = discord.Embed(title="Emoji to Unicode", description='~emoji~', color=8355711)
		embed.set_footer(text='Emoji-Converter')
		await ctx.send(embed=embed)

	@commands.command(name='info',description='shows informations about the bot and holds the invite link')
	async def info(self,ctx):
			"""Bot Info"""
			embed = discord.Embed()
			embed.add_field(name="Author", value='<@281201917802315776>')
			embed.add_field(name="Library", value='discord.py (Python)')
			embed.add_field(name='Invite Link', value="[\\o/ (hidden link)](https://discordapp.com/oauth2/authorize?client_id=437868948130758668&scope=bot 'can only be invited by the server admin')")
			embed.add_field(name="Servers", value="{} servers".format(len(self.bot.guilds)))

			total_members = sum(len(s.members) for s in self.bot.guilds)
			total_online = sum(1 for m in self.bot.get_all_members() if m.status != discord.Status.offline)
			unique_members = set(map(lambda x: x.id, self.bot.get_all_members()))
			embed.add_field(name="Total Members", value='{} ({} online)'.format(total_members, total_online))
			embed.add_field(name="Unique Members", value='{}'.format(len(unique_members)))

			embed.set_footer(text='Made with discord.py', icon_url='http://i.imgur.com/5BFecvA.png')
			embed.set_thumbnail(url=self.bot.user.avatar_url)
			await ctx.send(delete_after=60, embed=embed) 

	@commands.command(name='help',description='shows description of all commands')
	async def help(self,ctx):
		embed=discord.Embed()
		for command in sorted(list(self.bot.commands), key=lambda c: c.name):
			name=command.name
			aliases=' ,'.join(command.aliases)
			description=command.description
			embed.add_field(name='%s%s'%(name,' [alias: %s]'%aliases if aliases else ''),value=description if description else '/',inline=False)
		await ctx.send(embed=embed)

	####	REACTIONS   ######################################################################################
	async def on_raw_reaction_add(self,payload):
		#message_id #int – The message ID that got or lost a reaction.
		#user_id #int – The user ID who added or removed the reaction.
		#channel_id #int – The channel ID where the reaction got added or removed.
		#guild_id #Optional[int] – The guild ID where the reaction got added or removed, if applicable.
		#emoji #PartialEmoji – The custom or unicode emoji being used.
		try:
			msg = await self.bot.get_channel(payload.channel_id).get_message(payload.message_id)
		except:
			print('Failed to handle {payload},{msg}\nJump-Url:{URL}'.format(
					payload=payload,
					msg=payload.message_id,
					URL='https://discordapp.com/channels/{server}/{channel}/{message}'.format(
						server=payload.guild,
						channel=payload.channel_id,
						message=payload.message_id
						)
					)
				)
			return 0

		if payload.user_id != self.bot.user.id and msg.author == self.bot.user:
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
			elif ftext[:3] == 'Job':
				job = ftext[4:]
				await msg.edit(embed=ToEmbed.Job(job,PAGES['job'][emoji]))
			elif ftext == 'Item':
				item = FindBest(ToEmbed.DIRS['Item'], etitle)
				await msg.edit(embed=ToEmbed.Item(item,PAGES['item'][emoji]))
			elif ftext[:5] == 'Quest':
				quest = ftext[8:]
				await msg.edit(embed=ToEmbed.Quest(quest,PAGES['quest'][emoji],True))

# The setup fucntion below is neccesarry. Remember we give self.bot.add_cog() the name of the class in this case MembersCog.
# When we load the cog, we use the name of the file.
def setup(bot):
	bot.add_cog(The_Alchemist_Code_Cog(bot))