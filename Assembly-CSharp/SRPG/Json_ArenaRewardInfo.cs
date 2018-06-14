// Decompiled with JetBrains decompiler
// Type: SRPG.Json_ArenaRewardInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class Json_ArenaRewardInfo
  {
    public int gold;
    public int coin;
    public int arenacoin;
    public Json_Item[] items;

    public bool IsReward()
    {
      if (this.gold > 0 || this.coin > 0 || this.arenacoin > 0)
        return true;
      if (this.items != null)
        return this.items.Length > 0;
      return false;
    }
  }
}
