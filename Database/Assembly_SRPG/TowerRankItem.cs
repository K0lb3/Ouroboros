namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class TowerRankItem : MonoBehaviour
    {
        [SerializeField]
        private Text m_UserName;
        [SerializeField]
        private Text m_UserLv;
        [SerializeField]
        private Text m_Rank;
        [SerializeField]
        private Text m_Score;

        public TowerRankItem()
        {
            base..ctor();
            return;
        }

        private unsafe void SetText(Text text, int value)
        {
            if ((text != null) == null)
            {
                goto Label_0019;
            }
            text.set_text(&value.ToString());
        Label_0019:
            return;
        }

        private void SetText(Text text, string value)
        {
            if ((text != null) == null)
            {
                goto Label_0013;
            }
            text.set_text(value);
        Label_0013:
            return;
        }

        public void Setup(TowerResuponse.TowerRankParam rankData)
        {
            if (rankData != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.SetText(this.m_UserName, rankData.name);
            this.SetText(this.m_UserLv, rankData.lv);
            this.SetText(this.m_Rank, rankData.rank);
            this.SetText(this.m_Score, rankData.score);
            return;
        }
    }
}

