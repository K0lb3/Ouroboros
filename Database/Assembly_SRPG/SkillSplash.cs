// Decompiled with JetBrains decompiler
// Type: SRPG.SkillSplash
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (Animator))]
  public class SkillSplash : MonoBehaviour
  {
    public bool NoLoop;
    [Description("スプラッシュ表示を閉じるのに使用するトリガーの名前")]
    public string EndTrigger;
    [Description("スプラッシュ表示が閉じられた状態のステートの名前、この状態になると先へ進みます")]
    public string EndStateName;
    public RawImage[] Chara_01_Images;
    public RawImage[] Chara_02_Images;
    private Animator mAnimator;
    private bool mClose;

    public SkillSplash()
    {
      base.\u002Ector();
    }

    public void SetCharaImages(Texture2D newTexture1, Texture2D newTexture2)
    {
      for (int index = 0; index < this.Chara_01_Images.Length; ++index)
        this.Chara_01_Images[index].set_texture((Texture) newTexture1);
      for (int index = 0; index < this.Chara_02_Images.Length; ++index)
        this.Chara_02_Images[index].set_texture((Texture) newTexture2);
    }

    public void Close()
    {
      this.mClose = true;
    }

    private void Start()
    {
      this.mAnimator = (Animator) ((Component) this).GetComponent<Animator>();
      if (!this.NoLoop)
        return;
      this.Close();
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
