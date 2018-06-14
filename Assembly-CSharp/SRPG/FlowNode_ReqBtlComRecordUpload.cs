// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqBtlComRecordUpload
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(102, "クエストクリア編成情報機能メンテナンス中", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/BtlComRecordUpload", 32741)]
  [FlowNode.Pin(101, "Failed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(103, "クエストクリア編成情報アップロード制限中", FlowNode.PinTypes.Output, 13)]
  public class FlowNode_ReqBtlComRecordUpload : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
        this.Success();
      else
        this.RequestUpload();
    }

    private void Success()
    {
      this.ActivateOutputLinks(100);
    }

    private void RequestUpload()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      BattleCore battle = SceneBattle.Instance.Battle;
      string fuid = player.FUID;
      long btlId = battle.BtlID;
      BattleCore.Record questRecord = battle.GetQuestRecord();
      int[] beats = new int[questRecord.drops.Length];
      for (int index = 0; index < questRecord.drops.Length; ++index)
        beats[index] = (int) questRecord.drops[index];
      int[] itemSteals = new int[questRecord.item_steals.Length];
      for (int index = 0; index < questRecord.item_steals.Length; ++index)
        itemSteals[index] = (int) questRecord.item_steals[index];
      int[] goldSteals = new int[questRecord.gold_steals.Length];
      for (int index = 0; index < questRecord.gold_steals.Length; ++index)
        goldSteals[index] = (int) questRecord.gold_steals[index];
      int[] missions = new int[questRecord.bonusCount];
      for (int index = 0; index < missions.Length; ++index)
        missions[index] = (questRecord.allBonusFlags & 1 << index) == 0 ? 0 : 1;
      int[] achieved = new int[3]{ !battle.PlayByManually ? 1 : 0, !battle.IsAllAlive() ? 0 : 1, 0 };
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      bool flag = true;
      if (quest.bonusObjective != null)
      {
        for (int index = 0; index < questRecord.bonusCount; ++index)
        {
          if ((questRecord.allBonusFlags & 1 << index) == 0)
          {
            flag = false;
            break;
          }
        }
      }
      achieved[2] = !flag ? 0 : 1;
      this.ExecRequest((WebAPI) new ReqBtlComRecordUpload(fuid, btlId, achieved, 0, BtlResultTypes.Win, beats, itemSteals, goldSteals, missions, questRecord.used_items, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.BattleRecordMaintenance:
            Network.RemoveAPI();
            Network.ResetError();
            this.ActivateOutputLinks(102);
            break;
          case Network.EErrCode.RecordLimitUpload:
            Network.RemoveAPI();
            Network.ResetError();
            this.ActivateOutputLinks(103);
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        GlobalVars.PartyUploadFinished = true;
        Network.RemoveAPI();
        this.Success();
      }
    }
  }
}
