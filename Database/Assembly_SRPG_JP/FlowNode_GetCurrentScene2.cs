// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GetCurrentScene2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "Other", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "Cancel", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(106, "Unknown", FlowNode.PinTypes.Output, 106)]
  [FlowNode.Pin(105, "Title", FlowNode.PinTypes.Output, 105)]
  [FlowNode.Pin(104, "Home", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(103, "HomeMulti", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(102, "Multi", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(101, "Single", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(0, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Scene/GetCurrentScene2", 32741)]
  public class FlowNode_GetCurrentScene2 : FlowNode
  {
    public string HomeString = "Home";
    public string TownString = "town";

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Object.op_Inequality((Object) CanvasStack.Top, (Object) null) && ((CanvasStack) ((Component) CanvasStack.Top).GetComponent<CanvasStack>()).SystemModal)
        this.ActivateOutputLinks(110);
      else if (StreamingMovie.IsPlaying)
      {
        this.ActivateOutputLinks(110);
      }
      else
      {
        int pinID1;
        switch (GameUtility.GetCurrentScene())
        {
          case GameUtility.EScene.BATTLE:
            DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.BATTLE");
            pinID1 = 101;
            break;
          case GameUtility.EScene.BATTLE_MULTI:
            DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.BATTLE_MULTI");
            pinID1 = 102;
            break;
          case GameUtility.EScene.HOME:
            DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.NOT_HOME");
            pinID1 = 100;
            if (Object.op_Implicit((Object) HomeWindow.Current) && HomeWindow.Current.DesiredSceneIsHome)
            {
              DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.NONE");
              pinID1 = 104;
              break;
            }
            break;
          case GameUtility.EScene.HOME_MULTI:
            DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.HOME_MULTI");
            pinID1 = 103;
            break;
          case GameUtility.EScene.TITLE:
            DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.TITLE");
            pinID1 = 105;
            break;
          default:
            DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.UNKNOWN");
            pinID1 = 106;
            break;
        }
        this.ActivateOutputLinks(pinID1);
      }
    }
  }
}
