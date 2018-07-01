// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UpdateParameter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Updated", FlowNode.PinTypes.Output, 1)]
  [AddComponentMenu("")]
  [FlowNode.NodeType("UI/UpdateParameter", 32741)]
  [FlowNode.Pin(100, "Update", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "UpdateAll", FlowNode.PinTypes.Input, 2)]
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
