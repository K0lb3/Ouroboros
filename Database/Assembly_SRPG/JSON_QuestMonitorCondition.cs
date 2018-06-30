namespace SRPG
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class JSON_QuestMonitorCondition
    {
        public UnitMonitorCondition[] actions;
        public UnitMonitorCondition[] goals;
        public UnitMonitorCondition[] withdraw;

        public JSON_QuestMonitorCondition()
        {
            base..ctor();
            return;
        }

        public void CopyTo(QuestMonitorCondition dst)
        {
            dst.Clear();
            if (this.actions == null)
            {
                goto Label_0030;
            }
            if (((int) this.actions.Length) <= 0)
            {
                goto Label_0030;
            }
            dst.actions = new List<UnitMonitorCondition>(this.actions);
        Label_0030:
            if (this.goals == null)
            {
                goto Label_005A;
            }
            if (((int) this.goals.Length) <= 0)
            {
                goto Label_005A;
            }
            dst.goals = new List<UnitMonitorCondition>(this.goals);
        Label_005A:
            if (this.withdraw == null)
            {
                goto Label_0084;
            }
            if (((int) this.withdraw.Length) <= 0)
            {
                goto Label_0084;
            }
            dst.withdraw = new List<UnitMonitorCondition>(this.withdraw);
        Label_0084:
            return;
        }
    }
}

