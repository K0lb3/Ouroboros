namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class SkillAbilityDeriveData
    {
        public SkillAbilityDeriveParam m_SkillAbilityDeriveParam;
        public List<SkillDeriveData> m_SkillDeriveData;
        public List<AbilityDeriveData> m_AbilityDeriveData;
        public HashSet<SkillAbilityDeriveParam> m_AdditionalSkillAbilityDeriveParam;
        [CompilerGenerated]
        private static Func<SkillDeriveParam, SkillDeriveData> <>f__am$cache4;
        [CompilerGenerated]
        private static Func<AbilityDeriveParam, AbilityDeriveData> <>f__am$cache5;
        [CompilerGenerated]
        private static Func<SkillDeriveData, bool> <>f__am$cache6;
        [CompilerGenerated]
        private static Func<SkillDeriveData, SkillDeriveParam> <>f__am$cache7;
        [CompilerGenerated]
        private static Func<AbilityDeriveData, bool> <>f__am$cache8;
        [CompilerGenerated]
        private static Func<AbilityDeriveData, AbilityDeriveParam> <>f__am$cache9;

        public SkillAbilityDeriveData()
        {
            this.m_AdditionalSkillAbilityDeriveParam = new HashSet<SkillAbilityDeriveParam>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <GetAvailableAbilityDeriveParams>m__275(AbilityDeriveData deriveData)
        {
            return (deriveData.IsDisable == 0);
        }

        [CompilerGenerated]
        private static AbilityDeriveParam <GetAvailableAbilityDeriveParams>m__276(AbilityDeriveData deriveData)
        {
            return deriveData.Param;
        }

        [CompilerGenerated]
        private static bool <GetAvailableSkillDeriveParams>m__273(SkillDeriveData deriveData)
        {
            return (deriveData.IsDisable == 0);
        }

        [CompilerGenerated]
        private static SkillDeriveParam <GetAvailableSkillDeriveParams>m__274(SkillDeriveData deriveData)
        {
            return deriveData.Param;
        }

        [CompilerGenerated]
        private static SkillDeriveData <Setup>m__271(SkillDeriveParam param)
        {
            return new SkillDeriveData(param);
        }

        [CompilerGenerated]
        private static AbilityDeriveData <Setup>m__272(AbilityDeriveParam param)
        {
            return new AbilityDeriveData(param);
        }

        public bool CheckContainsTriggerIname(ESkillAbilityDeriveConds triggerType, string triggerIname)
        {
            return this.m_SkillAbilityDeriveParam.CheckContainsTriggerIname(triggerType, triggerIname);
        }

        public bool CheckContainsTriggerInames(SkillAbilityDeriveTriggerParam[] searchKeyTriggerParam)
        {
            return this.m_SkillAbilityDeriveParam.CheckContainsTriggerInames(searchKeyTriggerParam);
        }

        public List<AbilityDeriveParam> GetAvailableAbilityDeriveParams()
        {
            if (<>f__am$cache8 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache8 = new Func<AbilityDeriveData, bool>(SkillAbilityDeriveData.<GetAvailableAbilityDeriveParams>m__275);
        Label_001E:
            if (<>f__am$cache9 != null)
            {
                goto Label_0040;
            }
            <>f__am$cache9 = new Func<AbilityDeriveData, AbilityDeriveParam>(SkillAbilityDeriveData.<GetAvailableAbilityDeriveParams>m__276);
        Label_0040:
            return Enumerable.ToList<AbilityDeriveParam>(Enumerable.Select<AbilityDeriveData, AbilityDeriveParam>(Enumerable.Where<AbilityDeriveData>(this.m_AbilityDeriveData, <>f__am$cache8), <>f__am$cache9));
        }

        public List<SkillDeriveParam> GetAvailableSkillDeriveParams()
        {
            if (<>f__am$cache6 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache6 = new Func<SkillDeriveData, bool>(SkillAbilityDeriveData.<GetAvailableSkillDeriveParams>m__273);
        Label_001E:
            if (<>f__am$cache7 != null)
            {
                goto Label_0040;
            }
            <>f__am$cache7 = new Func<SkillDeriveData, SkillDeriveParam>(SkillAbilityDeriveData.<GetAvailableSkillDeriveParams>m__274);
        Label_0040:
            return Enumerable.ToList<SkillDeriveParam>(Enumerable.Select<SkillDeriveData, SkillDeriveParam>(Enumerable.Where<SkillDeriveData>(this.m_SkillDeriveData, <>f__am$cache6), <>f__am$cache7));
        }

        public unsafe void Setup(SkillAbilityDeriveParam skillAbilityDeriveParam, List<SkillAbilityDeriveParam> additionalSkillAbilityDeriveParams)
        {
            IEnumerable<SkillDeriveData> enumerable;
            IEnumerable<AbilityDeriveData> enumerable2;
            Dictionary<string, int> dictionary;
            SkillDeriveData data;
            List<SkillDeriveData>.Enumerator enumerator;
            AbilityDeriveData data2;
            List<AbilityDeriveData>.Enumerator enumerator2;
            SkillAbilityDeriveParam param;
            List<SkillAbilityDeriveParam>.Enumerator enumerator3;
            List<SkillDeriveParam> list;
            List<AbilityDeriveParam> list2;
            SkillDeriveParam param2;
            List<SkillDeriveParam>.Enumerator enumerator4;
            int num;
            SkillDeriveData data3;
            AbilityDeriveParam param3;
            List<AbilityDeriveParam>.Enumerator enumerator5;
            int num2;
            AbilityDeriveData data4;
            AbilityDeriveData data5;
            List<AbilityDeriveData>.Enumerator enumerator6;
            int num3;
            SkillDeriveData data6;
            List<SkillDeriveData>.Enumerator enumerator7;
            int num4;
            this.m_SkillDeriveData = new List<SkillDeriveData>();
            this.m_AbilityDeriveData = new List<AbilityDeriveData>();
            this.m_SkillAbilityDeriveParam = skillAbilityDeriveParam;
            if (<>f__am$cache4 != null)
            {
                goto Label_0040;
            }
            <>f__am$cache4 = new Func<SkillDeriveParam, SkillDeriveData>(SkillAbilityDeriveData.<Setup>m__271);
        Label_0040:
            enumerable = Enumerable.Select<SkillDeriveParam, SkillDeriveData>(this.m_SkillAbilityDeriveParam.SkillDeriveParams, <>f__am$cache4);
            if (<>f__am$cache5 != null)
            {
                goto Label_006E;
            }
            <>f__am$cache5 = new Func<AbilityDeriveParam, AbilityDeriveData>(SkillAbilityDeriveData.<Setup>m__272);
        Label_006E:
            enumerable2 = Enumerable.Select<AbilityDeriveParam, AbilityDeriveData>(this.m_SkillAbilityDeriveParam.AbilityDeriveParams, <>f__am$cache5);
            this.m_SkillDeriveData.AddRange(enumerable);
            this.m_AbilityDeriveData.AddRange(enumerable2);
            dictionary = new Dictionary<string, int>();
            enumerator = this.m_SkillDeriveData.GetEnumerator();
        Label_00A4:
            try
            {
                goto Label_00DE;
            Label_00A9:
                data = &enumerator.Current;
                if (dictionary.ContainsKey(data.Param.BaseSkillIname) != null)
                {
                    goto Label_00DE;
                }
                dictionary.Add(data.Param.BaseSkillIname, this.MasterIndex);
            Label_00DE:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00A9;
                }
                goto Label_00FC;
            }
            finally
            {
            Label_00EF:
                ((List<SkillDeriveData>.Enumerator) enumerator).Dispose();
            }
        Label_00FC:
            enumerator2 = this.m_AbilityDeriveData.GetEnumerator();
        Label_0109:
            try
            {
                goto Label_0146;
            Label_010E:
                data2 = &enumerator2.Current;
                if (dictionary.ContainsKey(data2.Param.BaseAbilityIname) != null)
                {
                    goto Label_0146;
                }
                dictionary.Add(data2.Param.BaseAbilityIname, this.MasterIndex);
            Label_0146:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_010E;
                }
                goto Label_0164;
            }
            finally
            {
            Label_0157:
                ((List<AbilityDeriveData>.Enumerator) enumerator2).Dispose();
            }
        Label_0164:
            enumerator3 = additionalSkillAbilityDeriveParams.GetEnumerator();
        Label_016C:
            try
            {
                goto Label_02F0;
            Label_0171:
                param = &enumerator3.Current;
                if (this.m_AdditionalSkillAbilityDeriveParam.Contains(param) != null)
                {
                    goto Label_019A;
                }
                this.m_AdditionalSkillAbilityDeriveParam.Add(param);
            Label_019A:
                list = param.SkillDeriveParams;
                list2 = param.AbilityDeriveParams;
                enumerator4 = list.GetEnumerator();
            Label_01B5:
                try
                {
                    goto Label_0230;
                Label_01BA:
                    param2 = &enumerator4.Current;
                    num = -1;
                    if (dictionary.TryGetValue(param2.BaseSkillIname, &num) == null)
                    {
                        goto Label_01FE;
                    }
                    num = Mathf.Min(param2.MasterIndex, num);
                    dictionary[param2.BaseSkillIname] = num;
                    goto Label_0212;
                Label_01FE:
                    dictionary.Add(param2.BaseSkillIname, param2.MasterIndex);
                Label_0212:
                    data3 = new SkillDeriveData(param2);
                    data3.IsAdd = 1;
                    this.m_SkillDeriveData.Add(data3);
                Label_0230:
                    if (&enumerator4.MoveNext() != null)
                    {
                        goto Label_01BA;
                    }
                    goto Label_024E;
                }
                finally
                {
                Label_0241:
                    ((List<SkillDeriveParam>.Enumerator) enumerator4).Dispose();
                }
            Label_024E:
                enumerator5 = list2.GetEnumerator();
            Label_0257:
                try
                {
                    goto Label_02D2;
                Label_025C:
                    param3 = &enumerator5.Current;
                    num2 = -1;
                    if (dictionary.TryGetValue(param3.BaseAbilityIname, &num2) == null)
                    {
                        goto Label_02A0;
                    }
                    num2 = Mathf.Min(param3.MasterIndex, num2);
                    dictionary[param3.BaseAbilityIname] = num2;
                    goto Label_02B4;
                Label_02A0:
                    dictionary.Add(param3.BaseAbilityIname, param3.MasterIndex);
                Label_02B4:
                    data4 = new AbilityDeriveData(param3);
                    data4.IsAdd = 1;
                    this.m_AbilityDeriveData.Add(data4);
                Label_02D2:
                    if (&enumerator5.MoveNext() != null)
                    {
                        goto Label_025C;
                    }
                    goto Label_02F0;
                }
                finally
                {
                Label_02E3:
                    ((List<AbilityDeriveParam>.Enumerator) enumerator5).Dispose();
                }
            Label_02F0:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_0171;
                }
                goto Label_030E;
            }
            finally
            {
            Label_0301:
                ((List<SkillAbilityDeriveParam>.Enumerator) enumerator3).Dispose();
            }
        Label_030E:
            enumerator6 = this.m_AbilityDeriveData.GetEnumerator();
        Label_031B:
            try
            {
                goto Label_036D;
            Label_0320:
                data5 = &enumerator6.Current;
                num3 = -1;
                if (dictionary.TryGetValue(data5.Param.BaseAbilityIname, &num3) == null)
                {
                    goto Label_036D;
                }
                if (data5.Param.MasterIndex > num3)
                {
                    goto Label_0365;
                }
                data5.IsDisable = 0;
                goto Label_036D;
            Label_0365:
                data5.IsDisable = 1;
            Label_036D:
                if (&enumerator6.MoveNext() != null)
                {
                    goto Label_0320;
                }
                goto Label_038B;
            }
            finally
            {
            Label_037E:
                ((List<AbilityDeriveData>.Enumerator) enumerator6).Dispose();
            }
        Label_038B:
            enumerator7 = this.m_SkillDeriveData.GetEnumerator();
        Label_0398:
            try
            {
                goto Label_03EA;
            Label_039D:
                data6 = &enumerator7.Current;
                num4 = -1;
                if (dictionary.TryGetValue(data6.Param.BaseSkillIname, &num4) == null)
                {
                    goto Label_03EA;
                }
                if (data6.Param.MasterIndex > num4)
                {
                    goto Label_03E2;
                }
                data6.IsDisable = 0;
                goto Label_03EA;
            Label_03E2:
                data6.IsDisable = 1;
            Label_03EA:
                if (&enumerator7.MoveNext() != null)
                {
                    goto Label_039D;
                }
                goto Label_0408;
            }
            finally
            {
            Label_03FB:
                ((List<SkillDeriveData>.Enumerator) enumerator7).Dispose();
            }
        Label_0408:
            return;
        }

        public int MasterIndex
        {
            get
            {
                return this.m_SkillAbilityDeriveParam.m_OriginIndex;
            }
        }
    }
}

