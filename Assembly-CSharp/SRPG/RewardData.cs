// Decompiled with JetBrains decompiler
// Type: SRPG.RewardData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class RewardData
  {
    public List<ItemData> Items = new List<ItemData>();
    public List<ArtifactRewardData> Artifacts = new List<ArtifactRewardData>();
    public List<int> ItemsBeforeAmount = new List<int>();
    public Dictionary<string, GiftRecieveItemData> GiftRecieveItemDataDic = new Dictionary<string, GiftRecieveItemData>();
    public int Exp;
    public int Gold;
    public int Coin;
    public int ArenaMedal;
    public int MultiCoin;
    public int KakeraCoin;
    public int Stamina;

    public RewardData()
    {
    }

    public RewardData(TrophyParam trophy)
    {
      this.Exp = trophy.Exp;
      this.Coin = trophy.Coin;
      this.Gold = trophy.Gold;
      this.Stamina = trophy.Stamina;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      foreach (TrophyParam.RewardItem rewardItem in trophy.Items)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        RewardData.\u003CRewardData\u003Ec__AnonStorey377 dataCAnonStorey377 = new RewardData.\u003CRewardData\u003Ec__AnonStorey377();
        // ISSUE: reference to a compiler-generated field
        dataCAnonStorey377.itemData = new ItemData();
        // ISSUE: reference to a compiler-generated field
        if (dataCAnonStorey377.itemData.Setup(0L, rewardItem.iname, rewardItem.Num))
        {
          // ISSUE: reference to a compiler-generated field
          this.Items.Add(dataCAnonStorey377.itemData);
          // ISSUE: reference to a compiler-generated field
          if (dataCAnonStorey377.itemData.Param.type != EItemType.Unit)
          {
            // ISSUE: reference to a compiler-generated field
            ItemData itemDataByItemId = instance.Player.FindItemDataByItemID(dataCAnonStorey377.itemData.Param.iname);
            this.ItemsBeforeAmount.Add(itemDataByItemId == null ? 0 : itemDataByItemId.Num);
          }
          else
          {
            // ISSUE: reference to a compiler-generated method
            this.ItemsBeforeAmount.Add(instance.Player.Units.Find(new Predicate<UnitData>(dataCAnonStorey377.\u003C\u003Em__403)) == null ? 0 : 1);
          }
        }
      }
      foreach (TrophyParam.RewardItem artifact in trophy.Artifacts)
        this.Artifacts.Add(new ArtifactRewardData()
        {
          ArtifactParam = instance.MasterParam.GetArtifactParam(artifact.iname),
          Num = artifact.Num
        });
    }

    public void AddReward(ArtifactParam param, int num)
    {
      if (param == null)
        return;
      this.AddReward(param.iname, GiftTypes.Artifact, param.rareini, num);
    }

    public void AddReward(ItemParam param, int num)
    {
      if (param == null)
        return;
      if (param.type == EItemType.Unit)
        this.AddReward(param.iname, GiftTypes.Unit, (int) param.rare, num);
      else if (param.type == EItemType.Award)
        this.AddReward(param.iname, GiftTypes.Award, (int) param.rare, num);
      else
        this.AddReward(param.iname, GiftTypes.Item, (int) param.rare, num);
    }

    private void AddReward(string iname, GiftTypes giftTipe, int rarity, int num)
    {
      if (this.GiftRecieveItemDataDic.ContainsKey(iname))
      {
        this.GiftRecieveItemDataDic[iname].num += num;
      }
      else
      {
        GiftRecieveItemData giftRecieveItemData = new GiftRecieveItemData();
        giftRecieveItemData.Set(iname, giftTipe, rarity, num);
        this.GiftRecieveItemDataDic.Add(iname, giftRecieveItemData);
      }
    }

    public void AddReward(GiftRecieveItemData giftRecieveItemData)
    {
      if (giftRecieveItemData == null)
        return;
      if (this.GiftRecieveItemDataDic.ContainsKey(giftRecieveItemData.iname))
        this.GiftRecieveItemDataDic[giftRecieveItemData.iname].num += giftRecieveItemData.num;
      else
        this.GiftRecieveItemDataDic.Add(giftRecieveItemData.iname, giftRecieveItemData);
    }
  }
}
