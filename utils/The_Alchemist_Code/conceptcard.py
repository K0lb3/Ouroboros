from ._main import DIRS,LinkDB,ConvertFields,StrBuff,StrCondition,Embed, Rarity,GitLabLink
import copy
def Conceptcard(iname, page):
	#get card dir
	card=DIRS['Conceptcard'][iname]

	#create basic embed
	embed= Embed(
		title='', #page name
		url=LinkDB('card',iname,'',True,'jp')  #page link
		)
	embed.set_author(name=card['name'],url=embed.url)#, url=LinkDB('card',iname))
	embed.set_thumbnail(url=GitLabLink('/ConceptCardIcon/%s.png'%iname))#'http://cdn.alchemistcodedb.com/images/cards/icons/{}.png'.format(iname))
	embed.set_image(url=GitLabLink('/ConceptCard/%s.png'%iname))#'http://cdn.alchemistcodedb.com/images/cards/artworks/{}.png'.format(iname))

	while page:
		if page=='main':
			embed.ConvertFields(main(card))
			embed.title='main'
			break
		break
	
	return embed

def main(card):
	try:
		unit=DIRS['Unit']['UN_V2_'+card['iname'].rsplit('_',2)[1]]['name']
	except:
		unit='None'

	fields = [
		{'name': 'Unit',			'value': unit, 'inline':True},
		{'name': 'Rarity',		  'value': Rarity(card['rare'],4), 'inline':True},
		{'name': 'Enhance Cost',	'value': str(card['en_cost']), 'inline':True},
		{'name': 'Enhance EXP',	 'value': str(card['en_exp']), 'inline':True},
		]
	#Weapon Type ~ Tag
	if 'trust_reward' in card:
		fields.append({'name': 'Trust Reward(s)', 'value': '```\t%s```'%'\n\t'.join(
			[
				'%s%s (%s)'%('%sx'%reward['reward_num'] if reward['reward_num']>1 else '', DIRS[reward['reward_type']][reward['iname']]['name'],reward['reward_type'].replace('Artifact','Gear')) 
				for reward in DIRS['Conceptcardtrustreward'][card['trust_reward']]['rewards']
			]
			), 'inline': False})

	#effects
	for i,effect in enumerate(card['effects']):
		value=[]
		#cnds_iname
		if 'cnds_iname' in effect:
			value.append(
				'__**Condition(s):**__\n'+CardConditions(effect['cnds_iname'])
			)
		#abil_iname
		if 'abil_iname' in effect:
			value.append('__**Vision Ability:**__\n'+
			DIRS['Ability'][effect['abil_iname']]['name']
			)
		#skin
		if 'skin' in effect:
			value.append('__**Skin:**__\n'+DIRS['Artifact'][effect['skin']]['name'])
		#statusup_skill ~ from equipping
		if 'statusup_skill' in effect:
			value.append('__**Stats:**__\n'+
			StrBuff(DIRS['Skill'][effect['statusup_skill']]['target_buff_iname'],2,2)
			)
		#####
		# LV 30:	stats (x per level)
		# Limit Break: x per level
		# Max Limit Break: total stats (lv 40, all limit breaks, + mlb bonus)
		#card_skill
		buffs=[]

		if 'card_skill' in effect:
			buffs.append(DIRS['Skill'][effect['card_skill']]['target_buff_iname'])
			value.append(
			'__**LV 30 Stats:**__	%s'%(
				StrBuff(DIRS['Skill'][effect['card_skill']]['target_buff_iname'],30,40)
				)
			)
		#add_card_skill_buff_awake
		if 'add_card_skill_buff_awake' in effect:
			buffs.append(effect['add_card_skill_buff_awake'])
			value.append('__**Limit Break Stats:**__\n1:%s\n5:%s'%(StrBuff(effect['add_card_skill_buff_awake'],1,5),StrBuff(effect['add_card_skill_buff_awake'],5,5))
			)
		#add_card_skill_buff_lvmax
		if 'add_card_skill_buff_lvmax' in effect:
			buffs.append(effect['add_card_skill_buff_lvmax'])
			value.append('__**MLB Bonus Stats:**__\n'+
			StrBuff(effect['add_card_skill_buff_lvmax'],2,2)
			)
		#total stats
		if buffs:
			value.append('__**Max Stats:**__\n'+
			StrBuff(add_buffs(buffs),2,2)
			)
			for buff in buffs:
				buff=DIRS['Buff'][buff]
				if 'un_group' in buff:
					value.append('__**Boosted Units:**__\n%s'%(', '.join([
						DIRS['Unit'][unit]['name']
						for unit in DIRS['Unitgroup'][buff['un_group']]['units']
					])))
				break
				#elif costum target 

		fields.append({'name':  'Effect '+str(i+1),	  'value': '\n'.join(value), 'inline':False})
	#lore
	fields.append({'name': 'Description',   'value': card['expr'], 'inline': False})
	return fields

def CardConditions(iname):
	conds=DIRS['Conceptcardconditions'][iname]
	ret=[]
	#unit group
	if 'unit_group' in conds:
		ret.append('Units: '+', '.join([
			DIRS['Unit'][uiname]['name']
			for uiname in DIRS['Unitgroup'][conds['unit_group']]['units']
		]))
	#job group
	if 'job_group' in conds:
		ret.append('Jobs: '+', '.join([
			DIRS['Job'][jiname]['name']
			for jiname in DIRS['Jobgroup'][conds['job_group']]['jobs']
		]))
	#element
	if 'conditions_elements' in conds:
		ret.append('Element: '+', '.join(conds['conditions_elements']))
	#birth
	if 'birth' in conds:
		ret.append('Birth: '+', '.join(conds['birth']))

	return '\n'.join(ret)

def add_buffs(buffs):
	#check type
	buffs=[
		copy.deepcopy(DIRS['Buff'][buff]) if type(buff)==str else copy.deepcopy(buff)
		for buff in buffs
	]
	#add effects up
	types={'Scale':{},'Add':{},'Fixed':{}}
	for buff in buffs:
		for stat in buff['buffs']:
			typ=types[stat['calc']]
			if stat['type'] not in typ:
				typ[stat['type']]=stat
			else:
				typ[stat['type']]['value_ini']+=stat['value_ini']
				typ[stat['type']]['value_max']+=stat['value_max']
				typ[stat['type']]['value_one']+=stat['value_one']
	#revert back
	return ({'buffs':[
		buff
		for typ,items in types.items()
		for key,buff in items.items() 
		]})