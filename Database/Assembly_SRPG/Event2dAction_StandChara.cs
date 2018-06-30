namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("立ち絵2/配置(2D)", "立ち絵2を配置します", 0x555555, 0x444488)]
    public class Event2dAction_StandChara : EventAction
    {
        private static readonly string AssetPath;
        public string CharaID;
        public bool Flip;
        public EventStandCharaController2.PositionTypes Position;
        public GameObject StandTemplate;
        public string Emotion;
        private string DummyID;
        private GameObject mStandObject;
        private EventStandChara2 mEventStandChara;
        private EventStandCharaController2 mEVCharaController;
        private LoadRequest mStandCharaResource;

        static Event2dAction_StandChara()
        {
            AssetPath = "Event2dAssets/Event2dStand";
            return;
        }

        public Event2dAction_StandChara()
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
                goto Label_0126;
            }
            this.mEVCharaController.Emotion = this.Emotion;
            transform = this.mStandObject.GetComponent<RectTransform>();
            if ((transform.get_transform().get_parent() == null) == null)
            {
                goto Label_00F1;
            }
            &vector..ctor(this.mEVCharaController.GetAnchorPostionX(this.Position), 0f);
            vector3 = vector;
            transform.set_anchorMax(vector3);
            transform.set_anchorMin(vector3);
            &vector2..ctor(1f, 1f, 1f);
            transform.set_localScale(vector2);
            this.mStandObject.get_transform().SetParent(base.ActiveCanvas.get_transform(), 0);
            this.mStandObject.get_transform().SetAsLastSibling();
        Label_00F1:
            if (this.Flip == null)
            {
                goto Label_0116;
            }
            transform.Rotate(new Vector3(0f, 180f, 0f));
        Label_0116:
            this.mEVCharaController.Open(0.5f);
        Label_0126:
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
            <PreloadAssets>c__IteratorA9 ra;
            ra = new <PreloadAssets>c__IteratorA9();
            return ra;
        }

        public override unsafe void PreStart()
        {
            string str;
            RectTransform transform;
            Vector2 vector;
            Vector2 vector2;
            if ((this.mStandObject == null) == null)
            {
                goto Label_0128;
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
                goto Label_0128;
            }
            if ((this.StandTemplate != null) == null)
            {
                goto Label_0128;
            }
            this.mStandObject = Object.Instantiate<GameObject>(this.StandTemplate);
            transform = this.mStandObject.GetComponent<RectTransform>();
            this.mEVCharaController = this.mStandObject.GetComponent<EventStandCharaController2>();
            &vector..ctor(this.mEVCharaController.GetAnchorPostionX(this.Position), 0f);
            vector2 = vector;
            transform.set_anchorMax(vector2);
            transform.set_anchorMin(vector2);
            this.mStandObject.get_transform().SetParent(base.ActiveCanvas.get_transform(), 0);
            this.mStandObject.get_transform().SetAsLastSibling();
            this.mEVCharaController.CharaID = this.CharaID;
            this.mStandObject.get_gameObject().SetActive(0);
        Label_0128:
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
                goto Label_0043;
            }
            if (this.mEVCharaController.State != null)
            {
                goto Label_0043;
            }
            this.mEVCharaController.State = 1;
            base.ActivateNext();
            goto Label_00B4;
        Label_0043:
            if (this.mEVCharaController.Fading != null)
            {
                goto Label_009E;
            }
            if (this.mEVCharaController.State != 2)
            {
                goto Label_009E;
            }
            this.mEVCharaController.State = 3;
            base.enabled = 0;
            this.mEVCharaController.get_gameObject().SetActive(0);
            this.mEVCharaController.get_transform().set_parent(null);
            goto Label_00B4;
        Label_009E:
            if (this.mEVCharaController.Fading != null)
            {
                goto Label_00B4;
            }
            base.ActivateNext();
        Label_00B4:
            return;
        }

        [CompilerGenerated]
        private sealed class <PreloadAssets>c__IteratorA9 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;

            public <PreloadAssets>c__IteratorA9()
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

