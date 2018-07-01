import os
import json
import re

def EnumToJson():
    # path to files
    path=os.path.dirname(os.path.realpath(__file__))+'\\res\\'
    mypath = os.path.dirname(os.path.realpath(__file__))+'\\Assembly_SRPG_JP\\'
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
                if ' ' in name:
                    name=name[:name.index(' ')]
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
                        if bracket!=1:
                            continue
                        if '=' in line: # = 16, // 0x00000010"
                            try:
                                line=line.split(' = ',1)
                                line[1]=line[1].rstrip(',').replace('L','')
                                cEnum[1][int(line[1],0)]=line[0]
                            except IndexError:
                                continue
                        else:
                            cEnum[0].append(line.rstrip(','))

                if len(cEnum[0])==0:
                    use=1
                print(str(len(cEnum[use])))    
                enum[name]=cEnum[use]
        except PermissionError:
            print('PermissionError:')
    
    name=(path+'Enums.json')
    dump=json.dumps(enum, indent=4, ensure_ascii=False)
    #fix stringified indexes
    
    #dump = re.sub( r'"(\d+)"(:)',r'\1\2',dump)

    os.makedirs(name[:name.rindex("\\")], exist_ok=True)
    with open(name, "wb") as f:
        f.write(dump.encode('utf8'))

EnumToJson()