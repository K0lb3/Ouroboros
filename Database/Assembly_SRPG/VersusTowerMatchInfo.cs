namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(20, "TowerMatchDetail", 1, 20), Pin(0x73, "FindDraftRoom", 1, 0x73), Pin(0x10, "NotFindRoom", 1, 0x10), Pin(1, "Search", 0, 1), Pin(15, "FindRoom", 1, 15), Pin(12, "AlreadyStartFriendMode", 1, 12), Pin(11, "AudienceDisable", 1, 11)]
    public class VersusTowerMatchInfo : MonoBehaviour, IFlowInterface
    {
        private const int PIN_INPUT_SEARCH = 1;
        private const int PIN_OUTPUT_AUDIENCE_DISABLE = 11;
        private const int PIN_OUTPUT_ALREADY_START_FRIEND_MODE = 12;
        private const int PIN_OUTPUT_FIND_ROOM = 15;
        private const int PIN_OUTPUT_NOT_FIND_ROOM = 0x10;
        private const int PIN_OUTPUT_TOWERMATCH_DETAIL = 20;
        private const int PIN_OUTPUT_FIND_DRAFT_ROOM = 0x73;
        private readonly string PVP_URL;
        public GameObject template;
        public GameObject winbonus;
        public GameObject keyrateup;
        public GameObject parent;
        public GameObject keyinfo;
        public GameObject keyname;
        public GameObject lastfloor;
        public Text nowKey;
        public Text maxKey;
        public Text floor;
        public Text bonusRate;
        public Text winCnt;
        public Text endAt;
        public Button detailBtn;

        public VersusTowerMatchInfo()
        {
            this.PVP_URL = "notice/detail/pvp/tower";
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_000D;
            }
            this.Search();
        Label_000D:
            return;
        }

        private void OnClickDetail()
        {
            StringBuilder builder;
            builder = new StringBuilder();
            builder.Append(Network.SiteHost);
            builder.Append(this.PVP_URL);
            FlowNode_Variable.Set("SHARED_WEBWINDOW_TITLE", LocalizedText.Get("sys.MULTI_VERSUS_TOWER_DETAIL"));
            FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", builder.ToString());
            FlowNode_GameObject.ActivateOutputLinks(this, 20);
            return;
        }

        private unsafe void RefreshData()
        {
            object[] objArray1;
            GameManager manager;
            PlayerData data;
            List<GameObject> list;
            int num;
            VersusTowerParam param;
            int num2;
            GameObject obj2;
            Transform transform;
            Transform transform2;
            int num3;
            DateTime time;
            int num4;
            int num5;
            int num6;
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player;
            list = new List<GameObject>();
            num = data.VersusTowerKey;
            param = manager.GetCurrentVersusTowerParam(-1);
            if (param == null)
            {
                goto Label_03B1;
            }
            num2 = 0;
            goto Label_00F7;
        Label_0032:
            obj2 = Object.Instantiate<GameObject>(this.template);
            if ((obj2 == null) == null)
            {
                goto Label_0051;
            }
            goto Label_00ED;
        Label_0051:
            obj2.SetActive(1);
            if ((this.parent != null) == null)
            {
                goto Label_0082;
            }
            obj2.get_transform().SetParent(this.parent.get_transform(), 0);
        Label_0082:
            transform = obj2.get_transform().FindChild("on");
            transform2 = obj2.get_transform().FindChild("off");
            if ((transform != null) == null)
            {
                goto Label_00C5;
            }
            transform.get_gameObject().SetActive(num > 0);
        Label_00C5:
            if ((transform2 != null) == null)
            {
                goto Label_00E5;
            }
            transform2.get_gameObject().SetActive((num > 0) == 0);
        Label_00E5:
            list.Add(obj2);
        Label_00ED:
            num2 += 1;
            num -= 1;
        Label_00F7:
            if (num2 < param.RankupNum)
            {
                goto Label_0032;
            }
            this.template.SetActive(0);
            if ((this.nowKey != null) == null)
            {
                goto Label_0146;
            }
            this.nowKey.set_text(GameUtility.HalfNum2FullNum(&data.VersusTowerKey.ToString()));
        Label_0146:
            if ((this.maxKey != null) == null)
            {
                goto Label_0173;
            }
            this.maxKey.set_text(GameUtility.HalfNum2FullNum(&param.RankupNum.ToString()));
        Label_0173:
            if ((this.floor != null) == null)
            {
                goto Label_019E;
            }
            this.floor.set_text(&data.VersusTowerFloor.ToString());
        Label_019E:
            if ((this.winbonus != null) == null)
            {
                goto Label_01C3;
            }
            this.winbonus.SetActive(data.VersusTowerWinBonus > 1);
        Label_01C3:
            if ((this.keyrateup != null) == null)
            {
                goto Label_01FD;
            }
            this.keyrateup.SetActive((data.VersusTowerWinBonus <= 0) ? 0 : (param.RankupNum > 0));
        Label_01FD:
            if ((((this.bonusRate != null) == null) || (data.VersusTowerWinBonus <= 0)) || (param.WinNum <= 0))
            {
                goto Label_0266;
            }
            num3 = (param.WinNum + param.BonusNum) / param.WinNum;
            this.bonusRate.set_text(&num3.ToString());
        Label_0266:
            if ((this.winCnt != null) == null)
            {
                goto Label_0291;
            }
            this.winCnt.set_text(&data.VersusTowerWinBonus.ToString());
        Label_0291:
            if ((this.endAt != null) == null)
            {
                goto Label_031A;
            }
            time = TimeManager.FromUnixTime(manager.VersusTowerMatchEndAt);
            objArray1 = new object[] { (int) &time.Year, (int) &time.Month, (int) &time.Day, (int) &time.Hour, (int) &time.Minute };
            this.endAt.set_text(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_END_AT"), objArray1));
        Label_031A:
            if ((this.keyinfo != null) == null)
            {
                goto Label_0348;
            }
            this.keyinfo.SetActive((param.RankupNum == 0) == 0);
        Label_0348:
            if ((this.keyname != null) == null)
            {
                goto Label_0376;
            }
            this.keyname.SetActive((param.RankupNum == 0) == 0);
        Label_0376:
            if ((this.lastfloor != null) == null)
            {
                goto Label_03CE;
            }
            this.lastfloor.SetActive((param.RankupNum != null) ? 0 : manager.VersusTowerMatchBegin);
            goto Label_03CE;
        Label_03B1:
            if ((this.lastfloor != null) == null)
            {
                goto Label_03CE;
            }
            this.lastfloor.SetActive(0);
        Label_03CE:
            return;
        }

        private void Search()
        {
            GameManager manager;
            int num;
            MyPhoton photon;
            JSON_MyPhotonRoomParam param;
            manager = MonoSingleton<GameManager>.Instance;
            num = GlobalVars.SelectedMultiPlayRoomID;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            manager.AudienceRoom = photon.SearchRoom(num);
            if (manager.AudienceRoom == null)
            {
                goto Label_00C5;
            }
            param = JSON_MyPhotonRoomParam.Parse(manager.AudienceRoom.json);
            if (param == null)
            {
                goto Label_0059;
            }
            if (param.audience != null)
            {
                goto Label_0059;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
            goto Label_00C0;
        Label_0059:
            if (manager.AudienceRoom.battle == null)
            {
                goto Label_009F;
            }
            if (param.draft_type != 1)
            {
                goto Label_0092;
            }
            if (manager.AudienceRoom.draft != null)
            {
                goto Label_0092;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x73);
            goto Label_009A;
        Label_0092:
            FlowNode_GameObject.ActivateOutputLinks(this, 12);
        Label_009A:
            goto Label_00C0;
        Label_009F:
            if (param.draft_type != 1)
            {
                goto Label_00B8;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x73);
            goto Label_00C0;
        Label_00B8:
            FlowNode_GameObject.ActivateOutputLinks(this, 15);
        Label_00C0:
            goto Label_00CD;
        Label_00C5:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x10);
        Label_00CD:
            return;
        }

        private void Start()
        {
            if ((this.template == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((this.detailBtn != null) == null)
            {
                goto Label_003F;
            }
            this.detailBtn.get_onClick().AddListener(new UnityAction(this, this.OnClickDetail));
        Label_003F:
            this.RefreshData();
            return;
        }
    }
}

