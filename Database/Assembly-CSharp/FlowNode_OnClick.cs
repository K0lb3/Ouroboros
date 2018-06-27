// Decompiled with JetBrains decompiler
// Type: FlowNode_OnClick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[FlowNode.NodeType("Event/OnClickButton", 58751)]
[AddComponentMenu("")]
[FlowNode.Pin(1, "Clicked", FlowNode.PinTypes.Output, 0)]
public class FlowNode_OnClick : FlowNodePersistent
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (Button), true)]
  public Button Target;
  private Button mBound;

  private void Start()
  {
    this.BindTargetButton();
    ((Behaviour) this).set_enabled(false);
  }

  private void BindTargetButton()
  {
    if (!Object.op_Inequality((Object) this.Target, (Object) null) || !Object.op_Inequality((Object) this.Target, (Object) this.mBound))
      return;
    // ISSUE: method pointer
    ((UnityEvent) this.Target.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnClick)));
    this.mBound = this.Target;
  }

  private void OnClick()
  {
    this.Activate(1);
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != 1)
      return;
    this.ActivateOutputLinks(1);
  }
}
