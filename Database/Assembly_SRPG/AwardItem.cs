namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(0, "UpdateConfigPlayerInfo", 0, 0)]
    public class AwardItem : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private GameObject AwardBG;
        [SerializeField]
        private Text AwardTxt;
        public PlayerType Type;
        private ImageArray mImageArray;
        private bool IsDone;
        private string mSelectedAward;
        private bool IsRefresh;
        private AwardParam mAwardParam;

        public AwardItem()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0013;
            }
            this.IsRefresh = 0;
            this.SetUp();
        Label_0013:
            return;
        }

        private void Awake()
        {
            ImageArray array;
            if ((this.AwardBG != null) == null)
            {
                goto Label_003C;
            }
            this.AwardBG.SetActive(0);
            array = this.AwardBG.GetComponent<ImageArray>();
            if ((array != null) == null)
            {
                goto Label_003C;
            }
            this.mImageArray = array;
        Label_003C:
            if ((this.AwardTxt != null) == null)
            {
                goto Label_0073;
            }
            this.AwardTxt.set_text(LocalizedText.Get("sys.TEXT_NOT_SELECT"));
            this.AwardTxt.get_gameObject().SetActive(0);
        Label_0073:
            return;
        }

        private void Initialize()
        {
            this.SetUp();
            this.IsRefresh = 0;
            return;
        }

        private void OnEnable()
        {
            this.Initialize();
            return;
        }

        private void Refresh()
        {
            this.SetUp();
            if (this.mAwardParam == null)
            {
                goto Label_00B0;
            }
            if ((this.mImageArray != null) == null)
            {
                goto Label_00D1;
            }
            if (((int) this.mImageArray.Images.Length) > this.mAwardParam.grade)
            {
                goto Label_005B;
            }
            this.SetExtraAwardImage();
            this.AwardTxt.set_text(string.Empty);
            goto Label_00AB;
        Label_005B:
            this.mImageArray.ImageIndex = this.mAwardParam.grade;
            this.AwardTxt.set_text((string.IsNullOrEmpty(this.mAwardParam.name) != null) ? LocalizedText.Get("sys.TEXT_NOT_SELECT") : this.mAwardParam.name);
        Label_00AB:
            goto Label_00D1;
        Label_00B0:
            this.mImageArray.ImageIndex = 0;
            this.AwardTxt.set_text(LocalizedText.Get("sys.TEXT_NOT_SELECT"));
        Label_00D1:
            if ((this.AwardBG != null) == null)
            {
                goto Label_00EE;
            }
            this.AwardBG.SetActive(1);
        Label_00EE:
            if ((this.AwardTxt != null) == null)
            {
                goto Label_0110;
            }
            this.AwardTxt.get_gameObject().SetActive(1);
        Label_0110:
            return;
        }

        private bool SetExtraAwardImage()
        {
            string str;
            SpriteSheet sheet;
            if (this.mAwardParam != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            str = this.mAwardParam.bg;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_004F;
            }
            sheet = AssetManager.Load<SpriteSheet>(AwardListItem.EXTRA_GRADE_IMAGEPATH);
            if ((sheet != null) == null)
            {
                goto Label_004D;
            }
            this.mImageArray.set_sprite(sheet.GetSprite(str));
        Label_004D:
            return 1;
        Label_004F:
            return 0;
        }

        private void SetUp()
        {
            string str;
            PlayerData data;
            FriendData data2;
            ArenaPlayer player;
            JSON_MyPhotonPlayerParam param;
            ChatPlayerData data3;
            TowerResuponse.TowerRankParam param2;
            AwardParam param3;
            str = string.Empty;
            if (this.Type != null)
            {
                goto Label_0030;
            }
            data = DataSource.FindDataOfClass<PlayerData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_010B;
            }
            str = data.SelectedAward;
            goto Label_010B;
        Label_0030:
            if (this.Type != 1)
            {
                goto Label_005B;
            }
            data2 = DataSource.FindDataOfClass<FriendData>(base.get_gameObject(), null);
            if (data2 == null)
            {
                goto Label_010B;
            }
            str = data2.SelectAward;
            goto Label_010B;
        Label_005B:
            if (this.Type != 2)
            {
                goto Label_0086;
            }
            player = DataSource.FindDataOfClass<ArenaPlayer>(base.get_gameObject(), null);
            if (player == null)
            {
                goto Label_010B;
            }
            str = player.SelectAward;
            goto Label_010B;
        Label_0086:
            if (this.Type != 3)
            {
                goto Label_00B4;
            }
            param = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_010B;
            }
            str = param.award;
            goto Label_010B;
        Label_00B4:
            if (this.Type != 4)
            {
                goto Label_00E2;
            }
            data3 = DataSource.FindDataOfClass<ChatPlayerData>(base.get_gameObject(), null);
            if (data3 == null)
            {
                goto Label_010B;
            }
            str = data3.award;
            goto Label_010B;
        Label_00E2:
            if (this.Type != 5)
            {
                goto Label_010B;
            }
            param2 = DataSource.FindDataOfClass<TowerResuponse.TowerRankParam>(base.get_gameObject(), null);
            if (param2 == null)
            {
                goto Label_010B;
            }
            str = param2.selected_award;
        Label_010B:
            this.mSelectedAward = str;
            if (string.IsNullOrEmpty(this.mSelectedAward) != null)
            {
                goto Label_014D;
            }
            param3 = MonoSingleton<GameManager>.Instance.MasterParam.GetAwardParam(this.mSelectedAward);
            if (param3 == null)
            {
                goto Label_0154;
            }
            this.mAwardParam = param3;
            goto Label_0154;
        Label_014D:
            this.mAwardParam = null;
        Label_0154:
            this.IsDone = 1;
            return;
        }

        private void Start()
        {
            this.Initialize();
            return;
        }

        private void Update()
        {
            if (this.IsDone == null)
            {
                goto Label_0023;
            }
            if (this.IsRefresh != null)
            {
                goto Label_0023;
            }
            this.IsRefresh = 1;
            this.Refresh();
        Label_0023:
            return;
        }

        public enum PlayerType : byte
        {
            Player = 0,
            Friend = 1,
            ArenaPlayer = 2,
            MultiPlayer = 3,
            ChatPlayer = 4,
            TowerPlayer = 5
        }
    }
}

