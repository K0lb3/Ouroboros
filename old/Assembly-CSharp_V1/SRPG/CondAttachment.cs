// Decompiled with JetBrains decompiler
// Type: SRPG.CondAttachment
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class CondAttachment
  {
    public Unit user;
    public Unit CheckTarget;
    private OInt mCheckTiming;
    private OInt mUseCondition;
    public OInt turn;
    public OBool IsPassive;
    public SkillData skill;
    private OInt mCondType;
    private OInt mCondition;

    public EffectCheckTimings CheckTiming
    {
      get
      {
        return (EffectCheckTimings) (int) this.mCheckTiming;
      }
      set
      {
        this.mCheckTiming = (OInt) ((int) value);
      }
    }

    public ESkillCondition UseCondition
    {
      get
      {
        return (ESkillCondition) (int) this.mUseCondition;
      }
      set
      {
        this.mUseCondition = (OInt) ((int) value);
      }
    }

    public ConditionEffectTypes CondType
    {
      get
      {
        return (ConditionEffectTypes) (int) this.mCondType;
      }
      set
      {
        this.mCondType = (OInt) ((int) value);
      }
    }

    public EUnitCondition Condition
    {
      get
      {
        return (EUnitCondition) (int) this.mCondition;
      }
      set
      {
        this.mCondition = (OInt) ((int) value);
      }
    }

    public bool IsCurse { get; set; }

    public void CopyTo(CondAttachment dsc)
    {
      dsc.user = this.user;
      dsc.skill = this.skill;
      dsc.turn = this.turn;
      dsc.IsPassive = this.IsPassive;
      dsc.CondType = this.CondType;
      dsc.Condition = this.Condition;
      dsc.CheckTarget = this.CheckTarget;
      dsc.CheckTiming = this.CheckTiming;
      dsc.UseCondition = this.UseCondition;
    }

    public bool IsFailCondition()
    {
      switch (this.CondType)
      {
        case ConditionEffectTypes.FailCondition:
        case ConditionEffectTypes.ForcedFailCondition:
        case ConditionEffectTypes.RandomFailCondition:
          return true;
        default:
          return false;
      }
    }

    public bool ContainsCondition(EUnitCondition condition)
    {
      return ((EUnitCondition) (int) this.mCondition & condition) != (EUnitCondition) 0;
    }
  }
}
