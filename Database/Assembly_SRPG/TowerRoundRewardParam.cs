namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    public class TowerRoundRewardParam : TowerRewardParam
    {
        public List<byte> mRoundList;

        public TowerRoundRewardParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_TowerRoundRewardParam json)
        {
            int num;
            TowerRewardItem item;
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            base.iname = json.iname;
            this.mRoundList = new List<byte>();
            if (json.rewards == null)
            {
                goto Label_008A;
            }
            base.mTowerRewardItems = new List<TowerRewardItem>();
            num = 0;
            goto Label_007C;
        Label_0040:
            item = new TowerRewardItem();
            item.Deserialize(json.rewards[num]);
            base.mTowerRewardItems.Add(item);
            this.mRoundList.Add(json.rewards[num].round_num);
            num += 1;
        Label_007C:
            if (num < ((int) json.rewards.Length))
            {
                goto Label_0040;
            }
        Label_008A:
            return;
        }

        public override List<TowerRewardItem> GetTowerRewardItem()
        {
            TowerResuponse resuponse;
            byte num;
            List<TowerRewardItem> list;
            int num2;
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            num = resuponse.round;
            list = new List<TowerRewardItem>();
            num2 = 0;
            goto Label_0041;
        Label_001F:
            if (resuponse.round < num)
            {
                goto Label_003D;
            }
            list.Add(base.mTowerRewardItems[num2]);
        Label_003D:
            num2 += 1;
        Label_0041:
            if (num2 < this.mRoundList.Count)
            {
                goto Label_001F;
            }
            return list;
        }
    }
}

