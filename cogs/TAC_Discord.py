import os
import discord
import asyncio
import json
import jellyfish
import urllib.request
import datetime
from discord.ext import commands
import io
import math
from os import environ
from utils.FindBest import FindBest

SERVER_ID=360289051011710979
ROLE_REQ_CHANNEL_ID=430379003153743872
ROLE_CHECK_CHANNEL_ID=469511848862023711
ROLE_LOG_CHANNEL_ID=469511831069917184
LOG_CHANNEL_ID=424438482832719873
WELL_LOG_ID=447334500872224768
WELL_ACTIVE_ID=490597479960674307


ALCHEMIST_ID   =   455472273927962654
SHALLOW_WELL_ID=   469550314639327233
DEPP_WELL_ID   =   455415186321309699
GUIDE_ID=469753409105297419
MOD_ID=455407410291671052

RoleBlackList=['gumi']
#Variables
roles=[]

# def ConvertFields(self,fields):
# 	for field in fields:
# 		if 'inline' not in field:
# 			field['inline']=True
# 		if type(field['value'])!=str:
# 			field['value']=str(field['value'])
# 		self._fields = [
# 				field
# 				for field in fields
# 				if len(field['name'].rstrip(' '))>0 and len(field['value'].rstrip(' '))>0
# 				]
# discord.Embed.ConvertFields=ConvertFields

