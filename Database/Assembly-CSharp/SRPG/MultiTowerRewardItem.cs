// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerRewardItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class MultiTowerRewardItem
  {
    public int round_st;
    public int round_ed;
    public MultiTowerRewardItem.RewardType type;
    public string itemname;
    public int num;

    public void Deserialize(JSON_MultiTowerRewardItem json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.round_st = json.round_st;
      this.round_ed = json.round_ed;
      this.itemname = json.itemname;
      this.num = json.num;
      this.type = (MultiTowerRewardItem.RewardType) json.type;
    }

    public enum RewardType : byte
    {
      None,
      Item,
      Coin,
      Artifact,
      Award,
      Unit,
      Gold,
    }
  }
}
