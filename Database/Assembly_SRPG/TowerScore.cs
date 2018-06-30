namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class TowerScore : MonoBehaviour
    {
        [SerializeField, HeaderBar("▼「スコア内訳」のテキスト")]
        private Text m_ScoreTitle;
        [HeaderBar("▼スコア"), SerializeField]
        private Text m_TurnNum;
        [SerializeField]
        private Text m_DiedNum;
        [SerializeField]
        private Text m_RetireNum;
        [SerializeField]
        private Text m_RecoveryNum;
        [SerializeField]
        private Text m_FloorResetNum;
        [SerializeField]
        private Text m_ChallengeNum;
        [SerializeField]
        private Text m_LoseNum;
        [SerializeField, HeaderBar("▼ランキング")]
        private TowerRankItem m_SpdRankTop;
        [SerializeField]
        private TowerRankItem m_TecRankTop;
        [SerializeField]
        private GameObject m_SpdRankHeader;
        [SerializeField]
        private GameObject m_TecRankHeader;
        [SerializeField]
        private GameObject m_NoRankData;
        private string NotSocreText;

        public TowerScore()
        {
            this.NotSocreText = "-";
            base..ctor();
            return;
        }

        public void SetRankData(TowerRankItem rankItem, TowerResuponse.TowerRankParam[] rankData)
        {
            TowerResuponse.TowerRankParam param;
            if (rankData == null)
            {
                goto Label_000F;
            }
            if (((int) rankData.Length) >= 1)
            {
                goto Label_0017;
            }
        Label_000F:
            GameUtility.SetGameObjectActive(rankItem, 0);
            return;
        Label_0017:
            param = rankData[0];
            if ((rankItem != null) == null)
            {
                goto Label_004A;
            }
            DataSource.Bind<UnitData>(rankItem.get_gameObject(), param.unit);
            rankItem.Setup(param);
            GameParameter.UpdateAll(rankItem.get_gameObject());
        Label_004A:
            return;
        }

        public void SetScoreTitleText(string floorName)
        {
            object[] objArray1;
            objArray1 = new object[] { floorName };
            SetText(this.m_ScoreTitle, LocalizedText.Get("sys.TOWER_BRAGDOWN_SCORE_FLOOR", objArray1));
            return;
        }

        private static unsafe void SetText(Text text, int value)
        {
            if ((text != null) == null)
            {
                goto Label_0019;
            }
            text.set_text(&value.ToString());
        Label_0019:
            return;
        }

        private static void SetText(Text text, string value)
        {
            if ((text != null) == null)
            {
                goto Label_0013;
            }
            text.set_text(value);
        Label_0013:
            return;
        }

        public void Setup(ViewParam param, TowerResuponse.TowerRankParam[] spdRankData, TowerResuponse.TowerRankParam[] tecRankData)
        {
            bool flag;
            if (param.IsNoData == null)
            {
                goto Label_0087;
            }
            SetText(this.m_TurnNum, this.NotSocreText);
            SetText(this.m_DiedNum, this.NotSocreText);
            SetText(this.m_RetireNum, this.NotSocreText);
            SetText(this.m_RecoveryNum, this.NotSocreText);
            SetText(this.m_FloorResetNum, this.NotSocreText);
            SetText(this.m_ChallengeNum, this.NotSocreText);
            SetText(this.m_LoseNum, this.NotSocreText);
            goto Label_00FE;
        Label_0087:
            SetText(this.m_TurnNum, param.TurnNum);
            SetText(this.m_DiedNum, param.DiedNum);
            SetText(this.m_RetireNum, param.RetireNum);
            SetText(this.m_RecoveryNum, param.RecoveryNum);
            SetText(this.m_FloorResetNum, param.FloorResetNum);
            SetText(this.m_ChallengeNum, param.ChallengeNum);
            SetText(this.m_LoseNum, param.LoseNum);
        Label_00FE:
            flag = 0;
            flag |= (spdRankData == null) ? 1 : (((int) spdRankData.Length) < 1);
            if ((flag | ((tecRankData == null) ? 1 : (((int) tecRankData.Length) < 1))) == null)
            {
                goto Label_016B;
            }
            GameUtility.SetGameObjectActive(this.m_NoRankData, 1);
            GameUtility.SetGameObjectActive(this.m_SpdRankTop, 0);
            GameUtility.SetGameObjectActive(this.m_TecRankTop, 0);
            GameUtility.SetGameObjectActive(this.m_SpdRankHeader, 0);
            GameUtility.SetGameObjectActive(this.m_TecRankHeader, 0);
            goto Label_01C1;
        Label_016B:
            this.SetRankData(this.m_SpdRankTop, spdRankData);
            this.SetRankData(this.m_TecRankTop, tecRankData);
            GameUtility.SetGameObjectActive(this.m_NoRankData, 0);
            GameUtility.SetGameObjectActive(this.m_SpdRankTop, 1);
            GameUtility.SetGameObjectActive(this.m_TecRankTop, 1);
            GameUtility.SetGameObjectActive(this.m_SpdRankHeader, 1);
            GameUtility.SetGameObjectActive(this.m_TecRankHeader, 1);
        Label_01C1:
            return;
        }

        public class ViewParam
        {
            private int m_TurnNum;
            private int m_DiedNum;
            private int m_RetireNum;
            private int m_RecoveryNum;
            private int m_FloorResetNum;
            private int m_ChallengeNum;
            private int m_LoseNum;

            public ViewParam()
            {
                this.m_TurnNum = -1;
                this.m_DiedNum = -1;
                this.m_RetireNum = -1;
                this.m_RecoveryNum = -1;
                this.m_FloorResetNum = -1;
                this.m_ChallengeNum = -1;
                this.m_LoseNum = -1;
                base..ctor();
                return;
            }

            public int TurnNum
            {
                get
                {
                    return this.m_TurnNum;
                }
                set
                {
                    this.m_TurnNum = value;
                    return;
                }
            }

            public int DiedNum
            {
                get
                {
                    return this.m_DiedNum;
                }
                set
                {
                    this.m_DiedNum = value;
                    return;
                }
            }

            public int RetireNum
            {
                get
                {
                    return this.m_RetireNum;
                }
                set
                {
                    this.m_RetireNum = value;
                    return;
                }
            }

            public int RecoveryNum
            {
                get
                {
                    return this.m_RecoveryNum;
                }
                set
                {
                    this.m_RecoveryNum = value;
                    return;
                }
            }

            public int FloorResetNum
            {
                get
                {
                    return this.m_FloorResetNum;
                }
                set
                {
                    this.m_FloorResetNum = value;
                    return;
                }
            }

            public int ChallengeNum
            {
                get
                {
                    return this.m_ChallengeNum;
                }
                set
                {
                    this.m_ChallengeNum = value;
                    return;
                }
            }

            public int LoseNum
            {
                get
                {
                    return this.m_LoseNum;
                }
                set
                {
                    this.m_LoseNum = value;
                    return;
                }
            }

            public bool IsNoData
            {
                get
                {
                    return (((((this.m_TurnNum == -1) || (this.m_DiedNum == -1)) || ((this.m_RetireNum == -1) || (this.m_RecoveryNum == -1))) || ((this.m_FloorResetNum == -1) || (this.m_ChallengeNum == -1))) ? 1 : (this.m_LoseNum == -1));
                }
            }
        }
    }
}

