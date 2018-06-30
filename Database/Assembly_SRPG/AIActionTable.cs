namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class AIActionTable
    {
        public List<AIAction> actions;
        public int looped;

        public AIActionTable()
        {
            this.actions = new List<AIAction>();
            base..ctor();
            return;
        }

        public void Clear()
        {
            this.actions.Clear();
            this.looped = 0;
            return;
        }

        public void CopyTo(AIActionTable result)
        {
            int num;
            AIAction action;
            if (result != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            result.actions.Clear();
            num = 0;
            goto Label_0124;
        Label_0019:
            action = new AIAction();
            action.skill = this.actions[num].skill;
            action.type = this.actions[num].type;
            action.turn = this.actions[num].turn;
            action.notBlock = this.actions[num].notBlock;
            action.noExecAct = this.actions[num].noExecAct;
            action.nextActIdx = this.actions[num].nextActIdx;
            action.nextTurnAct = this.actions[num].nextTurnAct;
            action.turnActIdx = this.actions[num].turnActIdx;
            if (this.actions[num].cond == null)
            {
                goto Label_0114;
            }
            action.cond = new SkillLockCondition();
            this.actions[num].cond.CopyTo(action.cond);
        Label_0114:
            result.actions.Add(action);
            num += 1;
        Label_0124:
            if (num < this.actions.Count)
            {
                goto Label_0019;
            }
            result.looped = this.looped;
            return;
        }
    }
}

