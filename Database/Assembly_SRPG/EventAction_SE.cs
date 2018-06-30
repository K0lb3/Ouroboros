namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [EventActionInfo("New/SE再生", "SEを再生します", 0x445555, 0x448888)]
    public class EventAction_SE : EventAction
    {
        public string m_CueName;
        public bool m_Async;
        public float m_Delay;
        [HideInInspector]
        public float m_Wait;
        private bool m_bPlay;

        public EventAction_SE()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            if (this.m_Async == null)
            {
                goto Label_004E;
            }
            if (this.m_Delay > 0f)
            {
                goto Label_0042;
            }
            MonoSingleton<MySound>.Instance.PlaySEOneShot(this.m_CueName, 0f);
            this.m_bPlay = 1;
            base.ActivateNext();
            goto Label_0049;
        Label_0042:
            base.ActivateNext(1);
        Label_0049:
            goto Label_0090;
        Label_004E:
            if (this.m_Delay > 0f)
            {
                goto Label_0090;
            }
            MonoSingleton<MySound>.Instance.PlaySEOneShot(this.m_CueName, 0f);
            this.m_bPlay = 1;
            if (this.m_Wait > 0f)
            {
                goto Label_0090;
            }
            base.ActivateNext();
        Label_0090:
            return;
        }

        public override void Update()
        {
            this.m_Delay -= Time.get_deltaTime();
            if (this.m_bPlay == null)
            {
                goto Label_004A;
            }
            this.m_Wait -= Time.get_deltaTime();
            if (this.m_Wait > 0f)
            {
                goto Label_00A3;
            }
            base.ActivateNext();
            goto Label_00A3;
        Label_004A:
            if (this.m_Delay >= 0f)
            {
                goto Label_00A3;
            }
            MonoSingleton<MySound>.Instance.PlaySEOneShot(this.m_CueName, 0f);
            this.m_bPlay = 1;
            if (this.m_Async == null)
            {
                goto Label_008D;
            }
            base.enabled = 0;
            goto Label_00A3;
        Label_008D:
            if (this.m_Wait > 0f)
            {
                goto Label_00A3;
            }
            base.ActivateNext();
        Label_00A3:
            return;
        }
    }
}

