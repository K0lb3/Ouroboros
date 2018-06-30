namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(3, "アイテムの汎用装備が選択された", 0, 3), Pin(1, "ウインドウを表示", 0, 1), Pin(2, "ウインドウの表示要素を再読み込み", 0, 2), Pin(100, "装備アイテムが装着された", 1, 100), Pin(0x65, "入手クエストが選択された", 1, 0x65), Pin(0x66, "アイテムが作成された", 1, 0x66), Pin(0x67, "アイテムが一括作成された", 1, 0x67), Pin(0x68, "汎用装備が選択された", 1, 0x68)]
    public class UnitEquipmentWindow : MonoBehaviour, IFlowInterface
    {
        public EquipEvent OnEquip;
        public EquipEvent OnCommonEquip;
        public EquipReloadEvent OnReload;
        public GameObject SubWindow;
        public SRPG_Button EquipButton;
        public SRPG_Button CommonEquipButton;
        public SRPG_Button ConfirmQuestButton;
        public SRPG_Button ConfirmRecipeButton;
        public SRPG_Button CreateButton;
        public SRPG_Button CreateButtonAll;
        public RectTransform RecipeParent;
        public RectTransform QuestsParent;
        public RectTransform SelectedItem;
        public RectTransform ItemTreeParent;
        public RectTransform ItemTreeArrow;
        public ListItemEvents ItemTreeTemplate;
        public RectTransform RecipeListParent;
        public RectTransform RecipeListLine;
        public ListItemEvents RecipeListItemTemplate;
        public RectTransform QuestListParent;
        public GameObject QuestListItemTemplate;
        public RectTransform EquipItemParamParent;
        public GameObject EquipItemParamTemplate;
        private EquipData mEquipmentData;
        private List<GameObject> mEquipmentParameters;
        private List<ItemParam> mItemParamTree;
        private List<GameObject> ItemTree;
        private List<EquipRecipeItem> RecipeItems;
        private List<GameObject> GainedQuests;
        private bool mCreateItemSuccessed;
        private ItemParam LastUpadatedItemParam;
        public GameObject[] NoCommonUI;
        public GameObject[] CommonUI;
        public GameObject QuestCommonEquipButton;
        public GameObject RecipeCommonEquipButton;
        public Text EquipAmount;
        public Color EquipAmountColor;
        public Color EquipAmountColorZero;
        private UnitData SelectedUnitData;
        private NeedEquipItemList mNeedEquipItemList;
        private List<ResetDefaultColor> EquipButtonColor;
        private List<ResetDefaultColor> CommonButtonEquipColor;

        public UnitEquipmentWindow()
        {
            this.mEquipmentParameters = new List<GameObject>(5);
            this.mItemParamTree = new List<ItemParam>(4);
            this.ItemTree = new List<GameObject>();
            this.RecipeItems = new List<EquipRecipeItem>();
            this.GainedQuests = new List<GameObject>();
            this.EquipButtonColor = new List<ResetDefaultColor>();
            this.CommonButtonEquipColor = new List<ResetDefaultColor>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private bool <RefreshItemTree>m__45B(RecipeItem p)
        {
            return (p.iname == this.mItemParamTree[this.mItemParamTree.Count - 1].iname);
        }

        public void ActivateCommonUI(bool is_common)
        {
            int num;
            int num2;
            int num3;
            int num4;
            if (this.CommonUI == null)
            {
                goto Label_0077;
            }
            num = 0;
            goto Label_0069;
        Label_0012:
            if ((this.CommonUI[num] == null) == null)
            {
                goto Label_002A;
            }
            goto Label_0065;
        Label_002A:
            this.CommonUI[num].SetActive(is_common);
            num2 = 0;
            goto Label_0054;
        Label_003F:
            this.EquipButtonColor[num2].Reset();
            num2 += 1;
        Label_0054:
            if (num2 < this.EquipButtonColor.Count)
            {
                goto Label_003F;
            }
        Label_0065:
            num += 1;
        Label_0069:
            if (num < ((int) this.CommonUI.Length))
            {
                goto Label_0012;
            }
        Label_0077:
            if (this.NoCommonUI == null)
            {
                goto Label_00F1;
            }
            num3 = 0;
            goto Label_00E3;
        Label_0089:
            if ((this.NoCommonUI[num3] == null) == null)
            {
                goto Label_00A1;
            }
            goto Label_00DF;
        Label_00A1:
            this.NoCommonUI[num3].SetActive(is_common == 0);
            num4 = 0;
            goto Label_00CE;
        Label_00B9:
            this.CommonButtonEquipColor[num4].Reset();
            num4 += 1;
        Label_00CE:
            if (num4 < this.CommonButtonEquipColor.Count)
            {
                goto Label_00B9;
            }
        Label_00DF:
            num3 += 1;
        Label_00E3:
            if (num3 < ((int) this.NoCommonUI.Length))
            {
                goto Label_0089;
            }
        Label_00F1:
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_002A;
            }
            if ((this.SubWindow != null) == null)
            {
                goto Label_0024;
            }
            this.SubWindow.SetActive(0);
        Label_0024:
            this.Refresh();
        Label_002A:
            if (pinID != 2)
            {
                goto Label_005B;
            }
            this.mCreateItemSuccessed = 1;
            this.Refresh();
            if (this.OnReload == null)
            {
                goto Label_0054;
            }
            this.OnReload();
        Label_0054:
            this.mCreateItemSuccessed = 0;
        Label_005B:
            if (pinID != 3)
            {
                goto Label_0069;
            }
            this.CommonEquip(null);
        Label_0069:
            return;
        }

        public void ActiveCommonEquipButton(bool is_common)
        {
            if ((this.QuestCommonEquipButton != null) == null)
            {
                goto Label_001D;
            }
            this.QuestCommonEquipButton.SetActive(is_common);
        Label_001D:
            if ((this.RecipeCommonEquipButton != null) == null)
            {
                goto Label_003A;
            }
            this.RecipeCommonEquipButton.SetActive(is_common);
        Label_003A:
            return;
        }

        private void AddPanel(QuestParam questparam, QuestParam[] availableQuests)
        {
            GameObject obj2;
            SRPG_Button button;
            Button button2;
            bool flag;
            bool flag2;
            <AddPanel>c__AnonStorey3C7 storeyc;
            storeyc = new <AddPanel>c__AnonStorey3C7();
            storeyc.questparam = questparam;
            if (storeyc.questparam != null)
            {
                goto Label_001C;
            }
            return;
        Label_001C:
            if (storeyc.questparam.IsMulti == null)
            {
                goto Label_002E;
            }
            return;
        Label_002E:
            obj2 = Object.Instantiate<GameObject>(this.QuestListItemTemplate);
            button = obj2.GetComponent<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_005F;
            }
            button.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestSelect));
        Label_005F:
            this.GainedQuests.Add(obj2);
            button2 = obj2.GetComponent<Button>();
            if ((button2 != null) == null)
            {
                goto Label_00B9;
            }
            flag = storeyc.questparam.IsDateUnlock(-1L);
            flag2 = (Array.Find<QuestParam>(availableQuests, new Predicate<QuestParam>(storeyc.<>m__45D)) == null) == 0;
            button2.set_interactable((flag == null) ? 0 : flag2);
        Label_00B9:
            DataSource.Bind<QuestParam>(obj2, storeyc.questparam);
            obj2.get_transform().SetParent(this.QuestListParent, 0);
            obj2.SetActive(1);
            return;
        }

        private void Awake()
        {
            if ((this.SubWindow != null) == null)
            {
                goto Label_001D;
            }
            this.SubWindow.SetActive(0);
        Label_001D:
            return;
        }

        public void ClearPanel(bool stop_coroutine)
        {
            int num;
            GameObject obj2;
            this.GainedQuests.Clear();
            num = 0;
            goto Label_003F;
        Label_0012:
            obj2 = this.QuestListParent.GetChild(num).get_gameObject();
            if ((this.QuestListItemTemplate != obj2) == null)
            {
                goto Label_003B;
            }
            Object.Destroy(obj2);
        Label_003B:
            num += 1;
        Label_003F:
            if (num < this.QuestListParent.get_childCount())
            {
                goto Label_0012;
            }
            return;
        }

        private void CommonEquip(GameObject go)
        {
            if (this.OnCommonEquip != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.OnCommonEquip();
            return;
        }

        public void CommonEquiped()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 0x68);
            return;
        }

        private int GetSelectedJobIndex(UnitData unit)
        {
            int num;
            num = 0;
            goto Label_0029;
        Label_0007:
            if (unit.Jobs[num].UniqueID != GlobalVars.SelectedJobUniqueID)
            {
                goto Label_0025;
            }
            return num;
        Label_0025:
            num += 1;
        Label_0029:
            if (num < unit.JobCount)
            {
                goto Label_0007;
            }
            return 0;
        }

        public unsafe bool IsCommonEquipUI(long unit_id, int slot)
        {
            GameManager manager;
            UnitData data;
            int num;
            EquipData data2;
            JobData data3;
            string str;
            ItemData data4;
            ItemParam param;
            ItemParam param2;
            ItemData data5;
            int num2;
            int num3;
            bool flag;
            if (this.IsTreeTop != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player.FindUnitDataByUniqueID(unit_id);
            num = this.GetSelectedJobIndex(data);
            if (data.GetRankupEquipData(num, slot).IsEquiped() == null)
            {
                goto Label_003E;
            }
            return 0;
        Label_003E:
            data3 = data.Jobs[num];
            if (data3.Rank != null)
            {
                goto Label_0056;
            }
            return 0;
        Label_0056:
            str = data3.GetRankupItems(data3.Rank)[slot];
            data4 = manager.Player.FindItemDataByItemID(str);
            param = manager.GetItemParam(str);
            if (((param != null) && (param.IsCommon != null)) && (param.equipLv <= data.Lv))
            {
                goto Label_00A8;
            }
            return 0;
        Label_00A8:
            if ((data4 == null) || (data4.Num < 1))
            {
                goto Label_00BE;
            }
            return 1;
        Label_00BE:
            param2 = manager.MasterParam.GetCommonEquip(param, data3.Rank == 0);
            if (param2 != null)
            {
                goto Label_00E0;
            }
            return 0;
        Label_00E0:
            data5 = manager.Player.FindItemDataByItemID(param2.iname);
            if (data5 != null)
            {
                goto Label_00FD;
            }
            return 1;
        Label_00FD:
            if (manager.MasterParam.FixParam.EquipCommonPieceNum != null)
            {
                goto Label_0114;
            }
            return 0;
        Label_0114:
            num2 = *(&(manager.MasterParam.FixParam.EquipCommonPieceNum[param.rare]));
            num3 = (data3.Rank <= 0) ? 1 : num2;
            if (((data5 == null) ? 0 : ((data5.Num < num3) == 0)) == null)
            {
                goto Label_017F;
            }
            return (data3.Rank > 0);
        Label_017F:
            return 1;
        }

        private void OnCommonEquipClick(SRPG_Button button)
        {
            object[] objArray1;
            UnitData data;
            int num;
            EquipData data2;
            ItemParam param;
            if (button.IsInteractable() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
            num = this.GetSelectedJobIndex(data);
            data2 = data.GetRankupEquipData(num, GlobalVars.SelectedEquipmentSlot);
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetCommonEquip(data2.ItemParam, data.Jobs[num].Rank == 0);
            if (param != null)
            {
                goto Label_006D;
            }
            return;
        Label_006D:
            objArray1 = new object[] { param.name, data2.ItemParam.name };
            UIUtility.ConfirmBox(LocalizedText.Get("sys.COMMON_EQUIP_SOUL_CONFIRM", objArray1), new UIUtility.DialogResultEvent(this.CommonEquip), null, null, 0, -1, null, null);
            return;
        }

        private void OnConfirmQuestClick(SRPG_Button button)
        {
            if (button.IsInteractable() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.SubWindow.SetActive(1);
            return;
        }

        private void OnConfirmRecipeClick(SRPG_Button button)
        {
            if (button.IsInteractable() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.SubWindow.SetActive(1);
            return;
        }

        private void OnCreateAllClick(SRPG_Button button)
        {
            ItemParam param;
            GlobalVars.SelectedCreateItemID = DataSource.FindDataOfClass<ItemParam>(this.SelectedItem.get_gameObject(), null).iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x67);
            return;
        }

        private void OnCreateClick()
        {
            ItemParam param;
            GlobalVars.SelectedCreateItemID = DataSource.FindDataOfClass<ItemParam>(this.SelectedItem.get_gameObject(), null).iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x66);
            return;
        }

        private void OnCreateClick(SRPG_Button button)
        {
            this.OnCreateClick();
            return;
        }

        private void OnEquipClick(SRPG_Button button)
        {
            UnitData data;
            int num;
            int num2;
            EquipData data2;
            ItemData data3;
            if (button.IsInteractable() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.OnEquip == null)
            {
                goto Label_0023;
            }
            this.OnEquip();
            return;
        Label_0023:
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
            if (data != null)
            {
                goto Label_0044;
            }
            return;
        Label_0044:
            num = GlobalVars.SelectedEquipmentSlot;
            if (MonoSingleton<GameManager>.Instance.Player.SetUnitEquipment(data, num) == null)
            {
                goto Label_00B1;
            }
            GameParameter.UpdateAll(base.get_gameObject());
            num2 = this.GetSelectedJobIndex(data);
            data2 = data.GetRankupEquipData(num2, num);
            data3 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(data2.ItemParam);
            GlobalVars.SelectedEquipUniqueID.Set(data3.UniqueID);
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_00B1:
            return;
        }

        private void OnItemTreeSelect(GameObject go)
        {
            int num;
            int num2;
            int num3;
            num2 = this.ItemTree.IndexOf(go) + 1;
            if (num2 != this.ItemTree.Count)
            {
                goto Label_0023;
            }
            return;
        Label_0023:
            num3 = this.mItemParamTree.Count - num2;
            if (num3 <= 0)
            {
                goto Label_0045;
            }
            this.mItemParamTree.RemoveRange(num2, num3);
        Label_0045:
            this.RefreshItemTree(0);
            this.RefreshRecipeItems();
            this.RefreshGainedQuests();
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void OnQuestSelect(SRPG_Button button)
        {
            int num;
            bool flag;
            QuestParam[] paramArray;
            bool flag2;
            <OnQuestSelect>c__AnonStorey3C5 storeyc;
            storeyc = new <OnQuestSelect>c__AnonStorey3C5();
            num = this.GainedQuests.IndexOf(button.get_gameObject());
            storeyc.quest = DataSource.FindDataOfClass<QuestParam>(this.GainedQuests[num], null);
            if (storeyc.quest == null)
            {
                goto Label_00C8;
            }
            if (storeyc.quest.IsDateUnlock(-1L) != null)
            {
                goto Label_0069;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), null, null, 0, -1);
            return;
        Label_0069:
            if (((Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, new Predicate<QuestParam>(storeyc.<>m__45A)) == null) == 0) != null)
            {
                goto Label_00AF;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), null, null, 0, -1);
            return;
        Label_00AF:
            GlobalVars.SelectedQuestID = storeyc.quest.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
        Label_00C8:
            return;
        }

        private void OnRecipeItemSelect(GameObject go)
        {
            RecipeItemParameter parameter;
            parameter = DataSource.FindDataOfClass<RecipeItemParameter>(go, null);
            this.mItemParamTree.Add(parameter.Item);
            this.RefreshItemTree(0);
            this.RefreshRecipeItems();
            this.RefreshGainedQuests();
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void Refresh()
        {
            this.Refresh(null, GlobalVars.SelectedEquipmentSlot);
            return;
        }

        public void Refresh(UnitData unit, int slot)
        {
            int num;
            EquipData[] dataArray;
            EquipData data;
            ItemParam param;
            bool flag;
            ItemData data2;
            ItemParam param2;
            ItemParam param3;
            ItemData data3;
            int num2;
            BuffEffect effect;
            int num3;
            GameObject obj2;
            GameObject obj3;
            EquipItemParameter parameter;
            ItemData data4;
            if ((unit != null) || (this.SelectedUnitData == null))
            {
                goto Label_0033;
            }
            unit = this.SelectedUnitData;
            GlobalVars.SelectedUnitUniqueID.Set(this.SelectedUnitData.UniqueID);
            goto Label_003A;
        Label_0033:
            this.SelectedUnitData = unit;
        Label_003A:
            if (unit != null)
            {
                goto Label_0041;
            }
            return;
        Label_0041:
            num = this.GetSelectedJobIndex(unit);
            dataArray = unit.GetRankupEquips(num);
            if ((slot >= 0) && (((int) dataArray.Length) > slot))
            {
                goto Label_0062;
            }
            return;
        Label_0062:
            data = dataArray[slot];
            param = data.ItemParam;
            if ((this.mItemParamTree.Count != null) && (this.mItemParamTree[0] == param))
            {
                goto Label_00A6;
            }
            this.mItemParamTree.Clear();
            this.mItemParamTree.Add(param);
        Label_00A6:
            flag = 0;
            if (((data == null) || (data.IsValid() == null)) || (data.IsEquiped() == null))
            {
                goto Label_00D1;
            }
            this.mEquipmentData = data;
            goto Label_01CB;
        Label_00D1:
            this.mEquipmentData = new EquipData();
            data2 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param.iname);
            param2 = null;
            if (data2 == null)
            {
                goto Label_010B;
            }
            param2 = data2.Param;
            goto Label_0122;
        Label_010B:
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(param.iname);
        Label_0122:
            param3 = MonoSingleton<GameManager>.Instance.MasterParam.GetCommonEquip(param2, 1);
            data3 = null;
            if (param3 == null)
            {
                goto Label_0158;
            }
            data3 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param3.iname);
        Label_0158:
            if ((((data2 != null) && (data2.Num > 0)) || ((param2.IsCommon == null) || (unit.Jobs[num].Rank != null))) || ((data3 == null) || (data3.Num <= 0)))
            {
                goto Label_01B9;
            }
            this.mEquipmentData.Setup(param3.iname);
            flag = 1;
            goto Label_01CB;
        Label_01B9:
            this.mEquipmentData.Setup(param.iname);
        Label_01CB:
            this.ActivateCommonUI(flag);
            num2 = 0;
            goto Label_01F4;
        Label_01DB:
            this.mEquipmentParameters[num2].SetActive(0);
            num2 += 1;
        Label_01F4:
            if (num2 < this.mEquipmentParameters.Count)
            {
                goto Label_01DB;
            }
            if ((this.mEquipmentData == null) || (this.mEquipmentData.Skill == null))
            {
                goto Label_02F5;
            }
            effect = this.mEquipmentData.Skill.GetBuffEffect(0);
            if ((effect == null) || (effect.targets == null))
            {
                goto Label_02F5;
            }
            num3 = 0;
            goto Label_02E2;
        Label_024F:
            if (num3 < this.mEquipmentParameters.Count)
            {
                goto Label_028E;
            }
            obj2 = Object.Instantiate<GameObject>(this.EquipItemParamTemplate);
            obj2.get_transform().SetParent(this.EquipItemParamParent, 0);
            this.mEquipmentParameters.Add(obj2);
        Label_028E:
            obj3 = this.mEquipmentParameters[num3];
            parameter = DataSource.FindDataOfClass<EquipItemParameter>(obj3, null);
            if (parameter != null)
            {
                goto Label_02B5;
            }
            parameter = new EquipItemParameter();
        Label_02B5:
            parameter.equip = this.mEquipmentData;
            parameter.param_index = num3;
            DataSource.Bind<EquipItemParameter>(obj3, parameter);
            obj3.SetActive(1);
            num3 += 1;
        Label_02E2:
            if (num3 < effect.targets.Count)
            {
                goto Label_024F;
            }
        Label_02F5:
            this.RefreshEquipButton(unit);
            DataSource.Bind<EquipData>(base.get_gameObject(), this.mEquipmentData);
            this.RefreshItemTree(this.mCreateItemSuccessed);
            this.RefreshRecipeItems();
            this.RefreshGainedQuests();
            data4 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mEquipmentData.ItemParam.iname);
        Label_035F:
            this.EquipAmount.set_color(((data4 != null) && (data4.Num != null)) ? this.EquipAmountColor : this.EquipAmountColorZero);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void RefreshEquipButton(UnitData unit)
        {
            bool flag;
            int num;
            EquipData data;
            ItemParam param;
            flag = 0;
            num = this.GetSelectedJobIndex(unit);
            data = unit.GetRankupEquipData(num, GlobalVars.SelectedEquipmentSlot);
            if (data == null)
            {
                goto Label_00CB;
            }
            if (data.IsValid() == null)
            {
                goto Label_00CB;
            }
            if (data.IsEquiped() != null)
            {
                goto Label_00CB;
            }
            if (MonoSingleton<GameManager>.Instance.Player.HasItem(data.ItemID) == null)
            {
                goto Label_006E;
            }
            flag = (data.ItemParam.equipLv > unit.Lv) == 0;
            goto Label_00CB;
        Label_006E:
            if (unit.Jobs[num].Rank > 0)
            {
                goto Label_00CB;
            }
            if (data.ItemParam.IsCommon == null)
            {
                goto Label_00CB;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetCommonEquip(data.ItemParam, 1);
            if (param == null)
            {
                goto Label_00C9;
            }
            flag = MonoSingleton<GameManager>.Instance.Player.HasItem(param.iname);
            goto Label_00CB;
        Label_00C9:
            flag = 0;
        Label_00CB:
            if ((this.EquipButton != null) == null)
            {
                goto Label_00E8;
            }
            this.EquipButton.set_interactable(flag);
        Label_00E8:
            if ((this.CommonEquipButton != null) == null)
            {
                goto Label_0105;
            }
            this.CommonEquipButton.set_interactable(flag);
        Label_0105:
            return;
        }

        private unsafe void RefreshGainedQuests()
        {
            int num;
            RecipeParam param;
            bool flag;
            ItemParam param2;
            QuestParam[] paramArray;
            List<QuestParam> list;
            QuestParam param3;
            List<QuestParam>.Enumerator enumerator;
            this.ClearPanel(1);
            this.QuestsParent.get_gameObject().SetActive(0);
            if ((this.QuestListItemTemplate == null) != null)
            {
                goto Label_003A;
            }
            if ((this.QuestListParent == null) == null)
            {
                goto Label_003B;
            }
        Label_003A:
            return;
        Label_003B:
            if ((this.ConfirmQuestButton != null) == null)
            {
                goto Label_005D;
            }
            this.ConfirmQuestButton.get_gameObject().SetActive(0);
        Label_005D:
            num = this.mItemParamTree.Count - 1;
            if (MonoSingleton<GameManager>.Instance.GetRecipeParam(this.mItemParamTree[num].recipe) == null)
            {
                goto Label_008E;
            }
            return;
        Label_008E:
            if ((this.ConfirmQuestButton != null) == null)
            {
                goto Label_00B0;
            }
            this.ConfirmQuestButton.get_gameObject().SetActive(1);
        Label_00B0:
            flag = this.IsCommonEquipUI(GlobalVars.SelectedUnitUniqueID, GlobalVars.SelectedEquipmentSlot);
            this.ActiveCommonEquipButton(flag);
            param2 = this.mItemParamTree[num];
            DataSource.Bind<ItemParam>(this.QuestsParent.get_gameObject(), param2);
            this.QuestsParent.get_gameObject().SetActive(1);
            if (this.LastUpadatedItemParam == param2)
            {
                goto Label_011A;
            }
            this.SetScrollTop();
            this.LastUpadatedItemParam = param2;
        Label_011A:
            if ((QuestDropParam.Instance != null) == null)
            {
                goto Label_018C;
            }
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            enumerator = QuestDropParam.Instance.GetItemDropQuestList(param2, GlobalVars.GetDropTableGeneratedDateTime()).GetEnumerator();
        Label_0156:
            try
            {
                goto Label_016E;
            Label_015B:
                param3 = &enumerator.Current;
                this.AddPanel(param3, paramArray);
            Label_016E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_015B;
                }
                goto Label_018C;
            }
            finally
            {
            Label_017F:
                ((List<QuestParam>.Enumerator) enumerator).Dispose();
            }
        Label_018C:
            GlobalVars.SelectedItemParamTree.Clear();
            GlobalVars.SelectedItemParamTree.AddRange(this.mItemParamTree.ToArray());
            return;
        }

        private void RefreshItemTree(bool created)
        {
            int num;
            RecipeParam param;
            RecipeItem item;
            int num2;
            int num3;
            int num4;
            ListItemEvents events;
            ListItemEvents events2;
            int num5;
            EquipTree tree;
            if ((this.ItemTreeTemplate == null) != null)
            {
                goto Label_0033;
            }
            if ((this.ItemTreeParent == null) != null)
            {
                goto Label_0033;
            }
            if ((this.ItemTreeArrow == null) == null)
            {
                goto Label_0034;
            }
        Label_0033:
            return;
        Label_0034:
            num = 0;
            goto Label_0056;
        Label_003B:
            this.ItemTree[num].get_gameObject().SetActive(0);
            num += 1;
        Label_0056:
            if (num < this.ItemTree.Count)
            {
                goto Label_003B;
            }
            if (created == null)
            {
                goto Label_0119;
            }
            if (this.mItemParamTree.Count <= 1)
            {
                goto Label_0119;
            }
            item = Array.Find<RecipeItem>(MonoSingleton<GameManager>.Instance.GetRecipeParam(this.mItemParamTree[this.mItemParamTree.Count - 2].recipe).items, new Predicate<RecipeItem>(this.<RefreshItemTree>m__45B));
            if (item == null)
            {
                goto Label_0119;
            }
            num2 = item.num;
            if (MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mItemParamTree[this.mItemParamTree.Count - 1].iname) < num2)
            {
                goto Label_0119;
            }
            this.mItemParamTree.RemoveAt(this.mItemParamTree.Count - 1);
        Label_0119:
            num4 = 0;
            goto Label_0227;
        Label_0121:
            if (num4 < this.ItemTree.Count)
            {
                goto Label_01B1;
            }
            this.ItemTreeTemplate.get_gameObject().SetActive(num4 > 0);
            this.ItemTreeArrow.get_gameObject().SetActive(num4 > 0);
            events = Object.Instantiate<ListItemEvents>(this.ItemTreeTemplate);
            events.get_transform().SetParent(this.ItemTreeParent, 0);
            this.ItemTree.Add(events.get_gameObject());
            this.ItemTreeArrow.get_gameObject().SetActive(0);
            this.ItemTreeTemplate.get_gameObject().SetActive(0);
        Label_01B1:
            events2 = this.ItemTree[num4].GetComponent<ListItemEvents>();
            if ((events2 != null) == null)
            {
                goto Label_01E5;
            }
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemTreeSelect);
        Label_01E5:
            this.ItemTree[num4].get_gameObject().SetActive(1);
            DataSource.Bind<ItemParam>(this.ItemTree[num4].get_gameObject(), this.mItemParamTree[num4]);
            num4 += 1;
        Label_0227:
            if (num4 < this.mItemParamTree.Count)
            {
                goto Label_0121;
            }
            num5 = 0;
            goto Label_02A2;
        Label_0241:
            if ((this.ItemTree[num5] == null) == null)
            {
                goto Label_025E;
            }
            goto Label_029C;
        Label_025E:
            tree = this.ItemTree[num5].GetComponent<EquipTree>();
            if ((tree == null) == null)
            {
                goto Label_0284;
            }
            goto Label_029C;
        Label_0284:
            tree.SetIsLast((this.mItemParamTree.Count - 1) == num5);
        Label_029C:
            num5 += 1;
        Label_02A2:
            if (num5 < this.ItemTree.Count)
            {
                goto Label_0241;
            }
            return;
        }

        private unsafe void RefreshRecipeItems()
        {
            int num;
            int num2;
            RecipeParam param;
            bool flag;
            int num3;
            ListItemEvents events;
            EquipRecipeItem item;
            ListItemEvents events2;
            RecipeItem item2;
            RecipeItemParameter parameter;
            int num4;
            Dictionary<string, int> dictionary;
            bool flag2;
            bool flag3;
            List<RecipeTree> list;
            bool flag4;
            EquipRecipeItem item3;
            int num5;
            RecipeTree tree;
            bool flag5;
            <RefreshRecipeItems>c__AnonStorey3C6 storeyc;
            if (((this.RecipeParent == null) == null) && ((this.SelectedItem == null) == null))
            {
                goto Label_0023;
            }
            return;
        Label_0023:
            this.RecipeParent.get_gameObject().SetActive(0);
            if ((this.CreateButton != null) == null)
            {
                goto Label_0056;
            }
            this.CreateButton.get_gameObject().SetActive(0);
        Label_0056:
            if ((this.CreateButtonAll != null) == null)
            {
                goto Label_0078;
            }
            this.CreateButtonAll.get_gameObject().SetActive(0);
        Label_0078:
            if ((this.ConfirmRecipeButton != null) == null)
            {
                goto Label_009A;
            }
            this.ConfirmRecipeButton.get_gameObject().SetActive(0);
        Label_009A:
            if ((((this.RecipeListItemTemplate == null) == null) && ((this.RecipeListParent == null) == null)) && ((this.RecipeListLine == null) == null))
            {
                goto Label_00CE;
            }
            return;
        Label_00CE:
            num = 0;
            goto Label_00F0;
        Label_00D5:
            this.RecipeItems[num].get_gameObject().SetActive(0);
            num += 1;
        Label_00F0:
            if (num < this.RecipeItems.Count)
            {
                goto Label_00D5;
            }
            num2 = this.mItemParamTree.Count - 1;
            DataSource.Bind<ItemParam>(this.SelectedItem.get_gameObject(), this.mItemParamTree[num2]);
            param = MonoSingleton<GameManager>.Instance.GetRecipeParam(this.mItemParamTree[num2].recipe);
            if (param != null)
            {
                goto Label_014E;
            }
            return;
        Label_014E:
            if ((this.ConfirmRecipeButton != null) == null)
            {
                goto Label_0170;
            }
            this.ConfirmRecipeButton.get_gameObject().SetActive(1);
        Label_0170:
            this.RecipeParent.get_gameObject().SetActive(1);
            flag = this.IsCommonEquipUI(GlobalVars.SelectedUnitUniqueID, GlobalVars.SelectedEquipmentSlot);
            this.ActiveCommonEquipButton(flag);
            num3 = 0;
            goto Label_0312;
        Label_01AB:
            if (num3 < this.RecipeItems.Count)
            {
                goto Label_0244;
            }
            this.RecipeListItemTemplate.get_gameObject().SetActive(num3 > 0);
            this.RecipeListLine.get_gameObject().SetActive(num3 > 0);
            events = Object.Instantiate<ListItemEvents>(this.RecipeListItemTemplate);
            events.get_transform().SetParent(this.RecipeListParent, 0);
            item = events.get_gameObject().GetComponent<EquipRecipeItem>();
            this.RecipeItems.Add(item);
            this.RecipeListLine.get_gameObject().SetActive(0);
            this.RecipeListItemTemplate.get_gameObject().SetActive(0);
        Label_0244:
            events2 = this.RecipeItems[num3].GetComponent<ListItemEvents>();
            if ((events2 != null) == null)
            {
                goto Label_0278;
            }
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.OnRecipeItemSelect);
        Label_0278:
            this.RecipeItems[num3].get_gameObject().SetActive(1);
            item2 = param.items[num3];
            parameter = new RecipeItemParameter();
            parameter.Item = MonoSingleton<GameManager>.Instance.GetItemParam(item2.iname);
            parameter.RecipeItem = item2;
            parameter.Amount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(parameter.Item.iname);
            parameter.RequiredAmount = item2.num;
            DataSource.Bind<RecipeItemParameter>(this.RecipeItems[num3].get_gameObject(), parameter);
            num3 += 1;
        Label_0312:
            if (num3 < ((int) param.items.Length))
            {
                goto Label_01AB;
            }
            num4 = 0;
            dictionary = null;
            flag2 = 0;
            this.mNeedEquipItemList = new NeedEquipItemList();
            flag3 = MonoSingleton<GameManager>.GetInstanceDirect().Player.CheckEnableCreateItem(this.mItemParamTree[num2], &flag2, &num4, &dictionary, this.mNeedEquipItemList);
            list = this.mNeedEquipItemList.GetCurrentRecipeTreeChildren();
            flag4 = (list == null) ? 0 : (list.Count > 0);
            item3 = this.SelectedItem.GetComponent<EquipRecipeItem>();
            if ((item3 != null) == null)
            {
                goto Label_03AD;
            }
            item3.SetIsCommonLine(this.mNeedEquipItemList.IsEnoughCommon());
        Label_03AD:
            num5 = 0;
            goto Label_046F;
        Label_03B5:
            storeyc = new <RefreshRecipeItems>c__AnonStorey3C6();
            this.RecipeItems[num5].SetIsCommonLine(((flag4 == null) || (flag3 != null)) ? 0 : this.mNeedEquipItemList.IsEnoughCommon());
            storeyc.recipe = DataSource.FindDataOfClass<RecipeItemParameter>(this.RecipeItems[num5].get_gameObject(), null);
            tree = (flag4 == null) ? null : list.Find(new Predicate<RecipeTree>(storeyc.<>m__45C));
            flag5 = this.mNeedEquipItemList.IsEnoughCommon();
            this.RecipeItems[num5].SetIsCommon((tree == null) ? 0 : ((tree.IsCommon == null) ? 0 : flag5));
            num5 += 1;
        Label_046F:
            if (num5 < this.RecipeItems.Count)
            {
                goto Label_03B5;
            }
            if (flag3 != null)
            {
                goto Label_0498;
            }
            if (this.mNeedEquipItemList.IsEnoughCommon() == null)
            {
                goto Label_04E8;
            }
        Label_0498:
            if (flag2 == null)
            {
                goto Label_04C6;
            }
            if ((this.CreateButtonAll != null) == null)
            {
                goto Label_04E8;
            }
            this.CreateButtonAll.get_gameObject().SetActive(1);
            goto Label_04E8;
        Label_04C6:
            if ((this.CreateButton != null) == null)
            {
                goto Label_04E8;
            }
            this.CreateButton.get_gameObject().SetActive(1);
        Label_04E8:
            return;
        }

        public void SetResetColor(List<ResetDefaultColor> EquipButtonColorList, Graphic graphic)
        {
            if ((graphic == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            EquipButtonColorList.Add(new ResetDefaultColor(graphic));
            return;
        }

        public void SetRestorationRecipeItem(List<ItemParam> item)
        {
            this.mItemParamTree.Clear();
            this.mItemParamTree.AddRange(item.ToArray());
            this.RefreshItemTree(0);
            this.RefreshRecipeItems();
            this.RefreshGainedQuests();
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private unsafe void SetScrollTop()
        {
            RectTransform transform;
            Vector2 vector;
            transform = this.QuestListParent.GetComponent<RectTransform>();
            if ((transform != null) == null)
            {
                goto Label_0032;
            }
            vector = transform.get_anchoredPosition();
            &vector.y = 0f;
            transform.set_anchoredPosition(vector);
        Label_0032:
            return;
        }

        private void Start()
        {
            if ((this.EquipButton != null) == null)
            {
                goto Label_0060;
            }
            this.EquipButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnEquipClick));
            this.SetResetColor(this.EquipButtonColor, this.EquipButton.get_gameObject().GetComponent<Image>());
            this.SetResetColor(this.EquipButtonColor, this.EquipButton.get_gameObject().GetComponentInChildren<Text>());
        Label_0060:
            if ((this.CommonEquipButton != null) == null)
            {
                goto Label_00C0;
            }
            this.CommonEquipButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnCommonEquipClick));
            this.SetResetColor(this.CommonButtonEquipColor, this.EquipButton.get_gameObject().GetComponent<Image>());
            this.SetResetColor(this.CommonButtonEquipColor, this.EquipButton.get_gameObject().GetComponentInChildren<Text>());
        Label_00C0:
            if ((this.CreateButton != null) == null)
            {
                goto Label_00E8;
            }
            this.CreateButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnCreateClick));
        Label_00E8:
            if ((this.CreateButtonAll != null) == null)
            {
                goto Label_0110;
            }
            this.CreateButtonAll.AddListener(new SRPG_Button.ButtonClickEvent(this.OnCreateAllClick));
        Label_0110:
            if ((this.ConfirmQuestButton != null) == null)
            {
                goto Label_0138;
            }
            this.ConfirmQuestButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnConfirmQuestClick));
        Label_0138:
            if ((this.ConfirmRecipeButton != null) == null)
            {
                goto Label_0160;
            }
            this.ConfirmRecipeButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnConfirmRecipeClick));
        Label_0160:
            if ((this.ItemTreeTemplate != null) == null)
            {
                goto Label_0182;
            }
            this.ItemTreeTemplate.get_gameObject().SetActive(0);
        Label_0182:
            if ((this.RecipeListItemTemplate != null) == null)
            {
                goto Label_01A4;
            }
            this.RecipeListItemTemplate.get_gameObject().SetActive(0);
        Label_01A4:
            if ((this.QuestListItemTemplate != null) == null)
            {
                goto Label_01C1;
            }
            this.QuestListItemTemplate.SetActive(0);
        Label_01C1:
            if ((this.EquipItemParamTemplate != null) == null)
            {
                goto Label_01DE;
            }
            this.EquipItemParamTemplate.SetActive(0);
        Label_01DE:
            this.Refresh();
            return;
        }

        private bool IsTreeTop
        {
            get
            {
                return (this.mItemParamTree.Count == 1);
            }
        }

        [CompilerGenerated]
        private sealed class <AddPanel>c__AnonStorey3C7
        {
            internal QuestParam questparam;

            public <AddPanel>c__AnonStorey3C7()
            {
                base..ctor();
                return;
            }

            internal bool <>m__45D(QuestParam p)
            {
                return (p == this.questparam);
            }
        }

        [CompilerGenerated]
        private sealed class <OnQuestSelect>c__AnonStorey3C5
        {
            internal QuestParam quest;

            public <OnQuestSelect>c__AnonStorey3C5()
            {
                base..ctor();
                return;
            }

            internal bool <>m__45A(QuestParam p)
            {
                return (p == this.quest);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshRecipeItems>c__AnonStorey3C6
        {
            internal RecipeItemParameter recipe;

            public <RefreshRecipeItems>c__AnonStorey3C6()
            {
                base..ctor();
                return;
            }

            internal bool <>m__45C(RecipeTree x)
            {
                return ((string.IsNullOrEmpty(x.iname) != null) ? 0 : (x.iname == this.recipe.Item.iname));
            }
        }

        public delegate void EquipEvent();

        public delegate void EquipReloadEvent();

        public class ResetDefaultColor
        {
            private Graphic graphic;
            private Color color;

            public ResetDefaultColor(Graphic graphic)
            {
                base..ctor();
                this.graphic = graphic;
                this.color = graphic.get_color();
                return;
            }

            public void Reset()
            {
                this.graphic.set_color(this.color);
                return;
            }
        }
    }
}

