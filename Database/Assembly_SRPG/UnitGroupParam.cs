namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class UnitGroupParam
    {
        public string iname;
        public string name;
        public string[] units;

        public UnitGroupParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_UnitGroupParam json)
        {
            this.iname = json.iname;
            this.name = json.name;
            this.units = json.units;
            return 1;
        }

        public string GetGroupUnitAllNameText()
        {
            StringBuilder builder;
            int num;
            UnitParam param;
            builder = new StringBuilder();
            if (this.units != null)
            {
                goto Label_0017;
            }
            return string.Empty;
        Label_0017:
            num = 0;
            goto Label_006E;
        Label_001E:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.units[num]);
            if (param != null)
            {
                goto Label_0041;
            }
            goto Label_006A;
        Label_0041:
            builder.Append(param.name);
            if (num >= (((int) this.units.Length) - 1))
            {
                goto Label_006A;
            }
            builder.Append("CONCEPT_CARD_SKILL_DESCRIPTION_COMMA");
        Label_006A:
            num += 1;
        Label_006E:
            if (num < ((int) this.units.Length))
            {
                goto Label_001E;
            }
            return builder.ToString();
        }

        public string GetName()
        {
            if (string.IsNullOrEmpty(this.name) == null)
            {
                goto Label_0017;
            }
            return this.GetGroupUnitAllNameText();
        Label_0017:
            return this.name;
        }

        public bool IsInGroup(string unit_iname)
        {
            <IsInGroup>c__AnonStorey2F0 storeyf;
            storeyf = new <IsInGroup>c__AnonStorey2F0();
            storeyf.unit_iname = unit_iname;
            return ((Array.FindIndex<string>(this.units, new Predicate<string>(storeyf.<>m__278)) < 0) == 0);
        }

        [CompilerGenerated]
        private sealed class <IsInGroup>c__AnonStorey2F0
        {
            internal string unit_iname;

            public <IsInGroup>c__AnonStorey2F0()
            {
                base..ctor();
                return;
            }

            internal bool <>m__278(string u)
            {
                return (u == this.unit_iname);
            }
        }
    }
}

