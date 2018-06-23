import sys
import os.path
import json

def main():
    if len(sys.argv) != 2:
        print("Wrong number of arguments. Run this script like this: 'python showAccountData.py path/to/*.json'")
        input()
        sys.exit(0)

    if not os.path.exists(sys.argv[1]):
        print("File", sys.argv[1], "was not found.")
        sys.exit(0)

    with open(sys.argv[1], "rt",encoding='utf8') as f:
        file = json.loads(f.read())

    with open(sys.argv[1], "wb") as f:
        f.write(json.dumps(file, indent=4, ensure_ascii=False).encode('utf8'))





main()