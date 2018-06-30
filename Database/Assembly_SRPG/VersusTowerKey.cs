namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "鍵の更新", 0, 0), Pin(100, "演出終了", 1, 100), Pin(3, "到達報酬", 0, 0), Pin(2, "階層更新", 0, 0)]
    public class VersusTowerKey : MonoBehaviour, IFlowInterface
    {
        public Text floortxt;
        public Text floorEfftxt;
        public Text okText;
        public Text arrivalNumText;
        public Text arrivalEffNumText;
        public Text arrivalRewardText;
        public GameObject key;
        public GameObject parent;
        public GameObject floorObj;
        public GameObject rewardObj;
        public GameObject unitObj;
        public GameObject itemRoot;
        public GameObject arrivalObj;
        public GameObject keyRoot;
        public GameObject rewardRoot;
        public GameObject reglegationRoot;
        public GameObject ArrivalInfoRoot;
        public GameObject ClearInfoRoot;
        public GameObject RightRoot;
        public GameObject infoText;
        public Text infoLastText;
        public Image frameObj;
        public Sprite coinBase;
        public Sprite goldBase;
        public RawImage rewardTex;
        public string keyGetAnim;
        public string keyDefAnim;
        public string keyLostAnim;
        public string updateFloorAnim;
        public string downFloorAnim;
        public string rewardGetAnim;
        public string trriggerIn;
        public string trriggerOut;
        public string trriggerRewardIn;
        public string trriggerInLastFloor;
        public string trriggerInLastFloorOut;
        public Color rankDownColor;
        public Texture CoinTex;
        public Texture GoldTex;
        private KEY_RESULT_STATE mState;
        private RESULT mBattleRes;
        private bool mUpdateAnim;
        private bool mUpdateFloor;
        private int mAnimKeyIndex;
        private int mMaxKeyCount;
        private List<GameObject> mCreateKey;

        public VersusTowerKey()
        {
            this.mCreateKey = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_005C;
            }
            this.mUpdateAnim = 1;
            this.mState = 1;
            if (this.mBattleRes != null)
            {
                goto Label_0032;
            }
            base.StartCoroutine(this.UpdateKeyAnim());
            goto Label_0057;
        Label_0032:
            if (this.mBattleRes != 1)
            {
                goto Label_0050;
            }
            base.StartCoroutine(this.UpdateLostKeyAnim());
            goto Label_0057;
        Label_0050:
            this.mUpdateAnim = 0;
        Label_0057:
            goto Label_00A5;
        Label_005C:
            if (pinID != 2)
            {
                goto Label_0083;
            }
            this.mUpdateAnim = 1;
            this.mState = 2;
            base.StartCoroutine(this.UpdateFloorAnim());
            goto Label_00A5;
        Label_0083:
            if (pinID != 3)
            {
                goto Label_00A5;
            }
            this.mUpdateAnim = 1;
            this.mState = 3;
            base.StartCoroutine(this.UpdateRewardAnim());
        Label_00A5:
            return;
        }

        public void OnClickNextButton()
        {
            GameManager manager;
            VsTowerMatchEndParam param;
            bool flag;
            KEY_RESULT_STATE key_result_state;
            if (this.mUpdateAnim == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            param = MonoSingleton<GameManager>.Instance.GetVsTowerMatchEndParam();
            flag = (param == null) ? 0 : (param.arravied == 1);
            switch ((this.mState - 1))
            {
                case 0:
                    goto Label_004F;

                case 1:
                    goto Label_009A;

                case 2:
                    goto Label_00EB;
            }
            goto Label_0114;
        Label_004F:
            MonoSingleton<MySound>.Instance.PlaySEOneShot(SoundSettings.Current.Tap, 0f);
            if (this.mUpdateFloor == null)
            {
                goto Label_008A;
            }
            this.SetButtonText(flag);
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "UPDATE_FLOOR");
            goto Label_0095;
        Label_008A:
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "FINISH_KEY_RESULT");
        Label_0095:
            goto Label_0114;
        Label_009A:
            MonoSingleton<MySound>.Instance.PlaySEOneShot(SoundSettings.Current.Tap, 0f);
            if (this.mBattleRes != null)
            {
                goto Label_00D4;
            }
            if (flag == null)
            {
                goto Label_00D4;
            }
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "GET_REWARD");
            goto Label_00E6;
        Label_00D4:
            this.SetButtonText(0);
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "FINISH_KEY_RESULT");
        Label_00E6:
            goto Label_0114;
        Label_00EB:
            MonoSingleton<MySound>.Instance.PlaySEOneShot(SoundSettings.Current.Tap, 0f);
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "FINISH_KEY_RESULT");
        Label_0114:
            return;
        }

        private unsafe void RefreshData()
        {
            GameManager manager;
            PlayerData data;
            PartyData data2;
            VsTowerMatchEndParam param;
            UnitData data3;
            int num;
            int num2;
            VersusTowerParam param2;
            int num3;
            GameObject obj2;
            SceneBattle battle;
            BattleCore core;
            BattleCore.Record record;
            int num4;
            int num5;
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player;
            data2 = data.FindPartyOfType(7);
            param = manager.GetVsTowerMatchEndParam();
            if (param != null)
            {
                goto Label_0023;
            }
            return;
        Label_0023:
            if (data2 == null)
            {
                goto Label_0051;
            }
            data3 = data.FindUnitDataByUniqueID(data2.GetUnitUniqueID(data2.LeaderIndex));
            if (data3 == null)
            {
                goto Label_0051;
            }
            DataSource.Bind<UnitData>(base.get_gameObject(), data3);
        Label_0051:
            num = data.VersusTowerFloor;
            if ((this.floortxt != null) == null)
            {
                goto Label_007C;
            }
            this.floortxt.set_text(&num.ToString());
        Label_007C:
            if ((this.floorEfftxt != null) == null)
            {
                goto Label_009F;
            }
            this.floorEfftxt.set_text(&num.ToString());
        Label_009F:
            num2 = data.VersusTowerKey;
            if ((this.key != null) == null)
            {
                goto Label_035D;
            }
            param2 = manager.GetCurrentVersusTowerParam(-1);
            if (param2 == null)
            {
                goto Label_035D;
            }
            num3 = 0;
            goto Label_0141;
        Label_00D0:
            obj2 = Object.Instantiate<GameObject>(this.key);
            if ((obj2 != null) == null)
            {
                goto Label_0135;
            }
            if ((this.parent != null) == null)
            {
                goto Label_0113;
            }
            obj2.get_transform().SetParent(this.parent.get_transform(), 0);
        Label_0113:
            if (num2 <= 0)
            {
                goto Label_0128;
            }
            GameUtility.SetAnimatorTrigger(obj2, this.keyDefAnim);
        Label_0128:
            this.mCreateKey.Add(obj2);
        Label_0135:
            num3 += 1;
            num2 -= 1;
        Label_0141:
            if (num3 < param2.RankupNum)
            {
                goto Label_00D0;
            }
            this.key.SetActive(0);
            battle = SceneBattle.Instance;
            if ((battle != null) == null)
            {
                goto Label_0324;
            }
            core = battle.Battle;
            if (core == null)
            {
                goto Label_0324;
            }
            record = core.GetQuestRecord();
            if (record.result != 1)
            {
                goto Label_0272;
            }
            num4 = (param.rankup == null) ? param.key : param2.RankupNum;
            this.mAnimKeyIndex = data.VersusTowerKey;
            this.mMaxKeyCount = Mathf.Min(num4, param2.RankupNum);
            this.mUpdateFloor = param.rankup;
            this.mBattleRes = 0;
            if (this.mUpdateFloor == null)
            {
                goto Label_0324;
            }
            if ((this.arrivalNumText != null) == null)
            {
                goto Label_0237;
            }
            this.arrivalNumText.set_text(&param.floor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR"));
        Label_0237:
            if ((this.arrivalEffNumText != null) == null)
            {
                goto Label_0324;
            }
            this.arrivalEffNumText.set_text(&param.floor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR"));
            goto Label_0324;
        Label_0272:
            if ((record.result != 2) || (param2.LoseNum <= 0))
            {
                goto Label_031D;
            }
            this.mAnimKeyIndex = data.VersusTowerKey - 1;
            this.mMaxKeyCount = Math.Max(param.key, 0);
            this.mUpdateFloor = (this.mAnimKeyIndex >= 0) ? 0 : (param2.DownFloor > 0);
            this.mBattleRes = 1;
            if ((this.mUpdateFloor == null) || ((this.arrivalNumText != null) == null))
            {
                goto Label_0324;
            }
            num5 = Math.Max(param.floor, 1);
            this.arrivalNumText.set_text(&num5.ToString());
            goto Label_0324;
        Label_031D:
            this.mBattleRes = 2;
        Label_0324:
            if ((this.infoText != null) == null)
            {
                goto Label_035D;
            }
            this.infoText.SetActive((this.mBattleRes != null) ? 0 : (param2.RankupNum > 0));
        Label_035D:
            if (this.mUpdateFloor == null)
            {
                goto Label_036F;
            }
            this.SetButtonText(1);
        Label_036F:
            return;
        }

        private void ReqAnim(string trrigger, bool isAnimator)
        {
            Animator animator;
            if ((this.RightRoot != null) == null)
            {
                goto Label_0047;
            }
            if (isAnimator == null)
            {
                goto Label_003B;
            }
            animator = this.RightRoot.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0047;
            }
            animator.Play(trrigger);
            goto Label_0047;
        Label_003B:
            GameUtility.SetAnimatorTrigger(this.RightRoot, trrigger);
        Label_0047:
            return;
        }

        private void SetButtonText(bool bNext)
        {
            if ((this.okText != null) == null)
            {
                goto Label_0036;
            }
            this.okText.set_text(LocalizedText.Get((bNext == null) ? "sys.CMD_OK" : "sys.BTN_NEXT"));
        Label_0036:
            return;
        }

        private void SetupRewardItem()
        {
            GameManager manager;
            VsTowerMatchEndParam param;
            VersusTowerParam param2;
            VersusTowerRewardItem item;
            manager = MonoSingleton<GameManager>.Instance;
            param = manager.GetVsTowerMatchEndParam();
            param2 = manager.GetCurrentVersusTowerParam(param.floor);
            if (param2 == null)
            {
                goto Label_0046;
            }
            if (string.IsNullOrEmpty(param2.ArrivalIteminame) != null)
            {
                goto Label_0046;
            }
            if ((this.rewardObj == null) == null)
            {
                goto Label_0047;
            }
        Label_0046:
            return;
        Label_0047:
            DataSource.Bind<VersusTowerParam>(this.rewardObj, param2);
            item = this.rewardObj.GetComponent<VersusTowerRewardItem>();
            if ((item != null) == null)
            {
                goto Label_0073;
            }
            item.Refresh(0, 0);
        Label_0073:
            return;
        }

        private void Start()
        {
            this.RefreshData();
            return;
        }

        [DebuggerHidden]
        public virtual IEnumerator UpdateFloorAnim()
        {
            <UpdateFloorAnim>c__Iterator181 iterator;
            iterator = new <UpdateFloorAnim>c__Iterator181();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        public virtual IEnumerator UpdateKeyAnim()
        {
            <UpdateKeyAnim>c__Iterator17F iteratorf;
            iteratorf = new <UpdateKeyAnim>c__Iterator17F();
            iteratorf.<>f__this = this;
            return iteratorf;
        }

        [DebuggerHidden]
        public virtual IEnumerator UpdateLostKeyAnim()
        {
            <UpdateLostKeyAnim>c__Iterator180 iterator;
            iterator = new <UpdateLostKeyAnim>c__Iterator180();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        public virtual IEnumerator UpdateRewardAnim()
        {
            <UpdateRewardAnim>c__Iterator182 iterator;
            iterator = new <UpdateRewardAnim>c__Iterator182();
            iterator.<>f__this = this;
            return iterator;
        }

        [CompilerGenerated]
        private sealed class <UpdateFloorAnim>c__Iterator181 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameManager <gm>__0;
            internal VsTowerMatchEndParam <endParam>__1;
            internal Animator <rootAnim>__2;
            internal Animator <anim>__3;
            internal VersusTowerParam <floorParam>__4;
            internal bool <isLast>__5;
            internal int $PC;
            internal object $current;
            internal VersusTowerKey <>f__this;

            public <UpdateFloorAnim>c__Iterator181()
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
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_040C;

                    case 2:
                        goto Label_042B;
                }
                goto Label_0432;
            Label_0025:
                this.<gm>__0 = MonoSingleton<GameManager>.Instance;
                this.<endParam>__1 = this.<gm>__0.GetVsTowerMatchEndParam();
                this.<rootAnim>__2 = this.<>f__this.get_gameObject().GetComponent<Animator>();
                if ((this.<rootAnim>__2 != null) == null)
                {
                    goto Label_0073;
                }
                this.<rootAnim>__2.Stop();
            Label_0073:
                if ((this.<>f__this.keyRoot != null) == null)
                {
                    goto Label_009A;
                }
                this.<>f__this.keyRoot.SetActive(0);
            Label_009A:
                if (this.<>f__this.mBattleRes != 1)
                {
                    goto Label_01CC;
                }
                if ((this.<>f__this.reglegationRoot != null) == null)
                {
                    goto Label_00D2;
                }
                this.<>f__this.reglegationRoot.SetActive(1);
            Label_00D2:
                if ((this.<>f__this.floortxt != null) == null)
                {
                    goto Label_0123;
                }
                this.<>f__this.floortxt.set_text(&this.<endParam>__1.floor.ToString());
                this.<>f__this.floortxt.set_color(this.<>f__this.rankDownColor);
            Label_0123:
                if ((this.<>f__this.floorEfftxt != null) == null)
                {
                    goto Label_0174;
                }
                this.<>f__this.floorEfftxt.set_text(&this.<endParam>__1.floor.ToString());
                this.<>f__this.floorEfftxt.set_color(this.<>f__this.rankDownColor);
            Label_0174:
                if ((this.<>f__this.floorObj != null) == null)
                {
                    goto Label_03F0;
                }
                this.<anim>__3 = this.<>f__this.floorObj.GetComponent<Animator>();
                if ((this.<anim>__3 != null) == null)
                {
                    goto Label_03F0;
                }
                this.<anim>__3.Play(this.<>f__this.downFloorAnim);
                goto Label_03F0;
            Label_01CC:
                if ((this.<>f__this.arrivalObj != null) == null)
                {
                    goto Label_01F3;
                }
                this.<>f__this.arrivalObj.SetActive(1);
            Label_01F3:
                if ((this.<>f__this.floortxt != null) == null)
                {
                    goto Label_0238;
                }
                this.<>f__this.floortxt.set_text(&this.<endParam>__1.floor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR"));
            Label_0238:
                if ((this.<>f__this.floorEfftxt != null) == null)
                {
                    goto Label_027D;
                }
                this.<>f__this.floorEfftxt.set_text(&this.<endParam>__1.floor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR"));
            Label_027D:
                MonoSingleton<MySound>.Instance.PlayJingle("JIN_0001", 0f, null);
                this.<floorParam>__4 = this.<gm>__0.GetCurrentVersusTowerParam(this.<endParam>__1.floor);
                MonoSingleton<GameManager>.Instance.Player.UpdateVersusTowerTrophyStates(this.<floorParam>__4.VersusTowerID, this.<endParam>__1.floor);
                this.<isLast>__5 = 0;
                if (this.<floorParam>__4 == null)
                {
                    goto Label_03B9;
                }
                if (this.<floorParam>__4.RankupNum != null)
                {
                    goto Label_038D;
                }
                if ((this.<>f__this.infoLastText != null) == null)
                {
                    goto Label_035A;
                }
                this.<>f__this.infoLastText.get_gameObject().SetActive(1);
                this.<>f__this.infoLastText.set_text(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_LASTFLOOR"), (int) this.<endParam>__1.floor));
            Label_035A:
                if ((this.<>f__this.infoText != null) == null)
                {
                    goto Label_0381;
                }
                this.<>f__this.infoText.SetActive(0);
            Label_0381:
                this.<isLast>__5 = 1;
                goto Label_03B9;
            Label_038D:
                if ((this.<>f__this.infoLastText != null) == null)
                {
                    goto Label_03B9;
                }
                this.<>f__this.infoLastText.get_gameObject().SetActive(0);
            Label_03B9:
                this.<>f__this.ReqAnim((this.<isLast>__5 == null) ? this.<>f__this.trriggerIn : this.<>f__this.trriggerInLastFloor, this.<isLast>__5);
            Label_03F0:
                this.$current = new WaitForSeconds(1f);
                this.$PC = 1;
                goto Label_0434;
            Label_040C:
                this.<>f__this.mUpdateAnim = 0;
                this.$current = null;
                this.$PC = 2;
                goto Label_0434;
            Label_042B:
                this.$PC = -1;
            Label_0432:
                return 0;
            Label_0434:
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
        private sealed class <UpdateKeyAnim>c__Iterator17F : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal VersusTowerKey <>f__this;

            public <UpdateKeyAnim>c__Iterator17F()
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
                        goto Label_0045;

                    case 2:
                        goto Label_010F;

                    case 3:
                        goto Label_0159;
                }
                goto Label_0160;
            Label_0029:
                this.$current = new WaitForSeconds(1f);
                this.$PC = 1;
                goto Label_0162;
            Label_0045:
                goto Label_010F;
            Label_004A:
                if (this.<>f__this.mAnimKeyIndex < 0)
                {
                    goto Label_00F3;
                }
                if (this.<>f__this.mAnimKeyIndex >= this.<>f__this.mCreateKey.Count)
                {
                    goto Label_00F3;
                }
                if ((this.<>f__this.mCreateKey[this.<>f__this.mAnimKeyIndex] != null) == null)
                {
                    goto Label_00F3;
                }
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0508", 0f);
                GameUtility.SetAnimatorTrigger(this.<>f__this.mCreateKey[this.<>f__this.mAnimKeyIndex], this.<>f__this.keyGetAnim);
                this.<>f__this.mAnimKeyIndex += 1;
            Label_00F3:
                this.$current = new WaitForSeconds(0.5f);
                this.$PC = 2;
                goto Label_0162;
            Label_010F:
                if (this.<>f__this.mAnimKeyIndex >= this.<>f__this.mMaxKeyCount)
                {
                    goto Label_013A;
                }
                if (this.<>f__this.mCreateKey != null)
                {
                    goto Label_004A;
                }
            Label_013A:
                this.<>f__this.mUpdateAnim = 0;
                this.$current = null;
                this.$PC = 3;
                goto Label_0162;
            Label_0159:
                this.$PC = -1;
            Label_0160:
                return 0;
            Label_0162:
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
        private sealed class <UpdateLostKeyAnim>c__Iterator180 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal VersusTowerKey <>f__this;

            public <UpdateLostKeyAnim>c__Iterator180()
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
                        goto Label_0045;

                    case 2:
                        goto Label_00FB;

                    case 3:
                        goto Label_0145;
                }
                goto Label_014C;
            Label_0029:
                this.$current = new WaitForSeconds(1f);
                this.$PC = 1;
                goto Label_014E;
            Label_0045:
                goto Label_00FB;
            Label_004A:
                if (this.<>f__this.mAnimKeyIndex < 0)
                {
                    goto Label_00DF;
                }
                if (this.<>f__this.mAnimKeyIndex >= this.<>f__this.mCreateKey.Count)
                {
                    goto Label_00DF;
                }
                if ((this.<>f__this.mCreateKey[this.<>f__this.mAnimKeyIndex] != null) == null)
                {
                    goto Label_00DF;
                }
                GameUtility.SetAnimatorTrigger(this.<>f__this.mCreateKey[this.<>f__this.mAnimKeyIndex], this.<>f__this.keyLostAnim);
                this.<>f__this.mAnimKeyIndex -= 1;
            Label_00DF:
                this.$current = new WaitForSeconds(0.5f);
                this.$PC = 2;
                goto Label_014E;
            Label_00FB:
                if (this.<>f__this.mAnimKeyIndex < this.<>f__this.mMaxKeyCount)
                {
                    goto Label_0126;
                }
                if (this.<>f__this.mCreateKey != null)
                {
                    goto Label_004A;
                }
            Label_0126:
                this.<>f__this.mUpdateAnim = 0;
                this.$current = null;
                this.$PC = 3;
                goto Label_014E;
            Label_0145:
                this.$PC = -1;
            Label_014C:
                return 0;
            Label_014E:
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
        private sealed class <UpdateRewardAnim>c__Iterator182 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameManager <gm>__0;
            internal VsTowerMatchEndParam <endParam>__1;
            internal VersusTowerParam <floorParam>__2;
            internal bool <isLast>__3;
            internal int $PC;
            internal object $current;
            internal VersusTowerKey <>f__this;

            public <UpdateRewardAnim>c__Iterator182()
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
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_002D;

                    case 1:
                        goto Label_00E3;

                    case 2:
                        goto Label_0262;

                    case 3:
                        goto Label_02A9;

                    case 4:
                        goto Label_02C8;
                }
                goto Label_02CF;
            Label_002D:
                this.<gm>__0 = MonoSingleton<GameManager>.Instance;
                this.<endParam>__1 = this.<gm>__0.GetVsTowerMatchEndParam();
                this.<floorParam>__2 = this.<gm>__0.GetCurrentVersusTowerParam(this.<endParam>__1.floor);
                this.<isLast>__3 = 0;
                if (this.<floorParam>__2 == null)
                {
                    goto Label_0090;
                }
                this.<isLast>__3 = this.<floorParam>__2.RankupNum == 0;
            Label_0090:
                this.<>f__this.ReqAnim((this.<isLast>__3 == null) ? this.<>f__this.trriggerOut : this.<>f__this.trriggerInLastFloorOut, this.<isLast>__3);
                this.$current = new WaitForSeconds(1f);
                this.$PC = 1;
                goto Label_02D1;
            Label_00E3:
                if ((this.<>f__this.arrivalRewardText != null) == null)
                {
                    goto Label_0119;
                }
                this.<>f__this.arrivalRewardText.set_text(&this.<endParam>__1.floor.ToString());
            Label_0119:
                if ((this.<>f__this.ArrivalInfoRoot != null) == null)
                {
                    goto Label_0140;
                }
                this.<>f__this.ArrivalInfoRoot.SetActive(1);
            Label_0140:
                if ((this.<>f__this.ClearInfoRoot != null) == null)
                {
                    goto Label_0167;
                }
                this.<>f__this.ClearInfoRoot.SetActive(0);
            Label_0167:
                if ((this.<>f__this.arrivalObj != null) == null)
                {
                    goto Label_018E;
                }
                this.<>f__this.arrivalObj.SetActive(0);
            Label_018E:
                if ((this.<>f__this.reglegationRoot != null) == null)
                {
                    goto Label_01B5;
                }
                this.<>f__this.reglegationRoot.SetActive(0);
            Label_01B5:
                if ((this.<>f__this.infoLastText != null) == null)
                {
                    goto Label_01E1;
                }
                this.<>f__this.infoLastText.get_gameObject().SetActive(0);
            Label_01E1:
                if ((this.<>f__this.infoText != null) == null)
                {
                    goto Label_0208;
                }
                this.<>f__this.infoText.SetActive(1);
            Label_0208:
                if ((this.<>f__this.rewardRoot != null) == null)
                {
                    goto Label_023A;
                }
                this.<>f__this.rewardRoot.SetActive(1);
                this.<>f__this.SetupRewardItem();
            Label_023A:
                this.<>f__this.SetButtonText(0);
                this.$current = new WaitForSeconds(0.5f);
                this.$PC = 2;
                goto Label_02D1;
            Label_0262:
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0508", 0f);
                this.<>f__this.ReqAnim(this.<>f__this.trriggerRewardIn, 0);
                this.$current = new WaitForSeconds(1f);
                this.$PC = 3;
                goto Label_02D1;
            Label_02A9:
                this.<>f__this.mUpdateAnim = 0;
                this.$current = null;
                this.$PC = 4;
                goto Label_02D1;
            Label_02C8:
                this.$PC = -1;
            Label_02CF:
                return 0;
            Label_02D1:
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

        private enum KEY_RESULT_STATE
        {
            NONE,
            GET_KEY,
            UPDATE_FLOOR,
            GET_REWARD
        }

        private enum RESULT
        {
            WIN,
            LOSE,
            DRAW
        }
    }
}

