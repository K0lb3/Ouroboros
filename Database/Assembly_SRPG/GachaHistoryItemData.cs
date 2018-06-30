namespace SRPG
{
    using System;

    public class GachaHistoryItemData
    {
        private GachaHistoryData[] mHistorys;
        private string mGachaTitle;
        private long mDropAt;

        public GachaHistoryItemData(GachaHistoryData[] historys, string title, long drop_at)
        {
            base..ctor();
            this.mHistorys = historys;
            this.mGachaTitle = title;
            this.mDropAt = drop_at;
            return;
        }

        public DateTime GetDropAt()
        {
            return GameUtility.UnixtimeToLocalTime(this.mDropAt);
        }

        public GachaHistoryData[] historys
        {
            get
            {
                return this.mHistorys;
            }
        }

        public string gachaTitle
        {
            get
            {
                return this.mGachaTitle;
            }
        }

        public long drop_at
        {
            get
            {
                return this.mDropAt;
            }
        }
    }
}

