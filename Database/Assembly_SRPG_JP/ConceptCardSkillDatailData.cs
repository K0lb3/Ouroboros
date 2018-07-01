// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardSkillDatailData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ConceptCardSkillDatailData
  {
    public ConceptCardEquipEffect effect;
    public SkillData skill_data;
    public LearningSkill learning_skill;
    public ConceptCardDetailAbility.ShowType type;

    public ConceptCardSkillDatailData(ConceptCardEquipEffect _effect, SkillData _data, ConceptCardDetailAbility.ShowType _type, LearningSkill _learning_skill = null)
    {
      this.skill_data = _data;
      this.effect = _effect;
      this.type = _type;
      this.learning_skill = _learning_skill;
    }
  }
}
