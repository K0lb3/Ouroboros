namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("New/立ち絵2/シェイク", "立ち絵2を揺らします", 0x555555, 0x444488)]
    public class Event2dAction_StandShake : EventAction
    {
        public string CharaID;
        public float Duration;
        public float FrequencyX;
        public float FrequencyY;
        public float AmplitudeX;
        public float AmplitudeY;
        public bool Async;
        private EventStandCharaController2 mStandChara;
        private RectTransform mStandCharaTransform;
        private float mSeedX;
        private float mSeedY;
        private float mTime;
        private Vector2 originalPvt;

        public Event2dAction_StandShake()
        {
            this.Duration = 0.5f;
            this.FrequencyX = 12.51327f;
            this.FrequencyY = 20.4651f;
            this.AmplitudeX = 0.1f;
            this.AmplitudeY = 0.1f;
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            if ((this.mStandChara == null) == null)
            {
                goto Label_0018;
            }
            base.ActivateNext();
            return;
        Label_0018:
            this.mSeedX = Random.get_value();
            this.mSeedY = Random.get_value();
            this.mStandCharaTransform = this.mStandChara.GetComponent<RectTransform>();
            this.originalPvt = this.mStandCharaTransform.get_pivot();
            if (this.Async == null)
            {
                goto Label_0062;
            }
            base.ActivateNext(1);
        Label_0062:
            return;
        }

        public override void PreStart()
        {
            if ((this.mStandChara == null) == null)
            {
                goto Label_0022;
            }
            this.mStandChara = EventStandCharaController2.FindInstances(this.CharaID);
        Label_0022:
            return;
        }

        public override unsafe void Update()
        {
            float num;
            float num2;
            float num3;
            float num4;
            Vector2 vector;
            this.mTime += Time.get_deltaTime();
            if (this.mTime < this.Duration)
            {
                goto Label_0052;
            }
            this.mStandCharaTransform.set_pivot(this.originalPvt);
            if (this.Async == null)
            {
                goto Label_004B;
            }
            base.enabled = 0;
            goto Label_0051;
        Label_004B:
            base.ActivateNext();
        Label_0051:
            return;
        Label_0052:
            num = Mathf.Clamp01(this.mTime / this.Duration);
            num2 = 1f - num;
            num3 = (Mathf.Sin(((Time.get_time() + this.mSeedX) * this.FrequencyX) * 3.141593f) * this.AmplitudeX) * num2;
            num4 = (Mathf.Sin(((Time.get_time() + this.mSeedY) * this.FrequencyY) * 3.141593f) * this.AmplitudeY) * num2;
            &vector..ctor(&this.originalPvt.x + num3, &this.originalPvt.y + num4);
            this.mStandCharaTransform.set_pivot(vector);
            return;
        }
    }
}

