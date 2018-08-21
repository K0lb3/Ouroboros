from datetime import datetime,timedelta
from functions import LoadResources
from discord.message import Embed

#variables
DIRS = LoadResources()
#Embed patch
def ConvertFields(self,fields):
    for field in fields:
        if 'inline' not in field:
            field['inline']=True
        if type(field['value'])!=str:
            field['value']=str(field['value'])

    self._fields = [
            field
            for field in fields
            if len(field['name'].rstrip(' '))>0 and len(field['value'].rstrip(' '))>0
        ]
Embed.ConvertFields=ConvertFields

#functions
def LinkDB(typ,iname,post='',convert=True):
    if convert:
        iname = iname.replace('_','-').lower()
    return 'http://www.alchemistcodedb.com/{typ}/{iname}{post}'.format(typ=typ,iname=iname,post=post)

def TimeDif_hms(time,time2=datetime.now()):
    FMT = '%H:%M:%S'
    tdelta = datetime.strptime(time, FMT) - time2
    if tdelta.days < 0:
        tdelta = timedelta(days=0,
            seconds=tdelta.seconds)

    return str(tdelta)

def TACScale(ini,max,lv,mlv):
    return int(ini + ((max-ini) / (mlv-1) * (lv-1)))

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
    text = ""
    for i in range(0, base+1):
        text += "★"
    for i in range(base, max):
        text += "☆"

    return text