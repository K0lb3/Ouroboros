namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0x29, "ランクアップ演出終了", 0, 0x29), Pin(100, "演出スキップ", 0, 100), Pin(40, "ランクアップ演出表示", 1, 40), Pin(10, "演出開始", 0, 10), Pin(0x1f, "演出終了", 1, 0x1f)]
    public class QuestResult : MonoBehaviour, IFlowInterface
    {
        [Description("確認用に使用するユニットのID。ユニットID@ジョブIDで指定する。")]
        public string[] DebugUnitIDs;
        public bool[] DebugObjectiveFlags;
        public string DebugMasterAbilityID;
        [Description("ユニットアイコンを梱包する親ゲームオブジェクト")]
        public GameObject UnitList;
        [Description("ユニットアイコンのゲームオブジェクト")]
        public GameObject UnitListItem;
        [Description("ユニット獲得経験値のゲームオブジェクト")]
        public GameObject UnitExpText;
        [Description("入手アイテムのリストになる親ゲームオブジェクト")]
        public GameObject TreasureList;
        [Description("入手アイテムのゲームオブジェクト")]
        public GameObject TreasureListItem;
        [Description("入手ユニットのゲームオブジェクト")]
        public GameObject TreasureListUnit;
        [Description("入手武具のゲームオブジェクト")]
        public GameObject TreasureListArtifact;
        [Description("バトルコインのゲームオブジェクト")]
        public GameObject TreasureListBattleCoin;
        [Description("入手真理念装のゲームオブジェクト")]
        public GameObject TreasureListConceptCard;
        [Description("クリア条件の星を白星に切り替えるトリガーの名前")]
        public string Star_TurnOnTrigger;
        [Description("クリア条件の星が白星にならなかった場合のトリガーの名前")]
        public string Star_KeepOffTrigger;
        [Description("クリア条件の星が白星に既になってる場合のトリガーの名前")]
        public string Star_ClearTrigger;
        [Description("クリア条件の星にトリガーを送る間隔 (秒数)")]
        public float Star_TriggerInterval;
        [Description("クリア条件の星で黒星を無視する")]
        public bool Star_SkipDarkStar;
        [Description("入手アイテムを可視状態に切り替えるトリガー")]
        public string Treasure_TurnOnTrigger;
        [Description("入手アイテムを可視状態に切り替える間隔 (秒数)")]
        public float Treasure_TriggerInterval;
        public GameObject Prefab_NewItemBadge;
        public GameObject Prefab_MasterAbilityPopup;
        public GameObject Prefab_UnitDataUnlockPopup;
        public Text TextConsumeAp;
        public Color TextConsumeApColor;
        [Description("ユニットのレベルアップ時に使用するトリガー。ユニットのゲームオブジェクトにアタッチされたAnimatorへ送られます。")]
        public string Unit_LevelUpTrigger;
        [Description("一秒あたりの経験値の増加量")]
        public float ExpGainRate;
        [Description("経験値増加アニメーションの最長時間。経験値がExpGainRateの速度で増加する時、これで設定した時間を超える時に加算速度を上げる。")]
        public float ExpGainTimeMax;
        protected List<GameObject> mUnitListItems;
        private List<GameObject> mTreasureListItems;
        private bool mUseLarge;
        public string PreStarAnimationTrigger;
        public string PostStarAnimationTrigger;
        public float PreStarAnimationDelay;
        public float PostStarAnimationDelay;
        public string PreExpAnimationTrigger;
        public string PostExpAnimationTrigger;
        public float PreExpAnimationDelay;
        public float PostExpAnimationDelay;
        public string PreItemAnimationTrigger;
        public string PostItemAnimationTrigger;
        public float PreItemAnimationDelay;
        public float PostItemAnimationDelay;
        protected QuestParam mCurrentQuest;
        private GameObject mMasterAbilityPopup;
        protected QuestResultData mResultData;
        protected string mQuestName;
        public GameObject RetryButton;
        public Button StarKakuninButton;
        public SRPG_Button TeamUploadButton;
        protected List<UnitData> mUnits;
        [Description("スキップボタン")]
        public Button ResultSkipButton;
        [Description("経験値増加アニメーションスキップの倍速設定")]
        public float ResultSkipSpeedMul;
        private bool mResultSkipElement;
        private bool mExpAnimationEnd;
        protected bool mContinueStarAnimation;
        public bool UseUnitGetEffect;
        public bool NewEffectUse;
        public int[] AcquiredUnitExp;
        [Description("アリーナ：勝ち表示するゲームオブジェクト")]
        public GameObject GoArenaResultWin;
        [Description("アリーナ：負けを表示するゲームオブジェクト")]
        public GameObject GoArenaResultLose;
        public BattleResultMissionDetail MissionDetailSmall;
        public BattleResultMissionDetail MissionDetailLarge;
        private List<GameObject> mObjectiveStars;
        protected BattleResultMissionDetail mMissionDetail;
        protected List<int> mMultiTowerUnitsId;
        public GameObject[] MultiTowerPlayerObj;
        public RectTransform[] MultiTowerPlayerTransform;
        public Texture2D GoldTex;
        public Sprite GoldFrame;
        public Animator MainAnimator;

        public QuestResult()
        {
            this.DebugUnitIDs = new string[0];
            this.DebugObjectiveFlags = new bool[3];
            this.Star_TurnOnTrigger = "on";
            this.Star_KeepOffTrigger = "off";
            this.Star_ClearTrigger = "clear";
            this.Star_TriggerInterval = 1f;
            this.Treasure_TurnOnTrigger = "on";
            this.Treasure_TriggerInterval = 1f;
            this.TextConsumeApColor = Color.get_white();
            this.Unit_LevelUpTrigger = "levelup";
            this.ExpGainRate = 100f;
            this.ExpGainTimeMax = 2f;
            this.mUnitListItems = new List<GameObject>();
            this.mTreasureListItems = new List<GameObject>();
            this.mUnits = new List<UnitData>();
            this.ResultSkipSpeedMul = 10f;
            this.mContinueStarAnimation = 1;
            this.mObjectiveStars = new List<GameObject>();
            this.mMultiTowerUnitsId = new List<int>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <OnClickMultiTowerRetry>m__3D6(GameObject g)
        {
            FlowNode_Variable.Set("MultiPlayPasscode", "1");
            GlobalVars.InvtationSameUser = 1;
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "FINISH_RESULT");
            return;
        }

        [CompilerGenerated]
        private void <OnClickMultiTowerRetry>m__3D7(GameObject g)
        {
            FlowNode_Variable.Set("MultiPlayPasscode", "0");
            GlobalVars.InvtationSameUser = 0;
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "FINISH_RESULT");
            return;
        }

        public virtual void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 10)
            {
                goto Label_0017;
            }
            if (num == 100)
            {
                goto Label_0029;
            }
            goto Label_0057;
        Label_0017:
            base.StartCoroutine(this.PlayAnimationAsync());
            goto Label_0057;
        Label_0029:
            this.mResultSkipElement = 1;
            if ((this.ResultSkipButton != null) == null)
            {
                goto Label_0057;
            }
            this.ResultSkipButton.get_gameObject().SetActive(0);
        Label_0057:
            return;
        }

        [DebuggerHidden]
        public virtual IEnumerator AddExp()
        {
            <AddExp>c__Iterator130 iterator;
            iterator = new <AddExp>c__Iterator130();
            iterator.<>f__this = this;
            return iterator;
        }

        public virtual void AddExpPlayer()
        {
            Transform transform;
            int num;
            GameObject obj2;
            CampaignPartyExp exp;
            ConceptCardIcon icon;
            if ((this.UnitExpText != null) == null)
            {
                goto Label_001D;
            }
            this.UnitExpText.AddComponent<CampaignPartyExp>();
        Label_001D:
            transform = ((this.UnitList != null) == null) ? this.UnitListItem.get_transform().get_parent() : this.UnitList.get_transform();
            num = 0;
            goto Label_00E6;
        Label_0056:
            obj2 = Object.Instantiate<GameObject>(this.UnitListItem);
            obj2.get_transform().SetParent(transform, 0);
            exp = obj2.GetComponentInChildren<CampaignPartyExp>();
            if ((exp != null) == null)
            {
                goto Label_0090;
            }
            exp.Exp = this.AcquiredUnitExp[num];
        Label_0090:
            icon = obj2.GetComponent<ConceptCardIcon>();
            if ((icon != null) == null)
            {
                goto Label_00BD;
            }
            icon.Setup(this.mUnits[num].ConceptCard);
        Label_00BD:
            this.mUnitListItems.Add(obj2);
            DataSource.Bind<UnitData>(obj2, this.mUnits[num]);
            obj2.SetActive(1);
            num += 1;
        Label_00E6:
            if (num < this.mUnits.Count)
            {
                goto Label_0056;
            }
            return;
        }

        public void AddExpPlayerMultiTower()
        {
            MyPhoton photon;
            List<JSON_MyPhotonPlayerParam> list;
            int num;
            int num2;
            List<UnitData>[] listArray;
            int num3;
            int num4;
            int num5;
            Transform transform;
            int num6;
            GameObject obj2;
            CampaignPartyExp exp;
            if (this.MultiTowerPlayerObj == null)
            {
                goto Label_0016;
            }
            if (this.MultiTowerPlayerTransform != null)
            {
                goto Label_0017;
            }
        Label_0016:
            return;
        Label_0017:
            if ((this.UnitExpText != null) == null)
            {
                goto Label_0034;
            }
            this.UnitExpText.AddComponent<CampaignPartyExp>();
        Label_0034:
            list = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted();
            num = SceneBattle.Instance.MultiPlayerCount + 1;
            num2 = 0;
            goto Label_0087;
        Label_0055:
            if (num <= num2)
            {
                goto Label_0075;
            }
            DataSource.Bind<JSON_MyPhotonPlayerParam>(this.MultiTowerPlayerObj[num2], list[num2]);
            goto Label_0083;
        Label_0075:
            this.MultiTowerPlayerObj[num2].SetActive(0);
        Label_0083:
            num2 += 1;
        Label_0087:
            if (num2 < ((int) this.MultiTowerPlayerObj.Length))
            {
                goto Label_0055;
            }
            listArray = new List<UnitData>[num];
            num3 = 0;
            goto Label_00B5;
        Label_00A5:
            listArray[num3] = new List<UnitData>();
            num3 += 1;
        Label_00B5:
            if (num3 < ((int) listArray.Length))
            {
                goto Label_00A5;
            }
            num4 = 0;
            goto Label_00F2;
        Label_00C8:
            listArray[this.mMultiTowerUnitsId[num4] - 1].Add(this.mUnits[num4]);
            num4 += 1;
        Label_00F2:
            if (num4 < this.mUnits.Count)
            {
                goto Label_00C8;
            }
            num5 = 0;
            goto Label_01A6;
        Label_010C:
            transform = this.MultiTowerPlayerTransform[num5];
            num6 = 0;
            goto Label_018F;
        Label_011F:
            obj2 = Object.Instantiate<GameObject>(this.UnitListItem);
            obj2.get_transform().SetParent(transform, 0);
            exp = obj2.GetComponentInChildren<CampaignPartyExp>();
            if ((exp != null) == null)
            {
                goto Label_0161;
            }
            exp.Exp = this.AcquiredUnitExp[num6];
        Label_0161:
            this.mUnitListItems.Add(obj2);
            DataSource.Bind<UnitData>(obj2, listArray[num5][num6]);
            obj2.SetActive(1);
            num6 += 1;
        Label_018F:
            if (num6 < listArray[num5].Count)
            {
                goto Label_011F;
            }
            num5 += 1;
        Label_01A6:
            if (num5 < num)
            {
                goto Label_010C;
            }
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
            <ApplyQuestCampaignParams>c__AnonStorey38B storeyb;
            this.AcquiredUnitExp = new int[this.mUnits.Count];
            if (campaignIds == null)
            {
                goto Label_022C;
            }
            dataArray = MonoSingleton<GameManager>.GetInstanceDirect().FindQuestCampaigns(campaignIds);
            list = this.mUnits;
            numArray = new float[list.Count];
            num = 1f;
            num2 = 0;
            goto Label_005B;
        Label_004C:
            numArray[num2] = 1f;
            num2 += 1;
        Label_005B:
            if (num2 < ((int) numArray.Length))
            {
                goto Label_004C;
            }
            storeyb = new <ApplyQuestCampaignParams>c__AnonStorey38B();
            dataArray2 = dataArray;
            num3 = 0;
            goto Label_0181;
        Label_0077:
            storeyb.data = dataArray2[num3];
            if (storeyb.data.type != 1)
            {
                goto Label_00F0;
            }
            if (string.IsNullOrEmpty(storeyb.data.unit) == null)
            {
                goto Label_00BE;
            }
            num = storeyb.data.GetRate();
            goto Label_00EB;
        Label_00BE:
            num4 = list.FindIndex(new Predicate<UnitData>(storeyb.<>m__3D5));
            if (num4 < 0)
            {
                goto Label_017B;
            }
            numArray[num4] = storeyb.data.GetRate();
        Label_00EB:
            goto Label_017B;
        Label_00F0:
            if (storeyb.data.type != null)
            {
                goto Label_0147;
            }
            num5 = this.mResultData.Record.playerexp;
            this.mResultData.Record.playerexp = Mathf.RoundToInt(((float) num5) * storeyb.data.GetRate());
            goto Label_017B;
        Label_0147:
            if (storeyb.data.type != 4)
            {
                goto Label_017B;
            }
            if ((this.TextConsumeAp != null) == null)
            {
                goto Label_017B;
            }
            this.TextConsumeAp.set_color(this.TextConsumeApColor);
        Label_017B:
            num3 += 1;
        Label_0181:
            if (num3 < ((int) dataArray2.Length))
            {
                goto Label_0077;
            }
            num6 = this.mResultData.Record.unitexp;
            num7 = 0;
            goto Label_021D;
        Label_01AB:
            num8 = 1f;
            if (num == 1f)
            {
                goto Label_01DA;
            }
            if (numArray[num7] == 1f)
            {
                goto Label_01DA;
            }
            num8 = num + numArray[num7];
            goto Label_0203;
        Label_01DA:
            if (num == 1f)
            {
                goto Label_01EF;
            }
            num8 = num;
            goto Label_0203;
        Label_01EF:
            if (numArray[num7] == 1f)
            {
                goto Label_0203;
            }
            num8 = numArray[num7];
        Label_0203:
            this.AcquiredUnitExp[num7] = Mathf.RoundToInt(((float) num6) * num8);
            num7 += 1;
        Label_021D:
            if (num7 < ((int) numArray.Length))
            {
                goto Label_01AB;
            }
            goto Label_0267;
        Label_022C:
            num9 = 0;
            goto Label_0258;
        Label_0234:
            this.AcquiredUnitExp[num9] = this.mResultData.Record.unitexp;
            num9 += 1;
        Label_0258:
            if (num9 < ((int) this.AcquiredUnitExp.Length))
            {
                goto Label_0234;
            }
        Label_0267:
            return;
        }

        private unsafe void CreateArtifactObjects(Transform parent)
        {
            List<ArtifactParam> list;
            OrderedDictionary dictionary;
            ArtifactParam param;
            List<ArtifactParam>.Enumerator enumerator;
            int num;
            IDictionaryEnumerator enumerator2;
            GameObject obj2;
            GameObject obj3;
            ArtifactParam param2;
            int num2;
            GameObject obj4;
            RectTransform transform;
            <CreateArtifactObjects>c__AnonStorey38A storeya;
            IDisposable disposable;
            list = MonoSingleton<GameManager>.Instance.MasterParam.Artifacts;
            dictionary = new OrderedDictionary();
            enumerator = this.mResultData.Record.artifacts.GetEnumerator();
        Label_002C:
            try
            {
                goto Label_0089;
            Label_0031:
                param = &enumerator.Current;
                if (dictionary.Contains(param.iname) == null)
                {
                    goto Label_0077;
                }
                num = (int) dictionary[param.iname];
                dictionary[param.iname] = (int) (num + 1);
                goto Label_0089;
            Label_0077:
                dictionary.Add(param.iname, (int) 1);
            Label_0089:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0031;
                }
                goto Label_00A6;
            }
            finally
            {
            Label_009A:
                ((List<ArtifactParam>.Enumerator) enumerator).Dispose();
            }
        Label_00A6:
            storeya = new <CreateArtifactObjects>c__AnonStorey38A();
            enumerator2 = dictionary.GetEnumerator();
        Label_00B5:
            try
            {
                goto Label_01B9;
            Label_00BA:
                storeya.artiAndNum = (DictionaryEntry) enumerator2.Current;
                obj3 = Object.Instantiate<GameObject>(this.TreasureListArtifact);
                obj3.get_transform().SetParent(parent, 0);
                param2 = Enumerable.FirstOrDefault<ArtifactParam>(list, new Func<ArtifactParam, bool>(storeya.<>m__3D4));
                this.mTreasureListItems.Add(obj3);
                DataSource.Bind<ArtifactParam>(obj3, param2);
                DataSource.Bind<int>(obj3, (int) &storeya.artiAndNum.Value);
                obj3.SetActive(1);
                GameParameter.UpdateAll(obj3);
                if ((this.Prefab_NewItemBadge != null) == null)
                {
                    goto Label_01B9;
                }
                if (MonoSingleton<GameManager>.Instance.Player.GetArtifactNumByRarity(param2.iname, param2.rareini) > 0)
                {
                    goto Label_01B9;
                }
                transform = Object.Instantiate<GameObject>(this.Prefab_NewItemBadge).get_transform() as RectTransform;
                transform.get_gameObject().SetActive(1);
                transform.set_anchoredPosition(Vector2.get_zero());
                transform.SetParent(obj3.get_transform(), 0);
            Label_01B9:
                if (enumerator2.MoveNext() != null)
                {
                    goto Label_00BA;
                }
                goto Label_01E0;
            }
            finally
            {
            Label_01CA:
                disposable = enumerator2 as IDisposable;
                if (disposable != null)
                {
                    goto Label_01D8;
                }
            Label_01D8:
                disposable.Dispose();
            }
        Label_01E0:
            return;
        }

        private unsafe void CreateGoldObjects(Transform parent)
        {
            GameObject obj2;
            GameObject obj3;
            Transform transform;
            Image_Transparent transparent;
            Transform transform2;
            RawImage_Transparent transparent2;
            Transform transform3;
            BitmapText text;
            if (this.mCurrentQuest == null)
            {
                goto Label_001C;
            }
            if (this.mCurrentQuest.IsVersus != null)
            {
                goto Label_001C;
            }
            return;
        Label_001C:
            obj2 = null;
            if (this.mResultData.Record.gold <= 0)
            {
                goto Label_0158;
            }
            obj3 = Object.Instantiate<GameObject>(this.TreasureListItem);
            obj3.get_transform().SetParent(parent, 0);
            this.mTreasureListItems.Add(obj3);
            obj3.SetActive(1);
            transform = obj3.get_transform().FindChild("BODY/frame");
            if ((transform != null) == null)
            {
                goto Label_00B4;
            }
            transparent = transform.GetComponent<Image_Transparent>();
            if ((transparent != null) == null)
            {
                goto Label_00B4;
            }
            if ((this.GoldFrame != null) == null)
            {
                goto Label_00B4;
            }
            transparent.set_sprite(this.GoldFrame);
        Label_00B4:
            transform2 = obj3.get_transform().FindChild("BODY/itemicon");
            if ((transform2 != null) == null)
            {
                goto Label_0107;
            }
            transparent2 = transform2.GetComponent<RawImage_Transparent>();
            if ((transparent2 != null) == null)
            {
                goto Label_0107;
            }
            if ((this.GoldTex != null) == null)
            {
                goto Label_0107;
            }
            transparent2.set_texture(this.GoldTex);
        Label_0107:
            transform3 = obj3.get_transform().FindChild("BODY/amount/Text_amount");
            if ((transform3 != null) == null)
            {
                goto Label_0158;
            }
            text = transform3.GetComponent<BitmapText>();
            if ((text != null) == null)
            {
                goto Label_0158;
            }
            text.text = &this.mResultData.Record.gold.ToString();
        Label_0158:
            return;
        }

        public virtual void CreateItemObject(List<DropItemData> items, Transform parent)
        {
            GameObject obj2;
            int num;
            GameObject obj3;
            ItemIcon icon;
            ItemIcon icon2;
            GameObject obj4;
            RectTransform transform;
            obj2 = null;
            num = 0;
            goto Label_01B8;
        Label_0009:
            obj3 = null;
            if (items[num].IsConceptCard == null)
            {
                goto Label_008D;
            }
            obj3 = Object.Instantiate<GameObject>(this.TreasureListConceptCard);
            obj3.get_transform().SetParent(parent, 0);
            this.mTreasureListItems.Add(obj3);
            DataSource.Bind<DropItemData>(obj3, items[num]);
            if (items[num].mIsSecret == null)
            {
                goto Label_007B;
            }
            icon = obj3.GetComponent<DropItemIcon>();
            if ((icon != null) == null)
            {
                goto Label_007B;
            }
            icon.IsSecret = 1;
        Label_007B:
            obj3.SetActive(1);
            GameParameter.UpdateAll(obj3);
            goto Label_0150;
        Label_008D:
            if (items[num].IsItem == null)
            {
                goto Label_0130;
            }
            obj2 = (items[num].ItemType != 0x10) ? this.TreasureListItem : this.TreasureListUnit;
            obj3 = Object.Instantiate<GameObject>(obj2);
            obj3.get_transform().SetParent(parent, 0);
            this.mTreasureListItems.Add(obj3);
            DataSource.Bind<ItemData>(obj3, items[num]);
            if (items[num].mIsSecret == null)
            {
                goto Label_011E;
            }
            icon2 = obj3.GetComponent<ItemIcon>();
            if ((icon2 != null) == null)
            {
                goto Label_011E;
            }
            icon2.IsSecret = 1;
        Label_011E:
            obj3.SetActive(1);
            GameParameter.UpdateAll(obj3);
            goto Label_0150;
        Label_0130:
            DebugUtility.LogError(string.Format("[コードの追加が必要] DropItemData.mBattleRewardType(={0})は不明な列挙です", (EBattleRewardType) items[num].BattleRewardType));
        Label_0150:
            if ((this.Prefab_NewItemBadge != null) == null)
            {
                goto Label_01B4;
            }
            if (items[num].IsNew == null)
            {
                goto Label_01B4;
            }
            transform = Object.Instantiate<GameObject>(this.Prefab_NewItemBadge).get_transform() as RectTransform;
            transform.get_gameObject().SetActive(1);
            transform.set_anchoredPosition(Vector2.get_zero());
            transform.SetParent(obj3.get_transform(), 0);
        Label_01B4:
            num += 1;
        Label_01B8:
            if (num < items.Count)
            {
                goto Label_0009;
            }
            return;
        }

        [DebuggerHidden]
        protected IEnumerator ObjectiveAnimation()
        {
            <ObjectiveAnimation>c__Iterator135 iterator;
            iterator = new <ObjectiveAnimation>c__Iterator135();
            iterator.<>f__this = this;
            return iterator;
        }

        public void OnClickMultiTowerNextRetry()
        {
            GlobalVars.SelectedMultiTowerFloor += 1;
            this.OnClickMultiTowerRetry();
            return;
        }

        public void OnClickMultiTowerRetry()
        {
            string str;
            GlobalVars.CreateAutoMultiTower = 1;
            str = LocalizedText.Get("sys.MULTI_TOWER_SAMEUSER");
            UIUtility.ConfirmBoxTitle(null, str, new UIUtility.DialogResultEvent(this.<OnClickMultiTowerRetry>m__3D6), new UIUtility.DialogResultEvent(this.<OnClickMultiTowerRetry>m__3D7), null, 0, -1, null, null);
            return;
        }

        private void OnDestroy()
        {
            GameUtility.DestroyGameObject(this.mMasterAbilityPopup);
            this.mMasterAbilityPopup = null;
            return;
        }

        public void OnPartyUploadFinished()
        {
            if (GlobalVars.PartyUploadFinished == null)
            {
                goto Label_0027;
            }
            if ((this.TeamUploadButton != null) == null)
            {
                goto Label_0027;
            }
            this.TeamUploadButton.set_interactable(0);
        Label_0027:
            return;
        }

        public void OnStarKakuninButtonClick()
        {
            this.mContinueStarAnimation = 1;
            return;
        }

        [DebuggerHidden]
        public virtual IEnumerator PlayAnimationAsync()
        {
            <PlayAnimationAsync>c__Iterator12F iteratorf;
            iteratorf = new <PlayAnimationAsync>c__Iterator12F();
            iteratorf.<>f__this = this;
            return iteratorf;
        }

        [DebuggerHidden]
        protected IEnumerator RecvExpAnimation()
        {
            <RecvExpAnimation>c__Iterator131 iterator;
            iterator = new <RecvExpAnimation>c__Iterator131();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        protected IEnumerator RecvTrustAnimation()
        {
            int num;
            int num2;
            int num3;
            float num4;
            <RecvTrustAnimation>c__Iterator132 iterator;
            iterator = new <RecvTrustAnimation>c__Iterator132();
            iterator.<>f__this = this;
            return iterator;
        }

        private void RefreshQuestMissionReward()
        {
            BattleResultMissionDetail detail;
            if (this.mCurrentQuest != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mUseLarge = (this.mCurrentQuest.bonusObjective == null) ? 0 : (((int) this.mCurrentQuest.bonusObjective.Length) > 3);
            if ((this.MissionDetailLarge != null) == null)
            {
                goto Label_005C;
            }
            this.MissionDetailLarge.get_gameObject().SetActive(this.mUseLarge);
        Label_005C:
            if ((this.MissionDetailSmall != null) == null)
            {
                goto Label_0086;
            }
            this.MissionDetailSmall.get_gameObject().SetActive(this.mUseLarge == 0);
        Label_0086:
            detail = (this.mUseLarge == null) ? this.MissionDetailSmall : this.MissionDetailLarge;
            if ((detail != null) == null)
            {
                goto Label_00DD;
            }
            this.mMissionDetail = detail.GetComponent<BattleResultMissionDetail>();
            if ((this.mMissionDetail != null) == null)
            {
                goto Label_00DD;
            }
            this.mObjectiveStars = this.mMissionDetail.GetObjectiveStars();
        Label_00DD:
            return;
        }

        protected void SetExpAnimationEnd()
        {
            this.mExpAnimationEnd = 1;
            return;
        }

        private unsafe void Start()
        {
            PlayerData data;
            SceneBattle battle;
            long num;
            long num2;
            bool flag;
            bool flag2;
            bool flag3;
            int num3;
            Unit unit;
            UnitData data2;
            Transform transform;
            List<DropItemData> list;
            int num4;
            bool flag4;
            int num5;
            DropItemData data3;
            ItemData data4;
            List<UnitData> list2;
            GameManager manager;
            DropItemData data5;
            DropItemData data6;
            int num6;
            TimeSpan span;
            DateTime time;
            TimeSpan span2;
            <Start>c__AnonStorey388 storey;
            <Start>c__AnonStorey389 storey2;
            data = MonoSingleton<GameManager>.Instance.Player;
            GlobalVars.PartyUploadFinished = 0;
            if ((this.UnitListItem != null) == null)
            {
                goto Label_002E;
            }
            this.UnitListItem.SetActive(0);
        Label_002E:
            if ((this.TreasureListItem != null) == null)
            {
                goto Label_004B;
            }
            this.TreasureListItem.SetActive(0);
        Label_004B:
            if ((this.TreasureListUnit != null) == null)
            {
                goto Label_0068;
            }
            this.TreasureListUnit.SetActive(0);
        Label_0068:
            if ((this.TreasureListArtifact != null) == null)
            {
                goto Label_0085;
            }
            this.TreasureListArtifact.SetActive(0);
        Label_0085:
            if ((this.TreasureListConceptCard != null) == null)
            {
                goto Label_00A2;
            }
            this.TreasureListConceptCard.SetActive(0);
        Label_00A2:
            if (((this.Prefab_NewItemBadge != null) == null) || (this.Prefab_NewItemBadge.get_gameObject().get_activeInHierarchy() == null))
            {
                goto Label_00D4;
            }
            this.Prefab_NewItemBadge.SetActive(0);
        Label_00D4:
            battle = SceneBattle.Instance;
            GameUtility.DestroyGameObjects(this.mUnitListItems);
            GameUtility.DestroyGameObjects(this.mTreasureListItems);
            if (((battle != null) == null) || (battle.ResultData == null))
            {
                goto Label_0410;
            }
            this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(battle.Battle.QuestID);
            DataSource.Bind<QuestParam>(base.get_gameObject(), this.mCurrentQuest);
            if ((this.RetryButton != null) == null)
            {
                goto Label_01B5;
            }
            &span = new TimeSpan(&TimeManager.ServerTime.Ticks);
            num = (long) &span.Days;
            &span2 = new TimeSpan(&data.LoginDate.Ticks);
            num2 = (long) &span2.Days;
            flag = ((num > num2) || (this.mCurrentQuest.type == 3)) ? 0 : (this.mCurrentQuest.IsCharacterQuest() == 0);
            this.RetryButton.SetActive(flag);
        Label_01B5:
            if ((this.mCurrentQuest.type != 3) || ((this.TeamUploadButton != null) == null))
            {
                goto Label_01E3;
            }
            this.TeamUploadButton.set_interactable(0);
        Label_01E3:
            this.mResultData = battle.ResultData;
            this.mQuestName = this.mCurrentQuest.iname;
            if (battle.IsPlayingArenaQuest == null)
            {
                goto Label_0322;
            }
            this.mResultData.Record.playerexp = GlobalVars.ResultArenaBattleResponse.got_pexp;
            this.mResultData.Record.gold = GlobalVars.ResultArenaBattleResponse.got_gold;
            this.mResultData.Record.unitexp = GlobalVars.ResultArenaBattleResponse.got_uexp;
            if (this.GoArenaResultWin == null)
            {
                goto Label_0296;
            }
            this.GoArenaResultWin.SetActive(this.mResultData.Record.result == 1);
        Label_0296:
            if (this.GoArenaResultLose == null)
            {
                goto Label_02C7;
            }
            this.GoArenaResultLose.SetActive((this.mResultData.Record.result == 1) == 0);
        Label_02C7:
            if (battle.IsArenaRankupInfo() == null)
            {
                goto Label_02FA;
            }
            MonoSingleton<GameManager>.Instance.Player.UpdateArenaRankTrophyStates(GlobalVars.ResultArenaBattleResponse.new_rank, GlobalVars.ResultArenaBattleResponse.new_rank);
            goto Label_0322;
        Label_02FA:
            MonoSingleton<GameManager>.Instance.Player.UpdateArenaRankTrophyStates(GlobalVars.ResultArenaBattleResponse.new_rank, MonoSingleton<GameManager>.Instance.Player.ArenaRankBest);
        Label_0322:
            flag2 = battle.Battle.IsMultiTower;
            flag3 = battle.Battle.IsMultiPlay;
            num3 = 0;
            goto Label_03E6;
        Label_0344:
            unit = battle.Battle.Units[num3];
            if (((flag2 != null) || (flag3 == null)) || (unit.OwnerPlayerIndex == battle.Battle.MyPlayerIndex))
            {
                goto Label_0382;
            }
            goto Label_03E0;
        Label_0382:
            if ((data.FindUnitDataByUniqueID(unit.UnitData.UniqueID) == null) && ((flag2 == null) || (unit.Side != null)))
            {
                goto Label_03E0;
            }
            data2 = new UnitData();
            data2.Setup(unit.UnitData);
            this.mUnits.Add(data2);
            this.mMultiTowerUnitsId.Add(unit.OwnerPlayerIndex);
        Label_03E0:
            num3 += 1;
        Label_03E6:
            if (num3 < battle.Battle.Units.Count)
            {
                goto Label_0344;
            }
            if (battle.IsArenaRankupInfo() == null)
            {
                goto Label_0410;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 40);
        Label_0410:
            DataSource.Bind<BattleCore.Record>(base.get_gameObject(), this.mResultData.Record);
            if (this.mResultData == null)
            {
                goto Label_09CC;
            }
            if ((this.TreasureListItem != null) == null)
            {
                goto Label_089E;
            }
            transform = ((this.TreasureList != null) == null) ? this.TreasureListItem.get_transform().get_parent() : this.TreasureList.get_transform();
            list = new List<DropItemData>();
            num4 = 0;
            goto Label_0762;
        Label_0484:
            flag4 = 0;
            num5 = 0;
            goto Label_0580;
        Label_048F:
            if (list[num5].mIsSecret == this.mResultData.Record.items[num4].mIsSecret)
            {
                goto Label_04C3;
            }
            goto Label_057A;
        Label_04C3:
            if (list[num5].IsItem == null)
            {
                goto Label_0521;
            }
            if (list[num5].itemParam != this.mResultData.Record.items[num4].itemParam)
            {
                goto Label_057A;
            }
            list[num5].Gain(1);
            flag4 = 1;
            goto Label_058E;
            goto Label_057A;
        Label_0521:
            if ((list[num5].IsConceptCard == null) || (list[num5].conceptCardParam != this.mResultData.Record.items[num4].conceptCardParam))
            {
                goto Label_057A;
            }
            list[num5].Gain(1);
            flag4 = 1;
            goto Label_058E;
        Label_057A:
            num5 += 1;
        Label_0580:
            if (num5 < list.Count)
            {
                goto Label_048F;
            }
        Label_058E:
            if (flag4 == null)
            {
                goto Label_059A;
            }
            goto Label_075C;
        Label_059A:
            data3 = new DropItemData();
            if (this.mResultData.Record.items[num4].IsItem == null)
            {
                goto Label_06E3;
            }
            data3.SetupDropItemData(2, 0L, this.mResultData.Record.items[num4].itemParam.iname, 1);
            if (this.mResultData.Record.items[num4].itemParam.type == 0x10)
            {
                goto Label_0686;
            }
            data4 = data.FindItemDataByItemParam(this.mResultData.Record.items[num4].itemParam);
            data3.IsNew = (data.ItemEntryExists(this.mResultData.Record.items[num4].itemParam.iname) == null) ? 1 : ((data4 == null) ? 1 : data4.IsNew);
            goto Label_06DE;
        Label_0686:
            storey = new <Start>c__AnonStorey388();
            storey.iid = this.mResultData.Record.items[num4].itemParam.iname;
            if (data.Units.Find(new Predicate<UnitData>(storey.<>m__3D2)) != null)
            {
                goto Label_0730;
            }
            data3.IsNew = 1;
        Label_06DE:
            goto Label_0730;
        Label_06E3:
            if (this.mResultData.Record.items[num4].IsConceptCard == null)
            {
                goto Label_0730;
            }
            data3.SetupDropItemData(3, 0L, this.mResultData.Record.items[num4].conceptCardParam.iname, 1);
        Label_0730:
            data3.mIsSecret = this.mResultData.Record.items[num4].mIsSecret;
            list.Add(data3);
        Label_075C:
            num4 += 1;
        Label_0762:
            if (num4 < this.mResultData.Record.items.Count)
            {
                goto Label_0484;
            }
            if (this.mCurrentQuest == null)
            {
                goto Label_0884;
            }
            if (this.mCurrentQuest.IsVersus == null)
            {
                goto Label_0884;
            }
            storey2 = new <Start>c__AnonStorey389();
            manager = MonoSingleton<GameManager>.Instance;
            storey2.coinParam = manager.GetVersusCoinParam(this.mCurrentQuest.iname);
            if (storey2.coinParam == null)
            {
                goto Label_0884;
            }
            data5 = list.Find(new Predicate<DropItemData>(storey2.<>m__3D3));
            if (data5 == null)
            {
                goto Label_080A;
            }
            data5.Gain(this.mResultData.Record.pvpcoin);
            goto Label_0884;
        Label_080A:
            if (this.mResultData.Record.pvpcoin <= 0)
            {
                goto Label_0884;
            }
            data6 = new DropItemData();
            data6.Setup(0L, storey2.coinParam.coin_iname, this.mResultData.Record.pvpcoin);
            data6.mIsSecret = 0;
            data6.IsNew = data.ItemEntryExists(storey2.coinParam.coin_iname) == 0;
            list.Add(data6);
        Label_0884:
            this.CreateItemObject(list, transform);
            this.CreateArtifactObjects(transform);
            this.CreateGoldObjects(transform);
        Label_089E:
            this.ApplyQuestCampaignParams(battle.Battle.QuestCampaignIds);
            if ((this.UnitListItem != null) == null)
            {
                goto Label_08E1;
            }
            if (battle.Battle.IsMultiTower == null)
            {
                goto Label_08DB;
            }
            this.AddExpPlayerMultiTower();
            goto Label_08E1;
        Label_08DB:
            this.AddExpPlayer();
        Label_08E1:
            GlobalVars.PlayerExpOld.Set(this.mResultData.StartExp);
            GlobalVars.PlayerExpNew.Set(this.mResultData.StartExp + this.mResultData.Record.playerexp);
            GlobalVars.PlayerLevelChanged.Set((data.Lv == PlayerData.CalcLevelFromExp(this.mResultData.StartExp)) == 0);
            this.RefreshQuestMissionReward();
            if (string.IsNullOrEmpty(this.Star_ClearTrigger) != null)
            {
                goto Label_09B1;
            }
            num6 = 0;
            goto Label_099F;
        Label_0964:
            if ((this.mCurrentQuest.clear_missions & (1 << (num6 & 0x1f))) != null)
            {
                goto Label_0981;
            }
            goto Label_0999;
        Label_0981:
            GameUtility.SetAnimatorTrigger(this.mObjectiveStars[num6], this.Star_ClearTrigger);
        Label_0999:
            num6 += 1;
        Label_099F:
            if (num6 < this.mObjectiveStars.Count)
            {
                goto Label_0964;
            }
        Label_09B1:
            data.OnGoldChange(this.mResultData.Record.gold);
        Label_09CC:
            if ((this.StarKakuninButton != null) == null)
            {
                goto Label_0A00;
            }
            this.StarKakuninButton.get_onClick().AddListener(new UnityAction(this, this.OnStarKakuninButtonClick));
            this.mContinueStarAnimation = 0;
        Label_0A00:
            GlobalVars.CreateAutoMultiTower = 0;
            GlobalVars.InvtationSameUser = 0;
            if ((this.ResultSkipButton != null) == null)
            {
                goto Label_0A2E;
            }
            this.ResultSkipButton.get_gameObject().SetActive(0);
        Label_0A2E:
            return;
        }

        [DebuggerHidden]
        public virtual IEnumerator StartTreasureAnimation()
        {
            <StartTreasureAnimation>c__Iterator133 iterator;
            iterator = new <StartTreasureAnimation>c__Iterator133();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        protected virtual IEnumerator TreasureAnimation(List<GameObject> ListItems)
        {
            <TreasureAnimation>c__Iterator134 iterator;
            iterator = new <TreasureAnimation>c__Iterator134();
            iterator.ListItems = ListItems;
            iterator.<$>ListItems = ListItems;
            iterator.<>f__this = this;
            return iterator;
        }

        public void TriggerAnimation(string trigger)
        {
            if (string.IsNullOrEmpty(trigger) != null)
            {
                goto Label_0028;
            }
            if ((this.MainAnimator != null) == null)
            {
                goto Label_0028;
            }
            this.MainAnimator.SetTrigger(trigger);
        Label_0028:
            return;
        }

        [CompilerGenerated]
        private sealed class <AddExp>c__Iterator130 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal QuestResult <>f__this;

            public <AddExp>c__Iterator130()
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
                        goto Label_002D;

                    case 1:
                        goto Label_007A;

                    case 2:
                        goto Label_00A2;

                    case 3:
                        goto Label_00CA;

                    case 4:
                        goto Label_010C;
                }
                goto Label_0129;
            Label_002D:
                this.<>f__this.TriggerAnimation(this.<>f__this.PreExpAnimationTrigger);
                if (this.<>f__this.PreExpAnimationDelay <= 0f)
                {
                    goto Label_007A;
                }
                this.$current = new WaitForSeconds(this.<>f__this.PreExpAnimationDelay);
                this.$PC = 1;
                goto Label_012B;
            Label_007A:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.RecvExpAnimation());
                this.$PC = 2;
                goto Label_012B;
            Label_00A2:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.RecvTrustAnimation());
                this.$PC = 3;
                goto Label_012B;
            Label_00CA:
                this.<>f__this.SetExpAnimationEnd();
                if (this.<>f__this.PostExpAnimationDelay <= 0f)
                {
                    goto Label_010C;
                }
                this.$current = new WaitForSeconds(this.<>f__this.PostExpAnimationDelay);
                this.$PC = 4;
                goto Label_012B;
            Label_010C:
                this.<>f__this.TriggerAnimation(this.<>f__this.PostExpAnimationTrigger);
                this.$PC = -1;
            Label_0129:
                return 0;
            Label_012B:
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
        private sealed class <ApplyQuestCampaignParams>c__AnonStorey38B
        {
            internal QuestCampaignData data;

            public <ApplyQuestCampaignParams>c__AnonStorey38B()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3D5(UnitData value)
            {
                return (value.UnitParam.iname == this.data.unit);
            }
        }

        [CompilerGenerated]
        private sealed class <CreateArtifactObjects>c__AnonStorey38A
        {
            internal DictionaryEntry artiAndNum;

            public <CreateArtifactObjects>c__AnonStorey38A()
            {
                base..ctor();
                return;
            }

            internal unsafe bool <>m__3D4(ArtifactParam arti)
            {
                return (arti.iname == ((string) &this.artiAndNum.Key));
            }
        }

        [CompilerGenerated]
        private sealed class <ObjectiveAnimation>c__Iterator135 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <currentStar>__0;
            internal int <lastStar>__1;
            internal QuestParam <quest>__2;
            internal bool <star>__3;
            internal bool <cleared>__4;
            internal float <waitTime>__5;
            internal int $PC;
            internal object $current;
            internal QuestResult <>f__this;

            public <ObjectiveAnimation>c__Iterator135()
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
                        goto Label_017F;

                    case 2:
                        goto Label_01FB;

                    case 3:
                        goto Label_027B;
                }
                goto Label_02CC;
            Label_0029:
                this.<currentStar>__0 = 0;
                this.<lastStar>__1 = 0;
                this.<quest>__2 = MonoSingleton<GameManager>.Instance.FindQuest(this.<>f__this.mQuestName);
                goto Label_027B;
            Label_0057:
                goto Label_0249;
            Label_005C:
                this.<star>__3 = ((this.<>f__this.mResultData.Record.bonusFlags & (1 << (this.<currentStar>__0 & 0x1f))) == 0) == 0;
                if (this.<>f__this.Star_SkipDarkStar == null)
                {
                    goto Label_00F3;
                }
                this.<cleared>__4 = (this.<quest>__2 == null) ? 0 : (((this.<quest>__2.clear_missions & (1 << (this.<currentStar>__0 & 0x1f))) == 0) == 0);
                if (this.<star>__3 == null)
                {
                    goto Label_00E0;
                }
                if (this.<cleared>__4 == null)
                {
                    goto Label_00F3;
                }
            Label_00E0:
                this.<currentStar>__0 += 1;
                goto Label_0249;
            Label_00F3:
                if (this.<star>__3 == null)
                {
                    goto Label_0200;
                }
                if (string.IsNullOrEmpty(this.<>f__this.Star_TurnOnTrigger) != null)
                {
                    goto Label_023B;
                }
                if (this.<>f__this.mUseLarge == null)
                {
                    goto Label_017F;
                }
                this.<waitTime>__5 = this.<>f__this.mMissionDetail.MoveTo(this.<currentStar>__0);
                if ((this.<currentStar>__0 - this.<lastStar>__1) <= 3)
                {
                    goto Label_017F;
                }
                if (this.<waitTime>__5 <= 0f)
                {
                    goto Label_017F;
                }
                this.$current = new WaitForSeconds(this.<waitTime>__5);
                this.$PC = 1;
                goto Label_02CE;
            Label_017F:
                if (SceneBattle.Instance.Battle.IsMultiPlay != null)
                {
                    goto Label_01A7;
                }
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0508", 0f);
            Label_01A7:
                GameUtility.SetAnimatorTrigger(this.<>f__this.mObjectiveStars[this.<currentStar>__0], this.<>f__this.Star_TurnOnTrigger);
                this.<lastStar>__1 = this.<currentStar>__0;
                this.$current = new WaitForSeconds(this.<>f__this.Star_TriggerInterval);
                this.$PC = 2;
                goto Label_02CE;
            Label_01FB:
                goto Label_023B;
            Label_0200:
                if (string.IsNullOrEmpty(this.<>f__this.Star_KeepOffTrigger) != null)
                {
                    goto Label_023B;
                }
                GameUtility.SetAnimatorTrigger(this.<>f__this.mObjectiveStars[this.<currentStar>__0], this.<>f__this.Star_KeepOffTrigger);
            Label_023B:
                this.<currentStar>__0 += 1;
            Label_0249:
                if (this.<currentStar>__0 < this.<>f__this.mObjectiveStars.Count)
                {
                    goto Label_005C;
                }
                this.$current = new WaitForEndOfFrame();
                this.$PC = 3;
                goto Label_02CE;
            Label_027B:
                if (this.<currentStar>__0 < this.<>f__this.mObjectiveStars.Count)
                {
                    goto Label_0249;
                }
                MonoSingleton<GameManager>.Instance.Player.SetQuestMissionFlags(this.<>f__this.mQuestName, this.<>f__this.mResultData.Record.bonusFlags);
                this.$PC = -1;
            Label_02CC:
                return 0;
            Label_02CE:
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
        private sealed class <PlayAnimationAsync>c__Iterator12F : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int[] <oldMemberLv>__0;
            internal int <i>__1;
            internal PlayerData <pd>__2;
            internal int <rest>__3;
            internal UnitGetWindowController <controller>__4;
            internal GameManager <gm>__5;
            internal List<QuestClearUnlockUnitDataParam> <unlockParams>__6;
            internal int <i>__7;
            internal UnitData <unit>__8;
            internal string <unlock>__9;
            internal QuestClearUnlockUnitDataParam[] <newUnlocks>__10;
            internal QuestClearUnlockUnitDataParam <master>__11;
            internal GameObject <clearUnlockPopup>__12;
            internal bool <showPopup>__13;
            internal int <index>__14;
            internal UnitData <unitData>__15;
            internal UnitData.CharacterQuestParam <beforeUnlock>__16;
            internal int <i>__17;
            internal AbilityParam <masterAbility>__18;
            internal string <masterAbilityID>__19;
            internal Exception <e>__20;
            internal GameObject <clearUnlockPopup>__21;
            internal QuestClearUnlockUnitDataParam[] <masterAbil>__22;
            internal int $PC;
            internal object $current;
            internal QuestResult <>f__this;

            public <PlayAnimationAsync>c__Iterator12F()
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
                Exception exception;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0049;

                    case 1:
                        goto Label_0138;

                    case 2:
                        goto Label_0160;

                    case 3:
                        goto Label_0178;

                    case 4:
                        goto Label_01BF;

                    case 5:
                        goto Label_028E;

                    case 6:
                        goto Label_0309;

                    case 7:
                        goto Label_0340;

                    case 8:
                        goto Label_05E0;

                    case 9:
                        goto Label_0638;

                    case 10:
                        goto Label_099C;

                    case 11:
                        goto Label_0A71;
                }
                goto Label_0ABF;
            Label_0049:
                this.<oldMemberLv>__0 = new int[this.<>f__this.mUnits.Count];
                this.<i>__1 = 0;
                goto Label_00A6;
            Label_0070:
                this.<oldMemberLv>__0[this.<i>__1] = this.<>f__this.mUnits[this.<i>__1].Lv;
                this.<i>__1 += 1;
            Label_00A6:
                if (this.<i>__1 < this.<>f__this.mUnits.Count)
                {
                    goto Label_0070;
                }
                if (this.<>f__this.mResultData.StartBonusFlags == this.<>f__this.mResultData.Record.bonusFlags)
                {
                    goto Label_01D5;
                }
                this.<>f__this.TriggerAnimation(this.<>f__this.PreStarAnimationTrigger);
                if (this.<>f__this.PreStarAnimationDelay <= 0f)
                {
                    goto Label_0138;
                }
                this.$current = new WaitForSeconds(this.<>f__this.PreStarAnimationDelay);
                this.$PC = 1;
                goto Label_0AC1;
            Label_0138:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.ObjectiveAnimation());
                this.$PC = 2;
                goto Label_0AC1;
            Label_0160:
                goto Label_0178;
            Label_0165:
                this.$current = null;
                this.$PC = 3;
                goto Label_0AC1;
            Label_0178:
                if (this.<>f__this.mContinueStarAnimation == null)
                {
                    goto Label_0165;
                }
                if (this.<>f__this.PostStarAnimationDelay <= 0f)
                {
                    goto Label_01BF;
                }
                this.$current = new WaitForSeconds(this.<>f__this.PostStarAnimationDelay);
                this.$PC = 4;
                goto Label_0AC1;
            Label_01BF:
                this.<>f__this.TriggerAnimation(this.<>f__this.PostStarAnimationTrigger);
            Label_01D5:
                if (this.<>f__this.mCurrentQuest.type != 15)
                {
                    goto Label_01FE;
                }
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 0x1f);
                goto Label_0ABF;
            Label_01FE:
                if ((this.<>f__this.ResultSkipButton != null) == null)
                {
                    goto Label_022A;
                }
                this.<>f__this.ResultSkipButton.get_gameObject().SetActive(1);
            Label_022A:
                this.<>f__this.StartCoroutine(this.<>f__this.AddExp());
                this.<>f__this.TriggerAnimation(this.<>f__this.PreItemAnimationTrigger);
                if (this.<>f__this.PreItemAnimationDelay <= 0f)
                {
                    goto Label_028E;
                }
                this.$current = new WaitForSeconds(this.<>f__this.PreItemAnimationDelay);
                this.$PC = 5;
                goto Label_0AC1;
            Label_028E:
                this.<pd>__2 = MonoSingleton<GameManager>.Instance.Player;
                this.<rest>__3 = this.<pd>__2.ChallengeMultiMax - this.<pd>__2.ChallengeMultiNum;
                if (((this.<>f__this.mCurrentQuest != null) && (this.<>f__this.mCurrentQuest.IsMulti != null)) && (this.<rest>__3 < 0))
                {
                    goto Label_0309;
                }
                this.$current = this.<>f__this.StartTreasureAnimation();
                this.$PC = 6;
                goto Label_0AC1;
            Label_0309:
                if (this.<>f__this.PostItemAnimationDelay <= 0f)
                {
                    goto Label_0340;
                }
                this.$current = new WaitForSeconds(this.<>f__this.PostItemAnimationDelay);
                this.$PC = 7;
                goto Label_0AC1;
            Label_0340:
                this.<>f__this.TriggerAnimation(this.<>f__this.PostItemAnimationTrigger);
                if ((this.<>f__this.UseUnitGetEffect == null) || (this.<>f__this.mResultData.GetUnits == null))
                {
                    goto Label_03AC;
                }
                this.<controller>__4 = this.<>f__this.get_gameObject().AddComponent<UnitGetWindowController>();
                this.<controller>__4.Init(this.<>f__this.mResultData.GetUnits);
            Label_03AC:
                this.<gm>__5 = MonoSingleton<GameManager>.Instance;
                if (((this.<>f__this.Prefab_UnitDataUnlockPopup != null) == null) || (this.<>f__this.mCurrentQuest == null))
                {
                    goto Label_0638;
                }
                this.<unlockParams>__6 = null;
                this.<i>__7 = 0;
                goto Label_05FF;
            Label_03F0:
                if (this.<gm>__5.Player.Units[this.<i>__7] != null)
                {
                    goto Label_0415;
                }
                goto Label_05F1;
            Label_0415:
                this.<unit>__8 = this.<gm>__5.Player.Units[this.<i>__7];
                this.<unlockParams>__6 = new List<QuestClearUnlockUnitDataParam>();
                if (this.<>f__this.mResultData.SkillUnlocks.TryGetValue(this.<unit>__8.UniqueID, &this.<unlock>__9) != null)
                {
                    goto Label_0471;
                }
                goto Label_05F1;
            Label_0471:
                this.<newUnlocks>__10 = this.<unit>__8.UnlockedSkillDiff(this.<unlock>__9);
                if (((int) this.<newUnlocks>__10.Length) < 1)
                {
                    goto Label_04A7;
                }
                this.<unlockParams>__6.AddRange(this.<newUnlocks>__10);
            Label_04A7:
                if (((string.IsNullOrEmpty(this.<unit>__8.UnitParam.ability) != null) || (string.IsNullOrEmpty(this.<unit>__8.UnitParam.ma_quest) != null)) || (((this.<>f__this.mCurrentQuest.iname == this.<unit>__8.UnitParam.ma_quest) == null) || (this.<>f__this.mCurrentQuest.type == null)))
                {
                    goto Label_056E;
                }
                this.<master>__11 = new QuestClearUnlockUnitDataParam();
                this.<master>__11.type = 4;
                this.<master>__11.add = 1;
                this.<master>__11.new_id = this.<unit>__8.UnitParam.ability.ToString();
                this.<unlockParams>__6.Add(this.<master>__11);
            Label_056E:
                if (this.<unlockParams>__6.Count < 1)
                {
                    goto Label_05F1;
                }
                this.<clearUnlockPopup>__12 = Object.Instantiate<GameObject>(this.<>f__this.Prefab_UnitDataUnlockPopup);
                this.<clearUnlockPopup>__12.SetActive(1);
                DataSource.Bind<UnitData>(this.<clearUnlockPopup>__12, this.<unit>__8);
                DataSource.Bind<QuestClearUnlockUnitDataParam[]>(this.<clearUnlockPopup>__12, this.<unlockParams>__6.ToArray());
                goto Label_05E0;
            Label_05CD:
                this.$current = null;
                this.$PC = 8;
                goto Label_0AC1;
            Label_05E0:
                if ((this.<clearUnlockPopup>__12 != null) != null)
                {
                    goto Label_05CD;
                }
            Label_05F1:
                this.<i>__7 += 1;
            Label_05FF:
                if (this.<i>__7 < this.<gm>__5.Player.Units.Count)
                {
                    goto Label_03F0;
                }
                goto Label_0638;
            Label_0624:
                this.$current = null;
                this.$PC = 9;
                goto Label_0AC1;
            Label_0638:
                if (this.<>f__this.mExpAnimationEnd == null)
                {
                    goto Label_0624;
                }
                this.<showPopup>__13 = 0;
                this.<index>__14 = 0;
                goto Label_0795;
            Label_065B:
                this.<unitData>__15 = this.<gm>__5.Player.FindUnitDataByUniqueID(this.<>f__this.mUnits[this.<index>__14].UniqueID);
                if (this.<unitData>__15 == null)
                {
                    goto Label_0787;
                }
                if (this.<>f__this.mMultiTowerUnitsId[this.<index>__14] == SceneBattle.Instance.Battle.MyPlayerIndex)
                {
                    goto Label_06C6;
                }
                goto Label_0787;
            Label_06C6:
                this.<beforeUnlock>__16 = (this.<>f__this.mResultData.CharacterQuest.ContainsKey(this.<unitData>__15.UniqueID) == null) ? null : this.<>f__this.mResultData.CharacterQuest[this.<unitData>__15.UniqueID];
                if (this.<beforeUnlock>__16 != null)
                {
                    goto Label_0727;
                }
                goto Label_0787;
            Label_0727:
                if (this.<unitData>__15.IsOpenCharacterQuest() == null)
                {
                    goto Label_0787;
                }
                if (this.<unitData>__15.OpenCharacterQuestOnQuestResult(this.<beforeUnlock>__16.Param, this.<oldMemberLv>__0[this.<index>__14]) == null)
                {
                    goto Label_0787;
                }
                this.<showPopup>__13 = 1;
                this.<gm>__5.AddCharacterQuestPopup(this.<>f__this.mUnits[this.<index>__14]);
            Label_0787:
                this.<index>__14 += 1;
            Label_0795:
                if (this.<index>__14 < this.<>f__this.mUnits.Count)
                {
                    goto Label_065B;
                }
                if (this.<showPopup>__13 == null)
                {
                    goto Label_07D0;
                }
                this.<gm>__5.ShowCharacterQuestPopup(GameSettings.Instance.CharacterQuest_Unlock);
            Label_07D0:
                if (this.<>f__this.mCurrentQuest == null)
                {
                    goto Label_0AAB;
                }
                if ((SceneBattle.Instance != null) == null)
                {
                    goto Label_0AAB;
                }
                if ((this.<>f__this.Prefab_MasterAbilityPopup != null) == null)
                {
                    goto Label_0AAB;
                }
                this.<i>__17 = 0;
                goto Label_0A90;
            Label_0812:
                if (this.<>f__this.mUnits[this.<i>__17].MasterAbility == null)
                {
                    goto Label_0837;
                }
                goto Label_0A82;
            Label_0837:
                if (string.IsNullOrEmpty(this.<>f__this.mUnits[this.<i>__17].UnitParam.ma_quest) != null)
                {
                    goto Label_0A82;
                }
                if ((this.<>f__this.mUnits[this.<i>__17].UnitParam.ma_quest != this.<>f__this.mCurrentQuest.iname) == null)
                {
                    goto Label_08A0;
                }
                goto Label_0A82;
            Label_08A0:
                if (this.<>f__this.mCurrentQuest.type == 6)
                {
                    goto Label_0A82;
                }
                this.<masterAbility>__18 = null;
            Label_08BD:
                try
                {
                    this.<masterAbilityID>__19 = this.<>f__this.mUnits[this.<i>__17].UnitParam.ability;
                    this.<masterAbility>__18 = this.<gm>__5.GetAbilityParam(this.<masterAbilityID>__19);
                    goto Label_091C;
                }
                catch (Exception exception1)
                {
                Label_08FF:
                    exception = exception1;
                    this.<e>__20 = exception;
                    DebugUtility.LogException(this.<e>__20);
                    goto Label_0A82;
                }
            Label_091C:
                if (this.<>f__this.NewEffectUse != null)
                {
                    goto Label_09B7;
                }
                this.<>f__this.mMasterAbilityPopup = Object.Instantiate<GameObject>(this.<>f__this.Prefab_MasterAbilityPopup);
                DataSource.Bind<UnitData>(this.<>f__this.mMasterAbilityPopup, this.<>f__this.mUnits[this.<i>__17]);
                DataSource.Bind<AbilityParam>(this.<>f__this.mMasterAbilityPopup, this.<masterAbility>__18);
                goto Label_099C;
            Label_0988:
                this.$current = null;
                this.$PC = 10;
                goto Label_0AC1;
            Label_099C:
                if ((this.<>f__this.mMasterAbilityPopup != null) != null)
                {
                    goto Label_0988;
                }
                goto Label_0A82;
            Label_09B7:
                this.<clearUnlockPopup>__21 = Object.Instantiate<GameObject>(this.<>f__this.Prefab_UnitDataUnlockPopup);
                this.<clearUnlockPopup>__21.SetActive(1);
                DataSource.Bind<UnitData>(this.<clearUnlockPopup>__21, this.<>f__this.mUnits[this.<i>__17]);
                this.<masterAbil>__22 = new QuestClearUnlockUnitDataParam[] { new QuestClearUnlockUnitDataParam() };
                this.<masterAbil>__22[0].type = 2;
                this.<masterAbil>__22[0].add = 1;
                this.<masterAbil>__22[0].new_id = this.<masterAbility>__18.iname;
                DataSource.Bind<QuestClearUnlockUnitDataParam[]>(this.<clearUnlockPopup>__21, this.<masterAbil>__22);
                goto Label_0A71;
            Label_0A5D:
                this.$current = null;
                this.$PC = 11;
                goto Label_0AC1;
            Label_0A71:
                if ((this.<clearUnlockPopup>__21 != null) != null)
                {
                    goto Label_0A5D;
                }
            Label_0A82:
                this.<i>__17 += 1;
            Label_0A90:
                if (this.<i>__17 < this.<>f__this.mUnits.Count)
                {
                    goto Label_0812;
                }
            Label_0AAB:
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 0x1f);
                this.$PC = -1;
            Label_0ABF:
                return 0;
            Label_0AC1:
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
        private sealed class <RecvExpAnimation>c__Iterator131 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <maxUnitExp>__0;
            internal int[] <$s_1048>__1;
            internal int <$s_1049>__2;
            internal int <exp>__3;
            internal float <gainRate>__4;
            internal float <totalTime>__5;
            internal int <playerLv>__6;
            internal int <expGained>__7;
            internal float <expAccumulator>__8;
            internal int[] <unitExpGained>__9;
            internal int <i>__10;
            internal int <deltaExp>__11;
            internal bool <seFlag>__12;
            internal int <i>__13;
            internal int <unitDeltaExp>__14;
            internal int <levelOld>__15;
            internal int $PC;
            internal object $current;
            internal QuestResult <>f__this;

            public <RecvExpAnimation>c__Iterator131()
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
                        goto Label_0456;
                }
                goto Label_046E;
            Label_0021:
                this.<maxUnitExp>__0 = this.<>f__this.mResultData.Record.unitexp;
                this.<$s_1048>__1 = this.<>f__this.AcquiredUnitExp;
                this.<$s_1049>__2 = 0;
                goto Label_0096;
            Label_005E:
                this.<exp>__3 = this.<$s_1048>__1[this.<$s_1049>__2];
                this.<maxUnitExp>__0 = Mathf.Max(this.<maxUnitExp>__0, this.<exp>__3);
                this.<$s_1049>__2 += 1;
            Label_0096:
                if (this.<$s_1049>__2 < ((int) this.<$s_1048>__1.Length))
                {
                    goto Label_005E;
                }
                this.<gainRate>__4 = this.<>f__this.ExpGainRate;
                this.<totalTime>__5 = ((float) this.<maxUnitExp>__0) / this.<>f__this.ExpGainRate;
                if (this.<totalTime>__5 <= this.<>f__this.ExpGainTimeMax)
                {
                    goto Label_0102;
                }
                this.<gainRate>__4 = ((float) this.<maxUnitExp>__0) / this.<>f__this.ExpGainTimeMax;
            Label_0102:
                if (this.<maxUnitExp>__0 <= 0)
                {
                    goto Label_0122;
                }
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0507", 0f);
            Label_0122:
                this.<playerLv>__6 = PlayerData.CalcLevelFromExp(GlobalVars.PlayerExpNew);
                this.<expGained>__7 = 0;
                this.<expAccumulator>__8 = 0f;
                this.<unitExpGained>__9 = new int[(int) this.<>f__this.AcquiredUnitExp.Length];
                this.<i>__10 = 0;
                goto Label_0189;
            Label_016D:
                this.<unitExpGained>__9[this.<i>__10] = 0;
                this.<i>__10 += 1;
            Label_0189:
                if (this.<i>__10 < ((int) this.<unitExpGained>__9.Length))
                {
                    goto Label_016D;
                }
                goto Label_0456;
            Label_01A1:
                this.<deltaExp>__11 = 0;
                this.<expAccumulator>__8 += this.<gainRate>__4 * Time.get_deltaTime();
                if (this.<>f__this.mResultSkipElement == null)
                {
                    goto Label_01E9;
                }
                this.<expAccumulator>__8 *= this.<>f__this.ResultSkipSpeedMul;
            Label_01E9:
                if (this.<expAccumulator>__8 < 1f)
                {
                    goto Label_043F;
                }
                this.<deltaExp>__11 = Mathf.FloorToInt(this.<expAccumulator>__8);
                this.<expAccumulator>__8 = this.<expAccumulator>__8 % 1f;
                this.<expGained>__7 += this.<deltaExp>__11;
                if (this.<maxUnitExp>__0 >= this.<expGained>__7)
                {
                    goto Label_026C;
                }
                this.<deltaExp>__11 = Math.Max(this.<deltaExp>__11 - (this.<expGained>__7 - this.<maxUnitExp>__0), 0);
                this.<expGained>__7 = this.<maxUnitExp>__0;
            Label_026C:
                this.<seFlag>__12 = 0;
                this.<i>__13 = 0;
                goto Label_0424;
            Label_027F:
                this.<unitDeltaExp>__14 = this.<deltaExp>__11;
                *((int*) &(this.<unitExpGained>__9[this.<i>__13])) += this.<unitDeltaExp>__14;
                if (this.<>f__this.AcquiredUnitExp[this.<i>__13] >= this.<unitExpGained>__9[this.<i>__13])
                {
                    goto Label_031C;
                }
                this.<unitDeltaExp>__14 = Math.Max(this.<unitDeltaExp>__14 - (this.<unitExpGained>__9[this.<i>__13] - this.<>f__this.AcquiredUnitExp[this.<i>__13]), 0);
                this.<unitExpGained>__9[this.<i>__13] = this.<>f__this.AcquiredUnitExp[this.<i>__13];
            Label_031C:
                if (this.<unitDeltaExp>__14 != null)
                {
                    goto Label_032C;
                }
                goto Label_0416;
            Label_032C:
                this.<levelOld>__15 = this.<>f__this.mUnits[this.<i>__13].Lv;
                this.<>f__this.mUnits[this.<i>__13].GainExp(this.<unitDeltaExp>__14, this.<playerLv>__6);
                GameParameter.UpdateAll(this.<>f__this.mUnitListItems[this.<i>__13]);
                if (this.<>f__this.mUnits[this.<i>__13].Lv == this.<levelOld>__15)
                {
                    goto Label_0416;
                }
                if (string.IsNullOrEmpty(this.<>f__this.Unit_LevelUpTrigger) != null)
                {
                    goto Label_0416;
                }
                GameUtility.SetAnimatorTrigger(this.<>f__this.mUnitListItems[this.<i>__13], this.<>f__this.Unit_LevelUpTrigger);
                if (this.<seFlag>__12 != null)
                {
                    goto Label_0416;
                }
                this.<seFlag>__12 = 1;
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0012", 0f);
            Label_0416:
                this.<i>__13 += 1;
            Label_0424:
                if (this.<i>__13 < this.<>f__this.mUnits.Count)
                {
                    goto Label_027F;
                }
            Label_043F:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0470;
            Label_0456:
                if (this.<expGained>__7 < this.<maxUnitExp>__0)
                {
                    goto Label_01A1;
                }
                this.$PC = -1;
            Label_046E:
                return 0;
            Label_0470:
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
        private sealed class <RecvTrustAnimation>c__Iterator132 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal List<ConceptCardData> <trustMasterBonusList>__0;
            internal bool <bNotEquipAnyone>__1;
            internal List<UnitData>.Enumerator <$s_1050>__2;
            internal UnitData <ud>__3;
            internal int <i>__4;
            internal List<QuestResult.TrustAnimWork> <trustAnimWork>__5;
            internal List<GameObject>.Enumerator <$s_1051>__6;
            internal GameObject <go>__7;
            internal UnitData <beforeUnit>__8;
            internal UnitData <afterUnit>__9;
            internal ConceptCardIconBattleResult <cardIcon>__10;
            internal ConceptCardData <cardData>__11;
            internal List<QuestResult.TrustAnimWork>.Enumerator <$s_1052>__12;
            internal QuestResult.TrustAnimWork <wk>__13;
            internal bool <is_reward>__14;
            internal List<QuestResult.TrustAnimWork>.Enumerator <$s_1053>__15;
            internal QuestResult.TrustAnimWork <wk>__16;
            internal int <i>__17;
            internal float <animtime>__18;
            internal List<QuestResult.TrustAnimWork>.Enumerator <$s_1054>__19;
            internal QuestResult.TrustAnimWork <wk>__20;
            internal float <percent>__21;
            internal float <drawTrust>__22;
            internal bool <play_trustup_se>__23;
            internal List<QuestResult.TrustAnimWork>.Enumerator <$s_1055>__24;
            internal QuestResult.TrustAnimWork <wk>__25;
            internal int <before>__26;
            internal int <after>__27;
            internal int <max>__28;
            internal bool <is_reward>__29;
            internal bool <is_trust_bonus>__30;
            internal List<ConceptCardData>.Enumerator <$s_1056>__31;
            internal ConceptCardData <data>__32;
            internal ConceptCardEffectRoutine <routine>__33;
            internal Canvas <overlayCanvas>__34;
            internal int $PC;
            internal object $current;
            internal QuestResult <>f__this;

            public <RecvTrustAnimation>c__Iterator132()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3D8(UnitData u)
            {
                return (u.UniqueID == this.<beforeUnit>__8.UniqueID);
            }

            internal bool <>m__3D9(ConceptCardData c)
            {
                return (c.UniqueID == this.<afterUnit>__9.ConceptCard.UniqueID);
            }

            [DebuggerHidden]
            public void Dispose()
            {
                uint num;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_004F;

                    case 1:
                        goto Label_004F;

                    case 2:
                        goto Label_004F;

                    case 3:
                        goto Label_004F;

                    case 4:
                        goto Label_004F;

                    case 5:
                        goto Label_0039;

                    case 6:
                        goto Label_0039;

                    case 7:
                        goto Label_0039;
                }
                goto Label_004F;
            Label_0039:
                try
                {
                    goto Label_004F;
                }
                finally
                {
                Label_003E:
                    ((List<ConceptCardData>.Enumerator) this.<$s_1056>__31).Dispose();
                }
            Label_004F:
                return;
            }

            public unsafe bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                flag = 0;
                switch (num)
                {
                    case 0:
                        goto Label_003B;

                    case 1:
                        goto Label_0114;

                    case 2:
                        goto Label_0415;

                    case 3:
                        goto Label_04D6;

                    case 4:
                        goto Label_0669;

                    case 5:
                        goto Label_085C;

                    case 6:
                        goto Label_085C;

                    case 7:
                        goto Label_085C;
                }
                goto Label_0958;
            Label_003B:
                this.<trustMasterBonusList>__0 = new List<ConceptCardData>();
                this.<bNotEquipAnyone>__1 = 1;
                this.<$s_1050>__2 = this.<>f__this.mUnits.GetEnumerator();
            Label_0063:
                try
                {
                    goto Label_0095;
                Label_0068:
                    this.<ud>__3 = &this.<$s_1050>__2.Current;
                    if (this.<ud>__3.ConceptCard != null)
                    {
                        goto Label_008E;
                    }
                    goto Label_0095;
                Label_008E:
                    this.<bNotEquipAnyone>__1 = 0;
                Label_0095:
                    if (&this.<$s_1050>__2.MoveNext() != null)
                    {
                        goto Label_0068;
                    }
                    goto Label_00BB;
                }
                finally
                {
                Label_00AA:
                    ((List<UnitData>.Enumerator) this.<$s_1050>__2).Dispose();
                }
            Label_00BB:
                if (this.<bNotEquipAnyone>__1 == null)
                {
                    goto Label_00CB;
                }
                goto Label_0958;
            Label_00CB:
                this.<i>__4 = 0;
                goto Label_0122;
            Label_00D7:
                if (this.<i>__4 < 3)
                {
                    goto Label_00F8;
                }
                if (this.<>f__this.mResultSkipElement == null)
                {
                    goto Label_00F8;
                }
                goto Label_012F;
            Label_00F8:
                this.$current = new WaitForSeconds(0.1f);
                this.$PC = 1;
                goto Label_095A;
            Label_0114:
                this.<i>__4 += 1;
            Label_0122:
                if (this.<i>__4 < 10)
                {
                    goto Label_00D7;
                }
            Label_012F:
                this.<trustAnimWork>__5 = new List<QuestResult.TrustAnimWork>();
                this.<$s_1051>__6 = this.<>f__this.mUnitListItems.GetEnumerator();
            Label_0150:
                try
                {
                    goto Label_0294;
                Label_0155:
                    this.<go>__7 = &this.<$s_1051>__6.Current;
                    this.<beforeUnit>__8 = DataSource.FindDataOfClass<UnitData>(this.<go>__7, null);
                    if (this.<beforeUnit>__8 != null)
                    {
                        goto Label_0188;
                    }
                    goto Label_0294;
                Label_0188:
                    this.<afterUnit>__9 = MonoSingleton<GameManager>.Instance.Player.Units.Find(new Predicate<UnitData>(this.<>m__3D8));
                    if (this.<afterUnit>__9 != null)
                    {
                        goto Label_01BE;
                    }
                    goto Label_0294;
                Label_01BE:
                    this.<cardIcon>__10 = this.<go>__7.GetComponent<ConceptCardIconBattleResult>();
                    if ((this.<cardIcon>__10 == null) == null)
                    {
                        goto Label_01E5;
                    }
                    goto Label_0294;
                Label_01E5:
                    this.<trustAnimWork>__5.Add(new QuestResult.TrustAnimWork(this.<beforeUnit>__8, this.<afterUnit>__9, this.<cardIcon>__10));
                    if (this.<afterUnit>__9.ConceptCard == null)
                    {
                        goto Label_0294;
                    }
                    this.<cardData>__11 = MonoSingleton<GameManager>.Instance.Player.ConceptCards.Find(new Predicate<ConceptCardData>(this.<>m__3D9));
                    if (this.<cardData>__11 == null)
                    {
                        goto Label_0294;
                    }
                    this.<cardData>__11.SetTrust(this.<afterUnit>__9.ConceptCard.Trust);
                    this.<cardData>__11.SetBonus(this.<afterUnit>__9.ConceptCard.TrustBonus);
                    this.<afterUnit>__9.ConceptCard = this.<cardData>__11;
                Label_0294:
                    if (&this.<$s_1051>__6.MoveNext() != null)
                    {
                        goto Label_0155;
                    }
                    goto Label_02BA;
                }
                finally
                {
                Label_02A9:
                    ((List<GameObject>.Enumerator) this.<$s_1051>__6).Dispose();
                }
            Label_02BA:
                this.<$s_1052>__12 = this.<trustAnimWork>__5.GetEnumerator();
            Label_02CB:
                try
                {
                    goto Label_03CC;
                Label_02D0:
                    this.<wk>__13 = &this.<$s_1052>__12.Current;
                    if ((this.<wk>__13.cardIcon == null) == null)
                    {
                        goto Label_02FC;
                    }
                    goto Label_03CC;
                Label_02FC:
                    if (this.<wk>__13.beforeUnit.ConceptCard == null)
                    {
                        goto Label_03CC;
                    }
                    if (this.<wk>__13.afterUnit.ConceptCard != null)
                    {
                        goto Label_032B;
                    }
                    goto Label_03CC;
                Label_032B:
                    this.<is_reward>__14 = (this.<wk>__13.afterUnit.ConceptCard.GetReward() == null) ? 0 : 1;
                    if (this.<is_reward>__14 == null)
                    {
                        goto Label_03AB;
                    }
                    this.<wk>__13.cardIcon.ShowStartAnimation((this.<wk>__13.beforeUnit.ConceptCard.Trust == this.<wk>__13.afterUnit.ConceptCard.Trust) == 0);
                    goto Label_03CC;
                Label_03AB:
                    this.<wk>__13.cardIcon.ShowStartAnimation(0);
                    this.<wk>__13.cardIcon.SetNoRewardTrustText();
                Label_03CC:
                    if (&this.<$s_1052>__12.MoveNext() != null)
                    {
                        goto Label_02D0;
                    }
                    goto Label_03F2;
                }
                finally
                {
                Label_03E1:
                    ((List<QuestResult.TrustAnimWork>.Enumerator) this.<$s_1052>__12).Dispose();
                }
            Label_03F2:
                FlowNode_TriggerLocalEvent.TriggerLocalEvent(this.<>f__this, "PLAY_SE_CONCEPTCARD_OPEN");
                this.$current = null;
                this.$PC = 2;
                goto Label_095A;
            Label_0415:
                this.<$s_1053>__15 = this.<trustAnimWork>__5.GetEnumerator();
            Label_0426:
                try
                {
                    goto Label_0467;
                Label_042B:
                    this.<wk>__16 = &this.<$s_1053>__15.Current;
                    if ((this.<wk>__16.cardIcon == null) == null)
                    {
                        goto Label_0457;
                    }
                    goto Label_0467;
                Label_0457:
                    this.<wk>__16.cardIcon.ShowAnimationAfter();
                Label_0467:
                    if (&this.<$s_1053>__15.MoveNext() != null)
                    {
                        goto Label_042B;
                    }
                    goto Label_048D;
                }
                finally
                {
                Label_047C:
                    ((List<QuestResult.TrustAnimWork>.Enumerator) this.<$s_1053>__15).Dispose();
                }
            Label_048D:
                this.<i>__17 = 0;
                goto Label_04E4;
            Label_0499:
                if (this.<i>__17 < 3)
                {
                    goto Label_04BA;
                }
                if (this.<>f__this.mResultSkipElement == null)
                {
                    goto Label_04BA;
                }
                goto Label_04F1;
            Label_04BA:
                this.$current = new WaitForSeconds(0.1f);
                this.$PC = 3;
                goto Label_095A;
            Label_04D6:
                this.<i>__17 += 1;
            Label_04E4:
                if (this.<i>__17 < 10)
                {
                    goto Label_0499;
                }
            Label_04F1:
                this.<animtime>__18 = 0f;
                goto Label_0669;
            Label_0501:
                this.<animtime>__18 += Time.get_deltaTime();
                this.<$s_1054>__19 = this.<trustAnimWork>__5.GetEnumerator();
            Label_0524:
                try
                {
                    goto Label_0630;
                Label_0529:
                    this.<wk>__20 = &this.<$s_1054>__19.Current;
                    if (this.<wk>__20.beforeUnit.ConceptCard != null)
                    {
                        goto Label_0554;
                    }
                    goto Label_0630;
                Label_0554:
                    if ((this.<wk>__20.cardIcon == null) == null)
                    {
                        goto Label_056F;
                    }
                    goto Label_0630;
                Label_056F:
                    this.<percent>__21 = Mathf.Min(1f, this.<animtime>__18 / 0.1f);
                    this.<drawTrust>__22 = (this.<percent>__21 * ((float) (this.<wk>__20.afterUnit.ConceptCard.Trust - this.<wk>__20.beforeUnit.ConceptCard.Trust))) + ((float) this.<wk>__20.beforeUnit.ConceptCard.Trust);
                    if (this.<wk>__20.afterUnit.ConceptCard.GetReward() == null)
                    {
                        goto Label_0620;
                    }
                    this.<wk>__20.cardIcon.SetTrustText((int) this.<drawTrust>__22);
                    goto Label_0630;
                Label_0620:
                    this.<wk>__20.cardIcon.SetNoRewardTrustText();
                Label_0630:
                    if (&this.<$s_1054>__19.MoveNext() != null)
                    {
                        goto Label_0529;
                    }
                    goto Label_0656;
                }
                finally
                {
                Label_0645:
                    ((List<QuestResult.TrustAnimWork>.Enumerator) this.<$s_1054>__19).Dispose();
                }
            Label_0656:
                this.$current = null;
                this.$PC = 4;
                goto Label_095A;
            Label_0669:
                if (this.<animtime>__18 < 0.1f)
                {
                    goto Label_0501;
                }
                this.<play_trustup_se>__23 = 0;
                this.<$s_1055>__24 = this.<trustAnimWork>__5.GetEnumerator();
            Label_0691:
                try
                {
                    goto Label_07E1;
                Label_0696:
                    this.<wk>__25 = &this.<$s_1055>__24.Current;
                    if (this.<wk>__25.beforeUnit.ConceptCard == null)
                    {
                        goto Label_07E1;
                    }
                    if (this.<wk>__25.afterUnit.ConceptCard != null)
                    {
                        goto Label_06D6;
                    }
                    goto Label_07E1;
                Label_06D6:
                    this.<before>__26 = this.<wk>__25.beforeUnit.ConceptCard.Trust;
                    this.<after>__27 = this.<wk>__25.afterUnit.ConceptCard.Trust;
                    this.<max>__28 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
                    this.<is_reward>__29 = (this.<wk>__25.afterUnit.ConceptCard.GetReward() == null) ? 0 : 1;
                    this.<is_trust_bonus>__30 = this.<wk>__25.beforeUnit.ConceptCard.TrustBonus;
                    if (this.<is_reward>__29 == null)
                    {
                        goto Label_07E1;
                    }
                    if (this.<before>__26 == this.<after>__27)
                    {
                        goto Label_07AA;
                    }
                    this.<play_trustup_se>__23 = 1;
                    this.<wk>__25.cardIcon.StartTrustUpAnimation();
                Label_07AA:
                    if (this.<is_trust_bonus>__30 != null)
                    {
                        goto Label_07E1;
                    }
                    if (this.<after>__27 < this.<max>__28)
                    {
                        goto Label_07E1;
                    }
                    this.<trustMasterBonusList>__0.Add(this.<wk>__25.beforeUnit.ConceptCard);
                Label_07E1:
                    if (&this.<$s_1055>__24.MoveNext() != null)
                    {
                        goto Label_0696;
                    }
                    goto Label_0807;
                }
                finally
                {
                Label_07F6:
                    ((List<QuestResult.TrustAnimWork>.Enumerator) this.<$s_1055>__24).Dispose();
                }
            Label_0807:
                if (0 >= this.<trustMasterBonusList>__0.Count)
                {
                    goto Label_082D;
                }
                FlowNode_TriggerLocalEvent.TriggerLocalEvent(this.<>f__this, "PLAY_SE_TRUSTMASTER");
                goto Label_0848;
            Label_082D:
                if (this.<play_trustup_se>__23 == null)
                {
                    goto Label_0848;
                }
                FlowNode_TriggerLocalEvent.TriggerLocalEvent(this.<>f__this, "PLAY_SE_TRUST_UP");
            Label_0848:
                this.<$s_1056>__31 = this.<trustMasterBonusList>__0.GetEnumerator();
                num = -3;
            Label_085C:
                try
                {
                    switch ((num - 5))
                    {
                        case 0:
                            goto Label_08A4;

                        case 1:
                            goto Label_08E7;

                        case 2:
                            goto Label_0912;
                    }
                    goto Label_0922;
                Label_0875:
                    this.<data>__32 = &this.<$s_1056>__31.Current;
                    this.$current = new WaitForSeconds(1f);
                    this.$PC = 5;
                    flag = 1;
                    goto Label_095A;
                Label_08A4:
                    this.<routine>__33 = new ConceptCardEffectRoutine();
                    this.<overlayCanvas>__34 = UIUtility.PushCanvas(0, -1);
                    this.$current = this.<routine>__33.PlayTrustMaster(this.<overlayCanvas>__34, this.<data>__32);
                    this.$PC = 6;
                    flag = 1;
                    goto Label_095A;
                Label_08E7:
                    this.$current = this.<routine>__33.PlayTrustMasterReward(this.<overlayCanvas>__34, this.<data>__32);
                    this.$PC = 7;
                    flag = 1;
                    goto Label_095A;
                Label_0912:
                    Object.Destroy(this.<overlayCanvas>__34.get_gameObject());
                Label_0922:
                    if (&this.<$s_1056>__31.MoveNext() != null)
                    {
                        goto Label_0875;
                    }
                    goto Label_094C;
                }
                finally
                {
                Label_0937:
                    if (flag == null)
                    {
                        goto Label_093B;
                    }
                Label_093B:
                    ((List<ConceptCardData>.Enumerator) this.<$s_1056>__31).Dispose();
                }
            Label_094C:
                goto Label_0958;
            Label_0958:
                return 0;
            Label_095A:
                return 1;
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
        private sealed class <Start>c__AnonStorey388
        {
            internal string iid;

            public <Start>c__AnonStorey388()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3D2(UnitData p)
            {
                return (p.UnitParam.iname == this.iid);
            }
        }

        [CompilerGenerated]
        private sealed class <Start>c__AnonStorey389
        {
            internal VersusCoinParam coinParam;

            public <Start>c__AnonStorey389()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3D3(QuestResult.DropItemData x)
            {
                return (x.Param.iname == this.coinParam.coin_iname);
            }
        }

        [CompilerGenerated]
        private sealed class <StartTreasureAnimation>c__Iterator133 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal QuestResult <>f__this;

            public <StartTreasureAnimation>c__Iterator133()
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
                        goto Label_0054;
                }
                goto Label_005B;
            Label_0021:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.TreasureAnimation(this.<>f__this.mTreasureListItems));
                this.$PC = 1;
                goto Label_005D;
            Label_0054:
                this.$PC = -1;
            Label_005B:
                return 0;
            Label_005D:
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
        private sealed class <TreasureAnimation>c__Iterator134 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal float <time>__0;
            internal int <currentItem>__1;
            internal List<GameObject> ListItems;
            internal float <waittime_interval>__2;
            internal ItemIcon <item_icon>__3;
            internal DropItemIcon <drop_item_icon>__4;
            internal int $PC;
            internal object $current;
            internal List<GameObject> <$>ListItems;
            internal QuestResult <>f__this;

            public <TreasureAnimation>c__Iterator134()
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
                        goto Label_01F0;
                }
                goto Label_020D;
            Label_0021:
                this.<time>__0 = 0f;
                this.<currentItem>__1 = 0;
                goto Label_01F0;
            Label_0038:
                this.<time>__0 += Time.get_deltaTime();
                goto Label_01C3;
            Label_004F:
                this.<waittime_interval>__2 = this.<>f__this.Treasure_TriggerInterval;
                if (this.<>f__this.mResultSkipElement == null)
                {
                    goto Label_008D;
                }
                this.<waittime_interval>__2 = this.<>f__this.Treasure_TriggerInterval / this.<>f__this.ResultSkipSpeedMul;
            Label_008D:
                if (this.<time>__0 < this.<waittime_interval>__2)
                {
                    goto Label_01D9;
                }
                if (string.IsNullOrEmpty(this.<>f__this.Treasure_TurnOnTrigger) != null)
                {
                    goto Label_0198;
                }
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0508", 0f);
                GameUtility.SetAnimatorTrigger(this.ListItems[this.<currentItem>__1], this.<>f__this.Treasure_TurnOnTrigger);
                if (this.ListItems[this.<currentItem>__1] == null)
                {
                    goto Label_0198;
                }
                this.<item_icon>__3 = this.ListItems[this.<currentItem>__1].GetComponent<ItemIcon>();
                if ((this.<item_icon>__3 != null) == null)
                {
                    goto Label_0150;
                }
                if (this.<item_icon>__3.IsSecret == null)
                {
                    goto Label_0198;
                }
                this.<item_icon>__3.ExchgSecretIcon();
                goto Label_0198;
            Label_0150:
                this.<drop_item_icon>__4 = this.ListItems[this.<currentItem>__1].GetComponent<DropItemIcon>();
                if ((this.<drop_item_icon>__4 != null) == null)
                {
                    goto Label_0198;
                }
                if (this.<drop_item_icon>__4.IsSecret == null)
                {
                    goto Label_0198;
                }
                this.<drop_item_icon>__4.ExchgSecretIcon();
            Label_0198:
                this.<time>__0 -= this.<waittime_interval>__2;
                this.<currentItem>__1 += 1;
                goto Label_01C3;
                goto Label_01D9;
            Label_01C3:
                if (this.<currentItem>__1 < this.ListItems.Count)
                {
                    goto Label_004F;
                }
            Label_01D9:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_020F;
            Label_01F0:
                if (this.<currentItem>__1 < this.ListItems.Count)
                {
                    goto Label_0038;
                }
                this.$PC = -1;
            Label_020D:
                return 0;
            Label_020F:
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

        private class CampaignPartyExp : MonoBehaviour
        {
            public int Exp;

            public CampaignPartyExp()
            {
                base..ctor();
                return;
            }

            private unsafe void Start()
            {
                Text text;
                text = base.get_gameObject().GetComponent<Text>();
                if ((text != null) == null)
                {
                    goto Label_0029;
                }
                text.set_text(&this.Exp.ToString());
            Label_0029:
                return;
            }
        }

        public class DropItemData : ItemData
        {
            public bool mIsSecret;
            private EBattleRewardType mBattleRewardType;
            private ConceptCardData mConceptCardData;

            public DropItemData()
            {
                this.mBattleRewardType = 2;
                base..ctor();
                return;
            }

            private void SetupConceptCard(string iname, int num)
            {
                this.mBattleRewardType = 3;
                this.mConceptCardData = ConceptCardData.CreateConceptCardDataForDisplay(iname);
                base.mNum = num;
                return;
            }

            public void SetupDropItemData(EBattleRewardType rewardType, long iid, string iname, int num)
            {
                this.mBattleRewardType = rewardType;
                if (rewardType != 2)
                {
                    goto Label_001E;
                }
                base.Setup(iid, iname, num);
                goto Label_002E;
            Label_001E:
                if (rewardType != 3)
                {
                    goto Label_002E;
                }
                this.SetupConceptCard(iname, num);
            Label_002E:
                return;
            }

            public EBattleRewardType BattleRewardType
            {
                get
                {
                    return this.mBattleRewardType;
                }
            }

            public bool IsItem
            {
                get
                {
                    return (this.mBattleRewardType == 2);
                }
            }

            public bool IsConceptCard
            {
                get
                {
                    return (this.mBattleRewardType == 3);
                }
            }

            public ItemParam itemParam
            {
                get
                {
                    return base.Param;
                }
            }

            public ItemData itemData
            {
                get
                {
                    return this;
                }
            }

            public ConceptCardParam conceptCardParam
            {
                get
                {
                    return ((this.mConceptCardData == null) ? null : this.mConceptCardData.Param);
                }
            }

            public ConceptCardData conceptCardData
            {
                get
                {
                    return this.mConceptCardData;
                }
            }
        }

        public class TrustAnimWork
        {
            public UnitData beforeUnit;
            public UnitData afterUnit;
            public ConceptCardIconBattleResult cardIcon;

            public TrustAnimWork(UnitData before, UnitData after, ConceptCardIconBattleResult card)
            {
                base..ctor();
                this.beforeUnit = before;
                this.afterUnit = after;
                this.cardIcon = card;
                return;
            }
        }
    }
}

