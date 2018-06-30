namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/会話/表示", "会話の文章を表示し、プレイヤーの入力を待ちます。", 0x555588, 0x5555aa)]
    public class EventAction_Dialog2 : EventAction
    {
        private const float DialogPadding = 20f;
        private const string ExtraEmotionDir = "ExtraEmotions/";
        [StringIsActorList]
        public string ActorID;
        [StringIsLocalUnitIDPopup]
        public string UnitID;
        [StringIsTextIDPopup(true)]
        public string TextID;
        private string mTextData;
        private string mVoiceID;
        private UnitParam mUnit;
        private EventDialogBubble mBubble;
        private LoadRequest mBubbleResource;
        private LoadRequest mPortraitResource;
        [Tooltip("非同期にするか？")]
        public bool Async;
        [HideInInspector]
        public PortraitSet.EmotionTypes Emotion;
        [HideInInspector, StringIsResourcePath(typeof(Texture2D), "ExtraEmotions/")]
        public string CustomEmotion;
        public EventDialogBubble.Anchors Position;
        private static readonly string AssetPath;

        static EventAction_Dialog2()
        {
            AssetPath = "UI/DialogBubble1";
            return;
        }

        public EventAction_Dialog2()
        {
            this.Position = 8;
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
                goto Label_005A;
            }
            if (this.mBubble.Finished == null)
            {
                goto Label_003F;
            }
            this.mBubble.StopVoice();
            this.mBubble.Forward();
            this.OnFinish();
            return 1;
        Label_003F:
            if (this.mBubble.IsPrinting == null)
            {
                goto Label_005A;
            }
            this.mBubble.Skip();
        Label_005A:
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
            Debug.LogError("Invalid Voice ID: " + src);
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

        public override void GoToEndState()
        {
            if ((this.mBubble != null) == null)
            {
                goto Label_0023;
            }
            this.mBubble.Close();
            base.enabled = 0;
        Label_0023:
            return;
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
            Rect rect;
            if (((this.mBubble != null) == null) || (this.mBubble.get_gameObject().get_activeInHierarchy() != null))
            {
                goto Label_009D;
            }
            num = 0;
            goto Label_0061;
        Label_002D:
            if ((EventDialogBubble.Instances[num].BubbleID == this.ActorID) == null)
            {
                goto Label_005D;
            }
            EventDialogBubble.Instances[num].Close();
        Label_005D:
            num += 1;
        Label_0061:
            if ((num < EventDialogBubble.Instances.Count) && ((EventDialogBubble.Instances[num] != this.mBubble) != null))
            {
                goto Label_002D;
            }
            this.mBubble.get_gameObject().SetActive(1);
        Label_009D:
            if ((this.mBubble != null) == null)
            {
                goto Label_0236;
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
            transform = this.mBubble.get_transform() as RectTransform;
            num2 = 0;
            goto Label_0157;
        Label_0104:
            transform2 = EventDialogBubble.Instances[num2].get_transform() as RectTransform;
            if (((transform != transform2) == null) || (&transform.get_rect().Overlaps(transform2.get_rect()) == null))
            {
                goto Label_0153;
            }
            EventDialogBubble.Instances[num2].Close();
        Label_0153:
            num2 += 1;
        Label_0157:
            if (num2 < EventDialogBubble.Instances.Count)
            {
                goto Label_0104;
            }
            this.mBubble.SetName((this.mUnit == null) ? "???" : this.mUnit.name);
            this.mBubble.SetBody(this.mTextData);
            if (this.mPortraitResource == null)
            {
                goto Label_021A;
            }
            if (this.mPortraitResource.isDone == null)
            {
                goto Label_021A;
            }
            if ((this.mPortraitResource.asset as PortraitSet) == null)
            {
                goto Label_01FF;
            }
            this.mBubble.PortraitSet = (PortraitSet) this.mPortraitResource.asset;
            this.mBubble.CustomEmotion = null;
            goto Label_021A;
        Label_01FF:
            this.mBubble.CustomEmotion = (Texture2D) this.mPortraitResource.asset;
        Label_021A:
            this.mBubble.Emotion = this.Emotion;
            this.mBubble.Open();
        Label_0236:
            if (this.Async == null)
            {
                goto Label_0247;
            }
            base.ActivateNext();
        Label_0247:
            return;
        }

        protected virtual void OnFinish()
        {
            base.ActivateNext();
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__Iterator89 iterator;
            iterator = new <PreloadAssets>c__Iterator89();
            iterator.<>f__this = this;
            return iterator;
        }

        public override void PreStart()
        {
            if ((this.mBubble == null) == null)
            {
                goto Label_00CF;
            }
            this.mBubble = EventDialogBubble.Find(this.ActorID);
            if (this.mBubbleResource == null)
            {
                goto Label_00AD;
            }
            if ((this.mBubble == null) != null)
            {
                goto Label_0054;
            }
            if (this.mBubble.Anchor == this.Position)
            {
                goto Label_00AD;
            }
        Label_0054:
            this.mBubble = Object.Instantiate(this.mBubbleResource.asset) as EventDialogBubble;
            this.mBubble.get_transform().SetParent(base.ActiveCanvas.get_transform(), 0);
            this.mBubble.BubbleID = this.ActorID;
            this.mBubble.get_gameObject().SetActive(0);
        Label_00AD:
            this.mBubble.AdjustWidth(this.mTextData);
            this.mBubble.Anchor = this.Position;
        Label_00CF:
            return;
        }

        public override void SkipImmediate()
        {
            if ((this.mBubble != null) == null)
            {
                goto Label_0022;
            }
            this.mBubble.Close();
            this.OnFinish();
        Label_0022:
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
        private sealed class <PreloadAssets>c__Iterator89 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal EventAction_Dialog2 <>f__this;

            public <PreloadAssets>c__Iterator89()
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
                        goto Label_0025;

                    case 1:
                        goto Label_0077;

                    case 2:
                        goto Label_01AD;
                }
                goto Label_01BF;
            Label_0025:
                this.<>f__this.mBubbleResource = AssetManager.LoadAsync<EventDialogBubble>(EventAction_Dialog2.AssetPath);
                if ((this.<>f__this.mBubbleResource.asset == null) == null)
                {
                    goto Label_00B2;
                }
                this.$current = this.<>f__this.mBubbleResource.StartCoroutine();
                this.$PC = 1;
                goto Label_01C1;
            Label_0077:
                if ((this.<>f__this.mBubbleResource.asset == null) == null)
                {
                    goto Label_00B2;
                }
                Debug.LogError("Failed to load " + EventAction_Dialog2.AssetPath);
                this.<>f__this.mBubbleResource = null;
            Label_00B2:
                if (string.IsNullOrEmpty(this.<>f__this.UnitID) != null)
                {
                    goto Label_0136;
                }
                this.<>f__this.mUnit = MonoSingleton<GameManager>.Instance.GetUnitParam(this.<>f__this.UnitID);
                if (this.<>f__this.mUnit == null)
                {
                    goto Label_0136;
                }
                if (string.IsNullOrEmpty(this.<>f__this.CustomEmotion) == null)
                {
                    goto Label_0136;
                }
                this.<>f__this.mPortraitResource = AssetManager.LoadAsync<PortraitSet>("PortraitsS/" + this.<>f__this.mUnit.model);
            Label_0136:
                if (string.IsNullOrEmpty(this.<>f__this.CustomEmotion) != null)
                {
                    goto Label_0170;
                }
                this.<>f__this.mPortraitResource = AssetManager.LoadAsync<Texture2D>("ExtraEmotions/" + this.<>f__this.CustomEmotion);
            Label_0170:
                if ((this.<>f__this.mPortraitResource.asset == null) == null)
                {
                    goto Label_01AD;
                }
                this.$current = this.<>f__this.mPortraitResource.StartCoroutine();
                this.$PC = 2;
                goto Label_01C1;
            Label_01AD:
                this.<>f__this.LoadTextData();
                this.$PC = -1;
            Label_01BF:
                return 0;
            Label_01C1:
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

