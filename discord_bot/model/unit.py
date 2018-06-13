from __future__ import absolute_import
from .model import Model


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
        embed.color = Unit.ELEMENT_COLOR.get(self.element, Unit.DEFAULT_ELEMENT_COLOR)

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
                if hasattr(self, 'tierlist') and name in self.tierlist:
                    value += " [{tier}]".format(tier=self.tierlist.get(name))

                fields.append({'name': name, 'value': value, 'inline': i != Unit.MAX_JOB_COUNT})

        for i in range(1, Unit.MAX_JOB_COUNT + 1):
            name = "jc {index}".format(index=i)
            value = getattr(self, name, None)
            if value and len(value)>1:                   
                if hasattr(self, 'tierlist') and name in self.tierlist:
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

        embed = self.to_embed(fields=fields)
        embed.set_footer(text='ᴶ - japan only', icon_url='')

        if 'total' in self.tierlist:
            embed.title += " [{tier}]".format(tier=self.tierlist.get('total'))

        return embed

    def to_lore_embed(self):
        fields = [
            {'name': 'Birthday', 'value': self.BIRTH, },
            'COUNTRY', 'HEIGHT', 'WEIGHT', 'ZODIAC', 'BLOOD', 'FAVORITE', 'HOBBY',
            {'name': 'illustrator', 'value': self.ILLUST, },
            'CV', 'PROFILE',
        ]
        return self.to_embed(fields=fields)

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
