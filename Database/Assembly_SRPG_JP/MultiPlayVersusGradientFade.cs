// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayVersusGradientFade
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class MultiPlayVersusGradientFade : MonoBehaviour
  {
    private const string STATE_FADE_IN = "FadeIn";
    private const string STATE_FADE_OUT = "FadeOut";
    private const string STATE_FADE_IN_FINISH = "FadeInFinish";
    private const string STATE_FADE_OUT_FINISH = "FadeOutFinish";
    private static MultiPlayVersusGradientFade sInstance;
    private bool mFading;
    private Animator mAnimator;
    private CanvasGroup mCanvasGroup;
    private string mStateName;

    public MultiPlayVersusGradientFade()
    {
      base.\u002Ector();
    }

    public static MultiPlayVersusGradientFade Instance
    {
      get
      {
        return MultiPlayVersusGradientFade.sInstance;
      }
    }

    public bool Fading
    {
      get
      {
        return this.mFading;
      }
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) MultiPlayVersusGradientFade.sInstance, (Object) null))
        Object.Destroy((Object) this);
      MultiPlayVersusGradientFade.sInstance = this;
      this.mAnimator = (Animator) ((Component) this).GetComponent<Animator>();
      this.mCanvasGroup = (CanvasGroup) ((Component) this).GetComponent<CanvasGroup>();
    }

    private void OnDestroy()
    {
      if (!Object.op_Equality((Object) MultiPlayVersusGradientFade.sInstance, (Object) this))
        return;
      MultiPlayVersusGradientFade.sInstance = (MultiPlayVersusGradientFade) null;
    }

    public void FadeIn()
    {
      if (this.mFading)
        return;
      this.mAnimator.Play(nameof (FadeIn));
      this.mStateName = "FadeInFinish";
      this.mFading = true;
    }

    public void FadeOut()
    {
      if (this.mFading)
        return;
      this.mCanvasGroup.set_blocksRaycasts(true);
      this.mAnimator.Play(nameof (FadeOut));
      this.mStateName = "FadeOutFinish";
      this.mFading = true;
    }

    private void Update()
    {
      if (!this.mFading)
        return;
      AnimatorStateInfo animatorStateInfo = this.mAnimator.GetCurrentAnimatorStateInfo(0);
      // ISSUE: explicit reference operation
      if (!((AnimatorStateInfo) @animatorStateInfo).IsName(this.mStateName))
        return;
      if (this.mStateName == "FadeInFinish")
        this.mCanvasGroup.set_blocksRaycasts(false);
      this.mFading = false;
    }
  }
}
