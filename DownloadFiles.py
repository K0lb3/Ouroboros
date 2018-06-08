import sys
import os.path
import os
import json
import uuid
import http.client
import time
import urllib.request
import re
import zlib

#global vars ############################
path=os.path.dirname(os.path.realpath(__file__))+'\\res\\'

api=""
ticket=1
#global
api_gl = "app.alcww.gumi.sg"
device_id_gl= "af4a61bd-e020-4260-8df0-8ed8241e00d0"
secret_key_gl="9487b67b-a586-48db-894a-e6c2e93db44c"
asset_ver="120145a055674b37ba6d46f5c68d5b20cdc6c8a7_gumi"

#japan
api_jp="alchemist.gu3.jp"
device_id_jp= "288cdf57-6dcb-4ddd-9702-6ca77e51ce86"
secret_key_jp="12dc28f5-de5e-4007-9a8c-f546464a93d6"
asset_ver_jp='d41d3a3719ebc556440132a5d6348fbbced201e0_gumi'

#functions ################################
def download(url,decoder='utf-8-sig'):
    resource = urllib.request.urlopen(url)
    try:
        return  resource.read().decode(decoder)
    except:
        print ('failed decoding')

def convertLoc(file):
    loc={}
    file=file.replace('\r','').split('\n')
    for line in file:
        if len(line)>1 :
            Param = line.index('Param_')+6
            line=line[Param:]
            try:
                [line,text]=line.split('\t')
                lI=line.rindex('_')
                iname = line[:lI]
                type = line[lI+1:]
            except:
                lI=line.rindex('_')
                iname = line[:lI]
                type = line[lI+1:]
                text=""

            
            if iname not in loc:
                loc[iname]={}
            loc[iname][type]=text
    
    return loc
        #pattern=r'^SRPG_(.+)Param_(.+)\_([^\t]+)\t(.*)\n$'#r'(?<=Param_)(.*)(?=\_).(.*)\t(.*)\n'
        #regex= re.compile(pattern, re.MULTILINE)
        #file=file
        #localisation={}
        #for match in regex.finditer(file):
        #    if match.group(2) not in localisation:
        #        localisation[match.group(2)]={}
        #    localisation[match.group(2)][match.group(3)]=match.group(4)
        #return(localisation)
  
def convertUnit(file):
    unit={}
    file=file.replace('\r','').split('\n')
    for line in file:
        if len(line)>1 :
            lI=line.rindex('_')
            iname = line[:lI]
            try:
                fI=line.index('\t')
                type = line[lI+1:fI]
                text = line[fI+1:]
            except:
                type=line[lI+1:]
                text=""
            
            if iname not in unit:
                unit[iname]={}
            unit[iname][type]=text
    
    return unit
  
def req_param(access_token,param):
    global ticket
    body = json.dumps({"ticket": ticket})
    ticket += 1

    headers = get_default_headers(len(body))

    headers["Authorization"] = "gumi " + access_token

    con = http.client.HTTPSConnection(api)

    con.connect()
    con.request("GET", "/mst/10/"+param, body, headers)
    res_body = con.getresponse().read()
    con.close()

    return json.loads(res_body)['body']

def req_asset(name, asset):
    #https://app.alcww.gumi.sg
    global asset_ver
    global ticket
    
    url="http://prod-dlc-alcww-gumi-sg.akamaized.net/assets/"+asset_ver+"/Text/"+asset
    file = urllib.request.urlopen(url).read()
    file = zlib.decompress(file)
    
    return file
  
def req_asset_jp(name, asset): #doesn't work
    #https://alchemist.gu3.jp
    #http://alchemist-dlc2.gu3.jp/assets/d41d3a3719ebc556440132a5d6348fbbced201e0_gumi/apvr/REVISION.dat
    global asset_ver_jp
    global ticket
    
    url="http://alchemist-dlc2.gu3.jp/assets/"+asset_ver_jp+"/apvr/"+asset
    file = urllib.request.urlopen(url).read()
    file = zlib.decompress(file)
    
    return file
    
def req_accesstoken(device_id, secret_key):
    global ticket
    body = json.dumps({
        "ticket": ticket,
        "access_token": "",
        "param": {
            "device_id": device_id,
            "secret_key": secret_key,
            "idfa": str(uuid.uuid4())  # Google advertising ID
        }
    })
    ticket += 1
    
    headers = get_default_headers(len(body))

    con = http.client.HTTPSConnection(api)

    con.connect()
    con.request("POST", "/gauth/accesstoken", body, headers)
    res_body = con.getresponse().read()
    con.close()

    return json.loads(res_body)
    
