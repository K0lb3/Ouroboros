// Decompiled with JetBrains decompiler
// Type: FlowNode_AnimatorPollFromName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(100, "Output", FlowNode.PinTypes.Output, 100)]
[FlowNode.NodeType("Animator/PollFromName", 32741)]
public class FlowNode_AnimatorPollFromName : FlowNode
{
  public string m_AnimatorName = string.Empty;
  public string m_StateName = string.Empty;
  private Animator m_Animator;

  protected override void Awake()
  {
    base.Awake();
    GameObject gameObject = GameObject.Find(this.m_AnimatorName);
    if (!Object.op_Inequality((Object) gameObject, (Object) null))
      return;
    this.m_Animator = (Animator) gameObject.GetComponent<Animator>();
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != 10)
      return;
    ((Behaviour) this).set_enabled(true);
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.m_Animator, (Object) null) || !((Component) this.m_Animator).get_gameObject().GetActive())
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(100);
    }
    else if (Object.op_Equality((Object) this.m_Animator.get_runtimeAnimatorController(), (Object) null) || this.m_Animator.get_runtimeAnimatorController().get_animationClips() == null || this.m_Animator.get_runtimeAnimatorController().get_animationClips().Length == 0)
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(100);
    }
    else
    {
      AnimatorStateInfo animatorStateInfo = this.m_Animator.GetCurrentAnimatorStateInfo(0);
      // ISSUE: explicit reference operation
      if (!((AnimatorStateInfo) @animatorStateInfo).IsName(this.m_StateName))
        return;
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(100);
    }
  }
}
