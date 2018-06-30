namespace SRPG
{
    using GR;
    using System;

    public class CurrencyTracker
    {
        public int Gold;
        public int Coin;
        public int ArenaCoin;
        public int MultiCoin;

        public CurrencyTracker()
        {
            PlayerData data;
            base..ctor();
            data = MonoSingleton<GameManager>.Instance.Player;
            this.Gold = data.Gold;
            this.Coin = data.Coin;
            this.ArenaCoin = data.ArenaCoin;
            this.MultiCoin = data.MultiCoin;
            return;
        }

        public void EndTracking()
        {
            PlayerData data;
            data = MonoSingleton<GameManager>.Instance.Player;
            this.Gold = data.Gold - this.Gold;
            this.Coin = data.Coin - this.Coin;
            this.ArenaCoin = data.ArenaCoin - this.ArenaCoin;
            this.MultiCoin = data.MultiCoin - this.MultiCoin;
            return;
        }
    }
}

