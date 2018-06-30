namespace SRPG
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class MultiPlayResumeBuff
    {
        public string iname;
        public int turn;
        public int unitindex;
        public int checkunit;
        public int timing;
        public bool passive;
        public int condition;
        public int type;
        public int vtp;
        public int calc;
        public int curse;
        public int skilltarget;
        public string bc_id;
        public uint lid;
        public int ubc;
        public List<int> atl;

        public MultiPlayResumeBuff()
        {
            this.atl = new List<int>();
            base..ctor();
            return;
        }
    }
}

