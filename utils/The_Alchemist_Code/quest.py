from ._main import DIRS,LinkDB,ConvertFields,StrBuff,StrCondition,Embed,ElementEmoji,GenderEmoji, GitLabLink
from .MapImage import MapImage
from PIL import Image, ImageFont, ImageDraw
import io

##### CONSTANTS #######################################################
UNIT=DIRS['Enemy']
UNIT.update(DIRS['Unit'])

##### QUEST PAGE DECISION #############################################
def Quest(iname,page,edit=False):
	quest=DIRS['Quests'][iname]
	MAP=quest['map'][0]
	SET=MAP['Set']
	SCENE=MAP['Scene']

	#page='main'

	#create basic embed
	embed= Embed(
		title=page.title(), #page name
		url=LinkDB('quest',iname)  #page link
		)
	embed.set_author(name=quest['name'], url=LinkDB('quest',iname))
	embed.set_footer(text='Quest | %s'%quest['iname'])

####	MAIN	####################################################################################################################
	if page=='main':
		fields=[
			{'name':	'Description',	'value':	quest['expr'] if 'expr' in quest else '',	'inline':True},
			{'name':	'Win Condition',	'value':	quest['cond'],	'inline':True},
			{'name':	'Objectives',	'value':	Objectives(quest['mission'] if 'mission' in quest else quest['bonusObjective'] if 'bonusObjective' in quest else ''),	'inline':False},
			{'name':	'BGM',	'value':	"[{name}]({link}')".format(name=MAP['bgmName'],link=GitLabLink('/StreamingAssets/{name}/00000_{name}.mp3'.format(name=MAP['bgmName']))),	'inline':True},
			{'name':	'Magnifications',	'value':	'\n'.join(Magnifications(quest)), 'inline':True},
			#{'name':	'',	'value':	'',	'inline':True},
		]

####	DROPS   ####################################################################################################################
	elif page=='drop':
		fields=[
			{'name':	'AP',	   'value':	quest['point'] if 'point' in quest else '/',  'inline':True},
			{'name':	'Enemies',  'value':	str(len(SET['enemy'])),  'inline':True},
			{'name':	'Chests',   'value':	str(len(SET['treasure'])),  'inline':True},
			{'name':	'Drops',	   'value': '\n'.join([
				DIRS['Item'][item]['name']
				for item in quest['dropList']
				]), 'inline':True}
		]
####	MAP ##########################################################################################################################
	elif page=='map':
		sides=['ally','enemy','jewel','wall']
		fields=[
			{'name': side.title(), 'value': '\n'.join([
				'{num} ~ {name}'.format(num=i+1, name=UNIT[unit['iname']]['name'] if 'iname' in unit else '/')
				for i,unit in enumerate(SET[side])
			]), 'inline':True}
			for side in sides
			if SET[side]
		]

	elif page=='allies':
		fields=[
			Spawn(i+1,'ally',spawn, SCENE)
			for i,spawn in enumerate(SET['ally'])
		]

	elif page=='enemies':
		fields=[
			Spawn(i+1,'enemy',spawn, SCENE)
			for i,spawn in enumerate(SET['enemy'])
		]

	elif page=='treasures':
		fields=[
			Spawn(i+1,'treasure',spawn)
			for i,spawn in enumerate(SET['treasure'])
		]

	elif page=='jewels':
		fields=[
			JewelSpawn(i+1,spawn)
			for i,spawn in enumerate(SET['jewel'])
		]

	elif page=='traps':
		fields=[
			TrapSpawn(i+1,spawn)
			for i,spawn in enumerate(SET['trap'])
		]

####	STORY   ########################################################################################################################
	elif page=='story':
		def convertStory(story):
			return( '\n'.join([
				'{speaker}\t {text}'.format(
					speaker='**%s**:'%line['voiceover'][3:line['voiceover'].index('.')].title() if line['voiceover'] else '',
					text='*%s*'%line['text']
					)
				for line in story
			]))

		fields=[]
		if 'event_start' in quest:
				fields.append({'name':'Pre-Battle','value':convertStory(quest['event_start']),'inline':False})
		if 'map' in quest:
			for map_ in quest['map']:
				if  'eventSceneName' in map_:
					fields.append({'name':'During-Battle','value':convertStory(map_['eventScene']),'inline':False})
		if 'event_clear' in quest:
			fields.append({'name':'Post-Battle','value':convertStory(quest['event_clear']),'inline':False})
		
		if len(fields):
			embed.description=('\n\n'.join(
				'__**%s**__\n%s'%(field['name'],field['value'])
				for field in fields
			))
		else:
			embed.description='This map has no story text or it wasn\'t included yet'
		
		if len(embed.description)<2048:
			return (embed,False)
		else:
			ret=[]
			for field in fields:
				des='__**%s**__\n%s'%(field['name'],field['value'])
				while len(des)>2048:
					cutindex=des.rindex('\n',0,2048)
					(des1,des)=(des[:cutindex],des[1+cutindex:])
					ret.append(Embed(description=des1))
				ret.append(Embed(description=des))
				ret[0].title=page.title()
				ret[0].url=LinkDB('quest',iname)
				ret[0].set_author(name=quest['name'], url=LinkDB('quest',iname))
			return (ret,True)

