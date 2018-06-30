namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class RankingQuestResult : MonoBehaviour
    {
        private const int HIGHER_RANK_TEXT_INDEX = 4;
        private const int MIDDLE_RANK_TEXT_INDEX = 10;
        [SerializeField]
        private Text m_HigherRankText;
        [SerializeField]
        private Text m_MiddleRankText;
        [SerializeField]
        private Text m_LowerRankText;
        [SerializeField]
        private GameObject m_RankDecoration;
        [SerializeField]
        private GameObject m_RankDecorationEffect;
        [SerializeField]
        private Text m_MainScoreText;
        [SerializeField]
        private Text m_MainScoreValue;
        [SerializeField]
        private Text m_SubScoreValue;

        public RankingQuestResult()
        {
            base..ctor();
            return;
        }

        private unsafe void Start()
        {
            RankingQuestParam param;
            Text text;
            int num;
            int num2;
            int num3;
            param = null;
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0046;
            }
            param = SceneBattle.Instance.Battle.GetRankingQuestParam();
            DataSource.Bind<UnitData>(base.get_gameObject(), SceneBattle.Instance.Battle.Leader.UnitData);
            goto Label_004C;
        Label_0046:
            param = GlobalVars.SelectedRankingQuestParam;
        Label_004C:
            if (param == null)
            {
                goto Label_01C5;
            }
            this.m_HigherRankText.get_gameObject().SetActive(0);
            this.m_MiddleRankText.get_gameObject().SetActive(0);
            this.m_LowerRankText.get_gameObject().SetActive(0);
            this.m_RankDecoration.SetActive(0);
            this.m_RankDecorationEffect.SetActive(0);
            text = this.m_LowerRankText;
            if (SceneBattle.Instance.RankingQuestNewRank > 4)
            {
                goto Label_00D8;
            }
            text = this.m_HigherRankText;
            this.m_RankDecoration.SetActive(1);
            this.m_RankDecorationEffect.SetActive(1);
            goto Label_0108;
        Label_00D8:
            if (SceneBattle.Instance.RankingQuestNewRank > 10)
            {
                goto Label_0108;
            }
            text = this.m_MiddleRankText;
            this.m_RankDecoration.SetActive(1);
            this.m_RankDecorationEffect.SetActive(1);
        Label_0108:
            text.get_gameObject().SetActive(1);
            text.set_text(&SceneBattle.Instance.RankingQuestNewRank.ToString());
            if (param.type != 1)
            {
                goto Label_0191;
            }
            if ((this.m_MainScoreText != null) == null)
            {
                goto Label_015E;
            }
            this.m_MainScoreText.set_text(LocalizedText.Get("sys.RANKING_QUEST_WND_TYPE_ACTION"));
        Label_015E:
            if ((this.m_MainScoreValue != null) == null)
            {
                goto Label_0191;
            }
            this.m_MainScoreValue.set_text(&SceneBattle.Instance.Battle.ActionCount.ToString());
        Label_0191:
            if ((this.m_SubScoreValue != null) == null)
            {
                goto Label_01C5;
            }
            this.m_SubScoreValue.set_text(&SceneBattle.Instance.Battle.CalcPlayerUnitsTotalParameter().ToString());
        Label_01C5:
            return;
        }
    }
}

