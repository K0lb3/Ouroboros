// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("UI/PartyWindow")]
  public class FlowNode_PartyWindow : FlowNode_GUI
  {
    public bool ShowQuestInfo = true;
    public bool UseQuest = true;
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
      componentInChildren.UseQuestInfo = this.UseQuest;
      if (this.BackButton != FlowNode_PartyWindow.TriBool.Unchanged)
        componentInChildren.ShowBackButton = this.BackButton == FlowNode_PartyWindow.TriBool.True;
      if (this.ForwardButton != FlowNode_PartyWindow.TriBool.Unchanged)
        componentInChildren.ShowForwardButton = this.ForwardButton == FlowNode_PartyWindow.TriBool.True;
      if (this.ShowRaidInfo != FlowNode_PartyWindow.TriBool.Unchanged)
        componentInChildren.ShowRaidInfo = this.ShowRaidInfo == FlowNode_PartyWindow.TriBool.True;
      this.OffCanvas(componentInChildren);
    }

    protected override void OnCreatePinActive()
    {
      if (Object.op_Inequality((Object) this.Instance, (Object) null))
      {
        PartyWindow2 component = (PartyWindow2) this.Instance.GetComponent<PartyWindow2>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        this.OffCanvas(component);
        component.Reopen();
      }
      else
        base.OnCreatePinActive();
    }

    private void OffCanvas(PartyWindow2 pw)
    {
      if (this.PartyType != PartyWindow2.EditPartyTypes.MultiTower)
        return;
      PartyWindow2 component = (PartyWindow2) ((Component) pw).GetComponent<PartyWindow2>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      ((Component) component.MainRect).get_gameObject().SetActive(false);
    }

    public enum TriBool
    {
      Unchanged,
      False,
      True,
    }
  }
}
