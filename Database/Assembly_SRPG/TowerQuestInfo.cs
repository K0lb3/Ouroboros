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

    [Pin(0, "表示更新", 0, 0), Pin(0x4b0, "クエスト詳細ウィンドウ（開いた）", 1, 0x4b0), Pin(210, "クエスト詳細ウィンドウ（閉じる）", 0, 210), Pin(200, "クエスト詳細ウィンドウ（開く）", 0, 200), Pin(10, "表示更新(強制)", 0, 10)]
    public class TowerQuestInfo : MonoBehaviour, IFlowInterface
    {
        private const int INPUT_REFRESH_UI = 0;
        private const int INPUT_FORCE_REFRESH_UI = 10;
        private const int INPUT_QUEST_DETAIL_OPEN = 200;
        private const int INPUT_QUEST_DETAIL_CLOSE = 210;
        private const int OUTPUT_QUEST_DETAIL_OPENED = 0x4b0;
        [SerializeField, HeaderBar("▼敵情報表示する用")]
        private GameObject EnemiesRoot;
        [SerializeField]
        private GameObject EnemyTemplate;
        [SerializeField]
        private GameObject EnemyTemplateUnKnown;
        [HeaderBar("▼報酬表示用"), SerializeField]
        private Text RewardText;
        [SerializeField]
        private Text RecommendText;
        [SerializeField]
        private GameObject ItemRoot;
        [SerializeField]
        private GameObject ArtifactRoot;
        [SerializeField]
        private GameObject CoinRoot;
        [HeaderBar("▼敵情報が表示できない階層に表示される"), SerializeField]
        private GameObject UnkownIcon;
        [HeaderBar("▼クリア済みの階層に表示される"), SerializeField]
        private GameObject ClearIcon;
        [StringIsResourcePath(typeof(GameObject)), HeaderBar("▼詳細表示用プレハブ"), SerializeField]
        private string Path_TowerQuestDetail;
        [SerializeField, HeaderBar("▼バトルリセットボタン")]
        private Button BattleResetButton;
        [SerializeField, HeaderBar("▼リセットに必要な幻晶石数")]
        private Text m_BattleResetCost;
        private GameObject m_TowerQuestDetail;
        private List<TowerEnemyListItem> EnemyList;
        private List<TowerEnemyListItem> UnknownEnemyList;
        private string FloorID;
        private Coroutine m_PrefabLoadRoutine;

        public TowerQuestInfo()
        {
            this.EnemyList = new List<TowerEnemyListItem>();
            this.UnknownEnemyList = new List<TowerEnemyListItem>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_003C;
            }
            if (string.IsNullOrEmpty(this.FloorID) != null)
            {
                goto Label_002B;
            }
            if ((this.FloorID != GlobalVars.SelectedQuestID) == null)
            {
                goto Label_0031;
            }
        Label_002B:
            this.Refresh();
        Label_0031:
            this.CheckBattleResetButton();
            goto Label_007C;
        Label_003C:
            if (pinID != 10)
            {
                goto Label_0055;
            }
            this.Refresh();
            this.CheckBattleResetButton();
            goto Label_007C;
        Label_0055:
            if (pinID != 200)
            {
                goto Label_006B;
            }
            this.OnQuestDetailOpen();
            goto Label_007C;
        Label_006B:
            if (pinID != 210)
            {
                goto Label_007C;
            }
            this.Refresh();
        Label_007C:
            return;
        }

        private void Awake()
        {
            GameUtility.SetGameObjectActive(this.EnemyTemplate, 0);
            GameUtility.SetGameObjectActive(this.UnkownIcon, 0);
            GameUtility.SetGameObjectActive(this.ClearIcon, 0);
            GameUtility.SetGameObjectActive(this.EnemyTemplateUnKnown, 0);
            return;
        }

        private void CheckBattleResetButton()
        {
            TowerParam param;
            TowerResuponse resuponse;
            TowerFloorParam param2;
            param = MonoSingleton<GameManager>.Instance.FindTower(GlobalVars.SelectedTowerID);
            if (param != null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            if (resuponse != null)
            {
                goto Label_0029;
            }
            return;
        Label_0029:
            param2 = resuponse.GetCurrentFloor();
            if (param2 != null)
            {
                goto Label_0037;
            }
            return;
        Label_0037:
            if (MonoSingleton<GameManager>.Instance.Player.Coin <= param.floor_reset_coin)
            {
                goto Label_0082;
            }
            if ((GlobalVars.SelectedQuestID == param2.iname) == null)
            {
                goto Label_0082;
            }
            if (resuponse.CheckEnemyDeck() == null)
            {
                goto Label_0082;
            }
            this.BattleResetButton.set_interactable(1);
            goto Label_008E;
        Label_0082:
            this.BattleResetButton.set_interactable(0);
        Label_008E:
            return;
        }

        private void CreateQuestDetailWindow(Object template)
        {
            QuestParam param;
            QuestCampaignData[] dataArray;
            TowerResuponse resuponse;
            TowerScore score;
            param = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
            if (((this.m_TowerQuestDetail == null) == null) || (param == null))
            {
                goto Label_00C0;
            }
            this.m_TowerQuestDetail = Object.Instantiate(template) as GameObject;
            DataSource.Bind<QuestParam>(this.m_TowerQuestDetail, param);
            dataArray = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(param);
            DataSource.Bind<QuestCampaignData[]>(this.m_TowerQuestDetail, (((int) dataArray.Length) != null) ? dataArray : null);
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            score = this.m_TowerQuestDetail.GetComponentInChildren<TowerScore>();
            if ((score != null) == null)
            {
                goto Label_00B4;
            }
            if (resuponse == null)
            {
                goto Label_00B4;
            }
            score.SetScoreTitleText(param.name);
            score.Setup(resuponse.FloorScores, resuponse.FloorSpdRank, resuponse.FloorTecRank);
        Label_00B4:
            this.m_TowerQuestDetail.SetActive(1);
        Label_00C0:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x4b0);
            return;
        }

        private void HideAllEnemyIcon()
        {
            int num;
            int num2;
            num = 0;
            goto Label_0022;
        Label_0007:
            this.UnknownEnemyList[num].get_gameObject().SetActive(0);
            num += 1;
        Label_0022:
            if (num < this.UnknownEnemyList.Count)
            {
                goto Label_0007;
            }
            num2 = 0;
            goto Label_0055;
        Label_003A:
            this.EnemyList[num2].get_gameObject().SetActive(0);
            num2 += 1;
        Label_0055:
            if (num2 < this.EnemyList.Count)
            {
                goto Label_003A;
            }
            return;
        }

        private void OnDestroy()
        {
            if ((this.m_TowerQuestDetail != null) == null)
            {
                goto Label_0023;
            }
            Object.DestroyObject(this.m_TowerQuestDetail);
            this.m_TowerQuestDetail = null;
        Label_0023:
            return;
        }

        public void OnQuestDetailOpen()
        {
            if (this.m_PrefabLoadRoutine != null)
            {
                goto Label_002F;
            }
            this.m_PrefabLoadRoutine = base.StartCoroutine(this.PrefabLoadRoutine(this.Path_TowerQuestDetail, new Action<Object>(this.CreateQuestDetailWindow)));
        Label_002F:
            return;
        }

        [DebuggerHidden]
        private IEnumerator PrefabLoadRoutine(string path, Action<Object> onLoadComplete)
        {
            <PrefabLoadRoutine>c__Iterator13F iteratorf;
            iteratorf = new <PrefabLoadRoutine>c__Iterator13F();
            iteratorf.path = path;
            iteratorf.onLoadComplete = onLoadComplete;
            iteratorf.<$>path = path;
            iteratorf.<$>onLoadComplete = onLoadComplete;
            iteratorf.<>f__this = this;
            return iteratorf;
        }

        public unsafe void Refresh()
        {
            object[] objArray1;
            TowerFloorParam param;
            QuestParam param2;
            int num;
            TowerFloorParam param3;
            TowerRewardParam param4;
            string str;
            string str2;
            JSON_MapUnit unit;
            TowerResuponse resuponse;
            TowerFloorParam param5;
            bool flag;
            bool flag2;
            List<EnemyIconData> list;
            bool flag3;
            RandDeckResult result;
            Unit unit2;
            NPCSetting setting;
            EnemyIconData data;
            List<EnemyIconData> list2;
            int num2;
            int num3;
            int num4;
            List<EnemyIconData> list3;
            List<EnemyIconData> list4;
            List<EnemyIconData> list5;
            int num5;
            int num6;
            int num7;
            int num8;
            EnemyIconData data2;
            int num9;
            TowerRewardParam param6;
            Text text;
            TowerRewardParam param7;
            TowerParam param8;
            <Refresh>c__AnonStorey3AE storeyae;
            param = MonoSingleton<GameManager>.Instance.FindTowerFloor(GlobalVars.SelectedQuestID);
            if (param != null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            param2 = param.GetQuestParam();
            DataSource.Bind<QuestParam>(base.get_gameObject(), param2);
            this.SetRecommendText(param.lv, param.joblv);
            num = base.GetComponentInParent<FlowNode_DownloadTowerMapSets>().DownloadAssetNum;
            param3 = MonoSingleton<GameManager>.Instance.TowerResuponse.GetCurrentFloor();
            if (param3 != null)
            {
                goto Label_005F;
            }
            return;
        Label_005F:
            if (param.FloorIndex >= (param3.FloorIndex + num))
            {
                goto Label_0594;
            }
            if (param2.state != 2)
            {
                goto Label_00C7;
            }
            GameUtility.SetGameObjectActive(this.UnkownIcon, 0);
            GameUtility.SetGameObjectActive(this.RewardText, 1);
            GameUtility.SetGameObjectActive(this.ClearIcon, 1);
            this.HideAllEnemyIcon();
            param4 = MonoSingleton<GameManager>.Instance.FindTowerReward(param.reward_id);
            this.SetRewards(param4);
            goto Label_058F;
        Label_00C7:
            str = AssetPath.LocalMap(param.map[0].mapSetName);
            str2 = AssetManager.LoadTextData(str);
            if (string.IsNullOrEmpty(str2) == null)
            {
                goto Label_0111;
            }
            DebugUtility.LogError("配置ファイルがありません : QuestIname = " + param.iname + ",SetFilePath = " + str);
            return;
        Label_0111:
            unit = JSONParser.parseJSONObject<JSON_MapUnit>(str2);
            GameUtility.SetGameObjectActive(this.UnkownIcon, 0);
            GameUtility.SetGameObjectActive(this.RewardText, 1);
            GameUtility.SetGameObjectActive(this.ClearIcon, 0);
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            this.HideAllEnemyIcon();
            if (unit.enemy == null)
            {
                goto Label_0575;
            }
            param5 = null;
            if (MonoSingleton<GameManager>.Instance.TowerResuponse == null)
            {
                goto Label_017F;
            }
            param5 = MonoSingleton<GameManager>.Instance.TowerResuponse.GetCurrentFloor();
        Label_017F:
            flag = (resuponse.lot_enemies == null) ? 0 : (((int) resuponse.lot_enemies.Length) > 0);
            flag2 = param.iname == param3.iname;
            if (flag == null)
            {
                goto Label_01D3;
            }
            if (flag2 == null)
            {
                goto Label_01D3;
            }
            unit.enemy = unit.ReplacedRandEnemy(resuponse.lot_enemies, 0);
        Label_01D3:
            list = new List<EnemyIconData>();
            storeyae = new <Refresh>c__AnonStorey3AE();
            storeyae.i = 0;
            goto Label_02CE;
        Label_01EE:
            flag3 = 0;
            if (unit.enemy[storeyae.i].IsRandSymbol == null)
            {
                goto Label_020D;
            }
            flag3 = 1;
        Label_020D:
            if (flag == null)
            {
                goto Label_0240;
            }
            if (flag2 == null)
            {
                goto Label_0240;
            }
            flag3 = (Array.Find<RandDeckResult>(resuponse.lot_enemies, new Predicate<RandDeckResult>(storeyae.<>m__428)) == null) == 0;
        Label_0240:
            unit2 = null;
            if (unit.enemy[storeyae.i].IsRandSymbol != null)
            {
                goto Label_0286;
            }
            setting = new NPCSetting(unit.enemy[storeyae.i]);
            unit2 = new Unit();
            unit2.Setup(null, setting, null, null);
        Label_0286:
            data = new EnemyIconData();
            data.unit = unit2;
            data.enemy = unit.enemy[storeyae.i];
            data.is_rand = flag3;
            list.Add(data);
            storeyae.i += 1;
        Label_02CE:
            if (storeyae.i < ((int) unit.enemy.Length))
            {
                goto Label_01EE;
            }
            list2 = new List<EnemyIconData>();
            num2 = 0;
            goto Label_0342;
        Label_02F2:
            if (list[num2].enemy.IsRandSymbol == null)
            {
                goto Label_030F;
            }
            goto Label_033C;
        Label_030F:
            if (list[num2].unit.IsGimmick == null)
            {
                goto Label_032C;
            }
            goto Label_033C;
        Label_032C:
            list2.Add(list[num2]);
        Label_033C:
            num2 += 1;
        Label_0342:
            if (num2 < list.Count)
            {
                goto Label_02F2;
            }
            list = new List<EnemyIconData>(list2);
            if (param5 == null)
            {
                goto Label_040A;
            }
            if ((param5.iname == GlobalVars.SelectedQuestID) == null)
            {
                goto Label_040A;
            }
            if (MonoSingleton<GameManager>.Instance.TowerEnemyUnit == null)
            {
                goto Label_040A;
            }
            num3 = 0;
            goto Label_03FC;
        Label_038D:
            if (list[num3].unit != null)
            {
                goto Label_03A5;
            }
            goto Label_03F6;
        Label_03A5:
            num4 = list[num3].unit.MaximumStatus.param.hp - MonoSingleton<GameManager>.Instance.TowerEnemyUnit[num3].hp;
            list[num3].unit.Damage(num4, 0);
        Label_03F6:
            num3 += 1;
        Label_03FC:
            if (num3 < list.Count)
            {
                goto Label_038D;
            }
        Label_040A:
            list3 = new List<EnemyIconData>();
            list4 = new List<EnemyIconData>();
            list5 = new List<EnemyIconData>();
            num5 = 0;
            goto Label_0455;
        Label_0427:
            if (list[num5].is_rand == null)
            {
                goto Label_043F;
            }
            goto Label_044F;
        Label_043F:
            list4.Add(list[num5]);
        Label_044F:
            num5 += 1;
        Label_0455:
            if (num5 < list.Count)
            {
                goto Label_0427;
            }
            if (resuponse.lot_enemies == null)
            {
                goto Label_0480;
            }
            if (param.FloorIndex <= param3.FloorIndex)
            {
                goto Label_0517;
            }
        Label_0480:
            if (unit.deck == null)
            {
                goto Label_055B;
            }
            if (((int) unit.deck.Length) <= 0)
            {
                goto Label_055B;
            }
            if (unit.rand_tag == null)
            {
                goto Label_055B;
            }
            if (((int) unit.rand_tag.Length) <= 0)
            {
                goto Label_055B;
            }
            num6 = 0;
            num7 = 0;
            goto Label_04DB;
        Label_04C1:
            num6 += unit.rand_tag[num7].spawn;
            num7 += 1;
        Label_04DB:
            if (num7 < ((int) unit.rand_tag.Length))
            {
                goto Label_04C1;
            }
            num8 = 0;
            goto Label_0509;
        Label_04F3:
            data2 = new EnemyIconData();
            list5.Add(data2);
            num8 += 1;
        Label_0509:
            if (num8 < num6)
            {
                goto Label_04F3;
            }
            goto Label_055B;
        Label_0517:
            num9 = 0;
            goto Label_054D;
        Label_051F:
            if (list[num9].is_rand != null)
            {
                goto Label_0537;
            }
            goto Label_0547;
        Label_0537:
            list5.Add(list[num9]);
        Label_0547:
            num9 += 1;
        Label_054D:
            if (num9 < list.Count)
            {
                goto Label_051F;
            }
        Label_055B:
            list3.AddRange(list4);
            list3.AddRange(list5);
            this.SetIcon(list3);
        Label_0575:
            param6 = MonoSingleton<GameManager>.Instance.FindTowerReward(param.reward_id);
            this.SetRewards(param6);
        Label_058F:
            goto Label_062C;
        Label_0594:
            GameUtility.SetGameObjectActive(this.UnkownIcon, 1);
            GameUtility.SetGameObjectActive(this.RewardText, 1);
            GameUtility.SetGameObjectActive(this.ClearIcon, 0);
            if ((this.UnkownIcon != null) == null)
            {
                goto Label_060C;
            }
            text = this.UnkownIcon.GetComponent<Text>();
            if ((text != null) == null)
            {
                goto Label_060C;
            }
            objArray1 = new object[] { (int) ((param.FloorIndex - num) + 1) };
            text.set_text(LocalizedText.Get("sys.TOWER_UNKNOWN_TEXT", objArray1));
        Label_060C:
            this.HideAllEnemyIcon();
            param7 = MonoSingleton<GameManager>.Instance.FindTowerReward(param.reward_id);
            this.SetRewards(param7);
        Label_062C:
            if ((this.m_BattleResetCost != null) == null)
            {
                goto Label_0666;
            }
            param8 = MonoSingleton<GameManager>.Instance.FindTower(param.tower_id);
            this.m_BattleResetCost.set_text(&param8.floor_reset_coin.ToString());
        Label_0666:
            GameParameter.UpdateAll(base.get_gameObject());
            this.FloorID = GlobalVars.SelectedQuestID;
            return;
        }

        private void SetIcon(List<EnemyIconData> icon_datas)
        {
            int num;
            bool flag;
            GameObject obj2;
            List<TowerEnemyListItem> list;
            TowerEnemyListItem item;
            int num2;
            int num3;
            int num4;
            num = 0;
            goto Label_00C6;
        Label_0007:
            flag = icon_datas[num].unit == null;
            obj2 = this.EnemyTemplate;
            list = this.EnemyList;
            if (flag == null)
            {
                goto Label_0039;
            }
            obj2 = this.EnemyTemplateUnKnown;
            list = this.UnknownEnemyList;
        Label_0039:
            item = null;
            if (list.Count > num)
            {
                goto Label_0087;
            }
            item = Object.Instantiate<GameObject>(obj2).GetComponent<TowerEnemyListItem>();
            item.get_transform().SetParent(this.EnemiesRoot.get_transform(), 0);
            item.get_gameObject().SetActive(1);
            list.Add(item);
            goto Label_009D;
        Label_0087:
            item = list[num];
            item.get_gameObject().SetActive(1);
        Label_009D:
            if (flag != null)
            {
                goto Label_00C2;
            }
            DataSource.Bind<Unit>(item.get_gameObject(), icon_datas[num].unit);
            item.UpdateValue();
        Label_00C2:
            num += 1;
        Label_00C6:
            if (num < icon_datas.Count)
            {
                goto Label_0007;
            }
            num2 = 0;
            num3 = 0;
            goto Label_0102;
        Label_00DD:
            this.EnemyList[num3].get_transform().SetSiblingIndex(num2);
            num2 += 1;
            num3 += 1;
        Label_0102:
            if (num3 < this.EnemyList.Count)
            {
                goto Label_00DD;
            }
            num4 = 0;
            goto Label_0141;
        Label_011C:
            this.UnknownEnemyList[num4].get_transform().SetSiblingIndex(num2);
            num2 += 1;
            num4 += 1;
        Label_0141:
            if (num4 < this.UnknownEnemyList.Count)
            {
                goto Label_011C;
            }
            return;
        }

        private void SetRecommendText(int lv, int joblv)
        {
            object[] objArray1;
            if ((this.RecommendText == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            objArray1 = new object[] { (int) lv, (int) joblv };
            this.RecommendText.set_text(LocalizedText.Get("sys.TOWER_RECOMMENDATION_TEXT", objArray1));
            return;
        }

        private unsafe void SetRewards(TowerRewardParam rewardParam)
        {
            GameManager manager;
            TowerRewardItem item;
            List<TowerRewardItem>.Enumerator enumerator;
            string str;
            ItemParam param;
            ArtifactParam param2;
            TowerRewardItem.RewardType type;
            if (rewardParam != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if ((this.RewardText == null) == null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            GameUtility.SetGameObjectActive(this.ItemRoot, 0);
            GameUtility.SetGameObjectActive(this.ArtifactRoot, 0);
            GameUtility.SetGameObjectActive(this.CoinRoot, 0);
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            enumerator = rewardParam.GetTowerRewardItem().GetEnumerator();
        Label_004F:
            try
            {
                goto Label_01BB;
            Label_0054:
                item = &enumerator.Current;
                if (item.visible != null)
                {
                    goto Label_006C;
                }
                goto Label_01BB;
            Label_006C:
                if (item.type != 1)
                {
                    goto Label_007D;
                }
                goto Label_01BB;
            Label_007D:
                str = string.Empty;
                switch (item.type)
                {
                    case 0:
                        goto Label_00B3;

                    case 1:
                        goto Label_0195;

                    case 2:
                        goto Label_00F9;

                    case 3:
                        goto Label_011A;

                    case 4:
                        goto Label_012A;

                    case 5:
                        goto Label_013A;

                    case 6:
                        goto Label_014A;
                }
                goto Label_0195;
            Label_00B3:
                param = manager.GetItemParam(item.iname);
                if (param == null)
                {
                    goto Label_00D0;
                }
                str = param.name;
            Label_00D0:
                DataSource.Bind<ItemParam>(this.ItemRoot, param);
                GameUtility.SetGameObjectActive(this.ItemRoot, 1);
                GameParameter.UpdateAll(this.ItemRoot);
                goto Label_0195;
            Label_00F9:
                str = LocalizedText.Get("sys.COIN");
                this.CoinRoot.get_gameObject().SetActive(1);
                goto Label_0195;
            Label_011A:
                str = LocalizedText.Get("sys.ARENA_COIN");
                goto Label_0195;
            Label_012A:
                str = LocalizedText.Get("sys.MULTI_COIN");
                goto Label_0195;
            Label_013A:
                str = LocalizedText.Get("sys.PIECE_POINT");
                goto Label_0195;
            Label_014A:
                param2 = manager.MasterParam.GetArtifactParam(item.iname);
                if (param2 == null)
                {
                    goto Label_016C;
                }
                str = param2.name;
            Label_016C:
                DataSource.Bind<ArtifactParam>(this.ArtifactRoot, param2);
                GameUtility.SetGameObjectActive(this.ArtifactRoot, 1);
                GameParameter.UpdateAll(this.ArtifactRoot);
            Label_0195:
                this.RewardText.set_text(string.Format("{0} \x00d7 {1}", str, (int) item.num));
                goto Label_01C7;
            Label_01BB:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0054;
                }
            Label_01C7:
                goto Label_01D8;
            }
            finally
            {
            Label_01CC:
                ((List<TowerRewardItem>.Enumerator) enumerator).Dispose();
            }
        Label_01D8:
            return;
        }

        [CompilerGenerated]
        private sealed class <PrefabLoadRoutine>c__Iterator13F : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string path;
            internal LoadRequest <req>__0;
            internal Action<Object> onLoadComplete;
            internal int $PC;
            internal object $current;
            internal string <$>path;
            internal Action<Object> <$>onLoadComplete;
            internal TowerQuestInfo <>f__this;

            public <PrefabLoadRoutine>c__Iterator13F()
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
                        goto Label_005F;
                }
                goto Label_00B8;
            Label_0021:
                this.<req>__0 = AssetManager.LoadAsync<GameObject>(this.path);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_005F;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_00BA;
            Label_005F:
                if ((this.<req>__0.asset != null) == null)
                {
                    goto Label_0090;
                }
                this.onLoadComplete(this.<req>__0.asset);
                goto Label_00A5;
            Label_0090:
                DebugUtility.LogError(string.Format("プレハブのロードに失敗 => {0}", this.path));
            Label_00A5:
                this.<>f__this.m_PrefabLoadRoutine = null;
                this.$PC = -1;
            Label_00B8:
                return 0;
            Label_00BA:
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
        private sealed class <Refresh>c__AnonStorey3AE
        {
            internal int i;

            public <Refresh>c__AnonStorey3AE()
            {
                base..ctor();
                return;
            }

            internal bool <>m__428(RandDeckResult lot)
            {
                return (lot.set_id == this.i);
            }
        }

        private class EnemyIconData
        {
            public bool is_rand;
            public JSON_MapEnemyUnit enemy;
            public Unit unit;

            public EnemyIconData()
            {
                base..ctor();
                return;
            }
        }
    }
}

