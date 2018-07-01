// Decompiled with JetBrains decompiler
// Type: SRPG.VipParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class VipParam
  {
    public int PlayerLevel;
    public int NextRankNeedPoint;
    public int Ticket;
    public int BuyCoinBonus;
    public int BuyCoinNum;
    public int BuyStaminaNum;
    public int ResetEliteNum;
    public int ResetArenaNum;

    public bool Deserialize(JSON_VipParam json)
    {
      if (json == null)
        return false;
      this.NextRankNeedPoint = json.exp;
      this.Ticket = json.ticket;
      this.BuyCoinBonus = json.buy_coin_bonus;
      this.BuyCoinNum = json.buy_coin_num;
      this.BuyStaminaNum = json.buy_stmn_num;
      this.ResetEliteNum = json.reset_elite;
      this.ResetArenaNum = json.reset_arena;
      return true;
    }
  }
}
