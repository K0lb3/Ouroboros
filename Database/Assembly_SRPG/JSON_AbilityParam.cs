namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_AbilityParam
    {
        public string iname;
        public string name;
        public string expr;
        public string icon;
        public int type;
        public int slot;
        public int fix;
        public int cap;
        public string skl1;
        public string skl2;
        public string skl3;
        public string skl4;
        public string skl5;
        public string skl6;
        public string skl7;
        public string skl8;
        public string skl9;
        public string skl10;
        public int lv1;
        public int lv2;
        public int lv3;
        public int lv4;
        public int lv5;
        public int lv6;
        public int lv7;
        public int lv8;
        public int lv9;
        public int lv10;
        public string[] units;
        public int units_cnds_type;
        public string[] jobs;
        public int jobs_cnds_type;
        public string birth;
        public int sex;
        public int elem;
        public int rmin;
        public int rmax;
        public int type_detail;

        public JSON_AbilityParam()
        {
            base..ctor();
            return;
        }
    }
}