####	RETURN	################################
	embed.ConvertFields(fields)
	if edit:
		return embed
	else:
		MAP=MapImage(MAP)
		return(embed,MAP)

def JewelSpawn(num,spawn):
	FIX={
		"GemsBuffTurn": 3,
		"GemsBuffValue": 20,
	}
	jewel=DIRS['Enemy'][spawn['iname']]

	name= '{num}{code}:\t{name} [{typ}]'.format(
		num=num,
		code='-``{%s}``'%spawn['uniqname'] if 'uniqname' in spawn else '',
		name=jewel['name'],
		typ=jewel['iname'].rsplit('_',1)[1]
	)

	buff={
		"None"	:	"+%s Jewels",
		"Heal"	:	"+%s%% HP",
	    "AtkUp"	:	"+%s%% PATK",
        "DefUp"	:	"+%s%% PDEF",
        "MagUp"	:	"+%s%% MATK",
        "MndUp"	:	"+%s%% MDEF",
        "RecUp"	:	"+%s%% Recovery",
        "SpdUp"	:	"+%s%% AGI",
        "CriUp"	:	"+%s%% CRIT",
        "LukUp"	:	"+%s%% LUCK",
        "MovUp"	:	"+%s Move",
	}
	gimmick=spawn['trigger']["mGimmickType"]

	if gimmick in ["None","Heal"]:
		value=buff[gimmick]%(spawn['trigger']['mIntValue'] if spawn['trigger']['mIntValue'] else FIX['GemsBuffValue'])
		return {'name':name,	'value':value,'inline':False}
	elif gimmick=="MovUp":
		value=buff[gimmick]%2
	else:
		value=buff[gimmick]%FIX['GemsBuffValue']
	return {'name':name,	'value':value+' for %s turns'%FIX['GemsBuffTurn'],'inline':False}

def TrapSpawn(num,spawn):
	trap=DIRS['Trick'][spawn['iname']]
	name= '{num}{code}:\t{name} [{typ}]'.format(
		num=num,
		code='-``{%s}``'%spawn['uniqname'] if 'uniqname' in spawn else '',
		name=trap['mName'],
		typ=trap["mMarkerName"].upper()
	)

	marker=trap["mMarkerName"]
	effect='~'
	#dmg
	if marker=='damage':
		if trap['mCalcType']=='Scale':
			effect='Decreases HP of {} by %s%%'%trap['mDamageVal']
		else:
			dmg=['DMG']
			if "mElem" in trap and trap["mElem"]!='None':
				dmg.append(trap["mElem"])
			if "mAttackDetail" in trap and trap["mAttackDetail"]!='None':
				dmg.append(trap["mAttackDetail"])
			effect='Does %s %s to {}'%(trap['mDamageVal'],'-'.join(dmg[::-1]))
	#heal
	if marker=='heal':
		if trap['mCalcType']=='Scale':
			effect='Increases HP of {} by %s%%'%trap['mDamageVal']
		else:
			effect='Heal %s HP  of {}'%(trap['mDamageVal'])
	#buff	&	cond
	if marker=='buff' and 'mBuffId' in trap:
		effect=StrBuff(trap["mBuffId"])
	if marker=='cond' and 'mCondId' in trap:
		effect=StrCondition(trap["mCondId"])
	#target
	TARGET_Type={
		"Self"	:	"self",
		"SelfSide"	:	"all units",
		"EnemySide"	:	"all enemies",
		"UnitAll"	:	"all units and enemies",
		"NotSelf"	:	"all besides self",
		"GridNoUnit"	:	"tile without unit",
		"ValidGrid"	:	"valid tile",
		"SelfSideNotSelf"	:	"all units besides self",
	}
	if effect:
		try:
			effect=effect.format(TARGET_Type[trap["mTarget"]])
		except:
			effect+=' %s'%TARGET_Type[trap["mTarget"]]

	values=[effect]
	#times
	values.append('Uses: %s'%(trap["mActionCount"] if "mActionCount" in trap else 'infinite'))

	return {'name':name,	'value':'\n'.join(values),'inline':False}


