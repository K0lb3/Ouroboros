namespace SRPG
{
    using System;

    public class RarityEquipEnhanceParam
    {
        public OInt rankcap;
        public OInt cost_scale;
        public RankParam[] ranks;

        public RarityEquipEnhanceParam()
        {
            base..ctor();
            return;
        }

        public RankParam GetRankParam(int rank)
        {
            return (((rank <= 0) || (rank > ((int) this.ranks.Length))) ? null : this.ranks[rank - 1]);
        }

        public class RankParam
        {
            public OInt need_point;
            public ReturnItem[] return_item;

            public RankParam()
            {
                this.return_item = new ReturnItem[3];
                base..ctor();
                return;
            }
        }
    }
}