def get_default_headers(content_len):
    return {
        #"x-app-ver": networkver,
        "X-GUMI-TRANSACTION": str(uuid.uuid4()),
        "User-Agent": "Mozilla/5.0 (Linux; Android 4.4.2; SM-G7200 Build/KTU84P) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/30.0.0.0 Mobile Safari/537.36",
        "X-Unity-Version": "5.3.6p1",
        "Content-Type": "application/json; charset=utf-8",
        "Host": api,
        "Connection": "Keep-Alive",
        "Accept-Encoding": "gzip",
        "Content-Length": content_len
    }
   
def saveAsJSON(name, var):
    global path
    with open(path+name, "wb") as f:
      f.write(json.dumps(var, indent=4, ensure_ascii=False).encode('utf8'))
    
    return 1
    
def createParamTypes():
    paramType={
        "0": "None",
        "1": "Hp",
        "2": "HpMax",
        "3": "Mp",
        "4": "MpIni",
        "5": "Atk",
        "6": "Def",
        "7": "Mag",
        "8": "Mnd",
        "9": "Rec",
        "10": "Dex",
        "11": "Spd",
        "12": "Cri",
        "13": "Luk",
        "14": "Mov",
        "15": "Jmp",
        "16": "EffectRange",
        "17": "EffectScope",
        "18": "EffectHeight",
        "19": "Assist_Fire",
        "20": "Assist_Water",
        "21": "Assist_Wind",
        "22": "Assist_Thunder",
        "23": "Assist_Shine",
        "24": "Assist_Dark",
        "25": "Assist_Poison",
        "26": "Assist_Paralysed",
        "27": "Assist_Stun",
        "28": "Assist_Sleep",
        "29": "Assist_Charm",
        "30": "Assist_Stone",
        "31": "Assist_Blind",
        "32": "Assist_DisableSkill",
        "33": "Assist_DisableMove",
        "34": "Assist_DisableAttack",
        "35": "Assist_Zombie",
        "36": "Assist_DeathSentence",
        "37": "Assist_Berserk",
        "38": "Assist_Knockback",
        "39": "Assist_ResistBuff",
        "40": "Assist_ResistDebuff",
        "41": "Assist_Stop",
        "42": "Assist_Fast",
        "43": "Assist_Slow",
        "44": "Assist_AutoHeal",
        "45": "Assist_Donsoku",
        "46": "Assist_Rage",
        "47": "Assist_GoodSleep",
        "48": "Assist_ConditionAll",
        "49": "Resist_Fire",
        "50": "Resist_Water",
        "51": "Resist_Wind",
        "52": "Resist_Thunder",
        "53": "Resist_Shine",
        "54": "Resist_Dark",
        "55": "Resist_Poison",
        "56": "Resist_Paralysed",
        "57": "Resist_Stun",
        "58": "Resist_Sleep",
        "59": "Resist_Charm",
        "60": "Resist_Stone",
        "61": "Resist_Blind",
        "62": "Resist_DisableSkill",
        "63": "Resist_DisableMove",
        "64": "Resist_DisableAttack",
        "65": "Resist_Zombie",
        "66": "Resist_DeathSentence",
        "67": "Resist_Berserk",
        "68": "Resist_Knockback",
        "69": "Resist_ResistBuff",
        "70": "Resist_ResistDebuff",
        "71": "Resist_Stop",
        "72": "Resist_Fast",
        "73": "Resist_Slow",
        "74": "Resist_AutoHeal",
        "75": "Resist_Donsoku",
        "76": "Resist_Rage",
        "77": "Resist_GoodSleep",
        "78": "Resist_ConditionAll",
        "79": "HitRate",
        "80": "AvoidRate",
        "81": "CriticalRate",
        "82": "GainJewel",
        "83": "UsedJewelRate",
        "84": "ActionCount",
        "85": "SlashAttack",
        "86": "PierceAttack",
        "87": "BlowAttack",
        "88": "ShotAttack",
        "89": "MagicAttack",
        "90": "ReactionAttack",
        "91": "JumpAttack",
        "92": "GutsRate",
        "93": "AutoJewel",
        "94": "ChargeTimeRate",
        "95": "CastTimeRate",
        "96": "BuffTurn",
        "97": "DebuffTurn",
        "98": "CombinationRange",
        "99": "HpCostRate",
        "100": "SkillUseCount",
        "101": "PoisonDamage",
        "102": "PoisonTurn",
        "103": "Assist_AutoJewel",
        "104": "Resist_AutoJewel",
        "105": "Assist_DisableHeal",
        "106": "Resist_DisableHeal",
        "107": "Resist_Slash",
        "108": "Resist_Pierce",
        "109": "Resist_Blow",
        "110": "Resist_Shot",
        "111": "Resist_Magic",
        "112": "Resist_Reaction",
        "113": "Resist_Jump",
        "114": "Avoid_Slash",
        "115": "Avoid_Pierce",
        "116": "Avoid_Blow",
        "117": "Avoid_Shot",
        "118": "Avoid_Magic",
        "119": "Avoid_Reaction",
        "120": "Avoid_Jump",
        "122": "Reduced Jewel Cost",
        "131": "Fire DMG",
        "132": "Water DMG",
        "133": "Wind DMG",
        "134": "Thunder DMG",
        "135": "Light DMG",
        "136": "Dark DMG",
    }
    iname={}
    sys = req_asset('sys', 'b7c330d8').decode('utf-8-sig').replace('\r','').replace('Modify ','').split('\n')
    
    for line in sys:
        try:
            seperator=line.rindex('\t')
            iname[line[:seperator]]=line[seperator+1:]          
        except:
            print (line)
    
    for i in paramType:
        try:
            paramType[i]=iname[paramType[i]]
        except:
            pass
                           
    paramType[89]="Magic ATK"
    saveAsJSON('ParamTypes.json',paramType)
 
 
