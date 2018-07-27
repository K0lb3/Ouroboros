import sys
import json
import uuid
import http.client
import time
import os
import urllib.request
import re
import zlib

#global vars ############################
access_token=""
idfa=""
idfv=""

ticket=1
#global
api_gl={
    'con':"app.alcww.gumi.sg",
    'device_id':"af4a61bd-e020-4260-8df0-8ed8241e00d0",
    'secret_key':"9487b67b-a586-48db-894a-e6c2e93db44c",
    }

api=api_gl

path=os.path.dirname(os.path.realpath(__file__))+'\\resources\\'
os.makedirs(path, exist_ok=True)
###########functions#####################################

def saveAsJSON(name, var, subdir=False):
    global path
    if subdir:
        name=subdir+'\\'+name
    with open(path+name, "wb") as f:
        f.write(json.dumps(var, indent=4, ensure_ascii=False).encode('utf8'))

def download(url, decoder='utf-8-sig'):
    resource = urllib.request.urlopen(url)
    try:
        return resource.read().decode(decoder)
    except:
        print('failed decoding')

def convertLoc(file):
    loc = {}
    file = file.replace('\r', '').split('\n')
    for line in file:
        if len(line) > 1:
            Param = line.index('Param_')+6
            line = line[Param:]
            try:
                [line, text] = line.split('\t')
                lI = line.rindex('_')
                iname = line[:lI]
                type = line[lI+1:]
            except:
                lI = line.rindex('_')
                iname = line[:lI]
                type = line[lI+1:]
                text = ""

            if iname not in loc:
                loc[iname] = {}
            loc[iname][type.lower()] = text

    return loc
    # pattern=r'^SRPG_(.+)Param_(.+)\_([^\t]+)\t(.*)\n$'#r'(?<=Param_)(.*)(?=\_).(.*)\t(.*)\n'
    #regex= re.compile(pattern, re.MULTILINE)
    # file=file
    # localisation={}
    # for match in regex.finditer(file):
    #    if match.group(2) not in localisation:
    #        localisation[match.group(2)]={}
    #    localisation[match.group(2)][match.group(3)]=match.group(4)
    # return(localisation)

def convertUnit(file):
    unit = {}
    file = file.replace('\r', '').split('\n')
    for line in file:
        if len(line) > 1:
            lI = line.rindex('_')
            iname = line[:lI]
            try:
                fI = line.index('\t')
                type = line[lI+1:fI]
                text = line[fI+1:]
            except:
                type = line[lI+1:]
                text = ""

            if iname not in unit:
                unit[iname] = {}
            unit[iname][type.lower()] = text

    return unit

def convertSys(file):
    sys={}
    file = file.replace('\r', '').split('\n')
    for line in file:
        line=line.split('\t')
        if len(line)==2:
            sys[line[0]]=line[1]
        else:
            sys[line[0]]=''
    return sys

def convertParam(file):
    return file
    for main,tree in file.items():
        if type(tree)==list and type(tree[0]) == dict and 'iname' in tree[0]:
            new_tree={}
            for obj in tree:
                new_tree[obj['iname']]=obj
            file[main]=new_tree
    return file
            
###########API##########################################


def api_connect(url, body={}, request="POST"):
    global api
    global ticket
    global con

    def get_default_headers(body):
        global access_token
        content_len=len(json.dumps(body))
        RID=str(uuid.uuid4()).replace('-','')
        header = {
            'X-GUMI-DEVICE-PLATFORM': 'android',
            'X-GUMI-DEVICE-OS': 'android',
            'X-Gumi-Game-Environment': 'sg_dev',
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
            "Host": api['con'],
            "Connection": "Keep-Alive",
            "Accept-Encoding": "gzip",
            "Content-Length": content_len
            }
        if url!="/gauth/accesstoken":
            if access_token == "":
                access_token = req_accesstoken()
            header["Authorization"] = "gauth " + access_token
        
        return(header)
    
    body['ticket']=ticket
    headers = get_default_headers(body)

    con = http.client.HTTPSConnection(api['con'])
    con.connect()

    con.request(request, url, json.dumps(body), headers)
    res_body = con.getresponse().read()

    con.close()

    json_res= json.loads(res_body)

    if json_res['stat'] == 5002:
        print (json_res)
        access_token=""
        print ('reconnecting')
        json_res = api_connect(url, body)

    if json_res['stat'] != 0:
        print (res_body)
        input ('Error')
        access_token=""
        json_res = api_connect(url, body)

    ticket+=1
    return(json_res)

