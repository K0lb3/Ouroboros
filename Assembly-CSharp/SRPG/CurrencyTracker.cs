// Decompiled with JetBrains decompiler
// Type: SRPG.CurrencyTracker
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class CurrencyTracker
  {
    public int Gold;
    public int Coin;
    public int ArenaCoin;
    public int MultiCoin;

    public CurrencyTracker()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      this.Gold = player.Gold;
      this.Coin = player.Coin;
      this.ArenaCoin = player.ArenaCoin;
      this.MultiCoin = player.MultiCoin;
    }

    public void EndTracking()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      this.Gold = player.Gold - this.Gold;
      this.Coin = player.Coin - this.Coin;
      this.ArenaCoin = player.ArenaCoin - this.ArenaCoin;
      this.MultiCoin = player.MultiCoin - this.MultiCoin;
    }
  }
}
