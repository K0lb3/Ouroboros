namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(100, "==", 1, 100), Pin(0x65, "!=", 1, 0x65), NodeType("System/EventPageListTypeIs"), Pin(0, "判定", 0, 0)]
    public class FlowNode_EventPageListTypeIs : FlowNode
    {
        private const int INPUT_JUDGE = 0;
        private const int OUTPUT_EQUAL = 100;
        private const int OUTPUT_NOT_EQUAL = 0x65;
        [SerializeField, ShowInInfo(true)]
        private GlobalVars.EventQuestListType m_TargetEventQuestListType;

        public FlowNode_EventPageListTypeIs()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_002D;
            }
            if (GlobalVars.ReqEventPageListType != this.m_TargetEventQuestListType)
            {
                goto Label_0024;
            }
            base.ActivateOutputLinks(100);
            goto Label_002D;
        Label_0024:
            base.ActivateOutputLinks(0x65);
        Label_002D:
            return;
        }
    }
}

