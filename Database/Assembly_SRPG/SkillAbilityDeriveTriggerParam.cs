namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class SkillAbilityDeriveTriggerParam
    {
        public ESkillAbilityDeriveConds m_TriggerType;
        public string m_TriggerIname;

        public SkillAbilityDeriveTriggerParam(ESkillAbilityDeriveConds triggerType, string triggerIname)
        {
            base..ctor();
            this.m_TriggerIname = triggerIname;
            this.m_TriggerType = triggerType;
            return;
        }

        public static List<SkillAbilityDeriveTriggerParam[]> CreateCombination(SkillAbilityDeriveTriggerParam[] triggerParams)
        {
            List<SkillAbilityDeriveTriggerParam[]> list;
            Stack<SkillAbilityDeriveTriggerParam> stack;
            int num;
            int num2;
            list = new List<SkillAbilityDeriveTriggerParam[]>();
            stack = new Stack<SkillAbilityDeriveTriggerParam>();
            num = 0;
            goto Label_0046;
        Label_0013:
            num2 = num;
            goto Label_0033;
        Label_001A:
            stack.Push(triggerParams[num2]);
            list.Add(stack.ToArray());
            num2 += 1;
        Label_0033:
            if (num2 < ((int) triggerParams.Length))
            {
                goto Label_001A;
            }
            stack.Clear();
            num += 1;
        Label_0046:
            if (num < ((int) triggerParams.Length))
            {
                goto Label_0013;
            }
            return list;
        }
    }
}

