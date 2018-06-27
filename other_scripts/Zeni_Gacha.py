import sys
import json
import uuid
import http.client
import time
import os.path
import datetime


#global vars ############################
access_token=""
idfa=""
idfv=""

ticket=1
#global
api={
    'con':"app.alcww.gumi.sg",
    'device_id':"",
    'secret_key':"",
    }

###########decoding#########################################


key = bytearray([0x08, 0x38, 0x55, 0x64, 0x17, 0xa0, 0x78, 0x4c, 0xf5, 0x97, 0x86, 0x4b, 0x16, 0xac, 0x9d, 0xd9,
                     0xaa, 0x1c, 0x81, 0x7a, 0x27, 0xae, 0x3f, 0x2c, 0xa1, 0x95, 0x80, 0xf4, 0xc8, 0x97, 0xd8, 0x6d,
                     0x98, 0x2c, 0x12, 0x5b, 0x88, 0x74, 0x13, 0xbe, 0xe6, 0x84, 0xda, 0xac, 0x14, 0x19, 0xf3, 0x38,
                     0x8a, 0xe2, 0x9d, 0x5d, 0xa0, 0x5c, 0x03, 0x71, 0xf6, 0x5b, 0x56, 0xb6, 0x48, 0x14, 0xe7, 0x16,
                     0xea, 0x44, 0x3b, 0xd0, 0xd8, 0x20, 0xd5, 0x65, 0xe9, 0xbe, 0xf9, 0xb2, 0xa8, 0x49, 0x1e, 0x80,
                     0x1e, 0xd8, 0x80, 0xf1, 0x3f, 0x71, 0x5f, 0x79, 0x92, 0xe3, 0xef, 0xb8, 0xbe, 0xe9, 0x63, 0x5a,
                     0x1e, 0xcf, 0x24, 0x5b, 0x87, 0x6b, 0xa2, 0xdc, 0x13, 0x3d, 0x7b, 0xfe, 0x19, 0x60, 0x53, 0xcf,
                     0x13, 0x03, 0x45, 0x4f, 0x0f, 0x84, 0xc8, 0x87, 0xac, 0x2a, 0xd5, 0xbc, 0x70, 0xbd, 0xfd, 0x66])

def decrypt(encrypted_bytes):
    result = bytearray(len(encrypted_bytes))

    for i in range(0, len(encrypted_bytes)):
        previous = 0x99 if i == 0 else result[i - 1]
        result[i] = previous ^ encrypted_bytes[i] ^ key[i & 0x7f]

    return result

def load_gu3c():
    global api
    #encoding gu3c.dat
    path = os.path.dirname(os.path.realpath(__file__))+"/gu3c.dat"
    if not os.path.exists(path):
        print("", path, " was not found.")
        input("\n \n Press enter to exit :(")
        sys.exit(0)

    with open(path, "rb") as f:
        content = bytearray(f.read())

    print("Decrypting gu3c.dat...")
    (api['device_id'], api['secret_key']) = decrypt(content).decode("utf-8").split(" ")

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
        access_token=""
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

def req_gacha():
    res_body=api_connect("/gacha")
    
    try:
        ret = res_body['body']
    except:
        print('error: failed to retrieve gacha')
        print(res_body)
        ret = ""

    return ret    

def req_login():
    res_body=api_connect("/login",{"param":{"device":"HUAWEI HUAWEI MLA-L12","dlc":"apvr"}})
    
    try:
        ret = res_body['body']
    except:
        print('error: failed to retrieve login')
        print(res_body)
        ret = ""

    return ret

def print_login(json_res):
    print("--------------------------------------------")
    print("Name:", json_res["player"]["name"])
    print("P. Lv:", json_res["player"]["lv"])
    print("User Code:", json_res["player"]["cuid"])
    print("Friend ID:", json_res["player"]["fuid"])
    print("Created at:", json_res["player"]["created_at"])
    print("Exp:", json_res["player"]["exp"])
    print("Stamina:", json_res["player"]["stamina"]["pt"], "/", json_res["player"]["stamina"]["max"])
    print("Zeni:", json_res["player"]["gold"])
    print("Gems:", json_res["player"]["coin"]["paid"], "Paid,", json_res["player"]["coin"]["free"], "Free")
    print("--------------------")

def do_zeni_gacha():
    res_body=api_connect("/gacha/exec",{"param":{"gachaid":"Normal_Gacha_10","free":0}})
    
    try:
        ret = res_body['body']
    except:
        print('error: failed to retrieve zeni gacha gacha')
        print(res_body)
        ret = ""

    return ret   


def spam_zeni_gacha():
    #opening masterparam
    global access_token

    #sending clears
    login=req_login()
    print_login(login)
    zeni=login["player"]["gold"]
    if zeni<90000:
        exit()

    count=int(input('How many zeni gacha pulls?: '))
    total={'pull':[],'item':{}}
    for i in range(0,count):
        print(str(i+1),count,sep='\t/\t')
        req_gacha()
        result=do_zeni_gacha()
        total['pull'].append(result['add'])
        for j in result['add']:
            try:
                total['item'][j['iname']]+=j['num']
            except:
                total['item'][j['iname']]=j['num']

        if result['player']['gold']<90000:
            break

    #print pull results:
    for key, num in total['item'].items():
        print(key,num,sep=':\t')

    #save result
    timestamp='{:%y.%m.%d_%H-%M-%S}'.format(datetime.datetime.utcnow())
    with open('Zeni Gacha - {ts}.json'.format(ts=timestamp), "wb") as f:
        f.write(json.dumps(total, indent=4, ensure_ascii=False).encode('utf8'))
    input('Finished doing Zeni Gacha\n press any key to exit')

load_gu3c()
spam_zeni_gacha()