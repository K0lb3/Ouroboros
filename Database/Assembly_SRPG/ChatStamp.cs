namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0, "SetStampId", 1, 0)]
    public class ChatStamp : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private Transform StampListRoot;
        [SerializeField]
        private Button NextPage;
        [SerializeField]
        private Button PrevPage;
        private GameObject[] mStampObjects;
        private ChatStampParam[] mStampParams;
        public static readonly string CHAT_STAMP_MASTER_PATH;
        public static readonly string CHAT_STAMP_IMAGE_PATH;
        public static readonly int STAMP_VIEW_MAX;
        private int MaxPage;
        private int mCurrentPageIndex;
        private bool IsRefresh;
        private bool IsSending;
        private int mPrevSelectId;
        private int mPrevSelectIndex;
        private SpriteSheet mStampImages;
        private bool IsImageLoaded;
        private bool mImageLoaded;

        static ChatStamp()
        {
            CHAT_STAMP_MASTER_PATH = "Data/Stamp";
            CHAT_STAMP_IMAGE_PATH = "Stamps/StampTable";
            STAMP_VIEW_MAX = 6;
            return;
        }

        public ChatStamp()
        {
            this.mPrevSelectId = -1;
            this.mPrevSelectIndex = -1;
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void Awake()
        {
            if ((this.NextPage != null) == null)
            {
                goto Label_0039;
            }
            this.NextPage.set_interactable(0);
            this.NextPage.get_onClick().AddListener(new UnityAction(this, this.OnNext));
        Label_0039:
            if ((this.PrevPage != null) == null)
            {
                goto Label_0072;
            }
            this.PrevPage.set_interactable(0);
            this.PrevPage.get_onClick().AddListener(new UnityAction(this, this.OnPrev));
        Label_0072:
            return;
        }

        [DebuggerHidden]
        private IEnumerator LoadStampImages()
        {
            <LoadStampImages>c__IteratorF1 rf;
            rf = new <LoadStampImages>c__IteratorF1();
            rf.<>f__this = this;
            return rf;
        }

        private void OnDisable()
        {
            int num;
            if (this.mStampObjects == null)
            {
                goto Label_0066;
            }
            if (((int) this.mStampObjects.Length) <= 0)
            {
                goto Label_0066;
            }
            num = 0;
            goto Label_0058;
        Label_0020:
            this.mStampObjects[num].get_transform().set_localScale(new Vector3(0.7f, 0.7f, 0.7f));
            this.mStampObjects[num].SetActive(0);
            num += 1;
        Label_0058:
            if (num < ((int) this.mStampObjects.Length))
            {
                goto Label_0020;
            }
        Label_0066:
            this.mPrevSelectId = -1;
            this.mPrevSelectIndex = -1;
            return;
        }

        private void OnEnable()
        {
            this.IsRefresh = 0;
            return;
        }

        private void OnNext()
        {
            this.mCurrentPageIndex = Mathf.Min(this.mCurrentPageIndex + 1, this.MaxPage);
            this.mPrevSelectId = -1;
            this.mPrevSelectIndex = -1;
            this.RefreshPager();
            this.IsRefresh = 0;
            return;
        }

        private void OnPrev()
        {
            this.mCurrentPageIndex = Mathf.Max(this.mCurrentPageIndex - 1, 0);
            this.mPrevSelectId = -1;
            this.mPrevSelectIndex = -1;
            this.RefreshPager();
            this.IsRefresh = 0;
            return;
        }

        private unsafe void OnTapStamp(int id, int index)
        {
            Transform transform;
            Transform transform2;
            if (id != this.mPrevSelectId)
            {
                goto Label_0033;
            }
            this.mPrevSelectId = -1;
            this.mPrevSelectIndex = -1;
            FlowNode_Variable.Set("SELECT_STAMP_ID", &id.ToString());
            FlowNode_GameObject.ActivateOutputLinks(this, 0);
            return;
        Label_0033:
            if (this.mPrevSelectId == -1)
            {
                goto Label_0084;
            }
            if (this.mPrevSelectIndex == -1)
            {
                goto Label_0084;
            }
            transform = this.mStampObjects[this.mPrevSelectIndex].get_transform();
            if ((transform != null) == null)
            {
                goto Label_0084;
            }
            transform.set_localScale(new Vector3(0.7f, 0.7f, 0.7f));
        Label_0084:
            this.mPrevSelectId = id;
            this.mPrevSelectIndex = index;
            transform2 = this.mStampObjects[index].get_transform();
            if ((transform2 != null) == null)
            {
                goto Label_00C6;
            }
            transform2.set_localScale(new Vector3(1f, 1f, 1f));
        Label_00C6:
            return;
        }

        private void Refresh()
        {
            int num;
            Transform transform;
            int num2;
            int num3;
            int num4;
            ChatStampParam param;
            GameObject obj2;
            Sprite sprite;
            Image image;
            Button button;
            <Refresh>c__AnonStorey314 storey;
            if ((this.mStampParams != null) && (((int) this.mStampParams.Length) > 0))
            {
                goto Label_001A;
            }
            return;
        Label_001A:
            if (((this.mStampObjects != null) && (((int) this.mStampObjects.Length) > 0)) || (((this.StampListRoot != null) == null) || (this.StampListRoot.get_childCount() <= 0)))
            {
                goto Label_00BA;
            }
            this.mStampObjects = new GameObject[this.StampListRoot.get_childCount()];
            num = 0;
            goto Label_00A9;
        Label_0072:
            transform = this.StampListRoot.GetChild(num);
            if ((transform != null) == null)
            {
                goto Label_00A5;
            }
            this.mStampObjects[num] = transform.get_gameObject();
            transform.get_gameObject().SetActive(0);
        Label_00A5:
            num += 1;
        Label_00A9:
            if (num < this.StampListRoot.get_childCount())
            {
                goto Label_0072;
            }
        Label_00BA:
            if ((this.mStampObjects == null) || (((int) this.mStampObjects.Length) <= 0))
            {
                goto Label_0235;
            }
            num2 = 0;
            goto Label_0112;
        Label_00DA:
            this.mStampObjects[num2].get_transform().set_localScale(new Vector3(0.7f, 0.7f, 0.7f));
            this.mStampObjects[num2].SetActive(0);
            num2 += 1;
        Label_0112:
            if (num2 < ((int) this.mStampObjects.Length))
            {
                goto Label_00DA;
            }
            num3 = STAMP_VIEW_MAX * this.mCurrentPageIndex;
            num4 = 0;
            goto Label_0226;
        Label_0135:
            if ((num3 + num4) < ((int) this.mStampParams.Length))
            {
                goto Label_014B;
            }
            goto Label_0235;
        Label_014B:
            param = this.mStampParams[num3 + num4];
            if (param == null)
            {
                goto Label_0220;
            }
            storey = new <Refresh>c__AnonStorey314();
            storey.<>f__this = this;
            storey.index = num4;
            storey.id = param.id;
            obj2 = this.mStampObjects[num4];
            sprite = this.mStampImages.GetSprite(param.img_id);
            image = obj2.GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_01D6;
            }
            image.set_sprite(((sprite != null) == null) ? null : sprite);
        Label_01D6:
            button = this.mStampObjects[num4].GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_0218;
            }
            button.get_onClick().RemoveAllListeners();
            button.get_onClick().AddListener(new UnityAction(storey, this.<>m__2B8));
        Label_0218:
            obj2.SetActive(1);
        Label_0220:
            num4 += 1;
        Label_0226:
            if (num4 < ((int) this.mStampObjects.Length))
            {
                goto Label_0135;
            }
        Label_0235:
            return;
        }

        private void RefreshPager()
        {
            if (this.MaxPage != null)
            {
                goto Label_0017;
            }
            if (this.mCurrentPageIndex != null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            if ((this.NextPage != null) == null)
            {
                goto Label_0043;
            }
            this.NextPage.set_interactable(this.MaxPage > (this.mCurrentPageIndex + 1));
        Label_0043:
            if ((this.PrevPage != null) == null)
            {
                goto Label_006D;
            }
            this.PrevPage.set_interactable((0 > (this.mCurrentPageIndex - 1)) == 0);
        Label_006D:
            return;
        }

        private bool SetupChatStampMaster()
        {
            string str;
            JSON_ChatStampParam[] paramArray;
            int num;
            ChatStampParam param;
            bool flag;
            str = AssetManager.LoadTextData(CHAT_STAMP_MASTER_PATH);
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
                this.MaxPage = ((((int) paramArray.Length) % STAMP_VIEW_MAX) <= 0) ? (((int) paramArray.Length) / STAMP_VIEW_MAX) : ((((int) paramArray.Length) / STAMP_VIEW_MAX) + 1);
                goto Label_00AB;
            }
            catch
            {
            Label_009D:
                flag = 0;
                goto Label_00AD;
            }
        Label_00AB:
            return 1;
        Label_00AD:
            return flag;
        }

        private void Start()
        {
            this.SetupChatStampMaster();
            if (this.mStampParams == null)
            {
                goto Label_0053;
            }
            this.MaxPage = ((int) this.mStampParams.Length) / STAMP_VIEW_MAX;
            this.MaxPage = ((((int) this.mStampParams.Length) % STAMP_VIEW_MAX) <= 0) ? this.MaxPage : (this.MaxPage + 1);
        Label_0053:
            this.mCurrentPageIndex = 0;
            this.RefreshPager();
            if ((this.mStampImages == null) == null)
            {
                goto Label_0089;
            }
            if (this.mImageLoaded != null)
            {
                goto Label_0089;
            }
            base.StartCoroutine(this.LoadStampImages());
        Label_0089:
            return;
        }

        private void Update()
        {
            if (this.IsRefresh != null)
            {
                goto Label_0023;
            }
            if (this.IsImageLoaded == null)
            {
                goto Label_0023;
            }
            this.IsRefresh = 1;
            this.Refresh();
        Label_0023:
            return;
        }

        [CompilerGenerated]
        private sealed class <LoadStampImages>c__IteratorF1 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <request>__0;
            internal SpriteSheet <sheet>__1;
            internal int $PC;
            internal object $current;
            internal ChatStamp <>f__this;

            public <LoadStampImages>c__IteratorF1()
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
                        goto Label_006D;

                    case 2:
                        goto Label_00C4;
                }
                goto Label_00CB;
            Label_0025:
                this.<>f__this.mImageLoaded = 1;
                if (string.IsNullOrEmpty(ChatStamp.CHAT_STAMP_IMAGE_PATH) != null)
                {
                    goto Label_00B1;
                }
                this.<request>__0 = AssetManager.LoadAsync<SpriteSheet>(ChatStamp.CHAT_STAMP_IMAGE_PATH);
                this.$current = this.<request>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_00CD;
            Label_006D:
                this.<sheet>__1 = this.<request>__0.asset as SpriteSheet;
                if ((this.<sheet>__1 != null) == null)
                {
                    goto Label_00A5;
                }
                this.<>f__this.mStampImages = this.<sheet>__1;
            Label_00A5:
                this.<>f__this.IsImageLoaded = 1;
            Label_00B1:
                this.$current = null;
                this.$PC = 2;
                goto Label_00CD;
            Label_00C4:
                this.$PC = -1;
            Label_00CB:
                return 0;
            Label_00CD:
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
        private sealed class <Refresh>c__AnonStorey314
        {
            internal int id;
            internal int index;
            internal ChatStamp <>f__this;

            public <Refresh>c__AnonStorey314()
            {
                base..ctor();
                return;
            }

            internal void <>m__2B8()
            {
                this.<>f__this.OnTapStamp(this.id, this.index);
                return;
            }
        }
    }
}

