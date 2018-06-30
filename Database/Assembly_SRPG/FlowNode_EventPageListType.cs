namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(0, "設定", 0, 0), Pin(100, "完了", 1, 100), NodeType("System/EventPageListType")]
    public class FlowNode_EventPageListType : FlowNode
    {
        [SerializeField]
        private GlobalVars.EventQuestListType m_TargetEventQuestListType;

        public FlowNode_EventPageListType()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            base.OnActivate(pinID);
            if (pinID != null)
            {
                goto Label_0021;
            }
            GlobalVars.ReqEventPageListType = this.m_TargetEventQuestListType;
            base.ActivateOutputLinks(100);
        Label_0021:
            return;
        }
    }
}

