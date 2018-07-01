// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TutorialToggleAutoBattle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Tutorial/ToggleAutoBattle", 32741)]
  [FlowNode.Pin(2, "Disable", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Enable", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_TutorialToggleAutoBattle : FlowNode
  {
    private const int PIN_IN_ENABLE = 1;
    private const int PIN_IN_DISABLE = 2;
    private const int PIN_OUT_OUTPUT = 10;

    public override void OnActivate(int pinID)
    {
      InGameMenu inGameMenu = (InGameMenu) null;
      SceneBattle instance = SceneBattle.Instance;
      if (Object.op_Implicit((Object) instance) && Object.op_Implicit((Object) instance.BattleUI))
        inGameMenu = (InGameMenu) ((Component) instance.BattleUI).GetComponent<InGameMenu>();
      if (Object.op_Implicit((Object) inGameMenu))
      {
        switch (pinID)
        {
          case 1:
            inGameMenu.ToggleAutoBattle(true);
            break;
          case 2:
            inGameMenu.ToggleAutoBattle(false);
            break;
        }
      }
      this.ActivateOutputLinks(10);
    }
  }
}
