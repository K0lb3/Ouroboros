// Decompiled with JetBrains decompiler
// Type: FlowNode_OnToggleChange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[AddComponentMenu("")]
[FlowNode.Pin(2, "Disable", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(1, "Enable", FlowNode.PinTypes.Output, 1)]
[FlowNode.NodeType("Event/OnToggleChange", 58751)]
public class FlowNode_OnToggleChange : FlowNodePersistent
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (Toggle), true)]
  public Toggle Target;

  private void Start()
  {
    if (Object.op_Inequality((Object) this.Target, (Object) null))
    {
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.Target.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnValueChanged)));
    }
    ((Behaviour) this).set_enabled(false);
  }

  private void OnValueChanged(bool value)
  {
    if (value)
      this.Activate(1);
    else
      this.Activate(2);
  }

  public override void OnActivate(int pinID)
  {
    if (pinID == 1)
    {
      this.ActivateOutputLinks(1);
    }
    else
    {
      if (pinID != 2)
        return;
      this.ActivateOutputLinks(2);
    }
  }
}
