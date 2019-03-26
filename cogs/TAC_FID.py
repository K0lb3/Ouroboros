import os
import copy
import discord
import asyncio
import aiohttp
import calendar
import random
import urllib.request
import datetime
import pymongo
import json
from discord.ext import commands
from utils.FindBest import FindBest
from utils.The_Alchemist_Code.TAC_API import TAC_API

SERVER_ID=360289051011710979
WELCOME_ID=0
ALCHEMIST_GL_ID=0
ALCHEMIST_JP_ID=0

PATH=os.path.dirname(os.path.realpath(__file__))
TAC_GL=TAC_API(api="app.alcww.gumi.sg",secret_key="965d6a95-cf7d-4f05-8735-8de0c85f4bf5",device_id="80c26c32-8729-4403-9b44-ace4123517a6",idfv="c0af330e-b6ce-39a4-86b8-0aa21f8041df",idfa="d2d39a1e-d2c2-4b7d-9604-8ea664145be3")
TAC_JP=TAC_API(api='alchemist.gu3.jp',secret_key="3f573a89-7bed-4dc3-af7c-0c11332202df",device_id="251ec8a9-2eae-408c-a900-ef999b7a4f49",idfv="3ca48ec7-2081-4339-9fa5-dd99bb423851",idfa="3cc59e0f-60f4-444f-8378-dddbb8735aa5")
#Variables
roles=[]
CLIENT=pymongo.MongoClient("mongodb+srv://{}:{}@discord-bsm0o.mongodb.net/test?retryWrites=true".format(*[os.environ.get(key) for key in ['MONGO_USER','MONGO_PW']]))
#FID system
DISCORD_DB=CLIENT['DISCORD']	#database
FID=DISCORD_DB['FID']
#user statistics
DUSER=CLIENT['DISCORD_USER']



OWNERSHIP_TEST={
	'unit':'change your rainbow merc',
	'job':'change the job of your rainbow merc',
	'sub ability':'change the sub ability of your rainbow merc',
	'reactive':'the reactive of your rainbow merc',
	'unequip all gear':'unequip all gear of your rainbow merc',
	'equip any gear':'equip any gear on your rainbow merc'
}


async def SearchCollection(col,query):
	return [x for x in col.find(query)]

async def AddToCollection(item):
	FID.insert_one(item)

async def UpdateCollection(key,val,skey,nval):
	res=[x for x in FID.find({key:val})]
	if len(res)==1:
		FID.update_one({key:val},{'$set':{skey:nval}})
	elif len(res)==0:
		await AddToCollection({key:val,skey:nval})
	else:
		print('shouldn\'t happen\n%s-%s\n%s'%(key,val,res))

