import os
import json

def saveAsJSON(name, var):
    os.makedirs(name[:name.rindex("\\")], exist_ok=True)
    with open(name, "wb") as f:
        f.write(json.dumps(var, indent=4, ensure_ascii=False).encode('utf8'))

def EnumToJson():
    # path to files
    path=os.path.dirname(os.path.realpath(__file__))+'\\res\\'
    mypath = os.path.dirname(os.path.realpath(__file__))+'\\Assembly-CSharp\\SRPG\\'
    files = os.listdir(mypath)

    # enum
    enum = {}

    for f in files:
        print(f)
        try:
            with open(mypath+f, "rt", encoding='utf8') as f:
                file = f.read()

            pre='public enum '

            while pre in file:
                file=file[file.index(pre)+len(pre):]
                text=file.split('\n')

                cEnum=[[],{}]
                use=0
                name=text[0]
                bracket=0

                for line in text[1:]:
                    line=line.lstrip(' ').rstrip(' ')
                    if line == '{':
                        bracket+=1
                    elif line == '}':
                        bracket-=1
                        if bracket==0:
                            break
                    else: #normal entry
                        if '=' in line: # = 16, // 0x00000010"
                            line=line.split(' = ')
                            line[1]=line[1][:line[1].index(',')]
                            cEnum[1][line[1]]=line[0]
                        else:
                            cEnum[0].append(line.rstrip(','))

                if len(cEnum[0])==0:
                    use=1
                print(str(len(cEnum[use])))    
                enum[name]=cEnum[use]
        except PermissionError:
            print('PermissionError:')
    
    saveAsJSON(path+'Enums.json',enum)

    with open(path+'sys.json', "rt", encoding='utf8') as f:
        sys = json.loads(f.read())

    lenum={}
    for param,items in enum.items():
        if type(items)==list:
            lenum[param]=[
                sys[item] if item in sys else item
                for item in items
                ]
        elif type(items)==dict:
            lenum[param]={
                key : sys[item] if item in sys else item
                for key,item in items.items()
                }
        else:
            print(param, items)

    saveAsJSON(path+'locEnums.json',lenum)

EnumToJson()