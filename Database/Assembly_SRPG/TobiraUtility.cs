namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class TobiraUtility
    {
        [CompilerGenerated]
        private static Func<Json_Tobira, bool> <>f__am$cache0;

        public TobiraUtility()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <GetUnlockTobiraNum>m__168(Json_Tobira json_tobira)
        {
            if (json_tobira.category == null)
            {
                goto Label_0018;
            }
            return ((json_tobira.lv < 1) == 0);
        Label_0018:
            return 0;
        }

        public static void CalcTobiraParameter(string unit_iname, TobiraParam.Category category, int lv, ref BaseStatus add, ref BaseStatus scale)
        {
            TobiraData data;
            UnitParam param;
            data = new TobiraData();
            if (data.Setup(unit_iname, category, lv) == null)
            {
                goto Label_0045;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(unit_iname);
            SkillData.GetHomePassiveBuffStatus(data.ParameterBuffSkill, (param == null) ? 0 : param.element, add, scale);
        Label_0045:
            return;
        }

        public static List<TobiraData> CreateDummyData(UnitData unit)
        {
            TobiraParam[] paramArray;
            List<TobiraData> list;
            int num;
            TobiraData data;
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraListForUnit(unit.UnitParam.iname);
            list = new List<TobiraData>();
            num = 0;
            goto Label_0062;
        Label_0028:
            if (paramArray[num] != null)
            {
                goto Label_0035;
            }
            goto Label_005E;
        Label_0035:
            data = new TobiraData();
            data.Setup(paramArray[num].UnitIname, paramArray[num].TobiraCategory, (num % 5) + 1);
            list.Add(data);
        Label_005E:
            num += 1;
        Label_0062:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0028;
            }
            return list;
        }

        public static void CreateDummyData(Json_Unit[] units)
        {
            int num;
            Json_Unit unit;
            TobiraParam[] paramArray;
            List<Json_Tobira> list;
            int num2;
            TobiraParam param;
            Json_Tobira tobira;
            if (units != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            num = 0;
            goto Label_009E;
        Label_000E:
            unit = units[num];
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraListForUnit(unit.iname);
            list = new List<Json_Tobira>();
            num2 = 0;
            goto Label_0078;
        Label_0036:
            param = paramArray[num2];
            if (param != null)
            {
                goto Label_0048;
            }
            goto Label_0072;
        Label_0048:
            tobira = new Json_Tobira();
            tobira.category = param.TobiraCategory;
            tobira.lv = (num2 % 5) + 1;
            list.Add(tobira);
        Label_0072:
            num2 += 1;
        Label_0078:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_0036;
            }
            if (list.Count <= 0)
            {
                goto Label_009A;
            }
            unit.doors = list.ToArray();
        Label_009A:
            num += 1;
        Label_009E:
            if (num < ((int) units.Length))
            {
                goto Label_000E;
            }
            return;
        }

        public static SkillData CreateParameterBuffSkill(TobiraParam tobiraParam, int currentLv)
        {
            int num;
            SkillData data;
            if (tobiraParam != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            num = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraLvCap;
            data = new SkillData();
            data.Setup(tobiraParam.SkillIname, currentLv, num, null);
            return data;
        }

        public static int GetAdditionalUnitLevelCapWithTobira(List<TobiraData> list)
        {
            int num;
            int num2;
            int num3;
            num = 0;
            if (list != null)
            {
                goto Label_000A;
            }
            return num;
        Label_000A:
            num2 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraUnitLvCapBonus;
            num3 = 0;
            goto Label_008B;
        Label_002B:
            if (list[num3] != null)
            {
                goto Label_003C;
            }
            goto Label_0087;
        Label_003C:
            if (list[num3].Param != null)
            {
                goto Label_0052;
            }
            goto Label_0087;
        Label_0052:
            if (list[num3].IsUnlocked != null)
            {
                goto Label_0068;
            }
            goto Label_0087;
        Label_0068:
            if (list[num3].Param.TobiraCategory != null)
            {
                goto Label_0083;
            }
            goto Label_0087;
        Label_0083:
            num += num2;
        Label_0087:
            num3 += 1;
        Label_008B:
            if (num3 < list.Count)
            {
                goto Label_002B;
            }
            return num;
        }

        public static int GetAdditionalUnitLevelCapWithUnlockNum(int unlockNum)
        {
            int num;
            int num2;
            int num3;
            num = 0;
            if (unlockNum != null)
            {
                goto Label_000A;
            }
            return num;
        Label_000A:
            num2 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraUnitLvCapBonus;
            num3 = 0;
            goto Label_0033;
        Label_002B:
            num += num2;
            num3 += 1;
        Label_0033:
            if (num3 < unlockNum)
            {
                goto Label_002B;
            }
            return num;
        }

        public static unsafe string GetOverwriteLeaderSkill(List<TobiraData> list)
        {
            TobiraData data;
            TobiraData data2;
            List<TobiraData>.Enumerator enumerator;
            if (list.Count <= 0)
            {
                goto Label_0096;
            }
            data = null;
            enumerator = list.GetEnumerator();
        Label_0015:
            try
            {
                goto Label_0062;
            Label_001A:
                data2 = &enumerator.Current;
                if (data2.IsLearnedLeaderSkill != null)
                {
                    goto Label_0032;
                }
                goto Label_0062;
            Label_0032:
                if (data == null)
                {
                    goto Label_0060;
                }
                data = (data.Param.Priority >= data2.Param.Priority) ? data : data2;
                goto Label_0062;
            Label_0060:
                data = data2;
            Label_0062:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001A;
                }
                goto Label_007F;
            }
            finally
            {
            Label_0073:
                ((List<TobiraData>.Enumerator) enumerator).Dispose();
            }
        Label_007F:
            return ((data == null) ? string.Empty : data.LearnedLeaderSkillIname);
        Label_0096:
            return string.Empty;
        }

        public static List<string> GetOverwrittenAbilitys(UnitData unitData)
        {
            List<string> list;
            List<TobiraParam> list2;
            List<TobiraLearnAbilityParam> list3;
            List<TobiraLearnAbilityParam> list4;
            int num;
            TobiraLearnAbilityParam param;
            TobiraLearnAbilityParam[] paramArray;
            int num2;
            int num3;
            TobiraLearnAbilityParam param2;
            <GetOverwrittenAbilitys>c__AnonStorey253 storey;
            <GetOverwrittenAbilitys>c__AnonStorey254 storey2;
            storey = new <GetOverwrittenAbilitys>c__AnonStorey253();
            list = new List<string>();
            if (unitData != null)
            {
                goto Label_0015;
            }
            return list;
        Label_0015:
            if (unitData.UnitParam != null)
            {
                goto Label_0022;
            }
            return list;
        Label_0022:
            list2 = Enumerable.ToList<TobiraParam>(MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraListForUnit(unitData.UnitParam.iname));
            list3 = new List<TobiraLearnAbilityParam>();
            storey.learnAbilitys = unitData.TobiraMasterAbilitys;
            if (list2 == null)
            {
                goto Label_016E;
            }
            list4 = new List<TobiraLearnAbilityParam>();
            num = 0;
            goto Label_00C0;
        Label_0069:
            if (list2[num] != null)
            {
                goto Label_007B;
            }
            goto Label_00BA;
        Label_007B:
            paramArray = list2[num].LeanAbilityParam;
            num2 = 0;
            goto Label_00AF;
        Label_0092:
            param = paramArray[num2];
            list4.Add(param);
            list3.Add(param);
            num2 += 1;
        Label_00AF:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_0092;
            }
        Label_00BA:
            num += 1;
        Label_00C0:
            if (num < list2.Count)
            {
                goto Label_0069;
            }
            list4.RemoveAll(new Predicate<TobiraLearnAbilityParam>(storey.<>m__169));
            num3 = 0;
            goto Label_0161;
        Label_00E9:
            storey2 = new <GetOverwrittenAbilitys>c__AnonStorey254();
            storey2.overWriteAbilityName = list4[num3].AbilityOverwrite;
            goto Label_014A;
        Label_0109:
            list.Add(storey2.overWriteAbilityName);
            param2 = list3.Find(new Predicate<TobiraLearnAbilityParam>(storey2.<>m__16A));
            storey2.overWriteAbilityName = (param2 == null) ? string.Empty : param2.AbilityOverwrite;
        Label_014A:
            if (string.IsNullOrEmpty(storey2.overWriteAbilityName) == null)
            {
                goto Label_0109;
            }
            num3 += 1;
        Label_0161:
            if (num3 < list4.Count)
            {
                goto Label_00E9;
            }
        Label_016E:
            return list;
        }

        public static List<SkillData> GetParameterBuffSkills(List<TobiraData> list)
        {
            List<SkillData> list2;
            int num;
            list2 = new List<SkillData>();
            if (list != null)
            {
                goto Label_000E;
            }
            return list2;
        Label_000E:
            num = 0;
            goto Label_006D;
        Label_0015:
            if (list[num] != null)
            {
                goto Label_0026;
            }
            goto Label_0069;
        Label_0026:
            if (list[num].ParameterBuffSkill != null)
            {
                goto Label_003C;
            }
            goto Label_0069;
        Label_003C:
            if (list[num].ParameterBuffSkill.SkillParam != null)
            {
                goto Label_0057;
            }
            goto Label_0069;
        Label_0057:
            list2.Add(list[num].ParameterBuffSkill);
        Label_0069:
            num += 1;
        Label_006D:
            if (num < list.Count)
            {
                goto Label_0015;
            }
            return list2;
        }

        public static int GetTobiraUnlockLevel(string unit_iname)
        {
            int num;
            GameManager manager;
            TobiraConditionParam[] paramArray;
            TobiraConditionParam param;
            TobiraConditionParam[] paramArray2;
            int num2;
            num = 0;
            paramArray = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetTobiraConditionsForUnit(unit_iname, 0);
            if (paramArray != null)
            {
                goto Label_001E;
            }
            return num;
        Label_001E:
            paramArray2 = paramArray;
            num2 = 0;
            goto Label_008E;
        Label_0029:
            param = paramArray2[num2];
            if (param.CondType != 1)
            {
                goto Label_0088;
            }
            if (string.IsNullOrEmpty(param.CondUnit.UnitIname) == null)
            {
                goto Label_0061;
            }
            num = param.CondUnit.Level;
            goto Label_0099;
        Label_0061:
            if ((unit_iname == param.CondUnit.UnitIname) == null)
            {
                goto Label_0088;
            }
            num = param.CondUnit.Level;
            goto Label_0099;
        Label_0088:
            num2 += 1;
        Label_008E:
            if (num2 < ((int) paramArray2.Length))
            {
                goto Label_0029;
            }
        Label_0099:
            return num;
        }

        public static int GetUnlockTobiraNum(Json_Tobira[] json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (<>f__am$cache0 != null)
            {
                goto Label_0021;
            }
            <>f__am$cache0 = new Func<Json_Tobira, bool>(TobiraUtility.<GetUnlockTobiraNum>m__168);
        Label_0021:
            return Enumerable.Count<Json_Tobira>(json, <>f__am$cache0);
        }

        public static bool IsClearAllToboraConditions(UnitData unit_data, TobiraParam.Category tobira_category)
        {
            TobiraConditionParam[] paramArray;
            bool flag;
            List<ConditionsResult> list;
            int num;
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraConditionsForUnit(unit_data.UnitID, tobira_category);
            if (paramArray != null)
            {
                goto Label_001F;
            }
            return 1;
        Label_001F:
            flag = 1;
            list = TobiraConditionsCheck(unit_data, paramArray);
            num = 0;
            goto Label_004C;
        Label_0030:
            if (list[num].isClear != null)
            {
                goto Label_0048;
            }
            flag = 0;
            goto Label_0058;
        Label_0048:
            num += 1;
        Label_004C:
            if (num < list.Count)
            {
                goto Label_0030;
            }
        Label_0058:
            return flag;
        }

        public static bool IsClearAllToboraRecipe(UnitData unit_data, TobiraParam.Category tobira_category, int tobiraLv)
        {
            bool flag;
            List<ConditionsResult> list;
            int num;
            flag = 1;
            list = TobiraRecipeCheck(unit_data, tobira_category, tobiraLv);
            num = 0;
            goto Label_002E;
        Label_0012:
            if (list[num].isClear != null)
            {
                goto Label_002A;
            }
            flag = 0;
            goto Label_003A;
        Label_002A:
            num += 1;
        Label_002E:
            if (num < list.Count)
            {
                goto Label_0012;
            }
        Label_003A:
            return flag;
        }

        public static List<ConditionsResult> TobiraConditionsCheck(UnitData unitData, TobiraConditionParam[] conds)
        {
            GameManager manager;
            List<ConditionsResult> list;
            int num;
            TobiraCondsUnitParam param;
            UnitData data;
            UnitParam param2;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            list = new List<ConditionsResult>();
            num = 0;
            goto Label_012A;
        Label_0013:
            if (conds[num].CondType != 1)
            {
                goto Label_00FF;
            }
            param = conds[num].CondUnit;
            data = unitData;
            param2 = null;
            if (param.HasFlag(1L) != null)
            {
                goto Label_0065;
            }
            data = manager.Player.FindUnitDataByUnitID(param.UnitIname);
            if (data != null)
            {
                goto Label_0065;
            }
            param2 = manager.GetUnitParam(param.UnitIname);
        Label_0065:
            if (param.HasFlag(2L) == null)
            {
                goto Label_0087;
            }
            list.Add(new ConditionsResult_UnitLv(data, param2, param.Level));
        Label_0087:
            if (param.HasFlag(0x10L) == null)
            {
                goto Label_00B0;
            }
            list.Add(new ConditionsResult_TobiraLv(data, param2, param.TobiraCategory, param.TobiraLv));
        Label_00B0:
            if (param.HasFlag(4L) == null)
            {
                goto Label_00D2;
            }
            list.Add(new ConditionsResult_AwakeLv(data, param2, param.AwakeLevel));
        Label_00D2:
            if (param.HasFlag(8L) == null)
            {
                goto Label_0126;
            }
            list.Add(new ConditionsResult_JobLv(data, param2, param.JobIname, param.JobLevel));
            goto Label_0126;
        Label_00FF:
            if (conds[num].CondType != 2)
            {
                goto Label_0126;
            }
            list.Add(new ConditionsResult_QuestClear(manager.FindQuest(conds[num].CondIname)));
        Label_0126:
            num += 1;
        Label_012A:
            if (num < ((int) conds.Length))
            {
                goto Label_0013;
            }
            if (list.Count >= 1)
            {
                goto Label_014A;
            }
            list.Add(new ConditionsResult_TobiraNoConditions());
        Label_014A:
            return list;
        }

        public static List<ConditionsResult> TobiraRecipeCheck(UnitData unitData, TobiraParam.Category category, int targetLevel)
        {
            List<ConditionsResult> list;
            GameManager manager;
            TobiraRecipeParam param;
            int num;
            ItemParam param2;
            int num2;
            ItemParam param3;
            int num3;
            ItemParam param4;
            TobiraRecipeMaterialParam param5;
            TobiraRecipeMaterialParam[] paramArray;
            int num4;
            int num5;
            ItemParam param6;
            list = new List<ConditionsResult>();
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            param = manager.MasterParam.GetTobiraRecipe(unitData.UnitID, category, targetLevel);
            if (param.Cost <= 0)
            {
                goto Label_003D;
            }
            list.Add(new ConditionsResult_HasGold(param.Cost));
        Label_003D:
            if (param.UnitPieceNum <= 0)
            {
                goto Label_0065;
            }
            list.Add(new ConditionsResult_HasItem(unitData.UnitParam.piece, param.UnitPieceNum));
        Label_0065:
            if (param.ElementNum <= 0)
            {
                goto Label_00A9;
            }
            num = param.ElementNum;
            param2 = unitData.GetElementPieceParam();
            if (param2 != null)
            {
                goto Label_0096;
            }
            DebugUtility.LogWarning("対応する属性欠片が見つかりませんでした。");
            goto Label_00A9;
        Label_0096:
            list.Add(new ConditionsResult_HasItem(param2.iname, num));
        Label_00A9:
            if (param.UnlockElementNum <= 0)
            {
                goto Label_00F5;
            }
            num2 = param.UnlockElementNum;
            param3 = manager.GetItemParam(unitData.GetUnlockTobiraElementID());
            if (param3 != null)
            {
                goto Label_00E1;
            }
            DebugUtility.LogWarning("属性に対応する扉用素材が見つかりませんでした。");
            goto Label_00F5;
        Label_00E1:
            list.Add(new ConditionsResult_HasItem(param3.iname, num2));
        Label_00F5:
            if (param.UnlockBirthNum <= 0)
            {
                goto Label_0141;
            }
            num3 = param.UnlockBirthNum;
            param4 = manager.GetItemParam(unitData.GetUnlockTobiraBirthID());
            if (param4 != null)
            {
                goto Label_012D;
            }
            DebugUtility.LogWarning("出自に対応する扉用素材が見つかりませんでした。");
            goto Label_0141;
        Label_012D:
            list.Add(new ConditionsResult_HasItem(param4.iname, num3));
        Label_0141:
            paramArray = param.Materials;
            num4 = 0;
            goto Label_01BD;
        Label_0151:
            param5 = paramArray[num4];
            if (param5 == null)
            {
                goto Label_01B7;
            }
            if (string.IsNullOrEmpty(param5.Iname) == null)
            {
                goto Label_0175;
            }
            goto Label_01B7;
        Label_0175:
            num5 = param5.Num;
            param6 = manager.GetItemParam(param5.Iname);
            if (param6 != null)
            {
                goto Label_01A3;
            }
            DebugUtility.LogWarning("アイテムが見つかりませんでした。");
            goto Label_01B7;
        Label_01A3:
            list.Add(new ConditionsResult_HasItem(param6.iname, num5));
        Label_01B7:
            num4 += 1;
        Label_01BD:
            if (num4 < ((int) paramArray.Length))
            {
                goto Label_0151;
            }
            return list;
        }

        public static string ToJsonString(List<TobiraData> list)
        {
            StringBuilder builder;
            int num;
            if (list == null)
            {
                goto Label_0078;
            }
            if (list.Count <= 0)
            {
                goto Label_0078;
            }
            builder = new StringBuilder(0x200);
            builder.Append("\"doors\":[");
            num = 0;
            goto Label_0059;
        Label_0030:
            if (num == null)
            {
                goto Label_0042;
            }
            builder.Append(",");
        Label_0042:
            builder.Append(list[num].ToJsonString());
            num += 1;
        Label_0059:
            if (num < list.Count)
            {
                goto Label_0030;
            }
            builder.Append("]");
            return builder.ToString();
        Label_0078:
            return string.Empty;
        }

        public static void TryCraeteAbilityData(TobiraParam tobiraParam, int currentLv, List<AbilityData> newAbilitys, List<AbilityData> oldAbilitys, bool isJust)
        {
            List<TobiraLearnAbilityParam> list;
            int num;
            AbilityData data;
            AbilityData data2;
            <TryCraeteAbilityData>c__AnonStorey255 storey;
            storey = new <TryCraeteAbilityData>c__AnonStorey255();
            storey.currentLv = currentLv;
            list = null;
            if (isJust == null)
            {
                goto Label_003B;
            }
            list = Enumerable.ToList<TobiraLearnAbilityParam>(Enumerable.Where<TobiraLearnAbilityParam>(tobiraParam.LeanAbilityParam, new Func<TobiraLearnAbilityParam, bool>(storey.<>m__16B)));
            goto Label_0059;
        Label_003B:
            list = Enumerable.ToList<TobiraLearnAbilityParam>(Enumerable.Where<TobiraLearnAbilityParam>(tobiraParam.LeanAbilityParam, new Func<TobiraLearnAbilityParam, bool>(storey.<>m__16C)));
        Label_0059:
            newAbilitys.Clear();
            oldAbilitys.Clear();
            newAbilitys.Capacity = list.Count;
            oldAbilitys.Capacity = list.Count;
            num = 0;
            goto Label_010C;
        Label_0084:
            data = new AbilityData();
            data2 = null;
            data.Setup(null, 0L, list[num].AbilityIname, 1, 0);
            newAbilitys.Add(data);
            if (string.IsNullOrEmpty(list[num].AbilityOverwrite) != null)
            {
                goto Label_0101;
            }
            if (list[num].AbilityAddType == 1)
            {
                goto Label_00E4;
            }
            if (list[num].AbilityAddType != 3)
            {
                goto Label_0101;
            }
        Label_00E4:
            data2 = new AbilityData();
            data2.Setup(null, 0L, list[num].AbilityOverwrite, 0, 0);
        Label_0101:
            oldAbilitys.Add(data2);
            num += 1;
        Label_010C:
            if (num < list.Count)
            {
                goto Label_0084;
            }
            return;
        }

        public static void TryCraeteLeaderSkill(TobiraParam tobiraParam, int currentLv, ref SkillData skillData, bool isJust)
        {
            if (isJust == null)
            {
                goto Label_002E;
            }
            if (tobiraParam.OverwriteLeaderSkillLevel != currentLv)
            {
                goto Label_0051;
            }
            *(skillData) = new SkillData();
            *(skillData).Setup(tobiraParam.OverwriteLeaderSkillIname, 1, 1, null);
            goto Label_0051;
        Label_002E:
            if (tobiraParam.OverwriteLeaderSkillLevel > currentLv)
            {
                goto Label_0051;
            }
            *(skillData) = new SkillData();
            *(skillData).Setup(tobiraParam.OverwriteLeaderSkillIname, 1, 1, null);
        Label_0051:
            return;
        }

        [CompilerGenerated]
        private sealed class <GetOverwrittenAbilitys>c__AnonStorey253
        {
            internal List<AbilityData> learnAbilitys;

            public <GetOverwrittenAbilitys>c__AnonStorey253()
            {
                base..ctor();
                return;
            }

            internal bool <>m__169(TobiraLearnAbilityParam lta)
            {
                int num;
                num = 0;
                goto Label_003A;
            Label_0007:
                if ((lta.AbilityIname == this.learnAbilitys[num].AbilityID) == null)
                {
                    goto Label_0036;
                }
                if (lta.AbilityAddType != 3)
                {
                    goto Label_0036;
                }
                return 0;
            Label_0036:
                num += 1;
            Label_003A:
                if (num < this.learnAbilitys.Count)
                {
                    goto Label_0007;
                }
                return 1;
            }
        }

        [CompilerGenerated]
        private sealed class <GetOverwrittenAbilitys>c__AnonStorey254
        {
            internal string overWriteAbilityName;

            public <GetOverwrittenAbilitys>c__AnonStorey254()
            {
                base..ctor();
                return;
            }

            internal bool <>m__16A(TobiraLearnAbilityParam abi)
            {
                return (abi.AbilityIname == this.overWriteAbilityName);
            }
        }

        [CompilerGenerated]
        private sealed class <TryCraeteAbilityData>c__AnonStorey255
        {
            internal int currentLv;

            public <TryCraeteAbilityData>c__AnonStorey255()
            {
                base..ctor();
                return;
            }

            internal bool <>m__16B(TobiraLearnAbilityParam learnAbil)
            {
                return (learnAbil.Level == this.currentLv);
            }

            internal bool <>m__16C(TobiraLearnAbilityParam learnAbil)
            {
                return ((learnAbil.Level > this.currentLv) == 0);
            }
        }
    }
}