class TAC_Discord_Cog:
	def __init__(self, bot):
		self.bot = bot

	async def on_ready(self):
		print(f'\n\nLogged in as: {self.bot.user.name} - {self.bot.user.id}\nVersion: {discord.__version__}\n')
		print(f'Successfully logged in and booted...!')
		#await self.bot.change_presence(status=discord.Status.online, activity=discord.Game('!role'))
		while True:
			await self.MessageCount()
			await asyncio.sleep(60*30)



	async def add_reactions(self,msg,reactions):
		for r in reactions:
			await msg.add_reaction(r)

	async def MessageCount(self):
		#loop through guilds
		for guild in self.bot.guilds:
			#select collection
			col = DUSER[str(guild.id)]
			#download user stats from mongodb
			users={item['_id']:item for item in col.find()}

			#loop through guild channels
			for channel in guild.channels:
				# check if text channel and if access to the channel
				if type(channel) != discord.channel.TextChannel or self.bot.user not in channel.members:
					continue
				#fetch last checked message from last round
				FormerLastMessage = await SearchCollection(col,{'_id':channel.id})
				
				if len(FormerLastMessage):
					FormerLastMessage = FormerLastMessage[0]['last_message']
				else:
					FormerLastMessage = 0
					col.insert_one({'_id':channel.id,'last_message':0})

				CurrentLastMessage = datetime.datetime.utcnow()
				
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
				before = CurrentLastMessage
				print(guild,channel,channel.id,'\n\tNow:','{:%y.%m.%d_%H-%M-%S}'.format(datetime.datetime.utcnow()),'\n\tCLast:',FormerLastMessage,'\n\tNLast:','{:%y.%m.%d_%H-%M-%S}'.format(CurrentLastMessage))
				# function returns messages and changes the variable f_messages
				while len(await Fetch_Messages(limit=1000,after=FormerLastMessage,before=before, reverse=False)):
					#print(f_messages)
					#local users for incremental counter increase on db
					for message in f_messages:
						mc+=1
						#user already registered?
						if message.author.id not in lusers:	#not during last messages
							lusers[message.author.id]=[0,0]
							if message.author.id not in users:	#never so far
								col.insert_one({'_id':message.author.id,'message_count':0,'total_characters':0})
								users[message.author.id]={'message_count':0,'total_characters':0}
						#increase user counters
						lusers[message.author.id][0]+=1
						lusers[message.author.id][1]+=len(message.content)
					
					before=f_messages[-1]


				print('\tMessages:',mc)
				#increase user db counters
				for userid,(mcount,mlen) in lusers.items():
					col.update_one({'_id':userid},{'$inc':{'message_count':mcount,'total_characters':mlen}})
				#change last message on db
				col.update_one({'_id':channel.id},{'$set':{'last_message':CurrentLastMessage}})

	#roles
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

		emoji = payload.emoji.name
		guild = self.bot.get_guild(payload.guild_id)
		member= guild.get_member(payload.user_id)
		#new member - side decision
		if payload.message_id == WELCOME_ID:
			role_des=False
			if emoji=='🇬':	#GLOBAL
				role_des=guild.get_role(ALCHEMIST_GL_ID)
			elif emoji=='🇯🇵': #JAPAN
				role_des=guild.get_role(ALCHEMIST_JP_ID)
			if role_des:
				await member.add_roles(role_des)

	@commands.command()
	async def mystats(self,ctx):
		embed=discord.Embed(title=ctx.author.name)

		acount=list(DUSER[str(ctx.guild.id)].find({'_id':ctx.author.id}))
		mcount=acount[0]['message_count'] if acount else 0
		ccount=acount[0]['total_characters'] if acount else 0
		

		description=[
			'member since:\t{:%d/%m/%Y}'.format(ctx.author.joined_at),
			'messages:\t%s'%mcount,
			'characters:\t%s'%ccount,
			'chr/msg:\t%s'%(round(ccount/mcount*100)/100),
			'msg/day:\t%s'%(round(mcount/(datetime.datetime.utcnow()-ctx.author.joined_at).days*100)/100)
		]
		ret = await SearchCollection(FID,{'_id':ctx.author.id})
		if ret:
			if ret[0]['gl']:
				tacc=TAC_GL.req_friend_fuid(fuid=ret[0]['gl'])
				description.append('Global:\t%s (LV: %s, FID: %s)'%(
					tacc['name'],
					tacc['lv'],
					ret[0]['gl']
					)
				)
				#playing since
			if ret[0]['jp']:
				tacc=TAC_JP.req_friend_fuid(fuid=ret[0]['jp'])
				description.append('Tagatame:\t%s (LV: %s, FID: %s)'%(
					tacc['name'],
					tacc['lv'],
					ret[0]['jp']
					)
				)
		else:
			description.append('no registered account')

		embed.description='```%s```'%('\n'.join(description))
		await ctx.send(embed=embed)

		try:
			if mcount>5000:
				await ctx.author.add_roles(ctx.guild.get_role(526054421902131210))
		except Exception as e:
			print(e)

	@commands.command()
	async def leaderboard(self,ctx):
		embed=discord.Embed(title='%s - Leaderboard'%str(ctx.guild))
		dbusers = [item for item in DUSER[str(ctx.guild.id)].find()]

		users=sorted( [(item['_id'],item['message_count']) for item in dbusers if 'message_count' in item],  key=lambda key:key[1], reverse=True)

		top=25
		try:
			inp=ctx.message.content.split('leaderboard')[-1].lstrip(' ').rstrip(' ')
			try:
				top=int(inp)
			except:
				if inp[0]=='a':
					top=int(inp[1:])
					users=[item for item in users if item[1]>top]
					embed.description='%s users\n%s'%(
						len(users),
						','.join(['<@%s>'%str(item[0]) for item in users])
					)[:2000]
					await ctx.send(embed=embed)
					return
			
				elif inp[0]=='c':
					users=sorted( [(item['_id'],item['total_characters']) for item in dbusers if 'total_characters' in item],  key=lambda key:key[1], reverse=True)
					embed.title+= ' total characters'

				elif inp[0]=='r':
					users=sorted( [(item['_id'],round(100*item['total_characters']/item['message_count'])/100 ) for item in dbusers if 'total_characters' in item],  key=lambda key:key[1], reverse=True)
					embed.title+= ' characters/message'

		except Exception as e:
			print(e)

		embed.description='\n'.join([
			'%s:  <@%s>    %s'%(i+1,*item)
			for i,item in enumerate(users[:top])
		])[:2000]

		await ctx.send(embed=embed)

	@commands.command()
	async def registerGL(self,ctx,*,inp:str):
		#look in db
		inp = inp.lstrip(' ').rstrip(' ')
		if len(inp)!=8:
			await ctx.send('Invalid FID')
			return

		ret = await SearchCollection(FID,{'_id':ctx.author.id})
		if ret and ret[0]['gl']:
			await ctx.send('Your discord account is already linked with the FID %s'%ret[0]['gl'])
			return

		if await SearchCollection(FID,{'gl':inp}):
			await ctx.send('This account was already registered by someone else.')
			return
		#check ingame
		friend= TAC_GL.req_friend_fuid(inp)
		if 'stat' in friend:
			await ctx.send('```json\nInvalid FID: %s\n%s'%(inp,str(friend)))
			return

		#test ownership
		if friend['lv']>100:
			#random test
			while True:
				(typ,req)=random.choice(list(OWNERSHIP_TEST.items()))
				if ownership_check_validiation(typ,friend):
					break

			rep = await ctx.send('The account is over level 100, please %s to proof that it is your account.\nThe account will be checked in 2 minutes or when you add a reaction to this message before the time is up.'%req)


			def check(reaction, user):
				return user == ctx.author

			try:
				reaction, user = await self.bot.wait_for('reaction_add', timeout=120.0, check=check)
			except asyncio.TimeoutError:
				await ctx.send('Time is up.')
			finally:
				friend2=TAC_GL.req_friend_fuid(inp)
				if ownership_check(typ,friend,friend2):
					#register account
					if not ret:
						ret={'_id':ctx.author.id,'gl':inp,'jp':''}
						await AddToCollection(ret)
					else:
						await UpdateCollection('_id',ctx.author.id,'gl',inp)

					await ctx.send('The FID is now connected to your discord account')
				
				else:
					await ctx.send('The required changes weren\'t made')
			

	@commands.command()
	async def checkFIDGL(self,ctx,*,inp:str):
		friend= TAC_GL.req_friend_fuid(inp)
		if 'stat' in friend:
			await ctx.send('```json\nInvalid FID: %s\n%s'%(inp,str(friend)))
			return
		else:
			await ctx.send('```%s```'%'\n'.join([
				'Name:\t%s'%friend['name'],
				'LV:\t%s'%friend['lv'],
				'Last Login:\t%s'%friend['lastlogin']
			]
			))
			return

	@commands.command()
	async def checkFIDJP(self,ctx,*,inp:str):
		friend= TAC_JP.req_friend_fuid(inp)
		if 'stat' in friend:
			await ctx.send('```json\nInvalid FID: %s\n%s'%(inp,str(friend)))
			return
		else:
			await ctx.send('```%s```'%'\n'.join([
				'Name:\t%s'%friend['name'],
				'LV:\t%s'%friend['lv'],
				'Last Login:\t%s'%friend['lastlogin']
			]
			))
			return


	@commands.command()
	async def registerJP(self,ctx,*,inp:str):
		#look in db
		inp = inp.lstrip(' ').rstrip(' ')
		if len(inp)!=8:
			await ctx.send('Invalid FID')
			return
		ret=await SearchCollection(FID,{'_id':ctx.author.id})
		if ret and ret[0]['jp']:
			await ctx.send('Your discord account is already linked with the FID %s'%ret[0]['jp'])
			return

		if await SearchCollection(FID,{'jp':inp}):
			await ctx.send('This account was already registered by someone else.')
			return
		#check ingame
		friend= TAC_JP.req_friend_fuid(inp)
		if 'stat' in friend:
			await ctx.send('```json\nInvalid FID: %s\n%s'%(inp,str(friend)))
			return

		#test ownership
		if friend['lv']>100:
			#random test
			while True:
				(typ,req)=random.choice(list(OWNERSHIP_TEST.items()))
				if ownership_check_validiation(typ,friend):
					break

			rep = await ctx.send('The account is over level 100, please %s to proof that it is your account.\nThe account will be checked in 2 minutes or when an reaction is added to this message'%req)


			def check(reaction, user):
				return user == ctx.author

			try:
				reaction, user = await self.bot.wait_for('reaction_add', timeout=120.0, check=check)
			except asyncio.TimeoutError:
				await ctx.send('Time is up.')
			finally:
				friend2=TAC_JP.req_friend_fuid(inp)
				if ownership_check(typ,friend,friend2):
					#register account
					if not ret:
						ret={'_id':ctx.author.id,'gl':'','jp':inp}
						await AddToCollection(ret)
					else:
						await UpdateCollection('_id',ctx.author.id,'jp',inp)

					await ctx.send('The FID is now connected to your discord account')
				
				else:
					await ctx.send('The required changes weren\'t made')

	@commands.command(name='uploadDB', hidden=True)
	@commands.is_owner()
	async def uploadDB(self,ctx):
		ret = await SearchCollection(FID,{})
		f = discord.File(fp=json.dumps(ret,indent='\t',ensure_ascii=False).encode('utf8'),filename='FID_db.json')
		await ctx.send(file=f)

