// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRewardItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class TowerRewardItem
  {
    public string iname;
    public int num;
    public TowerRewardItem.RewardType type;
    public bool visible;
    public bool is_new;

    public void Deserialize(JSON_TowerRewardItem json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.iname = json.iname;
      this.type = (TowerRewardItem.RewardType) json.type;
      this.num = (int) json.num;
      this.visible = (int) json.visible == 1;
    }

    public enum RewardType : byte
    {
      Item,
      Gold,
      Coin,
      ArenaCoin,
      MultiCoin,
      KakeraCoin,
      Artifact,
    }
  }
}
