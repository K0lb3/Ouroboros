namespace SRPG
{
    using GR;
    using System;

    [Pin(0xcc, "Tutorial", 1, 0xcc), Pin(0xd9, "RankMatch", 1, 0xd9), Pin(0xd8, "Ordeal", 1, 0xd8), Pin(0xd7, "MultiGps", 1, 0xd7), Pin(0xd6, "Beginner", 1, 0xd6), Pin(0xd5, "MultiTower", 1, 0xd5), Pin(0xd4, "Extra", 1, 0xd4), Pin(0xd3, "Gps", 1, 0xd3), Pin(210, "VersusRank", 1, 210), Pin(0xd1, "VersusFree", 1, 0xd1), Pin(0xd0, "Tower", 1, 0xd0), Pin(0xcf, "Character", 1, 0xcf), Pin(0xce, "Event", 1, 0xce), Pin(0xcd, "Free", 1, 0xcd), Pin(0xcb, "Arena", 1, 0xcb), Pin(0xca, "Multi", 1, 0xca), Pin(0xc9, "Story", 1, 0xc9), Pin(200, "Input", 0, 200), Pin(0x66, "MultiPlay", 1, 0x66), Pin(0x65, "SinglePlay", 1, 0x65), Pin(100, "Input", 0, 100), NodeType("Battle/LastQuestType", 0x7fe5)]
    public class FlowNode_LastQuestType : FlowNode
    {
        public FlowNode_LastQuestType()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            QuestParam param;
            QuestTypes types;
            base.set_enabled(0);
            param = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            if (param != null)
            {
                goto Label_0032;
            }
            base.ActivateOutputLinks(0x65);
            base.ActivateOutputLinks(0xc9);
        Label_0032:
            if (pinID != 100)
            {
                goto Label_00C9;
            }
            switch (param.type)
            {
                case 0:
                    goto Label_0090;

                case 1:
                    goto Label_009E;

                case 2:
                    goto Label_0090;

                case 3:
                    goto Label_0090;

                case 4:
                    goto Label_0090;

                case 5:
                    goto Label_0090;

                case 6:
                    goto Label_0090;

                case 7:
                    goto Label_0090;

                case 8:
                    goto Label_009E;

                case 9:
                    goto Label_009E;

                case 10:
                    goto Label_0090;

                case 11:
                    goto Label_0090;

                case 12:
                    goto Label_009E;

                case 13:
                    goto Label_0090;

                case 14:
                    goto Label_009E;

                case 15:
                    goto Label_0090;

                case 0x10:
                    goto Label_009E;
            }
            goto Label_00AC;
        Label_0090:
            base.ActivateOutputLinks(0x65);
            goto Label_00C4;
        Label_009E:
            base.ActivateOutputLinks(0x66);
            goto Label_00C4;
        Label_00AC:
            DebugUtility.LogError("QuestTypesにTypeを追加したらここも見てください。");
            base.ActivateOutputLinks(0x65);
        Label_00C4:
            goto Label_00E7;
        Label_00C9:
            if (pinID != 200)
            {
                goto Label_00E7;
            }
            base.ActivateOutputLinks(0xc9 + param.type);
        Label_00E7:
            return;
        }
    }
}