class TAC_Discord_Cog:
	def __init__(self, bot):
		self.bot = bot

	async def on_ready(self):
		print(f'\n\nLogged in as: {self.bot.user.name} - {self.bot.user.id}\nVersion: {discord.__version__}\n')
		print(f'Successfully logged in and booted...!')
		await self.bot.change_presence(status=discord.Status.online, activity=discord.Game('!role'))
		global roles, SERVER, ALCHEMIST, SHALLOW_WELL, DEPP_WELL, MOD, GUIDE
		SERVER=self.bot.get_guild(SERVER_ID)

		ALCHEMIST   =   SERVER.get_role(ALCHEMIST_ID)
		SHALLOW_WELL=   SERVER.get_role(SHALLOW_WELL_ID)
		DEPP_WELL   =   SERVER.get_role(DEPP_WELL_ID)
		GUIDE		=	SERVER.get_role(GUIDE_ID)
		MOD			=	SERVER.get_role(MOD_ID)


	async def add_reactions(self,msg,reactions):
		for r in reactions:
			await msg.add_reaction(r)

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
		#get alchemist role
		if payload.message_id == 495947294538203151 and emoji=='✅':
			member= SERVER.get_member(payload.user_id)
			await member.add_roles(ALCHEMIST)


		#roles
		if payload.channel_id == ROLE_CHECK_CHANNEL_ID and payload.user_id != self.bot.user.id:
			roles={
				r.name:r
				for r in SERVER.roles
				}

			user = SERVER.get_member(user_id=int(msg.embeds[0].description[2:-1]))
			user_id=msg.embeds[0].description[2:-1]
			role=msg.embeds[0].title
			ROLE_LOG_CHANNEL= self.bot.get_channel(ROLE_LOG_CHANNEL_ID)
			ROLE_REQ_CHANNEL= self.bot.get_channel(ROLE_REQ_CHANNEL_ID)
			if emoji=='✅':
				await user.add_roles(roles[role])
				msg.embeds[0].description+='\nwas accepted by <@{}>'.format(payload.user_id)
				await ROLE_LOG_CHANNEL.send(embed=msg.embeds[0])
				await ROLE_REQ_CHANNEL.send('<@%s> got the role: "%s"'%(user_id,role))
				#await user.send('You got the {} role'.format(msg.embeds[0].title))
				await msg.delete()
			elif emoji == '❌':
				msg.embeds[0].description+='\nwas rejected by <@{}>'.format(payload.user_id)
				await ROLE_LOG_CHANNEL.send(embed=msg.embeds[0])
				await ROLE_REQ_CHANNEL.send('<@%s> request for the role: "%s" was rejected'%(user_id,role))
				#await user.send('Your {} role request was rejected'.format(msg.embeds[0].title))
				await msg.delete()

		#troll
		if emoji=='🚫':
			member= SERVER.get_member(payload.user_id)
			author= SERVER.get_member(msg.author.id)
			guide_ch=[360289250979217418,473998586770882565]

			if MOD in member.roles:
				well=DEPP_WELL
			elif GUIDE in member.roles:# and payload.channel_id in guide_ch:
				well=SHALLOW_WELL
			else:
				return
			well = well
			await author.add_roles(well)		
			title=well.name

			embed= discord.Embed(
				title=title,
				color=16711680
				)
			embed.title+= ('	(Added)')
			embed.description='__**Content:**__\n%s'%msg.content
			embed.add_field(name='Agressor',value='<@{ID}>'.format(ID=msg.author.id))
			embed.add_field(name='Reported by',value='<@{ID}>'.format(ID=payload.user_id))
			embed.add_field(name='Created (UTC)',value=str(msg.created_at)[:-7])
			#embed.add_field(name='Content',value=msg.content, inline=False)
			embed.add_field(name='Jump URL',value=msg.jump_url)
			embed.set_thumbnail(url=author.avatar_url)
			REPORT_CHANNEL=self.bot.get_channel(WELL_LOG_ID)
			await REPORT_CHANNEL.send(embed=embed)
			REPORT_CHANNEL=self.bot.get_channel(WELL_ACTIVE_ID)
			msg = await REPORT_CHANNEL.send(embed=embed)
			await self.add_reactions(msg, ['✅'])
		#well out
		if payload.channel_id == WELL_ACTIVE_ID and emoji == '✅' and payload.user_id != self.bot.user.id:
			for field in msg.embeds[0].fields:
				if field.name == 'Agressor':
					member = SERVER.get_member(user_id=int(field.value[2:-1]))
					await member.remove_roles(*[
						DEPP_WELL,
						SHALLOW_WELL
					])

					REPORT_CHANNEL=self.bot.get_channel(WELL_LOG_ID)
					embed=msg.embeds[0]
					embed.title=embed.title.replace('Added','Removed')
					await REPORT_CHANNEL.send(embed=embed)
					await msg.delete()
					break



	@commands.command()
	async def role(self,ctx, *, input):
		if ctx.channel.id != ROLE_REQ_CHANNEL_ID:
			await ctx.send('Please use the role channel')
			return
		
		ROLE_CHECK_CHANNEL=self.bot.get_channel(ROLE_CHECK_CHANNEL_ID)
		if len(ctx.message.attachments)==0:
			await ctx.send(content='<@{}> not attachment was detected, please take care to upload the proof with the command as comment'.format(ctx.author.id))
			return
		if len(ctx.message.attachments)==1:
			roles={
				r.name:r
				for r in SERVER.roles
				}
			#get right role
			role,score=FindBest(roles,input,False,False)
			
			if str(role) in RoleBlackList:
				await ctx.send('You\'re risking a ban in this channel')
				return
				
			if score>0.8:
				#create embed for control channel
				embed= discord.Embed(title=role.name,description='<@{ID}>'.format(ID=ctx.author.id))
				embed.set_image(url=ctx.message.attachments[0].url)
				#send it
				msg = await ROLE_CHECK_CHANNEL.send(embed=embed)
				#add decision reactions
				await self.add_reactions(msg, ['✅','❌'])
				#confirm 
				await ctx.send('{} request send to secret channel'.format(role.name))
				return
			else:
				await ctx.send('Your input isn\'t similiar enough to be recognised, please use the actual role name.\nBest Result: %s - %s%%'%(role,round(score*100,4)))
		if len(ctx.message.attachments)>1:
			await ctx.send(content='<@{}> please post only one image at a time'.format(ctx.author.id))
			return

	#log stuff
	async def on_message_delete(self,msg):
		if len(msg.content)<2:
			return
		try:
			if msg.author.id != self.bot.user.id and msg.guild.id == SERVER.id:
				embed= discord.Embed(
					title='Deleted Message',
					color=16711680,
					)
				embed.description='__**Content**__\n%s'%(msg.content)
				fields=[
					{'name':'Channel','value':'<#{}>'.format(msg.channel.id)},
					{'name':'Author','value':'<@{ID}>'.format(ID=msg.author.id)},
					#{'name':'Content','value':msg.content, inline=False},
					{'name':'Created (UTC)','value':str(msg.created_at)[:-7]},
					{'name':'Deleted (UTC)','value':str(datetime.datetime.utcnow())[:-7]},
					{'name':'Jump URL','value':msg.jump_url}
					]
				embed.ConvertFields(fields)
				embed.set_thumbnail(url=msg.author.avatar_url)

				LOG_CHANNEL=self.bot.get_channel(LOG_CHANNEL_ID)
				if len(msg.attachments):
					embed.add_field(name='Attachments',value=len(msg.attachments),inline=True)
					await LOG_CHANNEL.send(embed=embed)
					for i,attachment in enumerate(msg.attachments):
						try:
							resource = urllib.request.urlopen(attachment.proxy_url)
							f = discord.File(fp=resource.read(),filename=attachment.filename)
							await LOG_CHANNEL.send(content='File %d'%(i+1),file=f)
						except Exception as e:
							await LOG_CHANNEL.send(content='Failed to save file %d,\nError: %s'%(i+1,e))
				else:
					await LOG_CHANNEL.send(embed=embed)
		except Exception as e:
			print('On_MSG_Delete: %s'%e)

	async def on_message_edit(self,before,after):
		if len(before.content)<2 and len(after.content)<2:
			return
		try:
			if before.author.id != self.bot.user.id and before.guild.id == SERVER.id:
				embed= discord.Embed(
					title='Edited Message',
					color=16711680
					)
				embed.description='__**Old Content**__\n%s\n\n__**New Content**__\n%s'%(before.content,after.content)
				fields=[
					{"name":'Channel',"value":'<#{}>'.format(before.channel.id)},
					{"name":'Author',"value":'<@{ID}>'.format(ID=before.author.id)},
					#embed.add_field(name='Old Content',value=before.content, inline=False)
					#embed.add_field(name='New Content',value=after.content, inline=False)
					{"name":'Created (UTC)',"value":str(before.created_at)[:-7]},
					{"name":'Edited (UTC)',"value":str(after.edited_at)[:-7]},
					{"name":'Jump URL',"value":after.jump_url}
				]
				embed.ConvertFields(fields)
				embed.set_thumbnail(url=before.author.avatar_url)
				LOG_CHANNEL=self.bot.get_channel(LOG_CHANNEL_ID)
				await LOG_CHANNEL.send(embed=embed)
		except Exception as e:
			print('On_MSG_Edit: %s'%e)

# The setup fucntion below is neccesarry. Remember we give self.bot.add_cog() the name of the class in this case MembersCog.
# When we load the cog, we use the name of the file.
def setup(bot):
	bot.add_cog(TAC_Discord_Cog(bot))
