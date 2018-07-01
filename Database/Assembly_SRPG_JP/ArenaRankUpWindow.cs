// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaRankUpWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ArenaRankUpWindow : MonoBehaviour
  {
    public Text OldRank;
    public Text NewRank;
    public Text DeltaRank;

    public ArenaRankUpWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Equality((Object) SceneBattle.Instance, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      ArenaBattleResponse arenaBattleResponse = GlobalVars.ResultArenaBattleResponse;
      int num = instance.Player.ArenaRankBest - arenaBattleResponse.new_rank;
      DataSource.Bind<RewardData>(((Component) this).get_gameObject(), new RewardData()
      {
        ArenaMedal = arenaBattleResponse.reward_info.arenacoin,
        Coin = arenaBattleResponse.reward_info.coin
      });
      this.OldRank.set_text(instance.Player.ArenaRankBest.ToString());
      this.NewRank.set_text(arenaBattleResponse.new_rank.ToString());
      this.DeltaRank.set_text(num.ToString());
      ((Behaviour) this.DeltaRank).set_enabled(num > 0);
      RewardWindow componentInChildren = (RewardWindow) ((Component) this).GetComponentInChildren<RewardWindow>();
      if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.Refresh();
    }
  }
}
