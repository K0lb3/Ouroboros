namespace SRPG
{
    using System;

    public class ChatPlayerData
    {
        public string name;
        public int exp;
        public UnitData unit;
        public long lastlogin;
        public byte is_friend;
        public byte is_favorite;
        public string fuid;
        public int lv;
        public string award;

        public ChatPlayerData()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_ChatPlayerData json)
        {
            UnitData data;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.name = json.name;
            this.exp = json.exp;
            this.lastlogin = json.lastlogin;
            this.fuid = json.fuid;
            this.is_friend = json.is_friend;
            this.is_favorite = json.is_favorite;
            this.lv = PlayerData.CalcLevelFromExp(this.exp);
            this.award = json.award;
            if (json.unit == null)
            {
                goto Label_0090;
            }
            data = new UnitData();
            data.Deserialize(json.unit);
            this.unit = data;
        Label_0090:
            return;
        }

        public FriendData ToFriendData()
        {
            FriendData data;
            data = new FriendData();
            data.FUID = this.fuid;
            data.PlayerName = this.name;
            data.PlayerLevel = this.lv;
            data.LastLogin = this.lastlogin;
            data.Unit = this.unit;
            data.SelectAward = this.award;
            data.IsFavorite = this.IsFavorite;
            if (this.unit == null)
            {
                goto Label_0098;
            }
            data.UnitID = this.unit.UnitID;
            data.UnitLevel = this.unit.Lv;
            data.UnitRarity = this.unit.Rarity;
        Label_0098:
            return data;
        }

        public bool IsFriend
        {
            get
            {
                return ((this.is_friend == 0) == 0);
            }
        }

        public bool IsFavorite
        {
            get
            {
                return ((this.is_favorite == 0) == 0);
            }
        }
    }
}

