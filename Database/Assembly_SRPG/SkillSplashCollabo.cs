// Decompiled with JetBrains decompiler
// Type: SRPG.SkillSplashCollabo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (Animator))]
  public class SkillSplashCollabo : MonoBehaviour
  {
    [Description("スプラッシュ表示を閉じるのに使用するトリガーの名前")]
    public string EndTrigger;
    [Description("スプラッシュ表示が閉じられた状態のステートの名前、この状態になると先へ進みます")]
    public string EndStateName;
    public RawImage[] UnitImages2_main_Images;
    public RawImage[] UnitImages2_sub_Images;
    public RawImage[] UnitEyeImages_main_Images;
    public RawImage[] UnitEyeImages_sub_Images;
    private Animator mAnimator;
    private bool mClose;

    public SkillSplashCollabo()
    {
      base.\u002Ector();
    }

    public void SetCharaImages(Texture2D u2_main_tex, Texture2D u2_sub_tex, Texture2D ue_main_tex, Texture2D ue_sub_tex)
    {
      for (int index = 0; index < this.UnitImages2_main_Images.Length; ++index)
        this.UnitImages2_main_Images[index].set_texture((Texture) u2_main_tex);
      for (int index = 0; index < this.UnitImages2_sub_Images.Length; ++index)
        this.UnitImages2_sub_Images[index].set_texture((Texture) u2_sub_tex);
      for (int index = 0; index < this.UnitEyeImages_main_Images.Length; ++index)
        this.UnitEyeImages_main_Images[index].set_texture((Texture) ue_main_tex);
      for (int index = 0; index < this.UnitEyeImages_sub_Images.Length; ++index)
        this.UnitEyeImages_sub_Images[index].set_texture((Texture) ue_sub_tex);
    }

    public void Close()
    {
      this.mClose = true;
    }

    private void Start()
    {
      this.mAnimator = (Animator) ((Component) this).GetComponent<Animator>();
    }

    private void Update()
    {
      if (this.mClose)
        this.mAnimator.SetTrigger(this.EndTrigger);
      AnimatorStateInfo animatorStateInfo1 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
      // ISSUE: explicit reference operation
      if (!((AnimatorStateInfo) @animatorStateInfo1).IsName(this.EndStateName) || this.mAnimator.IsInTransition(0))
        return;
      AnimatorStateInfo animatorStateInfo2 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
      // ISSUE: explicit reference operation
      if ((double) ((AnimatorStateInfo) @animatorStateInfo2).get_normalizedTime() < 1.0)
        return;
      Object.Destroy((Object) ((Component) this).get_gameObject());
    }
  }
}
