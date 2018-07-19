from os import environ

global FOOTER_URL
FOOTER_URL={
    'UNIT': "https://drive.google.com/uc?export=download&id=1PckfnsMfNkguRZ9-2XVM6MT_ZOrG2-Lr",
    'JOB':  "https://drive.google.com/uc?export=download&id=1OZ8QjPJXe-SKxuFsb3iUp6mlCzri6gAu",
    'GEAR': "https://drive.google.com/uc?export=download&id=1npHf6ZnYvlgQV8ursCEWpLdFPd5-JwZG",
    'FARM': "https://drive.google.com/uc?export=download&id=1P13M9rZa7-Zmyggn7C3LfUPpMTnomkYu",
    }

global prefix
prefix='o?'

global BOT_TOKEN
BOT_TOKEN = environ.get('DISCORD_BOT_TOKEN')

global PRESENCES
PRESENCES=[
        'WIP Job: o?job',
        'Collabs: o?collabs',
        'Unit: o?unit',
        'Lore: o?lore',
        'Art:  o?art',
        'Gear: o?gear',
        'Farm: o?farm',
        'Ranking: o?arena',
        'Arena Enemy: o?rank x'
        'Tierlist: o?tierlist',
        'Info: o?info',
        'Help: o?help'
        ]

global PAGES
PAGES={
    'gear':{
        '🗃':  'main',
    },
    'job':{
        '🗃':  'main',
        '🇲':   'main ability',     
        '🇸':   'sub ability',
        '🇷':   'reactives',
        '🇵':   'passives',
    },
    'unit':{
        '🗃':   'main',
        '📰':  'lore',
        '1⃣':   'job 1',     
        '2⃣':   'job 2',
        '3⃣':   'job 3',
        '4⃣':   'jc 1',
        '5⃣':   'jc 2',
        '6⃣':   'jc 3'
    },
    'drop':{
        's':  'story',
        'h':  'hard',
        'e':  'event'
    },

}