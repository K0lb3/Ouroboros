// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GetTowerMissionPrefs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("Tower/GetTowerMissionPrefs", 32741)]
  [FlowNode.Pin(0, "ミッションID取得", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "取得成功", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "取得出来ない", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_GetTowerMissionPrefs : FlowNode
  {
    private const int INPUT_GET_MISSION_ID = 0;
    private const int OUTPUT_SUCCESS_GET_MISSION_ID = 1;
    private const int OUTPUT_NONE_GET_MISSION_ID = 2;

    public override void OnActivate(int pinID)
    {
      string str1 = (string) null;
      string str2 = (string) null;
      TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(GlobalVars.SelectedQuestID);
      if (towerFloor != null)
      {
        QuestParam questParam = towerFloor.GetQuestParam();
        if (questParam != null)
          str1 = questParam.iname;
      }
      if (pinID == 0 && str1 != null)
        str2 = PlayerPrefsUtility.GetString(PlayerPrefsUtility.CONFIRM_TOWER_MISSION_QUEST_ID, string.Empty);
      if (str1 == str2)
      {
        this.ActivateOutputLinks(1);
        PlayerPrefsUtility.SetString(PlayerPrefsUtility.CONFIRM_TOWER_MISSION_QUEST_ID, string.Empty, true);
      }
      else
        this.ActivateOutputLinks(2);
    }
  }
}
