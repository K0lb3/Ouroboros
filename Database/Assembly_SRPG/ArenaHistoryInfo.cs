namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ArenaHistoryInfo : MonoBehaviour
    {
        [Space(10f)]
        public Text Ranking;
        public Text created_at;
        public Text PlayerName;
        public Text PlayerLevel;
        public GameObject unit_icon;
        public ImageArray result_image;
        public ImageArray ranking_delta;
        public ImageArray history_type;
        public Image NewImage;

        public ArenaHistoryInfo()
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
            ArenaPlayerHistory history;
            history = DataSource.FindDataOfClass<ArenaPlayerHistory>(base.get_gameObject(), null);
            if (history != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            this.PlayerLevel.set_text(&history.enemy.PlayerLevel.ToString());
            if (history.IsWin() == null)
            {
                goto Label_004B;
            }
            this.result_image.ImageIndex = 0;
            goto Label_0057;
        Label_004B:
            this.result_image.ImageIndex = 1;
        Label_0057:
            this.NewImage.get_gameObject().SetActive(history.IsNew());
            if (history.IsNew() == null)
            {
                goto Label_009C;
            }
            this.created_at.set_color(new Color(255f, 255f, 0f, 1f));
        Label_009C:
            if (history.IsAttack() == null)
            {
                goto Label_00B8;
            }
            this.history_type.ImageIndex = 0;
            goto Label_00C4;
        Label_00B8:
            this.history_type.ImageIndex = 1;
        Label_00C4:
            this.Ranking.set_text(&history.ranking.up.ToString());
            this.Ranking.get_gameObject().SetActive((history.ranking.up == 0) == 0);
            if (history.ranking.up <= 0)
            {
                goto Label_0122;
            }
            this.ranking_delta.ImageIndex = 0;
            goto Label_0179;
        Label_0122:
            if (history.ranking.up >= 0)
            {
                goto Label_0168;
            }
            this.ranking_delta.ImageIndex = 1;
            this.Ranking.set_color(new Color(255f, 0f, 0f, 1f));
            goto Label_0179;
        Label_0168:
            this.ranking_delta.get_gameObject().SetActive(0);
        Label_0179:
            this.PlayerName.set_text(history.enemy.PlayerName.ToString());
            this.created_at.set_text(&history.battle_at.ToString("MM/dd HH:mm"));
            return;
        }
    }
}

