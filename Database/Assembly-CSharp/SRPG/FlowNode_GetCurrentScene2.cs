// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GetCurrentScene2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(106, "Unknown", FlowNode.PinTypes.Output, 106)]
  [FlowNode.Pin(100, "Other", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Single", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Multi", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "HomeMulti", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "Home", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(105, "Title", FlowNode.PinTypes.Output, 105)]
  [FlowNode.NodeType("Scene/GetCurrentScene2", 32741)]
  [FlowNode.Pin(0, "Input", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_GetCurrentScene2 : FlowNode
  {
    public string HomeString = "Home";
    public string TownString = "town";

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      switch (GameUtility.GetCurrentScene())
      {
        case GameUtility.EScene.BATTLE:
          DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.BATTLE");
          this.ActivateOutputLinks(101);
          break;
        case GameUtility.EScene.BATTLE_MULTI:
          DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.BATTLE_MULTI");
          this.ActivateOutputLinks(102);
          break;
        case GameUtility.EScene.HOME:
          DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.NOT_HOME");
          int pinID1 = 100;
          if (Object.op_Implicit((Object) HomeWindow.Current) && HomeWindow.Current.DesiredSceneIsHome)
          {
            DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.NONE");
            pinID1 = 104;
          }
          this.ActivateOutputLinks(pinID1);
          break;
        case GameUtility.EScene.HOME_MULTI:
          DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.HOME_MULTI");
          this.ActivateOutputLinks(103);
          break;
        case GameUtility.EScene.TITLE:
          DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.TITLE");
          this.ActivateOutputLinks(105);
          break;
        default:
          DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.UNKNOWN");
          this.ActivateOutputLinks(106);
          break;
      }
    }
  }
}
