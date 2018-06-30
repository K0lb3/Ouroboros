namespace SRPG
{
    using System;

    public class Json_GachaParam
    {
        public string iname;
        public string ticket;
        public string cat;
        public long startat;
        public long endat;
        public Json_GachaCost cost;
        public int num;
        public string msg;
        public string name;
        public string movie;
        public string bg;
        public string asset_bg;
        public string asset_title;
        public string detail_url;
        public string[] units;
        public string[] ext_type;
        public Json_GachaExtParam ext_param;
        public string group;
        public string btext;
        public string confirm;
        public Json_GachaBonus[] bonus_items;
        public string[] pickup_art;
        public int disabled;
        public string bonus_msg;
        public Json_GachaAppealParam appeal;
        public int isHide;
        public int isLoop;
        public int isFreePause;

        public Json_GachaParam()
        {
            base..ctor();
            return;
        }
    }
}

