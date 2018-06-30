namespace SRPG
{
    using System;
    using UnityEngine;

    public class Json_MyPhotonPlayerBinaryParam
    {
        public int playerID;
        public int playerIndex;
        public string playerName;
        public int playerLevel;
        public string FUID;
        public string UID;
        public int totalAtk;
        public int totalStatus;
        public int rankpoint;
        public string award;
        public int state;
        public int rankmatch_score;
        public string support_unit;
        public int draft_id;
        public UnitDataElem[] units;
        public int leaderID;

        public Json_MyPhotonPlayerBinaryParam()
        {
            this.playerName = string.Empty;
            this.FUID = string.Empty;
            this.UID = string.Empty;
            this.award = string.Empty;
            this.state = 1;
            this.support_unit = string.Empty;
            base..ctor();
            return;
        }

        public static bool IsEqual(Json_MyPhotonPlayerBinaryParam data0, Json_MyPhotonPlayerBinaryParam data1)
        {
            bool flag;
            int num;
            string str;
            string str2;
            flag = 1;
            flag &= data0.playerID == data1.playerID;
            flag &= data0.playerIndex == data1.playerIndex;
            flag &= data0.playerName == data1.playerName;
            flag &= data0.playerLevel == data1.playerLevel;
            flag &= data0.FUID == data1.FUID;
            flag &= data0.UID == data1.UID;
            flag &= data0.totalAtk == data1.totalAtk;
            flag &= data0.totalStatus == data1.totalStatus;
            flag &= data0.rankpoint == data1.rankpoint;
            flag &= data0.award == data1.award;
            flag &= data0.state == data1.state;
            flag &= data0.rankmatch_score == data1.rankmatch_score;
            flag &= data0.support_unit == data1.support_unit;
            flag &= data0.draft_id == data1.draft_id;
            if (data0.units == null)
            {
                goto Label_01B1;
            }
            if (data1.units == null)
            {
                goto Label_01B1;
            }
            if (((int) data0.units.Length) != ((int) data1.units.Length))
            {
                goto Label_01B1;
            }
            num = 0;
            goto Label_01A3;
        Label_0131:
            flag &= data0.units[num].slotID == data1.units[num].slotID;
            flag &= data0.units[num].place == data1.units[num].place;
            str = JsonUtility.ToJson(data0.units[num].unitJson);
            str2 = JsonUtility.ToJson(data1.units[num].unitJson);
            flag &= str.Equals(str2);
            num += 1;
        Label_01A3:
            if (num < ((int) data0.units.Length))
            {
                goto Label_0131;
            }
        Label_01B1:
            return flag;
        }

        public void Set(JSON_MyPhotonPlayerParam param)
        {
            int num;
            this.playerID = param.playerID;
            this.playerIndex = param.playerIndex;
            this.playerName = param.playerName;
            this.playerLevel = param.playerLevel;
            this.FUID = param.FUID;
            this.UID = param.UID;
            this.totalAtk = param.totalAtk;
            this.totalStatus = param.totalStatus;
            this.rankpoint = param.rankpoint;
            this.award = param.award;
            this.state = param.state;
            this.rankmatch_score = param.rankmatch_score;
            this.support_unit = param.support_unit;
            this.draft_id = param.draft_id;
            if (param.units == null)
            {
                goto Label_013A;
            }
            this.units = new UnitDataElem[(int) param.units.Length];
            num = 0;
            goto Label_012C;
        Label_00CD:
            this.units[num] = new UnitDataElem();
            this.units[num].slotID = param.units[num].slotID;
            this.units[num].place = param.units[num].place;
            this.units[num].unitJson = param.units[num].unitJson;
            num += 1;
        Label_012C:
            if (num < ((int) param.units.Length))
            {
                goto Label_00CD;
            }
        Label_013A:
            return;
        }

        public class UnitDataElem
        {
            public int slotID;
            public int place;
            public Json_Unit unitJson;

            public UnitDataElem()
            {
                base..ctor();
                return;
            }
        }
    }
}