def Spawn(num,typ,spawn,SCENE={'w':0,'h':0}):
	#note, this will be a field, so 256 characters as name and 1024 as value at most
	sunit = DIRS['Enemy'][spawn['iname']]
	#   header ~ name
	details=[]
	if 'lv' in spawn:
		details.append('LV %s'%spawn['lv'])
	if 'elem' in spawn:
		details.append(ElementEmoji[spawn['elem']])
	if 'sex' in sunit:
		details.append(GenderEmoji[sunit['sex']])

	name= '{num}{code}:\t{name} [{details}]'.format(
		num=num,
		code='-``{%s}``'%spawn['uniqname'] if 'uniqname' in spawn else '',
		name=sunit['name'],
		details=', '.join(details)
	)   
	#   value ~ descriptions
	values=[]
	#   enemy/unit details ~ hidden ones
	if 'startCtVal' in spawn:
		values.append('Start CT:\t%s'%(round(spawn['startCtVal']/10)))
	if "waitMoveTurn" in spawn:
		values.append('Moves after %s turns'%spawn['waitMoveTurn'])
	if 'waitExitTurn' in spawn:
		values.append('Exits after %s turns'%spawn['waitExitTurn'])

	if 'ai' in spawn and spawn['ai']!='None':
		values.append('AI: %s'%spawn['ai'])
	if 'patrol' in spawn:
		patrol=spawn['patrol']
		values.append('Patrols {points}{repeat}{abandon}'.format(
				points=','.join([PointToChess(SCENE,rout['x'],rout['y']) for rout in patrol['routes']]),
				repeat=' one time' if not patrol['looped'] else '',
				abandon= ', doesn\'t hunt enemy' if patrol['keeped'] else ', hunts enemy'
			)
		)
	#   spawn details
	if 'waitEntryClock' in spawn:
		values.append('Spawns after %s clock ticks'%spawn['waitEntryClock'])
	
	if 'entries' in spawn:	#if one of following is true
		entries=[]
		OnGrid={}
		for entry in spawn['entries']:
			if entry['unit'] and not entry['unit'][0] in ['{','p']:
				entry['unit']='{%s}'%(entry['unit'] if not ',' in entry['unit'] else '}{'.join(entry['unit'].split(',')))
			typ=entry['type']
			if typ == "None":
				pass	
			elif typ == "DecrementMember":
				entries.append('less than %s enemies alive'%(entry['value']+1))
			elif typ == "DecrementHp":
				entries.append('HP of %s below %s'%(entry['unit'],entry['value']))
			elif typ == "DeadEnemy":
				entries.append('%s %s are dead'%(entry['value']+1,entry['unit']))
			elif typ == "UsedSkill":
				entries.append('%s used %s %s time(s)'%(entry['unit'],DIRS['Skill'][entry['skill']]['name'],entry['value']+1))	
			elif typ == "OnGridEnemy":
				if entry['unit']=='p':
						entry['unit']='player'
				if entry['unit'] not in OnGrid:
					OnGrid[entry['unit']]=[]
				OnGrid[entry['unit']].append(PointToChess(SCENE,entry['x'],entry['y']))
				#entries.append('%s stands on tile (%s|%s)'%(entry['unit'],entry['x'],entry['y']))
			elif typ == "TransformUnit":
				entries.append('%s transformed'%(entry['unit']))
			elif typ == "WithdrawEnemy":
				entries.append('%s %s withdrew'%(entry['value']+1, entry['unit']))
			elif typ == "DecrementUnit":
				entries.append('less than %s %s alive'%(entry['value']+1,entry['unit']))
		if OnGrid:
			for unit,points in OnGrid.items():
				entries.append('%s stands on %s'%(unit,', '.join(points)))
		if entry:
			values.append('Spawn Conditions:\n\t~ %s'%('\n\t~ '.join(entries)))

	#   return
	#check length of values
	value=''
	for val in values:
		if (len(value) + len(val))<1024:
			value+='\n%s'%val
	if not value:
		value='~'
	return {'name':name,	'value':'```%s```'%value,'inline':False}


def Objectives(objective):
	if not objective:
		return ''
	objective=DIRS['Objectives'][objective]['objective']

	for obj in objective:
		if 'TypeParam' in obj and obj['TypeParam'][:6]=='UN_V2_':
			replacement=	DIRS['Unit'][obj['TypeParam']]['name'] if obj['TypeParam'] in DIRS['Unit'] else DIRS['Enemy'][obj['TypeParam']]['name'] if obj['TypeParam'] in DIRS['Enemy'] else False
			if replacement:
				obj['Type']=obj['Type'].replace(obj['TypeParam'], replacement)
	try:
		return '\n'.join([
			'{objective} ~ {item}'.format(item=('%sx '%obj['itemNum'] if obj['itemNum'] else '')+DIRS[obj['itemType']][obj['item']]['name']  ,objective=obj['Type'])
			for obj in objective
		])
	except Exception as e:
		return e

def Magnifications(quest):
	def Sign(num):
		if num>0:
			return '+%s'%num
		return str(num)

	magnifications=[]
	if "PhysBonus" in quest:
		magnifications.append('%s%% PATK'%Sign(quest['PhysBonus']-100))
	if "MagBonus" in quest:
		magnifications.append('%s%% PATK'%Sign(quest['MagBonus']-100))
	if 'AtkTypeMags' in quest:
		magnifications.append(
		'\t'.join([
                '%s%% %s'%(Sign(val),key)
                for key,val in DIRS['Magnifications'][quest['AtkTypeMags']]['atkMagnifications'].items()
                if val!=0
                ])
		)
	if 'MapBuff' in quest:
		magnifications.append(StrBuff(quest['MapBuff']))
	return magnifications

def PointToChess(SCENE,x,y):
	if SCENE['w']<SCENE['h']:
		x,y=y,x
	return '(%s|%s)'%(chr(65+x),y+1)