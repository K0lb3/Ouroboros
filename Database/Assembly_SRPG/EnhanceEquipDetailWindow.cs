namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(1, "Refresh", 0, 1), Pin(2, "Reset", 0, 2), Pin(100, "強化", 1, 100)]
    public class EnhanceEquipDetailWindow : SRPG_FixedList, IFlowInterface
    {
        public GameObject Unit;
        public List<Button> Equipments;
        public List<RawImage> EquipmentRawImages;
        public List<GameObject> EquipmentCursors;
        public Transform ParamUpLayoutParent;
        public GameObject ParamUpTemplate;
        public Transform ItemLayoutParent;
        public GameObject ItemTemplate;
        public GameObject EquipSelectParent;
        public GameObject SelectedParent;
        public GameObject EnhanceGaugeParent;
        public GameObject ExpUpTemplate;
        public Text TxtExpUpValue;
        public Button BtnJob;
        public Button BtnAdd;
        public Button BtnSub;
        public Button BtnEnhance;
        public Text TxtJob;
        public Text TxtCost;
        public Text TxtComment;
        public Text TxtDisableEnhanceOnGauge;
        private UnitData mUnit;
        private int mJobIndex;
        private GameObject mSelectedEquipItem;
        private EnhanceEquipData mEnhanceEquipData;
        private List<GameObject> mEnhanceParameters;
        private GameObject mSelectedMaterialItem;
        private List<EnhanceMaterial> mEnhanceMaterials;
        private List<EnhanceMaterial> mEnableEnhanceMaterials;
        private List<ItemData> mMaterialItems;
        [CompilerGenerated]
        private static Comparison<ItemData> <>f__am$cache1E;

        public EnhanceEquipDetailWindow()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static int <Start>m__2F3(ItemData src, ItemData dsc)
        {
            if (src.ItemType == dsc.ItemType)
            {
                goto Label_002D;
            }
            if (src.ItemType != 8)
            {
                goto Label_001F;
            }
            return -1;
        Label_001F:
            if (dsc.ItemType != 8)
            {
                goto Label_002D;
            }
            return 1;
        Label_002D:
            return (dsc.Param.enhace_point - src.Param.enhace_point);
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0055;
            }
            this.mUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
            this.mJobIndex = GlobalVars.SelectedUnitJobIndex;
            this.mSelectedMaterialItem = null;
            this.ClearEnhancedMaterial();
            if (base.HasStarted == null)
            {
                goto Label_0054;
            }
            this.RefreshData();
        Label_0054:
            return;
        Label_0055:
            if (pinID != 2)
            {
                goto Label_00B1;
            }
            this.mUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
            this.mJobIndex = GlobalVars.SelectedUnitJobIndex;
            this.mSelectedEquipItem = null;
            this.mSelectedMaterialItem = null;
            this.ClearEnhancedMaterial();
            if (base.HasStarted == null)
            {
                goto Label_00B0;
            }
            this.RefreshData();
        Label_00B0:
            return;
        Label_00B1:
            return;
        }

        private bool CheckEquipItemEnhance()
        {
            EquipData data;
            int num;
            int num2;
            int num3;
            if (this.mEnhanceEquipData == null)
            {
                goto Label_001B;
            }
            if (this.mEnhanceEquipData.equip != null)
            {
                goto Label_001D;
            }
        Label_001B:
            return 0;
        Label_001D:
            data = this.mEnhanceEquipData.equip;
            num = data.Exp + this.mEnhanceEquipData.gainexp;
            num2 = data.CalcRankFromExp(num);
            num3 = data.GetRankCap();
            return (num2 < num3);
        }

        private void ClearEnhancedMaterial()
        {
            int num;
            num = 0;
            goto Label_002F;
        Label_0007:
            this.mEnhanceMaterials[num].num = 0;
            this.mEnhanceMaterials[num].selected = 0;
            num += 1;
        Label_002F:
            if (num < this.mEnhanceMaterials.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public void ClearMaterialSelect()
        {
            int num;
            this.mSelectedMaterialItem = null;
            num = 0;
            goto Label_0024;
        Label_000E:
            this.mEnhanceMaterials[num].selected = 0;
            num += 1;
        Label_0024:
            if (num < this.mEnhanceMaterials.Count)
            {
                goto Label_000E;
            }
            return;
        }

        protected override GameObject CreateItem()
        {
            return Object.Instantiate<GameObject>(this.ItemTemplate);
        }

        public override void GotoNextPage()
        {
            this.ClearMaterialSelect();
            this.RefreshData();
            base.GotoNextPage();
            return;
        }

        public override void GotoPreviousPage()
        {
            this.ClearMaterialSelect();
            this.RefreshData();
            base.GotoPreviousPage();
            return;
        }

        private void OnAddMaterial()
        {
            EnhanceMaterial material;
            int num;
            if ((this.mSelectedMaterialItem == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            material = DataSource.FindDataOfClass<EnhanceMaterial>(this.mSelectedMaterialItem, null);
            if (material == null)
            {
                goto Label_0082;
            }
            if (this.CheckEquipItemEnhance() != null)
            {
                goto Label_004F;
            }
            UIUtility.NegativeSystemMessage(LocalizedText.Get("sys.FAILED_ENHANCE"), LocalizedText.Get("sys.DIABLE_ENHANCE_RANKCAP_MESSAGE"), null, null, 0, -1);
            return;
        Label_004F:
            material.num = Math.Min(material.num += 1, material.item.Num);
            DataSource.Bind<EnhanceMaterial>(this.mSelectedMaterialItem, material);
        Label_0082:
            this.RefreshData();
            return;
        }

        private void OnCancel(GameObject go)
        {
            GlobalVars.SelectedEquipData = null;
            GlobalVars.SelectedEnhanceMaterials = null;
            return;
        }

        private void OnDecide(GameObject go)
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        private void OnEnhance()
        {
            bool flag;
            int num;
            EnhanceMaterial material;
            flag = 0;
            num = 0;
            goto Label_004E;
        Label_0009:
            material = this.mEnhanceMaterials[num];
            if (material.num != null)
            {
                goto Label_0026;
            }
            goto Label_004A;
        Label_0026:
            if (material.item.Rarity > 1)
            {
                goto Label_0048;
            }
            if (material.item.ItemType != 4)
            {
                goto Label_004A;
            }
        Label_0048:
            flag = 1;
        Label_004A:
            num += 1;
        Label_004E:
            if (num < this.mEnhanceMaterials.Count)
            {
                goto Label_0009;
            }
            GlobalVars.SelectedUnitUniqueID.Set(this.mUnit.UniqueID);
            GlobalVars.SelectedUnitJobIndex.Set(this.mJobIndex);
            GlobalVars.SelectedEquipData = DataSource.FindDataOfClass<EquipData>(this.mSelectedEquipItem, null);
            GlobalVars.SelectedEnhanceMaterials = this.mEnhanceMaterials;
            if (flag == null)
            {
                goto Label_00D3;
            }
            UIUtility.ConfirmBox(LocalizedText.Get("sys.ENHANCE_ITEM_RARITY_CAUTION"), null, new UIUtility.DialogResultEvent(this.OnDecide), new UIUtility.DialogResultEvent(this.OnCancel), null, 0, -1);
            return;
        Label_00D3:
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        protected override void OnItemSelect(GameObject go)
        {
            EnhanceMaterial material;
            int num;
            if ((this.mSelectedMaterialItem != go) == null)
            {
                goto Label_006D;
            }
            this.mSelectedMaterialItem = go;
            material = DataSource.FindDataOfClass<EnhanceMaterial>(this.mSelectedMaterialItem, null);
            if (material == null)
            {
                goto Label_0067;
            }
            num = 0;
            goto Label_0056;
        Label_0032:
            this.mEnhanceMaterials[num].selected = material == this.mEnhanceMaterials[num];
            num += 1;
        Label_0056:
            if (num < this.mEnhanceMaterials.Count)
            {
                goto Label_0032;
            }
        Label_0067:
            this.RefreshData();
        Label_006D:
            return;
        }

        private void OnJobChange()
        {
            int num;
            int num2;
            num = this.mJobIndex;
        Label_0007:
            this.mJobIndex = (this.mJobIndex += 1) % this.mUnit.JobCount;
            if (this.mUnit.GetJobData(this.mJobIndex).IsActivated == null)
            {
                goto Label_0007;
            }
            goto Label_004F;
            goto Label_0007;
        Label_004F:
            if (num == this.mJobIndex)
            {
                goto Label_0075;
            }
            this.mSelectedEquipItem = null;
            this.mSelectedMaterialItem = null;
            this.ClearEnhancedMaterial();
            this.RefreshData();
        Label_0075:
            return;
        }

        private void OnSelectEquipment(int slot)
        {
            if ((this.mSelectedEquipItem == this.Equipments[slot].get_gameObject()) == null)
            {
                goto Label_0022;
            }
            return;
        Label_0022:
            this.ClearEnhancedMaterial();
            this.ClearMaterialSelect();
            this.mSelectedEquipItem = this.Equipments[slot].get_gameObject();
            GlobalVars.SelectedEquipmentSlot.Set(slot);
            this.RefreshData();
            return;
        }

        private void OnSubMaterial()
        {
            EnhanceMaterial material;
            int num;
            if ((this.mSelectedMaterialItem == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            material = DataSource.FindDataOfClass<EnhanceMaterial>(this.mSelectedMaterialItem, null);
            if (material == null)
            {
                goto Label_004E;
            }
            material.num = Math.Max(material.num -= 1, 0);
            DataSource.Bind<EnhanceMaterial>(this.mSelectedMaterialItem, material);
        Label_004E:
            this.RefreshData();
            return;
        }

        protected override void OnUpdateItem(GameObject go, int index)
        {
        }

        private unsafe void RefreshData()
        {
            JobData data;
            int num;
            EquipData data2;
            bool flag;
            int num2;
            EquipData data3;
            int num3;
            int num4;
            int num5;
            EnhanceMaterial material;
            EnhanceMaterial material2;
            bool flag2;
            BuffEffect effect;
            int num6;
            GameObject obj2;
            GameObject obj3;
            EquipItemParameter parameter;
            int num7;
            int num8;
            <RefreshData>c__AnonStorey32D storeyd;
            data = this.mUnit.GetJobData(this.mJobIndex);
            DataSource.Bind<UnitData>(this.Unit, this.mUnit);
            num = 0;
            goto Label_00C5;
        Label_002A:
            data2 = data.Equips[num];
            DataSource.Bind<EquipData>(this.Equipments[num].get_gameObject(), data2);
            flag = (data2 == null) ? 0 : ((data2.IsValid() == null) ? 0 : data2.IsEquiped());
            this.Equipments[num].set_interactable(flag);
            this.EquipmentRawImages[num].get_gameObject().SetActive(flag);
            this.EquipmentCursors[num].SetActive(this.mSelectedEquipItem == this.Equipments[num].get_gameObject());
            num += 1;
        Label_00C5:
            if (num < this.Equipments.Count)
            {
                goto Label_002A;
            }
            this.mEnhanceEquipData.equip = null;
            this.mEnhanceEquipData.gainexp = 0;
            this.mEnhanceEquipData.is_enhanced = 0;
            num2 = 0;
            goto Label_011B;
        Label_0102:
            this.mEnhanceParameters[num2].SetActive(0);
            num2 += 1;
        Label_011B:
            if (num2 < this.mEnhanceParameters.Count)
            {
                goto Label_0102;
            }
            this.BtnEnhance.set_interactable(0);
            data3 = ((this.mSelectedEquipItem != null) == null) ? null : DataSource.FindDataOfClass<EquipData>(this.mSelectedEquipItem, null);
            num3 = 0;
            num4 = 0;
            num5 = 0;
            goto Label_022A;
        Label_016C:
            storeyd = new <RefreshData>c__AnonStorey32D();
            storeyd.item = this.mMaterialItems[num5];
            material = this.mEnhanceMaterials.Find(new Predicate<EnhanceMaterial>(storeyd.<>m__2F5));
            if (material != null)
            {
                goto Label_01D6;
            }
            material2 = new EnhanceMaterial();
            material2.item = storeyd.item;
            material2.num = 0;
            this.mEnhanceMaterials.Add(material2);
            material = material2;
        Label_01D6:
            if (data3 == null)
            {
                goto Label_0224;
            }
            num3 += ((storeyd.item.Param.enhace_cost * data3.GetEnhanceCostScale()) / 100) * material.num;
            num4 += storeyd.item.Param.enhace_point * material.num;
        Label_0224:
            num5 += 1;
        Label_022A:
            if (num5 < this.mMaterialItems.Count)
            {
                goto Label_016C;
            }
            flag2 = 0;
            if ((this.EquipSelectParent != null) == null)
            {
                goto Label_026D;
            }
            DataSource.Bind<EnhanceEquipData>(this.EquipSelectParent, null);
            this.EquipSelectParent.get_gameObject().SetActive(0);
        Label_026D:
            if ((this.TxtComment != null) == null)
            {
                goto Label_028F;
            }
            this.TxtComment.get_gameObject().SetActive(0);
        Label_028F:
            if ((this.mSelectedEquipItem != null) == null)
            {
                goto Label_042D;
            }
            this.mEnhanceEquipData.equip = data3;
            this.mEnhanceEquipData.gainexp = num4;
            if (data3 == null)
            {
                goto Label_03F5;
            }
            effect = data3.Skill.GetBuffEffect(0);
            if ((effect == null) || (effect.targets == null))
            {
                goto Label_03A5;
            }
            num6 = 0;
            goto Label_0392;
        Label_02EB:
            if (num6 < this.mEnhanceParameters.Count)
            {
                goto Label_0342;
            }
            this.ParamUpTemplate.SetActive(1);
            obj2 = Object.Instantiate<GameObject>(this.ParamUpTemplate);
            obj2.get_transform().SetParent(this.ParamUpLayoutParent, 0);
            this.mEnhanceParameters.Add(obj2);
            this.ParamUpTemplate.SetActive(0);
        Label_0342:
            obj3 = this.mEnhanceParameters[num6];
            parameter = DataSource.FindDataOfClass<EquipItemParameter>(obj3, null);
            if (parameter != null)
            {
                goto Label_0369;
            }
            parameter = new EquipItemParameter();
        Label_0369:
            parameter.equip = data3;
            parameter.param_index = num6;
            DataSource.Bind<EquipItemParameter>(obj3, parameter);
            obj3.SetActive(1);
            num6 += 1;
        Label_0392:
            if (num6 < effect.targets.Count)
            {
                goto Label_02EB;
            }
        Label_03A5:
            flag2 = 1;
            if (data3.Rank != data3.GetRankCap())
            {
                goto Label_03F5;
            }
            if ((this.TxtComment != null) == null)
            {
                goto Label_03F2;
            }
            this.TxtComment.set_text(LocalizedText.Get("sys.DIABLE_ENHANCE_RANKCAP_MESSAGE"));
            this.TxtComment.get_gameObject().SetActive(1);
        Label_03F2:
            flag2 = 0;
        Label_03F5:
            if ((this.EquipSelectParent != null) == null)
            {
                goto Label_0464;
            }
            DataSource.Bind<EnhanceEquipData>(this.EquipSelectParent, this.mEnhanceEquipData);
            this.EquipSelectParent.get_gameObject().SetActive(1);
            goto Label_0464;
        Label_042D:
            if ((this.TxtComment != null) == null)
            {
                goto Label_0464;
            }
            this.TxtComment.set_text(LocalizedText.Get("sys.ENHANCE_EQUIPMENT_MESSAGE"));
            this.TxtComment.get_gameObject().SetActive(1);
        Label_0464:
            this.mEnhanceEquipData.is_enhanced = flag2;
            if ((this.SelectedParent != null) == null)
            {
                goto Label_04A5;
            }
            DataSource.Bind<EnhanceEquipData>(this.SelectedParent, this.mEnhanceEquipData);
            this.SelectedParent.get_gameObject().SetActive(flag2);
        Label_04A5:
            if ((this.TxtDisableEnhanceOnGauge != null) == null)
            {
                goto Label_04CB;
            }
            this.TxtDisableEnhanceOnGauge.get_gameObject().SetActive(flag2 == 0);
        Label_04CB:
            if ((this.TxtCost != null) == null)
            {
                goto Label_04EE;
            }
            this.TxtCost.set_text(&num3.ToString());
        Label_04EE:
            if ((this.TxtJob != null) == null)
            {
                goto Label_0510;
            }
            this.TxtJob.set_text(data.Name);
        Label_0510:
            if (flag2 == null)
            {
                goto Label_054A;
            }
            num7 = data3.CalcRankFromExp(data3.Exp + num4);
            this.BtnEnhance.set_interactable((data3 == null) ? 0 : (data3.Rank < num7));
        Label_054A:
            if ((this.BtnAdd != null) == null)
            {
                goto Label_057C;
            }
            this.BtnAdd.set_interactable((flag2 == null) ? 0 : (this.mSelectedMaterialItem != null));
        Label_057C:
            if ((this.BtnSub != null) == null)
            {
                goto Label_05AE;
            }
            this.BtnSub.set_interactable((flag2 == null) ? 0 : (this.mSelectedMaterialItem != null));
        Label_05AE:
            this.mEnableEnhanceMaterials.Clear();
            num8 = 0;
            goto Label_061C;
        Label_05C1:
            if (this.mEnhanceMaterials[num8].item != null)
            {
                goto Label_05DD;
            }
            goto Label_0616;
        Label_05DD:
            if (this.mEnhanceMaterials[num8].item.Num != null)
            {
                goto Label_05FE;
            }
            goto Label_0616;
        Label_05FE:
            this.mEnableEnhanceMaterials.Add(this.mEnhanceMaterials[num8]);
        Label_0616:
            num8 += 1;
        Label_061C:
            if (num8 < this.mEnhanceMaterials.Count)
            {
                goto Label_05C1;
            }
            this.SetData(this.mEnableEnhanceMaterials.ToArray(), typeof(EnhanceMaterial));
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        protected override void Start()
        {
            List<ItemData> list;
            int num;
            int num2;
            <Start>c__AnonStorey32C storeyc;
            base.Start();
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            list = MonoSingleton<GameManager>.Instance.Player.Items;
            this.mMaterialItems = new List<ItemData>(list.Count);
            num = 0;
            goto Label_0082;
        Label_0040:
            if (list[num].CheckEquipEnhanceMaterial() == null)
            {
                goto Label_007E;
            }
            if (list[num].Param.is_valuables == null)
            {
                goto Label_006C;
            }
            goto Label_007E;
        Label_006C:
            this.mMaterialItems.Add(list[num]);
        Label_007E:
            num += 1;
        Label_0082:
            if (num < list.Count)
            {
                goto Label_0040;
            }
            if (<>f__am$cache1E != null)
            {
                goto Label_00AC;
            }
            <>f__am$cache1E = new Comparison<ItemData>(EnhanceEquipDetailWindow.<Start>m__2F3);
        Label_00AC:
            this.mMaterialItems.Sort(<>f__am$cache1E);
            this.mUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
            this.mJobIndex = GlobalVars.SelectedUnitJobIndex;
            this.mEnhanceEquipData = new EnhanceEquipData();
            this.mEnhanceMaterials = new List<EnhanceMaterial>(this.mMaterialItems.Count);
            this.mEnableEnhanceMaterials = new List<EnhanceMaterial>(this.mMaterialItems.Count);
            this.mEnhanceParameters = new List<GameObject>(5);
            this.mSelectedEquipItem = null;
            this.mSelectedMaterialItem = null;
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0174;
            }
            if (this.ItemTemplate.get_activeInHierarchy() == null)
            {
                goto Label_0174;
            }
            this.ItemTemplate.get_transform().SetSiblingIndex(0);
            this.ItemTemplate.SetActive(0);
        Label_0174:
            if ((this.BtnJob != null) == null)
            {
                goto Label_01A1;
            }
            this.BtnJob.get_onClick().AddListener(new UnityAction(this, this.OnJobChange));
        Label_01A1:
            if ((this.BtnAdd != null) == null)
            {
                goto Label_01CE;
            }
            this.BtnAdd.get_onClick().AddListener(new UnityAction(this, this.OnAddMaterial));
        Label_01CE:
            if ((this.BtnSub != null) == null)
            {
                goto Label_01FB;
            }
            this.BtnSub.get_onClick().AddListener(new UnityAction(this, this.OnSubMaterial));
        Label_01FB:
            if ((this.BtnEnhance != null) == null)
            {
                goto Label_0228;
            }
            this.BtnEnhance.get_onClick().AddListener(new UnityAction(this, this.OnEnhance));
        Label_0228:
            if ((this.TxtComment != null) == null)
            {
                goto Label_024E;
            }
            this.TxtComment.set_text(LocalizedText.Get("sys.ENHANCE_EQUIPMENT_MESSAGE"));
        Label_024E:
            if ((this.TxtDisableEnhanceOnGauge != null) == null)
            {
                goto Label_0274;
            }
            this.TxtDisableEnhanceOnGauge.set_text(LocalizedText.Get("sys.DIABLE_ENHANCE_MESSAGE"));
        Label_0274:
            num2 = 0;
            goto Label_02B5;
        Label_027B:
            storeyc = new <Start>c__AnonStorey32C();
            storeyc.<>f__this = this;
            storeyc.slot = num2;
            this.Equipments[num2].get_onClick().AddListener(new UnityAction(storeyc, this.<>m__2F4));
            num2 += 1;
        Label_02B5:
            if (num2 < this.Equipments.Count)
            {
                goto Label_027B;
            }
            this.RefreshData();
            return;
        }

        protected override void Update()
        {
            base.Update();
            return;
        }

        public override RectTransform ListParent
        {
            get
            {
                return (((this.ItemLayoutParent != null) == null) ? null : this.ItemLayoutParent.GetComponent<RectTransform>());
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshData>c__AnonStorey32D
        {
            internal ItemData item;

            public <RefreshData>c__AnonStorey32D()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2F5(EnhanceMaterial p)
            {
                return (p.item == this.item);
            }
        }

        [CompilerGenerated]
        private sealed class <Start>c__AnonStorey32C
        {
            internal int slot;
            internal EnhanceEquipDetailWindow <>f__this;

            public <Start>c__AnonStorey32C()
            {
                base..ctor();
                return;
            }

            internal void <>m__2F4()
            {
                this.<>f__this.OnSelectEquipment(this.slot);
                return;
            }
        }
    }
}

