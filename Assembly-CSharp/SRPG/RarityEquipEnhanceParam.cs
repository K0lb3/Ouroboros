// Decompiled with JetBrains decompiler
// Type: SRPG.RarityEquipEnhanceParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class RarityEquipEnhanceParam
  {
    public OInt rankcap;
    public OInt cost_scale;
    public RarityEquipEnhanceParam.RankParam[] ranks;

    public RarityEquipEnhanceParam.RankParam GetRankParam(int rank)
    {
      if (rank > 0 && rank <= this.ranks.Length)
        return this.ranks[rank - 1];
      return (RarityEquipEnhanceParam.RankParam) null;
    }

    public class RankParam
    {
      public ReturnItem[] return_item = new ReturnItem[3];
      public OInt need_point;
    }
  }
}