def req_accesstoken():
    global api
    global ticket
    global idfa
    global idfv

    ticket=0

    idfa=str(uuid.uuid4())
    idfv=str(uuid.uuid4())

    body = {
        "access_token": "",
        "param": {
            "device_id": api['device_id'],
            "secret_key": api['secret_key'],
            "idfa": idfa,  # Google advertising ID
            "idfv": idfv,
            "udid":""
        }
    }
    res_body=api_connect("/gauth/accesstoken", body)
    
    try:
        ret = res_body['body']['access_token']
    except:
        print('error: failed to retrieve access-token')
        print(res_body)
        ret = ""

    return ret
    
def req_gacha():
    res_body=api_connect("/gacha")
    
    try:
        ret = res_body['body']
    except:
        print('error: failed to retrieve gacha')
        print(res_body)
        ret = ""

    return ret    

def req_param(param):
    res_body=api_connect("/mst/10/"+param,{},'GET')
    return res_body['body']

def get_files():
    #opening masterparam
    global access_token
    global api
    Rangedz='https://bitbucket.org/Rangedz/alccodetext/raw/master/'
    LolGL = 'https://bitbucket.org/Lolaturface/alchemistcodedata/raw/master/'
    LolJP = 'https://bitbucket.org/Lolaturface/tagatamedata/raw/master/'
#global from server
    print('global')
    # MasterParam
    print('MasterParam')
    saveAsJSON('MasterParam.json', convertParam(req_param('master')),'GameFiles')

    # QuestParam
    print('QuestParam')
    saveAsJSON('QuestParam.json', convertParam(req_param('quest')),'GameFiles')

    #Gacha
    print('Gacha')
    saveAsJSON('Gacha.json', req_gacha(),'GameFiles')

    # from bitucket

