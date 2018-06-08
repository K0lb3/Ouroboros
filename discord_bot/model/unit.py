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

    def to_unit_embed(self):
        fields = [
            'gender', 'rarity', 'country', 'collab',
            {'name': 'master ability', 'inline': False},
            {'name': 'leader skill', 'inline': False},
        ]

        for i in range(1, Unit.MAX_JOB_COUNT + 1):
            name = "job {index}".format(index=i)
            if hasattr(self, name):
                fields.append({'name': name, 'inline': i != Unit.MAX_JOB_COUNT})

        for i in range(1, Unit.MAX_JOB_COUNT + 1):
            name = "jc {index}".format(index=i)
            if hasattr(self, name):
                fields.append({
                    'name': "job change {index}".format(index=i),
                    'value': getattr(self, name),
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
        embed.color = Unit.ELEMENT_COLOR.get(self.element, Unit.DEFAULT_ELEMENT_COLOR)

        return embed
