namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class VersusAudienceFriendRoom : MonoBehaviour
    {
        private readonly float UPDATE_INTERVAL;
        public GameObject RoomObj;
        public Text AudienceTxt;
        private float mUpdateTime;

        public VersusAudienceFriendRoom()
        {
            this.UPDATE_INTERVAL = 2f;
            base..ctor();
            return;
        }

        public void OnClickBack()
        {
            if (Network.IsConnecting == null)
            {
                goto Label_000F;
            }
            Network.Abort();
        Label_000F:
            return;
        }

        private unsafe void Refresh(MyPhoton.MyRoom room)
        {
            GameManager manager;
            VersusViewRoomInfo info;
            int num;
            int num2;
            string str;
            manager = MonoSingleton<GameManager>.Instance;
            if (((this.RoomObj != null) == null) || (manager.AudienceRoom == null))
            {
                goto Label_00BC;
            }
            DataSource.Bind<MyPhoton.MyRoom>(this.RoomObj, (room == null) ? manager.AudienceRoom : room);
            info = this.RoomObj.GetComponent<VersusViewRoomInfo>();
            if ((info != null) == null)
            {
                goto Label_005D;
            }
            info.Refresh();
        Label_005D:
            if ((this.AudienceTxt != null) == null)
            {
                goto Label_00BC;
            }
            num = manager.AudienceRoom.audience;
            num2 = manager.AudienceRoom.audienceMax;
            str = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_AUDIENCE_NUM"), GameUtility.HalfNum2FullNum(&num.ToString()), GameUtility.HalfNum2FullNum(&num2.ToString()));
            this.AudienceTxt.set_text(str);
        Label_00BC:
            return;
        }

        private void Start()
        {
            this.Refresh(null);
            return;
        }

        private void Update()
        {
            GameManager manager;
            MyPhoton photon;
            JSON_MyPhotonRoomParam param;
            MyPhoton.MyRoom room;
            this.mUpdateTime -= Time.get_deltaTime();
            if (this.mUpdateTime <= 0f)
            {
                goto Label_0023;
            }
            return;
        Label_0023:
            manager = MonoSingleton<GameManager>.Instance;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if ((photon != null) == null)
            {
                goto Label_019D;
            }
            if (manager.AudienceRoom == null)
            {
                goto Label_019D;
            }
            if (manager.AudienceRoom.battle == null)
            {
                goto Label_0066;
            }
            if (manager.AudienceRoom.draft != null)
            {
                goto Label_019D;
            }
        Label_0066:
            if (photon.IsConnected() != null)
            {
                goto Label_0094;
            }
            if (manager.AudienceRoom.battle != null)
            {
                goto Label_0094;
            }
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "FORCE_LEAVE");
            manager.AudienceRoom = null;
            return;
        Label_0094:
            if (photon.IsRoomListUpdated == null)
            {
                goto Label_019D;
            }
            param = JSON_MyPhotonRoomParam.Parse(manager.AudienceRoom.json);
            if (param == null)
            {
                goto Label_018A;
            }
            room = photon.SearchRoom(param.roomid);
            if (room == null)
            {
                goto Label_017F;
            }
            if (room.json.Equals(manager.AudienceRoom.json) != null)
            {
                goto Label_011B;
            }
            this.Refresh(room);
            param = JSON_MyPhotonRoomParam.Parse(room.json);
            if (param == null)
            {
                goto Label_011B;
            }
            if (param.audience != null)
            {
                goto Label_011B;
            }
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "AUDIENCE_DISABLE");
            manager.AudienceRoom = null;
            return;
        Label_011B:
            manager.AudienceRoom = room;
            if (param.draft_type != 1)
            {
                goto Label_0164;
            }
            if (room.draft == null)
            {
                goto Label_0149;
            }
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "START_AUDIENCE");
            goto Label_015F;
        Label_0149:
            if (room.battle == null)
            {
                goto Label_018A;
            }
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "WAIT_AUDIENCE");
        Label_015F:
            goto Label_017A;
        Label_0164:
            if (room.battle == null)
            {
                goto Label_018A;
            }
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "START_AUDIENCE");
        Label_017A:
            goto Label_018A;
        Label_017F:
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "DISBANDED");
        Label_018A:
            photon.IsRoomListUpdated = 0;
            this.mUpdateTime = this.UPDATE_INTERVAL;
        Label_019D:
            return;
        }
    }
}

