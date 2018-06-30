namespace SRPG
{
    using System;

    public class FirstChargeReward
    {
        private string m_Iname;
        private long m_Type;
        private int m_Num;

        public FirstChargeReward()
        {
            this.m_Iname = string.Empty;
            base..ctor();
            this.m_Iname = string.Empty;
            this.m_Type = 0L;
            this.m_Num = 0;
            return;
        }

        public FirstChargeReward(FlowNode_ReqFirstChargeBonus.Reward _reward)
        {
            this.m_Iname = string.Empty;
            base..ctor();
            this.m_Iname = _reward.iname;
            this.SetGiftTypes(_reward.GetGiftType());
            this.m_Num = _reward.num;
            return;
        }

        public FirstChargeReward(string _iname, GiftTypes _type, int _num)
        {
            this.m_Iname = string.Empty;
            base..ctor();
            this.m_Iname = _iname;
            this.SetGiftTypes(_type);
            this.m_Num = _num;
            return;
        }

        public bool CheckGiftTypes(GiftTypes flag)
        {
            return (((this.m_Type & flag) == 0L) == 0);
        }

        public void SetGiftTypes(GiftTypes flag)
        {
            this.m_Type |= flag;
            return;
        }

        public string iname
        {
            get
            {
                return this.m_Iname;
            }
        }

        public long type
        {
            get
            {
                return this.m_Type;
            }
        }

        public int num
        {
            get
            {
                return this.m_Num;
            }
        }
    }
}

