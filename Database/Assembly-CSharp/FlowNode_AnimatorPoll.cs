// Decompiled with JetBrains decompiler
// Type: FlowNode_AnimatorPoll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(2, "NoAnim", FlowNode.PinTypes.Output, 3)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(11, "Cancel", FlowNode.PinTypes.Input, 1)]
[FlowNode.NodeType("Animator/Poll", 32741)]
[FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 0)]
public class FlowNode_AnimatorPoll : FlowNode
{
  [FlowNode.ShowInInfo]
  public string StateName = string.Empty;
  [FlowNode.DropTarget(typeof (GameObject), true)]
  [FlowNode.ShowInInfo]
  public GameObject Target;
  private Animator mAnimator;

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 10:
        this.mAnimator = !Object.op_Inequality((Object) this.Target, (Object) null) ? (Animator) ((Component) this).GetComponent<Animator>() : (Animator) this.Target.GetComponent<Animator>();
        ((Behaviour) this).set_enabled(true);
        this.Update();
        break;
      case 11:
        ((Behaviour) this).set_enabled(false);
        break;
    }
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.mAnimator, (Object) null) || !((Component) this.mAnimator).get_gameObject().GetActive())
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(2);
    }
    else if (Object.op_Equality((Object) this.mAnimator.get_runtimeAnimatorController(), (Object) null) || this.mAnimator.get_runtimeAnimatorController().get_animationClips() == null || this.mAnimator.get_runtimeAnimatorController().get_animationClips().Length == 0)
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }
    else
    {
      AnimatorStateInfo animatorStateInfo = this.mAnimator.GetCurrentAnimatorStateInfo(0);
      // ISSUE: explicit reference operation
      if (!((AnimatorStateInfo) @animatorStateInfo).IsName(this.StateName))
        return;
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }
  }
}
