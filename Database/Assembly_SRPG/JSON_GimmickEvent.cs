namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_GimmickEvent
    {
        public int ev_type;
        public string skill;
        public string su_iname;
        public string su_tag;
        public string st_iname;
        public string st_tag;
        public int type;
        public string cu_iname;
        public string cu_tag;
        public string ct_iname;
        public string ct_tag;
        public int count;
        public int[] x;
        public int[] y;

        public JSON_GimmickEvent()
        {
            this.skill = string.Empty;
            this.su_iname = string.Empty;
            this.su_tag = string.Empty;
            this.st_iname = string.Empty;
            this.st_tag = string.Empty;
            this.cu_iname = string.Empty;
            this.cu_tag = string.Empty;
            this.ct_iname = string.Empty;
            this.ct_tag = string.Empty;
            this.x = new int[1];
            this.y = new int[1];
            base..ctor();
            return;
        }
    }
}

