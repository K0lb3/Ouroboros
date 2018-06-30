namespace SRPG
{
    using System;

    public class Json_PlayerData
    {
        public string name;
        public int exp;
        public int lv;
        public int gold;
        public int abilpt;
        public Json_Coin coin;
        public int arenacoin;
        public int multicoin;
        public int enseicoin;
        public int kakeracoin;
        public int cnt_multi;
        public int cnt_stmrecover;
        public int cnt_buygold;
        public string cuid;
        public string fuid;
        public int logincont;
        public int mail_unread;
        public int mail_f_unread;
        public long btlid;
        public string btltype;
        public Json_Hikkoshi tuid;
        public Json_Stamina stamina;
        public Json_Cave cave;
        public Json_AbilityUp abilup;
        public Json_Arena arena;
        public Json_Tour tour;
        public Json_Vip vip;
        public int unitbox_max;
        public int itembox_max;
        public Json_FreeGacha gachag;
        public Json_FreeGacha gachac;
        public Json_PaidGacha gachap;
        public Json_Friends friends;
        public int newgame_at;
        public string selected_award;
        public Json_MultiOption multi;
        public Json_GuerrillaShopPeriod g_shop;

        public Json_PlayerData()
        {
            base..ctor();
            return;
        }
    }
}

