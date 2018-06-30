namespace SRPG
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class QuestMonitorCondition
    {
        public List<UnitMonitorCondition> actions;
        public List<UnitMonitorCondition> goals;
        public List<UnitMonitorCondition> withdraw;

        public QuestMonitorCondition()
        {
            this.actions = new List<UnitMonitorCondition>();
            this.goals = new List<UnitMonitorCondition>();
            this.withdraw = new List<UnitMonitorCondition>();
            base..ctor();
            return;
        }

        public void Clear()
        {
            this.actions.Clear();
            this.goals.Clear();
            this.withdraw.Clear();
            return;
        }

        public void CopyTo(QuestMonitorCondition dst)
        {
            int num;
            int num2;
            int num3;
            this.Clear();
            num = 0;
            goto Label_0028;
        Label_000D:
            dst.actions.Add(this.actions[num]);
            num += 1;
        Label_0028:
            if (num < this.actions.Count)
            {
                goto Label_000D;
            }
            num2 = 0;
            goto Label_005B;
        Label_0040:
            dst.goals.Add(this.goals[num2]);
            num2 += 1;
        Label_005B:
            if (num2 < this.goals.Count)
            {
                goto Label_0040;
            }
            num3 = 0;
            goto Label_008E;
        Label_0073:
            dst.goals.Add(this.withdraw[num3]);
            num3 += 1;
        Label_008E:
            if (num3 < this.goals.Count)
            {
                goto Label_0073;
            }
            return;
        }
    }
}

