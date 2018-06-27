// Decompiled with JetBrains decompiler
// Type: UISplash
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class UISplash : MonoBehaviour
{
  public bool IsTapSplash;

  public UISplash()
  {
    base.\u002Ector();
  }

  private void Start()
  {
  }

  private void Update()
  {
    if (!this.IsTapSplash)
      return;
    Animator component = (Animator) ((Component) this).GetComponent<Animator>();
    if (!Object.op_Inequality((Object) component, (Object) null) || !GameUtility.CompareAnimatorStateName((Component) component, "loop") || !Input.GetMouseButtonDown(0))
      return;
    component.SetBool("close", true);
  }

  private void LateUpdate()
  {
    Animator component = (Animator) ((Component) this).GetComponent<Animator>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    if (this.IsTapSplash)
    {
      if (!GameUtility.CompareAnimatorStateName((Component) component, "close"))
        return;
      AnimatorStateInfo animatorStateInfo = component.GetCurrentAnimatorStateInfo(0);
      // ISSUE: explicit reference operation
      if ((double) ((AnimatorStateInfo) @animatorStateInfo).get_normalizedTime() < 1.0)
        return;
      UIUtility.PopCanvas(true);
      GameUtility.DestroyGameObject((Component) ((Component) this).GetComponentInParent<Canvas>());
    }
    else
    {
      if (!GameUtility.CompareAnimatorStateName((Component) component, "open"))
        return;
      AnimatorStateInfo animatorStateInfo = component.GetCurrentAnimatorStateInfo(0);
      // ISSUE: explicit reference operation
      if ((double) ((AnimatorStateInfo) @animatorStateInfo).get_normalizedTime() < 1.0)
        return;
      GameUtility.SetGameObjectActive(((Component) this).get_gameObject(), false);
      GameUtility.DestroyGameObject(((Component) this).get_gameObject());
    }
  }
}
