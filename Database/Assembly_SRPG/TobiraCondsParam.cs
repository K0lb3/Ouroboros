namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class TobiraCondsParam
    {
        private string mUnitIname;
        private TobiraParam.Category mCategory;
        private List<TobiraConditionParam> mConditions;

        public TobiraCondsParam()
        {
            this.mConditions = new List<TobiraConditionParam>();
            base..ctor();
            return;
        }

        public void Deserialize(JSON_TobiraCondsParam json)
        {
            int num;
            TobiraConditionParam param;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mUnitIname = json.unit_iname;
            this.mCategory = json.category;
            this.mConditions.Clear();
            if (json.conds == null)
            {
                goto Label_006E;
            }
            num = 0;
            goto Label_0060;
        Label_003C:
            param = new TobiraConditionParam();
            param.Deserialize(json.conds[num]);
            this.mConditions.Add(param);
            num += 1;
        Label_0060:
            if (num < ((int) json.conds.Length))
            {
                goto Label_003C;
            }
        Label_006E:
            return;
        }

        public string UnitIname
        {
            get
            {
                return this.mUnitIname;
            }
        }

        public TobiraParam.Category TobiraCategory
        {
            get
            {
                return this.mCategory;
            }
        }

        public TobiraConditionParam[] Conditions
        {
            get
            {
                return this.mConditions.ToArray();
            }
        }
    }
}

