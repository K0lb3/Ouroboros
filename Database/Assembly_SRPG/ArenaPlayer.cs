namespace SRPG
{
    using System;
    using System.Text;

    public class ArenaPlayer
    {
        public UnitData[] Unit;
        public string result;
        public string FUID;
        public string PlayerName;
        public int PlayerLevel;
        public int ArenaRank;
        public int TotalAtk;
        public DateTime battle_at;
        public string SelectAward;

        public ArenaPlayer()
        {
            this.Unit = new UnitData[3];
            this.battle_at = DateTime.MinValue;
            base..ctor();
            return;
        }

        public int BattleCount()
        {
            return 10;
        }

        public void Deserialize(Json_ArenaPlayer json)
        {
            int num;
            UnitData data;
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.FUID = json.fuid;
            this.PlayerName = json.name;
            this.PlayerLevel = json.lv;
            this.ArenaRank = json.rank;
            this.TotalAtk = 0;
            this.result = json.result;
            this.battle_at = GameUtility.UnixtimeToLocalTime(json.btl_at);
            this.SelectAward = json.award;
            if (json.units == null)
            {
                goto Label_0122;
            }
            num = 0;
            goto Label_0106;
        Label_007E:
            data = new UnitData();
            if (json.units[num] != null)
            {
                goto Label_00A0;
            }
            DebugUtility.LogWarning("Unit is NULL");
            goto Label_0102;
        Label_00A0:
            data.Deserialize(json.units[num]);
            data.SetJob(4);
            this.Unit[num] = data;
            this.TotalAtk += data.Status.param.atk;
            this.TotalAtk += data.Status.param.mag;
        Label_0102:
            num += 1;
        Label_0106:
            if (num >= ((int) json.units.Length))
            {
                goto Label_0122;
            }
            if (num < ((int) this.Unit.Length))
            {
                goto Label_007E;
            }
        Label_0122:
            return;
        }

        public void Serialize(StringBuilder sb)
        {
            int num;
            int num2;
            sb.Append("{\"result\":\"");
            sb.Append(this.result);
            sb.Append("\",\"fuid\":\"");
            sb.Append(this.FUID);
            sb.Append("\",\"lv\":");
            sb.Append(this.PlayerLevel);
            sb.Append(",\"rank\":");
            sb.Append(this.ArenaRank);
            sb.Append(",\"units\":[");
            num = 0;
            num2 = 0;
            goto Label_00B2;
        Label_0079:
            if (this.Unit[num2] == null)
            {
                goto Label_00AE;
            }
            if (num <= 0)
            {
                goto Label_0096;
            }
            sb.Append(0x2c);
        Label_0096:
            sb.Append(this.Unit[num2].Serialize());
            num += 1;
        Label_00AE:
            num2 += 1;
        Label_00B2:
            if (num2 < ((int) this.Unit.Length))
            {
                goto Label_0079;
            }
            sb.Append("]}");
            return;
        }

        public int WinCount()
        {
            return 5;
        }
    }
}

