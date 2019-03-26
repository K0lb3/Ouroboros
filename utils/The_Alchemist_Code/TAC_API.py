import sys
import json
import uuid
import http.client
import time
import os.path
import datetime
from copy import deepcopy
PATH=os.path.dirname(os.path.realpath(__file__))

class TAC_API(object):
	def __init__(self,api="app.alcww.gumi.sg",device_id=str(uuid.uuid4()),secret_key=str(uuid.uuid4()),idfa=str(uuid.uuid4()),idfv=str(uuid.uuid4()),cuid='',print_req=False, debug=False):
		object.__init__(self)
		self.api=api
		self.device_id=device_id
		self.secret_key=secret_key
		self.idfa=idfa
		self.idfv=idfv
		self.name=''
		self.ticket=0
		self.access_token=''
		self.cuid=cuid
		self.fuid=''
		self.print_req=print_req	#print request url
		self.ap=0
		self.debug=debug
		if debug:
			os.makedirs('debug',exist_ok=True)
		###Note
		#	either load account data via setting the variables yourself,
		# 	or use self.load_gu3c
		#	or use create_account
		#	JP logins are a bit tricky, they require the idfa and idfv from the login,
		#	they aren't saved in the gu3c.dat and have to be saved somewhere else

	def get(self,string):
		return getattr(self, string,False)

	def app_start(self):
		if not self.api=='alchemist.gu3.jp':
			res=self.req_chk_player()
			if res['result'] != 1:
				input('Problem during player check:\t%s'%res)
			self.req_bundle()
		self.req_product()
		self.req_achieve_auth()
		login=self.req_login()
		self.req_login_param()
		#check if running quest
		if 'btlid' in login['player'] and login['player']['btlid']:
			print('account still in a quest, please wait ~15s, quest has to be finished first')
			battle=self.resume_battle(login['player']['btlid'])
			time.sleep(10)
			self.end_battle(req_battle=battle)
			time.sleep(5)
		self.req_home()

	def create_account(self,PRINT=False,debug=False):
		self.device_id=''
		self.device_id=self.req_register()
		self.req_achieve_auth()
		self.req_login()
		login=(self.req_playnew(debug=debug))
		self.name=login["player"]["name"]
		if PRINT:
			self.print_login(login)

	def req_accesstoken(self,raw=False):
		self.ticket=0
		body = {
			"access_token": "",
			"param": {
				"device_id": self.device_id,
				"secret_key": self.secret_key,
				"idfa": self.idfa,	# Google advertising ID
				"idfv": self.idfv,
				"udid":""
			}
		}
		res = self.api_request("/gauth/accesstoken", body,'POST',True)
		if 'access_token' in res['body']:
			self.access_token=res['body']['access_token']
		else:
			print('Failed receiving access_token',res)
		return res if raw else res['body']
		
	def req_chk_player(self,raw=False):
		body = {
			"access_token": "",
			"param": {
				"device_id": self.device_id,
				"secret_key": self.secret_key,
				"idfa": self.idfa,	# Google advertising ID
			}
		}
		res_body = self.api_request("/player/chkplayer", body,'POST', True)
		try:
			res_body['body']
		except:
			print('error: failed to retrieve chklayer')
			res_body=self.req_chk_player(True)
		return res_body if raw else res_body['body']
		
	def req_achieve_auth(self):
		return(self.api_connect('/achieve/auth',{},'GET'))

	def req_register(self):
		body={"access_token": "",
			"param": {
			"udid": "",
			"secret_key": self.secret_key,
			"idfv": self.idfv,
			"idfa": self.idfa
			}
		}
		res_body=self.api_connect('/gauth/register',body,'POST')
		try:
			ret = res_body['body']['device_id']
		except:
			print('error: failed to register')
			print(res_body)
			input('/')
			ret=self.req_register()
		return ret

	def req_playnew(self,raw=False,debug=False):
		body={
			"param": {
				"permanent_id": self.idfa,
				}
			}
		if debug:
			body['param']["debug"]=1
		return self.api_request('/playnew',body,'POST',raw)
			
	def req_home(self,raw=False):
		return self.api_request('/home',{"param": {"is_multi_push": 1}},raw=raw)
	
	def req_bundle(self,raw=False):
		return self.api_request('/bundle',raw=raw)

	def req_product(self,raw=False):
		return self.api_request('/product',raw=raw)

	def req_login(self,raw=False):
		if not self.cuid:
			login=self.api_request("/login",{"param":{"device":"HUAWEI HUAWEI MLA-L12","dlc":"apvr"}},raw=True)
			if login['body']:
				self.cuid=login['body']['player']['cuid']
				self.fuid=login['body']['player']['fuid']
			return login if raw else login['body']
		return self.api_request("/login",{"param":{"device":"HUAWEI HUAWEI MLA-L12","dlc":"apvr"}},raw=raw)

	def req_login_param(self,relogin=0,raw=False):
		res = self.api_request('/login/param',{"param": {"relogin": int(relogin)}},raw=True)
		for key,val in res['body'].items():

			if type(val)==list and len(val) and type(val[0])==dict:
				subkey='iname' if 'iname' in val[0] else 'i' if 'i' in val[0] else False
				if key:
					val={item[subkey]:item for item in val}
			self.__setattr__(key,val)
		return res if raw else res['body']

	####	MAIL	##########################################################################
	def req_mail(self,page=1,period=1,read=0,raw=False):
		body = {
			"param": {
				"page": page,
				"isPeriod": period,
				"isRead": int(read)
				}
			}
		res = self.api_request('/mail',body,raw=raw)
		return res if raw else res['mails']

	def read_mail(self,mailIds,page=1,period=1,raw=False):
		if type(mailIds)!=list:
			mailIds=[int(mailIds)]
		body = {
			"param": {
				"mailids": mailIds,
				"page": page,
				"period": period
			}
		}
		return self.api_request('/mail/read',body,raw=raw)

	####	FRIEND	##########################################################################
	def req_friend_fuid(self,fuid,raw=False):
		ret=self.api_request('/friend/find',{'param':{'fuid':fuid}},'POST',True)
		try:
			friend=ret['body']['friends'][0]
			return ret if raw else friend
		except:
			return ret
 
	def req_friend_name(self,name,raw=False):
		ret = self.api_request('/friend/search',{'param':{'name':name}},'POST',True)
		try:
			friend=ret['body']['friends'][0]
			return ret if raw else friend
		except:
			return ret

	####	BATTLE	##############################################################################
	def req_quest_runs(self,raw=False):
		res = self.req_login_param(raw=raw)
		return res if raw else res['quests']

	def req_used_units(self,raw=False):
		return self.api_request('/btl/usedunit/multiple',{	"param": {"inames": ["quest", "arena", "tower_match"]}},raw=raw)
		#	returns array of {iname:,ranking:[unit_iname,job_iname,num],is_ready: just tells if can be used}

	### Single Player
	def req_quests(self,raw=False):
		res = self.api_request('/btl/com',{"param": {"event": 1}},raw=raw)
		return res if raw else res['quests']

	def resume_battle(self,btlid,raw=False):
		return self.api_request('/btl/com/resume',{'param':{'btlid':btlid}},raw=raw)

	def req_battle(self,quest,partyid=0,fuid="",raw=False):
		body={
			"param": {
				"iname": quest,
				"partyid": partyid,
				"req_at": UNIX_timestamp(),
				"btlparam": {
					"help": {
						"fuid": fuid
						}
					},
				"location": {
					"lat": 0,
					"lng": 0
					}
				}
			}
		ret = self.api_request('/btl/com/req',body,raw=True)
		if ret['stat']!=0:	#not enough AP or another error
			print(ret)
			return ret
		else:
			return ret if raw else ret['body']

	def end_battle(self,btlid=False,beats=[],result='win',missions=[],trophies=False,bingos=False,raw=False,req_battle=False):
		if req_battle:
			btlid=req_battle['btlid']
			empty={'gold':0,'secret':0}
			beats=[1 if drop!=empty else 0 for drop in req_battle['btlinfo']['drops']]
		if not btlid:
			input('btlid is missing')

		body={
			"param": {
				"btlid": btlid,
				"btlendparam": {
					"time": 0,
					"result": result,
					"beats": beats,
					"steals": {
						"items": [0]*len(beats),
						"golds": [0]*len(beats)
						},
					"missions": missions,
					"inputs": []
					}
				}
			}
		if trophies:
			body["trophyprogs"]=self.Trophyprogs(trophies)
		if bingos:
			body["bingoprogs"]=self.Bingoprogs(bingos)

		return self.api_request('btl/com/end',body,raw=raw)

	### Multi Player
	def req_multi_check(self,raw=False):
		return self.api_request('/btl/multi/check',raw=raw)	#device_id

	def req_multi_room(self,quest,raw=False):
		return self.api_request('btl/room',{"param": {"iname": quest}},raw=raw)	#empty arry

	def req_multi_room_make(self,quest,comment="",pwd="0",private=0,limit=0,unitlv=0,clear=0,raw=False):
		body = {
			"param": {
				"iname": quest,
				"comment": comment,
				"pwd": pwd,
				"private": int(private),
				"req_at": UNIX_timestamp(),
				"limit": int(limit),
				"unitlv": unitlv,
				"clear": int(clear)
				}
			}
		return self.api_request('/btl/room/make',body,raw=raw)	#roomid,app_id,token

	def req_multi_battle(self,quest,token,partyid=1,host=1,plid=1,seat=1,raw=False):
		body = {	
			"param": {
				"iname": quest,
				"partyid": partyid,
				"token": token,
				"host": str(host),
				"plid": str(plid),
				"seat": str(seat),
				"btlparam": {
					"help": {
						"fuid": ""
					}
					},
				"location": {
					"lat": 0,
					"lng": 0
					}
				}
			}
		return self.api_request('btl/multi/req',body,raw=raw)

	def end_multi_battle(self,btlid,token,beats,result="win",fuids=[],raw=False):
		body = {	
			"param": {
				"btlid": btlid,
				"btlendparam": {
					"time": 0,
					"result": result,
					"beats": beats,
					"steals": {
						"items": [0]*len(beats),
						"golds": [0]*len(beats)
						},
					"missions": [],
					"inputs": [],
					"token": token
					},
				"fuids": fuids
				}
			}
		return self.api_request('btl/multi/end',body,raw=raw)


	### Arena
	def req_arena(self,raw=False):
		return self.api_request("/btl/colo",raw=raw)

	def req_arena_ranking(self,raw=False):
		res_body = self.api_request("/btl/colo/ranking/world",{},'POST',True)
		try:
			ret = res_body['body']['coloenemies']
		except:
			print('error: failed to retrieve arena')
			print(res_body)
			raw=True
		return res_body if raw else ret

	def exec_arena(self,enemy_fuid,opp_rank,my_rank,result='win',beats=[1,1,1],trophies=False,bingos=False,raw=False):
		body={
			"param": {
				"fuid": enemy_fuid,
				"opp_rank": opp_rank,
				"my_rank": my_rank,
				"btlendparam": {
					"time": 0,
					"result": result,
					"beats": beats,
					"steals": {
						"items": [0, 0, 0],
						"golds": [0, 0, 0]
						},
					"missions": [],
					"inputs": []
					}
				}
			}
		if trophies:
			body["trophyprogs"]=self.Trophyprogs(trophies)
		if bingos:
			body["bingoprogs"]=self.Bingoprogs(bingos)
		
		return self.api_request('/btl/colo/exec',body,raw=raw)

	####	CHAT	###############################################################################
	def req_chat(self,channel=1,limit=30,last_msg_id=0,raw=False):
		body={
			"param": {
				"start_id": 0,
				"channel": channel,
				"limit": limit,
				"exclude_id": last_msg_id,
				"is_multi_push": 1
				}
			}
		res = self.api_request('/chat/message',body,raw=raw)
		return  res if raw else res['messages']

	def req_chat_room(self,roomtoken=1,limit=30,last_msg_id=0,raw=False):
		body={
			"param": {
				"start_id": 0,
				"roomtoken": roomtoken,
				"limit": limit,
				"exclude_id": last_msg_id,
				"is_multi_push": 1
				}
			}
		res = self.api_request('/chat/room/message',body,raw=raw)
		return  res if raw else res['messages']

	####	SHOPS	################################################################################
	def req_shopslist(self,typ='',items=True,raw=False):	#types:	limited,event, no type
		res_body = self.api_request('/shop%s/shoplist'%('/%s'%typ if typ else ''),raw=raw)
		try:
			ret=res_body['body']['shops']
			if items:
				for shop in res_body["body"]["shops"]:
					shop["shopitems"]= self.req_shop(shop["gname"],typ,raw=True)
					if not raw:
						shop['shopitems']=shop['shopitems']["body"]["shopitems"]
		except:
			print('error: failed to request shops')
			print(res_body)
			raw=True
		return res_body if raw else ret

	def req_shop(self,shopName,typ='',raw=False):
		return self.api_request('/shop%s'%('/%s'%typ if typ else ''),{"param": {"shopName": shopName}},'POST',raw)

	def update_shop(self,shopName,typ='',raw=False):
		return self.api_request('/shop%s/update'%('/%s'%typ if typ else ''),{"param": {"iname": shopName}},raw=raw)

	def buy_shop(self,shopName,typ='',id=1,num=1,raw=False):
		body={
			"param": {
				"iname": shopName,
				"id": id,
				"buynum": num
				}
			}
		return self.api_request('/shop%s/buy'%('/%s'%typ if typ else ''),body,raw=raw)

	####	GACHA	#################################################################################
	def req_gacha(self,raw=False):
		res=self.api_request("/gacha",{},'POST',raw)
		return res if raw else res['gachas']

	def exec_gacha(self,GachaID,raw=False):
		return self.api_request("/gacha/exec",{"param":{"gachaid":GachaID,"free":0}},'POST',raw)

	####	TROPHIES	##############################################################################
	def req_trophy(self,raw=False):
		res = self.req_login_param(raw=raw)
		return res if raw else res['trophyprogs']

	def exec_trophy(self,trophies,ymd=False, raw=False):
		if type(trophies) == dict:
			trophies = [trophies] 
		body = {
			"param":{
				"trophyprogs":self.Trophyprogs(trophies,ymd)
				}
			}
		return self.api_request("/trophy/exec",body,raw=raw)

	####	CHALLANGE BOARD/BINGOS	####################################################################
	def req_bingo(self,raw=False):
		res = self.req_login_param(raw=raw)
		return res if raw else res['bingoprogs']

	def exec_bingo(self,bingos,ymd=False,raw=False):
		if type(bingos) == dict:
			bingos = [bingos] 
		
		#multiple problems -> have to be solved in waves
		# 1. some bingos require others to be completed first - flg_quests
		# singular bingos have to be cleared for the board reward - parent_iname

		#sorting stuff into category - parent - child

		mbingos={}
		for bingo in bingos:
			iname=bingo['iname']
			category=bingo['category'] if 'category' in bingo else ''
			parent=bingo['parent_iname'] if 'parent_iname' in bingo else ''


			if category not in mbingos:
				mbingos[category]={}
			
			if parent:	#is child
				if parent not in mbingos[category]:
					mbingos[category][parent]={'childs':{}}
				mbingos[category][parent]['childs'][iname]=bingo
			else:	#is parent
				if iname not in mbingos[category]:
					mbingos[category][iname]={'childs':{}}
				mbingos[category][iname].update(bingo)	

		returns=[]
		cleared=[]
		while mbingos:	#loop stuff and destroy mbingos during it until is is destored
			sbingos=[]
			#generate send list
			del_category=[]
			for category,citems in mbingos.items():
				del_parent=[]
				for piname, parent in citems.items():
					exec=True
					#check if it can be run
					if 'flg_quests' in parent:
						for flag in parent['flg_quests']:
							if flag not in cleared:
								exec=False
								break
					if not exec:
						continue

					#check if board can be cleared or childs have to be cleared first
					if 'childs' in parent:
						#execute childs
						sbingos+=[child for ciname,child in parent['childs'].items()]
						del parent['childs']
					else:
						if parent:
							sbingos.append(parent)
						cleared.append(piname)
						del_parent.append(piname)

			#cleanup
				if del_parent:
					for piname in del_parent:
						del citems[piname]

					if not citems: #empty
						del_category.append(category)

			if del_category:
				for ciname in del_category:
					del mbingos[ciname]

			#request childs first
			body = {
				"param":{
					"bingoprogs":self.Bingoprogs(sbingos,ymd)
					}
				}
			returns.append(self.api_request("/bingo/exec",body,raw=raw))

		return returns

	####	Concept Cards	#############################################################################
	def req_cards(self,raw=False):
		return self.api_request('/unit/concept',{"param":{"last_iid": 0}},raw=raw)

	####	Unit	#############################
	def unit_set_skin(self,jobIDs,skin,raw=False):
		body={
			'param':{
				'sets':[
					{
						'iid':	jobid,
						'iname':	skin
					}
					for jobid in jobIDs
				]
			}
		}
		return self.api_request('/unit/skin/set',body=body,raw=raw)

	####	Register Account Linking	##################################################################
	def link_account(self,password='tagatame'):
		if not self.cuid:
			self.req_login()
		body={
				"ticket": "0",
				"access_token": "",
				"email": self.cuid,
				"password": password,
				"disable_validation_email": True,
				"device_id": self.device_id,
				"secret_key": self.secret_key,
				"udid": ""
			}
		ret=self.api_connect('/auth/email/register',body,ignoreStat=True)
		if ret["is_succeeded"]:
			return True
		else:
			return False

	def req_linked_account(self,cuid,password):
		body={
				"access_token": "",
				"email": cuid,
				"password": password,
				"idfv": self.idfv,
				"udid": ""
			}
		ret=self.api_connect('/auth/email/device',body,ignoreStat=True, no_access_token=True)
		if 'device_id' in ret:
			self.device_id=ret['device_id']
			self.secret_key=ret['secret_key']
			print('Login successfull, emulating login')
			self.app_start()
		else:
			print('Login failed')


	####	CONNECTION ###########################################################
	def api_request(self,url,body={},request='POST',raw=False,retry=False):
		if url[0]!= '/':
			url='/%s'%url

		res_body=self.api_connect(url,body,request)
		
		try:
			ret = res_body['body']
			if 'player' in ret and 'stamina' in ret['player']:
				self.ap=ret['player']['stamina']['pt']
		except Exception as e:
			print('error: failed to retrieve %s'%url)
			print(e)
			print(res_body)
			if retry:
				ret=self.api_request(url,body,request,raw,retry=False)
			else:
				raw=True
		return res_body if raw else ret

	def api_connect(self,url, body={},request="POST",api=False, ignoreStat=False, no_access_token=False):
		#print(self.access_token)
		if not api:
			api=self.api
		body['ticket']=self.ticket

		#create headers
		RID=str(uuid.uuid4()).replace('-','')
		headers={
			'X-GUMI-DEVICE-PLATFORM': 'android',
			'X-GUMI-DEVICE-OS': 'android',
			'X-Gumi-Game-Environment': 'sg_production',
			"X-GUMI-TRANSACTION": RID,
			'X-GUMI-REQUEST-ID': RID,
			'X-GUMI-CLIENT': 'gscc ver.0.1',
			'X-Gumi-User-Agent': json.dumps({
				"device_model":"HUAWEI HUAWEI MLA-L12",
				"device_vendor":"<unknown>",
				"os_info":"Android OS 4.4.2 / API-19 (HUAWEIMLA-L12/381180418)",
				"cpu_info":"ARMv7 VFPv3 NEON VMH","memory_size":"1.006GB"
				}),
			"User-Agent": "Dalvik/1.6.0 (Linux; U; Android 4.4.2; HUAWEI MLA-L12 Build/HUAWEIMLA-L12)",
			"X-Unity-Version": "5.3.6p1",
			"Content-Type": "application/json; charset=utf-8",
			"Host": api,
			"Connection": "Keep-Alive",
			"Accept-Encoding": "gzip",
			"Content-Length": len(json.dumps(body))
			}
		if url!="/gauth/accesstoken" and url!='/gauth/register' and not no_access_token:
			if self.access_token == "":
				self.access_token = self.req_accesstoken()['access_token']
			headers["Authorization"] = "gauth " + self.access_token

		if self.print_req:
			print(api+url)
		try:
			con = http.client.HTTPSConnection(api)
			con.connect()
			con.request(request, url, json.dumps(body), headers)
			res_body = con.getresponse().read()
			#print(res_body)
			con.close()
		except http.client.RemoteDisconnected:
			return self.api_connect(url, body)
		try:
			json_res= json.loads(res_body)

			if self.debug:
				with open(os.path.join('debug','{:%y%m%d-%H-%M-%S}{}.json'.format(datetime.datetime.utcnow(),url.replace('/','_'))),'wb') as f:
					f.write(json.dumps(json_res, indent=4, ensure_ascii=False).encode('utf8'))

			if not ignoreStat and json_res['stat'] in [5002,5003]:
				print('Error 5002 ~ have to login again')
				self.access_token=""
				json_res = self.api_connect(url, body)
			self.ticket+=1
			return(json_res)
		except Exception as e:
			print(e)
			print(url)
			print(res_body)

			if self.debug:
				with open(os.path.join('debug','{:%y%m%d-%H-%M-%S}{}.json'.format(datetime.datetime.utcnow(),url.replace('/','_'))),'wb') as f:
					f.write(res_body.encode('utf8'))

			if '504 Gateway Time-out' in str(res_body):
				print('Waiting 30s, then trying it again')
				time.sleep(30)
			elif str(e)=='maximum recursion depth exceeded while calling a Python object':
				raise ValueError('max recursion')
			else:
				input('Unknown Error')
			#return self.api_connect(url, body)
			raise RecursionError('-')

	def print_login(self,json_res=False,ret=False):
		if not json_res:
			json_res=self.req_login()
		print("--------------------------------------------")
		print("Name:", json_res["player"]["name"])
		print("P. Lv:", json_res["player"]["lv"])
		print("User Code:", json_res["player"]["cuid"])
		print("Friend ID:", json_res["player"]["fuid"])
		print("Created at:", json_res["player"]["created_at"])
		print("Exp:", json_res["player"]["exp"])
		print("Stamina:", json_res["player"]["stamina"]["pt"], "/", json_res["player"]["stamina"]["max"])
		print("Zeni:", json_res["player"]["gold"])
		print("Gems:", json_res["player"]["coin"]["paid"], "Paid,", json_res["player"]["coin"]["com"], "Shared,",json_res["player"]["coin"]["free"], "Free")
		print("--------------------")
		if ret:
			return json_res

	####	ETC	################################################
	def Trophyprogs(self,trophies,ymd=False):
		if type(trophies)==dict:
			if 'iname' in trophies[0]:
				trophies=[trophies]
			else:
				trophies=[item for key,item in trophies.items() if 'iname' in item]

		#already formated?
		if 'pts' in trophies[0]:
			skeys=['iname','pts','ymd','rewarded_at']
			return [{skey:item[skey] for skey in skeys if skey in item} for item in trophies]	#clean-up
			
		timestamp=self.ymd_timestamp(ymd)
		return (
			[
				{
					"iname": trophy['iname'],
					"pts": [trophy['ival']] if 'ival' in trophy else [1],
					"ymd":timestamp,
					"rewarded_at":timestamp
				}
				for trophy in trophies
			]
			)

	def Bingoprogs(self,bingos,ymd=False):
		if type(bingos)==dict:
			bingos=[bingos]
		timestamp=self.ymd_timestamp(ymd)
		return (
			[
				{
					"iname": bingo['iname'],
					"parent": bingo['parent_iname'] if 'parent_iname' in bingo else '',
					"pts": [bingo['ival']] if 'ival' in bingo else [1],
					"ymd":timestamp,
					"rewarded_at":timestamp
				}
				for bingo in bingos
				if bingo
			]
			)

	####	timestamps	######################################
	def ymd_timestamp(self,date=False,deltaH=8):
		if 'app.alcww.gumi.sg' in self.api:
			deltaH=8
		elif 'alchemist.gu3.jp' in self.api:
			deltaH=14
		return ymd_timestamp(date=date,deltaH=deltaH)

