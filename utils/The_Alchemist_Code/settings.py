from os import environ

global PRESENCES
PRESENCES=[
        'Job: o.job',
        'Unit: o.unit',
        'Lore: o.lore',
        'Art:  o.art',
        'Gear: o.gear',
        'Nensou: o.nensou',
        'Item: o.item',
        #'Recipe: o?recipe',
        'Farm: o.farm',
        'Ranking: o.arena',
        'Arena Enemy: o.rank x'
        #'Tierlist: o?tierlist',
        #'Info: o.info',
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
        '🇺':   'units',
        '🇲':   'main ability',     
        '🇸':   'sub ability',
        '🇷':   'reaction ability',
        '🇵':   'passive ability',
    },
    'unit':{
        '🗃':   'main',
        '📰':  'lore',
        '🇰':    'kaigan',
        '🇹':   'tierlist',
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
        '🇲':   'map',
        '🇩':   'drop',
        #'🇸':   'story',
        '🇦':   'allies',
        '🇪':   'enemies',
        '🇹':   'traps',
        '🇯':   'jewels',
        '🎁':   'treasures',

    },
    'item':{
        '🗃':   'main',
        '🇸':  'Story',
        '🇭':  'Hard',
        '🇪':  'Event'
    },

}
