from ._main import DIRS,ConvertFields,Embed

def AI(iname,page):
	#get ai dir
	ai=DIRS['Ai'][iname]

	#create basic embed
	embed= Embed(
		title=page, #page name
		)
	embed.set_author(name=ai['iname'])

	if page=='main':
		embed.ConvertFields(main(ai))
	return embed

def main(ai):
	fields = []

	fields.append({'name':'Settings','value':'\n'.join(ai['flags']),'inline':True})
	#fields.append({'name':'Jewel Border','value':'','inline':})
	borders={
		'gems_border'	:	'Jewel Border:\t%s%%',	#if (gems >= (int) ai.gems_border) use skills
		'heal_border'	:	'Heal Border:\t%s%%',	# hp2 * (int) mUnit.AI.heal_border >= hp1 * 100 -> heals if maxHP*border < currentHP
		'buff_border'	:	'Buff Border:\t%s',
		'cond_border'	:	'Condition Border:\t%s',
		'safe_border'	:	'Safety Border:\t%s tiles',	#int num1 = Math.Max(this.mSafeMap.get(start.x, start.y) + (self.AI == null ? 0 : (int) self.AI.safe_border * currentEnemyNum), 0); - GetSafePosition
		'escape_border'	:	'Escape Border:\t%s tiles',
		'gosa_border'	:	'GoSa Border:\t%s',	#Math.Abs(dsc.ext_damage - src.ext_damage) > (int) self.AI.gosa_border)
		'DisableSupportActionHpBorder'		:	'Disable support skills if own HP are below %s%%',
		'DisableSupportActionMemberBorder'	:	'Disable support skills if less than %s allies are alive'
	}

	fields.append({'name':'Border Conditions','value':'\n'.join([
		string%ai[key]
		for key,string in borders.items()
		if key in ai
		]
	),'inline':True})

	if 'SkillCategoryPriorities' in ai:
		fields.append({'name':'Skill Category Priorities','value':'\n'.join(ai['SkillCategoryPriorities']),'inline':True})
	if 'BuffPriorities' in ai:
		fields.append({'name':'Buff Priorities','value':'\n'.join(ai['BuffPriorities']),'inline':True})
	if 'ConditionPriorities' in ai:
		fields.append({'name':'Condition Priorities','value':'\n'.join(ai['ConditionPriorities']),'inline':True})

	return fields

        # {
        #   bool flag2 = false;
        #   bool flag3 = true;
        #   if ((int) self.AI.DisableSupportActionHpBorder != 0)
        #   {
        #     int num = (int) self.MaximumStatus.param.hp == 0 ? 100 : 100 * (int) self.CurrentStatus.param.hp / (int) self.MaximumStatus.param.hp;
        #     flag2 = true;
        #     flag3 &= (int) self.AI.DisableSupportActionHpBorder >= num;
        #   }
        #   if ((int) self.AI.DisableSupportActionMemberBorder != 0)
        #   {
        #     int aliveUnitCount = this.GetAliveUnitCount(self);
        #     flag2 = true;
        #     flag3 &= (int) self.AI.DisableSupportActionMemberBorder >= aliveUnitCount;
        #   }
        #   if (flag2 && flag3)
        #     return false;