####	decoding	#########################################
	def load_gu3c(self,path=os.path.join(PATH,"gu3c.dat")):
		if not os.path.exists(path):
			print("", path, " was not found.")
			return

		with open(path, "rb") as f:
			content = bytearray(f.read())

		print("Decrypting gu3c.dat...")
		(self.device_id, self.secret_key) = decrypt(content).decode("utf-8").split(" ")

	def save_gu3c(self,playerName=False,path=PATH):
		if not playerName:
			playerName=self.name
		print("Saving gu3c.dat...")
		print("Device_ID: ",self.device_id)
		print("Secret_Key: ",self.secret_key)
		#encryption
		src = self.device_id + " " + self.secret_key
			
		msg = bytearray(src,"utf8")
		enc = bytes(encrypt(msg))
		os.makedirs(path,exist_ok=True)
		with open(os.path.join(path,'%s.gu3c.dat'%playerName), "wb") as f:
			f.write(enc)


key = bytearray([0x08, 0x38, 0x55, 0x64, 0x17, 0xa0, 0x78, 0x4c, 0xf5, 0x97, 0x86, 0x4b, 0x16, 0xac, 0x9d, 0xd9,
						0xaa, 0x1c, 0x81, 0x7a, 0x27, 0xae, 0x3f, 0x2c, 0xa1, 0x95, 0x80, 0xf4, 0xc8, 0x97, 0xd8, 0x6d,
						0x98, 0x2c, 0x12, 0x5b, 0x88, 0x74, 0x13, 0xbe, 0xe6, 0x84, 0xda, 0xac, 0x14, 0x19, 0xf3, 0x38,
						0x8a, 0xe2, 0x9d, 0x5d, 0xa0, 0x5c, 0x03, 0x71, 0xf6, 0x5b, 0x56, 0xb6, 0x48, 0x14, 0xe7, 0x16,
						0xea, 0x44, 0x3b, 0xd0, 0xd8, 0x20, 0xd5, 0x65, 0xe9, 0xbe, 0xf9, 0xb2, 0xa8, 0x49, 0x1e, 0x80,
						0x1e, 0xd8, 0x80, 0xf1, 0x3f, 0x71, 0x5f, 0x79, 0x92, 0xe3, 0xef, 0xb8, 0xbe, 0xe9, 0x63, 0x5a,
						0x1e, 0xcf, 0x24, 0x5b, 0x87, 0x6b, 0xa2, 0xdc, 0x13, 0x3d, 0x7b, 0xfe, 0x19, 0x60, 0x53, 0xcf,
						0x13, 0x03, 0x45, 0x4f, 0x0f, 0x84, 0xc8, 0x87, 0xac, 0x2a, 0xd5, 0xbc, 0x70, 0xbd, 0xfd, 0x66])

def encrypt(bytes_to_encrypt):
	result = bytearray(len(bytes_to_encrypt))
	for i in range(0, len(bytes_to_encrypt)):
		previous = 0x99 if i == 0 else bytes_to_encrypt[i - 1]
		result[i] = previous ^ bytes_to_encrypt[i] ^ key[i & 0x7f]
	return result

def decrypt(encrypted_bytes):
	result = bytearray(len(encrypted_bytes))
	for i in range(0, len(encrypted_bytes)):
		previous = 0x99 if i == 0 else result[i - 1]
		result[i] = previous ^ encrypted_bytes[i] ^ key[i & 0x7f]
	return result

#timestamps 
def ymd_timestamp(date=False,deltaH=0):
	if not date:
		return '{:%y%m%d}'.format(datetime.datetime.utcnow() - datetime.timedelta(hours=deltaH))
	else:
		return '{:%y%m%d}'.format(datetime.date(*date) - datetime.timedelta(hours=deltaH))

def UNIX_timestamp(date=False):
	if not date:
		return round((datetime.datetime.utcnow() - datetime.datetime(1970,1,1)).total_seconds())
	else:
		return round((datetime.date(*date) - datetime.datetime(1970,1,1)).total_seconds())