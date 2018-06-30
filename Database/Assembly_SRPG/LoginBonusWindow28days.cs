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

    [Pin(0x10, "Message Closed", 1, 0), Pin(12, "Last Day", 1, 2), Pin(13, "Taked", 1, 3), Pin(14, "詳細表示（アイテム）", 1, 4), Pin(15, "詳細表示（真理念装）", 1, 5), Pin(10, "Load Complete", 1, 1), Pin(11, "Take Bonus", 0, 0)]
    public class LoginBonusWindow28days : MonoBehaviour, IFlowInterface
    {
        public GameObject ItemList;
        public GameObject[] PositionList;
        [HeaderBar("▼アイコン表示用オブジェクト")]
        public ListItemEvents Item_Normal;
        public ListItemEvents Item_Taken;
        public Json_LoginBonus[] DebugItems;
        public int DebugBonusCount;
        private int mLoginBonusCount;
        public GameObject BonusParticleEffect;
        [HeaderBar("▼演出時のアイコン表示用オブジェクト")]
        public GameObject LastItem;
        public GameObject TodayItem;
        public GameObject TommorowItem;
        public RectTransform TodayBadge;
        public RectTransform TommorowBadge;
        public string CheckName;
        public string[] DisabledFirstDayNames;
        public string TableID;
        public List<Toggle> WeakToggle;
        public Text GainLastItemMessage;
        public Text PopupMessage;
        public GameObject RemainingCounter;
        public Text RemainingCount;
        [HeaderBar("▼3Dモデル表示用")]
        public Transform PreviewParent;
        public RawImage PreviewImage;
        public Camera PreviewCamera;
        public float PreviewCameraDistance;
        public bool IsConfigWindow;
        public string DebugNotifyUnitID;
        public LoginBonusWindow Message;
        public float MessageDelay;
        private UnitData mCurrentUnit;
        private UnitPreview mCurrentPreview;
        private RenderTexture mPreviewUnitRT;
        private LoginBonusWindow mMessageWindow;
        private List<ListItemEvents> mItems;
        private int mCurrentWeak;
        private string[] mNotifyUnits;

        public LoginBonusWindow28days()
        {
            this.CheckName = "CHECK";
            this.PreviewCameraDistance = 10f;
            this.mItems = new List<ListItemEvents>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 11)
            {
                goto Label_000F;
            }
            goto Label_001A;
        Label_000F:
            this.FlipTodaysItem();
        Label_001A:
            return;
        }

        private void Awake()
        {
            if ((this.Item_Normal != null) == null)
            {
                goto Label_0022;
            }
            this.Item_Normal.get_gameObject().SetActive(0);
        Label_0022:
            if ((this.Item_Taken != null) == null)
            {
                goto Label_0044;
            }
            this.Item_Taken.get_gameObject().SetActive(0);
        Label_0044:
            if ((this.TodayBadge != null) == null)
            {
                goto Label_0066;
            }
            this.TodayBadge.get_gameObject().SetActive(0);
        Label_0066:
            if ((this.TommorowBadge != null) == null)
            {
                goto Label_0088;
            }
            this.TommorowBadge.get_gameObject().SetActive(0);
        Label_0088:
            return;
        }

        [DebuggerHidden]
        private IEnumerator DelayPopupMessage()
        {
            <DelayPopupMessage>c__Iterator122 iterator;
            iterator = new <DelayPopupMessage>c__Iterator122();
            iterator.<>f__this = this;
            return iterator;
        }

        private void DisableFirstDayHiddenOject(GameObject parent)
        {
            int num;
            string str;
            Transform transform;
            if ((parent == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (this.DisabledFirstDayNames != null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            num = 0;
            goto Label_0062;
        Label_0020:
            str = this.DisabledFirstDayNames[num];
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0039;
            }
            goto Label_005E;
        Label_0039:
            transform = parent.get_transform().FindChild(str);
            if ((transform != null) == null)
            {
                goto Label_005E;
            }
            transform.get_gameObject().SetActive(0);
        Label_005E:
            num += 1;
        Label_0062:
            if (num < ((int) this.DisabledFirstDayNames.Length))
            {
                goto Label_0020;
            }
            return;
        }

        private void FlipTodaysItem()
        {
            int num;
            ListItemEvents events;
            GiftRecieveItemData data;
            ListItemEvents events2;
            GiftRecieveItem item;
            Transform transform;
            Animator animator;
            if (this.mLoginBonusCount <= 0)
            {
                goto Label_0022;
            }
            if (this.mItems.Count >= this.mLoginBonusCount)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            num = Math.Max(this.mLoginBonusCount - 1, 0);
            events = this.mItems[num];
            if ((this.BonusParticleEffect != null) == null)
            {
                goto Label_0075;
            }
            UIUtility.SpawnParticle(this.BonusParticleEffect, events.get_transform() as RectTransform, new Vector2(0.5f, 0.5f));
        Label_0075:
            data = DataSource.FindDataOfClass<GiftRecieveItemData>(events.get_gameObject(), null);
            events2 = Object.Instantiate<ListItemEvents>(this.Item_Taken);
            DataSource.Bind<GiftRecieveItemData>(events2.get_gameObject(), data);
            events2.get_transform().SetParent(events.get_transform().get_parent(), 0);
            events2.get_transform().SetSiblingIndex(events.get_transform().GetSiblingIndex());
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            item = events2.GetComponentInChildren<GiftRecieveItem>();
            if ((item != null) == null)
            {
                goto Label_00F5;
            }
            item.UpdateValue();
        Label_00F5:
            if ((this.TodayBadge != null) == null)
            {
                goto Label_0118;
            }
            this.TodayBadge.SetParent(events2.get_transform(), 0);
        Label_0118:
            Object.Destroy(events.get_gameObject());
            events2.get_gameObject().SetActive(1);
            transform = events2.get_transform().FindChild(this.CheckName);
            if ((transform != null) == null)
            {
                goto Label_016D;
            }
            animator = transform.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_016D;
            }
            animator.set_enabled(1);
        Label_016D:
            if (num != null)
            {
                goto Label_017F;
            }
            this.DisableFirstDayHiddenOject(events2.get_gameObject());
        Label_017F:
            this.mItems[num] = events2;
            if ((this.Message != null) == null)
            {
                goto Label_01AB;
            }
            base.StartCoroutine(this.DelayPopupMessage());
            return;
        Label_01AB:
            FlowNode_GameObject.ActivateOutputLinks(this, 13);
            return;
        }

        private string GetGainLastItemMessage(GiftRecieveItemData item)
        {
            object[] objArray1;
            if (item != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            objArray1 = new object[] { item.name };
            return LocalizedText.Get("sys.LOGBO_GAIN_LASTITEM", objArray1);
        }

        private string GetPopupMessage(GiftRecieveItemData item)
        {
            object[] objArray1;
            if (item != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            objArray1 = new object[] { item.name, (int) item.num };
            return LocalizedText.Get("sys.LOGBO_POPUP_MESSAGE", objArray1);
        }

        private void OnDestroy()
        {
            GameUtility.DestroyGameObject(this.mCurrentPreview);
            this.mCurrentPreview = null;
            if ((this.mPreviewUnitRT != null) == null)
            {
                goto Label_0035;
            }
            RenderTexture.ReleaseTemporary(this.mPreviewUnitRT);
            this.mPreviewUnitRT = null;
        Label_0035:
            if ((this.mMessageWindow != null) == null)
            {
                goto Label_005D;
            }
            Object.Destroy(this.mMessageWindow.get_gameObject());
            this.mMessageWindow = null;
        Label_005D:
            return;
        }

        private void OnItemSelect(GameObject go)
        {
            GiftRecieveItemData data;
            ConceptCardData data2;
            data = DataSource.FindDataOfClass<GiftRecieveItemData>(go, null);
            if (data != null)
            {
                goto Label_000F;
            }
            return;
        Label_000F:
            if (data.type != 1L)
            {
                goto Label_0034;
            }
            GlobalVars.SelectedItemID = data.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 14);
            goto Label_0089;
        Label_0034:
            if (data.type != 0x1000L)
            {
                goto Label_0069;
            }
            data2 = ConceptCardData.CreateConceptCardDataForDisplay(data.iname);
            GlobalVars.SelectedConceptCardData.Set(data2);
            FlowNode_GameObject.ActivateOutputLinks(this, 15);
            goto Label_0089;
        Label_0069:
            DebugUtility.LogError(string.Format("不明な種類のログインボーナスが設定されています。{0} => {1}個", data.iname, (int) data.num));
        Label_0089:
            return;
        }

        private void OnWeakSelect(GameObject go)
        {
            int num;
            int num2;
            int num3;
            ListItemEvents events;
            Transform transform;
            Animator animator;
            <OnWeakSelect>c__AnonStorey35B storeyb;
            storeyb = new <OnWeakSelect>c__AnonStorey35B();
            storeyb.go = go;
            this.mCurrentWeak = this.WeakToggle.FindIndex(new Predicate<Toggle>(storeyb.<>m__35C));
            if (this.WeakToggle == null)
            {
                goto Label_006E;
            }
            num = 0;
            goto Label_005D;
        Label_003F:
            this.WeakToggle[num].set_isOn(num == this.mCurrentWeak);
            num += 1;
        Label_005D:
            if (num < this.WeakToggle.Count)
            {
                goto Label_003F;
            }
        Label_006E:
            num2 = 0;
            num3 = 0;
            goto Label_00DE;
        Label_0077:
            events = this.mItems[num2];
            events.get_gameObject().SetActive(num3 == this.mCurrentWeak);
            transform = events.get_transform().FindChild(this.CheckName);
            if ((transform != null) == null)
            {
                goto Label_00D6;
            }
            animator = transform.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_00D6;
            }
            animator.set_enabled(0);
        Label_00D6:
            num2 += 1;
            num3 = num2 / 7;
        Label_00DE:
            if (num2 < this.mItems.Count)
            {
                goto Label_0077;
            }
            return;
        }

        private unsafe void Start()
        {
            Type[] typeArray1;
            GameManager manager;
            Json_LoginBonus[] bonusArray;
            GiftRecieveItemData data;
            GiftRecieveItemData data2;
            GiftRecieveItemData data3;
            List<GiftRecieveItemData> list;
            int num;
            GiftRecieveItemData data4;
            string str;
            int num2;
            ItemParam param;
            ConceptCardParam param2;
            int num3;
            ListItemEvents events;
            ListItemEvents events2;
            Transform transform;
            Animator animator;
            Transform transform2;
            int num4;
            int num5;
            GiftRecieveItem item;
            int num6;
            int num7;
            bool flag;
            GiftRecieveItem item2;
            GiftRecieveItem item3;
            GiftRecieveItem item4;
            int num8;
            int num9;
            ItemData data5;
            string str2;
            Random random;
            int num10;
            GameObject obj2;
            int num11;
            int num12;
            <Start>c__AnonStorey35A storeya;
            manager = MonoSingleton<GameManager>.Instance;
            bonusArray = null;
            if (this.IsConfigWindow == null)
            {
                goto Label_0055;
            }
            bonusArray = manager.Player.LoginBonus28days.bonuses;
            this.mLoginBonusCount = manager.Player.LoginBonus28days.count;
            this.mNotifyUnits = manager.Player.LoginBonus28days.bonus_units;
            goto Label_0095;
        Label_0055:
            bonusArray = manager.Player.FindLoginBonuses(this.TableID);
            this.mLoginBonusCount = manager.Player.LoginCountWithType(this.TableID);
            this.mNotifyUnits = manager.Player.GetLoginBonuseUnitIDs(this.TableID);
        Label_0095:
            data = null;
            data2 = null;
            data3 = null;
            list = new List<GiftRecieveItemData>();
            if ((this.DebugItems == null) || (((int) this.DebugItems.Length) <= 0))
            {
                goto Label_00CF;
            }
            bonusArray = this.DebugItems;
            this.mLoginBonusCount = this.DebugBonusCount;
        Label_00CF:
            if ((this.RemainingCount != null) == null)
            {
                goto Label_0103;
            }
            this.RemainingCount.set_text(&Math.Max(0x1c - this.mLoginBonusCount, 0).ToString());
        Label_0103:
            this.mCurrentWeak = Math.Max(this.mLoginBonusCount - 1, 0) / 7;
            if (((bonusArray == null) || ((this.Item_Normal != null) == null)) || ((this.ItemList != null) == null))
            {
                goto Label_04D6;
            }
            num = 0;
            goto Label_045D;
        Label_0149:
            data4 = new GiftRecieveItemData();
            list.Add(data4);
            str = bonusArray[num].iname;
            num2 = bonusArray[num].num;
            if ((string.IsNullOrEmpty(str) == null) || (bonusArray[num].coin <= 0))
            {
                goto Label_019C;
            }
            str = "$COIN";
            num2 = bonusArray[num].coin;
        Label_019C:
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_01AD;
            }
            goto Label_0457;
        Label_01AD:
            param = manager.MasterParam.GetItemParam(str, 0);
            if (param == null)
            {
                goto Label_01E6;
            }
            data4.Set(str, 1L, param.rare, num2);
            data4.name = param.name;
        Label_01E6:
            param2 = manager.MasterParam.GetConceptCardParam(str);
            if (param2 == null)
            {
                goto Label_0222;
            }
            data4.Set(str, 0x1000L, param2.rare, num2);
            data4.name = param2.name;
        Label_0222:
            if ((param != null) || (param2 != null))
            {
                goto Label_0241;
            }
            DebugUtility.LogError(string.Format("不明な識別子が報酬として設定されています。itemID => {0}", str));
        Label_0241:
            num3 = this.mLoginBonusCount - ((this.IsConfigWindow == null) ? 1 : 0);
            if (num >= num3)
            {
                goto Label_0272;
            }
            events = this.Item_Taken;
            goto Label_027A;
        Label_0272:
            events = this.Item_Normal;
        Label_027A:
            events2 = Object.Instantiate<ListItemEvents>(events);
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            DataSource.Bind<GiftRecieveItemData>(events2.get_gameObject(), data4);
            if ((this.TodayBadge != null) == null)
            {
                goto Label_02FD;
            }
            if (num != (this.mLoginBonusCount - 1))
            {
                goto Label_02FD;
            }
            this.TodayBadge.SetParent(events2.get_transform(), 0);
            this.TodayBadge.set_anchoredPosition(Vector2.get_zero());
            this.TodayBadge.get_gameObject().SetActive(1);
            goto Label_034F;
        Label_02FD:
            if ((this.TommorowBadge != null) == null)
            {
                goto Label_034F;
            }
            if (num != this.mLoginBonusCount)
            {
                goto Label_034F;
            }
            this.TommorowBadge.SetParent(events2.get_transform(), 0);
            this.TommorowBadge.set_anchoredPosition(Vector2.get_zero());
            this.TommorowBadge.get_gameObject().SetActive(1);
        Label_034F:
            if (num >= (this.mLoginBonusCount - 1))
            {
                goto Label_039D;
            }
            transform = events2.get_transform().FindChild(this.CheckName);
            if ((transform != null) == null)
            {
                goto Label_039D;
            }
            animator = transform.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_039D;
            }
            animator.set_enabled(0);
        Label_039D:
            transform2 = this.ItemList.get_transform();
            num4 = num % 7;
            if (this.PositionList == null)
            {
                goto Label_03EE;
            }
            if (((int) this.PositionList.Length) <= num4)
            {
                goto Label_03EE;
            }
            if ((this.PositionList[num4] != null) == null)
            {
                goto Label_03EE;
            }
            transform2 = this.PositionList[num4].get_transform();
        Label_03EE:
            if (num != null)
            {
                goto Label_0402;
            }
            this.DisableFirstDayHiddenOject(events2.get_gameObject());
        Label_0402:
            num5 = num / 7;
            events2.get_transform().SetParent(transform2, 0);
            events2.get_gameObject().SetActive(num5 == this.mCurrentWeak);
            if (num5 != this.mCurrentWeak)
            {
                goto Label_044A;
            }
            events2.GetComponentInChildren<GiftRecieveItem>().UpdateValue();
        Label_044A:
            this.mItems.Add(events2);
        Label_0457:
            num += 1;
        Label_045D:
            if (num < ((int) bonusArray.Length))
            {
                goto Label_0149;
            }
            num6 = this.mLoginBonusCount - 1;
            num7 = this.mLoginBonusCount;
            if (num6 < 0)
            {
                goto Label_0495;
            }
            if (num6 >= ((int) bonusArray.Length))
            {
                goto Label_0495;
            }
            data2 = list[num6];
        Label_0495:
            if (num7 < 0)
            {
                goto Label_04B2;
            }
            if (num7 >= ((int) bonusArray.Length))
            {
                goto Label_04B2;
            }
            data3 = list[num7];
        Label_04B2:
            data = DataSource.FindDataOfClass<GiftRecieveItemData>(this.mItems[this.mItems.Count - 1].get_gameObject(), null);
        Label_04D6:
            if ((this.PopupMessage != null) == null)
            {
                goto Label_04F9;
            }
            this.PopupMessage.set_text(this.GetPopupMessage(data2));
        Label_04F9:
            flag = manager.Player.IsLastLoginBonus(this.TableID);
            if (bonusArray == null)
            {
                goto Label_0523;
            }
            if (this.mLoginBonusCount != ((int) bonusArray.Length))
            {
                goto Label_0523;
            }
            flag = 1;
        Label_0523:
            if ((this.RemainingCounter != null) == null)
            {
                goto Label_0544;
            }
            this.RemainingCounter.SetActive(flag == 0);
        Label_0544:
            if ((this.TodayItem != null) == null)
            {
                goto Label_057F;
            }
            DataSource.Bind<GiftRecieveItemData>(this.TodayItem.get_gameObject(), data2);
            this.TodayItem.get_gameObject().GetComponentInChildren<GiftRecieveItem>().UpdateValue();
        Label_057F:
            if ((this.TommorowItem != null) == null)
            {
                goto Label_05BB;
            }
            DataSource.Bind<GiftRecieveItemData>(this.TommorowItem.get_gameObject(), data3);
            this.TommorowItem.get_gameObject().GetComponentInChildren<GiftRecieveItem>().UpdateValue();
        Label_05BB:
            if ((this.LastItem != null) == null)
            {
                goto Label_05F6;
            }
            DataSource.Bind<GiftRecieveItemData>(this.LastItem.get_gameObject(), data);
            this.LastItem.get_gameObject().GetComponentInChildren<GiftRecieveItem>().UpdateValue();
        Label_05F6:
            if ((this.GainLastItemMessage != null) == null)
            {
                goto Label_0619;
            }
            this.GainLastItemMessage.set_text(this.GetGainLastItemMessage(data));
        Label_0619:
            if (flag == null)
            {
                goto Label_0628;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 12);
        Label_0628:
            if (this.WeakToggle == null)
            {
                goto Label_06F8;
            }
            num8 = 0;
            goto Label_06E6;
        Label_063B:
            storeya = new <Start>c__AnonStorey35A();
            storeya.<>f__this = this;
            num9 = (num8 * 7) + 6;
            if (num9 >= this.mItems.Count)
            {
                goto Label_0697;
            }
            data5 = DataSource.FindDataOfClass<ItemData>(this.mItems[num9].get_gameObject(), null);
            DataSource.Bind<ItemData>(this.WeakToggle[num8].get_gameObject(), data5);
        Label_0697:
            storeya.index = num8;
            this.WeakToggle[num8].set_isOn(num8 == this.mCurrentWeak);
            this.WeakToggle[num8].onValueChanged.AddListener(new UnityAction<bool>(storeya, this.<>m__35B));
            num8 += 1;
        Label_06E6:
            if (num8 < this.WeakToggle.Count)
            {
                goto Label_063B;
            }
        Label_06F8:
            str2 = null;
            if (this.mNotifyUnits == null)
            {
                goto Label_0738;
            }
            if (((int) this.mNotifyUnits.Length) <= 0)
            {
                goto Label_0738;
            }
            random = new Random();
            num10 = random.Next() % ((int) this.mNotifyUnits.Length);
            str2 = this.mNotifyUnits[num10];
        Label_0738:
            if (string.IsNullOrEmpty(this.DebugNotifyUnitID) != null)
            {
                goto Label_0750;
            }
            str2 = this.DebugNotifyUnitID;
        Label_0750:
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_0857;
            }
            this.mCurrentUnit = new UnitData();
            this.mCurrentUnit.Setup(str2, 0, 0, 0, null, 1, 0, 0);
            typeArray1 = new Type[] { typeof(UnitPreview) };
            obj2 = new GameObject("Preview", typeArray1);
            this.mCurrentPreview = obj2.GetComponent<UnitPreview>();
            this.mCurrentPreview.DefaultLayer = GameUtility.LayerHidden;
            this.mCurrentPreview.SetupUnit(this.mCurrentUnit, -1);
            obj2.get_transform().SetParent(this.PreviewParent, 0);
            if ((this.PreviewCamera != null) == null)
            {
                goto Label_0846;
            }
            if ((this.PreviewImage != null) == null)
            {
                goto Label_0846;
            }
            num11 = Mathf.FloorToInt(((float) Screen.get_height()) * 0.8f);
            this.mPreviewUnitRT = RenderTexture.GetTemporary(num11, num11, 0x10, 7);
            this.PreviewCamera.set_targetTexture(this.mPreviewUnitRT);
            this.PreviewImage.set_texture(this.mPreviewUnitRT);
        Label_0846:
            GameUtility.SetLayer(this.mCurrentPreview, GameUtility.LayerCH, 1);
        Label_0857:
            base.StartCoroutine(this.WaitLoadAsync());
            return;
        }

        [DebuggerHidden]
        private IEnumerator WaitForDestroy()
        {
            <WaitForDestroy>c__Iterator123 iterator;
            iterator = new <WaitForDestroy>c__Iterator123();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator WaitLoadAsync()
        {
            <WaitLoadAsync>c__Iterator121 iterator;
            iterator = new <WaitLoadAsync>c__Iterator121();
            iterator.<>f__this = this;
            return iterator;
        }

        [CompilerGenerated]
        private sealed class <DelayPopupMessage>c__Iterator122 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal LoginBonusWindow28days <>f__this;

            public <DelayPopupMessage>c__Iterator122()
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
                        goto Label_0043;
                }
                goto Label_0097;
            Label_0021:
                this.$current = new WaitForSeconds(this.<>f__this.MessageDelay);
                this.$PC = 1;
                goto Label_0099;
            Label_0043:
                this.<>f__this.mMessageWindow = Object.Instantiate<LoginBonusWindow>(this.<>f__this.Message);
                this.<>f__this.mMessageWindow.TableID = this.<>f__this.TableID;
                this.<>f__this.StartCoroutine(this.<>f__this.WaitForDestroy());
                this.$PC = -1;
            Label_0097:
                return 0;
            Label_0099:
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
        private sealed class <OnWeakSelect>c__AnonStorey35B
        {
            internal GameObject go;

            public <OnWeakSelect>c__AnonStorey35B()
            {
                base..ctor();
                return;
            }

            internal bool <>m__35C(Toggle p)
            {
                return (p.get_gameObject() == this.go);
            }
        }

        [CompilerGenerated]
        private sealed class <Start>c__AnonStorey35A
        {
            internal int index;
            internal LoginBonusWindow28days <>f__this;

            public <Start>c__AnonStorey35A()
            {
                base..ctor();
                return;
            }

            internal void <>m__35B(bool value)
            {
                if (value == null)
                {
                    goto Label_002C;
                }
                this.<>f__this.OnWeakSelect(this.<>f__this.WeakToggle[this.index].get_gameObject());
            Label_002C:
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <WaitForDestroy>c__Iterator123 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal LoginBonusWindow28days <>f__this;

            public <WaitForDestroy>c__Iterator123()
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
                        goto Label_0039;
                }
                goto Label_0063;
            Label_0021:
                goto Label_0039;
            Label_0026:
                this.$current = null;
                this.$PC = 1;
                goto Label_0065;
            Label_0039:
                if ((this.<>f__this.mMessageWindow != null) != null)
                {
                    goto Label_0026;
                }
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 0x10);
                this.$PC = -1;
            Label_0063:
                return 0;
            Label_0065:
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
        private sealed class <WaitLoadAsync>c__Iterator121 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal Transform <head>__0;
            internal int $PC;
            internal object $current;
            internal LoginBonusWindow28days <>f__this;

            public <WaitLoadAsync>c__Iterator121()
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
                        goto Label_003C;

                    case 2:
                        goto Label_0054;

                    case 3:
                        goto Label_009C;
                }
                goto Label_0177;
            Label_0029:
                this.$current = null;
                this.$PC = 1;
                goto Label_0179;
            Label_003C:
                goto Label_0054;
            Label_0041:
                this.$current = null;
                this.$PC = 2;
                goto Label_0179;
            Label_0054:
                if (AssetManager.IsLoading != null)
                {
                    goto Label_0041;
                }
                if (this.<>f__this.mCurrentUnit == null)
                {
                    goto Label_0163;
                }
                if ((this.<>f__this.mCurrentPreview != null) == null)
                {
                    goto Label_0163;
                }
                goto Label_009C;
            Label_0089:
                this.$current = null;
                this.$PC = 3;
                goto Label_0179;
            Label_009C:
                if (this.<>f__this.mCurrentPreview.IsLoading != null)
                {
                    goto Label_0089;
                }
                if ((this.<>f__this.PreviewCamera != null) == null)
                {
                    goto Label_014D;
                }
                this.<head>__0 = this.<>f__this.mCurrentPreview.GetHeadPosition();
                if ((this.<head>__0 != null) == null)
                {
                    goto Label_014D;
                }
                this.<>f__this.PreviewCamera.get_transform().set_position(this.<head>__0.get_position() + -(Vector3.get_forward() * this.<>f__this.PreviewCameraDistance));
                this.<>f__this.PreviewCamera.get_transform().LookAt(this.<head>__0.get_position());
            Label_014D:
                GameUtility.SetLayer(this.<>f__this.mCurrentPreview, GameUtility.LayerCH, 1);
            Label_0163:
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 10);
                this.$PC = -1;
            Label_0177:
                return 0;
            Label_0179:
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

