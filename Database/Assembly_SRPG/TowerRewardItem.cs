namespace SRPG
{
    using System;

    public class TowerRewardItem
    {
        public string iname;
        public int num;
        public RewardType type;
        public bool visible;
        public bool is_new;

        public TowerRewardItem()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_TowerRewardItem json)
        {
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.iname = json.iname;
            this.type = json.type;
            this.num = json.num;
            this.visible = json.visible == 1;
            return;
        }

        public enum RewardType : byte
        {
            Item = 0,
            Gold = 1,
            Coin = 2,
            ArenaCoin = 3,
            MultiCoin = 4,
            KakeraCoin = 5,
            Artifact = 6
        }
    }
}

