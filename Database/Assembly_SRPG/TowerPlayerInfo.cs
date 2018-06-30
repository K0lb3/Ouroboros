namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class TowerPlayerInfo : MonoBehaviour
    {
        [HeaderBar("▼現在のスコア")]
        public Text NowTurn;
        public Text NowDied;
        public Text NowRetire;
        public Text NowRecover;
        public Text NowChallenge;
        public Text NowLose;
        public Text NowReset;
        [HeaderBar("▼ベストスコア")]
        public Text BestTurn;
        public Text BestDied;
        public Text BestRetire;
        public Text BestRecover;
        public Text BestChallenge;
        public Text BestLose;
        public Text BestReset;
        [HeaderBar("▼ランキングの順位")]
        public Text SpdRank;
        public Text TecRank;
        [HeaderBar("▼スコアのアートフォント")]
        public GameObject TurnScore;
        public GameObject DiedScore;
        public GameObject RetireScore;
        public GameObject RecoverScore;
        public GameObject ChallengeScore;
        public GameObject LoseScore;
        public GameObject ResetScore;
        public GameObject TotalScore;
        public GameObject PlusObj;
        public GameObject MinusObj;
        [HeaderBar("▼ユニット表示用オブジェクトの親")]
        public GameObject Leader;
        private string NotSocreText;
        private SpriteSheet Icons;

        public TowerPlayerInfo()
        {
            this.NotSocreText = "-";
            base..ctor();
            return;
        }

        private void RefreshData()
        {
            GameManager manager;
            PlayerData data;
            PartyData data2;
            long num;
            UnitData data3;
            this.UpdateNowScore();
            this.UpdateBestScore();
            this.UpdateTotalScore();
            if ((this.Leader != null) == null)
            {
                goto Label_0066;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            data2 = data.Partys[6];
            num = data2.GetUnitUniqueID(0);
            data3 = data.FindUnitDataByUniqueID(num);
            DataSource.Bind<UnitData>(this.Leader, data3);
            GameParameter.UpdateAll(this.Leader);
        Label_0066:
            DataSource.Bind<PlayerData>(base.get_gameObject(), MonoSingleton<GameManager>.Instance.Player);
            return;
        }

        private void SetDefault(GameObject obj)
        {
            if ((obj != null) == null)
            {
                goto Label_0018;
            }
            obj.get_gameObject().SetActive(0);
        Label_0018:
            return;
        }

        private void SetRankIcon(GameObject obj, string rank)
        {
            Image image;
            if ((obj != null) == null)
            {
                goto Label_005B;
            }
            image = obj.GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_004F;
            }
            if ((this.Icons != null) == null)
            {
                goto Label_004F;
            }
            image.set_sprite(this.Icons.GetSprite(rank));
            obj.get_gameObject().SetActive(1);
            return;
        Label_004F:
            obj.get_gameObject().SetActive(0);
        Label_005B:
            return;
        }

        private void SetText(Text text, string val)
        {
            if ((text != null) == null)
            {
                goto Label_0013;
            }
            text.set_text(val);
        Label_0013:
            return;
        }

        private void Start()
        {
            this.Icons = AssetManager.Load<SpriteSheet>("UI/TowerRankIcon");
            this.RefreshData();
            return;
        }

        private unsafe void UpdateBestScore()
        {
            GameManager manager;
            TowerResuponse resuponse;
            TowerParam param;
            manager = MonoSingleton<GameManager>.Instance;
            resuponse = manager.TowerResuponse;
            param = manager.FindTower(resuponse.TowerID);
            if (resuponse.spd_score == null)
            {
                goto Label_0157;
            }
            this.SetText(this.BestTurn, &resuponse.spd_score.ToString());
            this.SetText(this.BestDied, &resuponse.tec_score.ToString());
            this.SetText(this.BestRetire, &resuponse.ret_score.ToString());
            this.SetText(this.BestRecover, &resuponse.rcv_score.ToString());
            this.SetText(this.BestChallenge, &resuponse.challenge_score.ToString());
            this.SetText(this.BestLose, &resuponse.lose_score.ToString());
            this.SetText(this.BestReset, &resuponse.reset_score.ToString());
            this.SetRankIcon(this.TurnScore, manager.ConvertTowerScoreToRank(param, resuponse.spd_score, 0));
            this.SetRankIcon(this.DiedScore, manager.ConvertTowerScoreToRank(param, resuponse.tec_score, 1));
            this.SetRankIcon(this.RetireScore, manager.ConvertTowerScoreToRank(param, resuponse.ret_score, 2));
            this.SetRankIcon(this.RecoverScore, manager.ConvertTowerScoreToRank(param, resuponse.rcv_score, 3));
            this.SetDefault(this.ChallengeScore);
            this.SetDefault(this.LoseScore);
            this.SetDefault(this.ResetScore);
            goto Label_0229;
        Label_0157:
            this.SetText(this.BestTurn, this.NotSocreText);
            this.SetText(this.BestDied, this.NotSocreText);
            this.SetText(this.BestRetire, this.NotSocreText);
            this.SetText(this.BestRecover, this.NotSocreText);
            this.SetText(this.BestChallenge, this.NotSocreText);
            this.SetText(this.BestLose, this.NotSocreText);
            this.SetText(this.BestReset, this.NotSocreText);
            this.SetDefault(this.TurnScore);
            this.SetDefault(this.DiedScore);
            this.SetDefault(this.RetireScore);
            this.SetDefault(this.RecoverScore);
            this.SetDefault(this.ChallengeScore);
            this.SetDefault(this.LoseScore);
            this.SetDefault(this.ResetScore);
        Label_0229:
            return;
        }

        private unsafe void UpdateNowScore()
        {
            TowerResuponse resuponse;
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            if ((this.NowTurn != null) == null)
            {
                goto Label_0032;
            }
            this.NowTurn.set_text(&resuponse.turn_num.ToString());
        Label_0032:
            if ((this.NowDied != null) == null)
            {
                goto Label_0059;
            }
            this.NowDied.set_text(&resuponse.died_num.ToString());
        Label_0059:
            if ((this.NowRetire != null) == null)
            {
                goto Label_0080;
            }
            this.NowRetire.set_text(&resuponse.retire_num.ToString());
        Label_0080:
            if ((this.NowRecover != null) == null)
            {
                goto Label_00A7;
            }
            this.NowRecover.set_text(&resuponse.recover_num.ToString());
        Label_00A7:
            if ((this.NowChallenge != null) == null)
            {
                goto Label_00CE;
            }
            this.NowChallenge.set_text(&resuponse.challenge_num.ToString());
        Label_00CE:
            if ((this.NowLose != null) == null)
            {
                goto Label_00F5;
            }
            this.NowLose.set_text(&resuponse.lose_num.ToString());
        Label_00F5:
            if ((this.NowReset != null) == null)
            {
                goto Label_011C;
            }
            this.NowReset.set_text(&resuponse.reset_num.ToString());
        Label_011C:
            return;
        }

        private unsafe void UpdateTotalScore()
        {
            GameManager manager;
            TowerResuponse resuponse;
            TOWER_RANK tower_rank;
            string str;
            string str2;
            Image image;
            manager = MonoSingleton<GameManager>.Instance;
            resuponse = manager.TowerResuponse;
            if (resuponse.speedRank == null)
            {
                goto Label_015D;
            }
            if (resuponse.techRank == null)
            {
                goto Label_015D;
            }
            if ((this.SpdRank != null) == null)
            {
                goto Label_004A;
            }
            this.SpdRank.set_text(&resuponse.speedRank.ToString());
        Label_004A:
            if ((this.TecRank != null) == null)
            {
                goto Label_0071;
            }
            this.TecRank.set_text(&resuponse.techRank.ToString());
        Label_0071:
            str = ((TOWER_RANK) manager.CalcTowerRank(1)).ToString();
            if ((this.TotalScore != null) == null)
            {
                goto Label_00ED;
            }
            str2 = str;
            str2 = str2.Replace("_PLUS", string.Empty).Replace("_MINUS", string.Empty);
            image = this.TotalScore.GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_00ED;
            }
            image.set_sprite(this.Icons.GetSprite(str2));
        Label_00ED:
            if (str.IndexOf("_PLUS") == -1)
            {
                goto Label_0125;
            }
            if ((this.PlusObj != null) == null)
            {
                goto Label_0207;
            }
            this.PlusObj.get_gameObject().SetActive(1);
            goto Label_0158;
        Label_0125:
            if (str.IndexOf("_MINUS") == -1)
            {
                goto Label_0207;
            }
            if ((this.MinusObj != null) == null)
            {
                goto Label_0207;
            }
            this.MinusObj.get_gameObject().SetActive(1);
        Label_0158:
            goto Label_0207;
        Label_015D:
            if ((this.SpdRank != null) == null)
            {
                goto Label_017F;
            }
            this.SpdRank.get_gameObject().SetActive(0);
        Label_017F:
            if ((this.TecRank != null) == null)
            {
                goto Label_01A1;
            }
            this.TecRank.get_gameObject().SetActive(0);
        Label_01A1:
            if ((this.TotalScore != null) == null)
            {
                goto Label_01C3;
            }
            this.TotalScore.get_gameObject().SetActive(0);
        Label_01C3:
            if ((this.PlusObj != null) == null)
            {
                goto Label_01E5;
            }
            this.PlusObj.get_gameObject().SetActive(0);
        Label_01E5:
            if ((this.MinusObj != null) == null)
            {
                goto Label_0207;
            }
            this.MinusObj.get_gameObject().SetActive(0);
        Label_0207:
            return;
        }
    }
}

