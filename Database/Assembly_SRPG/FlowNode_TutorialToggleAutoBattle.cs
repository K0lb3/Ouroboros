namespace SRPG
{
    using System;

    [NodeType("Tutorial/ToggleAutoBattle", 0x7fe5), Pin(2, "Disable", 0, 1), Pin(10, "Output", 1, 2), Pin(1, "Enable", 0, 0)]
    public class FlowNode_TutorialToggleAutoBattle : FlowNode
    {
        private const int PIN_IN_ENABLE = 1;
        private const int PIN_IN_DISABLE = 2;
        private const int PIN_OUT_OUTPUT = 10;

        public FlowNode_TutorialToggleAutoBattle()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            InGameMenu menu;
            SceneBattle battle;
            int num;
            menu = null;
            battle = SceneBattle.Instance;
            if (battle == null)
            {
                goto Label_002F;
            }
            if (battle.BattleUI == null)
            {
                goto Label_002F;
            }
            menu = battle.BattleUI.GetComponent<InGameMenu>();
        Label_002F:
            if (menu == null)
            {
                goto Label_0067;
            }
            num = pinID;
            if (num == 1)
            {
                goto Label_004F;
            }
            if (num == 2)
            {
                goto Label_005B;
            }
            goto Label_0067;
        Label_004F:
            menu.ToggleAutoBattle(1);
            goto Label_0067;
        Label_005B:
            menu.ToggleAutoBattle(0);
        Label_0067:
            base.ActivateOutputLinks(10);
            return;
        }
    }
}

