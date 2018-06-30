namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/会話/表示3(2D)", "会話の文章を表示し、プレイヤーの入力を待ちます。", 0x555555, 0x444488)]
    public class Event2dAction_Dialog3 : EventAction
    {
        private const float DialogPadding = 20f;
        private const float normalScale = 1f;
        private const EventDialogBubbleCustom.Anchors AnchorPoint = 8;
        [StringIsActorID, HideInInspector]
        public string ActorID;
        public string CharaID;
        [StringIsLocalUnitIDPopup]
        public string UnitID;
        [StringIsTextIDPopup(false)]
        public string TextID;
        public string Emotion;
        private List<GameObject> fadeInList;
        private List<GameObject> fadeOutList;
        private List<CanvasGroup> fadeInParticleList;
        private List<CanvasGroup> fadeOutParticleList;
        public bool Async;
        private string mTextData;
        private string mVoiceID;
        [HideInInspector]
        public float FadeTime;
        private float fadingTime;
        private bool IsFading;
        private bool FoldOut;
        [HideInInspector]
        public bool ActorParticle;
        private UnitParam mUnit;
        private EventDialogBubbleCustom mBubble;
        private LoadRequest mBubbleResource;
        private RectTransform bubbleTransform;
        [HideInInspector, SerializeField]
        public string[] IgnoreFadeOut;
        private static readonly string AssetPath;
        private bool FoldOutShake;
        [HideInInspector]
        public float Duration;
        [HideInInspector]
        public float FrequencyX;
        [HideInInspector]
        public float FrequencyY;
        [HideInInspector]
        public float AmplitudeX;
        [HideInInspector]
        public float AmplitudeY;
        private float mSeedX;
        private float mSeedY;
        private float ShakingTime;
        private bool IsShaking;
        private Vector2 originalPvt;

        static Event2dAction_Dialog3()
        {
            AssetPath = "UI/DialogBubble2";
            return;
        }

        public Event2dAction_Dialog3()
        {
            this.ActorID = "2DPlus";
            this.Emotion = string.Empty;
            this.fadeInList = new List<GameObject>();
            this.fadeOutList = new List<GameObject>();
            this.fadeInParticleList = new List<CanvasGroup>();
            this.fadeOutParticleList = new List<CanvasGroup>();
            this.FadeTime = 0.2f;
            this.IgnoreFadeOut = new string[1];
            this.FrequencyX = 12.51327f;
            this.FrequencyY = 20.4651f;
            this.AmplitudeX = 0.1f;
            this.AmplitudeY = 0.1f;
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
            float num;
            Color color;
            Color color2;
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            EventStandChara2 chara;
            string str;
            Color color3;
            Color color4;
            GameObject obj3;
            List<GameObject>.Enumerator enumerator2;
            EventStandChara2 chara2;
            string str2;
            Color color5;
            Color color6;
            float num2;
            float num3;
            CanvasGroup group;
            List<CanvasGroup>.Enumerator enumerator3;
            CanvasGroup group2;
            List<CanvasGroup>.Enumerator enumerator4;
            Color color7;
            Color color8;
            num = time / this.FadeTime;
            color = Color.Lerp(Color.get_white(), Color.get_grey(), num);
            color2 = Color.Lerp(Color.get_grey(), Color.get_white(), num);
            enumerator = this.fadeInList.GetEnumerator();
        Label_0038:
            try
            {
                goto Label_00DB;
            Label_003D:
                obj2 = &enumerator.Current;
                chara = obj2.GetComponent<EventStandChara2>();
                str = obj2.GetComponentInParent<EventStandCharaController2>().CharaID;
                color3 = Color.get_white();
                if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(str) == null)
                {
                    goto Label_0080;
                }
                color3 = Event2dAction_OperateStandChara.CharaColorDic[str];
            Label_0080:
                color4 = color3 * color;
                if (&chara.BodyObject.GetComponent<RawImage>().get_color().get_maxColorComponent() <= &color4.get_maxColorComponent())
                {
                    goto Label_00B5;
                }
                goto Label_00DB;
            Label_00B5:
                chara.FaceObject.GetComponent<RawImage>().set_color(color4);
                chara.BodyObject.GetComponent<RawImage>().set_color(color4);
            Label_00DB:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_003D;
                }
                goto Label_00F9;
            }
            finally
            {
            Label_00EC:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_00F9:
            enumerator2 = this.fadeOutList.GetEnumerator();
        Label_0106:
            try
            {
                goto Label_01AC;
            Label_010B:
                obj3 = &enumerator2.Current;
                chara2 = obj3.GetComponent<EventStandChara2>();
                str2 = obj3.GetComponentInParent<EventStandCharaController2>().CharaID;
                color5 = Color.get_white();
                if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(str2) == null)
                {
                    goto Label_0151;
                }
                color5 = Event2dAction_OperateStandChara.CharaColorDic[str2];
            Label_0151:
                color6 = color5 * color2;
                if (&chara2.BodyObject.GetComponent<RawImage>().get_color().get_maxColorComponent() >= &color6.get_maxColorComponent())
                {
                    goto Label_0186;
                }
                goto Label_01AC;
            Label_0186:
                chara2.FaceObject.GetComponent<RawImage>().set_color(color6);
                chara2.BodyObject.GetComponent<RawImage>().set_color(color6);
            Label_01AC:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_010B;
                }
                goto Label_01CA;
            }
            finally
            {
            Label_01BD:
                ((List<GameObject>.Enumerator) enumerator2).Dispose();
            }
        Label_01CA:
            num2 = Mathf.Lerp(1f, 0f, num);
            num3 = Mathf.Lerp(0f, 1f, num);
            enumerator3 = this.fadeInParticleList.GetEnumerator();
        Label_01FB:
            try
            {
                goto Label_0225;
            Label_0200:
                group = &enumerator3.Current;
                if (group.get_alpha() <= num2)
                {
                    goto Label_021C;
                }
                goto Label_0225;
            Label_021C:
                group.set_alpha(num2);
            Label_0225:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_0200;
                }
                goto Label_0243;
            }
            finally
            {
            Label_0236:
                ((List<CanvasGroup>.Enumerator) enumerator3).Dispose();
            }
        Label_0243:
            enumerator4 = this.fadeOutParticleList.GetEnumerator();
        Label_0250:
            try
            {
                goto Label_027A;
            Label_0255:
                group2 = &enumerator4.Current;
                if (group2.get_alpha() >= num3)
                {
                    goto Label_0271;
                }
                goto Label_027A;
            Label_0271:
                group2.set_alpha(num3);
            Label_027A:
                if (&enumerator4.MoveNext() != null)
                {
                    goto Label_0255;
                }
                goto Label_0298;
            }
            finally
            {
            Label_028B:
                ((List<CanvasGroup>.Enumerator) enumerator4).Dispose();
            }
        Label_0298:
            return;
        }

        public override bool Forward()
        {
            if (this.Async == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if ((this.mBubble != null) == null)
            {
                goto Label_0093;
            }
            if (this.mBubble.Finished == null)
            {
                goto Label_0078;
            }
            if (this.IsFading == null)
            {
                goto Label_0044;
            }
            this.FadeIn(0f);
        Label_0044:
            if (this.IsShaking == null)
            {
                goto Label_005A;
            }
            this.Shake(0f);
        Label_005A:
            this.mBubble.StopVoice();
            this.mBubble.Forward();
            base.ActivateNext();
            return 1;
        Label_0078:
            if (this.mBubble.IsPrinting == null)
            {
                goto Label_0093;
            }
            this.mBubble.Skip();
        Label_0093:
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
            int num2;
            RectTransform transform;
            EventStandCharaController2 controller;
            List<EventStandCharaController2>.Enumerator enumerator;
            Color color;
            GameObject obj2;
            GameObject[] objArray;
            int num3;
            GameObjectID[] tidArray;
            int num4;
            CanvasGroup group;
            int num5;
            Color color2;
            GameObject obj3;
            GameObject[] objArray2;
            int num6;
            GameObjectID[] tidArray2;
            int num7;
            CanvasGroup group2;
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
                goto Label_01CA;
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
            this.bubbleTransform = this.mBubble.get_transform() as RectTransform;
            num2 = 0;
            goto Label_0173;
        Label_0119:
            transform = EventDialogBubbleCustom.Instances[num2].get_transform() as RectTransform;
            if (((this.bubbleTransform != transform) == null) || (&this.bubbleTransform.get_rect().Overlaps(transform.get_rect()) == null))
            {
                goto Label_016F;
            }
            EventDialogBubbleCustom.Instances[num2].Close();
        Label_016F:
            num2 += 1;
        Label_0173:
            if (num2 < EventDialogBubbleCustom.Instances.Count)
            {
                goto Label_0119;
            }
            this.mBubble.SetName((this.mUnit == null) ? "???" : this.mUnit.name);
            this.mBubble.SetBody(this.mTextData);
            this.mBubble.Open();
        Label_01CA:
            this.fadeInList.Clear();
            this.fadeOutList.Clear();
            this.fadeInParticleList.Clear();
            this.fadeOutParticleList.Clear();
            this.IsFading = 0;
            if (EventStandCharaController2.Instances == null)
            {
                goto Label_04E0;
            }
            if (EventStandCharaController2.Instances.Count <= 0)
            {
                goto Label_04E0;
            }
            enumerator = EventStandCharaController2.Instances.GetEnumerator();
        Label_0223:
            try
            {
                goto Label_04C2;
            Label_0228:
                controller = &enumerator.Current;
                if (controller.IsClose == null)
                {
                    goto Label_0242;
                }
                goto Label_04C2;
            Label_0242:
                if ((controller.CharaID == this.CharaID) != null)
                {
                    goto Label_026B;
                }
                if (this.ContainIgnoreFO(controller.CharaID) == null)
                {
                    goto Label_03BC;
                }
            Label_026B:
                color = Color.get_white();
                if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(controller.CharaID) == null)
                {
                    goto Label_029B;
                }
                color = Event2dAction_OperateStandChara.CharaColorDic[controller.CharaID];
            Label_029B:
                objArray = controller.StandCharaList;
                num3 = 0;
                goto Label_02EF;
            Label_02AC:
                obj2 = objArray[num3];
                if ((obj2.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>().get_color() != color) == null)
                {
                    goto Label_02E9;
                }
                this.fadeInList.Add(obj2);
                this.IsFading = 1;
            Label_02E9:
                num3 += 1;
            Label_02EF:
                if (num3 < ((int) objArray.Length))
                {
                    goto Label_02AC;
                }
                if (this.ActorParticle == null)
                {
                    goto Label_0356;
                }
                tidArray = controller.get_gameObject().GetComponentsInChildren<GameObjectID>();
                num4 = 0;
                goto Label_034B;
            Label_031B:
                group = SRPG_Extensions.RequireComponent<CanvasGroup>(tidArray[num4]);
                if (group.get_alpha() == 1f)
                {
                    goto Label_0345;
                }
                this.fadeInParticleList.Add(group);
            Label_0345:
                num4 += 1;
            Label_034B:
                if (num4 < ((int) tidArray.Length))
                {
                    goto Label_031B;
                }
            Label_0356:
                num5 = this.mBubble.get_transform().GetSiblingIndex() - 1;
                controller.get_transform().SetSiblingIndex(num5);
                controller.get_transform().set_localScale(controller.get_transform().get_localScale() * 1f);
                if (string.IsNullOrEmpty(this.Emotion) != null)
                {
                    goto Label_04C2;
                }
                controller.UpdateEmotion(this.Emotion);
                goto Label_04C2;
            Label_03BC:
                if (controller.get_isActiveAndEnabled() != null)
                {
                    goto Label_03CD;
                }
                goto Label_04C2;
            Label_03CD:
                color2 = Color.get_white();
                if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(controller.CharaID) == null)
                {
                    goto Label_0407;
                }
                color2 = Event2dAction_OperateStandChara.CharaColorDic[controller.CharaID] * Color.get_gray();
            Label_0407:
                objArray2 = controller.StandCharaList;
                num6 = 0;
                goto Label_045B;
            Label_0418:
                obj3 = objArray2[num6];
                if ((obj3.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>().get_color() != color2) == null)
                {
                    goto Label_0455;
                }
                this.fadeOutList.Add(obj3);
                this.IsFading = 1;
            Label_0455:
                num6 += 1;
            Label_045B:
                if (num6 < ((int) objArray2.Length))
                {
                    goto Label_0418;
                }
                if (this.ActorParticle == null)
                {
                    goto Label_04C2;
                }
                tidArray2 = controller.get_gameObject().GetComponentsInChildren<GameObjectID>();
                num7 = 0;
                goto Label_04B7;
            Label_0487:
                group2 = SRPG_Extensions.RequireComponent<CanvasGroup>(tidArray2[num7]);
                if (group2.get_alpha() == 0f)
                {
                    goto Label_04B1;
                }
                this.fadeOutParticleList.Add(group2);
            Label_04B1:
                num7 += 1;
            Label_04B7:
                if (num7 < ((int) tidArray2.Length))
                {
                    goto Label_0487;
                }
            Label_04C2:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0228;
                }
                goto Label_04E0;
            }
            finally
            {
            Label_04D3:
                ((List<EventStandCharaController2>.Enumerator) enumerator).Dispose();
            }
        Label_04E0:
            if (this.IsFading == null)
            {
                goto Label_04F7;
            }
            this.fadingTime = this.FadeTime;
        Label_04F7:
            this.IsShaking = 0;
            if ((this.mBubble != null) == null)
            {
                goto Label_055D;
            }
            if (this.Duration <= 0f)
            {
                goto Label_055D;
            }
            this.IsShaking = 1;
            this.originalPvt = new Vector2(0.5f, 0f);
            this.ShakingTime = this.Duration;
            this.mSeedX = Random.get_value();
            this.mSeedY = Random.get_value();
        Label_055D:
            if (this.Async == null)
            {
                goto Label_056F;
            }
            base.ActivateNext(1);
        Label_056F:
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__IteratorA0 ra;
            ra = new <PreloadAssets>c__IteratorA0();
            ra.<>f__this = this;
            return ra;
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

        private unsafe void Shake(float time)
        {
            float num;
            float num2;
            float num3;
            Vector2 vector;
            if (time <= 0f)
            {
                goto Label_009B;
            }
            num = Mathf.Clamp01(time / this.Duration);
            num2 = (Mathf.Sin(((Time.get_time() + this.mSeedX) * this.FrequencyX) * 3.141593f) * this.AmplitudeX) * num;
            num3 = (Mathf.Sin(((Time.get_time() + this.mSeedY) * this.FrequencyY) * 3.141593f) * this.AmplitudeY) * num;
            &vector..ctor(&this.originalPvt.x + num2, &this.originalPvt.y + num3);
            this.bubbleTransform.set_pivot(vector);
            goto Label_00AC;
        Label_009B:
            this.bubbleTransform.set_pivot(this.originalPvt);
        Label_00AC:
            return;
        }

        public override void Update()
        {
            if (this.IsFading == null)
            {
                goto Label_004B;
            }
            this.fadingTime -= Time.get_deltaTime();
            if (this.fadingTime > 0f)
            {
                goto Label_003F;
            }
            this.fadingTime = 0f;
            this.IsFading = 0;
        Label_003F:
            this.FadeIn(this.fadingTime);
        Label_004B:
            if (this.IsShaking == null)
            {
                goto Label_0096;
            }
            this.ShakingTime -= Time.get_deltaTime();
            if (this.ShakingTime > 0f)
            {
                goto Label_008A;
            }
            this.ShakingTime = 0f;
            this.IsShaking = 0;
        Label_008A:
            this.Shake(this.ShakingTime);
        Label_0096:
            if (this.Async == null)
            {
                goto Label_00BE;
            }
            if (this.IsFading != null)
            {
                goto Label_00BE;
            }
            if (this.IsShaking != null)
            {
                goto Label_00BE;
            }
            base.enabled = 0;
        Label_00BE:
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
        private sealed class <PreloadAssets>c__IteratorA0 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal Event2dAction_Dialog3 <>f__this;

            public <PreloadAssets>c__IteratorA0()
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
                goto Label_00C6;
            Label_0021:
                this.<>f__this.mBubbleResource = AssetManager.LoadAsync<EventDialogBubbleCustom>(Event2dAction_Dialog3.AssetPath);
                this.$current = this.<>f__this.mBubbleResource.StartCoroutine();
                this.$PC = 1;
                goto Label_00C8;
            Label_0058:
                if ((this.<>f__this.mBubbleResource.asset == null) == null)
                {
                    goto Label_007F;
                }
                this.<>f__this.mBubbleResource = null;
            Label_007F:
                if (string.IsNullOrEmpty(this.<>f__this.UnitID) != null)
                {
                    goto Label_00B4;
                }
                this.<>f__this.mUnit = MonoSingleton<GameManager>.Instance.GetUnitParam(this.<>f__this.UnitID);
            Label_00B4:
                this.<>f__this.LoadTextData();
                this.$PC = -1;
            Label_00C6:
                return 0;
            Label_00C8:
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

