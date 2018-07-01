// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RewardApCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(0, "Check", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(3, "Overflow", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(2, "AlreadyCapped", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("Check/RewardApCheck", 32741)]
  public class FlowNode_RewardApCheck : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      RewardData rewardData = GlobalVars.LastReward.Get();
      if (rewardData == null || rewardData.Stamina < 1)
      {
        this.ActivateOutputLinks(1);
      }
      else
      {
        PlayerData player = MonoSingleton<GameManager>.GetInstanceDirect().Player;
        if (rewardData.Stamina > player.StaminaStockCap)
          this.ActivateOutputLinks(3);
        else if (player.Stamina >= player.StaminaStockCap)
        {
          if (rewardData.Exp > 0 || rewardData.Gold > 0 || (rewardData.Coin > 0 || rewardData.ArenaMedal > 0) || (rewardData.MultiCoin > 0 || rewardData.KakeraCoin > 0 || rewardData.Items.Count > 0))
            this.ActivateOutputLinks(3);
          else
            this.ActivateOutputLinks(2);
        }
        else if (player.Stamina + rewardData.Stamina <= player.StaminaStockCap)
          this.ActivateOutputLinks(1);
        else
          this.ActivateOutputLinks(3);
      }
    }
  }
}
