namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Animator))]
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
            this.EndTrigger = "end";
            this.EndStateName = "finished";
            this.mClose = 1;
            base..ctor();
            return;
        }

        public void Close()
        {
            this.mClose = 1;
            return;
        }

        public void SetCharaImages(Texture2D u2_main_tex, Texture2D u2_sub_tex, Texture2D ue_main_tex, Texture2D ue_sub_tex)
        {
            int num;
            int num2;
            int num3;
            int num4;
            num = 0;
            goto Label_0019;
        Label_0007:
            this.UnitImages2_main_Images[num].set_texture(u2_main_tex);
            num += 1;
        Label_0019:
            if (num < ((int) this.UnitImages2_main_Images.Length))
            {
                goto Label_0007;
            }
            num2 = 0;
            goto Label_0040;
        Label_002E:
            this.UnitImages2_sub_Images[num2].set_texture(u2_sub_tex);
            num2 += 1;
        Label_0040:
            if (num2 < ((int) this.UnitImages2_sub_Images.Length))
            {
                goto Label_002E;
            }
            num3 = 0;
            goto Label_0067;
        Label_0055:
            this.UnitEyeImages_main_Images[num3].set_texture(ue_main_tex);
            num3 += 1;
        Label_0067:
            if (num3 < ((int) this.UnitEyeImages_main_Images.Length))
            {
                goto Label_0055;
            }
            num4 = 0;
            goto Label_008F;
        Label_007C:
            this.UnitEyeImages_sub_Images[num4].set_texture(ue_sub_tex);
            num4 += 1;
        Label_008F:
            if (num4 < ((int) this.UnitEyeImages_sub_Images.Length))
            {
                goto Label_007C;
            }
            return;
        }

        private void Start()
        {
            this.mAnimator = base.GetComponent<Animator>();
            return;
        }

        private unsafe void Update()
        {
            AnimatorStateInfo info;
            AnimatorStateInfo info2;
            if (this.mClose == null)
            {
                goto Label_001C;
            }
            this.mAnimator.SetTrigger(this.EndTrigger);
        Label_001C:
            if (&this.mAnimator.GetCurrentAnimatorStateInfo(0).IsName(this.EndStateName) == null)
            {
                goto Label_0076;
            }
            if (this.mAnimator.IsInTransition(0) != null)
            {
                goto Label_0076;
            }
            if (&this.mAnimator.GetCurrentAnimatorStateInfo(0).get_normalizedTime() < 1f)
            {
                goto Label_0076;
            }
            Object.Destroy(base.get_gameObject());
            return;
        Label_0076:
            return;
        }
    }
}

