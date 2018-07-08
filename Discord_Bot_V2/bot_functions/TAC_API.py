import sys
import json
import uuid
import http.client
import time
import zlib

#global vars ############################
#path=os.path.dirname(os.path.realpath(__file__))+'\\res\\'
access_token=""
idfa=""
idfv=""

api={
    'con':"",
    'device_id':"",
    'secret_key':"",
    'asset_ver':""
    }
ticket=1
#global
api_gl={
    'con':"app.alcww.gumi.sg",
    'device_id':"af4a61bd-e020-4260-8df0-8ed8241e00d0",
    'secret_key':"9487b67b-a586-48db-894a-e6c2e93db44c",
    'asset_ver':"120145a055674b37ba6d46f5c68d5b20cdc6c8a7_gumi"
    }

#japan
api_jp={
    'con':"alchemist.gu3.jp",
    'device_id':"288cdf57-6dcb-4ddd-9702-6ca77e51ce86",
    'secret_key':"12dc28f5-de5e-4007-9a8c-f546464a93d6",
    'asset_ver':"d41d3a3719ebc556440132a5d6348fbbced201e0_gumi"
    }

api=api_gl
###########code#########################################

def api_connect(url, body={}, request="POST"):
    global api
    global ticket
    global con

    def get_default_headers(body):
        content_len=len(json.dumps(body))
        RID=str(uuid.uuid4()).replace('-','')
        header = {
            #'x-asset-ver': '7f10f37f81e36b1c08839b6cc6c43d54b3c9f232_gumi',
            #'x-app-ver': 'f8e916f30ab8bb21f9efd4ae6bf6ea96944098c3_a',
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
            "Host": api['con'],
            "Connection": "Keep-Alive",
            "Accept-Encoding": "gzip",
            "Content-Length": content_len
            }
        if url!="/gauth/accesstoken":
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
    
def req_chk_player():
    global api
    global ticket
    global idfa

    ticket=1

    body = {
        "access_token": "",
        "param": {
            "device_id": api['device_id'],
            "secret_key": api['secret_key'],
            "idfa": idfa,  # Google advertising ID
        }
    }
    res_body=api_connect("/player/chkplayer", body)
    
    try:
        ret = res_body['body']['result']
    except:
        print('error: failed to retrieve chklayer')
        print(res_body)
        ret = ""

    return ret

def req_achieve_auth():
    return(api_connect('/achieve/auth',{},'GET'))

def req_arena_ranking():
    res_body=api_connect("/btl/colo/ranking/world")
    
    try:
        ret = res_body['body']
    except:
        print('error: failed to retrieve arena')
        print(res_body)
        ret = ""
  
    return ret['coloenemies']

def req_login():
    res_body=api_connect("/login",{"param":{"device":"HUAWEI HUAWEI MLA-L12","dlc":"apvr"}})
    
    try:
        ret = res_body['body']
    except:
        print('error: failed to retrieve login')
        print(res_body)
        ret = ""

    return ret
