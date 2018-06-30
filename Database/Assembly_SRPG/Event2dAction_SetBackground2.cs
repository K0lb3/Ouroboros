namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/背景/配置(2D)", "背景を配置します", 0x555555, 0x444488)]
    public class Event2dAction_SetBackground2 : EventAction
    {
        private const string SHADER_NAME = "UI/Custom/EventDefault";
        [HideInInspector]
        public Texture2D Background;
        [HideInInspector]
        public EventBackGround mBackGround;
        [HideInInspector]
        public bool NewMaterial;
        private LoadRequest mBackGroundResource;
        private static readonly string AssetPath;

        static Event2dAction_SetBackground2()
        {
            AssetPath = "Event2dAssets/EventBackGround";
            return;
        }

        public Event2dAction_SetBackground2()
        {
            this.NewMaterial = 1;
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            if (((this.mBackGround != null) == null) || (this.mBackGround.get_gameObject().get_activeInHierarchy() != null))
            {
                goto Label_0037;
            }
            this.mBackGround.get_gameObject().SetActive(1);
        Label_0037:
            if (((this.mBackGround != null) == null) && ((this.mBackGround.get_gameObject().GetComponent<RawImage>().get_texture() != this.Background) == null))
            {
                goto Label_0088;
            }
            this.mBackGround.get_gameObject().GetComponent<RawImage>().set_texture(this.Background);
        Label_0088:
            if ((this.mBackGround != null) == null)
            {
                goto Label_00CE;
            }
            this.mBackGround.get_gameObject().GetComponent<RawImage>().set_material((this.NewMaterial == null) ? null : new Material(Shader.Find("UI/Custom/EventDefault")));
        Label_00CE:
            base.ActivateNext();
            return;
        }

        protected override void OnDestroy()
        {
            if ((this.mBackGround != null) == null)
            {
                goto Label_0021;
            }
            Object.Destroy(this.mBackGround.get_gameObject());
        Label_0021:
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__IteratorA4 ra;
            ra = new <PreloadAssets>c__IteratorA4();
            ra.<>f__this = this;
            return ra;
        }

        public override void PreStart()
        {
            if (this.NewMaterial == null)
            {
                goto Label_001F;
            }
            Shader.DisableKeyword("EVENT_MONOCHROME_ON");
            Shader.DisableKeyword("EVENT_SEPIA_ON");
        Label_001F:
            if ((this.mBackGround == null) == null)
            {
                goto Label_00AF;
            }
            this.mBackGround = EventBackGround.Find();
            if ((this.mBackGround == null) == null)
            {
                goto Label_00AF;
            }
            if (this.mBackGroundResource == null)
            {
                goto Label_00AF;
            }
            this.mBackGround = Object.Instantiate(this.mBackGroundResource.asset) as EventBackGround;
            this.mBackGround.get_transform().SetParent(base.ActiveCanvas.get_transform(), 0);
            this.mBackGround.get_transform().SetAsFirstSibling();
            this.mBackGround.get_gameObject().SetActive(0);
        Label_00AF:
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
        private sealed class <PreloadAssets>c__IteratorA4 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal Event2dAction_SetBackground2 <>f__this;

            public <PreloadAssets>c__IteratorA4()
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
                this.<>f__this.mBackGroundResource = AssetManager.LoadAsync<EventBackGround>(Event2dAction_SetBackground2.AssetPath);
                this.$current = this.<>f__this.mBackGroundResource.StartCoroutine();
                this.$PC = 1;
                goto Label_0088;
            Label_0058:
                if ((this.<>f__this.mBackGroundResource.asset == null) == null)
                {
                    goto Label_007F;
                }
                this.<>f__this.mBackGroundResource = null;
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
    }
}