####	other code
def ownership_check_validiation(mission,friend):
	if mission=='job' or mission=='sub ability' or mission=='reactive':
		return len(friend['unit']['jobs'])

	for ji,job in enumerate(friend['unit']['jobs']):
		if job['iid'] == friend['unit']['select']['job']:
			break
	if mission=='unequip all gear':
		return any(friend['unit']['jobs'][ji]['select']['artifacts'])

	elif mission=='equip any gear':
		for ji,job in enumerate(friend['unit']['jobs']):
			if job['iid'] == friend['unit']['select']['job']:
				break
		return not any(friend['unit']['jobs'][ji]['select']['artifacts'])
	
	return True
	


def ownership_check(mission,friend,friend2):
	#units: ['unit] -> gear, abilities (main-> job change)
	if mission=='unit':
		return friend['unit']['iname'] != friend2['unit']['iname']

	elif mission=='job':
		return friend['unit']['select']['job'] != friend2['unit']['select']['job']

	#abilities
	elif mission=='sub ability':
		for ji,job in enumerate(friend['unit']['jobs']):
			if job['iid'] == friend['unit']['select']['job']:
				break
		return friend['unit']['jobs'][ji]['select']['abils'][1] != friend2['unit']['jobs'][ji]['select']['abils'][1]

	elif mission=='reactive':
		for ji,job in enumerate(friend['unit']['jobs']):
			if job['iid'] == friend['unit']['select']['job']:
				break
		return friend['unit']['jobs'][ji]['select']['abils'][2] != friend2['unit']['jobs'][ji]['select']['abils'][2]

	#gear
	elif mission=='unequip all gear':
		for ji,job in enumerate(friend['unit']['jobs']):
			if job['iid'] == friend['unit']['select']['job']:
				break
		return not any(friend2['unit']['jobs'][ji]['select']['artifacts'])

	elif mission=='equip any gear':
		for ji,job in enumerate(friend['unit']['jobs']):
			if job['iid'] == friend['unit']['select']['job']:
				break
		return any(friend2['unit']['jobs'][ji]['select']['artifacts'])


