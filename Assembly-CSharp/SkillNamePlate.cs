// Decompiled with JetBrains decompiler
// Type: SkillNamePlate
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class SkillNamePlate : MonoBehaviour
{
  public string EndStateTrigger;
  public string HideStateTrigger;
  public string EndStateName;
  public Text SkillName;
  private Animator mAnimator;
  public bool mClose;

  public SkillNamePlate()
  {
    base.\u002Ector();
  }

  public void SetSkillName(string Name)
  {
    this.SkillName.set_text(Name);
  }

  public void Open()
  {
    if (!Object.op_Implicit((Object) this.mAnimator))
      return;
    ((Component) this).get_gameObject().SetActive(true);
    this.mAnimator.SetBool(this.EndStateTrigger, true);
    this.mAnimator.SetBool(this.HideStateTrigger, false);
    this.mClose = false;
  }

  public void Close()
  {
    this.mClose = true;
  }

  private void Start()
  {
    this.mAnimator = (Animator) ((Component) this).GetComponent<Animator>();
    this.mClose = true;
  }

  private void Update()
  {
    if (this.mClose)
      this.mAnimator.SetBool(this.EndStateTrigger, false);
    AnimatorStateInfo animatorStateInfo1 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
    // ISSUE: explicit reference operation
    if (!((AnimatorStateInfo) @animatorStateInfo1).IsName(this.EndStateName) || this.mAnimator.IsInTransition(0))
      return;
    AnimatorStateInfo animatorStateInfo2 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
    // ISSUE: explicit reference operation
    if ((double) ((AnimatorStateInfo) @animatorStateInfo2).get_normalizedTime() < 1.0)
      return;
    this.mAnimator.SetBool(this.HideStateTrigger, true);
    ((Component) this).get_gameObject().SetActive(false);
  }
}
