// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AppVer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/App Version", 32741)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Default", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_AppVer : FlowNode
  {
    [FlexibleArray]
    public string[] Versions = new string[0];

    public override FlowNode.Pin[] GetDynamicPins()
    {
      FlowNode.Pin[] pinArray = new FlowNode.Pin[this.Versions.Length];
      for (int index = 0; index < this.Versions.Length; ++index)
        pinArray[index] = new FlowNode.Pin(2 + index, this.Versions[index], FlowNode.PinTypes.Output, 2 + index);
      return pinArray;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      for (int index = 0; index < this.Versions.Length; ++index)
      {
        if (Application.get_version() == this.Versions[index])
        {
          this.ActivateOutputLinks(2 + index);
          return;
        }
      }
      this.ActivateOutputLinks(1);
    }
  }
}
