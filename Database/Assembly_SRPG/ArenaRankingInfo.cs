namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ArenaRankingInfo : MonoBehaviour
    {
        [Space(10f)]
        public Text Ranking;
        public Text PlayerName;
        public Text PlayerKOs;
        public ImageArray ranking_image;

        public ArenaRankingInfo()
        {
            base..ctor();
            return;
        }

        private void OnEnable()
        {
            this.UpdateValue();
            return;
        }

        public unsafe void UpdateValue()
        {
            ArenaPlayer player;
            player = DataSource.FindDataOfClass<ArenaPlayer>(base.get_gameObject(), null);
            if (player != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            if (player.ArenaRank > 3)
            {
                goto Label_0047;
            }
            this.ranking_image.ImageIndex = player.ArenaRank;
            this.Ranking.get_gameObject().SetActive(0);
            goto Label_009A;
        Label_0047:
            if ((this.ranking_image != null) == null)
            {
                goto Label_0064;
            }
            this.ranking_image.ImageIndex = 0;
        Label_0064:
            this.Ranking.get_gameObject().SetActive(1);
            this.Ranking.set_text(&player.ArenaRank.ToString() + LocalizedText.Get("sys.ARENA_LBL_RANK"));
        Label_009A:
            if (string.IsNullOrEmpty(player.PlayerName) != null)
            {
                goto Label_00C0;
            }
            this.PlayerName.set_text(player.PlayerName.ToString());
        Label_00C0:
            if ((player.battle_at > DateTime.MinValue) == null)
            {
                goto Label_00F0;
            }
            this.PlayerKOs.set_text(&player.battle_at.ToString("MM/dd HH:mm"));
        Label_00F0:
            return;
        }
    }
}

