namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class TowerRewardParam
    {
        public string iname;
        protected List<TowerRewardItem> mTowerRewardItems;

        public TowerRewardParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_TowerRewardParam json)
        {
            int num;
            TowerRewardItem item;
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.iname = json.iname;
            if (json.rewards == null)
            {
                goto Label_0067;
            }
            this.mTowerRewardItems = new List<TowerRewardItem>();
            num = 0;
            goto Label_0059;
        Label_0035:
            item = new TowerRewardItem();
            item.Deserialize(json.rewards[num]);
            this.mTowerRewardItems.Add(item);
            num += 1;
        Label_0059:
            if (num < ((int) json.rewards.Length))
            {
                goto Label_0035;
            }
        Label_0067:
            return;
        }

        public virtual List<TowerRewardItem> GetTowerRewardItem()
        {
            return this.mTowerRewardItems;
        }
    }
}

