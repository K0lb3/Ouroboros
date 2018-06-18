from __future__ import absolute_import
from abc import ABC
from discord.message import Embed


class Model(ABC):
    def __init__(self, source):
        self._source = source

    def __getattr__(self, item):
        if item in self._source:
            return self._source[item]
        else:
            raise AttributeError(item)

    def to_embed(self, title_key='name', thumbnail_key='icon', url_key='link', fields=[]):
        embed = Embed(
            title=getattr(self, title_key, None),
            url=getattr(self, url_key, None)
        )
        embed.set_thumbnail(url=getattr(self, thumbnail_key, None))

        for field in fields:
            if isinstance(field, str):
                args = {
                    'name': field,
                    'inline': True
                }
            elif isinstance(field, dict):
                args = field
            else:
                raise ValueError("Invalid field type: {type}".format(type=field.__class__.__name__))

            # Deduce value if needed
            if 'value' not in args:
                args['value'] = getattr(self, args['name'])

            # Value to string
            if not isinstance(args['value'], str):
                args['value']=str(args['value'])

            # Check if value is useable
            if len(args['value'].lstrip(' '))==0:
                continue

            # Inline defaults to True
            if 'inline' not in args:
                args['inline'] = True

            # Empty fields are not supported
            if not args['value']:
                continue

            # Clean up args a bit for aesthetics
            args['name'] = args['name'].title()

            embed.add_field(**args)

        return embed
