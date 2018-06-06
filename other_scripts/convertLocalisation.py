import sys
import os.path
import json
import http.client
import re
pattern=r'^SRPG_(.+)Param_(.+)\_([^\t]+)\t(.*)$'#r'(?<=Param_)(.*)(?=\_).(.*)\t(.*)\n'
regex= re.compile(pattern, re.MULTILINE)

def main():    
    file_n='LocalizedMasterParam'
    path = os.path.dirname(os.path.realpath(__file__))+'\\' + file_n
    if not os.path.exists(path):
        print("", path, " was not found.")
        input("\n \n Press enter to exit :(")
        sys.exit(0)

    with open(path, "rt", encoding='utf8') as f:
        f.read(1)
        file = f.read()
        
    
    localisation={}
    for match in regex.finditer(file):
        if match.group(2) not in localisation:
            localisation[match.group(2)]={}
        localisation[match.group(2)][match.group(3)]=match.group(4)
 
    with open(path+'.json', "wt", encoding='utf8') as f:
        f.write(json.dumps(localisation, indent=4))
        
        
        
        
        
        
#code
main()