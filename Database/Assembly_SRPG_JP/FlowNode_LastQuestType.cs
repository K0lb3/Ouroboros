// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LastQuestType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(204, "Tutorial", FlowNode.PinTypes.Output, 204)]
  [FlowNode.Pin(217, "RankMatch", FlowNode.PinTypes.Output, 217)]
  [FlowNode.Pin(216, "Ordeal", FlowNode.PinTypes.Output, 216)]
  [FlowNode.Pin(215, "MultiGps", FlowNode.PinTypes.Output, 215)]
  [FlowNode.Pin(214, "Beginner", FlowNode.PinTypes.Output, 214)]
  [FlowNode.Pin(213, "MultiTower", FlowNode.PinTypes.Output, 213)]
  [FlowNode.Pin(212, "Extra", FlowNode.PinTypes.Output, 212)]
  [FlowNode.Pin(211, "Gps", FlowNode.PinTypes.Output, 211)]
  [FlowNode.Pin(210, "VersusRank", FlowNode.PinTypes.Output, 210)]
  [FlowNode.Pin(209, "VersusFree", FlowNode.PinTypes.Output, 209)]
  [FlowNode.Pin(208, "Tower", FlowNode.PinTypes.Output, 208)]
  [FlowNode.Pin(207, "Character", FlowNode.PinTypes.Output, 207)]
  [FlowNode.Pin(206, "Event", FlowNode.PinTypes.Output, 206)]
  [FlowNode.Pin(205, "Free", FlowNode.PinTypes.Output, 205)]
  [FlowNode.Pin(203, "Arena", FlowNode.PinTypes.Output, 203)]
  [FlowNode.Pin(202, "Multi", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(201, "Story", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(200, "Input", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(102, "MultiPlay", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(101, "SinglePlay", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(100, "Input", FlowNode.PinTypes.Input, 100)]
  [FlowNode.NodeType("Battle/LastQuestType", 32741)]
  public class FlowNode_LastQuestType : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      ((Behaviour) this).set_enabled(false);
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      if (quest == null)
      {
        this.ActivateOutputLinks(101);
        this.ActivateOutputLinks(201);
      }
      switch (pinID)
      {
        case 100:
          switch (quest.type)
          {
            case QuestTypes.Story:
            case QuestTypes.Arena:
            case QuestTypes.Tutorial:
            case QuestTypes.Free:
            case QuestTypes.Event:
            case QuestTypes.Character:
            case QuestTypes.Tower:
            case QuestTypes.Gps:
            case QuestTypes.Extra:
            case QuestTypes.Beginner:
            case QuestTypes.Ordeal:
              this.ActivateOutputLinks(101);
              return;
            case QuestTypes.Multi:
            case QuestTypes.VersusFree:
            case QuestTypes.VersusRank:
            case QuestTypes.MultiTower:
            case QuestTypes.MultiGps:
            case QuestTypes.RankMatch:
              this.ActivateOutputLinks(102);
              return;
            default:
              DebugUtility.LogError("QuestTypesにTypeを追加したらここも見てください。");
              this.ActivateOutputLinks(101);
              return;
          }
        case 200:
          this.ActivateOutputLinks((int) ((byte) 201 + quest.type));
          break;
      }
    }
  }
}
