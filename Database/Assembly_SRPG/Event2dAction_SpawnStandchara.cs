namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("立ち絵/配置(2D)", "立ち絵を配置します", 0x555555, 0x444488)]
    public class Event2dAction_SpawnStandchara : EventAction
    {
        public string CharaID;
        public bool Flip;
        public EventStandChara.PositionTypes Position;
        [HideInInspector]
        public Texture2D StandcharaImage;
        [HideInInspector]
        public EventStandChara mStandChara;
        [HideInInspector]
        public IEnumerator mEnumerator;
        private LoadRequest mStandCharaResource;
        private static readonly string AssetPath;

        static Event2dAction_SpawnStandchara()
        {
            AssetPath = "Event2dAssets/EventStandChara";
            return;
        }

        public Event2dAction_SpawnStandchara()
        {
            base..ctor();
            return;
        }

        public override unsafe void OnActivate()
        {
            RectTransform transform;
            Vector3 vector;
            Vector3 vector2;
            Color color;
            Color color2;
            Color color3;
            if ((this.mStandChara != null) == null)
            {
                goto Label_0037;
            }
            if (this.mStandChara.get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_0037;
            }
            this.mStandChara.get_gameObject().SetActive(1);
        Label_0037:
            if ((this.mStandChara != null) == null)
            {
                goto Label_019A;
            }
            this.mStandChara.get_gameObject().GetComponent<RawImage>().set_color(new Color(&Color.get_gray().r, &Color.get_gray().g, &Color.get_gray().b, 0f));
            this.mStandChara.get_gameObject().GetComponent<RawImage>().set_texture(this.StandcharaImage);
            transform = this.mStandChara.GetComponent<RectTransform>();
            if ((this.mStandChara.get_transform().get_parent() == null) == null)
            {
                goto Label_0165;
            }
            &vector..ctor(this.mStandChara.GetPositionX(this.Position), 0f, 0f);
            transform.set_position(vector);
            &vector2..ctor(1f, 1f, 1f);
            transform.set_localScale(vector2);
            this.mStandChara.get_transform().SetParent(base.ActiveCanvas.get_transform(), 0);
            this.mStandChara.get_transform().SetAsFirstSibling();
            this.mStandChara.get_transform().SetSiblingIndex(EventDialogBubbleCustom.FindHead().get_transform().GetSiblingIndex() - 1);
        Label_0165:
            if (this.Flip == null)
            {
                goto Label_018A;
            }
            transform.Rotate(new Vector3(0f, 180f, 0f));
        Label_018A:
            this.mStandChara.Open(0.3f);
        Label_019A:
            return;
        }

        protected override void OnDestroy()
        {
            if ((this.mStandChara != null) == null)
            {
                goto Label_0021;
            }
            Object.Destroy(this.mStandChara.get_gameObject());
        Label_0021:
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__IteratorA8 ra;
            ra = new <PreloadAssets>c__IteratorA8();
            ra.<>f__this = this;
            return ra;
        }

        public override unsafe void PreStart()
        {
            RectTransform transform;
            RectTransform transform2;
            Vector3 vector;
            if ((this.mStandChara == null) == null)
            {
                goto Label_00EE;
            }
            this.mStandChara = EventStandChara.Find(this.CharaID);
            if ((this.mStandChara == null) == null)
            {
                goto Label_00EE;
            }
            this.mStandChara = Object.Instantiate(this.mStandCharaResource.asset) as EventStandChara;
            transform = this.mStandChara.GetComponent<RectTransform>();
            transform2 = base.ActiveCanvas.get_transform() as RectTransform;
            this.mStandChara.InitPositionX(transform2);
            &vector..ctor(this.mStandChara.GetPositionX(this.Position), 0f, 0f);
            transform.set_position(vector);
            this.mStandChara.get_transform().SetParent(base.ActiveCanvas.get_transform(), 0);
            this.mStandChara.get_transform().SetAsFirstSibling();
            this.mStandChara.CharaID = this.CharaID;
            this.mStandChara.get_gameObject().SetActive(0);
        Label_00EE:
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
            if (this.mStandChara.Fading != null)
            {
                goto Label_0040;
            }
            if (this.mStandChara.State != null)
            {
                goto Label_0040;
            }
            this.mStandChara.State = 1;
            base.ActivateNext(1);
            return;
        Label_0040:
            if (this.mStandChara.Fading != null)
            {
                goto Label_0086;
            }
            if (this.mStandChara.State != 2)
            {
                goto Label_0086;
            }
            this.mStandChara.State = 3;
            base.enabled = 0;
            this.mStandChara.get_gameObject().SetActive(0);
            return;
        Label_0086:
            return;
        }

        public override bool IsPreloadAssets
        {
            get
            {
                return 1;
            }
        }

        [CompilerGenerated]
        private sealed class <PreloadAssets>c__IteratorA8 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal Event2dAction_SpawnStandchara <>f__this;

            public <PreloadAssets>c__IteratorA8()
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
                        goto Label_0058;
                }
                goto Label_0086;
            Label_0021:
                this.<>f__this.mStandCharaResource = AssetManager.LoadAsync<EventStandChara>(Event2dAction_SpawnStandchara.AssetPath);
                this.$current = this.<>f__this.mStandCharaResource.StartCoroutine();
                this.$PC = 1;
                goto Label_0088;
            Label_0058:
                if ((this.<>f__this.mStandCharaResource.asset == null) == null)
                {
                    goto Label_007F;
                }
                this.<>f__this.mStandCharaResource = null;
            Label_007F:
                this.$PC = -1;
            Label_0086:
                return 0;
            Label_0088:
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

        public enum StateTypes
        {
            Initialized,
            StartFadeIn,
            FadeIn,
            EndFadeIn,
            StartFadeOut,
            FadeOut,
            EndFadeOut,
            Active,
            Inactive
        }
    }
}

