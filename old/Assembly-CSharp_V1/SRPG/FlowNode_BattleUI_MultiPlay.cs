// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattleUI_MultiPlay
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(4011, "思考中隠す", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4030, "他人切断通知開始", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4031, "他人切断通知終了", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(4032, "他人切断通知強制終了", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4050, "自分切断", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4100, "スタンプWindow開始通知", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(4101, "スタンプWindow終了通知", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(4201, "操作時間延長表示", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4300, "中断復帰開始通知", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4400, "同期待ち中開始", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4301, "中断復帰終了通知", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(4001, "制限時間隠す", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1100, "復活選択開始", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1072, "敵ユニット行動終了", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4500, "強制勝利", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1071, "他人ユニット行動終了", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4501, "強制勝利ウィンドウ閉じ", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1070, "自分ユニット行動終了", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1062, "敵ユニット行動開始", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1061, "他人ユニット行動開始", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1060, "自分ユニット行動開始", FlowNode.PinTypes.Output, 0)]
  [FlowNode.NodeType("Battle_MultiPlay/Events")]
  [FlowNode.Pin(4303, "中断復帰通知強制終了", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4302, "自身の復帰完了通知", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1111, "復活選択待ち表示終了", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1120, "コンティニュー選択待ち開始", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1121, "コンティニュー選択待ち終了", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1200, "クエスト勝利", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1201, "クエスト敗北", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1202, "クエスト中断", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4502, "強制勝利強制終了", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1300, "マップ開始", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4401, "同期待ち中終了", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1301, "マップ終了", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4000, "制限時間表示", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4010, "思考中表示", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4600, "対戦終了済み", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4601, "対戦終了済み終了", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(4304, "自身の復帰通知強制終了", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1110, "復活選択待ち表示開始", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1101, "コンティニュー選択開始", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_BattleUI_MultiPlay : FlowNodePersistent
  {
    public float inputSec = 20f;
    public bool CheckRandCheat = true;

    public void OnMyUnitStart()
    {
      this.ActivateOutputLinks(1060);
    }

    public void OnOtherUnitStart()
    {
      this.ActivateOutputLinks(1061);
    }

    public void OnEnemyUnitStart()
    {
      this.ActivateOutputLinks(1062);
    }

    public void OnMyUnitEnd()
    {
      this.ActivateOutputLinks(1070);
    }

    public void OnOtherUnitEnd()
    {
      this.ActivateOutputLinks(1071);
    }

    public void OnEnemyUnitEnd()
    {
      this.ActivateOutputLinks(1072);
    }

    public void StartSelectRevive()
    {
      this.ActivateOutputLinks(1100);
    }

    public void StartSelectContinue()
    {
      this.ActivateOutputLinks(1101);
    }

    public void ShowWaitRevive()
    {
      this.ActivateOutputLinks(1110);
    }

    public void HideWaitRevive()
    {
      this.ActivateOutputLinks(1111);
    }

    public void ShowWaitContinue()
    {
      this.ActivateOutputLinks(1120);
    }

    public void HideWaitContinue()
    {
      this.ActivateOutputLinks(1121);
    }

    public void OnQuestWin()
    {
      this.ActivateOutputLinks(1200);
    }

    public void OnQuestLose()
    {
      this.ActivateOutputLinks(1201);
    }

    public void OnQuestRetreat()
    {
      this.ActivateOutputLinks(1202);
    }

    public void OnMapStart()
    {
      this.ActivateOutputLinks(1300);
    }

    public void OnMapEnd()
    {
      this.ActivateOutputLinks(1301);
    }

    public void ShowInputTimeLimit()
    {
      this.ActivateOutputLinks(4000);
    }

    public void HideInputTimeLimit()
    {
      this.ActivateOutputLinks(4001);
    }

    public void ShowThinking()
    {
      this.ActivateOutputLinks(4010);
    }

    public void HideThinking()
    {
      this.ActivateOutputLinks(4011);
    }

    public void OnOtherDisconnected()
    {
      this.ActivateOutputLinks(4030);
    }

    public void OnOtherDisconnectedForce()
    {
      this.ActivateOutputLinks(4032);
    }

    public void OnMyDisconnected()
    {
      this.ActivateOutputLinks(4050);
    }

    public void OnExtInput()
    {
      this.ActivateOutputLinks(4201);
    }

    public void OnOtherPlayerResume()
    {
      this.ActivateOutputLinks(4300);
    }

    public void OnOtherPlayerResumeClose()
    {
      this.ActivateOutputLinks(4303);
    }

    public void OnMyPlayerResume()
    {
      this.ActivateOutputLinks(4302);
    }

    public void OnMyPlayerResumeClose()
    {
      this.ActivateOutputLinks(4304);
    }

    public void OnOtherPlayerSyncStart()
    {
      this.ActivateOutputLinks(4400);
    }

    public void OnOtherPlayerSyncEnd()
    {
      this.ActivateOutputLinks(4401);
    }

    public void OnForceWin()
    {
      this.ActivateOutputLinks(4500);
    }

    public void OnForceWinClose()
    {
      this.ActivateOutputLinks(4502);
    }

    public void OnAlreadyFinish()
    {
      this.ActivateOutputLinks(4600);
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 4031:
          SceneBattle instance1 = SceneBattle.Instance;
          if (!Object.op_Inequality((Object) instance1, (Object) null))
            break;
          instance1.CurrentNotifyDisconnectedPlayer = (JSON_MyPhotonPlayerParam) null;
          break;
        case 4100:
          this.StampWindowIsOpened = true;
          break;
        case 4101:
          this.StampWindowIsOpened = false;
          break;
        case 4301:
          SceneBattle instance2 = SceneBattle.Instance;
          if (!Object.op_Inequality((Object) instance2, (Object) null))
            break;
          instance2.CurrentResumePlayer = (JSON_MyPhotonPlayerParam) null;
          break;
        case 4501:
          SceneBattle instance3 = SceneBattle.Instance;
          if (!Object.op_Inequality((Object) instance3, (Object) null))
            break;
          instance3.Battle.IsVSForceWinComfirm = true;
          break;
        case 4601:
          SceneBattle instance4 = SceneBattle.Instance;
          if (!Object.op_Inequality((Object) instance4, (Object) null))
            break;
          instance4.AlreadyEndBattle = true;
          break;
      }
    }

    public bool StampWindowIsOpened { get; set; }

    public BattleStamp StampWindow
    {
      get
      {
        BattleStamp[] componentsInChildren = (BattleStamp[]) ((Component) this).get_gameObject().GetComponentsInChildren<BattleStamp>(true);
        if (componentsInChildren == null || componentsInChildren.Length <= 0)
          return (BattleStamp) null;
        return componentsInChildren[0];
      }
    }
  }
}
