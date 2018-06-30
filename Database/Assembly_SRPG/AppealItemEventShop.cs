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

    public class AppealItemEventShop : AppealItemBase
    {
        private readonly string SPRITES_PATH;
        private readonly string MASTER_PATH;
        [SerializeField]
        private Image AppealChara;
        [SerializeField]
        private RectTransform AppealCharaRect;
        [SerializeField]
        private Image AppealTextL;
        [SerializeField]
        private Image AppealTextR;
        [SerializeField]
        private RectTransform AppealTextRect;
        [SerializeField]
        private SRPG_Button EventShopButton;
        private string mAppealID;
        private float mPosX_Chara;
        private float mPosX_Text;
        private bool IsLoaded;
        private bool IsInitalize;
        private Sprite CharaSprite;
        private Sprite TextLSprite;
        private Sprite TextRSprite;

        public AppealItemEventShop()
        {
            this.SPRITES_PATH = "AppealSprites/eventshop";
            this.MASTER_PATH = "Data/appeal/AppealEventShop";
            base..ctor();
            return;
        }

        protected override unsafe void Awake()
        {
            Vector2 vector;
            Vector2 vector2;
            if ((this.AppealChara != null) == null)
            {
                goto Label_0022;
            }
            this.AppealChara.get_gameObject().SetActive(0);
        Label_0022:
            if ((this.AppealTextL != null) == null)
            {
                goto Label_0044;
            }
            this.AppealTextL.get_gameObject().SetActive(0);
        Label_0044:
            if ((this.AppealTextR != null) == null)
            {
                goto Label_0066;
            }
            this.AppealTextR.get_gameObject().SetActive(0);
        Label_0066:
            if ((this.AppealCharaRect != null) == null)
            {
                goto Label_0090;
            }
            this.mPosX_Chara = &this.AppealCharaRect.get_anchoredPosition().x;
        Label_0090:
            if ((this.AppealTextRect != null) == null)
            {
                goto Label_00BA;
            }
            this.mPosX_Text = &this.AppealTextRect.get_anchoredPosition().x;
        Label_00BA:
            return;
        }

        protected override void Destroy()
        {
            base.Destroy();
            return;
        }

        private bool LoadAppealMaster(string path)
        {
            string str;
            JSON_AppealEventShopMaster[] masterArray;
            long num;
            AppealEventShopMaster master;
            JSON_AppealEventShopMaster master2;
            JSON_AppealEventShopMaster[] masterArray2;
            int num2;
            AppealEventShopMaster master3;
            Exception exception;
            bool flag;
            if (string.IsNullOrEmpty(path) == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            str = AssetManager.LoadTextData(path);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0021;
            }
            return 0;
        Label_0021:
            try
            {
                masterArray = JSONParser.parseJSONArray<JSON_AppealEventShopMaster>(str);
                if (masterArray != null)
                {
                    goto Label_0034;
                }
                throw new InvalidJSONException();
            Label_0034:
                num = Network.GetServerTime();
                master = new AppealEventShopMaster();
                masterArray2 = masterArray;
                num2 = 0;
                goto Label_00AA;
            Label_004B:
                master2 = masterArray2[num2];
                master3 = new AppealEventShopMaster();
                if (master3.Deserialize(master2) == null)
                {
                    goto Label_00A4;
                }
                if (master3.start_at > num)
                {
                    goto Label_00A4;
                }
                if (master3.end_at <= num)
                {
                    goto Label_00A4;
                }
                if (master != null)
                {
                    goto Label_008F;
                }
                master = master3;
                goto Label_00A4;
            Label_008F:
                if (master.priority >= master3.priority)
                {
                    goto Label_00A4;
                }
                master = master3;
            Label_00A4:
                num2 += 1;
            Label_00AA:
                if (num2 < ((int) masterArray2.Length))
                {
                    goto Label_004B;
                }
                if (master == null)
                {
                    goto Label_00DF;
                }
                this.mAppealID = master.appeal_id;
                this.mPosX_Chara = master.position_chara;
                this.mPosX_Text = master.position_text;
            Label_00DF:
                goto Label_00FA;
            }
            catch (Exception exception1)
            {
            Label_00E4:
                exception = exception1;
                DebugUtility.LogException(exception);
                flag = 0;
                goto Label_00FC;
            }
        Label_00FA:
            return 1;
        Label_00FC:
            return flag;
        }

        [DebuggerHidden]
        private IEnumerator LoadAppealResources(string path)
        {
            <LoadAppealResources>c__IteratorDF rdf;
            rdf = new <LoadAppealResources>c__IteratorDF();
            rdf.path = path;
            rdf.<$>path = path;
            rdf.<>f__this = this;
            return rdf;
        }

        protected override unsafe void Refresh()
        {
            Vector2 vector;
            Vector2 vector2;
            this.AppealChara.get_gameObject().SetActive(((this.AppealChara != null) == null) ? 0 : (this.CharaSprite != null));
            this.AppealTextL.get_gameObject().SetActive(((this.AppealTextL != null) == null) ? 0 : (this.TextLSprite != null));
            this.AppealTextR.get_gameObject().SetActive(((this.AppealTextR != null) == null) ? 0 : (this.TextRSprite != null));
            this.AppealChara.set_sprite(this.CharaSprite);
            this.AppealTextL.set_sprite(this.TextLSprite);
            this.AppealTextR.set_sprite(this.TextRSprite);
            this.AppealCharaRect.set_anchoredPosition(new Vector2(this.mPosX_Chara, &this.AppealCharaRect.get_anchoredPosition().y));
            this.AppealTextRect.set_anchoredPosition(new Vector2(this.mPosX_Text, &this.AppealTextRect.get_anchoredPosition().y));
            this.EventShopButton.set_interactable(1);
            this.IsInitalize = 1;
            return;
        }

        protected override void Start()
        {
            if (this.LoadAppealMaster(this.MASTER_PATH) == null)
            {
                goto Label_0024;
            }
            base.StartCoroutine(this.LoadAppealResources(this.SPRITES_PATH));
        Label_0024:
            GlobalVars.IsEventShopOpen.Set(string.IsNullOrEmpty(this.mAppealID) == 0);
            return;
        }

        protected override void Update()
        {
            if (this.IsLoaded == null)
            {
                goto Label_001C;
            }
            if (this.IsInitalize != null)
            {
                goto Label_001C;
            }
            this.Refresh();
        Label_001C:
            return;
        }

        [CompilerGenerated]
        private sealed class <LoadAppealResources>c__IteratorDF : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string path;
            internal AppealBallonSprites <sprites>__0;
            internal LoadRequest <resources>__1;
            internal int $PC;
            internal object $current;
            internal string <$>path;
            internal AppealItemEventShop <>f__this;

            public <LoadAppealResources>c__IteratorDF()
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
                        goto Label_006A;

                    case 2:
                        goto Label_0128;
                }
                goto Label_012F;
            Label_0025:
                if (string.IsNullOrEmpty(this.path) != null)
                {
                    goto Label_0115;
                }
                this.<sprites>__0 = null;
                this.<resources>__1 = AssetManager.LoadAsync<AppealBallonSprites>(this.path);
                this.$current = this.<resources>__1.StartCoroutine();
                this.$PC = 1;
                goto Label_0131;
            Label_006A:
                this.<sprites>__0 = this.<resources>__1.asset as AppealBallonSprites;
                if ((this.<sprites>__0 != null) == null)
                {
                    goto Label_0115;
                }
                if (string.IsNullOrEmpty(this.<>f__this.mAppealID) != null)
                {
                    goto Label_0115;
                }
                this.<>f__this.CharaSprite = this.<sprites>__0.GetSprite(this.<>f__this.mAppealID);
                this.<>f__this.TextLSprite = this.<sprites>__0.GetSpriteTextL(this.<>f__this.mAppealID);
                this.<>f__this.TextRSprite = this.<sprites>__0.GetSpriteTextR(this.<>f__this.mAppealID);
                this.<>f__this.IsLoaded = 1;
            Label_0115:
                this.$current = null;
                this.$PC = 2;
                goto Label_0131;
            Label_0128:
                this.$PC = -1;
            Label_012F:
                return 0;
            Label_0131:
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

