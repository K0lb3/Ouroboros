namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class RankingQuestInfo : MonoBehaviour
    {
        [SerializeField]
        private Text m_UserName;
        [SerializeField]
        private Text m_MainScore;
        [SerializeField]
        private Text m_SubScore;
        [SerializeField]
        private RankViewObject[] m_SpecialRankObject;
        [SerializeField]
        private RankViewObject m_CommonRankObject;

        public RankingQuestInfo()
        {
            base..ctor();
            return;
        }

        private unsafe void Refrection_ActionCountRanking(RankingQuestUserData data)
        {
            if ((this.m_MainScore != null) == null)
            {
                goto Label_0027;
            }
            this.m_MainScore.set_text(&data.m_MainScore.ToString());
        Label_0027:
            if ((this.m_SubScore != null) == null)
            {
                goto Label_004E;
            }
            this.m_SubScore.set_text(&data.m_SubScore.ToString());
        Label_004E:
            return;
        }

        public unsafe void UpdateValue()
        {
            object[] objArray1;
            RankingQuestUserData data;
            int num;
            int num2;
            data = DataSource.FindDataOfClass<RankingQuestUserData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            num = 0;
            if ((this.m_UserName != null) == null)
            {
                goto Label_0038;
            }
            this.m_UserName.set_text(data.m_PlayerName);
        Label_0038:
            if (this.m_SpecialRankObject == null)
            {
                goto Label_00AC;
            }
            num = (int) this.m_SpecialRankObject.Length;
            num2 = 0;
            goto Label_009E;
        Label_0053:
            if ((data.m_Rank - 1) != num2)
            {
                goto Label_008C;
            }
            this.m_SpecialRankObject[num2].SetActive(1);
            this.m_SpecialRankObject[num2].SetRank(&data.m_Rank.ToString());
            goto Label_009A;
        Label_008C:
            this.m_SpecialRankObject[num2].SetActive(0);
        Label_009A:
            num2 += 1;
        Label_009E:
            if (num2 < ((int) this.m_SpecialRankObject.Length))
            {
                goto Label_0053;
            }
        Label_00AC:
            if (this.m_CommonRankObject == null)
            {
                goto Label_0109;
            }
            if (num >= data.m_Rank)
            {
                goto Label_00FD;
            }
            this.m_CommonRankObject.SetActive(1);
            objArray1 = new object[] { (int) data.m_Rank };
            this.m_CommonRankObject.SetRank(LocalizedText.Get("sys.RANKING_QUEST_WND_RANK", objArray1));
            goto Label_0109;
        Label_00FD:
            this.m_CommonRankObject.SetActive(0);
        Label_0109:
            if (data.IsActionCountRanking == null)
            {
                goto Label_011B;
            }
            this.Refrection_ActionCountRanking(data);
        Label_011B:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        [Serializable]
        public class RankViewObject
        {
            [SerializeField]
            public GameObject m_RootObject;
            [SerializeField]
            public Text m_TextObject;

            public RankViewObject()
            {
                base..ctor();
                return;
            }

            public void SetActive(bool value)
            {
                if ((this.m_RootObject != null) == null)
                {
                    goto Label_001D;
                }
                this.m_RootObject.SetActive(value);
            Label_001D:
                return;
            }

            public void SetRank(string value)
            {
                if ((this.m_TextObject != null) == null)
                {
                    goto Label_001D;
                }
                this.m_TextObject.set_text(value);
            Label_001D:
                return;
            }
        }
    }
}

