// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class TowerRewardParam
  {
    public string iname;
    protected List<TowerRewardItem> mTowerRewardItems;

    public void Deserialize(JSON_TowerRewardParam json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.iname = json.iname;
      if (json.rewards == null)
        return;
      this.mTowerRewardItems = new List<TowerRewardItem>();
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        TowerRewardItem towerRewardItem = new TowerRewardItem();
        towerRewardItem.Deserialize(json.rewards[index]);
        this.mTowerRewardItems.Add(towerRewardItem);
      }
    }

    public virtual List<TowerRewardItem> GetTowerRewardItem()
    {
      return this.mTowerRewardItems;
    }
  }
}
