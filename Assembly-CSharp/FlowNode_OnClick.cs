// Decompiled with JetBrains decompiler
// Type: FlowNode_OnClick
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[FlowNode.Pin(1, "Clicked", FlowNode.PinTypes.Output, 0)]
[FlowNode.NodeType("Event/OnClickButton", 58751)]
[AddComponentMenu("")]
public class FlowNode_OnClick : FlowNodePersistent
{
  [FlowNode.DropTarget(typeof (Button), true)]
  [FlowNode.ShowInInfo]
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
