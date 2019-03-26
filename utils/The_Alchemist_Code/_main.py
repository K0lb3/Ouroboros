from datetime import datetime,timedelta
from discord.message import Embed
from tabulate import tabulate
import os
import json
#variables
if os.name == 'nt':
	resPath=  os.path.join(os.path.realpath(__file__),*[os.pardir,os.pardir,os.pardir,'res'])
else:
	resPath=  os.path.join('res')
dbpath= os.path.join(resPath,'Database.json')	#os.path.join(os.path.realpath(__file__),*[os.pardir,os.pardir,os.pardir,'res','Database.json'])
with open(dbpath,'rb') as f:
	DIRS=json.loads(f.read())
DIRS['Quests'].update(DIRS['Towerfloors'])
#Emojis
ElementEmoji={
	'Fire'    :'🔥',
    'Water'    :'💧',
    'Thunder':':⚡',
    'Wind'    :'🍃',
    'Light'    :'☀',
    'Dark'    :'🌑',
}
GenderEmoji={
	'Male'	:	'♂️',
	'Female':	'♀️',
	'Unknown':	'/'
}


#Embed patch
def ConvertFields(self,fields):
	if fields:
		for field in fields:
			if 'inline' not in field:
				field['inline']=True
			if type(field['value'])!=str:
				field['value']=str(field['value'])
			for string in ['name','value']:
				if len(field[string])>255:
					field[string]=field[string][:256]

		if not getattr(self,'_fields',False):
			self._fields=[]
		self._fields += [
				field
				for field in fields
				if len(str(field['name']).rstrip(' '))>0 and len(str(field['value']).rstrip(' '))>0
			]
Embed.ConvertFields=ConvertFields

#functions
def createTable(data):
	# print tabulate([["value1", "value2"], ["value3", "value4"]], ["column 1", "column 2"], tablefmt="grid")
	# +------------+------------+
	# | column 1   | column 2   |
	# +============+============+
	# | value1     | value2	    |
	# +------------+------------+
	# | value3     | value4	    |
	# +------------+------------+
	if type(data)==dict:
		headers=[]
		values=[[]]
		for key,val in data.items():
			headers.append(key)
			values[0].append(val)
	elif type(data)==list and type(data[0])==dict:
		headers=[]
		for obj in data:
			for key in obj:
				if key not in headers:
					headers.append(key)
		values=[
			[
				obj[key] if key in obj else None
				for key in headers
			]
			for obj in data
		]
	else:	#list
		headers=[i+1 for i in range(len(data))]
		values=[data]

	return tabulate(values,headers,tablefmt='grid')

def GitLabLink(path):
	try:
		region=DIRS['Path'][path].lstrip('/')
		path=path.lstrip('/')
		if region=='AssetsGL':
			region='Global'
		elif region=='AssetsTW':
			region='Taiwan'
		elif region=='AssetsJP':
			region='Japan'
		else:
			return ''

		return 'https://gitlab.com/the-alchemist-codes/{region}/raw/master/{path}'.format(
			region=region,
			path=path
		)
	except Exception as e:
		print(e)
		return ''

def LinkDB(typ,iname,post='',convert=True,region=''):
	if convert:
		iname = iname.replace('_','-').lower()
	return 'http://www.alchemistcodedb.com/{region}{typ}/{iname}{post}'.format(typ=typ,iname=iname,post=post,region=(region+'/') if region else '')

def TimeDif_hms(time,time2=datetime.utcnow()):
	FMT = '%H:%M:%S'
	tdelta = datetime.strptime(time, FMT) - time2
	if tdelta.days < 0:
		tdelta = timedelta(days=0,seconds=tdelta.seconds)

	return str(tdelta)

def TACScale(ini,max,lv,mlv):
	return int(ini + ((max-ini) / (mlv-1) * (lv-1)))

