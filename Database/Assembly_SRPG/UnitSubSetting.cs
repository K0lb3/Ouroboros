namespace SRPG
{
    using System;

    public class UnitSubSetting
    {
        public eMapUnitCtCalcType startCtCalc;
        public OInt startCtVal;

        public UnitSubSetting()
        {
            base..ctor();
            return;
        }

        public UnitSubSetting(JSON_MapPartySubCT json)
        {
            base..ctor();
            this.startCtCalc = json.ct_calc;
            this.startCtVal = json.ct_val;
            return;
        }
    }
}

