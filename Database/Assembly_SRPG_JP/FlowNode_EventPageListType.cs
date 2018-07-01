// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EventPageListType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "設定", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "完了", FlowNode.PinTypes.Output, 100)]
  [FlowNode.NodeType("System/EventPageListType")]
  public class FlowNode_EventPageListType : FlowNode
  {
    [SerializeField]
    private GlobalVars.EventQuestListType m_TargetEventQuestListType;

    public override void OnActivate(int pinID)
    {
      base.OnActivate(pinID);
      if (pinID != 0)
        return;
      GlobalVars.ReqEventPageListType = this.m_TargetEventQuestListType;
      this.ActivateOutputLinks(100);
    }
  }
}
