// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattleUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1201, "方向入力完了", FlowNode.PinTypes.Output, 1201)]
  [FlowNode.Pin(1300, "スキル選択開始", FlowNode.PinTypes.Output, 1300)]
  [FlowNode.Pin(1301, "スキル選択終了", FlowNode.PinTypes.Output, 1301)]
  [FlowNode.Pin(1400, "アイテム選択開始", FlowNode.PinTypes.Output, 1400)]
  [FlowNode.Pin(1401, "アイテム選択終了", FlowNode.PinTypes.Output, 1401)]
  [FlowNode.Pin(1500, "入力打ち切り (マルチプレイ)", FlowNode.PinTypes.Output, 1500)]
  [FlowNode.Pin(1501, "BANされた (マルチプレイ)", FlowNode.PinTypes.Output, 1501)]
  [FlowNode.Pin(1502, "切断された (マルチプレイ)", FlowNode.PinTypes.Output, 1502)]
  [FlowNode.Pin(1503, "進行異常 (マルチプレイ)", FlowNode.PinTypes.Output, 1503)]
  [FlowNode.ShowInInspector]
  [FlowNode.Pin(7103, "ユニット交代確認終了", FlowNode.PinTypes.Output, 7103)]
  [FlowNode.Pin(7102, "ユニット交代確認開始", FlowNode.PinTypes.Output, 7102)]
  [FlowNode.Pin(7101, "ユニット交代選択終了", FlowNode.PinTypes.Output, 7101)]
  [FlowNode.Pin(7100, "ユニット交代選択開始", FlowNode.PinTypes.Output, 7100)]
  [FlowNode.Pin(7002, "パネルからターゲット切り替え", FlowNode.PinTypes.Input, 7002)]
  [FlowNode.Pin(7001, "ギミックからターゲット切り替え", FlowNode.PinTypes.Input, 7001)]
  [FlowNode.Pin(7000, "ユニットからターゲット切り替え", FlowNode.PinTypes.Input, 7000)]
  [FlowNode.Pin(6000, "フレンド選択", FlowNode.PinTypes.Output, 6000)]
  [FlowNode.Pin(5000, "購入ウインドウ表示", FlowNode.PinTypes.Output, 5000)]
  [FlowNode.Pin(2001, "バトル終了", FlowNode.PinTypes.Output, 2001)]
  [FlowNode.Pin(2000, "バトル開始", FlowNode.PinTypes.Output, 2000)]
  [FlowNode.Pin(1550, "初顔合わせ通知", FlowNode.PinTypes.Output, 1550)]
  [FlowNode.Pin(1600, "グリッド上イベント選択開始", FlowNode.PinTypes.Output, 1600)]
  [FlowNode.Pin(1601, "グリッド上イベント選択終了", FlowNode.PinTypes.Output, 1601)]
  [FlowNode.Pin(1035, "投げるターゲット選択開始", FlowNode.PinTypes.Output, 1035)]
  [FlowNode.Pin(1032, "ターゲット選択終了", FlowNode.PinTypes.Output, 1032)]
  [FlowNode.Pin(1030, "ターゲット選択開始", FlowNode.PinTypes.Output, 1030)]
  [FlowNode.Pin(1020, "コマンドが選択された", FlowNode.PinTypes.Output, 1020)]
  [FlowNode.Pin(1010, "コマンド選択開始", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1006, "マップ終了", FlowNode.PinTypes.Output, 1006)]
  [FlowNode.Pin(1005, "マップ開始", FlowNode.PinTypes.Output, 1005)]
  [FlowNode.Pin(1002, "クエスト終了 (敗亡)", FlowNode.PinTypes.Output, 1002)]
  [FlowNode.Pin(1001, "クエスト終了 (勝利)", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(1000, "クエスト開始", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.NodeType("Battle/Events")]
  [FlowNode.Pin(1036, "投げるターゲット選択終了", FlowNode.PinTypes.Output, 1036)]
  [FlowNode.Pin(1040, "移動先の選択開始", FlowNode.PinTypes.Output, 1040)]
  [FlowNode.Pin(1041, "移動先の選択完了", FlowNode.PinTypes.Output, 1041)]
  [FlowNode.Pin(1050, "マップ確認開始", FlowNode.PinTypes.Output, 1050)]
  [FlowNode.Pin(1060, "ユニット行動開始", FlowNode.PinTypes.Output, 1060)]
  [FlowNode.Pin(1070, "詠唱スキル発動", FlowNode.PinTypes.Output, 1070)]
  [FlowNode.Pin(1100, "メインターゲット選択", FlowNode.PinTypes.Output, 1100)]
  [FlowNode.Pin(1101, "メインターゲット選択解除", FlowNode.PinTypes.Output, 1101)]
  [FlowNode.Pin(1110, "サブターゲット選択", FlowNode.PinTypes.Output, 1110)]
  [FlowNode.Pin(1111, "サブターゲット選択解除", FlowNode.PinTypes.Output, 1111)]
  [FlowNode.Pin(1200, "方向入力開始", FlowNode.PinTypes.Output, 1200)]
  public class FlowNode_BattleUI : FlowNodePersistent
  {
    [StringIsGameObjectID]
    public string CommandObjectID;
    [StringIsGameObjectID]
    public string QueueObjectID;
    [StringIsGameObjectID]
    public string SkillListID;
    [StringIsGameObjectID]
    public string ItemListID;
    [StringIsGameObjectID]
    public string VirtualStickID;
    [StringIsGameObjectID]
    public string QuestStatusID;
    [StringIsGameObjectID]
    public string CameraControllerID;
    [StringIsGameObjectID]
    public string FukanCameraID;
    [StringIsGameObjectID]
    public string NextTargetButtonID;
    [StringIsGameObjectID]
    public string PrevTargetButtonID;
    [StringIsGameObjectID]
    public string MapHeightID;
    [StringIsGameObjectID]
    public string ElementDiagram;
    [StringIsGameObjectID]
    public string ArenaActionCountID;
    [StringIsGameObjectID]
    public string PlayerActionCountID;
    [StringIsGameObjectID]
    public string EnemyActionCountID;
    [StringIsGameObjectID]
    public string RankingQuestActionCountID;
    [StringIsGameObjectID]
    public string UnitChgListID;
    [StringIsGameObjectID]
    public string WeatherInfoID;
    public TargetPlate Prefab_TargetMain;
    public TargetPlate Prefab_TargetSub;
    public TargetPlate Prefab_ObjectTarget;
    public TargetPlate Prefab_TrickTarget;
    [NonSerialized]
    public TargetPlate TargetMain;
    [NonSerialized]
    public TargetPlate TargetSub;
    [NonSerialized]
    public TargetPlate TargetObjectSub;
    [NonSerialized]
    public TargetPlate TargetTrickSub;
    [StringIsLocalEventID]
    public string QuestStart;
    [StringIsLocalEventID]
    public string QuestStart_Short;
    [StringIsLocalEventID]
    public string QuestStart_Arena;
    [StringIsLocalEventID]
    public string QuestStart_VS;
    [StringIsLocalEventID]
    public string QuestStart_RankMatch;
    [StringIsLocalEventID]
    public string QuestEnd;
    [StringIsLocalEventID]
    public string QuestWin;
    [StringIsLocalEventID]
    public string QuestWin_Arena;
    [StringIsLocalEventID]
    public string QuestWin_Versus;
    [StringIsLocalEventID]
    public string QuestLose;
    [StringIsLocalEventID]
    public string QuestLose_Arena;
    [StringIsLocalEventID]
    public string QuestLose_Versus;
    [StringIsLocalEventID]
    public string QuestDraw_Versus;
    [StringIsLocalEventID]
    public string QuestWin_Audience;
    [StringIsLocalEventID]
    public string Result;
    [StringIsLocalEventID]
    public string Result_MP;
    [StringIsLocalEventID]
    public string Result_Arena;
    [StringIsLocalEventID]
    public string Result_Tower;
    [StringIsLocalEventID]
    public string Result_Versus;
    [StringIsLocalEventID]
    public string Result_MultiTower;
    [StringIsLocalEventID]
    public string Result_RankMatch;
    [StringIsLocalEventID]
    public string PlayAgain;
    [StringIsLocalEventID]
    public string MapViewStart;
    [StringIsLocalEventID]
    public string MapViewEnd;
    [StringIsLocalEventID]
    public string MapViewSelectUnit;
    [StringIsLocalEventID]
    public string MapViewSelectGrid;
    [StringIsLocalEventID]
    public string VersusStart;
    [StringIsLocalEventID]
    public string VersusEnd;
    [StringIsLocalEventID]
    public string MPSelectContinueWaitingStart;
    [StringIsLocalEventID]
    public string MPSelectContinueWaitingEnd;
    private VirtualStick2 mVirtualStick;
    private UnitAbilitySkillList mSkillListRef;
    private UnitCommands mCommandWindow;
    private BattleInventory mItemWindow;
    private BattleUnitChg mUnitChgWindow;
    public bool IsEnableUnit;
    public bool IsEnableGimmick;
    public bool IsEnableTrick;

    private T UpdateReference<T>(ref T obj, string path) where T : Component
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) (object) obj, (UnityEngine.Object) null))
      {
        GameObject gameObject = GameObjectID.FindGameObject(path);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        {
          Debug.LogError((object) (path + " は存在しません"));
          return (T) null;
        }
        obj = (T) gameObject.GetComponent(typeof (T));
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) (object) obj, (UnityEngine.Object) null))
        {
          Debug.LogError((object) (path + " は " + (object) typeof (T) + " を含みません"));
          return (T) null;
        }
      }
      return obj;
    }

    public VirtualStick2 VirtualStick
    {
      get
      {
        return this.UpdateReference<VirtualStick2>(ref this.mVirtualStick, this.VirtualStickID);
      }
    }

    public UnitAbilitySkillList SkillWindow
    {
      get
      {
        return this.UpdateReference<UnitAbilitySkillList>(ref this.mSkillListRef, this.SkillListID);
      }
    }

    public UnitCommands CommandWindow
    {
      get
      {
        return this.UpdateReference<UnitCommands>(ref this.mCommandWindow, this.CommandObjectID);
      }
    }

    public BattleInventory ItemWindow
    {
      get
      {
        return this.UpdateReference<BattleInventory>(ref this.mItemWindow, this.ItemListID);
      }
    }

    public BattleUnitChg UnitChgWindow
    {
      get
      {
        return this.UpdateReference<BattleUnitChg>(ref this.mUnitChgWindow, this.UnitChgListID);
      }
    }

    private void Output(int pinID)
    {
      this.ActivateOutputLinks(pinID);
    }

    public void OnQuestStart()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.QuestStart);
    }

    public void OnQuestStart_Short()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.QuestStart_Short);
    }

    public void OnQuestStart_Arena()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.QuestStart_Arena);
    }

    public void OnQuestStart_VS()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.QuestStart_VS);
    }

    public void OnQuestStart_RankMatch()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.QuestStart_RankMatch);
    }

    public void OnQuestEnd()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.QuestEnd);
    }

    public void OnQuestWin()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.QuestWin);
    }

    public void OnQuestLose()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.QuestLose);
    }

    public void OnArenaWin()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.QuestWin_Arena);
    }

    public void OnArenaLose()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.QuestLose_Arena);
    }

    public void OnVersusWin()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.QuestWin_Versus);
    }

    public void OnVersusLose()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.QuestLose_Versus);
    }

    public void OnVersusDraw()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.QuestDraw_Versus);
    }

    public void OnAudienceWin()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.QuestWin_Audience);
    }

    public void OnResult()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.Result);
    }

    public void OnResult_MP()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.Result_MP);
    }

    public void OnResult_Arena()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.Result_Arena);
    }

    public void OnResult_Tower()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.Result_Tower);
    }

    public void OnResult_Versus()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.Result_Versus);
    }

    public void OnResult_MultiTower()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.Result_MultiTower);
    }

    public void OnResult_RankMatch()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.Result_RankMatch);
    }

    public void OnMapViewStart()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.MapViewStart);
    }

    public void OnMapViewEnd()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.MapViewEnd);
    }

    public void OnMapViewSelectUnit()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.MapViewSelectUnit);
    }

    public void OnMapViewSelectGrid()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.MapViewSelectGrid);
    }

    public void OnVersusStart()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.VersusStart);
    }

    public void OnVersusEnd()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.VersusEnd);
    }

    public void OnMPSelectContinueWaitingStart()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.MPSelectContinueWaitingStart);
    }

    public void OnMPSelectContinueWaitingEnd()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.MPSelectContinueWaitingEnd);
    }

    public void OnMapStart()
    {
      this.Output(1005);
    }

    public void OnMapEnd()
    {
      this.Output(1006);
    }

    public void OnUnitStart()
    {
      this.Output(1060);
    }

    public void OnCastSkillStart()
    {
      this.Output(1070);
    }

    public void OnCommandSelectStart()
    {
      this.Output(1010);
    }

    public void OnCommandSelect()
    {
      this.Output(1020);
    }

    public void OnInputMoveStart()
    {
      this.Output(1040);
    }

    public void OnInputMoveEnd()
    {
      this.Output(1041);
    }

    public void OnMapModeStart()
    {
      this.Output(1050);
    }

    public void OnThrowTargetSelectStart()
    {
      this.Output(1035);
    }

    public void OnThrowTargetSelectEnd()
    {
      this.Output(1036);
    }

    public void OnTargetSelectStart()
    {
      this.Output(1030);
    }

    public void OnTargetSelectEnd()
    {
      this.Output(1032);
    }

    public void OnMainTargetSelect()
    {
      this.Output(1100);
    }

    public void OnMainTargetDeselect()
    {
      this.Output(1101);
    }

    public void OnSubTargetSelect()
    {
      this.Output(1110);
    }

    public void OnSubTargetDeselect()
    {
      this.Output(1111);
    }

    public bool IsInputDirectionStarted { get; private set; }

    public void OnInputDirectionStart()
    {
      this.Output(1200);
      this.IsInputDirectionStarted = true;
    }

    public void OnInputDirectionEnd()
    {
      this.Output(1201);
      this.IsInputDirectionStarted = false;
    }

    public void OnSkillSelectStart()
    {
      this.Output(1300);
    }

    public void OnSkillSelectEnd()
    {
      this.Output(1301);
    }

    public void OnItemSelectStart()
    {
      this.Output(1400);
    }

    public void OnItemSelectEnd()
    {
      this.Output(1401);
    }

    public void OnInputTimeLimit()
    {
      this.Output(1500);
    }

    public void OnDisconnected()
    {
      this.Output(1502);
    }

    public void OnBan()
    {
      this.Output(1501);
    }

    public void OnSequenceError()
    {
      this.Output(1503);
    }

    public void OnGridEventSelectStart()
    {
      this.Output(1600);
    }

    public void OnGridEventSelectEnd()
    {
      this.Output(1601);
    }

    public void OnBattleStart()
    {
      this.Output(2000);
    }

    public void OnBattleEnd()
    {
      this.Output(2001);
    }

    public void OnSupportSelectStart()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.PlayAgain);
    }

    public void OnUnitChgSelectStart()
    {
      this.Output(7100);
    }

    public void OnUnitChgSelectEnd()
    {
      this.Output(7101);
    }

    public void OnUnitChgConfirmStart()
    {
      this.Output(7102);
    }

    public void OnUnitChgConfirmEnd()
    {
      this.Output(7103);
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_TargetMain, (UnityEngine.Object) null))
      {
        this.TargetMain = (TargetPlate) UnityEngine.Object.Instantiate<TargetPlate>((M0) this.Prefab_TargetMain);
        ((Component) this.TargetMain).get_transform().SetParent(((Component) this).get_transform(), false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_TargetSub, (UnityEngine.Object) null))
      {
        this.TargetSub = (TargetPlate) UnityEngine.Object.Instantiate<TargetPlate>((M0) this.Prefab_TargetSub);
        ((Component) this.TargetSub).get_transform().SetParent(((Component) this).get_transform(), false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_ObjectTarget, (UnityEngine.Object) null))
      {
        this.TargetObjectSub = (TargetPlate) UnityEngine.Object.Instantiate<TargetPlate>((M0) this.Prefab_ObjectTarget);
        ((Component) this.TargetObjectSub).get_transform().SetParent(((Component) this).get_transform(), false);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_TrickTarget, (UnityEngine.Object) null))
        return;
      this.TargetTrickSub = (TargetPlate) UnityEngine.Object.Instantiate<TargetPlate>((M0) this.Prefab_TrickTarget);
      ((Component) this.TargetTrickSub).get_transform().SetParent(((Component) this).get_transform(), false);
    }

    public void Hide()
    {
      CanvasGroup component = (CanvasGroup) ((Component) this).GetComponent<CanvasGroup>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.set_alpha(0.0f);
      component.set_blocksRaycasts(false);
    }

    public void Show()
    {
      CanvasGroup component = (CanvasGroup) ((Component) this).GetComponent<CanvasGroup>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.set_alpha(1f);
      component.set_blocksRaycasts(true);
    }

    public void OnFirstContact()
    {
      this.Output(1550);
    }

    public void HideTargetWindows()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetMain, (UnityEngine.Object) null))
        this.TargetMain.ForceClose(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetSub, (UnityEngine.Object) null))
        this.TargetSub.ForceClose(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetObjectSub, (UnityEngine.Object) null))
        this.TargetObjectSub.ForceClose(true);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetTrickSub, (UnityEngine.Object) null))
        return;
      this.TargetTrickSub.ForceClose(true);
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 7000:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetSub, (UnityEngine.Object) null))
            this.TargetSub.Close();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetTrickSub, (UnityEngine.Object) null))
            break;
          this.TargetTrickSub.Open();
          this.OnMapViewSelectGrid();
          break;
        case 7001:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetObjectSub, (UnityEngine.Object) null))
            this.TargetObjectSub.Close();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetTrickSub, (UnityEngine.Object) null))
            break;
          this.TargetTrickSub.Open();
          break;
        case 7002:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetTrickSub, (UnityEngine.Object) null))
            this.TargetTrickSub.Close();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetSub, (UnityEngine.Object) null) && this.IsEnableUnit)
          {
            this.TargetSub.Open();
            this.OnMapViewSelectUnit();
            break;
          }
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetObjectSub, (UnityEngine.Object) null) || !this.IsEnableGimmick)
            break;
          this.TargetObjectSub.Open();
          break;
      }
    }

    public void ClearEnableAll()
    {
      this.IsEnableUnit = false;
      this.IsEnableGimmick = false;
      this.IsEnableTrick = false;
    }

    public bool IsNeedFlip()
    {
      if (!this.IsEnableTrick)
        return false;
      if (!this.IsEnableUnit)
        return this.IsEnableGimmick;
      return true;
    }
  }
}
