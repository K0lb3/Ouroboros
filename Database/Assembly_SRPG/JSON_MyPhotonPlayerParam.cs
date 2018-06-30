namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [Serializable]
    public class JSON_MyPhotonPlayerParam
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
        public int rankmatch_score;
        public string support_unit;
        public int draft_id;
        public int state;
        public UnitDataElem[] units;
        public int leaderID;
        public int mtChallengeFloor;
        public int mtClearedFloor;

        public JSON_MyPhotonPlayerParam()
        {
            this.playerName = string.Empty;
            this.FUID = string.Empty;
            this.UID = string.Empty;
            this.award = string.Empty;
            this.support_unit = string.Empty;
            base..ctor();
            return;
        }

        public static unsafe JSON_MyPhotonPlayerParam Create(int playerID, int playerIndex)
        {
            JSON_MyPhotonPlayerParam param;
            MyPhoton photon;
            PlayerData data;
            PlayerPartyTypes types;
            PartyData data2;
            List<PartyEditData> list;
            QuestParam param2;
            int num;
            int num2;
            int num3;
            int num4;
            PartyEditData data3;
            UnitData[] dataArray;
            UnitData[] dataArray2;
            int num5;
            int num6;
            long num7;
            int num8;
            int num9;
            int num10;
            bool flag;
            List<UnitDataElem> list2;
            int num11;
            UnitData data4;
            UnitDataElem elem;
            UnitData data5;
            JSON_MyPhotonRoomParam.EType type;
            PlayerPartyTypes types2;
            param = new JSON_MyPhotonPlayerParam();
            if (param != null)
            {
                goto Label_000E;
            }
            return null;
        Label_000E:
            if ((PunMonoSingleton<MyPhoton>.Instance == null) == null)
            {
                goto Label_0022;
            }
            return null;
        Label_0022:
            data = MonoSingleton<GameManager>.Instance.Player;
            param.playerID = playerID;
            param.playerIndex = playerIndex;
            param.playerName = data.Name;
            param.playerLevel = data.Lv;
            param.FUID = data.FUID;
            param.UID = MonoSingleton<GameManager>.Instance.DeviceId;
            param.award = data.SelectedAward;
            types = 2;
            switch (GlobalVars.SelectedMultiPlayRoomType)
            {
                case 0:
                    goto Label_00A0;

                case 1:
                    goto Label_00A7;

                case 2:
                    goto Label_00AE;

                case 3:
                    goto Label_00B5;
            }
            goto Label_00BD;
        Label_00A0:
            types = 2;
            goto Label_00BD;
        Label_00A7:
            types = 7;
            goto Label_00BD;
        Label_00AE:
            types = 8;
            goto Label_00BD;
        Label_00B5:
            types = 10;
        Label_00BD:
            param2 = null;
            num = 1;
            types2 = types;
            switch ((types2 - 7))
            {
                case 0:
                    goto Label_00E4;

                case 1:
                    goto Label_0172;

                case 2:
                    goto Label_0199;

                case 3:
                    goto Label_012B;
            }
            goto Label_0199;
        Label_00E4:
            data2 = data.Partys[types];
            num = data2.MAX_MAINMEMBER;
            list = PartyUtility.LoadTeamPresets(types, &num2, 0);
            if (string.IsNullOrEmpty(GlobalVars.SelectedQuestID) != null)
            {
                goto Label_0208;
            }
            param2 = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            goto Label_0208;
        Label_012B:
            data2 = data.Partys[types];
            num = data2.MAX_MAINMEMBER;
            list = PartyUtility.LoadTeamPresets(types, &num3, 0);
            if (string.IsNullOrEmpty(GlobalVars.SelectedQuestID) != null)
            {
                goto Label_0208;
            }
            param2 = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            goto Label_0208;
        Label_0172:
            data2 = data.Partys[types];
            num = data2.MAX_UNIT;
            list = PartyUtility.LoadTeamPresets(types, &num4, 0);
            goto Label_0208;
        Label_0199:
            data2 = data.Partys[types];
            num = data2.MAX_UNIT;
            if (string.IsNullOrEmpty(GlobalVars.SelectedQuestID) != null)
            {
                goto Label_01E5;
            }
            param2 = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            if (param2 == null)
            {
                goto Label_01E5;
            }
            num = param2.unitNum;
        Label_01E5:
            data3 = new PartyEditData(string.Empty, data2);
            list = new List<PartyEditData>();
            list.Add(data3);
        Label_0208:
            dataArray = new UnitData[num];
            if ((list == null) || (list.Count <= 0))
            {
                goto Label_0272;
            }
            PartyUtility.ResetToDefaultTeamIfNeeded(types, param2, list);
            dataArray2 = list[0].Units;
            num5 = 0;
            goto Label_0257;
        Label_0247:
            dataArray[num5] = dataArray2[num5];
            num5 += 1;
        Label_0257:
            if (num5 >= ((int) dataArray.Length))
            {
                goto Label_02AC;
            }
            if (num5 < ((int) dataArray2.Length))
            {
                goto Label_0247;
            }
            goto Label_02AC;
        Label_0272:
            num6 = 0;
            goto Label_02A1;
        Label_027A:
            num7 = data2.GetUnitUniqueID(num6);
            dataArray[num6] = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(num7);
            num6 += 1;
        Label_02A1:
            if (num6 < ((int) dataArray.Length))
            {
                goto Label_027A;
            }
        Label_02AC:
            num8 = 0;
            num9 = 0;
            num10 = 0;
            flag = GlobalVars.SelectedMultiPlayRoomType == 2;
            param.leaderID = data2.LeaderIndex;
            list2 = new List<UnitDataElem>();
            num11 = 0;
            goto Label_0620;
        Label_02DB:
            data4 = dataArray[num11];
            if (data4 != null)
            {
                goto Label_02EE;
            }
            goto Label_061A;
        Label_02EE:
            elem = new UnitDataElem();
            elem.slotID = num8++;
            if (flag == null)
            {
                goto Label_0317;
            }
            elem.place = -1;
            goto Label_0360;
        Label_0317:
            if (types != 10)
            {
                goto Label_0342;
            }
            elem.place = data.GetVersusPlacement(PlayerPrefsUtility.RANKMATCH_ID_KEY + ((int) num11));
            goto Label_0360;
        Label_0342:
            elem.place = data.GetVersusPlacement(PlayerPrefsUtility.VERSUS_ID_KEY + ((int) num11));
        Label_0360:
            elem.sub = ((num11 < data2.MAX_MAINMEMBER) || (data2.MAX_SUBMEMBER <= 0)) ? 0 : 1;
            elem.unit = data4;
            list2.Add(elem);
            num9 += data4.Status.param.atk;
            num9 += data4.Status.param.mag;
            num10 += (int) (((float) data4.Status.param.hp) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.HP);
            num10 += (int) (((float) data4.Status.param.atk) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Attack);
            num10 += (int) (((float) data4.Status.param.def) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Defense);
            num10 += (int) (((float) data4.Status.param.mag) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.MagAttack);
            num10 += (int) (((float) data4.Status.param.mnd) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.MagDefense);
            num10 += (int) (((float) data4.Status.param.dex) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Dex);
            num10 += (int) (((float) data4.Status.param.spd) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Speed);
            num10 += (int) (((float) data4.Status.param.cri) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Critical);
            num10 += (int) (((float) data4.Status.param.luk) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Luck);
            num10 += (int) (((float) data4.GetCombination()) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Combo);
            num10 += (int) (((float) data4.Status.param.mov) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Move);
            num10 += (int) (((float) data4.Status.param.jmp) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Jump);
        Label_061A:
            num11 += 1;
        Label_0620:
            if (num11 < ((int) dataArray.Length))
            {
                goto Label_02DB;
            }
            param.units = list2.ToArray();
            param.totalAtk = num9;
            param.totalStatus = Mathf.FloorToInt((float) (num10 / list2.Count));
            param.rankpoint = data.VERSUS_POINT;
            param.mtChallengeFloor = MonoSingleton<GameManager>.Instance.GetMTChallengeFloor();
            param.mtClearedFloor = MonoSingleton<GameManager>.Instance.GetMTClearedMaxFloor();
            param.rankmatch_score = MonoSingleton<GameManager>.Instance.Player.RankMatchScore;
            data5 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedSupportUnitUniqueID);
            param.support_unit = data5.Serialize();
            param.draft_id = VersusDraftList.DraftID;
            return param;
        }

        public void CreateJsonUnitData()
        {
            int num;
            string str;
            if (this.units != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_004F;
        Label_0013:
            if (this.units[num].unit == null)
            {
                goto Label_004B;
            }
            str = this.units[num].unit.Serialize();
            this.units[num].unitJson = JsonUtility.FromJson<Json_Unit>(str);
        Label_004B:
            num += 1;
        Label_004F:
            if (num < ((int) this.units.Length))
            {
                goto Label_0013;
            }
            return;
        }

        public SupportData CreateSupportData()
        {
            SupportData data;
            if (this.units == null)
            {
                goto Label_0019;
            }
            if (((int) this.units.Length) > 0)
            {
                goto Label_001B;
            }
        Label_0019:
            return null;
        Label_001B:
            if (this.units[0] == null)
            {
                goto Label_003A;
            }
            if (this.units[0].unit != null)
            {
                goto Label_003C;
            }
        Label_003A:
            return null;
        Label_003C:
            data = new SupportData();
            data.FUID = this.FUID;
            data.Unit = this.units[0].unit;
            data.PlayerName = this.playerName;
            data.PlayerLevel = this.playerLevel;
            data.UnitID = data.Unit.UnitID;
            data.UnitLevel = data.Unit.Lv;
            data.UnitRarity = data.Unit.Rarity;
            data.JobID = data.Unit.CurrentJob.JobID;
            data.LeaderSkillLevel = UnitParam.GetLeaderSkillLevel(data.UnitRarity, data.Unit.AwakeLv);
            return data;
        }

        public static JSON_MyPhotonPlayerParam Parse(string json)
        {
            JSON_MyPhotonPlayerParam param;
            if (string.IsNullOrEmpty(json) == null)
            {
                goto Label_0011;
            }
            return new JSON_MyPhotonPlayerParam();
        Label_0011:
            param = JSONParser.parseJSONObject<JSON_MyPhotonPlayerParam>(json);
            param.SetupUnits();
            return param;
        }

        public string Serialize()
        {
            string str;
            int num;
            str = "{";
            str = (((((((((((((((((str + "\"playerID\":" + ((int) this.playerID)) + ",\"playerIndex\":" + ((int) this.playerIndex)) + ",\"playerName\":\"" + JsonEscape.Escape(this.playerName) + "\"") + ",\"playerLevel\":" + ((int) this.playerLevel)) + ",\"FUID\":\"" + JsonEscape.Escape(this.FUID) + "\"") + ",\"UID\":\"" + JsonEscape.Escape(this.UID) + "\"") + ",\"state\":" + ((int) this.state)) + ",\"leaderID\":" + ((int) this.leaderID)) + ",\"totalAtk\":" + ((int) this.totalAtk)) + ",\"totalStatus\":" + ((int) this.totalStatus)) + ",\"rankpoint\":" + ((int) this.rankpoint)) + ",\"mtChallengeFloor\":" + ((int) this.mtChallengeFloor)) + ",\"mtClearedFloor\":" + ((int) this.mtClearedFloor)) + ",\"award\":\"" + JsonEscape.Escape(this.award) + "\"") + ",\"rankmatch_score\":" + ((int) this.rankmatch_score)) + ",\"support_unit\":\"" + JsonEscape.Escape(this.support_unit) + "\"") + ",\"draft_id\":" + ((int) this.draft_id)) + ",\"units\":[";
            if (this.units == null)
            {
                goto Label_0206;
            }
            num = 0;
            goto Label_01F8;
        Label_01C4:
            str = (str + ((num != null) ? "," : string.Empty)) + this.units[num].Serialize();
            num += 1;
        Label_01F8:
            if (num < ((int) this.units.Length))
            {
                goto Label_01C4;
            }
        Label_0206:
            return ((str + "]") + "}");
        }

        public void SetupUnits()
        {
            int num;
            UnitData data;
            if (this.units != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_0055;
        Label_0013:
            if (this.units[num].unitJson != null)
            {
                goto Label_002A;
            }
            goto Label_0051;
        Label_002A:
            data = new UnitData();
            data.Deserialize(this.units[num].unitJson);
            this.units[num].unit = data;
        Label_0051:
            num += 1;
        Label_0055:
            if (num < ((int) this.units.Length))
            {
                goto Label_0013;
            }
            return;
        }

        public void UpdateMultiTowerPlacement(bool isDefault)
        {
            int num;
            int num2;
            if (GlobalVars.SelectedMultiPlayRoomType != 2)
            {
                goto Label_00C2;
            }
            if (isDefault == null)
            {
                goto Label_0079;
            }
            num = 0;
            goto Label_0066;
        Label_0018:
            if (this.units[num] == null)
            {
                goto Label_0062;
            }
            this.units[num].place = ((this.playerIndex - 1) * 2) + num;
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.MULTITW_ID_KEY + ((int) num), this.units[num].place, 1);
        Label_0062:
            num += 1;
        Label_0066:
            if (num < ((int) this.units.Length))
            {
                goto Label_0018;
            }
            goto Label_00C2;
        Label_0079:
            num2 = 0;
            goto Label_00B4;
        Label_0080:
            if (this.units[num2] == null)
            {
                goto Label_00B0;
            }
            this.units[num2].place = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.MULTITW_ID_KEY + ((int) num2), 0);
        Label_00B0:
            num2 += 1;
        Label_00B4:
            if (num2 < ((int) this.units.Length))
            {
                goto Label_0080;
            }
        Label_00C2:
            return;
        }

        public enum EState
        {
            NOP,
            READY,
            START,
            START_CONFIRM,
            EDIT,
            FLOOR_SELECT,
            NUM
        }

        [Serializable]
        public class UnitDataElem
        {
            public int slotID;
            public int place;
            public int sub;
            public Json_Unit unitJson;
            public UnitData unit;

            public UnitDataElem()
            {
                base..ctor();
                return;
            }

            public string Serialize()
            {
                string str;
                str = "{";
                str = ((str + "\"slotID\":" + ((int) this.slotID)) + ",\"place\":" + ((int) this.place)) + ",\"sub\":" + ((int) this.sub);
                if (this.unit == null)
                {
                    goto Label_0072;
                }
                str = str + ",\"unitJson\":" + this.unit.Serialize2();
                goto Label_00B0;
            Label_0072:
                if (this.unitJson == null)
                {
                    goto Label_00B0;
                }
                this.unit = new UnitData();
                this.unit.Deserialize(this.unitJson);
                str = str + ",\"unitJson\":" + this.unit.Serialize2();
            Label_00B0:
                return (str + "}");
            }
        }
    }
}

