// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ShowTips
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Tips/ShowTips", 32741)]
  public class FlowNode_ShowTips : FlowNode_GUI
  {
    private const int PIN_ID_IN = 1;
    [SerializeField]
    private string Tips;

    protected override void OnCreatePinActive()
    {
      GlobalVars.LastReadTips = this.Tips;
      base.OnCreatePinActive();
    }
  }
}