# The setup fucntion below is neccesarry. Remember we give self.bot.add_cog() the name of the class in this case MembersCog.
# When we load the cog, we use the name of the file.
def setup(bot):
	bot.add_cog(TAC_Discord_Cog(bot))
#example
# {
# 	"friends": [
# 		{
# 			"fuid": "VLMPR1HH",
# 			"name": "Winsa",
# 			"type": "none",
# 			"lv": 181,
# 			"lastlogin": 1544026294,
# 			"unit": {
# 				"iid": 19759306,
# 				"iname": "UN_V2_BALT",
# 				"rare": 4,
# 				"plus": 25,
# 				"lv": 85,
# 				"exp": 5579088,
# 				"jobs": [
# 					{
# 						"iid": 27053079,
# 						"iname": "JB_THI",
# 						"rank": 11,
# 						"equips": [
# 							{
# 								"iid": 30803499,
# 								"iname": "IT_EQ_HOOK",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 30803500,
# 								"iname": "IT_EQ_ATK_SPEAR",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 30803501,
# 								"iname": "IT_EQ_DEF_HAGOROMO",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 30803502,
# 								"iname": "IT_EQ_DEX_COMPASS",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 30803503,
# 								"iname": "IT_EQ_SPD_BOOTS",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 30803504,
# 								"iname": "IT_EQ_STATUS_BIG",
# 								"exp": 0
# 							}
# 						],
# 						"abils": [
# 							{
# 								"iid": 27053080,
# 								"iname": "AB_THI_UPPER",
# 								"exp": 19
# 							},
# 							{
# 								"iid": 27053081,
# 								"iname": "AB_THI_LOWER",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 27053111,
# 								"iname": "AB_THI_MOVE1",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 27053172,
# 								"iname": "AB_THI_QUICK_ACTION",
# 								"exp": 19
# 							},
# 							{
# 								"iid": 27053217,
# 								"iname": "AB_THI_JUMP1",
# 								"exp": 0
# 							}
# 						],
# 						"artis": [
# 							{
# 								"iid": 27256075,
# 								"iname": "AF_ARMS_THI_03",
# 								"rare": 2,
# 								"exp": 0
# 							}
# 						],
# 						"cur_skin": "AF_SK_BALT_UNIQUE",
# 						"select": {
# 							"abils": [
# 								27053080,
# 								45341290,
# 								27053172,
# 								27053111,
# 								45341294
# 							],
# 							"artifacts": [
# 								27256075,
# 								0,
# 								0
# 							]
# 						}
# 					},
# 					{
# 						"iid": 38742419,
# 						"iname": "JB_BAA",
# 						"rank": 11,
# 						"equips": [
# 							{
# 								"iid": 43714194,
# 								"iname": "IT_EQ_DARKKABUTO",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 43714195,
# 								"iname": "IT_EQ_ATK_SPEAR",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 43714196,
# 								"iname": "IT_EQ_DEF_HAGOROMO",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 43714197,
# 								"iname": "IT_EQ_DEX_COMPASS",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 43714198,
# 								"iname": "IT_EQ_SPD_BOOTS",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 43714199,
# 								"iname": "IT_EQ_STATUS_BIG",
# 								"exp": 0
# 							}
# 						],
# 						"abils": [
# 							{
# 								"iid": 38742420,
# 								"iname": "AB_BAA_UPPER",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 38742421,
# 								"iname": "AB_BAA_LOWER",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 39597871,
# 								"iname": "AB_BAA_OVER_SOUL",
# 								"exp": 19
# 							},
# 							{
# 								"iid": 39597872,
# 								"iname": "AB_BAA_AVENGER",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 39597873,
# 								"iname": "AB_BAA_SOUL_SUCKER",
# 								"exp": 0
# 							}
# 						],
# 						"artis": [],
# 						"cur_skin": "AF_SK_BALT_UNIQUE",
# 						"select": {
# 							"abils": [
# 								38742420,
# 								19759309,
# 								21583942,
# 								21583926,
# 								39597871
# 							],
# 							"artifacts": [
# 								0,
# 								0,
# 								0
# 							]
# 						}
# 					},
# 					{
# 						"iid": 45341288,
# 						"iname": "JB_BALT_SOL",
# 						"rank": 11,
# 						"equips": [
# 							{
# 								"iid": 45341308,
# 								"iname": "IT_EQ_BALT_TWIN_SWORD",
# 								"exp": 90
# 							},
# 							{
# 								"iid": 45341309,
# 								"iname": "IT_EQ_BALT_BOOTS",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 45341310,
# 								"iname": "IT_EQ_BALT_BOTTOM",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 45341311,
# 								"iname": "IT_EQ_BALT_JACKET",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 45341312,
# 								"iname": "IT_EQ_BALT_BANDANA",
# 								"exp": 0
# 							},
# 							{
# 								"iid": 45341313,
# 								"iname": "IT_EQ_BALT_MEDALLION",
# 								"exp": 0
# 							}
# 						],
# 						"abils": [
# 							{
# 								"iid": 45341289,
# 								"iname": "AB_BALT_SOL_UPPER",
# 								"exp": 19
# 							},
# 							{
# 								"iid": 45341290,
# 								"iname": "AB_BALT_SOL_LOWER",
# 								"exp": 19
# 							},
# 							{
# 								"iid": 45341294,
# 								"iname": "AB_SOL_FEINT",
# 								"exp": 19
# 							},
# 							{
# 								"iid": 45341295,
# 								"iname": "AB_SOL_PERFECT_AVOID",
# 								"exp": 19
# 							}
# 						],
# 						"artis": [
# 							{
# 								"iid": 34460554,
# 								"iname": "AF_ARMS_SOL_04",
# 								"rare": 4,
# 								"exp": 82020
# 							},
# 							{
# 								"iid": 26943676,
# 								"iname": "AF_ACCS_CLOAK",
# 								"rare": 3,
# 								"exp": 43800
# 							},
# 							{
# 								"iid": 34038275,
# 								"iname": "AF_ACCS_GL_SIEG_HAT",
# 								"rare": 4,
# 								"exp": 82020
# 							}
# 						],
# 						"cur_skin": "AF_SK_BALT_UNIQUE",
# 						"select": {
# 							"abils": [
# 								45341289,
# 								45341290,
# 								45341295,
# 								45341294,
# 								39597871
# 							],
# 							"artifacts": [
# 								34460554,
# 								26943676,
# 								34038275
# 							]
# 						}
# 					}
# 				],
# 				"select": {
# 					"job": 45341288,
# 					"quests": [
# 						{
# 							"qtype": "coldef",
# 							"jiid": 45341288
# 						}
# 					]
# 				},
# 				"skin_unlocked": [
# 					"AF_SK_BALT_UNIQUE"
# 				],
# 				"quest_clear_unlocks": []
# 			}
# 		}
# 	]
# }