def StrSkill(skill):
	ret=[]
	base=[]
	if 'count' in skill:
		base.append("Uses: %d"%skill['count'])
	if 'cost' in skill:
		base.append("Cost: %d"%skill['cost'])
	# if skill['effect_value']:
	# 	base.append("EV: %d%%"%(100+skill['effect_value']['max']))
	# if skill['effect_rate']:
	# 	base.append("ER: %d%%"%(skill['effect_rate']['max']))

	# for defignor in ['ignore_defense_rate','back_defrate']:
	# 	if defignor in skill:
	# 		base.append("DEF Ignor: %d%%"%(-1*skill[defignor]))
	if base:
		ret.append('[%s]'%', '.join(base))
	if 'expr2' in skill:
		ret.append('-~'*20)
		ret.append(skill['expr2'])
	else:
		effects=[]
		if 'self_buff_iname' in skill:
			effects.append('Self: %s'%StrBuff(skill['self_buff_iname'],2,2,False))
		if 'target_buff_iname' in skill:
			effects.append('Target: %s'%StrBuff(skill['target_buff_iname'],2,2,False))
		if 'self_cond_iname' in skill:
			effects.append('Self: %s'%StrCondition(skill['self_cond_iname'],2,2))
		if 'target_cond_iname'in skill:
			effects.append('Target: %s'%StrCondition(skill['target_cond_iname'],2,2))
		if effects:
			ret.append('[%s]'%'\n'.join(effects))

		if skill['flags']:
			ret.append('[Flags: %s]'%', '.join(skill['flags']))
	return '\n'.join(ret)

def StrBuff(buff,lv=0,mlv=99,mods_only=True):
	if type(buff)==str:
		buff=DIRS['Buff'][buff]
	text=""

	rest={
		'birth': 'Country',
		'sex': 'Gender',
		'elem': 'Element'
		}

	restrictions=[
		'{rest}: {value}'.format(rest=rest[key],value=buff[key])
		for key,value in rest.items()
		if key in buff
		]

	def withSign(number):
		if number<0:
			return str(number)
		return '+'+str(number)

	mods=[
		'{value}{calc} {stat}'.format(
			stat= mod['type'],
			value=withSign(TACScale(mod['value_ini'],mod['value_max'],lv,mlv)),
			calc='%' if 'Scale' == mod['calc'] else '*Fixed*' if mod['calc']== 'Fixed' else ''
			)
		for mod in buff['buffs']
		]
	if mods_only:
		return ', '.join(mods)

	runt=[]
	if 'rate' in buff and buff['rate']!=100:
		runt.append('Chance: {chance}%'.format(chance=buff['rate']))
	if 'turn' in buff and buff['turn']!=0:
		runt.append('Turns: {turns}'.format(turns=buff['turn']-1))

	if len(restrictions)!=0:
		text+='Restriction(s): {rest}\n'.format(rest=', '.join(restrictions))
	if len(mods)!=0:
		text+=', '.join(mods)
	if len(runt)!=0:
		text+=' [{runt}]'.format(runt=', '.join(runt))
	return text

def StrCondition(cond,lv=0,mlv=99):  
	if type(cond)==str:
		cond=DIRS['Cond'][cond]  
	vals=[]
	if 'turn_ini' in cond:
		turn = TACScale(cond['turn_ini'],cond['turn_max'],lv,mlv)
		vals.append('Turns: {turn}'.format(turn=turn-1))

	if 'rate_ini' in cond:
		rate = TACScale(cond['rate_ini'],cond['rate_max'],lv,mlv)
		vals.append('Chance: {rate}%'.format(rate=rate))

	if 'value_ini' in cond:
		value = TACScale(cond['value_ini'],cond['value_max'],lv,mlv)
		vals.append('Value: {val}'.format(val=value))
	
	if len(vals)!=0:
		vals='[{values}]'.format(values=', '.join(vals))
		if len(cond['conditions'])<6:
			return('{cond} {vals}'.format(vals=vals,cond=', '.join(cond['conditions'])))
		else:
			return('{vals} {cond}'.format(vals=vals,cond=', '.join(cond['conditions'])))
	else:
		return(', '.join(cond['conds']))

def Rarity(base, max):
	base+=1
	max+=1
	return "★"*base + "☆"*(max-base)