# global - Rangedz
    # LocalizedMasterParam
    name = 'LocalizedMasterParam'
    print(name)
    file = download(Rangedz+name)
    with open(path+name, "wt", encoding='utf-8') as f:
        f.write(file)
    saveAsJSON(name+'.json', convertLoc(file),'GameFiles')

    # LocalizedQuestParam
    name = 'LocalizedQuestParam'
    print(name)
    file = download(Rangedz+name)
    with open(path+name, "wt", encoding='utf-8') as f:
        f.write(file)
    saveAsJSON(name+'.json', convertLoc(file),'GameFiles')

    # QuestDropParam
    name='QuestDropParam.json'
    print(name)
    file = download(Rangedz+name)
    with open(path+'GameFiles\\'+name, "wt", encoding='utf-8') as f:
        f.write(file)

    #sys
    name='sys'
    print(name)
    file = download(LolGL+name).replace("MagicAttack	Modify MATK","MagicAttack	Modify Magic ATK").replace('Modify ','')
    SYS_FIX={
        "Assist_ESA_Fire" : "Fire Skill Dmg against Wind",
        "Assist_ESA_Water" : "Water Skill Dmg against Fire",
        "Assist_ESA_Wind" : "Wind Skill Dmg against Thunder",
        "Assist_ESA_Thunder" : "Thunder Skill Dmg against Water",
        "Assist_ESA_Shine" : "Light Skill Dmg against Dark",
        "Assist_ESA_Dark" : "Dark Skill Dmg against Light",
        "Resist_ESA_Fire" : "Fire Skill Dmg Red against Wind",
        "Resist_ESA_Water" : "Water Skill Dmg Red against Fire",
        "Resist_ESA_Wind" : "Wind Skill Dmg Red against Thunder",
        "Resist_ESA_Thunder" : "Thunder Skill Dmg Red against Water",
        "Resist_ESA_Shine" : "Light Skill Dmg Red against Dark",
        "Resist_ESA_Dark" : "Dark Skill Dmg Red against Light",
        "UnitDefenseFire" : "Fire Skill Dmg Red",
        "UnitDefenseWater" : "Water Skill Dmg Red",
        "UnitDefenseWind" : "Wind Skill Dmg Red",
        "UnitDefenseThunder" : "Thunder Skill Dmg Red",
        "UnitDefenseShine" : "Light Skill Dmg Red",
        "UnitDefenseDark" : "Dark Skill Dmg Red",
        "Assist_MaxDamageHp" : "Max HP Dmg",
        "Assist_MaxDamageMp" : "Max Jewel Dmg",
        "Resist_MaxDamageHp" : "Max HP Dmg Res",
        "Resist_MaxDamageMp" : "Max Jewel Dmg Res"
    }
    for key, val in SYS_FIX.items():
        file+= '{key}\t{val}\n'.format(key=key,val=val)
    with open(path+name, "wt", encoding='utf-8') as f:
        f.write(file)
    saveAsJSON(name+'.json', convertSys(file),'GameFiles')

    

#unit fusion
    # unit ~ lore
    name = 'unitJP'
    print(name)
    file = download(LolJP+'unit', 'utf16')
    with open(path+name, "wt", encoding='utf-8') as f:
        f.write(file)

    unit = convertUnit(file)

    name = 'unit'
    print(name)
    file = download(Rangedz+name)
    with open(path+name, "wt", encoding='utf-8') as f:
        f.write(file)

    unit.update(convertUnit(file))
    saveAsJSON(name+'.json', unit,'GameFiles')

#japan - Lolaturface
    print("jp")

    name = 'MasterParamJP.json'
    print(name)
    file = download(LolJP+'MasterParam.json', 'utf-8')
    saveAsJSON(name, convertParam(json.loads(file)),'GameFiles')

    name = 'QuestParamJP.json'
    print(name)
    file = download(LolJP+'QuestParam.json', 'utf-8')
    saveAsJSON(name, convertParam(json.loads(file)),'GameFiles')

# wytesong's compendium
    print('wytesong\'s copendium')
    # https://docs.google.com/spreadsheets/d/1nNmHzEfU3OSt-VlGma4diO4405fmqDLWJ8LiiOsyRmg
    SSID = '1nNmHzEfU3OSt-VlGma4diO4405fmqDLWJ8LiiOsyRmg'
    url = "https://script.google.com/macros/s/AKfycbzMqkHxhLsiMWqtFDmMHzqgT4a1R8yhBAxHN6YRkeN1lotYmsfg/exec?id="+SSID
    wyte = download(url, 'utf8')
    saveAsJSON('wytesong.json', json.loads(wyte))

# Game's tierlist
    print('Game\'s tierlist')
    # https://docs.google.com/spreadsheets/d/1DWeFk0wiPaDKAYEcmf_9LnMFYy1nBy2lPTNAX52LkPU
    SSID = '1DWeFk0wiPaDKAYEcmf_9LnMFYy1nBy2lPTNAX52LkPU'
    url = "https://script.google.com/macros/s/AKfycbzMqkHxhLsiMWqtFDmMHzqgT4a1R8yhBAxHN6YRkeN1lotYmsfg/exec?type=array&id="+SSID
    game = download(url, 'utf8')
    saveAsJSON('tierlist_gl.json', json.loads(game))


#code ################################
get_files()
