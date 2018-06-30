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

    [Pin(50, "素材をクリック", 0, 50), Pin(0x33, "クエストをクリック", 0, 0x33), Pin(0x65, "入手クエストが選択された", 1, 0x65), Pin(100, "扉強化ボタン押下", 0, 100), Pin(0x66, "閉じる：完了", 1, 0x66), Pin(0, "表示を更新", 0, 0)]
    public class UnitTobiraEnhanceWindow : MonoBehaviour, IFlowInterface
    {
        public const int ON_CLICK_ELEMENT_BUTTON = 50;
        public const int ON_CLICK_QUEST_BUTTON = 0x33;
        public const int ON_CLICK_TOBIRA_ENHANCE_BUTTON = 100;
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
        [SerializeField]
        private Button EnhanceTobiraButton;
        [SerializeField]
        private ScrollRect ScrollParent;
        [SerializeField]
        private Transform QuestListParent;
        [SerializeField]
        private GameObject QuestListItemTemplate;
        [SerializeField]
        private Button MainPanelCloseBtn;
        [SerializeField]
        private GameObject ItemSlotRoot;
        [SerializeField]
        private GameObject ItemSlotBox;
        [SerializeField]
        private GameObject MainPanel;
        [SerializeField]
        private GameObject SubPanel;
        [SerializeField]
        private Text TitleText;
        [SerializeField]
        private Text MessageText;
        private UnitData mCurrentUnit;
        private TobiraData mCurrentTobira;
        private List<GameObject> mItems;
        private List<GameObject> mBoxs;
        private List<GameObject> mGainedQuests;
        private string mLastSelectItemIname;
        private float mDecelerationRate;
        public CallbackEvent OnCallback;

        public UnitTobiraEnhanceWindow()
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
            if (this.mCurrentUnit == null)
            {
                goto Label_0016;
            }
            if (this.mCurrentTobira != null)
            {
                goto Label_0022;
            }
        Label_0016:
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():CurrentUnit or CurrentTobira is Null or Empty!");
            return 0;
        Label_0022:
            if ((this.ItemSlotTemplate == null) == null)
            {
                goto Label_003F;
            }
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():ItemSlotTemplate is Null or Empty!");
            return 0;
        Label_003F:
            if ((this.ItemSlotRoot == null) == null)
            {
                goto Label_005C;
            }
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():ItemSlotRoot is Null or Empty!");
            return 0;
        Label_005C:
            if ((this.ItemSlotBox == null) == null)
            {
                goto Label_0079;
            }
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():ItemSlotBox is Null or Empty!");
            return 0;
        Label_0079:
            if ((this.SubPanel == null) == null)
            {
                goto Label_0096;
            }
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():SubPanel is Null References!");
            return 0;
        Label_0096:
            if ((this.MainPanelCloseBtn == null) == null)
            {
                goto Label_00B3;
            }
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():MainPanelCloseBtn is Null References!");
            return 0;
        Label_00B3:
            return 1;
        }

        public void Activated(int pinID)
        {
            SerializeValueList list;
            TobiraEnhanceRecipe recipe;
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
            recipe = list.GetDataSource<TobiraEnhanceRecipe>("_self");
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
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => AddList():qparam is Null Reference!");
            return;
        Label_001C:
            if ((this.QuestListItemTemplate == null) == null)
            {
                goto Label_0038;
            }
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => AddList():QuestListItemTemplate is Null Reference!");
            return;
        Label_0038:
            if ((this.QuestListParent == null) == null)
            {
                goto Label_0054;
            }
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => AddList():QuestListParent is Null Reference!");
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
            DebugUtility.LogError("UnitTobiraEnhanceWindow.cs => GetCurrentRecipe():unit and mCurrentUnit is Null References!");
            return null;
        Label_0017:
            return MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraRecipe(this.mCurrentUnit.UnitID, this.mCurrentTobira.Param.TobiraCategory, this.mCurrentTobira.Lv);
        }

        public void Initialize(UnitData unit, TobiraData tobira)
        {
            this.mCurrentUnit = unit;
            this.mCurrentTobira = tobira;
            return;
        }

        private void OnQuestSelect(QuestParam quest)
        {
            bool flag;
            QuestParam[] paramArray;
            bool flag2;
            <OnQuestSelect>c__AnonStorey3DC storeydc;
            storeydc = new <OnQuestSelect>c__AnonStorey3DC();
            storeydc.quest = quest;
            if (storeydc.quest != null)
            {
                goto Label_0023;
            }
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => OnQuestSelect():quest is Null Reference!");
            return;
        Label_0023:
            if (storeydc.quest.IsDateUnlock(-1L) != null)
            {
                goto Label_004D;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), null, null, 0, -1);
            return;
        Label_004D:
            if (((Array.Find<QuestParam>(MonoSingleton<GameManager>.GetInstanceDirect().Player.AvailableQuests, new Predicate<QuestParam>(storeydc.<>m__47E)) == null) == 0) != null)
            {
                goto Label_0092;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), null, null, 0, -1);
            return;
        Label_0092:
            GlobalVars.SelectedQuestID = storeydc.quest.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        }

        private void Refresh()
        {
            GameManager manager;
            string str;
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
            TobiraEnhanceRecipe recipe;
            GameObject obj4;
            ItemParam param3;
            TobiraEnhanceRecipe recipe2;
            GameObject obj5;
            ItemParam param4;
            TobiraEnhanceRecipe recipe3;
            GameObject obj6;
            ItemParam param5;
            TobiraEnhanceRecipe recipe4;
            TobiraRecipeMaterialParam param6;
            TobiraRecipeMaterialParam[] paramArray;
            int num6;
            GameObject obj7;
            ItemParam param7;
            TobiraEnhanceRecipe recipe5;
            bool flag2;
            if (this._CheckNullReference() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.MainPanelCloseBtn.get_gameObject().SetActive(1);
            this.SubPanel.SetActive(0);
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_003C;
            }
            return;
        Label_003C:
            GameUtility.DestroyGameObjects(this.mItems);
            GameUtility.DestroyGameObjects(this.mBoxs);
            this.mItems.Clear();
            this.mBoxs.Clear();
            DataSource.Bind<UnitData>(base.get_gameObject(), this.mCurrentUnit);
            GameParameter.UpdateAll(base.get_gameObject());
            str = null;
            flag = 1;
            param = this.GetCurrentRecipe();
            if (param != null)
            {
                goto Label_0096;
            }
            return;
        Label_0096:
            DataSource.Bind<TobiraRecipeParam>(base.get_gameObject(), param);
            if (param.Cost <= manager.Player.Gold)
            {
                goto Label_00C0;
            }
            str = "sys.GOLD_NOT_ENOUGH";
            flag = 0;
        Label_00C0:
            num = (int) param.Materials.Length;
            if (param.UnitPieceNum <= 0)
            {
                goto Label_00DC;
            }
            num += 1;
        Label_00DC:
            if (param.ElementNum <= 0)
            {
                goto Label_00EE;
            }
            num += 1;
        Label_00EE:
            if (param.UnlockElementNum <= 0)
            {
                goto Label_0100;
            }
            num += 1;
        Label_0100:
            if (param.UnlockBirthNum <= 0)
            {
                goto Label_0112;
            }
            num += 1;
        Label_0112:
            group = this.ItemSlotBox.GetComponent<GridLayoutGroup>();
            if ((group == null) == null)
            {
                goto Label_0137;
            }
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh2():gridlayout is Not Component [GridLayoutGroup]!");
            return;
        Label_0137:
            num2 = (num / group.get_constraintCount()) + 1;
            num3 = 0;
            goto Label_018D;
        Label_014D:
            obj2 = Object.Instantiate<GameObject>(this.ItemSlotBox);
            obj2.get_transform().SetParent(this.ItemSlotRoot.get_transform(), 0);
            obj2.SetActive(1);
            this.mBoxs.Add(obj2);
            num3 += 1;
        Label_018D:
            if (num3 < num2)
            {
                goto Label_014D;
            }
            num4 = 0;
            num5 = 0;
            if (param.UnitPieceNum <= 0)
            {
                goto Label_029B;
            }
            obj3 = Object.Instantiate<GameObject>(this.ItemSlotTemplate);
            obj3.get_transform().SetParent(this.mBoxs[num5].get_transform(), 0);
            obj3.SetActive(1);
            this.mItems.Add(obj3.get_gameObject());
            param2 = manager.GetItemParam(this.mCurrentUnit.UnitParam.piece);
            if (param2 != null)
            {
                goto Label_0218;
            }
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():item_param is Null References!");
            return;
        Label_0218:
            DataSource.Bind<ItemParam>(obj3.get_gameObject(), param2);
            recipe = new TobiraEnhanceRecipe();
            recipe.Amount = manager.Player.GetItemAmount(param2.iname);
            recipe.RequiredAmount = param.UnitPieceNum;
            recipe.Index = num4;
            DataSource.Bind<TobiraEnhanceRecipe>(obj3.get_gameObject(), recipe);
            if (flag == null)
            {
                goto Label_0295;
            }
            if (param.UnitPieceNum <= manager.Player.GetItemAmount(param2.iname))
            {
                goto Label_0295;
            }
            flag = 0;
            str = "sys.ITEM_NOT_ENOUGH";
        Label_0295:
            num4 += 1;
        Label_029B:
            num5 = num4 / group.get_constraintCount();
            if (param.ElementNum <= 0)
            {
                goto Label_039B;
            }
            obj4 = Object.Instantiate<GameObject>(this.ItemSlotTemplate);
            obj4.get_transform().SetParent(this.mBoxs[num5].get_transform(), 0);
            obj4.SetActive(1);
            this.mItems.Add(obj4.get_gameObject());
            param3 = this.mCurrentUnit.GetElementPieceParam();
            if (param3 != null)
            {
                goto Label_0318;
            }
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():item_param is Null References!");
            return;
        Label_0318:
            DataSource.Bind<ItemParam>(obj4.get_gameObject(), param3);
            recipe2 = new TobiraEnhanceRecipe();
            recipe2.Amount = manager.Player.GetItemAmount(param3.iname);
            recipe2.RequiredAmount = param.ElementNum;
            recipe2.Index = num4;
            DataSource.Bind<TobiraEnhanceRecipe>(obj4.get_gameObject(), recipe2);
            if (flag == null)
            {
                goto Label_0395;
            }
            if (param.ElementNum <= manager.Player.GetItemAmount(param3.iname))
            {
                goto Label_0395;
            }
            flag = 0;
            str = "sys.ITEM_NOT_ENOUGH";
        Label_0395:
            num4 += 1;
        Label_039B:
            num5 = num4 / group.get_constraintCount();
            if (param.UnlockElementNum <= 0)
            {
                goto Label_04A5;
            }
            obj5 = Object.Instantiate<GameObject>(this.ItemSlotTemplate);
            obj5.get_transform().SetParent(this.mBoxs[num5].get_transform(), 0);
            obj5.SetActive(1);
            this.mItems.Add(obj5.get_gameObject());
            param4 = manager.GetItemParam(this.mCurrentUnit.GetUnlockTobiraElementID());
            if (param4 != null)
            {
                goto Label_041E;
            }
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():item_param is Null References!");
            return;
        Label_041E:
            DataSource.Bind<ItemParam>(obj5.get_gameObject(), param4);
            recipe3 = new TobiraEnhanceRecipe();
            recipe3.Amount = manager.Player.GetItemAmount(param4.iname);
            recipe3.RequiredAmount = param.UnlockElementNum;
            recipe3.Index = num4;
            DataSource.Bind<TobiraEnhanceRecipe>(obj5.get_gameObject(), recipe3);
            if (flag == null)
            {
                goto Label_049F;
            }
            if (param.UnlockElementNum <= manager.Player.GetItemAmount(this.mCurrentUnit.GetUnlockTobiraElementID()))
            {
                goto Label_049F;
            }
            flag = 0;
            str = "sys.ITEM_NOT_ENOUGH";
        Label_049F:
            num4 += 1;
        Label_04A5:
            num5 = num4 / group.get_constraintCount();
            if (param.UnlockBirthNum <= 0)
            {
                goto Label_05AF;
            }
            obj6 = Object.Instantiate<GameObject>(this.ItemSlotTemplate);
            obj6.get_transform().SetParent(this.mBoxs[num5].get_transform(), 0);
            obj6.SetActive(1);
            this.mItems.Add(obj6.get_gameObject());
            param5 = manager.GetItemParam(this.mCurrentUnit.GetUnlockTobiraBirthID());
            if (param5 != null)
            {
                goto Label_0528;
            }
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():item_param is Null References!");
            return;
        Label_0528:
            DataSource.Bind<ItemParam>(obj6.get_gameObject(), param5);
            recipe4 = new TobiraEnhanceRecipe();
            recipe4.Amount = manager.Player.GetItemAmount(param5.iname);
            recipe4.RequiredAmount = param.UnlockBirthNum;
            recipe4.Index = num4;
            DataSource.Bind<TobiraEnhanceRecipe>(obj6.get_gameObject(), recipe4);
            if (flag == null)
            {
                goto Label_05A9;
            }
            if (param.UnlockBirthNum <= manager.Player.GetItemAmount(this.mCurrentUnit.GetUnlockTobiraBirthID()))
            {
                goto Label_05A9;
            }
            flag = 0;
            str = "sys.ITEM_NOT_ENOUGH";
        Label_05A9:
            num4 += 1;
        Label_05AF:
            paramArray = param.Materials;
            num6 = 0;
            goto Label_06E1;
        Label_05BF:
            param6 = paramArray[num6];
            if (param6 == null)
            {
                goto Label_06DB;
            }
            if (string.IsNullOrEmpty(param6.Iname) == null)
            {
                goto Label_05E3;
            }
            goto Label_06DB;
        Label_05E3:
            num5 = num4 / group.get_constraintCount();
            obj7 = Object.Instantiate<GameObject>(this.ItemSlotTemplate);
            obj7.get_transform().SetParent(this.mBoxs[num5].get_transform(), 0);
            obj7.SetActive(1);
            this.mItems.Add(obj7.get_gameObject());
            param7 = manager.GetItemParam(param6.Iname);
            if (param7 != null)
            {
                goto Label_0656;
            }
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => Refresh():item_param is Null References!");
            return;
        Label_0656:
            DataSource.Bind<ItemParam>(obj7.get_gameObject(), param7);
            recipe5 = new TobiraEnhanceRecipe();
            recipe5.Amount = manager.Player.GetItemAmount(param7.iname);
            recipe5.RequiredAmount = param6.Num;
            recipe5.Index = num4;
            DataSource.Bind<TobiraEnhanceRecipe>(obj7.get_gameObject(), recipe5);
            if (flag == null)
            {
                goto Label_06D5;
            }
            if (param6.Num <= manager.Player.GetItemAmount(param6.Iname))
            {
                goto Label_06D5;
            }
            flag = 0;
            str = "sys.ITEM_NOT_ENOUGH";
        Label_06D5:
            num4 += 1;
        Label_06DB:
            num6 += 1;
        Label_06E1:
            if (num6 < ((int) paramArray.Length))
            {
                goto Label_05BF;
            }
            if ((this.HelpText != null) == null)
            {
                goto Label_0732;
            }
            flag2 = string.IsNullOrEmpty(str) == 0;
            this.HelpText.get_gameObject().SetActive(flag2);
            if (flag2 == null)
            {
                goto Label_0732;
            }
            this.HelpText.set_text(LocalizedText.Get(str));
        Label_0732:
            if ((this.EnhanceTobiraButton != null) == null)
            {
                goto Label_074F;
            }
            this.EnhanceTobiraButton.set_interactable(flag);
        Label_074F:
            return;
        }

        [DebuggerHidden]
        private IEnumerator RefreshScrollRect()
        {
            <RefreshScrollRect>c__Iterator177 iterator;
            iterator = new <RefreshScrollRect>c__Iterator177();
            iterator.<>f__this = this;
            return iterator;
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
            <RefreshSubPanel>c__AnonStorey3DD storeydd;
            GameUtility.DestroyGameObjects(this.mGainedQuests);
            this.mGainedQuests.Clear();
            this.MainPanelCloseBtn.get_gameObject().SetActive(0);
            if (index >= 0)
            {
                goto Label_0039;
            }
            DebugUtility.LogWarning("UnitTobiraEnhanceWindow.cs => RefreshSubPanel():index is 0!");
            return;
        Label_0039:
            param = this.GetCurrentRecipe();
            if (param != null)
            {
                goto Label_0051;
            }
            DebugUtility.LogError("UnitTobiraEnhanceWindow.cs => RefreshSubPanel():recipeParam is Null References!");
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
            DebugUtility.LogError("UnitTobiraEnhanceWindow.cs => RefreshSubPanel():itemParam is Null References!");
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
            storeydd = new <RefreshSubPanel>c__AnonStorey3DD();
            enumerator = list.GetEnumerator();
        Label_01ED:
            try
            {
                goto Label_022B;
            Label_01F2:
                storeydd.qp = &enumerator.Current;
                flag = (Array.Find<QuestParam>(paramArray, new Predicate<QuestParam>(storeydd.<>m__47F)) == null) == 0;
                this.AddList(storeydd.qp, flag);
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
            string str;
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
            str = TobiraParam.GetCategoryName(this.mCurrentTobira.Param.TobiraCategory);
            if ((this.TitleText != null) == null)
            {
                goto Label_00D3;
            }
            this.TitleText.set_text(string.Format(LocalizedText.Get("sys.TOBIRA_ENHANCE_ITEM_BTN_ENHANCE_TITLE"), str));
        Label_00D3:
            if ((this.MessageText != null) == null)
            {
                goto Label_00FF;
            }
            this.MessageText.set_text(string.Format(LocalizedText.Get("sys.TOBIRA_ENHANCE_ITEM_BTN_ENHANCE_MESSAGE"), str));
        Label_00FF:
            this.Refresh();
            return;
        }

        [CompilerGenerated]
        private sealed class <OnQuestSelect>c__AnonStorey3DC
        {
            internal QuestParam quest;

            public <OnQuestSelect>c__AnonStorey3DC()
            {
                base..ctor();
                return;
            }

            internal bool <>m__47E(QuestParam p)
            {
                return (p.iname == this.quest.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshScrollRect>c__Iterator177 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitTobiraEnhanceWindow <>f__this;

            public <RefreshScrollRect>c__Iterator177()
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
        private sealed class <RefreshSubPanel>c__AnonStorey3DD
        {
            internal QuestParam qp;

            public <RefreshSubPanel>c__AnonStorey3DD()
            {
                base..ctor();
                return;
            }

            internal bool <>m__47F(QuestParam p)
            {
                return (p.iname == this.qp.iname);
            }
        }

        public delegate void CallbackEvent();
    }
}

