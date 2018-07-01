// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class VersusRankReward
  {
    private RewardType mType;
    private string mIName;
    private int mNum;

    public RewardType Type
    {
      get
      {
        return this.mType;
      }
    }

    public string IName
    {
      get
      {
        return this.mIName;
      }
    }

    public int Num
    {
      get
      {
        return this.mNum;
      }
    }

    public bool Deserialize(JSON_VersusRankRewardRewardParam json)
    {
      this.mType = (RewardType) json.item_type;
      this.mIName = json.item_iname;
      this.mNum = json.item_num;
      return true;
    }
  }
}
