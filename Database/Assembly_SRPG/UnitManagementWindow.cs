namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(1, "初期化開始", 0, 10), Pin(12, "限界突破画面へ戻る", 1, 120), Pin(11, "キャラクタークエスト画面へ戻る", 1, 110), Pin(10, "強化画面へ戻る", 1, 100), Pin(13, "進化ウィンドウへ戻る", 1, 130), Pin(14, "錬成画面へ戻る", 1, 140), Pin(15, "真理開眼ウィンドウへ戻る", 1, 150), Pin(2, "ユニット選択へ", 1, 20), Pin(510, "ユニット選択へ戻る", 1, 510), Pin(500, "ユニット強化", 0, 500)]
    public class UnitManagementWindow : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private string PreviewParentID;
        [SerializeField]
        private string PreviewBaseID;
        [SerializeField]
        private string UnitDetailPreviewPosID;
        [SerializeField]
        private string UnitDetailPreviewBasePosID;
        [SerializeField]
        private string UnitTobiraPreviewPosID;
        [SerializeField]
        private string UnitTobiraPreviewBasePosID;
        [SerializeField]
        public string PATH_UNIT_LIST_WINDOW;
        [SerializeField]
        public string PATH_UNIT_EQUIPMENT_WINDOW;
        [SerializeField]
        public string PATH_UNIT_KAKERA_WINDOW;
        [SerializeField]
        public string PATH_UNIT_INVENTORY_WINDOW;
        [SerializeField]
        public string PATH_TOBIRA_INVENTORY_WINDOW;
        private bool mInitialize;
        private UnitListWindow mUnitListWindow;
        private UnitEnhanceV3 mUEMain;
        private UnitEquipmentWindow mEquipWindow;
        private UnitKakeraWindow mKakeraWindow;
        private LoadRequest mReqAbilityDetail;
        private LoadRequest mUnitInventoryWindowLoadRequest;
        private UnitListRootWindow.Tab mCurrentTab;
        private Vector2 mCurrentTabAnchorePos;
        private long mCurrentUnit;
        private Transform mUnitModelPreviewParent;
        private Transform mUnitModelPreviewBase;
        private QuestDropParam mQuestDropParam;
        private static UnitManagementWindow instance;

        public UnitManagementWindow()
        {
            this.PreviewParentID = "UNITPREVIEW";
            this.PreviewBaseID = "UNITPREVIEWBASE";
            this.UnitDetailPreviewPosID = "UNITDETAIL_PREVIEW_POS";
            this.UnitDetailPreviewBasePosID = "UNITDETAIL_PREVIEWBASE_POS";
            this.UnitTobiraPreviewPosID = "UNITTOBIRA_PREVIEW_POS";
            this.UnitTobiraPreviewBasePosID = "UNITTOBIRA_PREVIEWBASE_POS";
            this.PATH_UNIT_LIST_WINDOW = "UI/UnitListWindow";
            this.PATH_UNIT_EQUIPMENT_WINDOW = "UI/UnitEquipmentWindow2";
            this.PATH_UNIT_KAKERA_WINDOW = "UI/UnitKakeraWindow";
            this.PATH_UNIT_INVENTORY_WINDOW = "UI/UnitInventoryWindow2";
            this.PATH_TOBIRA_INVENTORY_WINDOW = "UI/TobiraInventoryWindow";
            this.mCurrentTabAnchorePos = Vector2.get_zero();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0013;
            }
            this.mInitialize = 1;
            goto Label_002E;
        Label_0013:
            if (pinID != 500)
            {
                goto Label_002E;
            }
            this.OnUnitSelect(GlobalVars.SelectedUnitUniqueID.Get());
        Label_002E:
            return;
        }

        private void Awake()
        {
            instance = this;
            return;
        }

        public void ChangeUnitPreviewPos(bool is_unit_detail)
        {
            string str;
            string str2;
            GameObject obj2;
            GameObject obj3;
            if (((this.mUnitModelPreviewParent == null) == null) && ((this.mUnitModelPreviewBase == null) == null))
            {
                goto Label_0023;
            }
            return;
        Label_0023:
            str = (is_unit_detail == null) ? this.UnitTobiraPreviewPosID : this.UnitDetailPreviewPosID;
            str2 = (is_unit_detail == null) ? this.UnitTobiraPreviewBasePosID : this.UnitDetailPreviewBasePosID;
            obj2 = GameObjectID.FindGameObject(str);
            obj3 = GameObjectID.FindGameObject(str2);
            if ((obj2 != null) == null)
            {
                goto Label_00AF;
            }
            if ((obj3 != null) == null)
            {
                goto Label_00AF;
            }
            this.mUnitModelPreviewParent.get_transform().set_localPosition(obj2.get_transform().get_localPosition());
            this.mUnitModelPreviewBase.get_transform().set_localPosition(obj3.get_transform().get_localPosition());
        Label_00AF:
            return;
        }

        [DebuggerHidden]
        private IEnumerator CreateUnitInventoryWindow()
        {
            <CreateUnitInventoryWindow>c__Iterator16D iteratord;
            iteratord = new <CreateUnitInventoryWindow>c__Iterator16D();
            iteratord.<>f__this = this;
            return iteratord;
        }

        public void CreateUnitList()
        {
            GameObject obj2;
            GameObject obj3;
            if ((this.mUnitListWindow == null) == null)
            {
                goto Label_0053;
            }
            obj2 = AssetManager.Load<GameObject>(this.PATH_UNIT_LIST_WINDOW);
            if ((obj2 != null) == null)
            {
                goto Label_0053;
            }
            obj3 = Object.Instantiate<GameObject>(obj2);
            this.mUnitListWindow = obj3.GetComponent<UnitListWindow>();
            this.mUnitListWindow.get_transform().SetParent(base.get_transform(), 0);
        Label_0053:
            return;
        }

        public void DestroyUnitList()
        {
            if ((this.mUnitListWindow != null) == null)
            {
                goto Label_0028;
            }
            Object.Destroy(this.mUnitListWindow.get_gameObject());
            this.mUnitListWindow = null;
        Label_0028:
            return;
        }

        private List<long> GetDefaultUnitList(long selectUniq)
        {
            List<long> list;
            List<UnitData> list2;
            List<UnitListWindow.Data> list3;
            int num;
            UnitListWindow.Data data;
            int num2;
            list = new List<long>();
            if ((this.mUEMain != null) == null)
            {
                goto Label_0103;
            }
            list2 = MonoSingleton<GameManager>.Instance.Player.Units;
            list3 = new List<UnitListWindow.Data>();
            num = 0;
            goto Label_0076;
        Label_0034:
            if (list2[num] == null)
            {
                goto Label_0072;
            }
            data = new UnitListWindow.Data(list2[num]);
            data.Refresh();
            if (data.GetUniq() != selectUniq)
            {
                goto Label_006A;
            }
            data.filterMask = 0;
        Label_006A:
            list3.Add(data);
        Label_0072:
            num += 1;
        Label_0076:
            if (num < list2.Count)
            {
                goto Label_0034;
            }
            if ((this.mUnitListWindow != null) == null)
            {
                goto Label_00D5;
            }
            if (this.mUnitListWindow.filterWindow == null)
            {
                goto Label_00B4;
            }
            this.mUnitListWindow.filterWindow.CalcUnit(list3);
        Label_00B4:
            if (this.mUnitListWindow.sortWindow == null)
            {
                goto Label_00D5;
            }
            this.mUnitListWindow.sortWindow.CalcUnit(list3);
        Label_00D5:
            num2 = 0;
            goto Label_00F6;
        Label_00DD:
            list.Add(list3[num2].GetUniq());
            num2 += 1;
        Label_00F6:
            if (num2 < list3.Count)
            {
                goto Label_00DD;
            }
        Label_0103:
            return list;
        }

        private int GetOutputPinByRestorePoint(RestorePoints rp)
        {
            int num;
            Dictionary<RestorePoints, int> dictionary;
            Dictionary<RestorePoints, int> dictionary2;
            num = 2;
            dictionary2 = new Dictionary<RestorePoints, int>();
            dictionary2.Add(3, 10);
            dictionary2.Add(6, 11);
            dictionary2.Add(8, 12);
            dictionary2.Add(13, 13);
            dictionary2.Add(4, 14);
            dictionary2.Add(0x10, 15);
            dictionary = dictionary2;
            if (dictionary.ContainsKey(rp) == null)
            {
                goto Label_0056;
            }
            num = dictionary[rp];
        Label_0056:
            return num;
        }

        [DebuggerHidden]
        private IEnumerator InitializeUnitCharacterQuest()
        {
            <InitializeUnitCharacterQuest>c__Iterator170 iterator;
            iterator = new <InitializeUnitCharacterQuest>c__Iterator170();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator InitializeUnitEnhanceTobira()
        {
            <InitializeUnitEnhanceTobira>c__Iterator175 iterator;
            iterator = new <InitializeUnitEnhanceTobira>c__Iterator175();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator InitializeUnitEvolution()
        {
            <InitializeUnitEvolution>c__Iterator172 iterator;
            iterator = new <InitializeUnitEvolution>c__Iterator172();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator InitializeUnitKakera()
        {
            <InitializeUnitKakera>c__Iterator171 iterator;
            iterator = new <InitializeUnitKakera>c__Iterator171();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator InitializeUnitKyouka()
        {
            <InitializeUnitKyouka>c__Iterator16F iteratorf;
            iteratorf = new <InitializeUnitKyouka>c__Iterator16F();
            iteratorf.<>f__this = this;
            return iteratorf;
        }

        [DebuggerHidden]
        private IEnumerator InitializeUnitUnlock()
        {
            <InitializeUnitUnlock>c__Iterator173 iterator;
            iterator = new <InitializeUnitUnlock>c__Iterator173();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator InitializeUnitUnlockTobira()
        {
            <InitializeUnitUnlockTobira>c__Iterator174 iterator;
            iterator = new <InitializeUnitUnlockTobira>c__Iterator174();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator InitUnitInventoryWindow(UnitListRootWindow window, long unique_id)
        {
            <InitUnitInventoryWindow>c__Iterator16E iteratore;
            iteratore = new <InitUnitInventoryWindow>c__Iterator16E();
            iteratore.window = window;
            iteratore.unique_id = unique_id;
            iteratore.<$>window = window;
            iteratore.<$>unique_id = unique_id;
            iteratore.<>f__this = this;
            return iteratore;
        }

        private void InitUnitModelPreviewBase()
        {
            if (string.IsNullOrEmpty(this.PreviewParentID) != null)
            {
                goto Label_0021;
            }
            this.mUnitModelPreviewParent = GameObjectID.FindGameObject<Transform>(this.PreviewParentID);
        Label_0021:
            if (string.IsNullOrEmpty(this.PreviewBaseID) != null)
            {
                goto Label_0064;
            }
            this.mUnitModelPreviewBase = GameObjectID.FindGameObject<Transform>(this.PreviewBaseID);
            if ((this.mUnitModelPreviewBase != null) == null)
            {
                goto Label_0064;
            }
            this.mUnitModelPreviewBase.get_gameObject().SetActive(0);
        Label_0064:
            return;
        }

        private void OnDestroy()
        {
            this.Release();
            return;
        }

        private void OnUEWindowClosedByUser()
        {
            UnitListRootWindow.TabRegister register;
            string str;
            long num;
            string str2;
            long num2;
            SerializeValueList list;
            this.mUEMain.OnUserClose = null;
            if (((int) this.mUEMain.GetDirtyUnits().Length) <= 0)
            {
                goto Label_002A;
            }
            this.mUEMain.ClearDirtyUnits();
        Label_002A:
            register = new UnitListRootWindow.TabRegister();
            if (this.mCurrentTab == null)
            {
                goto Label_009F;
            }
            register.tab = this.mCurrentTab;
            register.anchorePos = this.mCurrentTabAnchorePos;
            str = FlowNode_Variable.Get("LAST_SELECTED_UNITID");
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_00F8;
            }
            num = long.Parse(str);
            if (num <= 0L)
            {
                goto Label_008B;
            }
            if (this.mCurrentUnit == num)
            {
                goto Label_008B;
            }
            register.forcus = num;
        Label_008B:
            FlowNode_Variable.Set("LAST_SELECTED_UNITID", string.Empty);
            goto Label_00F8;
        Label_009F:
            register.tab = 0xffff;
            register.forcus = GlobalVars.SelectedUnitUniqueID.Get();
            str2 = FlowNode_Variable.Get("LAST_SELECTED_UNITID");
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_00F8;
            }
            num2 = long.Parse(str2);
            if (num2 <= 0L)
            {
                goto Label_00E9;
            }
            register.forcus = num2;
        Label_00E9:
            FlowNode_Variable.Set("LAST_SELECTED_UNITID", string.Empty);
        Label_00F8:
            list = new SerializeValueList();
            list.AddObject("data_register", register);
            FlowNode_ButtonEvent.currentValue = list;
            if ((this.mUnitListWindow == null) == null)
            {
                goto Label_012B;
            }
            this.CreateUnitList();
        Label_012B:
            this.mUnitListWindow.Enabled(1);
            FlowNode_GameObject.ActivateOutputLinks(this, 510);
            return;
        }

        private void OnUnitSelect(long uniqueID)
        {
            UnitListRootWindow window;
            if ((this.mUnitListWindow == null) != null)
            {
                goto Label_0021;
            }
            if (this.mUnitListWindow.IsEnabled() != null)
            {
                goto Label_0022;
            }
        Label_0021:
            return;
        Label_0022:
            window = this.mUnitListWindow.rootWindow;
            if (window == null)
            {
                goto Label_004E;
            }
            if (window.GetEditType() != null)
            {
                goto Label_004E;
            }
            base.StartCoroutine(this.InitUnitInventoryWindow(window, uniqueID));
        Label_004E:
            return;
        }

        private void Release()
        {
            this.DestroyUnitList();
            if ((this.mUEMain != null) == null)
            {
                goto Label_002E;
            }
            Object.Destroy(this.mUEMain.get_gameObject());
            this.mUEMain = null;
        Label_002E:
            if ((this.mEquipWindow != null) == null)
            {
                goto Label_0056;
            }
            Object.Destroy(this.mEquipWindow.get_gameObject());
            this.mEquipWindow = null;
        Label_0056:
            if ((this.mKakeraWindow != null) == null)
            {
                goto Label_007E;
            }
            Object.Destroy(this.mKakeraWindow.get_gameObject());
            this.mKakeraWindow = null;
        Label_007E:
            return;
        }

        [DebuggerHidden]
        private IEnumerator SetupByRestorePoint(RestorePoints restore_point)
        {
            <SetupByRestorePoint>c__Iterator16C iteratorc;
            iteratorc = new <SetupByRestorePoint>c__Iterator16C();
            iteratorc.restore_point = restore_point;
            iteratorc.<$>restore_point = restore_point;
            iteratorc.<>f__this = this;
            return iteratorc;
        }

        [DebuggerHidden]
        private IEnumerator Start()
        {
            <Start>c__Iterator16B iteratorb;
            iteratorb = new <Start>c__Iterator16B();
            iteratorb.<>f__this = this;
            return iteratorb;
        }

        public static UnitManagementWindow Instance
        {
            get
            {
                return instance;
            }
        }

        public Transform UnitModelPreviewParent
        {
            get
            {
                return this.mUnitModelPreviewParent;
            }
        }

        public Transform UnitModelPreviewBase
        {
            get
            {
                return this.mUnitModelPreviewBase;
            }
        }

        [CompilerGenerated]
        private sealed class <CreateUnitInventoryWindow>c__Iterator16D : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <obj>__0;
            internal int $PC;
            internal object $current;
            internal UnitManagementWindow <>f__this;

            public <CreateUnitInventoryWindow>c__Iterator16D()
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
                        goto Label_0025;

                    case 1:
                        goto Label_008D;

                    case 2:
                        goto Label_013C;
                }
                goto Label_0143;
            Label_0025:
                if ((this.<>f__this.mUEMain == null) == null)
                {
                    goto Label_0114;
                }
                if (this.<>f__this.mUnitInventoryWindowLoadRequest != null)
                {
                    goto Label_008D;
                }
                this.<>f__this.mUnitInventoryWindowLoadRequest = AssetManager.LoadAsync<GameObject>(this.<>f__this.PATH_UNIT_INVENTORY_WINDOW);
                goto Label_008D;
            Label_006B:
                this.$current = this.<>f__this.mUnitInventoryWindowLoadRequest.StartCoroutine();
                this.$PC = 1;
                goto Label_0145;
            Label_008D:
                if (this.<>f__this.mUnitInventoryWindowLoadRequest.isDone == null)
                {
                    goto Label_006B;
                }
                if ((this.<>f__this.mUnitInventoryWindowLoadRequest.asset != null) == null)
                {
                    goto Label_0114;
                }
                this.<obj>__0 = Object.Instantiate(this.<>f__this.mUnitInventoryWindowLoadRequest.asset) as GameObject;
                this.<>f__this.mUEMain = this.<obj>__0.GetComponent<UnitEnhanceV3>();
                this.<>f__this.mUEMain.get_transform().SetParent(this.<>f__this.get_transform(), 0);
            Label_0114:
                if (this.<>f__this.mUEMain.HasStarted != null)
                {
                    goto Label_013C;
                }
                this.$current = null;
                this.$PC = 2;
                goto Label_0145;
            Label_013C:
                this.$PC = -1;
            Label_0143:
                return 0;
            Label_0145:
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
        private sealed class <InitializeUnitCharacterQuest>c__Iterator170 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitManagementWindow <>f__this;

            public <InitializeUnitCharacterQuest>c__Iterator170()
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
                        goto Label_00C5;
                }
                goto Label_011C;
            Label_0021:
                if (GlobalVars.PreBattleUnitUniqueID == null)
                {
                    goto Label_0115;
                }
                this.<>f__this.mUEMain.MuteVoice = 1;
                this.<>f__this.mUEMain.UnitList = this.<>f__this.GetDefaultUnitList(GlobalVars.PreBattleUnitUniqueID);
                this.<>f__this.mUEMain.Refresh(GlobalVars.PreBattleUnitUniqueID, GlobalVars.SelectedJobUniqueID, 1, 1);
                this.<>f__this.mUEMain.OnUserClose = new UnitEnhanceV3.CloseEvent(this.<>f__this.OnUEWindowClosedByUser);
                goto Label_00C5;
            Label_00B2:
                this.$current = null;
                this.$PC = 1;
                goto Label_011E;
            Label_00C5:
                if (this.<>f__this.mUEMain.IsLoading != null)
                {
                    goto Label_00B2;
                }
                if (this.<>f__this.mUEMain.CharaQuestButton.IsActive() == null)
                {
                    goto Label_0104;
                }
                this.<>f__this.mUEMain.OnCharacterQuestRestore();
            Label_0104:
                this.<>f__this.mUEMain.MuteVoice = 0;
            Label_0115:
                this.$PC = -1;
            Label_011C:
                return 0;
            Label_011E:
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
        private sealed class <InitializeUnitEnhanceTobira>c__Iterator175 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitManagementWindow <>f__this;

            public <InitializeUnitEnhanceTobira>c__Iterator175()
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
                        goto Label_00C5;
                }
                goto Label_0102;
            Label_0021:
                if (GlobalVars.PreBattleUnitUniqueID == null)
                {
                    goto Label_00FB;
                }
                this.<>f__this.mUEMain.MuteVoice = 1;
                this.<>f__this.mUEMain.UnitList = this.<>f__this.GetDefaultUnitList(GlobalVars.PreBattleUnitUniqueID);
                this.<>f__this.mUEMain.Refresh(GlobalVars.PreBattleUnitUniqueID, GlobalVars.SelectedJobUniqueID, 1, 1);
                this.<>f__this.mUEMain.OnUserClose = new UnitEnhanceV3.CloseEvent(this.<>f__this.OnUEWindowClosedByUser);
                goto Label_00C5;
            Label_00B2:
                this.$current = null;
                this.$PC = 1;
                goto Label_0104;
            Label_00C5:
                if (this.<>f__this.mUEMain.IsLoading != null)
                {
                    goto Label_00B2;
                }
                this.<>f__this.mUEMain.OnEnhanceTobiraRestore();
                this.<>f__this.mUEMain.MuteVoice = 0;
            Label_00FB:
                this.$PC = -1;
            Label_0102:
                return 0;
            Label_0104:
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
        private sealed class <InitializeUnitEvolution>c__Iterator172 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitManagementWindow <>f__this;

            public <InitializeUnitEvolution>c__Iterator172()
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
                        goto Label_00C5;
                }
                goto Label_0102;
            Label_0021:
                if (GlobalVars.PreBattleUnitUniqueID == null)
                {
                    goto Label_00FB;
                }
                this.<>f__this.mUEMain.MuteVoice = 1;
                this.<>f__this.mUEMain.UnitList = this.<>f__this.GetDefaultUnitList(GlobalVars.PreBattleUnitUniqueID);
                this.<>f__this.mUEMain.Refresh(GlobalVars.PreBattleUnitUniqueID, GlobalVars.SelectedJobUniqueID, 1, 1);
                this.<>f__this.mUEMain.OnUserClose = new UnitEnhanceV3.CloseEvent(this.<>f__this.OnUEWindowClosedByUser);
                goto Label_00C5;
            Label_00B2:
                this.$current = null;
                this.$PC = 1;
                goto Label_0104;
            Label_00C5:
                if (this.<>f__this.mUEMain.IsLoading != null)
                {
                    goto Label_00B2;
                }
                this.<>f__this.mUEMain.OnEvolutionRestore();
                this.<>f__this.mUEMain.MuteVoice = 0;
            Label_00FB:
                this.$PC = -1;
            Label_0102:
                return 0;
            Label_0104:
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
        private sealed class <InitializeUnitKakera>c__Iterator171 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitManagementWindow <>f__this;

            public <InitializeUnitKakera>c__Iterator171()
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
                        goto Label_0025;

                    case 1:
                        goto Label_00B8;

                    case 2:
                        goto Label_00F5;
                }
                goto Label_011B;
            Label_0025:
                if (GlobalVars.PreBattleUnitUniqueID == null)
                {
                    goto Label_0114;
                }
                this.<>f__this.mUEMain.UnitList = this.<>f__this.GetDefaultUnitList(GlobalVars.PreBattleUnitUniqueID);
                this.<>f__this.mUEMain.Refresh(GlobalVars.PreBattleUnitUniqueID, GlobalVars.SelectedJobUniqueID, 1, 1);
                this.<>f__this.mUEMain.OnUserClose = new UnitEnhanceV3.CloseEvent(this.<>f__this.OnUEWindowClosedByUser);
                goto Label_00B8;
            Label_00A5:
                this.$current = null;
                this.$PC = 1;
                goto Label_011D;
            Label_00B8:
                if (this.<>f__this.mUEMain.IsLoading != null)
                {
                    goto Label_00A5;
                }
                this.<>f__this.mUEMain.OpenKakeraWindow();
                goto Label_00F5;
            Label_00E2:
                this.$current = null;
                this.$PC = 2;
                goto Label_011D;
            Label_00F5:
                if (this.<>f__this.mUEMain.KakeraWindow.GetComponent<WindowController>().IsOpened == null)
                {
                    goto Label_00E2;
                }
            Label_0114:
                this.$PC = -1;
            Label_011B:
                return 0;
            Label_011D:
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
        private sealed class <InitializeUnitKyouka>c__Iterator16F : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal long <selected_job_uniqueID>__0;
            internal int $PC;
            internal object $current;
            internal UnitManagementWindow <>f__this;

            public <InitializeUnitKyouka>c__Iterator16F()
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
                        goto Label_0025;

                    case 1:
                        goto Label_00D9;

                    case 2:
                        goto Label_017B;
                }
                goto Label_01B2;
            Label_0025:
                if (GlobalVars.PreBattleUnitUniqueID == null)
                {
                    goto Label_01AB;
                }
                this.<>f__this.mUEMain.MuteVoice = 1;
                this.<>f__this.mUEMain.UnitList = this.<>f__this.GetDefaultUnitList(GlobalVars.PreBattleUnitUniqueID);
                this.<selected_job_uniqueID>__0 = GlobalVars.SelectedJobUniqueID.Get();
                this.<>f__this.mUEMain.Refresh(GlobalVars.PreBattleUnitUniqueID, GlobalVars.SelectedJobUniqueID, 1, 1);
                this.<>f__this.mUEMain.OnUserClose = new UnitEnhanceV3.CloseEvent(this.<>f__this.OnUEWindowClosedByUser);
                goto Label_00D9;
            Label_00C6:
                this.$current = null;
                this.$PC = 1;
                goto Label_01B4;
            Label_00D9:
                if (this.<>f__this.mUEMain.IsLoading != null)
                {
                    goto Label_00C6;
                }
                this.<>f__this.mUEMain.RefreshReturningJobState(this.<selected_job_uniqueID>__0);
                if (GlobalVars.SelectedEquipmentSlot.Get() < 0)
                {
                    goto Label_019A;
                }
                this.<>f__this.mUEMain.OpenEquipmentSlot(GlobalVars.SelectedEquipmentSlot);
                this.<>f__this.mUEMain.EquipmentWindow.SubWindow.SetActive(1);
                this.<>f__this.mUEMain.EquipmentWindow.SetRestorationRecipeItem(GlobalVars.SelectedItemParamTree);
                goto Label_017B;
            Label_0168:
                this.$current = null;
                this.$PC = 2;
                goto Label_01B4;
            Label_017B:
                if (this.<>f__this.mUEMain.EquipmentWindow.GetComponent<WindowController>().IsOpened == null)
                {
                    goto Label_0168;
                }
            Label_019A:
                this.<>f__this.mUEMain.MuteVoice = 0;
            Label_01AB:
                this.$PC = -1;
            Label_01B2:
                return 0;
            Label_01B4:
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
        private sealed class <InitializeUnitUnlock>c__Iterator173 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitManagementWindow <>f__this;

            public <InitializeUnitUnlock>c__Iterator173()
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
                        goto Label_0025;

                    case 1:
                        goto Label_005A;

                    case 2:
                        goto Label_007F;
                }
                goto Label_009C;
            Label_0025:
                if (GlobalVars.UnlockUnitID.Length <= 0)
                {
                    goto Label_0095;
                }
                this.<>f__this.mUnitListWindow.Activate(100);
                this.$current = null;
                this.$PC = 1;
                goto Label_009E;
            Label_005A:
                this.<>f__this.mUnitListWindow.Activate(110);
                this.$current = null;
                this.$PC = 2;
                goto Label_009E;
            Label_007F:
                this.<>f__this.mUnitListWindow.ActivateOutputLinks(0x1a7);
            Label_0095:
                this.$PC = -1;
            Label_009C:
                return 0;
            Label_009E:
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
        private sealed class <InitializeUnitUnlockTobira>c__Iterator174 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitManagementWindow <>f__this;

            public <InitializeUnitUnlockTobira>c__Iterator174()
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
                        goto Label_00C5;
                }
                goto Label_0102;
            Label_0021:
                if (GlobalVars.PreBattleUnitUniqueID == null)
                {
                    goto Label_00FB;
                }
                this.<>f__this.mUEMain.MuteVoice = 1;
                this.<>f__this.mUEMain.UnitList = this.<>f__this.GetDefaultUnitList(GlobalVars.PreBattleUnitUniqueID);
                this.<>f__this.mUEMain.Refresh(GlobalVars.PreBattleUnitUniqueID, GlobalVars.SelectedJobUniqueID, 1, 1);
                this.<>f__this.mUEMain.OnUserClose = new UnitEnhanceV3.CloseEvent(this.<>f__this.OnUEWindowClosedByUser);
                goto Label_00C5;
            Label_00B2:
                this.$current = null;
                this.$PC = 1;
                goto Label_0104;
            Label_00C5:
                if (this.<>f__this.mUEMain.IsLoading != null)
                {
                    goto Label_00B2;
                }
                this.<>f__this.mUEMain.OnUnlockTobiraRestore();
                this.<>f__this.mUEMain.MuteVoice = 0;
            Label_00FB:
                this.$PC = -1;
            Label_0102:
                return 0;
            Label_0104:
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
        private sealed class <InitUnitInventoryWindow>c__Iterator16E : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal bool <bNoList>__0;
            internal UnitListRootWindow window;
            internal UnitListRootWindow.ListData <list>__1;
            internal long unique_id;
            internal List<long> <tmp>__2;
            internal List<UnitData> <units>__3;
            internal int <i>__4;
            internal int $PC;
            internal object $current;
            internal UnitListRootWindow <$>window;
            internal long <$>unique_id;
            internal UnitManagementWindow <>f__this;

            public <InitUnitInventoryWindow>c__Iterator16E()
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
                        goto Label_0025;

                    case 1:
                        goto Label_0048;

                    case 2:
                        goto Label_00A0;
                }
                goto Label_022D;
            Label_0025:
                ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", null);
                goto Label_0048;
            Label_0035:
                this.$current = null;
                this.$PC = 1;
                goto Label_022F;
            Label_0048:
                if ((this.<>f__this.mUnitListWindow != null) == null)
                {
                    goto Label_0078;
                }
                if (this.<>f__this.mUnitListWindow.IsState("closed") == null)
                {
                    goto Label_0035;
                }
            Label_0078:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.CreateUnitInventoryWindow());
                this.$PC = 2;
                goto Label_022F;
            Label_00A0:
                this.<bNoList>__0 = 1;
                if (this.window == null)
                {
                    goto Label_0132;
                }
                this.<list>__1 = this.window.GetListData("unitlist");
                if (this.<list>__1 == null)
                {
                    goto Label_0132;
                }
                this.<>f__this.mCurrentTab = this.window.GetCurrentTab();
                this.<>f__this.mCurrentTabAnchorePos = this.window.GetCurrentTabAnchore();
                this.<>f__this.mCurrentUnit = this.unique_id;
                this.<>f__this.mUEMain.UnitList = this.<list>__1.GetUniqs();
                this.<bNoList>__0 = 0;
            Label_0132:
                if (this.<bNoList>__0 == null)
                {
                    goto Label_01C4;
                }
                this.<tmp>__2 = new List<long>();
                this.<units>__3 = MonoSingleton<GameManager>.Instance.Player.Units;
                this.<i>__4 = 0;
                goto Label_0198;
            Label_0169:
                this.<tmp>__2.Add(this.<units>__3[this.<i>__4].UniqueID);
                this.<i>__4 += 1;
            Label_0198:
                if (this.<i>__4 < this.<units>__3.Count)
                {
                    goto Label_0169;
                }
                this.<>f__this.mUEMain.UnitList = this.<tmp>__2;
            Label_01C4:
                this.<>f__this.mUEMain.Refresh(this.unique_id, 0L, 0, 1);
                this.<>f__this.mUEMain.OnUserClose = new UnitEnhanceV3.CloseEvent(this.<>f__this.OnUEWindowClosedByUser);
                if ((this.<>f__this.mUnitListWindow != null) == null)
                {
                    goto Label_0226;
                }
                this.<>f__this.mUnitListWindow.Enabled(0);
            Label_0226:
                this.$PC = -1;
            Label_022D:
                return 0;
            Label_022F:
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
        private sealed class <SetupByRestorePoint>c__Iterator16C : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal RestorePoints restore_point;
            internal int $PC;
            internal object $current;
            internal RestorePoints <$>restore_point;
            internal UnitManagementWindow <>f__this;

            public <SetupByRestorePoint>c__Iterator16C()
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
                RestorePoints points;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0039;

                    case 1:
                        goto Label_00AA;

                    case 2:
                        goto Label_00D7;

                    case 3:
                        goto Label_0104;

                    case 4:
                        goto Label_0131;

                    case 5:
                        goto Label_015E;

                    case 6:
                        goto Label_018B;

                    case 7:
                        goto Label_01B8;
                }
                goto Label_01C4;
            Label_0039:
                points = this.restore_point;
                switch ((points - 3))
                {
                    case 0:
                        goto Label_0082;

                    case 1:
                        goto Label_0136;

                    case 2:
                        goto Label_0060;

                    case 3:
                        goto Label_00AF;

                    case 4:
                        goto Label_0060;

                    case 5:
                        goto Label_00DC;
                }
            Label_0060:
                switch ((points - 13))
                {
                    case 0:
                        goto Label_0109;

                    case 1:
                        goto Label_01BD;

                    case 2:
                        goto Label_01BD;

                    case 3:
                        goto Label_0163;

                    case 4:
                        goto Label_0190;
                }
                goto Label_01BD;
            Label_0082:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.InitializeUnitKyouka());
                this.$PC = 1;
                goto Label_01C6;
            Label_00AA:
                goto Label_01BD;
            Label_00AF:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.InitializeUnitCharacterQuest());
                this.$PC = 2;
                goto Label_01C6;
            Label_00D7:
                goto Label_01BD;
            Label_00DC:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.InitializeUnitKakera());
                this.$PC = 3;
                goto Label_01C6;
            Label_0104:
                goto Label_01BD;
            Label_0109:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.InitializeUnitEvolution());
                this.$PC = 4;
                goto Label_01C6;
            Label_0131:
                goto Label_01BD;
            Label_0136:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.InitializeUnitUnlock());
                this.$PC = 5;
                goto Label_01C6;
            Label_015E:
                goto Label_01BD;
            Label_0163:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.InitializeUnitUnlockTobira());
                this.$PC = 6;
                goto Label_01C6;
            Label_018B:
                goto Label_01BD;
            Label_0190:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.InitializeUnitEnhanceTobira());
                this.$PC = 7;
                goto Label_01C6;
            Label_01B8:;
            Label_01BD:
                this.$PC = -1;
            Label_01C4:
                return 0;
            Label_01C6:
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
        private sealed class <Start>c__Iterator16B : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal RestorePoints <restore_point>__0;
            internal bool <load_unit_inventory>__1;
            internal string <tutorial_unit_select>__2;
            internal int $PC;
            internal object $current;
            internal UnitManagementWindow <>f__this;

            public <Start>c__Iterator16B()
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
                        goto Label_0056;

                    case 2:
                        goto Label_0148;

                    case 3:
                        goto Label_0175;

                    case 4:
                        goto Label_0235;
                }
                goto Label_023C;
            Label_002D:
                this.<restore_point>__0 = HomeWindow.GetRestorePoint();
                HomeWindow.SetRestorePoint(0);
                goto Label_0056;
            Label_0043:
                this.$current = null;
                this.$PC = 1;
                goto Label_023E;
            Label_0056:
                if (this.<>f__this.mInitialize == null)
                {
                    goto Label_0043;
                }
                this.<>f__this.InitUnitModelPreviewBase();
                this.<load_unit_inventory>__1 = 1;
                if (this.<restore_point>__0 == 3)
                {
                    goto Label_00C3;
                }
                if (this.<restore_point>__0 == 6)
                {
                    goto Label_00C3;
                }
                if (this.<restore_point>__0 == 8)
                {
                    goto Label_00C3;
                }
                if (this.<restore_point>__0 == 13)
                {
                    goto Label_00C3;
                }
                if (this.<restore_point>__0 == 0x10)
                {
                    goto Label_00C3;
                }
                if (this.<restore_point>__0 != 0x11)
                {
                    goto Label_017A;
                }
            Label_00C3:
                this.<load_unit_inventory>__1 = 0;
                this.<tutorial_unit_select>__2 = FlowNode_Variable.Get("TUTORIAL_010003_UNIT_SELECT");
                if (this.<restore_point>__0 != 3)
                {
                    goto Label_014D;
                }
                if (this.<tutorial_unit_select>__2 == null)
                {
                    goto Label_014D;
                }
                if ((this.<tutorial_unit_select>__2 == "1") == null)
                {
                    goto Label_014D;
                }
                FlowNode_Variable.Set("TUTORIAL_010003_UNIT_SELECT", "0");
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.InitUnitInventoryWindow(null, GlobalVars.SelectedUnitUniqueID.Get()));
                this.$PC = 2;
                goto Label_023E;
            Label_0148:
                goto Label_0175;
            Label_014D:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.CreateUnitInventoryWindow());
                this.$PC = 3;
                goto Label_023E;
            Label_0175:
                goto Label_0185;
            Label_017A:
                this.<>f__this.CreateUnitList();
            Label_0185:
                this.<>f__this.StartCoroutine(this.<>f__this.SetupByRestorePoint(this.<restore_point>__0));
                if (string.IsNullOrEmpty(GameSettings.Instance.Dialog_AbilityDetail) != null)
                {
                    goto Label_01E0;
                }
                this.<>f__this.mReqAbilityDetail = AssetManager.LoadAsync<GameObject>(GameSettings.Instance.Dialog_AbilityDetail);
                if (this.<>f__this.mReqAbilityDetail == null)
                {
                    goto Label_01E0;
                }
            Label_01E0:
                if (this.<load_unit_inventory>__1 == null)
                {
                    goto Label_0206;
                }
                this.<>f__this.mUnitInventoryWindowLoadRequest = AssetManager.LoadAsync<GameObject>(this.<>f__this.PATH_UNIT_INVENTORY_WINDOW);
            Label_0206:
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, this.<>f__this.GetOutputPinByRestorePoint(this.<restore_point>__0));
                this.$current = null;
                this.$PC = 4;
                goto Label_023E;
            Label_0235:
                this.$PC = -1;
            Label_023C:
                return 0;
            Label_023E:
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

