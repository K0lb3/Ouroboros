namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(100, "データ反映", 0, 100)]
    public class RankingQuestUserParty : MonoBehaviour, IFlowInterface
    {
        public const int INPUT_REFRECTION_DATA = 100;
        [SerializeField]
        private QuestClearedPartyViewer m_PartyWindow;
        [SerializeField]
        private Text m_MainScoreText;
        [SerializeField]
        private Text m_MainScoreValue;
        [SerializeField]
        private Text m_MainScoreValueSuffix;
        [SerializeField]
        private Text m_SubScoreText;
        [SerializeField]
        private Text m_SubScoreValue;

        public RankingQuestUserParty()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_001A;
            }
            this.m_PartyWindow.set_enabled(1);
            this.Refresh();
        Label_001A:
            return;
        }

        private unsafe void Refresh()
        {
            RankingQuestParam param;
            RankingQuestUserData data;
            param = GlobalVars.SelectedRankingQuestParam;
            data = DataSource.FindDataOfClass<RankingQuestUserData>(base.get_gameObject(), null);
            if ((this.m_MainScoreText != null) == null)
            {
                goto Label_004B;
            }
            if (param == null)
            {
                goto Label_004B;
            }
            if (param.type != 1)
            {
                goto Label_004B;
            }
            this.m_MainScoreText.set_text(LocalizedText.Get("sys.RANKING_QUEST_WND_TYPE_ACTION"));
        Label_004B:
            if ((this.m_MainScoreValueSuffix != null) == null)
            {
                goto Label_00AA;
            }
            if (param == null)
            {
                goto Label_00AA;
            }
            if (param.type != 1)
            {
                goto Label_0099;
            }
            this.m_MainScoreValueSuffix.get_gameObject().SetActive(1);
            this.m_MainScoreValueSuffix.set_text(LocalizedText.Get("sys.RANKING_QUEST_PARTY_ACTION_SUFFIX"));
            goto Label_00AA;
        Label_0099:
            this.m_MainScoreValueSuffix.get_gameObject().SetActive(0);
        Label_00AA:
            if ((this.m_SubScoreText != null) == null)
            {
                goto Label_00D0;
            }
            this.m_SubScoreText.set_text(LocalizedText.Get("sys.RANKING_QUEST_WND_UNIT_TOTAL"));
        Label_00D0:
            if ((this.m_MainScoreValue != null) == null)
            {
                goto Label_00F7;
            }
            this.m_MainScoreValue.set_text(&data.m_MainScore.ToString());
        Label_00F7:
            if ((this.m_SubScoreValue != null) == null)
            {
                goto Label_011E;
            }
            this.m_SubScoreValue.set_text(&data.m_SubScore.ToString());
        Label_011E:
            return;
        }
    }
}

