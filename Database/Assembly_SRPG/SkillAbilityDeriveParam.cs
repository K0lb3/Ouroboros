namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class SkillAbilityDeriveParam
    {
        public string iname;
        public SkillAbilityDeriveTriggerParam[] deriveTriggers;
        public string[] base_abils;
        public string[] derive_abils;
        public string[] base_skills;
        public string[] derive_skills;
        public int m_OriginIndex;
        private List<SkillDeriveParam> m_SkillDeriveParams;
        private List<AbilityDeriveParam> m_AbilityDeriveParams;
        [CompilerGenerated]
        private static Func<ESkillAbilityDeriveConds, bool> <>f__am$cache9;
        [CompilerGenerated]
        private static Func<string, bool> <>f__am$cacheA;
        [CompilerGenerated]
        private static Func<bool, bool> <>f__am$cacheB;

        public SkillAbilityDeriveParam(int index)
        {
            base..ctor();
            this.m_OriginIndex = index;
            return;
        }

        [CompilerGenerated]
        private static bool <CheckContainsTriggerInames>m__26E(bool value)
        {
            return value;
        }

        [CompilerGenerated]
        private static bool <Deserialize>m__26C(ESkillAbilityDeriveConds trig_type)
        {
            return ((trig_type == 0) == 0);
        }

        [CompilerGenerated]
        private static bool <Deserialize>m__26D(string trig_iname)
        {
            return (string.IsNullOrEmpty(trig_iname) == 0);
        }

        public bool CheckContainsTriggerIname(ESkillAbilityDeriveConds conditionsType, string triggerIname)
        {
            int num;
            int num2;
            if (string.IsNullOrEmpty(triggerIname) == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            num = triggerIname.GetHashCode();
            num2 = 0;
            goto Label_006D;
        Label_001B:
            if (this.deriveTriggers[num2].m_TriggerType == conditionsType)
            {
                goto Label_0033;
            }
            goto Label_0069;
        Label_0033:
            if (string.IsNullOrEmpty(this.deriveTriggers[num2].m_TriggerIname) == null)
            {
                goto Label_004F;
            }
            goto Label_0069;
        Label_004F:
            if (this.deriveTriggers[num2].m_TriggerIname.GetHashCode() != num)
            {
                goto Label_0069;
            }
            return 1;
        Label_0069:
            num2 += 1;
        Label_006D:
            if (num2 < ((int) this.deriveTriggers.Length))
            {
                goto Label_001B;
            }
            return 0;
        }

        public bool CheckContainsTriggerInames(SkillAbilityDeriveTriggerParam[] searchKeyTriggerParam)
        {
            bool[] flagArray;
            int num;
            int num2;
            if (searchKeyTriggerParam != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (((int) searchKeyTriggerParam.Length) == ((int) this.deriveTriggers.Length))
            {
                goto Label_001A;
            }
            return 0;
        Label_001A:
            flagArray = new bool[(int) searchKeyTriggerParam.Length];
            num = 0;
            goto Label_00CE;
        Label_002A:
            if (this.deriveTriggers[num].m_TriggerType == searchKeyTriggerParam[num].m_TriggerType)
            {
                goto Label_0049;
            }
            goto Label_00CA;
        Label_0049:
            if (string.IsNullOrEmpty(this.deriveTriggers[num].m_TriggerIname) == null)
            {
                goto Label_0065;
            }
            goto Label_00CA;
        Label_0065:
            num2 = 0;
            goto Label_00C1;
        Label_006C:
            if (flagArray[num2] == null)
            {
                goto Label_0079;
            }
            goto Label_00BD;
        Label_0079:
            if (string.IsNullOrEmpty(searchKeyTriggerParam[num2].m_TriggerIname) == null)
            {
                goto Label_0090;
            }
            goto Label_00BD;
        Label_0090:
            if (this.deriveTriggers[num].m_TriggerIname.GetHashCode() != searchKeyTriggerParam[num2].m_TriggerIname.GetHashCode())
            {
                goto Label_00BD;
            }
            flagArray[num2] = 1;
            goto Label_00CA;
        Label_00BD:
            num2 += 1;
        Label_00C1:
            if (num2 < ((int) searchKeyTriggerParam.Length))
            {
                goto Label_006C;
            }
        Label_00CA:
            num += 1;
        Label_00CE:
            if (num < ((int) this.deriveTriggers.Length))
            {
                goto Label_002A;
            }
            if (<>f__am$cacheB != null)
            {
                goto Label_00F5;
            }
            <>f__am$cacheB = new Func<bool, bool>(SkillAbilityDeriveParam.<CheckContainsTriggerInames>m__26E);
        Label_00F5:
            return ((Enumerable.Count<bool>(flagArray, <>f__am$cacheB) < ((int) searchKeyTriggerParam.Length)) == 0);
        }

        private void Deserialize(JSON_SkillAbilityDeriveParam json)
        {
            string[] textArray1;
            ESkillAbilityDeriveConds[] condsArray1;
            ESkillAbilityDeriveConds[] condsArray;
            string[] strArray;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            this.iname = json.iname;
            condsArray1 = new ESkillAbilityDeriveConds[] { json.trig_type_1, json.trig_type_2, json.trig_type_3 };
            condsArray = condsArray1;
            if (<>f__am$cache9 != null)
            {
                goto Label_0047;
            }
            <>f__am$cache9 = new Func<ESkillAbilityDeriveConds, bool>(SkillAbilityDeriveParam.<Deserialize>m__26C);
        Label_0047:
            condsArray = Enumerable.ToArray<ESkillAbilityDeriveConds>(Enumerable.Where<ESkillAbilityDeriveConds>(condsArray, <>f__am$cache9));
            textArray1 = new string[] { json.trig_iname_1, json.trig_iname_2, json.trig_iname_3 };
            strArray = textArray1;
            if (<>f__am$cacheA != null)
            {
                goto Label_0092;
            }
            <>f__am$cacheA = new Func<string, bool>(SkillAbilityDeriveParam.<Deserialize>m__26D);
        Label_0092:
            strArray = Enumerable.ToArray<string>(Enumerable.Where<string>(strArray, <>f__am$cacheA));
            this.deriveTriggers = new SkillAbilityDeriveTriggerParam[(int) strArray.Length];
            num = 0;
            goto Label_00CE;
        Label_00B7:
            this.deriveTriggers[num] = new SkillAbilityDeriveTriggerParam(condsArray[num], strArray[num]);
            num += 1;
        Label_00CE:
            if (num < ((int) strArray.Length))
            {
                goto Label_00B7;
            }
            if (json.base_abils == null)
            {
                goto Label_011E;
            }
            this.base_abils = new string[(int) json.base_abils.Length];
            num2 = 0;
            goto Label_0110;
        Label_00FC:
            this.base_abils[num2] = json.base_abils[num2];
            num2 += 1;
        Label_0110:
            if (num2 < ((int) this.base_abils.Length))
            {
                goto Label_00FC;
            }
        Label_011E:
            if (json.derive_abils == null)
            {
                goto Label_016B;
            }
            this.derive_abils = new string[(int) json.derive_abils.Length];
            num3 = 0;
            goto Label_015C;
        Label_0144:
            this.derive_abils[num3] = json.derive_abils[num3];
            num3 += 1;
        Label_015C:
            if (num3 < ((int) this.derive_abils.Length))
            {
                goto Label_0144;
            }
        Label_016B:
            if (json.base_skills == null)
            {
                goto Label_01B8;
            }
            this.base_skills = new string[(int) json.base_skills.Length];
            num4 = 0;
            goto Label_01A9;
        Label_0191:
            this.base_skills[num4] = json.base_skills[num4];
            num4 += 1;
        Label_01A9:
            if (num4 < ((int) this.base_skills.Length))
            {
                goto Label_0191;
            }
        Label_01B8:
            if (json.base_skills == null)
            {
                goto Label_0205;
            }
            this.derive_skills = new string[(int) json.derive_skills.Length];
            num5 = 0;
            goto Label_01F6;
        Label_01DE:
            this.derive_skills[num5] = json.derive_skills[num5];
            num5 += 1;
        Label_01F6:
            if (num5 < ((int) this.derive_skills.Length))
            {
                goto Label_01DE;
            }
        Label_0205:
            return;
        }

        public void Deserialize(JSON_SkillAbilityDeriveParam json, MasterParam masterParam)
        {
            this.Deserialize(json);
            this.FindSkillAbilityDeriveParams(masterParam);
            return;
        }

        private void FindSkillAbilityDeriveParams(MasterParam masterParam)
        {
            if (masterParam == null)
            {
                goto Label_0020;
            }
            this.m_SkillDeriveParams = this.GetSkillDeriveParams(masterParam);
            this.m_AbilityDeriveParams = this.GetAbilityDeriveParams(masterParam);
        Label_0020:
            return;
        }

        private List<AbilityDeriveParam> GetAbilityDeriveParams(MasterParam masterParam)
        {
            List<AbilityDeriveParam> list;
            int num;
            string str;
            string str2;
            AbilityDeriveParam param;
            list = new List<AbilityDeriveParam>();
            if (this.base_abils == null)
            {
                goto Label_001C;
            }
            if (this.derive_abils != null)
            {
                goto Label_001E;
            }
        Label_001C:
            return list;
        Label_001E:
            num = 0;
            goto Label_00A9;
        Label_0025:
            str = this.base_abils[num];
            str2 = this.derive_abils[num];
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0052;
            }
            if (string.IsNullOrEmpty(str2) == null)
            {
                goto Label_0052;
            }
            goto Label_00A5;
        Label_0052:
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0062;
            }
            goto Label_00A5;
        Label_0062:
            if (string.IsNullOrEmpty(str2) == null)
            {
                goto Label_0072;
            }
            goto Label_00A5;
        Label_0072:
            param = new AbilityDeriveParam();
            param.m_BaseParam = masterParam.GetAbilityParam(str);
            param.m_DeriveParam = masterParam.GetAbilityParam(str2);
            param.m_SkillAbilityDeriveParam = this;
            list.Add(param);
        Label_00A5:
            num += 1;
        Label_00A9:
            if (num < ((int) this.base_abils.Length))
            {
                goto Label_0025;
            }
            return list;
        }

        private List<SkillDeriveParam> GetSkillDeriveParams(MasterParam masterParam)
        {
            List<SkillDeriveParam> list;
            int num;
            string str;
            string str2;
            SkillDeriveParam param;
            list = new List<SkillDeriveParam>();
            if (this.base_skills == null)
            {
                goto Label_001C;
            }
            if (this.derive_skills != null)
            {
                goto Label_001E;
            }
        Label_001C:
            return list;
        Label_001E:
            num = 0;
            goto Label_00A9;
        Label_0025:
            str = this.base_skills[num];
            str2 = this.derive_skills[num];
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0052;
            }
            if (string.IsNullOrEmpty(str2) == null)
            {
                goto Label_0052;
            }
            goto Label_00A5;
        Label_0052:
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0062;
            }
            goto Label_00A5;
        Label_0062:
            if (string.IsNullOrEmpty(str2) == null)
            {
                goto Label_0072;
            }
            goto Label_00A5;
        Label_0072:
            param = new SkillDeriveParam();
            param.m_BaseParam = masterParam.GetSkillParam(str);
            param.m_DeriveParam = masterParam.GetSkillParam(str2);
            param.m_SkillAbilityDeriveParam = this;
            list.Add(param);
        Label_00A5:
            num += 1;
        Label_00A9:
            if (num < ((int) this.base_skills.Length))
            {
                goto Label_0025;
            }
            return list;
        }

        public string GetTriggerArtifactIname(int index)
        {
            SkillAbilityDeriveTriggerParam param;
            if (this.deriveTriggers == null)
            {
                goto Label_0035;
            }
            if (index >= ((int) this.deriveTriggers.Length))
            {
                goto Label_0035;
            }
            param = this.deriveTriggers[index];
            if (param.m_TriggerType != 1)
            {
                goto Label_0035;
            }
            return param.m_TriggerIname;
        Label_0035:
            return string.Empty;
        }

        public ArtifactParam GetTriggerArtifactParam(int index)
        {
            string str;
            str = this.GetTriggerArtifactIname(index);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0015;
            }
            return null;
        Label_0015:
            return MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(str);
        }

        public List<SkillDeriveParam> SkillDeriveParams
        {
            get
            {
                return this.m_SkillDeriveParams;
            }
        }

        public List<AbilityDeriveParam> AbilityDeriveParams
        {
            get
            {
                return this.m_AbilityDeriveParams;
            }
        }
    }
}

