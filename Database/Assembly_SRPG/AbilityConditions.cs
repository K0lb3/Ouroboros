namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AbilityConditions
    {
        public AbilityParam m_AbilityParam;
        public List<UnitParam> m_CondsUnits;
        public List<JobParam> m_CondsJobs;
        private string m_CondsBirth;
        private ESex m_CondsSex;
        private EElement m_CondsElem;

        public AbilityConditions()
        {
            this.m_CondsUnits = new List<UnitParam>();
            this.m_CondsJobs = new List<JobParam>();
            base..ctor();
            return;
        }

        private static string ElementToString(EElement element)
        {
            string str;
            return LocalizedText.Get(string.Format("sys.ABILITY_CONDS_ELEMENT_{0}", (int) element));
        }

        private static string InternalMakeConditionsText(params string[] arg)
        {
            object[] objArray1;
            string str;
            str = string.Join(string.Empty, arg);
            objArray1 = new object[] { str };
            return LocalizedText.Get("sys.ABILITY_CONDS_TEXT_FORMAT", objArray1);
        }

        public unsafe string MakeConditionsText()
        {
            string[] textArray7;
            string[] textArray6;
            object[] objArray11;
            string[] textArray5;
            object[] objArray10;
            string[] textArray4;
            object[] objArray9;
            string[] textArray3;
            object[] objArray8;
            string[] textArray2;
            object[] objArray7;
            object[] objArray6;
            string[] textArray1;
            object[] objArray5;
            object[] objArray4;
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            StringBuilder builder;
            string str;
            string str2;
            string str3;
            UnitParam param;
            List<UnitParam>.Enumerator enumerator;
            JobParam param2;
            List<JobParam>.Enumerator enumerator2;
            string str4;
            string str5;
            string str6;
            string str7;
            string str8;
            string str9;
            UnitParam param3;
            List<UnitParam>.Enumerator enumerator3;
            string str10;
            string str11;
            string str12;
            string str13;
            JobParam param4;
            List<JobParam>.Enumerator enumerator4;
            string str14;
            string str15;
            string str16;
            string str17;
            string str18;
            string str19;
            builder = new StringBuilder();
            str = string.Empty;
            if (this.enableCondsSex == null)
            {
                goto Label_003E;
            }
            str2 = SexToString(this.m_CondsSex);
            objArray1 = new object[] { str2 };
            str = str + LocalizedText.Get("sys.ABILITY_CONDS_SEX", objArray1);
        Label_003E:
            if (this.enableCondsElem == null)
            {
                goto Label_0070;
            }
            str3 = ElementToString(this.m_CondsElem);
            objArray2 = new object[] { str3 };
            str = str + LocalizedText.Get("sys.ABILITY_CONDS_ELEMENT", objArray2);
        Label_0070:
            if (this.enableCondsBirth == null)
            {
                goto Label_00A0;
            }
            objArray3 = new object[] { (bool) this.enableCondsBirth };
            str = str + LocalizedText.Get("sys.ABILITY_CONDS_BIRTH", objArray3);
        Label_00A0:
            if (this.enableCondsUnits == null)
            {
                goto Label_02E1;
            }
            if (this.enableCondsJobs == null)
            {
                goto Label_0209;
            }
            enumerator = this.m_CondsUnits.GetEnumerator();
        Label_00C3:
            try
            {
                goto Label_01E6;
            Label_00C8:
                param = &enumerator.Current;
                enumerator2 = this.m_CondsJobs.GetEnumerator();
            Label_00DE:
                try
                {
                    goto Label_01C8;
                Label_00E3:
                    param2 = &enumerator2.Current;
                    if (string.IsNullOrEmpty(str) != null)
                    {
                        goto Label_0164;
                    }
                    objArray4 = new object[] { param.name };
                    str4 = LocalizedText.Get("sys.ABILITY_CONDS_UNIT", objArray4);
                    objArray5 = new object[] { param2.name };
                    str5 = LocalizedText.Get("sys.ABILITY_CONDS_JOB", objArray5);
                    textArray1 = new string[] { str4, str5, str };
                    str6 = InternalMakeConditionsText(textArray1);
                    builder.Append(str6);
                    builder.Append("\n");
                    goto Label_01C8;
                Label_0164:
                    objArray6 = new object[] { param.name };
                    str7 = LocalizedText.Get("sys.ABILITY_CONDS_UNIT", objArray6);
                    objArray7 = new object[] { param2.name };
                    str8 = LocalizedText.Get("sys.ABILITY_CONDS_JOB", objArray7);
                    textArray2 = new string[] { str7, str8 };
                    str9 = InternalMakeConditionsText(textArray2);
                    builder.Append(str9);
                    builder.Append("\n");
                Label_01C8:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_00E3;
                    }
                    goto Label_01E6;
                }
                finally
                {
                Label_01D9:
                    ((List<JobParam>.Enumerator) enumerator2).Dispose();
                }
            Label_01E6:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00C8;
                }
                goto Label_0204;
            }
            finally
            {
            Label_01F7:
                ((List<UnitParam>.Enumerator) enumerator).Dispose();
            }
        Label_0204:
            goto Label_02DC;
        Label_0209:
            enumerator3 = this.m_CondsUnits.GetEnumerator();
        Label_0216:
            try
            {
                goto Label_02BE;
            Label_021B:
                param3 = &enumerator3.Current;
                if (string.IsNullOrEmpty(str) != null)
                {
                    goto Label_027B;
                }
                objArray8 = new object[] { param3.name };
                str10 = LocalizedText.Get("sys.ABILITY_CONDS_UNIT", objArray8);
                textArray3 = new string[] { str10, str };
                str11 = InternalMakeConditionsText(textArray3);
                builder.Append(str11);
                builder.Append("\n");
                goto Label_02BE;
            Label_027B:
                objArray9 = new object[] { param3.name };
                str12 = LocalizedText.Get("sys.ABILITY_CONDS_UNIT", objArray9);
                textArray4 = new string[] { str12 };
                str13 = InternalMakeConditionsText(textArray4);
                builder.Append(str13);
                builder.Append("\n");
            Label_02BE:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_021B;
                }
                goto Label_02DC;
            }
            finally
            {
            Label_02CF:
                ((List<UnitParam>.Enumerator) enumerator3).Dispose();
            }
        Label_02DC:
            goto Label_041B;
        Label_02E1:
            if (this.enableCondsJobs == null)
            {
                goto Label_03C4;
            }
            enumerator4 = this.m_CondsJobs.GetEnumerator();
        Label_02F9:
            try
            {
                goto Label_03A1;
            Label_02FE:
                param4 = &enumerator4.Current;
                if (string.IsNullOrEmpty(str) != null)
                {
                    goto Label_035E;
                }
                objArray10 = new object[] { param4.name };
                str14 = LocalizedText.Get("sys.ABILITY_CONDS_JOB", objArray10);
                textArray5 = new string[] { str14, str };
                str15 = InternalMakeConditionsText(textArray5);
                builder.Append(str15);
                builder.Append("\n");
                goto Label_03A1;
            Label_035E:
                objArray11 = new object[] { param4.name };
                str16 = LocalizedText.Get("sys.ABILITY_CONDS_JOB", objArray11);
                textArray6 = new string[] { str16 };
                str17 = InternalMakeConditionsText(textArray6);
                builder.Append(str17);
                builder.Append("\n");
            Label_03A1:
                if (&enumerator4.MoveNext() != null)
                {
                    goto Label_02FE;
                }
                goto Label_03BF;
            }
            finally
            {
            Label_03B2:
                ((List<JobParam>.Enumerator) enumerator4).Dispose();
            }
        Label_03BF:
            goto Label_041B;
        Label_03C4:
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_03FA;
            }
            textArray7 = new string[] { str };
            str18 = InternalMakeConditionsText(textArray7);
            builder.Append(str18);
            builder.Append("\n");
            goto Label_041B;
        Label_03FA:
            str19 = LocalizedText.Get("sys.ABILITY_CONDS_NO_CONDS");
            builder.Append(str19);
            builder.Append("\n");
        Label_041B:
            return builder.ToString();
        }

        public void Setup(AbilityParam abil, MasterParam master)
        {
            this.m_AbilityParam = abil;
            this.m_CondsUnits = this.m_AbilityParam.FindConditionUnitParams(master);
            this.m_CondsJobs = this.m_AbilityParam.FindConditionJobParams(master);
            this.m_CondsBirth = this.m_AbilityParam.condition_birth;
            this.m_CondsSex = this.m_AbilityParam.condition_sex;
            this.m_CondsElem = this.m_AbilityParam.condition_element;
            return;
        }

        private static string SexToString(ESex sex)
        {
            string str;
            return LocalizedText.Get(string.Format("sys.SEX_{0}", (int) sex));
        }

        private bool enableCondsUnits
        {
            get
            {
                return (this.m_CondsUnits.Count > 0);
            }
        }

        private bool enableCondsJobs
        {
            get
            {
                return (this.m_CondsJobs.Count > 0);
            }
        }

        private bool enableCondsBirth
        {
            get
            {
                return (string.IsNullOrEmpty(this.m_CondsBirth) == 0);
            }
        }

        private bool enableCondsSex
        {
            get
            {
                return ((this.m_CondsSex == 0) == 0);
            }
        }

        private bool enableCondsElem
        {
            get
            {
                return ((this.m_CondsElem == 0) == 0);
            }
        }
    }
}

