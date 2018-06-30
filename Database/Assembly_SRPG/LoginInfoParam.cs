namespace SRPG
{
    using System;

    [Serializable]
    public class LoginInfoParam
    {
        public string iname;
        public string path;
        public SelectScene scene;
        public long begin_at;
        public long end_at;
        public DispConditions conditions;
        public int conditions_value;

        public LoginInfoParam()
        {
            base..ctor();
            return;
        }

        private bool CheckConditions(int player_level, bool is_beginner)
        {
            DispConditions conditions;
            switch (this.conditions)
            {
                case 0:
                    goto Label_0066;

                case 1:
                    goto Label_0026;

                case 2:
                    goto Label_0039;

                case 3:
                    goto Label_004C;

                case 4:
                    goto Label_0059;
            }
            goto Label_0066;
        Label_0026:
            if (player_level < this.conditions_value)
            {
                goto Label_0068;
            }
            return 1;
            goto Label_0068;
        Label_0039:
            if (player_level > this.conditions_value)
            {
                goto Label_0068;
            }
            return 1;
            goto Label_0068;
        Label_004C:
            if (is_beginner == null)
            {
                goto Label_0068;
            }
            return 1;
            goto Label_0068;
        Label_0059:
            if (is_beginner != null)
            {
                goto Label_0068;
            }
            return 1;
            goto Label_0068;
        Label_0066:
            return 1;
        Label_0068:
            return 0;
        }

        public unsafe bool Deserialize(JSON_LoginInfoParam json)
        {
            DateTime time;
            DateTime time2;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.path = json.path;
            this.scene = (byte) json.scene;
            time = DateTime.MinValue;
            time2 = DateTime.MaxValue;
            if (string.IsNullOrEmpty(json.begin_at) != null)
            {
                goto Label_0057;
            }
            DateTime.TryParse(json.begin_at, &time);
        Label_0057:
            if (string.IsNullOrEmpty(json.end_at) != null)
            {
                goto Label_0075;
            }
            DateTime.TryParse(json.end_at, &time2);
        Label_0075:
            this.begin_at = TimeManager.FromDateTime(time);
            this.end_at = TimeManager.FromDateTime(time2);
            this.conditions = json.conditions;
            this.conditions_value = json.conditions_value;
            return 1;
        }

        public bool IsDisplayable(DateTime server_time, int player_level, bool is_beginner)
        {
            long num;
            num = TimeManager.FromDateTime(server_time);
            if (this.begin_at > num)
            {
                goto Label_001F;
            }
            if (num < this.end_at)
            {
                goto Label_0021;
            }
        Label_001F:
            return 0;
        Label_0021:
            if (this.CheckConditions(player_level, is_beginner) != null)
            {
                goto Label_0030;
            }
            return 0;
        Label_0030:
            return 1;
        }

        public enum DispConditions
        {
            None,
            PlayerLvMore,
            PlayerLvLess,
            Beginner,
            NotBeginner
        }

        public enum SelectScene : byte
        {
            None = 0,
            Gacha = 1,
            LimitedShop = 2,
            EventQuest = 3,
            TowerQuest = 4,
            BuyShop = 5
        }
    }
}

