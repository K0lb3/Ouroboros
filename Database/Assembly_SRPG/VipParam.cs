namespace SRPG
{
    using System;

    public class VipParam
    {
        public int PlayerLevel;
        public int NextRankNeedPoint;
        public int Ticket;
        public int BuyCoinBonus;
        public int BuyCoinNum;
        public int BuyStaminaNum;
        public int ResetEliteNum;
        public int ResetArenaNum;

        public VipParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_VipParam json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.NextRankNeedPoint = json.exp;
            this.Ticket = json.ticket;
            this.BuyCoinBonus = json.buy_coin_bonus;
            this.BuyCoinNum = json.buy_coin_num;
            this.BuyStaminaNum = json.buy_stmn_num;
            this.ResetEliteNum = json.reset_elite;
            this.ResetArenaNum = json.reset_arena;
            return 1;
        }
    }
}

