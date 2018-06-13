import sys
import json
import uuid
import http.client
import time
import zlib

#global vars ############################
#path=os.path.dirname(os.path.realpath(__file__))+'\\res\\'
access_token=""

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

def api_connect(url, body={}):
    global api
    global ticket
    global con

    def get_default_headers(body):
        global access_token
        content_len=len(json.dumps(body))
        header = {
            #"x-app-ver": networkver,
            "X-GUMI-TRANSACTION": str(uuid.uuid4()),
            "User-Agent": "Mozilla/5.0 (Linux; Android 4.4.2; SM-G7200 Build/KTU84P) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/30.0.0.0 Mobile Safari/537.36",
            "X-Unity-Version": "5.3.6p1",
            "Content-Type": "application/json; charset=utf-8",
            "Host": api['con'],
            "Connection": "Keep-Alive",
            "Accept-Encoding": "gzip",
            "Content-Length": content_len
            }
        if url!="/gauth/accesstoken":
            if access_token=="":
                access_token=req_accesstoken()
            if api['con'] == "app.alcww.gumi.sg":
                header["Authorization"] = "gumi " + access_token
            if api['con'] == "alchemist.gu3.jp":
                header["Authorization"] = "gauth " + access_token
        
        return(header)
    
    
    body['ticket']=ticket
    headers = get_default_headers(body)

    con = http.client.HTTPSConnection(api['con'])
    con.connect()

    con.request("POST", url, json.dumps(body), headers)
    res_body = con.getresponse().read()
    con.close()

    json_res= json.loads(res_body)

    if json_res['stat'] == 5002:
        access_token=""
        json_res = api_connect(url, body)

    ticket+=1
    return(json_res)

def req_accesstoken():
    global api
    global ticket
    ticket=1

    body = {
        "access_token": "",
        "param": {
            "device_id": api['device_id'],
            "secret_key": api['secret_key'],
            "idfa": str(uuid.uuid4())  # Google advertising ID
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
    
def req_arena_ranking():
    res_body=api_connect("/btl/colo/ranking/world")
    
    try:
        ret = res_body['body']["coloenemies"]
    except:
        print('error: failed to retrieve arena ranking')
        print(res_body)
        ret = ""
    
    return ret

print(req_arena_ranking())
print(req_arena_ranking())
print(req_arena_ranking())
print(req_arena_ranking())