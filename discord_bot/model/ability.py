from __future__ import absolute_import
from .model import Model


class Ability(Model):
    _LINK_FORMAT = "http://www.alchemistcodedb.com/skill/{iname}"

    @property
    def name(self):
        return self._source.get('NAME')

    @property
    def expr(self):
        return self._source.get('EXPR')

    @property
    def link(self):
        return Ability._LINK_FORMAT.format(
            iname=self.iname.lower().replace('_','-')
        )

    def __str__(self):
        if self.restriction != "":
            return '\n'.join([
                "[{name}]({link})".format(name=self.name, link=self.link),
                "Restriction: {rest}".format(rest=self.restriction),
                self.expr,
            ])
        else:
            return '\n'.join([
                "[{name}]({link})".format(name=self.name, link=self.link),
                self.expr,
            ])
            
