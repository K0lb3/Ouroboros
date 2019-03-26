from .TAC_API import TAC_API
from ._main import TimeDif_hms,DIRS,StrBuff
from .quest import MapImage,Magnifications
import discord
from datetime import datetime,timedelta
from copy import deepcopy


API=TAC_API(api='app.alcww.gumi.sg',device_id="9776c2e7-bab9-4411-bba1-ebe8f24982ed",secret_key="ba993058-c726-4568-abb2-54de034741f3")

def Arena():
	arena = API.req_arena_ranking(raw=True)
	ts=arena['time']
	arena = arena['body']['coloenemies']
	
	#start embed - title
	embed = discord.Embed(
		title="Top 50",
		description="ranking in:  " + TimeDif_hms("04:00:00",datetime.utcfromtimestamp(ts)) + "\n" + "reset in:	" + TimeDif_hms("08:00:00",datetime.utcfromtimestamp(ts)),
		url="",
		color=16711680
		)
	#icon
	embed.set_thumbnail(url="https://drive.google.com/uc?export=download&id=1W369kdS_4ISgJgOcHAgNxoidJRqWNNsl")
	#fields
	field=[
		str(i+1)+": " + arena[i]['name']
		for i in range(0,len(arena))
		]

	embed.add_field(name="Rank  1-25",	value='\n'.join(field[0:25]),	inline=True)
	embed.add_field(name="Rank 26-50",	value='\n'.join(field[25:]),	inline=True)
	return embed

def Arena_Map():
	quest = DIRS['Quests'][API.req_arena()['quest_iname']]
	map = deepcopy(quest['map'][0])
	map['Set'] ={
		key:map['Set'][key]
		for key in ['party','enemy']
	}
	MAP=MapImage(map)
	embed= discord.Embed(
		title='%s - %s'%(quest['name'], quest['iname']), #page name
		#url=LinkDB('quest',iname)  #page link
		)
	magnifications=Magnifications(quest)
	if magnifications:
		embed.add_field(name='Magnifications', value='\n'.join(magnifications) if magnifications else '/', inline=False)
	return(embed,MAP)
	
def Rank(input):
	try:
		number = int(input)
	except:
		return 'invalid input'

	#request info
	try:
		enemy = API.req_arena_ranking()[number-1]
	except IndexError:
		return 'Only the top 50 can be accessed'

	time = datetime.fromtimestamp(enemy['btl_at']) - timedelta(hours=8)
	#create embed header
	embed = discord.Embed(
		title=input+': '+enemy['name'], 
		description="last battle at: "+time.strftime('%H:%M:%S ~ %d.%m'), #epoch time in sec 1528371048
		color=0xeee657,
		)
	#diggest info
	units=[]
	for unit in enemy['units']:
		c={
			'name'  : DIRS['Unit'][unit['iname']]['name'],
			'LB'	: unit['plus'],
			'LV'	: unit['lv'],
			'jobs'  : [],
			}
		seljob = unit['select']['job'] #number, compare to jobs
		for job in unit['jobs']:
			#selected job?
			if job['iid']==seljob:
				seljob=len(c['jobs'])
			#basic job info
			cjob={
				'name'  :   DIRS['Job'][job['iname']]['name'],
				'LV'	:   str(job['rank']), #if equip wthout empty jm,
				'gears'  :   [],
				'abilities':	[]
			}
			#rank
			if cjob['LV']=='11':
				e=0
				for equip in job['equips']:
					if equip['iid']!=0:
						e+=1
				if e==6:
					cjob['LV']='JM'
			#gear
			for gear in job['artis']:
				cjob['gears'].append({
					'name'  : DIRS['Artifact'][gear['iname']]['name'],
					'rarity': str(gear['rare']+1),
				})
			#abilities
			for abil in job['abils']:
				cjob['abilities'].append({
					'name'  : DIRS['Ability'][abil['iname']]['name'],
					'lv'	: str(abil['exp']+1),
				})
			c['jobs'].append(cjob)
		c['job']=seljob
		units.append(c)

	#add fields
		#"value": "Lv: 75\tLb: 25\nJobs:\n\tJob1: JM\n\tJob2: JM\n\tJob 3: JM\nGear:\n\tGear 1\n\tGear 2\n\tGear 3",
	for unit in units:
		value = 'LV: {LV}/{maxLV}'.format(LV=str(unit['LV']), maxLV=str(unit['LB']+60))
		#job info
		value+='\n__Jobs:__'
		for i in range(0,len(unit['jobs'])):
			j= unit['jobs'][i]
			cvalue='\n\t'+str(j['name']+': '+j['LV'])
			if i == unit['job']:
				cvalue='**'+cvalue+'**'
			value+=cvalue

		#gear
		value+='\n__Gear:__'
		for g in unit['jobs'][unit['job']]['gears']:
			value+='\n\t'+g['name']+' '+str(g['rarity']+'*')
		#create field
		embed.add_field(name=unit['name'], value=value, inline=True)

	return embed