namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;

    public class ConceptCardSkillDatailData
    {
        public ConceptCardEquipEffect effect;
        public SkillData skill_data;
        public LearningSkill learning_skill;
        public ConceptCardDetailAbility.ShowType type;

        public ConceptCardSkillDatailData(ConceptCardEquipEffect _effect, SkillData _data, ConceptCardDetailAbility.ShowType _type, LearningSkill _learning_skill)
        {
            base..ctor();
            this.skill_data = _data;
            this.effect = _effect;
            this.type = _type;
            this.learning_skill = _learning_skill;
            return;
        }
    }
}