# main ############################### 
def main():
    global api
    global path
    os.makedirs(path, exist_ok=True)
    
    print("global")
#from game server    
    #access token
    api=api_gl
    access_token = req_accesstoken(device_id_gl,secret_key_gl)['body']['access_token']
    
    #MasterParam
    print('MasterParam')
    saveAsJSON('MasterParam.json', req_param(access_token,'master'))
    
    #QuestParam
    print('QuestParam')
    saveAsJSON('QuestParam.json', req_param(access_token,'quest'))
    
    #assets
    #req_asset(access_token, name, asset)
    print('ParamTypes')
    createParamTypes()
    
#from bitucket    

# global - Rangedz 
    #LocalizedMasterParam
    name='LocalizedMasterParam'
    print(name)
    file = download('https://bitbucket.org/Rangedz/alccodetext/raw/master/'+name)
    with open(path+name, "wt", encoding='utf-8') as f:
        f.write(file)
    saveAsJSON(name+'.json', convertLoc(file))
    
    #LocalizedQuestParam
    name='LocalizedQuestParam'
    print(name)
    file = download('https://bitbucket.org/Rangedz/alccodetext/raw/master/'+name)
    with open(path+name, "wt", encoding='utf-8') as f:
        f.write(file)
    saveAsJSON(name+'.json', convertLoc(file))
        
#fusion
    #unit ~ lore
    name='unitJP'
    print(name)
    file=  download('https://bitbucket.org/Lolaturface/tagatamedata/raw/master/unit','utf16')
    with open(path+name, "wt", encoding='utf-8') as f:
        f.write(file)
    
    unit=convertUnit(file)
        
    name='unit'
    print(name)
    file = download('https://bitbucket.org/Rangedz/alccodetext/raw/master/'+name)
    with open(path+name, "wt", encoding='utf-8') as f:
        f.write(file)
        
    unit.update(convertUnit(file))    
    saveAsJSON(name+'.json', unit)

 
#japan - Lolaturface    
    print("jp")

    name='MasterParamJP.json'
    print(name)
    file=  download('https://bitbucket.org/Lolaturface/tagatamedata/raw/master/MasterParam.json','utf-8')
    saveAsJSON(name,json.loads(file))
    
    name='QuestParamJP.json'
    print(name)
    file=  download('https://bitbucket.org/Lolaturface/tagatamedata/raw/master/QuestParam.json','utf-8')
    saveAsJSON(name,json.loads(file))

#wytesong's compendium
    print('wytesong\'s copendium')
    SSID='1nNmHzEfU3OSt-VlGma4diO4405fmqDLWJ8LiiOsyRmg' #https://docs.google.com/spreadsheets/d/1nNmHzEfU3OSt-VlGma4diO4405fmqDLWJ8LiiOsyRmg
    url = "https://script.google.com/macros/s/AKfycbzMqkHxhLsiMWqtFDmMHzqgT4a1R8yhBAxHN6YRkeN1lotYmsfg/exec?id="+SSID
    wyte = download(url,'utf8')
    saveAsJSON('wytesong.json',json.loads(wyte))

#Game's tierlist
    print('Game\'s tierlist')
    SSID='1DWeFk0wiPaDKAYEcmf_9LnMFYy1nBy2lPTNAX52LkPU' #https://docs.google.com/spreadsheets/d/1DWeFk0wiPaDKAYEcmf_9LnMFYy1nBy2lPTNAX52LkPU
    url = "https://script.google.com/macros/s/AKfycbzMqkHxhLsiMWqtFDmMHzqgT4a1R8yhBAxHN6YRkeN1lotYmsfg/exec?type=array&id="+SSID
    game = download(url,'utf8')
    saveAsJSON('tierlist_gl.json',json.loads(game))

        
#code ################################       
        
main()
