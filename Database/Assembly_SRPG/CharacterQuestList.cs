namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;

    [Pin(0, "Refresh(Chara)", 0, 0), Pin(1, "Refresh(Collabo)", 0, 1), Pin(10, "Refreshed", 1, 11), Pin(11, "Selected(Chara)", 1, 12), Pin(12, "Selected(Collabo)", 1, 13), Pin(13, "Sort Change", 1, 14)]
    public class CharacterQuestList : SRPG_FixedList, IFlowInterface, ISortableList
    {
        private const int PIN_ID_REFRESH_CHARA = 0;
        private const int PIN_ID_REFRESH_COLLABO = 1;
        private const int PIN_ID_REFRESHED = 10;
        private const int PIN_ID_CHARA_SELECTED = 11;
        private const int PIN_ID_COLLABO_REFRESHED = 12;
        private const int PIN_ID_SORT_CHANGE = 13;
        [SerializeField]
        private GameObject ItemTemplate;
        [SerializeField]
        private GameObject FlowRoot;
        [SerializeField]
        private SortMenu mSortMenu;
        [SerializeField]
        private SortMenuButton mSortMenuButton;
        private FilterMethod mFilterMethod;

        public CharacterQuestList()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private bool <RefreshCharaData>m__2AF(CharacterQuestData quest)
        {
            return this.mFilterMethod(quest);
        }

        [CompilerGenerated]
        private bool <RefreshCollaboData>m__2B0(CharacterQuestData quest)
        {
            return this.mFilterMethod(quest);
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_001D;
            }
            this.RefreshFilter();
            this.RefreshCharaData();
            base.Refresh();
            goto Label_0036;
        Label_001D:
            if (pinID != 1)
            {
                goto Label_0036;
            }
            this.RefreshFilter();
            this.RefreshCollaboData();
            base.Refresh();
        Label_0036:
            return;
        }

        protected override GameObject CreateItem()
        {
            GameObject obj2;
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0026;
            }
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            obj2.SetActive(1);
            return obj2;
        Label_0026:
            return null;
        }

        private static unsafe string FilterValue(EFilter filter)
        {
            int num;
            num = filter;
            return &num.ToString();
        }

        private List<CharacterQuestDataChunk> GetCharacterQuestList()
        {
            GameManager manager;
            QuestParam[] paramArray;
            List<CharacterQuestDataChunk> list;
            QuestParam[] paramArray2;
            int num;
            CharacterQuestDataChunk chunk;
            <GetCharacterQuestList>c__AnonStorey311 storey;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_0014;
            }
            return null;
        Label_0014:
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            list = new List<CharacterQuestDataChunk>();
            storey = new <GetCharacterQuestList>c__AnonStorey311();
            paramArray2 = paramArray;
            num = 0;
            goto Label_0132;
        Label_003B:
            storey.qp = paramArray2[num];
            if (storey.qp.type != 6)
            {
                goto Label_012C;
            }
            if (string.IsNullOrEmpty(storey.qp.ChapterID) == null)
            {
                goto Label_008E;
            }
            DebugUtility.LogError("ChapterIDが設定されていません:QuestParam.iname = " + storey.qp.iname);
            goto Label_012C;
        Label_008E:
            if (manager.MasterParam.ContainsUnitID(storey.qp.ChapterID) != null)
            {
                goto Label_00AF;
            }
            goto Label_012C;
        Label_00AF:
            chunk = list.Find(new Predicate<CharacterQuestDataChunk>(storey.<>m__2AE));
            if (chunk != null)
            {
                goto Label_00DA;
            }
            chunk = new CharacterQuestDataChunk();
            list.Add(chunk);
        Label_00DA:
            chunk.questParams.Add(storey.qp);
            chunk.areaName = storey.qp.ChapterID;
            chunk.SetUnitNameFromChapterID(storey.qp.ChapterID);
            chunk.unitParam = manager.MasterParam.GetUnitParam(chunk.unitName);
        Label_012C:
            num += 1;
        Label_0132:
            if (num < ((int) paramArray2.Length))
            {
                goto Label_003B;
            }
            return list;
        }

        public static unsafe List<KeyValuePair<QuestParam, bool>> GetCharacterQuests(UnitData unitData)
        {
            List<KeyValuePair<QuestParam, bool>> list;
            List<QuestParam> list2;
            UnitData.CharacterQuestParam[] paramArray;
            int num;
            bool flag;
            bool flag2;
            KeyValuePair<QuestParam, bool> pair;
            KeyValuePair<QuestParam, bool> pair2;
            list = new List<KeyValuePair<QuestParam, bool>>();
            list2 = unitData.FindCondQuests();
            paramArray = unitData.GetCharaEpisodeList();
            num = 0;
            goto Label_0098;
        Label_001B:
            flag = list2[num].state == 2;
            if ((((paramArray[num] == null) || (paramArray[num].IsAvailable == null)) ? 0 : unitData.IsChQuestParentUnlocked(list2[num])) != null)
            {
                goto Label_0061;
            }
            if (flag == null)
            {
                goto Label_007D;
            }
        Label_0061:
            &pair = new KeyValuePair<QuestParam, bool>(list2[num], 1);
            list.Add(pair);
            goto Label_0094;
        Label_007D:
            &pair2 = new KeyValuePair<QuestParam, bool>(list2[num], 0);
            list.Add(pair2);
        Label_0094:
            num += 1;
        Label_0098:
            if (num < list2.Count)
            {
                goto Label_001B;
            }
            return list;
        }

        public static unsafe List<KeyValuePair<QuestParam, bool>> GetCollaboSkillQuests(UnitData unitData1, UnitData unitData2)
        {
            List<KeyValuePair<QuestParam, bool>> list;
            QuestParam[] paramArray;
            bool flag;
            bool flag2;
            bool flag3;
            string str;
            bool flag4;
            bool flag5;
            KeyValuePair<QuestParam, bool> pair;
            KeyValuePair<QuestParam, bool> pair2;
            <GetCollaboSkillQuests>c__AnonStorey30F storeyf;
            <GetCollaboSkillQuests>c__AnonStorey310 storey;
            storeyf = new <GetCollaboSkillQuests>c__AnonStorey30F();
            list = new List<KeyValuePair<QuestParam, bool>>();
            storeyf.questList = CollaboSkillQuestList.GetCollaboSkillQuests(unitData1, unitData2);
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            storey = new <GetCollaboSkillQuests>c__AnonStorey310();
            storey.<>f__ref$783 = storeyf;
            storey.i = 0;
            goto Label_013D;
        Label_0048:
            flag = storeyf.questList[storey.i].IsDateUnlock(-1L);
            flag2 = (Array.Find<QuestParam>(paramArray, new Predicate<QuestParam>(storey.<>m__2AD)) == null) == 0;
            flag3 = storeyf.questList[storey.i].state == 2;
            str = string.Empty;
            flag4 = storeyf.questList[storey.i].IsEntryQuestConditionCh(unitData1, &str);
            flag5 = ((flag == null) || (flag2 == null)) ? 0 : (flag3 == 0);
            if (flag4 == null)
            {
                goto Label_010A;
            }
            if (flag5 == null)
            {
                goto Label_010A;
            }
            &pair = new KeyValuePair<QuestParam, bool>(storeyf.questList[storey.i], 1);
            list.Add(pair);
            goto Label_012D;
        Label_010A:
            &pair2 = new KeyValuePair<QuestParam, bool>(storeyf.questList[storey.i], 0);
            list.Add(pair2);
        Label_012D:
            storey.i += 1;
        Label_013D:
            if (storey.i < storeyf.questList.Count)
            {
                goto Label_0048;
            }
            return list;
        }

        private bool OnFilter_ALL(CharacterQuestData questData)
        {
            if (questData != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            return 1;
        }

        private bool OnFilter_Complete(CharacterQuestData questData)
        {
            if (questData != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            return (questData.Status == 3);
        }

        private bool OnFilter_Lock(CharacterQuestData questData)
        {
            if (questData != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            return (questData.Status == 2);
        }

        private bool OnFilter_Unlock(CharacterQuestData questData)
        {
            if (questData != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            return ((questData.Status == null) ? 1 : (questData.Status == 1));
        }

        protected override void OnItemSelect(GameObject go)
        {
            CharacterQuestData data;
            CollaboSkillParam.Pair pair;
            data = DataSource.FindDataOfClass<CharacterQuestData>(go, null);
            if (data == null)
            {
                goto Label_0091;
            }
            if (data.questType != null)
            {
                goto Label_0066;
            }
            if (data.HasUnit == null)
            {
                goto Label_0091;
            }
            if (data.IsLock != null)
            {
                goto Label_0091;
            }
            GlobalVars.SelectedUnitUniqueID.Set(data.unitData1.UniqueID);
            GlobalVars.PreBattleUnitUniqueID.Set(data.unitData1.UniqueID);
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
            goto Label_0091;
        Label_0066:
            if (data.HasPairUnit == null)
            {
                goto Label_0091;
            }
            if (data.IsLock != null)
            {
                goto Label_0091;
            }
            GlobalVars.SelectedCollaboSkillPair = data.GetPairUnit();
            FlowNode_GameObject.ActivateOutputLinks(this, 12);
        Label_0091:
            return;
        }

        public void OnNextPage()
        {
            if (this.IsActive() == null)
            {
                goto Label_0011;
            }
            this.GotoNextPage();
        Label_0011:
            return;
        }

        public void OnPreviousPage()
        {
            if (this.IsActive() == null)
            {
                goto Label_0011;
            }
            this.GotoPreviousPage();
        Label_0011:
            return;
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            base.CancelRefresh();
            return;
        }

        public void RefreshCharaData()
        {
            GameManager manager;
            List<CharacterQuestData> list;
            List<CharacterQuestDataChunk> list2;
            int num;
            CharacterQuestDataChunk chunk;
            CharacterQuestData data;
            UnitData data2;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            list = new List<CharacterQuestData>();
            list2 = this.GetCharacterQuestList();
            list2.Reverse();
            num = 0;
            goto Label_00A0;
        Label_002D:
            chunk = list2[num];
            if (chunk.unitParam != null)
            {
                goto Label_0047;
            }
            goto Label_009C;
        Label_0047:
            data = new CharacterQuestData();
            data.questType = 0;
            data2 = manager.Player.FindUnitDataByUniqueParam(chunk.unitParam);
            if (data2 != null)
            {
                goto Label_0084;
            }
            data.unitParam1 = chunk.unitParam;
            goto Label_008D;
        Label_0084:
            data.unitData1 = data2;
        Label_008D:
            data.UpdateStatus();
            list.Add(data);
        Label_009C:
            num += 1;
        Label_00A0:
            if (num < list2.Count)
            {
                goto Label_002D;
            }
            if (this.mFilterMethod != null)
            {
                goto Label_00C6;
            }
            Debug.Log("mFilterMethod == null");
            goto Label_00D9;
        Label_00C6:
            list = list.FindAll(new Predicate<CharacterQuestData>(this.<RefreshCharaData>m__2AF));
        Label_00D9:
            this.SetData(list.ToArray(), typeof(CharacterQuestData));
            return;
        }

        public void RefreshCollaboData()
        {
            GameManager manager;
            List<CharacterQuestData> list;
            List<CollaboSkillParam.Pair> list2;
            int num;
            UnitData data;
            UnitData data2;
            CharacterQuestData data3;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            list = new List<CharacterQuestData>();
            list2 = CollaboSkillParam.GetPairLists();
            if (list2 == null)
            {
                goto Label_00DC;
            }
            num = 0;
            goto Label_00D0;
        Label_002C:
            data = manager.Player.FindUnitDataByUniqueParam(list2[num].UnitParam1);
            data2 = manager.Player.FindUnitDataByUniqueParam(list2[num].UnitParam2);
            data3 = new CharacterQuestData();
            data3.questType = 1;
            if (data != null)
            {
                goto Label_008C;
            }
            data3.unitParam1 = list2[num].UnitParam1;
            goto Label_0095;
        Label_008C:
            data3.unitData1 = data;
        Label_0095:
            if (data2 != null)
            {
                goto Label_00B4;
            }
            data3.unitParam2 = list2[num].UnitParam2;
            goto Label_00BD;
        Label_00B4:
            data3.unitData2 = data2;
        Label_00BD:
            data3.UpdateStatus();
            list.Add(data3);
            num += 1;
        Label_00D0:
            if (num < list2.Count)
            {
                goto Label_002C;
            }
        Label_00DC:
            if (this.mFilterMethod != null)
            {
                goto Label_00F6;
            }
            Debug.Log("mFilterMethod == null");
            goto Label_0109;
        Label_00F6:
            list = list.FindAll(new Predicate<CharacterQuestData>(this.<RefreshCollaboData>m__2B0));
        Label_0109:
            this.SetData(list.ToArray(), typeof(CharacterQuestData));
            return;
        }

        private void RefreshFilter()
        {
            string[] strArray;
            if ((this.mSortMenu != null) == null)
            {
                goto Label_0030;
            }
            this.mSortMenuButton.ForceReloadFilter();
            strArray = this.mSortMenu.GetFilters(0);
            this.SetSortMethod(strArray);
        Label_0030:
            return;
        }

        protected override void RefreshItems()
        {
            base.RefreshItems();
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            return;
        }

        public void SetSortMethod(string[] filters)
        {
            int num;
            if (filters == null)
            {
                goto Label_00AA;
            }
            num = 0;
            goto Label_00A1;
        Label_000D:
            if ((filters[num] == FilterValue(1)) == null)
            {
                goto Label_0037;
            }
            this.mFilterMethod = new FilterMethod(this.OnFilter_Unlock);
            goto Label_009D;
        Label_0037:
            if ((filters[num] == FilterValue(2)) == null)
            {
                goto Label_0061;
            }
            this.mFilterMethod = new FilterMethod(this.OnFilter_Lock);
            goto Label_009D;
        Label_0061:
            if ((filters[num] == FilterValue(3)) == null)
            {
                goto Label_008B;
            }
            this.mFilterMethod = new FilterMethod(this.OnFilter_Complete);
            goto Label_009D;
        Label_008B:
            this.mFilterMethod = new FilterMethod(this.OnFilter_ALL);
        Label_009D:
            num += 1;
        Label_00A1:
            if (num < ((int) filters.Length))
            {
                goto Label_000D;
            }
        Label_00AA:
            return;
        }

        public void SetSortMethod(string method, bool ascending, string[] filters)
        {
            this.SetSortMethod(filters);
            FlowNode_GameObject.ActivateOutputLinks(this, 13);
            return;
        }

        protected override void Start()
        {
            base.Start();
            base.RegisterNextButtonCallBack(new UnityAction(this, this.OnNextPage));
            base.RegisterPrevButtonCallBack(new UnityAction(this, this.OnPreviousPage));
            return;
        }

        [CompilerGenerated]
        private sealed class <GetCharacterQuestList>c__AnonStorey311
        {
            internal QuestParam qp;

            public <GetCharacterQuestList>c__AnonStorey311()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2AE(CharacterQuestDataChunk cqdc)
            {
                return (cqdc.areaName == this.qp.ChapterID);
            }
        }

        [CompilerGenerated]
        private sealed class <GetCollaboSkillQuests>c__AnonStorey30F
        {
            internal List<QuestParam> questList;

            public <GetCollaboSkillQuests>c__AnonStorey30F()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <GetCollaboSkillQuests>c__AnonStorey310
        {
            internal int i;
            internal CharacterQuestList.<GetCollaboSkillQuests>c__AnonStorey30F <>f__ref$783;

            public <GetCollaboSkillQuests>c__AnonStorey310()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2AD(QuestParam p)
            {
                return (p == this.<>f__ref$783.questList[this.i]);
            }
        }

        public enum EFilter
        {
            ALL,
            Unlock,
            Lock,
            Complete
        }

        private delegate bool FilterMethod(CharacterQuestData questData);
    }
}

