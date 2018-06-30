namespace SRPG
{
    using System;

    public class ShopParam
    {
        public string iname;
        public ESaleType UpdateCostType;
        public int[] UpdateCosts;

        public ShopParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_ShopParam json)
        {
            int num;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.UpdateCostType = json.upd_type;
            this.UpdateCosts = null;
            if (json.upd_costs == null)
            {
                goto Label_007C;
            }
            if (((int) json.upd_costs.Length) <= 0)
            {
                goto Label_007C;
            }
            this.UpdateCosts = new int[(int) json.upd_costs.Length];
            num = 0;
            goto Label_006E;
        Label_005A:
            this.UpdateCosts[num] = json.upd_costs[num];
            num += 1;
        Label_006E:
            if (num < ((int) json.upd_costs.Length))
            {
                goto Label_005A;
            }
        Label_007C:
            return 1;
        }
    }
}

