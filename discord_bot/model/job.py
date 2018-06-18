from __future__ import absolute_import
from .model import Model

class Job(Model):
    _IGNORE_STATS = ['move', 'jump']

    def to_job_embed(self):
        modifiers = [
            "{key}: {value}%".format(key=key, value=value)
            for key, value in self.modifiers.items()
            if value != 0
        ]

        stats = [
            "{key}: {value}".format(key=key, value=value)
            for key, value in self.stats.items()
            if key.lower() not in Job._IGNORE_STATS
        ]

        fields = [
            {'name': 'description', 'value': getattr(self, 'long description'), 'inline': False},
            'formula', 'weapon','origin',
            {'name': 'move', 'value': self.stats['Move']},
            {'name': 'jump', 'value': self.stats['Jump']},
            {'name': 'modifiers', 'value': ' | '.join(modifiers), 'inline': False},
            {'name': 'stats', 'value': '\n'.join(stats)},
            {'name': 'units', 'value': '\n'.join(self.units)},
        ]

        embed = self.to_embed(fields=fields)

        # select thubnail
        if ':' in self.name or len(getattr(self, 'short description')) > 5:
            embed.set_thumbnail(url=self.token)
        else:
            embed.set_thumbnail(url=self.icon)

        # footer
        embed.set_footer(text='ᴶ - japan only', icon_url='')

        return embed
