namespace SRPG
{
    using System;
    using UnityEngine;

    [NodeType("Tutorial/Quest Tutorial", 0x7fe5), Pin(3, "Confirm", 1, 3), Pin(2, "No", 1, 2), Pin(1, "Yes", 1, 1), Pin(0, "In", 0, 0)]
    public class FlowNode_QuestTutorial : FlowNode
    {
        public string QuestID;
        public TriggerConditions Condition;
        public string ConfirmText;
        public string LocalFlag;
        public bool CheckLastPlayed;

        public FlowNode_QuestTutorial()
        {
            base..ctor();
            return;
        }

        private bool CheckCondition()
        {
            TriggerConditions conditions;
            switch ((this.Condition - 1))
            {
                case 0:
                    goto Label_008C;

                case 1:
                    goto Label_0047;

                case 2:
                    goto Label_006B;

                case 3:
                    goto Label_0028;

                case 4:
                    goto Label_0036;
            }
            goto Label_009A;
        Label_0028:
            return (GlobalVars.LastQuestResult.Get() == 1);
        Label_0036:
            return ((GlobalVars.LastQuestResult.Get() == 1) == 0);
        Label_0047:
            return ((GlobalVars.LastQuestResult.Get() != 1) ? 0 : ((GlobalVars.LastQuestState.Get() == 2) == 0));
        Label_006B:
            return ((GlobalVars.LastQuestResult.Get() == 1) ? 0 : (GlobalVars.LastQuestState.Get() == 0));
        Label_008C:
            return (GlobalVars.LastQuestState.Get() == 0);
        Label_009A:
            return 1;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            string str2;
            if (pinID != null)
            {
                goto Label_0101;
            }
            str = (this.CheckLastPlayed == null) ? GlobalVars.SelectedQuestID : GlobalVars.LastPlayedQuest.Get();
            if (string.IsNullOrEmpty(this.QuestID) != null)
            {
                goto Label_004F;
            }
            if ((str != this.QuestID) == null)
            {
                goto Label_004F;
            }
            this.OnNo(null);
            return;
        Label_004F:
            str2 = null;
            if (string.IsNullOrEmpty(this.LocalFlag) != null)
            {
                goto Label_006D;
            }
            str2 = FlowNode_Variable.Get(this.LocalFlag);
        Label_006D:
            if (this.CheckCondition() == null)
            {
                goto Label_00FA;
            }
            if (string.IsNullOrEmpty(str2) == null)
            {
                goto Label_00FA;
            }
            if (string.IsNullOrEmpty(this.LocalFlag) != null)
            {
                goto Label_00A3;
            }
            FlowNode_Variable.Set(this.LocalFlag, "1");
        Label_00A3:
            if (string.IsNullOrEmpty(this.ConfirmText) != null)
            {
                goto Label_00EE;
            }
            base.ActivateOutputLinks(3);
            UIUtility.ConfirmBox(LocalizedText.Get(this.ConfirmText), new UIUtility.DialogResultEvent(this.OnYes), new UIUtility.DialogResultEvent(this.OnNo), null, 1, -1, null, null);
            goto Label_00F5;
        Label_00EE:
            this.OnYes(null);
        Label_00F5:
            goto Label_0101;
        Label_00FA:
            this.OnNo(null);
        Label_0101:
            return;
        }

        private void OnNo(GameObject go)
        {
            base.ActivateOutputLinks(2);
            return;
        }

        private void OnYes(GameObject go)
        {
            base.ActivateOutputLinks(1);
            return;
        }

        public enum TriggerConditions
        {
            None,
            FirstTry,
            FirstWin,
            FirstLose,
            Win,
            Lose
        }
    }
}

