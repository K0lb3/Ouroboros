// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PartyWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("UI/PartyWindow")]
  public class FlowNode_PartyWindow : FlowNode_GUI
  {
    public bool ShowQuestInfo = true;
    public PartyWindow2.EditPartyTypes PartyType;
    public FlowNode_PartyWindow.TriBool BackButton;
    public FlowNode_PartyWindow.TriBool ForwardButton;
    public FlowNode_PartyWindow.TriBool ShowRaidInfo;

    protected override void OnInstanceCreate()
    {
      base.OnInstanceCreate();
      PartyWindow2 componentInChildren = (PartyWindow2) this.Instance.GetComponentInChildren<PartyWindow2>();
      if (Object.op_Equality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.PartyType = this.PartyType;
      componentInChildren.ShowQuestInfo = this.ShowQuestInfo;
      if (this.BackButton != FlowNode_PartyWindow.TriBool.Unchanged)
        componentInChildren.ShowBackButton = this.BackButton == FlowNode_PartyWindow.TriBool.True;
      if (this.ForwardButton != FlowNode_PartyWindow.TriBool.Unchanged)
        componentInChildren.ShowForwardButton = this.ForwardButton == FlowNode_PartyWindow.TriBool.True;
      if (this.ShowRaidInfo == FlowNode_PartyWindow.TriBool.Unchanged)
        return;
      componentInChildren.ShowRaidInfo = this.ShowRaidInfo == FlowNode_PartyWindow.TriBool.True;
    }

    protected override void OnCreatePinActive()
    {
      if (Object.op_Inequality((Object) this.Instance, (Object) null))
      {
        PartyWindow2 component = (PartyWindow2) this.Instance.GetComponent<PartyWindow2>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        component.Reopen();
      }
      else
        base.OnCreatePinActive();
    }

    public enum TriBool
    {
      Unchanged,
      False,
      True,
    }
  }
}
