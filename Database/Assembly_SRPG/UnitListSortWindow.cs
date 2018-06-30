namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitListSortWindow : FlowWindowBase
    {
        private const string SAVEKEY = "UNITLIST_SORT";
        private const string SAVEKEY_OLD = "UNITLIST";
        public const SelectType MASK_SECTION = 0xffffff;
        public const SelectType MASK_ALIGNMENT = 0xf000000;
        private SerializeParam m_Param;
        private SerializeValueList m_ValueList;
        private UnitListWindow m_Root;
        private Result m_Result;
        private SelectType m_SelectTypeReg;
        private SelectType m_SelectType;
        private Dictionary<SelectType, Toggle> m_Toggles;
        [CompilerGenerated]
        private static Comparison<UnitListWindow.Data> <>f__am$cache7;

        public UnitListSortWindow()
        {
            this.m_Toggles = new Dictionary<SelectType, Toggle>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static unsafe int <CalcUnit>m__477(UnitListWindow.Data p1, UnitListWindow.Data p2)
        {
            int num;
            string str;
            string str2;
            num = &p1.sortPriority.CompareTo(p2.sortPriority);
            if (num != null)
            {
                goto Label_0062;
            }
            str = (p1.param == null) ? string.Empty : p1.param.iname;
            str2 = (p2.param == null) ? string.Empty : p2.param.iname;
            num = str.CompareTo(str2);
        Label_0062:
            return num;
        }

        private void CacheToggleParam(GameObject toggle_parent_obj)
        {
            string[] textArray1;
            Toggle[] toggleArray;
            int num;
            SelectType type;
            ButtonEvent event2;
            ButtonEvent.Event event3;
            Exception exception;
            toggleArray = toggle_parent_obj.GetComponentsInChildren<Toggle>();
            num = 0;
            goto Label_00EC;
        Label_000E:
            try
            {
                type = (int) Enum.Parse(typeof(SelectType), toggleArray[num].get_name());
                event2 = toggleArray[num].GetComponent<ButtonEvent>();
                if ((event2 != null) == null)
                {
                    goto Label_0090;
                }
                event3 = event2.GetEvent("UNITSORT_TGL_CHANGE");
                if (event3 == null)
                {
                    goto Label_0090;
                }
                if ((type & 0xffffff) == null)
                {
                    goto Label_0072;
                }
                event3.valueList.SetField("section", type);
            Label_0072:
                if ((type & 0xf000000) == null)
                {
                    goto Label_0090;
                }
                event3.valueList.SetField("alignment", type);
            Label_0090:
                this.m_Toggles.Add(type, toggleArray[num]);
                goto Label_00E8;
            }
            catch (Exception exception1)
            {
            Label_00A4:
                exception = exception1;
                textArray1 = new string[] { "UnitSortWindow トグル名からSelectTypeを取得できない！ > ", toggleArray[num].get_name(), " ( Exception > ", exception.ToString(), " )" };
                Debug.LogError(string.Concat(textArray1));
                goto Label_00E8;
            }
        Label_00E8:
            num += 1;
        Label_00EC:
            if (num < ((int) toggleArray.Length))
            {
                goto Label_000E;
            }
            return;
        }

        public void CalcUnit(List<UnitListWindow.Data> list)
        {
            SelectType type;
            int num;
            SelectType type2;
            if (this.IsType(1) != null)
            {
                goto Label_005A;
            }
            type = this.GetSection();
            num = 0;
            goto Label_002B;
        Label_001A:
            list[num].RefreshSortPriority(type);
            num += 1;
        Label_002B:
            if (num < list.Count)
            {
                goto Label_001A;
            }
            if (<>f__am$cache7 != null)
            {
                goto Label_0050;
            }
            <>f__am$cache7 = new Comparison<UnitListWindow.Data>(UnitListSortWindow.<CalcUnit>m__477);
        Label_0050:
            SortUtility.StableSort<UnitListWindow.Data>(list, <>f__am$cache7);
        Label_005A:
            if (this.GetAlignment() != 0x2000000)
            {
                goto Label_0072;
            }
            list.Reverse();
        Label_0072:
            return;
        }

        public static SelectType ConvertReverse(bool isReverse)
        {
            if (isReverse == null)
            {
                goto Label_000C;
            }
            return 0x1000000;
        Label_000C:
            return 0x2000000;
        }

        public static GameUtility.UnitSortModes ConvertSelectType(SelectType selectType)
        {
            SelectType type;
            type = selectType;
            switch ((type - 2))
            {
                case 0:
                    goto Label_0094;

                case 1:
                    goto Label_0016;

                case 2:
                    goto Label_0097;
            }
        Label_0016:
            if (type == 8)
            {
                goto Label_0087;
            }
            if (type == 0x10)
            {
                goto Label_007C;
            }
            if (type == 0x20)
            {
                goto Label_007E;
            }
            if (type == 0x40)
            {
                goto Label_0080;
            }
            if (type == 0x80)
            {
                goto Label_0082;
            }
            if (type == 0x100)
            {
                goto Label_0090;
            }
            if (type == 0x200)
            {
                goto Label_0084;
            }
            if (type == 0x400)
            {
                goto Label_008D;
            }
            if (type == 0x800)
            {
                goto Label_0092;
            }
            if (type == 0x1000)
            {
                goto Label_008A;
            }
            goto Label_0099;
        Label_007C:
            return 4;
        Label_007E:
            return 5;
        Label_0080:
            return 6;
        Label_0082:
            return 7;
        Label_0084:
            return 9;
        Label_0087:
            return 10;
        Label_008A:
            return 11;
        Label_008D:
            return 12;
        Label_0090:
            return 3;
        Label_0092:
            return 2;
        Label_0094:
            return 13;
        Label_0097:
            return 1;
        Label_0099:
            return 0;
        }

        public static SelectType ConvertSortMode(GameUtility.UnitSortModes oldMode)
        {
            GameUtility.UnitSortModes modes;
            modes = oldMode;
            switch (modes)
            {
                case 0:
                    goto Label_0045;

                case 1:
                    goto Label_0078;

                case 2:
                    goto Label_0070;

                case 3:
                    goto Label_006A;

                case 4:
                    goto Label_0047;

                case 5:
                    goto Label_004A;

                case 6:
                    goto Label_004D;

                case 7:
                    goto Label_0050;

                case 8:
                    goto Label_007A;

                case 9:
                    goto Label_0056;

                case 10:
                    goto Label_005C;

                case 11:
                    goto Label_005E;

                case 12:
                    goto Label_0064;

                case 13:
                    goto Label_0076;
            }
            goto Label_007A;
        Label_0045:
            return 1;
        Label_0047:
            return 0x10;
        Label_004A:
            return 0x20;
        Label_004D:
            return 0x40;
        Label_0050:
            return 0x80;
        Label_0056:
            return 0x200;
        Label_005C:
            return 8;
        Label_005E:
            return 0x1000;
        Label_0064:
            return 0x400;
        Label_006A:
            return 0x100;
        Label_0070:
            return 0x800;
        Label_0076:
            return 2;
        Label_0078:
            return 4;
        Label_007A:
            return 0;
        }

        private void CreateInstance()
        {
            string str;
            GameObject obj2;
            GameObject obj3;
            if ((base.m_Window != null) == null)
            {
                goto Label_0023;
            }
            if ((base.m_Animator != null) == null)
            {
                goto Label_0023;
            }
            return;
        Label_0023:
            str = this.m_ValueList.GetString("path");
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0040;
            }
            return;
        Label_0040:
            obj2 = AssetManager.Load<GameObject>(str);
            if ((obj2 == null) == null)
            {
                goto Label_0054;
            }
            return;
        Label_0054:
            obj3 = null;
            if ((base.m_Animator == null) == null)
            {
                goto Label_0091;
            }
            obj3 = Object.Instantiate<GameObject>(obj2);
            obj3.get_transform().SetParent(base.m_Window.get_transform(), 0);
            base.m_Animator = obj3.GetComponent<Animator>();
        Label_0091:
            this.CacheToggleParam(obj3);
            return;
        }

        public SelectType GetAlignment()
        {
            return (this.m_SelectType & 0xf000000);
        }

        public static Sprite GetIcon(SelectType selectType)
        {
            return GameSettings.Instance.GetUnitSortModeIcon(ConvertSelectType(selectType));
        }

        public SelectType GetSection()
        {
            return (this.m_SelectType & 0xffffff);
        }

        public SelectType GetSectionReg()
        {
            return (this.m_SelectTypeReg & 0xffffff);
        }

        public static long GetSortPriority(UnitListWindow.Data data, SelectType type)
        {
            UnitParam param;
            UnitData data2;
            SelectType type2;
            if (data.param != null)
            {
                goto Label_000E;
            }
            return 0L;
        Label_000E:
            param = data.param;
            data2 = data.unit;
            type2 = type;
            switch ((type2 - 1))
            {
                case 0:
                    goto Label_0151;

                case 1:
                    goto Label_00FB;

                case 2:
                    goto Label_0046;

                case 3:
                    goto Label_0126;

                case 4:
                    goto Label_0046;

                case 5:
                    goto Label_0046;

                case 6:
                    goto Label_0046;

                case 7:
                    goto Label_00A5;
            }
        Label_0046:
            if (type2 == 0x10)
            {
                goto Label_00A5;
            }
            if (type2 == 0x20)
            {
                goto Label_00A5;
            }
            if (type2 == 0x40)
            {
                goto Label_00A5;
            }
            if (type2 == 0x80)
            {
                goto Label_00A5;
            }
            if (type2 == 0x100)
            {
                goto Label_00A5;
            }
            if (type2 == 0x200)
            {
                goto Label_00A5;
            }
            if (type2 == 0x400)
            {
                goto Label_00A5;
            }
            if (type2 == 0x800)
            {
                goto Label_00D5;
            }
            if (type2 == 0x1000)
            {
                goto Label_00A5;
            }
            goto Label_0151;
        Label_00A5:
            return GetSortPriority(GetSortStatus(data, type), data2.Lv, data2.Rarity, data2.CurrentJob.Rank, param.raremax, param.rare);
        Label_00D5:
            return GetSortPriority(GetSortStatus(data, type), data2.Lv, data2.Rarity, 0, param.raremax, param.rare);
        Label_00FB:
            return GetSortPriority(GetSortStatus(data, type), data2.Lv, 0, data2.CurrentJob.Rank, param.raremax, param.rare);
        Label_0126:
            return GetSortPriority(GetSortStatus(data, type), 0, data2.Rarity, data2.CurrentJob.Rank, param.raremax, param.rare);
        Label_0151:;
        Label_0156:
            return 0L;
        }

        public static long GetSortPriority(int main, int pri1, int pri2, int pri3, int pri4, int pri5)
        {
            long num;
            long num2;
            long num3;
            long num4;
            long num5;
            long num6;
            num = (long) (main & 0xffff);
            num2 = (long) (pri1 & 0xff);
            num3 = (long) (pri2 & 0xff);
            num4 = (long) (pri3 & 0xff);
            num5 = (long) (pri4 & 0xff);
            num6 = (long) (pri5 & 0xff);
            return ((((((num << 40) | (num2 << 0x20)) | (num3 << 0x18)) | (num4 << 0x10)) | (num5 << 8)) | num6);
        }

        public static int GetSortStatus(UnitListWindow.Data data, SelectType type)
        {
            UnitParam param;
            UnitData data2;
            StatusParam param2;
            int num;
            SelectType type2;
            if (data.param != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            param = data.param;
            data2 = data.unit;
            param2 = param.ini_status.param;
            if (data2 == null)
            {
                goto Label_0039;
            }
            param2 = data2.Status.param;
        Label_0039:
            type2 = type;
            switch ((type2 - 1))
            {
                case 0:
                    goto Label_01E9;

                case 1:
                    goto Label_01C3;

                case 2:
                    goto Label_0065;

                case 3:
                    goto Label_01D6;

                case 4:
                    goto Label_0065;

                case 5:
                    goto Label_0065;

                case 6:
                    goto Label_0065;

                case 7:
                    goto Label_0109;
            }
        Label_0065:
            if (type2 == 0x10)
            {
                goto Label_00CD;
            }
            if (type2 == 0x20)
            {
                goto Label_00D9;
            }
            if (type2 == 0x40)
            {
                goto Label_00E5;
            }
            if (type2 == 0x80)
            {
                goto Label_00F1;
            }
            if (type2 == 0x100)
            {
                goto Label_019F;
            }
            if (type2 == 0x200)
            {
                goto Label_00FD;
            }
            if (type2 == 0x400)
            {
                goto Label_018C;
            }
            if (type2 == 0x800)
            {
                goto Label_01AB;
            }
            if (type2 == 0x1000)
            {
                goto Label_0179;
            }
            goto Label_01E9;
        Label_00CD:
            return param2.atk;
        Label_00D9:
            return param2.def;
        Label_00E5:
            return param2.mag;
        Label_00F1:
            return param2.mnd;
        Label_00FD:
            return param2.spd;
        Label_0109:
            num = param2.atk + param2.def;
            num += param2.mag;
            num += param2.mnd;
            num += param2.spd;
            num += param2.dex;
            num += param2.cri;
            num += param2.luk;
            return num;
        Label_0179:
            return ((data2 == null) ? 1 : data2.AwakeLv);
        Label_018C:
            return ((data2 == null) ? 1 : data2.GetCombination());
        Label_019F:
            return param2.hp;
        Label_01AB:
            return ((data2 == null) ? 1 : data2.CurrentJob.Rank);
        Label_01C3:
            return ((data2 == null) ? 1 : data2.Rarity);
        Label_01D6:
            return ((data2 == null) ? 1 : data2.Lv);
        Label_01E9:;
        Label_01EE:
            return 0;
        }

        public static string GetText(SelectType selectType)
        {
            return LocalizedText.Get("sys.SORT_" + ((SelectType) selectType).ToString());
        }

        public override void Initialize(FlowWindowBase.SerializeParamBase param)
        {
            SerializeValueBehaviour behaviour;
            base.Initialize(param);
            this.m_Param = param as SerializeParam;
            if (this.m_Param != null)
            {
                goto Label_0034;
            }
            throw new Exception(this.ToString() + " > Failed serializeParam null.");
        Label_0034:
            behaviour = base.GetChildComponent<SerializeValueBehaviour>("sort");
            if ((behaviour != null) == null)
            {
                goto Label_005D;
            }
            this.m_ValueList = behaviour.list;
            goto Label_0068;
        Label_005D:
            this.m_ValueList = new SerializeValueList();
        Label_0068:
            if ((base.m_Window != null) == null)
            {
                goto Label_009B;
            }
            if ((base.m_Animator != null) == null)
            {
                goto Label_009B;
            }
            this.CacheToggleParam(base.m_Animator.get_gameObject());
        Label_009B:
            this.LoadSelectType();
            base.Close(1);
            return;
        }

        private unsafe void InitializeToggleContent()
        {
            KeyValuePair<SelectType, Toggle> pair;
            Dictionary<SelectType, Toggle>.Enumerator enumerator;
            enumerator = this.m_Toggles.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_004B;
            Label_0011:
                pair = &enumerator.Current;
                if ((this.m_SelectType & &pair.Key) == null)
                {
                    goto Label_003E;
                }
                &pair.Value.set_isOn(1);
                goto Label_004B;
            Label_003E:
                &pair.Value.set_isOn(0);
            Label_004B:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_0068;
            }
            finally
            {
            Label_005C:
                ((Dictionary<SelectType, Toggle>.Enumerator) enumerator).Dispose();
            }
        Label_0068:
            this.m_SelectTypeReg = this.m_SelectType;
            return;
        }

        public bool IsType(SelectType value)
        {
            return (((this.m_SelectType & value) == null) ? 0 : 1);
        }

        public unsafe void LoadSelectType()
        {
            string str;
            string str2;
            int num;
            string str3;
            GameUtility.UnitSortModes modes;
            string str4;
            bool flag;
            this.m_SelectType = 0;
            str = "UNITLIST_SORT";
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_011D;
            }
            if (PlayerPrefsUtility.HasKey(str) == null)
            {
                goto Label_0055;
            }
            str2 = PlayerPrefsUtility.GetString(str, string.Empty);
            num = 0;
            if ((string.IsNullOrEmpty(str2) != null) || (int.TryParse(str2, &num) == null))
            {
                goto Label_011D;
            }
            this.m_SelectType = num;
            goto Label_011D;
        Label_0055:
            str3 = "UNITLIST";
            if (string.IsNullOrEmpty(str3) != null)
            {
                goto Label_011D;
            }
            this.m_SelectType = 0;
            if (PlayerPrefsUtility.HasKey(str3) == null)
            {
                goto Label_00D6;
            }
            modes = 0;
            str4 = PlayerPrefsUtility.GetString(str3, string.Empty);
        Label_0088:
            try
            {
                if (string.IsNullOrEmpty(str4) != null)
                {
                    goto Label_00AD;
                }
                modes = (int) Enum.Parse(typeof(GameUtility.UnitSortModes), str4, 1);
            Label_00AD:
                goto Label_00C9;
            }
            catch (Exception)
            {
            Label_00B2:
                DebugUtility.LogError("Unknown sort mode:" + str4);
                goto Label_00C9;
            }
        Label_00C9:
            this.SetSection(ConvertSortMode(modes));
        Label_00D6:
            if (PlayerPrefsUtility.HasKey(str3 + "#") == null)
            {
                goto Label_0117;
            }
            flag = (PlayerPrefsUtility.GetInt(str3 + "#", 0) != null) ? 1 : 0;
            this.SetAlignment(ConvertReverse(flag));
        Label_0117:
            this.SaveSelectType();
        Label_011D:
            if (this.GetSection() != null)
            {
                goto Label_012F;
            }
            this.SetSection(1);
        Label_012F:
            if (this.GetAlignment() != null)
            {
                goto Label_0145;
            }
            this.SetAlignment(0x1000000);
        Label_0145:
            this.m_SelectTypeReg = this.m_SelectType;
            return;
        }

        public override int OnActivate(int pinId)
        {
            SerializeValueList list;
            Toggle toggle;
            int num;
            if (pinId != 500)
            {
                goto Label_0022;
            }
            this.CreateInstance();
            this.InitializeToggleContent();
            base.Open();
            goto Label_010F;
        Label_0022:
            if (pinId != 510)
            {
                goto Label_0053;
            }
            this.ReleaseToggleContent();
            this.m_SelectType = this.m_SelectTypeReg;
            this.m_Result = 2;
            base.Close(0);
            return 0x24f;
        Label_0053:
            if (pinId != 520)
            {
                goto Label_008A;
            }
            this.SaveSelectType();
            this.ReleaseToggleContent();
            this.m_SelectTypeReg = this.m_SelectType;
            this.m_Result = 1;
            base.Close(0);
            return 590;
        Label_008A:
            if (pinId != 530)
            {
                goto Label_010F;
            }
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_010F;
            }
            toggle = list.GetUIToggle("_self");
            if ((toggle != null) == null)
            {
                goto Label_010F;
            }
            num = list.GetInt("section", 0);
            if (num <= 0)
            {
                goto Label_00E9;
            }
            if (toggle.get_isOn() == null)
            {
                goto Label_010F;
            }
            this.SetSection(num);
            goto Label_010F;
        Label_00E9:
            num = list.GetInt("alignment", 0);
            if (num <= 0)
            {
                goto Label_010F;
            }
            if (toggle.get_isOn() == null)
            {
                goto Label_010F;
            }
            this.SetAlignment(num);
        Label_010F:
            return -1;
        }

        public override void Release()
        {
            base.Release();
            return;
        }

        private void ReleaseToggleContent()
        {
        }

        private void ResetAlignment(SelectType selectType)
        {
            this.m_SelectType &= ~(selectType & 0xf000000);
            return;
        }

        private void ResetSection(SelectType selectType)
        {
            this.m_SelectType &= ~(selectType & 0xffffff);
            return;
        }

        public unsafe void SaveSelectType()
        {
            string str;
            int num;
            str = "UNITLIST_SORT";
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0027;
            }
            PlayerPrefsUtility.SetString(str, &this.m_SelectType.ToString(), 1);
        Label_0027:
            return;
        }

        private void SetAlignment(SelectType selectType)
        {
            this.m_SelectType &= -251658241;
            this.m_SelectType |= selectType & 0xf000000;
            return;
        }

        public void SetRoot(UnitListWindow root)
        {
            this.m_Root = root;
            return;
        }

        private void SetSection(SelectType selectType)
        {
            this.m_SelectType &= -16777216;
            this.m_SelectType |= selectType & 0xffffff;
            return;
        }

        public override int Update()
        {
            base.Update();
            if (this.m_Result == null)
            {
                goto Label_0029;
            }
            if (base.isClosed == null)
            {
                goto Label_003B;
            }
            base.SetActiveChild(0);
            goto Label_003B;
        Label_0029:
            if (base.isClosed == null)
            {
                goto Label_003B;
            }
            base.SetActiveChild(0);
        Label_003B:
            return -1;
        }

        public override string name
        {
            get
            {
                return "UnitListSortWindow";
            }
        }

        public enum Result
        {
            NONE,
            CONFIRM,
            CANCEL
        }

        public enum SelectType
        {
            NONE = 0,
            TIME = 1,
            RARITY = 2,
            LEVEL = 4,
            TOTAL = 8,
            ATK = 0x10,
            DEF = 0x20,
            MAG = 0x40,
            MND = 0x80,
            HP = 0x100,
            SPD = 0x200,
            COMBINATION = 0x400,
            JOBRANK = 0x800,
            AWAKE = 0x1000,
            SYOJYUN = 0x1000000,
            KOUJYUN = 0x2000000
        }

        [Serializable]
        public class SerializeParam : FlowWindowBase.SerializeParamBase
        {
            public SerializeParam()
            {
                base..ctor();
                return;
            }

            public override Type type
            {
                get
                {
                    return typeof(UnitListSortWindow);
                }
            }
        }
    }
}

