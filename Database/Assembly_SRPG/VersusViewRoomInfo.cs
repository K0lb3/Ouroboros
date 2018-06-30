namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "Refresh", 0, 1), Pin(2, "Select Reset", 0, 2), Pin(3, "JoinFriendRoom", 0, 3)]
    public class VersusViewRoomInfo : MonoBehaviour, IFlowInterface
    {
        private readonly string FREE_SUFFIX;
        public GameObject Player1P;
        public GameObject Player2P;
        public Image RoomType;
        public Image RoomIcon;
        public Image MapThumnail;
        public Sprite FreeMatch;
        public Sprite TowerMatch;
        public Sprite FreeIcon;
        public Sprite TowerIcon;
        public Text MapName;
        public Text MapDetail;

        public VersusViewRoomInfo()
        {
            this.FREE_SUFFIX = "_free";
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0012;
            }
            this.Refresh();
            goto Label_0024;
        Label_0012:
            if (pinID != 2)
            {
                goto Label_0024;
            }
            MonoSingleton<GameManager>.Instance.AudienceRoom = null;
        Label_0024:
            return;
        }

        public void OnClickRoomInfo()
        {
            MyPhoton.MyRoom room;
            room = DataSource.FindDataOfClass<MyPhoton.MyRoom>(base.get_gameObject(), null);
            if (room == null)
            {
                goto Label_0029;
            }
            MonoSingleton<GameManager>.Instance.AudienceRoom = room;
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "COMFIRM_AUDIENCE");
        Label_0029:
            return;
        }

        public void Refresh()
        {
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param;
            VersusViewPlayerInfo info;
            VersusViewPlayerInfo info2;
            QuestParam param2;
            SpriteSheet sheet;
            room = DataSource.FindDataOfClass<MyPhoton.MyRoom>(base.get_gameObject(), null);
            if (room != null)
            {
                goto Label_0020;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_0020:
            param = JSON_MyPhotonRoomParam.Parse(room.json);
            if (param.players != null)
            {
                goto Label_0044;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_0044:
            base.get_gameObject().SetActive(1);
            if ((this.Player1P != null) == null)
            {
                goto Label_00A0;
            }
            if (((int) param.players.Length) <= 0)
            {
                goto Label_0082;
            }
            DataSource.Bind<JSON_MyPhotonPlayerParam>(this.Player1P, param.players[0]);
        Label_0082:
            info = this.Player1P.GetComponent<VersusViewPlayerInfo>();
            if ((info != null) == null)
            {
                goto Label_00A0;
            }
            info.Refresh();
        Label_00A0:
            if ((this.Player2P != null) == null)
            {
                goto Label_0101;
            }
            if (((int) param.players.Length) <= 1)
            {
                goto Label_00D7;
            }
            DataSource.Bind<JSON_MyPhotonPlayerParam>(this.Player2P, param.players[1]);
            goto Label_00E3;
        Label_00D7:
            DataSource.Bind<JSON_MyPhotonPlayerParam>(this.Player2P, null);
        Label_00E3:
            info2 = this.Player2P.GetComponent<VersusViewPlayerInfo>();
            if ((info2 != null) == null)
            {
                goto Label_0101;
            }
            info2.Refresh();
        Label_0101:
            if ((this.RoomType != null) == null)
            {
                goto Label_0150;
            }
            if (room.name.IndexOf(this.FREE_SUFFIX) == -1)
            {
                goto Label_013F;
            }
            this.RoomType.set_sprite(this.FreeMatch);
            goto Label_0150;
        Label_013F:
            this.RoomType.set_sprite(this.TowerMatch);
        Label_0150:
            if ((this.RoomIcon != null) == null)
            {
                goto Label_019F;
            }
            if (room.name.IndexOf(this.FREE_SUFFIX) == -1)
            {
                goto Label_018E;
            }
            this.RoomIcon.set_sprite(this.FreeIcon);
            goto Label_019F;
        Label_018E:
            this.RoomIcon.set_sprite(this.TowerIcon);
        Label_019F:
            param2 = MonoSingleton<GameManager>.Instance.FindQuest(param.iname);
            if (param2 == null)
            {
                goto Label_0241;
            }
            if ((this.MapThumnail != null) == null)
            {
                goto Label_01FB;
            }
            sheet = AssetManager.Load<SpriteSheet>("pvp/pvp_map");
            if ((sheet != null) == null)
            {
                goto Label_01FB;
            }
            this.MapThumnail.set_sprite(sheet.GetSprite(param2.VersusThumnail));
        Label_01FB:
            if ((this.MapName != null) == null)
            {
                goto Label_021E;
            }
            this.MapName.set_text(param2.name);
        Label_021E:
            if ((this.MapDetail != null) == null)
            {
                goto Label_0241;
            }
            this.MapDetail.set_text(param2.expr);
        Label_0241:
            return;
        }

        private void Start()
        {
        }
    }
}

