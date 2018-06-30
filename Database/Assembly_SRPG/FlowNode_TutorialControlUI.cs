namespace SRPG
{
    using System;
    using UnityEngine;

    [NodeType("Tutorial/ControlUI", 0x7fe5), Pin(10, "Output", 1, 2), Pin(2, "Disable", 0, 1), Pin(1, "Enable", 0, 0)]
    public class FlowNode_TutorialControlUI : FlowNode
    {
        private const int PIN_IN_ENABLE = 1;
        private const int PIN_IN_DISABLE = 2;
        private const int PIN_OUT_OUTPUT = 10;
        [SerializeField, BitMask]
        private SceneBattle.eMaskBattleUI ControlType;

        public FlowNode_TutorialControlUI()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            SceneBattle battle;
            int num;
            battle = SceneBattle.Instance;
            if (battle == null)
            {
                goto Label_004C;
            }
            num = pinID;
            if (num == 1)
            {
                goto Label_0026;
            }
            if (num == 2)
            {
                goto Label_0039;
            }
            goto Label_004C;
        Label_0026:
            battle.EnableControlBattleUI(this.ControlType, 1);
            goto Label_004C;
        Label_0039:
            battle.EnableControlBattleUI(this.ControlType, 0);
        Label_004C:
            base.ActivateOutputLinks(10);
            return;
        }
    }
}

