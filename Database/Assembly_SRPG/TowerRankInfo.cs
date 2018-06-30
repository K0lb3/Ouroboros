namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class TowerRankInfo : MonoBehaviour
    {
        private readonly int SHOW_VIP_RANK;
        public Text Ranking;
        public Image OwnIcon;
        public GameObject OwnObj;
        public Image OwnTotalScore;
        public Text OwnSpeedScore;
        public Text OwnTechScore;
        public GameObject OwnSpeedObj;
        public GameObject OwnTechObj;
        [HeaderBar("▼「ランキング」表示用（最上階までクリア済み）")]
        public GameObject ClearPage;
        public GameObject ClearPageParent;
        public ListItemEvents ClearTemplate;
        [HeaderBar("▼「ランキング」表示用（最上階まで未クリア）")]
        public GameObject NotClearPage;
        public GameObject NotClearPageParent;
        public ListItemEvents NotClearTemplate;
        [HeaderBar("▼「ランキング」表示用（集計中）")]
        public GameObject NotDataObj;
        [HeaderBar("▼「自分の戦績」表示用オブジェクトの親"), SerializeField]
        private GameObject m_TowerPlayerInfoRoot;
        [HeaderBar("▼タブ")]
        public Toggle Speed;
        public Toggle Tech;
        public Toggle OwnStatus;
        public GameObject Root;
        private SpriteSheet mSheet;
        private bool IsSpeed;

        public TowerRankInfo()
        {
            this.SHOW_VIP_RANK = 4;
            this.IsSpeed = 1;
            base..ctor();
            return;
        }

        private int GetSameRank(int score, int rank)
        {
            GameManager manager;
            TowerResuponse resuponse;
            TowerResuponse.TowerRankParam[] paramArray;
            int num;
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            if (((resuponse.SpdRank == null) || (((int) resuponse.SpdRank.Length) <= 0)) && ((resuponse.TecRank == null) || (((int) resuponse.TecRank.Length) <= 0)))
            {
                goto Label_0082;
            }
            paramArray = (this.IsSpeed == null) ? resuponse.TecRank : resuponse.SpdRank;
            num = 0;
            goto Label_0079;
        Label_0063:
            if (paramArray[num].score != score)
            {
                goto Label_0075;
            }
            return (num + 1);
        Label_0075:
            num += 1;
        Label_0079:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0063;
            }
        Label_0082:
            return rank;
        }

        public void OnChangeOwnStatus(bool val)
        {
            if (val == null)
            {
                goto Label_0036;
            }
            GameUtility.SetGameObjectActive(this.m_TowerPlayerInfoRoot, 1);
            GameUtility.SetGameObjectActive(this.ClearPage, 0);
            GameUtility.SetGameObjectActive(this.NotClearPage, 0);
            GameUtility.SetGameObjectActive(this.NotDataObj, 0);
        Label_0036:
            return;
        }

        public void OnChangeSpeed(bool val)
        {
            if (val == null)
            {
                goto Label_002B;
            }
            GameUtility.SetGameObjectActive(this.m_TowerPlayerInfoRoot, 0);
            this.IsSpeed = 1;
            this.UpdateOwnValue();
            this.UpdateRankValue(this.IsSpeed);
        Label_002B:
            return;
        }

        public void OnChangeTech(bool val)
        {
            if (val == null)
            {
                goto Label_002B;
            }
            GameUtility.SetGameObjectActive(this.m_TowerPlayerInfoRoot, 0);
            this.IsSpeed = 0;
            this.UpdateOwnValue();
            this.UpdateRankValue(this.IsSpeed);
        Label_002B:
            return;
        }

        private void OnItemDetail(GameObject go)
        {
        }

        private void OnItemSelect(GameObject go)
        {
        }

        private void Start()
        {
            this.mSheet = AssetManager.Load<SpriteSheet>("UI/TowerRankIcon");
            this.UpdateOwnValue();
            this.UpdateRankValue(1);
            if ((this.Speed != null) == null)
            {
                goto Label_004A;
            }
            this.Speed.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnChangeSpeed));
        Label_004A:
            if ((this.Tech != null) == null)
            {
                goto Label_0077;
            }
            this.Tech.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnChangeTech));
        Label_0077:
            if ((this.OwnStatus != null) == null)
            {
                goto Label_00A4;
            }
            this.OwnStatus.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnChangeOwnStatus));
        Label_00A4:
            if ((this.OwnObj != null) == null)
            {
                goto Label_00CA;
            }
            DataSource.Bind<PlayerData>(this.OwnObj, MonoSingleton<GameManager>.Instance.Player);
        Label_00CA:
            return;
        }

        private unsafe void UpdateOwnValue()
        {
            GameManager manager;
            TowerResuponse resuponse;
            TowerParam param;
            bool flag;
            int num;
            PlayerData data;
            PartyData data2;
            long num2;
            UnitData data3;
            string str;
            int num3;
            manager = MonoSingleton<GameManager>.Instance;
            resuponse = manager.TowerResuponse;
            param = manager.FindTower(resuponse.TowerID);
            if (resuponse == null)
            {
                goto Label_0082;
            }
            flag = (resuponse.speedRank == null) ? 0 : ((resuponse.techRank == 0) == 0);
            if ((this.ClearPage != null) == null)
            {
                goto Label_005D;
            }
            this.ClearPage.get_gameObject().SetActive(flag);
        Label_005D:
            if ((this.NotClearPage != null) == null)
            {
                goto Label_0082;
            }
            this.NotClearPage.get_gameObject().SetActive(flag == 0);
        Label_0082:
            num = (this.IsSpeed == null) ? resuponse.techRank : resuponse.speedRank;
            num = this.GetSameRank((this.IsSpeed == null) ? resuponse.tec_score : resuponse.spd_score, num);
            if ((this.OwnIcon != null) == null)
            {
                goto Label_0127;
            }
            if (num > this.SHOW_VIP_RANK)
            {
                goto Label_010C;
            }
            num3 = num - 1;
            this.OwnIcon.set_sprite(this.mSheet.GetSprite(&num3.ToString()));
            goto Label_0127;
        Label_010C:
            this.OwnIcon.set_sprite(this.mSheet.GetSprite("normal"));
        Label_0127:
            if ((this.Ranking != null) == null)
            {
                goto Label_0173;
            }
            this.Ranking.get_gameObject().SetActive(num > this.SHOW_VIP_RANK);
            this.Ranking.set_text(&num.ToString() + LocalizedText.Get("sys.TOWER_RANK_LABEL"));
        Label_0173:
            if ((this.OwnObj != null) == null)
            {
                goto Label_01BD;
            }
            data = manager.Player;
            data2 = data.Partys[6];
            num2 = data2.GetUnitUniqueID(0);
            data3 = data.FindUnitDataByUniqueID(num2);
            DataSource.Bind<UnitData>(this.OwnObj, data3);
        Label_01BD:
            if ((this.OwnSpeedObj != null) == null)
            {
                goto Label_01E4;
            }
            this.OwnSpeedObj.get_gameObject().SetActive(this.IsSpeed);
        Label_01E4:
            if ((this.OwnTechObj != null) == null)
            {
                goto Label_020E;
            }
            this.OwnTechObj.get_gameObject().SetActive(this.IsSpeed == 0);
        Label_020E:
            if (this.IsSpeed == null)
            {
                goto Label_0245;
            }
            if ((this.OwnSpeedScore != null) == null)
            {
                goto Label_026C;
            }
            this.OwnSpeedScore.set_text(&resuponse.spd_score.ToString());
            goto Label_026C;
        Label_0245:
            if ((this.OwnSpeedScore != null) == null)
            {
                goto Label_026C;
            }
            this.OwnTechScore.set_text(&resuponse.tec_score.ToString());
        Label_026C:
            if ((this.OwnTotalScore != null) == null)
            {
                goto Label_02CC;
            }
            str = string.Empty;
            if (this.IsSpeed == null)
            {
                goto Label_02A4;
            }
            str = manager.ConvertTowerScoreToRank(param, resuponse.spd_score, 0);
            goto Label_02B4;
        Label_02A4:
            str = manager.ConvertTowerScoreToRank(param, resuponse.tec_score, 1);
        Label_02B4:
            this.OwnTotalScore.set_sprite(this.mSheet.GetSprite(str));
        Label_02CC:
            return;
        }

        private void UpdateRankValue(bool isSpeedRank)
        {
            TowerResuponse resuponse;
            GameObject obj2;
            ListItemEvents events;
            Transform transform;
            int num;
            Transform transform2;
            int num2;
            TowerResuponse.TowerRankParam[] paramArray;
            int num3;
            ListItemEvents events2;
            int num4;
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            obj2 = this.NotClearPageParent;
            events = this.NotClearTemplate;
            if (resuponse == null)
            {
                goto Label_00C2;
            }
            obj2 = ((resuponse.speedRank == null) || (resuponse.techRank == null)) ? this.NotClearPageParent : this.ClearPageParent;
            events = ((resuponse.speedRank == null) || (resuponse.techRank == null)) ? this.NotClearTemplate : this.ClearTemplate;
            if (((resuponse.SpdRank != null) && (((int) resuponse.SpdRank.Length) != null)) && ((resuponse.TecRank != null) && (((int) resuponse.TecRank.Length) != null)))
            {
                goto Label_00C2;
            }
            if ((this.NotDataObj != null) == null)
            {
                goto Label_00C1;
            }
            this.NotDataObj.get_gameObject().SetActive(1);
        Label_00C1:
            return;
        Label_00C2:
            if ((this.NotClearTemplate != null) == null)
            {
                goto Label_00E4;
            }
            this.NotClearTemplate.get_gameObject().SetActive(0);
        Label_00E4:
            if ((this.ClearTemplate != null) == null)
            {
                goto Label_0106;
            }
            this.ClearTemplate.get_gameObject().SetActive(0);
        Label_0106:
            transform = obj2.get_transform();
            num = transform.get_childCount() - 1;
            goto Label_015B;
        Label_011C:
            transform2 = transform.GetChild(num);
            if ((transform2 == null) == null)
            {
                goto Label_0138;
            }
            goto Label_0155;
        Label_0138:
            if (transform2.get_gameObject().get_activeSelf() == null)
            {
                goto Label_0155;
            }
            Object.DestroyImmediate(transform2.get_gameObject());
        Label_0155:
            num -= 1;
        Label_015B:
            if (num >= 0)
            {
                goto Label_011C;
            }
            if ((obj2 != null) == null)
            {
                goto Label_017B;
            }
            obj2.get_gameObject().SetActive(1);
        Label_017B:
            num2 = 0;
            paramArray = (isSpeedRank == null) ? resuponse.TecRank : resuponse.SpdRank;
            num3 = 0;
            goto Label_0263;
        Label_019F:
            events2 = Object.Instantiate<ListItemEvents>(events);
            num2 = num3;
            if ((events2 != null) == null)
            {
                goto Label_025D;
            }
            DataSource.Bind<UnitData>(events2.get_gameObject(), paramArray[num3].unit);
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            events2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
            events2.get_transform().SetParent(obj2.get_transform(), 0);
            events2.get_gameObject().SetActive(1);
            num4 = 0;
            goto Label_0244;
        Label_021C:
            if (paramArray[num4].score != paramArray[num3].score)
            {
                goto Label_023E;
            }
            num2 = num4;
            goto Label_024D;
        Label_023E:
            num4 += 1;
        Label_0244:
            if (num4 < num3)
            {
                goto Label_021C;
            }
        Label_024D:
            this.UpdateValue(events2, num2, paramArray[num3], isSpeedRank);
        Label_025D:
            num3 += 1;
        Label_0263:
            if (num3 < ((int) paramArray.Length))
            {
                goto Label_019F;
            }
            if ((this.Root != null) == null)
            {
                goto Label_028A;
            }
            GameParameter.UpdateAll(this.Root);
        Label_028A:
            return;
        }

        private unsafe void UpdateValue(ListItemEvents obj, int num, TowerResuponse.TowerRankParam param, bool isSpeed)
        {
            GameManager manager;
            TowerResuponse resuponse;
            TowerParam param2;
            Transform transform;
            Transform transform2;
            Image image;
            Transform transform3;
            Text text;
            Transform transform4;
            Text text2;
            Transform transform5;
            Text text3;
            Transform transform6;
            Transform transform7;
            Text text4;
            string str;
            Transform transform8;
            Image image2;
            Transform transform9;
            Transform transform10;
            Text text5;
            string str2;
            Transform transform11;
            Image image3;
            int num2;
            if ((this.mSheet == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            manager = MonoSingleton<GameManager>.Instance;
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            param2 = manager.FindTower(resuponse.TowerID);
            DataSource.Bind<TowerResuponse.TowerRankParam>(obj.get_gameObject(), param);
            transform = obj.get_transform().FindChild("body");
            if ((transform != null) == null)
            {
                goto Label_0381;
            }
            transform2 = transform.get_transform().FindChild("ranking");
            if ((transform2 != null) == null)
            {
                goto Label_00CF;
            }
            image = transform2.GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_00CF;
            }
            if (num >= this.SHOW_VIP_RANK)
            {
                goto Label_00B8;
            }
            image.set_sprite(this.mSheet.GetSprite(&num.ToString()));
            goto Label_00CF;
        Label_00B8:
            image.set_sprite(this.mSheet.GetSprite("normal"));
        Label_00CF:
            transform3 = transform.get_transform().FindChild("rank");
            if ((transform3 != null) == null)
            {
                goto Label_0143;
            }
            text = transform3.GetComponent<Text>();
            if ((text != null) == null)
            {
                goto Label_0143;
            }
            if (num >= this.SHOW_VIP_RANK)
            {
                goto Label_0121;
            }
            text.set_text(string.Empty);
            goto Label_0143;
        Label_0121:
            num2 = num + 1;
            text.set_text(&num2.ToString() + LocalizedText.Get("sys.TOWER_RANK_LABEL"));
        Label_0143:
            transform4 = transform.get_transform().FindChild("Text_player");
            if ((transform4 != null) == null)
            {
                goto Label_0185;
            }
            text2 = transform4.GetComponent<Text>();
            if ((text2 != null) == null)
            {
                goto Label_0185;
            }
            text2.set_text(param.name);
        Label_0185:
            transform5 = transform.get_transform().FindChild("player_level");
            if ((transform5 != null) == null)
            {
                goto Label_01DB;
            }
            text3 = transform5.GetComponent<Text>();
            if ((text3 != null) == null)
            {
                goto Label_01DB;
            }
            text3.set_text(LocalizedText.Get("sys.TOWER_RANK_LBL_LV") + &param.lv.ToString());
        Label_01DB:
            if (isSpeed == null)
            {
                goto Label_02B4;
            }
            transform6 = transform.get_transform().FindChild("speed");
            if ((transform6 != null) == null)
            {
                goto Label_0256;
            }
            transform7 = transform6.get_transform().FindChild("speed_cnt");
            if ((transform7 != null) == null)
            {
                goto Label_0249;
            }
            text4 = transform7.GetComponent<Text>();
            if ((text4 != null) == null)
            {
                goto Label_0249;
            }
            text4.set_text(&param.score.ToString());
        Label_0249:
            transform6.get_gameObject().SetActive(1);
        Label_0256:
            str = manager.ConvertTowerScoreToRank(param2, param.score, 0);
            transform8 = transform.get_transform().FindChild("score_img");
            if ((transform8 != null) == null)
            {
                goto Label_0381;
            }
            image2 = transform8.GetComponent<Image>();
            if ((image2 != null) == null)
            {
                goto Label_0381;
            }
            image2.set_sprite(this.mSheet.GetSprite(str));
            goto Label_0381;
        Label_02B4:
            transform9 = transform.get_transform().FindChild("tech");
            if ((transform9 != null) == null)
            {
                goto Label_0328;
            }
            transform10 = transform9.get_transform().FindChild("tech_cnt");
            if ((transform10 != null) == null)
            {
                goto Label_031B;
            }
            text5 = transform10.GetComponent<Text>();
            if ((text5 != null) == null)
            {
                goto Label_031B;
            }
            text5.set_text(&param.score.ToString());
        Label_031B:
            transform9.get_gameObject().SetActive(1);
        Label_0328:
            str2 = manager.ConvertTowerScoreToRank(param2, param.score, 1);
            transform11 = transform.get_transform().FindChild("score_img");
            if ((transform11 != null) == null)
            {
                goto Label_0381;
            }
            image3 = transform11.GetComponent<Image>();
            if ((image3 != null) == null)
            {
                goto Label_0381;
            }
            image3.set_sprite(this.mSheet.GetSprite(str2));
        Label_0381:
            return;
        }
    }
}

