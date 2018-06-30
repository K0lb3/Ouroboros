namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class TowerClear : MonoBehaviour
    {
        private readonly int RANKIN_SCORE;
        public Text TowerName;
        public Text ArrivedNum;
        public Text BigArrivedNum;
        public Text BigArrivedNumEff;
        public GameObject RankInfo;
        public GameObject ResultInfo;
        public Button BackButton;
        public Button NextButton;
        public Text[] CountupText;
        public GameObject[] ScoreObj;
        public GameObject[] NewRecord;
        public GameObject[] ScoreContents;
        public GameObject SpeedRank;
        public GameObject TechRank;
        public GameObject RankInObj;
        public GameObject RankingObj;
        public GameObject ContinueObj;
        public GameObject CongraObj;
        public GameObject TotalScoreObj;
        public GameObject PlusObj;
        public GameObject MinusObj;
        public Image TotalScoreImg;
        public Text SpeedRankText;
        public Text TechRankText;
        private CLEAR_FLOW Nowflow;
        private int NowCount;
        private Text NowText;
        private bool NextState;
        private SpriteSheet Icons;
        private int CountupIndex;
        private int[] NowScore;
        private int[] OldBestScore;
        private int CountValue;
        private TowerParam m_TowerParam;
        private readonly int EFF_TIME;

        public TowerClear()
        {
            this.RANKIN_SCORE = 10;
            this.CountupText = new Text[0];
            this.ScoreObj = new GameObject[0];
            this.NewRecord = new GameObject[0];
            this.ScoreContents = new GameObject[0];
            this.EFF_TIME = 2;
            base..ctor();
            return;
        }

        private void OnClickNext()
        {
            this.NextState = 1;
            if (this.Nowflow != 1)
            {
                goto Label_0020;
            }
            this.RefleshResult();
            this.NextState = 0;
        Label_0020:
            return;
        }

        private void RefleshResult()
        {
            if (MonoSingleton<GameManager>.Instance.TowerResuponse.clear != 2)
            {
                goto Label_0065;
            }
            if ((this.RankInfo != null) == null)
            {
                goto Label_0037;
            }
            this.RankInfo.get_gameObject().SetActive(0);
        Label_0037:
            if ((this.ResultInfo != null) == null)
            {
                goto Label_0059;
            }
            this.ResultInfo.get_gameObject().SetActive(1);
        Label_0059:
            this.Nowflow = 3;
            goto Label_006D;
        Label_0065:
            this.Nowflow = 9;
        Label_006D:
            return;
        }

        public unsafe void Refresh()
        {
            object[] objArray1;
            GameManager manager;
            SRPG.TowerResuponse resuponse;
            PlayerData data;
            PartyData data2;
            UnitData data3;
            string str;
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            data = MonoSingleton<GameManager>.Instance.Player;
            data2 = data.FindPartyOfType(6);
            data3 = data.FindUnitDataByUniqueID(data2.GetUnitUniqueID(data2.LeaderIndex));
            if (data3 == null)
            {
                goto Label_0048;
            }
            DataSource.Bind<UnitData>(base.get_gameObject(), data3);
        Label_0048:
            if (resuponse.arrived_num <= 0)
            {
                goto Label_01A1;
            }
            if (this.TowerFloorParam == null)
            {
                goto Label_009B;
            }
            objArray1 = new object[] { this.TowerFloorParam.title, (int) this.TowerFloorParam.GetFloorNo() };
            this.TowerName.set_text(LocalizedText.Get("sys.TOWER_CLEAR_DESC", objArray1));
        Label_009B:
            str = &this.TowerResuponse.arrived_num.ToString();
            if (this.TowerResuponse.arrived_num <= 0x63)
            {
                goto Label_00F3;
            }
            if ((this.ArrivedNum != null) == null)
            {
                goto Label_0151;
            }
            this.ArrivedNum.get_gameObject().SetActive(1);
            this.ArrivedNum.set_text(str);
            goto Label_0151;
        Label_00F3:
            if ((this.BigArrivedNum != null) == null)
            {
                goto Label_0151;
            }
            if ((this.BigArrivedNumEff != null) == null)
            {
                goto Label_0151;
            }
            this.BigArrivedNum.get_gameObject().SetActive(1);
            this.BigArrivedNumEff.get_gameObject().SetActive(1);
            this.BigArrivedNum.set_text(str);
            this.BigArrivedNumEff.set_text(str);
        Label_0151:
            if ((this.ResultInfo != null) == null)
            {
                goto Label_0173;
            }
            this.ResultInfo.get_gameObject().SetActive(0);
        Label_0173:
            if ((this.BackButton != null) == null)
            {
                goto Label_0195;
            }
            this.BackButton.get_gameObject().SetActive(0);
        Label_0195:
            this.Nowflow = 1;
            goto Label_01A7;
        Label_01A1:
            this.RefleshResult();
        Label_01A7:
            return;
        }

        private void Start()
        {
            GameManager manager;
            SRPG.TowerResuponse resuponse;
            manager = MonoSingleton<GameManager>.Instance;
            resuponse = manager.TowerResuponse;
            this.m_TowerParam = manager.FindTower(manager.TowerResuponse.TowerID);
            this.NowScore = new int[] { resuponse.turn_num, resuponse.died_num, resuponse.retire_num, resuponse.recover_num };
            this.OldBestScore = new int[] { resuponse.spd_score, resuponse.tec_score, resuponse.ret_score, resuponse.rcv_score };
            this.CountupIndex = 0;
            if ((this.NextButton != null) == null)
            {
                goto Label_00E0;
            }
            this.NextButton.get_onClick().AddListener(new UnityAction(this, this.OnClickNext));
        Label_00E0:
            if (this.NowScore == null)
            {
                goto Label_010C;
            }
            if (this.ScoreContents == null)
            {
                goto Label_010C;
            }
            if (this.ScoreObj == null)
            {
                goto Label_010C;
            }
            if (this.NewRecord != null)
            {
                goto Label_0116;
            }
        Label_010C:
            DebugUtility.LogWarning("Error TowerClear Param Check!!!!");
        Label_0116:
            this.Icons = AssetManager.Load<SpriteSheet>("UI/TowerRankIcon");
            return;
        }

        private unsafe void Update()
        {
            GameManager manager;
            SRPG.TowerResuponse resuponse;
            Image image;
            string str;
            TOWER_RANK tower_rank;
            string str2;
            string str3;
            CLEAR_FLOW clear_flow;
            int num;
            if (this.NowScore == null)
            {
                goto Label_002C;
            }
            if (this.ScoreContents == null)
            {
                goto Label_002C;
            }
            if (this.ScoreObj == null)
            {
                goto Label_002C;
            }
            if (this.NewRecord != null)
            {
                goto Label_002D;
            }
        Label_002C:
            return;
        Label_002D:
            manager = MonoSingleton<GameManager>.Instance;
            resuponse = manager.TowerResuponse;
            switch ((this.Nowflow - 2))
            {
                case 0:
                    goto Label_0070;

                case 1:
                    goto Label_007B;

                case 2:
                    goto Label_0105;

                case 3:
                    goto Label_0188;

                case 4:
                    goto Label_0293;

                case 5:
                    goto Label_03A8;

                case 6:
                    goto Label_04A8;

                case 7:
                    goto Label_05A9;
            }
            goto Label_05D7;
        Label_0070:
            this.RefleshResult();
            goto Label_05DC;
        Label_007B:
            this.Nowflow = 4;
            this.NowCount = 0;
            this.CountValue = (int) (((float) (this.NowScore[this.CountupIndex] / this.EFF_TIME)) * Time.get_deltaTime());
            this.CountValue = Mathf.Max(this.CountValue, 1);
            this.NowText = this.CountupText[this.CountupIndex];
            if ((this.ScoreContents[this.CountupIndex] != null) == null)
            {
                goto Label_05DC;
            }
            this.ScoreContents[this.CountupIndex].get_gameObject().SetActive(1);
            goto Label_05DC;
        Label_0105:
            this.NowCount += this.CountValue;
            if (this.NowCount >= this.NowScore[this.CountupIndex])
            {
                goto Label_013B;
            }
            if (this.NextState == null)
            {
                goto Label_015C;
            }
        Label_013B:
            this.NowCount = this.NowScore[this.CountupIndex];
            this.Nowflow = 5;
            this.NextState = 0;
        Label_015C:
            if ((this.NowText != null) == null)
            {
                goto Label_05DC;
            }
            this.NowText.set_text(&this.NowCount.ToString());
            goto Label_05DC;
        Label_0188:
            if ((this.Icons != null) == null)
            {
                goto Label_0213;
            }
            if ((this.ScoreObj[this.CountupIndex] != null) == null)
            {
                goto Label_0213;
            }
            this.ScoreObj[this.CountupIndex].get_gameObject().SetActive(1);
            image = this.ScoreObj[this.CountupIndex].GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_0213;
            }
            str = manager.ConvertTowerScoreToRank(this.m_TowerParam, this.NowCount, this.CountupIndex);
            image.set_sprite(this.Icons.GetSprite(str));
        Label_0213:
            if ((this.NewRecord[this.CountupIndex] != null) == null)
            {
                goto Label_0262;
            }
            if (this.NowScore[this.CountupIndex] >= this.OldBestScore[this.CountupIndex])
            {
                goto Label_0262;
            }
            this.NewRecord[this.CountupIndex].get_gameObject().SetActive(1);
        Label_0262:
            if ((this.CountupIndex += 1) >= 4)
            {
                goto Label_0287;
            }
            this.Nowflow = 3;
            goto Label_028E;
        Label_0287:
            this.Nowflow = 6;
        Label_028E:
            goto Label_05DC;
        Label_0293:
            if ((this.TotalScoreObj != null) == null)
            {
                goto Label_039C;
            }
            this.TotalScoreObj.get_gameObject().SetActive(1);
            str2 = ((TOWER_RANK) manager.CalcTowerRank(1)).ToString();
            if ((this.TotalScoreImg != null) == null)
            {
                goto Label_031F;
            }
            str3 = str2;
            str3 = str3.Replace("_PLUS", string.Empty).Replace("_MINUS", string.Empty);
            this.TotalScoreImg.set_sprite(this.Icons.GetSprite(str3));
        Label_031F:
            if (str2.IndexOf("_PLUS") == -1)
            {
                goto Label_0358;
            }
            if ((this.PlusObj != null) == null)
            {
                goto Label_038C;
            }
            this.PlusObj.get_gameObject().SetActive(1);
            goto Label_038C;
        Label_0358:
            if (str2.IndexOf("_MINUS") == -1)
            {
                goto Label_038C;
            }
            if ((this.MinusObj != null) == null)
            {
                goto Label_038C;
            }
            this.MinusObj.get_gameObject().SetActive(1);
        Label_038C:
            MonoSingleton<GameManager>.Instance.Player.OnTowerScore(1);
        Label_039C:
            this.Nowflow = 7;
            goto Label_05DC;
        Label_03A8:
            if (this.NextState == null)
            {
                goto Label_05DC;
            }
            if (resuponse.speedRank > this.RANKIN_SCORE)
            {
                goto Label_0495;
            }
            if ((this.RankingObj != null) == null)
            {
                goto Label_03E6;
            }
            this.RankingObj.get_gameObject().SetActive(1);
        Label_03E6:
            if ((this.RankInObj != null) == null)
            {
                goto Label_0408;
            }
            this.RankInObj.get_gameObject().SetActive(1);
        Label_0408:
            if ((this.CongraObj != null) == null)
            {
                goto Label_042A;
            }
            this.CongraObj.get_gameObject().SetActive(0);
        Label_042A:
            if ((this.ResultInfo != null) == null)
            {
                goto Label_044C;
            }
            this.ResultInfo.get_gameObject().SetActive(0);
        Label_044C:
            if ((this.SpeedRank != null) == null)
            {
                goto Label_046E;
            }
            this.SpeedRank.get_gameObject().SetActive(1);
        Label_046E:
            if ((this.SpeedRankText != null) == null)
            {
                goto Label_0495;
            }
            this.SpeedRankText.set_text(&resuponse.speedRank.ToString());
        Label_0495:
            this.Nowflow = 8;
            this.NextState = 0;
            goto Label_05DC;
        Label_04A8:
            if (this.NextState == null)
            {
                goto Label_05DC;
            }
            if (resuponse.techRank > this.RANKIN_SCORE)
            {
                goto Label_0595;
            }
            if ((this.RankingObj != null) == null)
            {
                goto Label_04E6;
            }
            this.RankingObj.get_gameObject().SetActive(1);
        Label_04E6:
            if ((this.RankInObj != null) == null)
            {
                goto Label_0508;
            }
            this.RankInObj.get_gameObject().SetActive(1);
        Label_0508:
            if ((this.CongraObj != null) == null)
            {
                goto Label_052A;
            }
            this.CongraObj.get_gameObject().SetActive(0);
        Label_052A:
            if ((this.ResultInfo != null) == null)
            {
                goto Label_054C;
            }
            this.ResultInfo.get_gameObject().SetActive(0);
        Label_054C:
            if ((this.TechRank != null) == null)
            {
                goto Label_056E;
            }
            this.TechRank.get_gameObject().SetActive(1);
        Label_056E:
            if ((this.TechRankText != null) == null)
            {
                goto Label_0595;
            }
            this.TechRankText.set_text(&resuponse.techRank.ToString());
        Label_0595:
            this.Nowflow = 9;
            this.NextState = 0;
            goto Label_05DC;
        Label_05A9:
            if ((this.BackButton != null) == null)
            {
                goto Label_05CB;
            }
            this.BackButton.get_gameObject().SetActive(1);
        Label_05CB:
            this.Nowflow = 0;
            goto Label_05DC;
        Label_05D7:;
        Label_05DC:
            return;
        }

        private SRPG.TowerFloorParam TowerFloorParam
        {
            get
            {
                if (this.TowerResuponse != null)
                {
                    goto Label_000D;
                }
                return null;
            Label_000D:
                return this.TowerResuponse.GetCurrentFloor();
            }
        }

        private SRPG.TowerResuponse TowerResuponse
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.TowerResuponse;
            }
        }

        private enum CLEAR_FLOW
        {
            NONE,
            RANK_IN,
            SCORE_IN,
            IN,
            LOOP,
            SCORE,
            TOTAL_SCORE,
            SPEED_RANK_IN,
            TECH_RANK_IN,
            FINISH
        }

        public enum TOWER_CLEAR_FLAG
        {
            NOT_CLEAR,
            CLEAR,
            CLEAR_AND_SCORE
        }
    }
}

