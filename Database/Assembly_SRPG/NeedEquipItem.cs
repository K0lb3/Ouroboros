namespace SRPG
{
    using System;

    public class NeedEquipItem
    {
        private ItemParam param;
        private int need_picec_num;

        public NeedEquipItem(ItemParam item_param, int need_picec)
        {
            base..ctor();
            this.param = item_param;
            this.need_picec_num = need_picec;
            return;
        }

        public string Iname
        {
            get
            {
                return this.param.iname;
            }
        }

        public int CommonType
        {
            get
            {
                return this.param.cmn_type;
            }
        }

        public int NeedPiece
        {
            get
            {
                return this.need_picec_num;
            }
        }

        public ItemParam Param
        {
            get
            {
                return this.param;
            }
        }
    }
}

