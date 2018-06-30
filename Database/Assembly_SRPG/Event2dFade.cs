namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class Event2dFade : MonoBehaviour
    {
        public RawImage mImage;
        private Color mCurrentColor;
        private Color mStartColor;
        private Color mEndColor;
        private float mCurrentTime;
        private float mDuration;
        private bool mInitialized;
        [CompilerGenerated]
        private static Event2dFade <Instance>k__BackingField;

        public Event2dFade()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((null != Instance) == null)
            {
                goto Label_0016;
            }
            Object.Destroy(this);
        Label_0016:
            Instance = this;
            return;
        }

        public unsafe void FadeTo(Color dest, float time)
        {
            if (this.mInitialized != null)
            {
                goto Label_0046;
            }
            this.mCurrentColor = dest;
            &this.mCurrentColor.a = 1f - &this.mCurrentColor.a;
            this.mInitialized = 1;
            this.mImage.set_color(this.mCurrentColor);
        Label_0046:
            if (time <= 0f)
            {
                goto Label_0087;
            }
            this.mStartColor = this.mCurrentColor;
            this.mEndColor = dest;
            this.mCurrentTime = 0f;
            this.mDuration = time;
            base.get_gameObject().SetActive(1);
            goto Label_00D2;
        Label_0087:
            this.mCurrentColor = dest;
            this.mCurrentTime = 0f;
            this.mDuration = 0f;
            this.mImage.set_color(this.mCurrentColor);
            base.get_gameObject().SetActive(&this.mCurrentColor.a > 0f);
        Label_00D2:
            return;
        }

        public static Event2dFade Find()
        {
            return Instance;
        }

        private void OnDestroy()
        {
            if ((this == Instance) == null)
            {
                goto Label_0016;
            }
            Instance = null;
        Label_0016:
            return;
        }

        private unsafe void Update()
        {
            float num;
            if (this.mCurrentTime < this.mDuration)
            {
                goto Label_0047;
            }
            if (&this.mCurrentColor.a > 0f)
            {
                goto Label_0095;
            }
            if (GameObjectExtensions.GetActive(base.get_gameObject()) == null)
            {
                goto Label_0095;
            }
            base.get_gameObject().SetActive(0);
            goto Label_0095;
        Label_0047:
            this.mCurrentTime += Time.get_unscaledDeltaTime();
            num = Mathf.Clamp01(this.mCurrentTime / this.mDuration);
            this.mCurrentColor = Color.Lerp(this.mStartColor, this.mEndColor, num);
            this.mImage.set_color(this.mCurrentColor);
        Label_0095:
            return;
        }

        private static Event2dFade Instance
        {
            [CompilerGenerated]
            get
            {
                return <Instance>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                <Instance>k__BackingField = value;
                return;
            }
        }

        public bool IsFading
        {
            get
            {
                return (this.mCurrentTime < this.mDuration);
            }
        }
    }
}

