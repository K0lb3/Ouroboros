import os
import json
from MainFunctions import ENUM

def saveAsJSON(name, var):
    os.makedirs(name[:name.rindex("\\")], exist_ok=True)
    with open(name, "wb") as f:
        f.write(json.dumps(var, indent=4, ensure_ascii=False).encode('utf8'))

def ConvertAssembly():
    # path to files
    path=os.path.dirname(os.path.realpath(__file__))+'\\res\\'
    mypath = os.path.dirname(os.path.realpath(__file__))+'\\Assembly_SRPG_JP\\'
    epath = os.path.dirname(os.path.realpath(__file__))+'\\genConvert\\'
    files = os.listdir(mypath)

    # Convert
    Convert = {}
    Link={}

    for f in files:
        print(f)
        try:
            with open(mypath+f, "rt", encoding='utf8') as f:
                file = f.read()

            pre='Deserialize(JSON_'

            while pre in file:
                file=file[file.index(pre)+len(pre):].replace(' ','')
                text=file.split('\n')

                name=text[0][:-5] #removing  json)
                current=[]
                bracket=0
                found=0
                for ind,line in enumerate(text,1):
                    line=line.lstrip(' ').rstrip(' ')
                    if line == '{':
                        bracket+=1
                    elif line == '}':
                        bracket-=1
                        if bracket==0:
                            break
                    else: #normal entry
                        try:
                            [new,old]=line.split('=',1)
                            if 'this.' in new and 'json.' in old:
                                if 'new' in line and '[index]' in text[ind+1]:
                                    text[ind+1]=text[ind+1].replace('[index]','')
                                    continue
                                current.append((bracket,[new,old]))
                                found+=1
                            else:
                                current.append((bracket,[line]))
                        except ValueError:
                            current.append((bracket,[line]))

                if found==0:
                    continue
                print(str(len(current)))

                # convert to code
                cText='def {name}(json):\n    this={b}'.format(name=name,b='{}')
                this={}
                Link[name]={}
                for bracket, info in current:
                    #print(info)
                    indent=bracket*4

                    if len(info)==1:
                        cText+='{indent}#{outcommented}\n'.format(indent=' '*indent, outcommented=info[0])

                    else:
                        #this inputs
                        #this[][]preparation
                        tdirs=info[0].split('.')[1:]
                        if len(tdirs)>1:
                            cThis=this
                            for ind,var in enumerate(tdirs):
                                if var not in cThis:
                                    cThis[var]={}
                                    cText+='{indent}this[\'{inputs}\']\n'.format(inputs='\'][\''.join(tdirs[:ind]),indent=' '*indent)
                                cThis=cThis[var]

                        #if x is in json
                        jtext=info[1]
                        j_index=jtext.index('json.')+5
                        ind=j_index
                        jvar=''
                        while jtext[ind].isalnum() or jtext[ind]=='_':
                            jvar+=jtext[ind]
                            ind+=1
                        cText+='{indent}if \'{var}\' in json:\n'.format(indent=' '*indent,var=jvar)
                        #reforming json line
                        jtext=jtext.replace('.'+jvar,'[\''+jvar+'\']')
                        #ENUM usage???
                        if '(' in jtext:
                            param=jtext[jtext.index('(')+1:jtext.index(')')]
                            if param in ENUM:
                                jtext='ENUM[\'{param}\'][json[\'{jvar}\']]'.format(param=param,jvar=jvar)
                            else:
                                jtext=jtext.replace('('+param+')','')

                        indent+=4
                                
                        #final statemant
                        this_path='this'
                        for var in tdirs:
                            var=var.split('[')
                            this_path+='[\'{}\']'.format(var[0])
                            print(var)
                            if len(var)>=2:
                                for pa in var[1]:
                                    pa=pa.split(']')
                                    this_path+='[{}]'.format(pa[0])
                                    print(pa)
                                    if len(pa)>=2 and pa[1] != '':
                                        this_path+='[\'{}\']'.format(pa[1])

                                
                        cText+='{indent}{this} = {json}\n'.format(
                            indent=' '*indent,
                            this= this_path,#'this[\'{inputs}\']'.format(inputs='\'][\''.join(tdirs)),
                            json= jtext
                            )
                        Link[name]['.'.join(tdirs)]=jvar
                cText+='return this\n'

                Convert[name]=cText.replace(';','').replace('[index]','').replace('[]','').replace('[i][n][d][e][x]','')
        except PermissionError:
            print('PermissionError:')
    

    saveAsJSON(path+'Convert_Try.json',Convert)
    saveAsJSON(path+'Convert_Link.json',Link)
    os.makedirs(epath, exist_ok=True)
    for name,code in Convert.items():
        with open(epath+name+'.py', "wt",encoding='utf8') as f:
            f.write(code)



ConvertAssembly()