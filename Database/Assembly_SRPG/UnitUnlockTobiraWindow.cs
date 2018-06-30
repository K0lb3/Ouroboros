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

    [Pin(100, "真理開眼開放ボタン押下", 0, 100), Pin(0x66, "閉じる：完了", 1, 0x66), Pin(0, "表示を更新", 0, 0), Pin(50, "素材をクリック", 0, 50), Pin(0x33, "クエストをクリック", 0, 0x33), Pin(0x65, "入手クエストが選択された", 1, 0x65)]
    public class UnitUnlockTobiraWindow : MonoBehaviour, IFlowInterface
    {
        public const int ON_CLICK_ELEMENT_BUTTON = 50;
        public const int ON_CLICK_QUEST_BUTTON = 0x33;
        public const int ON_CLICK_UNLOCK_TOBIRA_BUTTON = 100;
        public const int OUT_SELECT_QUEST = 0x65;
        public const int OUT_CLOSE_WINDOW = 0x66;
        [HelpBox("アイテムの親となるゲームオブジェクト")]
        public RectTransform ListParent;
        [HelpBox("アイテムスロットの雛形")]
        public GameObject ItemSlotTemplate;
        [HelpBox("不要なスロットの雛形")]
        public GameObject UnusedSlotTemplate;
        [HelpBox("不要なスロットを表示する")]
        public bool ShowUnusedSlots;
        [HelpBox("最大スロット数")]
        public int MaxSlots;
        [HelpBox("足りてないものを表示するラベル")]
        public Text HelpText;
        public Button UnlockTobiraButton;
        public ScrollRect ScrollParent;
        public Transform QuestListParent;
        public GameObject QuestListItemTemplate;
        public Button MainPanelCloseBtn;
        public GameObject ItemSlotRoot;
        public GameObject ItemSlotBox;
        public GameObject MainPanel;
        public GameObject SubPanel;
        private UnitData mCurrentUnit;
        private List<GameObject> mItems;
        private List<GameObject> mBoxs;
        private List<GameObject> mGainedQuests;
        private string mLastSelectItemIname;
        private float mDecelerationRate;
        public CallbackEvent OnCallback;

        public UnitUnlockTobiraWindow()
        {
            this.ShowUnusedSlots = 1;
            this.MaxSlots = 5;
            this.mItems = new List<GameObject>();
            this.mBoxs = new List<GameObject>();
            this.mGainedQuests = new List<GameObject>();
            base..ctor();
            return;
        }

        private bool _CheckNullReference()
        {
            if ((this.ItemSlotTemplate == null) == null)
            {
                goto Label_001D;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh():ItemSlotTemplate is Null or Empty!");
            return 0;
        Label_001D:
            if ((this.ItemSlotRoot == null) == null)
            {
                goto Label_003A;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh():ItemSlotRoot is Null or Empty!");
            return 0;
        Label_003A:
            if ((this.ItemSlotBox == null) == null)
            {
                goto Label_0057;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh():ItemSlotBox is Null or Empty!");
            return 0;
        Label_0057:
            if ((this.SubPanel == null) == null)
            {
                goto Label_0074;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh():SubPanel is Null References!");
            return 0;
        Label_0074:
            if ((this.MainPanelCloseBtn == null) == null)
            {
                goto Label_0091;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh():MainPanelCloseBtn is Null References!");
            return 0;
        Label_0091:
            return 1;
        }

        public void Activated(int pinID)
        {
            SerializeValueList list;
            UnlockTobiraRecipe recipe;
            SerializeValueList list2;
            QuestParam param;
            int num;
            num = pinID;
            if (num == 50)
            {
                goto Label_0035;
            }
            if (num == 0x33)
            {
                goto Label_0069;
            }
            if (num == null)
            {
                goto Label_002A;
            }
            if (num == 100)
            {
                goto Label_009E;
            }
            goto Label_00C1;
        Label_002A:
            this.Refresh();
            goto Label_00C1;
        Label_0035:
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_00C1;
            }
            recipe = list.GetDataSource<UnlockTobiraRecipe>("_self");
            if (recipe == null)
            {
                goto Label_00C1;
            }
            this.RefreshSubPanel(recipe.Index);
            goto Label_00C1;
        Label_0069:
            list2 = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list2 == null)
            {
                goto Label_00C1;
            }
            param = DataSource.FindDataOfClass<QuestParam>(list2.GetGameObject("_self"), null);
            if (param == null)
            {
                goto Label_00C1;
            }
            this.OnQuestSelect(param);
            goto Label_00C1;
        Label_009E:
            if (this.OnCallback == null)
            {
                goto Label_00B4;
            }
            this.OnCallback();
        Label_00B4:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x66);
        Label_00C1:
            return;
        }

        private void AddList(QuestParam qparam, bool isActive)
        {
            GameObject obj2;
            Button button;
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
            obj2.get_transform().SetParent(this.QuestListParent, 0);
            obj2.SetActive(1);
            this.mGainedQuests.Add(obj2);
            button = obj2.GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_00B1;
            }
            flag = qparam.IsDateUnlock(-1L);
            button.set_interactable((flag == null) ? 0 : isActive);
        Label_00B1:
            DataSource.Bind<QuestParam>(obj2, qparam);
            return;
        }

        private TobiraRecipeParam GetCurrentRecipe()
        {
            if (this.mCurrentUnit != null)
            {
                goto Label_0017;
            }
            DebugUtility.LogError("UnitEvolutionWindow.cs => GetCurrentRecipe():unit and mCurrentUnit is Null References!");
            return null;
        Label_0017:
            return MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraRecipe(this.mCurrentUnit.UnitID, 0, 0);
        }

        private void OnQuestSelect(QuestParam quest)
        {
            bool flag;
            QuestParam[] paramArray;
            bool flag2;
            <OnQuestSelect>c__AnonStorey3E1 storeye;
            storeye = new <OnQuestSelect>c__AnonStorey3E1();
            storeye.quest = quest;
            if (storeye.quest != null)
            {
                goto Label_0023;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => OnQuestSelect():quest is Null Reference!");
            return;
        Label_0023:
            if (storeye.quest.IsDateUnlock(-1L) != null)
            {
                goto Label_004D;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), null, null, 0, -1);
            return;
        Label_004D:
            if (((Array.Find<QuestParam>(MonoSingleton<GameManager>.GetInstanceDirect().Player.AvailableQuests, new Predicate<QuestParam>(storeye.<>m__48C)) == null) == 0) != null)
            {
                goto Label_0092;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), null, null, 0, -1);
            return;
        Label_0092:
            GlobalVars.SelectedQuestID = storeye.quest.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        }

        private unsafe void Refresh()
        {
            GameManager manager;
            List<UnitData.TobiraConditioError> list;
            bool flag;
            TobiraRecipeParam param;
            int num;
            GridLayoutGroup group;
            int num2;
            int num3;
            GameObject obj2;
            int num4;
            int num5;
            GameObject obj3;
            ItemParam param2;
            UnlockTobiraRecipe recipe;
            GameObject obj4;
            ItemParam param3;
            UnlockTobiraRecipe recipe2;
            GameObject obj5;
            ItemParam param4;
            UnlockTobiraRecipe recipe3;
            GameObject obj6;
            ItemParam param5;
            UnlockTobiraRecipe recipe4;
            TobiraRecipeMaterialParam param6;
            TobiraRecipeMaterialParam[] paramArray;
            int num6;
            GameObject obj7;
            ItemParam param7;
            UnlockTobiraRecipe recipe5;
            bool flag2;
            <Refresh>c__AnonStorey3E3 storeye;
            storeye = new <Refresh>c__AnonStorey3E3();
            if (this._CheckNullReference() != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            this.MainPanelCloseBtn.get_gameObject().SetActive(1);
            this.SubPanel.SetActive(0);
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_0043;
            }
            return;
        Label_0043:
            this.mCurrentUnit = manager.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
            if (this.mCurrentUnit != null)
            {
                goto Label_006A;
            }
            return;
        Label_006A:
            GameUtility.DestroyGameObjects(this.mItems);
            GameUtility.DestroyGameObjects(this.mBoxs);
            this.mItems.Clear();
            this.mBoxs.Clear();
            DataSource.Bind<UnitData>(base.get_gameObject(), this.mCurrentUnit);
            GameParameter.UpdateAll(base.get_gameObject());
            storeye.errMsg = null;
            list = null;
            flag = (this.mCurrentUnit.CanUnlockTobira() == null) ? 0 : this.mCurrentUnit.MeetsTobiraConditions(0, &list);
            storeye.condsLv = TobiraUtility.GetTobiraUnlockLevel(this.mCurrentUnit.UnitParam.iname);
            param = this.GetCurrentRecipe();
            if (param != null)
            {
                goto Label_0108;
            }
            return;
        Label_0108:
            DataSource.Bind<TobiraRecipeParam>(base.get_gameObject(), param);
            if (list.Count <= 0)
            {
                goto Label_0139;
            }
            list.Find(new Predicate<UnitData.TobiraConditioError>(storeye.<>m__48E));
            goto Label_0162;
        Label_0139:
            if (param.Cost <= manager.Player.Gold)
            {
                goto Label_0162;
            }
            storeye.errMsg = LocalizedText.Get("sys.GOLD_NOT_ENOUGH");
            flag = 0;
        Label_0162:
            num = (int) param.Materials.Length;
            if (param.UnitPieceNum <= 0)
            {
                goto Label_017E;
            }
            num += 1;
        Label_017E:
            if (param.ElementNum <= 0)
            {
                goto Label_0190;
            }
            num += 1;
        Label_0190:
            if (param.UnlockElementNum <= 0)
            {
                goto Label_01A2;
            }
            num += 1;
        Label_01A2:
            if (param.UnlockBirthNum <= 0)
            {
                goto Label_01B4;
            }
            num += 1;
        Label_01B4:
            group = this.ItemSlotBox.GetComponent<GridLayoutGroup>();
            if ((group == null) == null)
            {
                goto Label_01D9;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh2():gridlayout is Not Component [GridLayoutGroup]!");
            return;
        Label_01D9:
            num2 = (num / group.get_constraintCount()) + 1;
            num3 = 0;
            goto Label_022F;
        Label_01EF:
            obj2 = Object.Instantiate<GameObject>(this.ItemSlotBox);
            obj2.get_transform().SetParent(this.ItemSlotRoot.get_transform(), 0);
            obj2.SetActive(1);
            this.mBoxs.Add(obj2);
            num3 += 1;
        Label_022F:
            if (num3 < num2)
            {
                goto Label_01EF;
            }
            num4 = 0;
            num5 = 0;
            if (param.UnitPieceNum <= 0)
            {
                goto Label_0351;
            }
            obj3 = Object.Instantiate<GameObject>(this.ItemSlotTemplate);
            obj3.get_transform().SetParent(this.mBoxs[num5].get_transform(), 0);
            obj3.SetActive(1);
            this.mItems.Add(obj3.get_gameObject());
            param2 = manager.GetItemParam(this.mCurrentUnit.UnitParam.piece);
            if (param2 != null)
            {
                goto Label_02BA;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh():item_param is Null References!");
            return;
        Label_02BA:
            DataSource.Bind<ItemParam>(obj3.get_gameObject(), param2);
            recipe = new UnlockTobiraRecipe();
            recipe.Amount = manager.Player.GetItemAmount(param2.iname);
            recipe.RequiredAmount = param.UnitPieceNum;
            recipe.Index = num4;
            DataSource.Bind<UnlockTobiraRecipe>(obj3.get_gameObject(), recipe);
            if (flag == null)
            {
                goto Label_034B;
            }
            if (param.UnitPieceNum <= manager.Player.GetItemAmount(this.mCurrentUnit.UnitParam.piece))
            {
                goto Label_034B;
            }
            flag = 0;
            storeye.errMsg = LocalizedText.Get("sys.ITEM_NOT_ENOUGH");
        Label_034B:
            num4 += 1;
        Label_0351:
            num5 = num4 / group.get_constraintCount();
            if (param.ElementNum <= 0)
            {
                goto Label_045C;
            }
            obj4 = Object.Instantiate<GameObject>(this.ItemSlotTemplate);
            obj4.get_transform().SetParent(this.mBoxs[num5].get_transform(), 0);
            obj4.SetActive(1);
            this.mItems.Add(obj4.get_gameObject());
            param3 = this.mCurrentUnit.GetElementPieceParam();
            if (param3 != null)
            {
                goto Label_03CE;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh():item_param is Null References!");
            return;
        Label_03CE:
            DataSource.Bind<ItemParam>(obj4.get_gameObject(), param3);
            recipe2 = new UnlockTobiraRecipe();
            recipe2.Amount = manager.Player.GetItemAmount(param3.iname);
            recipe2.RequiredAmount = param.ElementNum;
            recipe2.Index = num4;
            DataSource.Bind<UnlockTobiraRecipe>(obj4.get_gameObject(), recipe2);
            if (flag == null)
            {
                goto Label_0456;
            }
            if (param.ElementNum <= manager.Player.GetItemAmount(param3.iname))
            {
                goto Label_0456;
            }
            flag = 0;
            storeye.errMsg = LocalizedText.Get("sys.ITEM_NOT_ENOUGH");
        Label_0456:
            num4 += 1;
        Label_045C:
            num5 = num4 / group.get_constraintCount();
            if (param.UnlockElementNum <= 0)
            {
                goto Label_0571;
            }
            obj5 = Object.Instantiate<GameObject>(this.ItemSlotTemplate);
            obj5.get_transform().SetParent(this.mBoxs[num5].get_transform(), 0);
            obj5.SetActive(1);
            this.mItems.Add(obj5.get_gameObject());
            param4 = manager.GetItemParam(this.mCurrentUnit.GetUnlockTobiraElementID());
            if (param4 != null)
            {
                goto Label_04DF;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh():item_param is Null References!");
            return;
        Label_04DF:
            DataSource.Bind<ItemParam>(obj5.get_gameObject(), param4);
            recipe3 = new UnlockTobiraRecipe();
            recipe3.Amount = manager.Player.GetItemAmount(param4.iname);
            recipe3.RequiredAmount = param.UnlockElementNum;
            recipe3.Index = num4;
            DataSource.Bind<UnlockTobiraRecipe>(obj5.get_gameObject(), recipe3);
            if (flag == null)
            {
                goto Label_056B;
            }
            if (param.UnlockElementNum <= manager.Player.GetItemAmount(this.mCurrentUnit.GetUnlockTobiraElementID()))
            {
                goto Label_056B;
            }
            flag = 0;
            storeye.errMsg = LocalizedText.Get("sys.ITEM_NOT_ENOUGH");
        Label_056B:
            num4 += 1;
        Label_0571:
            num5 = num4 / group.get_constraintCount();
            if (param.UnlockBirthNum <= 0)
            {
                goto Label_0686;
            }
            obj6 = Object.Instantiate<GameObject>(this.ItemSlotTemplate);
            obj6.get_transform().SetParent(this.mBoxs[num5].get_transform(), 0);
            obj6.SetActive(1);
            this.mItems.Add(obj6.get_gameObject());
            param5 = manager.GetItemParam(this.mCurrentUnit.GetUnlockTobiraBirthID());
            if (param5 != null)
            {
                goto Label_05F4;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh():item_param is Null References!");
            return;
        Label_05F4:
            DataSource.Bind<ItemParam>(obj6.get_gameObject(), param5);
            recipe4 = new UnlockTobiraRecipe();
            recipe4.Amount = manager.Player.GetItemAmount(param5.iname);
            recipe4.RequiredAmount = param.UnlockBirthNum;
            recipe4.Index = num4;
            DataSource.Bind<UnlockTobiraRecipe>(obj6.get_gameObject(), recipe4);
            if (flag == null)
            {
                goto Label_0680;
            }
            if (param.UnlockBirthNum <= manager.Player.GetItemAmount(this.mCurrentUnit.GetUnlockTobiraBirthID()))
            {
                goto Label_0680;
            }
            flag = 0;
            storeye.errMsg = LocalizedText.Get("sys.ITEM_NOT_ENOUGH");
        Label_0680:
            num4 += 1;
        Label_0686:
            paramArray = param.Materials;
            num6 = 0;
            goto Label_07C3;
        Label_0696:
            param6 = paramArray[num6];
            if (param6 == null)
            {
                goto Label_07BD;
            }
            if (string.IsNullOrEmpty(param6.Iname) == null)
            {
                goto Label_06BA;
            }
            goto Label_07BD;
        Label_06BA:
            num5 = num4 / group.get_constraintCount();
            obj7 = Object.Instantiate<GameObject>(this.ItemSlotTemplate);
            obj7.get_transform().SetParent(this.mBoxs[num5].get_transform(), 0);
            obj7.SetActive(1);
            this.mItems.Add(obj7.get_gameObject());
            param7 = manager.GetItemParam(param6.Iname);
            if (param7 != null)
            {
                goto Label_072D;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => Refresh():item_param is Null References!");
            return;
        Label_072D:
            DataSource.Bind<ItemParam>(obj7.get_gameObject(), param7);
            recipe5 = new UnlockTobiraRecipe();
            recipe5.Amount = manager.Player.GetItemAmount(param7.iname);
            recipe5.RequiredAmount = param6.Num;
            recipe5.Index = num4;
            DataSource.Bind<UnlockTobiraRecipe>(obj7.get_gameObject(), recipe5);
            if (flag == null)
            {
                goto Label_07B7;
            }
            if (param6.Num <= manager.Player.GetItemAmount(param6.Iname))
            {
                goto Label_07B7;
            }
            flag = 0;
            storeye.errMsg = LocalizedText.Get("sys.ITEM_NOT_ENOUGH");
        Label_07B7:
            num4 += 1;
        Label_07BD:
            num6 += 1;
        Label_07C3:
            if (num6 < ((int) paramArray.Length))
            {
                goto Label_0696;
            }
            if ((this.HelpText != null) == null)
            {
                goto Label_081B;
            }
            flag2 = string.IsNullOrEmpty(storeye.errMsg) == 0;
            this.HelpText.get_gameObject().SetActive(flag2);
            if (flag2 == null)
            {
                goto Label_081B;
            }
            this.HelpText.set_text(storeye.errMsg);
        Label_081B:
            if ((this.UnlockTobiraButton != null) == null)
            {
                goto Label_0838;
            }
            this.UnlockTobiraButton.set_interactable(flag);
        Label_0838:
            return;
        }

        [DebuggerHidden]
        private IEnumerator RefreshScrollRect()
        {
            <RefreshScrollRect>c__Iterator17A iteratora;
            iteratora = new <RefreshScrollRect>c__Iterator17A();
            iteratora.<>f__this = this;
            return iteratora;
        }

        private unsafe void RefreshSubPanel(int index)
        {
            TobiraRecipeParam param;
            ItemParam param2;
            int num;
            int num2;
            QuestParam[] paramArray;
            List<QuestParam> list;
            List<QuestParam>.Enumerator enumerator;
            bool flag;
            <RefreshSubPanel>c__AnonStorey3E2 storeye;
            GameUtility.DestroyGameObjects(this.mGainedQuests);
            this.mGainedQuests.Clear();
            this.MainPanelCloseBtn.get_gameObject().SetActive(0);
            if (index >= 0)
            {
                goto Label_0039;
            }
            DebugUtility.LogWarning("UnitEvolutionWindow.cs => RefreshSubPanel():index is 0!");
            return;
        Label_0039:
            param = this.GetCurrentRecipe();
            if (param != null)
            {
                goto Label_0051;
            }
            DebugUtility.LogError("UnitEvolutionWindow.cs => RefreshSubPanel():recipeParam is Null References!");
            return;
        Label_0051:
            param2 = null;
            num = 0;
            if (param.UnitPieceNum <= 0)
            {
                goto Label_008C;
            }
            if (index != num)
            {
                goto Label_0088;
            }
            param2 = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentUnit.UnitParam.piece);
            goto Label_0149;
        Label_0088:
            num += 1;
        Label_008C:
            if (param.ElementNum <= 0)
            {
                goto Label_00B4;
            }
            if (index != num)
            {
                goto Label_00B0;
            }
            param2 = this.mCurrentUnit.GetElementPieceParam();
            goto Label_0149;
        Label_00B0:
            num += 1;
        Label_00B4:
            if (param.UnlockElementNum <= 0)
            {
                goto Label_00E6;
            }
            if (index != num)
            {
                goto Label_00E2;
            }
            param2 = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentUnit.GetUnlockTobiraElementID());
            goto Label_0149;
        Label_00E2:
            num += 1;
        Label_00E6:
            if (param.UnlockBirthNum <= 0)
            {
                goto Label_0118;
            }
            if (index != num)
            {
                goto Label_0114;
            }
            param2 = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentUnit.GetUnlockTobiraBirthID());
            goto Label_0149;
        Label_0114:
            num += 1;
        Label_0118:
            num2 = index - num;
            if (0 > num2)
            {
                goto Label_0149;
            }
            if (num2 >= ((int) param.Materials.Length))
            {
                goto Label_0149;
            }
            param2 = MonoSingleton<GameManager>.Instance.GetItemParam(param.Materials[num2].Iname);
        Label_0149:
            if (param2 != null)
            {
                goto Label_015A;
            }
            DebugUtility.LogError("UnitEvolutionWindow.cs => RefreshSubPanel():itemParam is Null References!");
            return;
        Label_015A:
            this.SubPanel.SetActive(1);
            DataSource.Bind<ItemParam>(this.SubPanel, param2);
            GameParameter.UpdateAll(this.SubPanel.get_gameObject());
            if ((this.mLastSelectItemIname != param2.iname) == null)
            {
                goto Label_01AA;
            }
            this.ResetScrollPosition();
            this.mLastSelectItemIname = param2.iname;
        Label_01AA:
            if ((QuestDropParam.Instance != null) == null)
            {
                goto Label_0249;
            }
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            list = QuestDropParam.Instance.GetItemDropQuestList(param2, GlobalVars.GetDropTableGeneratedDateTime());
            storeye = new <RefreshSubPanel>c__AnonStorey3E2();
            enumerator = list.GetEnumerator();
        Label_01ED:
            try
            {
                goto Label_022B;
            Label_01F2:
                storeye.qp = &enumerator.Current;
                flag = (Array.Find<QuestParam>(paramArray, new Predicate<QuestParam>(storeye.<>m__48D)) == null) == 0;
                this.AddList(storeye.qp, flag);
            Label_022B:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_01F2;
                }
                goto Label_0249;
            }
            finally
            {
            Label_023C:
                ((List<QuestParam>.Enumerator) enumerator).Dispose();
            }
        Label_0249:
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
                goto Label_001D;
            }
            this.ItemSlotTemplate.SetActive(0);
        Label_001D:
            if ((this.UnusedSlotTemplate != null) == null)
            {
                goto Label_003A;
            }
            this.UnusedSlotTemplate.SetActive(0);
        Label_003A:
            if ((this.ItemSlotBox != null) == null)
            {
                goto Label_0057;
            }
            this.ItemSlotBox.SetActive(0);
        Label_0057:
            if ((this.SubPanel != null) == null)
            {
                goto Label_0074;
            }
            this.SubPanel.SetActive(0);
        Label_0074:
            if ((this.QuestListItemTemplate != null) == null)
            {
                goto Label_0091;
            }
            this.QuestListItemTemplate.SetActive(0);
        Label_0091:
            this.Refresh();
            return;
        }

        [CompilerGenerated]
        private sealed class <OnQuestSelect>c__AnonStorey3E1
        {
            internal QuestParam quest;

            public <OnQuestSelect>c__AnonStorey3E1()
            {
                base..ctor();
                return;
            }

            internal bool <>m__48C(QuestParam p)
            {
                return (p.iname == this.quest.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey3E3
        {
            internal int condsLv;
            internal string errMsg;

            public <Refresh>c__AnonStorey3E3()
            {
                base..ctor();
                return;
            }

            internal bool <>m__48E(UnitData.TobiraConditioError error)
            {
                object[] objArray1;
                if (error.Type != 3)
                {
                    goto Label_0032;
                }
                objArray1 = new object[] { (int) this.condsLv };
                this.errMsg = LocalizedText.Get("sys.TOBIRA_CONDS_ERR_UNIT_LV", objArray1);
                return 1;
            Label_0032:
                return 0;
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshScrollRect>c__Iterator17A : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitUnlockTobiraWindow <>f__this;

            public <RefreshScrollRect>c__Iterator17A()
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
        private sealed class <RefreshSubPanel>c__AnonStorey3E2
        {
            internal QuestParam qp;

            public <RefreshSubPanel>c__AnonStorey3E2()
            {
                base..ctor();
                return;
            }

            internal bool <>m__48D(QuestParam p)
            {
                return (p.iname == this.qp.iname);
            }
        }

        public delegate void CallbackEvent();
    }
}

