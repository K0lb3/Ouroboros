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
        'Job: o?job',
        'Unit: o?unit',
        'Lore: o?lore',
        'Art:  o?art',
        'Gear: o?gear',
        'Nensou: o?nensou',
        'Item: o?item',
        #'Recipe: o?recipe',
        'Farm: o?farm',
        'Ranking: o?arena',
        'Arena Enemy: o?rank x'
        #'Tierlist: o?tierlist',
        'Info: o?info',
        #'Help: o?help'
        ]

global PAGES
PAGES={
    'gear':{
        '🗃':  'main',
    },
    'conceptcard':{
        '🗃':  'main',
    },
    'job':{
        '🗃':  'main',
        '🇲':   'main ability',     
        '🇸':   'sub ability',
        '🇷':   'reaction ability',
        '🇵':   'passive ability',
    },
    'unit':{
        '🗃':   'main',
        '📰':  'lore',
        '🇰':    'kaigan',
        #'🇳':    'nensou',
        '1⃣':   'job1',     
        '2⃣':   'job2',
        '3⃣':   'job3',
        '4⃣':   'job4',
        '5⃣':   'job5',
        '6⃣':   'job6'
    },
    'quest':{
        '🗃':   'main',
        '📰':  'drop',
    },
    'item':{
        '🗃':   'main',
        '🇸':  'Story',
        #'🇭':  'hard',
        '🇪':  'Event'
    },

}
