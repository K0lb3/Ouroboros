namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitListFilterWindow : FlowWindowBase
    {
        private const string SAVEKEY = "UNITLIST_FILTER";
        private const string SAVEKEY_OLD = "UNITLIST";
        public const SelectType MASK_RARITY = 0x1f;
        public const SelectType MASK_WEAPON = 0xfc0;
        public const SelectType MASK_BIRTH = 0x7ff000;
        public const SelectType MASK_SELECT = 0x7fffdf;
        private SerializeParam m_Param;
        private SerializeValueList m_ValueList;
        private UnitListWindow m_Root;
        private Result m_Result;
        private SelectType m_SelectTypeReg;
        private SelectType m_SelectType;
        private Dictionary<SelectType, Toggle> m_Toggles;

        public UnitListFilterWindow()
        {
            this.m_Toggles = new Dictionary<SelectType, Toggle>();
            base..ctor();
            return;
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
            goto Label_00C2;
        Label_000E:
            try
            {
                type = (int) Enum.Parse(typeof(SelectType), toggleArray[num].get_name());
                event2 = toggleArray[num].GetComponent<ButtonEvent>();
                if ((event2 != null) == null)
                {
                    goto Label_0066;
                }
                event3 = event2.GetEvent("UNITFILTER_TGL_CHANGE");
                if (event3 == null)
                {
                    goto Label_0066;
                }
                event3.valueList.SetField("select", type);
            Label_0066:
                this.m_Toggles.Add(type, toggleArray[num]);
                goto Label_00BE;
            }
            catch (Exception exception1)
            {
            Label_007A:
                exception = exception1;
                textArray1 = new string[] { "UnitSortWindow トグル名からSelectTypeを取得できない！ > ", toggleArray[num].get_name(), " ( Exception > ", exception.ToString(), " )" };
                Debug.LogError(string.Concat(textArray1));
                goto Label_00BE;
            }
        Label_00BE:
            num += 1;
        Label_00C2:
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
            UnitListWindow.Data data;
            type = ~this.m_SelectType & 0x7fffdf;
            num = list.Count - 1;
            goto Label_003C;
        Label_001C:
            data = list[num];
            if ((data.filterMask & type) == null)
            {
                goto Label_0038;
            }
            list.RemoveAt(num);
        Label_0038:
            num -= 1;
        Label_003C:
            if (num >= 0)
            {
                goto Label_001C;
            }
            return;
        }

        public static SelectType ConvertFilterString(string text)
        {
            if ((text == "RARE:0") == null)
            {
                goto Label_0012;
            }
            return 1;
        Label_0012:
            if ((text == "RARE:1") == null)
            {
                goto Label_0024;
            }
            return 2;
        Label_0024:
            if ((text == "RARE:2") == null)
            {
                goto Label_0036;
            }
            return 4;
        Label_0036:
            if ((text == "RARE:3") == null)
            {
                goto Label_0048;
            }
            return 8;
        Label_0048:
            if ((text == "RARE:4") == null)
            {
                goto Label_005B;
            }
            return 0x10;
        Label_005B:
            if ((text == "WEAPON:Slash") == null)
            {
                goto Label_006E;
            }
            return 0x40;
        Label_006E:
            if ((text == "WEAPON:Stab") == null)
            {
                goto Label_0084;
            }
            return 0x80;
        Label_0084:
            if ((text == "WEAPON:Blow") == null)
            {
                goto Label_009A;
            }
            return 0x100;
        Label_009A:
            if ((text == "WEAPON:Shot") == null)
            {
                goto Label_00B0;
            }
            return 0x200;
        Label_00B0:
            if ((text == "WEAPON:Magic") == null)
            {
                goto Label_00C6;
            }
            return 0x400;
        Label_00C6:
            if ((text == "WEAPON:None") == null)
            {
                goto Label_00DC;
            }
            return 0x800;
        Label_00DC:
            if ((text == "BIRTH:エンヴィリア") == null)
            {
                goto Label_00F2;
            }
            return 0x1000;
        Label_00F2:
            if ((text == "BIRTH:ラーストリス") == null)
            {
                goto Label_0108;
            }
            return 0x2000;
        Label_0108:
            if ((text == "BIRTH:サガ地方") == null)
            {
                goto Label_011E;
            }
            return 0x4000;
        Label_011E:
            if ((text == "BIRTH:スロウスシュタイン") == null)
            {
                goto Label_0134;
            }
            return 0x8000;
        Label_0134:
            if ((text == "BIRTH:ルストブルグ") == null)
            {
                goto Label_014A;
            }
            return 0x10000;
        Label_014A:
            if ((text == "BIRTH:ワダツミ") == null)
            {
                goto Label_0160;
            }
            return 0x20000;
        Label_0160:
            if ((text == "BIRTH:砂漠地帯") == null)
            {
                goto Label_0176;
            }
            return 0x40000;
        Label_0176:
            if ((text == "BIRTH:グリードダイク") == null)
            {
                goto Label_018C;
            }
            return 0x80000;
        Label_018C:
            if ((text == "BIRTH:グラトニー＝フォス") == null)
            {
                goto Label_01A2;
            }
            return 0x100000;
        Label_01A2:
            if ((text == "BIRTH:その他") == null)
            {
                goto Label_01B8;
            }
            return 0x200000;
        Label_01B8:
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

        public static SelectType GetFilterMask(UnitListWindow.Data data)
        {
            GameManager manager;
            MasterParam param;
            UnitParam param2;
            SelectType type;
            AttackDetailTypes types;
            UnitData data2;
            JobSetParam param3;
            JobParam param4;
            string str;
            SkillParam param5;
            string str2;
            if (data.param != null)
            {
                goto Label_0011;
            }
            return 0x7fffdf;
        Label_0011:
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            param2 = data.param;
            type = 0;
            types = 0;
            if (data.unit == null)
            {
                goto Label_00E6;
            }
            data2 = data.unit;
            if (data2.CurrentJob.GetAttackSkill() == null)
            {
                goto Label_0061;
            }
            types = data2.CurrentJob.GetAttackSkill().AttackDetailType;
        Label_0061:
            if (data2.Rarity != null)
            {
                goto Label_0076;
            }
            type |= 1;
            goto Label_00E1;
        Label_0076:
            if (data2.Rarity != 1)
            {
                goto Label_008C;
            }
            type |= 2;
            goto Label_00E1;
        Label_008C:
            if (data2.Rarity != 2)
            {
                goto Label_00A2;
            }
            type |= 4;
            goto Label_00E1;
        Label_00A2:
            if (data2.Rarity != 3)
            {
                goto Label_00B8;
            }
            type |= 8;
            goto Label_00E1;
        Label_00B8:
            if (data2.Rarity != 4)
            {
                goto Label_00CF;
            }
            type |= 0x10;
            goto Label_00E1;
        Label_00CF:
            if (data2.Rarity != 5)
            {
                goto Label_01FA;
            }
            type |= 0x20;
        Label_00E1:
            goto Label_01FA;
        Label_00E6:
            if (param2.jobsets == null)
            {
                goto Label_0180;
            }
            if (((int) param2.jobsets.Length) <= 0)
            {
                goto Label_0180;
            }
            param3 = MonoSingleton<GameManager>.Instance.GetJobSetParam(param2.jobsets[0]);
            if (param3 == null)
            {
                goto Label_0180;
            }
            param4 = MonoSingleton<GameManager>.Instance.GetJobParam(param3.job);
            if (param4 == null)
            {
                goto Label_0180;
            }
            if (param4.atkskill == null)
            {
                goto Label_0180;
            }
            if (((int) param4.atkskill.Length) <= 0)
            {
                goto Label_0180;
            }
            str = param4.atkskill[0];
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0180;
            }
            param5 = param.GetSkillParam(str);
            if (param5 == null)
            {
                goto Label_0180;
            }
            types = param5.attack_detail;
        Label_0180:
            if (param2.rare != null)
            {
                goto Label_0194;
            }
            type |= 1;
            goto Label_01FA;
        Label_0194:
            if (param2.rare != 1)
            {
                goto Label_01A9;
            }
            type |= 2;
            goto Label_01FA;
        Label_01A9:
            if (param2.rare != 2)
            {
                goto Label_01BE;
            }
            type |= 4;
            goto Label_01FA;
        Label_01BE:
            if (param2.rare != 3)
            {
                goto Label_01D3;
            }
            type |= 8;
            goto Label_01FA;
        Label_01D3:
            if (param2.rare != 4)
            {
                goto Label_01E9;
            }
            type |= 0x10;
            goto Label_01FA;
        Label_01E9:
            if (param2.rare != 5)
            {
                goto Label_01FA;
            }
            type |= 0x20;
        Label_01FA:
            if (types != 1)
            {
                goto Label_020C;
            }
            type |= 0x40;
            goto Label_026F;
        Label_020C:
            if (types != 2)
            {
                goto Label_0221;
            }
            type |= 0x80;
            goto Label_026F;
        Label_0221:
            if (types != 3)
            {
                goto Label_0236;
            }
            type |= 0x100;
            goto Label_026F;
        Label_0236:
            if (types != 4)
            {
                goto Label_024B;
            }
            type |= 0x200;
            goto Label_026F;
        Label_024B:
            if (types != 5)
            {
                goto Label_0260;
            }
            type |= 0x400;
            goto Label_026F;
        Label_0260:
            if (types != null)
            {
                goto Label_026F;
            }
            type |= 0x800;
        Label_026F:
            if (string.IsNullOrEmpty(param2.birth) != null)
            {
                goto Label_03D6;
            }
            str2 = param2.birth;
            if ((str2 == "エンヴィリア") == null)
            {
                goto Label_02AF;
            }
            type |= 0x1000;
            goto Label_03D6;
        Label_02AF:
            if ((str2 == "ラーストリス") == null)
            {
                goto Label_02CD;
            }
            type |= 0x2000;
            goto Label_03D6;
        Label_02CD:
            if ((str2 == "サガ地方") == null)
            {
                goto Label_02EB;
            }
            type |= 0x4000;
            goto Label_03D6;
        Label_02EB:
            if ((str2 == "スロウスシュタイン") == null)
            {
                goto Label_0309;
            }
            type |= 0x8000;
            goto Label_03D6;
        Label_0309:
            if ((str2 == "ルストブルグ") == null)
            {
                goto Label_0327;
            }
            type |= 0x10000;
            goto Label_03D6;
        Label_0327:
            if ((str2 == "ワダツミ") == null)
            {
                goto Label_0345;
            }
            type |= 0x20000;
            goto Label_03D6;
        Label_0345:
            if ((str2 == "砂漠地帯") == null)
            {
                goto Label_0363;
            }
            type |= 0x40000;
            goto Label_03D6;
        Label_0363:
            if ((str2 == "グリードダイク") == null)
            {
                goto Label_0381;
            }
            type |= 0x80000;
            goto Label_03D6;
        Label_0381:
            if ((str2 == "グラトニー＝フォス") == null)
            {
                goto Label_039F;
            }
            type |= 0x100000;
            goto Label_03D6;
        Label_039F:
            if ((str2 == "その他") == null)
            {
                goto Label_03BD;
            }
            type |= 0x200000;
            goto Label_03D6;
        Label_03BD:
            if ((str2 == "ノーザンブライド") == null)
            {
                goto Label_03D6;
            }
            type |= 0x400000;
        Label_03D6:
            return type;
        }

        public SelectType GetSelect(SelectType mask)
        {
            return (this.m_SelectType & mask);
        }

        public SelectType GetSelectReg(SelectType mask)
        {
            return (this.m_SelectTypeReg & mask);
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
            behaviour = base.GetChildComponent<SerializeValueBehaviour>("filter");
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

        public unsafe void LoadSelectType()
        {
            char[] chArray1;
            string str;
            string str2;
            int num;
            string str3;
            string str4;
            string[] strArray;
            int num2;
            this.m_SelectType = 0x7fffdf;
            str = "UNITLIST_FILTER";
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_00E5;
            }
            if (PlayerPrefsUtility.HasKey(str) == null)
            {
                goto Label_0059;
            }
            str2 = PlayerPrefsUtility.GetString(str, string.Empty);
            num = 0;
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_00E5;
            }
            if (int.TryParse(str2, &num) == null)
            {
                goto Label_00E5;
            }
            this.ResetSelect(num);
            goto Label_00E5;
        Label_0059:
            str3 = "UNITLIST";
            if (string.IsNullOrEmpty(str3) != null)
            {
                goto Label_00E5;
            }
            if (PlayerPrefsUtility.HasKey(str3 + "&") == null)
            {
                goto Label_00DF;
            }
            str4 = PlayerPrefsUtility.GetString(str3 + "&", string.Empty);
            if (string.IsNullOrEmpty(str4) != null)
            {
                goto Label_00DF;
            }
            chArray1 = new char[] { 0x26 };
            strArray = str4.Split(chArray1);
            num2 = 0;
            goto Label_00D4;
        Label_00BE:
            this.ResetSelect(ConvertFilterString(strArray[num2]));
            num2 += 1;
        Label_00D4:
            if (num2 < ((int) strArray.Length))
            {
                goto Label_00BE;
            }
        Label_00DF:
            this.SaveSelectType();
        Label_00E5:
            this.m_SelectTypeReg = this.m_SelectType;
            return;
        }

        public override unsafe int OnActivate(int pinId)
        {
            SerializeValueList list;
            Toggle toggle;
            int num;
            KeyValuePair<SelectType, Toggle> pair;
            Dictionary<SelectType, Toggle>.Enumerator enumerator;
            KeyValuePair<SelectType, Toggle> pair2;
            Dictionary<SelectType, Toggle>.Enumerator enumerator2;
            if (pinId != 600)
            {
                goto Label_0022;
            }
            this.CreateInstance();
            this.InitializeToggleContent();
            base.Open();
            goto Label_01B6;
        Label_0022:
            if (pinId != 610)
            {
                goto Label_0053;
            }
            this.ReleaseToggleContent();
            this.m_SelectType = this.m_SelectTypeReg;
            this.m_Result = 2;
            base.Close(0);
            return 0x2b3;
        Label_0053:
            if (pinId != 620)
            {
                goto Label_008A;
            }
            this.SaveSelectType();
            this.ReleaseToggleContent();
            this.m_SelectTypeReg = this.m_SelectType;
            this.m_Result = 1;
            base.Close(0);
            return 690;
        Label_008A:
            if (pinId != 630)
            {
                goto Label_00F5;
            }
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_01B6;
            }
            toggle = list.GetUIToggle("_self");
            if ((toggle != null) == null)
            {
                goto Label_01B6;
            }
            num = list.GetInt("select", 0);
            if (num <= 0)
            {
                goto Label_01B6;
            }
            if (toggle.get_isOn() == null)
            {
                goto Label_00E9;
            }
            this.SetSelect(num);
            goto Label_00F0;
        Label_00E9:
            this.ResetSelect(num);
        Label_00F0:
            goto Label_01B6;
        Label_00F5:
            if (pinId != 640)
            {
                goto Label_015E;
            }
            this.m_SelectType = 0;
            enumerator = this.m_Toggles.GetEnumerator();
        Label_0114:
            try
            {
                goto Label_013B;
            Label_0119:
                pair = &enumerator.Current;
                this.SetSelect(&pair.Key);
                &pair.Value.set_isOn(1);
            Label_013B:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0119;
                }
                goto Label_0159;
            }
            finally
            {
            Label_014C:
                ((Dictionary<SelectType, Toggle>.Enumerator) enumerator).Dispose();
            }
        Label_0159:
            goto Label_01B6;
        Label_015E:
            if (pinId != 650)
            {
                goto Label_01B6;
            }
            this.m_SelectType = 0;
            enumerator2 = this.m_Toggles.GetEnumerator();
        Label_017D:
            try
            {
                goto Label_0198;
            Label_0182:
                pair2 = &enumerator2.Current;
                &pair2.Value.set_isOn(0);
            Label_0198:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0182;
                }
                goto Label_01B6;
            }
            finally
            {
            Label_01A9:
                ((Dictionary<SelectType, Toggle>.Enumerator) enumerator2).Dispose();
            }
        Label_01B6:
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

        private void ResetSelect(SelectType selectType)
        {
            this.m_SelectType &= ~(selectType & 0x7fffdf);
            return;
        }

        public unsafe void SaveSelectType()
        {
            string str;
            int num;
            int num2;
            int num3;
            str = "UNITLIST_FILTER";
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0059;
            }
            num = 0;
            num2 = 0;
            goto Label_0042;
        Label_001A:
            num3 = 1 << (num2 & 0x1f);
            if ((num3 & 0x7fffdf) == null)
            {
                goto Label_003E;
            }
            if ((this.m_SelectType & num3) != null)
            {
                goto Label_003E;
            }
            num |= num3;
        Label_003E:
            num2 += 1;
        Label_0042:
            if (num2 < 0x20)
            {
                goto Label_001A;
            }
            PlayerPrefsUtility.SetString(str, &num.ToString(), 1);
        Label_0059:
            return;
        }

        public void SetRoot(UnitListWindow root)
        {
            this.m_Root = root;
            return;
        }

        private void SetSelect(SelectType selectType)
        {
            this.m_SelectType |= selectType & 0x7fffdf;
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
                return "UnitListFilterWindow";
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
            RARITY_1 = 1,
            RARITY_2 = 2,
            RARITY_3 = 4,
            RARITY_4 = 8,
            RARITY_5 = 0x10,
            RARITY_6 = 0x20,
            WEAPON_SLASH = 0x40,
            WEAPON_STAB = 0x80,
            WEAPON_BLOW = 0x100,
            WEAPON_SHOT = 0x200,
            WEAPON_MAG = 0x400,
            WEAPON_NONE = 0x800,
            BIRTH_ENV = 0x1000,
            BIRTH_WRATH = 0x2000,
            BIRTH_SAGA = 0x4000,
            BIRTH_SLOTH = 0x8000,
            BIRTH_LUST = 0x10000,
            BIRTH_WADATSUMI = 0x20000,
            BIRTH_DESERT = 0x40000,
            BIRTH_GREED = 0x80000,
            BIRTH_GLUTTONY = 0x100000,
            BIRTH_OTHER = 0x200000,
            BIRTH_NOZ = 0x400000
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
                    return typeof(UnitListFilterWindow);
                }
            }
        }
    }
}

