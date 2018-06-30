namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "変更前のスキルを表示", 0, 1), Pin(2, "変更前のスキルを非表示", 0, 2)]
    public class AbilityDetailWindow : MonoBehaviour, IFlowInterface
    {
        public const int INPUT_ENABLE_BASE_SKILL = 1;
        public const int INPUT_DIABLE_BASE_SKILL = 2;
        private static UnitData BindUnit;
        private static AbilityParam BindAbility;
        public Transform SkillLayoutParent;
        public GameObject SkillTemplate;
        public GameObject SkillLockedTemplate;
        [StringIsGameObjectID]
        public string UnlockCondTextId;
        public GameObject SkillUnlockCondWindow;
        [SerializeField]
        private Selectable m_ShowBaseToggle;
        public static bool IsEnableSkillChange;
        private List<SkillDeriveList> m_SkillDeriveList;

        static AbilityDetailWindow()
        {
        }

        public AbilityDetailWindow()
        {
            this.m_SkillDeriveList = new List<SkillDeriveList>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 1)
            {
                goto Label_0015;
            }
            if (num == 2)
            {
                goto Label_0021;
            }
            goto Label_002D;
        Label_0015:
            this.SwitchBaseSkillEnable(1);
            goto Label_002D;
        Label_0021:
            this.SwitchBaseSkillEnable(0);
        Label_002D:
            return;
        }

        private static void AddSkillParam(List<ViewContentParam> viewContentParams, SkillParam skillParam, SkillDeriveParam skillDeriveParam)
        {
            ViewContentParam param;
            ViewContentParam param2;
            <AddSkillParam>c__AnonStorey2F2 storeyf;
            storeyf = new <AddSkillParam>c__AnonStorey2F2();
            storeyf.skillParam = skillParam;
            param = null;
            if (skillDeriveParam == null)
            {
                goto Label_0021;
            }
            storeyf.skillParam = skillDeriveParam.m_BaseParam;
        Label_0021:
            param = viewContentParams.Find(new Predicate<ViewContentParam>(storeyf.<>m__27A));
            if (param == null)
            {
                goto Label_004B;
            }
            param.m_SkillDeriveParams.Add(skillDeriveParam);
            goto Label_0081;
        Label_004B:
            param2 = new ViewContentParam();
            param2.m_SkillParam = storeyf.skillParam;
            if (skillDeriveParam == null)
            {
                goto Label_007A;
            }
            param2.m_SkillDeriveParams = new List<SkillDeriveParam>();
            param2.m_SkillDeriveParams.Add(skillDeriveParam);
        Label_007A:
            viewContentParams.Add(param2);
        Label_0081:
            return;
        }

        private void Awake()
        {
            if ((this.SkillTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.SkillTemplate.SetActive(0);
        Label_001D:
            if ((this.SkillLockedTemplate != null) == null)
            {
                goto Label_003A;
            }
            this.SkillLockedTemplate.SetActive(0);
        Label_003A:
            if ((this.SkillUnlockCondWindow != null) == null)
            {
                goto Label_0057;
            }
            this.SkillUnlockCondWindow.SetActive(0);
        Label_0057:
            if ((this.m_ShowBaseToggle != null) == null)
            {
                goto Label_0074;
            }
            this.m_ShowBaseToggle.set_interactable(0);
        Label_0074:
            return;
        }

        private void OnEnable()
        {
            this.Refresh();
            return;
        }

        public void OnOpenSkillUnlockCondWindow(GameObject button)
        {
            GameObject obj2;
            QuestClearUnlockUnitDataParam param;
            Text text;
            string str;
            if ((this.SkillUnlockCondWindow != null) == null)
            {
                goto Label_00EF;
            }
            obj2 = Object.Instantiate<GameObject>(this.SkillUnlockCondWindow);
            param = DataSource.FindDataOfClass<QuestClearUnlockUnitDataParam>(button, null);
            DataSource.Bind<QuestClearUnlockUnitDataParam>(obj2, param);
            obj2.SetActive(1);
            obj2.get_transform().SetParent(base.get_transform(), 0);
            text = GameObjectID.FindGameObject<Text>(this.UnlockCondTextId);
            if ((text != null) == null)
            {
                goto Label_00EF;
            }
            text.set_text(param.GetUnitLevelCond());
            str = param.GetAbilityLevelCond();
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_00AC;
            }
            text.set_text(text.get_text() + ((string.IsNullOrEmpty(text.get_text()) != null) ? string.Empty : "\n") + str);
        Label_00AC:
            str = param.GetClearQuestCond();
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_00EF;
            }
            text.set_text(text.get_text() + ((string.IsNullOrEmpty(text.get_text()) != null) ? string.Empty : "\n") + str);
        Label_00EF:
            return;
        }

        private unsafe void Refresh()
        {
            UnitData data;
            List<ViewContentParam> list;
            int num;
            SkillParam param;
            int num2;
            GameObject obj2;
            SkillDeriveList list2;
            AbilityData data2;
            AbilityParam param2;
            QuestClearUnlockUnitDataParam[] paramArray;
            List<QuestClearUnlockUnitDataParam> list3;
            QuestClearUnlockUnitDataParam[] paramArray2;
            int num3;
            RarityParam param3;
            int num4;
            AbilityParam param4;
            string str;
            List<ViewContentParam> list4;
            int num5;
            string str2;
            int num6;
            string str3;
            SkillData[] dataArray;
            SkillData data3;
            SkillData[] dataArray2;
            int num7;
            int num8;
            int num9;
            AbilityData data4;
            SkillData data5;
            List<SkillData>.Enumerator enumerator;
            int num10;
            GameObject obj3;
            SkillDeriveList list5;
            int num11;
            GameObject obj4;
            int num12;
            <Refresh>c__AnonStorey2F1 storeyf;
            data = null;
            if (UnitEnhanceV3.Instance == null)
            {
                goto Label_001C;
            }
            data = UnitEnhanceV3.Instance.CurrentUnit;
        Label_001C:
            if (data != null)
            {
                goto Label_003C;
            }
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
        Label_003C:
            if (data != null)
            {
                goto Label_0052;
            }
            if (BindUnit == null)
            {
                goto Label_0052;
            }
            data = BindUnit;
        Label_0052:
            if (data != null)
            {
                goto Label_006D;
            }
            if (BindAbility != null)
            {
                goto Label_006D;
            }
            DebugUtility.LogError("Not found bind unit data.");
            return;
        Label_006D:
            if (BindAbility == null)
            {
                goto Label_016D;
            }
            DataSource.Bind<AbilityParam>(base.get_gameObject(), BindAbility);
            if ((this.SkillTemplate != null) == null)
            {
                goto Label_070B;
            }
            list = new List<ViewContentParam>();
            if (BindAbility.skills == null)
            {
                goto Label_00EE;
            }
            num = 0;
            goto Label_00DC;
        Label_00B4:
            param = MonoSingleton<GameManager>.Instance.GetSkillParam(BindAbility.skills[num].iname);
            AddSkillParam(list, param, null);
            num += 1;
        Label_00DC:
            if (num < ((int) BindAbility.skills.Length))
            {
                goto Label_00B4;
            }
        Label_00EE:
            num2 = 0;
            goto Label_015B;
        Label_00F6:
            obj2 = Object.Instantiate<GameObject>(this.SkillTemplate);
            list2 = obj2.GetComponentInChildren<SkillDeriveList>();
            list2.Setup(list[num2].m_SkillParam, list[num2].m_SkillDeriveParams);
            obj2.get_transform().SetParent(this.SkillLayoutParent, 0);
            obj2.SetActive(1);
            this.m_SkillDeriveList.Add(list2);
            num2 += 1;
        Label_015B:
            if (num2 < list.Count)
            {
                goto Label_00F6;
            }
            goto Label_070B;
        Label_016D:
            data2 = data.GetAbilityData(GlobalVars.SelectedAbilityUniqueID);
            param2 = MonoSingleton<GameManager>.Instance.GetAbilityParam(GlobalVars.SelectedAbilityID);
            paramArray = data.UnlockedSkills;
            list3 = new List<QuestClearUnlockUnitDataParam>();
            paramArray2 = MonoSingleton<GameManager>.Instance.MasterParam.GetAllUnlockUnitDatas();
            if (paramArray2 == null)
            {
                goto Label_0262;
            }
            num3 = 0;
            goto Label_0257;
        Label_01C4:
            storeyf = new <Refresh>c__AnonStorey2F1();
            storeyf.param = paramArray2[num3];
            if (storeyf.param.type != 1)
            {
                goto Label_0251;
            }
            if ((storeyf.param.uid == data.UnitID) == null)
            {
                goto Label_0251;
            }
            if ((storeyf.param.parent_id == param2.iname) == null)
            {
                goto Label_0251;
            }
            if (paramArray == null)
            {
                goto Label_0243;
            }
            if (Array.FindIndex<QuestClearUnlockUnitDataParam>(paramArray, new Predicate<QuestClearUnlockUnitDataParam>(storeyf.<>m__279)) != -1)
            {
                goto Label_0251;
            }
        Label_0243:
            list3.Add(storeyf.param);
        Label_0251:
            num3 += 1;
        Label_0257:
            if (num3 < ((int) paramArray2.Length))
            {
                goto Label_01C4;
            }
        Label_0262:
            if (data2 == null)
            {
                goto Label_028A;
            }
            if (data2.Param == null)
            {
                goto Label_028A;
            }
            if (data2.IsDeriveBaseAbility == null)
            {
                goto Label_028A;
            }
            data2 = data2.DerivedAbility;
        Label_028A:
            param3 = MonoSingleton<GameManager>.Instance.GetRarityParam(data.UnitParam.raremax);
            num4 = Math.Min(param3.UnitLvCap + param3.UnitAwakeLvCap, param2.GetRankCap());
            DataSource.Bind<UnitData>(base.get_gameObject(), data);
            DataSource.Bind<AbilityData>(base.get_gameObject(), data2);
            param4 = param2;
            if (IsEnableSkillChange == null)
            {
                goto Label_0318;
            }
            str = data.SearchAbilityReplacementSkill(param2.iname);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0318;
            }
            param4 = MonoSingleton<GameManager>.Instance.GetAbilityParam(str);
        Label_0318:
            DataSource.Bind<AbilityParam>(base.get_gameObject(), param4);
            if ((this.SkillTemplate != null) == null)
            {
                goto Label_070B;
            }
            list4 = new List<ViewContentParam>();
            if (data2 == null)
            {
                goto Label_04D2;
            }
            if (data2.LearningSkills == null)
            {
                goto Label_04D2;
            }
            num5 = 0;
            goto Label_0457;
        Label_0358:
            if (data2.LearningSkills[num5].locklv > num4)
            {
                goto Label_0451;
            }
            str2 = data2.LearningSkills[num5].iname;
            if (paramArray == null)
            {
                goto Label_03C6;
            }
            num6 = 0;
            goto Label_03BB;
        Label_038E:
            if ((paramArray[num6].old_id == str2) == null)
            {
                goto Label_03B5;
            }
            str2 = paramArray[num6].new_id;
            goto Label_03C6;
        Label_03B5:
            num6 += 1;
        Label_03BB:
            if (num6 < ((int) paramArray.Length))
            {
                goto Label_038E;
            }
        Label_03C6:
            if (IsEnableSkillChange == null)
            {
                goto Label_03EA;
            }
            str3 = data.SearchReplacementSkill(str2);
            if (string.IsNullOrEmpty(str3) != null)
            {
                goto Label_03EA;
            }
            str2 = str3;
        Label_03EA:
            dataArray = data2.FindDeriveSkills(str2);
            if (((int) dataArray.Length) <= 0)
            {
                goto Label_043D;
            }
            dataArray2 = dataArray;
            num7 = 0;
            goto Label_042D;
        Label_040B:
            data3 = dataArray2[num7];
            AddSkillParam(list4, data3.SkillParam, data3.DeriveParam);
            num7 += 1;
        Label_042D:
            if (num7 < ((int) dataArray2.Length))
            {
                goto Label_040B;
            }
            goto Label_0451;
        Label_043D:
            AddSkillParam(list4, MonoSingleton<GameManager>.Instance.GetSkillParam(str2), null);
        Label_0451:
            num5 += 1;
        Label_0457:
            if (num5 < ((int) data2.LearningSkills.Length))
            {
                goto Label_0358;
            }
            if (paramArray == null)
            {
                goto Label_0527;
            }
            num8 = 0;
            goto Label_04C2;
        Label_0476:
            if (paramArray[num8].add == null)
            {
                goto Label_04BC;
            }
            if ((paramArray[num8].parent_id == data2.AbilityID) == null)
            {
                goto Label_04BC;
            }
            AddSkillParam(list4, MonoSingleton<GameManager>.Instance.GetSkillParam(paramArray[num8].new_id), null);
        Label_04BC:
            num8 += 1;
        Label_04C2:
            if (num8 < ((int) paramArray.Length))
            {
                goto Label_0476;
            }
            goto Label_0527;
        Label_04D2:
            num9 = 0;
            goto Label_0517;
        Label_04DA:
            if (param2.skills[num9].locklv > num4)
            {
                goto Label_0511;
            }
            AddSkillParam(list4, MonoSingleton<GameManager>.Instance.GetSkillParam(param2.skills[num9].iname), null);
        Label_0511:
            num9 += 1;
        Label_0517:
            if (num9 < ((int) param2.skills.Length))
            {
                goto Label_04DA;
            }
        Label_0527:
            if (data.MasterAbility == null)
            {
                goto Label_05B8;
            }
            if (data.CollaboAbility == null)
            {
                goto Label_05B8;
            }
            data4 = data.MasterAbility;
            if (data.MasterAbility.IsDeriveBaseAbility == null)
            {
                goto Label_0562;
            }
            data4 = data.MasterAbility.DerivedAbility;
        Label_0562:
            if (data4 != data2)
            {
                goto Label_05B8;
            }
            enumerator = data.CollaboAbility.Skills.GetEnumerator();
        Label_057D:
            try
            {
                goto Label_059A;
            Label_0582:
                data5 = &enumerator.Current;
                AddSkillParam(list4, data5.SkillParam, null);
            Label_059A:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0582;
                }
                goto Label_05B8;
            }
            finally
            {
            Label_05AB:
                ((List<SkillData>.Enumerator) enumerator).Dispose();
            }
        Label_05B8:
            num10 = 0;
            goto Label_0627;
        Label_05C0:
            obj3 = Object.Instantiate<GameObject>(this.SkillTemplate);
            list5 = obj3.GetComponentInChildren<SkillDeriveList>();
            list5.Setup(list4[num10].m_SkillParam, list4[num10].m_SkillDeriveParams);
            obj3.get_transform().SetParent(this.SkillLayoutParent, 0);
            obj3.SetActive(1);
            this.m_SkillDeriveList.Add(list5);
            num10 += 1;
        Label_0627:
            if (num10 < list4.Count)
            {
                goto Label_05C0;
            }
            num11 = 0;
            goto Label_069A;
        Label_063D:
            obj4 = Object.Instantiate<GameObject>(this.SkillLockedTemplate);
            DataSource.Bind<SkillParam>(obj4, MonoSingleton<GameManager>.Instance.GetSkillParam(list3[num11].new_id));
            DataSource.Bind<QuestClearUnlockUnitDataParam>(obj4, list3[num11]);
            obj4.get_transform().SetParent(this.SkillLayoutParent, 0);
            obj4.SetActive(1);
            num11 += 1;
        Label_069A:
            if (num11 < list3.Count)
            {
                goto Label_063D;
            }
            num12 = 0;
            goto Label_06FD;
        Label_06B0:
            if (list4[num12].m_SkillDeriveParams != null)
            {
                goto Label_06C8;
            }
            goto Label_06F7;
        Label_06C8:
            if (list4[num12].m_SkillDeriveParams.Count >= 1)
            {
                goto Label_06E6;
            }
            goto Label_06F7;
        Label_06E6:
            this.m_ShowBaseToggle.set_interactable(1);
            goto Label_070B;
        Label_06F7:
            num12 += 1;
        Label_06FD:
            if (num12 < list4.Count)
            {
                goto Label_06B0;
            }
        Label_070B:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public static void SetBindAbility(AbilityParam ability)
        {
            BindUnit = null;
            BindAbility = ability;
            return;
        }

        public static void SetBindObject(UnitData udata)
        {
            BindUnit = udata;
            BindAbility = null;
            return;
        }

        private unsafe void SwitchBaseSkillEnable(bool enable)
        {
            SkillDeriveList list;
            List<SkillDeriveList>.Enumerator enumerator;
            enumerator = this.m_SkillDeriveList.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_0020;
            Label_0011:
                list = &enumerator.Current;
                list.SwitchBaseSkillEnable(enable);
            Label_0020:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_003D;
            }
            finally
            {
            Label_0031:
                ((List<SkillDeriveList>.Enumerator) enumerator).Dispose();
            }
        Label_003D:
            return;
        }

        [CompilerGenerated]
        private sealed class <AddSkillParam>c__AnonStorey2F2
        {
            internal SkillParam skillParam;

            public <AddSkillParam>c__AnonStorey2F2()
            {
                base..ctor();
                return;
            }

            internal bool <>m__27A(AbilityDetailWindow.ViewContentParam param)
            {
                return (param.m_SkillParam == this.skillParam);
            }
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey2F1
        {
            internal QuestClearUnlockUnitDataParam param;

            public <Refresh>c__AnonStorey2F1()
            {
                base..ctor();
                return;
            }

            internal bool <>m__279(QuestClearUnlockUnitDataParam p)
            {
                return (p.iname == this.param.iname);
            }
        }

        private class ViewContentParam
        {
            public SkillParam m_SkillParam;
            public List<SkillDeriveParam> m_SkillDeriveParams;

            public ViewContentParam()
            {
                base..ctor();
                return;
            }
        }
    }
}

