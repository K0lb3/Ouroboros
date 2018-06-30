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

    [Pin(5, "BuyCoin", 1, 5), Pin(4, "TowerQuest", 1, 4), Pin(3, "EventQuest", 1, 3), Pin(2, "LimitedShop", 1, 2), Pin(1, "Gacha", 1, 1), Pin(0, "None", 1, 0)]
    public class LoginInfoWindow : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private Button Move;
        [SerializeField]
        private Text MoveBtnText;
        [SerializeField]
        private Image InfoImage;
        [SerializeField]
        private Toggle CheckToggle;
        [SerializeField]
        private Button CloseBtn;
        private LoginInfoParam.SelectScene mSelectScene;
        private bool mLoaded;
        private bool mRefresh;

        public LoginInfoWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void Awake()
        {
            if ((this.Move != null) == null)
            {
                goto Label_001D;
            }
            this.Move.set_interactable(0);
        Label_001D:
            if ((this.InfoImage != null) == null)
            {
                goto Label_003F;
            }
            this.InfoImage.get_gameObject().SetActive(0);
        Label_003F:
            if ((this.CheckToggle != null) == null)
            {
                goto Label_006C;
            }
            this.CheckToggle.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnValueChange));
        Label_006C:
            return;
        }

        [DebuggerHidden]
        private IEnumerator LoadImages(string path, string img)
        {
            <LoadImages>c__Iterator124 iterator;
            iterator = new <LoadImages>c__Iterator124();
            iterator.path = path;
            iterator.img = img;
            iterator.<$>path = path;
            iterator.<$>img = img;
            iterator.<>f__this = this;
            return iterator;
        }

        private void OnMoveScene()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, this.mSelectScene);
            return;
        }

        private unsafe void OnValueChange(bool value)
        {
            string str;
            DateTime time;
            str = string.Empty;
            if (value == null)
            {
                goto Label_001F;
            }
            str = &TimeManager.ServerTime.ToString("yyyy/MM/dd");
        Label_001F:
            GameUtility.setLoginInfoRead(str);
            return;
        }

        private void Refresh()
        {
            if ((this.Move != null) == null)
            {
                goto Label_011D;
            }
            if ((this.MoveBtnText != null) == null)
            {
                goto Label_011D;
            }
            if (this.mSelectScene != 1)
            {
                goto Label_0048;
            }
            this.MoveBtnText.set_text(LocalizedText.Get("sys.TEXT_LOGININFO_GACHA"));
            goto Label_00F5;
        Label_0048:
            if (this.mSelectScene != 2)
            {
                goto Label_006E;
            }
            this.MoveBtnText.set_text(LocalizedText.Get("sys.TEXT_LOGININFO_LIMITEDSHOP"));
            goto Label_00F5;
        Label_006E:
            if (this.mSelectScene != 3)
            {
                goto Label_0094;
            }
            this.MoveBtnText.set_text(LocalizedText.Get("sys.TEXT_LOGININFO_EVENTQUEST"));
            goto Label_00F5;
        Label_0094:
            if (this.mSelectScene != 4)
            {
                goto Label_00BA;
            }
            this.MoveBtnText.set_text(LocalizedText.Get("sys.TEXT_LOGININFO_TOWERQUEST"));
            goto Label_00F5;
        Label_00BA:
            if (this.mSelectScene != 5)
            {
                goto Label_00E0;
            }
            this.MoveBtnText.set_text(LocalizedText.Get("sys.TEXT_LOGININFO_BUYCOIN"));
            goto Label_00F5;
        Label_00E0:
            this.MoveBtnText.set_text(LocalizedText.Get("sys.OK"));
        Label_00F5:
            this.Move.get_onClick().AddListener(new UnityAction(this, this.OnMoveScene));
            this.Move.set_interactable(1);
        Label_011D:
            if ((this.InfoImage != null) == null)
            {
                goto Label_014F;
            }
            this.InfoImage.get_gameObject().SetActive(this.InfoImage.get_sprite() != null);
        Label_014F:
            return;
        }

        private void Start()
        {
            char[] chArray1;
            LoginInfoParam[] paramArray;
            int num;
            string str;
            string[] strArray;
            paramArray = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetActiveLoginInfos();
            if (paramArray == null)
            {
                goto Label_001F;
            }
            if (((int) paramArray.Length) > 0)
            {
                goto Label_0027;
            }
        Label_001F:
            FlowNode_GameObject.ActivateOutputLinks(this, 0);
            return;
        Label_0027:
            num = Random.Range(0, (int) paramArray.Length);
            this.mSelectScene = paramArray[num].scene;
            chArray1 = new char[] { 0x2f };
            strArray = paramArray[num].path.Split(chArray1);
            if (strArray == null)
            {
                goto Label_007C;
            }
            if (((int) strArray.Length) < 2)
            {
                goto Label_007C;
            }
            base.StartCoroutine(this.LoadImages(strArray[0], strArray[1]));
        Label_007C:
            return;
        }

        private void Update()
        {
            if (this.mLoaded == null)
            {
                goto Label_0023;
            }
            if (this.mRefresh != null)
            {
                goto Label_0023;
            }
            this.mRefresh = 1;
            this.Refresh();
        Label_0023:
            return;
        }

        [CompilerGenerated]
        private sealed class <LoadImages>c__Iterator124 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string path;
            internal string img;
            internal LoadRequest <request>__0;
            internal SpriteSheet <sheet>__1;
            internal Sprite <sprite>__2;
            internal int $PC;
            internal object $current;
            internal string <$>path;
            internal string <$>img;
            internal LoginInfoWindow <>f__this;

            public <LoadImages>c__Iterator124()
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
                        goto Label_007D;

                    case 2:
                        goto Label_0101;
                }
                goto Label_0108;
            Label_0025:
                if (string.IsNullOrEmpty(this.path) != null)
                {
                    goto Label_00E2;
                }
                if (string.IsNullOrEmpty(this.img) != null)
                {
                    goto Label_00E2;
                }
                this.<request>__0 = AssetManager.LoadAsync<SpriteSheet>("LoginInfo/" + this.path);
                this.$current = this.<request>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_010A;
            Label_007D:
                this.<sheet>__1 = this.<request>__0.asset as SpriteSheet;
                if ((this.<sheet>__1 != null) == null)
                {
                    goto Label_00E2;
                }
                this.<sprite>__2 = this.<sheet>__1.GetSprite(this.img);
                if ((this.<sprite>__2 != null) == null)
                {
                    goto Label_00E2;
                }
                this.<>f__this.InfoImage.set_sprite(this.<sprite>__2);
            Label_00E2:
                this.<>f__this.mLoaded = 1;
                this.$current = null;
                this.$PC = 2;
                goto Label_010A;
            Label_0101:
                this.$PC = -1;
            Label_0108:
                return 0;
            Label_010A:
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

        public enum SelectScene : byte
        {
            None = 0,
            Gacha = 1,
            LimitedShop = 2,
            EventQuest = 3,
            TowerQuest = 4,
            BuyShop = 5
        }
    }
}

