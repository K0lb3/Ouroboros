namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(5, "閉じる", 0, 4), Pin(0x66, "取消：完了", 1, 0x66), Pin(4, "全ジョブ設定", 0, 4), Pin(1, "選択確認：決定", 0, 1), Pin(3, "外す", 0, 3), Pin(2, "選択確認：取消", 0, 2), Pin(0x69, "閉じる：完了", 1, 0x69), Pin(0x68, "全ジョブ設定：完了", 1, 0x68), Pin(0x67, "外す：完了", 1, 0x67), Pin(0x65, "決定：完了", 1, 0x65)]
    public class UnitSkinSelectWindow : SRPG_ListBase, IFlowInterface
    {
        public GameObject ListItemTemplate;
        public GameObject SelectConfirmTemplate;
        public GameObject SettingOverlay;
        public GameObject PointingOverlay;
        public SkinSelectEvent OnSkinSelect;
        public SkinDecideEvent OnSkinDecide;
        public SkinDecideEvent OnSkinDecideAll;
        public GameObject RemoveButton;
        public Text RemoveName;
        public SkinRemoveEvent OnSkinRemove;
        public SkinRemoveEvent OnSkinRemoveAll;
        public SRPG_Button DecideButton;
        public SkinSelectEvent OnDecide;
        public SkinCloseEvent OnSkinClose;
        public bool IsViewOnly;
        private UnitData mCurrentUnit;
        private GameObject mPointingItem;
        private GameObject mDecidedItem;
        private List<GameObject> mSkins;
        private ArtifactParam mConfirmSkin;
        private GameObject mDecidedOverlay;
        private GameObject mPointingOverlay;
        public Text TitleSkinName;
        public Text TitleSkinDesc;

        public UnitSkinSelectWindow()
        {
            this.mSkins = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            ArtifactParam param;
            int num;
            num = pinID;
            switch ((num - 1))
            {
                case 0:
                    goto Label_0023;

                case 1:
                    goto Label_0094;

                case 2:
                    goto Label_00A0;

                case 3:
                    goto Label_00DF;

                case 4:
                    goto Label_0195;
            }
            goto Label_01BF;
        Label_0023:
            if (this.mCurrentUnit == null)
            {
                goto Label_0088;
            }
            if (this.mConfirmSkin == null)
            {
                goto Label_0088;
            }
            if (string.IsNullOrEmpty(this.mConfirmSkin.iname) != null)
            {
                goto Label_0088;
            }
            this.mCurrentUnit.SetJobSkin(this.mConfirmSkin.iname, -1, 1);
            this.Refresh();
            if (this.OnSkinDecide == null)
            {
                goto Label_0088;
            }
            this.OnSkinDecide(this.mConfirmSkin);
        Label_0088:
            this.mConfirmSkin = null;
            goto Label_01BF;
        Label_0094:
            this.mConfirmSkin = null;
            goto Label_01BF;
        Label_00A0:
            if (this.mCurrentUnit == null)
            {
                goto Label_00D3;
            }
            this.mCurrentUnit.ResetJobSkin(-1);
            this.Refresh();
            if (this.OnSkinRemove == null)
            {
                goto Label_00D3;
            }
            this.OnSkinRemove();
        Label_00D3:
            this.mConfirmSkin = null;
            goto Label_01BF;
        Label_00DF:
            param = DataSource.FindDataOfClass<ArtifactParam>(this.mPointingItem, null);
            if (this.mCurrentUnit == null)
            {
                goto Label_0181;
            }
            if (param == null)
            {
                goto Label_0123;
            }
            if (string.IsNullOrEmpty(param.iname) != null)
            {
                goto Label_0123;
            }
            this.mCurrentUnit.SetJobSkinAll(param.iname);
            goto Label_012F;
        Label_0123:
            this.mCurrentUnit.SetJobSkinAll(null);
        Label_012F:
            this.SetPointingOverLay(this.mPointingItem);
            this.SetDecidedOverlay(this.mPointingItem);
            this.DecideButtonInteractive(0);
            this.Refresh();
            if (this.OnSkinDecideAll == null)
            {
                goto Label_016B;
            }
            this.OnSkinDecideAll(param);
        Label_016B:
            if (this.OnSkinClose == null)
            {
                goto Label_0181;
            }
            this.OnSkinClose();
        Label_0181:
            this.mConfirmSkin = null;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x68);
            goto Label_01BF;
        Label_0195:
            if (this.OnSkinClose == null)
            {
                goto Label_01AB;
            }
            this.OnSkinClose();
        Label_01AB:
            this.mConfirmSkin = null;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x69);
        Label_01BF:
            return;
        }

        private void DecideButtonInteractive(bool interactive)
        {
            SRPG_Button button;
            button = this.DecideButton.get_gameObject().GetComponent<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_0024;
            }
            button.set_interactable(interactive);
        Label_0024:
            return;
        }

        private ArtifactParam[] MergeSkinArray(ArtifactParam[] array1, ArtifactParam[] array2)
        {
            ArtifactParam[] paramArray;
            paramArray = new ArtifactParam[((int) array1.Length) + ((int) array2.Length)];
            array1.CopyTo(paramArray, 0);
            array2.CopyTo(paramArray, (int) array1.Length);
            return paramArray;
        }

        public void OnClose()
        {
            this.mConfirmSkin = null;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x68);
            return;
        }

        protected override void OnDestroy()
        {
            if (this.OnSkinSelect == null)
            {
                goto Label_0052;
            }
            if (this.OnSkinSelect.Target == null)
            {
                goto Label_0052;
            }
            if ((this.OnSkinSelect.Target as Object) == null)
            {
                goto Label_0052;
            }
            if (this.OnSkinSelect.Target.Equals(null) != null)
            {
                goto Label_0052;
            }
            this.OnSelectAll(this.mDecidedItem);
        Label_0052:
            return;
        }

        private void OnRemoveAll(GameObject go)
        {
            if ((this.mPointingOverlay != null) == null)
            {
                goto Label_0049;
            }
            this.mPointingOverlay.get_transform().SetParent(go.get_transform(), 0);
            this.mPointingOverlay.get_transform().SetAsLastSibling();
            this.mPointingOverlay.get_gameObject().SetActive(1);
        Label_0049:
            this.mPointingItem = go;
            return;
        }

        private void OnRemoveSelect(GameObject go)
        {
            this.Activated(3);
            return;
        }

        private void OnSelect(GameObject go)
        {
            if ((this.mPointingOverlay != null) == null)
            {
                goto Label_0049;
            }
            this.mPointingOverlay.get_gameObject().SetActive(1);
            this.mPointingOverlay.get_transform().SetParent(go.get_transform(), 0);
            this.mPointingOverlay.get_transform().SetAsLastSibling();
        Label_0049:
            return;
        }

        private void OnSelectAll(GameObject go)
        {
            ArtifactParam param;
            if ((this.mDecidedItem == go) == null)
            {
                goto Label_001D;
            }
            this.DecideButtonInteractive(0);
            goto Label_002C;
        Label_001D:
            this.DecideButtonInteractive(this.IsViewOnly == 0);
        Label_002C:
            if ((this.mPointingItem == go) == null)
            {
                goto Label_003E;
            }
            return;
        Label_003E:
            param = DataSource.FindDataOfClass<ArtifactParam>(go, null);
            if (param == null)
            {
                goto Label_006E;
            }
            this.TitleSkinName.set_text(param.name);
            this.TitleSkinDesc.set_text(param.Expr);
        Label_006E:
            if (this.OnSkinSelect == null)
            {
                goto Label_0085;
            }
            this.OnSkinSelect(param);
        Label_0085:
            this.SetPointingOverLay(go);
            if ((this.mDecidedItem == go) == null)
            {
                goto Label_00A9;
            }
            this.SetDecidedOverlay(this.mDecidedItem);
        Label_00A9:
            this.mPointingItem = go;
            return;
        }

        private void Refresh()
        {
            if (this.mCurrentUnit == null)
            {
                goto Label_0038;
            }
            if (this.mCurrentUnit.UnitParam.skins == null)
            {
                goto Label_0038;
            }
            if (((int) this.mCurrentUnit.UnitParam.skins.Length) >= 1)
            {
                goto Label_0039;
            }
        Label_0038:
            return;
        Label_0039:
            return;
        }

        private void SetActiveListItem(GameObject listItem, bool active)
        {
            UnitSkinListItem item;
            item = listItem.GetComponent<UnitSkinListItem>();
            item.Button.set_interactable(active);
            if ((item.Lock != null) == null)
            {
                goto Label_0033;
            }
            item.Lock.SetActive(active == 0);
        Label_0033:
            return;
        }

        private void SetDecidedOverlay(GameObject parent)
        {
            if ((this.mDecidedOverlay != null) == null)
            {
                goto Label_0044;
            }
            this.mDecidedOverlay.SetActive(1);
            this.mDecidedOverlay.get_transform().SetParent(parent.get_transform(), 0);
            this.mDecidedOverlay.get_transform().SetAsLastSibling();
        Label_0044:
            this.mDecidedItem = parent;
            return;
        }

        private void SetPointingOverLay(GameObject parent)
        {
            if ((this.mPointingOverlay != null) == null)
            {
                goto Label_0044;
            }
            this.mPointingOverlay.SetActive(1);
            this.mPointingOverlay.get_transform().SetParent(parent.get_transform(), 0);
            this.mPointingOverlay.get_transform().SetAsLastSibling();
        Label_0044:
            this.mPointingItem = parent;
            return;
        }

        protected override void Start()
        {
            object[] objArray1;
            ArtifactParam[] paramArray;
            ArtifactParam[] paramArray2;
            ArtifactParam param;
            ArtifactParam[] paramArray3;
            UnitData data;
            UnitSkinListItem item;
            int num;
            GameObject obj2;
            bool flag;
            UnitData data2;
            UnitSkinListItem item2;
            <Start>c__AnonStorey3D9 storeyd;
            base.Start();
            if ((this.ListItemTemplate != null) == null)
            {
                goto Label_0023;
            }
            this.ListItemTemplate.SetActive(0);
        Label_0023:
            this.mCurrentUnit = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if ((this.SettingOverlay != null) == null)
            {
                goto Label_007F;
            }
            this.mDecidedOverlay = this.SettingOverlay;
            this.mDecidedOverlay.get_transform().SetParent(this.SettingOverlay.get_transform().get_parent(), 0);
            this.mDecidedOverlay.SetActive(0);
        Label_007F:
            if ((this.PointingOverlay != null) == null)
            {
                goto Label_00C9;
            }
            this.mPointingOverlay = this.PointingOverlay;
            this.mPointingOverlay.get_transform().SetParent(this.SettingOverlay.get_transform().get_parent(), 0);
            this.mPointingOverlay.SetActive(0);
        Label_00C9:
            this.DecideButtonInteractive(0);
            paramArray = this.mCurrentUnit.GetAllSkins(-1);
            paramArray2 = this.mCurrentUnit.GetSelectableSkins(-1);
            param = this.mCurrentUnit.GetSelectedSkinData(-1);
            paramArray3 = this.mCurrentUnit.GetEnableConceptCardSkins(-1);
            data = new UnitData();
            data.Setup(this.mCurrentUnit);
            data.ResetJobSkinAll();
            DataSource.Bind<UnitData>(this.RemoveButton, data);
            item = this.RemoveButton.GetComponent<UnitSkinListItem>();
            if ((item != null) == null)
            {
                goto Label_016C;
            }
            item.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
            item.OnSelectAll = new ListItemEvents.ListItemEvent(this.OnSelectAll);
        Label_016C:
            if (param != null)
            {
                goto Label_018A;
            }
            this.SetPointingOverLay(this.RemoveButton);
            this.SetDecidedOverlay(this.RemoveButton);
        Label_018A:
            objArray1 = new object[] { this.mCurrentUnit.UnitParam.name };
            this.RemoveName.set_text(LocalizedText.Get("sys.UNITLIST_SKIN_DEFAULT_NAME", objArray1));
            Array.Reverse(paramArray);
            paramArray = this.MergeSkinArray(paramArray, paramArray3);
            paramArray2 = this.MergeSkinArray(paramArray2, paramArray3);
            num = 0;
            goto Label_0310;
        Label_01D8:
            storeyd = new <Start>c__AnonStorey3D9();
            storeyd.skin = paramArray[num];
            obj2 = Object.Instantiate<GameObject>(this.ListItemTemplate);
            obj2.SetActive(1);
            obj2.get_transform().SetParent(base.get_gameObject().get_transform(), 0);
            DataSource.Bind<ArtifactParam>(obj2, storeyd.skin);
            flag = 1;
            if (paramArray2 == null)
            {
                goto Label_0246;
            }
            if (Array.Find<ArtifactParam>(paramArray2, new Predicate<ArtifactParam>(storeyd.<>m__47B)) != null)
            {
                goto Label_0249;
            }
        Label_0246:
            flag = 0;
        Label_0249:
            this.SetActiveListItem(obj2, flag);
            data2 = new UnitData();
            data2.Setup(this.mCurrentUnit);
            data2.SetJobSkin(storeyd.skin.iname, this.mCurrentUnit.JobIndex, 0);
            DataSource.Bind<UnitData>(obj2, data2);
            item2 = obj2.GetComponent<UnitSkinListItem>();
            if ((item2 != null) == null)
            {
                goto Label_02FD;
            }
            item2.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
            item2.OnSelectAll = new ListItemEvents.ListItemEvent(this.OnSelectAll);
            if (param == null)
            {
                goto Label_02FD;
            }
            if ((storeyd.skin.iname == param.iname) == null)
            {
                goto Label_02FD;
            }
            this.SetPointingOverLay(obj2);
            this.SetDecidedOverlay(obj2);
        Label_02FD:
            this.mSkins.Add(obj2);
            num += 1;
        Label_0310:
            if (num < ((int) paramArray.Length))
            {
                goto Label_01D8;
            }
            return;
        }

        [CompilerGenerated]
        private sealed class <Start>c__AnonStorey3D9
        {
            internal ArtifactParam skin;

            public <Start>c__AnonStorey3D9()
            {
                base..ctor();
                return;
            }

            internal bool <>m__47B(ArtifactParam s)
            {
                return (s.iname == this.skin.iname);
            }
        }

        public delegate void SkinCloseEvent();

        public delegate void SkinDecideEvent(ArtifactParam artifact);

        public delegate void SkinRemoveEvent();

        public delegate void SkinSelectEvent(ArtifactParam artifact);
    }
}

