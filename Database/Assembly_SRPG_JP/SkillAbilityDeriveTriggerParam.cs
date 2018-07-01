// Decompiled with JetBrains decompiler
// Type: SRPG.SkillAbilityDeriveTriggerParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class SkillAbilityDeriveTriggerParam
  {
    public ESkillAbilityDeriveConds m_TriggerType;
    public string m_TriggerIname;

    public SkillAbilityDeriveTriggerParam(ESkillAbilityDeriveConds triggerType, string triggerIname)
    {
      this.m_TriggerIname = triggerIname;
      this.m_TriggerType = triggerType;
    }

    public static List<SkillAbilityDeriveTriggerParam[]> CreateCombination(SkillAbilityDeriveTriggerParam[] triggerParams)
    {
      List<SkillAbilityDeriveTriggerParam[]> deriveTriggerParamArrayList = new List<SkillAbilityDeriveTriggerParam[]>();
      Stack<SkillAbilityDeriveTriggerParam> deriveTriggerParamStack = new Stack<SkillAbilityDeriveTriggerParam>();
      for (int index1 = 0; index1 < triggerParams.Length; ++index1)
      {
        for (int index2 = index1; index2 < triggerParams.Length; ++index2)
        {
          deriveTriggerParamStack.Push(triggerParams[index2]);
          deriveTriggerParamArrayList.Add(deriveTriggerParamStack.ToArray());
        }
        deriveTriggerParamStack.Clear();
      }
      return deriveTriggerParamArrayList;
    }
  }
}
