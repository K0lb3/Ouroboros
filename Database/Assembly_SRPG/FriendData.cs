namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FriendData
    {
        public UnitData Unit;
        public FriendStates State;
        public string UID;
        public string FUID;
        public string PlayerName;
        public int PlayerLevel;
        public long LastLogin;
        public string CreatedAt;
        public bool IsFavorite;
        public string UnitID;
        public int UnitLevel;
        public int UnitRarity;
        public string SelectAward;
        public string Wish;
        public string WishStatus;
        public bool MultiPush;
        public string MultiComment;
        [CompilerGenerated]
        private static Dictionary<string, int> <>f__switch$mapC;

        public FriendData()
        {
            base..ctor();
            return;
        }

        public unsafe void Deserialize(Json_Friend json)
        {
            UnitData data;
            string str;
            Dictionary<string, int> dictionary;
            int num;
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.UID = json.uid;
            this.FUID = json.fuid;
            this.PlayerName = json.name;
            this.PlayerLevel = json.lv;
            this.LastLogin = json.lastlogin;
            this.CreatedAt = json.created_at;
            this.IsFavorite = (json.is_favorite == 0) == 0;
            this.SelectAward = json.award;
            this.Wish = json.wish;
            this.WishStatus = json.status;
            this.MultiPush = (json.is_multi_push != 1) ? 0 : 1;
            this.MultiComment = json.multi_comment;
            if (json.unit == null)
            {
                goto Label_0106;
            }
            this.UnitID = json.unit.iname;
            this.UnitLevel = json.unit.lv;
            this.UnitRarity = json.unit.rare;
            data = new UnitData();
            data.Deserialize(json.unit);
            this.Unit = data;
        Label_0106:
            str = json.type;
            if (str == null)
            {
                goto Label_019B;
            }
            if (<>f__switch$mapC != null)
            {
                goto Label_014E;
            }
            dictionary = new Dictionary<string, int>(3);
            dictionary.Add("friend", 0);
            dictionary.Add("follow", 1);
            dictionary.Add("follower", 2);
            <>f__switch$mapC = dictionary;
        Label_014E:
            if (<>f__switch$mapC.TryGetValue(str, &num) == null)
            {
                goto Label_019B;
            }
            switch (num)
            {
                case 0:
                    goto Label_0177;

                case 1:
                    goto Label_0183;

                case 2:
                    goto Label_018F;
            }
            goto Label_019B;
        Label_0177:
            this.State = 1;
            goto Label_01A7;
        Label_0183:
            this.State = 2;
            goto Label_01A7;
        Label_018F:
            this.State = 3;
            goto Label_01A7;
        Label_019B:
            this.State = 0;
        Label_01A7:
            return;
        }

        public int GetCost()
        {
            int num;
            if (this.Unit != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            num = this.Unit.Lv * MonoSingleton<GameManager>.Instance.MasterParam.FixParam.SupportCost;
            if (this.State == 1)
            {
                goto Label_0043;
            }
            num *= 2;
        Label_0043:
            return num;
        }

        public bool IsFriend()
        {
            return (this.State == 1);
        }

        public EElement UnitElement
        {
            get
            {
                UnitParam param;
                param = MonoSingleton<GameManager>.Instance.GetUnitParam(this.UnitID);
                return ((param == null) ? 0 : param.element);
            }
        }
    }
}

