namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("会話/表示(2D)", "会話の文章を表示し、プレイヤーの入力を待ちます。", 0x555555, 0x444488)]
    public class Event2dAction_Dialog : EventAction
    {
        private const float DialogPadding = 20f;
        private const EventDialogBubbleCustom.Anchors AnchorPoint = 8;
        [StringIsActorID, HideInInspector]
        public string ActorID;
        public string CharaID;
        [StringIsLocalUnitID]
        public string UnitID;
        [StringIsTextID(false)]
        public string TextID;
        public string Emotion;
        public bool Async;
        private string mTextData;
        private string mVoiceID;
        private UnitParam mUnit;
        private string mPlayerName;
        private EventDialogBubbleCustom mBubble;
        private LoadRequest mBubbleResource;
        private static readonly string AssetPath;

        static Event2dAction_Dialog()
        {
            AssetPath = "UI/DialogBubble2";
            return;
        }

        public Event2dAction_Dialog()
        {
            this.ActorID = "2DPlus";
            this.Emotion = string.Empty;
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
            if (EventStandCharaController2.Instances == null)
            {
                goto Label_02AF;
            }
            if (EventStandCharaController2.Instances.Count <= 0)
            {
                goto Label_02AF;
            }
            enumerator = EventStandCharaController2.Instances.GetEnumerator();
        Label_020A:
            try
            {
                goto Label_0291;
            Label_020F:
                controller = &enumerator.Current;
                if ((controller.CharaID == this.CharaID) == null)
                {
                    goto Label_0280;
                }
                if (controller.IsClose == null)
                {
                    goto Label_0240;
                }
                goto Label_0291;
            Label_0240:
                controller.get_transform().SetSiblingIndex(this.mBubble.get_transform().GetSiblingIndex() - 1);
                if (string.IsNullOrEmpty(this.Emotion) != null)
                {
                    goto Label_0291;
                }
                controller.UpdateEmotion(this.Emotion);
                goto Label_0291;
            Label_0280:
                if (controller.IsClose == null)
                {
                    goto Label_0291;
                }
            Label_0291:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_020F;
                }
                goto Label_02AF;
            }
            finally
            {
            Label_02A2:
                ((List<EventStandCharaController2>.Enumerator) enumerator).Dispose();
            }
        Label_02AF:
            if (this.Async == null)
            {
                goto Label_02C1;
            }
            base.ActivateNext();
            return;
        Label_02C1:
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__Iterator9E iteratore;
            iteratore = new <PreloadAssets>c__Iterator9E();
            iteratore.<>f__this = this;
            return iteratore;
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

        public override bool IsPreloadAssets
        {
            get
            {
                return 1;
            }
        }

        [CompilerGenerated]
        private sealed class <PreloadAssets>c__Iterator9E : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal Event2dAction_Dialog <>f__this;

            public <PreloadAssets>c__Iterator9E()
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
                this.<>f__this.mBubbleResource = AssetManager.LoadAsync<EventDialogBubbleCustom>(Event2dAction_Dialog.AssetPath);
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

