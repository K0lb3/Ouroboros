// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRoundRewardParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class TowerRoundRewardParam : TowerRewardParam
  {
    public List<byte> mRoundList;

    public void Deserialize(JSON_TowerRoundRewardParam json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.iname = json.iname;
      this.mRoundList = new List<byte>();
      if (json.rewards == null)
        return;
      this.mTowerRewardItems = new List<TowerRewardItem>();
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        TowerRewardItem towerRewardItem = new TowerRewardItem();
        towerRewardItem.Deserialize((JSON_TowerRewardItem) json.rewards[index]);
        this.mTowerRewardItems.Add(towerRewardItem);
        this.mRoundList.Add(json.rewards[index].round_num);
      }
    }

    public override List<TowerRewardItem> GetTowerRewardItem()
    {
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      byte round = towerResuponse.round;
      List<TowerRewardItem> towerRewardItemList = new List<TowerRewardItem>();
      for (int index = 0; index < this.mRoundList.Count; ++index)
      {
        if ((int) towerResuponse.round >= (int) round)
          towerRewardItemList.Add(this.mTowerRewardItems[index]);
      }
      return towerRewardItemList;
    }
  }
}
