namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(0x7ed, "レビューへ移動", 1, 0x7ed), Pin(0x7ec, "イベントショップへ移動", 1, 0x7ec), Pin(0x7eb, "限定ショップへ移動", 1, 0x7eb), Pin(0x7ea, "塔クエスト選択へ移動", 1, 0x7ea), Pin(0x7e9, "ユニット選択へ移動", 1, 0x7e9), Pin(0x7e5, "武具の店へ移動", 1, 0x7e5), Pin(0x7e4, "魂の交換所へ移動", 1, 0x7e4), Pin(0x7dd, "アンナの店へ移動", 1, 0x7dd), Pin(0x7d8, "FgGID画面へ移動", 1, 0x7d8), Pin(0x7d7, "アリーナへ移動", 1, 0x7d7), Pin(0x7d6, "イベントクエスト選択へ移動", 1, 0x7d6), Pin(0x7d5, "マルチプレイへ移動", 1, 0x7d5), Pin(0x7d4, "装備強化画面へ移動", 1, 0x7d4), Pin(0x7d3, "ゴールド購入画面へ移動", 1, 0x7d3), Pin(0x7d2, "クエスト選択へ移動", 1, 0x7d2), Pin(0x7d1, "ガチャへ移動", 1, 0x7d1), Pin(0x7d0, "報酬を受け取り", 1, 0x7d0), Pin(1, "更新", 0, 1), Pin(0x3e9, "レコード", 1, 0x3e9), Pin(0x3e8, "デイリー", 1, 0x3e8), Pin(0, "初期化", 0, 0), Pin(0x7f7, "フレンド画面を開く", 1, 0x7f7), Pin(0x7f6, "お知らせを開く", 1, 0x7f6), Pin(0x7f5, "ホーム画面へ移動", 1, 0x7f5), Pin(0x7f4, "異層圏カルマへ移動", 1, 0x7f4), Pin(0x7f3, "真理念装へ移動", 1, 0x7f3), Pin(0x7f2, "初心者の館へ移動", 1, 0x7f2), Pin(0x7f1, "初心者クエストへ移動", 1, 0x7f1), Pin(0x7f0, "キャラクエストへ移動", 1, 0x7f0), Pin(0x7ef, "マルチ塔へ移動", 1, 0x7ef), Pin(0x7ee, "対戦へ移動", 1, 0x7ee)]
    public class TrophyWindow : MonoBehaviour, IFlowInterface
    {
        public const int PIN_REFRESH = 1;
        public const int PIN_DAILY = 0x3e8;
        public const int PIN_RECORD = 0x3e9;
        public const int PIN_REWARD_CHECK = 0x7d0;
        public const int PIN_GOTO_GACHA = 0x7d1;
        public const int PIN_GOTO_QUEST = 0x7d2;
        public const int PIN_GOTO_BUYGOLD = 0x7d3;
        public const int PIN_GOTO_SOUBI = 0x7d4;
        public const int PIN_GOTO_MULTI = 0x7d5;
        public const int PIN_GOTO_EVENT = 0x7d6;
        public const int PIN_GOTO_ARENA = 0x7d7;
        public const int PIN_GOTO_FGGID = 0x7d8;
        public const int PIN_GOTO_SHOP_NORMAL = 0x7dd;
        public const int PIN_GOTO_SHOP_KAKERA = 0x7e4;
        public const int PIN_GOTO_SHOP_ARTIFACT = 0x7e5;
        public const int PIN_GOTO_UNIT = 0x7e9;
        public const int PIN_GOTO_TOWER = 0x7ea;
        public const int PIN_GOTO_SHOP_LIMITE = 0x7eb;
        public const int PIN_GOTO_SHOP_EVENT = 0x7ec;
        public const int PIN_GOTO_REVIEW = 0x7ed;
        public const int PIN_GOTO_VERSUS = 0x7ee;
        public const int PIN_GOTO_MULTITOWER = 0x7ef;
        public const int PIN_GOTO_CHARAQUEST = 0x7f0;
        public const int PIN_GOTO_EVENT_BEGINEER = 0x7f1;
        public const int PIN_GOTO_BEGINNER_TOP = 0x7f2;
        public const int PIN_GOTO_CONCEPT_CARD = 0x7f3;
        public const int PIN_GOTO_QUEST_ORDEAL = 0x7f4;
        public const int PIN_GOTO_HOME = 0x7f5;
        public const int PIN_VIEW_NEWS = 0x7f6;
        public const int PIN_GOTO_FRIEND = 0x7f7;
        [SerializeField]
        private GameObject daily_badge_obj;
        [SerializeField]
        private GameObject record_badge_obj;
        [SerializeField]
        private GameObject story_badge_obj;
        [SerializeField]
        private GameObject event_badge_obj;
        [SerializeField]
        private GameObject multi_badge_obj;
        [SerializeField]
        private GameObject training_badge_obj;
        [SerializeField]
        private GameObject campaign_badge_obj;
        [SerializeField]
        private GameObject other_badge_obj;
        public Toggle[] TrophyTab;
        private Dictionary<TrophyCategorys, List<TrophyCategoryData>> trophy_record_datas;
        private Dictionary<int, TrophyCategoryData> trophy_category_datas;
        private List<TrophyState> trophy_daily_datas;
        private List<TrophyState> tmp_trophy_daily_datas;
        private List<TrophyState> trophy_ended_datas;

        public TrophyWindow()
        {
            this.trophy_record_datas = new Dictionary<TrophyCategorys, List<TrophyCategoryData>>();
            this.trophy_category_datas = new Dictionary<int, TrophyCategoryData>();
            this.trophy_daily_datas = new List<TrophyState>();
            this.tmp_trophy_daily_datas = new List<TrophyState>();
            this.trophy_ended_datas = new List<TrophyState>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            TrophyList list;
            if (pinID != null)
            {
                goto Label_0011;
            }
            this.Initialize();
            goto Label_0037;
        Label_0011:
            if (pinID != 1)
            {
                goto Label_0037;
            }
            this.RefreshTrophyDatas();
            list = base.GetComponentInChildren<TrophyList>();
            if ((list != null) == null)
            {
                goto Label_0037;
            }
            list.RefreshLight();
        Label_0037:
            return;
        }

        public void ActivateOutputLinks(int pin_id)
        {
            FlowNode_GameObject.ActivateOutputLinks(this, pin_id);
            return;
        }

        private bool DailyCheck()
        {
            int num;
            num = 0;
            goto Label_0023;
        Label_0007:
            if (this.TrophyDailyDatas[num].IsCompleted == null)
            {
                goto Label_001F;
            }
            return 1;
        Label_001F:
            num += 1;
        Label_0023:
            if (num < this.TrophyDailyDatas.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        private void Initialize()
        {
            bool flag;
            TrophyCategorys categorys;
            int num;
            TabIndex index;
            this.RefreshTrophyDatas();
            flag = 0;
            this.SetToggleIsOn(0);
            categorys = 1;
            goto Label_009F;
        Label_0016:
            if (this.TrophyRecordDatas.ContainsKey(categorys) != null)
            {
                goto Label_002C;
            }
            goto Label_009B;
        Label_002C:
            if (flag == null)
            {
                goto Label_0037;
            }
            goto Label_00A6;
        Label_0037:
            num = 0;
            goto Label_0084;
        Label_003E:
            if (this.TrophyRecordDatas[categorys][num].IsInCompletedData != null)
            {
                goto Label_005F;
            }
            goto Label_0080;
        Label_005F:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3e9);
            flag = 1;
            index = this.TrophyCategorysToTabIndex(categorys);
            this.SetToggleIsOn(index);
            goto Label_009B;
        Label_0080:
            num += 1;
        Label_0084:
            if (num < this.TrophyRecordDatas[categorys].Count)
            {
                goto Label_003E;
            }
        Label_009B:
            categorys += 1;
        Label_009F:
            if (categorys <= 6)
            {
                goto Label_0016;
            }
        Label_00A6:
            if (this.DailyCheck() == null)
            {
                goto Label_00BD;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3e8);
            return;
        Label_00BD:
            if (flag == null)
            {
                goto Label_00C4;
            }
            return;
        Label_00C4:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3e8);
            return;
        }

        private bool IsDisplayTrophy(TrophyState st)
        {
            if (st.Param.DispType != 2)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            if (st.Param.IsChallengeMission == null)
            {
                goto Label_0025;
            }
            return 0;
        Label_0025:
            if (st.Param.IsInvisibleVip() == null)
            {
                goto Label_0037;
            }
            return 0;
        Label_0037:
            if (st.Param.IsInvisibleCard() == null)
            {
                goto Label_0049;
            }
            return 0;
        Label_0049:
            if (st.Param.IsInvisibleStamina() == null)
            {
                goto Label_005B;
            }
            return 0;
        Label_005B:
            if (st.Param.RequiredTrophies == null)
            {
                goto Label_0084;
            }
            if (TrophyParam.CheckRequiredTrophies(MonoSingleton<GameManager>.Instance, st.Param, 1, 0) != null)
            {
                goto Label_0084;
            }
            return 0;
        Label_0084:
            if (st.Param.IsBeginner == null)
            {
                goto Label_00B5;
            }
            if (MonoSingleton<GameManager>.Instance.Player.IsBeginner() != null)
            {
                goto Label_00B5;
            }
            if (st.IsCompleted != null)
            {
                goto Label_00B5;
            }
            return 0;
        Label_00B5:
            if (st.IsCompleted != null)
            {
                goto Label_0128;
            }
            if (st.Param.ContainsCondition(0x3e) == null)
            {
                goto Label_0128;
            }
            if (string.IsNullOrEmpty(st.Param.Objectives[0].sval_base) == null)
            {
                goto Label_0105;
            }
            if (MonoSingleton<GameManager>.Instance.ExistsOpenVersusTower(null) != null)
            {
                goto Label_0128;
            }
            return 0;
            goto Label_0128;
        Label_0105:
            if (MonoSingleton<GameManager>.Instance.ExistsOpenVersusTower(st.Param.Objectives[0].sval_base) != null)
            {
                goto Label_0128;
            }
            return 0;
        Label_0128:
            if (st.Param.iname.Length < 7)
            {
                goto Label_0175;
            }
            if ((st.Param.iname.Substring(0, 7) == "REVIEW_") == null)
            {
                goto Label_0175;
            }
            if (Network.Host.Contains("eval.alchemist.gu3.jp") == null)
            {
                goto Label_0175;
            }
            return 0;
        Label_0175:
            if (st.Param.IsBeginner != null)
            {
                goto Label_01A7;
            }
            if (st.Param.CategoryParam.IsOpenLinekedQuest(TimeManager.ServerTime, st.IsCompleted) != null)
            {
                goto Label_01A7;
            }
            return 0;
        Label_01A7:
            if (st.Param.ContainsCondition(0x13) == null)
            {
                goto Label_01BB;
            }
            return 0;
        Label_01BB:
            return 1;
        }

        private bool IsDisplayTrophyCategory(TrophyCategoryParam _tcp)
        {
            if (_tcp.IsAvailablePeriod(TimeManager.ServerTime, 1) != null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            return 1;
        }

        private unsafe void RefreshBadge()
        {
            int num;
            bool flag;
            GameObject obj2;
            TrophyCategorys categorys;
            Dictionary<TrophyCategorys, List<TrophyCategoryData>>.KeyCollection.Enumerator enumerator;
            int num2;
            TrophyCategorys categorys2;
            this.story_badge_obj.SetActive(0);
            this.event_badge_obj.SetActive(0);
            this.multi_badge_obj.SetActive(0);
            this.training_badge_obj.SetActive(0);
            this.campaign_badge_obj.SetActive(0);
            this.other_badge_obj.SetActive(0);
            if ((this.daily_badge_obj != null) == null)
            {
                goto Label_00A8;
            }
            this.daily_badge_obj.SetActive(0);
            num = 0;
            goto Label_0097;
        Label_006C:
            if (this.trophy_daily_datas[num].IsCompleted == null)
            {
                goto Label_0093;
            }
            this.daily_badge_obj.SetActive(1);
            goto Label_00A8;
        Label_0093:
            num += 1;
        Label_0097:
            if (num < this.trophy_daily_datas.Count)
            {
                goto Label_006C;
            }
        Label_00A8:
            flag = 0;
            obj2 = null;
            enumerator = this.trophy_record_datas.Keys.GetEnumerator();
        Label_00BE:
            try
            {
                goto Label_01A0;
            Label_00C3:
                categorys = &enumerator.Current;
                num2 = 0;
                goto Label_0171;
            Label_00D3:
                if (this.trophy_record_datas[categorys][num2].IsInCompletedData != null)
                {
                    goto Label_00F5;
                }
                goto Label_016B;
            Label_00F5:
                categorys2 = categorys;
                switch ((categorys2 - 1))
                {
                    case 0:
                        goto Label_011E;

                    case 1:
                        goto Label_012A;

                    case 2:
                        goto Label_0136;

                    case 3:
                        goto Label_0142;

                    case 4:
                        goto Label_014E;

                    case 5:
                        goto Label_015A;
                }
                goto Label_0166;
            Label_011E:
                obj2 = this.story_badge_obj;
                goto Label_0166;
            Label_012A:
                obj2 = this.event_badge_obj;
                goto Label_0166;
            Label_0136:
                obj2 = this.multi_badge_obj;
                goto Label_0166;
            Label_0142:
                obj2 = this.training_badge_obj;
                goto Label_0166;
            Label_014E:
                obj2 = this.campaign_badge_obj;
                goto Label_0166;
            Label_015A:
                obj2 = this.other_badge_obj;
            Label_0166:
                goto Label_0189;
            Label_016B:
                num2 += 1;
            Label_0171:
                if (num2 < this.trophy_record_datas[categorys].Count)
                {
                    goto Label_00D3;
                }
            Label_0189:
                if ((obj2 != null) == null)
                {
                    goto Label_01A0;
                }
                flag = 1;
                obj2.SetActive(1);
                obj2 = null;
            Label_01A0:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00C3;
                }
                goto Label_01BE;
            }
            finally
            {
            Label_01B1:
                ((Dictionary<TrophyCategorys, List<TrophyCategoryData>>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_01BE:
            this.record_badge_obj.SetActive(flag);
            return;
        }

        private unsafe void RefreshTrophyDatas()
        {
            TrophyCategoryParam[] paramArray;
            int num;
            TrophyParam[] paramArray2;
            TrophyState state;
            int num2;
            int num3;
            Dictionary<int, TrophyCategoryData>.KeyCollection.Enumerator enumerator;
            int num4;
            int num5;
            this.trophy_record_datas.Clear();
            this.trophy_daily_datas.Clear();
            this.trophy_ended_datas.Clear();
            this.trophy_category_datas.Clear();
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.TrophyCategories;
            if (paramArray != null)
            {
                goto Label_0043;
            }
            return;
        Label_0043:
            num = 0;
            goto Label_0094;
        Label_004A:
            if (this.IsDisplayTrophyCategory(paramArray[num]) != null)
            {
                goto Label_005D;
            }
            goto Label_0090;
        Label_005D:
            if (this.trophy_category_datas.ContainsKey(paramArray[num].hash_code) != null)
            {
                goto Label_0090;
            }
            this.trophy_category_datas.Add(paramArray[num].hash_code, new TrophyCategoryData(paramArray[num]));
        Label_0090:
            num += 1;
        Label_0094:
            if (num < ((int) paramArray.Length))
            {
                goto Label_004A;
            }
            paramArray2 = MonoSingleton<GameManager>.Instance.Trophies;
            state = null;
            num2 = 0;
            goto Label_015A;
        Label_00B2:
            state = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(paramArray2[num2], 1);
            if (state.Param.is_none_category_hash == null)
            {
                goto Label_00DC;
            }
            goto Label_0154;
        Label_00DC:
            if (this.IsDisplayTrophy(state) != null)
            {
                goto Label_00ED;
            }
            goto Label_0154;
        Label_00ED:
            if (state.IsEnded == null)
            {
                goto Label_0109;
            }
            this.trophy_ended_datas.Add(state);
            goto Label_0154;
        Label_0109:
            if (paramArray2[num2].DispType != 1)
            {
                goto Label_011D;
            }
            goto Label_0154;
        Label_011D:
            if (this.trophy_category_datas.ContainsKey(state.Param.category_hash_code) == null)
            {
                goto Label_0154;
            }
            this.trophy_category_datas[state.Param.category_hash_code].AddTrophy(state);
        Label_0154:
            num2 += 1;
        Label_015A:
            if (num2 < ((int) paramArray2.Length))
            {
                goto Label_00B2;
            }
            enumerator = this.trophy_category_datas.Keys.GetEnumerator();
        Label_0176:
            try
            {
                goto Label_03B1;
            Label_017B:
                num3 = &enumerator.Current;
                if (this.trophy_category_datas[num3].Trophies.Count > 0)
                {
                    goto Label_01A6;
                }
                goto Label_03B1;
            Label_01A6:
                if (this.trophy_category_datas[num3].Param.IsAvailablePeriod(TimeManager.ServerTime, 0) != null)
                {
                    goto Label_0260;
                }
                if (this.trophy_category_datas[num3].IsInCompletedData != null)
                {
                    goto Label_01E4;
                }
                goto Label_03B1;
            Label_01E4:
                num4 = this.trophy_category_datas[num3].Trophies.Count - 1;
                goto Label_0258;
            Label_0204:
                if (this.trophy_category_datas[num3].Trophies[num4].IsCompleted != null)
                {
                    goto Label_0252;
                }
                this.trophy_category_datas[num3].RemoveTrophy(this.trophy_category_datas[num3].Trophies[num4]);
            Label_0252:
                num4 -= 1;
            Label_0258:
                if (num4 >= 0)
                {
                    goto Label_0204;
                }
            Label_0260:
                if (this.trophy_category_datas[num3].Param.IsDaily == null)
                {
                    goto Label_031D;
                }
                num5 = 0;
                goto Label_02FA;
            Label_0284:
                if (this.trophy_category_datas[num3].Trophies[num5].IsCompleted == null)
                {
                    goto Label_02D0;
                }
                this.trophy_daily_datas.Add(this.trophy_category_datas[num3].Trophies[num5]);
                goto Label_02F4;
            Label_02D0:
                this.tmp_trophy_daily_datas.Add(this.trophy_category_datas[num3].Trophies[num5]);
            Label_02F4:
                num5 += 1;
            Label_02FA:
                if (num5 < this.trophy_category_datas[num3].Trophies.Count)
                {
                    goto Label_0284;
                }
                goto Label_03B1;
            Label_031D:
                if (this.trophy_record_datas.ContainsKey(this.trophy_category_datas[num3].Param.category) != null)
                {
                    goto Label_036B;
                }
                this.trophy_record_datas.Add(this.trophy_category_datas[num3].Param.category, new List<TrophyCategoryData>());
            Label_036B:
                this.trophy_category_datas[num3].Apply();
                this.trophy_record_datas[this.trophy_category_datas[num3].Param.category].Add(this.trophy_category_datas[num3]);
            Label_03B1:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_017B;
                }
                goto Label_03CF;
            }
            finally
            {
            Label_03C2:
                ((Dictionary<int, TrophyCategoryData>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_03CF:
            this.trophy_daily_datas.AddRange(this.tmp_trophy_daily_datas);
            this.trophy_category_datas.Clear();
            this.tmp_trophy_daily_datas.Clear();
            this.RefreshBadge();
            return;
        }

        private void SetToggleIsOn(int index)
        {
            int num;
            num = 0;
            goto Label_0038;
        Label_0007:
            if (this.TrophyTab[num] == null)
            {
                goto Label_0034;
            }
            this.TrophyTab[num].set_isOn((num != index) ? 0 : 1);
        Label_0034:
            num += 1;
        Label_0038:
            if (num < ((int) this.TrophyTab.Length))
            {
                goto Label_0007;
            }
            return;
        }

        private TabIndex TrophyCategorysToTabIndex(TrophyCategorys category)
        {
            if (category != 1)
            {
                goto Label_0009;
            }
            return 0;
        Label_0009:
            if (category != 2)
            {
                goto Label_0012;
            }
            return 1;
        Label_0012:
            if (category != 3)
            {
                goto Label_001B;
            }
            return 2;
        Label_001B:
            if (category != 4)
            {
                goto Label_0024;
            }
            return 3;
        Label_0024:
            if (category != 5)
            {
                goto Label_002D;
            }
            return 4;
        Label_002D:
            return 5;
        }

        public Dictionary<TrophyCategorys, List<TrophyCategoryData>> TrophyRecordDatas
        {
            get
            {
                return this.trophy_record_datas;
            }
        }

        public List<TrophyState> TrophyDailyDatas
        {
            get
            {
                return this.trophy_daily_datas;
            }
        }

        public List<TrophyState> TrophyEndedDatas
        {
            get
            {
                return this.trophy_ended_datas;
            }
        }

        private enum TabIndex
        {
            Story,
            Event,
            Multi,
            Training,
            Campaign,
            Other,
            MAX
        }
    }
}

