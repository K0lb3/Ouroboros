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
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0x66, "閉じる：完了", 1, 0x66), Pin(0, "表示を更新", 0, 0), Pin(2, "閉じる", 0, 2), Pin(100, "ユニットが進化した", 1, 100), Pin(0x65, "入手クエストが選択された", 1, 0x65)]
    public class UnitEvolutionWindow : MonoBehaviour, IFlowInterface
    {
        [HelpBox("アイテムの親となるゲームオブジェクト")]
        public RectTransform ListParent;
        [HelpBox("アイテムスロットの雛形")]
        public ListItemEvents ItemSlotTemplate;
        [HelpBox("不要なスロットの雛形")]
        public GameObject UnusedSlotTemplate;
        [HelpBox("不要なスロットを表示する")]
        public bool ShowUnusedSlots;
        [HelpBox("最大スロット数")]
        public int MaxSlots;
        [HelpBox("足りてないものを表示するラベル")]
        public Text HelpText;
        public Button EvolveButton;
        public UnitData Unit;
        public ScrollRect ScrollParent;
        public Transform QuestListParent;
        public GameObject QuestListItemTemplate;
        public SRPG_Button MainPanelCloseBtn;
        public GameObject ItemSlotRoot;
        public GameObject ItemSlotBox;
        public GameObject MainPanel;
        public GameObject SubPanel;
        private List<GameObject> mItems;
        private UnitData mCurrentUnit;
        private List<GameObject> mBoxs;
        private List<GameObject> mGainedQuests;
        private string mLastSelectItemIname;
        private float mDecelerationRate;
        public UnitEvolveEvent OnEvolve;
        public EvolveCloseEvent OnEvolveClose;

        public UnitEvolutionWindow()
        {
            this.ShowUnusedSlots = 1;
            this.MaxSlots = 5;
            this.mItems = new List<GameObject>();
            this.mBoxs = new List<GameObject>();
            this.mGainedQuests = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0011;
            }
            this.Refresh2();
            goto Label_0036;
        Label_0011:
            if (pinID != 2)
            {
                goto Label_0036;
            }
            if (this.OnEvolveClose == null)
            {
                goto Label_002E;
            }
            this.OnEvolveClose();
        Label_002E:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x66);
        Label_0036:
            return;
        }

        private void AddList(QuestParam qparam, bool isActive)
        {
            GameObject obj2;
            SRPG_Button button;
            bool flag;
            if ((qparam != null) && (qparam.IsMulti == null))
            {
                goto Label_001C;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => AddList():qparam is Null Reference!");
            return;
        Label_001C:
            if ((this.QuestListItemTemplate == null) == null)
            {
                goto Label_0038;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => AddList():QuestListItemTemplate is Null Reference!");
            return;
        Label_0038:
            if ((this.QuestListParent == null) == null)
            {
                goto Label_0054;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => AddList():QuestListParent is Null Reference!");
            return;
        Label_0054:
            obj2 = Object.Instantiate<GameObject>(this.QuestListItemTemplate);
            this.mGainedQuests.Add(obj2);
            button = obj2.GetComponent<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_00AA;
            }
            button.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestSelect));
            flag = qparam.IsDateUnlock(-1L);
            button.set_interactable((flag == null) ? 0 : isActive);
        Label_00AA:
            DataSource.Bind<QuestParam>(obj2, qparam);
            obj2.get_transform().SetParent(this.QuestListParent, 0);
            obj2.SetActive(1);
            return;
        }

        private void ClearPanel()
        {
            GameUtility.DestroyGameObjects(this.mGainedQuests);
            this.mGainedQuests.Clear();
            return;
        }

        private RecipeParam GetCurrentRecipe(UnitData unit)
        {
            RecipeParam param;
            unit = (unit != null) ? unit : this.mCurrentUnit;
            if (unit != null)
            {
                goto Label_0026;
            }
            DebugUtility.LogError("UnitEvolutionWindow.cs => GetCurrentRecipe():unit and mCurrentUnit is Null References!");
            return null;
        Label_0026:
            return MonoSingleton<GameManager>.Instance.GetRecipeParam(unit.UnitParam.recipes[unit.Rarity]);
        }

        private void OnEvolveClick()
        {
            if (this.OnEvolveClose == null)
            {
                goto Label_0016;
            }
            this.OnEvolveClose();
        Label_0016:
            if (this.OnEvolve == null)
            {
                goto Label_002D;
            }
            this.OnEvolve();
            return;
        Label_002D:
            MonoSingleton<GameManager>.Instance.Player.RarityUpUnit(this.mCurrentUnit);
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        private void OnItemSelect(GameObject go)
        {
            int num;
            RecipeParam param;
            ItemParam param2;
            string str;
            int num2;
            QuestParam param3;
            num = this.mItems.IndexOf(go);
            if (num < 0)
            {
                goto Label_00A0;
            }
            param = this.GetCurrentRecipe(this.mCurrentUnit);
            param2 = MonoSingleton<GameManager>.Instance.GetItemParam(param.items[num].iname);
            str = string.Empty;
            if (param2.quests == null)
            {
                goto Label_008F;
            }
            num2 = 0;
            goto Label_0080;
        Label_0052:
            param3 = MonoSingleton<GameManager>.Instance.FindQuest(param2.quests[num2]);
            str = str + param3.name + "\n";
            num2 += 1;
        Label_0080:
            if (num2 < ((int) param2.quests.Length))
            {
                goto Label_0052;
            }
        Label_008F:
            UIUtility.SystemMessage(param2.name, str, null, null, 0, -1);
        Label_00A0:
            return;
        }

        private void OnItemSelect2(GameObject go)
        {
            int num;
            if (this.mCurrentUnit != null)
            {
                goto Label_0016;
            }
            DebugUtility.LogError("UnitEvolutionWindow.cs => OnItemSelect2():mCurrentUnit is Null or Empty!");
            return;
        Label_0016:
            num = this.mItems.IndexOf(go);
            if (num < 0)
            {
                goto Label_0031;
            }
            this.RefreshSubPanel(num);
        Label_0031:
            return;
        }

        private void OnQuestSelect(SRPG_Button button)
        {
            int num;
            bool flag;
            QuestParam[] paramArray;
            bool flag2;
            <OnQuestSelect>c__AnonStorey3C9 storeyc;
            storeyc = new <OnQuestSelect>c__AnonStorey3C9();
            num = this.mGainedQuests.IndexOf(button.get_gameObject());
            storeyc.quest = DataSource.FindDataOfClass<QuestParam>(this.mGainedQuests[num], null);
            if (storeyc.quest != null)
            {
                goto Label_0049;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => OnQuestSelect():quest is Null Reference!");
            return;
        Label_0049:
            if (storeyc.quest.IsDateUnlock(-1L) != null)
            {
                goto Label_0074;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), null, null, 0, -1);
            return;
        Label_0074:
            if (((Array.Find<QuestParam>(MonoSingleton<GameManager>.GetInstanceDirect().Player.AvailableQuests, new Predicate<QuestParam>(storeyc.<>m__45F)) == null) == 0) != null)
            {
                goto Label_00BA;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), null, null, 0, -1);
            return;
        Label_00BA:
            GlobalVars.SelectedQuestID = storeyc.quest.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        }

        public void Refresh()
        {
            string str;
            bool flag;
            RecipeParam param;
            int num;
            RecipeItem item;
            GameObject obj2;
            ListItemEvents events;
            ItemParam param2;
            JobEvolutionRecipe recipe;
            if ((this.ItemSlotTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((this.ShowUnusedSlots == null) || ((this.UnusedSlotTemplate == null) == null))
            {
                goto Label_002F;
            }
            return;
        Label_002F:
            this.mCurrentUnit = (this.Unit == null) ? MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID) : this.Unit;
            if (this.mCurrentUnit != null)
            {
                goto Label_0070;
            }
            return;
        Label_0070:
            GameUtility.DestroyGameObjects(this.mItems);
            this.mItems.Clear();
            DataSource.Bind<UnitData>(base.get_gameObject(), this.mCurrentUnit);
            GameParameter.UpdateAll(base.get_gameObject());
            str = null;
            flag = this.mCurrentUnit.CheckUnitRarityUp();
            param = this.GetCurrentRecipe(this.mCurrentUnit);
            DataSource.Bind<RecipeParam>(base.get_gameObject(), param);
            if (str != null)
            {
                goto Label_00F7;
            }
            if (param == null)
            {
                goto Label_00F7;
            }
            if (param.cost <= MonoSingleton<GameManager>.Instance.Player.Gold)
            {
                goto Label_00F7;
            }
            str = "sys.GOLD_NOT_ENOUGH";
            flag = 0;
        Label_00F7:
            if (str != null)
            {
                goto Label_012B;
            }
            if (this.mCurrentUnit.Lv >= this.mCurrentUnit.GetRarityLevelCap(this.mCurrentUnit.Rarity))
            {
                goto Label_012B;
            }
            str = "sys.LEVEL_NOT_ENOUGH";
            flag = 0;
        Label_012B:
            if (param == null)
            {
                goto Label_028E;
            }
            num = 0;
            goto Label_0280;
        Label_0138:
            item = param.items[num];
            if (item == null)
            {
                goto Label_015A;
            }
            if (string.IsNullOrEmpty(item.iname) == null)
            {
                goto Label_01A4;
            }
        Label_015A:
            if (this.ShowUnusedSlots != null)
            {
                goto Label_016A;
            }
            goto Label_027C;
        Label_016A:
            obj2 = Object.Instantiate<GameObject>(this.UnusedSlotTemplate);
            obj2.get_transform().SetParent(this.ListParent, 0);
            this.mItems.Add(obj2);
            obj2.SetActive(1);
            goto Label_027C;
        Label_01A4:
            events = Object.Instantiate<ListItemEvents>(this.ItemSlotTemplate);
            events.get_transform().SetParent(this.ListParent, 0);
            this.mItems.Add(events.get_gameObject());
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            events.get_gameObject().SetActive(1);
            param2 = MonoSingleton<GameManager>.Instance.GetItemParam(item.iname);
            recipe = new JobEvolutionRecipe();
            recipe.Item = param2;
            recipe.RecipeItem = item;
            recipe.Amount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(item.iname);
            recipe.RequiredAmount = item.num;
            if (recipe.Amount >= recipe.RequiredAmount)
            {
                goto Label_026E;
            }
            flag = 0;
            if (str != null)
            {
                goto Label_026E;
            }
            str = "sys.ITEM_NOT_ENOUGH";
        Label_026E:
            DataSource.Bind<JobEvolutionRecipe>(events.get_gameObject(), recipe);
        Label_027C:
            num += 1;
        Label_0280:
            if (num < ((int) param.items.Length))
            {
                goto Label_0138;
            }
        Label_028E:
            if ((this.HelpText != null) == null)
            {
                goto Label_02CD;
            }
            this.HelpText.get_gameObject().SetActive((str == null) == 0);
            if (str == null)
            {
                goto Label_02CD;
            }
            this.HelpText.set_text(LocalizedText.Get(str));
        Label_02CD:
            if ((this.EvolveButton != null) == null)
            {
                goto Label_02EA;
            }
            this.EvolveButton.set_interactable(flag);
        Label_02EA:
            return;
        }

        public void Refresh2()
        {
            GameManager manager;
            UnitData data;
            string str;
            bool flag;
            RecipeParam param;
            int num;
            GridLayoutGroup group;
            GameObject obj2;
            GameObject obj3;
            int num2;
            RecipeItem item;
            int num3;
            ListItemEvents events;
            ItemParam param2;
            JobEvolutionRecipe recipe;
            if ((this.ItemSlotTemplate == null) == null)
            {
                goto Label_001C;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():ItemSlotTemplate is Null or Empty!");
            return;
        Label_001C:
            if ((this.ItemSlotRoot == null) == null)
            {
                goto Label_0038;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():ItemSlotRoot is Null or Empty!");
            return;
        Label_0038:
            if ((this.ItemSlotBox == null) == null)
            {
                goto Label_0054;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():ItemSlotBox is Null or Empty!");
            return;
        Label_0054:
            if ((this.SubPanel == null) == null)
            {
                goto Label_0070;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():SubPanel is Null References!");
            return;
        Label_0070:
            this.SubPanel.SetActive(0);
            if ((this.MainPanelCloseBtn == null) == null)
            {
                goto Label_0098;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():MainPanelCloseBtn is Null References!");
            return;
        Label_0098:
            this.MainPanelCloseBtn.get_gameObject().SetActive(1);
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_00BC;
            }
            return;
        Label_00BC:
            data = (this.Unit == null) ? manager.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID) : this.Unit;
            if (data != null)
            {
                goto Label_00EF;
            }
            return;
        Label_00EF:
            this.mCurrentUnit = data;
            GameUtility.DestroyGameObjects(this.mItems);
            this.mItems.Clear();
            GameUtility.DestroyGameObjects(this.mBoxs);
            this.mBoxs.Clear();
            DataSource.Bind<UnitData>(base.get_gameObject(), data);
            GameParameter.UpdateAll(base.get_gameObject());
            str = null;
            flag = data.CheckUnitRarityUp();
            param = this.GetCurrentRecipe(data);
            DataSource.Bind<RecipeParam>(base.get_gameObject(), param);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0189;
            }
            if (param == null)
            {
                goto Label_0189;
            }
            if (param.cost <= manager.Player.Gold)
            {
                goto Label_0189;
            }
            str = "sys.GOLD_NOT_ENOUGH";
            flag = 0;
        Label_0189:
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_01B3;
            }
            if (data.Lv >= data.GetRarityLevelCap(data.Rarity))
            {
                goto Label_01B3;
            }
            str = "sys.LEVEL_NOT_ENOUGH";
            flag = 0;
        Label_01B3:
            if (param == null)
            {
                goto Label_0478;
            }
            if (param.items == null)
            {
                goto Label_01D5;
            }
            if (((int) param.items.Length) > 0)
            {
                goto Label_01E4;
            }
        Label_01D5:
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():recipe_param.items is Null or Count 0!");
            goto Label_0478;
        Label_01E4:
            num = (int) param.items.Length;
            group = this.ItemSlotBox.GetComponent<GridLayoutGroup>();
            if ((group == null) == null)
            {
                goto Label_0214;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():gridlayout is Not Component [GridLayoutGroup]!");
            return;
        Label_0214:
            obj2 = Object.Instantiate<GameObject>(this.ItemSlotBox);
            obj2.get_transform().SetParent(this.ItemSlotRoot.get_transform(), 0);
            obj2.SetActive(1);
            this.mBoxs.Add(obj2);
            if (num <= group.get_constraintCount())
            {
                goto Label_0296;
            }
            obj3 = Object.Instantiate<GameObject>(this.ItemSlotBox);
            obj3.get_transform().SetParent(this.ItemSlotRoot.get_transform(), 0);
            obj3.SetActive(1);
            this.mBoxs.Add(obj3);
        Label_0296:
            num2 = 0;
            goto Label_040C;
        Label_029E:
            item = param.items[num2];
            if (item == null)
            {
                goto Label_0406;
            }
            if (string.IsNullOrEmpty(item.iname) == null)
            {
                goto Label_02C7;
            }
            goto Label_0406;
        Label_02C7:
            num3 = 0;
            if (num <= group.get_constraintCount())
            {
                goto Label_030A;
            }
            if ((num % 2) != null)
            {
                goto Label_02F9;
            }
            if (num2 < (group.get_constraintCount() - 1))
            {
                goto Label_030A;
            }
            num3 = 1;
            goto Label_030A;
        Label_02F9:
            if (num2 < group.get_constraintCount())
            {
                goto Label_030A;
            }
            num3 = 1;
        Label_030A:
            events = Object.Instantiate<ListItemEvents>(this.ItemSlotTemplate);
            events.get_transform().SetParent(this.mBoxs[num3].get_transform(), 0);
            this.mItems.Add(events.get_gameObject());
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect2);
            events.get_gameObject().SetActive(1);
            param2 = manager.GetItemParam(item.iname);
            if (param2 != null)
            {
                goto Label_0389;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():item_param is Null References!");
            return;
        Label_0389:
            DataSource.Bind<ItemParam>(events.get_gameObject(), param2);
            recipe = new JobEvolutionRecipe();
            recipe.Item = param2;
            recipe.RecipeItem = item;
            recipe.Amount = manager.Player.GetItemAmount(item.iname);
            recipe.RequiredAmount = item.num;
            if (recipe.Amount >= recipe.RequiredAmount)
            {
                goto Label_03F8;
            }
            flag = 0;
            if (str != null)
            {
                goto Label_03F8;
            }
            str = "sys.ITEM_NOT_ENOUGH";
        Label_03F8:
            DataSource.Bind<JobEvolutionRecipe>(events.get_gameObject(), recipe);
        Label_0406:
            num2 += 1;
        Label_040C:
            if (num2 < num)
            {
                goto Label_029E;
            }
            if ((this.HelpText != null) == null)
            {
                goto Label_045B;
            }
            this.HelpText.get_gameObject().SetActive(string.IsNullOrEmpty(str) == 0);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_045B;
            }
            this.HelpText.set_text(LocalizedText.Get(str));
        Label_045B:
            if ((this.EvolveButton != null) == null)
            {
                goto Label_0478;
            }
            this.EvolveButton.set_interactable(flag);
        Label_0478:
            return;
        }

        [DebuggerHidden]
        private IEnumerator RefreshScrollRect()
        {
            <RefreshScrollRect>c__Iterator166 iterator;
            iterator = new <RefreshScrollRect>c__Iterator166();
            iterator.<>f__this = this;
            return iterator;
        }

        private unsafe void RefreshSubPanel(int index)
        {
            RecipeParam param;
            ItemParam param2;
            QuestParam[] paramArray;
            List<QuestParam> list;
            List<QuestParam>.Enumerator enumerator;
            bool flag;
            <RefreshSubPanel>c__AnonStorey3C8 storeyc;
            this.ClearPanel();
            if ((this.MainPanelCloseBtn == null) == null)
            {
                goto Label_0022;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => RefreshSubPanel():MainPanelCloseBtn is Null References!");
            return;
        Label_0022:
            this.MainPanelCloseBtn.get_gameObject().SetActive(0);
            if (index >= 0)
            {
                goto Label_0045;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => RefreshSubPanel():index is 0!");
            return;
        Label_0045:
            param = this.GetCurrentRecipe(this.mCurrentUnit);
            if (param != null)
            {
                goto Label_0063;
            }
            DebugUtility.LogError("UnitEvolutionWindow.cs => RefreshSubPanel():recipeParam is Null References!");
            return;
        Label_0063:
            param2 = MonoSingleton<GameManager>.GetInstanceDirect().GetItemParam(param.items[index].iname);
            if (param2 != null)
            {
                goto Label_008C;
            }
            DebugUtility.LogError("UnitEvolutionWindow.cs => RefreshSubPanel():itemParam is Null References!");
            return;
        Label_008C:
            this.SubPanel.SetActive(1);
            DataSource.Bind<ItemParam>(this.SubPanel, param2);
            GameParameter.UpdateAll(this.SubPanel.get_gameObject());
            if ((this.mLastSelectItemIname != param2.iname) == null)
            {
                goto Label_00DC;
            }
            this.ResetScrollPosition();
            this.mLastSelectItemIname = param2.iname;
        Label_00DC:
            if ((QuestDropParam.Instance != null) == null)
            {
                goto Label_0192;
            }
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            list = QuestDropParam.Instance.GetItemDropQuestList(param2, GlobalVars.GetDropTableGeneratedDateTime());
            storeyc = new <RefreshSubPanel>c__AnonStorey3C8();
            enumerator = list.GetEnumerator();
        Label_011C:
            try
            {
                goto Label_0174;
            Label_0121:
                storeyc.qp = &enumerator.Current;
                DebugUtility.Log("QuestList:" + storeyc.qp.iname);
                flag = (Array.Find<QuestParam>(paramArray, new Predicate<QuestParam>(storeyc.<>m__45E)) == null) == 0;
                this.AddList(storeyc.qp, flag);
            Label_0174:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0121;
                }
                goto Label_0192;
            }
            finally
            {
            Label_0185:
                ((List<QuestParam>.Enumerator) enumerator).Dispose();
            }
        Label_0192:
            return;
        }

        private unsafe void ResetScrollPosition()
        {
            RectTransform transform;
            Vector2 vector;
            if ((this.ScrollParent == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mDecelerationRate = this.ScrollParent.get_decelerationRate();
            this.ScrollParent.set_decelerationRate(0f);
            transform = this.QuestListParent as RectTransform;
            transform.set_anchoredPosition(new Vector2(&transform.get_anchoredPosition().x, 0f));
            base.StartCoroutine(this.RefreshScrollRect());
            return;
        }

        private void Start()
        {
            if ((this.ItemSlotTemplate != null) == null)
            {
                goto Label_0022;
            }
            this.ItemSlotTemplate.get_gameObject().SetActive(0);
        Label_0022:
            if ((this.UnusedSlotTemplate != null) == null)
            {
                goto Label_0044;
            }
            this.UnusedSlotTemplate.get_gameObject().SetActive(0);
        Label_0044:
            if ((this.EvolveButton != null) == null)
            {
                goto Label_0071;
            }
            this.EvolveButton.get_onClick().AddListener(new UnityAction(this, this.OnEvolveClick));
        Label_0071:
            if ((this.ItemSlotBox != null) == null)
            {
                goto Label_008E;
            }
            this.ItemSlotBox.SetActive(0);
        Label_008E:
            if ((this.SubPanel != null) == null)
            {
                goto Label_00AB;
            }
            this.SubPanel.SetActive(0);
        Label_00AB:
            if ((this.QuestListItemTemplate != null) == null)
            {
                goto Label_00C8;
            }
            this.QuestListItemTemplate.SetActive(0);
        Label_00C8:
            this.Refresh2();
            return;
        }

        [CompilerGenerated]
        private sealed class <OnQuestSelect>c__AnonStorey3C9
        {
            internal QuestParam quest;

            public <OnQuestSelect>c__AnonStorey3C9()
            {
                base..ctor();
                return;
            }

            internal bool <>m__45F(QuestParam p)
            {
                return (p.iname == this.quest.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshScrollRect>c__Iterator166 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitEvolutionWindow <>f__this;

            public <RefreshScrollRect>c__Iterator166()
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
                        goto Label_0034;
                }
                goto Label_006C;
            Label_0021:
                this.$current = null;
                this.$PC = 1;
                goto Label_006E;
            Label_0034:
                if ((this.<>f__this.ScrollParent != null) == null)
                {
                    goto Label_0065;
                }
                this.<>f__this.ScrollParent.set_decelerationRate(this.<>f__this.mDecelerationRate);
            Label_0065:
                this.$PC = -1;
            Label_006C:
                return 0;
            Label_006E:
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
        private sealed class <RefreshSubPanel>c__AnonStorey3C8
        {
            internal QuestParam qp;

            public <RefreshSubPanel>c__AnonStorey3C8()
            {
                base..ctor();
                return;
            }

            internal bool <>m__45E(QuestParam p)
            {
                return (p.iname == this.qp.iname);
            }
        }

        public delegate void EvolveCloseEvent();

        public delegate void UnitEvolveEvent();
    }
}

