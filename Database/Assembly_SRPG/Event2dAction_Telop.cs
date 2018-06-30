namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("会話/テロップ(2D)", "会話の文章を表示し、プレイヤーの入力を待ちます。", 0x555555, 0x444488)]
    public class Event2dAction_Telop : EventAction
    {
        private const float DialogPadding = 20f;
        [HideInInspector]
        public string ActorID;
        [StringIsTextIDPopup(false)]
        public string TextID;
        public bool TextColor;
        public TextPositionTypes TextPosition;
        private string mTextData;
        private EventTelopBubble mBubble;
        private LoadRequest mBubbleResource;
        private LoadRequest mPortraitResource;
        private static readonly string AssetPath;

        static Event2dAction_Telop()
        {
            AssetPath = "Event2dAssets/TelopBubble";
            return;
        }

        public Event2dAction_Telop()
        {
            this.ActorID = "2DPlus";
            base..ctor();
            return;
        }

        private unsafe Vector2 CalcBubblePosition(Vector3 position)
        {
            Camera camera;
            Vector2 vector;
            vector = Camera.get_main().WorldToScreenPoint(position);
            &vector.x /= (float) Screen.get_width();
            &vector.y /= (float) Screen.get_height();
            return vector;
        }

        public override bool Forward()
        {
            if ((this.mBubble != null) == null)
            {
                goto Label_004F;
            }
            if (this.mBubble.Finished == null)
            {
                goto Label_0034;
            }
            this.mBubble.Forward();
            base.ActivateNext();
            return 1;
        Label_0034:
            if (this.mBubble.IsPrinting == null)
            {
                goto Label_004F;
            }
            this.mBubble.Skip();
        Label_004F:
            return 0;
        }

        private string GetActorName(string actorID)
        {
            GameObject obj2;
            TacticsUnitController controller;
            Unit unit;
            obj2 = EventAction.FindActor(this.ActorID);
            if ((obj2 != null) == null)
            {
                goto Label_003F;
            }
            controller = obj2.GetComponent<TacticsUnitController>();
            if ((controller != null) == null)
            {
                goto Label_003F;
            }
            unit = controller.Unit;
            if (unit == null)
            {
                goto Label_003F;
            }
            return unit.UnitName;
        Label_003F:
            return actorID;
        }

        public override unsafe void OnActivate()
        {
            RectTransform transform;
            RectTransform transform2;
            int num;
            RectTransform transform3;
            Vector2 vector;
            Rect rect;
            if ((this.mBubble != null) == null)
            {
                goto Label_008E;
            }
            if (this.mBubble.get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_008E;
            }
            this.mBubble.get_gameObject().SetActive(1);
            transform = this.mBubble.GetComponent<RectTransform>();
            vector = new Vector2(0.5f, 0.5f);
            transform.set_anchorMax(vector);
            transform.set_anchorMin(vector);
            transform.set_pivot(new Vector2(0.5f, 0.5f));
            transform.set_anchoredPosition(new Vector2(0f, 0f));
        Label_008E:
            if ((this.mBubble != null) == null)
            {
                goto Label_0155;
            }
            transform2 = this.mBubble.get_transform() as RectTransform;
            num = 0;
            goto Label_0107;
        Label_00B7:
            transform3 = EventTelopBubble.Instances[num].get_transform() as RectTransform;
            if ((transform2 != transform3) == null)
            {
                goto Label_0103;
            }
            if (&transform2.get_rect().Overlaps(transform3.get_rect()) == null)
            {
                goto Label_0103;
            }
            EventTelopBubble.Instances[num].Close();
        Label_0103:
            num += 1;
        Label_0107:
            if (num < EventTelopBubble.Instances.Count)
            {
                goto Label_00B7;
            }
            this.mBubble.TextColor = this.TextColor;
            this.mBubble.TextPosition = this.TextPosition;
            this.mBubble.SetBody(this.mTextData);
            this.mBubble.Open();
        Label_0155:
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__IteratorAB rab;
            rab = new <PreloadAssets>c__IteratorAB();
            rab.<>f__this = this;
            return rab;
        }

        public override void PreStart()
        {
            if ((this.mBubble == null) == null)
            {
                goto Label_00B8;
            }
            this.mBubble = EventTelopBubble.Find(this.ActorID);
            if ((this.mBubble == null) == null)
            {
                goto Label_00A7;
            }
            if (this.mBubbleResource == null)
            {
                goto Label_00A7;
            }
            this.mBubble = Object.Instantiate(this.mBubbleResource.asset) as EventTelopBubble;
            this.mBubble.get_transform().SetParent(base.ActiveCanvas.get_transform(), 0);
            this.mBubble.BubbleID = this.ActorID;
            this.mBubble.get_transform().SetAsLastSibling();
            this.mBubble.get_gameObject().SetActive(0);
        Label_00A7:
            this.mBubble.AdjustWidth(this.mTextData);
        Label_00B8:
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
        private sealed class <PreloadAssets>c__IteratorAB : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal Event2dAction_Telop <>f__this;

            public <PreloadAssets>c__IteratorAB()
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
                goto Label_00A1;
            Label_0021:
                this.<>f__this.mBubbleResource = AssetManager.LoadAsync<EventTelopBubble>(Event2dAction_Telop.AssetPath);
                this.$current = this.<>f__this.mBubbleResource.StartCoroutine();
                this.$PC = 1;
                goto Label_00A3;
            Label_0058:
                if ((this.<>f__this.mBubbleResource.asset == null) == null)
                {
                    goto Label_007F;
                }
                this.<>f__this.mBubbleResource = null;
            Label_007F:
                this.<>f__this.mTextData = LocalizedText.Get(this.<>f__this.TextID);
                this.$PC = -1;
            Label_00A1:
                return 0;
            Label_00A3:
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

        public enum TextPositionTypes
        {
            Left,
            Center,
            Right
        }

        public enum TextSpeedTypes
        {
            Normal,
            Slow,
            Fast
        }
    }
}

