from __future__ import absolute_import
from .model import Model
from .ability import Ability


class Gear(Model):
    TYPE_COLOR = {
        1: 0xFF0000,
        2: 0x2828FF,
        3: 0x007F00,
    }
    DEFAULT_TYPE_COLOR = 0x7F7F7F

    @property
    def stats(self):
        return self._source.get('stats', {})

    @property
    def atk_buff(self):
        return self._source.get('atk_buff', {})

    @property
    def ability(self):
        return self._source.get('ability', [])

    def to_embed(self, title_key='name', thumbnail_key='icon', url_key='link', fields=[]):
        embed = super(Gear, self).to_embed(
            title_key=title_key, thumbnail_key=thumbnail_key, url_key=url_key, fields=fields
        )

        embed.title += ' ' + self.rarity
        embed.color = Gear.TYPE_COLOR.get(self.type, Gear.DEFAULT_TYPE_COLOR)

        return embed

    def to_gear_embed(self):
        fields = []

        if self.stats:
            stats = [
                "{key}★: {value}".format(key=key, value=value)
                for key, value in self.stats.items()
            ]
            fields.append({'name': 'Max Stats', 'value': '\n'.join(stats), 'inline': False})

        if self.atk_buff:
            buffs = [
                "{key}★: {value}".format(key=key, value=value)
                for key, value in self.atk_buff.items()
            ]
            fields.append({'name': 'Max Attack (De)Buff', 'value': '\n'.join(buffs), 'inline': False})

        abilities = [str(Ability(source=a)) for a in self.ability]
        fields.append({'name': 'Ability', 'value': "\n".join(abilities), 'inline': False})

        fields.append({'name': 'Flavor Text', 'value': self.flavor, 'inline': False})

        return self.to_embed(fields=fields)
