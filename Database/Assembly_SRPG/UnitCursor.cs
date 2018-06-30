namespace SRPG
{
    using System;
    using UnityEngine;

    public class UnitCursor : MonoBehaviour
    {
        public float StartScale;
        public float LoopScale;
        public float EndScale;
        public float Opacity;
        public float FadeTime;
        private float mCurrentScale;
        private float mTime;
        private float mDuration;
        private float mDesiredScale;
        private float mStartScale;
        private bool mDiscard;
        private UnityEngine.Color mColor;
        private float mCurrentOpacity;
        private float mStartOpacity;
        private float mDesiredOpacity;

        public UnitCursor()
        {
            this.StartScale = 2f;
            this.LoopScale = 1f;
            this.EndScale = 2f;
            this.Opacity = 1f;
            this.FadeTime = 1f;
            base..ctor();
            return;
        }

        private void AnimateScale(float endScale, float opacity, float time, bool destroyLater)
        {
            this.mTime = 0f;
            this.mDuration = time;
            this.mStartScale = this.mCurrentScale;
            this.mDesiredScale = endScale;
            this.mDiscard = destroyLater;
            this.mStartOpacity = this.mCurrentOpacity;
            this.mDesiredOpacity = opacity;
            return;
        }

        public void FadeOut()
        {
            this.AnimateScale(this.EndScale, 0f, this.FadeTime, 1);
            return;
        }

        private void Start()
        {
            this.mCurrentScale = this.StartScale;
            this.AnimateScale(this.LoopScale, this.Opacity, this.FadeTime, 0);
            this.Update();
            return;
        }

        private unsafe void Update()
        {
            float num;
            float num2;
            UnityEngine.Color color;
            if (this.mTime > this.mDuration)
            {
                goto Label_00CA;
            }
            this.mTime += Time.get_deltaTime();
            num2 = Mathf.Sin((Mathf.Clamp01(this.mTime / this.mDuration) * 3.141593f) * 0.5f);
            this.mCurrentScale = Mathf.Lerp(this.mStartScale, this.mDesiredScale, num2);
            this.mCurrentOpacity = Mathf.Lerp(this.mStartOpacity, this.mDesiredOpacity, num2);
            color = this.mColor;
            &color.a *= this.mCurrentOpacity;
            base.get_transform().set_localScale(Vector3.get_one() * this.mCurrentScale);
            base.GetComponent<Renderer>().get_material().SetColor("_color", color);
            goto Label_00E1;
        Label_00CA:
            if (this.mDiscard == null)
            {
                goto Label_00E1;
            }
            Object.DestroyImmediate(base.get_gameObject());
            return;
        Label_00E1:
            return;
        }

        public UnityEngine.Color Color
        {
            set
            {
                this.mColor = value;
                return;
            }
        }
    }
}

