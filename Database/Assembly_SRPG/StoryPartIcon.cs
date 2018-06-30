namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class StoryPartIcon : MonoBehaviour
    {
        private const string ANIMATION_RELEASE_NAME = "open";
        private const float FADE_TIME = 0.1f;
        [SerializeField]
        private GameObject IconGo;
        [SerializeField]
        private GameObject LockGo;
        [SerializeField]
        private ImageArray LockCover;
        [SerializeField]
        private ImageArray LockCoverPart;
        [SerializeField]
        private ImageArray LockOpen;
        [SerializeField]
        private ImageArray BlurIcon;
        [SerializeField]
        private ImageArray BlurIcon2;
        [SerializeField]
        private Text TxtConditions;
        [SerializeField]
        private GameObject IconMask;
        [SerializeField]
        private GameObject LockMask;
        [SerializeField]
        private GameObject LockTitleMask;
        [SerializeField]
        private CanvasGroup mCanvasGroup;
        private int mStoryNum;
        private bool mLockFlag;
        private float mCountDelat;
        private AlphaState mAlphaState;

        public StoryPartIcon()
        {
            base..ctor();
            return;
        }

        public unsafe bool IsPlayingReleaseAnim()
        {
            bool flag;
            Animator animator;
            AnimatorStateInfo info;
            if (this.mLockFlag != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            flag = 0;
            if (&this.LockGo.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).get_normalizedTime() >= 1f)
            {
                goto Label_0036;
            }
            flag = 1;
        Label_0036:
            return flag;
        }

        public bool PlayReleaseAnim()
        {
            Animator animator;
            if (this.mLockFlag != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            this.LockGo.GetComponent<Animator>().Play("open");
            return 1;
        }

        public void ReleaseIcon()
        {
            this.IconGo.SetActive(1);
            this.IconMask.SetActive(1);
            this.LockGo.SetActive(0);
            this.LockMask.SetActive(0);
            this.LockTitleMask.SetActive(0);
            this.mLockFlag = 0;
            return;
        }

        public void SetMask(bool mask_flag)
        {
            if (mask_flag == null)
            {
                goto Label_0029;
            }
            if (this.mAlphaState == 1)
            {
                goto Label_001D;
            }
            this.mCountDelat = 0f;
        Label_001D:
            this.mAlphaState = 1;
            goto Label_0047;
        Label_0029:
            if (this.mAlphaState == 2)
            {
                goto Label_0040;
            }
            this.mCountDelat = 0f;
        Label_0040:
            this.mAlphaState = 2;
        Label_0047:
            return;
        }

        public bool Setup(bool lock_flag, int story_part)
        {
            ImageArray array;
            int num;
            string str;
            string str2;
            this.mStoryNum = 1;
            this.mLockFlag = 0;
            this.mAlphaState = 0;
            if ((this.IconGo == null) != null)
            {
                goto Label_008C;
            }
            if ((this.LockGo == null) != null)
            {
                goto Label_008C;
            }
            if ((this.LockCover == null) != null)
            {
                goto Label_008C;
            }
            if ((this.LockCoverPart == null) != null)
            {
                goto Label_008C;
            }
            if ((this.LockOpen == null) != null)
            {
                goto Label_008C;
            }
            if ((this.BlurIcon == null) != null)
            {
                goto Label_008C;
            }
            if ((this.BlurIcon2 == null) == null)
            {
                goto Label_008E;
            }
        Label_008C:
            return 0;
        Label_008E:
            array = this.IconGo.GetComponent<ImageArray>();
            if ((array == null) == null)
            {
                goto Label_00A8;
            }
            return 0;
        Label_00A8:
            if (story_part <= ((int) array.Images.Length))
            {
                goto Label_00B8;
            }
            return 0;
        Label_00B8:
            num = story_part - 1;
            this.mStoryNum = story_part;
            this.mLockFlag = lock_flag;
            if (lock_flag != null)
            {
                goto Label_0118;
            }
            this.IconGo.SetActive(1);
            this.LockGo.SetActive(0);
            this.IconMask.SetActive(1);
            this.LockMask.SetActive(0);
            this.LockTitleMask.SetActive(0);
            array.ImageIndex = num;
            goto Label_01DC;
        Label_0118:
            array.ImageIndex = num;
            this.IconGo.SetActive(0);
            this.LockGo.SetActive(1);
            this.IconMask.SetActive(0);
            this.LockMask.SetActive(1);
            this.LockTitleMask.SetActive(1);
            this.LockCover.ImageIndex = num;
            this.LockCoverPart.ImageIndex = num;
            this.LockOpen.ImageIndex = num;
            this.BlurIcon.ImageIndex = num;
            this.BlurIcon2.ImageIndex = num;
            if ((this.TxtConditions != null) == null)
            {
                goto Label_01DC;
            }
            str = MonoSingleton<GameManager>.Instance.GetReleaseStoryPartWorldName(this.StoryNum);
            if (str == null)
            {
                goto Label_01DC;
            }
            str2 = LocalizedText.Get("sys.STORYPART_RELEASE_TIMING");
            this.TxtConditions.set_text(string.Format(str2, str));
        Label_01DC:
            this.IconMask.GetComponent<ImageArray>().ImageIndex = num;
            this.LockMask.GetComponent<ImageArray>().ImageIndex = num;
            this.LockTitleMask.GetComponent<ImageArray>().ImageIndex = num;
            return 1;
        }

        private void Start()
        {
        }

        private void Update()
        {
            AlphaState state;
            if ((this.mCanvasGroup == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            state = this.mAlphaState;
            if (state == 1)
            {
                goto Label_0085;
            }
            if (state == 2)
            {
                goto Label_002C;
            }
            goto Label_00D8;
        Label_002C:
            this.mCountDelat += Time.get_deltaTime();
            if (this.mCountDelat <= 0.1f)
            {
                goto Label_0063;
            }
            this.mCanvasGroup.set_alpha(0f);
            goto Label_0080;
        Label_0063:
            this.mCanvasGroup.set_alpha(1f - (this.mCountDelat / 0.1f));
        Label_0080:
            goto Label_00D8;
        Label_0085:
            this.mCountDelat += Time.get_deltaTime();
            if (this.mCountDelat <= 0.1f)
            {
                goto Label_00BC;
            }
            this.mCanvasGroup.set_alpha(1f);
            goto Label_00D3;
        Label_00BC:
            this.mCanvasGroup.set_alpha(this.mCountDelat / 0.1f);
        Label_00D3:;
        Label_00D8:
            return;
        }

        public int StoryNum
        {
            get
            {
                return this.mStoryNum;
            }
        }

        public bool LockFlag
        {
            get
            {
                return this.mLockFlag;
            }
        }

        private enum AlphaState
        {
            None,
            Fadeout,
            Fadein
        }
    }
}

