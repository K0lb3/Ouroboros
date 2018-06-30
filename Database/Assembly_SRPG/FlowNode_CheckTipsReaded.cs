namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(3, "False", 1, 3), NodeType("Tips/CheckTipsReaded", 0x7fe5), Pin(1, "既読か？", 0, 1), Pin(2, "True", 1, 2)]
    public class FlowNode_CheckTipsReaded : FlowNode
    {
        private const int PIN_ID_IN = 1;
        private const int PIN_ID_TRUE = 2;
        private const int PIN_ID_FALSE = 3;
        [SerializeField]
        private string Tips;

        public FlowNode_CheckTipsReaded()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0036;
            }
            if (MonoSingleton<GameManager>.Instance.Tips.Contains(this.Tips) == null)
            {
                goto Label_002E;
            }
            base.ActivateOutputLinks(2);
            goto Label_0036;
        Label_002E:
            base.ActivateOutputLinks(3);
        Label_0036:
            return;
        }
    }
}

