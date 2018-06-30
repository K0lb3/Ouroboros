namespace SRPG
{
    using System;

    public abstract class ConditionsResult_Unit : ConditionsResult
    {
        private UnitData mUnitData;
        private UnitParam mUnitParam;

        public ConditionsResult_Unit(UnitData unitData, UnitParam unitParam)
        {
            base..ctor();
            this.mUnitData = unitData;
            this.mUnitParam = unitParam;
            return;
        }

        public UnitData unitData
        {
            get
            {
                return this.mUnitData;
            }
        }

        public bool hasUnitData
        {
            get
            {
                return ((this.mUnitData == null) == 0);
            }
        }

        public string unitName
        {
            get
            {
                if (this.mUnitData == null)
                {
                    goto Label_001C;
                }
                return this.mUnitData.UnitParam.name;
            Label_001C:
                return ((this.mUnitParam == null) ? string.Empty : this.mUnitParam.name);
            }
        }
    }
}

