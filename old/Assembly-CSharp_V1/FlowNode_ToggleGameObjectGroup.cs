// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleGameObjectGroup
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(10, "Enable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(3, "Disabled", FlowNode.PinTypes.Output, 4)]
[FlowNode.Pin(11, "Disable", FlowNode.PinTypes.Input, 1)]
[FlowNode.NodeType("Toggle/GameObjectGroup", 32741)]
[FlowNode.Pin(2, "Enabled", FlowNode.PinTypes.Output, 3)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 2)]
public class FlowNode_ToggleGameObjectGroup : FlowNode
{
  [FlowNode.DropTarget(typeof (GameObject), true)]
  [FlowNode.ShowInInfo]
  public GameObject[] Target;

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 10:
        if (this.Target != null)
        {
          for (int index = 0; index < this.Target.Length; ++index)
          {
            if (Object.op_Inequality((Object) this.Target[index], (Object) null))
              this.Target[index].SetActive(true);
          }
        }
        this.ActivateOutputLinks(2);
        break;
      case 11:
        if (this.Target != null)
        {
          for (int index = 0; index < this.Target.Length; ++index)
          {
            if (Object.op_Inequality((Object) this.Target[index], (Object) null))
              this.Target[index].SetActive(false);
          }
        }
        this.ActivateOutputLinks(3);
        break;
    }
    this.ActivateOutputLinks(1);
  }
}
