namespace SRPG
{
    using GR;
    using System;

    [NodeType("Tower/GetTowerMissionPrefs", 0x7fe5), Pin(0, "ミッションID取得", 0, 0), Pin(1, "取得成功", 1, 1), Pin(2, "取得出来ない", 1, 2)]
    public class FlowNode_GetTowerMissionPrefs : FlowNode
    {
        private const int INPUT_GET_MISSION_ID = 0;
        private const int OUTPUT_SUCCESS_GET_MISSION_ID = 1;
        private const int OUTPUT_NONE_GET_MISSION_ID = 2;

        public FlowNode_GetTowerMissionPrefs()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            string str2;
            TowerFloorParam param;
            QuestParam param2;
            int num;
            str = null;
            str2 = null;
            param = MonoSingleton<GameManager>.Instance.FindTowerFloor(GlobalVars.SelectedQuestID);
            if (param == null)
            {
                goto Label_002E;
            }
            param2 = param.GetQuestParam();
            if (param2 == null)
            {
                goto Label_002E;
            }
            str = param2.iname;
        Label_002E:
            if (pinID == null)
            {
                goto Label_003D;
            }
            goto Label_0058;
        Label_003D:
            if (str == null)
            {
                goto Label_0058;
            }
            str2 = PlayerPrefsUtility.GetString(PlayerPrefsUtility.CONFIRM_TOWER_MISSION_QUEST_ID, string.Empty);
        Label_0058:
            if ((str == str2) == null)
            {
                goto Label_0082;
            }
            base.ActivateOutputLinks(1);
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.CONFIRM_TOWER_MISSION_QUEST_ID, string.Empty, 1);
            goto Label_008A;
        Label_0082:
            base.ActivateOutputLinks(2);
        Label_008A:
            return;
        }
    }
}

