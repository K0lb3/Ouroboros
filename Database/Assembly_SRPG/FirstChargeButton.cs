namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(0, "Click FirstChargeEvent", 0, 0)]
    public class FirstChargeButton : AppealItemBase, IFlowInterface
    {
        private static readonly string SPRITES_PATH;
        private static readonly string MASTER_PATH;
        private static readonly string CHARGEINFO_PATH;
        private static readonly int INPUT_CLICK_CHARGE_EVENT;
        [SerializeField]
        private GameObject Badge;
        [SerializeField]
        private GameObject Ballon;
        [SerializeField]
        private bool IsDebug;
        private string m_CurrentAppealId;
        private int m_CurrentAppealIndex;
        private List<AppealChargeParam> m_ValidAppeal;
        private Dictionary<string, Sprite> mCacheAppealSprites;
        private bool m_IsSpriteLoaded;
        private GameObject m_ChargeInfoView;

        static FirstChargeButton()
        {
            SPRITES_PATH = "AppealSprites/charge";
            MASTER_PATH = "Data/appeal/AppealCharge";
            CHARGEINFO_PATH = "UI/ChargeInfo";
            return;
        }

        public FirstChargeButton()
        {
            this.m_CurrentAppealId = string.Empty;
            this.m_ValidAppeal = new List<AppealChargeParam>();
            this.mCacheAppealSprites = new Dictionary<string, Sprite>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != INPUT_CLICK_CHARGE_EVENT)
            {
                goto Label_0035;
            }
            if (this.IsDebug == null)
            {
                goto Label_0028;
            }
            base.StartCoroutine(this.CreateResultWindow());
            goto Label_0035;
        Label_0028:
            base.StartCoroutine(this.CreateInfoView());
        Label_0035:
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            if (this.IsDebug == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            base.get_gameObject().SetActive(0);
            return;
        }

        [DebuggerHidden]
        private IEnumerator CreateInfoView()
        {
            <CreateInfoView>c__Iterator108 iterator;
            iterator = new <CreateInfoView>c__Iterator108();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator CreateResultWindow()
        {
            <CreateResultWindow>c__Iterator10A iteratora;
            iteratora = new <CreateResultWindow>c__Iterator10A();
            return iteratora;
        }

        private void InitMaster()
        {
            if (this.m_ValidAppeal == null)
            {
                goto Label_003A;
            }
            if (this.m_ValidAppeal.Count <= 0)
            {
                goto Label_003A;
            }
            if (this.m_IsSpriteLoaded != null)
            {
                goto Label_0039;
            }
            base.StartCoroutine(this.LoadAppealSprite(SPRITES_PATH));
        Label_0039:
            return;
        Label_003A:
            if (this.LoadAppealMaster(MASTER_PATH) == null)
            {
                goto Label_0067;
            }
            if (this.m_IsSpriteLoaded != null)
            {
                goto Label_0067;
            }
            base.StartCoroutine(this.LoadAppealSprite(SPRITES_PATH));
        Label_0067:
            return;
        }

        private bool IsValidState(FirstChargeState _state)
        {
            return (((byte) MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus) == _state);
        }

        private bool LoadAppealMaster(string _path)
        {
            bool flag;
            string str;
            JSON_AppealChargeParam[] paramArray;
            long num;
            JSON_AppealChargeParam param;
            JSON_AppealChargeParam[] paramArray2;
            int num2;
            AppealChargeParam param2;
            Exception exception;
            flag = 0;
            if (string.IsNullOrEmpty(_path) != null)
            {
                goto Label_00A6;
            }
            str = AssetManager.LoadTextData(_path);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_00A6;
            }
        Label_001F:
            try
            {
                paramArray = JSONParser.parseJSONArray<JSON_AppealChargeParam>(str);
                if (paramArray != null)
                {
                    goto Label_0032;
                }
                throw new InvalidJSONException();
            Label_0032:
                num = Network.GetServerTime();
                paramArray2 = paramArray;
                num2 = 0;
                goto Label_0081;
            Label_0043:
                param = paramArray2[num2];
                param2 = new AppealChargeParam();
                param2.Deserialize(param);
                if (param2 == null)
                {
                    goto Label_007B;
                }
                if (param2.end_at < num)
                {
                    goto Label_007B;
                }
                this.m_ValidAppeal.Add(param2);
            Label_007B:
                num2 += 1;
            Label_0081:
                if (num2 < ((int) paramArray2.Length))
                {
                    goto Label_0043;
                }
                flag = 1;
                goto Label_00A6;
            }
            catch (Exception exception1)
            {
            Label_0093:
                exception = exception1;
                DebugUtility.LogError(exception.ToString());
                goto Label_00A6;
            }
        Label_00A6:
            return flag;
        }

        [DebuggerHidden]
        private IEnumerator LoadAppealSprite(string _path)
        {
            <LoadAppealSprite>c__Iterator109 iterator;
            iterator = new <LoadAppealSprite>c__Iterator109();
            iterator._path = _path;
            iterator.<$>_path = _path;
            iterator.<>f__this = this;
            return iterator;
        }

        private void OnEnable()
        {
            this.InitMaster();
            return;
        }

        protected override void Refresh()
        {
            if (MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus != -1)
            {
                goto Label_0016;
            }
            return;
        Label_0016:
            if (this.IsValidState(3) != null)
            {
                goto Label_002E;
            }
            if (this.IsValidState(0) == null)
            {
                goto Label_003B;
            }
        Label_002E:
            base.get_gameObject().SetActive(0);
            return;
        Label_003B:
            if ((this.Badge != null) == null)
            {
                goto Label_005E;
            }
            this.Badge.SetActive(this.IsValidState(2));
        Label_005E:
            if ((this.Ballon != null) == null)
            {
                goto Label_0084;
            }
            this.Ballon.SetActive(this.IsValidState(2) == 0);
        Label_0084:
            this.SetCurrentAppeal();
            base.Refresh();
            return;
        }

        private void SetCurrentAppeal()
        {
            long num;
            int num2;
            if (this.m_ValidAppeal == null)
            {
                goto Label_00BB;
            }
            num = Network.GetServerTime();
            num2 = 0;
            goto Label_006D;
        Label_0018:
            if (this.m_ValidAppeal[num2].start_at >= num)
            {
                goto Label_0069;
            }
            if (this.m_ValidAppeal[num2].end_at <= num)
            {
                goto Label_0069;
            }
            this.m_CurrentAppealId = this.m_ValidAppeal[num2].appeal_id;
            this.m_CurrentAppealIndex = num2;
            goto Label_007E;
        Label_0069:
            num2 += 1;
        Label_006D:
            if (num2 < this.m_ValidAppeal.Count)
            {
                goto Label_0018;
            }
        Label_007E:
            if (string.IsNullOrEmpty(this.m_CurrentAppealId) != null)
            {
                goto Label_00BB;
            }
            if (this.mCacheAppealSprites.ContainsKey(this.m_CurrentAppealId) == null)
            {
                goto Label_00BB;
            }
            base.AppealSprite = this.mCacheAppealSprites[this.m_CurrentAppealId];
        Label_00BB:
            return;
        }

        protected override void Start()
        {
            base.Start();
            if ((this.Ballon != null) == null)
            {
                goto Label_0028;
            }
            this.Ballon.get_gameObject().SetActive(0);
        Label_0028:
            if ((this.Badge != null) == null)
            {
                goto Label_004A;
            }
            this.Badge.get_gameObject().SetActive(0);
        Label_004A:
            this.InitMaster();
            return;
        }

        protected override void Update()
        {
            base.Update();
            if (this.m_IsSpriteLoaded == null)
            {
                goto Label_0017;
            }
            this.Refresh();
        Label_0017:
            return;
        }

        [CompilerGenerated]
        private sealed class <CreateInfoView>c__Iterator108 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal ChargeInfoWindow <window>__1;
            internal string <img_id>__2;
            internal int $PC;
            internal object $current;
            internal FirstChargeButton <>f__this;

            public <CreateInfoView>c__Iterator108()
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
                        goto Label_0087;

                    case 2:
                        goto Label_0185;
                }
                goto Label_018C;
            Label_0025:
                if (((this.<>f__this.m_ChargeInfoView == null) == null) || (string.IsNullOrEmpty(FirstChargeButton.CHARGEINFO_PATH) != null))
                {
                    goto Label_00C8;
                }
                this.<req>__0 = AssetManager.LoadAsync<GameObject>(FirstChargeButton.CHARGEINFO_PATH);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_0087;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_018E;
            Label_0087:
                if ((this.<req>__0 == null) || ((this.<req>__0.asset != null) == null))
                {
                    goto Label_00C8;
                }
                this.<>f__this.m_ChargeInfoView = Object.Instantiate(this.<req>__0.asset) as GameObject;
            Label_00C8:
                if ((this.<>f__this.m_ChargeInfoView != null) == null)
                {
                    goto Label_0172;
                }
                this.<window>__1 = this.<>f__this.m_ChargeInfoView.GetComponent<ChargeInfoWindow>();
                if ((this.<window>__1 != null) == null)
                {
                    goto Label_0172;
                }
                this.<img_id>__2 = (this.<>f__this.IsValidState(2) == null) ? this.<>f__this.m_ValidAppeal[this.<>f__this.m_CurrentAppealIndex].before_img : this.<>f__this.m_ValidAppeal[this.<>f__this.m_CurrentAppealIndex].after_img;
                this.<window>__1.Setup(this.<img_id>__2);
            Label_0172:
                this.$current = null;
                this.$PC = 2;
                goto Label_018E;
            Label_0185:
                this.$PC = -1;
            Label_018C:
                return 0;
            Label_018E:
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
        private sealed class <CreateResultWindow>c__Iterator10A : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal GameObject <window>__1;
            internal FirstChargeReward <reward0>__2;
            internal FirstChargeReward <reward1>__3;
            internal FirstChargeReward <reward2>__4;
            internal FirstChargeReward <reward3>__5;
            internal FirstChargeReward <reward4>__6;
            internal FirstChargeReward <reward5>__7;
            internal FirstChargeReward[] <rewards>__8;
            internal ChargeInfoResultWindow <charge>__9;
            internal int $PC;
            internal object $current;

            public <CreateResultWindow>c__Iterator10A()
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
                FirstChargeReward[] rewardArray1;
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_005E;
                }
                goto Label_01A6;
            Label_0021:
                this.<req>__0 = AssetManager.LoadAsync<GameObject>("UI/ChargeInfoResult_2");
                if (this.<req>__0.isDone != null)
                {
                    goto Label_005E;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_01A8;
            Label_005E:
                if (this.<req>__0 == null)
                {
                    goto Label_019F;
                }
                if ((this.<req>__0.asset != null) == null)
                {
                    goto Label_019F;
                }
                this.<window>__1 = Object.Instantiate(this.<req>__0.asset) as GameObject;
                this.<reward0>__2 = new FirstChargeReward("IT_PI_CLOE", 1L, 10);
                this.<reward1>__3 = new FirstChargeReward("UN_V2_CLOE", 0x80L, 1);
                this.<reward2>__4 = new FirstChargeReward(string.Empty, 2L, 0x3e8);
                this.<reward3>__5 = new FirstChargeReward(string.Empty, 4L, 0x3e8);
                this.<reward4>__6 = new FirstChargeReward("AF_ARMS_FF15_PROM_01", 0x40L, 1);
                this.<reward5>__7 = new FirstChargeReward("TS_ENVYRIA_VETTEL_01", 0x1000L, 1);
                rewardArray1 = new FirstChargeReward[] { this.<reward0>__2, this.<reward1>__3, this.<reward2>__4, this.<reward3>__5, this.<reward4>__6, this.<reward5>__7 };
                this.<rewards>__8 = rewardArray1;
                this.<charge>__9 = this.<window>__1.GetComponent<ChargeInfoResultWindow>();
                if ((this.<charge>__9 != null) == null)
                {
                    goto Label_0193;
                }
                this.<charge>__9.SetUp(this.<rewards>__8);
            Label_0193:
                this.<window>__1.SetActive(1);
            Label_019F:
                this.$PC = -1;
            Label_01A6:
                return 0;
            Label_01A8:
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
        private sealed class <LoadAppealSprite>c__Iterator109 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string _path;
            internal LoadRequest <req>__0;
            internal AppealSprites <sprites>__1;
            internal int <i>__2;
            internal Sprite <sprite>__3;
            internal int $PC;
            internal object $current;
            internal string <$>_path;
            internal FirstChargeButton <>f__this;

            public <LoadAppealSprite>c__Iterator109()
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
                        goto Label_01C3;
                }
                goto Label_01CA;
            Label_0025:
                if (string.IsNullOrEmpty(this._path) != null)
                {
                    goto Label_01B0;
                }
                this.<req>__0 = AssetManager.LoadAsync<AppealSprites>(this._path);
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
                goto Label_01CC;
            Label_007E:
                if ((this.<req>__0.asset != null) == null)
                {
                    goto Label_01B0;
                }
                this.<sprites>__1 = this.<req>__0.asset as AppealSprites;
                if ((this.<sprites>__1 != null) == null)
                {
                    goto Label_01B0;
                }
                if (this.<>f__this.m_ValidAppeal.Count <= 0)
                {
                    goto Label_01B0;
                }
                this.<i>__2 = 0;
                goto Label_0189;
            Label_00DD:
                this.<sprite>__3 = this.<sprites>__1.GetSprite(this.<>f__this.m_ValidAppeal[this.<i>__2].appeal_id);
                if ((this.<sprite>__3 != null) == null)
                {
                    goto Label_017B;
                }
                if (this.<>f__this.mCacheAppealSprites.ContainsKey(this.<>f__this.m_ValidAppeal[this.<i>__2].appeal_id) != null)
                {
                    goto Label_017B;
                }
                this.<>f__this.mCacheAppealSprites.Add(this.<>f__this.m_ValidAppeal[this.<i>__2].appeal_id, this.<sprite>__3);
            Label_017B:
                this.<i>__2 += 1;
            Label_0189:
                if (this.<i>__2 < this.<>f__this.m_ValidAppeal.Count)
                {
                    goto Label_00DD;
                }
                this.<>f__this.m_IsSpriteLoaded = 1;
            Label_01B0:
                this.$current = null;
                this.$PC = 2;
                goto Label_01CC;
            Label_01C3:
                this.$PC = -1;
            Label_01CA:
                return 0;
            Label_01CC:
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

