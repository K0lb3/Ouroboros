import os

mypath = os.path.dirname(os.path.realpath(__file__))
files = os.listdir(mypath)
folder= mypath[mypath.rindex('\\')+1:]
str = ''

for f in files:
    if '.py' in f and '_' not in f:
        str+= 'from {folder}.{name} import {name}\n'.format(folder=folder,name=f[:-3])

with open(mypath+'\\__init__.py', "wt") as f:
    f.write(str)