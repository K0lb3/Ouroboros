namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_CollaboSkillParam
    {
        public string iname;
        public string uname;
        public string abid;
        public string[] lqs;

        public JSON_CollaboSkillParam()
        {
            base..ctor();
            return;
        }
    }
}

