// Decompiled with JetBrains decompiler
// Type: AnimateBlockRaycasts
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class AnimateBlockRaycasts : MonoBehaviour
{
  private Animator mAnimator;
  private CanvasGroup mCanvasGroup;
  private int mBlockCount;
  public string[] BlockStates;

  public AnimateBlockRaycasts()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.mAnimator = (Animator) ((Component) this).GetComponent<Animator>();
    this.mCanvasGroup = (CanvasGroup) ((Component) this).GetComponent<CanvasGroup>();
    if (!Object.op_Equality((Object) this.mAnimator, (Object) null) && !Object.op_Equality((Object) this.mCanvasGroup, (Object) null))
      return;
    Object.Destroy((Object) ((Component) this).get_gameObject());
  }

  private void LateUpdate()
  {
    if (this.mAnimator.IsInTransition(0))
    {
      this.mBlockCount = 0;
      this.mCanvasGroup.set_blocksRaycasts(false);
    }
    else
    {
      for (int index = 0; index < this.BlockStates.Length; ++index)
      {
        AnimatorStateInfo animatorStateInfo = this.mAnimator.GetCurrentAnimatorStateInfo(0);
        // ISSUE: explicit reference operation
        if (((AnimatorStateInfo) @animatorStateInfo).IsName(this.BlockStates[index]))
        {
          this.mCanvasGroup.set_blocksRaycasts(this.mBlockCount > 0);
          ++this.mBlockCount;
          return;
        }
      }
      this.mBlockCount = 0;
      this.mCanvasGroup.set_blocksRaycasts(false);
    }
  }
}
