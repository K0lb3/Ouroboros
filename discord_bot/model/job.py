from __future__ import absolute_import
from .model import Model
from .settings import FOOTER_URL 

class Job(Model):
    _IGNORE_STATS = ['move', 'jump']

    def to_embed(self, title_key='name', thumbnail_key='icon', url_key='link', fields=[]):
        embed = super(Job, self).to_embed(
            title_key=title_key, thumbnail_key=thumbnail_key, url_key=url_key, fields=fields
            )
        embed.set_author(name=self.inputs[0], url=self.link)

        #embed.color = Unit.ELEMENT_COLOR.get(self.element, Unit.DEFAULT_ELEMENT_COLOR)

        # select thubnail
        if ':' in self.name or len(getattr(self, 'short description')) > 5:
            embed.set_thumbnail(url=self.token)
        else:
            embed.set_thumbnail(url=self.icon)

        # footer
        embed.set_footer(text='ᴶ - japan only', icon_url=FOOTER_URL['JOB'])

        return embed

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

        jm_values = [
            "{key}: {value}{mod}".format(key=value['stat'], value=value['value'],mod=value['mod'])
            for value in getattr(self,'job master buff')
        ]

        fields = [
            {'name': 'description', 'value': getattr(self, 'long description'), 'inline': False},
            {'name': 'formula',     'value': getattr(self, 'formula'), 'inline': False},
            'weapon','origin',
            {'name': 'move',        'value': self.stats['Move']},
            {'name': 'jump',        'value': self.stats['Jump']},
            {'name': 'modifiers',   'value': ' , '.join(modifiers), 'inline': False},
            {'name': 'JM bonus',    'value': ' , '.join(jm_values), 'inline':False},
            {'name': 'stats (without JM bonus)',       'value': '\n'.join(stats)},
            {'name': 'units',       'value': '\n'.join(self.units)},
        ]
        embed = self.to_embed(title_key='general info',fields=fields)
        return embed


    def to_skill_embed(self, tree_):
        tree=getattr(self, tree_)

        fields = [
            {'name':'description','value': tree['expr'], 'inline':False},
            ]

        for skill in tree['skills']:
            fields.append({
                'name': skill['name'],
                'value': skill['expr'],
                'inline': False
            })

        embed = self.to_embed(title_key=tree_+' ',fields=fields)

        return embed
