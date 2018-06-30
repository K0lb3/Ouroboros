namespace SRPG
{
    using GR;
    using System;

    [Pin(1, "設定完了", 1, 1), NodeType("Tower/SetTowerMissionPrefs", 0x7fe5), Pin(0, "ミッションID設定", 0, 0)]
    public class FlowNode_SetTowerMissionPrefs : FlowNode
    {
        private const int INPUT_SET_MISSION_ID = 0;
        private const int OUTPUT_SET_MISSION_ID = 1;

        public FlowNode_SetTowerMissionPrefs()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            bool flag;
            string str;
            TowerFloorParam param;
            QuestParam param2;
            int num;
            flag = 0;
            str = null;
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
            goto Label_0055;
        Label_003D:
            if (str == null)
            {
                goto Label_0055;
            }
            flag = PlayerPrefsUtility.SetString(PlayerPrefsUtility.CONFIRM_TOWER_MISSION_QUEST_ID, str, 1);
        Label_0055:
            if (flag != null)
            {
                goto Label_0065;
            }
            DebugUtility.Log("PlayerPrefsの設定に失敗しました");
        Label_0065:
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

