// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleCanvas
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(0, "Out", FlowNode.PinTypes.Output, 999)]
[FlowNode.NodeType("Toggle/Canvas", 32741)]
[FlowNode.Pin(2, "Turn Off", FlowNode.PinTypes.Input, 6)]
[FlowNode.Pin(1, "Turn On", FlowNode.PinTypes.Input, 5)]
public class FlowNode_ToggleCanvas : FlowNode
{
  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 1:
        Canvas component1 = (Canvas) ((Component) this).GetComponent<Canvas>();
        if (!Object.op_Inequality((Object) component1, (Object) null))
          break;
        ((Behaviour) component1).set_enabled(true);
        if (!Object.op_Inequality((Object) ((Component) component1).GetComponent<CanvasStack>(), (Object) null))
          break;
        CanvasStack.SortCanvases();
        break;
      case 2:
        Canvas component2 = (Canvas) ((Component) this).GetComponent<Canvas>();
        if (!Object.op_Inequality((Object) component2, (Object) null))
          break;
        ((Behaviour) component2).set_enabled(false);
        if (!Object.op_Inequality((Object) ((Component) component2).GetComponent<CanvasStack>(), (Object) null))
          break;
        CanvasStack.SortCanvases();
        break;
    }
  }
}
