namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(100, "Out", 1, 100), NodeType("System/Quest/Selector", 0x7fe5), Pin(0, "In", 0, 0)]
    public class FlowNode_QuestSelector : FlowNode
    {
        [SerializeField]
        private SRPG.GlobalVars.EventQuestListType EventQuestListType;

        public FlowNode_QuestSelector()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0011;
            }
            GlobalVars.ReqEventPageListType = this.EventQuestListType;
        Label_0011:
            base.ActivateOutputLinks(100);
            return;
        }
    }
}

