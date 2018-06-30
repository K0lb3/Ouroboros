namespace SRPG
{
    using System;
    using UnityEngine;

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
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((sInstance != null) == null)
            {
                goto Label_0016;
            }
            Object.Destroy(this);
        Label_0016:
            sInstance = this;
            this.mAnimator = base.GetComponent<Animator>();
            this.mCanvasGroup = base.GetComponent<CanvasGroup>();
            return;
        }

        public void FadeIn()
        {
            if (this.mFading == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mAnimator.Play("FadeIn");
            this.mStateName = "FadeInFinish";
            this.mFading = 1;
            return;
        }

        public void FadeOut()
        {
            if (this.mFading == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mCanvasGroup.set_blocksRaycasts(1);
            this.mAnimator.Play("FadeOut");
            this.mStateName = "FadeOutFinish";
            this.mFading = 1;
            return;
        }

        private void OnDestroy()
        {
            if ((sInstance == this) == null)
            {
                goto Label_0016;
            }
            sInstance = null;
        Label_0016:
            return;
        }

        private unsafe void Update()
        {
            AnimatorStateInfo info;
            if (this.mFading != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (&this.mAnimator.GetCurrentAnimatorStateInfo(0).IsName(this.mStateName) == null)
            {
                goto Label_0053;
            }
            if ((this.mStateName == "FadeInFinish") == null)
            {
                goto Label_004C;
            }
            this.mCanvasGroup.set_blocksRaycasts(0);
        Label_004C:
            this.mFading = 0;
        Label_0053:
            return;
        }

        public static MultiPlayVersusGradientFade Instance
        {
            get
            {
                return sInstance;
            }
        }

        public bool Fading
        {
            get
            {
                return this.mFading;
            }
        }
    }
}

