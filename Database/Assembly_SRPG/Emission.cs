namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class Emission : MonoBehaviour
    {
        public UnityEngine.AnimationCurve AnimationCurve;
        public float Delay;
        public float Duration;
        public UnityEngine.UI.Image Image;
        private float m_Factor;
        private float m_Time;

        public unsafe Emission()
        {
            Keyframe[] keyframeArray1;
            keyframeArray1 = new Keyframe[2];
            *(&(keyframeArray1[0])) = new Keyframe(0f, 0f, 0f, 1f);
            *(&(keyframeArray1[1])) = new Keyframe(1f, 1f, 1f, 0f);
            this.AnimationCurve = new UnityEngine.AnimationCurve(keyframeArray1);
            base..ctor();
            return;
        }

        private unsafe void Update()
        {
            float num;
            float num2;
            float num3;
            Color color;
            num = Time.get_deltaTime();
            this.m_Time += num;
            if (this.m_Time >= this.Delay)
            {
                goto Label_0031;
            }
            this.m_Factor = 0f;
            return;
        Label_0031:
            this.m_Factor += num;
            num2 = (this.Duration > 0f) ? (this.m_Factor / this.Duration) : 1f;
            if (num2 < 1f)
            {
                goto Label_007D;
            }
            this.m_Factor = 0f;
        Label_007D:
            num3 = this.AnimationCurve.Evaluate(Mathf.Clamp01(num2));
            if ((this.Image != null) == null)
            {
                goto Label_00CC;
            }
            color = this.Image.get_color();
            &color.a = num3;
            this.Image.set_color(color);
            this.Image.set_enabled(1);
        Label_00CC:
            return;
        }
    }
}

