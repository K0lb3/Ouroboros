// Decompiled with JetBrains decompiler
// Type: UIDeactivator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class UIDeactivator : MonoBehaviour
{
  private float mCountDown;

  public UIDeactivator()
  {
    base.\u002Ector();
    this.keyname = "close";
  }

  public string keyname { get; set; }

  private void OnEnable()
  {
    this.mCountDown = 0.5f;
  }

  private void OnDisable()
  {
    this.mCountDown = 0.5f;
  }

  private void LateUpdate()
  {
    bool flag = false;
    Animator component1 = (Animator) ((Component) this).GetComponent<Animator>();
    if (Object.op_Inequality((Object) component1, (Object) null) && GameUtility.CompareAnimatorStateName((Component) component1, this.keyname))
    {
      AnimatorStateInfo animatorStateInfo = component1.GetCurrentAnimatorStateInfo(0);
      // ISSUE: explicit reference operation
      if ((double) ((AnimatorStateInfo) @animatorStateInfo).get_normalizedTime() >= 1.0)
        flag = ((flag ? 1 : 0) | 1) != 0;
    }
    CanvasGroup component2 = (CanvasGroup) ((Component) this).GetComponent<CanvasGroup>();
    if (Object.op_Inequality((Object) component2, (Object) null))
      flag |= (double) component2.get_alpha() <= 0.0;
    if (flag)
    {
      if ((double) this.mCountDown <= 0.0)
        ((Component) this).get_gameObject().SetActive(false);
      else
        this.mCountDown = Mathf.Max(this.mCountDown - Time.get_deltaTime(), 0.0f);
    }
    else
      this.mCountDown = 0.5f;
  }
}
