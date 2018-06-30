namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VersusTowerParam
    {
        public string vstower_id;
        public string iname;
        public int floor;
        public int rankup_num;
        public int win_num;
        public int lose_num;
        public int bonus_num;
        public int downfloor;
        public int resetfloor;
        public string[] winitem;
        public int[] win_itemnum;
        public string[] joinitem;
        public int[] join_itemnum;
        public string arrival_item;
        public string arrival_type;
        public int arrival_num;
        public string[] spbtl_item;
        public int[] spbtl_itemnum;
        public string[] season_item;
        public string[] season_itype;
        public int[] season_itemnum;

        public JSON_VersusTowerParam()
        {
            base..ctor();
            return;
        }
    }
}

