namespace SRPG
{
    using System;

    public class WeaponParam
    {
        public string iname;
        public OInt atk;
        public OInt formula;

        public WeaponParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_WeaponParam json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.atk = json.atk;
            this.formula = json.formula;
            return 1;
        }

        public WeaponFormulaTypes FormulaType
        {
            get
            {
                return this.formula;
            }
        }
    }
}

