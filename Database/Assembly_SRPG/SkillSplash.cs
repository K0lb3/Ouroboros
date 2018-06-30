namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Animator))]
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
            this.EndTrigger = "end";
            this.EndStateName = "finished";
            base..ctor();
            return;
        }

        public void Close()
        {
            this.mClose = 1;
            return;
        }

        public void SetCharaImages(Texture2D newTexture1, Texture2D newTexture2)
        {
            int num;
            int num2;
            if ((newTexture1 != null) == null)
            {
                goto Label_0033;
            }
            num = 0;
            goto Label_0025;
        Label_0013:
            this.Chara_01_Images[num].set_texture(newTexture1);
            num += 1;
        Label_0025:
            if (num < ((int) this.Chara_01_Images.Length))
            {
                goto Label_0013;
            }
        Label_0033:
            if ((newTexture2 != null) == null)
            {
                goto Label_0066;
            }
            num2 = 0;
            goto Label_0058;
        Label_0046:
            this.Chara_02_Images[num2].set_texture(newTexture2);
            num2 += 1;
        Label_0058:
            if (num2 < ((int) this.Chara_02_Images.Length))
            {
                goto Label_0046;
            }
        Label_0066:
            return;
        }

        private void Start()
        {
            this.mAnimator = base.GetComponent<Animator>();
            if (this.NoLoop == null)
            {
                goto Label_001D;
            }
            this.Close();
        Label_001D:
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

