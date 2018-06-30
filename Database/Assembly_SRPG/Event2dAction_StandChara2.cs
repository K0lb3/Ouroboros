namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/立ち絵2/配置2(2D)", "立ち絵2を配置します", 0x555555, 0x444488)]
    public class Event2dAction_StandChara2 : EventAction
    {
        private static readonly string AssetPath;
        public string CharaID;
        public bool Flip;
        public EventStandCharaController2.PositionTypes Position;
        public GameObject StandTemplate;
        public string Emotion;
        public bool Async;
        public bool Fade;
        [HideInInspector]
        public float FadeTime;
        private string DummyID;
        private GameObject mStandObject;
        private EventStandChara2 mEventStandChara;
        private EventStandCharaController2 mEVCharaController;
        private LoadRequest mStandCharaResource;

        static Event2dAction_StandChara2()
        {
            AssetPath = "Event2dAssets/Event2dStand";
            return;
        }

        public Event2dAction_StandChara2()
        {
            this.DummyID = "dummyID";
            base..ctor();
            return;
        }

        public override unsafe void OnActivate()
        {
            RectTransform transform;
            Vector2 vector;
            Vector3 vector2;
            Vector2 vector3;
            if ((this.mStandObject != null) == null)
            {
                goto Label_0037;
            }
            if (this.mStandObject.get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_0037;
            }
            this.mStandObject.get_gameObject().SetActive(1);
        Label_0037:
            if ((this.mStandObject != null) == null)
            {
                goto Label_0147;
            }
            this.mEVCharaController.Emotion = this.Emotion;
            transform = this.mStandObject.GetComponent<RectTransform>();
            &vector..ctor(this.mEVCharaController.GetAnchorPostionX(this.Position), 0f);
            vector3 = vector;
            transform.set_anchorMax(vector3);
            transform.set_anchorMin(vector3);
            &vector2..ctor(1f, 1f, 1f);
            transform.set_localScale(vector2);
            if ((transform.get_transform().get_parent() == null) == null)
            {
                goto Label_00F1;
            }
            this.mStandObject.get_transform().SetParent(base.ActiveCanvas.get_transform(), 0);
            this.mStandObject.get_transform().SetAsLastSibling();
        Label_00F1:
            if (this.Flip == null)
            {
                goto Label_0116;
            }
            transform.Rotate(new Vector3(0f, 180f, 0f));
        Label_0116:
            if (this.Fade == null)
            {
                goto Label_0137;
            }
            this.mEVCharaController.Open(this.FadeTime);
            goto Label_0147;
        Label_0137:
            this.mEVCharaController.Open(0f);
        Label_0147:
            if (this.Async == null)
            {
                goto Label_0159;
            }
            base.ActivateNext(1);
        Label_0159:
            return;
        }

        protected override void OnDestroy()
        {
            if ((this.mStandObject != null) == null)
            {
                goto Label_0021;
            }
            Object.Destroy(this.mStandObject.get_gameObject());
        Label_0021:
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__IteratorAA raa;
            raa = new <PreloadAssets>c__IteratorAA();
            return raa;
        }

        public override unsafe void PreStart()
        {
            string str;
            RectTransform transform;
            Vector2 vector;
            Vector2 vector2;
            if ((this.mStandObject == null) == null)
            {
                goto Label_0139;
            }
            str = this.DummyID;
            if (string.IsNullOrEmpty(this.CharaID) != null)
            {
                goto Label_002F;
            }
            str = this.CharaID;
        Label_002F:
            if ((EventStandCharaController2.FindInstances(str) != null) == null)
            {
                goto Label_005D;
            }
            this.mEVCharaController = EventStandCharaController2.FindInstances(str);
            this.mStandObject = this.mEVCharaController.get_gameObject();
        Label_005D:
            if ((this.mStandObject == null) == null)
            {
                goto Label_00B2;
            }
            if ((this.StandTemplate != null) == null)
            {
                goto Label_00B2;
            }
            this.mStandObject = Object.Instantiate<GameObject>(this.StandTemplate);
            this.mEVCharaController = this.mStandObject.GetComponent<EventStandCharaController2>();
            this.mEVCharaController.CharaID = this.CharaID;
        Label_00B2:
            if ((this.mStandObject != null) == null)
            {
                goto Label_0139;
            }
            this.mStandObject.get_transform().SetParent(base.ActiveCanvas.get_transform(), 0);
            this.mStandObject.get_transform().SetAsLastSibling();
            this.mStandObject.get_gameObject().SetActive(0);
            transform = this.mStandObject.GetComponent<RectTransform>();
            &vector..ctor(this.mEVCharaController.GetAnchorPostionX(this.Position), 0f);
            vector2 = vector;
            transform.set_anchorMax(vector2);
            transform.set_anchorMin(vector2);
        Label_0139:
            return;
        }

        public override void Update()
        {
            if (base.enabled != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mEVCharaController.Fading != null)
            {
                goto Label_005A;
            }
            if (this.mEVCharaController.State != null)
            {
                goto Label_005A;
            }
            this.mEVCharaController.State = 1;
            if (this.Async != null)
            {
                goto Label_004E;
            }
            base.ActivateNext();
            goto Label_0055;
        Label_004E:
            base.enabled = 0;
        Label_0055:
            goto Label_00E2;
        Label_005A:
            if (this.mEVCharaController.Fading != null)
            {
                goto Label_00B5;
            }
            if (this.mEVCharaController.State != 2)
            {
                goto Label_00B5;
            }
            this.mEVCharaController.State = 3;
            base.enabled = 0;
            this.mEVCharaController.get_gameObject().SetActive(0);
            this.mEVCharaController.get_transform().set_parent(null);
            goto Label_00E2;
        Label_00B5:
            if (this.mEVCharaController.Fading != null)
            {
                goto Label_00E2;
            }
            if (this.Async != null)
            {
                goto Label_00DB;
            }
            base.ActivateNext();
            goto Label_00E2;
        Label_00DB:
            base.enabled = 0;
        Label_00E2:
            return;
        }

        [CompilerGenerated]
        private sealed class <PreloadAssets>c__IteratorAA : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;

            public <PreloadAssets>c__IteratorAA()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0034;
                }
                goto Label_003B;
            Label_0021:
                this.$current = null;
                this.$PC = 1;
                goto Label_003D;
            Label_0034:
                this.$PC = -1;
            Label_003B:
                return 0;
            Label_003D:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }
    }
}

