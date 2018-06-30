namespace SRPG
{
    using System;

    [NodeType("System/クエスト終了", 0x7fe5), Pin(0x65, "ForceEnded", 1, 0x65), Pin(1, "ForceEnd", 0, 1), Pin(0, "End", 0, 0)]
    public class FlowNode_EndQuest : FlowNode
    {
        public bool Restart;

        public FlowNode_EndQuest()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            QuestParam param;
            BattleCore.Record record;
            if ((pinID != null) || ((SceneBattle.Instance != null) == null))
            {
                goto Label_0081;
            }
            if (Network.Mode != 1)
            {
                goto Label_0060;
            }
            param = SceneBattle.Instance.Battle.GetQuest();
            record = SceneBattle.Instance.Battle.GetQuestRecord();
            if ((param == null) || (record == null))
            {
                goto Label_0060;
            }
            param.clear_missions |= record.bonusFlags;
        Label_0060:
            SceneBattle.Instance.ExitRequest = (this.Restart == null) ? 1 : 2;
            goto Label_00BA;
        Label_0081:
            if (pinID != 1)
            {
                goto Label_00BA;
            }
            if ((SceneBattle.Instance == null) == null)
            {
                goto Label_00A9;
            }
            base.set_enabled(0);
            base.ActivateOutputLinks(0x65);
            return;
        Label_00A9:
            base.set_enabled(1);
            SceneBattle.Instance.ForceEndQuest();
        Label_00BA:
            return;
        }

        private void Update()
        {
            if ((SceneBattle.Instance == null) == null)
            {
                goto Label_0021;
            }
            base.set_enabled(0);
            base.ActivateOutputLinks(0x65);
            return;
        Label_0021:
            return;
        }
    }
}

