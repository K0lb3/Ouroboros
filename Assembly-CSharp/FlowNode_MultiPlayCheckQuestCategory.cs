// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlayCheckQuestCategory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;

[FlowNode.Pin(2, "EventOnly", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(3, "Both", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(1, "NormalOnly", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(0, "None", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(100, "TestExist", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Multi/MultiPlayCheckQuestCategory", 32741)]
public class FlowNode_MultiPlayCheckQuestCategory : FlowNode
{
  public override void OnActivate(int pinID)
  {
    if (pinID != 100)
      return;
    bool flag1 = false;
    bool flag2 = false;
    for (int index = 0; index < MonoSingleton<GameManager>.Instance.Quests.Length; ++index)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.Quests[index];
      if (quest.IsMulti)
      {
        if (quest.IsMultiEvent)
          flag2 = true;
        else
          flag1 = true;
        if (flag1 && flag2)
          break;
      }
    }
    if (flag1 && flag2)
    {
      DebugUtility.Log("BOTH");
      this.ActivateOutputLinks(3);
    }
    else if (!flag1 && flag2)
    {
      DebugUtility.Log("EVENT Only");
      this.ActivateOutputLinks(2);
    }
    else if (flag1 && !flag2)
    {
      DebugUtility.Log("NORMAL Only");
      this.ActivateOutputLinks(1);
    }
    else
    {
      DebugUtility.Log("NONE");
      this.ActivateOutputLinks(0);
    }
  }
}
