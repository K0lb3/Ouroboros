namespace SRPG
{
    using System;
    using UnityEngine;

    public class FlashEffect : MonoBehaviour
    {
        private RenderPipeline mTarget;
        public float Strength;
        public float Duration;
        private float mTime;

        public FlashEffect()
        {
            this.Strength = 1f;
            this.Duration = 0.3f;
            base..ctor();
            return;
        }

        private void OnDestroy()
        {
            if ((this.mTarget != null) == null)
            {
                goto Label_001D;
            }
            this.mTarget.SwapEffect = 0;
        Label_001D:
            return;
        }

        private void Start()
        {
            this.mTarget = base.GetComponent<RenderPipeline>();
            if ((this.mTarget == null) == null)
            {
                goto Label_0024;
            }
            Object.Destroy(this);
            return;
        Label_0024:
            return;
        }

        private void Update()
        {
            float num;
            this.mTime += Time.get_deltaTime();
            num = Mathf.Clamp01(this.mTime / this.Duration);
            this.mTarget.SwapEffect = 1;
            this.mTarget.SwapEffectOpacity = (1f - num) * this.Strength;
            if (num < 1f)
            {
                goto Label_005B;
            }
            Object.Destroy(this);
        Label_005B:
            return;
        }
    }
}

