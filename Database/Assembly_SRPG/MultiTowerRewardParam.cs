namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class MultiTowerRewardParam
    {
        public string iname;
        public MultiTowerRewardItem[] mReward;
        [CompilerGenerated]
        private static Func<MultiTowerRewardItem, int> <>f__am$cache2;

        public MultiTowerRewardParam()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static int <GetReward>m__25E(MultiTowerRewardItem data)
        {
            return data.round_ed;
        }

        public void Deserialize(JSON_MultiTowerRewardParam json)
        {
            int num;
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.iname = json.iname;
            if (json.rewards == null)
            {
                goto Label_0071;
            }
            this.mReward = new MultiTowerRewardItem[(int) json.rewards.Length];
            num = 0;
            goto Label_0063;
        Label_003D:
            this.mReward[num] = new MultiTowerRewardItem();
            this.mReward[num].Deserialize(json.rewards[num]);
            num += 1;
        Label_0063:
            if (num < ((int) json.rewards.Length))
            {
                goto Label_003D;
            }
        Label_0071:
            return;
        }

        public List<MultiTowerRewardItem> GetReward(int round)
        {
            List<MultiTowerRewardItem> list;
            int num;
            int num2;
            int num3;
            list = new List<MultiTowerRewardItem>();
            if (this.mReward == null)
            {
                goto Label_009B;
            }
            if (<>f__am$cache2 != null)
            {
                goto Label_002F;
            }
            <>f__am$cache2 = new Func<MultiTowerRewardItem, int>(MultiTowerRewardParam.<GetReward>m__25E);
        Label_002F:
            num = Enumerable.Max(Enumerable.Select<MultiTowerRewardItem, int>(this.mReward, <>f__am$cache2));
            num2 = (round < num) ? round : num;
            num3 = 0;
            goto Label_008D;
        Label_0055:
            if (this.mReward[num3].round_st > num2)
            {
                goto Label_0089;
            }
            if (this.mReward[num3].round_ed < num2)
            {
                goto Label_0089;
            }
            list.Add(this.mReward[num3]);
        Label_0089:
            num3 += 1;
        Label_008D:
            if (num3 < ((int) this.mReward.Length))
            {
                goto Label_0055;
            }
        Label_009B:
            return list;
        }
    }
}

