namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class RewardData
    {
        public int Exp;
        public int Gold;
        public int Coin;
        public int ArenaMedal;
        public int MultiCoin;
        public int KakeraCoin;
        public int Stamina;
        public List<ItemData> Items;
        public List<ArtifactRewardData> Artifacts;
        public List<int> ItemsBeforeAmount;
        public bool IsOverLimit;
        public Dictionary<string, GiftRecieveItemData> GiftRecieveItemDataDic;

        public RewardData()
        {
            this.Items = new List<ItemData>();
            this.Artifacts = new List<ArtifactRewardData>();
            this.ItemsBeforeAmount = new List<int>();
            this.GiftRecieveItemDataDic = new Dictionary<string, GiftRecieveItemData>();
            base..ctor();
            return;
        }

        public unsafe RewardData(TrophyParam trophy)
        {
            GameManager manager;
            TrophyParam.RewardItem item;
            TrophyParam.RewardItem[] itemArray;
            int num;
            ItemData data;
            int num2;
            UnitData data2;
            TrophyParam.RewardItem item2;
            TrophyParam.RewardItem[] itemArray2;
            int num3;
            ArtifactRewardData data3;
            TrophyParam.RewardItem item3;
            TrophyParam.RewardItem[] itemArray3;
            int num4;
            ConceptCardParam param;
            <RewardData>c__AnonStorey39C storeyc;
            this.Items = new List<ItemData>();
            this.Artifacts = new List<ArtifactRewardData>();
            this.ItemsBeforeAmount = new List<int>();
            this.GiftRecieveItemDataDic = new Dictionary<string, GiftRecieveItemData>();
            base..ctor();
            this.Exp = trophy.Exp;
            this.Coin = trophy.Coin;
            this.Gold = trophy.Gold;
            this.Stamina = trophy.Stamina;
            manager = MonoSingleton<GameManager>.Instance;
            itemArray = trophy.Items;
            num = 0;
            goto Label_0163;
        Label_0076:
            item = *(&(itemArray[num]));
            storeyc = new <RewardData>c__AnonStorey39C();
            storeyc.itemData = new ItemData();
            if (storeyc.itemData.Setup(0L, &item.iname, &item.Num) == null)
            {
                goto Label_015F;
            }
            this.Items.Add(storeyc.itemData);
            if (storeyc.itemData.Param.type == 0x10)
            {
                goto Label_0127;
            }
            data = manager.Player.FindItemDataByItemID(storeyc.itemData.Param.iname);
            num2 = (data == null) ? 0 : data.Num;
            this.ItemsBeforeAmount.Add(num2);
            goto Label_015F;
        Label_0127:
            data2 = manager.Player.Units.Find(new Predicate<UnitData>(storeyc.<>m__3F3));
            this.ItemsBeforeAmount.Add((data2 == null) ? 0 : 1);
        Label_015F:
            num += 1;
        Label_0163:
            if (num < ((int) itemArray.Length))
            {
                goto Label_0076;
            }
            itemArray2 = trophy.Artifacts;
            num3 = 0;
            goto Label_01CD;
        Label_017C:
            item2 = *(&(itemArray2[num3]));
            data3 = new ArtifactRewardData();
            data3.ArtifactParam = manager.MasterParam.GetArtifactParam(&item2.iname);
            data3.Num = &item2.Num;
            this.Artifacts.Add(data3);
            num3 += 1;
        Label_01CD:
            if (num3 < ((int) itemArray2.Length))
            {
                goto Label_017C;
            }
            itemArray3 = trophy.ConceptCards;
            num4 = 0;
            goto Label_0221;
        Label_01E8:
            item3 = *(&(itemArray3[num4]));
            param = manager.MasterParam.GetConceptCardParam(&item3.iname);
            this.AddReward(param, &item3.Num);
            num4 += 1;
        Label_0221:
            if (num4 < ((int) itemArray3.Length))
            {
                goto Label_01E8;
            }
            return;
        }

        public void AddReward(GiftRecieveItemData giftRecieveItemData)
        {
            GiftRecieveItemData local1;
            if (giftRecieveItemData != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (this.GiftRecieveItemDataDic.ContainsKey(giftRecieveItemData.iname) == null)
            {
                goto Label_0045;
            }
            local1 = this.GiftRecieveItemDataDic[giftRecieveItemData.iname];
            local1.num += giftRecieveItemData.num;
            goto Label_0057;
        Label_0045:
            this.GiftRecieveItemDataDic.Add(giftRecieveItemData.iname, giftRecieveItemData);
        Label_0057:
            return;
        }

        public void AddReward(ArtifactParam param, int num)
        {
            if (param != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.AddReward(param.iname, 0x40L, param.rareini, num);
            return;
        }

        public void AddReward(ConceptCardParam param, int num)
        {
            if (param != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.AddReward(param.iname, 0x1000L, param.rare, num);
            return;
        }

        public void AddReward(ItemParam param, int num)
        {
            if (param != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (param.type != 0x10)
            {
                goto Label_0032;
            }
            this.AddReward(param.iname, 0x80L, param.rare, num);
            goto Label_0072;
        Label_0032:
            if (param.type != 0x13)
            {
                goto Label_005D;
            }
            this.AddReward(param.iname, 0x800L, param.rare, num);
            goto Label_0072;
        Label_005D:
            this.AddReward(param.iname, 1L, param.rare, num);
        Label_0072:
            return;
        }

        private void AddReward(string iname, GiftTypes giftTipe, int rarity, int num)
        {
            GiftRecieveItemData local1;
            GiftRecieveItemData data;
            if (this.GiftRecieveItemDataDic.ContainsKey(iname) == null)
            {
                goto Label_0030;
            }
            local1 = this.GiftRecieveItemDataDic[iname];
            local1.num += num;
            goto Label_004E;
        Label_0030:
            data = new GiftRecieveItemData();
            data.Set(iname, giftTipe, rarity, num);
            this.GiftRecieveItemDataDic.Add(iname, data);
        Label_004E:
            return;
        }

        [CompilerGenerated]
        private sealed class <RewardData>c__AnonStorey39C
        {
            internal ItemData itemData;

            public <RewardData>c__AnonStorey39C()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3F3(UnitData u)
            {
                return (u.UnitParam.iname == this.itemData.ItemID);
            }
        }
    }
}

