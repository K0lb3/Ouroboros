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

    [Pin(11, "アイテムスキップHoldOff", 0, 11), Pin(1, "ドロップアイテムの獲得演出開始", 0, 1), Pin(2, "プレイヤー経験値の獲得演出開始", 0, 2), Pin(3, "ファントム経験値の獲得演出開始", 0, 3), Pin(0x3e8, "終了", 1, 0x3e8), Pin(300, "ファントム経験値の獲得演出終了", 1, 300), Pin(200, "プレイヤー経験値の獲得演出終了", 1, 200), Pin(100, "ドロップアイテムの獲得演出終了", 1, 100), Pin(20, "経験値演出スキップ", 0, 20), Pin(10, "アイテムスキップHoldOn", 0, 10)]
    public class RaidResultWindow : SRPG_ListBase, IFlowInterface
    {
        public ScrollRect ResultLayout;
        public Transform ResultParent;
        public GameObject ResultTemplate;
        public Button BtnUp;
        public Button BtnDown;
        public Button BtnOutSide;
        public Button BtnGainExpOutSide;
        [Description("入手アイテムのリストになる親ゲームオブジェクト")]
        public GameObject TreasureList;
        [Description("入手アイテムのゲームオブジェクト")]
        public GameObject TreasureListItem;
        [Description("入手真理念装のゲームオブジェクト")]
        public GameObject TreasureListConceptCard;
        [Description("入新規取得アイテムのバッジ")]
        public GameObject NewItemBadge;
        public GameObject GainExpWindow;
        public GameObject PlayerResult;
        public Slider PlayerGauge;
        public Text TxtPlayerLvVal;
        public Text TxtPlayerExpVal;
        public Text TxtGainGoldVal;
        [Description("レベルアップ時に使用するトリガー。ゲームオブジェクトにアタッチされたAnimatorへ送られます。")]
        public string LevelUpTrigger;
        [Description("一秒あたりの経験値の増加量")]
        public float ExpGainRate;
        [Description("経験値増加アニメーションの最長時間。経験値がExpGainRateの速度で増加する時、これで設定した時間を超える時に加算速度を上げる。")]
        public float ExpGainTimeMax;
        public float ResultScrollInterval;
        [Description("経験値増加アニメーションスキップの倍速設定")]
        public float ExpSkipSpeedMul;
        public GameObject UnitList;
        public GameObject UnitListItem;
        public Button SkipButton;
        public Button ExpSkipButton;
        [Range(0.1f, 10f)]
        public float SkipTimeScale;
        private RaidResult mRaidResult;
        private List<GameObject> mResults;
        private List<GameObject> mUnitListItems;
        private RaidResultElement mCurrentElement;
        private bool mItemSkipElement;
        private bool mExpSkipElement;
        public int[] AcquiredUnitExp;

        public RaidResultWindow()
        {
            this.LevelUpTrigger = "levelup";
            this.ExpGainRate = 100f;
            this.ExpGainTimeMax = 2f;
            this.ResultScrollInterval = 1f;
            this.ExpSkipSpeedMul = 10f;
            this.SkipTimeScale = 2f;
            this.mResults = new List<GameObject>();
            this.mUnitListItems = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0036;
            }
            if ((this.SkipButton != null) == null)
            {
                goto Label_0029;
            }
            this.SkipButton.get_gameObject().SetActive(1);
        Label_0029:
            base.StartCoroutine(this.QuestResultAnimation());
        Label_0036:
            if (pinID != 2)
            {
                goto Label_006C;
            }
            if ((this.ExpSkipButton != null) == null)
            {
                goto Label_005F;
            }
            this.ExpSkipButton.get_gameObject().SetActive(1);
        Label_005F:
            base.StartCoroutine(this.GainPlayerExpAnimation());
        Label_006C:
            if (pinID != 3)
            {
                goto Label_00A2;
            }
            if ((this.ExpSkipButton != null) == null)
            {
                goto Label_0095;
            }
            this.ExpSkipButton.get_gameObject().SetActive(1);
        Label_0095:
            base.StartCoroutine(this.GainUnitExpAnimation());
        Label_00A2:
            if (pinID != 10)
            {
                goto Label_00B1;
            }
            this.mItemSkipElement = 1;
        Label_00B1:
            if (pinID != 11)
            {
                goto Label_00C0;
            }
            this.mItemSkipElement = 0;
        Label_00C0:
            if (pinID != 20)
            {
                goto Label_00CF;
            }
            this.mExpSkipElement = 1;
        Label_00CF:
            return;
        }

        private void ApplyQuestCampaignParams(string[] campaignIds)
        {
            GameManager manager;
            QuestCampaignData[] dataArray;
            List<UnitData> list;
            float[] numArray;
            float num;
            int num2;
            QuestCampaignData[] dataArray2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            float num8;
            int num9;
            <ApplyQuestCampaignParams>c__AnonStorey38F storeyf;
            this.AcquiredUnitExp = new int[this.mRaidResult.members.Count];
            if (campaignIds == null)
            {
                goto Label_01DF;
            }
            dataArray = MonoSingleton<GameManager>.GetInstanceDirect().FindQuestCampaigns(campaignIds);
            list = this.mRaidResult.members;
            numArray = new float[list.Count];
            num = 1f;
            num2 = 0;
            goto Label_0065;
        Label_0056:
            numArray[num2] = 1f;
            num2 += 1;
        Label_0065:
            if (num2 < ((int) numArray.Length))
            {
                goto Label_0056;
            }
            storeyf = new <ApplyQuestCampaignParams>c__AnonStorey38F();
            dataArray2 = dataArray;
            num3 = 0;
            goto Label_013E;
        Label_0081:
            storeyf.data = dataArray2[num3];
            if (storeyf.data.type != 1)
            {
                goto Label_00FA;
            }
            if (string.IsNullOrEmpty(storeyf.data.unit) == null)
            {
                goto Label_00C8;
            }
            num = storeyf.data.GetRate();
            goto Label_00F5;
        Label_00C8:
            num4 = list.FindIndex(new Predicate<UnitData>(storeyf.<>m__3E1));
            if (num4 < 0)
            {
                goto Label_0138;
            }
            numArray[num4] = storeyf.data.GetRate();
        Label_00F5:
            goto Label_0138;
        Label_00FA:
            if (storeyf.data.type != null)
            {
                goto Label_0138;
            }
            num5 = this.mRaidResult.pexp;
            this.mRaidResult.pexp = Mathf.RoundToInt(((float) num5) * storeyf.data.GetRate());
        Label_0138:
            num3 += 1;
        Label_013E:
            if (num3 < ((int) dataArray2.Length))
            {
                goto Label_0081;
            }
            num6 = this.mRaidResult.uexp;
            num7 = 0;
            goto Label_01D0;
        Label_015E:
            num8 = 1f;
            if (num == 1f)
            {
                goto Label_018D;
            }
            if (numArray[num7] == 1f)
            {
                goto Label_018D;
            }
            num8 = num + numArray[num7];
            goto Label_01B6;
        Label_018D:
            if (num == 1f)
            {
                goto Label_01A2;
            }
            num8 = num;
            goto Label_01B6;
        Label_01A2:
            if (numArray[num7] == 1f)
            {
                goto Label_01B6;
            }
            num8 = numArray[num7];
        Label_01B6:
            this.AcquiredUnitExp[num7] = Mathf.RoundToInt(((float) num6) * num8);
            num7 += 1;
        Label_01D0:
            if (num7 < ((int) numArray.Length))
            {
                goto Label_015E;
            }
            goto Label_0210;
        Label_01DF:
            num9 = 0;
            goto Label_0201;
        Label_01E7:
            this.AcquiredUnitExp[num9] = this.mRaidResult.uexp;
            num9 += 1;
        Label_0201:
            if (num9 < ((int) this.AcquiredUnitExp.Length))
            {
                goto Label_01E7;
            }
        Label_0210:
            return;
        }

        private void CreateDropItemObjects(QuestResult.DropItemData[] items)
        {
            Transform transform;
            GameObject obj2;
            QuestResult.DropItemData data;
            QuestResult.DropItemData[] dataArray;
            int num;
            GameObject obj3;
            GameObject obj4;
            RectTransform transform2;
            transform = ((this.TreasureList != null) == null) ? this.TreasureListItem.get_transform().get_parent() : this.TreasureList.get_transform();
            obj2 = null;
            dataArray = items;
            num = 0;
            goto Label_014D;
        Label_003E:
            data = dataArray[num];
            obj3 = null;
            if (data.IsConceptCard == null)
            {
                goto Label_008A;
            }
            obj3 = Object.Instantiate<GameObject>(this.TreasureListConceptCard);
            obj3.get_transform().SetParent(transform, 0);
            DataSource.Bind<QuestResult.DropItemData>(obj3, data);
            obj3.SetActive(1);
            GameParameter.UpdateAll(obj3);
            goto Label_00E8;
        Label_008A:
            if (data.IsItem == null)
            {
                goto Label_00CE;
            }
            obj3 = Object.Instantiate<GameObject>(this.TreasureListItem);
            obj3.get_transform().SetParent(transform, 0);
            DataSource.Bind<ItemData>(obj3, data);
            obj3.SetActive(1);
            GameParameter.UpdateAll(obj3);
            goto Label_00E8;
        Label_00CE:
            DebugUtility.LogError(string.Format("[コードの追加が必要] DropItemData.mBattleRewardType(={0})は不明な列挙です", (EBattleRewardType) data.BattleRewardType));
        Label_00E8:
            if ((this.NewItemBadge != null) == null)
            {
                goto Label_0147;
            }
            if (data.IsNew == null)
            {
                goto Label_0147;
            }
            transform2 = Object.Instantiate<GameObject>(this.NewItemBadge).get_transform() as RectTransform;
            transform2.get_gameObject().SetActive(1);
            transform2.set_anchoredPosition(Vector2.get_zero());
            transform2.SetParent(obj3.get_transform(), 0);
        Label_0147:
            num += 1;
        Label_014D:
            if (num < ((int) dataArray.Length))
            {
                goto Label_003E;
            }
            return;
        }

        [DebuggerHidden]
        private IEnumerator GainPlayerExpAnimation()
        {
            <GainPlayerExpAnimation>c__Iterator138 iterator;
            iterator = new <GainPlayerExpAnimation>c__Iterator138();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator GainUnitExpAnimation()
        {
            <GainUnitExpAnimation>c__Iterator139 iterator;
            iterator = new <GainUnitExpAnimation>c__Iterator139();
            iterator.<>f__this = this;
            return iterator;
        }

        protected override RectTransform GetRectTransform()
        {
            return (this.ResultParent as RectTransform);
        }

        protected override ScrollRect GetScrollRect()
        {
            return this.ResultLayout;
        }

        private unsafe QuestResult.DropItemData[] MergeDropItems(RaidResult raidResult)
        {
            PlayerData data;
            List<QuestResult.DropItemData> list;
            RaidQuestResult result;
            List<RaidQuestResult>.Enumerator enumerator;
            QuestResult.DropItemData data2;
            QuestResult.DropItemData[] dataArray;
            int num;
            bool flag;
            int num2;
            QuestResult.DropItemData data3;
            ItemData data4;
            List<UnitData> list2;
            <MergeDropItems>c__AnonStorey38E storeye;
            if (raidResult != null)
            {
                goto Label_000D;
            }
            return new QuestResult.DropItemData[0];
        Label_000D:
            data = MonoSingleton<GameManager>.Instance.Player;
            list = new List<QuestResult.DropItemData>();
            enumerator = raidResult.results.GetEnumerator();
        Label_002A:
            try
            {
                goto Label_0243;
            Label_002F:
                result = &enumerator.Current;
                if (result == null)
                {
                    goto Label_0243;
                }
                dataArray = result.drops;
                num = 0;
                goto Label_0238;
            Label_004D:
                data2 = dataArray[num];
                if (data2 != null)
                {
                    goto Label_0060;
                }
                goto Label_0232;
            Label_0060:
                flag = 0;
                num2 = 0;
                goto Label_0117;
            Label_006B:
                if (((list[num2].IsItem == null) || (data2.IsItem == null)) || (list[num2].itemParam != data2.itemParam))
                {
                    goto Label_00BE;
                }
                list[num2].Gain(data2.Num);
                flag = 1;
                goto Label_0124;
            Label_00BE:
                if (((list[num2].IsConceptCard == null) || (data2.IsConceptCard == null)) || (list[num2].conceptCardParam != data2.conceptCardParam))
                {
                    goto Label_0111;
                }
                list[num2].Gain(data2.Num);
                flag = 1;
                goto Label_0124;
            Label_0111:
                num2 += 1;
            Label_0117:
                if (num2 < list.Count)
                {
                    goto Label_006B;
                }
            Label_0124:
                if (flag == null)
                {
                    goto Label_0130;
                }
                goto Label_0232;
            Label_0130:
                data3 = new QuestResult.DropItemData();
                if (data2.IsItem == null)
                {
                    goto Label_0201;
                }
                data3.SetupDropItemData(2, 0L, data2.itemParam.iname, data2.Num);
                if (data2.itemParam.type == 0x10)
                {
                    goto Label_01B9;
                }
                data4 = data.FindItemDataByItemParam(data2.itemParam);
                data3.IsNew = (data.ItemEntryExists(data2.itemParam.iname) == null) ? 1 : ((data4 == null) ? 1 : data4.IsNew);
                goto Label_01FC;
            Label_01B9:
                storeye = new <MergeDropItems>c__AnonStorey38E();
                storeye.iid = data2.itemParam.iname;
                if (data.Units.Find(new Predicate<UnitData>(storeye.<>m__3E0)) != null)
                {
                    goto Label_022A;
                }
                data3.IsNew = 1;
            Label_01FC:
                goto Label_022A;
            Label_0201:
                if (data2.IsConceptCard == null)
                {
                    goto Label_022A;
                }
                data3.SetupDropItemData(3, 0L, data2.conceptCardParam.iname, data2.Num);
            Label_022A:
                list.Add(data3);
            Label_0232:
                num += 1;
            Label_0238:
                if (num < ((int) dataArray.Length))
                {
                    goto Label_004D;
                }
            Label_0243:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_002F;
                }
                goto Label_0260;
            }
            finally
            {
            Label_0254:
                ((List<RaidQuestResult>.Enumerator) enumerator).Dispose();
            }
        Label_0260:
            return list.ToArray();
        }

        [DebuggerHidden]
        private IEnumerator QuestResultAnimation()
        {
            <QuestResultAnimation>c__Iterator137 iterator;
            iterator = new <QuestResultAnimation>c__Iterator137();
            iterator.<>f__this = this;
            return iterator;
        }

        protected override unsafe void Start()
        {
            int num;
            GameObject obj2;
            ListItemEvents events;
            Transform transform;
            int num2;
            GameObject obj3;
            QuestResult.DropItemData[] dataArray;
            base.Start();
            if ((this.ResultTemplate != null) == null)
            {
                goto Label_0023;
            }
            this.ResultTemplate.SetActive(0);
        Label_0023:
            if (this.UnitListItem == null)
            {
                goto Label_003F;
            }
            this.UnitListItem.SetActive(0);
        Label_003F:
            if ((this.BtnUp != null) == null)
            {
                goto Label_005C;
            }
            this.BtnUp.set_interactable(0);
        Label_005C:
            if ((this.BtnDown != null) == null)
            {
                goto Label_0079;
            }
            this.BtnUp.set_interactable(0);
        Label_0079:
            if ((this.BtnOutSide != null) == null)
            {
                goto Label_0096;
            }
            this.BtnOutSide.set_interactable(0);
        Label_0096:
            if ((this.BtnGainExpOutSide != null) == null)
            {
                goto Label_00B3;
            }
            this.BtnGainExpOutSide.set_interactable(0);
        Label_00B3:
            if ((this.ResultLayout != null) == null)
            {
                goto Label_00D0;
            }
            this.ResultLayout.set_enabled(0);
        Label_00D0:
            if ((this.GainExpWindow != null) == null)
            {
                goto Label_00ED;
            }
            this.GainExpWindow.SetActive(0);
        Label_00ED:
            if ((this.TreasureListItem != null) == null)
            {
                goto Label_010A;
            }
            this.TreasureListItem.SetActive(0);
        Label_010A:
            if ((this.TreasureListConceptCard != null) == null)
            {
                goto Label_0127;
            }
            this.TreasureListConceptCard.SetActive(0);
        Label_0127:
            if ((this.NewItemBadge != null) == null)
            {
                goto Label_0144;
            }
            this.NewItemBadge.SetActive(0);
        Label_0144:
            this.mRaidResult = GlobalVars.RaidResult;
            if (this.mRaidResult == null)
            {
                goto Label_02EB;
            }
            this.ApplyQuestCampaignParams(this.mRaidResult.campaignIds);
            if ((this.ResultTemplate != null) == null)
            {
                goto Label_01F8;
            }
            num = 0;
            goto Label_01E2;
        Label_0183:
            obj2 = Object.Instantiate<GameObject>(this.ResultTemplate);
            obj2.get_transform().SetParent(this.ResultParent, 0);
            DataSource.Bind<RaidQuestResult>(obj2, this.mRaidResult.results[num]);
            events = obj2.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_01D2;
            }
            base.AddItem(events);
        Label_01D2:
            this.mResults.Add(obj2);
            num += 1;
        Label_01E2:
            if (num < this.mRaidResult.results.Count)
            {
                goto Label_0183;
            }
        Label_01F8:
            if ((this.UnitListItem != null) == null)
            {
                goto Label_02A9;
            }
            transform = ((this.UnitList != null) == null) ? this.UnitListItem.get_transform().get_parent() : this.UnitList.get_transform();
            num2 = 0;
            goto Label_0292;
        Label_0243:
            obj3 = Object.Instantiate<GameObject>(this.UnitListItem);
            obj3.get_transform().SetParent(transform, 0);
            this.mUnitListItems.Add(obj3);
            DataSource.Bind<UnitData>(obj3, this.mRaidResult.members[num2]);
            obj3.SetActive(1);
            num2 += 1;
        Label_0292:
            if (num2 < this.mRaidResult.members.Count)
            {
                goto Label_0243;
            }
        Label_02A9:
            dataArray = this.MergeDropItems(this.mRaidResult);
            this.CreateDropItemObjects(dataArray);
            if ((this.TxtGainGoldVal != null) == null)
            {
                goto Label_02EB;
            }
            this.TxtGainGoldVal.set_text(&this.mRaidResult.gold.ToString());
        Label_02EB:
            GlobalVars.RaidResult = null;
            GlobalVars.RaidNum = 0;
            if ((this.SkipButton != null) == null)
            {
                goto Label_0319;
            }
            this.SkipButton.get_gameObject().SetActive(0);
        Label_0319:
            if ((this.ExpSkipButton != null) == null)
            {
                goto Label_033B;
            }
            this.ExpSkipButton.get_gameObject().SetActive(0);
        Label_033B:
            return;
        }

        [CompilerGenerated]
        private sealed class <ApplyQuestCampaignParams>c__AnonStorey38F
        {
            internal QuestCampaignData data;

            public <ApplyQuestCampaignParams>c__AnonStorey38F()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3E1(UnitData value)
            {
                return (value.UnitParam.iname == this.data.unit);
            }
        }

        [CompilerGenerated]
        private sealed class <GainPlayerExpAnimation>c__Iterator138 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameManager <gm>__0;
            internal int <gainexp>__1;
            internal int <playerexp>__2;
            internal int <lv>__3;
            internal int <lvcap>__4;
            internal int <needexp>__5;
            internal int <nextexp>__6;
            internal float <gainRate>__7;
            internal float <totalTime>__8;
            internal int <expGained>__9;
            internal float <expAccumulator>__10;
            internal int <deltaExp>__11;
            internal int <lvold>__12;
            internal int <exp>__13;
            internal int $PC;
            internal object $current;
            internal RaidResultWindow <>f__this;

            public <GainPlayerExpAnimation>c__Iterator138()
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
                        goto Label_0021;

                    case 1:
                        goto Label_0548;
                }
                goto Label_0570;
            Label_0021:
                if ((this.<>f__this.GainExpWindow != null) == null)
                {
                    goto Label_0559;
                }
                this.<>f__this.GainExpWindow.SetActive(1);
                this.<gm>__0 = MonoSingleton<GameManager>.Instance;
                this.<gainexp>__1 = this.<>f__this.mRaidResult.pexp;
                this.<playerexp>__2 = GlobalVars.PlayerExpOld;
                this.<lv>__3 = PlayerData.CalcLevelFromExp(this.<playerexp>__2);
                this.<lvcap>__4 = this.<gm>__0.MasterParam.GetPlayerLevelCap();
                this.<needexp>__5 = this.<gm>__0.MasterParam.GetPlayerLevelExp(this.<lv>__3);
                this.<nextexp>__6 = ((this.<lv>__3 + 1) > this.<lvcap>__4) ? 0 : this.<gm>__0.MasterParam.GetPlayerNextExp(this.<lv>__3 + 1);
                if (this.<lv>__3 < this.<lvcap>__4)
                {
                    goto Label_0129;
                }
                this.<gainexp>__1 = 0;
                this.<nextexp>__6 = 0;
                this.<playerexp>__2 = Math.Min(this.<playerexp>__2, this.<needexp>__5);
            Label_0129:
                if ((this.<>f__this.TxtPlayerLvVal != null) == null)
                {
                    goto Label_015A;
                }
                this.<>f__this.TxtPlayerLvVal.set_text(&this.<lv>__3.ToString());
            Label_015A:
                if ((this.<>f__this.TxtPlayerExpVal != null) == null)
                {
                    goto Label_0185;
                }
                this.<>f__this.TxtPlayerExpVal.set_text(string.Empty);
            Label_0185:
                if ((this.<>f__this.PlayerGauge != null) == null)
                {
                    goto Label_01FE;
                }
                this.<>f__this.PlayerGauge.set_maxValue((float) ((this.<lv>__3 >= this.<lvcap>__4) ? 1 : this.<nextexp>__6));
                this.<>f__this.PlayerGauge.set_value((float) ((this.<lv>__3 >= this.<lvcap>__4) ? 1 : (this.<playerexp>__2 - this.<needexp>__5)));
            Label_01FE:
                this.<gainRate>__7 = this.<>f__this.ExpGainRate;
                this.<totalTime>__8 = ((float) this.<gainexp>__1) / this.<>f__this.ExpGainRate;
                if (this.<totalTime>__8 <= this.<>f__this.ExpGainTimeMax)
                {
                    goto Label_0257;
                }
                this.<gainRate>__7 = ((float) this.<gainexp>__1) / this.<>f__this.ExpGainTimeMax;
            Label_0257:
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0507", 0f);
                this.<expGained>__9 = 0;
                this.<expAccumulator>__10 = 0f;
                goto Label_0548;
            Label_0282:
                this.<deltaExp>__11 = 0;
                this.<expAccumulator>__10 += this.<gainRate>__7 * Time.get_deltaTime();
                if (this.<>f__this.mExpSkipElement == null)
                {
                    goto Label_02CA;
                }
                this.<expAccumulator>__10 *= this.<>f__this.ExpSkipSpeedMul;
            Label_02CA:
                if (this.<expAccumulator>__10 < 1f)
                {
                    goto Label_0531;
                }
                this.<deltaExp>__11 = Mathf.FloorToInt(this.<expAccumulator>__10);
                this.<expAccumulator>__10 = this.<expAccumulator>__10 % 1f;
                if (this.<gainexp>__1 >= (this.<expGained>__9 + this.<deltaExp>__11))
                {
                    goto Label_0328;
                }
                this.<deltaExp>__11 = this.<gainexp>__1 - this.<expGained>__9;
            Label_0328:
                this.<expGained>__9 += this.<deltaExp>__11;
                if (this.<gainexp>__1 >= this.<expGained>__9)
                {
                    goto Label_0372;
                }
                this.<deltaExp>__11 += this.<expGained>__9 - this.<gainexp>__1;
                this.<expGained>__9 = this.<gainexp>__1;
            Label_0372:
                this.<lvold>__12 = PlayerData.CalcLevelFromExp(this.<playerexp>__2);
                this.<playerexp>__2 += this.<deltaExp>__11;
                this.<lv>__3 = PlayerData.CalcLevelFromExp(this.<playerexp>__2);
                if ((this.<lv>__3 == this.<lvold>__12) || (string.IsNullOrEmpty(this.<>f__this.LevelUpTrigger) != null))
                {
                    goto Label_04A9;
                }
                if ((this.<>f__this.TxtPlayerLvVal != null) == null)
                {
                    goto Label_03FE;
                }
                this.<>f__this.TxtPlayerLvVal.set_text(&this.<lv>__3.ToString());
            Label_03FE:
                this.<needexp>__5 = this.<gm>__0.MasterParam.GetPlayerLevelExp(this.<lv>__3);
                this.<nextexp>__6 = this.<gm>__0.MasterParam.GetPlayerNextExp(this.<lv>__3);
                if ((this.<>f__this.PlayerGauge != null) == null)
                {
                    goto Label_047A;
                }
                this.<>f__this.PlayerGauge.set_maxValue((float) ((this.<lv>__3 >= this.<lvcap>__4) ? 0 : this.<nextexp>__6));
            Label_047A:
                GameUtility.SetAnimatorTrigger(this.<>f__this.PlayerResult, this.<>f__this.LevelUpTrigger);
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0012", 0f);
            Label_04A9:
                this.<exp>__13 = (this.<lv>__3 >= this.<lvcap>__4) ? 1 : (this.<playerexp>__2 - this.<needexp>__5);
                if ((this.<>f__this.PlayerGauge != null) == null)
                {
                    goto Label_0500;
                }
                this.<>f__this.PlayerGauge.set_value((float) this.<exp>__13);
            Label_0500:
                if ((this.<>f__this.TxtPlayerExpVal != null) == null)
                {
                    goto Label_0531;
                }
                this.<>f__this.TxtPlayerExpVal.set_text(&this.<exp>__13.ToString());
            Label_0531:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0572;
            Label_0548:
                if (this.<expGained>__9 < this.<gainexp>__1)
                {
                    goto Label_0282;
                }
            Label_0559:
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 200);
                this.$PC = -1;
            Label_0570:
                return 0;
            Label_0572:
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
        private sealed class <GainUnitExpAnimation>c__Iterator139 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameManager <gm>__0;
            internal int[] <oldMemberLv>__1;
            internal int <i>__2;
            internal int <maxUnitExp>__3;
            internal int <i>__4;
            internal float <gainRate>__5;
            internal float <totalTime>__6;
            internal int <playerLv>__7;
            internal int <expGained>__8;
            internal float <expAccumulator>__9;
            internal int[] <unitExpGained>__10;
            internal int <i>__11;
            internal int <deltaExp>__12;
            internal bool <seFlag>__13;
            internal int <i>__14;
            internal UnitData <unit>__15;
            internal int <unitDeltaExp>__16;
            internal int <lvold>__17;
            internal bool <showPopup>__18;
            internal int <i>__19;
            internal UnitData <unit>__20;
            internal int $PC;
            internal object $current;
            internal RaidResultWindow <>f__this;

            public <GainUnitExpAnimation>c__Iterator139()
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
                        goto Label_0021;

                    case 1:
                        goto Label_04E5;
                }
                goto Label_0627;
            Label_0021:
                if ((this.<>f__this.GainExpWindow != null) == null)
                {
                    goto Label_05E4;
                }
                this.<>f__this.GainExpWindow.SetActive(1);
                this.<gm>__0 = MonoSingleton<GameManager>.Instance;
                this.<oldMemberLv>__1 = new int[this.<>f__this.mRaidResult.members.Count];
                this.<i>__2 = 0;
                goto Label_00BA;
            Label_007F:
                this.<oldMemberLv>__1[this.<i>__2] = this.<>f__this.mRaidResult.members[this.<i>__2].Lv;
                this.<i>__2 += 1;
            Label_00BA:
                if (this.<i>__2 < this.<>f__this.mRaidResult.members.Count)
                {
                    goto Label_007F;
                }
                this.<maxUnitExp>__3 = this.<>f__this.mRaidResult.uexp;
                this.<i>__4 = 0;
                goto Label_012D;
            Label_00FC:
                this.<maxUnitExp>__3 = Mathf.Max(this.<maxUnitExp>__3, this.<>f__this.AcquiredUnitExp[this.<i>__4]);
                this.<i>__4 += 1;
            Label_012D:
                if (this.<i>__4 < ((int) this.<>f__this.AcquiredUnitExp.Length))
                {
                    goto Label_00FC;
                }
                this.<gainRate>__5 = this.<>f__this.ExpGainRate;
                this.<totalTime>__6 = ((float) this.<maxUnitExp>__3) / this.<>f__this.ExpGainRate;
                if (this.<totalTime>__6 <= this.<>f__this.ExpGainTimeMax)
                {
                    goto Label_019E;
                }
                this.<gainRate>__5 = ((float) this.<maxUnitExp>__3) / this.<>f__this.ExpGainTimeMax;
            Label_019E:
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0507", 0f);
                this.<playerLv>__7 = this.<gm>__0.Player.Lv;
                this.<expGained>__8 = 0;
                this.<expAccumulator>__9 = 0f;
                this.<unitExpGained>__10 = new int[this.<>f__this.mRaidResult.members.Count];
                this.<i>__11 = 0;
                goto Label_0222;
            Label_0206:
                this.<unitExpGained>__10[this.<i>__11] = 0;
                this.<i>__11 += 1;
            Label_0222:
                if (this.<i>__11 < ((int) this.<unitExpGained>__10.Length))
                {
                    goto Label_0206;
                }
                goto Label_04E5;
            Label_023A:
                this.<deltaExp>__12 = 0;
                this.<expAccumulator>__9 += this.<gainRate>__5 * Time.get_deltaTime();
                if (this.<>f__this.mExpSkipElement == null)
                {
                    goto Label_0282;
                }
                this.<expAccumulator>__9 *= this.<>f__this.ExpSkipSpeedMul;
            Label_0282:
                if (this.<expAccumulator>__9 < 1f)
                {
                    goto Label_04CE;
                }
                this.<deltaExp>__12 = Mathf.FloorToInt(this.<expAccumulator>__9);
                this.<expAccumulator>__9 = this.<expAccumulator>__9 % 1f;
                this.<expGained>__8 += this.<deltaExp>__12;
                if (this.<maxUnitExp>__3 >= this.<expGained>__8)
                {
                    goto Label_0305;
                }
                this.<deltaExp>__12 = Math.Max(this.<deltaExp>__12 - (this.<expGained>__8 - this.<maxUnitExp>__3), 0);
                this.<expGained>__8 = this.<maxUnitExp>__3;
            Label_0305:
                this.<seFlag>__13 = 0;
                this.<i>__14 = 0;
                goto Label_04AE;
            Label_0318:
                this.<unit>__15 = this.<>f__this.mRaidResult.members[this.<i>__14];
                this.<unitDeltaExp>__16 = this.<deltaExp>__12;
                *((int*) &(this.<unitExpGained>__10[this.<i>__14])) += this.<unitDeltaExp>__16;
                if (this.<>f__this.AcquiredUnitExp[this.<i>__14] >= this.<unitExpGained>__10[this.<i>__14])
                {
                    goto Label_03D6;
                }
                this.<unitDeltaExp>__16 = Math.Max(this.<unitDeltaExp>__16 - (this.<unitExpGained>__10[this.<i>__14] - this.<>f__this.AcquiredUnitExp[this.<i>__14]), 0);
                this.<unitExpGained>__10[this.<i>__14] = this.<>f__this.AcquiredUnitExp[this.<i>__14];
            Label_03D6:
                if (this.<unitDeltaExp>__16 != null)
                {
                    goto Label_03E6;
                }
                goto Label_04A0;
            Label_03E6:
                this.<lvold>__17 = this.<unit>__15.Lv;
                this.<unit>__15.GainExp(this.<unitDeltaExp>__16, this.<playerLv>__7);
                GameParameter.UpdateAll(this.<>f__this.mUnitListItems[this.<i>__14]);
                if (this.<unit>__15.Lv == this.<lvold>__17)
                {
                    goto Label_04A0;
                }
                if (string.IsNullOrEmpty(this.<>f__this.LevelUpTrigger) != null)
                {
                    goto Label_04A0;
                }
                GameUtility.SetAnimatorTrigger(this.<>f__this.mUnitListItems[this.<i>__14], this.<>f__this.LevelUpTrigger);
                if (this.<seFlag>__13 != null)
                {
                    goto Label_04A0;
                }
                this.<seFlag>__13 = 1;
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0012", 0f);
            Label_04A0:
                this.<i>__14 += 1;
            Label_04AE:
                if (this.<i>__14 < this.<>f__this.mRaidResult.members.Count)
                {
                    goto Label_0318;
                }
            Label_04CE:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0629;
            Label_04E5:
                if (this.<expGained>__8 < this.<maxUnitExp>__3)
                {
                    goto Label_023A;
                }
                MonoSingleton<GameManager>.Instance.Player.UpdateUnitTrophyStates(0);
                this.<showPopup>__18 = 0;
                this.<i>__19 = 0;
                goto Label_05A4;
            Label_0519:
                this.<unit>__20 = this.<>f__this.mRaidResult.members[this.<i>__19];
                if (this.<unit>__20.IsOpenCharacterQuest() == null)
                {
                    goto Label_0596;
                }
                if (this.<unit>__20.OpenCharacterQuestOnQuestResult(this.<>f__this.mRaidResult.chquest[this.<i>__19], this.<oldMemberLv>__1[this.<i>__19]) == null)
                {
                    goto Label_0596;
                }
                this.<showPopup>__18 = 1;
                this.<gm>__0.AddCharacterQuestPopup(this.<unit>__20);
            Label_0596:
                this.<i>__19 += 1;
            Label_05A4:
                if (this.<i>__19 < this.<>f__this.mRaidResult.members.Count)
                {
                    goto Label_0519;
                }
                if (this.<showPopup>__18 == null)
                {
                    goto Label_05E4;
                }
                this.<gm>__0.ShowCharacterQuestPopup(GameSettings.Instance.CharacterQuest_Unlock);
            Label_05E4:
                if ((this.<>f__this.ExpSkipButton != null) == null)
                {
                    goto Label_0610;
                }
                this.<>f__this.ExpSkipButton.get_gameObject().SetActive(0);
            Label_0610:
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 300);
                this.$PC = -1;
            Label_0627:
                return 0;
            Label_0629:
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
        private sealed class <MergeDropItems>c__AnonStorey38E
        {
            internal string iid;

            public <MergeDropItems>c__AnonStorey38E()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3E0(UnitData p)
            {
                return (p.UnitParam.iname == this.iid);
            }
        }

        [CompilerGenerated]
        private sealed class <QuestResultAnimation>c__Iterator137 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <index>__0;
            internal float <time>__1;
            internal ListExtras <list>__2;
            internal ListExtras <list>__3;
            internal int $PC;
            internal object $current;
            internal RaidResultWindow <>f__this;

            public <QuestResultAnimation>c__Iterator137()
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
                        goto Label_010A;

                    case 2:
                        goto Label_0194;

                    case 3:
                        goto Label_022C;
                }
                goto Label_0349;
            Label_0029:
                this.<index>__0 = 0;
                this.<time>__1 = 0f;
                if ((this.<>f__this.ResultLayout != null) == null)
                {
                    goto Label_022C;
                }
                this.<>f__this.ResultLayout.set_enabled(1);
                goto Label_022C;
            Label_0067:
                this.<>f__this.mCurrentElement = this.<>f__this.mResults[this.<index>__0].GetComponent<RaidResultElement>();
                if (this.<>f__this.mItemSkipElement == null)
                {
                    goto Label_00B8;
                }
                this.<>f__this.mCurrentElement.TimeScale = this.<>f__this.SkipTimeScale;
            Label_00B8:
                if ((this.<>f__this.mCurrentElement != null) == null)
                {
                    goto Label_0199;
                }
                if (this.<>f__this.mCurrentElement.IsRequest() != null)
                {
                    goto Label_010F;
                }
                this.<>f__this.mCurrentElement.RequestAnimation();
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_034B;
            Label_010A:
                goto Label_022C;
            Label_010F:
                if (this.<>f__this.mCurrentElement.IsFinished() != null)
                {
                    goto Label_0199;
                }
                if ((this.<>f__this.ResultLayout != null) == null)
                {
                    goto Label_017D;
                }
                if (this.<index>__0 <= 0)
                {
                    goto Label_017D;
                }
                this.<list>__2 = this.<>f__this.ResultLayout.GetComponent<ListExtras>();
                if ((this.<list>__2 != null) == null)
                {
                    goto Label_017D;
                }
                this.<list>__2.PageDown(1f);
            Label_017D:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_034B;
            Label_0194:
                goto Label_022C;
            Label_0199:
                this.<time>__1 += Time.get_deltaTime() * this.<>f__this.mCurrentElement.TimeScale;
                goto Label_01FA;
            Label_01C1:
                if (this.<time>__1 < this.<>f__this.ResultScrollInterval)
                {
                    goto Label_0215;
                }
                this.<time>__1 = 0f;
                this.<index>__0 += 1;
                goto Label_01FA;
                goto Label_0215;
            Label_01FA:
                if (this.<index>__0 < this.<>f__this.mResults.Count)
                {
                    goto Label_01C1;
                }
            Label_0215:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 3;
                goto Label_034B;
            Label_022C:
                if (this.<index>__0 < this.<>f__this.mResults.Count)
                {
                    goto Label_0067;
                }
                if ((this.<>f__this.ResultLayout != null) == null)
                {
                    goto Label_0294;
                }
                this.<list>__3 = this.<>f__this.ResultLayout.GetComponent<ListExtras>();
                if ((this.<list>__3 != null) == null)
                {
                    goto Label_0294;
                }
                this.<list>__3.PageDown(999f);
            Label_0294:
                if ((this.<>f__this.BtnUp != null) == null)
                {
                    goto Label_02BB;
                }
                this.<>f__this.BtnUp.set_interactable(1);
            Label_02BB:
                if ((this.<>f__this.BtnDown != null) == null)
                {
                    goto Label_02E2;
                }
                this.<>f__this.BtnDown.set_interactable(1);
            Label_02E2:
                if ((this.<>f__this.ResultLayout != null) == null)
                {
                    goto Label_0309;
                }
                this.<>f__this.ResultLayout.set_enabled(1);
            Label_0309:
                if ((this.<>f__this.SkipButton != null) == null)
                {
                    goto Label_0335;
                }
                this.<>f__this.SkipButton.get_gameObject().SetActive(0);
            Label_0335:
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 100);
                this.$PC = -1;
            Label_0349:
                return 0;
            Label_034B:
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

