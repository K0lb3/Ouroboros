namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(100, "Output", 1, 100), Pin(1, "Request", 0, 0)]
    public class RecentPartyList : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private RecentPartyPanel PartyPanelTemplate;
        [SerializeField]
        private GameObject PartyPanelHolder;
        [SerializeField]
        private Text ErrorText;
        [SerializeField]
        private UnityEngine.UI.ScrollRect ScrollRect;
        [SerializeField]
        private SRPG_Button PrevButton;
        [SerializeField]
        private SRPG_Button NextButton;
        [SerializeField]
        private GameObject PageInfo;
        [SerializeField]
        private Text CurrentPage;
        [SerializeField]
        private Text MaxPage;
        [SerializeField]
        private GameObject CheckBox;
        private QuestParam mCurrentQuest;
        private JSON_List[] mLastRecords;
        private List<UnitData[]> allParties;
        private List<SupportData> allHelpUnits;
        private List<int[]> allAchieves;
        private List<ItemData[]> allUsedItems;
        private List<RecentPartyPanel> allTeamPanel;
        private bool mOwnedUnitOnly;
        private int mCurrentPage;
        private int mMaxPage;
        private int mUnitId;
        [CompilerGenerated]
        private static Func<RecentPartyPanel, bool> <>f__am$cache15;

        public RecentPartyList()
        {
            this.allParties = new List<UnitData[]>();
            this.allHelpUnits = new List<SupportData>();
            this.allAchieves = new List<int[]>();
            this.allUsedItems = new List<ItemData[]>();
            this.allTeamPanel = new List<RecentPartyPanel>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <Refresh>m__3EC(RecentPartyPanel panel)
        {
            return (panel.get_gameObject().get_activeSelf() == 0);
        }

        public void Activated(int pinID)
        {
            this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            this.mCurrentPage = 1;
            this.Connect();
            return;
        }

        private void Connect()
        {
            Network.RequestAPI(new ReqBtlComRecord(this.mCurrentQuest.iname, this.mCurrentPage, this.mUnitId, new SRPG.Network.ResponseCallback(this.ResponseCallback)), 0);
            return;
        }

        private unsafe RecentPartyPanel CreatePartyPanel(UnitData[] party, SupportData support, JSON_List record, SRPG_Button.ButtonClickEvent buttonClickCallback)
        {
            RecentPartyPanel panel;
            int num;
            panel = Object.Instantiate<GameObject>(this.PartyPanelTemplate.get_gameObject()).GetComponent<RecentPartyPanel>();
            panel.SetPartyInfo(party, support, this.mCurrentQuest);
            panel.SetUnitIconPressedCallback(buttonClickCallback);
            panel.SetUserName(record.detail.my.name);
            panel.SetUserRank(&record.detail.my.lv.ToString());
            panel.SetClearDate(this.GetClearedTime(record.created_at));
            num = 0;
            goto Label_008F;
        Label_0076:
            panel.SetConditionStarActive(num, (record.achieved[num] == 0) == 0);
            num += 1;
        Label_008F:
            if (num < ((int) record.achieved.Length))
            {
                goto Label_0076;
            }
            if (this.mCurrentQuest.type != 7)
            {
                goto Label_00B6;
            }
            panel.SetConditionItemActive(2, 0);
        Label_00B6:
            panel.get_gameObject().SetActive(1);
            return panel;
        }

        private void DisableUnnecessaryUIOnError()
        {
            if ((this.ScrollRect != null) == null)
            {
                goto Label_0022;
            }
            this.ScrollRect.get_gameObject().SetActive(0);
        Label_0022:
            if ((this.PageInfo != null) == null)
            {
                goto Label_003F;
            }
            this.PageInfo.SetActive(0);
        Label_003F:
            if ((this.PrevButton != null) == null)
            {
                goto Label_0061;
            }
            this.PrevButton.get_gameObject().SetActive(0);
        Label_0061:
            if ((this.NextButton != null) == null)
            {
                goto Label_0083;
            }
            this.NextButton.get_gameObject().SetActive(0);
        Label_0083:
            if ((this.CheckBox != null) == null)
            {
                goto Label_00A5;
            }
            this.CheckBox.get_gameObject().SetActive(0);
        Label_00A5:
            return;
        }

        private void EnableErrorText(string errorMessage)
        {
            if ((this.ErrorText != null) == null)
            {
                goto Label_002E;
            }
            this.ErrorText.get_gameObject().SetActive(1);
            this.ErrorText.set_text(errorMessage);
        Label_002E:
            return;
        }

        private unsafe string GetClearedTime(string iso8601String)
        {
            CultureInfo info;
            DateTime time;
            info = new CultureInfo("ja-JP");
            if (DateTime.TryParseExact(iso8601String, TimeManager.ISO_8601_FORMAT, info, 0, &time) == null)
            {
                goto Label_002C;
            }
            return &time.ToString("yyyy/M/d");
        Label_002C:
            return string.Empty;
        }

        private unsafe void Initialize()
        {
            RecentPartyPanel panel;
            List<RecentPartyPanel>.Enumerator enumerator;
            this.allParties.Clear();
            this.allHelpUnits.Clear();
            this.allAchieves.Clear();
            enumerator = this.allTeamPanel.GetEnumerator();
        Label_002D:
            try
            {
                goto Label_0045;
            Label_0032:
                panel = &enumerator.Current;
                Object.Destroy(panel.get_gameObject());
            Label_0045:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0032;
                }
                goto Label_0062;
            }
            finally
            {
            Label_0056:
                ((List<RecentPartyPanel>.Enumerator) enumerator).Dispose();
            }
        Label_0062:
            this.allTeamPanel.Clear();
            if ((this.PartyPanelTemplate != null) == null)
            {
                goto Label_008F;
            }
            this.PartyPanelTemplate.get_gameObject().SetActive(0);
        Label_008F:
            if ((this.ErrorText != null) == null)
            {
                goto Label_00B1;
            }
            this.ErrorText.get_gameObject().SetActive(0);
        Label_00B1:
            this.ScrollRect.set_verticalNormalizedPosition(1f);
            this.ScrollRect.set_horizontalNormalizedPosition(1f);
            return;
        }

        private void LoadAllParties(JSON_List[] winRecords)
        {
            int num;
            JSON_List list;
            JSON_List[] listArray;
            int num2;
            SRPG_Button.ButtonClickEvent event2;
            UnitData[] dataArray;
            SupportData data;
            ItemData[] dataArray2;
            RecentPartyPanel panel;
            SRPG_Button button;
            <LoadAllParties>c__AnonStorey39A storeya;
            num = 0;
            listArray = winRecords;
            num2 = 0;
            goto Label_012D;
        Label_000B:
            list = listArray[num2];
            storeya = new <LoadAllParties>c__AnonStorey39A();
            storeya.<>f__this = this;
            storeya.index = num;
            event2 = new SRPG_Button.ButtonClickEvent(storeya.<>m__3ED);
            if (list.id <= this.mUnitId)
            {
                goto Label_0052;
            }
            this.mUnitId = list.id;
        Label_0052:
            dataArray = this.LoadParty(list.detail.my.units);
            data = this.LoadHelpUnit(list.detail.help);
            dataArray2 = this.LoadUsedItems(list.detail.items);
            this.allParties.Add(Enumerable.ToArray<UnitData>(dataArray));
            this.allHelpUnits.Add(data);
            this.allAchieves.Add(list.achieved);
            this.allUsedItems.Add(dataArray2);
            panel = this.CreatePartyPanel(dataArray, data, list, event2);
            this.allTeamPanel.Add(panel);
            panel.get_transform().SetParent(this.PartyPanelHolder.get_gameObject().get_transform(), 0);
            button = panel.GetComponent<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_0125;
            }
            button.AddListener(event2);
        Label_0125:
            num += 1;
            num2 += 1;
        Label_012D:
            if (num2 < ((int) listArray.Length))
            {
                goto Label_000B;
            }
            return;
        }

        private SupportData LoadHelpUnit(Json_Support json)
        {
            SupportData data;
            if (json != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            data = new SupportData();
            data.Deserialize(json);
            return data;
        }

        private UnitData[] LoadParty(IEnumerable<Json_Unit> jsonUnit)
        {
            List<UnitData> list;
            Json_Unit unit;
            IEnumerator<Json_Unit> enumerator;
            UnitData data;
            list = new List<UnitData>();
            enumerator = jsonUnit.GetEnumerator();
        Label_000D:
            try
            {
                goto Label_004F;
            Label_0012:
                unit = enumerator.Current;
                data = new UnitData();
                if (unit == null)
                {
                    goto Label_0035;
                }
                if (string.IsNullOrEmpty(unit.iname) == null)
                {
                    goto Label_0041;
                }
            Label_0035:
                list.Add(null);
                goto Label_004F;
            Label_0041:
                data.Deserialize(unit);
                list.Add(data);
            Label_004F:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0012;
                }
                goto Label_006A;
            }
            finally
            {
            Label_005F:
                if (enumerator != null)
                {
                    goto Label_0063;
                }
            Label_0063:
                enumerator.Dispose();
            }
        Label_006A:
            return list.ToArray();
        }

        private ItemData[] LoadUsedItems(JSON_Item[] jsonItem)
        {
            List<ItemData> list;
            JSON_Item item;
            JSON_Item[] itemArray;
            int num;
            ItemData data;
            if (jsonItem != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            list = new List<ItemData>();
            itemArray = jsonItem;
            num = 0;
            goto Label_0044;
        Label_0017:
            item = itemArray[num];
            data = new ItemData();
            data.Setup(0L, item.iname, item.num);
            list.Add(data);
            num += 1;
        Label_0044:
            if (num < ((int) itemArray.Length))
            {
                goto Label_0017;
            }
            return list.ToArray();
        }

        private void OnButtonClick(int index)
        {
            GlobalVars.UserSelectionPartyData data;
            if (index < 0)
            {
                goto Label_0029;
            }
            if (index >= this.allParties.Count)
            {
                goto Label_0029;
            }
            if (index < this.allAchieves.Count)
            {
                goto Label_002A;
            }
        Label_0029:
            return;
        Label_002A:
            data = new GlobalVars.UserSelectionPartyData();
            data.unitData = this.allParties[index];
            data.supportData = this.allHelpUnits[index];
            data.achievements = this.allAchieves[index];
            data.usedItems = this.allUsedItems[index];
            GlobalVars.UserSelectionPartyDataInfo = data;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        public void OnNextButtonPressed()
        {
            if ((this.mCurrentPage + 1) > this.mMaxPage)
            {
                goto Label_0027;
            }
            this.mCurrentPage += 1;
            this.Connect();
        Label_0027:
            return;
        }

        public void OnPrevButtonPressed()
        {
            if ((this.mCurrentPage - 1) < 1)
            {
                goto Label_0022;
            }
            this.mCurrentPage -= 1;
            this.Connect();
        Label_0022:
            return;
        }

        public void OnToggleDisabled()
        {
            this.mOwnedUnitOnly = 0;
            this.Refresh();
            return;
        }

        public void OnToggleEnabled()
        {
            this.mOwnedUnitOnly = 1;
            this.Refresh();
            return;
        }

        private unsafe void Refresh()
        {
            bool flag;
            PlayerData data;
            int num;
            UnitData data2;
            UnitData[] dataArray;
            int num2;
            this.Initialize();
            if (((this.mLastRecords == null) == 0) != null)
            {
                goto Label_0030;
            }
            this.EnableErrorText(LocalizedText.Get("sys.PARTYEDITOR_RECENT_CLEARED_PARTY_NOT_FOUND"));
            this.DisableUnnecessaryUIOnError();
            return;
        Label_0030:
            this.LoadAllParties(this.mLastRecords);
            if (this.mOwnedUnitOnly == null)
            {
                goto Label_00E4;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            num = 0;
            goto Label_00C2;
        Label_0059:
            dataArray = this.allParties[num];
            num2 = 0;
            goto Label_00B3;
        Label_006F:
            data2 = dataArray[num2];
            if (data2 != null)
            {
                goto Label_0080;
            }
            goto Label_00AD;
        Label_0080:
            if (data.FindUnitDataByUnitID(data2.UnitParam.iname) != null)
            {
                goto Label_00AD;
            }
            this.allTeamPanel[num].get_gameObject().SetActive(0);
        Label_00AD:
            num2 += 1;
        Label_00B3:
            if (num2 < ((int) dataArray.Length))
            {
                goto Label_006F;
            }
            num += 1;
        Label_00C2:
            if (num >= this.allParties.Count)
            {
                goto Label_00E4;
            }
            if (num < this.allTeamPanel.Count)
            {
                goto Label_0059;
            }
        Label_00E4:
            if (<>f__am$cache15 != null)
            {
                goto Label_0102;
            }
            <>f__am$cache15 = new Func<RecentPartyPanel, bool>(RecentPartyList.<Refresh>m__3EC);
        Label_0102:
            if (Enumerable.All<RecentPartyPanel>(this.allTeamPanel, <>f__am$cache15) == null)
            {
                goto Label_0121;
            }
            this.EnableErrorText(LocalizedText.Get("sys.PARTYEDITOR_RECENT_CLEARED_PARTY_NOT_FOUND_OWNED_UNIT"));
        Label_0121:
            this.SetActiveUICoponent();
            this.CurrentPage.set_text(&this.mCurrentPage.ToString());
            this.MaxPage.set_text(&this.mMaxPage.ToString());
            return;
        }

        private unsafe void ResponseCallback(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_Body> response;
            Network.EErrCode code;
            if (FlowNode_Network.HasCommonError(www) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (Network.IsError == null)
            {
                goto Label_005E;
            }
            code = Network.ErrCode;
            if (code == 0xcc)
            {
                goto Label_0037;
            }
            if (code == 0xced)
            {
                goto Label_0037;
            }
            goto Label_0058;
        Label_0037:
            Network.RemoveAPI();
            Network.ResetError();
            this.EnableErrorText(LocalizedText.Get("sys.PARTYEDITOR_RECENT_CLEARED_PARTY_ERROR_UPLOAD_LIMIT_2"));
            this.DisableUnnecessaryUIOnError();
            return;
        Label_0058:
            FlowNode_Network.Retry();
            return;
        Label_005E:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_Body>>(&www.text);
            this.mLastRecords = response.body.list;
            this.mMaxPage = response.body.option.totalPage;
            Network.RemoveAPI();
            this.Refresh();
            return;
        }

        private void SetActiveUICoponent()
        {
            if ((this.ScrollRect != null) == null)
            {
                goto Label_0022;
            }
            this.ScrollRect.get_gameObject().SetActive(1);
        Label_0022:
            if ((this.PageInfo != null) == null)
            {
                goto Label_003F;
            }
            this.PageInfo.SetActive(1);
        Label_003F:
            if ((this.PrevButton != null) == null)
            {
                goto Label_0075;
            }
            this.PrevButton.get_gameObject().SetActive(1);
            this.PrevButton.set_interactable(this.mCurrentPage > 1);
        Label_0075:
            if ((this.NextButton != null) == null)
            {
                goto Label_00B0;
            }
            this.NextButton.get_gameObject().SetActive(1);
            this.NextButton.set_interactable(this.mCurrentPage < this.mMaxPage);
        Label_00B0:
            if ((this.CheckBox != null) == null)
            {
                goto Label_00D2;
            }
            this.CheckBox.get_gameObject().SetActive(1);
        Label_00D2:
            return;
        }

        [CompilerGenerated]
        private sealed class <LoadAllParties>c__AnonStorey39A
        {
            internal int index;
            internal RecentPartyList <>f__this;

            public <LoadAllParties>c__AnonStorey39A()
            {
                base..ctor();
                return;
            }

            internal void <>m__3ED(SRPG_Button b)
            {
                this.<>f__this.OnButtonClick(this.index);
                return;
            }
        }

        public class JSON_Body
        {
            public RecentPartyList.JSON_List[] list;
            public RecentPartyList.JSON_Option option;

            public JSON_Body()
            {
                base..ctor();
                return;
            }
        }

        public class JSON_Detail
        {
            public RecentPartyList.JSON_My my;
            public Json_Support help;
            public RecentPartyList.JSON_Item[] items;

            public JSON_Detail()
            {
                base..ctor();
                return;
            }
        }

        public class JSON_Item
        {
            public string iname;
            public int num;

            public JSON_Item()
            {
                base..ctor();
                return;
            }
        }

        public class JSON_List
        {
            public int id;
            public int[] achieved;
            public string created_at;
            public RecentPartyList.JSON_Detail detail;

            public JSON_List()
            {
                base..ctor();
                return;
            }
        }

        public class JSON_My
        {
            public int lv;
            public string name;
            public Json_Unit[] units;

            public JSON_My()
            {
                base..ctor();
                return;
            }
        }

        public class JSON_Option
        {
            public int totalPage;

            public JSON_Option()
            {
                base..ctor();
                return;
            }
        }
    }
}

