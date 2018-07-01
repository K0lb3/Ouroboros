// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TowerPartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("UI/TowerPartyWindow")]
  public class FlowNode_TowerPartyWindow : FlowNode_GUI
  {
    public bool ShowQuestInfo = true;
    public PartyWindow2.EditPartyTypes PartyType;
    public FlowNode_TowerPartyWindow.TriBool BackButton;
    public FlowNode_TowerPartyWindow.TriBool ForwardButton;
    public FlowNode_TowerPartyWindow.TriBool ShowRaidInfo;

    protected override void OnInstanceCreate()
    {
      base.OnInstanceCreate();
      TowerPartyWindow componentInChildren = (TowerPartyWindow) this.Instance.GetComponentInChildren<TowerPartyWindow>();
      if (Object.op_Equality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.PartyType = this.PartyType;
      componentInChildren.ShowQuestInfo = this.ShowQuestInfo;
      if (this.BackButton != FlowNode_TowerPartyWindow.TriBool.Unchanged)
        componentInChildren.ShowBackButton = this.BackButton == FlowNode_TowerPartyWindow.TriBool.True;
      if (this.ForwardButton != FlowNode_TowerPartyWindow.TriBool.Unchanged)
        componentInChildren.ShowForwardButton = this.ForwardButton == FlowNode_TowerPartyWindow.TriBool.True;
      if (this.ShowRaidInfo == FlowNode_TowerPartyWindow.TriBool.Unchanged)
        return;
      componentInChildren.ShowRaidInfo = this.ShowRaidInfo == FlowNode_TowerPartyWindow.TriBool.True;
    }

    protected override void OnCreatePinActive()
    {
      if (Object.op_Inequality((Object) this.Instance, (Object) null))
      {
        TowerPartyWindow component = (TowerPartyWindow) this.Instance.GetComponent<TowerPartyWindow>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        component.Reopen(false);
        GameParameter.UpdateAll(((Component) component).get_gameObject());
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
