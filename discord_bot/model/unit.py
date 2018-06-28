from __future__ import absolute_import
from .model import Model
from .settings import FOOTER_URL 

class Unit(Model):
    MAX_JOB_COUNT = 3
    ELEMENT_COLOR = {
        'Fire': 0xFF0000,
        'Wind': 0x007F00,
        'Water': 0x2828FF,
        'Thunder': 0xFFCC00,
        'Light': 0xFFFFFF,
        'Dark': 0x140014,
    }
    DEFAULT_ELEMENT_COLOR = 0x7F7F7F

    @property
    def tierlist(self):
        return self._source.get('tierlist', {})

    def to_embed(self, title_key='name', thumbnail_key='icon', url_key='link', fields=[]):
        embed = super(Unit, self).to_embed(
            title_key=title_key, thumbnail_key=thumbnail_key, url_key=url_key, fields=fields
            )
        embed.set_author(name=self.inputs[0], url=self.link)

        embed.color = Unit.ELEMENT_COLOR.get(self.element, Unit.DEFAULT_ELEMENT_COLOR)
        embed.set_footer(text='ᴶ - japan only', icon_url=FOOTER_URL['UNIT'])

        return embed

    def to_unit_embed(self):
        fields = [
            'gender', 'rarity', 'country', 'collab',
            {'name': 'master ability', 'inline': False},
            {'name': 'leader skill', 'inline': False},
        ]

        for i in range(1, Unit.MAX_JOB_COUNT + 1):
            name = "job {index}".format(index=i)
            value = getattr(self, name, None)
            if value and len(value)>1:
                if name in self.tierlist:
                    value += " [{tier}]".format(tier=self.tierlist.get(name))

                fields.append({'name': name, 'value': value, 'inline': i != Unit.MAX_JOB_COUNT})

        for i in range(1, Unit.MAX_JOB_COUNT + 1):
            name = "jc {index}".format(index=i)
            value = getattr(self, name, None)
            if value and len(value)>1:                   
                if name in self.tierlist:
                    if '\n' in value:
                        job_name, job_desc = value.split('\n')
                        value = "{name} [{tier}]\n{desc}".format(
                            name=job_name, tier=self.tierlist.get(name), desc=job_desc
                        )
                    else:
                        value += " [{tier}]".format(tier=self.tierlist.get(name))

                fields.append({
                    'name': "job change {index}".format(index=i),
                    'value': value,
                    'inline': i != Unit.MAX_JOB_COUNT
                })

        farm = getattr(self, 'farm', [])
        if len(farm) > 0:
            fields.append({
                'name': 'shard hard quests',
                'value': '\n'.join(farm),
                'inline': False,
            })

        embed = self.to_embed(title_key='general info, overall rank:',fields=fields)

        if 'total' in self.tierlist:
            embed.title += " [{tier}]".format(tier=self.tierlist.get('total'))

        return embed

    def to_lore_embed(self):
        fields = [
            {'name': 'Birthday', 'value': self.birth, },
            'country', 'height', 'weight', 'zodiac', 'blood', 'favorite', 'hobby',
            {'name': 'illustrator', 'value': self.illust, },
            'cv', 'profile',
        ]
        return self.to_embed(title_key='lore ',fields=fields)

    def to_art_embeds(self):
        embeds = []

        for art in self.artworks:
            embed = self.to_embed()
            if 'name' in art:
                embed.title += ' - ' + art.get('name')
            if 'closeup' in art:
                embed.set_thumbnail(url=art.get('closeup'))
            if 'full' in art:
                embed.set_image(url=art.get('full'))

            embeds.append(embed)

        return embeds

    def to_unit_job_embed(self,job):
        _IGNORE_STATS = ['move', 'jump']

        #modifiers
        modifiers = [
            "{key}: {value}%".format(key=key, value=value)
            for key, value in job.modifiers.items()
            if value != 0
            ]

        #stats
        #combine stats
        rstats={}
        rstats.update(getattr(self,'stats'))
        for key, value in job.stats.items():
            if key.lower() not in _IGNORE_STATS:
                if key in rstats:
                    rstats[key] += int(value*84/99)
                else:
                    rstats[key] = int(value*84/99)

        #multiply with modifiers
        for key, value in job.modifiers.items():
            if key in rstats:
                rstats[key] = int(rstats[key]*(value+100)/100)
            elif key=='Initial Jewels':
                rstats[key] = int(rstats['Max Jewels']*(value)/100)
                

        #transform to output
        stats = [
            "{key}: {value}".format(key=key, value=value)
            for key, value in rstats.items()
        ]
        
        jm_values = [
            "{key}: {value}{mod}".format(key=value['stat'], value=value['value'],mod=value['mod'])
            for value in getattr(job,'job master buff')
        ]

        fields = [
            {'name': 'description', 'value': getattr(job, 'long description'),  'inline': False},
            {'name': 'formula',     'value': getattr(job, 'formula'),           'inline': False},
            {'name': 'weapon',      'value': getattr(job, 'weapon')},
            {'name': 'origin',      'value': getattr(job, 'origin')},
            {'name': 'move',        'value': job.stats['Move']},
            {'name': 'jump',        'value': job.stats['Jump']},
            {'name': 'JM bonus',    'value': ' , '.join(jm_values), 'inline':False},
            {'name': 'modifiers',   'value': '\n'.join(modifiers)},
            {'name': 'max stats (without JM bonus)', 'value': '\n'.join(stats)},
        ]

        embed = self.to_embed(title_key=job.name,fields=fields)

        return embed
