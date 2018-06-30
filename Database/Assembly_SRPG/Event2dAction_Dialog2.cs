namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/会話/表示2(2D)", "会話の文章を表示し、プレイヤーの入力を待ちます。", 0x555555, 0x444488)]
    public class Event2dAction_Dialog2 : EventAction
    {
        private const float DialogPadding = 20f;
        private const float normalScale = 1f;
        private const EventDialogBubbleCustom.Anchors AnchorPoint = 8;
        [HideInInspector, StringIsActorID]
        public string ActorID;
        public string CharaID;
        [StringIsLocalUnitID]
        public string UnitID;
        [StringIsTextID(false)]
        public string TextID;
        public string Emotion;
        private List<GameObject> fadeInList;
        private List<GameObject> fadeOutList;
        public bool Async;
        private string mTextData;
        private string mVoiceID;
        [HideInInspector]
        public float FadeTime;
        public bool Fade;
        private bool IsFading;
        private UnitParam mUnit;
        private string mPlayerName;
        private EventDialogBubbleCustom mBubble;
        private LoadRequest mBubbleResource;
        [HideInInspector, SerializeField]
        public string[] IgnoreFadeOut;
        private static readonly string AssetPath;

        static Event2dAction_Dialog2()
        {
            AssetPath = "UI/DialogBubble2";
            return;
        }

        public Event2dAction_Dialog2()
        {
            this.ActorID = "2DPlus";
            this.Emotion = string.Empty;
            this.fadeInList = new List<GameObject>();
            this.fadeOutList = new List<GameObject>();
            this.FadeTime = 0.2f;
            this.IgnoreFadeOut = new string[1];
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

        private bool ContainIgnoreFO(string charID)
        {
            int num;
            num = 0;
            goto Label_0020;
        Label_0007:
            if (this.IgnoreFadeOut[num].Equals(charID) == null)
            {
                goto Label_001C;
            }
            return 1;
        Label_001C:
            num += 1;
        Label_0020:
            if (num < ((int) this.IgnoreFadeOut.Length))
            {
                goto Label_0007;
            }
            return 0;
        }

        private unsafe void FadeIn(float time)
        {
            Color color;
            Color color2;
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            GameObject obj3;
            List<GameObject>.Enumerator enumerator2;
            Color color3;
            Color color4;
            Color color5;
            Color color6;
            Color color7;
            Color color8;
            Color color9;
            Color color10;
            Color color11;
            Color color12;
            Color color13;
            Color color14;
            Color color15;
            Color color16;
            &color..ctor(Mathf.Lerp(&Color.get_white().r, &Color.get_gray().r, time), Mathf.Lerp(&Color.get_white().g, &Color.get_gray().g, time), Mathf.Lerp(&Color.get_white().b, &Color.get_gray().b, time), 1f);
            &color2..ctor(Mathf.Lerp(&Color.get_gray().r, &Color.get_white().r, time), Mathf.Lerp(&Color.get_gray().g, &Color.get_white().g, time), Mathf.Lerp(&Color.get_gray().b, &Color.get_white().b, time), 1f);
            enumerator = this.fadeInList.GetEnumerator();
        Label_00F0:
            try
            {
                goto Label_0158;
            Label_00F5:
                obj2 = &enumerator.Current;
                if (&obj2.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>().get_color().r <= &color.r)
                {
                    goto Label_012C;
                }
                goto Label_0158;
            Label_012C:
                obj2.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>().set_color(color);
                obj2.GetComponent<EventStandChara2>().BodyObject.GetComponent<RawImage>().set_color(color);
            Label_0158:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00F5;
                }
                goto Label_0175;
            }
            finally
            {
            Label_0169:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_0175:
            enumerator2 = this.fadeOutList.GetEnumerator();
        Label_0182:
            try
            {
                goto Label_01EE;
            Label_0187:
                obj3 = &enumerator2.Current;
                if (&obj3.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>().get_color().r >= &color2.r)
                {
                    goto Label_01C0;
                }
                goto Label_01EE;
            Label_01C0:
                obj3.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>().set_color(color2);
                obj3.GetComponent<EventStandChara2>().BodyObject.GetComponent<RawImage>().set_color(color2);
            Label_01EE:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0187;
                }
                goto Label_020C;
            }
            finally
            {
            Label_01FF:
                ((List<GameObject>.Enumerator) enumerator2).Dispose();
            }
        Label_020C:
            return;
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

        private static string[] GetIDPair(string src)
        {
            char[] chArray1;
            string[] strArray;
            chArray1 = new char[] { 0x2e };
            strArray = src.Split(chArray1, 2);
            if (((int) strArray.Length) < 2)
            {
                goto Label_003A;
            }
            if (strArray[0].Length <= 0)
            {
                goto Label_003A;
            }
            if (strArray[1].Length <= 0)
            {
                goto Label_003A;
            }
            return strArray;
        Label_003A:
            return null;
        }

        public override string[] GetUnManagedAssetListData()
        {
            string[] strArray;
            if (string.IsNullOrEmpty(this.TextID) != null)
            {
                goto Label_003A;
            }
            this.LoadTextData();
            if (string.IsNullOrEmpty(this.mVoiceID) != null)
            {
                goto Label_003A;
            }
            return EventAction.GetUnManagedStreamAssets(GetIDPair(this.mVoiceID), 0);
        Label_003A:
            return null;
        }

        private void LoadTextData()
        {
            char[] chArray1;
            string str;
            string[] strArray;
            string str2;
            if (string.IsNullOrEmpty(this.TextID) != null)
            {
                goto Label_0054;
            }
            chArray1 = new char[] { 9 };
            strArray = LocalizedText.Get(this.TextID).Split(chArray1);
            this.mTextData = strArray[0];
            this.mVoiceID = (((int) strArray.Length) <= 1) ? null : strArray[1];
            goto Label_0064;
        Label_0054:
            this.mTextData = this.mVoiceID = null;
        Label_0064:
            return;
        }

        public override unsafe void OnActivate()
        {
            int num;
            string[] strArray;
            RectTransform transform;
            int num2;
            RectTransform transform2;
            EventStandCharaController2 controller;
            List<EventStandCharaController2>.Enumerator enumerator;
            GameObject obj2;
            GameObject[] objArray;
            int num3;
            int num4;
            GameObject obj3;
            GameObject[] objArray2;
            int num5;
            Rect rect;
            if (((this.mBubble != null) == null) || (this.mBubble.get_gameObject().get_activeInHierarchy() != null))
            {
                goto Label_009D;
            }
            num = 0;
            goto Label_0061;
        Label_002D:
            if ((EventDialogBubbleCustom.Instances[num].BubbleID == this.ActorID) == null)
            {
                goto Label_005D;
            }
            EventDialogBubbleCustom.Instances[num].Close();
        Label_005D:
            num += 1;
        Label_0061:
            if ((num < EventDialogBubbleCustom.Instances.Count) && ((EventDialogBubbleCustom.Instances[num] != this.mBubble) != null))
            {
                goto Label_002D;
            }
            this.mBubble.get_gameObject().SetActive(1);
        Label_009D:
            if ((this.mBubble != null) == null)
            {
                goto Label_01E4;
            }
            if (string.IsNullOrEmpty(this.mVoiceID) != null)
            {
                goto Label_00EC;
            }
            strArray = GetIDPair(this.mVoiceID);
            if (strArray == null)
            {
                goto Label_00EC;
            }
            this.mBubble.VoiceSheetName = strArray[0];
            this.mBubble.VoiceCueName = strArray[1];
        Label_00EC:
            this.mBubble.get_transform().SetAsLastSibling();
            transform = this.mBubble.get_transform() as RectTransform;
            num2 = 0;
            goto Label_0167;
        Label_0114:
            transform2 = EventDialogBubbleCustom.Instances[num2].get_transform() as RectTransform;
            if (((transform != transform2) == null) || (&transform.get_rect().Overlaps(transform2.get_rect()) == null))
            {
                goto Label_0163;
            }
            EventDialogBubbleCustom.Instances[num2].Close();
        Label_0163:
            num2 += 1;
        Label_0167:
            if (num2 < EventDialogBubbleCustom.Instances.Count)
            {
                goto Label_0114;
            }
            if (string.IsNullOrEmpty(this.mPlayerName) == null)
            {
                goto Label_01B7;
            }
            this.mBubble.SetName((this.mUnit == null) ? "???" : this.mUnit.name);
            goto Label_01C8;
        Label_01B7:
            this.mBubble.SetName(this.mPlayerName);
        Label_01C8:
            this.mBubble.SetBody(this.mTextData);
            this.mBubble.Open();
        Label_01E4:
            this.fadeInList.Clear();
            this.fadeOutList.Clear();
            this.IsFading = 0;
            if (EventStandCharaController2.Instances == null)
            {
                goto Label_03D7;
            }
            if (EventStandCharaController2.Instances.Count <= 0)
            {
                goto Label_03D7;
            }
            enumerator = EventStandCharaController2.Instances.GetEnumerator();
        Label_0227:
            try
            {
                goto Label_03B9;
            Label_022C:
                controller = &enumerator.Current;
                if (controller.IsClose == null)
                {
                    goto Label_0246;
                }
                goto Label_03B9;
            Label_0246:
                if ((controller.CharaID == this.CharaID) != null)
                {
                    goto Label_026F;
                }
                if (this.ContainIgnoreFO(controller.CharaID) == null)
                {
                    goto Label_0346;
                }
            Label_026F:
                objArray = controller.StandCharaList;
                num3 = 0;
                goto Label_02C6;
            Label_0280:
                obj2 = objArray[num3];
                if ((obj2.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>().get_color() != Color.get_white()) == null)
                {
                    goto Label_02C0;
                }
                this.fadeInList.Add(obj2);
                this.IsFading = 1;
            Label_02C0:
                num3 += 1;
            Label_02C6:
                if (num3 < ((int) objArray.Length))
                {
                    goto Label_0280;
                }
                num4 = this.mBubble.get_transform().GetSiblingIndex() - 1;
                Debug.Log("set index:" + ((int) num4));
                controller.get_transform().SetSiblingIndex(num4);
                controller.get_transform().set_localScale(Vector3.get_one() * 1f);
                if (string.IsNullOrEmpty(this.Emotion) != null)
                {
                    goto Label_03B9;
                }
                controller.UpdateEmotion(this.Emotion);
                goto Label_03B9;
            Label_0346:
                if (controller.get_isActiveAndEnabled() != null)
                {
                    goto Label_0357;
                }
                goto Label_03B9;
            Label_0357:
                objArray2 = controller.StandCharaList;
                num5 = 0;
                goto Label_03AE;
            Label_0368:
                obj3 = objArray2[num5];
                if ((obj3.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>().get_color() != Color.get_gray()) == null)
                {
                    goto Label_03A8;
                }
                this.fadeOutList.Add(obj3);
                this.IsFading = 1;
            Label_03A8:
                num5 += 1;
            Label_03AE:
                if (num5 < ((int) objArray2.Length))
                {
                    goto Label_0368;
                }
            Label_03B9:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_022C;
                }
                goto Label_03D7;
            }
            finally
            {
            Label_03CA:
                ((List<EventStandCharaController2>.Enumerator) enumerator).Dispose();
            }
        Label_03D7:
            if (this.Async == null)
            {
                goto Label_03E9;
            }
            base.ActivateNext();
            return;
        Label_03E9:
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__Iterator9F iteratorf;
            iteratorf = new <PreloadAssets>c__Iterator9F();
            iteratorf.<>f__this = this;
            return iteratorf;
        }

        public override void PreStart()
        {
            if ((this.mBubble == null) == null)
            {
                goto Label_00E0;
            }
            if (string.IsNullOrEmpty(this.UnitID) != null)
            {
                goto Label_002D;
            }
            this.ActorID = this.UnitID;
        Label_002D:
            this.mBubble = EventDialogBubbleCustom.Find(this.ActorID);
            if (this.mBubbleResource == null)
            {
                goto Label_00C3;
            }
            if ((this.mBubble == null) == null)
            {
                goto Label_00C3;
            }
            this.mBubble = Object.Instantiate(this.mBubbleResource.asset) as EventDialogBubbleCustom;
            this.mBubble.get_transform().SetParent(base.ActiveCanvas.get_transform(), 0);
            this.mBubble.BubbleID = this.ActorID;
            this.mBubble.get_transform().SetAsLastSibling();
            this.mBubble.get_gameObject().SetActive(0);
        Label_00C3:
            this.mBubble.AdjustWidth(this.mTextData);
            this.mBubble.Anchor = 8;
        Label_00E0:
            return;
        }

        public override void Update()
        {
            if (this.IsFading != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.FadeTime -= Time.get_deltaTime();
            if (this.FadeTime > 0f)
            {
                goto Label_0041;
            }
            this.FadeTime = 0f;
            this.IsFading = 0;
            return;
        Label_0041:
            this.FadeIn(this.FadeTime);
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
        private sealed class <PreloadAssets>c__Iterator9F : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal Event2dAction_Dialog2 <>f__this;

            public <PreloadAssets>c__Iterator9F()
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
                        goto Label_0073;
                }
                goto Label_0144;
            Label_0021:
                this.<>f__this.mBubbleResource = AssetManager.LoadAsync<EventDialogBubbleCustom>(Event2dAction_Dialog2.AssetPath);
                if ((this.<>f__this.mBubbleResource.asset == null) == null)
                {
                    goto Label_009A;
                }
                this.$current = this.<>f__this.mBubbleResource.StartCoroutine();
                this.$PC = 1;
                goto Label_0146;
            Label_0073:
                if ((this.<>f__this.mBubbleResource.asset == null) == null)
                {
                    goto Label_009A;
                }
                this.<>f__this.mBubbleResource = null;
            Label_009A:
                if (string.IsNullOrEmpty(this.<>f__this.UnitID) != null)
                {
                    goto Label_0132;
                }
                this.<>f__this.mPlayerName = string.Empty;
                if (this.<>f__this.UnitID.Equals("<p_name>") == null)
                {
                    goto Label_0112;
                }
                this.<>f__this.mPlayerName = ((MonoSingleton<GameManager>.GetInstanceDirect() != null) == null) ? "GUMI" : MonoSingleton<GameManager>.GetInstanceDirect().Player.Name;
                goto Label_0132;
            Label_0112:
                this.<>f__this.mUnit = MonoSingleton<GameManager>.Instance.GetUnitParam(this.<>f__this.UnitID);
            Label_0132:
                this.<>f__this.LoadTextData();
                this.$PC = -1;
            Label_0144:
                return 0;
            Label_0146:
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

        public enum TextSpeedTypes
        {
            Normal,
            Slow,
            Fast
        }
    }
}

