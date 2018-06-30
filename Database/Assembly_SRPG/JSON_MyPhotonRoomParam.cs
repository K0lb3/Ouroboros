namespace SRPG
{
    using GR;
    using System;

    public class JSON_MyPhotonRoomParam
    {
        public static readonly int LINE_PARAM_ENCODE_KEY;
        public string creatorName;
        public int creatorLV;
        public string creatorFUID;
        public string comment;
        public string passCode;
        public int type;
        public int isLINE;
        public string iname;
        public int started;
        public int roomid;
        public int audience;
        public int audienceNum;
        public int unitlv;
        public int challegedMTFloor;
        public int vsmode;
        public int draft_type;
        public JSON_MyPhotonPlayerParam[] players;

        static JSON_MyPhotonRoomParam()
        {
            LINE_PARAM_ENCODE_KEY = 0x1a85;
            return;
        }

        public JSON_MyPhotonRoomParam()
        {
            this.creatorName = string.Empty;
            this.creatorLV = 1;
            this.creatorFUID = string.Empty;
            this.comment = string.Empty;
            this.passCode = string.Empty;
            this.iname = string.Empty;
            base..ctor();
            return;
        }

        public static string GetMyCreatorFUID()
        {
            return ((Network.Mode != 1) ? MonoSingleton<GameManager>.Instance.Player.FUID : MonoSingleton<GameManager>.Instance.UdId);
        }

        public JSON_MyPhotonPlayerParam GetOwner()
        {
            JSON_MyPhotonPlayerParam param;
            JSON_MyPhotonPlayerParam param2;
            JSON_MyPhotonPlayerParam[] paramArray;
            int num;
            if (this.players != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            param = null;
            paramArray = this.players;
            num = 0;
            goto Label_004F;
        Label_001D:
            param2 = paramArray[num];
            if (param2.playerIndex > 0)
            {
                goto Label_0032;
            }
            goto Label_004B;
        Label_0032:
            if (param == null)
            {
                goto Label_0049;
            }
            if (param2.playerIndex >= param.playerIndex)
            {
                goto Label_004B;
            }
        Label_0049:
            param = param2;
        Label_004B:
            num += 1;
        Label_004F:
            if (num < ((int) paramArray.Length))
            {
                goto Label_001D;
            }
            return param;
        }

        public int GetOwnerLV()
        {
            JSON_MyPhotonPlayerParam param;
            param = this.GetOwner();
            return ((param != null) ? param.playerLevel : this.creatorLV);
        }

        public string GetOwnerName()
        {
            JSON_MyPhotonPlayerParam param;
            param = this.GetOwner();
            return ((param != null) ? param.playerName : this.creatorName);
        }

        public static int GetTotalUnitNum(QuestParam param)
        {
            if (param != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            return (param.unitNum * param.playerNum);
        }

        public int GetUnitSlotNum()
        {
            return this.GetUnitSlotNum(PunMonoSingleton<MyPhoton>.Instance.MyPlayerIndex);
        }

        public int GetUnitSlotNum(int playerIndex)
        {
            QuestParam param;
            param = MonoSingleton<GameManager>.Instance.FindQuest(this.iname);
            if (param != null)
            {
                goto Label_0019;
            }
            return 0;
        Label_0019:
            return param.unitNum;
        }

        public static JSON_MyPhotonRoomParam Parse(string json)
        {
            if (json == null)
            {
                goto Label_0012;
            }
            if (json.Length > 0)
            {
                goto Label_0018;
            }
        Label_0012:
            return new JSON_MyPhotonRoomParam();
        Label_0018:
            return JSONParser.parseJSONObject<JSON_MyPhotonRoomParam>(json);
        }

        public string Serialize()
        {
            string str;
            int num;
            str = "{";
            str = ((((((((((((((((str + "\"creatorName\":\"" + JsonEscape.Escape(this.creatorName) + "\"") + ",\"creatorLV\":" + ((int) this.creatorLV)) + ",\"creatorFUID\":\"" + JsonEscape.Escape(this.creatorFUID) + "\"") + ",\"comment\":\"" + JsonEscape.Escape(this.comment) + "\"") + ",\"passCode\":\"" + JsonEscape.Escape(this.passCode) + "\"") + ",\"iname\":\"" + JsonEscape.Escape(this.iname) + "\"") + ",\"type\":" + ((int) this.type)) + ",\"isLINE\":" + ((int) this.isLINE)) + ",\"started\":" + ((int) this.started)) + ",\"roomid\":" + ((int) this.roomid)) + ",\"audience\":" + ((int) this.audience)) + ",\"audienceNum\":" + ((int) this.audienceNum)) + ",\"unitlv\":" + ((int) this.unitlv)) + ",\"challegedMTFloor\":" + ((int) GlobalVars.SelectedMultiTowerFloor)) + ",\"vsmode\":" + ((int) this.vsmode)) + ",\"draft_type\":" + ((int) this.draft_type)) + ",\"players\":[";
            if (this.players == null)
            {
                goto Label_01EF;
            }
            num = 0;
            goto Label_01E1;
        Label_01AC:
            str = (str + ((num > 0) ? "," : string.Empty)) + this.players[num].Serialize();
            num += 1;
        Label_01E1:
            if (num < ((int) this.players.Length))
            {
                goto Label_01AC;
            }
        Label_01EF:
            return ((str + "]") + "}");
        }

        public enum EType
        {
            RAID,
            VERSUS,
            TOWER,
            RANKMATCH,
            NUM
        }
    }
}

