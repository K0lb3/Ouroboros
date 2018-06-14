// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UpdateParameter
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("UI/UpdateParameter", 32741)]
  [AddComponentMenu("")]
  [FlowNode.Pin(1, "Updated", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(101, "UpdateAll", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(100, "Update", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_UpdateParameter : FlowNode
  {
    [FlowNode.ShowInInfo]
    [FlowNode.DropTarget(typeof (GameObject), false)]
    public GameObject Target;

    public override void OnActivate(int pinID)
    {
      if (pinID != 100 && pinID != 101 || !Object.op_Inequality((Object) this.Target, (Object) null))
        return;
      foreach (IGameParameter componentsInChild in this.Target.GetComponentsInChildren(typeof (IGameParameter), pinID == 101))
        componentsInChild.UpdateValue();
      this.ActivateOutputLinks(1);
    }
  }
}
