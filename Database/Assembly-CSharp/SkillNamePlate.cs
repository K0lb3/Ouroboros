// Decompiled with JetBrains decompiler
// Type: SkillNamePlate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;
using UnityEngine.UI;

public class SkillNamePlate : MonoBehaviour
{
  public string EndStateTrigger;
  public string HideStateTrigger;
  public string EndStateName;
  public Text SkillName;
  public ImageArray SkillBgHead;
  public ImageArray SkillBgBody;
  public ImageArray SkillAttackType;
  public ImageArray SkillElement;
  public ImageArray SkillAttackDetail;
  private Animator mAnimator;
  private float mSpeed;
  public bool mClose;

  public SkillNamePlate()
  {
    base.\u002Ector();
  }

  public void SetSkillName(string Name, EUnitSide side, EElement element = EElement.None, AttackDetailTypes ad_type = AttackDetailTypes.None, AttackTypes atk_type = AttackTypes.None)
  {
    if (Object.op_Implicit((Object) this.SkillName))
      this.SkillName.set_text(Name);
    if (Object.op_Implicit((Object) this.SkillBgHead))
    {
      int num = (int) side;
      if (num >= 0 && num < this.SkillBgHead.Images.Length)
        this.SkillBgHead.ImageIndex = num;
    }
    if (Object.op_Implicit((Object) this.SkillBgBody))
    {
      int num = (int) side;
      if (num >= 0 && num < this.SkillBgBody.Images.Length)
        this.SkillBgBody.ImageIndex = num;
    }
    if (Object.op_Implicit((Object) this.SkillAttackType))
    {
      if (atk_type != AttackTypes.None)
      {
        int num = (int) atk_type;
        if (num >= 0 && num < this.SkillAttackType.Images.Length)
          this.SkillAttackType.ImageIndex = num;
      }
      ((Component) this.SkillAttackType).get_gameObject().SetActive(atk_type != AttackTypes.None);
    }
    if (Object.op_Implicit((Object) this.SkillElement))
    {
      if (element != EElement.None)
      {
        int num = (int) element;
        if (num >= 0 && num < this.SkillElement.Images.Length)
          this.SkillElement.ImageIndex = num;
      }
      ((Component) this.SkillElement).get_gameObject().SetActive(element != EElement.None);
    }
    if (!Object.op_Implicit((Object) this.SkillAttackDetail))
      return;
    if (ad_type != AttackDetailTypes.None)
    {
      int num = (int) ad_type;
      if (num >= 0 && num < this.SkillAttackDetail.Images.Length)
        this.SkillAttackDetail.ImageIndex = num;
    }
    ((Component) this.SkillAttackDetail).get_gameObject().SetActive(ad_type != AttackDetailTypes.None);
  }

  public void Open(float speed = 1f)
  {
    if (!Object.op_Implicit((Object) this.mAnimator))
      return;
    ((Component) this).get_gameObject().SetActive(true);
    this.mAnimator.SetBool(this.EndStateTrigger, true);
    this.mAnimator.SetBool(this.HideStateTrigger, false);
    this.mSpeed = speed;
    this.mClose = false;
  }

  public void Close()
  {
    this.mClose = true;
  }

  private void Start()
  {
    this.mAnimator = (Animator) ((Component) this).GetComponent<Animator>();
    this.mSpeed = 1f;
    this.mClose = true;
    if (Object.op_Implicit((Object) this.SkillElement))
      ((Component) this.SkillElement).get_gameObject().SetActive(false);
    if (!Object.op_Implicit((Object) this.SkillAttackDetail))
      return;
    ((Component) this.SkillAttackDetail).get_gameObject().SetActive(false);
  }

  private void Update()
  {
    if (this.mClose)
      this.mAnimator.SetBool(this.EndStateTrigger, false);
    AnimatorStateInfo animatorStateInfo1 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
    // ISSUE: explicit reference operation
    if (((AnimatorStateInfo) @animatorStateInfo1).IsName(this.EndStateTrigger))
    {
      this.mAnimator.set_speed(this.mSpeed);
    }
    else
    {
      this.mSpeed = 1f;
      this.mAnimator.set_speed(1f);
    }
    AnimatorStateInfo animatorStateInfo2 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
    // ISSUE: explicit reference operation
    if (!((AnimatorStateInfo) @animatorStateInfo2).IsName(this.EndStateName) || this.mAnimator.IsInTransition(0))
      return;
    AnimatorStateInfo animatorStateInfo3 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
    // ISSUE: explicit reference operation
    if ((double) ((AnimatorStateInfo) @animatorStateInfo3).get_normalizedTime() < 1.0)
      return;
    this.mAnimator.SetBool(this.HideStateTrigger, true);
    ((Component) this).get_gameObject().SetActive(false);
  }

  public bool IsClosed()
  {
    if (this.mClose)
    {
      if (!((Component) this).get_gameObject().get_activeSelf())
        return true;
      AnimatorStateInfo animatorStateInfo1 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
      // ISSUE: explicit reference operation
      if (((AnimatorStateInfo) @animatorStateInfo1).IsName("closed") && !this.mAnimator.IsInTransition(0))
      {
        AnimatorStateInfo animatorStateInfo2 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
        // ISSUE: explicit reference operation
        if ((double) ((AnimatorStateInfo) @animatorStateInfo2).get_normalizedTime() >= 1.0)
          return true;
      }
    }
    return false;
  }
}
