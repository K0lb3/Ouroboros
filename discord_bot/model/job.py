from __future__ import absolute_import
from .model import Model

class Job(Model):
    def to_embed(self, title_key='name', url_key='link', fields=[]):
        embed = super(Job, self).to_embed(
            title_key=title_key, url_key=url_key, fields=fields
        )
        #embed.color = Unit.ELEMENT_COLOR.get(self.element, Unit.DEFAULT_ELEMENT_COLOR)

        return embed

    def to_job_embed(self):
        
        modifiers=""
        for i in self.modifiers:
            if self.modifiers[i] != 0:
                modifiers+= i +': '+str(self.modifiers[i])+'% | '

        stats=""
        for s in self.stats:
            if s not in ['Move','Jump']:
                stats+= s +': ' + str(self.stats.get(s)) +'\n'

        fields = [
            {'name': 'description', 'value': getattr(self,'long description'), 'inline': False},
            'formula', 'origin',
            {'name': 'move',        'value': self.stats['Move']},
            {'name': 'jump',        'value': self.stats['Jump']},
            {'name': 'modifiers',   'value': modifiers, 'inline': False},
            {'name': 'stats',       'value': stats},
            {'name': 'units',       'value': '\n'.join(self.units)},
        ]

        embed = self.to_embed(fields=fields)

        #select thubnail
        if ':' in self.name or len(getattr(self,'short description'))>5:
            embed.set_thumbnail(url=self.token)
        else:
            embed.set_thumbnail(url=self.icon)

        #footer
        embed.set_footer(text='ᴶ - japan only', icon_url='')


        return embed
