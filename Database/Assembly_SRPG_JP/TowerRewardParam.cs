// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
