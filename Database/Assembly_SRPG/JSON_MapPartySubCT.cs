namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_MapPartySubCT
    {
        public int ct_calc;
        public int ct_val;

        public JSON_MapPartySubCT()
        {
            base..ctor();
            return;
        }

        public void CopyTo(JSON_MapPartySubCT dst)
        {
            dst.ct_calc = this.ct_calc;
            dst.ct_val = this.ct_val;
            return;
        }
    }
}

