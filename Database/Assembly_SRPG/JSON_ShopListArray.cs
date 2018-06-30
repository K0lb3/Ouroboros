namespace SRPG
{
    using System;

    public class JSON_ShopListArray
    {
        public Shops[] shops;

        public JSON_ShopListArray()
        {
            base..ctor();
            return;
        }

        public class Shops
        {
            public int id;
            public string gname;
            public string gtype;
            public long yymmddhhmm;
            public string created_at;
            public string update_at;
            public JSON_ShopListInfo info;
            public long start;
            public long end;
            public JSON_UnlockInfo unlock;

            public Shops()
            {
                base..ctor();
                return;
            }

            public class JSON_ShopListInfo
            {
                public string rare;
                public string title;
                public string msg;
                private JSON_ShopListInfoCost cost;
                private int gold;

                public JSON_ShopListInfo()
                {
                    base..ctor();
                    return;
                }

                public class JSON_ShopListInfoCost
                {
                    private int gold;

                    public JSON_ShopListInfoCost()
                    {
                        base..ctor();
                        return;
                    }
                }
            }

            public class JSON_UnlockInfo
            {
                public int flg;
                public string message;

                public JSON_UnlockInfo()
                {
                    base..ctor();
                    return;
                }
            }
        }
    }
}

