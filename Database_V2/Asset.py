# #!/usr/bin/perl
import json
import os
import http.client
import urllib.request
import struct
import zlib

gameVersion = "3b43e34d4f54bc49d5e84dd80c27a7a30148023d_a"
host = "api.alcww.gumi.sg"

def Download_Text_Assets():
    global host, gameVersion
    #global
    path=os.path.join(os.path.dirname(os.path.realpath(__file__)),'resources\\Assets')

    gTEXT=get_assetlist()
    for asset in gTEXT['assets']:
        if 'Compressed' not in asset['flags'] or 'RawData' not in asset['flags']:
            continue
        print(asset['path'])
        data = requestAsset(gTEXT['host'],gTEXT['version'],'aatc',asset['id'])
        if 'Compressed' in asset['flags']:
            data=zlib.decompress(data)

        fpath=os.path.join(path,asset['path'].replace('/','\\'))
        os.makedirs(fpath.rsplit('\\',1)[0], exist_ok=True)
        with open(fpath, 'wb') as f:
            f.write(data)

def get_assetlist(listT='aatc',save=False):
    ver = req_chkver()['body']
    version= ver['assets']
    host_dl= ver['host_dl']
    #lists=['apvr','aatc','text']

    AssetList = requestAsset(host_dl,version,listT,"ASSETLIST")
    res= parse_assetlist(AssetList)
    res['gameVersion']=gameVersion
    res['version']=version
    res['host']= host_dl
    #format to path
    a2={}
    for asset in res['assets']:
        f = asset['path'].split('/')
        path=a2
        for index,p in enumerate(f):
            if index!= len(f)-1:
                if p not in path:
                    path[p]={}
                path=path[p]
            else:
                path[p]=asset
    res['path']=a2
    if save:
        with open('AssetList-{}.json'.format(listT),'w') as f:
            json.dump(res,f,indent=2)
    return res


def parse_assetlist(fh):
    global pointer
    pointer=0

    bundleFlags = ["Compressed","RawData","Required","Scene","Tutorial","Multiplay","StreamingAsset","TutorialMovie","Persistent","DiffAsset",None,None,"IsLanguage","IsCombined","IsFolder"]

    assetlist = {
        'revision'  : readInt32(fh),
        'length'    : readInt32(fh)
    }    
    assetlist["assets"] = [{
            "id" :          "%08x" % readUInt32(fh),
            "size" :        readInt32(fh),
            "compSize" :    readInt32(fh),
            "path" :        readString(fh),
            "pathHash" :    readInt32(fh),
            "hash" :        readUInt32(fh),
            "flags" : [
                bundleFlags[index]               #flags
                for index, bit in enumerate( bin(readInt32(fh))[2:][::-1] )
                if bit == "1"
            ],
            "dependencies" : [
                readInt32(fh)     #array length
                for j in range(readInt32(fh))
            ],
            "additionalDependencies" : [
                readInt32(fh)     #array length
                for j in range(readInt32(fh))
            ],
            "additionalStreamingDependencies" : [
                readInt32(fh)     #array length
                for j in range(readInt32(fh))
            ],
        }
        for i in range(0, assetlist["length"])
    ]
    return assetlist
    
def readInt32(fh):
    byts = readBytes(fh,4)
    return  struct.unpack('i', byts)[0]

def readUInt32(fh):
    byts = readBytes(fh,4)
    return  struct.unpack('I', byts)[0]

def readString(fh):
    res = ""
    control = "1"
    while control == "1":
        bit_str = bin(ord(readBytes(fh,1)))[2:].zfill(8)
        control = bit_str[0]
        res += bit_str[1:]
    length = int(res,2)

    return struct.unpack("{}s".format(length), readBytes(fh,length))[0].decode("utf8") if length else ''

def readBytes(fh,size):
    global pointer
    bts=fh[pointer:size+pointer]
    pointer+=size
    return bts

##API ############################################
def requestAsset(host_dl,version,typ,name):
    #request asset list
    url = '{host_dl}/assets/{version}/{typ}/{name}'.format(
        host_dl=host_dl,
        version=version,
        typ=typ,
        name=name
        )
    print(url)
    asset = urllib.request.urlopen(url).read()
    return asset

def get_default_headers(content_len):
    return {
        "x-app-ver": gameVersion,
        'X-GUMI-DEVICE-PLATFORM': 'android',
        'X-GUMI-DEVICE-OS': 'android',
        'X-Gumi-Game-Environment': 'sg_dev',
        'X-GUMI-CLIENT': 'gscc ver.0.1',
        "User-Agent": "Mozilla/5.0 (Linux; Android 4.4.2; SM-G7200 Build/KTU84P) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/30.0.0.0 Mobile Safari/537.36",
        "X-Unity-Version": "5.3.6p1",
        "Content-Type": "application/json; charset=utf-8",
        "Host": host,
        "Connection": "Keep-Alive",
        "Accept-Encoding": "gzip",
        "Content-Length": content_len
    }

def req_chkver():
    body = json.dumps({
        "param": {
            "ver": gameVersion,
            "os": 'android'
        }
    })

    con = http.client.HTTPSConnection(host)
    con.connect()
    con.request("POST", "/chkver", body, get_default_headers(len(body)))
    res_body = con.getresponse().read()
    con.close()

    return json.loads(res_body)


Download_Text_Assets()
