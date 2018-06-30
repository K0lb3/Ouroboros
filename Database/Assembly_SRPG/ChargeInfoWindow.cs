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

    [Pin(0, "ClickBuyCoin", 0, 0), Pin(11, "ToResult", 1, 11), Pin(10, "ToBuyCoin", 1, 10), Pin(1, "ClickResult", 0, 1)]
    public class ChargeInfoWindow : MonoBehaviour, IFlowInterface
    {
        private const int INPUT_CLICK_BUYCOIN = 0;
        private const int INPUT_CLICK_RESULT = 1;
        private const int OUTPUT_TO_BUYCOIN = 10;
        private const int OUTPUT_TO_RESULT = 11;
        private static readonly string SPRITE_SHEET_PATH;
        private static readonly string CHARGERESULT_PATH;
        [SerializeField]
        private GameObject AppealObject;
        [SerializeField]
        private GameObject MoveBuyButton;
        [SerializeField]
        private GameObject MoveResultButton;
        private string m_CurrentAppealImg;
        private bool m_loaded;
        private bool m_Refresh;
        private Sprite m_CacheAppealSprite;

        static ChargeInfoWindow()
        {
            SPRITE_SHEET_PATH = "ChargeInfo/ChargeInfo";
            CHARGERESULT_PATH = "UI/ChargeInfoResult";
            return;
        }

        public ChargeInfoWindow()
        {
            this.m_CurrentAppealImg = string.Empty;
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0013;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            goto Label_0027;
        Label_0013:
            if (pinID != 1)
            {
                goto Label_0027;
            }
            base.StartCoroutine(this.CreateResultView());
        Label_0027:
            return;
        }

        [DebuggerHidden]
        private IEnumerator CreateResultView()
        {
            <CreateResultView>c__IteratorED red;
            red = new <CreateResultView>c__IteratorED();
            red.<>f__this = this;
            return red;
        }

        [DebuggerHidden]
        private IEnumerator LoadImages(string _path)
        {
            <LoadImages>c__IteratorEE ree;
            ree = new <LoadImages>c__IteratorEE();
            ree._path = _path;
            ree.<$>_path = _path;
            ree.<>f__this = this;
            return ree;
        }

        private void Refresh()
        {
            Image image;
            FirstChargeState state;
            if ((this.AppealObject != null) == null)
            {
                goto Label_0035;
            }
            image = this.AppealObject.GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_0035;
            }
            image.set_sprite(this.m_CacheAppealSprite);
        Label_0035:
            state = (byte) MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus;
            if ((this.MoveResultButton != null) == null)
            {
                goto Label_0066;
            }
            this.MoveResultButton.SetActive(state == 2);
        Label_0066:
            if ((this.MoveBuyButton != null) == null)
            {
                goto Label_0086;
            }
            this.MoveBuyButton.SetActive(state == 1);
        Label_0086:
            return;
        }

        public void Setup(string _img_id)
        {
            if (string.IsNullOrEmpty(_img_id) == null)
            {
                goto Label_0016;
            }
            DebugUtility.LogError("初回購入キャンペーンの有効な訴求IDがありません.");
            return;
        Label_0016:
            this.m_CurrentAppealImg = _img_id;
            base.StartCoroutine(this.LoadImages(SPRITE_SHEET_PATH));
            return;
        }

        private void Start()
        {
            if ((this.MoveBuyButton != null) == null)
            {
                goto Label_001D;
            }
            this.MoveBuyButton.SetActive(0);
        Label_001D:
            if ((this.MoveResultButton != null) == null)
            {
                goto Label_003A;
            }
            this.MoveResultButton.SetActive(0);
        Label_003A:
            return;
        }

        private void Update()
        {
            if (this.m_loaded == null)
            {
                goto Label_0023;
            }
            if (this.m_Refresh != null)
            {
                goto Label_0023;
            }
            this.m_Refresh = 1;
            this.Refresh();
        Label_0023:
            return;
        }

        [CompilerGenerated]
        private sealed class <CreateResultView>c__IteratorED : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal GameObject <window>__1;
            internal int $PC;
            internal object $current;
            internal ChargeInfoWindow <>f__this;

            public <CreateResultView>c__IteratorED()
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
                        goto Label_0071;

                    case 2:
                        goto Label_00D9;
                }
                goto Label_00E0;
            Label_0025:
                if (string.IsNullOrEmpty(ChargeInfoWindow.CHARGERESULT_PATH) != null)
                {
                    goto Label_00B9;
                }
                this.<req>__0 = AssetManager.LoadAsync<GameObject>(ChargeInfoWindow.CHARGERESULT_PATH);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_0071;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_00E2;
            Label_0071:
                if (this.<req>__0 == null)
                {
                    goto Label_00B9;
                }
                if ((this.<req>__0.asset != null) == null)
                {
                    goto Label_00B9;
                }
                this.<window>__1 = Object.Instantiate(this.<req>__0.asset) as GameObject;
                this.<window>__1.SetActive(1);
            Label_00B9:
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 11);
                this.$current = null;
                this.$PC = 2;
                goto Label_00E2;
            Label_00D9:
                this.$PC = -1;
            Label_00E0:
                return 0;
            Label_00E2:
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
        private sealed class <LoadImages>c__IteratorEE : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string _path;
            internal LoadRequest <req>__0;
            internal SpriteSheet <sheets>__1;
            internal Sprite <sprite>__2;
            internal int $PC;
            internal object $current;
            internal string <$>_path;
            internal ChargeInfoWindow <>f__this;

            public <LoadImages>c__IteratorEE()
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
                        goto Label_007E;

                    case 2:
                        goto Label_0138;
                }
                goto Label_013F;
            Label_0025:
                if (string.IsNullOrEmpty(this._path) != null)
                {
                    goto Label_0125;
                }
                this.<req>__0 = AssetManager.LoadAsync<SpriteSheet>(this._path);
                if (this.<req>__0 == null)
                {
                    goto Label_007E;
                }
                if (this.<req>__0.isDone != null)
                {
                    goto Label_007E;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_0141;
            Label_007E:
                if (this.<req>__0 == null)
                {
                    goto Label_0125;
                }
                if ((this.<req>__0.asset != null) == null)
                {
                    goto Label_0125;
                }
                this.<sheets>__1 = this.<req>__0.asset as SpriteSheet;
                if ((this.<sheets>__1 != null) == null)
                {
                    goto Label_0125;
                }
                if (string.IsNullOrEmpty(this.<>f__this.m_CurrentAppealImg) != null)
                {
                    goto Label_0125;
                }
                this.<sprite>__2 = this.<sheets>__1.GetSprite(this.<>f__this.m_CurrentAppealImg);
                if ((this.<sprite>__2 != null) == null)
                {
                    goto Label_0125;
                }
                this.<>f__this.m_CacheAppealSprite = this.<sprite>__2;
                this.<>f__this.m_loaded = 1;
            Label_0125:
                this.$current = null;
                this.$PC = 2;
                goto Label_0141;
            Label_0138:
                this.$PC = -1;
            Label_013F:
                return 0;
            Label_0141:
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

