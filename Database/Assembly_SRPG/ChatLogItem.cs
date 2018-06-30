namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class ChatLogItem : MonoBehaviour
    {
        [SerializeField]
        private LayoutElement Element;
        [SerializeField]
        private GameObject Icon;
        [SerializeField]
        private RawImage LeftIcon;
        [SerializeField]
        private RawImage RightIcon;
        [SerializeField]
        private GameObject MessageIcon;
        [SerializeField]
        private GameObject MessageLog;
        [SerializeField]
        private GameObject MyMessageIcon;
        [SerializeField]
        private GameObject MyMessageLog;
        [SerializeField]
        private RectTransform AnyLogRoot;
        [SerializeField]
        private RectTransform MyLogRoot;
        [SerializeField]
        private GameObject MyStampObj;
        [SerializeField]
        private GameObject AnyStampObj;
        [SerializeField]
        private Text MyNameObj;
        [SerializeField]
        private Text MyFuIDObj;
        [SerializeField]
        private Text MyPostAtObj;
        [SerializeField]
        private Image MyStampImageObj;
        [SerializeField]
        private Text MyMessageTextObj;
        [SerializeField]
        private Text AnyNameObj;
        [SerializeField]
        private Text AnyFuIDObj;
        [SerializeField]
        private Text AnyPostAtObj;
        [SerializeField]
        private Image AnyStampImageObj;
        [SerializeField]
        private Text AnyMessageTextObj;
        [SerializeField]
        private GameObject SystemMessageRootObj;
        [SerializeField]
        private Text SystemMessageTextObj;
        private Transform mStampRoot;
        private RectTransform mLogRoot;
        private Image mLogImg;
        private ChatStampParam[] mStampParams;
        private bool IsStampSettings;
        private Coroutine mCoroutine;
        private GameObject mRoot;
        private Text mNameObj;
        private Text mPostAtObj;
        private Text mFuIDObj;
        private Image mStampImageObj;
        private Text mMessageObj;
        private SRPG.ChatLogParam mChatLogParam;
        public readonly int STAMP_SIZE;

        public ChatLogItem()
        {
            this.STAMP_SIZE = 0x9a;
            base..ctor();
            return;
        }

        public void Awake()
        {
            if ((this.MessageIcon != null) == null)
            {
                goto Label_001D;
            }
            this.MessageIcon.SetActive(0);
        Label_001D:
            if ((this.MessageLog != null) == null)
            {
                goto Label_003A;
            }
            this.MessageLog.SetActive(0);
        Label_003A:
            if ((this.MyMessageIcon != null) == null)
            {
                goto Label_0057;
            }
            this.MyMessageIcon.SetActive(0);
        Label_0057:
            if ((this.MyMessageLog != null) == null)
            {
                goto Label_0074;
            }
            this.MyMessageLog.SetActive(0);
        Label_0074:
            return;
        }

        public void Clear()
        {
            SRPG_Button button;
            base.get_gameObject().SetActive(0);
            button = this.GetIcon.GetComponent<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_0036;
            }
            button.get_onClick().RemoveAllListeners();
            this.mChatLogParam = null;
        Label_0036:
            return;
        }

        public static unsafe string GetPostAt(long postat)
        {
            object[] objArray4;
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            string str;
            DateTime time;
            TimeSpan span;
            int num;
            int num2;
            int num3;
            int num4;
            str = string.Empty;
            time = GameUtility.UnixtimeToLocalTime(postat);
            span = DateTime.Now - time;
            num = &span.Days;
            num2 = &span.Hours;
            num3 = &span.Minutes;
            num4 = &span.Seconds;
            if (num <= 0)
            {
                goto Label_0063;
            }
            objArray1 = new object[] { &num.ToString() };
            str = LocalizedText.Get("sys.CHAT_POSTAT_DAY", objArray1);
            goto Label_00E7;
        Label_0063:
            if (num2 <= 0)
            {
                goto Label_008B;
            }
            objArray2 = new object[] { &num2.ToString() };
            str = LocalizedText.Get("sys.CHAT_POSTAT_HOUR", objArray2);
            goto Label_00E7;
        Label_008B:
            if (num3 <= 0)
            {
                goto Label_00B3;
            }
            objArray3 = new object[] { &num3.ToString() };
            str = LocalizedText.Get("sys.CHAT_POSTAT_MINUTE", objArray3);
            goto Label_00E7;
        Label_00B3:
            if (num4 <= 10)
            {
                goto Label_00DC;
            }
            objArray4 = new object[] { &num4.ToString() };
            str = LocalizedText.Get("sys.CHAT_POSTAT_SECOND", objArray4);
            goto Label_00E7;
        Label_00DC:
            str = LocalizedText.Get("sys.CHAT_POSTAT_NOW");
        Label_00E7:
            return str;
        }

        public void OnDisable()
        {
            if (this.mCoroutine == null)
            {
                goto Label_001E;
            }
            base.StopCoroutine(this.mCoroutine);
            this.mCoroutine = null;
        Label_001E:
            return;
        }

        public unsafe void Refresh(SRPG.ChatLogParam param, ChatWindow.MessageTemplateType type)
        {
            object[] objArray1;
            RawImage image;
            UnitParam param2;
            List<ArtifactParam> list;
            ArtifactParam param3;
            string str;
            int num;
            VerticalLayoutGroup group;
            <Refresh>c__AnonStorey313 storey;
            Vector2 vector;
            storey = new <Refresh>c__AnonStorey313();
            storey.param = param;
            if (storey.param != null)
            {
                goto Label_001C;
            }
            return;
        Label_001C:
            if (this.mCoroutine == null)
            {
                goto Label_003A;
            }
            base.StopCoroutine(this.mCoroutine);
            this.mCoroutine = null;
        Label_003A:
            if ((this.mRoot == null) == null)
            {
                goto Label_007D;
            }
            if ((base.get_transform().get_parent() != null) == null)
            {
                goto Label_007C;
            }
            this.mRoot = base.get_transform().get_parent().get_gameObject();
            goto Label_007D;
        Label_007C:
            return;
        Label_007D:
            this.MessageIcon.SetActive(0);
            this.MessageLog.SetActive(0);
            this.MyMessageIcon.SetActive(0);
            this.MyMessageLog.SetActive(0);
            this.SystemMessageRootObj.SetActive(0);
            if (type != 1)
            {
                goto Label_015E;
            }
            this.MessageIcon.SetActive(1);
            this.MessageLog.SetActive(1);
            this.mStampRoot = ((this.AnyStampObj != null) == null) ? null : this.AnyStampObj.get_transform();
            this.mNameObj = this.AnyNameObj;
            this.mFuIDObj = this.AnyFuIDObj;
            this.mPostAtObj = this.AnyPostAtObj;
            this.mStampImageObj = this.AnyStampImageObj;
            this.mMessageObj = this.AnyMessageTextObj;
            this.mLogRoot = this.AnyLogRoot;
            this.mLogImg = this.AnyLogRoot.GetComponent<Image>();
            goto Label_024C;
        Label_015E:
            if (type != 2)
            {
                goto Label_0203;
            }
            this.MyMessageIcon.SetActive(1);
            this.MyMessageLog.SetActive(1);
            this.mStampRoot = ((this.MyStampObj != null) == null) ? null : this.MyStampObj.get_transform();
            this.mNameObj = this.MyNameObj;
            this.mFuIDObj = this.MyFuIDObj;
            this.mPostAtObj = this.MyPostAtObj;
            this.mStampImageObj = this.MyStampImageObj;
            this.mMessageObj = this.MyMessageTextObj;
            this.mLogRoot = this.MyLogRoot;
            this.mLogImg = this.MyLogRoot.GetComponent<Image>();
            goto Label_024C;
        Label_0203:
            if (type != 3)
            {
                goto Label_024C;
            }
            this.SystemMessageRootObj.SetActive(1);
            this.SystemMessageTextObj.set_text(storey.param.message);
            this.mCoroutine = base.StartCoroutine(this.RefreshTextLine(storey.param.message));
            return;
        Label_024C:
            if ((((this.Icon != null) == null) || ((this.LeftIcon != null) == null)) || ((this.RightIcon != null) == null))
            {
                goto Label_0345;
            }
            image = (type != 2) ? this.LeftIcon : this.RightIcon;
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(storey.param.icon);
            if (param2 == null)
            {
                goto Label_0345;
            }
            if (string.IsNullOrEmpty(storey.param.skin_iname) != null)
            {
                goto Label_0328;
            }
            if ((image != null) == null)
            {
                goto Label_0328;
            }
            param3 = Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), new Predicate<ArtifactParam>(storey.<>m__2B7));
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(image, AssetPath.UnitSkinIconSmall(param2, param3, storey.param.job_iname));
            goto Label_0345;
        Label_0328:
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(image, AssetPath.UnitIconSmall(param2, storey.param.job_iname));
        Label_0345:
            if ((this.mNameObj != null) == null)
            {
                goto Label_036D;
            }
            this.mNameObj.set_text(storey.param.name);
        Label_036D:
            if ((this.mFuIDObj != null) == null)
            {
                goto Label_03C5;
            }
            str = storey.param.fuid.Substring(storey.param.fuid.Length - 4, 4);
            objArray1 = new object[] { str };
            this.mFuIDObj.set_text(LocalizedText.Get("sys.TEXT_CHAT_FUID", objArray1));
        Label_03C5:
            if ((this.mPostAtObj != null) == null)
            {
                goto Label_03F2;
            }
            this.mPostAtObj.set_text(GetPostAt(storey.param.posted_at));
        Label_03F2:
            if (storey.param.message_type != 1)
            {
                goto Label_046A;
            }
            if ((this.mRoot != null) == null)
            {
                goto Label_055E;
            }
            if (this.mRoot.get_activeInHierarchy() == null)
            {
                goto Label_055E;
            }
            if ((this.mStampRoot != null) == null)
            {
                goto Label_0447;
            }
            this.mStampRoot.get_gameObject().SetActive(0);
        Label_0447:
            this.mCoroutine = base.StartCoroutine(this.RefreshTextLine(storey.param.message));
            goto Label_055E;
        Label_046A:
            if (storey.param.message_type != 2)
            {
                goto Label_055E;
            }
            if ((this.mRoot != null) == null)
            {
                goto Label_055E;
            }
            if (this.mRoot.get_activeInHierarchy() == null)
            {
                goto Label_055E;
            }
            if ((this.mStampRoot != null) == null)
            {
                goto Label_04BF;
            }
            this.mStampRoot.get_gameObject().SetActive(1);
        Label_04BF:
            if ((this.Element != null) == null)
            {
                goto Label_0534;
            }
            num = this.STAMP_SIZE;
            group = this.mLogRoot.GetComponent<VerticalLayoutGroup>();
            num += group.get_padding().get_top();
            num += group.get_padding().get_bottom();
            num += (int) Mathf.Abs(&this.mLogRoot.get_anchoredPosition().y);
            this.Element.set_minHeight((float) num);
        Label_0534:
            this.mLogImg.set_enabled(0);
            this.mCoroutine = base.StartCoroutine(this.RefreshStamp(storey.param.stamp_id));
        Label_055E:
            return;
        }

        [DebuggerHidden]
        private IEnumerator RefreshStamp(int id)
        {
            <RefreshStamp>c__IteratorF0 rf;
            rf = new <RefreshStamp>c__IteratorF0();
            rf.id = id;
            rf.<$>id = id;
            rf.<>f__this = this;
            return rf;
        }

        [DebuggerHidden]
        private IEnumerator RefreshTextLine(string text)
        {
            <RefreshTextLine>c__IteratorEF ref2;
            ref2 = new <RefreshTextLine>c__IteratorEF();
            ref2.text = text;
            ref2.<$>text = text;
            ref2.<>f__this = this;
            return ref2;
        }

        public void SetParam(SRPG.ChatLogParam param, SRPG_Button.ButtonClickEvent OnClickEvent)
        {
            ChatWindow.MessageTemplateType type;
            SRPG_Button button;
            if (param != null)
            {
                goto Label_001A;
            }
            base.get_gameObject().SetActive(0);
            this.mChatLogParam = null;
            return;
        Label_001A:
            this.mChatLogParam = param;
            type = 1;
            if ((MonoSingleton<GameManager>.Instance.Player.FUID == param.fuid) == null)
            {
                goto Label_0049;
            }
            type = 2;
            goto Label_005B;
        Label_0049:
            if (string.IsNullOrEmpty(param.fuid) == null)
            {
                goto Label_005B;
            }
            type = 3;
        Label_005B:
            base.get_gameObject().SetActive(1);
            this.Refresh(param, type);
            button = this.GetIcon.GetComponent<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_00B8;
            }
            button.get_onClick().RemoveAllListeners();
            if ((param.fuid != MonoSingleton<GameManager>.Instance.Player.FUID) == null)
            {
                goto Label_00B8;
            }
            button.AddListener(OnClickEvent);
        Label_00B8:
            return;
        }

        private bool SetupChatStampMaster()
        {
            string str;
            JSON_ChatStampParam[] paramArray;
            int num;
            ChatStampParam param;
            bool flag;
            str = AssetManager.LoadTextData(ChatStamp.CHAT_STAMP_MASTER_PATH);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0018;
            }
            return 0;
        Label_0018:
            try
            {
                paramArray = JSONParser.parseJSONArray<JSON_ChatStampParam>(str);
                if (paramArray != null)
                {
                    goto Label_002B;
                }
                throw new InvalidJSONException();
            Label_002B:
                this.mStampParams = new ChatStampParam[(int) paramArray.Length];
                num = 0;
                goto Label_0061;
            Label_0040:
                param = new ChatStampParam();
                if (param.Deserialize(paramArray[num]) == null)
                {
                    goto Label_005D;
                }
                this.mStampParams[num] = param;
            Label_005D:
                num += 1;
            Label_0061:
                if (num < ((int) paramArray.Length))
                {
                    goto Label_0040;
                }
                goto Label_007D;
            }
            catch
            {
            Label_006F:
                flag = 0;
                goto Label_007F;
            }
        Label_007D:
            return 1;
        Label_007F:
            return flag;
        }

        private void Start()
        {
            this.SetupChatStampMaster();
            this.IsStampSettings = 1;
            return;
        }

        public GameObject GetIcon
        {
            get
            {
                return this.Icon;
            }
        }

        public SRPG.ChatLogParam ChatLogParam
        {
            get
            {
                return this.mChatLogParam;
            }
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey313
        {
            internal ChatLogParam param;

            public <Refresh>c__AnonStorey313()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2B7(ArtifactParam p)
            {
                return (p.iname == this.param.skin_iname);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshStamp>c__IteratorF0 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal Transform <tr>__0;
            internal Text <text_obj>__1;
            internal string <img_id>__2;
            internal int <i>__3;
            internal int id;
            internal Image <img>__4;
            internal LoadRequest <req>__5;
            internal SpriteSheet <sheet>__6;
            internal Sprite <sprite>__7;
            internal int $PC;
            internal object $current;
            internal int <$>id;
            internal ChatLogItem <>f__this;

            public <RefreshStamp>c__IteratorF0()
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
                        goto Label_0029;

                    case 1:
                        goto Label_00F5;

                    case 2:
                        goto Label_0231;

                    case 3:
                        goto Label_02A4;
                }
                goto Label_02AB;
            Label_0029:
                if ((this.<>f__this.mLogRoot == null) == null)
                {
                    goto Label_0044;
                }
                goto Label_02AB;
            Label_0044:
                if ((this.<>f__this.mStampImageObj == null) == null)
                {
                    goto Label_005F;
                }
                goto Label_02AB;
            Label_005F:
                if ((this.<>f__this.mStampRoot == null) != null)
                {
                    goto Label_0098;
                }
                if (this.<>f__this.mStampParams == null)
                {
                    goto Label_0098;
                }
                if (((int) this.<>f__this.mStampParams.Length) > 0)
                {
                    goto Label_00F5;
                }
            Label_0098:
                if (this.<>f__this.mStampParams != null)
                {
                    goto Label_02AB;
                }
                this.<>f__this.IsStampSettings = this.<>f__this.SetupChatStampMaster();
                if (this.<>f__this.IsStampSettings != null)
                {
                    goto Label_00F5;
                }
                goto Label_02AB;
                goto Label_00DD;
                goto Label_02AB;
            Label_00DD:
                goto Label_00F5;
            Label_00E2:
                this.$current = null;
                this.$PC = 1;
                goto Label_02AD;
            Label_00F5:
                if (this.<>f__this.IsStampSettings == null)
                {
                    goto Label_00E2;
                }
                this.<tr>__0 = this.<>f__this.mLogRoot.FindChild("text");
                if (this.<tr>__0 == null)
                {
                    goto Label_0161;
                }
                this.<text_obj>__1 = this.<tr>__0.GetComponent<Text>();
                if (this.<text_obj>__1 == null)
                {
                    goto Label_0161;
                }
                this.<text_obj>__1.set_text(string.Empty);
            Label_0161:
                this.<img_id>__2 = string.Empty;
                this.<i>__3 = 0;
                goto Label_01CA;
            Label_0178:
                if (this.id != this.<>f__this.mStampParams[this.<i>__3].id)
                {
                    goto Label_01BC;
                }
                this.<img_id>__2 = this.<>f__this.mStampParams[this.<i>__3].img_id;
                goto Label_01E2;
            Label_01BC:
                this.<i>__3 += 1;
            Label_01CA:
                if (this.<i>__3 < ((int) this.<>f__this.mStampParams.Length))
                {
                    goto Label_0178;
                }
            Label_01E2:
                this.<img>__4 = this.<>f__this.mStampImageObj;
                if ((this.<img>__4 != null) == null)
                {
                    goto Label_0291;
                }
                this.<req>__5 = AssetManager.LoadAsync<SpriteSheet>(ChatStamp.CHAT_STAMP_IMAGE_PATH);
                this.$current = this.<req>__5.StartCoroutine();
                this.$PC = 2;
                goto Label_02AD;
            Label_0231:
                this.<sheet>__6 = this.<req>__5.asset as SpriteSheet;
                if ((this.<sheet>__6 != null) == null)
                {
                    goto Label_0291;
                }
                this.<sprite>__7 = this.<sheet>__6.GetSprite(this.<img_id>__2);
                if ((this.<sprite>__7 != null) == null)
                {
                    goto Label_0291;
                }
                this.<img>__4.set_sprite(this.<sprite>__7);
            Label_0291:
                this.$current = null;
                this.$PC = 3;
                goto Label_02AD;
            Label_02A4:
                this.$PC = -1;
            Label_02AB:
                return 0;
            Label_02AD:
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

        [CompilerGenerated]
        private sealed class <RefreshTextLine>c__IteratorEF : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <hhh>__0;
            internal Text <text_obj>__1;
            internal string text;
            internal int <fsize>__2;
            internal float <linep>__3;
            internal int <line_num>__4;
            internal float <text_hhh>__5;
            internal VerticalLayoutGroup <vvv>__6;
            internal int $PC;
            internal object $current;
            internal string <$>text;
            internal ChatLogItem <>f__this;

            public <RefreshTextLine>c__IteratorEF()
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

            public unsafe bool MoveNext()
            {
                uint num;
                Vector2 vector;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_00B2;
                }
                goto Label_01C4;
            Label_0021:
                if ((this.<>f__this.mLogRoot == null) == null)
                {
                    goto Label_003C;
                }
                goto Label_01C4;
            Label_003C:
                if ((this.<>f__this.mMessageObj == null) == null)
                {
                    goto Label_0057;
                }
                goto Label_01C4;
            Label_0057:
                this.<hhh>__0 = 0;
                this.<text_obj>__1 = this.<>f__this.mMessageObj;
                this.<text_obj>__1.set_text(this.text);
                if ((this.<>f__this.Element == null) == null)
                {
                    goto Label_009B;
                }
                goto Label_01C4;
            Label_009B:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_01C6;
            Label_00B2:
                this.<fsize>__2 = this.<text_obj>__1.get_fontSize();
                this.<linep>__3 = this.<text_obj>__1.get_lineSpacing();
                this.<line_num>__4 = this.<text_obj>__1.get_cachedTextGeneratorForLayout().get_lineCount();
                this.<text_hhh>__5 = (((float) this.<fsize>__2) + this.<linep>__3) * ((float) this.<line_num>__4);
                this.<hhh>__0 += (int) this.<text_hhh>__5;
                this.<vvv>__6 = this.<>f__this.mLogRoot.GetComponent<VerticalLayoutGroup>();
                this.<hhh>__0 += this.<vvv>__6.get_padding().get_top();
                this.<hhh>__0 += this.<vvv>__6.get_padding().get_bottom();
                this.<hhh>__0 += (int) Mathf.Abs(&this.<>f__this.mLogRoot.get_anchoredPosition().y);
                this.<>f__this.Element.set_minHeight((float) this.<hhh>__0);
                this.<>f__this.mLogImg.set_enabled(1);
                this.$PC = -1;
            Label_01C4:
                return 0;
            Label_01C6:
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

