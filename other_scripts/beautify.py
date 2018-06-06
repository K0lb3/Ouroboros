import sys
import os.path
import json
import uuid
import http.client
import time

def main():
    if len(sys.argv) != 2:
        print("Wrong number of arguments. Run this script like this: 'python showAccountData.py path/to/gu3c.dat'")
        input()
        sys.exit(0)

    if not os.path.exists(sys.argv[1]):
        print("File", sys.argv[1], "was not found.")
        sys.exit(0)

    with open(sys.argv[1], "rt",encoding='utf8') as f:
        file = json.loads(f.read())
        
    with open(sys.argv[1], "wt",encoding='utf8') as f:
        f.write(json.dumps(file, indent=4))





main()