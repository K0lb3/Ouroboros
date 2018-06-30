namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Check", 0, 0), Pin(3, "召喚完了済み", 1, 3), Pin(2, "チュートリアル召喚後(未確定)", 1, 2), Pin(1, "チュートリアル召喚前", 1, 1), NodeType("Tutorial/CheckPhase(進行状況確認)", 0x7fe5)]
    public class FlowNode_CheckTutorialPhase : FlowNode
    {
        private const int PIN_IN_CHECK = 0;
        private const int PIN_OT_BEFORE_GACHA = 1;
        private const int PIN_OT_AFTER_GACHA = 2;
        private const int PIN_OT_PREV_VERSION_GACHA = 3;

        public FlowNode_CheckTutorialPhase()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            PlayerData data;
            num = 1;
            if (MonoSingleton<GameManager>.Instance.Player.Units.Count != 4)
            {
                goto Label_0025;
            }
            num = 3;
            goto Label_0040;
        Label_0025:
            if ((FlowNode_Variable.Get("REDRAW_GACHA_PENDING") == "1") == null)
            {
                goto Label_0040;
            }
            num = 2;
        Label_0040:
            base.ActivateOutputLinks(num);
            return;
        }
    }
}

