// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_QuestTutorial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Tutorial/Quest Tutorial", 32741)]
  [FlowNode.Pin(3, "Confirm", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_QuestTutorial : FlowNode
  {
    public string QuestID;
    public FlowNode_QuestTutorial.TriggerConditions Condition;
    public string ConfirmText;
    public string LocalFlag;
    public bool CheckLastPlayed;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (!string.IsNullOrEmpty(this.QuestID) && (!this.CheckLastPlayed ? GlobalVars.SelectedQuestID : GlobalVars.LastPlayedQuest.Get()) != this.QuestID)
      {
        this.OnNo((GameObject) null);
      }
      else
      {
        string str = (string) null;
        if (!string.IsNullOrEmpty(this.LocalFlag))
          str = FlowNode_Variable.Get(this.LocalFlag);
        if (this.CheckCondition() && string.IsNullOrEmpty(str))
        {
          if (!string.IsNullOrEmpty(this.LocalFlag))
            FlowNode_Variable.Set(this.LocalFlag, "1");
          if (!string.IsNullOrEmpty(this.ConfirmText))
          {
            this.ActivateOutputLinks(3);
            UIUtility.ConfirmBox(LocalizedText.Get(this.ConfirmText), new UIUtility.DialogResultEvent(this.OnYes), new UIUtility.DialogResultEvent(this.OnNo), (GameObject) null, true, -1, (string) null, (string) null);
          }
          else
            this.OnYes((GameObject) null);
        }
        else
          this.OnNo((GameObject) null);
      }
    }

    private bool CheckCondition()
    {
      switch (this.Condition)
      {
        case FlowNode_QuestTutorial.TriggerConditions.FirstTry:
          return GlobalVars.LastQuestState.Get() == QuestStates.New;
        case FlowNode_QuestTutorial.TriggerConditions.FirstWin:
          if (GlobalVars.LastQuestResult.Get() == BattleCore.QuestResult.Win)
            return GlobalVars.LastQuestState.Get() != QuestStates.Cleared;
          return false;
        case FlowNode_QuestTutorial.TriggerConditions.FirstLose:
          if (GlobalVars.LastQuestResult.Get() != BattleCore.QuestResult.Win)
            return GlobalVars.LastQuestState.Get() == QuestStates.New;
          return false;
        case FlowNode_QuestTutorial.TriggerConditions.Win:
          return GlobalVars.LastQuestResult.Get() == BattleCore.QuestResult.Win;
        case FlowNode_QuestTutorial.TriggerConditions.Lose:
          return GlobalVars.LastQuestResult.Get() != BattleCore.QuestResult.Win;
        default:
          return true;
      }
    }

    private void OnYes(GameObject go)
    {
      this.ActivateOutputLinks(1);
    }

    private void OnNo(GameObject go)
    {
      this.ActivateOutputLinks(2);
    }

    public enum TriggerConditions
    {
      None,
      FirstTry,
      FirstWin,
      FirstLose,
      Win,
      Lose,
    }
  }
}
