namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ArenaRankUpWindow : MonoBehaviour
    {
        public Text OldRank;
        public Text NewRank;
        public Text DeltaRank;

        public ArenaRankUpWindow()
        {
            base..ctor();
            return;
        }

        private unsafe void Start()
        {
            GameManager manager;
            ArenaBattleResponse response;
            int num;
            RewardData data;
            RewardWindow window;
            int num2;
            if ((SceneBattle.Instance == null) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            manager = MonoSingleton<GameManager>.Instance;
            response = GlobalVars.ResultArenaBattleResponse;
            num = manager.Player.ArenaRankBest - response.new_rank;
            data = new RewardData();
            data.ArenaMedal = response.reward_info.arenacoin;
            data.Coin = response.reward_info.coin;
            DataSource.Bind<RewardData>(base.get_gameObject(), data);
            this.OldRank.set_text(&manager.Player.ArenaRankBest.ToString());
            this.NewRank.set_text(&response.new_rank.ToString());
            this.DeltaRank.set_text(&num.ToString());
            this.DeltaRank.set_enabled(num > 0);
            window = base.GetComponentInChildren<RewardWindow>();
            if ((window != null) == null)
            {
                goto Label_00D6;
            }
            window.Refresh();
        Label_00D6:
            return;
        }
    }
}

