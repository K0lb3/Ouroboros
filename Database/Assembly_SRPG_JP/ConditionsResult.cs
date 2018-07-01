// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public abstract class ConditionsResult
  {
    protected bool mIsClear;
    protected int mTargetValue;
    protected int mCurrentValue;

    public bool isClear
    {
      get
      {
        return this.mIsClear;
      }
    }

    public int targetValue
    {
      get
      {
        return this.mTargetValue;
      }
    }

    public int currentValue
    {
      get
      {
        return this.mCurrentValue;
      }
    }

    public abstract string text { get; }

    public abstract string errorText { get; }

    public bool isConditionsUnitLv
    {
      get
      {
        return (object) this.GetType() == (object) typeof (ConditionsResult_UnitLv);
      }
    }

    public bool isConditionsAwake
    {
      get
      {
        return (object) this.GetType() == (object) typeof (ConditionsResult_AwakeLv);
      }
    }

    public bool isConditionsJobLv
    {
      get
      {
        return (object) this.GetType() == (object) typeof (ConditionsResult_JobLv);
      }
    }

    public bool isConditionsTobiraLv
    {
      get
      {
        return (object) this.GetType() == (object) typeof (ConditionsResult_TobiraLv);
      }
    }

    public bool isConditionsQuestClear
    {
      get
      {
        return (object) this.GetType() == (object) typeof (ConditionsResult_QuestClear);
      }
    }

    public bool isConditionsTobiraNoConditions
    {
      get
      {
        return (object) this.GetType() == (object) typeof (ConditionsResult_TobiraNoConditions);
      }
    }
  }
}
