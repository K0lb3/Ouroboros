namespace SRPG
{
    using System;

    public class MultiTowerRewardItem
    {
        public int round_st;
        public int round_ed;
        public RewardType type;
        public string itemname;
        public int num;

        public MultiTowerRewardItem()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_MultiTowerRewardItem json)
        {
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.round_st = json.round_st;
            this.round_ed = json.round_ed;
            this.itemname = json.itemname;
            this.num = json.num;
            this.type = (byte) json.type;
            return;
        }

        public enum RewardType : byte
        {
            None = 0,
            Item = 1,
            Coin = 2,
            Artifact = 3,
            Award = 4,
            Unit = 5,
            Gold = 6
        }
    }
}

