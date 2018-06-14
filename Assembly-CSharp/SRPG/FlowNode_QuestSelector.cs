// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_QuestSelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/Quest/Selector", 32741)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_QuestSelector : FlowNode
  {
    [SerializeField]
    private GlobalVars.EventQuestListType EventQuestListType;

    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
        GlobalVars.ReqEventPageListType = this.EventQuestListType;
      this.ActivateOutputLinks(100);
    }
  }
}
