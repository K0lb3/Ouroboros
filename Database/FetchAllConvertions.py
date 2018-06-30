import os
import json

def saveAsJSON(name, var):
    os.makedirs(name[:name.rindex("\\")], exist_ok=True)
    with open(name, "wb") as f:
        f.write(json.dumps(var, indent=4, ensure_ascii=False).encode('utf8'))

def ConvertAssembly():
    # path to files
    path=os.path.dirname(os.path.realpath(__file__))+'\\res\\'
    mypath = os.path.dirname(os.path.realpath(__file__))+'\\Assembly-CSharp\\SRPG\\'
    files = os.listdir(mypath)

    # Convert
    Convert = {}

    for f in files:
        print(f)
        try:
            with open(mypath+f, "rt", encoding='utf8') as f:
                file = f.read()

            pre='Deserialize(JSON_'

            while pre in file:
                file=file[file.index(pre)+len(pre):].replace(' ','')
                text=file.split('\n')

                name=text[0][:-6] #removing  json)
                current=[]
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
                        try:
                            [new,old]=line.split('=',1)
                            if 'this' in new and 'json' in old:
                                    current.append((bracket,[new,old]))
                        except ValueError:
                            current.append((bracket,[line]))

                print(str(len(current)))

                # convert to code
                cText='def {name}(json):\n    this=\{\}'.format(name=name)
                for line in Convert:
                    this={}
                    for bracket, info in line:
                        indent=4*bracket

                        if len(info)==1:
                            cText+='{indent}#{outcommented}\n'.format(indent=' '*indent, outcommented=info[0])

                        else:
                            #this inputs

                            #if x is in json
                            #reforming json line
                            cText+='{indent}if {var} in json:\n'.format(indent=' '*indent,var=var)
                            jtext=''
                            indent+=4
                            #this[][]preparation
                            tdirs=info[0].split('.')[1:]
                            if len(tdirs)>1:
                                cThis=this
                                for ind,var in enumerate(tdirs):
                                    if var not in cThis:
                                        cThis[var]={}
                                        cText+='{indent}this[\'{inputs}'+'\']\n'.format(inputs='\'][\''.join(tdirs[:ind]),indent=' '*indent)
                                    cThis=cThis[var]
                                
                            #final statemant
                            cText+='{indent}{this} = {json}\n'.format(
                                indent=' '*indent,
                                this= 'this[\'{inputs}'+'\']'.format(inputs='\'][\''.join(tdirs),
                                json= jtext
                            )
                cText+='return this\n'

                Convert[name]=cText
        except PermissionError:
            print('PermissionError:')
    

    saveAsJSON(path+'Convert_Try.json',Convert)



ConvertAssembly()