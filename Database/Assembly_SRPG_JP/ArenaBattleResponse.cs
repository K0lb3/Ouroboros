// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaBattleResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ArenaBattleResponse
  {
    public Json_ArenaRewardInfo reward_info;
    public int new_rank;
    public int def_rank;
    public int got_pexp;
    public int got_uexp;
    public int got_gold;

    public void Deserialize(Json_ArenaBattleResponse json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.new_rank = json.new_rank;
      this.def_rank = json.def_rank;
      this.got_pexp = json.got_pexp;
      this.got_uexp = json.got_uexp;
      this.got_gold = json.got_gold;
      this.reward_info = new Json_ArenaRewardInfo();
      if (json.reward_info == null)
        return;
      this.reward_info.gold = json.reward_info.gold;
      this.reward_info.coin = json.reward_info.coin;
      this.reward_info.arenacoin = json.reward_info.arenacoin;
    }
  }
}
